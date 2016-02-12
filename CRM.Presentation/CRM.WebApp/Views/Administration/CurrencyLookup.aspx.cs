using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Core.Constants;
using CRM.DataAccess;
using CRM.DataAccess.AdministrationDAL;
using CRM.DataAccess.BookingDal.Hotel;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Telerik.Web.UI;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Data;


namespace CRM.WebApp.Views.Administration
{
    public partial class CurrencyLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        CurrencyLookupDal objCurrencyLookup = null;
        public const String vsCurrency = "Currency";
        AuthorizationBDto objAuthorizationBDto;
        CurrencyBDto objCurrencyLookupBDto = null;
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
                Session["currentevent"] = "Currency";
            }

            hdnEditableMode.Value = "false";
            acbCurrency.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdCurrency.ClientID));
            acbCurrency.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdCurrency.ClientID));





        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Currency");
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
        /// Bind RoomType grid
        /// </summary>
        private void BindGrid()
        {

            objCurrencyLookup = new CurrencyLookupDal();
            DataSet dsCurrency = objCurrencyLookup.GetCurrency("");
            radgrdCurrency.DataSource = dsCurrency;
            radgrdCurrency.DataBind();
            ViewState[vsCurrency] = dsCurrency;

        }

        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int result = 0;
                objCurrencyLookup = new CurrencyLookupDal();
                objCurrencyLookupBDto = new CurrencyBDto();
                objCurrencyLookupBDto.CurrencyName = txtCurrency.Text;
                objCurrencyLookupBDto.CurrencySymbol = txtCurrencySymbol.Text;
                objCurrencyLookupBDto.UserId = objAuthorizationBDto.UserProfile.UserId;
                result = objCurrencyLookup.InsertCurrency(objCurrencyLookupBDto);
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
            txtCurrency.Text = String.Empty;
            txtCurrencySymbol.Text = String.Empty;

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
            string xmlHeaderNodeStructure = "<Currency CURRENCY_ID=\"{0}\" CURRENCY_NAME=\"{1}\" CURRENCY_SYMBOL=\"{2}\" USER_ID=\"{3}\"></Currency>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int CurrencyId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdCurrencyId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdCurrencyEdit");
                        TextBox txtgrdCurrencyName = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdCurrencyName");
                        TextBox txtgrdCurrencySymbol = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdCurrencySymbol");
                        CurrencyId = int.Parse(lblgrdCurrencyId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, CurrencyId, txtgrdCurrencyName.Text, txtgrdCurrencySymbol.Text, objAuthorizationBDto.UserProfile.UserId);
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

        protected void acbCurrency_NewClick(object sender, EventArgs e)
        {
            acbCurrency.EditableMode = true;
            acbCurrency.SaveNewButton.Visible = true;
            acbCurrency.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtCurrency.Focus();
            radgrdCurrency.Rebind();
            Reset();
        }

        protected void acbCurrency_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objCurrencyLookup = new CurrencyLookupDal();
                switch (acbCurrency.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbCurrency.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdCurrency);
                        result = objCurrencyLookup.UpdateCurrency(xmlData);
                        if (result == 1)
                        {
                            acbCurrency.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdCurrency.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objCurrencyLookup = new CurrencyLookupDal();

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbCurrency_SaveNewClick(object sender, EventArgs e)
        {
            txtCurrency.Focus();

            Save();
            Reset();
        }

        protected void acbCurrency_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdCurrency.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdCurrency.Rebind();

            }
            acbCurrency.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbCurrency_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder CurrencyId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblCurrencyId = (Label)radgrdCurrency.Items[i].FindControl("lblgrdCurrencyIdItem");

                        if (lblCurrencyId != null)
                        {
                            CurrencyId.Append(lblCurrencyId.Text + ",");
                        }
                    }
                }

                objCurrencyLookup = new CurrencyLookupDal();
                String CurrenId = CurrencyId.ToString().TrimEnd(',');
                result = objCurrencyLookup.DeleteCurrency(CurrenId);


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

        protected void acbCurrency_SearchClick(object sender, EventArgs e)
        {
            try
            {

                objCurrencyLookup = new CurrencyLookupDal();
                DataSet dsCurrency = objCurrencyLookup.GetCurrency(acbCurrency.SearchTextBox.Text);
                radgrdCurrency.DataSource = dsCurrency;
                radgrdCurrency.DataBind();
                ViewState[vsCurrency] = dsCurrency;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }

        }

        protected void acbCurrency_EditClick(object sender, EventArgs e)
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
                        radgrdCurrency.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    radgrdCurrency.Rebind();
                    acbCurrency.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbCurrency.SaveButton.CommandName = "Update";
                    acbCurrency.SaveNewButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbCurrency_RefreshClick(object sender, EventArgs e)
        {
            acbCurrency.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbCurrency.SearchTextBox.Text = String.Empty;
            BindGrid();
        }
        #endregion

        #region Grid Event

        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        protected void radgrdCurrency_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdCurrency.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdCurrency.Items)
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
        protected void radgrdCurrency_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdCurrency.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdCurrency.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdCurrency_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsCurrency] != null)
                radgrdCurrency.DataSource = ViewState[vsCurrency];
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
                            radgrdCurrency.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdCurrency.ExportSettings.ExportOnlyData = true;
            radgrdCurrency.ExportSettings.OpenInNewWindow = true;
            radgrdCurrency.ExportSettings.IgnorePaging = true;

            string filename = "Currency_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdCurrency.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdCurrency.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdCurrency.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdCurrency.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdCurrency.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbCurrency_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbCurrency_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbCurrency_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbCurrency_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion




    }
}

