using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeesMs.Models
{
    public class EmployeeDb : DbContext
    {
        public EmployeeDb() : base("DefaultConnection")
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}