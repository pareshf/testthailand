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
using CRM.DataAccess;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Shared.Gadgets
{
    public partial class MyPortal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FetchValue();
        }

        #region Method
        public void FetchValue()
        {
            try
            {
                AuthorizationBDto objAuthorizationBDto;
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                DefaultGadgetsDAL objDefaultGadgetsDAL = new DefaultGadgetsDAL();
                if (objAuthorizationBDto.UserProfile.UserId != 0)
                {
                    DataSet ds = new DataSet();
                    ds = objDefaultGadgetsDAL.FetchMyMortal(objAuthorizationBDto.UserProfile.UserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string Prefix, UserName, LastLoginDate, LastLoginTime, IP;

                        if (ds.Tables[0].Rows[0]["PREFIX"].ToString() != null)
                            Prefix = ds.Tables[0].Rows[0]["PREFIX"].ToString();
                        else
                            Prefix = "";
                        if (ds.Tables[0].Rows[0]["USER_NAME"].ToString() != null)
                            UserName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                        else
                            UserName = "";
                        if (ds.Tables[0].Rows[0]["LOGIN_DATE"].ToString() != null)
                            LastLoginDate = ds.Tables[0].Rows[0]["LOGIN_DATE"].ToString();
                        else
                            LastLoginDate = "";
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != null)
                            LastLoginTime = ds.Tables[0].Rows[0]["TIME"].ToString();
                        else
                            LastLoginTime = "";
                        if (ds.Tables[0].Rows[0]["IP_ADDRESS"].ToString() != null)
                            IP = ds.Tables[0].Rows[0]["IP_ADDRESS"].ToString();
                        else
                            IP = "";

                        lblLoginName.Text = "<b>" + Prefix + ", " + UserName + "</b>";
                        lbllastLoginDate.Text = "Last Login Date : " + LastLoginDate;
                        lblLastLoginTime.Text = "Last Login Time : " + LastLoginTime;
                        lblLastLoginOn.Text = "Last Login On : " + IP;
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion
    }
}