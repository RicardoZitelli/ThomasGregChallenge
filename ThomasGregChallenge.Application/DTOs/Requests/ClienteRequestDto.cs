using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Application.DTOs.Requests
{
    public record ClienteRequestDto(Guid Id, string Nome, string Email, string Logotipo, IEnumerable<Logradouro>Logradouros);    
}
