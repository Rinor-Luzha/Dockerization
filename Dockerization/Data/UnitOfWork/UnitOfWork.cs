using Microsoft.EntityFrameworkCore;
using System.Collections;
using Dockerization.Data.Repository;

namespace Dockerization.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories;
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CompleteAsync()
        {
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }
        public bool Complete()
        {
            int affectedRows = _dbContext.SaveChanges();
            return affectedRows > 0;
        }
        public IGenericRepository<Entity> Repository<Entity>() where Entity : class
        {
            if (repositories is null) repositories = new Hashtable();

            string type = typeof(Entity).Name;
            if (!repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(GenericRepository<>);
                object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(Entity)), _dbContext);
                repositories[type] = repositoryInstance;
            }
            return (IGenericRepository<Entity>)repositories[type];

        }
    }
}
