using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeMS.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using PagedList;

namespace EmployeeMS.Services
{
    public class EmployeeService:IEmployeeService
    {
        private EmployeeDb _db;
        public EmployeeService(EmployeeDb db)
        {
            this._db = db;
        }
        public IEnumerable<Employee> GetEmployees()
        { 
            string userId = HttpContext.Current.User.Identity.GetUserId();
            var employees = _db.Employees.Where(x => x.UserId == userId);
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
            _db.Employees.Add(emp);
            _db.Save();
        }
        public void EditEmployee(Employee employee)
        {

            _db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            _db.Save();
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = _db.Employees.Find(id);
            _db.Employees.Remove(employee);
            _db.Save();
        }
        public IEnumerable<Employee>SearchEmployee(string searchBy)
        {
            var searchedEmployees = GetEmployees().Where(x => x.Name.Contains(searchBy));
            return searchedEmployees;
        }
        public IEnumerable<Employee> Sorting(string sortOrder)
        {

            string userId = HttpContext.Current.User.Identity.GetUserId();
            var employees = _db.Employees.Where(x => x.UserId == userId);
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