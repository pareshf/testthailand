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
using CRM.Model.Security;
using CRM.DataAccess.AdministrationDAL;
using CRM.DataAccess;
using Telerik.Web.UI;
using CRM.Model.AdministrationModel;
using CRM.Core.Constants;
using CRM.WebApp.Views.Shared.Controls.Navigation;

namespace CRM.WebApp.Views.Administration
{
    public partial class RoleAccessMapping : System.Web.UI.Page
    {
        #region Member variables
        RoleAccessMappingDal objRoleAccessMappingDal = null;
        BindCombo objBindCombo = null;
        RoleAccessMappingBDto objRoleAccessMappingBDto = null;
        PanelBarNavigation objPanelBarNavigation = null;
        public static int GlobalRoleId;
        public static int GlobalDeptId;
        public static int GlobalCompId;
        public const String vsAssignedModule = "AssignedModule";
        public const String vsRoleName = "RoleName";
        AuthorizationBDto objAuthorizationBDto;
        #endregion

        #region events

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Role Access Mapping";
            }
            String sEventArguments = Request.Params["__EVENTARGUMENT"];
            if (sEventArguments == "DeleteModule")
                DeallocateModule();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                if (objAuthorizationBDto.ProgramWriteAccess == true && objAuthorizationBDto.ProgramDeleteAccess == true)
                {
                    btnAssignRightModule.Enabled = true;
                    btnAssignLeftModule.Enabled = true;
                    btnSave.Enabled = true;
                }
                if (objAuthorizationBDto.ProgramWriteAccess == true && objAuthorizationBDto.ProgramDeleteAccess != true)
                {
                    btnAssignRightModule.Enabled = true;
                    btnAssignLeftModule.Enabled = false;
                    btnSave.Enabled = true;
                }
                if (objAuthorizationBDto.ProgramWriteAccess != true && objAuthorizationBDto.ProgramDeleteAccess == true)
                {
                    btnAssignRightModule.Enabled = false;
                    btnAssignLeftModule.Enabled = true;
                    btnSave.Enabled = false;
                }

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Role Access Mapping");
        }
        #region Override Style Sheet Theme
        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session[PageConstants.ThemeName] == null)
                    return "Default";
                else
                    return HttpContext.Current.Session[PageConstants.ThemeName].ToString();
            }
        }
        #endregion
        #endregion

        #region GridEvent
        protected void radgrdRoleAccess_OnItemCommand(object Sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    //showWarningMessage();
                    GridDataItem item = radgrdRoleAccess.Items[e.Item.ItemIndex];
                    objRoleAccessMappingBDto = new RoleAccessMappingBDto();
                    GlobalRoleId = int.Parse(item["ROLE_ID"].Text);
                    lblRoleTitle.Text = item["ROLE_NAME"].Text;
                    GlobalDeptId = int.Parse(item["DEPARTMENT_ID"].Text);
                    //lblDeptTitle.Text = item["DEPARTMENT_NAME"].Text;
                    GlobalCompId = int.Parse(item["COMPANY_ID"].Text);
                    //lblCompTitle.Text = item["COMPANY_NAME"].Text;

                    pnlModule.Visible = true;
                    //BindModulelistbox(GlobalRoleId);
                    if (lstAssignModuleName.Items.Count > 0)
                    {
                        lstAssignModuleName.Items[0].Selected = true;
                        objRoleAccessMappingBDto.RoleID = GlobalRoleId;
                        objRoleAccessMappingBDto.ModuleID = int.Parse(lstAssignModuleName.SelectedValue);
                        pnlAccessGrid.Visible = true;
                        lstAssignModuleName.Items[0].Selected = true;
                        BindGridAccess(objRoleAccessMappingBDto);
                    }
                    else
                    {
                        pnlAccessGrid.Visible = false;
                    }
                }
                else
                {
                    pnlModule.Visible = false;
                }
            }
            catch (Exception ex)
            {
                pnlModule.Visible = false;
            }
        }

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdRoleAccess_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsRoleName] != null)
                radgrdRoleAccess.DataSource = ViewState[vsRoleName];
        }

        protected void radGridAccess_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBoxRead = (CheckBox)e.Item.FindControl("grdChkAllRead");
                    CheckBox chkBoxWrite = (CheckBox)e.Item.FindControl("grdChkAllWrite");
                    CheckBox chkBoxDelete = (CheckBox)e.Item.FindControl("grdChkAllDelete");
                    CheckBox chkBoxPrint = (CheckBox)e.Item.FindControl("grdChkAllPrint");
                    if (chkBoxRead != null)
                        chkBoxRead.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radGridAccess.ClientID, 1, chkBoxRead.ClientID));
                    if (chkBoxWrite != null)
                        chkBoxWrite.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radGridAccess.ClientID, 2, chkBoxWrite.ClientID));
                    if (chkBoxDelete != null)
                        chkBoxDelete.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radGridAccess.ClientID, 3, chkBoxDelete.ClientID));
                    if (chkBoxPrint != null)
                        chkBoxPrint.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radGridAccess.ClientID, 4, chkBoxPrint.ClientID));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region Button Event

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int result;
            bool SaveFail = true;
            objRoleAccessMappingBDto = new RoleAccessMappingBDto();
            objRoleAccessMappingDal = new RoleAccessMappingDal();
            foreach (GridDataItem item in radGridAccess.Items)
            {
                CheckBox chkRead = (CheckBox)item.FindControl("grdChkRead");
                CheckBox chkWrite = (CheckBox)item.FindControl("grdChkWrite");
                CheckBox chkDelete = (CheckBox)item.FindControl("grdChkDelete");
                CheckBox chkPrint = (CheckBox)item.FindControl("grdChkPrint");
                if (item["PROGRAM_ID"] != null)
                    objRoleAccessMappingBDto.ProgramID = int.Parse(item["PROGRAM_ID"].Text);
                if (GlobalRoleId != 0)
                    objRoleAccessMappingBDto.RoleID = GlobalRoleId;
                if (chkRead != null)
                    objRoleAccessMappingBDto.ReadAccess = chkRead.Checked;
                if (chkWrite != null)
                    objRoleAccessMappingBDto.WriteAccess = chkWrite.Checked;
                if (chkDelete != null)
                    objRoleAccessMappingBDto.DeleteAccess = chkDelete.Checked;
                if (chkPrint != null)
                    objRoleAccessMappingBDto.PrintAccess = chkPrint.Checked;
                result = objRoleAccessMappingDal.InsertAccess(objRoleAccessMappingBDto);
                if (result != 1)
                    SaveFail = false;
            }
            if (SaveFail)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
                objPanelBarNavigation = new PanelBarNavigation();
                PanelBarNavigation ucNavigation = (PanelBarNavigation)Master.FindControl("pnlLeftMenu");
                if (objAuthorizationBDto.UserSelectedModuleId != 0 && objAuthorizationBDto.UserSelectedRoleId != 0)
                    ucNavigation.BindLeftMenu(objAuthorizationBDto.UserSelectedModuleId, objAuthorizationBDto.UserSelectedRoleId,objAuthorizationBDto.UserSelectedCompanyId,objAuthorizationBDto.UserSelectedDepartmentId);

            }
            pnlModule.Visible = false;
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            pnlModule.Visible = false;
        }

        #endregion

        #region ListBox
        protected void btnAssignRightModule_Click(object sender, EventArgs e)
        {
            try
            {
                int ModuleCount = 0;
                for (int i = 0; i < lstModuleName.Items.Count; i++)
                {
                    if (lstModuleName.Items[i].Selected)
                        ModuleCount++;

                }
                if (ModuleCount > 0)
                {
                    for (int i = 0; i < lstModuleName.Items.Count; i++)
                    {
                        if (lstModuleName.Items[i].Selected)
                        {
                            lstAssignModuleName.Items.Add(lstModuleName.Items[i]);
                            lstModuleName.Items.Remove(lstModuleName.Items[i]);
                            lstModuleName.ClearSelection();
                            lstAssignModuleName.ClearSelection();
                        }
                    }
                    pnlAccessGrid.Visible = false;
                }
                else
                {
                    showMessage("Select ModuleName");
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnAssignLeftModule_Click(object sender, EventArgs e)
        {
            int AssignModuleCount = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState[vsAssignedModule];
            bool isPopup = false;
            for (int i = 0; i < lstAssignModuleName.Items.Count; i++)
            {
                if (lstAssignModuleName.Items[i].Selected)
                    AssignModuleCount++;
            }
            if (AssignModuleCount > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["MODULE_ID"].ToString() == lstAssignModuleName.SelectedValue)
                        isPopup = true;
                }
                if (isPopup)
                {
                    //showWarningMessage();
                    //up.Update();
                    DeallocateModule();
                }
                else
                {
                    lstModuleName.Items.Add(lstAssignModuleName.SelectedItem);
                    lstAssignModuleName.Items.Remove(lstAssignModuleName.SelectedItem);
                    lstModuleName.ClearSelection();
                    lstAssignModuleName.ClearSelection();
                    pnlAccessGrid.Visible = false;
                }
            }
            else
            {
                showMessage("Select ModuleName");
            }



        }

        protected void lstAssignModuleName_OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            pnlAccessGrid.Visible = true;
            objRoleAccessMappingBDto = new RoleAccessMappingBDto();
            objRoleAccessMappingBDto.RoleID = GlobalRoleId;
            objRoleAccessMappingBDto.ModuleID = int.Parse(lstAssignModuleName.SelectedValue);
            BindGridAccess(objRoleAccessMappingBDto);
        }

        #endregion

        #endregion

        #region Methods

        #region Bind Grid
        /// <summary>
        /// Bind customer grid
        /// </summary>
        private void BindGrid()
        {
            objBindCombo = new BindCombo();
            DataSet dsRoleMapping = objBindCombo.GetRoleKeyValue();
            radgrdRoleAccess.DataSource = dsRoleMapping;
            radgrdRoleAccess.DataBind();
            ViewState[vsRoleName] = dsRoleMapping;
        }
        #endregion

        #region Bind Role Access Grid
        /// <summary>
        /// Bind customer grid
        /// </summary>
        private void BindGridAccess(RoleAccessMappingBDto objRoleAccessMappingBDto)
        {
            DataSet dsRoleMapping1 = null;
            objRoleAccessMappingDal = new RoleAccessMappingDal();
            dsRoleMapping1 = objRoleAccessMappingDal.GetAccessGrid(objRoleAccessMappingBDto);
            radGridAccess.DataSource = dsRoleMapping1;
            radGridAccess.DataBind();
        }
        #endregion

        #region Both Module Lists
        public void BindModulelistbox(int RoleId,int DeptId,int CompId)
        {
            BindAssignModule_ByRole(RoleId,DeptId,CompId);
            BindModuleList();
        }
        #endregion

        #region Fill AssignRole_ByUserId and CompanyId
        private void BindAssignModule_ByRole(int RoleId,int DeptId,int CompId)
        {
            objRoleAccessMappingDal = new RoleAccessMappingDal();
            lstAssignModuleName.Items.Clear();
            lstAssignModuleName.ClearSelection();
            lstAssignModuleName.DataTextField = "MODULE_NAME";
            lstAssignModuleName.DataValueField = "MODULE_ID";
            DataSet ds = new DataSet();
            ds = objRoleAccessMappingDal.GetModuleByRole(RoleId,DeptId,CompId);
            lstAssignModuleName.DataSource = ds;
            lstAssignModuleName.DataBind();
            ViewState[vsAssignedModule] = ds.Tables[0];
        }
        #endregion

        #region Fill Module name List
        private void BindModuleList()
        {
            objBindCombo = new BindCombo();
            lstModuleName.Items.Clear();
            lstModuleName.ClearSelection();
            lstModuleName.DataTextField = "MODULE_NAME";
            lstModuleName.DataValueField = "MODULE_ID";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objBindCombo.GetModuleKeyValue();
            dt = ds.Tables[0];
            if (lstAssignModuleName.Items.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < lstAssignModuleName.Items.Count; j++)
                    {
                        if (dt.Rows[i]["MODULE_ID"].ToString() == lstAssignModuleName.Items[j].Value)
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
            }
            lstModuleName.DataSource = dt;
            lstModuleName.DataBind();
        }
        #endregion

        #region  Message
        public void showMessage(String Message)
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('" + Message + "', 330, 110, 'Warning Message'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", radalertscript, false);
        }

        public void showWarningMessage()
        {
            //string radalertscript = "<script language='javascript'>radconfirm('Are you sure?', event, 400, 200,'','Custom title');</script>";
            string radalertscript = "<script language='javascript'>DeletePopup();</script>";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", radalertscript, false);

        }

        #endregion
        public void DeallocateModule()
        {
            if (lstAssignModuleName.SelectedValue != null || lstAssignModuleName.SelectedValue != "")
            {
                objRoleAccessMappingBDto = new RoleAccessMappingBDto();
                objRoleAccessMappingDal = new RoleAccessMappingDal();
                objRoleAccessMappingBDto.RoleID = GlobalRoleId;
                objRoleAccessMappingBDto.ModuleID = int.Parse(lstAssignModuleName.SelectedValue);
                int Result = objRoleAccessMappingDal.DeleteAccess(objRoleAccessMappingBDto);
                //BindModulelistbox(GlobalRoleId);
                pnlAccessGrid.Visible = false;
            }

        }

        #endregion

        protected void CancelButton_Click(object o, EventArgs e)
        {

        }

    }
}
