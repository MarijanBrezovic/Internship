using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Repositories;
using EmployeeMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMS.Conversion
{
    public class EmployeeIntoEmployeeViewModel
    {
        public  static  EmployeeViewModel ConvertEmployeeIntoEmployeeViewModel(Employee employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Name = employee.Name;
            employeeViewModel.BirthDate = employee.BirthDate;
            employeeViewModel.Gender = employee.Gender;
            return employeeViewModel;


        }
        
    }
}