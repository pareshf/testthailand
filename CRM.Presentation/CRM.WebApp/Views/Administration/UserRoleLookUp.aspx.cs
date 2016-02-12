 using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using CRM.Core.Constants;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using Telerik.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Web;

namespace CRM.WebApp.Views.Administration
{
    public partial class UserRoleLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        UserRoleDal objuserRoleLookup = null;
        public const String vsUserRole = "UserRole";
        AuthorizationBDto objAuthorizationBDto;
        LookupBDto objLookupBDto = null;
        #endregion

        #region Page Events

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "User Role";
            }
            hdnEditableMode.Value = "false";
            acbUserRole.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdUserRole.ClientID));
            acbUserRole.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdUserRole.ClientID));


        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();

            WebHelper.WebManager.CheckUserAuthorizationForProgram("User Role");
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
        #endregion

        #region Method

        #region Bind Grid

        /// <summary>
        /// Bind UserRole grid
        /// </summary>

        private void BindGrid()
        {
            objuserRoleLookup = new UserRoleDal();
            DataSet dsUserRole = objuserRoleLookup.GetUserRole("");
            radgrdUserRole.DataSource = dsUserRole;
            radgrdUserRole.DataBind();
            ViewState[vsUserRole] = dsUserRole;

        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objuserRoleLookup = new UserRoleDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtUserRole.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objuserRoleLookup.InsertUserRole(objLookupBDto);
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
                BindGrid();
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        #region Reset
        private void Reset()
        {
            txtUserRole.Text = String.Empty;
           
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
            string xmlHeaderNodeStructure = "<Role ROLE_ID=\"{0}\" ROLE_NAME=\"{1}\" USER_ID=\"{2}\"></Role>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int RoleId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdRoleId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdRoleIdEdit");
                        TextBox txtgrdRoleName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdRoleName");
                        RoleId = int.Parse(lblgrdRoleId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, RoleId, txtgrdRoleName.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #endregion

        #region Actionbar Events

        protected void acbUserRole_NewClick(object sender, EventArgs e)
        {
            acbUserRole.EditableMode = true;
            acbUserRole.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtUserRole.Focus();
            radgrdUserRole.Rebind();
            Reset();

        }

        protected void acbUserRole_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objuserRoleLookup = new UserRoleDal();
                switch (acbUserRole.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbUserRole.EditableMode = false;


                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdUserRole);
                        result = objuserRoleLookup.UpdateUserRole(xmlData);
                        if (result == 1)
                        {
                            acbUserRole.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdUserRole.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
                            bisEdit = false;
                            ViewState[PageConstants.vsItemIndexes] = null;
                            BindGrid();
                        }
                        else
                        {
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Update].ToString());
                            Master.MessageCssClass = "errorMessage";
                        }
                        break;
                }
                objuserRoleLookup = new UserRoleDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbUserRole_SaveNewClick(object sender, EventArgs e)
        {
            txtUserRole.Focus();
            Save();
            Reset();
        }

        protected void acbUserRole_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdUserRole.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdUserRole.Rebind();

            }
            acbUserRole.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbUserRole_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder RoleId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblRoleId = (Label)radgrdUserRole.Items[i].FindControl("lblgrdRoleIdItem");
                        if (lblRoleId != null)
                        {
                            RoleId.Append(lblRoleId.Text + ",");
                        }
                    }
                }

                objuserRoleLookup = new UserRoleDal();
                String URoleId = RoleId.ToString().TrimEnd(',');
                result = objuserRoleLookup.DeleteUserRole(URoleId);


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


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbUserRole_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objuserRoleLookup = new UserRoleDal();
                DataSet dsRole = objuserRoleLookup.GetUserRole(acbUserRole.SearchTextBox.Text);
                radgrdUserRole.DataSource = dsRole;
                radgrdUserRole.DataBind();
                ViewState[vsUserRole] = dsRole;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbUserRole_EditClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                }
                if (htItemIndex != null)
                {
                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        radgrdUserRole.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdUserRole.Rebind();
                    acbUserRole.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbUserRole.SaveButton.CommandName = "Update";
                    acbUserRole.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbUserRole_RefreshClick(object sender, EventArgs e)
        {
            acbUserRole.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbUserRole.SearchTextBox.Text = String.Empty;
            BindGrid();


        }

        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdUserRole_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdUserRole.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdUserRole.Items)
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
        protected void radgrdUserRole_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdUserRole.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdUserRole.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdUserRole_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsUserRole] != null)
                radgrdUserRole.DataSource = ViewState[vsUserRole];
        }

       
        #endregion

        #region GridCheckBox event

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
                            radgrdUserRole.Items[htItemIndex[i].ToString()].Edit = false;
                            htItemIndex.Remove(i);

                            break;
                        }
                    }
                }
            }
            ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
        }

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdUserRole.ExportSettings.ExportOnlyData = true;
            radgrdUserRole.ExportSettings.OpenInNewWindow = true;
            radgrdUserRole.ExportSettings.IgnorePaging = true;

            string filename = "UserRole_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdUserRole.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdUserRole.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdUserRole.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdUserRole.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdUserRole.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbUserRole_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbUserRole_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbUserRole_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbUserRole_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}

