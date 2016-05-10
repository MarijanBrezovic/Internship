using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Entities;
using System.Web;

namespace EmployeeMS.Domain.Repositories
{
     public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();
        Employee GetOne(int id);
        //IQueryable<Employee> GetEmployees(string userId);
        //Employee GetEmployeeById(int? id,string userId);
        void CreateEmployee(Employee employee,string userId,byte[] image);
        void EditEmployee(Employee employee,string userId,byte[] image);
        void DeleteEmployee(int id);
        //IQueryable<Employee> SearchEmployee(string searchBy,string userId);
        //IQueryable<Employee> Sorting(string sortOrder,string userId);
        

    }
}
