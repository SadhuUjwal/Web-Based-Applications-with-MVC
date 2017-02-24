using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public interface IMyAuthenticationService
    {
        bool ValidateUser(string userName, string password);
        string GetRolesForUser(string uname);
        
        bool SignIn(string userName, string password, bool createPersistentCookie);
        void SignOut();
    }
}
