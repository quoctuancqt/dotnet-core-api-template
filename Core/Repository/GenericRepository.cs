namespace Core.Repository
{
    public sealed class GenericRepository<TEntity, TContext> : Repository<TEntity, TContext>, IRepository<TEntity>
        where TEntity : class, Domain.IEntity
        where TContext : Context
    {
        public GenericRepository(TContext context) : base(context) { }
    }
}
