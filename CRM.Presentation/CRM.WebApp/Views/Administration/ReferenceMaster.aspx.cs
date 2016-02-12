#region Program Information
/**********************************************************************************************************************************************
 Class Name           : ReferenceLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for Status details.
 Author               : Chirag.
 Created Date         : April 02, 2010
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
    public partial class ReferenceMaster : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        ReferenceMasterDal objReferenceMaster = null;
        public const String vsReference = "Reference";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Reference");
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
                Session["currentevent"] = "Reference";
            }

            acbReference.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdReference.ClientID));
            acbReference.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdReference.ClientID));
        }

        #endregion

        #region Actionbar Events


        /// <summary>
        /// Action bar's new button event which open view to insert new Status.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbReference_NewClick(object sender, EventArgs e)
        {
            try
            {
                txtReference.Focus();
                acbReference.EditableMode = true;
                acbReference.SaveNewButton.Visible = true;
                acbReference.SaveButton.CommandName = "Save";
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
        protected void acbReference_EditClick(object sender, EventArgs e)
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
                        radgrdReference.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdReference.Rebind();
                    acbReference.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbReference.SaveNewButton.Visible = false;
                    acbReference.SaveButton.CommandName = "Update";
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
        protected void acbReference_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder ReferenceId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblReferenceId = (Label)radgrdReference.Items[i].FindControl("lblGrdReferenceIdItem");
                        if (lblReferenceId != null)
                        {
                            ReferenceId.Append(lblReferenceId.Text + ",");
                        }
                    }
                }
                objReferenceMaster = new ReferenceMasterDal();
                String ReferId = ReferenceId.ToString().TrimEnd(',');
                result = objReferenceMaster.DeleteReference(ReferId);

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
        protected void acbReference_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objReferenceMaster = new ReferenceMasterDal();
                switch (acbReference.SaveButton.CommandName)
                {
                    case "Save":
                        SaveReference();
                        pnlAddNewMode.Visible = false;
                        acbReference.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdReference);
                        result = objReferenceMaster.UpdateReference(xmlData);
                        if (result == 1)
                        {
                            acbReference.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdReference.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
        protected void acbReference_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdReference.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdReference.Rebind();
                }
                acbReference.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbReference_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objReferenceMaster = new ReferenceMasterDal();
                DataSet dsReference = objReferenceMaster.GetReference(acbReference.SearchTextBox.Text);
                radgrdReference.DataSource = dsReference;
                radgrdReference.DataBind();
                ViewState[vsReference] = dsReference;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbReference_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtReference.Focus();
                SaveReference();
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
        protected void radgrdReference_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdReference.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdReference.Items)
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
        protected void radgrdReference_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdReference.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdReference.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdReference_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsReference] != null)
                radgrdReference.DataSource = ViewState[vsReference];
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
                                radgrdReference.Items[htItemIndex[i].ToString()].Edit = false;
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
        /// Bind ReferenceLookUp grid
        /// </summary>
        private void BindGrid()
        {
            objReferenceMaster = new ReferenceMasterDal();
            DataSet dsReference = objReferenceMaster.GetReference("");
            radgrdReference.DataSource = dsReference;
            radgrdReference.DataBind();
            ViewState[vsReference] = dsReference;
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
            string xmlHeaderNodeStructure = "<Reference REFERENCE_ID=\"{0}\" REFERENCE_NAME=\"{1}\" USER_ID=\"{2}\"></Reference>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int ReferenceId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdReferenceIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdReferenceIdEdit");
                        TextBox txtReference = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdReference");
                        ReferenceId = int.Parse(lblGrdReferenceIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, ReferenceId, txtReference.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveReference()
        {
            try
            {
                int result = 0;
                objReferenceMaster = new ReferenceMasterDal();
                LookupBDto objStatus = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.LookupName = txtReference.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objReferenceMaster.InsertReference(objLookUpBDto);
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
            txtReference.Text = string.Empty;

        }

        #endregion


        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdReference.ExportSettings.ExportOnlyData = true;
            radgrdReference.ExportSettings.OpenInNewWindow = true;
            radgrdReference.ExportSettings.IgnorePaging = true;

            string filename = "Reference_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdReference.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdReference.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdReference.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdReference.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdReference.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbReference_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbReference_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbReference_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbReference_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
