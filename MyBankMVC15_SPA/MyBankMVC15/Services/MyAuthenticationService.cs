using MyBankMVC15.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyBankMVC15.Services
{
    public class MyAuthenticationService : IMyAuthenticationService
    {
        public object SessionKeys { get; private set; }

        public string GetRolesForUser(string uname)
        {
            string roles = "";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetUserRoles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Username", uname));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    roles += reader["RoleName"].ToString() + "|";
                if (roles != "")  // remove last "|"
                    roles = roles.Substring(0, roles.Length - 1);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;

        }

        public string GetAccNo(string uname)
        {
            string accno = "";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetUserAccno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Username", uname));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    accno += reader["CheckingAccountNum"].ToString() + "|";
                if (accno != "")  // remove last "|"
                    accno = accno.Substring(0, accno.Length - 1);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            
            return accno;

        }


        public string GetChkBal(string accnum)
        {
            string bal = "";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetChkAccBal", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CheckingAccountNumber", accnum));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    bal += reader["Balance"].ToString() + "|";
                if (bal != "")  // remove last "|"
                    bal = bal.Substring(0, bal.Length - 1);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bal;

        }


        public string GetSavBal(string accnum)
        {
            string bal = "";
            accnum = accnum + "1";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetSavAccBal", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SavingAccountNumber", accnum));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    bal += reader["Balance"].ToString() + "|";
                if (bal != "")  // remove last "|"
                    bal = bal.Substring(0, bal.Length - 1);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bal;

        }


        public string TrnsChkToSav(string accnum,string Amt)
        {
            string bal = "False";
            string saccnum = accnum + "1";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SPXferChkToSav", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChkAcctNum", accnum));
                cmd.Parameters.Add(new SqlParameter("@SavAcctNum", saccnum));
                
                cmd.Parameters.Add(new SqlParameter("@Amt", Amt));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    bal += reader["Balance"].ToString() + "|";
                if (bal != "")  // remove last "|"
                    bal = bal.Substring(0, bal.Length - 1);
                conn.Close();
                GetChkBal(accnum);
                GetSavBal(saccnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bal;

        }

        public bool SignIn(string userName, string password, bool createPersistentCookie)
        {
            bool bret = false;
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            try
            {
                //-----------Create authentication cookie----
                if (ValidateUser(userName, password) == true)
                {
                    string roles = GetRolesForUser(userName);//pipe or comma delimited role list - add later
                   // string accnum = GetAccNo(userName);
                    
                    
                    FormsAuthenticationTicket authTicket     // cookie timeout is also set
                            = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(5), false, roles);
                    //  encrypt the ticket
                    string encryptedTicket =
                        FormsAuthentication.Encrypt(authTicket);

                    // add the encrypted ticket to the cookie as data
                    HttpCookie authCookie = new HttpCookie
                        (FormsAuthentication.FormsCookieName, encryptedTicket);
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                    bret = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bret;

        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            string sql = "select Username from Users where Username='" +
                                 userName + "' and Password='" + password + "'";
            string connStr = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            object obj = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (obj != null)
            {
                //HttpContext.Current.Session[SessionKeys.USERID] = obj;
                return true;
            }
            else
                return false;
        }
    }
}