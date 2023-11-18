using Frontend.Dto;
using System.Text.Json;

namespace Frontend.Clients
{
    public class ApuracaoClient
    {
        private readonly HttpClient _httpClient;

        public ApuracaoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultadoApuracaoDTO>> Get()
        {
            var response = await _httpClient.GetAsync("/api/Apuracao");
            if (!response.IsSuccessStatusCode)
            {
                // TODO validar depois
            }
            var json = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var resultados = JsonSerializer.Deserialize<List<ResultadoApuracaoDTO>>(json, options);
            if (resultados == null)
            {
                return [];
            }
            return resultados;
        }
    }
}
