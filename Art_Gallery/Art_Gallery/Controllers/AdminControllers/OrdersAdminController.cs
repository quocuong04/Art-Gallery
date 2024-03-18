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
    public class OrdersAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: OrderAdmin
        public async Task<ActionResult> Index()
        {
            var purcher_order = db.Purcher_order.Include(p => p.Customer).Include(p=>p.Artwork).Include(p => p.Auction).Include(p => p.Status);
            return View(await purcher_order.ToListAsync());
        }

        
        // GET: OrderAdmin/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email");
            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Name");
            ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "AuctionName");
            ViewBag.StatusCode = new SelectList(db.Status.Where(a => a.StatusCode == "A" || a.StatusCode == "P" || a.StatusCode == "I"), "StatusCode", "StatusName");
            return View();
        }

        // POST: OrderAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TotalAmount,OrderDate,CustomerId,OrderId,ArtworkId,AuctionId,StatusCode")] Purcher_order purcher_order)
        {
            if (ModelState.IsValid)
            {
                db.Purcher_order.Add(purcher_order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Name", purcher_order.ArtworkId);
            ViewBag.AuctionId = new SelectList(db.Auctions, "AuctionId", "AuctionName", purcher_order.AuctionId);
            ViewBag.StatusCode = new SelectList(db.Status.Where(a => a.StatusCode == "A" || a.StatusCode == "P" || a.StatusCode == "I"), "StatusCode", "StatusName", purcher_order.StatusCode);
            return View(purcher_order);
        }

        // GET: OrderAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purcher_order purcher_order = await db.Purcher_order.FindAsync(id);
            if (purcher_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            ViewBag.StatusCode = new SelectList(db.Status.Where(a=>a.StatusCode == "A" || a.StatusCode == "P" || a.StatusCode =="I"), "StatusCode", "StatusName", purcher_order.StatusCode);
            return View(purcher_order);
        }

        // POST: OrderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TotalAmount,OrderDate,CustomerId,OrderId,ArtworkId")] Purcher_order purcher_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purcher_order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", purcher_order.CustomerId);
            return View(purcher_order);
        }

        // GET: OrderAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purcher_order purcher_order = await db.Purcher_order.FindAsync(id);
            if (purcher_order == null)
            {
                return HttpNotFound();
            }
            return View(purcher_order);
        }

        // POST: OrderAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Purcher_order purcher_order = await db.Purcher_order.FindAsync(id);
            db.Purcher_order.Remove(purcher_order);
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
