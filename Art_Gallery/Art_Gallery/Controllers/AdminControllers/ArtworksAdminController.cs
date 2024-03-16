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
using System.IO;
using System.Web.Helpers;

namespace Art_Gallery.Controllers.AdminControllers
{
    public class ArtworksAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();     

        // GET: ArtworksAdmin
        public async Task<ActionResult> Index()
        {
            var artworks = db.Artworks.Include(a => a.Artist).Include(a => a.Category).Include(a => a.Customer).Include(a => a.Employee).Include(a => a.Purcher_order);
            return View(await artworks.ToListAsync());
        }


        // GET: ArtworksAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ArtworksAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,Discount,Name,Price,ArtistId,ArtworkId, Image")] Artwork artwork, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    artwork.Image = fileName;
                }
                var currentDate = DateTime.Now;
                var currentUserEmail = (string)Session["User"];
                var employee = db.Employees.FirstOrDefault(e => e.Email == currentUserEmail);

                artwork.CreateDate = currentDate;
                artwork.EmployeeId = employee.EmployeeId;

                // Lưu đối tượng Artwork vào cơ sở dữ liệu
                db.Artworks.Add(artwork);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
            return View(artwork);
        }

        // GET: ArtworksAdmin/Edit/5
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
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", artwork.EmployeeId);
            return View(artwork);
        }

        // POST: ArtworksAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,Status,CountAuction,AuctionPrice,Discount,Name,CreateDate,Price,OrderId,EmployeeId,CustomerId,ArtistId,ArtworkId,Image")] Artwork artwork, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    artwork.Image = fileName;
                }
                else
                {
                    Artwork existingArtwork = db.Artworks.AsNoTracking().FirstOrDefault(a => a.ArtworkId == artwork.ArtworkId);
                    artwork.Image = existingArtwork.Image;
                }

                db.Entry(artwork).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "ArtistName", artwork.ArtistId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", artwork.EmployeeId);
            return View(artwork);
        }

        // GET: ArtworksAdmin/Delete/5
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

        // POST: ArtworksAdmin/Delete/5
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
