using Newtonsoft.Json;
using System.Text;
using ThomasGregChallenge.UI.Models;
namespace ThomasGregChallenge.UI.Services
{
    public class TokenService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));        
        private readonly string baseAddress = configuration.GetSection("ApiUrl").Value!;

        public async Task<string> AutenticateAsync(UsuarioModel usuarioModel, CancellationToken cancellationToken)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(usuarioModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseAddress}api/v1/Usuario/Autorizar", jsonContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public bool TokenExists(string token)
        {
            if (string.IsNullOrWhiteSpace(token))            
                return false;

            return true;
            
        }
    }
}
