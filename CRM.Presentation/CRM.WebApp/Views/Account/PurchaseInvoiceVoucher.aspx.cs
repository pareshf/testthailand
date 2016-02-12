using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess.Account;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Core.Utility;

namespace CRM.WebApp.Views.Account
{
    public partial class PurchaseInvoiceVoucher : System.Web.UI.Page
    {
        PurchaseVoucherStoredProcedure objPurchaseVoucherStoredProcedure = new PurchaseVoucherStoredProcedure();
        PurchaseHeader objPurchaseHeader = new PurchaseHeader();

        #region Varaibles

        bool flagblankAmount = true;


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsPercentage = objPurchaseHeader.getEmpPercentage("GET_ACCOUNT_PERCENTAGE", int.Parse(Session["empid"].ToString()));
                ViewState["Percentage"] = dsPercentage.Tables[0].Rows[0][0].ToString();

                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()))
                {
                    DataSet ds = objPurchaseHeader.getAllInformationEditMode("GET_PURCHASE_HEADER_INFORMATION", Request.QueryString["IV"].ToString());

                    ViewState["RowCount"] = ds.Tables[0].Rows.Count - 1;

                    DataSet dsSupplierType = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, dsSupplierType);

                    drpsupplier_type.Text = ds.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();

                    DataSet dsSupplier = objPurchaseVoucherStoredProcedure.fetch_supplier("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
                    binddropdownlist(drpsupplier, dsSupplier);

                    drpsupplier.Text = ds.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();

                    txtdue_date.Text = ds.Tables[0].Rows[0]["DUE_DATE"].ToString();
                    txtgl_date.Text = ds.Tables[0].Rows[0]["GL_DATE"].ToString();
                    // new code

                    txtArrivalDate.Text = ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString();
                    txtdeparturedate.Text = ds.Tables[0].Rows[0]["DEPARTURE_DATE"].ToString();
                    txtservicedate.Text = ds.Tables[0].Rows[0]["SERVICE_DATE"].ToString();


                    txtPurchaseVoucher.Text = ds.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString();
                    lblsettleamt.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    txtNarration.Text = ds.Tables[0].Rows[0]["NARRATION"].ToString();

                    DataSet dsStatus = objPurchaseHeader.commonSp("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpOrderStatus, dsStatus);
                    drpOrderStatus.Text = ds.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                    AddHotels(GridInvoice, upInvoiceGrid);
                    fillDetailsEditMode();

                    DataSet dsAllGlCode = objPurchaseHeader.commonSp("FETCH_ALL_GL_CODE");

                    binddropdownlist(row1_drp_glcode, dsAllGlCode);
                    binddropdownlist(row2_drp_glcode, dsAllGlCode);

                    DataSet dsGlDescription = objPurchaseHeader.getGlDesription("GET_GL_DESCRIPTION", drpsupplier.Text);

                    row1_drp_glcode.Text = dsGlDescription.Tables[0].Rows[0][0].ToString();
                    row2_drp_glcode.Text = getProductGlCode();

                    row1_txt_credit.Text = lblsettleamt.Text;
                    row2_txt_debit.Text = lblsettleamt.Text;

                    if (drpOrderStatus.Text == "POSTED")
                    {
                        statusPosted();
                    }

                    trsrno.Visible = true;
                    txtvouchersrno.Text = Request.QueryString["IV"].ToString();
                }
                else
                {
                    AddHotels(GridInvoice, upInvoiceGrid);
                    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, ds);

                    DataSet dsStatus = objPurchaseHeader.commonSp("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpOrderStatus, dsStatus);

                    drpOrderStatus.Text = "APPROVED";


                    txtgl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    DataSet dsAllGlCode = objPurchaseHeader.commonSp("FETCH_ALL_GL_CODE");

                    binddropdownlist(row1_drp_glcode, dsAllGlCode);
                    binddropdownlist(row2_drp_glcode, dsAllGlCode);
                }


            }
        }

        protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if (drpsupplier.Text != "")
                {
                    clearGrid();

                    //bindInvoiceFromSupplier(0);

                    DataSet dsGlDescription = objPurchaseHeader.getGlDesription("GET_GL_DESCRIPTION", drpsupplier.Text);

                    row1_drp_glcode.Text = dsGlDescription.Tables[0].Rows[0][0].ToString();
                    updategrid.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        public void bindInvoiceFromSupplier(int Index)
        {
            try
            {
                foreach (GridViewRow item in GridInvoice.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        if (ViewState["RowCount"] != null)
                        {
                            if (Index <= int.Parse(ViewState["RowCount"].ToString()))
                            {
                                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                                drpInvoiceNo.Enabled = false;
                                DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE", drpsupplier_type.Text, drpsupplier.Text);
                                binddropdownlist(drpInvoiceNo, ds);
                            }
                            else
                            {
                                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                                DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE_NOT_POST", drpsupplier_type.Text, drpsupplier.Text);
                                binddropdownlist(drpInvoiceNo, ds);
                            }
                        }
                        else
                        {
                            DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                            DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE_NOT_POST", drpsupplier_type.Text, drpsupplier.Text);
                            binddropdownlist(drpInvoiceNo, ds);
                        }
                    }
                }
                //DataSet ds_sup_type = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                //if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
                //{
                //    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SALES_INVOICE_NO_FOR_HOTEL", drpsupplier.Text);
                //    foreach (GridViewRow item in GridInvoice.Rows)
                //    {
                //        if (Index == item.DataItemIndex)
                //        {
                //            DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");

                //            binddropdownlist(drpInvoiceNo, ds);
                //        }
                //    }
                //}
                //else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString())
                //{
                //    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SALES_INVOICE_NO_FOR_SITE_SEEING", drpsupplier.Text);
                //    foreach (GridViewRow item in GridInvoice.Rows)
                //    {
                //        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                //        binddropdownlist(drpInvoiceNo, ds);
                //    }
                //}
                //else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString())
                //{
                //    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SALES_INVOICE_NO_FOR_TRANSFER_PACKAGE", drpsupplier.Text);
                //    foreach (GridViewRow item in GridInvoice.Rows)
                //    {
                //        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                //        binddropdownlist(drpInvoiceNo, ds);
                //    }
                //}

                //else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString())
                //{
                //    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier.Text);
                //    foreach (GridViewRow item in GridInvoice.Rows)
                //    {
                //        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                //        binddropdownlist(drpInvoiceNo, ds);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upInvoiceGrid.Update();
            }
        }

        protected void txtSettledAmount_TextChanged(object sender, EventArgs e)
        {
            TextBox txtAmount = sender as TextBox;
            int repeaterItemIndex = ((GridViewRow)txtAmount.NamingContainer).DataItemIndex;

            foreach (GridViewRow item in GridInvoice.Rows)
            {
                if (item.DataItemIndex == repeaterItemIndex)
                {
                    Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");

                    if (ViewState["Percentage"].ToString() == "0")
                    {

                    }
                    else
                    {
                        if (lblRemaingBalance.Text != "")
                        {
                            if (decimal.Parse(txtSettledAmount.Text) > decimal.Parse(lblRemaingBalance.Text))
                            {
                                Master.DisplayMessage("You do not have permission to enter more than '" + ViewState["Percentage"].ToString() + "' % total Invoice amount.", "successMessage", 8000);
                                txtSettledAmount.Text = "";
                                upInvoiceGrid.Update();
                            }
                        }
                    }
                }
            }

            decimal amount = 0;
            foreach (GridViewRow item in GridInvoice.Rows)
            {

                TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtSettledAmount");

                if (txtInvoiceAmount.Text != "")
                {
                    amount += Convert.ToDecimal(txtInvoiceAmount.Text);

                    lblsettleamt.Text = amount.ToString();

                    row1_txt_credit.Text = lblsettleamt.Text;
                    row2_txt_debit.Text = lblsettleamt.Text;
                }

            }
            btnAdd.Focus();
            upInvoiceGrid.Update();
            updategrid.Update();
        }


        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }

        protected void drpsupplier_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearGrid();

                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_supplier("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
                binddropdownlist(drpsupplier, ds);

                row2_drp_glcode.Text = getProductGlCode();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UpdatePanel_Generate_Invoice.Update();
                updategrid.Update();
            }
        }

        protected void btnAddHotel_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridInvoice, upInvoiceGrid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                removeRow(GridInvoice, rowID, upInvoiceGrid);
                //new
                decimal amount = 0;
                foreach (GridViewRow item in GridInvoice.Rows)
                {

                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtSettledAmount");

                    if (txtInvoiceAmount.Text != "")
                    {
                        amount += Convert.ToDecimal(txtInvoiceAmount.Text);

                        lblsettleamt.Text = amount.ToString();

                        row1_txt_credit.Text = lblsettleamt.Text;
                        row2_txt_debit.Text = lblsettleamt.Text;
                    }

                }
                btnAdd.Focus();
                upInvoiceGrid.Update();
                updategrid.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upInvoiceGrid.Update();
            }

        }




        protected void AddHotels(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                    Label lblAmount = (Label)item.FindControl("lblTotalAmount");
                    TextBox txtSettleAmount = (TextBox)item.FindControl("txtSettledAmount");
                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");

                    Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                    //Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    //Label lblAmountValidate = (Label)item.FindControl("lblAmountValidate");
                    TextBox txtInvoiceAmountTHB = (TextBox)item.FindControl("txtInvoiceAmountTHB");
                    Label lblTourName = (Label)item.FindControl("lblTourName");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("SettleAmount");
                        dt.Columns.Add("InvoiceAmount");
                        //dt.Columns.Add("RemainingAmount");
                        //dt.Columns.Add("ValidateAmount");

                        dt.Columns.Add("lblRemaingBalance");
                        dt.Columns.Add("lblAmountSettled");

                        dt.Columns.Add("InvoiceAmountTHB");
                        dt.Columns.Add("TourName");
                    }

                    DataRow dr = dt.NewRow();
                    dr["InvoiceNo"] = drpInvoiceNo.Text;
                    dr["Amount"] = lblAmount.Text;
                    dr["SettleAmount"] = txtSettleAmount.Text;
                    dr["InvoiceAmount"] = txtInvoiceAmount.Text;
                    //dr["RemainingAmount"] = lblRemaingBalance.Text;
                    //dr["ValidateAmount"] = lblAmountValidate.Text;

                    dr["lblRemaingBalance"] = lblRemaingBalance.Text;
                    dr["lblAmountSettled"] = lblAmountSettled.Text;

                    dr["InvoiceAmountTHB"] = txtInvoiceAmountTHB.Text;
                    dr["TourName"] = lblTourName.Text;
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("SettleAmount");
                        dt.Columns.Add("InvoiceAmount");

                        dt.Columns.Add("lblRemaingBalance");
                        dt.Columns.Add("lblAmountSettled");

                        //dt.Columns.Add("RemainingAmount");
                        //dt.Columns.Add("ValidateAmount");
                        dt.Columns.Add("InvoiceAmountTHB");
                        dt.Columns.Add("TourName");
                    }

                    DataRow dr = dt.NewRow();
                    dr["InvoiceNo"] = "";
                    dr["Amount"] = "";
                    dr["SettleAmount"] = "";
                    dr["InvoiceAmount"] = "";

                    dr["lblRemaingBalance"] = "";
                    dr["lblAmountSettled"] = "";

                    //dr["RemainingAmount"] = "";
                    //dr["ValidateAmount"] = "";

                    dr["InvoiceAmountTHB"] = "";
                    dr["TourName"] = "";
                    dt.Rows.Add(dr);

                    gv.DataSource = dt;
                    gv.DataBind();

                    uppanel.Update();
                }

                if (count != 0)
                {
                    DataRow dr1 = dt.NewRow();

                    dt.Rows.Add(dr1);
                }

                gv.DataSource = dt;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                    Label lblAmount = (Label)item.FindControl("lblTotalAmount");
                    TextBox txtSettleAmount = (TextBox)item.FindControl("txtSettledAmount");
                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");

                    Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                    //Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    // Label lblAmountValidate = (Label)item.FindControl("lblAmountValidate");

                    Button btnHotelRemove = (Button)item.FindControl("btnHotelRemove");
                    Label lblTourName = (Label)item.FindControl("lblTourName");
                    TextBox txtInvoiceAmountTHB = (TextBox)item.FindControl("txtInvoiceAmountTHB");

                    if (ViewState["RowCount"] != null)
                    {
                        if (itm <= int.Parse(ViewState["RowCount"].ToString()))
                        {
                            btnHotelRemove.Visible = false;
                        }
                        else
                        {

                        }
                    }

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            bindInvoiceFromSupplier(k);

                            drpInvoiceNo.Text = dt.Rows[itm]["InvoiceNo"].ToString();
                            lblAmount.Text = dt.Rows[itm]["Amount"].ToString();
                            txtSettleAmount.Text = dt.Rows[itm]["SettleAmount"].ToString();
                            txtInvoiceAmount.Text = dt.Rows[itm]["InvoiceAmount"].ToString();

                            lblRemaingBalance.Text = dt.Rows[itm]["lblRemaingBalance"].ToString();
                            lblAmountSettled.Text = dt.Rows[itm]["lblAmountSettled"].ToString();
                            //lblRemaingBalance.Text = dt.Rows[itm]["RemainingAmount"].ToString();
                            //lblAmountValidate.Text = dt.Rows[itm]["ValidateAmount"].ToString();
                            txtInvoiceAmountTHB.Text = dt.Rows[itm]["InvoiceAmountTHB"].ToString();
                            lblTourName.Text = dt.Rows[itm]["TourName"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uppanel.Update();
            }

        }

        protected void removeRow(GridView gv, int rowIndex, UpdatePanel uppanel)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                int count = gv.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    dt1.Rows.Add();
                }
                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                    Label lblAmount = (Label)item.FindControl("lblTotalAmount");
                    TextBox txtSettleAmount = (TextBox)item.FindControl("txtSettledAmount");
                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");

                    Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                    //Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    //Label lblAmountValidate = (Label)item.FindControl("lblAmountValidate");
                    TextBox txtInvoiceAmountTHB = (TextBox)item.FindControl("txtInvoiceAmountTHB");
                    Label lblTourName = (Label)item.FindControl("lblTourName");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("SettleAmount");
                        dt.Columns.Add("InvoiceAmount");

                        dt.Columns.Add("lblRemaingBalance");
                        dt.Columns.Add("lblAmountSettled");

                        //dt.Columns.Add("RemainingAmount");
                        //dt.Columns.Add("ValidateAmount");
                        dt.Columns.Add("InvoiceAmountTHB");
                        dt.Columns.Add("TourName");
                    }

                    DataRow dr = dt.NewRow();
                    dr["InvoiceNo"] = drpInvoiceNo.Text;
                    dr["Amount"] = lblAmount.Text;
                    dr["SettleAmount"] = txtSettleAmount.Text;
                    dr["InvoiceAmount"] = txtInvoiceAmount.Text;
                    // dr["RemainingAmount"] = lblRemaingBalance.Text;
                    //dr["ValidateAmount"] = lblAmountValidate.Text;

                    dr["lblRemaingBalance"] = "";
                    dr["lblAmountSettled"] = "";

                    dr["InvoiceAmountTHB"] = txtInvoiceAmountTHB.Text;
                    dr["TourName"] = lblTourName.Text;
                    dt.Rows.Add(dr);

                }

                gv.DataSource = dt1;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    int itm1 = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }

                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                    Label lblAmount = (Label)item.FindControl("lblTotalAmount");
                    TextBox txtSettleAmount = (TextBox)item.FindControl("txtSettledAmount");
                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");

                    Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                    //Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                    //Label lblAmountValidate = (Label)item.FindControl("lblAmountValidate");
                    TextBox txtInvoiceAmountTHB = (TextBox)item.FindControl("txtInvoiceAmountTHB");
                    Label lblTourName = (Label)item.FindControl("lblTourName");

                    Button btnHotelRemove = (Button)item.FindControl("btnHotelRemove");
                    if (ViewState["RowCount"] != null)
                    {
                        if (itm <= int.Parse(ViewState["RowCount"].ToString()))
                        {
                            btnHotelRemove.Visible = false;
                        }
                        else
                        {

                        }
                    }

                    bindInvoiceFromSupplier(itm1);

                    drpInvoiceNo.Text = dt.Rows[itm]["InvoiceNo"].ToString();
                    lblAmount.Text = dt.Rows[itm]["Amount"].ToString();
                    txtSettleAmount.Text = dt.Rows[itm]["SettleAmount"].ToString();
                    txtInvoiceAmount.Text = dt.Rows[itm]["InvoiceAmount"].ToString();

                    lblRemaingBalance.Text = dt.Rows[itm]["lblRemaingBalance"].ToString();
                    lblAmountSettled.Text = dt.Rows[itm]["lblAmountSettled"].ToString();
                    // lblRemaingBalance.Text = dt.Rows[itm]["RemainingAmount"].ToString();
                    // lblAmountValidate.Text = dt.Rows[itm]["ValidateAmount"].ToString();
                    txtInvoiceAmountTHB.Text = dt.Rows[itm]["InvoiceAmountTHB"].ToString();
                    lblTourName.Text = dt.Rows[itm]["TourName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uppanel.Update();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()))
            {
                BlankAmountValidation();
                if (flagblankAmount == false)
                {
                    if (ViewState["BkankAmountMsg"] != null)
                    {
                        Master.DisplayMessage(ViewState["BkankAmountMsg"].ToString(), "successMessage", 8000);
                    }
                    else
                    {

                        Master.DisplayMessage("Settle Amount for invoice should not be blank.", "successMessage", 8000);
                    }
                }
                else
                {
                    foreach (GridViewRow item in GridInvoice.Rows)
                    {
                        bool flag = true;
                        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                        TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");

                        //CheckBox chkSettled = (CheckBox)item.FindControl("chkSettled");

                        if (drpInvoiceNo.Text != "")
                        {
                            DataSet ds = objPurchaseHeader.getAllInformationEditMode("GET_PURCHASE_HEADER_INFORMATION", Request.QueryString["IV"].ToString());

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["SALES_INVOICE_NO"].ToString() == drpInvoiceNo.Text)
                                {
                                    flag = false;
                                    break;
                                }
                            }

                            if (flag == false)
                            {
                                objPurchaseHeader.InsertUpdatePurchaseHeaderonEdit(txtdue_date.Text, drpsupplier_type.Text, drpsupplier.Text, txtgl_date.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["CompanyId"].ToString()), lblsettleamt.Text, "0", lblsettleamt.Text, txtNarration.Text, "APPROVED", drpInvoiceNo.Text, txtSettledAmount.Text, 3, txtPurchaseVoucher.Text, int.Parse(Session["usersid"].ToString()),txtArrivalDate.Text,txtdeparturedate.Text,txtservicedate.Text);
                            }
                            else
                            {
                                objPurchaseHeader.InsertUpdatePurchaseHeaderonEdit(txtdue_date.Text, drpsupplier_type.Text, drpsupplier.Text, txtgl_date.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["CompanyId"].ToString()), lblsettleamt.Text, "0", lblsettleamt.Text, txtNarration.Text, "APPROVED", drpInvoiceNo.Text, txtSettledAmount.Text, 1, txtPurchaseVoucher.Text, int.Parse(Session["usersid"].ToString()), txtArrivalDate.Text, txtdeparturedate.Text, txtservicedate.Text);
                            }

                            //if (chkSettled.Checked)
                            //{
                            //    updatePurchaseInvoiceStatus(drpInvoiceNo.Text);

                            //}
                        }
                    }

                    objPurchaseHeader.InsertUpdatePurchaseHeaderonEdit(txtdue_date.Text, drpsupplier_type.Text, drpsupplier.Text, txtgl_date.Text, int.Parse(Session["usersid"].ToString()), 1, lblsettleamt.Text, "0", lblsettleamt.Text, txtNarration.Text, "APPROVED", "", "", 2, txtPurchaseVoucher.Text, int.Parse(Session["usersid"].ToString()),txtArrivalDate.Text, txtdeparturedate.Text, txtservicedate.Text);
                    updateAccountVoucher(); // Update Account Voucher 
                    Master.DisplayMessage("Record Updated Successfully.", "successMessage", 8000);
                }
            }
            else
            {
                BlankAmountValidation();
                if (flagblankAmount == false)
                {
                    if (ViewState["BkankAmountMsg"] != null)
                    {
                        Master.DisplayMessage(ViewState["BkankAmountMsg"].ToString(), "successMessage", 8000);
                    }
                    else
                    {
                        Master.DisplayMessage("Settle Amount for invoice should not be blank.", "successMessage", 8000);
                    }
                }
                else
                {
                    objPurchaseHeader.InsertPurchaseHeader(txtdue_date.Text, drpsupplier_type.Text, drpsupplier.Text, txtgl_date.Text, int.Parse(Session["usersid"].ToString()), 1, lblsettleamt.Text, "0", lblsettleamt.Text, txtNarration.Text, "APPROVED", "", "", 1, "THB", int.Parse(Session["usersid"].ToString()),txtArrivalDate.Text, txtdeparturedate.Text, txtservicedate.Text);

                    foreach (GridViewRow item in GridInvoice.Rows)
                    {
                        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                        TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");

                        if (drpInvoiceNo.Text != "")
                        {

                            objPurchaseHeader.InsertPurchaseHeader(txtdue_date.Text, drpsupplier_type.Text, drpsupplier.Text, txtgl_date.Text, int.Parse(Session["usersid"].ToString()), 1, lblsettleamt.Text, "0", lblsettleamt.Text, txtNarration.Text, "APPROVED", drpInvoiceNo.Text, txtSettledAmount.Text, 2, "THB", int.Parse(Session["usersid"].ToString()),txtArrivalDate.Text, txtdeparturedate.Text, txtservicedate.Text);
                        }

                    }

                    InsertAccountVoucherEntry();
                    clearAll();
                    DataSet dsInvoiceNo = objPurchaseHeader.commonSp("GET_MAX_PURCHASE_INVOICE_NO_NEW");
                    Master.DisplayMessage("Record Save Successfully. Your Voucher no is : '" + dsInvoiceNo.Tables[0].Rows[0]["INVOICE_NO"].ToString() + "' ", "successMessage", 8000);
                }
            }

        }



        protected void drpInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = sender as DropDownList;
            int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
            Validation();
            getInvoiceInformation(repeaterItemIndex);
        }

        protected void getInvoiceInformation(int Index)
        {
            try
            {
                foreach (GridViewRow item in GridInvoice.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                        if (drpInvoiceNo.Text != "")
                        {
                            TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");
                            TextBox txtInvoiceAmountTHB = (TextBox)item.FindControl("txtInvoiceAmountTHB");
                            Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                            Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                            Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                            DataSet dsDescription = objPurchaseHeader.getInvoiceDescription("GET_INVOICE_DESCRIPTION_FOR_PURCHASE_VOUCHER", drpInvoiceNo.Text, drpsupplier.Text);



                            Label lblTourName = (Label)item.FindControl("lblTourName");

                            lblTourName.Text = dsDescription.Tables[0].Rows[0]["TOUR_SHORT_NAME"].ToString();
                            txtInvoiceAmount.Text = dsDescription.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();

                            txtInvoiceAmountTHB.Text = dsDescription.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                            //DataSet dsRemainingBalance = objPurchaseHeader.getRemainingBalance("GET_REMAINING_BALANCE_TO_BE_PAID", drpInvoiceNo.Text, drpsupplier.Text);
                            TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");

                            //if (int.Parse(dsDescription.Tables[0].Rows[0]["VOUCHER_STATUS_ID"].ToString()) == 3)
                            //{
                            //    DataSet dsReceiptAmount = objPurchaseHeader.getInvoiceTotalReceiptAmount("GET_INVOICE_RECEIPT_TOTAL_AMOUNT_IN_THB", drpInvoiceNo.Text);
                            //    lblTotalAmount.Text = dsReceiptAmount.Tables[0].Rows[0]["TOTAL_RECEIPT_AMOUNT"].ToString();
                            //}
                            //else
                            //{
                            //    lblTotalAmount.Text = "";
                            //}
                            lblTotalAmount.Text = "";

                            DataSet dsSettledAmount = objPurchaseHeader.getInvoiceAmountSettled("GET_INVOICE_SETTLED_AMOUNT", drpInvoiceNo.Text);


                            if (dsSettledAmount.Tables[0].Rows.Count != 0)
                            {
                                if (dsSettledAmount.Tables[0].Rows[0][0].ToString() != "")
                                {
                                    lblAmountSettled.Text = dsSettledAmount.Tables[0].Rows[0][0].ToString();
                                }
                                else
                                {
                                    lblAmountSettled.Text = "0";
                                }
                            }
                            else
                            {
                                lblAmountSettled.Text = "0";
                            }

                            if (ViewState["RowCount"] != null)
                            {
                                if (Index <= int.Parse(ViewState["RowCount"].ToString()))
                                {
                                    lblRemaingBalance.Text = Math.Round(((decimal.Parse(txtInvoiceAmountTHB.Text) * decimal.Parse(ViewState["Percentage"].ToString())) / 100) - decimal.Parse(lblAmountSettled.Text) + decimal.Parse(txtSettledAmount.Text), 2).ToString();
                                }
                                else
                                {
                                    if (ViewState["Percentage"].ToString() != "0")
                                    {
                                        lblRemaingBalance.Text = Math.Round(((decimal.Parse(txtInvoiceAmountTHB.Text) * decimal.Parse(ViewState["Percentage"].ToString())) / 100) - decimal.Parse(lblAmountSettled.Text), 2).ToString();
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {
                                if (ViewState["Percentage"].ToString() != "0")
                                {
                                    lblRemaingBalance.Text = Math.Round(((decimal.Parse(txtInvoiceAmountTHB.Text) * decimal.Parse(ViewState["Percentage"].ToString())) / 100) - decimal.Parse(lblAmountSettled.Text), 2).ToString();
                                }
                                else
                                {

                                }
                            }

                            //if (dsRemainingBalance != null)
                            //{
                            //    lblRemaingBalance.Text = dsRemainingBalance.Tables[0].Rows[0]["REMAIN_AMOUNT"].ToString();
                            //    if (dsRemainingBalance.Tables[0].Rows[0]["REMAIN_AMOUNT"].ToString() != "")
                            //    {
                            //        if (txtSettledAmount.Text != "")
                            //        {
                            //            lblAmountValidate.Text = (decimal.Parse(dsRemainingBalance.Tables[0].Rows[0]["REMAIN_AMOUNT"].ToString()) + decimal.Parse(txtSettledAmount.Text)).ToString();
                            //        }
                            //        else
                            //        {
                            //            lblAmountValidate.Text = ((decimal.Parse(dsRemainingBalance.Tables[0].Rows[0]["REMAIN_AMOUNT"].ToString()))).ToString();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        lblRemaingBalance.Text = lblTotalAmount.Text;
                            //        lblAmountValidate.Text = lblTotalAmount.Text;
                            //    }
                            //}
                            //else if (dsRemainingBalance.Tables[0].Rows[0]["SETTLED_AMOUNT"].ToString() == "")
                            //{
                            //    lblRemaingBalance.Text = lblTotalAmount.Text;
                            //    lblAmountValidate.Text = lblTotalAmount.Text;
                            //}
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upInvoiceGrid.Update();
            }
        }

        protected void fillDetailsEditMode()
        {
            try
            {
                DataSet ds = objPurchaseHeader.getAllInformationEditMode("GET_PURCHASE_HEADER_INFORMATION", Request.QueryString["IV"].ToString());
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in GridInvoice.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {

                            DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");



                            TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtInvoiceAmount");
                            // Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");
                            // Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");


                            //Label lblRemaingBalance = (Label)item.FindControl("lblRemaingBalance");
                            //Label lblAmountSettled = (Label)item.FindControl("lblAmountSettled");

                            TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");
                            Label lblTourName = (Label)item.FindControl("lblTourName");

                            drpInvoiceNo.Text = ds.Tables[0].Rows[j]["SALES_INVOICE_NO"].ToString();

                            txtInvoiceAmount.Text = ds.Tables[0].Rows[j]["SALES_AMOUNT"].ToString();
                            //lblTotalAmount.Text = ds.Tables[0].Rows[j]["TOTAL_PURCHASE_AMOUNT"].ToString();
                            txtSettledAmount.Text = ds.Tables[0].Rows[j]["SETTLED_AMOUNT"].ToString();

                            //lblTourName.Text = ds.Tables[0].Rows[j]["TOUR_SHORT_NAME"].ToString();

                            getInvoiceInformation(item.DataItemIndex);

                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        AddHotels(GridInvoice, upInvoiceGrid);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void Validation()
        {

            if (GridInvoice.Rows.Count > 1)
            {
                foreach (GridViewRow item in GridInvoice.Rows)
                {
                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");

                    foreach (GridViewRow item1 in GridInvoice.Rows)
                    {
                        if (item.DataItemIndex != item1.DataItemIndex)
                        {
                            DropDownList drpInvoiceNo1 = (DropDownList)item1.FindControl("drpInvoiceNo");

                            if (drpInvoiceNo.Text == drpInvoiceNo1.Text)
                            {
                                drpInvoiceNo1.Text = "";
                                Master.DisplayMessage("Same Sales Invoice not allowed to select.", "successMessage", 5000);
                                break;
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {

            }
        }

        protected void updatePurchaseInvoiceStatus(string Invoice_no)
        {
            objPurchaseHeader.updatePurchaseInvoiceStatus("UPDATE_PURCHASE_INVOICE_STATUS", Invoice_no, drpsupplier.Text);
        }

        protected void InsertAccountVoucherEntry()
        {
            DataSet dsGlDescription = objPurchaseHeader.getGlDesription("GET_GL_DESCRIPTION", drpsupplier.Text);
            DataSet dsVoucherType = objPurchaseHeader.commonSp("FETCH_VOUCHER_TYPE");
            DataSet dsInvoiceNo = objPurchaseHeader.commonSp("GET_MAX_PURCHASE_INVOICE_NO");
            DataSet dsAllGlCode = objPurchaseHeader.fetch_all_gl_code();
            objPurchaseHeader.insertAccountEntry(0, dsGlDescription.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dsInvoiceNo.Tables[0].Rows[0]["INVOICE_NO"].ToString(), txtgl_date.Text, dsVoucherType.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), txtNarration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "APPROVED", 0, lblsettleamt.Text, lblsettleamt.Text, int.Parse(Session["CompanyId"].ToString()), "THB", 1);
            objPurchaseHeader.insertAccountEntry(0, dsGlDescription.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dsInvoiceNo.Tables[0].Rows[0]["INVOICE_NO"].ToString(), txtgl_date.Text, dsVoucherType.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), txtNarration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "APPROVED", 0, lblsettleamt.Text, "", int.Parse(Session["CompanyId"].ToString()), "THB", 2);
            objPurchaseHeader.insertAccountEntry(0, getProductGlCode(), dsInvoiceNo.Tables[0].Rows[0]["INVOICE_NO"].ToString(), txtgl_date.Text, dsVoucherType.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), txtNarration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "APPROVED", 0, "", lblsettleamt.Text, int.Parse(Session["CompanyId"].ToString()), "THB", 2);
        }

        protected string getProductGlCode()
        {
            string glDescription = "";
            try
            {
                DataSet ds_sup_type = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                DataSet dsAllGlCode = objPurchaseHeader.fetch_all_gl_code();

                /*HOTEL*/
                if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
                {
                    glDescription = dsAllGlCode.Tables[0].Rows[2]["AutoSearchResult"].ToString();
                }
                /*SITE SEEING*/
                else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString())
                {
                    glDescription = dsAllGlCode.Tables[0].Rows[3]["AutoSearchResult"].ToString();
                }
                /*TRANSFER PACKAGE*/
                else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString())
                {
                    glDescription = dsAllGlCode.Tables[0].Rows[5]["AutoSearchResult"].ToString();
                }
                /*ADDITIONAL SERVICE*/
                else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString())
                {
                    glDescription = dsAllGlCode.Tables[0].Rows[10]["AutoSearchResult"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return glDescription;
        }

        protected void updateAccountVoucher()
        {
            DataSet ds = objPurchaseHeader.getAccountVouchersDeatils("GET_VOUCHERS_DETAILS", txtPurchaseVoucher.Text, "PURCHASE");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    objPurchaseHeader.updateAccountVoucherAmount(int.Parse(ds.Tables[0].Rows[0]["ACCOUNT_VOUCHER_ID"].ToString()), int.Parse(ds.Tables[0].Rows[0]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), lblsettleamt.Text, lblsettleamt.Text, lblsettleamt.Text, txtgl_date.Text, txtNarration.Text, 1);
                    objPurchaseHeader.updateAccountVoucherAmount(int.Parse(ds.Tables[0].Rows[0]["ACCOUNT_VOUCHER_ID"].ToString()), int.Parse(ds.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), lblsettleamt.Text, "", lblsettleamt.Text, txtgl_date.Text, txtNarration.Text, 2);
                }
                else if (i == 1)
                {
                    objPurchaseHeader.updateAccountVoucherAmount(int.Parse(ds.Tables[0].Rows[0]["ACCOUNT_VOUCHER_ID"].ToString()), int.Parse(ds.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", lblsettleamt.Text, lblsettleamt.Text, txtgl_date.Text, txtNarration.Text, 2);
                }
            }
        }

        protected void BlankAmountValidation()
        {
            foreach (GridViewRow item in GridInvoice.Rows)
            {
                TextBox txtSettledAmount = (TextBox)item.FindControl("txtSettledAmount");
                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpInvoiceNo");
                if (txtSettledAmount.Text == "")
                {
                    flagblankAmount = false;
                    ViewState["BkankAmountMsg"] = "Settled amount not allowed blank for Invoice no " + drpInvoiceNo.Text + "";
                    break;
                }
            }
        }

        protected void statusPosted()
        {

            btnSave.Visible = false;
            drpsupplier.Enabled = false;
            drpsupplier_type.Enabled = false;
            btnAdd.Visible = false;
            GridInvoice.Enabled = false;

        }

        protected void clearAll()
        {
            DataTable dt = new DataTable();

            GridInvoice.DataSource = dt;
            GridInvoice.DataBind();

            AddHotels(GridInvoice, upInvoiceGrid);
            lblsettleamt.Text = "";

            drpsupplier_type.Text = "";
            row1_drp_glcode.Text = "";
            row2_drp_glcode.Text = "";
            drpsupplier.Text = "";
            lbl_row1.Text = "";
            row1_txt_credit.Text = "";
            row2_txt_debit.Text = "";
            row1_txt_credit.Text = "";

            updategrid.Update();
            upInvoiceGrid.Update();
            UpdatePanel1.Update();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void clearGrid()
        {
            DataTable dt = new DataTable();

            GridInvoice.DataSource = dt;
            GridInvoice.DataBind();

            AddHotels(GridInvoice, upInvoiceGrid);
            lblsettleamt.Text = "";

            upInvoiceGrid.Update();
        }




    }
}