using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Application.DTOs.Responses
{  
    public sealed record ClienteResponseDto(int Id, string Nome, string Email, string Logotipo);
    
}
