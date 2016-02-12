using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CRM.Model.Security;
using CRM.Core.Constants;
using CRM.DataAccess.SecurityDAL;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class CompanyRoleNavigationBox : System.Web.UI.UserControl
    {
        AuthorizationBDto objAuthorizationBDto;
        AuthorizationDal objAuthorizationDal;

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            }

            if (!IsPostBack)
            {
                BindCompanyRoleGrid();
                //Function to select one Radio Button at a time 
                SetGrdCompanyRoleRadioButtonOnClick();
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadUserSelectedPreferences();
        }

        protected void btnLoadAndSetAsDefault_Click(object sender, EventArgs e)
        {
            SaveAndLoadUserSelectedPreferences();
        }

        #endregion

        #region Methods

        private void LoadUserSelectedPreferences()
        {
            int RoleId = 0;
            int CompanyId = 0;
            int DeptId = 0;
            string RoleName = string.Empty;
            string CompanyName = string.Empty;
            string DeptName = string.Empty;

            for (int i = 0; i < grdCompanyRole.Items.Count; i++)
            {
                if (grdCompanyRole.Items[i].FindControl("rbCompanyRole") != null)
                {
                    if (((RadioButton)grdCompanyRole.Items[i].FindControl("rbCompanyRole")).Checked)
                    {
                        RoleId = Convert.ToInt32(grdCompanyRole.Items[i]["ROLE_ID"].Text.Trim());
                        RoleName = grdCompanyRole.Items[i]["ROLE_NAME"].Text.Trim();
                        CompanyId = Convert.ToInt32(grdCompanyRole.Items[i]["COMPANY_ID"].Text.Trim());
                        CompanyName = grdCompanyRole.Items[i]["COMPANY_NAME"].Text.Trim();
                        DeptId = Convert.ToInt32(grdCompanyRole.Items[i]["DEPARTMENT_ID"].Text.Trim());
                        DeptName = grdCompanyRole.Items[i]["DEPARTMENT_NAME"].Text.Trim();

                        if (objAuthorizationBDto != null)
                            objAuthorizationBDto.SetUserSelectedPreferences(CompanyId, CompanyName, RoleId, RoleName,DeptId,DeptName);
                    }
                }
            }
            ContentPlaceHolder ph = (ContentPlaceHolder)Page.Master.FindControl("");

        }

        private void SaveAndLoadUserSelectedPreferences()
        {
            int RoleId = 0;
            int CompanyId = 0;
            int DeptId = 0;
            string RoleName = string.Empty;
            string CompanyName = string.Empty;
            string DeptName = string.Empty;

            for (int i = 0; i < grdCompanyRole.Items.Count; i++)
            {
                if (grdCompanyRole.Items[i].FindControl("rbCompanyRole") != null)
                {
                    if (((RadioButton)grdCompanyRole.Items[i].FindControl("rbCompanyRole")).Checked)
                    {
                        RoleId = Convert.ToInt32(grdCompanyRole.Items[i]["ROLE_ID"].Text.Trim());
                        RoleName = grdCompanyRole.Items[i]["ROLE_NAME"].Text.Trim();
                        CompanyId = Convert.ToInt32(grdCompanyRole.Items[i]["COMPANY_ID"].Text.Trim());
                        CompanyName = grdCompanyRole.Items[i]["COMPANY_NAME"].Text.Trim();
                        DeptId = Convert.ToInt32(grdCompanyRole.Items[i]["DEPARTMENT_ID"].Text.Trim());
                        DeptName = grdCompanyRole.Items[i]["DEPARTMENT_NAME"].Text.Trim();

                        //objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                        if (objAuthorizationBDto != null)
                            objAuthorizationBDto.SetUserSelectedPreferences(CompanyId, CompanyName, RoleId, RoleName, DeptId, DeptName);

                        objAuthorizationDal = new AuthorizationDal();
                        int Result = objAuthorizationDal.UpdateUserDefaultPreference(objAuthorizationBDto.UserProfile.UserId, CompanyId, RoleId,DeptId);
                    }
                }
            }
        }

        private void BindCompanyRoleGrid()
        {
            if (objAuthorizationBDto != null && objAuthorizationBDto.UserPermission != null)
            {
                grdCompanyRole.DataSource = objAuthorizationBDto.UserPermission;
                grdCompanyRole.DataBind();
            }
        }

        private void SetGrdCompanyRoleRadioButtonOnClick()
        {
            RadioButton radioButton;
            for (int i = 0; i < grdCompanyRole.Items.Count; i++)
            {
                radioButton = (RadioButton)grdCompanyRole.Items[i].FindControl("rbCompanyRole");
                radioButton.Attributes.Add("OnClick", "SelectMeOnly(" + radioButton.ClientID + ", " + "'grdCompanyRole'" + ")");
            }
        }

        #endregion
    }
}