using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage="Customer Name Required",AllowEmptyStrings=false)]
        public string Customername { get; set; }

        [Key]
        [Required(ErrorMessage = "User Name Required",AllowEmptyStrings=false)]
        public string username { get; set; }
        [Required(ErrorMessage = "Password Required", AllowEmptyStrings = false)]
        public string password { get; set; }
        [Required(ErrorMessage = "Email Required", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact Required", AllowEmptyStrings = false)]
        public string contact { get; set; }
        [Required(ErrorMessage = "CardNumber Required", AllowEmptyStrings = false)]
        public string Cardno { get; set; }
    }
}