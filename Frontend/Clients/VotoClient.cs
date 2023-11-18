using Frontend.Dto;
using System.Net.Http.Json;

namespace Frontend.Clients
{
    public class VotoClient
    {
        private readonly HttpClient _httpClient;

        public VotoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string GetBaseUrl()
        {
            return _httpClient.BaseAddress.ToString();
        }

        public async void Post(VotoDTO votoDTO)
        {
            await _httpClient.PostAsJsonAsync<VotoDTO>("/api/voto", votoDTO);
        }
    }
}
