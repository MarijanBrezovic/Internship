using EmployeeMS.Domain.Entities;
using System.Data.Entity;

namespace EmployeeMS.Data.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext() 
        //{

        //}
        public ApplicationDbContext()
            : base("Ojsa Prske")
        {
        }
        public DbSet<Employee> EmployeeDatabase { get; set; }

        public void Save()
        {
            SaveChanges();
        }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<ExternalLogin> Logins { get; set; }

    }
}