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
            var artworkDetails = from aw in db.Artworks
                                 join ar in db.Artists on aw.ArtistId equals ar.ArtistId
                                 join cat in db.Categories on aw.CategoryId equals cat.CategoryId
                                 where aw.ArtworkId == id
                                 select new ArtworkDetailsModel
                                 {
                                     Artwork = aw,
                                     Artist = ar,
                                     Category = cat
                                 };

            var artworkDetailsModel = artworkDetails.FirstOrDefault();

            if (artworkDetailsModel == null)
            {
                return HttpNotFound();
            }

            viewModel.ArtworkDetail = artworkDetailsModel;

            var saleArtworks =
                db.Artworks
                 .Join(
                     db.Artists,
                     artwork => artwork.ArtistId,
                     artist => artist.ArtistId,
                     (artwork, artist) => new { Artwork = artwork, Artist = artist }
                 )
                 .Join(
                     db.Categories,
                     joined => joined.Artwork.CategoryId,
                     category => category.CategoryId,
                     (joined, category) => new ArtWorkListModel
                     {
                         ArtworkId = joined.Artwork.ArtworkId,
                         Descriptions = joined.Artwork.Descriptions,
                         Price = joined.Artwork.Price,
                         Name = joined.Artwork.Name,
                         Image = joined.Artwork.Image,
                         ArtistName = joined.Artist.ArtistName,
                         CategoryName = category.CategoryName,
                         Discount = joined.Artwork.Discount
                     }
                 ).Where(a => a.Discount >= 1)
                .ToList();
            viewModel.SaleArtworks = saleArtworks.ToList();
            return View(viewModel);

        }
    }
    public class ArtworkDetailViewModel
    {
        public ArtworkDetailsModel ArtworkDetail { get; set; }
        public List<ArtWorkListModel> SaleArtworks { get; set; }

    }
    public class ArtworkDetailsModel
    {
        public Artwork Artwork { get; set; }
        public Artist Artist { get; set; }
        public Category Category { get; set; }
    }
}