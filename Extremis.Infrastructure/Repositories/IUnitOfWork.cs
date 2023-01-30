using Extremis.Contracts;
using Extremis.DbContexts;

namespace Extremis.Repositories;

public interface IUnitOfWork : IDisposable
{
    AppDbContext AppDbContext { get; }
    IRepositoryAsync<T> GetRepository<T>() where T : class, IEntity;
    Task<int> Commit();
    Task Rollback();
}
