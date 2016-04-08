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
            return _db.EmployeeDatabase;
        }
        public Employee GetOne(int id)
        {
            return _db.EmployeeDatabase.FirstOrDefault(x=>x.Id==id);
        }
        public IQueryable<Employee> GetEmployees(string userId)
        {
            IQueryable < Employee > query = from c in _db.EmployeeDatabase
                                            where c.UserId == userId
                                            select c;
            return query;
        }
        public Employee GetEmployeeById(int? id,string userId)
        {
            if(id==null)
            {
                throw new Exception("U have not entered the Employee Id");
            }
            if (!GetEmployees(userId).Any(x => x.Id == id))
            {
                throw new Exception("U do not have an Emplooye with the id specified" );
            }
            Employee employee = GetEmployees(userId).Single(x => x.Id == id);
            return employee;

        }
        public Employee CreateEmployee(Employee employee,string userId,byte [] image)
        {
            if (employee.Name == null)
            {
                throw new Exception("Your Employee Must Have a Name");
            }
            if (employee.BirthDate == DateTime.MinValue)
            {
                throw new Exception("Your Employee Must Have a BirthDate");
            }
            Guid userid = new Guid(userId);
            var user = _db.Users.Single(x=>x.UserId == userid);
            Employee emp = new Employee();
            emp.Name = employee.Name;
            emp.UserId = userId;
            emp.BirthDate = employee.BirthDate;
            emp.Gender = employee.Gender;
            emp.Image = image;
            user.Employees.Add(emp);
            _db.EmployeeDatabase.Add(emp);
            _db.Save();
            return emp;
        }
        public void EditEmployee(Employee employee,string userId,byte[] image)
        {
            
            if(employee.Name==null)
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
            _db.Save();
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = _db.EmployeeDatabase.Find(id);
            _db.EmployeeDatabase.Remove(employee);
            _db.Save();
        }
        public IQueryable<Employee> SearchEmployee(string searchBy,string userId)
        {

            IQueryable<Employee> query = from c in GetEmployees(userId)
                                         where c.Name.Contains(searchBy)
                                         select c;
            return query;
        }
        public IQueryable<Employee> Sorting(string sortOrder,string userId)
        {
            var employees = GetEmployees(userId);
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
        public User NovaMetoda(string userId)
        {
            Guid g = new Guid(userId);
            return _db.Users.Single(x=>x.UserId==g);
        }
        //public User asd(string userIdd)
        //{
        //    Guid userId = new Guid(userIdd);
        //    return _db.Users.Single(x => x.UserId == userId);
        //}
    }
}
