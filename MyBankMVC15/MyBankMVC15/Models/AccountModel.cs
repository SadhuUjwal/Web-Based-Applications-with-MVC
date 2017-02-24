using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankMVC15.Models
{
    public class AccountModel
    {
        
        public string Username { get; set; }
  
        public string Acno { get; set; }

        public string ChkBal { get; set; }

        public string SavBal { get; set; }

        public string TransAmt { get; set; }
    }
}
