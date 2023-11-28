using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Domain.Interfaces.Services
{
    public interface ILogradouroService : IBaseService<Logradouro>
    {
        Task<IEnumerable<Logradouro>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
        Task<IEnumerable<Logradouro>> GetByClientIdAsync(int clienteId, CancellationToken cancellationToken);
    }
}
