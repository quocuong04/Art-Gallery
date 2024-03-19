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
        public ActionResult Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // Save the contact information to the database
                using (var dbContext = new Art_GalleryEntities()) // Replace "YourDbContext" with your actual DbContext class
                {
                    contact.CreateDate = DateTime.Now; // Set the creation date to the current date and time

                    // Assign the values from the form to the Contact object
                    contact.Email = contact.Email;
                    contact.PhoneNumber = contact.PhoneNumber;
                    contact.Message = contact.Message;
                    contact.Name = contact.Name;

                    dbContext.Contacts.Add(contact); // Add the contact object to the Contacts DbSet
                    dbContext.SaveChanges(); // Save changes to the database
                }

                // Optionally, you can redirect to a success page or display a success message
                return RedirectToAction("Success");
            }

            // If the model state is not valid, return the view with validation errors
            return View(contact);
        }
    }
}