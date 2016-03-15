using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Repositories;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Data.Configuration;
using System.Web;
using Microsoft.AspNet.Identity;

namespace EmployeeMS.Data.Repositories
{
    //Logic for Employee services
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDb _db;
        public EmployeeRepository()
        {
            this._db = new EmployeeDb();
        }
        public IEnumerable<Employee> GetEmployees()
        {
            
            string userId = HttpContext.Current.User.Identity.GetUserId();
            var employees = _db.EmployeeDatabase.Where(x => x.UserId == userId);
            return employees;
        }
        public Employee GetEmployeeById(int? id)
        {
            Employee employee = GetEmployees().Single(x => x.Id == id);
            return employee;
        }
        public void CreateEmployee(Employee employee)
        {
            Employee emp = new Employee();
            emp.Name = employee.Name;
            emp.UserId = HttpContext.Current.User.Identity.GetUserId();
            emp.BirthDate = employee.BirthDate;
            emp.Gender = employee.Gender;
            _db.EmployeeDatabase.Add(emp);
            _db.Save();
        }
        public void EditEmployee(Employee employee)
        {
            employee.UserId = HttpContext.Current.User.Identity.GetUserId();
            _db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            _db.Save();
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = _db.EmployeeDatabase.Find(id);
            _db.EmployeeDatabase.Remove(employee);
            _db.Save();
        }
        public IEnumerable<Employee> SearchEmployee(string searchBy)
        {
            var searchedEmployees = GetEmployees().Where(x => x.Name.Contains(searchBy));
            return searchedEmployees;
        }
        public IEnumerable<Employee> Sorting(string sortOrder)
        {

            string userId = HttpContext.Current.User.Identity.GetUserId();
            var employees = _db.EmployeeDatabase.Where(x => x.UserId == userId);
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(x => x.Name);
                    break;
                case "Gender":
                    employees = employees.OrderBy(s => s.Gender);
                    break;
                case "gender_desc":
                    employees = employees.OrderByDescending(s => s.Gender);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Name);
                    break;
            }
            return employees;
        }
    }
}
