using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;

namespace ThomasGregChallenge.Application.Interfaces.Services
{
    public interface ILogradouroApplicationService
    {
        Task SaveAsync(LogradouroRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(LogradouroRequestDto clienteRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid logradouroId, CancellationToken cancellationToken);
        Task<LogradouroResponseDto> GetByIdAsync(Guid logradouroId, CancellationToken cancellationToken);        
        Task<IEnumerable<LogradouroResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
    }
}