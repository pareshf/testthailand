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
using CRM.Core.Constants;
using Telerik.Web.UI;
using CRM.DataAccess.SecurityDAL;

namespace CRM.WebApp.Views.Shared
{
    public partial class CRMMaster : System.Web.UI.MasterPage
    {

        #region Member varibles

        AuthorizationBDto objAuthorizationBDto;
        AuthorizationDal objAuthorizationDal;
        #endregion

        #region Flash Message Properties and Methods

        #region Get/Set Message Text
        /// <summary>
        /// Gets or sets a value indicating message text of flash message control.
        /// </summary>
        public String MessageText
        {
            get
            { return flashControl.MessageText; }
            set
            { flashControl.MessageText = value; }
        }
        #endregion

        #region DisplayMessage
        /// <summary>
        /// Display flash message.
        /// </summary>
        /// <param name="message">String values that displayed in flash message</param>
        public void DisplayMessage(string message)
        {
            flashControl.MessageText = message;
            flashControl.Display();
        }

        /// <summary>
        /// Display flash message.
        /// </summary>
        /// <param name="message">String values that displayed in flash message</param>
        /// <param name="cssClass">Css class name</param>
        public void DisplayMessage(string message, string cssClass)
        {
            flashControl.MessageText = message;
            flashControl.CssClass = cssClass;
            flashControl.Display();
        }
        /// <summary>
        /// Display flash message.
        /// </summary>
        /// <param name="message">String values that displayed in flash message</param>
        /// <param name="cssClass">Css class name</param>
        /// <param name="diaplayTime">How much second message display on screen(1000 = 1 second)</param>
        public void DisplayMessage(string message, string cssClass, int diaplayTime)
        {
            flashControl.Interval = diaplayTime;
            flashControl.MessageText = message;
            flashControl.CssClass = cssClass;
            flashControl.Display();
        }

        /// <summary>
        /// Display flash message.
        /// </summary>
        /// <param name="message">String values that displayed in flash message</param>
        /// <param name="diaplayTime">How much second message display on screen(1000 = 1 second)</param>
        public void DisplayMessage(string message, int diaplayTime)
        {
            flashControl.Interval = diaplayTime;
            flashControl.MessageText = message;
            flashControl.Display();
        }

        #endregion

        #region CSS Class of Message Control
        /// <summary>
        /// Gets or sets a value that indicating Css Class of flash message control.
        /// </summary>
        public String MessageCssClass
        {
            get
            { return flashControl.CssClass; }
            set
            { flashControl.CssClass = value; }
        }
        #endregion

