using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class NewDetailController : Controller
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        // GET: NewDetail
        public ActionResult Index(int id)
        {
            New news = db.News.Find(id);
            return View(news);
        }
    }
}