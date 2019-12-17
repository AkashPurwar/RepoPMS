using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class SampleController : Controller
    {
        private ProductEntities db = new ProductEntities();

        // GET: Sample
        public ActionResult Index()
        {
            var tbl_Product = db.Tbl_Product.Include(t => t.Tbl_Category);
            return View(tbl_Product.ToList());
        }

        // GET: Sample/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // GET: Sample/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Sample/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,CategoryId,Price,Currency,Units")] Tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Product.Add(tbl_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // GET: Sample/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // POST: Sample/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,CategoryId,Price,Currency,Units")] Tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // GET: Sample/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // POST: Sample/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            db.Tbl_Product.Remove(tbl_Product);
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
