using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class PaymentController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        // GET: Payment
        public ActionResult Index(int orderId)
        {
            var payment = db.Purcher_order
                .SingleOrDefault(a => a.OrderId == orderId);

            var paymentMethods = new List<SelectListItem>
            {
                new SelectListItem { Value = "Cash", Text = "Cash" },
            };

            ViewBag.PaymentMethods = paymentMethods;
            ViewBag.ArtworkName = payment.Artwork.Name;
            ViewBag.TotalAmount = payment.TotalAmount;
            return View(payment);
        }

        // POST: Payment/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "PaymentId,PaymentMethod,Amount,PaymentDate,CustomerId,OrderId,PhoneNumber,Address")] Payment_gateways payment_gateways)
        {
            try
            {
                Purcher_order order = await db.Purcher_order.FindAsync(payment_gateways.OrderId);
                if (order != null)
                {
                    order.StatusCode = "I";
                }
                payment_gateways.StatusCode = "A";
                payment_gateways.PaymentDate = DateTime.Now;
                db.Payment_gateways.Add(payment_gateways);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "OrderCustomer");
            }
            catch
            {
                return View();
            }
        }
    }
}
