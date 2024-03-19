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
    public class SearchController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: Search
        public async Task<ActionResult> Index(string searchTerm)
        {
            var artworks = db.Artworks.Include(a => a.Artist).Include(a => a.Category).Include(a => a.Customer).Include(a => a.Employee).Include(a => a.Purcher_order).Include(a => a.Status)
                            .Where(a => a.Name.Contains(searchTerm));
            return View(await artworks.ToListAsync());
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
