using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class OrderCustomerController : BaseController
    {
        // GET: OrderCustomer
        private Art_GalleryEntities db = new Art_GalleryEntities();

        public async Task<ActionResult> Index()
        {
            var username = Session["User"];
            var user = db.Customers.FirstOrDefault(u => u.Email == username);
            var purcher_order = db.Purcher_order.Include(p => p.Customer).Include(p => p.Artwork).Include(p => p.Auction).Include(p => p.Status)
                                .Where(a => a.CustomerId == user.CustomerId);
            return View(await purcher_order.ToListAsync());
        }

    }
}
