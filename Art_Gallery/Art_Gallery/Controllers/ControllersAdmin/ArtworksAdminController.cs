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
    public class ArtworksAdminController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: ArtworksAdmin
        public ActionResult Index()
        {
            var artworks = db.Artworks.Include(a => a.Artist).Include(a => a.Category).Include(a => a.Customer).Include(a => a.Employee).Include(a => a.Purcher_order);
            return View(artworks.ToList());
        }

        // GET: ArtworksAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // GET: ArtworksAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Email");
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId");
            return View();
        }

        // POST: ArtworksAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Status,CountAuction,AuctionPrice,Discount,Name,CreateDate,Price,OrderId,EmployeeId,CustomerId,ArtistId,ArtworkId")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Artworks.Add(artwork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // GET: ArtworksAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // POST: ArtworksAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Status,CountAuction,AuctionPrice,Discount,Name,CreateDate,Price,OrderId,EmployeeId,CustomerId,ArtistId,ArtworkId")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artwork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", artwork.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", artwork.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", artwork.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Purcher_order, "OrderId", "OrderId", artwork.OrderId);
            return View(artwork);
        }

        // GET: ArtworksAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: ArtworksAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            db.Artworks.Remove(artwork);
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
