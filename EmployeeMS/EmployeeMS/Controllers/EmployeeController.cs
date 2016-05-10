using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeMS.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using PagedList;
using EmployeeMS.Domain.Repositories;
using EmployeeMS.Data.Repositories;
using EmployeeMS.Domain.Entities;
using Microsoft.Practices.Unity;
using EmployeeMS.Conversion;
using System.IO;

namespace EmployeeMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository employeeService;
        private string UserId
        {
            get { return HttpContext.User.Identity.GetUserId(); }
        }
        public EmployeeController(IEmployeeRepository iEmployeeRepository)
        {
            this.employeeService = iEmployeeRepository;
        }
        // GET: Employee
        public ActionResult Index(IndexParameterModel indexParamaterModel)
        {
            if(indexParamaterModel.PerPage==0)
            {
                indexParamaterModel.PerPage = 5;
            }
            if(indexParamaterModel.PageNo==0)
            {
                indexParamaterModel.PageNo = 1;
            }
            ViewBag.CurrentSort = indexParamaterModel.SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(indexParamaterModel.SortOrder) ? "name_desc" : "";
            ViewBag.GenderSortParm = indexParamaterModel.SortOrder == "Gender" ? "gender_desc" : "Gender";
            ViewBag.perPage = Enum.GetValues(typeof(DropDownValues)).Cast<DropDownValues>().Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() });
            ViewBag.CurrentItemsPerPage = indexParamaterModel.PerPage;
            if (!String.IsNullOrEmpty(indexParamaterModel.SearchBy))
            {
                indexParamaterModel.PageNo = 1;
            }
            else
            {
                indexParamaterModel.SearchBy = indexParamaterModel.CurrentFilter;
            }
            ViewBag.CurrentFilter = indexParamaterModel.SearchBy;
            if(!ModelState.IsValid)
            {
                throw new Exception("Model State is Not Valid!");
            }
            if (!String.IsNullOrEmpty(indexParamaterModel.SearchBy))
            {
                return View(employeeService.SearchEmployee(indexParamaterModel.SearchBy,UserId).OrderBy(x => x.Name).ToPagedList(indexParamaterModel.PageNo, indexParamaterModel.PerPage));
            }
            return View(employeeService.Sorting(indexParamaterModel.SortOrder,UserId).ToPagedList(indexParamaterModel.PageNo, indexParamaterModel.PerPage));
        }
        public ActionResult Details(int? id)
        {
            return View(employeeService.GetEmployeeById(id,UserId));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmployeeViewModel());
        }
        [HttpPost]
        public ActionResult Create(Employee employee,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0 && upload.ContentType.Contains("image"))
                {

                    byte[] image = new byte[upload.ContentLength];
                    upload.InputStream.Read(image, 0, image.Length);
                    employeeService.CreateEmployee(employee, UserId,image);
                    return RedirectToAction("Index");
                }
            }
            return View(new EmployeeViewModel());
        }
        public ActionResult Delete(int? id)
        {
            return View(employeeService.GetEmployeeById(id,UserId));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            return View(EmployeeIntoEmployeeViewModel.ConvertEmployeeIntoEmployeeViewModel(employeeService.GetEmployeeById(id,UserId)));
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId,BirthDate,Gender")]Employee emp,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0 && upload.ContentType.Contains("image"))
                {
                    byte[] image = new byte[upload.ContentLength];
                    upload.InputStream.Read(image, 0, image.Length);
                    employeeService.EditEmployee(emp, UserId,image);
                    return RedirectToAction("Index");
                }
            }
            return View(new EmployeeViewModel());  
        }
    }  
}