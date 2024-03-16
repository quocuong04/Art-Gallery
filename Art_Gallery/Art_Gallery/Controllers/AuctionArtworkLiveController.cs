using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class AuctionArtworkLiveController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public ActionResult Index(int id)
        {
            var proposedAuction = from a in db.Artworks
                                  join aa in db.Rel_Artwork_Auctions on a.ArtworkId equals aa.ArtworkId
                                  join au in db.Auctions on aa.AuctionId equals au.AuctionId
                                  where au.AuctionId == id && au.Status.Equals("L")
                                  select new
                                  {
                                      Artwork = a,
                                      Auction = au
                                  };

            var artworkAuctionLiveList = proposedAuction.GroupBy(p => p.Auction)
                                        .Select(g => new ArtWork_AuctionModel
                                        {
                                            Artworks = g.Select(p => p.Artwork).ToList(),
                                            Auction = g.Key
                                        })
                                        .ToList();

            return View(artworkAuctionLiveList.FirstOrDefault());
        }
    }
}