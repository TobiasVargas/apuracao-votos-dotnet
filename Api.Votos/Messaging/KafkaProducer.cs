using Api.Votos.Dto;
using Confluent.Kafka;
using System.Text.Json;

namespace Api.Votos.Messaging
{
    public class KafkaProducer
    {
        public void Send(VotoDTO votoDTO)
        {
            var json = JsonSerializer.Serialize(votoDTO);
            var config = new ProducerConfig { BootstrapServers = "localhost:29092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer
                        .ProduceAsync("voto-computado", new Message<Null, string> { Value = json })
                        .GetAwaiter()
                        .GetResult();
        }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
