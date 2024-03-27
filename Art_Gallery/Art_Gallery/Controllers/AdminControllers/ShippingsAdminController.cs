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
    public class ShippingsAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: ShippingsAdmin
        public async Task<ActionResult> Index()
        {
            var payment = db.Shippings
                .Include(a => a.Payment_gateways);
            return View(await payment.ToListAsync());
        }

        
        // GET: ShippingsAdmin/Create
        public ActionResult Create(int paymentId)
        {
            var payment = db.Payment_gateways
                .Include(a => a.Customer)
                .Include(a => a.Purcher_order)
                .Include(a => a.Purcher_order.Artwork)
                .SingleOrDefault(a => a.PaymentId == paymentId);
            return View(payment);
        }

        // POST: ShippingsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShippingId,ActualDeliveryDate,EstimateDeliveryDate,ShippingDate,PaymentId,ShipperName,PhoneNumberShipper")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                Payment_gateways payment = await db.Payment_gateways.FindAsync(shipping.PaymentId);
                if (payment != null)
                {
                    payment.StatusCode = "I";
                }
                shipping.StatusCode = "A";
                shipping.ShippingDate = DateTime.Now;
                db.Shippings.Add(shipping);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shipping);
        }

        // GET: ShippingsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipping shipping = await db.Shippings.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "ShippingId,ActualDeliveryDate,EstimateDeliveryDate,ShippingDate,PaymentId,ShipperName,PhoneNumberShipper,StatusCode")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipping).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shipping);
        }

        // GET: ShippingsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipping shipping = await db.Shippings.FindAsync(id);
            if (shipping == null)
            {
                return HttpNotFound();
            }
            return View(shipping);
        }

        // POST: ShippingsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shipping shipping = await db.Shippings.FindAsync(id);
            Payment_gateways payment = await db.Payment_gateways.FindAsync(shipping.PaymentId);
            if (payment != null)
            {
                payment.StatusCode = "A";
            }
            db.Shippings.Remove(shipping);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Endding(int id)
        {
            Shipping ship = await db.Shippings.FindAsync(id);
            if (ship != null)
            {

                ship.StatusCode = "I";
                await db.SaveChangesAsync();
            }
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
