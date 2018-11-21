using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, Domain.IEntity
        where TContext : Context
    {
        protected readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity FindBy(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> pression = null)
        {
            IQueryable<TEntity> query = null;

            if (pression == null) query = _context.Set<TEntity>();

            else query = _context.Set<TEntity>().Where(pression);

            return query;
        }

        public virtual async Task<TEntity> FindByAsync(params object[] keyValues)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues);
        }

        public IQueryable<TEntity> AsNoTracking(Expression<Func<TEntity, bool>> pression = null)
        {
            if (pression == null)
            {
                return _context.Set<TEntity>().AsNoTracking();
            }

            return _context.Set<TEntity>().AsNoTracking().Where(pression);
        }
    }
}
