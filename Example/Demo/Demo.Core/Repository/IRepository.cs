using Demo.Core.Interfaces;
using Demo.Domain;
using Demo.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Core.Repository
{
    public interface IRepository<T, TKey>
        where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetByIdAsync(TKey id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
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
