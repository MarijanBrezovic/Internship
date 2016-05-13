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
            //if(_employeeRepository.GetAll() == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, _employeeRepository.GetAll());
        }
        public HttpResponseMessage Get(int id)
        {
            //if(_employeeRepository.GetOne(id) == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, _employeeRepository.GetOne(id));
        }
        [HttpPost]
        public HttpResponseMessage Post()
        {
            Employee employee = SetEmployeeFromClientSide();
            if(employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            _employeeRepository.CreateEmployee(employee, employee.UserId, employee.Image);
            return Request.CreateResponse(HttpStatusCode.Created);
        }


        public HttpResponseMessage Delete(int id)
        {
            //if(_employeeRepository.GetOne(id) == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            _employeeRepository.DeleteEmployee(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Put(int id,[FromBody]Employee employee)
        {
            //if (!_employeeRepository.GetAll().Any(x=>x.Id==id))
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            employee.Id = id;
            _employeeRepository.EditEmployee(employee, employee.UserId, employee.Image);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// Returns an employee instance that has been passed down by the client side.
        /// </summary>
        /// <returns></returns>
        private Employee SetEmployeeFromClientSide ()
        {
            Employee employee = new Employee();
            try {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var httpRequest = HttpContext.Current.Request;
                employee = ConvertEmployeeDtoToEmployee(Map<EmployeeDto>((key) => httpRequest.Form.Get(key)));
                var image = Map<ImageDto>((key) => httpRequest.Files.Get(key));
                if (image.Image != null)
                {
                    try
                    {
                        employee.Image = new byte[image.Image.ContentLength];
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    int numbytes = (int)employee.Image.Length;

                    int n = image.Image.InputStream.Read(employee.Image, 0, numbytes);
                }
                else {
                    employee.Image = GetDefaultEmployeePicture(employee.Gender);
                }
            }
            catch(Exception ex)
            {
                Log.Fatal(ex.ToString());
                return null;
            }
            return employee;
        }
        /// <summary>
        /// Sets a default picture based on the employee Gender.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private byte[] GetDefaultEmployeePicture(Gender gender)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (gender == Gender.Male) {
                return File.ReadAllBytes(path + "Uploads\\Man.jpg");
            }
            else if (gender == Gender.Female)
            {
                return File.ReadAllBytes(path + "Uploads\\Woman.jpg");
            }
            else
            {
                return File.ReadAllBytes(path + "Uploads\\Other.jpg");
            }
        }
        private Employee ConvertEmployeeDtoToEmployee(EmployeeDto emp)
        {
            Employee employee = new Employee();
            employee.Name = emp.Name;
            employee.Gender = emp.Gender;
            employee.BirthDate = emp.BirthDate;
            employee.UserId = emp.UserId;
            return employee;
        }
        private static T Map<T>(Func<string, object> fun)
        {
            var mappedObject = Activator.CreateInstance<T>();
            foreach(var prop in mappedObject.GetType().GetProperties())
            {
                var custAttr = Attribute.GetCustomAttribute(prop, typeof(MapNameAttribute)) as MapNameAttribute;
                if (custAttr != null)
                {
                    custAttr.Map(prop, mappedObject, fun(custAttr.Name));
                }
                    var reqAtttr = Attribute.GetCustomAttribute(prop, typeof(RequiredAttribute)) as RequiredAttribute;
                    if (reqAtttr != null)
                    {
                        if (reqAtttr.IsValid(fun(custAttr.Name)) == false)
                        {
                            throw new Exception(custAttr.Name + "Is Required");
                        }
                    } 
            }
            return mappedObject;
        }


        public class EmployeeDto
        {
            [Required]
            [MapName(Name = "employee[Name]")]
            public string Name { get; set; }
            [MapName(Name = "employee[UserId]")]
            [Required]
            public string UserId { get; set; }
            [Required]
            [MapName(Name = "employee[BirthDate]")]
            public DateTime BirthDate { get; set; }
            [Required]
            [MapName(Name = "employee[Gender]")]
            public Gender Gender { get; set; }

        }
        public class ImageDto
        {
            [MapName(Name ="employee[Image][0]")]
            public HttpPostedFile Image { get; set; }
        }
        class MapNameAttribute : Attribute
        {
            public string Name { get; set; }
            public void Map(System.Reflection.PropertyInfo prop, object mappedObject, object value)
            {
                if(prop.PropertyType == typeof(string))
                {
                    prop.SetValue(mappedObject, value);
                }
                else if (prop.PropertyType == typeof(HttpPostedFile))
                {
                    prop.SetValue(mappedObject, value);
                }
                else if (prop.PropertyType == typeof(Gender))
                {
                    value = (Gender)Enum.Parse(typeof(Gender), value.ToString());
                    prop.SetValue(mappedObject, value);
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    value = Convert.ToDateTime(value);
                    prop.SetValue(mappedObject, value);
                }

            }
        }
    }
    
}
