using Art_Gallery.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class AuctionArtworkDetailController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public async Task<ActionResult> Index(int id)
        {
            var artwork = await db.Artworks.FindAsync(id);

            if (artwork == null)
            {
                return HttpNotFound();
            }
            ArtWorkAuctionDetailModel modelView = new ArtWorkAuctionDetailModel();
            var autionArtwork = await (from a in db.Auctions
                                       join ra in db.Rel_Artwork_Auctions on a.AuctionId equals ra.AuctionId
                                       join aw in db.Artworks on ra.ArtworkId equals aw.ArtworkId
                                       join c in db.Categories on aw.CategoryId equals c.CategoryId
                                       join ar in db.Artists on aw.ArtistId equals ar.ArtistId
                                       where aw.ArtworkId == id
                                       select new ArtWorkAuctionModel
                                       {
                                           Artwork = aw,
                                           Auction = a,
                                           Category = c,
                                           Artist = ar
                                       }).SingleOrDefaultAsync();
            if (autionArtwork == null)
            {
                return HttpNotFound();
            }
            modelView.ArworkAuction = autionArtwork;

            var requestActive = await db.Requests
                    .FirstOrDefaultAsync(r => r.StatusCode == "A" && r.RequestType == "B" && r.ArtworkId == id);

            modelView.Request = requestActive;

            return View(modelView);
        }
        public async Task<ActionResult> Create([Bind(Include = "RequestId,RequestType,StatusCode,PriceAuction,CreateUserId,CreateDate,ArtworkId")] Request req, int artworkId, int priceAuction)
        {
            if (ModelState.IsValid)
            {
                var user = Session["User"];
                var customer = await db.Customers.FirstOrDefaultAsync(e => e.Email == user);
                var currentDate = DateTime.Now;

                req.ArtworkId = artworkId;
                req.CreateDate = DateTime.Now;
                req.CreateUserId = customer.CustomerId;
                req.PriceAuction = priceAuction;
                req.StatusCode = "A";
                req.RequestType = "B";

                db.Requests.Add(req);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = artworkId });
        }
    }
    public class ArtWorkAuctionModel
    {
        public Artwork Artwork { get; set; }
        public Auction Auction { get; set; }
        public Category Category { get; set; }
        public Artist Artist { get; set; }

    }
    public class ArtWorkAuctionDetailModel
    {
        public ArtWorkAuctionModel ArworkAuction { get; set; }
        public Request Request { get; set; }

}
}