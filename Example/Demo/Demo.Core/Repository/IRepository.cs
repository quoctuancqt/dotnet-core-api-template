using Demo.Core.Interfaces;
using Demo.Domain;
using Demo.Persistence;
using System.Collections.Generic;

namespace Demo.Core.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        T GetById(int id);
        IReadOnlyList<T> ListAll();
        IReadOnlyList<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(ISpecification<T> spec);
    }
}
