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
using System.Text;
using CRM.Core.Constants;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.Security;
using Telerik.Web.UI;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using CRM.Core.Utility.DateTimeUtility;

namespace CRM.WebApp.Views.Administration
{
    public partial class UserMaster : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        UserMasterDal objUserMasterDAL = null;
        UserMasterBDto objUserMasterBdto = null;
        public const String vsUser = "User";
        public static int GlobalUserId;
        public static int GlobalEmpId;
        AuthorizationBDto objAuthorizationBDto;
        #endregion

        #region Events

        #region Page Event
        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Manage User");
        }
        #endregion

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    switch (Request.QueryString["Action"].ToLower())
                    {
                        case "addnewuser":
                            acbUser_NewClick(new object(), new EventArgs());
                            break;
                    }
                }
                BindGrid();
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Manage User";
            }
            hdnEditableMode.Value = "false";
            acbUser.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdUser.ClientID));
            acbUser.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdUser.ClientID));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                if (objAuthorizationBDto.ProgramWriteAccess != true && objAuthorizationBDto.ProgramDeleteAccess != true)
                {
                    btnAssignRightCompany.Enabled = false;
                    btnAssignLeftCompany.Enabled = false;
                    btnAssignRightRole.Enabled = false;
                    btnAssignLeftRole.Enabled = false;
                }
                if (objAuthorizationBDto.ProgramWriteAccess != true && objAuthorizationBDto.ProgramDeleteAccess == true)
                {
                    btnAssignRightCompany.Enabled = false;
                    btnAssignLeftCompany.Enabled = true;
                    btnAssignRightRole.Enabled = false;
                    btnAssignLeftRole.Enabled = true;
                }
                if (objAuthorizationBDto.ProgramWriteAccess == true && objAuthorizationBDto.ProgramDeleteAccess != true)
                {
                    btnAssignRightCompany.Enabled = true;
                    btnAssignLeftCompany.Enabled = false;
                    btnAssignRightRole.Enabled = true;
                    btnAssignLeftRole.Enabled = false;
                }

            }
        }
        #endregion

        #region Actionbar Events

        /// <summary>
        /// Action bar's new button event which open view to insert new customer.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbUser_NewClick(object sender, EventArgs e)
        {

            //acbUser.EditableMode = true;
            acbUser.Visible = false;
            pnlGrid.Visible = false;
            ucControlBox.SaveButton.CommandName = "Save";
            ucControlBox.SaveButton.Text = "Save";
            ValidationGroupSave();
            pnlAddMode.Visible = true;
            Reset();
            BindEmployeeCombo(ddlEmployeeName);
            BindSecurityQuestionCombo(ddlSecurityQus);
            txtUserName.Focus();
            txtPassword.Enabled = true;
            txtRetypePassword.Enabled = true;

            radMultiPage.SelectedIndex = 0;
            radTabStripe.FindTabByText("User Access").Enabled = false;
            radTabStripe.FindTabByText("User Info").Selected = true;

        }

        /// <summary>
        /// Action bar's edit button event which open multiple rows in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbUser_EditClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
            }

            if (htItemIndex != null)
            {
                if (htItemIndex.Count == 1)
                {
                    pnlGrid.Visible = false;
                    radMultiPage.SelectedIndex = 0;
                    radTabStripe.FindTabByText("User Info").Selected = true;

                    BindEmployeeCombo(ddlEmployeeName);
                    BindSecurityQuestionCombo(ddlSecurityQus);
                    txtPassword.Enabled = true;
                    txtRetypePassword.Enabled = true;

                    hdnEditableMode.Value = "true";
                    pnlAddMode.Visible = true;
                    acbUser.Visible = false;
                    ucControlBox.SaveButton.CommandName = "Update";
                    ucControlBox.SaveButton.Text = "Save";
                    ValidationGroupUpdate();
                    GridDataItem item = radgrdUser.Items[int.Parse(htItemIndex[0].ToString())];

                    ddlEmployeeName.Items.FindItemByValue(item["EMP_ID"].Text).Selected = true;

                    ddlSecurityQus.Items.FindItemByText(item["SECURITY_QUESTION_DESC"].Text).Selected = true;
                    rdpFromDate.Clear();
                    rdpFromDate.SelectedDate = DateTimeHelper.ConvertToMmDdYyyy(item["FROM_DATE"].Text);
                    rdpToDate.Clear();
                    rdpToDate.SelectedDate = DateTimeHelper.ConvertToMmDdYyyy(item["TO_DATE"].Text);
                    txtUserName.Text = item["USER_NAME"].Text;
                    txtPassword.Text = item["PASSWORD"].Text;
                    txtRetypePassword.Text = item["PASSWORD"].Text;
                    txtSecurityQusAns.Text = item["SECURITY_ANSWERS"].Text;
                    GlobalUserId = Convert.ToInt32(item["USER_ID"].Text);
                    GlobalEmpId = Convert.ToInt32(item["EMP_ID"].Text);
                    radTabStripe.FindTabByText("User Access").Enabled = true;
                    FillCompanyLists(GlobalUserId, GlobalEmpId);
                    if (lstAssignCompanyName.Items.Count > 0)
                    {
                        lstAssignCompanyName.Items[0].Selected = true;
                        FillRoleLists(GlobalUserId, int.Parse(lstAssignCompanyName.Items[0].Value));
                    }
                    else
                    {
                        lstAssignRole.Items.Clear();
                        lstAssignRole.ClearSelection();
                        BindRoleList();
                    }

                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings["OnlyOneRecord"].ToString());
                    Master.MessageCssClass = "errorMessage";
                    htItemIndex = null;
                }
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings["AtleastOneRecord"].ToString());
                Master.MessageCssClass = "errorMessage";
                htItemIndex = null;
            }

        }

        /// <summary>
        /// Action bar's delete button event which delete records physically from database.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbUser_DeleteClick(object sender, EventArgs e)
        {
            StringBuilder UserId = new StringBuilder();
            int result = 0;
            if (ViewState[PageConstants.vsItemIndexes] != null)
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

            if (htItemIndex != null)
            {
                foreach (int i in htItemIndex.Values)
                {
                    GridDataItem item = radgrdUser.Items[i];
                    UserId.Append(int.Parse(item["USER_ID"].Text) + ",");
                }
            }
            objUserMasterDAL = new UserMasterDal();
            String Id = UserId.ToString().TrimEnd(',');
            result = objUserMasterDAL.DeleteUser(Id);
            if (result == 1)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Delete].ToString());
                Master.MessageCssClass = "successMessage";
                ViewState[PageConstants.vsItemIndexes] = null;
                BindGrid();
            }
            else if (result == 547)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Delete].ToString());
                Master.MessageCssClass = "errorMessage";
            }

        }

        /// <summary>
        /// Action bar's Search button event which search records from database. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void acbUser_SearchClick(object sender, EventArgs e)
        {
            #region OpenSearchInNewForm
            //acbUser.Visible = false;
            //pnlGrid.Visible = false;
            //ucControlBox.SaveButton.CommandName = "Search";
            //ucControlBox.SaveButton.Text = "Search";
            //ValidationGroupSearch();
            //txtPassword.Enabled = false;
            //txtRetypePassword.Enabled = false;
            //pnlAddMode.Visible = true;

            //radMultiPage.SelectedIndex = 0;
            //radTabStripe.FindTabByText("User Access").Enabled = false;
            //radTabStripe.FindTabByText("User Info").Selected = true;

            //Reset();
            //BindEmployeeCombo(ddlEmployeeName);
            //BindSecurityQuestionCombo(ddlSecurityQus);
            #endregion
            ////
            UserMasterBDto objUserMasterBDto = new UserMasterBDto();
            UserMasterDal objUserMasterDal = new UserMasterDal();
            objUserMasterBDto.UserName = acbUser.SearchTextBox.Text.Trim();
            DataSet dsUser = objUserMasterDal.GetUserSearch(objUserMasterBDto);
            if (dsUser != null && dsUser.Tables[0].Rows.Count > 0)
            {
                radgrdUser.DataSource = dsUser;
                radgrdUser.DataBind();
                ViewState[vsUser] = dsUser;
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings["NoRecord"].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        /// <summary>
        /// Action bar's save button event which saves grid data by xml.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbUser_SaveClick(object sender, EventArgs e)
        {
            int result = 0;
            objUserMasterDAL = new UserMasterDal();
            switch (ucControlBox.SaveButton.CommandName)
            {
                case "Save":
                    objUserMasterBdto = new UserMasterBDto();
                    objUserMasterBdto.UserId = objAuthorizationBDto.UserProfile.UserId;
                    objUserMasterBdto.UserName = txtUserName.Text;
                    objUserMasterBdto.Password = txtPassword.Text;
                    objUserMasterBdto.EmpID = int.Parse(ddlEmployeeName.SelectedValue);
                    objUserMasterBdto.SecurityQusId = int.Parse(ddlSecurityQus.SelectedValue);
                    objUserMasterBdto.SecurityQusAns = txtSecurityQusAns.Text;
                    objUserMasterBdto.FromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
                    objUserMasterBdto.ToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
                    result = objUserMasterDAL.InsertUser(objUserMasterBdto);

                    GlobalUserId = result;
                    GlobalEmpId = int.Parse(ddlEmployeeName.SelectedValue);

                    if (result == -2)
                    {
                        Master.DisplayMessage("Username already exists. Record is not saved.");
                        Master.MessageCssClass = "errorMessage";
                    }
                    else if (result >= 1)
                    {
                        Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                        Master.MessageCssClass = "successMessage";
                        BindGrid();
                        radTabStripe.FindTabByText("User Access").Enabled = true;
                        FillCompanyLists(GlobalUserId, GlobalEmpId);
                        BindRoleList();
                    }
                    else
                    {
                        Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                        Master.MessageCssClass = "errorMessage";
                    }
                    break;
                case "Update":
                    objUserMasterBdto = new UserMasterBDto();
                    objUserMasterDAL = new UserMasterDal();
                    //UpdateUser();

                    if (ViewState[PageConstants.vsItemIndexes] != null)
                    {
                        htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                    }
                    if (htItemIndex != null)
                    {
                        GridDataItem item = radgrdUser.Items[int.Parse(htItemIndex[0].ToString())];
                        //Label lblGrdUserId = (Label)radgrdUser.Items[Convert.ToInt32(htItemIndex[0])].FindControl("lblGrdUserID");
                        objUserMasterBdto.m_UserId = int.Parse(item["USER_ID"].Text);
                        objUserMasterBdto.UserId = objAuthorizationBDto.UserProfile.UserId;
                        objUserMasterBdto.UserName = txtUserName.Text;
                        objUserMasterBdto.Password = txtPassword.Text;
                        objUserMasterBdto.EmpID = int.Parse(ddlEmployeeName.SelectedValue);
                        objUserMasterBdto.SecurityQusId = int.Parse(ddlSecurityQus.SelectedValue);
                        objUserMasterBdto.SecurityQusAns = txtSecurityQusAns.Text;
                        objUserMasterBdto.FromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
                        objUserMasterBdto.ToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
                        GlobalEmpId = int.Parse(ddlEmployeeName.SelectedValue);
                        result = objUserMasterDAL.UpdateUser(objUserMasterBdto);
                        if (result == 1)
                        {
                            acbUser.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdUser.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                            //bisEdit = true;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGrid();
                            radgrdUser.Rebind();
                            acbUser.Visible = true;
                            pnlAddMode.Visible = false;
                            pnlGrid.Visible = true;
                        }
                        else
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                            Master.MessageCssClass = "errorMessage";
                            acbUser.Visible = true;
                            pnlAddMode.Visible = false;
                        }
                    }
                    break;
                case "Search":
                    {
                        //UserMasterBDto objUserMasterBDto = new UserMasterBDto();
                        //objUserMasterBDto.UserName = txtUserName.Text;
                        //objUserMasterBDto.EmpID = int.Parse(ddlEmployeeName.SelectedValue);
                        //objUserMasterBDto.SecurityQusId = int.Parse(ddlSecurityQus.SelectedValue);
                        //objUserMasterBDto.SecurityQusAns = txtSecurityQusAns.Text;
                        //objUserMasterBDto.FromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
                        //objUserMasterBDto.ToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
                        //DataSet dsUser = objUserMasterDAL.GetUser(objUserMasterBDto);
                        //if (dsUser != null && dsUser.Tables[0].Rows.Count > 0)
                        //{
                        //    radgrdUser.DataSource = dsUser;
                        //    radgrdUser.DataBind();
                        //    ViewState[vsUser] = dsUser;
                        //    acbUser.Visible = true;
                        //    pnlAddMode.Visible = false;
                        //    pnlGrid.Visible = true;
                        //}
                        //else
                        //{
                        //    Master.DisplayMessage(ConfigurationSettings.AppSettings["NoRecord"].ToString());
                        //    Master.MessageCssClass = "errorMessage";
                        //}
                        break;
                    }
            }
        }

        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbUser_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdUser.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                //bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdUser.Rebind();
            }
            acbUser.Visible = true;
            pnlAddMode.Visible = false;
            pnlGrid.Visible = true;
        }

        protected void acbAddress_RefreshClick(object sender, EventArgs e)
        {
            acbUser.DefaultMode = true;
            pnlAddMode.Visible = false;
            acbUser.SearchTextBox.Text = String.Empty;
            BindGrid();
        }


        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void upControlBox_ClearClick(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                //bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                //if (rethrow)
                //{ throw ex; }
            }
        }



        #endregion

        #region Grid events
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdUser_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdUser.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdUser.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }

        /// <summary>
        /// Rad grid's item databound event which add client side event to check box of rows.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdUser_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdUser.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdUser.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign data source after grid's page changing and sorting.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdUser_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsUser] != null)
                radgrdUser.DataSource = ViewState[vsUser];
        }
        #endregion

        #region Grid Checkbox Events

        /// <summary>
        /// Saves checkbox checked status into hashtable after check status changed.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        public void chkItemWrt_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridDataItem item = (GridDataItem)chkBox.NamingContainer;
            if ((ViewState[PageConstants.vsItemIndexes] != null))
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
            }
            else
            {
                htItemIndex = new Hashtable();
            }
            if (chkBox.Checked == true)
            {
                hdnCheckIndex.Value = item.ItemIndex.ToString();
                htItemIndex.Add(htItemIndex.Count, item.ItemIndex);
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
                for (int i = 0; i <= htItemIndex.Count - 1; i++)
                {
                    if (htItemIndex[i] != null)
                    {
                        if (htItemIndex[i].ToString() == item.ItemIndex.ToString())
                        {
                            radgrdUser.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }

        #endregion

        #region Acccess View

        #region Access Action Bar Event
        protected void usCancelAccessFoolter_Click(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdUser.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                //bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdUser.Rebind();
            }
            acbUser.Visible = true;
            pnlAddMode.Visible = false;
            pnlGrid.Visible = true;
        }
        #endregion

        #region Access Move Button Event
        protected void btnAssignRightCompany_Click(object Sender, EventArgs e)
        {
            int CompanyCount = 0;
            for (int i = 0; i < lstCompanyName.Items.Count; i++)
            {
                if (lstCompanyName.Items[i].Selected)
                    CompanyCount++;
            }
            if (CompanyCount > 0)
            {
                StringBuilder CompanyId = new StringBuilder();
                for (int i = 0; i < lstCompanyName.Items.Count; i++)
                {
                    if (lstCompanyName.Items[i].Selected)
                        CompanyId.Append(lstCompanyName.Items[i].Value + ",");
                }
                String Id = CompanyId.ToString().TrimEnd(',');
                if (Id != string.Empty)
                {
                    AssignCompany(Id, GlobalUserId);
                    FillCompanyLists(GlobalUserId, GlobalEmpId);
                    lstAssignCompanyName.ClearSelection();
                    lstAssignCompanyName.Items.FindByValue(Id).Selected = true;
                    FillRoleLists(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue));
                }
            }
            else
            {
                showMessage("Select CompanyName");
            }

        }

        protected void btnAssignLeftCompany_Click(object Sender, EventArgs e)
        {

            StringBuilder AssignCompanyId = new StringBuilder();
            int CompanyCount = 0;
            for (int i = 0; i < lstAssignCompanyName.Items.Count; i++)
            {
                if (lstAssignCompanyName.Items[i].Selected)
                    CompanyCount++;
            }
            if (CompanyCount > 0)
            {

                for (int i = 0; i < lstAssignCompanyName.Items.Count; i++)
                {
                    if (lstAssignCompanyName.Items[i].Selected)
                        AssignCompanyId.Append(lstAssignCompanyName.Items[i].Value + ",");
                }

                String Id = AssignCompanyId.ToString().TrimEnd(',');
                if (Id != string.Empty)
                {
                    UnAssignCompany(Id, GlobalUserId);
                    FillCompanyLists(GlobalUserId, GlobalEmpId);
                    lstAssignRole.Items.Clear();
                    BindRoleList();
                }
            }
            else
            {
                showMessage("Select Assigned CompanyName");
            }
        }

        protected void btnAssignRightRole_Click(object Sender, EventArgs e)
        {
            int CompanyCount = 0;
            int RollCount = 0;
            for (int i = 0; i < lstAssignCompanyName.Items.Count; i++)
            {
                if (lstAssignCompanyName.Items[i].Selected)
                    CompanyCount++;
            }
            for (int i = 0; i < lstRole.Items.Count; i++)
            {
                if (lstRole.Items[i].Selected)
                    RollCount++;
            }
            if (CompanyCount > 0 && RollCount > 0)
            {
                AssignRole(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue), int.Parse(lstRole.SelectedValue));
                FillRoleLists(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue));
            }
            else
            {
                showMessage("Select one Company And Roll");
            }
        }

        protected void btnAssignLeftRole_Click(object Sender, EventArgs e)
        {
            int CompanyCount = 0;
            int RollCount = 0;
            for (int i = 0; i < lstAssignCompanyName.Items.Count; i++)
            {
                if (lstAssignCompanyName.Items[i].Selected)
                    CompanyCount++;
            }
            for (int i = 0; i < lstAssignRole.Items.Count; i++)
            {
                if (lstAssignRole.Items[i].Selected)
                    RollCount++;
            }
            if (CompanyCount > 0 && RollCount > 0)
            {

                UnAssignRole(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue), int.Parse(lstAssignRole.SelectedValue));
                FillRoleLists(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue));
            }
            else
            {
                showMessage("Select one Company And Roll");
            }
        }

        #endregion

        #region List View Event
        protected void lstAssignCompanyName_OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstAssignCompanyName.SelectedValue != null && GlobalUserId != 0)
            {
                FillRoleLists(GlobalUserId, int.Parse(lstAssignCompanyName.SelectedValue));
                //BindAssignRoleList_ByUserID(GlobalUserId, Convert.ToInt32(lstAssignCompanyName.SelectedValue));
            }
        }
        #endregion

        #endregion

        #endregion Events

        #region Methods

        #region Bind Grid
        /// <summary>
        /// Bind customer grid
        /// </summary>
        private void BindGrid()
        {
            objUserMasterDAL = new UserMasterDal();
            DataSet dsCustomer = objUserMasterDAL.GetUser();
            radgrdUser.DataSource = dsCustomer;
            radgrdUser.DataBind();
            ViewState[vsUser] = dsCustomer;
        }
        #endregion

        #region Generate Xml String
        /// <summary>
        /// Generate xml format data from grid.
        /// </summary>
        /// <param name="grid">Rad grid control which data to be converted into xml format.</param>
        /// <returns>Returns xml format data in string.</returns>
        private String GenerateXmlString(RadGrid grid)
        {
            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<Customers  CUST_ID=\"{0}\" CUST_SURNAME=\"{1}\" CUST_NAME =\"{2}\" CUST_PROFILE = \"{3}\" CUST_COMPANY_NAME = \"{4}\" ></Customers>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdCustId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdCustIdEdit");
                        xmlString.AppendFormat(xmlHeaderNodeStructure, lblGrdCustId.Text, txtUserName.Text, txtPassword.Text, int.Parse(ddlEmployeeName.SelectedValue), (ddlSecurityQus.SelectedValue));
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region BInd Question Combo
        private void BindSecurityQuestionCombo(RadComboBox sender)
        {
            objUserMasterDAL = new UserMasterDal();
            sender.Items.Clear();
            sender.ClearSelection();
            sender.DataTextField = "SECURITY_QUESTION_DESC";
            sender.DataValueField = "SECURITY_QUESTION_ID";
            sender.DataSource = objUserMasterDAL.GetSecurityQuestion();
            sender.DataBind();
            sender.Items.Insert(0, new RadComboBoxItem("--Select--", "0"));
            sender.SelectedValue = "0";
        }
        #endregion

        #region BInd Employee COmbo
        private void BindEmployeeCombo(RadComboBox sender)
        {
            objUserMasterDAL = new UserMasterDal();
            sender.Items.Clear();
            sender.ClearSelection();
            sender.DataTextField = "EMP_NAME";
            sender.DataValueField = "EMP_ID";
            sender.DataSource = objUserMasterDAL.GetEmployeeKeyValue();
            sender.DataBind();
            sender.Items.Insert(0, new RadComboBoxItem("", "0"));
            sender.SelectedValue = "0";
        }
        #endregion

        #region Bind Both Company List
        public void FillCompanyLists(int UserId, int EmpID)
        {
            if (UserId != 0 && EmpID != 0)
            {
                BindAssignCompanyList_ByUserID(UserId);
                BindCompanyList(EmpID);
            }
        }
        #endregion

        #region Bind Both Role List
        public void FillRoleLists(int UserID, int CompanyId)
        {
            BindAssignRoleList_ByUserID(UserID, CompanyId);
            BindRoleList();
        }
        #endregion

        #region Fill AssignCompanyName_ByUserId
        private void BindAssignCompanyList_ByUserID(int UserId)
        {
            UserMasterDal ObjUserMasterDal = new UserMasterDal();
            lstAssignCompanyName.Items.Clear();
            lstAssignCompanyName.ClearSelection();
            lstAssignCompanyName.DataTextField = "COMPANY_NAME";
            lstAssignCompanyName.DataValueField = "COMPANY_ID";
            lstAssignCompanyName.DataSource = ObjUserMasterDal.GetAssignedCompanyList_ByUserID(UserId);
            lstAssignCompanyName.DataBind();
        }
        #endregion

        #region Fill AssignRole_ByUserId and CompanyId
        private void BindAssignRoleList_ByUserID(int UserId, int CompanyId)
        {
            UserMasterDal ObjUserMasterDal = new UserMasterDal();
            lstAssignRole.Items.Clear();
            lstAssignRole.ClearSelection();
            lstAssignRole.DataTextField = "ROLE_NAME";
            lstAssignRole.DataValueField = "ROLE_ID";
            lstAssignRole.DataSource = ObjUserMasterDal.GetAssignedRoleList_ByUserID(UserId, CompanyId);
            lstAssignRole.DataBind();
        }
        #endregion

        #region Fill CompanyName
        private void BindCompanyList(int EmpID)
        {
            BindCombo ObjBindCombo = new BindCombo();
            lstCompanyName.Items.Clear();
            lstCompanyName.ClearSelection();
            lstCompanyName.DataTextField = "COMPANY_NAME";
            lstCompanyName.DataValueField = "COMPANY_ID";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objUserMasterDAL.GetCompanyByEmployeeId(EmpID);
            dt = ds.Tables[0];
            if (lstAssignCompanyName.Items.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < lstAssignCompanyName.Items.Count; j++)
                    {
                        if (dt.Rows[i]["COMPANY_ID"].ToString() == lstAssignCompanyName.Items[j].Value)
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
            }
            lstCompanyName.DataSource = dt;
            lstCompanyName.DataBind();
        }
        #endregion

        #region Fill RoleName
        private void BindRoleList()
        {
            BindCombo ObjBindCombo = new BindCombo();
            lstRole.Items.Clear();
            lstRole.ClearSelection();
            lstRole.DataTextField = "ROLE_NAME";
            lstRole.DataValueField = "ROLE_ID";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = ObjBindCombo.GetRoleKeyValue();
            dt = ds.Tables[0];
            if (lstAssignRole.Items.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < lstAssignRole.Items.Count; j++)
                    {
                        if (dt.Rows[i]["ROLE_ID"].ToString() == lstAssignRole.Items[j].Value)
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
            }
            lstRole.DataSource = dt;
            lstRole.DataBind();
        }
        #endregion

        #region validation Group
        public void ValidationGroupSave()
        {
            ucControlBox.SaveButton.ValidationGroup = "User";
            rfvtxtUserName.ValidationGroup = "User";
            rfvtxtPassword.ValidationGroup = "User";
            rfvtxtRetypePassword.ValidationGroup = "User";
            cmptxtRetypePassword.ValidationGroup = "User";
            cmpddlEmployeeName.ValidationGroup = "User";
            cmpddlSecurityQus.ValidationGroup = "User";
            rfvrdpFromDate.ValidationGroup = "User";
            //rfvrdpToDate.ValidationGroup = "User";
            cmprdpToDate.ValidationGroup = "User";
            rfvtxtSecurityQusAns.ValidationGroup = "User";
            cmpddlSecurityQus.ValueToCompare = "--Select--";
            cmpddlEmployeeName.ValueToCompare = "--Select--";

            lblNameRequire.Visible = true;
            lblPasswordRequired.Visible = true;
            lblRetypePasswordRequired.Visible = true;
            lblEmployeeNameRequired.Visible = true;
            lblSecurityQuestionIdRequired.Visible = true;
            lblSecurityQusAnsRequired.Visible = true;
            lblFromDateRequired.Visible = true;
        }

        public void ValidationGroupUpdate()
        {
            ucControlBox.SaveButton.ValidationGroup = "Update";
            rfvtxtUserName.ValidationGroup = "Update";
            cmptxtRetypePassword.ValidationGroup = "Update";
            cmpddlEmployeeName.ValidationGroup = "Update";
            cmpddlSecurityQus.ValidationGroup = "Update";
            rfvrdpFromDate.ValidationGroup = "Update";
            //rfvrdpToDate.ValidationGroup = "Update";
            cmprdpToDate.ValidationGroup = "Update";
            rfvtxtSecurityQusAns.ValidationGroup = "Update";
            cmpddlSecurityQus.ValueToCompare = "--Select--";
            cmpddlEmployeeName.ValueToCompare = "--Select--";

            lblNameRequire.Visible = true;
            lblPasswordRequired.Visible = true;
            lblRetypePasswordRequired.Visible = true;
            lblEmployeeNameRequired.Visible = true;
            lblSecurityQuestionIdRequired.Visible = true;
            lblSecurityQusAnsRequired.Visible = true;
            lblFromDateRequired.Visible = true;
        }

        public void ValidationGroupSearch()
        {
            ucControlBox.SaveButton.ValidationGroup = "Search";
            ucControlBox.CancelButton.ValidationGroup = "Search";
            rfvtxtUserName.ValidationGroup = "";
            cmptxtRetypePassword.ValidationGroup = "";
            cmpddlEmployeeName.ValidationGroup = "";
            cmpddlSecurityQus.ValidationGroup = "";
            rfvrdpFromDate.ValidationGroup = "";
            //rfvrdpToDate.ValidationGroup = "";
            cmprdpToDate.ValidationGroup = "";
            rfvtxtSecurityQusAns.ValidationGroup = "";
            cmpddlSecurityQus.ValueToCompare = "";
            cmpddlEmployeeName.ValueToCompare = "";

            lblNameRequire.Visible = false;
            lblPasswordRequired.Visible = false;
            lblRetypePasswordRequired.Visible = false;
            lblEmployeeNameRequired.Visible = false;
            lblSecurityQuestionIdRequired.Visible = false;
            lblSecurityQusAnsRequired.Visible = false;
            lblFromDateRequired.Visible = false;
        }

        #endregion

        private void UpdateUser()
        {
            objUserMasterBdto = new UserMasterBDto();
            try
            {
                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdUserId = (Label)radgrdUser.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdUserID");
                        objUserMasterBdto.UserId = objAuthorizationBDto.UserProfile.UserId;
                        objUserMasterBdto.UserName = txtUserName.Text;
                        objUserMasterBdto.Password = txtPassword.Text;
                        objUserMasterBdto.EmpID = int.Parse(ddlEmployeeName.SelectedValue);
                        objUserMasterBdto.SecurityQusId = int.Parse(ddlSecurityQus.SelectedValue);
                        objUserMasterBdto.SecurityQusAns = txtSecurityQusAns.Text;
                        objUserMasterBdto.FromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
                        objUserMasterBdto.ToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void Reset()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRetypePassword.Text = string.Empty;
            txtSecurityQusAns.Text = string.Empty;
            ddlEmployeeName.ClearSelection();
            ddlSecurityQus.ClearSelection();
            rdpFromDate.Clear();
            rdpToDate.Clear();
        }

        public void AssignCompany(string lst, int userid)
        {
            objUserMasterDAL = new UserMasterDal();
            int CreatedBy = objAuthorizationBDto.UserProfile.UserId;
            int result = objUserMasterDAL.InsertAssignCompany(lst, userid, CreatedBy);
            if (result >= 1)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        public void UnAssignCompany(string lst, int userid)
        {
            objUserMasterDAL = new UserMasterDal();
            //int CreatedBy = objAuthorizationBDto.UserProfile.UserId;
            int result = objUserMasterDAL.UnAssignCompany(lst, userid);
            if (result >= 1)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        public void AssignRole(int UserId, int CompanyId, int RoleId)
        {
            objUserMasterDAL = new UserMasterDal();
            int CreatedBy = objAuthorizationBDto.UserProfile.UserId;
            int result = objUserMasterDAL.InsertAssignRole(UserId, CompanyId, RoleId);
            if (result >= 1)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        public void UnAssignRole(int UserId, int CompanyId, int RoleId)
        {
            objUserMasterDAL = new UserMasterDal();
            //int CreatedBy = objAuthorizationBDto.UserProfile.UserId;
            int result = objUserMasterDAL.UnAssignRole(UserId, CompanyId, RoleId);
            if (result >= 1)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            else
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        public void showMessage(String Message)
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('" + Message + "', 330, 110, 'Warning Message'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", radalertscript, false);
        }

        #endregion
    }
}
