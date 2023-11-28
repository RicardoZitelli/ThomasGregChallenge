using Microsoft.EntityFrameworkCore;
using ThomasGregChallenge.Domain.Interfaces.Repositories;

namespace ThomasGregChallenge.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity>(SqlContext unitOfWork) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SqlContext _unitOfWork = unitOfWork;

        public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.Set<TEntity>().AddRange(entities);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
                _unitOfWork.Set<TEntity>().Add(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
                _unitOfWork.Entry(entity).State = EntityState.Modified;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
                _unitOfWork.Entry(entities).State = EntityState.Modified;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
                _unitOfWork.Set<TEntity>().Remove(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
                _unitOfWork.Set<TEntity>().RemoveRange(entities);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.Set<TEntity>().ToListAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.Set<TEntity>().FindAsync([new { Id = id }, cancellationToken], cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
