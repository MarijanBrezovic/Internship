using EmployeeMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Data.Configuration
{
    class EmployeeDb : DbContext
    {
        public DbSet<Employee> EmployeeDatabase { get; set; }

        public void Save()
        {
            SaveChanges();
        }
    }
}
