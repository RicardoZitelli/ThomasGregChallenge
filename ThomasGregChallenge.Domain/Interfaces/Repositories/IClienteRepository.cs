using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);
    }
}
