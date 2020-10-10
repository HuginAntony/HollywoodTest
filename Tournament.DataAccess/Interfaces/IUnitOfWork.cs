using System;
using System.Threading.Tasks;

namespace Tournament.DataAccess.Interfaces
{
    public interface IUnitOfWork<out TContext> : IDisposable
    {
        TContext Context { get; }
        void Commit();

        Task CommitAsync();

        void Dispose(bool disposing);
    }
}