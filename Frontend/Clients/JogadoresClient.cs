using Frontend.Dto;
using System.Text.Json;

namespace Frontend.Clients
{
    public class JogadoresClient
    {
        private readonly HttpClient _httpClient;

        public JogadoresClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JogadorDTO>> Get()
        {
            var response = await _httpClient.GetAsync($"/api/jogadores");
            if (!response.IsSuccessStatusCode)
            {
                // TODO adicionar validação
            }
            var json = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jogadores = JsonSerializer.Deserialize<List<JogadorDTO>>(json, options);
            if (jogadores == null)
            {
                return [];
            }
            return jogadores;
        }

        public string GetBaseUrl()
        {
            return _httpClient.BaseAddress.ToString();
        }
    }
}
