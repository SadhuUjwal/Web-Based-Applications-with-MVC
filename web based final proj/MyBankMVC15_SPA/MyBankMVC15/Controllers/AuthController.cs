﻿using MyBankMVC15.Models;
using MyBankMVC15.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBankMVC15.Controllers
{
    [HandleError]
    public class AuthController : Controller
    {
        public IMyAuthenticationService MyAuthService { get; set; }
        // public IMyMembershipService MyMembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (MyAuthService == null) { MyAuthService = new MyAuthenticationService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string a = ViewBag.Acc;
                if (MyAuthService.SignIn(model.Username, model.Password, false))
                {
                    model.Success = true;
                }
                else
                {
                    model.Success = false;
                    model.Message = "The user name or password provided is incorrect.";
                }
            }

            // If we got this far, something failed, redisplay form
            return Json(model);
        }

        public ActionResult LogOff()
        {
            MyAuthService.SignOut();
            return RedirectToAction("Index", "Home");
        }


        //[Authorize(Roles = "Customer")]



    }
}