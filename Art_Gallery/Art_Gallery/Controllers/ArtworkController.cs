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
            var artworks = db.Artworks.Include(a => a.Artist)
                                .Include(a => a.Category)
                                .Include(a => a.Customer)
                                .Include(a => a.Employee)
                                .Include(a => a.Purcher_order)
                                .Include(a => a.Status1).ToList();
            return View(artworks);
        }
    }
}