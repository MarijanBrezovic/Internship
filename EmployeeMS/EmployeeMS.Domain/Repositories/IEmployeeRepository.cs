using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Entities;

namespace EmployeeMS.Domain.Repositories
{
     public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int? id);
        void CreateEmployee(Employee employee);
        void EditEmployee(Employee employee);
        void DeleteEmployee(int id);
        IEnumerable<Employee> SearchEmployee(string searchBy);
        IEnumerable<Employee> Sorting(string sortOrder);
    }
}
