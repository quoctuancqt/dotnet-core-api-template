using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApiTemplate.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        Task SaveChangeAsync(Func<Task> action = null);
    }
}
