using ThomasGregChallenge.Domain.Interfaces.Repositories;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Domain.Services
{
    public class BaseService<TEntity>(IBaseRepository<TEntity> repositoryBase) : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repositoryBase = repositoryBase;
        public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken) => 
            await _repositoryBase.AddRangeAsync(entities, cancellationToken);

        public async Task AddAsync(TEntity obj, CancellationToken cancellationToken) => 
            await _repositoryBase.AddAsync(obj, cancellationToken);

        public async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken) => 
            await _repositoryBase.UpdateAsync(obj, cancellationToken);

        public async Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken) => 
            await _repositoryBase.UpdateRangeAsync(entities, cancellationToken);

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => 
            await _repositoryBase.GetAllAsync(cancellationToken);

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken) => 
            await _repositoryBase.GetByIdAsync(id, cancellationToken);

        public async Task DeleteAsync(TEntity obj, CancellationToken cancellationToken) => 
            await _repositoryBase.DeleteAsync(obj, cancellationToken);

        public async Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => 
            await _repositoryBase.DeleteAllAsync(entities, cancellationToken);
    }
}
