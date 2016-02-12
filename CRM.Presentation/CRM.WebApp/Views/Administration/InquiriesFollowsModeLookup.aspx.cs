#region Program Information
/**********************************************************************************************************************************************
 Class Name           : InquiriesFollowsModeLookup
 Class Description    : Implementation logic for save, edit, delete and find operation for InquiriesFollowsMode details.
 Author               : Chirag.
 Created Date         : Mar 18, 2010
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
    public partial class InquiriesFollowsModeLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        InquiriesFollowsModeLookupDal objInquiriesFollowsModeLookup = null;
        public const String vsInqFollowsMode = "InquiriesFollowsMode";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Inquiry Followup");
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
                Session["currentevent"] = "Inquiry Followup";
            }

            acbInquiriesFollowsMode.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdInquiriesFollowsMode.ClientID));
            acbInquiriesFollowsMode.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdInquiriesFollowsMode.ClientID));
        }

        #endregion

        #region Actionbar Events

        protected void acbInquiriesFollowsMode_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtInquiriesFollowsMode.Focus();
                acbInquiriesFollowsMode.EditableMode = true;
                acbInquiriesFollowsMode.SaveNewButton.Visible = true;
                acbInquiriesFollowsMode.SaveButton.CommandName = "Save";
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

        protected void acbInquiriesFollowsMode_EditClick(object sender, EventArgs e)
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
                        radgrdInquiriesFollowsMode.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdInquiriesFollowsMode.Rebind();
                    acbInquiriesFollowsMode.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbInquiriesFollowsMode.SaveNewButton.Visible = false;
                    acbInquiriesFollowsMode.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbInquiriesFollowsMode_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder inqFollowsModeId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblinqFollowsModeId = (Label)radgrdInquiriesFollowsMode.Items[i].FindControl("lblGrdInqFollowsModeItem");
                        if (lblinqFollowsModeId != null)
                        {
                            inqFollowsModeId.Append(lblinqFollowsModeId.Text + ",");
                        }
                    }
                }
                objInquiriesFollowsModeLookup = new InquiriesFollowsModeLookupDal();
                String inqFollowModeId = inqFollowsModeId.ToString().TrimEnd(',');
                result = objInquiriesFollowsModeLookup.DeleteInquiriesFollowsMode(inqFollowModeId);

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

        protected void acbInquiriesFollowsMode_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objInquiriesFollowsModeLookup = new InquiriesFollowsModeLookupDal();
                switch (acbInquiriesFollowsMode.SaveButton.CommandName)
                {
                    case "Save":
                        SaveInqFollowMode();
                        pnlAddNewMode.Visible = false;
                        acbInquiriesFollowsMode.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdInquiriesFollowsMode);
                        result = objInquiriesFollowsModeLookup.UpdateInquiriesFollowsMode(xmlData);
                        if (result == 1)
                        {
                            acbInquiriesFollowsMode.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdInquiriesFollowsMode.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

        protected void acbInquiriesFollowsMode_CancelClick(object sender, EventArgs e)
        {
            try
            {

                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdInquiriesFollowsMode.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdInquiriesFollowsMode.Rebind();
                }
                acbInquiriesFollowsMode.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbInquiriesFollowsMode_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objInquiriesFollowsModeLookup = new InquiriesFollowsModeLookupDal();
                DataSet dsInqFollowsMode = objInquiriesFollowsModeLookup.GetInquiriesFollowsMode(acbInquiriesFollowsMode.SearchTextBox.Text);
                radgrdInquiriesFollowsMode.DataSource = dsInqFollowsMode;
                radgrdInquiriesFollowsMode.DataBind();
                ViewState[vsInqFollowsMode] = dsInqFollowsMode;
                Reset();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbacbInquiriesFollowsMode_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtInquiriesFollowsMode.Focus();
                SaveInqFollowMode();
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbacbInquiriesFollowsMode_RefreshClick(object sender, EventArgs e)
        {
            acbInquiriesFollowsMode.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbInquiriesFollowsMode.SearchTextBox.Text = string.Empty;
            BindGrid();
        }



        #endregion

        #region Grid events

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdInquiriesFollowsMode_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdInquiriesFollowsMode.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdInquiriesFollowsMode.Items)
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
        protected void radgrdInquiriesFollowsMode_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdInquiriesFollowsMode.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdInquiriesFollowsMode.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdInquiriesFollowsMode_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsInqFollowsMode] != null)
                radgrdInquiriesFollowsMode.DataSource = ViewState[vsInqFollowsMode];
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
                                radgrdInquiriesFollowsMode.Items[htItemIndex[i].ToString()].Edit = false;
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
        /// Bind InquiriesFollowsModeLookup grid
        /// </summary>
        private void BindGrid()
        {
            objInquiriesFollowsModeLookup = new InquiriesFollowsModeLookupDal();
            DataSet dsInqFollowsMode = objInquiriesFollowsModeLookup.GetInquiriesFollowsMode("");
            radgrdInquiriesFollowsMode.DataSource = dsInqFollowsMode;
            radgrdInquiriesFollowsMode.DataBind();
            ViewState[vsInqFollowsMode] = dsInqFollowsMode;
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
            string xmlHeaderNodeStructure = "<InqFollowsMode IF_ID=\"{0}\" IF_DESC=\"{1}\" USER_ID=\"{2}\"></InqFollowsMode>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int InqFollowsModeId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdInqFollowsModeIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdInqFollowsModeEdit");
                        TextBox txtInqFollowsModeId = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdInqFollowsModeDescEdit");
                        InqFollowsModeId = int.Parse(lblGrdInqFollowsModeIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, InqFollowsModeId, txtInqFollowsModeId.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveInqFollowMode()
        {
            try
            {
                int result = 0;
                objInquiriesFollowsModeLookup = new InquiriesFollowsModeLookupDal();
                LookupBDto objReligion = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtInquiriesFollowsMode.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objInquiriesFollowsModeLookup.InsertInquiriesFollowsMode(objLookUpBDto);
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
            txtInquiriesFollowsMode.Text = string.Empty;

        }
        #endregion

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdInquiriesFollowsMode.ExportSettings.ExportOnlyData = true;
            radgrdInquiriesFollowsMode.ExportSettings.OpenInNewWindow = true;
            radgrdInquiriesFollowsMode.ExportSettings.IgnorePaging = true;

            string filename = "InquiriesFollowup" + System.DateTime.Now.ToString("ddmmyyyy") + "_" + System.DateTime.Now.Hour.ToString();
            radgrdInquiriesFollowsMode.ExportSettings.FileName = filename;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdInquiriesFollowsMode.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdInquiriesFollowsMode.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdInquiriesFollowsMode.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdInquiriesFollowsMode.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbacbInquiriesFollowsMode_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbacbInquiriesFollowsMode_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbacbInquiriesFollowsMode_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbacbInquiriesFollowsMode_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
