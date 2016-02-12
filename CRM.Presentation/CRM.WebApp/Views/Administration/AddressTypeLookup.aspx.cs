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
using CRM.WebApp.Views.Shared.Controls.Navigation;

namespace CRM.WebApp.Views.Administration 
{
    public partial class AddressTypeLookup : System.Web.UI.Page
    {

        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        AddressTypeLookupDal objAddressTypeLookup = null;
        public const String vsAddress = "Address";
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
                Session["currentevent"] = "Address Type";
            }
            hdnEditableMode.Value = "false";
            acbAddress.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdAddress.ClientID));
            acbAddress.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdAddress.ClientID));

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
        /// Bind Address grid
        /// </summary>

        private void BindGrid()
        {
            objAddressTypeLookup = new AddressTypeLookupDal();
            DataSet dsAddress = objAddressTypeLookup.GetAddressType("");
            radgrdAddress.DataSource = dsAddress;
            radgrdAddress.DataBind();
            ViewState[vsAddress] = dsAddress;

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
            string xmlHeaderNodeStructure = "<Address ADDRESS_TYPE_ID=\"{0}\" ADDRESS_TYPE_NAME=\"{1}\" USER_ID=\"{2}\"></Address>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int AddressId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdAddressId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdAddressIdEdit");
                        TextBox txtgrdAddressType = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdAddressType");
                        AddressId = int.Parse(lblgrdAddressId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, AddressId, txtgrdAddressType.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
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
                objLookupBDto.LookupName = txtAddressType.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objAddressTypeLookup.InsertAddressType(objLookupBDto);
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

            txtAddressType.Text = String.Empty;

       }
        #endregion

        #endregion

        #region Actionbar Events

        protected void acbAddress_NewClick(object sender, EventArgs e)
        {
            acbAddress.EditableMode = true;
            acbAddress.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtAddressType.Focus();
            radgrdAddress.Rebind();
            Reset();
        }

        protected void acbAddress_SaveClick(object sender, EventArgs e)
        {

            try
            {
                int result = 0;
                objAddressTypeLookup = new AddressTypeLookupDal();
                switch (acbAddress.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbAddress.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdAddress);
                        result = objAddressTypeLookup.UpdateAddress(xmlData);
                        if (result == 1)
                        {
                            acbAddress.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdAddress.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

        protected void acbAddress_SaveNewClick(object sender, EventArgs e)
        {
            txtAddressType.Focus();
            Save();
            Reset();
        }

        protected void acbAddress_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdAddress.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdAddress.Rebind();

            }

            acbAddress.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbAddress_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder AddressId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblAddressId = (Label)radgrdAddress.Items[i].FindControl("lblgrdAddressIdItem");
                        if (lblAddressId != null)
                        {
                            AddressId.Append(lblAddressId.Text + ",");
                        }
                    }
                }

                objAddressTypeLookup = new AddressTypeLookupDal();
                String AddId = AddressId.ToString().TrimEnd(',');
                result = objAddressTypeLookup.DeleteAddressType(AddId);


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

        protected void acbAddress_SearchClick(object sender, EventArgs e)
        {

            try
            {

                objAddressTypeLookup = new AddressTypeLookupDal();
                DataSet dsAddress = objAddressTypeLookup.GetAddressType(acbAddress.SearchTextBox.Text);
                radgrdAddress.DataSource = dsAddress;
                radgrdAddress.DataBind();
                ViewState[vsAddress] = dsAddress;



            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAddress_EditClick(object sender, EventArgs e)
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
                        radgrdAddress.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;

                    }
                    bisEdit = true;
                    radgrdAddress.Rebind();
                    acbAddress.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbAddress.SaveNewButton.Visible = false;
                    acbAddress.SaveButton.CommandName = "Update";


                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbAddress_RefreshClick(object sender, EventArgs e)
        {
            acbAddress.DefaultMode = true;
            pnlAddNewMode.Visible = false;

            acbAddress.SearchTextBox.Text = String.Empty;
            BindGrid();
        }


        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdAddress_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdAddress.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdAddress.Items)
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
        protected void radgrdAddress_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdAddress.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdAddress.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdAddress_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsAddress] != null)
                radgrdAddress.DataSource = ViewState[vsAddress];
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
                            radgrdAddress.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdAddress.ExportSettings.ExportOnlyData = true;
            radgrdAddress.ExportSettings.OpenInNewWindow = true;
            radgrdAddress.ExportSettings.IgnorePaging = true;

            string filename = "Address_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdAddress.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdAddress.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdAddress.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdAddress.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdAddress.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbAddress_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbAddress_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbAddress_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbAddress_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion



    }
}
