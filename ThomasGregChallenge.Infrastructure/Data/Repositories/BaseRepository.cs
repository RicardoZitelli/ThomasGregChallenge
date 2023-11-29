using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Interfaces.Repositories;

namespace ThomasGregChallenge.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity>(SqlContext sqlContext) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Set<TEntity>().AddRange(entities);
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Set<TEntity>().Add(entity);
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Entry(entity).State = EntityState.Modified;
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Entry(entities).State = EntityState.Modified;
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Set<TEntity>().Remove(entity);
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                _sqlContext.Set<TEntity>().RemoveRange(entities);
                await _sqlContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _sqlContext.Set<TEntity>().ToListAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _sqlContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
