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
    public class AuctionsController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: Auctions
        public async Task<ActionResult> Index()
        {
            var auctions = await (from a in db.Artworks
                                  join aa in db.Rel_Artwork_Auctions on a.ArtworkId equals aa.ArtworkId
                                  join au in db.Auctions on aa.AuctionId equals au.AuctionId
                                  group new { Artwork = a, Auction = au } by au into g
                                  select new ArtWorks_AuctionModel
                                  {
                                      Auction = g.Key,
                                      Artworks = g.Select(x => x.Artwork).Take(3).ToList(),
                                      ArtworksAll = g.Select(x => x.Artwork).ToList()
                                  }).ToListAsync();

            return View(auctions);
        }
        public class ArtWorks_AuctionModel
        {
            public List<Artwork> Artworks { get; set; }
            public List<Artwork> ArtworksAll { get; set; }
            public Auction Auction { get; set; }
        }
    }
}