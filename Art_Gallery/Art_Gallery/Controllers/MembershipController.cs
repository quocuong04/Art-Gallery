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
    public class MembershipController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create([Bind(Include = "RequestId,RequestType,RequestMessage,StatusCode")] Request req, string RequestMessage)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"];
                var customer = await db.Customers.FirstOrDefaultAsync(e => e.Email == user);
                var currentDate = DateTime.Now;

                req.RequestType = "M";
                req.RequestMessage = RequestMessage;
                req.CreateUserId = customer.CustomerId;
                req.CreateDate = currentDate;
                req.StatusCode = "A";
                db.Requests.Add(req);

                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}