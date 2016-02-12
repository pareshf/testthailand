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
    public partial class CustomerCodeLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        CustomerCodeLookupDal objCustomerCodeLookup = null;
        public const String vsCustCode = "CustCode";
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
                Session["currentevent"] = "Customers Code";
            }
            hdnEditableMode.Value = "false";
            acbCustCode.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdcode.ClientID));
            acbCustCode.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdcode.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Customers Code");
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
        /// Bind Cust Code grid
        /// </summary>

        private void BindGrid()
        {

            objCustomerCodeLookup = new CustomerCodeLookupDal();
            DataSet dsCustCode = objCustomerCodeLookup.GetCustomerCode("");
            radgrdcode.DataSource = dsCustCode;
            radgrdcode.DataBind();
            ViewState[vsCustCode] = dsCustCode;

        }

        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objCustomerCodeLookup = new CustomerCodeLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtCodeName.Text;
                objLookupBDto.Description = txtCodeDesc.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objCustomerCodeLookup.InsertCustomerCode(objLookupBDto);
                if (result >= 1)
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                    Master.MessageCssClass = "successMessage";
                    bisEdit = false;
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
            txtCodeName.Text = String.Empty;
            txtCodeDesc.Text = String.Empty;
            acbCustCode.SearchTextBox.Text = String.Empty;
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
            string xmlHeaderNodeStructure = "<CustCode CUST_CODE_ID=\"{0}\" CUST_CODE_NAME=\"{1}\" CUST_CODE_DESC=\"{2}\" USER_ID=\"{3}\"></CustCode>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int CustId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdCodeId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdCodeIdEdit");
                        TextBox txtgrdCodeName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdCodeName");
                        TextBox txtgrdCodeDesc = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdCodeDesc");
                        CustId = int.Parse(lblgrdCodeId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, CustId, txtgrdCodeName.Text, txtgrdCodeDesc.Text, objAuthorizationBDto.UserProfile.UserId);
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

        protected void acbCustCode_NewClick(object sender, EventArgs e)
        {
            acbCustCode.EditableMode = true;
            acbCustCode.SaveNewButton.Visible = true;
            acbCustCode.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtCodeName.Focus();
            radgrdcode.Rebind();
            Reset();
        }

        protected void acbCustCode_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objCustomerCodeLookup = new CustomerCodeLookupDal();
                switch (acbCustCode.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbCustCode.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdcode);
                        result = objCustomerCodeLookup.UpdateCustomerCode(xmlData);
                        if (result == 1)
                        {
                            acbCustCode.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdcode.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objCustomerCodeLookup = new CustomerCodeLookupDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbCustCode_SaveNewClick(object sender, EventArgs e)
        {
            txtCodeName.Focus();

            Save();
            Reset();
        }

        protected void acbCustCode_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdcode.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdcode.Rebind();

            }
            acbCustCode.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbCustCode_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder CodeId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblCodeId = (Label)radgrdcode.Items[i].FindControl("lblgrdCodeIdItem");

                        if (lblCodeId != null)
                        {
                            CodeId.Append(lblCodeId.Text + ",");
                        }
                    }
                }

                objCustomerCodeLookup = new CustomerCodeLookupDal();
                String CustCodeId = CodeId.ToString().TrimEnd(',');
                result = objCustomerCodeLookup.DeleteCustomerCode(CustCodeId);


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

        protected void acbCustCode_SearchClick(object sender, EventArgs e)
        {
            try
            {
                acbCustCode.SearchTextBox.Focus();
                objCustomerCodeLookup = new CustomerCodeLookupDal();
                DataSet dsCode = objCustomerCodeLookup.GetCustomerCode(acbCustCode.SearchTextBox.Text);
                radgrdcode.DataSource = dsCode;
                radgrdcode.DataBind();
                ViewState[vsCustCode] = dsCode;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbCustCode_EditClick(object sender, EventArgs e)
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
                        radgrdcode.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdcode.Rebind();
                    acbCustCode.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbCustCode.SaveButton.CommandName = "Update";
                    acbCustCode.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbCustCode_RefreshClick(object sender, EventArgs e)
        {
            acbCustCode.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbCustCode.SearchTextBox.Text = String.Empty;
            BindGrid();
        }


        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdcode_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdcode.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdcode.Items)
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
        protected void radgrdcode_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdcode.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdcode.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdcode_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsCustCode] != null)
                radgrdcode.DataSource = ViewState[vsCustCode];
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
                            radgrdcode.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdcode.ExportSettings.ExportOnlyData = true;
            radgrdcode.ExportSettings.OpenInNewWindow = true;
            radgrdcode.ExportSettings.IgnorePaging = true;

            string filename = "Cust_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdcode.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdcode.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdcode.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdcode.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdcode.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbCustCode_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbCustCode_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbCustCode_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbCustCode_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion


    }
}
