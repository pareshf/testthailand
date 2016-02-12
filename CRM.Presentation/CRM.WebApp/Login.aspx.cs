using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CRM.Model.Security;

using CRM.DataAccess.SecurityDAL;
using CRM.Core.Constants;

namespace CRM.WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        UserProfileBDto objUserProfile = new UserProfileBDto();
        AuthorizationBDto objAuthorizationBDto = new AuthorizationBDto();


        #region Page Event
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();

        } 
        #endregion

        #region Button Events

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            AuthorizationDal objAuthorizationDal = new AuthorizationDal();

            DataSet ds = objAuthorizationDal.ValidateSystemLogin(username, password);



            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "-2") //Restricted User
                    {
                        if (ds.Tables[0].Rows[0]["USER_ID"].ToString() != null)
                        {
                            int UserId = int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString());
                            string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (ipaddress == "" || ipaddress == null)
                                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                            int Result = objAuthorizationDal.InsertLoginLog(UserId, ipaddress, false);
                        }
                        lblError.Text = "Invalid Username or Password.<br />Login failed!";
                    }
                    else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "-1") //Login Failed
                    {
                        if (ds.Tables[0].Rows[0]["USER_ID"].ToString() != null)
                        {
                            int UserId = int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString());
                            string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (ipaddress == "" || ipaddress == null)
                                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                            int Result = objAuthorizationDal.InsertLoginLog(UserId, ipaddress, false);
                        }
                        lblError.Text = "Invalid Username or Password.<br />Login failed!";
                    }
                    else if (ds.Tables[0].Rows[0]["USER_STATUS_ID"].ToString() != "1")
                    {
                        lblError.Text = "User is inactive.<br />Login failed!";
                    }
                    else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1") //Login SucessFully
                    {
                        bool isPersistent = chkRememberMe.Checked;

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          username,
                          DateTime.Now,
                          DateTime.Now.AddMinutes(120),
                          isPersistent,
                          username,
                          FormsAuthentication.FormsCookiePath);

                        // Encrypt the ticket.
                        string encTicket = FormsAuthentication.Encrypt(ticket);

                        // Create the cookie.
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                        // Add Userdata to Session
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                objUserProfile.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["USER_ID"]);
                                objUserProfile.UserName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                                objUserProfile.EmployeeId = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_ID"]);
                                objUserProfile.EmployeeName = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();
                                objUserProfile.EmployeeEmail = ds.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                                objUserProfile.EmployeeMobile = ds.Tables[0].Rows[0]["EMP_MOBILE"].ToString();
                                objUserProfile.DefaultTheme = ds.Tables[0].Rows[0]["DEFAULT_THEME"].ToString();
                                objUserProfile.Signature = ds.Tables[0].Rows[0]["SIGNATURE_PASSWORD"].ToString();
                                objUserProfile.Password = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                                objUserProfile.Cust_id = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
                                objUserProfile.Supplier_id = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                                objUserProfile.EmployeeFlag = ds.Tables[0].Rows[0]["FLAG"].ToString();
                                objUserProfile.CompanyName = ds.Tables[1].Rows[0]["COMPANY_NAME"].ToString();

                                Session[PageConstants.ThemeName] = objUserProfile.DefaultTheme.Trim();
                            }

                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                objAuthorizationBDto.UserPermission = ds.Tables[1];
                            }

                            Session["usersname"] = objUserProfile.UserName;
                            Session["usersid"] = objUserProfile.UserId;
                            Session["DateTime.Now"] = DateTime.Now.ToString("d");
                            Session["users"] = ds.Tables[0];
                            Session["empid"] = objUserProfile.EmployeeId;
                            Session["signature"] = objUserProfile.Signature;
                            Session["password"] = objUserProfile.Password;
                            Session["cust_id"] = objUserProfile.Cust_id;
                            Session["supplier_id"] = objUserProfile.Supplier_id;
                            Session["FLAG"] = objUserProfile.EmployeeFlag;
                            Session["COMPANY_NAME"] = objUserProfile.CompanyName;
                            
                            #region for online users


                            int USERID = objUserProfile.UserId;
                            string LOGINDATE = DateTime.Now.ToString("d");

                            DataSet dstype = objAuthorizationDal.GetUsertype(Convert.ToInt32(Session["cust_id"]));
                            string CustTypeID = Convert.ToString(dstype.Tables[0].Rows[0]["CUST_TYPE_ID"]);
                            Session["CustTypeID"] = CustTypeID;

                            DataSet ds1 = objAuthorizationDal.GetOnlineUsers(USERID, LOGINDATE);
                            Session["users1"] = ds1.Tables[0];

                            #endregion

                        }

                        //Add User Login Log
                        if (ds.Tables[0].Rows[0]["USER_ID"].ToString() != null)
                        {
                            int UserId = int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString());


                            string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (ipaddress == "" || ipaddress == null)
                                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                            int Result = objAuthorizationDal.InsertLoginLog(UserId, ipaddress, true);
                        }

                        Session["AUTH_USER_NAME"] = username;
                        objAuthorizationBDto.UserProfile = objUserProfile;
                        Session[PageConstants.ssnUserAuthorization] = objAuthorizationBDto;

                        // Redirect to requested url
                        //Response.Redirect(FormsAuthentication.GetRedirectUrl(username, isPersistent));
                        Response.Redirect("~/Views/Workplace/Dashboard.aspx");
                    }
                    else
                    {
                        lblError.Text = "Invalid Username or Password.<br />Login failed!";
                    }
                }
                else
                {
                    lblError.Text = "Invalid Username or Password.<br />Login failed!";
                }
            }
           

            #region Comment


            //if (ds != null && ds.Tables.Count > 0)
            //{

            //    bool isPersistent = chkRememberMe.Checked;

            //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
            //      username,
            //      DateTime.Now,
            //      DateTime.Now.AddMinutes(60),
            //      isPersistent,
            //      username,
            //      FormsAuthentication.FormsCookiePath);

            //    // Encrypt the ticket.
            //    string encTicket = FormsAuthentication.Encrypt(ticket);

            //    // Create the cookie.
            //    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));



            //    // Add Userdata to Session
            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //        {
            //            objUserProfile.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["USER_ID"]);
            //            objUserProfile.UserName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
            //            objUserProfile.DefaultTheme = ds.Tables[0].Rows[0]["DEFAULT_THEME"].ToString();

            //            Session[PageConstants.ThemeName] = objUserProfile.DefaultTheme.Trim();

            //        }

            //        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            //        {
            //            objAuthorizationBDto.UserPermission = ds.Tables[1];
            //        }
            //    }

            //    //Session["AUTH_USER_NAME"] = username;
            //    objAuthorizationBDto.UserProfile = objUserProfile;
            //    Session[PageConstants.ssnUserAuthorization] = objAuthorizationBDto;

            //    // Redirect to requested url
            //    //Response.Redirect(FormsAuthentication.GetRedirectUrl(username, isPersistent));
            //    Response.Redirect("~/Default.aspx");
            //}
            //else
            //{
            //    lblError.Text = "Invalid Username or Password.<br />Login failed!";
            //}
            #endregion
        } 
        #endregion

        #region Method
        public void showMessage(String Message)
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('" + Message + "', 330, 110, 'Warning Message'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", radalertscript, false);
        }
        #endregion

       

        
    }
}