using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Repositories;

namespace ThomasGregChallenge.Infrastructure.Data.Repositories
{
    public sealed class LogradouroRepository(SqlContext sqlContext) : BaseRepository<Logradouro>(sqlContext), ILogradouroRepository
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task<IEnumerable<Logradouro>> GetByDescriptionAsync(string description, CancellationToken cancellationToken)
        {
            var logradouros = await _sqlContext.Set<Logradouro>().ToListAsync(cancellationToken);

            return logradouros.Where(x =>
                x.Bairro.Contains(description, StringComparison.CurrentCultureIgnoreCase) ||
                x.Cidade.Contains(description, StringComparison.CurrentCultureIgnoreCase) ||                
                x.Endereco.Contains(description, StringComparison.CurrentCultureIgnoreCase) ||
                x.Estado.Contains(description, StringComparison.CurrentCultureIgnoreCase) ||                
                x.Numero.Contains(description, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public async Task<IEnumerable<Logradouro>> GetByClientIdAsync(int clienteId, CancellationToken cancellationToken)
        {
            var logradouros = await _sqlContext.Set<Logradouro>().ToListAsync(cancellationToken);

            return logradouros.Where(x =>x.ClienteId == clienteId).ToList();
        }
    }
}
