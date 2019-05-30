﻿using System;
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
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IUnitOfWork
    {
        public virtual DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");

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