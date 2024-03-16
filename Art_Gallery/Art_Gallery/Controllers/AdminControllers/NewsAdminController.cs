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

namespace Art_Gallery.Controllers.AdminControllers
{
    public class NewsAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: NewsAdmin
        public async Task<ActionResult> Index()
        {
            return View(await db.News.ToListAsync());
        }

        
        // GET: NewsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewId,Title,Description,Image,CreateDate")] New News, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    News.Image = fileName;
                }
                db.News.Add(News);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(News);
        }

        // GET: NewsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = await db.News.FindAsync(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: NewsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewId,Title,Description,Image,CreateDate")] New News, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);

                    image.SaveAs(filePath);

                    News.Image = fileName;
                }
                else
                {
                    New existingNew = db.News.AsNoTracking().FirstOrDefault(a => a.NewId == News.NewId);
                    News.Image = existingNew.Image;
                }

                db.Entry(News).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(News);
        }

        // GET: NewsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = await db.News.FindAsync(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: NewsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            New @new = await db.News.FindAsync(id);
            db.News.Remove(@new);
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
    }
}
