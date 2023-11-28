using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Application.Interfaces.Services
{
    public interface IClienteApplicationService
    {
        Task SaveAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid clienteId, CancellationToken cancellationToken);
        Task<ClienteResponseDto> GetByIdAsync(Guid clienteId, CancellationToken cancellationToken);
        Task<IEnumerable<ClienteResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
    }
}
