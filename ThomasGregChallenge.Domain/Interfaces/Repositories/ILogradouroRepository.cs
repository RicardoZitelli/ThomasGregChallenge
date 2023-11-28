using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Domain.Interfaces.Repositories
{
    public interface ILogradouroRepository : IBaseRepository<Logradouro>
    {
        Task<IEnumerable<Logradouro>> GetByDescriptionAsync(string description, CancellationToken cancellationToken);        
    }
}
