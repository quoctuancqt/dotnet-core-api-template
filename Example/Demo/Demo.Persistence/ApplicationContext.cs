using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Common.Factories;
using Demo.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Demo.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Demo.Domain;

namespace Demo.Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IUnitOfWork
    {
        public virtual DbSet<Category> Categories { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

        }

        public override int SaveChanges()
        {
            this.BeforeCommit(ResolverFactory.GetCurrentUserId());

            return base.SaveChanges();
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
                        await SaveChangesAsync();

                        if (action != null)
                        {
                            await action.Invoke();
                        }

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
