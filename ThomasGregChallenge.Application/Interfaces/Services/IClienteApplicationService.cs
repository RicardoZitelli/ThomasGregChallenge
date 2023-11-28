using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;

namespace ThomasGregChallenge.Application.Interfaces.Services
{
    public interface IClienteApplicationService
    {
        Task SaveAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(int clienteId, CancellationToken cancellationToken);
        Task<ClienteResponseDto> GetByIdAsync(int clienteId, CancellationToken cancellationToken);
        Task<IEnumerable<ClienteResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
        Task<IEnumerable<ClienteResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
