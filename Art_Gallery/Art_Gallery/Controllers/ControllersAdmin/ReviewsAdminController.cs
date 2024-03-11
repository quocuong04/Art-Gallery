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
    public class ReviewsAdminController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: ReviewsAdmin
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Artwork).Include(r => r.Customer);
            return View(reviews.ToList());
        }

        // GET: ReviewsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: ReviewsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Status");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email");
            return View();
        }

        // POST: ReviewsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewId,CustomerId,ArtworkId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Status", review.ArtworkId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", review.CustomerId);
            return View(review);
        }

        // GET: ReviewsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Status", review.ArtworkId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", review.CustomerId);
            return View(review);
        }

        // POST: ReviewsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewId,CustomerId,ArtworkId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtworkId = new SelectList(db.Artworks, "ArtworkId", "Status", review.ArtworkId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Email", review.CustomerId);
            return View(review);
        }

        // GET: ReviewsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: ReviewsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
