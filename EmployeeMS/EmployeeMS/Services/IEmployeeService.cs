using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeMS.Models;
using PagedList;

namespace EmployeeMS.Services
{
    public interface IEmployeeService
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