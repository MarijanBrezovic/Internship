using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeMS.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace EmployeeMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        EmployeeDb db = new EmployeeDb();
        ApplicationDbContext adbc = new ApplicationDbContext();
       
        // GET: Employee
        public ActionResult Index()
        {
            
            string userId = User.Identity.GetUserId();
            var employees = db.Employees.Where(x => x.UserId == userId);
            return View(employees);
        }
        public ActionResult Details(int id)
        {

           

            var detail = db.Employees.Single(x => x.Id == id);
            return View(detail);

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
                Employee emp = new Employee();
                emp.Name = employee.Name;
                emp.BirthDate = employee.BirthDate;
                emp.Gender = employee.Gender;
                emp.UserId = User.Identity.GetUserId();
                db.Employees.Add(emp);
                db.SaveChanges();
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
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee =  db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.Save();

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee =  db.Employees.Find(id);
            if(employee==null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId,BirthDate,EmployeeSex")]Employee emp)
        {
            if(ModelState.IsValid)
            {
                db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(emp);
        }

    }

    
}