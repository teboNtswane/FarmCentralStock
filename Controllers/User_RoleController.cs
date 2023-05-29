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
    public class User_RoleController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: User_Role
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.User_Role.ToList());
        }

        // GET: User_Role/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // GET: User_Role/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role")] User_Role user_Role)
        {
            if (ModelState.IsValid)
            {
                db.User_Role.Add(user_Role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Role);
        }

        // GET: User_Role/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // POST: User_Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role")] User_Role user_Role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Role);
        }

        // GET: User_Role/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // POST: User_Role/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Role user_Role = db.User_Role.Find(id);
            db.User_Role.Remove(user_Role);
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
