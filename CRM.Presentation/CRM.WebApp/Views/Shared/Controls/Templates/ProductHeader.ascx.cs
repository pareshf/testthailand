using System;
using System.Web;
using System.Web.Security;

using CRM.Model.Security;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Shared.Controls.Templates
{
    public partial class ProductHeader : System.Web.UI.UserControl
    {
        #region Member variables

        AuthorizationBDto objAuthorizationBDto;
        public DateTime dtNow;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                lblWelcome.Text = "Logged in as: " + objAuthorizationBDto.UserProfile.UserName + "(" + objAuthorizationBDto.UserSelectedRoleName + ")";
                lblPreferedCompanyName.Text = objAuthorizationBDto.UserSelectedCompanyName;
            }
        }

        protected void RedirectToDefaultPage()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void lnkBtnSignOut_Click(object sender, EventArgs e)
        {
            RedirectToDefaultPage();
        }
    }
}