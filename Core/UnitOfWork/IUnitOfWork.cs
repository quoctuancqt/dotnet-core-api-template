using System;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        string UserId { get; }

        string UserName { get; }

        void Commit();

        Task CommitAsync();
    }
}
