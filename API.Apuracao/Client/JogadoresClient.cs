using API.Apuracao.Dto;

namespace API.Apuracao.Client
{
    public class JogadoresClient
    {
        private readonly HttpClient _httpClient;

        public JogadoresClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpResponseMessage GetById(long id)
        {
            var response = _httpClient.GetAsync($"/api/jogadores/{id}").Result;
            return response;
        }
    }
}
