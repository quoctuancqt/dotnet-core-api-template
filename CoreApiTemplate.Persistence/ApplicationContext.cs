using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreApiTemplate.Common.Factories;
using CoreApiTemplate.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoreApiTemplate.Domain.Identities;
using Microsoft.AspNetCore.Identity;

namespace CoreApiTemplate.Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IUnitOfWork
    {
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
    }
}
