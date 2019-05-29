using CoreApiTemplate.Domain;
using CoreApiTemplate.Domain.Interfaces;
using CoreApiTemplate.Persistence;

namespace CoreApiTemplate.Core.Repository
{
    public sealed class GenericRepository<T, TContext> : EfRepository<T, TContext>, IRepository<T>
        where T : BaseEntity, IEntity
        where TContext : ApplicationContext
    {
        public GenericRepository(TContext dbContext) : base(dbContext)
        {
        }
    }
}
