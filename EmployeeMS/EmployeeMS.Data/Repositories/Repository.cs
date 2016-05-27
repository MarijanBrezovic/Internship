using EmployeeMS.Data.Configuration;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeMS.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // CANNOT DO DI TO MUCH BIND TO ENTITY!
        private IMongoDbContext _mongoDbContext = new MongoDbContext();
        private ApplicationDbContext _context;
        private DbSet<TEntity> _set;

        internal Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public IEnumerable<User> GetAll()
        {
            var user = _mongoDbContext.GetUserCollection().Find(x => true).ToList();
            return user;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Set.ToListAsync(cancellationToken);
        }

        public List<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToList();
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public User FindById(string id)
        {
            
            var Id = new ObjectId(id);
            var user = _mongoDbContext.GetUserCollection().Find(x => x._id.Equals(Id)).FirstOrDefault();
            Log.Information("Find by id started");

            return user;
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            return Set.FindAsync(cancellationToken, id);
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}