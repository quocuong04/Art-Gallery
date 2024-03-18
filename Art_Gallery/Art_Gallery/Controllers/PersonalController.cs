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

namespace Art_Gallery.Controllers
{
    public class PersonalController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: Personal
        public async Task<ActionResult> Index()
        {
            string userEmail = Session["User"] as string;

            PersonalModel modelView = new PersonalModel();
            Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);

            if (customer == null)
            {
                return HttpNotFound(); 
            }

            modelView.UserInfor = customer;

            var favoriteArtworks = db.Rel_Customer_Artwork
                .Where(rel => rel.CustomerId == customer.CustomerId)
                .Select(rel => rel.Artwork)
                .Include(a => a.Artist)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .Include(a => a.Purcher_order)
                .Include(a => a.Status1)
                .ToList();
            modelView.ArtworkFavorit = favoriteArtworks;
            return View(modelView);
        }

        // GET: Personal/Edit/5
        public async Task<ActionResult> Edit()
        {
            string userEmail = Session["User"] as string;
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Login");  
            }

            Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);

            if (customer == null)
            {
                return HttpNotFound(); 
            }

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,CustomerName,ReditCard,Password,Address,PhoneNumber,Age,Sex,CustomerId,FavoriteArtwork")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFavorit(int id)
        {
            Artwork artwork = await db.Artworks.FindAsync(id);
            string userEmail = Session["User"] as string;
            Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);

            if (artwork != null)
            {
                Rel_Customer_Artwork rel = new Rel_Customer_Artwork();
                rel.ArtworkId = id;
                rel.CustomerId = customer.CustomerId;
                db.Rel_Customer_Artwork.Add(rel);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "ArtworkDetail", new { id = id });
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFavorite(int id)
        {
            string userEmail = Session["User"] as string;
            Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);

            if (customer != null)
            {
                Rel_Customer_Artwork rel = await db.Rel_Customer_Artwork.FirstOrDefaultAsync(r => r.ArtworkId == id && r.CustomerId == customer.CustomerId);

                if (rel != null)
                {
                    db.Rel_Customer_Artwork.Remove(rel);
                    await db.SaveChangesAsync();
                }
            }

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
    public class PersonalModel
    {
        public Customer UserInfor { get; set; }
        public List<Artwork> ArtworkFavorit { get; set; }
    }
}
