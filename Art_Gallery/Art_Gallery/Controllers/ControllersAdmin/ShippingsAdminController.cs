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
    public class ShippingsAdminController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: ShippingsAdmin
        public ActionResult Index()
        {
            return View(db.Shippings.ToList());
        }

        // GET: ShippingsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipping shipping = db.Shippings.Find(id);
            if (shipping == null)
            {
                return HttpNotFound();
            }
            return View(shipping);
        }

        // GET: ShippingsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShippingsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingId,ActualDeliveryDate,EstimateDeliveryDate,ShippingDate,PaymentId")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                db.Shippings.Add(shipping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipping);
        }

        // GET: ShippingsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipping shipping = db.Shippings.Find(id);
            if (shipping == null)
            {
                return HttpNotFound();
            }
            return View(shipping);
        }

        // POST: ShippingsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingId,ActualDeliveryDate,EstimateDeliveryDate,ShippingDate,PaymentId")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shipping);
        }

        // GET: ShippingsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipping shipping = db.Shippings.Find(id);
            if (shipping == null)
            {
                return HttpNotFound();
            }
            return View(shipping);
        }

        // POST: ShippingsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipping shipping = db.Shippings.Find(id);
            db.Shippings.Remove(shipping);
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
