using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class NewsController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        // GET: News
        public ActionResult Index()
        {
            var news = db.News
            .OrderByDescending(n => n.CreateDate)
            .ToList();

            return View(news);
        }
    }
}