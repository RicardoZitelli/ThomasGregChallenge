using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Repositories;

namespace ThomasGregChallenge.Infrastructure.Data.Repositories
{
    public sealed class ClienteRepository(SqlContext sqlContext) : BaseRepository<Cliente>(sqlContext), IClienteRepository
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task<IEnumerable<Cliente>> GetByDescriptionAsync(string description, CancellationToken cancellationToken)
        {
            var clientes = await _sqlContext
                .Set<Cliente>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return clientes.Where(x =>                 
                x.Nome.Contains(description,StringComparison.CurrentCultureIgnoreCase) ||
                x.Email.Contains(description, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }
    }
}
