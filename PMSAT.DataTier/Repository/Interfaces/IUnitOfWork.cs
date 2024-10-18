using Microsoft.EntityFrameworkCore;
using PMSAT.DataTier.Repository.Interfaces;

namespace PMSAT.Repository.Interfaces
{
    public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
