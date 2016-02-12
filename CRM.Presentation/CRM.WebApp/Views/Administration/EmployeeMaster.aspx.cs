using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using CRM.Core.Constants;
using CRM.DataAccess.AdministrationDAL;
using CRM.DataAccess.HRDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.HRModel;
using CRM.Model.Security;
using Telerik.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using CRM.Core.Utility.DateTimeUtility;
using CRM.WebApp.WebHelper;
using System.Web.UI;
using System.Web;
using System.IO;

namespace CRM.WebApp.Views.Administration
{
    public partial class EmployeeMaster : System.Web.UI.Page
    {
        #region Member variables
        EmployeeMasterDal objEmployeeMasterDal = null;
        AuthorizationBDto objAuthorizationBDto;
        #endregion
        #region Page Events
        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Employee");
        }
        #endregion

        #region Override Style Sheet Theme

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            }

            if (!IsPostBack)
            {

            }
        }
             #endregion
    }
}
   
