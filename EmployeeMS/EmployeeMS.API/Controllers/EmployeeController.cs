using EmployeeMS.API.Models;
using EmployeeMS.Data.Configuration;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeMS.API.Controllers
{
    [EnableCorsAttribute("http://localhost:11820", "*", "*")]
    public class EmployeeController : ApiController
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository er)
        {
            _employeeRepository = er;
        }
        public HttpResponseMessage Get()
        {
            var employees = _employeeRepository.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK,employees);
        }
        public HttpResponseMessage Get(string id)
        {
            //if(_employeeRepository.GetOne(id) == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            var employee = _employeeRepository.GetOne(id);
            return Request.CreateResponse(HttpStatusCode.OK,employee);
        }
        [HttpPost]
        public HttpResponseMessage Post()
        {
            EmployeeDtoModel employee = new EmployeeDto().SetEverything().ToEmployeeDtoModel();
            if (employee.Image == null)
            {
                employee.Image = employee.GetDefaultEmployeePicture(employee.Gender);
            }

            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            _employeeRepository.CreateEmployee(employee);
            return Request.CreateResponse(HttpStatusCode.Created);

        }


        public HttpResponseMessage Delete(string id)
        {
            //if(_employeeRepository.GetOne(id) == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            _employeeRepository.DeleteEmployee(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Put(string id,[FromBody]EmployeeDtoModel employee)
        {
            //if (!_employeeRepository.GetAll().Any(x=>x.Id==id))
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            employee.Id = id;
            _employeeRepository.EditEmployee(employee);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
    
}
