using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
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
    public partial class PaymentModeLoopup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        PaymentModeLookupDal objPaymentModeLookup = null;
        public const String vsPayment = "Payment";
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
                Session["currentevent"] = "Payment Mode";
            }
            hdnEditableMode.Value = "false";
            acbPayment.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdPayment.ClientID));
            acbPayment.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdPayment.ClientID));

        }

        #region Check If Session is active or not
        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Payment Mode");
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
        /// Bind Title grid
        /// </summary>

        private void BindGrid()
        {
            objPaymentModeLookup = new PaymentModeLookupDal();
            DataSet dsPayment = objPaymentModeLookup.GetPayment("");
            radgrdPayment.DataSource = dsPayment;
            radgrdPayment.DataBind();
            ViewState[vsPayment] = dsPayment;

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
            string xmlHeaderNodeStructure = "<Payment PAYMENT_MODE_ID=\"{0}\" PAYMENT_MODE_NAME=\"{1}\" USER_ID=\"{2}\"></Payment>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int PaymentId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblgrdPaymentId = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblgrdPaymentIdEdit");
                        TextBox txtgrdPaymentMode = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtgrdPaymentMode");
                        PaymentId = int.Parse(lblgrdPaymentId.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, PaymentId, txtgrdPaymentMode.Text, objAuthorizationBDto.UserProfile.UserId);
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
                objPaymentModeLookup = new PaymentModeLookupDal();
                objLookupBDto = new LookupBDto();
                objLookupBDto.LookupName = txtPaymentMode.Text;
                objLookupBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objPaymentModeLookup.InsertPayment(objLookupBDto);
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
            txtPaymentMode.Text = String.Empty;

        }
        #endregion
        #endregion


        #region Actionbar Events

        protected void acbPayment_NewClick(object sender, EventArgs e)
        {
            acbPayment.EditableMode = true;
            acbPayment.SaveButton.CommandName = "Save";
            pnlAddNewMode.Visible = true;
            txtPaymentMode.Focus();
            radgrdPayment.Rebind();
            Reset();
        }

        protected void acbPayment_SaveClick(object sender, EventArgs e)
        {

            try
            {
                int result = 0;
                objPaymentModeLookup = new PaymentModeLookupDal();
                switch (acbPayment.SaveButton.CommandName)
                {
                    case "Save":
                        Save();
                        pnlAddNewMode.Visible = false;
                        acbPayment.EditableMode = false;

                        break;

                    case "Update":
                        String xmlData = GenerateXmlString(radgrdPayment);
                        result = objPaymentModeLookup.UpdatePayment(xmlData);
                        if (result == 1)
                        {
                            acbPayment.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings["UpdateRecord"].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdPayment.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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
                objPaymentModeLookup = new PaymentModeLookupDal();

            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbPayment_SaveNewClick(object sender, EventArgs e)
        {
            txtPaymentMode.Focus();
            Save();
            Reset();
        }

        protected void acbPayment_CancelClick(object sender, EventArgs e)
        {
            if (ViewState[PageConstants.vsItemIndexes] != null)
            {
                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                for (int i = 0; i < htItemIndex.Count; i++)
                    radgrdPayment.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                bisEdit = false;
                ViewState[PageConstants.vsItemIndexes] = null;
                radgrdPayment.Rebind();

            }

            acbPayment.DefaultMode = true;
            pnlAddNewMode.Visible = false;
        }

        protected void acbPayment_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                StringBuilder PaymentId = new StringBuilder();
                int result = 0;
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {

                        Label lblPaymentId = (Label)radgrdPayment.Items[i].FindControl("lblgrdPaymentIdItem");
                        if (lblPaymentId != null)
                        {
                            PaymentId.Append(lblPaymentId.Text + ",");
                        }
                    }
                }

                objPaymentModeLookup = new PaymentModeLookupDal();
                String PayId = PaymentId.ToString().TrimEnd(',');
                result = objPaymentModeLookup.DeletePayment(PayId);


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

        protected void acbPayment_EditClick(object sender, EventArgs e)
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
                        radgrdPayment.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;

                    }
                    bisEdit = true;
                    radgrdPayment.Rebind();
                    acbPayment.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbPayment.SaveNewButton.Visible = false;
                    acbPayment.SaveButton.CommandName = "Update";


                }

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbPayment_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objPaymentModeLookup = new PaymentModeLookupDal();
                DataSet dsPayment = objPaymentModeLookup.GetPayment(acbPayment.SearchTextBox.Text);
                radgrdPayment.DataSource = dsPayment;
                radgrdPayment.DataBind();
                ViewState[vsPayment] = dsPayment;

            }


            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbPayment_RefreshClick(object sender, EventArgs e)
        {
            acbPayment.DefaultMode = true;
            pnlAddNewMode.Visible = false;
            acbPayment.SearchTextBox.Text = String.Empty;
            BindGrid();


        }

        #endregion

        #region Grid Event
        /// <summary>
        /// Rad grid's pre rander event which visible true/false check box in editable mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void radgrdPayment_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdPayment.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;

                foreach (GridDataItem item in radgrdPayment.Items)
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
        protected void radgrdPayment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdPayment.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdPayment.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdPayment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsPayment] != null)
                radgrdPayment.DataSource = ViewState[vsPayment];
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
                            radgrdPayment.Items[htItemIndex[i].ToString()].Edit = false;
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
            radgrdPayment.ExportSettings.ExportOnlyData = true;
            radgrdPayment.ExportSettings.OpenInNewWindow = true;
            radgrdPayment.ExportSettings.IgnorePaging = true;

            string filename = "Payment_" + System.DateTime.Now.ToString("ddmmyyyy");
            radgrdPayment.ExportSettings.FileName = filename;



            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdPayment.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdPayment.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdPayment.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdPayment.MasterTableView.ExportToCSV();
                        break;
                    }
            }


        }


        protected void acbPayment_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbPayment_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbPayment_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbPayment_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
    }
}
