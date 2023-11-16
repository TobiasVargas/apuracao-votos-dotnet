using API.Apuracao.Client;
using API.Apuracao.Data;
using API.Apuracao.Dto;
using API.Apuracao.Entity;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace API.Apuracao.Messaging
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly JogadoresClient _jogadoresClient;
        private PgContext? _pgContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ConsumerConfig conf;
        private readonly IOptions<KafkaConfig> _config;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, JogadoresClient jogadoresClient, IServiceScopeFactory serviceScopeFactory, IOptions<KafkaConfig> config)
        {
            _logger = logger;
            _jogadoresClient = jogadoresClient;
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;

            this.conf = new ConsumerConfig
            {
                GroupId = "apuracao",
                BootstrapServers = _config.Value.BootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                ConsumeMessages();
            });
            return task;
        }

        private async void ConsumeMessages()
        {
            var scope = _serviceScopeFactory.CreateScope();
            
            using (var c = new ConsumerBuilder<Ignore, string>(this.conf).Build())
            {
                c.Subscribe("voto-computado");
                var cts = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        this._pgContext = scope.ServiceProvider.GetRequiredService<PgContext>();

                        var message = c.Consume(cts.Token);
                        var votoDTO = JsonSerializer.Deserialize<VotoDTO>(message.Message.Value);
                        _logger.LogInformation($"Mensagem: {message.Message.Value} recebida de {message.TopicPartitionOffset}");

                        if (votoDTO == null)
                        {
                            return;
                        }

                        HttpResponseMessage response = _jogadoresClient.GetById(votoDTO.IdJogador);
                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation($"Resposta: {json}");
                            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                            var jogadorDTO = JsonSerializer.Deserialize<JogadorDTO>(json, options);
                            if (jogadorDTO == null)
                            {
                                return;
                            }
                            Voto voto = new Voto
                            {
                                IdJogador = jogadorDTO.Id,
                                CamisaJogador = jogadorDTO.Camisa,
                                TimeJogador = jogadorDTO.Time,
                                NomeJogador = jogadorDTO.Nome
                            };
                            _pgContext.Add(voto);
                            await _pgContext.SaveChangesAsync();
                        }
                        else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                        {
                            _logger.LogInformation($"Voto inválido - jogador não encontrado: {votoDTO}");
                        }
                        else
                        {
                            _logger.LogInformation($"Erro ao se comunicar com o serviço de jogadores: {votoDTO}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}