        #endregion

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            //use to set style sheet
            this.Page.Theme = HttpContext.Current.Session[PageConstants.ThemeName].ToString();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManagerCRMMaster.RegisterAsyncPostBackControl(btnLoad);
            ScriptManagerCRMMaster.RegisterAsyncPostBackControl(btnLoadAndSetAsDefault);
            if (!IsPostBack)
            {
                if (Session[PageConstants.ssnUserAuthorization] != null)
                {
                    objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];

                    if (objAuthorizationBDto != null && objAuthorizationBDto.UserPermission != null && objAuthorizationBDto.UserPermission.Rows.Count > 0)
                    {
                        DataTable dtUserPermission = objAuthorizationBDto.UserPermission;
                        // Find a row with default preference
                        DataRow[] drUserDefaultPermission = dtUserPermission.Select("IS_DEFAULT = 1");

                        if (drUserDefaultPermission.Length == 0 && objAuthorizationBDto.UserSelectedRoleId == 0)  // perference not found
                        {
                            FillUserLable();
                            ucPanelBarModule.Visible = false;
                            tdPanelBarNavigation.Visible = false;
                            BindCompanyRoleGrid();
                            //Function to select one Radio Button at a time 
                            SetgrdCompanyRoleaspRadioButtonOnClick();
                            PopEx_lnkBtnChangePreference.Show();
                        }
                        else  // perference found
                        {
                            if (objAuthorizationBDto.UserSelectedCompanyId == 0 && objAuthorizationBDto.UserSelectedRoleId == 0)
                            {
                                foreach (DataRow dr in drUserDefaultPermission)
                                {
                                    objAuthorizationBDto.SetUserSelectedPreferences(Convert.ToInt32(dr["COMPANY_ID"]), dr["COMPANY_NAME"].ToString(),
                                        Convert.ToInt32(dr["ROLE_ID"]), dr["ROLE_NAME"].ToString(), Convert.ToInt32(dr["DEPARTMENT_ID"]), dr["DEPARTMENT_NAME"].ToString());
                                }
                            }
                            ucPanelBarModule.BindModuleList(objAuthorizationBDto.UserSelectedRoleId,objAuthorizationBDto.UserSelectedDepartmentId,objAuthorizationBDto.UserSelectedCompanyId);
                            ucPanelBarModule.Visible = true;

                            if (objAuthorizationBDto.UserSelectedModuleId != 0)
                            {
                                pnlLeftMenu.BindLeftMenu(objAuthorizationBDto.UserSelectedModuleId, objAuthorizationBDto.UserSelectedRoleId,objAuthorizationBDto.UserSelectedCompanyId,objAuthorizationBDto.UserSelectedDepartmentId);
                                tdPanelBarNavigation.Visible = true;
                            }
                            else
                            {
                                tdPanelBarNavigation.Visible = false;
                            }
                            BindCompanyRoleGrid();
                            SetgrdCompanyRoleaspRadioButtonOnClick();
                            FillUserLable();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }


        #endregion

        #region Signout Link Event
        protected void lnkBtnSignOut_Click(object sender, EventArgs e)
        {
            RedirectToDefaultPage();
        }
        #endregion

        #region Preference Event

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadUserSelectedPreferences();
            //ucPanelBarModule.Visible = true;
            //objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            //ucPanelBarModule.BindModuleList(objAuthorizationBDto.UserSelectedRoleId);
            //upModule.Update();
            //FillUserLable();
            PopEx_lnkBtnChangePreference.Hide();
            tdPanelBarNavigation.Visible = false;
            Response.Redirect("~/Views/Workplace/Dashboard.aspx");
          //  updMenu.Update();

        }

        protected void btnLoadAndSetAsDefault_Click(object sender, EventArgs e)
        {
            SaveAndLoadUserSelectedPreferences();
            //objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            //ucPanelBarModule.BindModuleList(objAuthorizationBDto.UserSelectedRoleId);
            //upModule.Update();
            //FillUserLable();
            PopEx_lnkBtnChangePreference.Hide();
            tdPanelBarNavigation.Visible = false;
            Response.Redirect("~/Views/Workplace/Dashboard.aspx");

        }

        #endregion

        #region MOdule Panel Event

        protected void ucPanelBarModule_OnOnItemClick(object sender, MenuEventArgs e)
        {
            e.Item.Selected = true;

            int ModuleId = int.Parse(e.Item.Value);
            if (ModuleId != 0)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                if (objAuthorizationBDto != null)
                {
                    objAuthorizationBDto.SetUserSelectedPreferences(objAuthorizationBDto.UserSelectedCompanyId, objAuthorizationBDto.UserSelectedCompanyName, objAuthorizationBDto.UserSelectedRoleId, objAuthorizationBDto.UserSelectedRoleName, ModuleId, string.Empty,objAuthorizationBDto.UserSelectedDepartmentId,objAuthorizationBDto.UserSelectedDepartmentName);
                }
                else
                {
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }

                switch (ModuleId)
                {
                    case 1: // Administration
                        HttpContext.Current.Response.Redirect("~/Views/Administration/HotelMaster.aspx");
                        break;
                    case 2: // Customers
                        HttpContext.Current.Response.Redirect("~/Views/Marketing/Campaigns.aspx");
                        break;
                    case 3: // Inquiry
                        HttpContext.Current.Response.Redirect("~/Views/Sales/Customers.aspx");
                        break;
                    case 4: // Orders
                        HttpContext.Current.Response.Redirect("~/Views/Settings/GeneralSettings.aspx");
                        break;
                    case 5: // Fares
                        HttpContext.Current.Response.Redirect("~/Views/Workplace/Dashboard.aspx");
                        break;
                    case 10: // Fares
                        HttpContext.Current.Response.Redirect("~/Views/Reports/dummy.aspx");
                        break;
                    case 11: // Fares
                        HttpContext.Current.Response.Redirect("~/Views/WebPortal/dummy.aspx");
                        break;
                    case 12://acount
                        HttpContext.Current.Response.Redirect("~/Views/Workplace/AccountDashboard.aspx");
                        break;
                    case 13://fit
                        HttpContext.Current.Response.Redirect("~/Views/FIT/BookingFit.aspx");
                        break;
                    case 14://supplier
                        HttpContext.Current.Response.Redirect("~/Views/FIT/Default.aspx");
                        break;
                    case 15://agent
                        HttpContext.Current.Response.Redirect("~/Views/FIT/Default.aspx");
                        break;
                    case 16://git
                        HttpContext.Current.Response.Redirect("~/Views/BackOffice/Default.aspx");
                        break;
                    case 17://catalogue
                        HttpContext.Current.Response.Redirect("~/Views/BackOffice/Default.aspx");
                        break;
                    case 18://MIS
                        HttpContext.Current.Response.Redirect("~/Views/MIS/Default.aspx");
                        break;
                    case 19://EMAIL
                        HttpContext.Current.Response.Redirect("~/Views/EmailSettings/Default.aspx");
                        break;
                    
                }
            }
            if (ModuleId == 0)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                objAuthorizationBDto.SetUserSelectedPreferences(objAuthorizationBDto.UserSelectedCompanyId, objAuthorizationBDto.UserSelectedCompanyName, objAuthorizationBDto.UserSelectedRoleId, objAuthorizationBDto.UserSelectedRoleName, ModuleId, string.Empty,objAuthorizationBDto.UserSelectedDepartmentId,objAuthorizationBDto.UserSelectedDepartmentName);
                HttpContext.Current.Response.Redirect("~/Views/Workplace/Dashboard.aspx");
            }
            BindCompanyRoleGrid();

        }

