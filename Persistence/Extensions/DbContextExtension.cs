using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public static class DbContextExtension
    {
        public static void BeforeCommit(this DbContext context, string userId, bool isAudit = true)
        {
            var entriesAdded = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity);

            var entriesModified = context.ChangeTracker.Entries()
                  .Where(e => e.State == EntityState.Modified).Select(e => e.Entity as Domain.Interfaces.IAudit);

            if (entriesAdded.Count() > 0) ProcessAudit(entriesAdded, EntityState.Added, userId);

            if (entriesModified.Count() > 0) ProcessAudit(entriesModified, EntityState.Modified, userId);
        }

        private static void ProcessAudit(IEnumerable<object> entries, EntityState state, string userId)
        {
            foreach (var e in entries.Select(e => e as Domain.Interfaces.IAudit))
            {
                if (e != null)
                {
                    if (state == EntityState.Added)
                    {
                        e.CreatedBy = userId;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        e.UpdatedBy = userId;
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
