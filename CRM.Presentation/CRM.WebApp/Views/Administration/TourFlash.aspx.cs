using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.DataAccess.FaresDal.TourDal;

namespace CRM.WebApp.Views.Administration
{
    public partial class TourFlash : System.Web.UI.Page
    {
        AuthorizationBDto objAuthorizationBDto = null;

        #region Properties
        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session[PageConstants.ThemeName] == null)
                {
                    return "Default";

                }
                else
                {
                    return HttpContext.Current.Session[PageConstants.ThemeName].ToString();
                }
            }
        }
        #endregion

        #region Page Events
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                WebHelper.WebManager.CheckSessionIsActive();
                WebHelper.WebManager.CheckUserAuthorizationForProgram("Tour Flash");
            }
            catch (Exception) { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Tour Flash";

            }
            else
            {
                objAuthorizationBDto = new AuthorizationBDto();
            }

            if (!IsPostBack)
            {
                ControlBox1_ResetClick(ControlBox1, new EventArgs());
            }

            ControlBox1.CancelButton.Text = "Reset";
            //ControlBox1.AddAttributeToClearButton("onClick", "javascript:return ControlBox1_ClearClientClick()");
        }
        #endregion

        #region Control Bos Events

        protected void ControlBox1_ResetClick(object sender, EventArgs e)
        {
            TourMasterDal tour = new TourMasterDal();
            DataTable dt = tour.GetTourFlash();
            if (dt != null && dt.Rows.Count > 0)
            {
                txtTopLeft.Text = dt.Rows[0]["TOP_LEFT"].ToString();
                txtTopRight.Text = dt.Rows[0]["TOP_RIGHT"].ToString();
                txtBottomLeft.Text = dt.Rows[0]["BOTTOM_LEFT"].ToString();
                txtBottomRight.Text = dt.Rows[0]["BOTTOM_RIGHT"].ToString();
            }
        }

        protected void ControlBox1_SaveClick(object sender, EventArgs e)
        {
            TourMasterDal tour = new TourMasterDal();
            int i = tour.SetTourFlash(txtTopLeft.Text, txtTopRight.Text, txtBottomLeft.Text, txtBottomRight.Text);

            if (i > 0)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString(), "successMessage");
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString(), "errorMessage");
            }
        }

        protected void ControlBox1_ClearClick(object sender, EventArgs e)
        {
            txtBottomLeft.Text = string.Empty;
            txtBottomRight.Text = string.Empty;
            txtTopLeft.Text = string.Empty;
            txtTopRight.Text = string.Empty;
        }

        #endregion
    }
}
