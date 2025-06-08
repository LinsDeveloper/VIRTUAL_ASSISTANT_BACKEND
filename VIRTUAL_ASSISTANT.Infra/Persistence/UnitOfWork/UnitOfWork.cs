using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Interfaces;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Repository;
using VIRTUAL_ASSISTANT.Infra.Persistence.Repository.Base;

namespace VIRTUAL_ASSISTANT.Infra.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextManager _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ContextManager context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
                return (IBaseRepository<TEntity>)_repositories[typeof(TEntity)];

            var repository = new BaseRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = false) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CommitAsyncCancelToken(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
