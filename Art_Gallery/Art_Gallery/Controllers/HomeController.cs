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
    public class HomeController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();

            var auctionLive = (from a in db.Artworks
                               join aa in db.Rel_Artwork_Auctions on a.ArtworkId equals aa.ArtworkId
                               join au in db.Auctions on aa.AuctionId equals au.AuctionId
                               select new
                               {
                                   Artwork = a,
                                   Auction = au
                               })
                  .ToList()
                  .Where(p => p.Auction.Status == "L")
                  .GroupBy(p => p.Auction)
                  .Select(g => new ArtWork_AuctionModel
                  {
                      Artworks = g.Select(p => p.Artwork).ToList(),
                      Auction = g.Key
                  })
                  .ToList();


            viewModel.AuctionLive = auctionLive.FirstOrDefault();

            var proposedAuction = from a in db.Artworks
                                  join aa in db.Rel_Artwork_Auctions on a.ArtworkId equals aa.ArtworkId
                                  join au in db.Auctions on aa.AuctionId equals au.AuctionId
                                  where au.StartDate == db.Auctions.OrderByDescending(x => x.StartDate).FirstOrDefault().StartDate
                                  select new
                                  {
                                      Artwork = a,
                                      Auction = au
                                  };

            var artworkAuctionList = proposedAuction.GroupBy(p => p.Auction)
                                        .Select(g => new ArtWork_AuctionModel
                                        {
                                            Artworks = g.Select(p => p.Artwork).Take(3).ToList(),
                                            ArtworksAll = g.Select(p => p.Artwork).ToList(),
                                            Auction = g.Key
                                        })
                                        .ToList();

            viewModel.ProposedAuction = artworkAuctionList.FirstOrDefault();

            var currentDate = DateTime.Now;
            var oneMonthAgo = currentDate.AddMonths(-1);

            var newArtworks = db.Artworks
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
                     CreateDate = joined.Artwork.CreateDate
                 }
             )
             .Where(a => a.CreateDate >= oneMonthAgo && a.CreateDate <= currentDate)
             .ToList();

            viewModel.NewArtworks = newArtworks;

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
                         Image  = joined.Artwork.Image,
                         ArtistName = joined.Artist.ArtistName,
                         CategoryName = category.CategoryName,
                         Discount = joined.Artwork.Discount
                     }
                 ).Where(a => a.Discount >= 1)
                .ToList();

            viewModel.SaleArtworks = saleArtworks.ToList();

            viewModel.Artists = db.Artists.ToList();

            viewModel.News = db.News
            .OrderByDescending(n => n.CreateDate)
            .Take(3)
            .ToList();

            return View(viewModel);
        }

    }
    public class HomeViewModel
    {
        public List<Artist> Artists { get; set; }
        public List<ArtWorkListModel> SaleArtworks { get; set; }
        public ArtWork_AuctionModel ProposedAuction { get; set; }
        public ArtWork_AuctionModel AuctionLive { get; set; }
        public List<ArtWorkListModel> NewArtworks { get; set; }
        public List<New> News { get; set; }

    }
    public class ArtWork_AuctionModel
    {
        public List<Artwork> Artworks { get; set; }
        public List<Artwork> ArtworksAll { get; set; }
        public Auction Auction { get; set; }
    }

    public class ArtWorkListModel: Artwork
    {
        public string ArtistName { get; set; }
        public string CategoryName { get; set; }
    }
}