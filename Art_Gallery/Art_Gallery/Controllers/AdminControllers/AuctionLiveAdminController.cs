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
    public class AuctionLiveAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: AuctionLive
        public async Task<ActionResult> Index()
        {
            var artworks = db.Artworks.Include(a => a.Artist).Include(a => a.Category).Include(a => a.Customer).Include(a => a.Employee).Include(a => a.Purcher_order);
            return View(await artworks.ToListAsync());
        }

        

        // GET: AuctionLive/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId");
            return View();
        }

        // POST: AuctionLive/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,Status,CountAuction,AuctionPrice,Discount,Name,CreateDate,Price,OrderId,EmployeeId,CustomerId,ArtistId,ArtworkId")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Artworks.Add(artwork);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // GET: AuctionLive/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // POST: AuctionLive/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,Status,CountAuction,AuctionPrice,Discount,Name,CreateDate,Price,OrderId,EmployeeId,CustomerId,ArtistId,ArtworkId")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artwork).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // GET: AuctionLive/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: AuctionLive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Artwork artwork = await db.Artworks.FindAsync(id);
            db.Artworks.Remove(artwork);
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
