using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Common.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppContext : DbContext
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
            this.BeforeCommit(_httpContextAccessor?.HttpContext?.User?.GetPropValue<string>(ClaimTypes.NameIdentifier));

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
