using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Art_Gallery.Models;

namespace Art_Gallery.Controllers.AdminControllers
{
    public class PaymentsAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: PaymentsAdmin
        public async Task<ActionResult> Index()
        {
            var payment_gateways = db.Payment_gateways.Include(p => p.Purcher_order);
            return View(await payment_gateways.ToListAsync());
        }

        
        // GET: PaymentsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId");
            return View();
        }

        // POST: PaymentsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaymentId,PaymentMethod,Amount,PaymentDate,EmployeeId,OrderId")] Payment_gateways payment_gateways)
        {
            if (ModelState.IsValid)
            {
                db.Payment_gateways.Add(payment_gateways);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // GET: PaymentsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_gateways payment_gateways = await db.Payment_gateways.FindAsync(id);
            if (payment_gateways == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // POST: PaymentsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PaymentId,PaymentMethod,Amount,PaymentDate,EmployeeId,OrderId")] Payment_gateways payment_gateways)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_gateways).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", payment_gateways.OrderId);
            return View(payment_gateways);
        }

        // GET: PaymentsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_gateways payment_gateways = await db.Payment_gateways.FindAsync(id);
            if (payment_gateways == null)
            {
                return HttpNotFound();
            }
            return View(payment_gateways);
        }

        // POST: PaymentsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payment_gateways payment_gateways = await db.Payment_gateways.FindAsync(id);
            db.Payment_gateways.Remove(payment_gateways);
            await db.SaveChangesAsync();
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
