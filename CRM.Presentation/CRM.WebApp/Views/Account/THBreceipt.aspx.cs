using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.FIT;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.Account
{
    public partial class THBreceipt : System.Web.UI.Page
    {
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        THBReceiptDA objthbreceipt = new THBReceiptDA();

        bool flg_on_acc = true;
        bool flag_date = true;
        bool flag_amount = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                {
                    DataSet ds8 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_AGENT_COMPANY_NAME");
                    
                    binddropdownlist(drpagent_company_name, ds8);

                    DataSet ds_vt = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds_vt);


                    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpvoucher_type.Items[i];
                        if (item.ToString() == "RECEIPT")
                        {

                        }
                        else
                        {
                            drpvoucher_type.Items.Remove(item);
                        }
                    }

                    DataSet ds3 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_PAYMENT_MODE");
                    binddropdownlist(drppayment_mode, ds3);

                    for (int i = drppayment_mode.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drppayment_mode.Items[i];
                        if (item.ToString() == "CASH" || item.ToString() == "CREDIT CARD" || item.ToString() == "TRANSFER")
                        {

                        }
                        else
                        {
                            drppayment_mode.Items.Remove(item);
                        }
                    }

                    // DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE");
                    //DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FECH_COMPANYS_BANK");
                    DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FECH_BANK_FOR_RECEIPT");
                    binddropdownlist(drp_gl_code, ds5);

                    DataSet ds4 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds4);

                    //drp_currency.Text = "USD";
                    drp_currency.Enabled = false;
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpvoucher_status, ds1);

                    // DATE LABEL
                    string result;
                    System.DateTime today = DateTime.Now;
                    today.ToString("MM-dd-yyyy");
                    string source = today.ToString();
                    string str1 = source;
                    string[] w = str1.Split('/');

                    string t = w[2];
                    string[] t1 = t.Split(' ');

                    if (w[1] == "1" || w[1] == "2" || w[1] == "3" || w[1] == "4" || w[1] == "5" || w[1] == "6" || w[1] == "7" || w[1] == "8" || w[1] == "9")
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = "0" + w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                        else
                        {
                            result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                    }
                    else
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                        else
                        {
                            result = w[1] + "/" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                    }


                    DataSet ds_for_edit = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_RECEIPT_VOUCHER_DETAILS", Request["VN"].ToString());

                    ViewState["RowCount"] = ds_for_edit.Tables[0].Rows.Count - 2;

                    drpagent_company_name.Text = ds_for_edit.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    DataSet ds12 = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);
                    if (ds12.Tables[0].Rows.Count != 0)
                    {
                        ViewState["cust_rel_no"] = ds12.Tables[0].Rows[0]["CUST_ID"].ToString();

                        ViewState["cust_sr_no"] = ds12.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    }
                    txtgl_date.Text = ds_for_edit.Tables[0].Rows[0]["GL_DATE"].ToString();

                    lbl_voucher_no.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_NO"].ToString();

                    drpvoucher_type.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_TYPE"].ToString();

                    lbl_voucher_date.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_DATE"].ToString();

                    txt_payment_date.Text = ds_for_edit.Tables[0].Rows[0]["CHEQUE_DATE"].ToString();

                    drppayment_mode.Text = ds_for_edit.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();

                    if (ds_for_edit.Tables[0].Rows[0]["ON_ACCOUNT_RECEIPT"].ToString() == "True")
                    {
                        chk_onaccount.Checked = true;
                    }
                    chk_onaccount.Enabled = false;
                    if (drppayment_mode.Text == "CASH")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");

                        Label15.Text = "Cash Receipt Number";
                        txtcash_receipt.Text = ds_for_edit.Tables[0].Rows[0]["CASH_RECIEPT_NO"].ToString();
                        update_payments.Update();
                    }
                    else if (drppayment_mode.Text == "CREDIT CARD")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");

                        Label15.Text = "PayPal ID";
                        txtcash_receipt.Text = ds_for_edit.Tables[0].Rows[0]["CASH_RECIEPT_NO"].ToString();
                        update_payments.Update();
                    }
                    else if (drppayment_mode.Text == "TRANSFER")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        update_payments.Update();
                    }

                    drp_currency.Text = ds_for_edit.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    txt_amount.Text = ds_for_edit.Tables[0].Rows[0]["FOREX_AMOUNT"].ToString();

                    // VIEW STATE STORE PREVIUOS TOTAL AMOUNT OF VOUCHER 
                    ViewState["total_amount_old"] = txt_amount.Text;

                    ViewState["seqno"] = ds_for_edit.Tables[0].Rows[0]["SEQ_NO"].ToString();

                    txt_narration.Text = ds_for_edit.Tables[0].Rows[0]["NARRATION"].ToString();
                    drpvoucher_status.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                    fillReceipt(GridReceipt, upReceipt);
                    fillDetailsEditMode();

                    grid2_table.Visible = true;

                    DataSet ds11 = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);
                    DataSet ds6 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds11.Tables[0].Rows[0]["CUST_ID"].ToString(), ds11.Tables[0].Rows[0]["FLAG"].ToString());
                    ViewState["row1_receipt_glcode"] = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                    lbl_gl_code.Text = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                    //lbl_gl_code.Text = ds_for_edit.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                    //ViewState["row1_receipt_glcode"] = lbl_gl_code.Text;

                    lbl_total_amount.Text = get_total_amount();

                    lbl_row1_credit.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                    lbl_row2_debit.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                    //lbl_row1_credit.Text = ds_for_edit.Tables[0].Rows[0]["FOREX_AMOUNT"].ToString();
                    //lbl_row2_debit.Text = ds_for_edit.Tables[0].Rows[0]["FOREX_AMOUNT"].ToString();

                    int count = ds_for_edit.Tables[0].Rows.Count;

                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[count -1 ]["GL_DESCRIPTION"].ToString();

                    //if (lbl_voucher_no.Text != "")
                    //{
                        //btnSave.Visible = false;
                        //btnAdd.Visible = false;
                        //GridReceipt.Enabled = false;
                    //}

                    upReceipt.Update();
                }

                else
                {


                    DataSet ds8 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_AGENT_COMPANY_NAME");
                    binddropdownlist(drpagent_company_name, ds8);

                    DataSet ds_vt = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds_vt);


                    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpvoucher_type.Items[i];
                        if (item.ToString() == "RECEIPT")
                        {

                        }
                        else
                        {
                            drpvoucher_type.Items.Remove(item);
                        }
                    }

                    drpvoucher_type.Text = "RECEIPT";

                    DataSet ds2 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_NAME");
                    binddropdownlist(drpbank_name, ds2);

                    DataSet ds3 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_PAYMENT_MODE");
                    binddropdownlist(drppayment_mode, ds3);

                    for (int i = drppayment_mode.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drppayment_mode.Items[i];
                        if (item.ToString() == "CASH" || item.ToString() == "CREDIT CARD" || item.ToString() == "TRANSFER")
                        {

                        }
                        else
                        {
                            drppayment_mode.Items.Remove(item);
                        }
                    }

                    // DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE");
                    //DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FECH_COMPANYS_BANK");
                    DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FECH_BANK_FOR_RECEIPT");
                    binddropdownlist(drp_gl_code, ds5);

                    DataSet ds4 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds4);

                    drp_currency.Text = "THB";
                    drp_currency.Enabled = false;
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpvoucher_status, ds1);

                    // DATE LABEL
                    string result;
                    System.DateTime today = DateTime.Now;
                    today.ToString("MM-dd-yyyy");
                    string source = today.ToString();
                    string str1 = source;
                    string[] w = str1.Split('/');

                    string t = w[2];
                    string[] t1 = t.Split(' ');

                    if (w[1] == "1" || w[1] == "2" || w[1] == "3" || w[1] == "4" || w[1] == "5" || w[1] == "6" || w[1] == "7" || w[1] == "8" || w[1] == "9")
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = "0" + w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                        else
                        {
                            result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                    }
                    else
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                        else
                        {
                            result = w[1] + "/" + w[0] + "/" + t1[0];
                            lbl_voucher_date.Text = result;
                        }
                    }
                    txtgl_date.Text = lbl_voucher_date.Text;

                    chk_onaccount.Checked = true;

                    fillReceipt(GridReceipt, upReceipt);

                    GridReceipt.Enabled = false;

                    btnAdd.Visible = false;
                }
            }
        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
           
        }
        #endregion

        #region ALL ADD BUTTONS
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddReceipt(GridReceipt, upReceipt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

        #region Add Reciept

        protected void AddReceipt(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();


                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                    Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                    TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                    Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");
                    Label lblDetailId = (Label)item.FindControl("lblDetailId");
                    Label lblOldAmount = (Label)item.FindControl("lblOldAmount");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("AmountSattled");
                        dt.Columns.Add("Baltobepaid");
                        dt.Columns.Add("DetailId");
                        dt.Columns.Add("lblOldAmount");

                    }

                    DataRow dr = dt.NewRow();

                    dr["InvoiceNo"] = drpinvoice.Text;
                    dr["Amount"] = lblAmountTHB.Text;
                    dr["AmountSattled"] = txtAmountSettled.Text;
                    dr["Baltobepaid"] = lblBalancetobepaid.Text;
                    dr["DetailId"] = lblDetailId.Text;
                    dr["lblOldAmount"] = lblOldAmount.Text;
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("AmountSattled");
                        dt.Columns.Add("Baltobepaid");
                        dt.Columns.Add("DetailId");
                        dt.Columns.Add("lblOldAmount");
                    }

                    DataRow dr = dt.NewRow();

                    dr["InvoiceNo"] = "";
                    dr["Amount"] = "";
                    dr["AmountSattled"] = "";
                    dr["Baltobepaid"] = "";
                    dr["DetailId"] = "";
                    dr["lblOldAmount"] = "";
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

                    DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                    Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                    TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                    Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");
                    Label lblDetailId = (Label)item.FindControl("lblDetailId");
                    Label lblOldAmount = (Label)item.FindControl("lblOldAmount");

                    Button btnHotelRemove = (Button)item.FindControl("btnHotelRemove");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            //binddropdownlist(drpHotelName, ds);

                            //drpHotelName.Text = dt.Rows[itm]["HotelName"].ToString();

                            if (drpagent_company_name.Text != "")
                            {
                                bindInvoiceFromSupplier(itm);
                                //DataSet ds = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);

                                //drpinvoice.Items.Clear();

                                //DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                                //binddropdownlist(drpinvoice, ds1);


                            }
                            else
                            {
                                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                                binddropdownlist(drpinvoice, ds7);
                            }


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

                            drpinvoice.Text = dt.Rows[itm]["InvoiceNo"].ToString();

                            lblAmountTHB.Text = dt.Rows[itm]["Amount"].ToString();
                            txtAmountSettled.Text = dt.Rows[itm]["AmountSattled"].ToString();
                            lblBalancetobepaid.Text = dt.Rows[itm]["Baltobepaid"].ToString();
                            lblDetailId.Text = dt.Rows[itm]["DetailId"].ToString();
                            lblOldAmount.Text = dt.Rows[itm]["lblOldAmount"].ToString();
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

        #endregion

        protected void fillReceipt(GridView gv, UpdatePanel up)
        {
            try
            {

                AddReceipt(gv, up);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #region REMOVE Reciept BUTTONS

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridReceipt, rowID);
                get_total_amount();
                lbl_total_amount.Text = get_total_amount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upReceipt.Update();
            }

        }
        #endregion

        protected void removeRow(GridView gv, int rowIndex)
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
                    DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                    Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                    TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                    Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");
                    Label lblDetailId = (Label)item.FindControl("lblDetailId");
                    Label lblOldAmount = (Label)item.FindControl("lblOldAmount");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("AmountSattled");
                        dt.Columns.Add("Baltobepaid");
                        dt.Columns.Add("DetailId");
                        dt.Columns.Add("lblOldAmount");
                    }

                    DataRow dr = dt.NewRow();

                    dr["InvoiceNo"] = drpinvoice.Text;
                    dr["Amount"] = lblAmountTHB.Text;
                    dr["AmountSattled"] = txtAmountSettled.Text;
                    dr["Baltobepaid"] = lblBalancetobepaid.Text;
                    dr["DetailId"] = lblDetailId.Text;
                    dr["lblOldAmount"] = lblOldAmount.Text;
                    dt.Rows.Add(dr);

                }

                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }

                    DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                    Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                    TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                    Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");
                    Label lblDetailId = (Label)item.FindControl("lblDetailId");
                    Label lblOldAmount = (Label)item.FindControl("lblOldAmount");


                    //binddropdownlist(drpHotelName, ds);
                    //drpHotelName.Text = dt.Rows[itm]["HotelName"].ToString();

                    if (drpagent_company_name.Text != "")
                    {
                        bindInvoiceFromSupplier(itm);
                        //DataSet ds = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);

                        //drpinvoice.Items.Clear();

                        //DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                        //binddropdownlist(drpinvoice, ds1);


                    }
                    else
                    {
                        DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                        binddropdownlist(drpinvoice, ds7);
                    }
                    drpinvoice.Text = dt.Rows[itm]["InvoiceNo"].ToString();

                    lblAmountTHB.Text = dt.Rows[itm]["Amount"].ToString();
                    txtAmountSettled.Text = dt.Rows[itm]["AmountSattled"].ToString();
                    lblBalancetobepaid.Text = dt.Rows[itm]["Baltobepaid"].ToString();
                    lblDetailId.Text = dt.Rows[itm]["DetailId"].ToString();
                    lblOldAmount.Text = dt.Rows[itm]["lblOldAmount"].ToString();
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

        protected void drpagent_company_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GridReceipt.Rows)
            {
                DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");

                if (drpagent_company_name.Text != "")
                {

                    DataSet ds = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);
                    ViewState["cust_rel_no"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
                    
                    drp_gl_code.Text = ds.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(); // CHANGE
                    ViewState["cust_sr_no"] = ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    drpinvoice.Items.Clear();

                    DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                    
                    binddropdownlist(drpinvoice, ds1);

                    DataSet ds6 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                    ViewState["row1_receipt_glcode"] = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();



                }
                else
                {
                    DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(drpinvoice, ds7);
                }
            }
            upReceipt.Update();
            update_bedit_select.Update();
        }

        protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GridReceipt.Rows)
            {
                DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");

                // clear_and_hide();
                if (drpvoucher_type.Text == "PAYMENT" || drpvoucher_type.Text == "RECEIPT")
                {

                    if (drpvoucher_type.Text == "RECEIPT")
                    {
                        DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                        binddropdownlist(drpinvoice, ds7);

                        DataSet ds8 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_AGENT_COMPANY_NAME");
                        binddropdownlist(drpagent_company_name, ds8);
                    }
                    else if (drpvoucher_type.Text == "PAYMENT")
                    {
                        drpagent_company_name.Items.Clear();
                        
                    }
                }
                update_voucher.Update();


            }
        }

        protected void drppayment_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_gl_code.Text == "Cash-in-hand" && drppayment_mode.Text != "CASH")
            {
                Master.DisplayMessage("Please, Select other Payment mode.", "successMessage", 5000);
                drppayment_mode.Text = "";
            }
            else if (drp_gl_code.Text != "Cash-in-hand" && drppayment_mode.Text == "CASH")
            {
                Master.DisplayMessage("Can not Select Payment mode then CASH.", "successMessage", 5000);
                drppayment_mode.Text = "";
            }
            else
            {

                if (drppayment_mode.Text == "CASH")
                {
                    cash_receipt_no_tr.Attributes.Add("style", "display");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    Label15.Text = "Cash Receipt Number";

                    update_payments.Update();
                }
                else if (drppayment_mode.Text == "CREDIT CARD")
                {
                    cash_receipt_no_tr.Attributes.Add("style", "display");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    Label15.Text = "PayPal ID";

                    update_payments.Update();
                }
                else if (drppayment_mode.Text == "TRANSFER")
                {
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display");




                }
            }
            update_payments.Update();
        }

        protected void txt_amount_TextChanged(object sender, EventArgs e)
        {

            grid2_table.Visible = true;

            lbl_gl_code.Text = ViewState["row1_receipt_glcode"].ToString();
            lbl_row1_credit.Text = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text));
            lbl_row2_debit.Text = lbl_row1_credit.Text;
            update_bedit_select.Update();
        }

        protected void row1_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = sender as DropDownList;
            int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
            Validation();

            if (ViewState["sameInvoice"] != null)
            {
                Master.DisplayMessage("Can not select Same Invoice in voucher.", "successMessage", 5000);
            }
            else
            {
                int indx = 0;
                foreach (GridViewRow item in GridReceipt.Rows)
                {
                    
                    Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");

                    if (lblBalancetobepaid.Text == "0.00")
                    {
                        indx = indx + 1;
                    }

                    if (repeaterItemIndex == item.DataItemIndex)
                    {

                        DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                        Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                        TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                        
                        Label lblOldAmount = (Label)item.FindControl("lblOldAmount");

                        if (drpvoucher_type.Text == "RECEIPT")
                        {

                            string status = on_account_status(drpinvoice.Text);

                            on_account_invoice_select(drpinvoice.Text, repeaterItemIndex - indx);

                            if (ViewState["invoice_check"] != null)
                            {
                                if (drpinvoice.Text != ViewState["invoice_check"].ToString())
                                {
                                    flg_on_acc = false;
                                }
                            }

                            if (txt_amount.Text == "")
                            {
                                Master.DisplayMessage("First enter amount.", "successMessage", 5000);
                                drpinvoice.Text = "";
                            }


                            //else if (status == "POSTED")
                            //{
                            //    Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                            //    drpinvoice.Text = "";
                            //}
                            //else if (flg_on_acc == false)
                            //{
                            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
                            //    drpinvoice.Text = "";
                            //}
                            else
                            {


                                ViewState["row1"] = "done";

                                DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drpinvoice.Text);


                                lblAmountTHB.Text = ds.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();



                                DataSet ds6 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                                ViewState["row1_receipt_glcode"] = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                                lblBalancetobepaid.Text = get_balance_be_paid(lblAmountTHB.Text, drpinvoice.Text);


                                if (txtAmountSettled.Text != "")
                                {
                                    lblOldAmount.Text = (decimal.Parse(lblBalancetobepaid.Text) + decimal.Parse(txtAmountSettled.Text)).ToString();
                                }
                                else
                                {
                                    lblOldAmount.Text = lblBalancetobepaid.Text;
                                }

                            }
                        }
                        upReceipt.Update();
                    }
                }
            }

        }

        public string on_account_status(string invoice_no)
        {
            DataSet ds = objVouchersStoredProcedure.get_invoice_status("CHECK_INVOICE_POSTED", invoice_no);

            return ds.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
        }

        public void on_account_invoice_select(string invoice_no, int srno)
        {
            if (chk_onaccount.Checked == true)
            {
                DataSet ds1 = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);

                DataSet ds = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB_NOT_POST", int.Parse(ds1.Tables[0].Rows[0]["USER_ID"].ToString()));

                //if (ViewState["RowCount"] != null)
                //{
                //    if (int.Parse(ViewState["RowCount"].ToString()) - 1 <= srno)
                //    {
                //        srno = srno - int.Parse(ViewState["RowCount"].ToString()) - 1;
                //    }
                //}

                if (ds.Tables[0].Rows.Count > srno)
                {
                    ViewState["invoice_check"] = ds.Tables[0].Rows[srno]["AutoSearchResult"].ToString();

                }
                else
                {
                    ViewState["invoice_check"] = null;
                }


            }
            else
            {
                ViewState["invoice_check"] = null;
            }


        }

        protected void txt_payment_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txt_payment_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered date is not in valid format", "successMessage", 8000);
                flag_date = false;
            }
            update_payments.Update();
        }

        protected void txtgl_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtgl_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered GL Date is not in valid format", "successMessage", 8000);
                flag_date = false;
            }
            update_voucher.Update();
        }

        #region HOW MANY AMOUNTS IS LEFT TO RECEIVE FOR INVOICE VISE
        public string get_balance_be_paid(String row1_lbl, String invoice_no)
        {
            decimal amount = decimal.Parse(row1_lbl);
            string lbl_be_paid;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_RECEIPT_VOUCHER_DETAILS", invoice_no);
            if (ds2.Tables[0].Rows.Count != 0)
            {

                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    if (drpvoucher_type.Text == "RECEIPT")
                    {
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString());
                    }
                    else if (drpvoucher_type.Text == "PAYMENT")
                    {
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString());
                    }
                }
                lbl_be_paid = amount.ToString();
            }
            else
            {
                lbl_be_paid = row1_lbl;
            }
            return lbl_be_paid;
        }

        public string get_balance_paid(String row1_lbl, String invoice_no)
        {
            decimal amount = decimal.Parse(row1_lbl);
            string lbl_be_paid;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_RECEIPT_VOUCHER_DETAILS", invoice_no);
            if (ds2.Tables[0].Rows.Count != 0)
            {

                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    if (drpvoucher_type.Text == "RECEIPT")
                    {
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString());
                    }
                    else if (drpvoucher_type.Text == "PAYMENT")
                    {
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString());
                    }
                }
                lbl_be_paid = amount.ToString();
            }
            else
            {
                lbl_be_paid = row1_lbl;
            }
            return lbl_be_paid;
        }
        #endregion

        #region GET TOTAL AMOUNT OF INVOICE THAT WE RECEIVE
        public string get_total_amount()
        {
            decimal total_amount = 0;
            foreach (GridViewRow item in GridReceipt.Rows)
            {

                TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");



                lbl_total_amount.Text = "";
                if (txtAmountSettled.Text == "")
                {
                    txtAmountSettled.Text = "0";
                }

                total_amount = total_amount + decimal.Parse(txtAmountSettled.Text);

                if (chk_onaccount.Checked == true)
                {

                }
                else
                {
                    //   txt_amount.Text = total_amount.ToString();
                    //   update_forex.Update();
                }

            }
            return total_amount.ToString();
        }
        #endregion

        #region GRID 2 GL CODE SELECTION
        protected void grid_amount()
        {

            if (drpvoucher_type.Text == "RECEIPT")
            {
                if (Request["VN"] == null)
                {
                    lbl_gl_code.Text = ViewState["row1_receipt_glcode"].ToString();
                }

                //   lbl_row1_credit.Text = lbl_total_amount.Text;
                //    lbl_row2_debit.Text = lbl_total_amount.Text;
                //pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {

            }
            update_forex.Update();
            upReceipt.Update();
            update_bedit_select.Update();

        }
        #endregion

        #region SAVE RECORDS
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lbl_total_amount.Text == "")
            {
                lbl_total_amount.Text = "0";
            }

            if (flag_date == false)
            {
                Master.DisplayMessage("Entered date is not in valid format.", "successMessage", 8000);
            }
            else if (txtgl_date.Text == "")
            {
                Master.DisplayMessage("GL Date is required.", "successMessage", 5000);
            }

            else if (flag_amount == false)
            {
                Master.DisplayMessage(ViewState["error_amount"].ToString(), "successMessage", 8000);
            }

            else if (drp_gl_code.Text == "")
            {
                Master.DisplayMessage("First entered Voucher's debit's General Leger Code.", "successMessage", 8000);
            }
            else if (txt_payment_date.Text == "")
            {
                Master.DisplayMessage("Payment date is required.", "successMessage", 8000);
            }

            else if (drp_currency.Text == "")
            {
                Master.DisplayMessage("First entered Currency type.", "successMessage", 8000);
            }
            else if (drppayment_mode.Text == "")
            {
                Master.DisplayMessage("First entered Payment's Details.", "successMessage", 8000);
            }

            else if (decimal.Parse(txt_amount.Text) < decimal.Parse(lbl_total_amount.Text))
            {
                Master.DisplayMessage("Can not enter more amount that is in amount field.", "successMessage", 8000);
            }

            else if (decimal.Parse(txt_amount.Text) != decimal.Parse(lbl_total_amount.Text) && chk_onaccount.Checked == false)
            {
                Master.DisplayMessage("Total Amount is not same as Total Setteled amount.", "successMessage", 8000);
            }
            else
            {

               // DataSet ds_for_edit = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_RECEIPT_VOUCHER_DETAILS", Request["VN"].ToString());



                if (drpvoucher_type.Text == "RECEIPT")
                {
                    if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                    {
                        updateVoucher();


                        if (decimal.Parse(txt_amount.Text) == decimal.Parse(lbl_total_amount.Text))
                        {
                            if(ViewState["seqno"] != null)
                            {
                                insert_payment_voucher_no(ViewState["seqno"].ToString());
                            }
                        }

                        Master.DisplayMessage("Voucher Updated Successfully.", "successMessage", 8000);

                    }
                    else
                    {
                        bool on_acc_flag = true;
                        if (chk_onaccount.Checked == false)
                        {
                            on_acc_flag = false;
                        }

                        string total_voucher_amount = (decimal.Parse(txt_amount.Text)).ToString();
                        string total_voucher_debit = (decimal.Parse(txt_amount.Text)).ToString();

                        
                        foreach (GridViewRow item in GridReceipt.Rows)
                        {
                            DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                            Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                            TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                            Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");

                            if (chk_onaccount.Checked == true)
                            {
                                string total_voucher_debit1 = (decimal.Parse(txt_amount.Text)).ToString();
                                string total_voucher_amount1 = (decimal.Parse(txt_amount.Text)).ToString();
                                objthbreceipt.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), drpinvoice.Text, total_voucher_amount1, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "APPROVED", 0, txtAmountSettled.Text, txtAmountSettled.Text, "", drppayment_mode.Text, txtcheque_no.Text, txt_payment_date.Text, drpbank_name.Text, drpbank_name.Text, "Remarks", txtcash_receipt.Text, txtcashreceipt_date.Text, "", txt_amount.Text, drp_currency.Text, int.Parse(ViewState["cust_rel_no"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 1, true);
                                objthbreceipt.insert_accounts_entry(0, "0", drp_gl_code.Text, "", txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "APPROVED", 0, txt_amount.Text, "", total_voucher_debit1, drppayment_mode.Text, txtcheque_no.Text, txt_payment_date.Text, drpbank_name.Text, drpbank_name.Text, "Remarks", txtcash_receipt.Text, txtcashreceipt_date.Text, "", txt_amount.Text, drp_currency.Text, int.Parse(ViewState["cust_rel_no"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2, true);

                                Response.Redirect("~/Views/Account/SearchreceiptVouchers.aspx");
                            }
                        }

                    }
                }

            }
        }

        #endregion

        #region GENERATING VOUCHER NO
        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String invoice_no)
        {
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

            DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

            DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", invoice_no, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

            if (ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
            {
                if (ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
                {
                    string str = DateTime.Today.ToString("dd/MM/yy");
                    string year = "";
                    string voucher_no = "";
                    string[] words1 = str.Split('/');
                    if (int.Parse(words1[1].ToString()) > 3)
                    {
                        year = words1[2].ToString() + (int.Parse(words1[2].ToString()) + 1).ToString();
                    }
                    else
                    {
                        year = (int.Parse(words1[2].ToString()) - 1).ToString() + words1[2].ToString();
                    }
                    voucher_no = "V" + year + "-" + ds_vn_code.Tables[0].Rows[0]["VOUCHER_NO_CODE"].ToString() + "-" + "00001";

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), voucher_no);
                }
                else
                {
                    string str = DateTime.Today.ToString("dd/MM/yy");
                    string year = "";
                    string voucher_no = "";
                    string[] words1 = str.Split('/');
                    if (int.Parse(words1[1].ToString()) > 3)
                    {
                        year = words1[2].ToString() + (int.Parse(words1[2].ToString()) + 1).ToString();
                    }
                    else
                    {
                        year = (int.Parse(words1[2].ToString()) - 1).ToString() + words1[2].ToString();
                    }

                    string str11 = ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString(); // ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                    string[] words = str11.Split('-');
                    string no = (int.Parse(words[2].ToString()) + 01).ToString();
                    int len = no.Length;
                    for (int i = 0; i < 5 - len; i++)
                    {
                        no = "0" + no;
                    }
                    voucher_no = "V" + year + "-" + ds_vn_code.Tables[0].Rows[0]["VOUCHER_NO_CODE"].ToString() + "-" + no;

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), voucher_no);
                }

            }
        }
        #endregion

        #region INSERT VOUCHER FOR PAYMENTS
        protected void insert_payment_voucher_no(string seq_no)
        {
            bool flag = true;
            DataSet ds = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("GET_RECEIPT_VOUCHER_DETAILS_FOR_VOUCHER_NO", seq_no);

            if (chk_onaccount.Checked == false)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == "" || ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == null)
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag == true)
            {
                DataSet ds1 = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("CHECK_VOUCHER_NO_FOR_SALES_RECEIPT", seq_no);

                if (ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
                {
                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[8]["AutoSearchResult"].ToString());
                    DataSet ds_vn = objVouchersStoredProcedure.fetch_voucher_type("FETCH_MAX_VOUCHER_NO_FROM_SALES_RECEIPT");
                    DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                    if (ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
                    {
                        string str = DateTime.Today.ToString("dd/MM/yy");
                        string year = "";
                        string voucher_no = "";
                        string[] words1 = str.Split('/');
                        if (int.Parse(words1[1].ToString()) > 3)
                        {
                            year = words1[2].ToString() + (int.Parse(words1[2].ToString()) + 1).ToString();
                        }
                        else
                        {
                            year = (int.Parse(words1[2].ToString()) - 1).ToString() + words1[2].ToString();
                        }
                        voucher_no = "V" + year + "-" + ds_vn_code.Tables[0].Rows[0]["VOUCHER_NO_CODE"].ToString() + "-" + "00001";

                        objVouchersStoredProcedure.insert_accounts_voucher_no_sales_receipt(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seq_no, voucher_no);
                    }
                    else
                    {
                        string str = DateTime.Today.ToString("dd/MM/yy");
                        string year = "";
                        string voucher_no = "";
                        string[] words1 = str.Split('/');
                        if (int.Parse(words1[1].ToString()) > 3)
                        {
                            year = words1[2].ToString() + (int.Parse(words1[2].ToString()) + 1).ToString();
                        }
                        else
                        {
                            year = (int.Parse(words1[2].ToString()) - 1).ToString() + words1[2].ToString();
                        }

                        string str11 = ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString(); // ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                        string[] words = str11.Split('-');
                        string no = (int.Parse(words[2].ToString()) + 01).ToString();
                        int len = no.Length;
                        for (int i = 0; i < 5 - len; i++)
                        {
                            no = "0" + no;
                        }
                        voucher_no = "V" + year + "-" + ds_vn_code.Tables[0].Rows[0]["VOUCHER_NO_CODE"].ToString() + "-" + no;
                        objVouchersStoredProcedure.insert_accounts_voucher_no_sales_receipt(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seq_no, voucher_no);
                    }

                }
            }
        }
        #endregion
        #endregion

   
        #region FILL DETAILS WHILE EDIT MODE

        protected void fillDetailsEditMode()
        {
            fillHotelsEditMode(GridReceipt, upReceipt);
        }

        protected void fillHotelsEditMode(GridView gv, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_RECEIPT_VOUCHER_DETAILS", Request["VN"].ToString());
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {
                            if (ds.Tables[0].Rows[j]["INVOICE_NO"].ToString() != "")
                            {
                                DropDownList drpinvoice = (DropDownList)item.FindControl("drpinvoice");
                                Label lblAmountTHB = (Label)item.FindControl("lblAmountTHB");
                                TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                                Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");

                                Label lblDetailId = (Label)item.FindControl("lblDetailId");

                                Label lblOldAmount = (Label)item.FindControl("lblOldAmount");

                                lblDetailId.Text = ds.Tables[0].Rows[j]["SALES_RECEIPT_VOUCHER_DETAIL_ID"].ToString();

                                drpinvoice.Text = ds.Tables[0].Rows[j]["INVOICE_NO"].ToString();

                                DataSet ds1 = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drpinvoice.Text);


                                lblAmountTHB.Text = ds1.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                                //lblAmountTHB.Text = ds.Tables[0].Rows[j]["VOUCHER_AMOUNT"].ToString();

                                txtAmountSettled.Text = ds.Tables[0].Rows[j]["DETAILS_FOREX"].ToString();

                                lblBalancetobepaid.Text = get_balance_be_paid(lblAmountTHB.Text, drpinvoice.Text);

                                lblOldAmount.Text = (decimal.Parse(lblBalancetobepaid.Text) + decimal.Parse(txtAmountSettled.Text)).ToString();
                            }

                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        AddReceipt(gv, uppanel);
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

        #endregion

        protected void updateVoucher()
        {
            foreach (GridViewRow item in GridReceipt.Rows)
            {
                objVouchersStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), "", txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse("0"), "", "", "", drppayment_mode.Text, txtcheque_no.Text, txt_payment_date.Text, drpbank_name.Text, drpbank_name.Text, "Remarks", txtcash_receipt.Text, txtcashreceipt_date.Text, "1", txt_amount.Text, drp_currency.Text, txtgl_date.Text, 1);
                DataSet ds_for_edit = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_RECEIPT_VOUCHER_DETAILS", Request["VN"].ToString());
                
                bool flag = true;

                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpinvoice");
                TextBox txtAmountSettled = (TextBox)item.FindControl("txtAmountSettled");
                Label lblBalancetobepaid = (Label)item.FindControl("lblBalancetobepaid");
                Label lblDetailId = (Label)item.FindControl("lblDetailId");
                Label  lblOldAmount = (Label)item.FindControl("lblOldAmount");
                if (drpInvoiceNo.Text != "")
                {
                    for (int i = 0; i < ds_for_edit.Tables[0].Rows.Count; i++)
                    {
                        if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == drpInvoiceNo.Text)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag == false)
                    {
                        objVouchersStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), drpInvoiceNo.Text, txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(lblDetailId.Text), txtAmountSettled.Text, txtAmountSettled.Text, "", drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbank_name.Text, "Remarks", txtcash_receipt.Text, txtcashreceipt_date.Text, "1", txt_amount.Text, drp_currency.Text, txtgl_date.Text, 2);

                        if (decimal.Parse(txtAmountSettled.Text) == decimal.Parse(lblOldAmount.Text))
                        {
                            
                            insert_voucher_no(drpInvoiceNo.Text);
                        }

                    }
                    else
                    {
                        objthbreceipt.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), drpInvoiceNo.Text, txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, txtAmountSettled.Text, txtAmountSettled.Text, "", drppayment_mode.Text, txtcheque_no.Text, txt_payment_date.Text, drpbank_name.Text, drpbank_name.Text, "Remarks", txtcash_receipt.Text, txtcashreceipt_date.Text, "", txt_amount.Text, drp_currency.Text, int.Parse(ViewState["cust_rel_no"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, true, 2, true);
                        if (decimal.Parse(txtAmountSettled.Text) == decimal.Parse(lblOldAmount.Text))
                        {
                            insert_voucher_no(drpInvoiceNo.Text);
                        }
                    }
                }
            }
        }

        protected void txtSettledAmount_TextChanged(object sender, EventArgs e)
        {
            TextBox txtAmount = sender as TextBox;
            int repeaterItemIndex = ((GridViewRow)txtAmount.NamingContainer).DataItemIndex;

            bool amntflag = true;

            foreach (GridViewRow item in GridReceipt.Rows)
            {
                if (item.DataItemIndex == repeaterItemIndex)
                {
                    Label lblAmountValidate = (Label)item.FindControl("lblOldAmount");
                    TextBox txtSettledAmount = (TextBox)item.FindControl("txtAmountSettled");

                    if (lblAmountValidate.Text != "")
                    {
                        if (decimal.Parse(txtSettledAmount.Text) > decimal.Parse(lblAmountValidate.Text))
                        {
                            Master.DisplayMessage("Entered Sattle amount higher then actual amount.", "successMessage", 8000);
                            txtSettledAmount.Text = "";
                            amntflag = false;
                            upReceipt.Update();
                        }
                    }
                }
            }

            if (amntflag == true)
            {
                decimal amount = 0;
                foreach (GridViewRow item in GridReceipt.Rows)
                {

                    TextBox txtInvoiceAmount = (TextBox)item.FindControl("txtAmountSettled");

                    if (txtInvoiceAmount.Text != "")
                    {
                        amount += Convert.ToDecimal(txtInvoiceAmount.Text);

                        lbl_total_amount.Text = amount.ToString();


                    }

                }
                btnAdd.Focus();
                upReceipt.Update();
            }
        }


        public void bindInvoiceFromSupplier(int Index)
        {

            try
            {

                foreach (GridViewRow item in GridReceipt.Rows)
                {
                    DataSet ds = objVouchersStoredProcedure.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpagent_company_name.Text);

                    //drpinvoice.Items.Clear();

                    if (Index == item.DataItemIndex)
                    {
                        if (ViewState["RowCount"] != null)
                        {
                            if (Index <= int.Parse(ViewState["RowCount"].ToString()))
                            {
                                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpinvoice");
                                drpInvoiceNo.Enabled = false;

                                DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                              
                                //DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE", drpsupplier_type.Text, drpsupplier.Text);
                                binddropdownlist(drpInvoiceNo, ds1);
                            }
                            else
                            {
                                DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpinvoice");
                                DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB_NOT_POST", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                                binddropdownlist(drpInvoiceNo, ds1);
                                //DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE_NOT_POST", drpsupplier_type.Text, drpsupplier.Text);
                                
                            }
                        }
                        else
                        {
                            DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpinvoice");
                            DataSet ds1 = objVouchersStoredProcedure.get_invoice_left("GET_INVOICE_NO_THB_NOT_POST", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                            binddropdownlist(drpInvoiceNo, ds1);
                           // DataSet ds = objPurchaseHeader.getInvoiceNo("GET_INVOICE_NO_FOR_PURCHASE_NOT_POST", drpsupplier_type.Text, drpsupplier.Text);
                            
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
                upReceipt.Update();
            }
        }

        protected void Validation()
        {
           
            ViewState["sameInvoice"] = null;
            if (GridReceipt.Rows.Count > 1)
            {
                foreach (GridViewRow item in GridReceipt.Rows)
                {
                    DropDownList drpInvoiceNo = (DropDownList)item.FindControl("drpinvoice");

                    foreach (GridViewRow item1 in GridReceipt.Rows)
                    {
                        if (item.DataItemIndex != item1.DataItemIndex)
                        {
                            DropDownList drpInvoiceNo1 = (DropDownList)item1.FindControl("drpinvoice");

                            if (drpInvoiceNo.Text == drpInvoiceNo1.Text)
                            {
                                drpInvoiceNo1.Text = "";
                                upReceipt.Update();
                                ViewState["sameInvoice"] = "Same Invoice";
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
    }
}