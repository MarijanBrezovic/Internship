using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeMS.Models
{
    public class EmployeeDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
       
       public void Save()
        {
            SaveChanges();
        }

    }
}