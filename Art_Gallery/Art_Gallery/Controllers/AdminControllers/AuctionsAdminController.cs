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
using System.Data.Entity.Infrastructure;

namespace Art_Gallery.Controllers.AdminControllers
{
    public class AuctionsAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: AuctionsAdmin
        public async Task<ActionResult> Index()
        {
            var auctions = await (from au in db.Auctions
                                  join aa in db.Rel_Artwork_Auctions on au.AuctionId equals aa.AuctionId into gj
                                  from subAuctionArtwork in gj.DefaultIfEmpty()
                                  join a in db.Artworks on subAuctionArtwork.ArtworkId equals a.ArtworkId into gj2
                                  from subArtwork in gj2.DefaultIfEmpty()
                                  select new { Auction = au, Artwork = subArtwork })
                                  .GroupBy(x => x.Auction)
                                  .Select(g => new ArtWork_AuctionModel
                                  {
                                      Auction = g.Key,
                                      Artworks = g.Select(x => x.Artwork).ToList(),
                                      StatusName = db.Status.Where(a => a.StatusCode == g.Key.Status).Select(a => a.StatusName).FirstOrDefault()
                                  })
                                  .ToListAsync();

            return View(auctions);
        }



        // GET: AuctionsAdmin/Create
        public ActionResult Create()
        {
            var artworkWithoutAuction = db.Artworks.Where(a => a.Status == null).ToList();

            ViewBag.Artworks = new MultiSelectList(artworkWithoutAuction, "ArtworkId", "Name");
            return View();
        }

        // POST: AuctionsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuctionId,EndDate,StartDate,AuctionName")] Auction auction, HttpPostedFileBase image, int[] selectedArtworkIds)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    auction.Image = fileName;
                }

                auction.Status = "A";
                db.Auctions.Add(auction);

                await db.SaveChangesAsync();

                if (selectedArtworkIds != null && selectedArtworkIds.Length > 0)
                {
                    foreach (var artworkId in selectedArtworkIds)
                    {
                        var relArtworkAuction = new Rel_Artwork_Auctions
                        {
                            ArtworkId = artworkId,
                            AuctionId = auction.AuctionId
                        };
                        var artwork = db.Artworks.Find(artworkId);
                        if (artwork != null)
                        {
                            artwork.Status = "A";
                        }

                        db.Rel_Artwork_Auctions.Add(relArtworkAuction);
                    }

                    await db.SaveChangesAsync();
                }
        
                return RedirectToAction("Index");
            }

            return View(auction);
        }

        // GET: AuctionsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artworkIds = await (from rel in db.Rel_Artwork_Auctions
                                    where rel.AuctionId == id
                                    select rel.ArtworkId).ToArrayAsync();

            var auctionStatus = await db.Auctions.FindAsync(id);
            var statusList = await db.Status.ToListAsync();

            var artworks = await db.Artworks
                .Where(a => a.Status == null || (a.Status == "A" && artworkIds.Contains(a.ArtworkId)) || (a.Status == "L" && auctionStatus.Status == "L" && artworkIds.Contains(a.ArtworkId)))
                .ToListAsync();

            ViewBag.Artworks = new MultiSelectList(artworks, "ArtworkId", "Name", artworkIds);
            ViewBag.StatusList = new SelectList(statusList, "StatusCode", "StatusName", auctionStatus.Status);

            var model = new AuctionEditModel
            {
                Auction = await db.Auctions.FindAsync(id),
                SelectedArtworkIds = artworkIds,
                StatusSelected = auctionStatus.Status
            };

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: AuctionsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuctionId, EndDate, StartDate,Status, AuctionName")] Auction auction, HttpPostedFileBase image, int[] selectedArtworkIds, string statusSelected)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    auction.Image = fileName;
                }
                else
                {
                    Auction existingAuction = db.Auctions.AsNoTracking().FirstOrDefault(a => a.AuctionId == auction.AuctionId);
                    if (existingAuction != null)
                    {
                        auction.Image = existingAuction.Image;
                    }
                }
                auction.Status = statusSelected;

                db.Entry(auction).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (selectedArtworkIds != null && selectedArtworkIds.Length > 0)
                {
                    var existingRelArtworkAuctions = db.Rel_Artwork_Auctions.Where(rel => rel.AuctionId == auction.AuctionId);
                    db.Rel_Artwork_Auctions.RemoveRange(existingRelArtworkAuctions);

                    foreach (var artworkId in selectedArtworkIds)
                    {
                        var relArtworkAuction = new Rel_Artwork_Auctions
                        {
                            ArtworkId = artworkId,
                            AuctionId = auction.AuctionId
                        };
                        var artwork = db.Artworks.Find(artworkId);
                        if (artwork != null)
                        {
                            if(auction.Status == "L")
                            {
                                artwork.Status = "L";
                            } else
                            {
                                artwork.Status = "A";
                            }
                        }
                        db.Rel_Artwork_Auctions.Add(relArtworkAuction);
                    }

                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            return View(auction);
        }

        // GET: AuctionsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = await db.Auctions.FindAsync(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }

        // POST: AuctionsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Auction auction = await db.Auctions.FindAsync(id);
            db.Auctions.Remove(auction);

            var artworkIds = await (from rel in db.Rel_Artwork_Auctions
                                    where rel.AuctionId == id
                                    select rel.ArtworkId).ToArrayAsync();
            await db.Artworks
            .Where(a => artworkIds.Contains(a.ArtworkId))
            .ForEachAsync(a => a.Status = null);

            db.Rel_Artwork_Auctions
                .RemoveRange(db.Rel_Artwork_Auctions.Where(rel => rel.AuctionId == id));
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
        public class AuctionEditModel
        {
            public Auction Auction { get; set; }
            public int[] SelectedArtworkIds { get; set; }
            public string StatusSelected { get; set; }
        }
        public class ArtWorkAuctionModifyModel
        {
            public Artwork Artwork { get; set; }
            public Auction Auction { get; set; }
            public List<int> SelectedArtworkIds { get; set; }
        }
    }
}
