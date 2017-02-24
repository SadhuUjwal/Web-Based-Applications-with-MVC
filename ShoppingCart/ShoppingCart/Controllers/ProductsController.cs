using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{

    public class ProductsController : Controller
    {
        public List<Products> prodlist=new List<Products>();
        // GET: Products
        public ActionResult ViewProducts(Products prodobj)
        {
            string connstr = ConfigurationManager.ConnectionStrings["SHOPCART"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            string sqlcmd = "select * from products";
            SqlCommand cmd = new SqlCommand(sqlcmd, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Products()
                {
                    ProductId=(dr[0].ToString()),
                    CatID=(dr[1].ToString()),
                    ProductSDesc = (dr[2].ToString()),
                    ProductLDesc = (dr[3].ToString()),
                    ProductImage = (dr[4].ToString()),
                    price = (dr[5].ToString()),
                    Instock=(dr[6].ToString()),
                    Inventory=(dr[7].ToString())
                }
                );
            }
            return View(prodlist);
        }




        public ActionResult ViewProductsElectronics(Products prodobj)
        {
            string connstr = ConfigurationManager.ConnectionStrings["SHOPCART"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand("GetCategoryProducts", conn);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.Add("@CatDesc", "Electronic & Gadgets");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Products()
                {
                    ProductId = (dr[0].ToString()),
                    CatID = (dr[1].ToString()),
                    ProductSDesc = (dr[2].ToString()),
                    ProductLDesc = (dr[3].ToString()),
                    ProductImage = (dr[4].ToString()),
                    price = (dr[5].ToString()),
                    Instock = (dr[6].ToString()),
                    Inventory = (dr[7].ToString())
                }
                );
            }
            return View(prodlist);
        }



        public ActionResult ViewProductsKitchen(Products prodobj)
        {
            string connstr = ConfigurationManager.ConnectionStrings["SHOPCART"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand("GetCategoryProducts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CatDesc", "Kitchen Electrics");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Products()
                {
                    ProductId = (dr[0].ToString()),
                    CatID = (dr[1].ToString()),
                    ProductSDesc = (dr[2].ToString()),
                    ProductLDesc = (dr[3].ToString()),
                    ProductImage = (dr[4].ToString()),
                    price = (dr[5].ToString()),
                    Instock = (dr[6].ToString()),
                    Inventory = (dr[7].ToString())
                }
                );
            }
            return View(prodlist);
        }



        public ActionResult ViewProductsLuggage(Products prodobj)
        {
            string connstr = ConfigurationManager.ConnectionStrings["SHOPCART"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand("GetCategoryProducts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CatDesc", "Luggage");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Products()
                {
                    ProductId = (dr[0].ToString()),
                    CatID = (dr[1].ToString()),
                    ProductSDesc = (dr[2].ToString()),
                    ProductLDesc = (dr[3].ToString()),
                    ProductImage = (dr[4].ToString()),
                    price = (dr[5].ToString()),
                    Instock = (dr[6].ToString()),
                    Inventory = (dr[7].ToString())
                }
                );
            }
            return View(prodlist);
        }

       [Authorize(Roles = "Admin")]
         public ActionResult AddProducts()
        { return View(); }
        [HttpPost]
        public ActionResult AddProducts(Products prod)
        {

            string connstr = ConfigurationManager.ConnectionStrings["SHOPCART"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("AddProducts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductId",int.Parse( prod.ProductId)));
            cmd.Parameters.Add(new SqlParameter("@CatID",int.Parse( prod.CatID)));
            cmd.Parameters.Add(new SqlParameter("@ProductSDesc", prod.ProductSDesc));
            cmd.Parameters.Add(new SqlParameter("@ProductLDesc", prod.ProductLDesc));
            cmd.Parameters.Add(new SqlParameter("@price", prod.price));
            cmd.Parameters.Add(new SqlParameter("@Instock",decimal.Parse( prod.Instock)));
            cmd.Parameters.Add(new SqlParameter("@Inventory",int.Parse( prod.Inventory)));

            int i=cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Welcome", "Customer");
        }



        public ActionResult Addcart()
        {
            return View("AddCart");
        }
    }
}