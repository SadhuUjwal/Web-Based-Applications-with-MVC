using MyBankMVC15.Models;
using MyBankMVC15.Services;

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web.Routing;


namespace MyBankMVC15.Controllers
{
    public class HistoryController : Controller
    {

        public IMyAuthenticationService MyAuthService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (MyAuthService == null) { MyAuthService = new MyAuthenticationService(); }

            base.Initialize(requestContext);
        }



        // GET: History
        public ActionResult Index()
        {
            return View();
        }
        private List<HistoryModel> records = new List<HistoryModel>();
         [Authorize(Roles = "Customer")]
        public ActionResult History(AccountModel a)
        {
           
       
             a.Username = HttpContext.User.Identity.Name;
            String uname = HttpContext.User.Identity.Name;
            a.Acno = MyAuthService.GetAccNo(uname);
            string sql = "select * from TransferHistory where CheckingAccountNumber='" +a.Acno+"'";
                          
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da;
            
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                records.Add(new HistoryModel() { FromAccountNum = (dr[0].ToString()), ToAccountNum = dr[1].ToString(), Date = dr[2].ToString(), Ammount = (dr[3].ToString()), CheckingAccountNumber=dr[4].ToString() });
            }
            return View(records);
        }
    }
}