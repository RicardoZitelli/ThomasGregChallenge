using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Application.DTOs.Responses
{  
    public record ClienteResponseDto(string Nome, string Email, string Logotipo, List<Logradouro> Logradouros);
    
}
