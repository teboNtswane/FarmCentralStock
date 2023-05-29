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
    public class Product_ListController : Controller
    {
        private FCSDatabaseEntities db = new FCSDatabaseEntities();

        // GET: Product_List
        [Authorize(Roles = "Admin,Employee,Farmer")]
        public ActionResult Index(string search)
        {
            var products = from prd in db.Product_List select prd;

            ViewData["CurrentFilter"] = search;
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(prd => prd.Product_Type.Equals(search));
            }

            var product_List = db.Product_List.Include(p => p.Farmer).Include(p => p.Product_Type);
            return View(product_List.ToList());
        }

        // GET: Product_List/Details/5
        [Authorize(Roles = "Admin,Employee,Farmer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_List product_List = db.Product_List.Find(id);
            if (product_List == null)
            {
                return HttpNotFound();
            }
            return View(product_List);
        }

        // GET: Product_List/Create
        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Create()
        {
            ViewBag.Farmer_ID = new SelectList(db.Farmers, "Id", "Name");
            ViewBag.Type_ID = new SelectList(db.Product_Type, "Id", "Description");
            return View();
        }

        // POST: Product_List/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type_ID,Farmer_ID,Product_Name,Price,Quantity,Date_Registered")] Product_List product_List)
        {
            if (ModelState.IsValid)
            {
                db.Product_List.Add(product_List);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Farmer_ID = new SelectList(db.Farmers, "Id", "Name", product_List.Farmer_ID);
            ViewBag.Type_ID = new SelectList(db.Product_Type, "Id", "Description", product_List.Type_ID);
            return View(product_List);
        }

        // GET: Product_List/Edit/5

        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_List product_List = db.Product_List.Find(id);
            if (product_List == null)
            {
                return HttpNotFound();
            }
            ViewBag.Farmer_ID = new SelectList(db.Farmers, "Id", "Name", product_List.Farmer_ID);
            ViewBag.Type_ID = new SelectList(db.Product_Type, "Id", "Description", product_List.Type_ID);
            return View(product_List);
        }

        // POST: Product_List/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type_ID,Farmer_ID,Product_Name,Price,Quantity,Date_Registered")] Product_List product_List)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_List).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Farmer_ID = new SelectList(db.Farmers, "Id", "Name", product_List.Farmer_ID);
            ViewBag.Type_ID = new SelectList(db.Product_Type, "Id", "Description", product_List.Type_ID);
            return View(product_List);
        }

        // GET: Product_List/Delete/5

        [Authorize(Roles = "Admin,Farmer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_List product_List = db.Product_List.Find(id);
            if (product_List == null)
            {
                return HttpNotFound();
            }
            return View(product_List);
        }

        // POST: Product_List/Delete/5

        [Authorize(Roles = "Admin,Farmer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_List product_List = db.Product_List.Find(id);
            db.Product_List.Remove(product_List);
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
