using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeMS.API.Controllers
{
    public class EmployeeController : ApiController
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository er)
        {
            _employeeRepository = er;
        }
        public HttpResponseMessage Get()
        {
            if(_employeeRepository.GetAll() == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, _employeeRepository.GetAll());
        }
        public HttpResponseMessage Get(int id)
        {
            if(_employeeRepository.GetOne(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, _employeeRepository.GetOne(id));
        }
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            string userId = "268d9574-7c45-4c04-8666-2b9be39681bc";
            byte[] image = null;
            return Request.CreateResponse(HttpStatusCode.Created, _employeeRepository.CreateEmployee(employee,userId,image));
        }
        public HttpResponseMessage Delete(int id)
        {
            if(_employeeRepository.GetOne(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            _employeeRepository.DeleteEmployee(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Patch(int id,[FromBody]Employee employee)
        {
            string userId = "268d9574-7c45-4c04-8666-2b9be39681bc";
            byte[] image = null;
            if (!_employeeRepository.GetAll().Any(x=>x.Id==id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            employee.Id = id;
            _employeeRepository.EditEmployee(employee, userId, image);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
