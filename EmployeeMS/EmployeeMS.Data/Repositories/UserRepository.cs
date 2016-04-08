using EmployeeMS.Data.Configuration;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMS.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public User FindByUserName(string username)
        {
            return Set.FirstOrDefault(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
            
        }
    }
}