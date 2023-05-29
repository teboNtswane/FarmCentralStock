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
    
    public class FarmersController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: Farmers

        //Access to All users
        [Authorize(Roles = "Admin,Employee,Farmer")]
        public ActionResult Index()
        {
            var farmers = db.Farmers.Include(f => f.User_Role);
            return View(farmers.ToList());
        }

        // GET: Farmers/Details/5

        //Access to All users
        [Authorize(Roles = "Admin,Employee,Farmer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = db.Farmers.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // GET: Farmers/Create

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Create()
        {
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role");
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role_Id,Name,Surname,Contact,Address")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                db.Farmers.Add(farmer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", farmer.Role_Id);
            return View(farmer);
        }

        // GET: Farmers/Edit/5

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = db.Farmers.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", farmer.Role_Id);
            return View(farmer);
        }

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role_Id,Name,Surname,Contact,Address")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farmer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", farmer.Role_Id);
            return View(farmer);
        }

        // GET: Farmers/Delete/5

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = db.Farmers.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // POST: Farmers/Delete/5

        //Access to Admin and Employee Only
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Farmer farmer = db.Farmers.Find(id);
            db.Farmers.Remove(farmer);
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
