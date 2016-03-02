namespace EmployeesMs.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeesMs.Models.EmployeeDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EmployeesMs.Models.EmployeeDb context)
        {
            context.Employees.AddOrUpdate(x => x.Name,
                new Models.Employee() { Name = "Jovan" },
                new Models.Employee() { Name = "Petar" },
                new Models.Employee() { Name = "Dusan" }
                );
        }
    }
}
