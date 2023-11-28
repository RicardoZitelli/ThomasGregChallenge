using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Domain.Interfaces.Services
{
    public interface IClienteService : IBaseService<Cliente>
    {
        Task<IEnumerable<Cliente>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);                
    }
}