        #endregion

        #region Method
		
        public void ResetProgramNavigationPanel(int ModuleId, int RoleId,int CompId,int DeptId)
        {
            pnlLeftMenu.BindLeftMenu(ModuleId, RoleId,CompId,DeptId);
            tdPanelBarNavigation.Visible = true;
            
        }

        public void ResetModuleBar(int ModuleId)
        {

            //			int ModuleId = int.Parse(e.Item.Value);
            if (ModuleId != 0)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                if (objAuthorizationBDto != null)
                {
                    objAuthorizationBDto.SetUserSelectedPreferences(objAuthorizationBDto.UserSelectedCompanyId, objAuthorizationBDto.UserSelectedCompanyName, objAuthorizationBDto.UserSelectedRoleId, objAuthorizationBDto.UserSelectedRoleName, ModuleId, string.Empty,objAuthorizationBDto.UserSelectedDepartmentId,objAuthorizationBDto.UserSelectedDepartmentName);
                }
                else
                {
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Response.Redirect("~/Views/Workplace/Dashboard.aspx");
                }

                switch (ModuleId)
                {
                    case 1: // Administration
                        HttpContext.Current.Response.Redirect("~/Views/Administration/Default.aspx");
                        break;
                    case 2: // Customers
                        HttpContext.Current.Response.Redirect("~/Views/Customers/Default.aspx");
                        break;
                    case 3: // Inquiry
                        HttpContext.Current.Response.Redirect("~/Views/Inquiry/Default.aspx");
                        break;
                    case 4: // Orders
                        HttpContext.Current.Response.Redirect("~/Views/Orders/Default.aspx");
                        break;
                    case 5: // Fares
                        HttpContext.Current.Response.Redirect("~/Views/Fares/Default.aspx");
                        break;
                    case 6: // HR
                        HttpContext.Current.Response.Redirect("~/Views/HR/Default.aspx");
                        break;
                    default:
                        Response.Redirect("~/Default.aspx");
                        break;
                }
            }
            if (ModuleId == 0)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                objAuthorizationBDto.SetUserSelectedPreferences(objAuthorizationBDto.UserSelectedCompanyId, objAuthorizationBDto.UserSelectedCompanyName, objAuthorizationBDto.UserSelectedRoleId, objAuthorizationBDto.UserSelectedRoleName, ModuleId, string.Empty,objAuthorizationBDto.UserSelectedDepartmentId,objAuthorizationBDto.UserSelectedDepartmentName);
                Response.Redirect("~/Default.aspx");
            }
            BindCompanyRoleGrid();

        }

        protected void RedirectToDefaultPage()
        {
			#region for online users

			AuthorizationDal objAuthorizationDal = new AuthorizationDal();
			UserProfileBDto objUserProfile = new UserProfileBDto();

			//int ISLOGIN=1;
			int USERID = Convert.ToInt32(Session["usersid"]);
			//int USERID = objUserProfile.UserId;
			string LOGINDATE = DateTime.Now.ToString();

			DataSet ds1 = objAuthorizationDal.GetSignOutUsers(USERID, LOGINDATE);
			Session.Remove("users1");
			Session["users1"] = ds1.Tables[0];


			#endregion
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void FillUserLable()
        {
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                if (objAuthorizationBDto.UserSelectedRoleName != null)
                    lblWelcome.Text = "Logged in as: " + objAuthorizationBDto.UserProfile.UserName + " ("+objAuthorizationBDto.UserSelectedDepartmentName+" - " + objAuthorizationBDto.UserSelectedRoleName + ") ";
                else
                    lblWelcome.Text = "Logged in as: " + objAuthorizationBDto.UserProfile.UserName;

                if (objAuthorizationBDto.UserSelectedCompanyName != null)
                    lnkBtnChangePreference.Text = objAuthorizationBDto.UserSelectedCompanyName.ToUpper();
            }
        }

        #region Methods(Preference)

        private void LoadUserSelectedPreferences()
        {
            int RoleId = 0;
            int CompanyId = 0;
            int DeptId = 0;
            string RoleName = string.Empty;
            string CompanyName = string.Empty;
            string DeptName = string.Empty;

            for (int i = 0; i < grdCompanyRoleasp.Rows.Count; i++)
            {
                if (grdCompanyRoleasp.Rows[i].FindControl("rbCompanyRole") != null)
                {
                    if (((RadioButton)grdCompanyRoleasp.Rows[i].Cells[0].FindControl("rbCompanyRole")).Checked)
                    {
                        RoleId = Convert.ToInt32(((Label)grdCompanyRoleasp.Rows[i].FindControl("lblroleid")).Text);
                        RoleName = ((Label)grdCompanyRoleasp.Rows[i].FindControl("lblrolename")).Text;
                        CompanyId = Convert.ToInt32(((Label)grdCompanyRoleasp.Rows[i].FindControl("lblcmpid")).Text);
                        CompanyName = ((Label)grdCompanyRoleasp.Rows[i].FindControl("lblcmpname")).Text;
                        DeptId = Convert.ToInt32(((Label)grdCompanyRoleasp.Rows[i].FindControl("lbldeptid")).Text);
                        DeptName = ((Label)grdCompanyRoleasp.Rows[i].FindControl("lbldeptname")).Text;

                        objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                        if (objAuthorizationBDto != null)
                            objAuthorizationBDto.SetUserSelectedPreferences(CompanyId, CompanyName, RoleId, RoleName,DeptId,DeptName);
                    }
                }
            }
        }

        private void SaveAndLoadUserSelectedPreferences()
        {
          
            DataTable dtPreference = new DataTable();
            Label RoleId=new Label();
            Label RoleName = new Label();
            Label CompanyId = new Label();
            Label CompanyName = new Label();
            Label DeptId = new Label();
            Label DeptName = new Label();
            for (int i = 0; i < grdCompanyRoleasp.Rows.Count; i++)
            {
                if (grdCompanyRoleasp.Rows[i].FindControl("rbCompanyRole") != null)
                {
                    if (((RadioButton)grdCompanyRoleasp.Rows[i].Cells[0].FindControl("rbCompanyRole")).Checked)
                    {
                        RoleId = (Label)grdCompanyRoleasp.Rows[i].Cells[6].FindControl("lblroleid");
                        RoleName = (Label)grdCompanyRoleasp.Rows[i].Cells[4].FindControl("lblrolename");
                        CompanyId = (Label)grdCompanyRoleasp.Rows[i].Cells[5].FindControl("lblcmpid");
                        CompanyName = (Label)grdCompanyRoleasp.Rows[i].Cells[1].FindControl("lblcmpname");
                        DeptId = (Label)grdCompanyRoleasp.Rows[i].Cells[7].FindControl("lbldeptid");
                        DeptName = (Label)grdCompanyRoleasp.Rows[i].Cells[3].FindControl("lbldeptname");

                        objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];

                        objAuthorizationDal = new AuthorizationDal();
                        int Result = objAuthorizationDal.UpdateUserDefaultPreference(objAuthorizationBDto.UserProfile.UserId, int.Parse(CompanyId.Text),int.Parse(RoleId.Text),int.Parse(DeptId.Text));
                    }
                }
            }
            dtPreference = objAuthorizationDal.PreferenceGridByUserId(objAuthorizationBDto.UserProfile.UserId).Tables[0];

            if (objAuthorizationBDto != null)
                objAuthorizationBDto.SetUserSelectedPreferences(dtPreference, int.Parse(CompanyId.Text), CompanyName.Text, int.Parse(RoleId.Text), RoleName.Text,int.Parse(DeptId.Text),DeptName.Text);

        }

        private void BindCompanyRoleGrid()
        {
            if (objAuthorizationBDto != null && objAuthorizationBDto.UserPermission != null)
            {
                grdCompanyRoleasp.DataSource = objAuthorizationBDto.UserPermission;
                grdCompanyRoleasp.DataBind();
            }
            Label RoleId = new Label();
            Label CompanyId = new Label();
            Label DeptId = new Label();
            if (objAuthorizationBDto.UserSelectedCompanyId != 0)
            {
                for (int i = 0; i <= grdCompanyRoleasp.Rows.Count; i++)
                {
                    try
                    {
                        RadioButton rbCompanyRole = (RadioButton)grdCompanyRoleasp.Rows[i].Cells[0].FindControl("rbCompanyRole");
                        RoleId = (Label)grdCompanyRoleasp.Rows[i].Cells[5].FindControl("lblroleid");
                        CompanyId = (Label)grdCompanyRoleasp.Rows[i].Cells[4].FindControl("lblcmpid");
                        DeptId = (Label)grdCompanyRoleasp.Rows[i].Cells[6].FindControl("lbldeptid");

                        if (rbCompanyRole != null && int.Parse(CompanyId.Text) != 0 && int.Parse(RoleId.Text) != 0 && int.Parse(DeptId.Text) != 0)
                        {
                            if (objAuthorizationBDto.UserSelectedCompanyId == int.Parse(CompanyId.Text) && objAuthorizationBDto.UserSelectedRoleId == int.Parse(RoleId.Text) && objAuthorizationBDto.UserSelectedDepartmentId == int.Parse(DeptId.Text))
                                rbCompanyRole.Checked = true;
                            Session["CompanyId"] = CompanyId.Text;
                            Session["DeptId"] = DeptId.Text;
                            Session["RoleId"] = RoleId.Text;
                        }
                    }
                    catch (Exception ex) { }

                }
            }
        }

        private void SetgrdCompanyRoleaspRadioButtonOnClick()
        {
            RadioButton radioButton;
            for (int i = 0; i < grdCompanyRoleasp.Rows.Count; i++)
            {
                radioButton = (RadioButton)grdCompanyRoleasp.Rows[i].Cells[0].FindControl("rbCompanyRole");
                radioButton.Attributes.Add("OnClick", "SelectMeOnly(" + radioButton.ClientID + ", " + "'grdCompanyRoleasp'" + ")");
            }
        }

        #endregion
        public void RegisterPostbackTrigger(Control triggerOn)
        {
            ScriptManagerCRMMaster.RegisterPostBackControl(triggerOn);

        }

        #endregion

        protected void lnkBtnMyProfile_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}


