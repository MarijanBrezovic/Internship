using EmployeeMS.Domain.Entities;
using System.Data.Entity;

namespace EmployeeMS.Data.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :base("DefaultConnection")
        {

        }
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        internal IDbSet<User> Users { get; set; }
        internal IDbSet<Role> Roles { get; set; }
        internal IDbSet<ExternalLogin> Logins { get; set; }

    }
}