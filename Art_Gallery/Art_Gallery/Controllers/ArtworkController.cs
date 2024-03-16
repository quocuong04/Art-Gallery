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
    public class ArtworkController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        // GET: Artwork
        public async Task<ActionResult> Index()
        {
            var artworks =
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
                 )
                .ToList();
            return View(artworks);
        }
    }
}