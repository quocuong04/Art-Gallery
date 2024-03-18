using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class RegisterController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        public ActionResult Index()
        {
            var model = new RegisterViewModel(); 
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Any(c => c.Email == model.email))
                {
                    ModelState.AddModelError("Email", "Email is exist.");
                    return View("Index", model);
                }

                var customer = new Customer
                {
                    CustomerName = model.fullname,
                    Email = model.email,
                    Password = model.password
                };

                db.Customers.Add(customer);
                db.SaveChanges();
                Session["User"] = model.email;
                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }
        public class RegisterViewModel
        {
            [Required(ErrorMessage = "Please enter email")]
            [EmailAddress(ErrorMessage = "Email not invalid")]
            public string email { get; set; }

            [Required(ErrorMessage = "Please enter fullname.")]
            public string fullname { get; set; }

            [Required(ErrorMessage = "Please enter address")]
            public string address { get; set; }

            public string age { get; set; }

            public string sex { get; set; }

            [Required(ErrorMessage = "Please enter password")]
            [DataType(DataType.Password)]
            public string password { get; set; }
        }
    }
}