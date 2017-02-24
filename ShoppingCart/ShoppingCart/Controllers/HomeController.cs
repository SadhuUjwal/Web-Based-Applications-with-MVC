using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Home Page ActionResult
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContactUS()
        {
            return View("ContactUS");
        }

    }
}