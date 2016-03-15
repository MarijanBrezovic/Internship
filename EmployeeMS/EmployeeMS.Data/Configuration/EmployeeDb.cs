using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Entities;
namespace EmployeeMS.Data.Configuration
{
    public class EmployeeDb  : DbContext
    {
        public DbSet<Employee> EmployeeDatabase { get; set; }

        public void Save()
        {
            SaveChanges();
        }

    }
}
