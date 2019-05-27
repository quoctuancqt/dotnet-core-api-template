using Domain;
using System.Collections.Generic;

namespace Core.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IReadOnlyList<T> ListAll();
        IReadOnlyList<T> List(Interfaces.ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(Interfaces.ISpecification<T> spec);
    }
}
