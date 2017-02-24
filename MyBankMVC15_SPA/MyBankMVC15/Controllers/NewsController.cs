using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBankMVC15.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin,Customer")]
        public ActionResult News()
        {
            ViewBag.Username = HttpContext.User.Identity.Name;
            return View();
        }
    }
}