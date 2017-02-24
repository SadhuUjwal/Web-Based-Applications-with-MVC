using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DAL;
using ShoppingCart.Services;
using System.Web.Routing;

namespace ShoppingCart.Controllers
{
    public class CustomerController : Controller
    {

        public IMyAuthenticationService MyAuthService { get; set; }
        // public IMyMembershipService MyMembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (MyAuthService == null) { MyAuthService = new MyAuthenticationService(); }

            base.Initialize(requestContext);
        }


        // GET: Customer
        //ActionResult for Registration Form 
        public ActionResult Registration()
        {
            return View();
        }

        //ActionResult for Registration Form To Save Data To Database
        [HttpPost]
   
        public ActionResult Registration(CustomerModel obj)
        {
            if(ModelState.IsValid)
            { 
            RegisterDal rdal = new RegisterDal();
            rdal.customers.Add(obj);
            rdal.SaveChanges();
            return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //ActionResult for Login Form
        public ActionResult Login()
        {
            return View();
        }

        //ActionResult to Validate Login Form
        [HttpPost]
        public ActionResult Login(LoginModel model,string returnUrl)
        { 
           /* using(RegisterDal dal=new RegisterDal())
            {
                var v = dal.customers.Where(m => m.username.Equals(obj.username) && m.password.Equals(obj.password)).FirstOrDefault();
                if (v != null)
                {
                    Session["LoggedCustomer"] = v.Customername.ToString();
                    return RedirectToAction("Welcome", "Customer");
                }
            }*/

            if (ModelState.IsValid)
            {
                if (MyAuthService.SignIn(model.username, model.password, false))
                {

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult Welcome()
        {
            return View();
        }


        
    }
}