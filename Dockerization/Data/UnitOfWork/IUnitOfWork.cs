using Dockerization.Data.Repository;

namespace Dockerization.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
        bool Complete();
        IGenericRepository<Entity> Repository<Entity>() where Entity : class;
    }
}
