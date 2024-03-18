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

namespace Art_Gallery.Controllers.AdminControllers
{
    public class AuctionLiveAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: AuctionLive
        public async Task<ActionResult> Index()
        {
            var proposedAuction = from a in db.Artworks
                                  join aa in db.Rel_Artwork_Auctions on a.ArtworkId equals aa.ArtworkId
                                  join au in db.Auctions on aa.AuctionId equals au.AuctionId
                                  where au.Status.Equals("L")
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Endding(int id)
        {
            Auction aution = await db.Auctions.FindAsync(id);
            if (aution != null)
            {
                aution.Status = "I";
                db.Entry(aution).State = EntityState.Modified;

                var artworkIds = db.Rel_Artwork_Auctions
                    .Where(rel => rel.AuctionId == id)
                    .Select(rel => rel.ArtworkId)
                    .ToList();

                var artworks = await db.Artworks
                    .Where(artwork => artworkIds.Contains(artwork.ArtworkId))
                    .ToListAsync();

                foreach (var artwork in artworks)
                {
                    if(artwork.AuctionPrice != null)
                    {
                        artwork.Status = "I";
                        db.Entry(artwork).State = EntityState.Modified;

                        if (artwork.CustomerId != null)
                        {
                            Purcher_order orders = new Purcher_order();
                            orders.OrderDate = DateTime.Now;
                            orders.ArtworkId = artwork.ArtworkId;
                            orders.CustomerId = artwork.CustomerId;
                            orders.AuctionId = aution.AuctionId;
                            orders.TotalAmount = artwork.AuctionPrice;
                            db.Purcher_order.Add(orders);
                        }
                    } else
                    {
                        artwork.Status = "A";
                        db.Entry(artwork).State = EntityState.Modified;
                    }
                }
                
                await db.SaveChangesAsync();
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
}
