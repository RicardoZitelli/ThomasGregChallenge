namespace ThomasGregChallenge.Domain.Interfaces.Services;

public interface IBaseService<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj, CancellationToken cancellationToken);
    Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity obj, CancellationToken cancellationToken);
    Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity obj, CancellationToken cancellationToken);
    Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
}

