using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppContext : DbContext
    {
        private string UserId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            UserId = Common.Factories.ResolverFactory.UserId;
        }

        public CatalogDbContext(DbContextOptions<AppContext> options, IHttpContextAccessor httpContextAccessor)
         : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public 

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.BeforeCommit(UserId);

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(Func<Task> action)
        {
            var strategy = this.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = this.Database.BeginTransaction())
                {
                    await this.SaveChangesAsync();

                    await action();

                    transaction.Commit();
                }
            });
        }
    }
}
