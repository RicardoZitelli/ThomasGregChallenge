using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;

namespace ThomasGregChallenge.Application.Interfaces.Services
{
    public interface ILogradouroApplicationService
    {
        Task SaveAsync(LogradouroRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(LogradouroRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(int logradouroId, CancellationToken cancellationToken);
        Task<LogradouroResponseDto> GetByIdAsync(int logradouroId, CancellationToken cancellationToken);        
        Task<IEnumerable<LogradouroResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
        Task<IEnumerable<LogradouroResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<LogradouroResponseDto>> GetByClientIdAsync(int clienteId, CancellationToken cancellationToken);
    }
}