using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Factories;
using Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Identities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(ConfigurateApplicationUser);

            base.OnModelCreating(builder);
        }

        private void ConfigurateApplicationUser(EntityTypeBuilder<ApplicationUser> buidler)
        {
            buidler.Property(au => au.PasswordHash)
                .HasMaxLength(8)
                .IsRequired();

            buidler.Property(au => au.UserName)
                .IsRequired();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.BeforeCommit(ResolverFactory.GetCurrentUserId());

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(Func<Task> action = null)
        {
            var strategy = Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = Database.BeginTransaction())
                {
                    try
                    {
                        if (action != null)
                        {
                            await action.Invoke();
                        }

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
