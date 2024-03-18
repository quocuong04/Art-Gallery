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
    public class ArtworkDetailController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public async Task<ActionResult> Index(int id)
        {
            ArtworkDetailViewModel viewModel = new ArtworkDetailViewModel();
            var artworkDetails = db.Artworks.Include(a => a.Artist)
                                .Include(a => a.Category)
                                .Include(a => a.Customer)
                                .Include(a => a.Employee)
                                .Include(a => a.Purcher_order)
                                .Include(a => a.Status1)
                                .FirstOrDefault(a => a.ArtworkId == id);

            if (artworkDetails == null)
            {
                return HttpNotFound();
            }

            viewModel.ArtworkDetail = artworkDetails;

            var saleArtworks = db.Artworks.Include(a => a.Artist)
                                .Include(a => a.Category)
                                .Include(a => a.Customer)
                                .Include(a => a.Employee)
                                .Include(a => a.Purcher_order)
                                .Include(a => a.Status1)
                                .Where(a => a.Discount >= 1)
                                .ToList();

            viewModel.SaleArtworks = saleArtworks;

            var reviews = db.Reviews.Where(a => a.ArtworkId == id).Include(a=>a.Customer).ToList();
            viewModel.reviews = reviews.ToList();
            var isFavorit = await IsArtworkInFavorites(id);

            viewModel.isFavorit = isFavorit;

            return View(viewModel);

        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, string reviewMessage)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"];
                var customer = db.Customers.FirstOrDefault(e => e.Email == user);
                if (id != null && customer != null)
                {
                    Review review = new Review();
                    review.ArtworkId = id;
                    review.Reviews = reviewMessage;
                    review.CustomerId = customer.CustomerId;

                    db.Reviews.Add(review);
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", new {id=id});
        }
        public async Task<bool> IsArtworkInFavorites(int artworkId)
        {
            var user = Session["User"];
            Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == user);

            if (customer != null)
            {
                return !(await db.Rel_Customer_Artwork.AnyAsync(r => r.ArtworkId == artworkId && r.CustomerId == customer.CustomerId));
            }

            return false;
        }
    }
    
    public class ArtworkDetailViewModel
    {
        public Artwork ArtworkDetail { get; set; }
        public List<Artwork> SaleArtworks { get; set; }
        public List<Review> reviews { get; set; }

        public bool isFavorit { get; set; }

    }
    public class ArtworkDetailsModel
    {
        public Artwork Artwork { get; set; }
        public Artist Artist { get; set; }
        public Category Category { get; set; }
    }
}