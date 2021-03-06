﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApiTemplate.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
