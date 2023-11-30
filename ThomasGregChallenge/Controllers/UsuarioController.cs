using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThomasGregChallenge.Application.DTOs.Requests;

namespace ThomasGregChallenge.Controllers
{
    /// <summary>
    /// Controller de autenticação
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        /// <summary>
        /// Criar Token JWT
        /// </summary>
        /// <returns>Token JWT</returns>
        
        [HttpPost("Autorizar")]
        [AllowAnonymous]
        public ActionResult GenerateJwtToken([FromBody]UsuarioRequestDto usuarioRequestDto)
        {
            if ((string.IsNullOrWhiteSpace(usuarioRequestDto.Usuario) || usuarioRequestDto.Usuario != "Admin") &&
                (string.IsNullOrWhiteSpace(usuarioRequestDto.Senha) || usuarioRequestDto.Senha != "Admin123"))
            {
                return BadRequest("Usuário não encontrado");
            }

            var secret = _configuration.GetSection("JwtSecret")?.Value; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, "Administrator"),
                    new(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)                
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return Ok(tokenHandler.WriteToken(token));            
        }
    }
}
