using Art_Gallery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            var model = new LoginViewModel(); // Khởi tạo model
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = db.Customers.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email does not exist");
                return View("Index", model);
            }
            else
            {
                if (user.Password == model.Password)
                {
                    Session["User"] = user.Email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect password");
                    return View("Index", model);
                }
            }
        }
    }
    public class BaseController : Controller
    {
        protected async override void OnActionExecuting(ActionExecutingContext filterContext)
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
                    
                    if (employee !=null)
                    {
                        var functions = (from f in dbContext.Functions
                                         join g in dbContext.Groups on f.FunctionId equals g.FunctionId
                                         join e in dbContext.Employees on g.EmployeeId equals e.EmployeeId
                                         select new
                                         {
                                             Function = f
                                         }).ToList();

                        ViewBag.Functions = functions;
                        ViewBag.AllowedFunctions = functions.Select(f => f.Function.FunctionCode).ToList();
                        var allowedFunctions = filterContext.Controller.ViewBag.AllowedFunctions as List<string>;

                        foreach (var function in functions)
                        {
                            if (!allowedFunctions.Contains(function.Function.FunctionCode))
                            {
                                filterContext.Result = new RedirectResult("/Unauthorized");
                                break; 
                            }
                        }
                        var requests = dbContext.Requests.Include(n => n.Artwork).Include(n => n.Customer).Include(n => n.Status)
                            .Where(e => e.StatusCode == "A").ToList();
                        ViewBag.Requestlist = requests;
                    }
                    
                    if (employee != null)
                    {
                        ViewBag.isMember = true;
                    }
                    else
                    {
                        ViewBag.isMember = false;
                    }
                    if (IsMemberShipController(controllerName))
                    {
                        filterContext.Result = new RedirectResult("/Home");
                    }
                }

                ViewBag.UserName = user;

            }
            else
            {
                if (isAdminController|| IsLogin(controllerName))
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
        private bool IsMemberShipController(string controllerName)
        {
            return controllerName.Contains("MemberShipController");


        }
        private bool IsLogin(string controllerName)
        {
            return controllerName.Contains("OrderCustomerController")
                    || controllerName.Contains("PersonalController");
        }
    }
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Email not invalid")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter fullname.")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
