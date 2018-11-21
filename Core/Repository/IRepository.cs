using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class, Domain.IEntity
    {
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> pression = null);

        IQueryable<TEntity> AsNoTracking(Expression<Func<TEntity, bool>> pression = null);

        TEntity FindBy(params object[] keyValues);

        Task<TEntity> FindByAsync(params object[] keyValues);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
