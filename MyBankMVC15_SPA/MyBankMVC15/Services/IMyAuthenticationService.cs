using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankMVC15.Models
{
    public interface IMyAuthenticationService
    {
        bool ValidateUser(string userName, string password);
        string GetRolesForUser(string uname);
        string GetAccNo(string uname);
        string GetChkBal(string accNum);
        string GetSavBal(string accNum);
        string TrnsChkToSav(string accnum, string Amt);
        bool SignIn(string userName, string password, bool createPersistentCookie);
        void SignOut();
    }
}
