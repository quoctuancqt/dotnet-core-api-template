using Demo.Core.Interfaces;
using Demo.Domain;
using Demo.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Core.Repository
{
    public class EfRepository<T, TKey> : IRepository<T, TKey>
        where T : BaseEntity
    {
        protected readonly ApplicationContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public EfRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(TKey id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IReadOnlyList<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IReadOnlyList<T> List(ISpecification<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public virtual T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public int Count(ISpecification<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }

    public class EfRepository<T> : EfRepository<T, string>, IRepository<T>
        where T : BaseEntity
    {
        public EfRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
