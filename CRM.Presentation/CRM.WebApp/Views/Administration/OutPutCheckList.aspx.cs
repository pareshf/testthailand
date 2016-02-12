using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using CRM.Core.Constants;
using CRM.DataAccess;
using CRM.DataAccess.AdministrationDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Administration
{
	public partial class OutPutCheckList : System.Web.UI.Page
	{

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        AddressTypeLookupDal objAddressTypeLookup = null;
        public const String vsOutputCheckList = "OutputCheckList";
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
            }
            hdnEditableMode.Value = "false";
            acbOutputCheckList.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdOutputCheckList.ClientID));
            acbOutputCheckList.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdOutputCheckList.ClientID));
        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Address Type");
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
        /// Bind Output CheckList grid
        /// </summary>
        private void BindGrid()
        {
            objAddressTypeLookup = new AddressTypeLookupDal();
            DataSet dsOutputCheckList = objAddressTypeLookup.GetOutputCheckList("");
            radgrdOutputCheckList.DataSource = dsOutputCheckList;
            radgrdOutputCheckList.DataBind();
            ViewState[vsOutputCheckList] = dsOutputCheckList;
        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objAddressTypeLookup = new AddressTypeLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtOutputCheckListName.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objAddressTypeLookup.InsertOutputCheckList(objLookupBDto);
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

        #region Update
        private void Update()
        {
            try
            {
                int result = 0;
                objAddressTypeLookup = new AddressTypeLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtOutputCheckListName.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                objLookupBDto.LookupId = 0;
                string CityName = txtOutputCheckListName.Text;
                Int32 modifyBy = Convert.ToInt32(objAuthorizationBDto.UserProfile);
                result = objAddressTypeLookup.UpdateOutputCheckList(objLookupBDto);
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

            txtOutputCheckListName.Text = String.Empty;

        }
        #endregion

        #endregion

        #region Actionbar Events

        protected void acbOutputCheckList_NewClick(object sender, EventArgs e)
        {
            acbOutputCheckList.EditableMode = true;
            acbOutputCheckList.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtOutputCheckListName.Focus();
            radgrdOutputCheckList.Rebind();
            Reset();
        }

        protected void acbOutputCheckList_SaveClick(object sender, EventArgs e)
        {

            try
            {
                int result = 0;
                objAddressTypeLookup = new AddressTypeLookupDal();
                switch (acbOutputCheckList.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbOutputCheckList.EditableMode = false;

                        break;

                    case "Update":
                        try
                        {
                            objAddressTypeLookup = new AddressTypeLookupDal();
                            objLookupBDto = new LookupBDto();
                            if (ViewState[PageConstants.vsItemIndexes] != null)
                            {
                                htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                            }
                            int OutputCheckListId = 0;
                            for (int i = 0; i < htItemIndex.Count; i++)
                            {
                                objAuthorizationBDto = new AuthorizationBDto();
                                objLookupBDto = new LookupBDto();

                                Label lblgrdOutputCheckList = (Label)radgrdOutputCheckList.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdOutputCheckListEdit");
                                TextBox txtgrdOutputCheckList = (TextBox)radgrdOutputCheckList.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdOutputCheckList");
                                OutputCheckListId = int.Parse(lblgrdOutputCheckList.Text);
                                objAddressTypeLookup = new AddressTypeLookupDal();
                                objLookupBDto = new LookupBDto();
                                objLookupBDto.LookupName = txtgrdOutputCheckList.Text;
                                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                                objLookupBDto.LookupId = OutputCheckListId;
                                result = objAddressTypeLookup.UpdateOutputCheckList(objLookupBDto);
                            }
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
                        catch (Exception ex) { }

                        result = objAddressTypeLookup.UpdateOutputCheckList(objLookupBDto);
                        if (result == 1)
                        {
                            acbOutputCheckList.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdOutputCheckList.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objAddressTypeLookup = new AddressTypeLookupDal();
            }

            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbOutputCheckList_SaveNewClick(object sender, EventArgs e)
        {
            txtOutputCheckListName.Focus();
            Save();
            Reset();
        }

        protected void acbOutputCheckList_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdOutputCheckList.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdOutputCheckList.Rebind();
            }

            acbOutputCheckList.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbOutputCheckList_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder OutputCheckListId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblOutputCheckListId = (Label)radgrdOutputCheckList.Items[i].FindControl("lblgrdOutputCheckListItem");
                        if (lblOutputCheckListId != null)
                        {
                            OutputCheckListId.Append(lblOutputCheckListId.Text + ",");
                        }
                    }
                }

                objAddressTypeLookup = new AddressTypeLookupDal();
                String AddId = OutputCheckListId.ToString().TrimEnd(',');
                result = objAddressTypeLookup.DeleteOutputCheckList(AddId);

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

        protected void acbOutputCheckList_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objAddressTypeLookup = new AddressTypeLookupDal();
                DataSet dsOutputCheckList = objAddressTypeLookup.GetOutputCheckList(acbOutputCheckList.SearchTextBox.Text);
                radgrdOutputCheckList.DataSource = dsOutputCheckList;
                radgrdOutputCheckList.DataBind();
                ViewState[vsOutputCheckList] = dsOutputCheckList;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbOutputCheckList_EditClick(object sender, EventArgs e)
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
                        radgrdOutputCheckList.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdOutputCheckList.Rebind();
                    acbOutputCheckList.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbOutputCheckList.SaveNewButton.Visible = false;
                    acbOutputCheckList.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbOutputCheckList_RefreshClick(object sender, EventArgs e)
        {
            acbOutputCheckList.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbOutputCheckList.SearchTextBox.Text = String.Empty;
            BindGrid();
        }


        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdOutputCheckList_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdOutputCheckList.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdOutputCheckList.Items)
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
        protected void radgrdOutputCheckList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdOutputCheckList.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdOutputCheckList.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdOutputCheckList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsOutputCheckList] != null)
                radgrdOutputCheckList.DataSource = ViewState[vsOutputCheckList];
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
                            radgrdOutputCheckList.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdOutputCheckList.ExportSettings.ExportOnlyData = true;
            radgrdOutputCheckList.ExportSettings.OpenInNewWindow = true;
            radgrdOutputCheckList.ExportSettings.IgnorePaging = true;

            string filename = "OutputCheckList_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdOutputCheckList.ExportSettings.FileName = filename;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdOutputCheckList.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdOutputCheckList.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdOutputCheckList.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdOutputCheckList.MasterTableView.ExportToCSV();
                        break;
                    }
            }

        }


        protected void acbOutputCheckList_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbOutputCheckList_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbOutputCheckList_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbOutputCheckList_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
