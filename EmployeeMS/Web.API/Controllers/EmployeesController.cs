using EmployeeMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.API.Controllers
{
    public class EmployeesController : ApiController
    {
        private IEmployeeRepository _er;

        public EmployeesController(IEmployeeRepository er)
        {
            _er = er;
        }
        public IEnumerable<string>Get()
        {
            return new string[] { "value1", "value2" };

        }
    }
}
