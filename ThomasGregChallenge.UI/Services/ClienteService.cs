using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ThomasGregChallenge.UI.Models;

namespace ThomasGregChallenge.UI.Services
{
    public class ClienteService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string baseAddress = configuration.GetSection("ApiUrl").Value!;

        public async Task<string> SaveClienteAsync(ClienteModel clienteModel, string tokenJwt, CancellationToken cancellationToken)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(clienteModel), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.PostAsync($"{baseAddress}api/v1/Cliente/Gravar", jsonContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<string> UpdateClienteAsync(ClienteModel clienteModel, string tokenJwt, CancellationToken cancellationToken)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(clienteModel), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.PutAsync($"{baseAddress}api/v1/Cliente/Atualizar", jsonContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<string> DeleteClienteAsync(int id, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            //var response = await _httpClient.DeleteAsync($"{baseAddress}api/v1/Cliente/Excluir?id={id}", cancellationToken);
            var response = await _httpClient.DeleteAsync($"{baseAddress}api/v1/Cliente/Excluir/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<IEnumerable<ClienteModel>> ListarClientesAsync(string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Cliente/Listar", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(content) ?? new List<ClienteModel>();
        }

        public async Task<ClienteModel> GetClientByIdAsync(int id, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Cliente/Obter/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<ClienteModel>(content);
            return result ?? new ClienteModel { Email="", Logotipo="", Nome=""};
        }

        public async Task<IEnumerable<ClienteModel>?> ObterClientesPorDescricaoAsync(string description, string tokenJwt, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

            var response = await _httpClient.GetAsync($"{baseAddress}api/v1/Cliente/ObterPorDescricao?description={description}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(content);
        }
    }
}
