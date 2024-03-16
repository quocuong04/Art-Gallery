using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery.Controllers
{
    public class MembershipController : BaseController
    {
        // GET: Membership
        public ActionResult Index()
        {
            return View();
        }
    }
}