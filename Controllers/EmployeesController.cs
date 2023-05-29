using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FarmCentralStock.Models;

namespace FarmCentralStock.Controllers
{
    public class EmployeesController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: Employees
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.User_Role).Include(e => e.Employee_Type);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
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

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role");
            ViewBag.Employee_Type_Id = new SelectList(db.Employee_Type, "Id", "Description");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role_Id,Employee_Type_Id,Name,Surname,Contact")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", employee.Role_Id);
            ViewBag.Employee_Type_Id = new SelectList(db.Employee_Type, "Id", "Description", employee.Employee_Type_Id);
            return View(employee);
        }

        // GET: Employees/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", employee.Role_Id);
            ViewBag.Employee_Type_Id = new SelectList(db.Employee_Type, "Id", "Description", employee.Employee_Type_Id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role_Id,Employee_Type_Id,Name,Surname,Contact")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", employee.Role_Id);
            ViewBag.Employee_Type_Id = new SelectList(db.Employee_Type, "Id", "Description", employee.Employee_Type_Id);
            return View(employee);
        }

        // GET: Employees/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
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

        // POST: Employees/Delete/5

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
