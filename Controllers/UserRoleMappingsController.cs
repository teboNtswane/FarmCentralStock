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
    public class UserRoleMappingsController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: UserRoleMappings
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var userRoleMappings = db.UserRoleMappings.Include(u => u.User).Include(u => u.User_Role);
            return View(userRoleMappings.ToList());
        }

        // GET: UserRoleMappings/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRoleMapping);
        }

        // GET: UserRoleMappings/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username");
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role");
            return View();
        }

        // POST: UserRoleMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,Role_Id")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                db.UserRoleMappings.Add(userRoleMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", userRoleMapping.User_Id);
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", userRoleMapping.Role_Id);
            return View(userRoleMapping);
        }

        // GET: UserRoleMappings/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", userRoleMapping.User_Id);
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", userRoleMapping.Role_Id);
            return View(userRoleMapping);
        }

        // POST: UserRoleMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,Role_Id")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRoleMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Username", userRoleMapping.User_Id);
            ViewBag.Role_Id = new SelectList(db.User_Role, "Id", "Role", userRoleMapping.Role_Id);
            return View(userRoleMapping);
        }

        // GET: UserRoleMappings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRoleMapping);
        }

        // POST: UserRoleMappings/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            db.UserRoleMappings.Remove(userRoleMapping);
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
