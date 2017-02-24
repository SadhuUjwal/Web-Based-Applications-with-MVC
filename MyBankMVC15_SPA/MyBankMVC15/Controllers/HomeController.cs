using MyBankMVC15.Models;
using MyBankMVC15.Services;
using MyBankMVC15.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBankMVC15.Controllers
{
    public class HomeController : Controller
    {
        public IMyAuthenticationService MyAuthService { get; set; }
        AuthController au = new AuthController();
        //public IMyAuthenticationService MyAuthService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        
        /*
         public ActionResult XferChkToSav(LoginModel model, String returnUrl)
         {

             if (ModelState.IsValid)
             {
                 if (MyAuthService.SignIn(model.Username, model.Password, false))
                 {
                     ViewBag.Acc = MyAuthService.GetAccNo(model.Username);
                     if (!String.IsNullOrEmpty(returnUrl))
                     {
                         return Redirect(returnUrl);
                     }
                     else
                     {
                         return RedirectToAction("XferChkToSav", "Home");
                     }
                 }
                 else
                 {
                     ModelState.AddModelError("", "The user name or password provided is incorrect.");
                 }
             }
             return View(model);
         }*/

    }
}