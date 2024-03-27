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
            Purcher_order order = await db.Purcher_order.FindAsync(payment_gateways.OrderId);
            if (order != null)
            {
                order.StatusCode = "A";
            }

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
