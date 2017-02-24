using MyBankMVC15.Models;
using MyBankMVC15.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBankMVC15.Controllers
{
    public class TransferController : Controller
    {
       public IMyAuthenticationService MyAuthService { get; set; }

       protected override void Initialize(RequestContext requestContext)
       {
           if (MyAuthService == null) { MyAuthService = new MyAuthenticationService(); }

           base.Initialize(requestContext);
       }

        
        
        // GET: Transfer
        public ActionResult Index()
        {
           
            return View();
        }

        [Authorize(Roles = "Customer")]

        public ActionResult XferChkToSav()
        {
            AccountModel a = new AccountModel();
            a.Username = HttpContext.User.Identity.Name;
            String uname = HttpContext.User.Identity.Name;
            a.Acno = MyAuthService.GetAccNo(uname);
            a.ChkBal = MyAuthService.GetChkBal(a.Acno);
            a.SavBal = MyAuthService.GetSavBal(a.Acno);
            ViewBag.Username = HttpContext.User.Identity.Name;
            ViewBag.Accn=a.Acno;
            ViewBag.ChkBall = a.ChkBal;
            ViewBag.SavBall = a.SavBal;
            return View(a);
        }

        [HttpPost]
        public ActionResult XferChkToSav(AccountModel a)
        {
            a.Username = HttpContext.User.Identity.Name;
            String uname = HttpContext.User.Identity.Name;
            a.Acno = MyAuthService.GetAccNo(uname);
            a.ChkBal = MyAuthService.GetChkBal(a.Acno);
            a.SavBal = MyAuthService.GetSavBal(a.Acno);
            MyAuthService.TrnsChkToSav(a.Acno, a.TransAmt);
            a.ChkBal = MyAuthService.GetChkBal(a.Acno);
            a.SavBal = MyAuthService.GetSavBal(a.Acno);
            ViewBag.Username = HttpContext.User.Identity.Name;
            ViewBag.Accn = a.Acno;
            ViewBag.ChkBall = a.ChkBal;
            ViewBag.SavBall = a.SavBal;
            return View(a);
        }



    }
}