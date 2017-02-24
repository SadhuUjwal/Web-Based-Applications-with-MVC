using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Products
    {
        public string ProductId { get; set; }
        public string CatID { get; set; } 
        public string ProductSDesc { get; set; }
        public string ProductLDesc { get; set; }
        public string ProductImage { get; set; }
        public string price { get; set; }
        public string Instock { get; set; }
        public string Inventory { get; set; }
     
    }
}