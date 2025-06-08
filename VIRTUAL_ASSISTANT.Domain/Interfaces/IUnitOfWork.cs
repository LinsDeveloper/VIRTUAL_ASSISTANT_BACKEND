using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Repository;

namespace VIRTUAL_ASSISTANT.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task<int> CommitAsyncCancelToken(CancellationToken cancellationToken);
        int Commit();
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IQueryable<TEntity> Query<TEntity>(bool asNoTracking = false) where TEntity : class;
    }
}

