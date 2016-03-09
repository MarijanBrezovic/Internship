using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeMS.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using EmployeeMS.Services;
using PagedList;

namespace EmployeeMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;
        public EmployeeController()
        {
            this.employeeService = new EmployeeService(new EmployeeDb());
        }

        // GET: Employee
        public ActionResult Index(string sortOrder,string searchBy, string currentFilter, int? pageNo, int perPage=5)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GenderSortParm = sortOrder == "Gender" ? "gender_desc" : "Gender";
            ViewBag.CurrentFilter = searchBy;
            List<SelectListItem> numberOfEmployees = new List<SelectListItem>();
            numberOfEmployees.Add(new SelectListItem() { Text="2",Value="2"});
            numberOfEmployees.Add(new SelectListItem() { Text = "5", Value = "5" });
            numberOfEmployees.Add(new SelectListItem() { Text = "10", Value = "10" });
            ViewBag.perPage = numberOfEmployees;
            ViewBag.CurrentItemsPerPage = perPage;
            if(searchBy!=null)
            {
                pageNo = 1;
            }
            else
            {
                searchBy = currentFilter;
            }

            int pageNumber = (pageNo ?? 1);
            if (!String.IsNullOrEmpty(searchBy))
            {
                
                return View(employeeService.SearchEmployee(searchBy).OrderBy(x=>x.Name).ToPagedList(pageNumber, perPage));
                
            }
            
            return View(employeeService.Sorting(sortOrder).ToPagedList(pageNumber, perPage));

        }
        public ActionResult Details(int id)
        {
            return View(employeeService.GetEmployeeById(id));

        }
        [HttpGet]
        public ActionResult Create()
        {

            var model = new Employee();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                employeeService.CreateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            employeeService.DeleteEmployee(id);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeService.GetEmployeeById(id);
            if(employee==null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId,BirthDate,Gender")]Employee emp)
        {
            if(ModelState.IsValid)
            {
                employeeService.EditEmployee(emp);
                return RedirectToAction("Index");

            }
            return View(emp);
        }

    }

    
}