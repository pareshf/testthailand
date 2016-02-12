#region Program Information
/**********************************************************************************************************************************************
 Class Name           : SecurityQuestionLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for Security Question details.
 Author               : Chirag.
 Created Date         : Mar 24, 2010
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
    public partial class SecurityQuestionLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        SecurityQuestionLookupDal objSecurityQuestionLookup = null;
        public const String vsSecuQues = "SecurityQuestion";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Security Question");
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
                Session["currentevent"] = "Security Question";
            }

            acbSecuQuestion.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdSecuQuestion.ClientID));
            acbSecuQuestion.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdSecuQuestion.ClientID));
        }

        #endregion

        #region Actionbar Events


        protected void acbSecuQuestion_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtSecuQuestion.Focus();
                acbSecuQuestion.EditableMode = true;
                acbSecuQuestion.SaveNewButton.Visible = true;
                acbSecuQuestion.SaveButton.CommandName = "Save";
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

        protected void acbSecuQuestion_EditClick(object sender, EventArgs e)
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
                        radgrdSecuQuestion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdSecuQuestion.Rebind();
                    acbSecuQuestion.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbSecuQuestion.SaveNewButton.Visible = false;
                    acbSecuQuestion.SaveButton.CommandName = "Update";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbSecuQuestion_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder secuQuesId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblsecuQuesId = (Label)radgrdSecuQuestion.Items[i].FindControl("lblGrdSecuQueIdItem");
                        if (lblsecuQuesId != null)
                        {
                            secuQuesId.Append(lblsecuQuesId.Text + ",");
                        }
                    }
                }
                objSecurityQuestionLookup = new SecurityQuestionLookupDal();
                String secuQuId = secuQuesId.ToString().TrimEnd(',');
                result = objSecurityQuestionLookup.DeleteSecurityQuestion(secuQuId);

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

        protected void acbSecuQuestion_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objSecurityQuestionLookup = new SecurityQuestionLookupDal();
                switch (acbSecuQuestion.SaveButton.CommandName)
                {
                    case "Save":
                        SaveSecurityQuestion();
                        pnlAddNewMode.Visible = false;
                        acbSecuQuestion.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdSecuQuestion);
                        result = objSecurityQuestionLookup.UpdateSecurityQuestion(xmlData);
                        if (result == 1)
                        {
                            acbSecuQuestion.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdSecuQuestion.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }


            Reset();
        }

        protected void acbSecuQuestion_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdSecuQuestion.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdSecuQuestion.Rebind();
                }
                acbSecuQuestion.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbSecuQuestion_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objSecurityQuestionLookup = new SecurityQuestionLookupDal();
                DataSet dsSecuQue = objSecurityQuestionLookup.GetSecurityQuestion(acbSecuQuestion.SearchTextBox.Text);
                radgrdSecuQuestion.DataSource = dsSecuQue;
                radgrdSecuQuestion.DataBind();
                ViewState[vsSecuQues] = dsSecuQue;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbSecuQuestion_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtSecuQuestion.Focus();
                SaveSecurityQuestion();
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
        protected void radgrdSecuQuestion_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdSecuQuestion.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdSecuQuestion.Items)
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
        protected void radgrdSecuQuestion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdSecuQuestion.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdSecuQuestion.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdSecuQuestion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsSecuQues] != null)
                radgrdSecuQuestion.DataSource = ViewState[vsSecuQues];
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
                                radgrdSecuQuestion.Items[htItemIndex[i].ToString()].Edit = false;
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
            objSecurityQuestionLookup = new SecurityQuestionLookupDal();
            DataSet dsSecuQues = objSecurityQuestionLookup.GetSecurityQuestion("");
            radgrdSecuQuestion.DataSource = dsSecuQues;
            radgrdSecuQuestion.DataBind();
            ViewState[vsSecuQues] = dsSecuQues;
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
            string xmlHeaderNodeStructure = "<SecuQuestion SECURITY_QUESTION_ID=\"{0}\" SECURITY_QUESTION_DESC=\"{1}\" USER_ID=\"{2}\"></SecuQuestion>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int SecuQuestionId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdSecuQuestionIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdSecuQueIdEdit");
                        TextBox txtsecuQuestionId = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdSecuQueDescEdit");
                        SecuQuestionId = int.Parse(lblGrdSecuQuestionIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, SecuQuestionId, txtsecuQuestionId.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveSecurityQuestion()
        {
            try
            {
                int result = 0;
                objSecurityQuestionLookup = new SecurityQuestionLookupDal();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtSecuQuestion.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objSecurityQuestionLookup.InsertSecurityQuestion(objLookUpBDto);
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
            txtSecuQuestion.Text = string.Empty;

        }

        #endregion
     

        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdSecuQuestion.ExportSettings.ExportOnlyData = true;
            radgrdSecuQuestion.ExportSettings.OpenInNewWindow = true;
            radgrdSecuQuestion.ExportSettings.IgnorePaging = true;

            string filename = "SecurityQuestion" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdSecuQuestion.ExportSettings.FileName = filename;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdSecuQuestion.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdSecuQuestion.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdSecuQuestion.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdSecuQuestion.MasterTableView.ExportToCSV();
                        break;
                    }
            }

        }

        protected void acbSecuQuestion_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbSecuQuestion_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbSecuQuestion_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbSecuQuestion_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
