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
    public class PaymentAdminController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: PaymentAdmin
        public ActionResult Index()
        {
            var payment_gateways = db.Payment_gateways.Include(p => p.Purcher_order);
            return View(payment_gateways.ToList());
        }

        // GET: PaymentAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_gateways payment_gateways = db.Payment_gateways.Find(id);
            if (payment_gateways == null)
            {
                return HttpNotFound();
            }
            return View(payment_gateways);
        }

        // GET: PaymentAdmin/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId");
            return View();
        }

        // POST: PaymentAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,PaymentMethod,Amount,PaymentDate,EmployeeId,OrderId")] Payment_gateways payment_gateways)
        {
            if (ModelState.IsValid)
            {
                db.Payment_gateways.Add(payment_gateways);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // GET: PaymentAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_gateways payment_gateways = db.Payment_gateways.Find(id);
            if (payment_gateways == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // POST: PaymentAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,PaymentMethod,Amount,PaymentDate,EmployeeId,OrderId")] Payment_gateways payment_gateways)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_gateways).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // GET: PaymentAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_gateways payment_gateways = db.Payment_gateways.Find(id);
            if (payment_gateways == null)
            {
                return HttpNotFound();
            }
            return View(payment_gateways);
        }

        // POST: PaymentAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_gateways payment_gateways = db.Payment_gateways.Find(id);
            db.Payment_gateways.Remove(payment_gateways);
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
