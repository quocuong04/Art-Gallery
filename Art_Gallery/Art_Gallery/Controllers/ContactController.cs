using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new Art_GalleryEntities()) 
                {
                    contact.CreateDate = DateTime.Now; 

                    contact.Email = contact.Email;
                    contact.PhoneNumber = contact.PhoneNumber;
                    contact.Message = contact.Message;
                    contact.Name = contact.Name;

                    dbContext.Contacts.Add(contact); 
                    dbContext.SaveChanges(); 
                }

                return RedirectToAction("Index");
            }

            return View(contact);
        }
    }
}