using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ThomasGregChallenge.UI.Models;

namespace ThomasGregChallenge.UI.Services
{
    public class LogradouroService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string baseAddress = configuration.GetSection("ApiUrl").Value!;

        public async Task<string> SaveLogradouroAsync(LogradouroModel logradouroModel, string tokenJwt, CancellationToken cancellationToken)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(logradouroModel), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.PostAsync($"{baseAddress}api/v1/Logradouro/Gravar", jsonContent, cancellationToken);
            var result = response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<string> UpdateLogradouroAsync(LogradouroModel logradouroModel, string tokenJwt, CancellationToken cancellationToken)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(logradouroModel), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.PutAsync($"{baseAddress}api/v1/Logradouro/Atualizar", jsonContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<string> DeleteLogradouroAsync(int id, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.DeleteAsync($"{baseAddress}api/v1/Logradouro/Excluir/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<IEnumerable<LogradouroModel>> ListarLogradourosAsync(string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Logradouro/Listar", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<IEnumerable<LogradouroModel>>(content) ?? new List<LogradouroModel>();
        }

        public async Task<LogradouroModel> GetLogradouroByIdAsync(int id, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Logradouro/Obter/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var retorno = JsonConvert.DeserializeObject<LogradouroModel>(content);
            return retorno ?? new LogradouroModel { Bairro = "", Numero="", Cidade = "", Endereco = "", Estado = "", ClienteId = 0 };
        }

        public async Task<IEnumerable<LogradouroModel>> ObterLogradourosPorDescricaoAsync(string description, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Logradouro/ObterPorDescricao?description={description}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<IEnumerable<LogradouroModel>>(content) ?? new List<LogradouroModel>();
        }

        public async Task<IEnumerable<LogradouroModel>> ObterLogradourosPorClienteAsync(int clienteId, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Logradouro/ObterPorClienteId/{clienteId}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<IEnumerable<LogradouroModel>>(content) ?? new List<LogradouroModel>();
        }
    }
}
