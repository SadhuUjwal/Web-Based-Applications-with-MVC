using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;


namespace ShoppingCart.Models
{
    public class LoginModel
    {
         [Required(ErrorMessage = "Username is required..")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string password { get; set; }
    }
}