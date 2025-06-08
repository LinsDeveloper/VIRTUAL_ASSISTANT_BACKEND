using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetById(int id);
        TEntity GetById(long id);
        IEnumerable<TEntity> GetAll(bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
    }
}
