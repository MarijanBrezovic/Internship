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
using System.Net;
using Serilog;

namespace EmployeeMS.Data.Repositories
{
    //Logic for Employee services
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        //public EmployeeRepository()
        //{
        //    this._db = new ApplicationDbContext();
        //}
        public IQueryable<Employee> GetAll()
        {
            Log.Information("Started Returning All Employees");
            try {
                return _db.EmployeeDatabase;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Employee GetOne(int id)
        {
            Log.Information("Geting employee with id:" + id);
            return _db.EmployeeDatabase.FirstOrDefault(x => x.Id == id);
        }
        //public IQueryable<Employee> GetEmployees(string userId)
        //{
        //    IQueryable<Employee> query = from c in _db.EmployeeDatabase
        //                                 where c.UserId == userId
        //                                 select c;
        //    return query;
        //}
        //public Employee GetEmployeeById(int? id, string userId)
        //{
        //    if (id == null)
        //    {
        //        throw new Exception("U have not entered the Employee Id");
        //    }
        //    if (!GetEmployees(userId).Any(x => x.Id == id))
        //    {
        //        throw new Exception("U do not have an Emplooye with the id specified");
        //    }
        //    Employee employee = GetEmployees(userId).Single(x => x.Id == id);
        //    return employee;

        //}
        public void CreateEmployee(Employee employee, string userId, byte[] image)
        {
            Employee emp = new Employee();
            Log.Information("CreateEmployee Started");
            try {
                Guid userid = new Guid(userId);
                emp.Name = employee.Name;
                emp.UserId = userId;
                emp.BirthDate = employee.BirthDate;
                emp.Gender = employee.Gender;
                emp.Image = image;
                _db.Users.Single(x => x.UserId == userid).Employees.Add(emp);
                _db.EmployeeDatabase.Add(emp);
                _db.Save();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex.ToString());

            }  
        }
        public void EditEmployee(Employee employee, string userId, byte[] image)
        {
            Log.Information("Edit Employee Started");
            if (employee.Name == null)
            {
                throw new Exception("Your Employee Must Have a Name");
            }
            if (employee.BirthDate == null)
            {
                throw new Exception("Your Employee Must Have a BirthDate");
            }
            //if (!GetAll().Any(x => x.Id == employee.Id))
            //{
            //    throw new Exception("U do not have an Emplooye with the id specified");
            //}
            employee.UserId = userId;
            employee.Image = image;
            _db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            Log.Information("Edit Employee Ended");
            _db.Save();
        }
        public void DeleteEmployee(int id)
        {
            Log.Information("Delete Employee Started");
            Employee employee = _db.EmployeeDatabase.Find(id);
            _db.EmployeeDatabase.Remove(employee);
            _db.Save();
        }
        //public IQueryable<Employee> SearchEmployee(string searchBy, string userId)
        //{

        //    IQueryable<Employee> query = from c in GetEmployees(userId)
        //                                 where c.Name.Contains(searchBy)
        //                                 select c;
        //    return query;
        //}
        //public IQueryable<Employee> Sorting(string sortOrder, string userId)
        //{
        //    var employees = GetEmployees(userId);
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            employees = employees.OrderByDescending(x => x.Name);
        //            break;
        //        case "Gender":
        //            employees = employees.OrderBy(s => s.Gender);
        //            break;
        //        case "gender_desc":
        //            employees = employees.OrderByDescending(s => s.Gender);
        //            break;
        //        default:
        //            employees = employees.OrderBy(s => s.Name);
        //            break;
        //    }
        //    return employees;
        //}
    }
}
