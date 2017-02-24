using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankMVC15.Models
{
    public class HistoryModel
    {
        

        public string FromAccountNum { get; set; }

        public string ToAccountNum{ get; set;}

        public string Date { get; set;}
        public string Ammount{ get; set;}
        public string CheckingAccountNumber { get; set; }


    }
}