using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast;

namespace Art_Gallery.Controllers
{
    public class LoginController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Customers.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not exist");
                return View();
            } else
            {
                if(user.Password == password)
                {
                    Session["User"] = user.Email;
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ModelState.AddModelError("", "Password is wrong");
                    return View();
                }
            }
        }
    }
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = Session["User"];
            var controllerName = filterContext.Controller.GetType().Name;
            var isAdminController = IsAdminController(controllerName);
            if (user != null)
            {
                using (var dbContext = new Art_GalleryEntities())
                {
                    var customer = dbContext.Customers.FirstOrDefault(c => c.Email == user);
                    var employee = dbContext.Employees.FirstOrDefault(e => e.Email == user);

                    if (customer != null)
                    {
                        ViewBag.FullName = customer.CustomerName;
                    }
                   
                    if (employee == null && isAdminController)
                    {
                        filterContext.Result = new RedirectResult("/Home");
                    }

                    if(employee !=null)
                    {
                        var functions = (from f in dbContext.Functions
                                         join g in dbContext.Groups on f.FunctionId equals g.FunctionId
                                         join e in dbContext.Employees on g.EmployeeId equals e.EmployeeId
                                         select new
                                         {
                                             Function = f
                                         }).ToList();

                        ViewBag.Functions = functions;
                        ViewBag.AllowedFunctions = functions.Select(f => f.Function.FunctionName).ToList();
                        var allowedFunctions = filterContext.Controller.ViewBag.AllowedFunctions as List<string>;

                        foreach (var function in functions)
                        {
                            if (!allowedFunctions.Contains(function.Function.FunctionName))
                            {
                                filterContext.Result = new RedirectResult("/Unauthorized");
                                break; 
                            }
                        }

                    }

                }

                ViewBag.UserName = user;
                
            }
            else
            {
                if (isAdminController)
                {
                    filterContext.Result = new RedirectResult("/Home");
                }
            }

            base.OnActionExecuting(filterContext);
        }
        private bool IsAdminController(string controllerName)
        {
            return controllerName.Contains("AdminController");
        }
    }

}
