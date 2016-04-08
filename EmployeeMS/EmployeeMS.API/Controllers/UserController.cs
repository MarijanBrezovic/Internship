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
    public class UserController : ApiController
    {
        IUnitOfWork _userrepo;
        public UserController(IUnitOfWork ur)
        {
            _userrepo = ur;
        }
        public HttpResponseMessage Get()
        {
            var result = _userrepo.UserRepository.GetAll();
            if(result==null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }
        public HttpResponseMessage Get(Guid id)
        {
            var result = _userrepo.UserRepository.FindById(id);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }
    }
}
