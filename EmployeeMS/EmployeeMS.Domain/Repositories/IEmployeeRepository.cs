using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Entities;
using System.Web;
using MongoDB.Bson;

namespace EmployeeMS.Domain.Repositories
{
     public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetOne(string id);
        void CreateEmployee(EmployeeDtoModel employee);
        void EditEmployee(EmployeeDtoModel employee);
        void DeleteEmployee(string id);
    }
}
