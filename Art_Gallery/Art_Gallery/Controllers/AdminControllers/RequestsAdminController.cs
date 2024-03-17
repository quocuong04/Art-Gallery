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
    public class RequestsAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: RequestsAdmin
        public async Task<ActionResult> Index()
        {
            var requests = db.Requests.Include(n => n.Artwork).Include(n => n.Customer).Include(n => n.Status);
            return View(await requests.ToListAsync());
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reject(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request != null)
            {
                request.StatusCode = "R";
                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Accept(int id, string requestType)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request != null)
            {
                request.StatusCode = "AC";
                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (requestType == "M")
                {
                    Customer customer = await db.Customers.FindAsync(request.CreateUserId);
                    if (customer != null)
                    {
                        Employee employee = new Employee();
                        employee.Email = customer.Email;
                        employee.EmployeeName = customer.CustomerName;
                        employee.Adress = customer.Address;
                        employee.PhoneNumber = customer.PhoneNumber;
                        employee.Sex = customer.Sex;
                        employee.Age = customer.Age;

                        db.Employees.Add(employee);
                        await db.SaveChangesAsync();
                    }
                }
                else
                {
                    Artwork artwork = await db.Artworks.FindAsync(request.ArtworkId);
                    if (artwork != null)
                    {
                        artwork.CustomerId = request.CreateUserId;
                        artwork.AuctionPrice = request.PriceAuction;
                        artwork.CountAuction = artwork.CountAuction != null ? artwork.CountAuction + 1 : 1;
                        db.Entry(artwork).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // GET: RequestsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request req = await db.Requests.FindAsync(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        // POST: RequestsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Request req = await db.Requests.FindAsync(id);
            db.Requests.Remove(req);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Detail(int id)
        {
            var request = await db.Requests
        .Include(n => n.Artwork)
        .Include(n => n.Customer)
        .Include(n => n.Status)
        .FirstOrDefaultAsync(e => e.RequestId == id);

            if (request == null)
            {
                return HttpNotFound();
            }

            return View(request);
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
