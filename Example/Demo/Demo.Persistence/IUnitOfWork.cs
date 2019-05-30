﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task SaveChangeAsync(Func<Task> action = null);
    }
}