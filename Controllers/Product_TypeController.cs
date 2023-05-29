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
    public class Product_TypeController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: Product_Type
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Index()
        {
            return View(db.Product_Type.ToList());
        }

        // GET: Product_Type/Details/5
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = db.Product_Type.Find(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // GET: Product_Type/Create
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] Product_Type product_Type)
        {
            if (ModelState.IsValid)
            {
                db.Product_Type.Add(product_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Type);
        }

        // GET: Product_Type/Edit/5
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = db.Product_Type.Find(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // POST: Product_Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] Product_Type product_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Type);
        }

        // GET: Product_Type/Delete/5
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = db.Product_Type.Find(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // POST: Product_Type/Delete/5
        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Type product_Type = db.Product_Type.Find(id);
            db.Product_Type.Remove(product_Type);
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
