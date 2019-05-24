using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Common.Factories;
using Persistence.Extensions;

namespace Persistence
{
    public class AppContext : DbContext, IUnitOfWork
    {
        private string UserId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public AppContext(DbContextOptions<AppContext> options, IHttpContextAccessor httpContextAccessor)
         : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.BeforeCommit(ResolverFactory.GetCurrentUserId());

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(Func<Task> action)
        {
            var strategy = Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = Database.BeginTransaction())
                {
                    try
                    {
                        await action();

                        await SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction?.Rollback();

                        if (transaction != null)
                        {
                            transaction.Dispose();
                        }

                        throw;
                    }
                }
            });
        }
    }
}
