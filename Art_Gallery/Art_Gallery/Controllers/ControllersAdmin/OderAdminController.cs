using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Art_Gallery.Models;

namespace Art_Gallery.Controllers.ControllersAdmin
{
    public class OderAdminController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: OderAdmin
        public ActionResult Index()
        {
            var purcher_order = db.Purcher_order.Include(p => p.Customer);
            return View(purcher_order.ToList());
        }

        // GET: OderAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purcher_order purcher_order = db.Purcher_order.Find(id);
            if (purcher_order == null)
            {
                return HttpNotFound();
            }
            return View(purcher_order);
        }

        // GET: OderAdmin/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email");
            return View();
        }

        // POST: OderAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TotalAmount,OrderDate,CustomerId,OrderId,ArtworkId")] Purcher_order purcher_order)
        {
            if (ModelState.IsValid)
            {
                db.Purcher_order.Add(purcher_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            return View(purcher_order);
        }

        // GET: OderAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purcher_order purcher_order = db.Purcher_order.Find(id);
            if (purcher_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            return View(purcher_order);
        }

        // POST: OderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TotalAmount,OrderDate,CustomerId,OrderId,ArtworkId")] Purcher_order purcher_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purcher_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            return View(purcher_order);
        }

        // GET: OderAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purcher_order purcher_order = db.Purcher_order.Find(id);
            if (purcher_order == null)
            {
                return HttpNotFound();
            }
            return View(purcher_order);
        }

        // POST: OderAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purcher_order purcher_order = db.Purcher_order.Find(id);
            db.Purcher_order.Remove(purcher_order);
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
