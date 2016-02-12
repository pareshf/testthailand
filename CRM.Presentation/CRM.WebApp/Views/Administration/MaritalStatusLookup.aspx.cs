#region Program Information
/**********************************************************************************************************************************************
 Class Name           :MaritalStatusUp
 Class Description    : Implementation logic for save, edit, delete and find operation for MaritalStatus details.
 Author               : Chirag.
 Created Date         : Mar 15, 2010
***********************************************************************************************************************************************/
#endregion

#region Imports assemblies
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
#endregion

namespace CRM.WebApp.Views.Administration
{
    public partial class MaritalStatusLookup : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        MaritalStatusLookupDal objMaritalStatusLookUp = null;
        public const String vsMaritalStatus = "MaritalStatus";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Marital Status");
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }

            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
                Session["currentevent"] = "Marital Status";
            }

            acbMaritalStatus.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdMaritalStatus.ClientID));
            acbMaritalStatus.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdMaritalStatus.ClientID));
        }

        #endregion

        #region Actionbar Events


        /// <summary>
        /// Action bar's new button event which open view to insert new MaritalStatus.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbMaritalStatus_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtMaritalStatusName.Focus();
                acbMaritalStatus.EditableMode = true;
                //acbMaritalStatus.SaveNewButton.Visible = true;
                acbMaritalStatus.SaveButton.CommandName = "Save";
                pnlAddNewMode.Visible = true;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }



        }

        /// <summary>
        /// Action bar's edit button event which open multiple rows in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbMaritalStatus_EditClick(object sender, EventArgs e)
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
                        radgrdMaritalStatus.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdMaritalStatus.Rebind();
                    acbMaritalStatus.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbMaritalStatus.SaveNewButton.Visible = false;
                    acbMaritalStatus.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        /// <summary>
        /// Action bar's delete button event which delete records physically from database.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbMaritalStatus_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder maritalStatusId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblMaritalStatusId = (Label)radgrdMaritalStatus.Items[i].FindControl("lblGrdMaritalStatusIdItem");
                        if (lblMaritalStatusId != null)
                        {
                            maritalStatusId.Append(lblMaritalStatusId.Text + ",");
                        }
                    }
                }
                objMaritalStatusLookUp = new MaritalStatusLookupDal();
                String maritalId = maritalStatusId.ToString().TrimEnd(',');
                result = objMaritalStatusLookUp.DeleteMaritalStatus(maritalId);

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

        /// <summary>
        /// Action bar's save button event which saves grid data by xml.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void acbMaritalStatus_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objMaritalStatusLookUp = new MaritalStatusLookupDal();
                switch (acbMaritalStatus.SaveButton.CommandName)
                {
                    case "Save":
                        SaveMaritalStatus();
                        pnlAddNewMode.Visible = false;
                        acbMaritalStatus.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdMaritalStatus);
                        result = objMaritalStatusLookUp.UpdateMaritalStatus(xmlData);
                        if (result == 1)
                        {
                            acbMaritalStatus.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdMaritalStatus.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        /// 
        protected void acbMaritalStatus_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdMaritalStatus.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdMaritalStatus.Rebind();
                }
                acbMaritalStatus.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbMaritalStatus_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objMaritalStatusLookUp = new MaritalStatusLookupDal();
                DataSet dsMaritalStatus = objMaritalStatusLookUp.GetMaritalStatus(acbMaritalStatus.SearchTextBox.Text);
                radgrdMaritalStatus.DataSource = dsMaritalStatus;
                radgrdMaritalStatus.DataBind();
                ViewState[vsMaritalStatus] = dsMaritalStatus;
                Reset();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbMaritalStatus_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtMaritalStatusName.Focus();
                SaveMaritalStatus();
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

        #region Grid events

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdMaritalStatus_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdMaritalStatus.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdMaritalStatus.Items)
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
        protected void radgrdMaritalStatus_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdMaritalStatus.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdMaritalStatus.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdMaritalStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsMaritalStatus] != null)
                radgrdMaritalStatus.DataSource = ViewState[vsMaritalStatus];
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
            try
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
                                radgrdMaritalStatus.Items[htItemIndex[i].ToString()].Edit = false;
                                htItemIndex.Remove(i);

                                break;
                            }
                        }
                    }
                }
                ViewState.Add(PageConstants.vsItemIndexes, htItemIndex);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }
        #endregion

        #endregion

        #region Methods

        #region Bind Grid
        /// <summary>
        /// Bind objMaritalStatusLookUp grid
        /// </summary>
        private void BindGrid()
        {
            objMaritalStatusLookUp = new MaritalStatusLookupDal();
            DataSet dsMaritalStatus = objMaritalStatusLookUp.GetMaritalStatus("");
            radgrdMaritalStatus.DataSource = dsMaritalStatus;
            radgrdMaritalStatus.DataBind();
            ViewState[vsMaritalStatus] = dsMaritalStatus;
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
            string xmlHeaderNodeStructure = "<MaritalStatus MARITAL_STATUS_ID=\"{0}\" MARITAL_STATUS_NAME=\"{1}\" USER_ID=\"{2}\"></MaritalStatus>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int MaritalStatusId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdMaritalStatusIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdMaritalStatusIdEdit");
                        TextBox txtmaritalStatus = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdMaritalStatusName");
                        MaritalStatusId = int.Parse(lblGrdMaritalStatusIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, MaritalStatusId, txtmaritalStatus.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveMaritalStatus()
        {
            try
            {
                int result = 0;
                objMaritalStatusLookUp = new MaritalStatusLookupDal();
                LookupBDto objLookupBDto = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtMaritalStatusName.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objMaritalStatusLookUp.InsertMaritalStatus(objLookUpBDto);
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
            txtMaritalStatusName.Text = string.Empty;

        }
        #endregion
        


        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdMaritalStatus.ExportSettings.ExportOnlyData = true;
            radgrdMaritalStatus.ExportSettings.OpenInNewWindow = true;
            radgrdMaritalStatus.ExportSettings.IgnorePaging = true;

            string filename = "MaritalStatus" + System.DateTime.Now.ToString("ddmmyyyy") + "_" + System.DateTime.Now.Hour.ToString();
            radgrdMaritalStatus.ExportSettings.FileName = filename;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdMaritalStatus.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdMaritalStatus.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdMaritalStatus.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdMaritalStatus.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbMaritalStatus_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbMaritalStatus_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbMaritalStatus_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbMaritalStatus_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion

    }
}
