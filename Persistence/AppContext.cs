using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppContext : DbContext
    {
        private string UserId;

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            UserId = Common.Factories.ResolverFactory.UserId;
        }

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
