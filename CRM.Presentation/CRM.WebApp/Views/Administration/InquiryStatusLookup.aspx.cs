#region Program Information
/**********************************************************************************************************************************************
 Class Name           : InquiryStatusLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for Status details.
 Author               : Chirag.
 Created Date         : Mar 26, 2010
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
    public partial class InquiryStatusLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        InquiryStatusLookupDal objInquiryStatusLookup = null;
        public const String vsInquiryStatus = "InquiryStatus";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Inquiry Status");
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
                Session["currentevent"] = "Inquiry Status";
            }

            acbInquiryStatus.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdInquiryStatus.ClientID));
            acbInquiryStatus.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdInquiryStatus.ClientID));
        }

        #endregion

        #region Actionbar Events

        protected void acbInquiryStatus_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtInquiryStatus.Focus();
                acbInquiryStatus.EditableMode = true;
                acbInquiryStatus.SaveNewButton.Visible = true;
                acbInquiryStatus.SaveButton.CommandName = "Save";
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

        protected void acbInquiryStatus_EditClick(object sender, EventArgs e)
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
                        radgrdInquiryStatus.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdInquiryStatus.Rebind();
                    acbInquiryStatus.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbInquiryStatus.SaveNewButton.Visible = false;
                    acbInquiryStatus.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbInquiryStatus_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder InquiryStatusId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblInquiryStatusId = (Label)radgrdInquiryStatus.Items[i].FindControl("lblGrdInquiryStatusIdItem");
                        if (lblInquiryStatusId != null)
                        {
                            InquiryStatusId.Append(lblInquiryStatusId.Text + ",");
                        }
                    }
                }
                objInquiryStatusLookup = new InquiryStatusLookupDal();
                String empStatusId = InquiryStatusId.ToString().TrimEnd(',');
                result = objInquiryStatusLookup.DeleteInquiryStatus(empStatusId);

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

        protected void acbInquiryStatus_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objInquiryStatusLookup = new InquiryStatusLookupDal();
                switch (acbInquiryStatus.SaveButton.CommandName)
                {
                    case "Save":
                        SaveInquiryStatus();
                        pnlAddNewMode.Visible = false;
                        acbInquiryStatus.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdInquiryStatus);
                        result = objInquiryStatusLookup.UpdateInquiryStatus(xmlData);
                        if (result == 1)
                        {
                            acbInquiryStatus.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdInquiryStatus.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

        protected void acbInquiryStatus_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdInquiryStatus.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdInquiryStatus.Rebind();
                }
                acbInquiryStatus.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbInquiryStatus_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objInquiryStatusLookup = new InquiryStatusLookupDal();
                DataSet dsInquiryStatus = objInquiryStatusLookup.GetInquiryStatus(acbInquiryStatus.SearchTextBox.Text);
                radgrdInquiryStatus.DataSource = dsInquiryStatus;
                radgrdInquiryStatus.DataBind();
                ViewState[vsInquiryStatus] = dsInquiryStatus;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbInquiryStatus_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtInquiryStatus.Focus();
                SaveInquiryStatus();
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbInquiryStatus_RefreshClick(object sender, EventArgs e)
        {
            acbInquiryStatus.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbInquiryStatus.SearchTextBox.Text = string.Empty;
            BindGrid();
        }


        #endregion

        #region Grid events

        protected void radgrdInquiryStatus_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdInquiryStatus.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdInquiryStatus.Items)
                {
                    CheckBox chkItem = (CheckBox)item.FindControl("chkItemWrt");
                    if (chkItem != null)
                        chkItem.Visible = false;
                }
            }
        }

        protected void radgrdInquiryStatus_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdInquiryStatus.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdInquiryStatus.ClientID, chkBox.ClientID, e.Item.ItemIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void radgrdInquiryStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsInquiryStatus] != null)
                radgrdInquiryStatus.DataSource = ViewState[vsInquiryStatus];
        }
        #endregion

        #region Grid Checkbox Events
        
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
                                radgrdInquiryStatus.Items[htItemIndex[i].ToString()].Edit = false;
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
        private void BindGrid()
        {
            objInquiryStatusLookup = new InquiryStatusLookupDal();
            DataSet dsInquiryStatus = objInquiryStatusLookup.GetInquiryStatus("");
            radgrdInquiryStatus.DataSource = dsInquiryStatus;
            radgrdInquiryStatus.DataBind();
            ViewState[vsInquiryStatus] = dsInquiryStatus;
        }
        #endregion

        #region Generate Xml String
       
        private String GenerateXmlString(RadGrid grid)
        {
            string xmlRootStart = "<{0}>";
            string xmlRootEnd = "</{0}>";
            string xmlHeaderRootValue = "Node";
            string xmlHeaderNodeStructure = "<InquiryStatus INQUIRY_STATUS_ID=\"{0}\" INQUIRY_STATUS_NAME=\"{1}\" USER_ID=\"{2}\"></InquiryStatus>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int InquiryStatusId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdInqStatusIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdInquiryStatusIdEdit");
                        TextBox txtInqStatus = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdInquiryStatus");
                        InquiryStatusId = int.Parse(lblGrdInqStatusIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, InquiryStatusId, txtInqStatus.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveInquiryStatus()
        {
            try
            {
                int result = 0;
                objInquiryStatusLookup = new InquiryStatusLookupDal();
                LookupBDto objStatus = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName =txtInquiryStatus.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objInquiryStatusLookup.InsertInquiryStatus(objLookUpBDto);
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
            txtInquiryStatus.Text = string.Empty;

        }

        #endregion

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdInquiryStatus.ExportSettings.ExportOnlyData = true;
            radgrdInquiryStatus.ExportSettings.OpenInNewWindow = true;
            radgrdInquiryStatus.ExportSettings.IgnorePaging = true;

            string filename = "InquiryStatus_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdInquiryStatus.ExportSettings.FileName = filename;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdInquiryStatus.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdInquiryStatus.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdInquiryStatus.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdInquiryStatus.MasterTableView.ExportToCSV();
                        break;
                    }
            }

        }

        protected void acbInquiryStatus_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbInquiryStatus_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbInquiryStatus_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbInquiryStatus_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
        
    }
}
