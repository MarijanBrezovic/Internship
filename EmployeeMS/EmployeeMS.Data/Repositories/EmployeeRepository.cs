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
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace EmployeeMS.Data.Repositories
{
    //Logic for Employee services
    public class EmployeeRepository : IEmployeeRepository
    {
        private IMongoDbContext _mongoDbContext;
        public EmployeeRepository(IMongoDbContext mdbc)
        {
            _mongoDbContext = mdbc;
        }
        public IEnumerable<Employee> GetAll()
        {
            Log.Information("Started Returning All Employees");
            try {
                var employees = _mongoDbContext.GetEmployeeCollection().Find(x=>true).ToList();
                return employees;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Employee GetOne(string id)
        {
            Log.Information("Geting employee with id:" + id);
            var employee = _mongoDbContext.GetEmployeeCollection().Find(x => x._id.Equals(new ObjectId(id))).FirstOrDefault();
            return employee;
        }

        public void CreateEmployee(EmployeeDtoModel employee)
        {
            
            Employee emp = new Employee();
            Log.Information("CreateEmployee Started");
            try
            {
                ObjectId userid = new ObjectId(employee.UserId);
                emp = employee.ToEmployeeModel();
                var user = _mongoDbContext.GetUserCollection().Find(x => x._id.Equals(userid)).FirstOrDefault();
                var asd = user.UserName;
                _mongoDbContext.GetEmployeeCollection().InsertOne(emp);
                user.Employees.Add(emp);
                _mongoDbContext.GetUserCollection().ReplaceOne<User>(x => x._id.Equals(userid), user, new UpdateOptions { IsUpsert = true });


            }
            catch(Exception ex)
            {
                Log.Fatal(ex.ToString());
            }
        }
        public void EditEmployee(EmployeeDtoModel employeeDto)
        {
            Log.Information("Edit Employee Started");
            if (employeeDto.Name == null)
            {
                throw new Exception("Your Employee Must Have a Name");
            }
            if (employeeDto.BirthDate == null)
            {
                throw new Exception("Your Employee Must Have a BirthDate");
            }
            var employee = employeeDto.ToEmployeeModel();
            employee._id = new ObjectId(employeeDto.Id);
            User user = new User();
            var users = _mongoDbContext.GetUserCollection().Find(x => true).ToList();
            foreach (User foreachUser in users)
            {
                var firsOrDefaultEmployee = foreachUser.Employees.FirstOrDefault(x => x._id.Equals(employee._id));
                if(firsOrDefaultEmployee != null)
                {
                    user = foreachUser;
                    foreachUser.Employees.Remove(firsOrDefaultEmployee);
                    foreachUser.Employees.Add(employee);
                    break;
                }
            }
            _mongoDbContext.GetEmployeeCollection().ReplaceOne<Employee>(x => x._id.Equals(new ObjectId(employeeDto.Id)), employee, new UpdateOptions { IsUpsert = true });
            _mongoDbContext.GetUserCollection().ReplaceOne<User>(x => x._id.Equals(user._id), user, new UpdateOptions { IsUpsert = true });
            Log.Information("Edit Employee Ended");
        }
        public void DeleteEmployee(string id)
        {
            User user = new User();
            Log.Information("Delete Employee Started");
            var users = _mongoDbContext.GetUserCollection().Find(x => true).ToList();
            foreach (User foreachUser in users)
            {
                var firsOrDefaultEmployee = foreachUser.Employees.FirstOrDefault(x => x._id.Equals(new ObjectId(id)));
                if (firsOrDefaultEmployee != null)
                {
                    user = foreachUser;
                    foreachUser.Employees.Remove(firsOrDefaultEmployee);
                    break;
                }
            }
            _mongoDbContext.GetUserCollection().ReplaceOne<User>(x => x._id.Equals(user._id), user, new UpdateOptions { IsUpsert = true });
            _mongoDbContext.GetEmployeeCollection().DeleteOne<Employee>(x => x._id.Equals(new ObjectId(id)));

        }
        
    }
}
