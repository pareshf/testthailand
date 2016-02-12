﻿#region Program Information
/**********************************************************************************************************************************************
 Class Name           : ProductLookUp
 Class Description    : Implementation logic for save, edit, delete and find operation for customer details.
 Author               : Chirag.
 Created Date         : Mar 13, 2010
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
    public partial class ProductLookup : System.Web.UI.Page
    {
        #region Member variables
        Hashtable htItemIndex;
        Boolean bisEdit = false;
        ProductLookupDal objProductLookUp = null;
        public const String vsProduct = "Product";
        LookupBDto objLookUpBDto = null;
        AuthorizationBDto objAuthorizationBDto = null;

        #endregion

        #region Events

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            WebHelper.WebManager.CheckUserAuthorizationForProgram("Product");
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
                Session["currentevent"] = "Product";
            }

            acbProduct.AddAttributeToEditButton("onClick", String.Format("javascript:return ValidateEdit('{0}')", radgrdProduct.ClientID));
            acbProduct.AddAttributeToDeleteButton("onClick", String.Format("javascript:return ValidateDelete('{0}')", radgrdProduct.ClientID));
        }

        #endregion

        #region Actionbar Events


        /// <summary>
        /// Action bar's new button event which open view to insert new Product.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>

        protected void acbProduct_NewClick(object sender, EventArgs e)
        {
            
            try
            {
                txtProductDesc.Focus();    
                acbProduct.EditableMode = true;
                acbProduct.SaveNewButton.Visible = true;
                acbProduct.SaveButton.CommandName = "Save";
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
        protected void acbProduct_EditClick(object sender, EventArgs e)
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
                        radgrdProduct.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = true;
                    }
                    bisEdit = true;
                    //Session[PageConstants.ssnHasTableIndex] = htItemIndex;
                    radgrdProduct.Rebind();
                    acbProduct.EditableMode = true;
                    hdnEditableMode.Value = "true";
                    acbProduct.SaveNewButton.Visible = false;
                    acbProduct.SaveButton.CommandName = "Update";
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
        protected void acbProduct_DeleteClick(object sender, EventArgs e)
        {
            StringBuilder ProductId = new StringBuilder();
            int result = 0;

            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                if (htItemIndex != null)
                {
                    foreach (int i in htItemIndex.Values)
                    {
                        Label lblProductId = (Label)radgrdProduct.Items[i].FindControl("lblGrdProductIdItem");
                        if (lblProductId != null)
                        {
                            ProductId.Append(lblProductId.Text + ",");
                        }
                    }
                }
                objProductLookUp = new ProductLookupDal();
                String ProductDescId = ProductId.ToString().TrimEnd(',');
                result = objProductLookUp.DeleteProduct(ProductDescId);

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
        protected void acbProduct_SaveClick(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                objProductLookUp = new ProductLookupDal();
                switch (acbProduct.SaveButton.CommandName)
                {
                    case "Save":
                        SaveProduct();
                        pnlAddNewMode.Visible = false;
                        acbProduct.EditableMode = false;
                        BindGrid();

                        break;
                    case "Update":
                        String xmlData = GenerateXmlString(radgrdProduct);
                        result = objProductLookUp.UpdateProduct(xmlData);
                        if (result == 1)
                        {
                            acbProduct.DefaultMode = true;
                            Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Update].ToString());
                            Master.MessageCssClass = "successMessage";

                            if (ViewState[PageConstants.vsItemIndexes] != null)
                                htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];
                            for (int i = 0; i < htItemIndex.Count; i++)
                                radgrdProduct.Items[Convert.ToInt32(htItemIndex[i])].Edit = false;
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

        /// <summary>
        /// Action bar's cancel button event which opens grid in default mode.
        /// </summary>
        /// <param name="sender">The object which raised the event</param>
        /// <param name="e">The event listener object</param>
        /// 
        protected void acbProduct_CancelClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)ViewState[PageConstants.vsItemIndexes];

                    for (int i = 0; i < htItemIndex.Count; i++)
                        radgrdProduct.Items[Convert.ToInt32(htItemIndex[i].ToString())].Edit = false;
                    bisEdit = false;
                    ViewState[PageConstants.vsItemIndexes] = null;
                    radgrdProduct.Rebind();
                }
                acbProduct.DefaultMode = true;
                pnlAddNewMode.Visible = false;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbProduct_SearchClick(object sender, EventArgs e)
        {
            try
            {
                objProductLookUp = new ProductLookupDal();
                DataSet dsProduct = objProductLookUp.GetproductDesc(acbProduct.SearchTextBox.Text);
                radgrdProduct.DataSource = dsProduct;
                radgrdProduct.DataBind();
                ViewState[vsProduct] = dsProduct;
                Reset();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                { throw ex; }
            }
        }

        protected void acbProduct_SaveNewClick(object sender, EventArgs e)
        {
            try
            {
                txtProductDesc.Focus();
                SaveProduct();
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
        protected void radgrdProduct_PreRender(object source, EventArgs e)
        {
            if (bisEdit)
            {
                GridHeaderItem headerItem = radgrdProduct.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

                CheckBox chkHeader = (CheckBox)headerItem.FindControl("chkHeadWrT");
                if (chkHeader != null)
                    chkHeader.Visible = false;
                foreach (GridDataItem item in radgrdProduct.Items)
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
        protected void radgrdProduct_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkHeadWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("HeaderCheckBox_Click('{0}',{1},'{2}')", radgrdProduct.ClientID, 0, chkBox.ClientID));
                }

                if (e.Item is GridDataItem)
                {
                    CheckBox chkBox = (CheckBox)e.Item.FindControl("chkItemWrt");
                    if (chkBox != null)
                        chkBox.Attributes.Add("onclick", string.Format("RowCheckBox_Click('{0}',{1},'{2}')", radgrdProduct.ClientID, chkBox.ClientID, e.Item.ItemIndex));
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
        protected void radgrdProduct_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (ViewState[vsProduct] != null)
                radgrdProduct.DataSource = ViewState[vsProduct];
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
                                radgrdProduct.Items[htItemIndex[i].ToString()].Edit = false;
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
        /// Bind ProductLookup grid
        /// </summary>
        private void BindGrid()
        {
            objProductLookUp = new ProductLookupDal();
            DataSet dsProduct = objProductLookUp.GetproductDesc("");
            radgrdProduct.DataSource = dsProduct;
            radgrdProduct.DataBind();
            ViewState[vsProduct] = dsProduct;
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
            string xmlHeaderNodeStructure = "<Products PRODUCT_ID=\"{0}\" PRODUCT_DESC=\"{1}\" USER_ID=\"{2}\"></Products>";
            StringBuilder xmlString = new StringBuilder();
            try
            {
                if (ViewState[PageConstants.vsItemIndexes] != null)
                {
                    htItemIndex = (Hashtable)(ViewState[PageConstants.vsItemIndexes]);
                }

                if (htItemIndex != null && htItemIndex.Count > 0)
                {
                    int ProductId = 0;
                    xmlString.AppendFormat(xmlRootStart, xmlHeaderRootValue);

                    for (int i = 0; i < htItemIndex.Count; i++)
                    {
                        Label lblGrdProductIdItem = (Label)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("lblGrdProductIdEdit");
                        TextBox txtproduct = (TextBox)grid.Items[Convert.ToInt32(htItemIndex[i])].FindControl("txtGrdProductDesc");
                        ProductId = int.Parse(lblGrdProductIdItem.Text);
                        xmlString.AppendFormat(xmlHeaderNodeStructure, ProductId, txtproduct.Text, objAuthorizationBDto.UserProfile.UserId);
                    }
                    xmlString.AppendFormat(xmlRootEnd, xmlHeaderRootValue);
                }
            }
            catch (Exception ex) { }
            return xmlString.ToString();
        }
        #endregion

        #region Save
        private void SaveProduct()
        {
            try
            {
                int result = 0;
                objProductLookUp = new ProductLookupDal();
                LookupBDto objProduct = new LookupBDto();
                objLookUpBDto = new LookupBDto();
                objLookUpBDto.Description = txtProductDesc.Text;
                objLookUpBDto.UserProfile = objAuthorizationBDto.UserProfile;
                result = objProductLookUp.InsertProduct(objLookUpBDto);
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
            txtProductDesc.Text = string.Empty;

        }
        #endregion

        #region Export Methods and Control Events

        private void ExportData(string format)
        {
            radgrdProduct.ExportSettings.ExportOnlyData = true;
            radgrdProduct.ExportSettings.OpenInNewWindow = true;
            radgrdProduct.ExportSettings.IgnorePaging = true;

            string filename = "Product" + System.DateTime.Now.ToString("ddmmyyyy") + "_" + System.DateTime.Now.Hour.ToString();
            radgrdProduct.ExportSettings.FileName = filename;

            //radgrdDepartment.MasterTableView.GetColumn("chkDepartment").Visible = false;

            switch (format)
            {
                case ExportOptions.Excel:
                    {
                        radgrdProduct.MasterTableView.ExportToExcel();
                        break;
                    }
                case ExportOptions.Word:
                    {
                        radgrdProduct.MasterTableView.ExportToWord();
                        break;
                    }
                case ExportOptions.Pdf:
                    {
                        radgrdProduct.MasterTableView.ExportToPdf();
                        break;
                    }
                case ExportOptions.Csv:
                    {
                        radgrdProduct.MasterTableView.ExportToCSV();
                        break;
                    }
            }

            //ClientScript.RegisterStartupScript(GetType(), "", "window.location.reload();", true);
        }

        protected void acbProduct_ExcelExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Excel);
        }

        protected void acbProduct_WordExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Word);
        }

        protected void acbProduct_PdfExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Pdf);
        }

        protected void acbProduct_CsvExportClick(object sender, EventArgs e)
        {
            ExportData(ExportOptions.Csv);
        }

        #endregion
        
        #endregion

    }
}
