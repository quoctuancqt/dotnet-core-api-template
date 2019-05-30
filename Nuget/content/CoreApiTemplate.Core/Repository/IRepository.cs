using CoreApiTemplate.Core.Interfaces;
using CoreApiTemplate.Domain;
using CoreApiTemplate.Persistence;
using System.Collections.Generic;

namespace CoreApiTemplate.Core.Repository
{
    public interface IRepository<T, TKey>
        where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        T GetById(TKey id);
        IReadOnlyList<T> ListAll();
        IReadOnlyList<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(ISpecification<T> spec);
    }

    public interface IRepository<T> : IRepository<T, string>
        where T : BaseEntity
    { }
}
