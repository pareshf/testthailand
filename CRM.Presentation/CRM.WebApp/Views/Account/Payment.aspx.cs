﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using CRM.DataAccess.FIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.WebApp.Views.Account
{
    public partial class Payment : System.Web.UI.Page
    {
        PaymentStoredProcedure ObjPaymentStoredProcedure = new PaymentStoredProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        #region VARIABLES

        bool flg_on_acc = true;
        bool flag_date = true;
        bool flag_amount = true;

        #endregion

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 234);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (!IsPostBack)
            {
                if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                {
                    DataSet ds_vt = ObjPaymentStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds_vt);

                    DataSet ds = ObjPaymentStoredProcedure.fetch_voucher_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, ds);

                    //DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_OUTWARD_BANK");
                    DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_FOR_PAYMENT");
                    binddropdownlist(drp_gl_code, ds5);

                    DataSet ds51 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpvoucher_status, ds51);

                    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpvoucher_type.Items[i];
                        if (item.ToString() == "PAYMENT")
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

                    // PART PAYMENT
                    DataSet ds_part_payment = ObjPaymentStoredProcedure.fetch_invoice_description(Request["VN"].ToString());

                    if (ds_part_payment.Tables[0].Rows.Count > 0)
                    {
                        ViewState["PartPayments"] = "true";
                        drpsupplier_type.Text = ds_part_payment.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();

                        DataSet ds12 = ObjPaymentStoredProcedure.fetch_supplier_names("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
                        binddropdownlist(drpsupplier, ds12);

                        txtgl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        drpvoucher_type.Text = "PAYMENT";

                        lbl_voucher_date.Text = DateTime.Now.ToString("dd/MM/yyyy");


                        drpsupplier_type.Text = ds_part_payment.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();

                        drpsupplier.Text = ds_part_payment.Tables[0].Rows[0]["CHAIN_NAME"].ToString();

                        chk_onaccount.Enabled = false;

                       
                                DataSet ds7 = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO", drpsupplier.Text, drpsupplier_type.Text);
                                binddropdownlist(row1_drp_invoice, ds7);


                                DataSet ds6 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                                binddropdownlist(row1_drp_status, ds6);

                                row1_drp_invoice.Text = Request["VN"].ToString();
                                row1_drp_invoice.Enabled = false;

                                DataSet ds1 = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row1_drp_invoice.Text);
                                row1_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                row1_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                row1_drp_status.Text = ds1.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();

                                row1_txt_received.Text = "0";

                              
                                row1_lbl_bal_paid.Text = get_balance_be_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);

                        

                                DataSet dss = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_SUPPLIER", drpsupplier.Text, drpsupplier_type.Text);
                                DataSet ds66 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dss.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");
                                ViewState["row1_receipt_glcode"] = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                                lbl_gl_code.Text = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                                if ((row1_lbl_amount.Text + " " + row1_lbl_currency.Text) != row1_lbl_bal_paid.Text)
                                {
                                    row1_btn_view.Attributes.Add("style", "display");
                                }
                            }
                 

                    // NORMAL EDIT MODE
                    else
                    {

                        ViewState["PartPayments"] = "false";

                        DataSet ds_for_edit = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_PAYMENT_VOUCHER_DETAILS", Request["VN"].ToString());

                        drpsupplier_type.Text = ds_for_edit.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();

                        DataSet ds12 = ObjPaymentStoredProcedure.fetch_supplier_names("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
                        binddropdownlist(drpsupplier, ds12);

                        txtgl_date.Text = ds_for_edit.Tables[0].Rows[0]["GL_DATE"].ToString();

                        lbl_voucher_no.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_NO"].ToString();

                        drpvoucher_type.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_TYPE"].ToString();

                        lbl_voucher_date.Text = ds_for_edit.Tables[0].Rows[0]["DATE"].ToString();

                        txt_payment_date.Text = ds_for_edit.Tables[0].Rows[0]["PAY_DATE"].ToString();

                        drppayment_mode.Text = ds_for_edit.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();


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

                            Label15.Text = "E-Mail Address";
                            txtcash_receipt.Text = ds_for_edit.Tables[0].Rows[0]["CASH_RECIEPT_NO"].ToString();
                            update_payments.Update();
                        }
                        else if (drppayment_mode.Text == "TRANSFER")
                        {
                            cash_receipt_no_tr.Attributes.Add("style", "display:none");
                            cash_receipt_date_tr.Attributes.Add("style", "display");



                            update_payments.Update();
                        }

                        txt_amount.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();

                        ViewState["total_amount_old"] = txt_amount.Text;


                        txt_narration.Text = ds_for_edit.Tables[0].Rows[0]["NARRATION"].ToString();

                        drpvoucher_status.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                        drpsupplier_type.Text = ds_for_edit.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();

                        drpsupplier.Text = ds_for_edit.Tables[0].Rows[0]["CHAIN_NAME"].ToString();


                        if (ds_for_edit.Tables[0].Rows[0]["ON_ACCOUNT_PAYMENT"].ToString() == "True")
                        {
                            chk_onaccount.Checked = true;
                        }
                        chk_onaccount.Enabled = false;

                        int count = ds_for_edit.Tables[0].Rows.Count;

                        ViewState["editModerows"] = count;

                        for (int i = 0; i < count; i++)
                        {
                            if (i == 0)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();

                                    //DataSet ds7 = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO", drpsupplier.Text, drpsupplier_type.Text);
                                    DataSet ds7 = getPurchaseInvoiceNo();
                                    binddropdownlist(row1_drp_invoice, ds7);
                                }
                                else
                                {
                                    row1_btn_view.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row1_drp_invoice, ds7);

                                    row1_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row1_drp_invoice.Enabled = false;

                                    //DataSet ds1 = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row1_drp_invoice.Text);
                                    DataSet ds1 = getInvoiceNoDecsription(row1_drp_invoice.Text);
                                    row1_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row1_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row1_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row1_previous_amount"] = row1_txt_received.Text;

                                    row1_lbl_bal_paid.Text = get_balance_be_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);

                                    ViewState["sales_details_id_1"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }
                            else if (i == 1)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row2.Attributes.Add("style", "display");
                                    row2_btn_view.Attributes.Add("style", "display");
                                    row2_btn_remove.Visible = false;
                                    row2_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row2_drp_invoice, ds7);

                                    row2_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row2_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row2_drp_invoice.Text);
                                    row2_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row2_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row2_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row2_previous_amount"] = row2_txt_received.Text;

                                    row2_lbl_bal_paid.Text = get_balance_be_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);

                                    ViewState["sales_details_id_2"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }

                            else if (i == 2)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row3.Attributes.Add("style", "display");
                                    row3_btn_view.Attributes.Add("style", "display");
                                    row3_btn_remove.Visible = false;
                                    row3_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row3_drp_invoice, ds7);

                                    row3_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row3_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row3_drp_invoice.Text);
                                    row3_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row3_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row3_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row3_previous_amount"] = row3_txt_received.Text;

                                    row3_lbl_bal_paid.Text = get_balance_be_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);

                                    ViewState["sales_details_id_3"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }

                            else if (i == 3)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row4.Attributes.Add("style", "display");
                                    row4_btn_remove.Visible = false;
                                    row4_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row4_drp_invoice, ds7);

                                    row4_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row4_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row4_drp_invoice.Text);
                                    row4_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row4_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row4_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row4_previous_amount"] = row4_txt_received.Text;

                                    row4_lbl_bal_paid.Text = get_balance_be_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);

                                    ViewState["sales_details_id_4"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }

                            else if (i == 4)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row5.Attributes.Add("style", "display");
                                    row5_btn_remove.Visible = false;
                                    row5_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display:none");
                                    btnadd6.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row5_drp_invoice, ds7);

                                    row5_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row5_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row5_drp_invoice.Text);
                                    row5_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row5_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row5_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row5_previous_amount"] = row5_txt_received.Text;

                                    row5_lbl_bal_paid.Text = get_balance_be_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);

                                    ViewState["sales_details_id_5"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }
                            else if (i == 5)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row6.Attributes.Add("style", "display");
                                    row6_btn_remove.Visible = false;
                                    row6_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display:none");
                                    btnadd6.Attributes.Add("style", "display:none");
                                    btnadd7.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row6_drp_invoice, ds7);

                                    row6_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row6_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row6_drp_invoice.Text);
                                    row6_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row6_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row6_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row6_previous_amount"] = row6_txt_received.Text;

                                    row6_lbl_bal_paid.Text = get_balance_be_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);

                                    ViewState["sales_details_id_6"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }
                            else if (i == 6)
                            {

                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row7.Attributes.Add("style", "display");
                                    row7_btn_remove.Visible = false;
                                    row7_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display:none");
                                    btnadd6.Attributes.Add("style", "display:none");
                                    btnadd7.Attributes.Add("style", "display:none");
                                    btnadd8.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row7_drp_invoice, ds7);

                                    row7_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row7_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row7_drp_invoice.Text);
                                    row7_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row7_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row7_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row7_previous_amount"] = row7_txt_received.Text;

                                    row7_lbl_bal_paid.Text = get_balance_be_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);

                                    ViewState["sales_details_id_7"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }

                            else if (i == 7)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row8.Attributes.Add("style", "display");
                                    row8_btn_remove.Visible = false;
                                    row8_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display:none");
                                    btnadd6.Attributes.Add("style", "display:none");
                                    btnadd7.Attributes.Add("style", "display:none");
                                    btnadd8.Attributes.Add("style", "display:none");
                                    btnadd9.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row8_drp_invoice, ds7);

                                    row8_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row8_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row8_drp_invoice.Text);
                                    row8_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row8_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row8_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row8_previous_amount"] = row8_txt_received.Text;

                                    row8_lbl_bal_paid.Text = get_balance_be_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);

                                    ViewState["sales_details_id_8"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }

                            else if (i == 8)
                            {

                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row9.Attributes.Add("style", "display");
                                    row9_btn_remove.Visible = false;
                                    row9_btn_view.Visible = true;

                                    btnadd2.Attributes.Add("style", "display:none");
                                    btnadd3.Attributes.Add("style", "display:none");
                                    btnadd4.Attributes.Add("style", "display:none");
                                    btnadd5.Attributes.Add("style", "display:none");
                                    btnadd6.Attributes.Add("style", "display:none");
                                    btnadd7.Attributes.Add("style", "display:none");
                                    btnadd8.Attributes.Add("style", "display:none");
                                    btnadd9.Attributes.Add("style", "display:none");
                                    btnadd10.Attributes.Add("style", "display");

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row9_drp_invoice, ds7);

                                    row9_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row9_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row9_drp_invoice.Text);
                                    row9_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row9_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row9_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row9_previous_amount"] = row9_txt_received.Text;

                                    row9_lbl_bal_paid.Text = get_balance_be_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);

                                    ViewState["sales_details_id_9"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }
                            else if (i == 9)
                            {
                                if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() == "")
                                {
                                    drp_gl_code.Text = ds_for_edit.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                                    ViewState["sales_details_id_select"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                                else
                                {
                                    row10.Attributes.Add("style", "display");
                                    row10_btn_remove.Visible = false;
                                    row10_btn_view.Visible = true;

                                    
                                    DataSet ds7 = getAllPurchaseInvoiceNo();
                                    binddropdownlist(row10_drp_invoice, ds7);

                                    row10_drp_invoice.Text = ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString();
                                    row10_drp_invoice.Enabled = false;

                                    
                                    DataSet ds1 = getInvoiceNoDecsription(row10_drp_invoice.Text);
                                    row10_lbl_amount.Text = ds1.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                                    row10_lbl_currency.Text = ds1.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                                    
                                    row10_txt_received.Text = ds_for_edit.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    ViewState["row10_previous_amount"] = row9_txt_received.Text;

                                    row10_lbl_bal_paid.Text = get_balance_be_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);

                                    ViewState["sales_details_id_10"] = ds_for_edit.Tables[0].Rows[i]["PURCHASE_PAYMENT_VOUCHER_DETAIL_ID"].ToString();
                                }
                            }
                        }
                        grid2_table.Visible = true;

                        DataSet dss = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_SUPPLIER", drpsupplier.Text, drpsupplier_type.Text);
                        DataSet ds66 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dss.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");
                        ViewState["row1_receipt_glcode"] = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                        lbl_gl_code.Text = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                        
                        lbl_row1_debit.Text = ds_for_edit.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                        lbl_row2_credit.Text = ds_for_edit.Tables[0].Rows[0]["TOTAL_VOUCHER_AMOUNT"].ToString();
                        txtBankFees.Text = ds_for_edit.Tables[0].Rows[0]["BANK_CHARGES"].ToString();
                        lbl_total_amount.Text = get_total_amount();

                        if (lbl_voucher_no.Text != "")
                        {
                            diabled_controls();
                        }

                        updategrid.Update();
                    }
                }
                else
                {
                    ViewState["PartPayments"] = "true";

                    DataSet ds_vt = ObjPaymentStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds_vt);

                    DataSet ds = ObjPaymentStoredProcedure.fetch_voucher_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, ds);

                   // DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_OUTWARD_BANK");
                    DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_FOR_PAYMENT");
                    
                    binddropdownlist(drp_gl_code, ds5);

                    DataSet ds51 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpvoucher_status, ds51);


                    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpvoucher_type.Items[i];
                        if (item.ToString() == "RECEIPT" || item.ToString() == "PAYMENT")
                        {

                        }
                        else
                        {
                            drpvoucher_type.Items.Remove(item);
                        }
                    }

                    drpvoucher_type.Text = "PAYMENT";

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

                    if (chk_onaccount.Checked == true)
                    {
                        txt_amount.Enabled = true;
                        row1.Attributes.Add("style", "display:none");
                        btnadd2.Attributes.Add("style", "display:none");

                        clear_and_hide();
                    }
                    else
                    {
                        txt_amount.Enabled = false;
                        row1.Attributes.Add("style", "display");
                        btnadd2.Attributes.Add("style", "display");

                    }

                }
            }
        }

        protected void drpsupplier_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = ObjPaymentStoredProcedure.fetch_supplier_names("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
            binddropdownlist(drpsupplier, ds);

            update_voucher.Update();
        }

        protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED", drpsupplier.Text, drpsupplier_type.Text);
            DataSet ds = ObjPaymentStoredProcedure.getSupplierInvoice("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED_FOR_PAYMENT", drpsupplier.Text, drpsupplier_type.Text);
            binddropdownlist(row1_drp_invoice , ds);
            DataSet dss = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_SUPPLIER", drpsupplier.Text, drpsupplier_type.Text);
            DataSet ds66 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dss.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");
            ViewState["row1_receipt_glcode"] = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");

                lbl_gl_code.Text = dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                DataSet ds6 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");
                ViewState["row1_receipt_glcode"] = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row1_drp_status, ds7);
            }
            updategrid.Update();
            update_bedit_select.Update();
        }

        #region BIND DROP DOWN METHOD
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //  r.SelectedValue = "0";
        }
        #endregion


        #region ADD BUTTONS
        protected void btnadd2_Click(object sender, EventArgs e)
        {


            if (txt_amount.Text == "")
            {
                Master.DisplayMessage("First enter amount.", "successMessage", 5000);
                row1_drp_invoice.Text = "";
            }
            else
            {
                row2.Attributes.Add("style", "display");
                btnadd2.Attributes.Add("style", "display:none");
                btnadd3.Attributes.Add("style", "display");

                if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds6 = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED", drpsupplier.Text, drpsupplier_type.Text);
                    DataSet ds6 = getPurchaseInvoiceNo();
                    binddropdownlist(row2_drp_invoice, ds6);

                    DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(row2_drp_status, ds7);
                }
               
            }
            update_voucher.Update();
            updategrid.Update();
        }

        protected void btnadd3_Click(object sender, EventArgs e)
        {
            row3.Attributes.Add("style", "display");
            btnadd3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display");

            row2_btn_remove.Visible = false;
           
            if (drpvoucher_type.Text == "PAYMENT")
            {
                //DataSet ds6 = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED", drpsupplier.Text, drpsupplier_type.Text);
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row3_drp_invoice, ds6);

                
                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row3_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd4_Click(object sender, EventArgs e)
        {
            row4.Attributes.Add("style", "display");
            btnadd4.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display");

            row3_btn_remove.Visible = false;


            
            if (drpvoucher_type.Text == "PAYMENT")
            {
               
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row4_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row4_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd5_Click(object sender, EventArgs e)
        {
            row5.Attributes.Add("style", "display");
            btnadd5.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display");

            row4_btn_remove.Visible = false;
             if (drpvoucher_type.Text == "PAYMENT")
            {
              
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row5_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row5_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd6_Click(object sender, EventArgs e)
        {
            row6.Attributes.Add("style", "display");
            btnadd6.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display");

            row5_btn_remove.Visible = false;
            if (drpvoucher_type.Text == "PAYMENT")
            {
              
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row6_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row6_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd7_Click(object sender, EventArgs e)
        {
            row7.Attributes.Add("style", "display");
            btnadd7.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display");

            row6_btn_remove.Visible = false;
           if (drpvoucher_type.Text == "PAYMENT")
            {
              
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row7_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row7_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd8_Click(object sender, EventArgs e)
        {
            row8.Attributes.Add("style", "display");
            btnadd8.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display");

            row7_btn_remove.Visible = false;
             if (drpvoucher_type.Text == "PAYMENT")
            {
              
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row8_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row8_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd9_Click(object sender, EventArgs e)
        {
            row9.Attributes.Add("style", "display");
            btnadd9.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display");

            row8_btn_remove.Visible = false;


            if (drpvoucher_type.Text == "PAYMENT")
            {
              
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row9_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row9_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        protected void btnadd10_Click(object sender, EventArgs e)
        {
            row10.Attributes.Add("style", "display");
            btnadd10.Attributes.Add("style", "display:none");

            row9_btn_remove.Visible = false;
             if (drpvoucher_type.Text == "PAYMENT")
            {
             
                DataSet ds6 = getPurchaseInvoiceNo();
                binddropdownlist(row10_drp_invoice, ds6);

                DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(row10_drp_status, ds7);
            }
            update_voucher.Update();
            updategrid.Update();

        }

        public DataSet getPurchaseInvoiceNo()
        {
            DataSet ds6 = ObjPaymentStoredProcedure.getSupplierInvoice("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED_FOR_PAYMENT", drpsupplier.Text, drpsupplier_type.Text);
            return ds6;
        }

        public DataSet getAllPurchaseInvoiceNo()
        {
            DataSet ds6 = ObjPaymentStoredProcedure.getSupplierInvoice("FETCH_PURCHASE_INVOICE_NO_ALL_FOR_PAYMENT", drpsupplier.Text, drpsupplier_type.Text);
            return ds6;
        }



        #endregion

        #region ALL INVOICE DROPDOWN
        protected void row1_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");

            string status = on_account_status(row1_drp_invoice.Text);

            on_account_invoice_select(row1_drp_invoice.Text, 0);

            if (ViewState["invoice_check"] != null)
            {
                if (row1_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }
          
            if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row1_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row1_drp_invoice.Text = "";
            //}
            else if (txt_amount.Text == "")
            {
                Master.DisplayMessage("First enter amount.", "successMessage", 5000);
                row1_drp_invoice.Text = "";
            }
            else
            {
                ViewState["row1"] = "done";
                if (drpvoucher_type.Text == "PAYMENT")
                {
                    DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(row1_drp_status, ds7);
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row1_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row1_drp_invoice.Text);
                    row1_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row1_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    //row1_drp_status.Text = ds.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                    

                }
                row1_lbl_bal_paid.Text = get_balance_be_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row1_txt_received.Text = get_balance_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);

                    if (decimal.Parse(row1_txt_received.Text) > decimal.Parse(txt_amount.Text))
                    {
                        row1_txt_received.Text = (decimal.Parse(row1_txt_received.Text) - decimal.Parse(txt_amount.Text)).ToString();
                    }

                }
                else
                {

                    row1_txt_received.Text = get_balance_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row1_lbl_bal_paid.Text != row1_lbl_amount.Text + " " + row1_lbl_currency.Text)
                {
                    row1_btn_view.Attributes.Add("style", "display");
                  
                }
                else
                {
                    row1_btn_view.Attributes.Add("style", "display:none");
                    
                }
            }
            updategrid.Update();
        }

        protected void row2_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row2_drp_invoice.Text);

            on_account_invoice_select(row2_drp_invoice.Text, 1);

            if (ViewState["invoice_check"] != null)
            {
                if (row2_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row2_drp_invoice.Text == row1_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row2_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row2_drp_invoice.Text = "";
            }

            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row2_drp_invoice.Text = "";
            //}

            else
            {


                ViewState["row2"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row2_drp_status, ds1);

                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row2_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row2_drp_invoice.Text);
                    row2_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row2_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row2_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();

                    DataSet ds6 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                    ViewState["row2_receipt_glcode"] = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row2_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row2_drp_invoice.Text);
                    row2_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row2_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 //   row2_drp_status.Text = ds.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                 
                }
                row2_lbl_bal_paid.Text = get_balance_be_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row2_txt_received.Text = get_balance_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);

                    if (decimal.Parse(row2_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row2_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row2_txt_received.Text = get_balance_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row2_lbl_bal_paid.Text != row2_lbl_amount.Text + " " + row2_lbl_currency.Text)
                {
                    row2_btn_view.Visible = true;

                }
                else
                {
                    row2_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row3_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row3_drp_invoice.Text);

            on_account_invoice_select(row3_drp_invoice.Text, 2);

            if (ViewState["invoice_check"] != null)
            {
                if (row3_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row3_drp_invoice.Text == row1_drp_invoice.Text || row3_drp_invoice.Text == row2_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row3_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row3_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row3_drp_invoice.Text = "";
            //}
            else
            {

                ViewState["row3"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row3_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row3_drp_invoice.Text);
                    row3_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row3_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row3_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row3_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row3_drp_invoice.Text);
                    row3_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row3_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                  
                }
                row3_lbl_bal_paid.Text = get_balance_be_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row3_txt_received.Text = get_balance_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);

                    if (decimal.Parse(row3_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row3_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row3_txt_received.Text = get_balance_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row3_lbl_bal_paid.Text != row3_lbl_amount.Text + " " + row3_lbl_currency.Text)
                {
                    row3_btn_view.Visible = true;
                }
                else
                {
                    row3_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row4_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            string status = on_account_status(row4_drp_invoice.Text);

            on_account_invoice_select(row4_drp_invoice.Text, 3);

            if (ViewState["invoice_check"] != null)
            {
                if (row4_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row4_drp_invoice.Text == row1_drp_invoice.Text || row4_drp_invoice.Text == row2_drp_invoice.Text || row4_drp_invoice.Text == row3_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row4_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row4_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row4_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row4"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row4_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row4_drp_invoice.Text);
                    row4_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row4_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row4_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row4_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row4_drp_invoice.Text);
                    row4_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row4_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 
                }
                row4_lbl_bal_paid.Text = get_balance_be_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row4_txt_received.Text = get_balance_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);

                    if (decimal.Parse(row4_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row4_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row4_txt_received.Text = get_balance_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row4_lbl_bal_paid.Text != row4_lbl_amount.Text + " " + row4_lbl_currency.Text)
                {
                    row4_btn_view.Visible = true;
                }
                else
                {
                    row4_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row5_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row5_drp_invoice.Text);

            on_account_invoice_select(row5_drp_invoice.Text, 4);

            if (ViewState["invoice_check"] != null)
            {
                if (row5_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row5_drp_invoice.Text == row1_drp_invoice.Text || row5_drp_invoice.Text == row2_drp_invoice.Text || row5_drp_invoice.Text == row3_drp_invoice.Text || row5_drp_invoice.Text == row4_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row5_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row5_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row5_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row5"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row5_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row5_drp_invoice.Text);
                    row5_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row5_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();

                    row5_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row5_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row5_drp_invoice.Text);
                    row5_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row5_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 
                }
                row5_lbl_bal_paid.Text = get_balance_be_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row5_txt_received.Text = get_balance_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);

                    if (decimal.Parse(row5_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row5_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row5_txt_received.Text = get_balance_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row5_lbl_bal_paid.Text != row5_lbl_amount.Text + " " + row5_lbl_currency.Text)
                {
                    row5_btn_view.Visible = true;
                }
                else
                {
                    row5_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row6_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            string status = on_account_status(row6_drp_invoice.Text);

            on_account_invoice_select(row6_drp_invoice.Text, 5);

            if (ViewState["invoice_check"] != null)
            {
                if (row6_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row6_drp_invoice.Text == row1_drp_invoice.Text || row6_drp_invoice.Text == row2_drp_invoice.Text || row6_drp_invoice.Text == row3_drp_invoice.Text || row6_drp_invoice.Text == row4_drp_invoice.Text || row6_drp_invoice.Text == row5_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row6_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row6_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row6_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row6"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row6_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row6_drp_invoice.Text);
                    row6_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row6_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row6_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row6_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row6_drp_invoice.Text);
                    row6_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row6_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 
                }
                row6_lbl_bal_paid.Text = get_balance_be_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row6_txt_received.Text = get_balance_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);

                    if (decimal.Parse(row6_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row6_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row6_txt_received.Text = get_balance_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row6_lbl_bal_paid.Text != row6_lbl_amount.Text + " " + row6_lbl_currency.Text)
                {
                    row6_btn_view.Visible = true;
                }
                else
                {
                    row6_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row7_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            string status = on_account_status(row7_drp_invoice.Text);

            on_account_invoice_select(row7_drp_invoice.Text, 6);

            if (ViewState["invoice_check"] != null)
            {
                if (row7_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row7_drp_invoice.Text == row1_drp_invoice.Text || row7_drp_invoice.Text == row2_drp_invoice.Text || row7_drp_invoice.Text == row3_drp_invoice.Text || row7_drp_invoice.Text == row4_drp_invoice.Text || row7_drp_invoice.Text == row5_drp_invoice.Text || row7_drp_invoice.Text == row6_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row7_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row7_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row7_drp_invoice.Text = "";
            //}
            
            else
            {
                ViewState["row7"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row7_drp_status, ds1);

                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row7_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row7_drp_invoice.Text);
                    row7_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row7_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row7_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row7_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row7_drp_invoice.Text);
                    row7_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row7_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                //    row7_drp_status.Text = ds.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                   // ViewState["row7_payment_glcode"] = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                }
                row7_lbl_bal_paid.Text = get_balance_be_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row7_txt_received.Text = get_balance_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);

                    if (decimal.Parse(row7_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row7_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row7_txt_received.Text = get_balance_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row7_lbl_bal_paid.Text != row7_lbl_amount.Text + " " + row7_lbl_currency.Text)
                {
                    row7_btn_view.Visible = true;
                }
                else
                {
                    row7_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row8_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row8_drp_invoice.Text);

            on_account_invoice_select(row8_drp_invoice.Text, 7);

            if (ViewState["invoice_check"] != null)
            {
                if (row8_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row8_drp_invoice.Text == row1_drp_invoice.Text || row8_drp_invoice.Text == row2_drp_invoice.Text || row8_drp_invoice.Text == row3_drp_invoice.Text || row8_drp_invoice.Text == row4_drp_invoice.Text || row8_drp_invoice.Text == row5_drp_invoice.Text || row8_drp_invoice.Text == row6_drp_invoice.Text || row8_drp_invoice.Text == row7_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row8_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row8_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row8_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row8"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row8_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row8_drp_invoice.Text);
                    row8_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row8_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row8_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row8_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row8_drp_invoice.Text);
                    row8_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row8_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 
                }
                row8_lbl_bal_paid.Text = get_balance_be_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row8_txt_received.Text = get_balance_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);

                    if (decimal.Parse(row8_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row8_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row8_txt_received.Text = get_balance_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row8_lbl_bal_paid.Text != row8_lbl_amount.Text + " " + row8_lbl_currency.Text)
                {
                    row8_btn_view.Visible = true;
                }
                else
                {
                    row8_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row9_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row9_drp_invoice.Text);

            on_account_invoice_select(row9_drp_invoice.Text, 8);

            if (ViewState["invoice_check"] != null)
            {
                if (row9_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row9_drp_invoice.Text == row1_drp_invoice.Text || row9_drp_invoice.Text == row2_drp_invoice.Text || row9_drp_invoice.Text == row3_drp_invoice.Text || row9_drp_invoice.Text == row4_drp_invoice.Text || row9_drp_invoice.Text == row5_drp_invoice.Text || row9_drp_invoice.Text == row6_drp_invoice.Text || row9_drp_invoice.Text == row7_drp_invoice.Text || row9_drp_invoice.Text == row8_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row9_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row9_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row9_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row9"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row9_drp_status, ds1);

                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row9_drp_invoice.Text);
                    row9_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row9_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row9_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row9_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row9_drp_invoice.Text);
                    row9_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row9_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                 
                }
                row9_lbl_bal_paid.Text = get_balance_be_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row9_txt_received.Text = get_balance_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);

                    if (decimal.Parse(row9_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row9_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row9_txt_received.Text = get_balance_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row9_lbl_bal_paid.Text != row9_lbl_amount.Text + " " + row9_lbl_currency.Text)
                {
                    row9_btn_view.Visible = true;
                }
                else
                {
                    row9_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }

        protected void row10_drp_invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = on_account_status(row10_drp_invoice.Text);

            on_account_invoice_select(row10_drp_invoice.Text, 9);

            if (ViewState["invoice_check"] != null)
            {
                if (row10_drp_invoice.Text != ViewState["invoice_check"].ToString())
                {
                    flg_on_acc = false;
                }
            }

            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            if (row10_drp_invoice.Text == row1_drp_invoice.Text || row10_drp_invoice.Text == row2_drp_invoice.Text || row10_drp_invoice.Text == row3_drp_invoice.Text || row10_drp_invoice.Text == row4_drp_invoice.Text || row10_drp_invoice.Text == row5_drp_invoice.Text || row10_drp_invoice.Text == row6_drp_invoice.Text || row10_drp_invoice.Text == row7_drp_invoice.Text || row10_drp_invoice.Text == row8_drp_invoice.Text || row10_drp_invoice.Text == row9_drp_invoice.Text)
            {
                Master.DisplayMessage("Can not select same invoice in a voucher", "successMessage", 5000);
                row10_drp_invoice.Text = "";
            }
            else if (status == "POSTED")
            {
                Master.DisplayMessage("Invoice is already Posted.", "successMessage", 5000);
                row10_drp_invoice.Text = "";
            }
            //else if (flg_on_acc == false)
            //{
            //    Master.DisplayMessage("First Selct old Invoice to Setteled.", "successMessage", 5000);
            //    row10_drp_invoice.Text = "";
            //}

            else
            {
                ViewState["row10"] = "done";
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds1 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ORDER_STATUS");
                    binddropdownlist(row10_drp_status, ds1);
                    DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", row10_drp_invoice.Text);
                    row10_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row10_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    row10_drp_status.Text = ds.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    //DataSet ds = objVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", row10_drp_invoice.Text);
                    DataSet ds = getInvoiceNoDecsription(row10_drp_invoice.Text);
                    row10_lbl_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    row10_lbl_currency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                
                }
                row10_lbl_bal_paid.Text = get_balance_be_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);

                if (chk_onaccount.Checked == true)
                {
                    row10_txt_received.Text = get_balance_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);

                    if (decimal.Parse(row10_txt_received.Text) > (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)))
                    {
                        row10_txt_received.Text = (decimal.Parse(txt_amount.Text) - decimal.Parse(lbl_total_amount.Text)).ToString();
                    }
                }
                else
                {
                    row10_txt_received.Text = get_balance_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);
                }

                lbl_total_amount.Text = get_total_amount();

                grid_amount();

                if (row10_lbl_bal_paid.Text != row10_lbl_amount.Text + " " + row10_lbl_currency.Text)
                {
                    row10_btn_view.Visible = true;
                }
                else
                {
                    row10_btn_view.Visible = false;
                }

            }
            updategrid.Update();
        }


        public DataSet getInvoiceNoDecsription(string purchaseInvoiceNo)
        {
            DataSet ds = ObjPaymentStoredProcedure.getPurchaseInvoiceDetails("GET_DECSRIPTION_PURCHASE_INVOICE", purchaseInvoiceNo);
            return ds;
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

         
                pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                if (chk_onaccount.Checked == false)
                {
                    lbl_row1_debit.Text = lbl_total_amount.Text;
                    lbl_row2_credit.Text = lbl_total_amount.Text;
                }
            }
            update_forex.Update();
            updategrid.Update();
            update_bedit_select.Update();

        }
        #endregion

        #region GRID'S TEXTBOX TEXT CHENGED EVENTS
        protected void row1_txt_received_TextChanged(object sender, EventArgs e)
        {

            string str1 = row1_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

           
            if (ViewState["row1_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row1_previous_amount"].ToString())).ToString();
            }
            if (decimal.Parse(row1_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 1 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                ViewState["row1"] = "Done";
                if (chk_onaccount.Checked == false)
                {
                    grid2_table.Visible = true;
                }
                lbl_total_amount.Text = get_total_amount();
            
                grid_amount();
            }

            if (decimal.Parse(row1_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row1_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
              
                row1_txt_duedate.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
        }

        protected void row2_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row2_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row2_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row2_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row2_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 2 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
       
                grid_amount();
            }

            if (decimal.Parse(row2_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row2_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
             
                row2_txt_duedate.Attributes.Add("style", "display:none");
                row2_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row3_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row3_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row3_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row3_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row3_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 3 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
             
                grid_amount();
            }

            if (decimal.Parse(row3_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row3_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
             
                row3_txt_duedate.Attributes.Add("style", "display:none");
                row3_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row4_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row4_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row4_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row4_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row4_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 4 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
            
                grid_amount();
            }

            if (decimal.Parse(row4_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row4_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
               
                row4_txt_duedate.Attributes.Add("style", "display:none");
                row4_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row5_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row5_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row5_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row5_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row5_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 5 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
          
                grid_amount();
            }

            if (decimal.Parse(row5_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row5_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
                
                row5_txt_duedate.Attributes.Add("style", "display:none");
                row5_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row6_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row6_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row6_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row6_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row6_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 6 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
            
                grid_amount();
            }

            if (decimal.Parse(row6_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row6_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
                row6_txt_duedate.Attributes.Add("style", "display:none");
                row6_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row7_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row7_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row7_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row7_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row7_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 7 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
            
                grid_amount();
            }

            if (decimal.Parse(row7_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row7_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
                
                row7_txt_duedate.Attributes.Add("style", "display:none");
                row7_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row8_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row8_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row8_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row8_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row8_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 8 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
             
                grid_amount();
            }

            if (decimal.Parse(row8_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row8_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
               
                row8_txt_duedate.Attributes.Add("style", "display:none");
                row8_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row9_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row9_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row9_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row9_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row9_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 9 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
             
                grid_amount();
            }


            if (decimal.Parse(row9_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row9_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
               
                row9_txt_duedate.Attributes.Add("style", "display:none");
                row9_txt_duedate.Text = "";
                updategrid.Update();
            }
        }

        protected void row10_txt_received_TextChanged(object sender, EventArgs e)
        {
            string str1 = row10_lbl_bal_paid.Text;
            string[] w = str1.Split(' ');

            if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["row10_previous_amount"] != null)
            {
                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row10_previous_amount"].ToString())).ToString();
            }

            if (decimal.Parse(row10_txt_received.Text) > decimal.Parse(w[0]))
            {
                ViewState["error_amount"] = "Enter amount for Sr. no 10 is more then balance to be paid";
                flag_amount = false;
                Master.DisplayMessage("Entered amount is higher then balance to be paid", "successMessage", 5000);
            }
            else
            {
                lbl_total_amount.Text = get_total_amount();
            
                grid_amount();
            }

            if (decimal.Parse(row10_txt_received.Text) < decimal.Parse(w[0]))
            {
                lblduedate.Attributes.Add("style", "display");
                row10_txt_duedate.Attributes.Add("style", "display");
                updategrid.Update();
            }
            else
            {
                
                row10_txt_duedate.Attributes.Add("style", "display:none");
                row10_txt_duedate.Text = "";
                updategrid.Update();
            }
        }
        #endregion

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

                    Label15.Text = "E-Mail Address";

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

        #region SAVE RECORDS
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (lbl_total_amount.Text == "")
            {
                lbl_total_amount.Text = "0";
            }

            if (flag_date == false)
            {
                Master.DisplayMessage("Entered date is not in valid format", "successMessage", 8000);
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
                Master.DisplayMessage("First entered Voucher's debit's General Leger Code", "successMessage", 8000);
            }

            else if (txt_payment_date.Text  == "")
            {
                Master.DisplayMessage("Payment Date is required.", "successMessage", 8000);
            }
            else if (drppayment_mode.Text == "")
            {
                Master.DisplayMessage("First entered Payment's Details", "successMessage", 8000);
            }
            else if (txt_amount.Text == "")
            {
                Master.DisplayMessage("First entered Total Amount.", "successMessage", 8000);
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

                if (drpvoucher_type.Text == "PAYMENT")
                {
                    if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()) && ViewState["PartPayments"].ToString() == "false")
                    {


                        string voucherAmount = "0";

                        if (txt_amount.Text != "")
                        {
                            if (txtBankFees.Text != "")
                            {
                                voucherAmount = (decimal.Parse(txt_amount.Text) + decimal.Parse(txtBankFees.Text)).ToString();
                            }
                            else
                            {
                                voucherAmount = txt_amount.Text ;
                            }
                        }


                        bool on_acc_flag = true;
                        if (chk_onaccount.Checked == false)
                        {
                            on_acc_flag = false;
                        }

                     
                            ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse("0"), "", row1_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txtgl_date.Text, 1);
                            if (ViewState["sales_details_id_1"] != null)
                            {

                                drpvoucher_status.Text = "APPROVED";

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_1"].ToString()), "", row1_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txtgl_date.Text, 2);

                                string str1 = row1_lbl_bal_paid.Text;
                                string[] w = str1.Split(' ');

                                if (row1_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row1_drp_invoice.Text, row1_txt_duedate.Text);
                                }

                                w[0] = (decimal.Parse(w[0]) + decimal.Parse(ViewState["row1_previous_amount"].ToString())).ToString();

                                string bal_paid = get_balance_be_paid(w[0], row1_drp_invoice.Text, row1_lbl_currency.Text);

                                string str11 = bal_paid;
                                string[] w11 = str11.Split(' ');

                                if (bal_paid == "0.00 THB" || decimal.Parse(w11[0]) <= 0)
                                {
                                    insert_voucher_no(row1_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row1"] != null && row1_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row1_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                string bal_paid1 = get_balance_be_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);

                                if (row1_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row1_drp_invoice.Text, row1_txt_duedate.Text);
                                }

                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row1_drp_invoice.Text);
                                }
                            }
                            //   2
                            if (ViewState["sales_details_id_2"] != null && row2_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row2_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_2"].ToString()), "", row2_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row2_drp_status.Text, txtgl_date.Text, 2);

                                string str2 = row2_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');

                                if (row2_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row2_drp_invoice.Text, row2_txt_duedate.Text);
                                }

                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row2_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row2_drp_invoice.Text, row2_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row2_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row2"] != null && row2_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row2_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row2_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row2_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row2_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row2_drp_invoice.Text, row2_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row2_drp_invoice.Text);
                                }
                            }

                            // 3 
                            if (ViewState["sales_details_id_3"] != null && row3_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row3_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_3"].ToString()), "", row3_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row3_drp_status.Text, txtgl_date.Text, 2);

                                string str2 = row3_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');

                                if (row3_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row3_drp_invoice.Text, row3_txt_duedate.Text);
                                }

                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row3_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row3_drp_invoice.Text, row3_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row3_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row3"] != null && row3_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row3_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row3_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row3_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row3_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row3_drp_invoice.Text, row3_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row3_drp_invoice.Text);
                                }
                            }

                            // 4
                            if (ViewState["sales_details_id_4"] != null && row4_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row4_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_4"].ToString()), "", row4_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row4_drp_status.Text, txtgl_date.Text, 2);

                                string str2 = row4_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');

                                if (row4_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row4_drp_invoice.Text, row4_txt_duedate.Text);
                                }

                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row4_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row4_drp_invoice.Text, row4_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row4_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row4"] != null && row4_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row4_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row4_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row4_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row4_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row4_drp_invoice.Text, row4_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row4_drp_invoice.Text);
                                }
                            }

                            // 5 
                            if (ViewState["sales_details_id_5"] != null && row5_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row5_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_5"].ToString()), "", row5_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row5_drp_status.Text, txtgl_date.Text, 2);

                                if (row5_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row5_drp_invoice.Text, row5_txt_duedate.Text);
                                }

                                string str2 = row5_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row5_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row5_drp_invoice.Text, row5_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row5_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row5"] != null && row5_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row5_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row5_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row5_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row5_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row5_drp_invoice.Text, row5_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row5_drp_invoice.Text);
                                }
                            }

                            // 6
                            if (ViewState["sales_details_id_6"] != null && row6_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row6_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_6"].ToString()), "", row6_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row6_drp_status.Text, txtgl_date.Text, 2);

                                if (row6_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row6_drp_invoice.Text, row6_txt_duedate.Text);
                                }

                                string str2 = row6_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row6_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row6_drp_invoice.Text, row6_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row6_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row6"] != null && row6_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row6_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row6_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row6_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                           
                                if (row6_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row6_drp_invoice.Text, row6_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row6_drp_invoice.Text);
                                }
                            }

                            //7
                            if (ViewState["sales_details_id_7"] != null && row7_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row7_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_7"].ToString()), "", row7_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row7_drp_status.Text, txtgl_date.Text, 2);

                                if (row7_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row7_drp_invoice.Text, row7_txt_duedate.Text);
                                }

                                string str2 = row7_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row7_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row7_drp_invoice.Text, row7_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row7_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row7"] != null && row7_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row7_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row7_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row7_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row7_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row7_drp_invoice.Text, row7_txt_duedate.Text);
                                }


                                string bal_paid1 = get_balance_be_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row7_drp_invoice.Text);
                                }
                            }

                            // 8 
                            if (ViewState["sales_details_id_8"] != null && row8_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row8_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_8"].ToString()), "", row8_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row8_drp_status.Text, txtgl_date.Text, 2);

                                if (row8_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row8_drp_invoice.Text, row8_txt_duedate.Text);
                                }

                                string str2 = row8_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row8_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row8_drp_invoice.Text, row8_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row8_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row8"] != null && row8_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row8_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row8_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row8_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row8_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row8_drp_invoice.Text, row8_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row8_drp_invoice.Text);
                                }
                            }

                            // 9 
                            if (ViewState["sales_details_id_9"] != null && row9_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row9_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_9"].ToString()), "", row9_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row9_drp_status.Text, txtgl_date.Text, 2);

                                if (row9_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row9_drp_invoice.Text, row9_txt_duedate.Text);
                                }

                                string str2 = row9_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row9_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row9_drp_invoice.Text, row9_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row9_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row9"] != null && row9_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row9_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row9_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row9_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row9_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row9_drp_invoice.Text, row9_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row9_drp_invoice.Text);
                                }
                            }


                            if (ViewState["sales_details_id_10"] != null && row10_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row10_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_10"].ToString()), "", row10_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row10_drp_status.Text, txtgl_date.Text, 2);

                                if (row10_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row10_drp_invoice.Text, row10_txt_duedate.Text);
                                }

                                string str2 = row10_lbl_bal_paid.Text;
                                string[] w1 = str2.Split(' ');


                                w1[0] = (decimal.Parse(w1[0]) + decimal.Parse(ViewState["row10_previous_amount"].ToString())).ToString();

                                string bal_paid1 = get_balance_be_paid(w1[0], row10_drp_invoice.Text, row10_lbl_currency.Text);

                                string str12 = bal_paid1;
                                string[] w12 = str12.Split(' ');

                                if (bal_paid1 == "0.00 THB" || decimal.Parse(w12[0]) <= 0)
                                {
                                    insert_voucher_no(row10_drp_invoice.Text);
                                }
                            }
                            else if (ViewState["row10"] != null && row10_drp_invoice.Text != "")
                            {

                                ObjPaymentStoredProcedure.insert_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row10_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row10_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row10_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row10_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row10_drp_invoice.Text, row10_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row2_drp_invoice.Text);
                                }
                            }
                            if (chk_onaccount.Checked == true && row1_drp_invoice.Text == "")
                            {
                                ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse("0"), "", row1_txt_received.Text, drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txtgl_date.Text, 1);
                            }
                            ObjPaymentStoredProcedure.update_accounts_entry(0, Request["VN"].ToString(), drp_gl_code.Text, "", txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["sales_details_id_select"].ToString()), lbl_row2_credit.Text, "", drppayment_mode.Text, "REMARKS", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, "", txtgl_date.Text, 2);

                            ObjPaymentStoredProcedure.updatBankFees("UPDATE_BANK_CHARGE_FOR_PAYMENT_IM_EDIT", txtBankFees.Text, int.Parse(Request["VN"].ToString()));

                            if (decimal.Parse(txt_amount.Text) <= decimal.Parse(lbl_total_amount.Text))
                            {
                                insert_payment_voucher_no(Request["VN"].ToString());
                            }
                      //  }

                            /************************* INVOICE VICE LOGIC *************************/
                            if (ViewState["sales_details_id_1"] != null)
                            {
                                DataSet ds_check = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row1_drp_invoice.Text);
                                if (ds_check.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (row1_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row1_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            // 2
                            if (ViewState["sales_details_id_2"] != null && row2_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row2_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row2"] != null && row2_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row2_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }


                            if (ViewState["sales_details_id_3"] != null && row3_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row3_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row3"] != null && row3_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row3_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }


                            // 4
                            if (ViewState["sales_details_id_4"] != null && row4_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row4_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row4"] != null && row4_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row4_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 5 
                            if (ViewState["sales_details_id_5"] != null && row5_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row5_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row5"] != null && row5_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row5_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 6
                            if (ViewState["sales_details_id_6"] != null && row6_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row6_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row6"] != null && row6_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row6_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 7
                            if (ViewState["sales_details_id_7"] != null && row7_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row7_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row7"] != null && row7_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row7_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 8
                            if (ViewState["sales_details_id_8"] != null && row8_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row8_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row8"] != null && row8_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row8_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 9
                            if (ViewState["sales_details_id_9"] != null && row9_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row9_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row9"] != null && row9_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row9_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }

                            // 10
                            if (ViewState["sales_details_id_10"] != null && row10_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row10_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (ViewState["row10"] != null && row10_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row10_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }


                        }
                        

                    else if (ViewState["PartPayments"].ToString() == "true")
                    {
                        bool on_acc_flag = true;
                        if (chk_onaccount.Checked == false)
                        {
                            on_acc_flag = false;
                        }
                        drpvoucher_status.Text = "APPROVED";
                        if (row1_drp_invoice.Text != "")
                        {
                            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_received.Text, row1_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 1);
                            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row1_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                            if (row1_txt_duedate.Text != "")
                            {
                                ObjPaymentStoredProcedure.updaet_due_date(row1_drp_invoice.Text, row1_txt_duedate.Text);
                            }

                            string bal_paid = get_balance_be_paid(row1_lbl_amount.Text, row1_drp_invoice.Text, row1_lbl_currency.Text);
                            if (bal_paid == "0.00 THB")
                            {
                                insert_voucher_no(row1_drp_invoice.Text);
                            }

                            if (ViewState["row2"] != null && row2_drp_invoice.Text != "")
                            {
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row2_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row2_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row2_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row2_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row2_drp_invoice.Text, row2_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row2_lbl_amount.Text, row2_drp_invoice.Text, row2_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row2_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row3"] != null && row3_drp_invoice.Text != "")
                            {
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row3_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row3_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row3_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row3_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row3_drp_invoice.Text, row3_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row3_lbl_amount.Text, row3_drp_invoice.Text, row3_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row3_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row4"] != null && row4_drp_invoice.Text != "")
                            {
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row4_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row4_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row4_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row4_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row4_drp_invoice.Text, row4_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row4_lbl_amount.Text, row4_drp_invoice.Text, row4_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row4_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row5"] != null && row5_drp_invoice.Text != "")
                            {   
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row5_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row5_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row5_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row5_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row5_drp_invoice.Text, row5_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row5_lbl_amount.Text, row5_drp_invoice.Text, row5_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row5_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row6"] != null && row6_drp_invoice.Text != "")
                            {
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row6_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row6_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row6_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row6_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row6_drp_invoice.Text, row6_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row6_lbl_amount.Text, row6_drp_invoice.Text, row6_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row6_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row7"] != null && row7_drp_invoice.Text != "")
                            {   
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row7_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row7_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row7_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row7_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row7_drp_invoice.Text, row7_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row7_lbl_amount.Text, row7_drp_invoice.Text, row7_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row7_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row8"] != null && row8_drp_invoice.Text != "")
                            {   
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row8_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row8_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row8_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row8_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row8_drp_invoice.Text, row8_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row8_lbl_amount.Text, row8_drp_invoice.Text, row8_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row8_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row9"] != null && row9_drp_invoice.Text != "")
                            {   
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row9_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row9_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row9_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row9_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row9_drp_invoice.Text, row9_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row9_lbl_amount.Text, row9_drp_invoice.Text, row9_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row9_drp_invoice.Text);
                                }
                            }


                            if (ViewState["row10"] != null && row10_drp_invoice.Text != "")
                            {
                                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row10_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, "", row10_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row10_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                                if (row10_txt_duedate.Text != "")
                                {
                                    ObjPaymentStoredProcedure.updaet_due_date(row10_drp_invoice.Text, row10_txt_duedate.Text);
                                }

                                string bal_paid1 = get_balance_be_paid(row10_lbl_amount.Text, row10_drp_invoice.Text, row10_lbl_currency.Text);
                                if (bal_paid1 == "0.00 THB")
                                {
                                    insert_voucher_no(row10_drp_invoice.Text);
                                }
                            }

                            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", drp_gl_code.Text, "", txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, lbl_row2_credit.Text, "", drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, "", txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                            ObjPaymentStoredProcedure.inserBankFees("UPDATE_BANK_CHARGE_FOR_PAYMENT", txtBankFees.Text);

                            DataSet fetch_seq_no = objVouchersStoredProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO");

                            insert_payment_voucher_no(fetch_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString());


                            /************************* INVOICE VICE LOGIC *************************/
                            DataSet ds_check = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row1_drp_invoice.Text);
                            if (ds_check.Tables[0].Rows.Count != 0)
                            {
                                for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                                {
                                    DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                    if (ds_seq_no.Tables.Count > 0)
                                    {
                                        insert_payment_voucher_no(ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                    }
                                }
                            }

                            if (ViewState["row2"] != null && row2_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row2_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                            if (ViewState["row3"] != null && row3_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row3_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }



                            if (ViewState["row4"] != null && row4_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row4_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }

                            }


                            if (ViewState["row5"] != null && row5_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row5_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }



                            if (ViewState["row6"] != null && row6_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row6_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }

                            }


                            if (ViewState["row7"] != null && row7_drp_invoice.Text != "")
                            {
                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row7_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }

                            }


                            if (ViewState["row8"] != null && row8_drp_invoice.Text != "")
                            {

                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row8_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }



                            if (ViewState["row9"] != null && row9_drp_invoice.Text != "")
                            {

                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row9_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }

                            }


                            if (ViewState["row10"] != null && row10_drp_invoice.Text != "")
                            {

                                DataSet ds_check1 = objVouchersStoredProcedure.get_seq_no_from_invoice_no("GET_PURCHASE_SEQ_NO", row10_drp_invoice.Text);

                                if (ds_check1.Tables[0].Rows.Count != 0)
                                {
                                    for (int i = 0; i < ds_check1.Tables[0].Rows.Count; i++)
                                    {
                                        DataSet ds_seq_no = objVouchersStoredProcedure.get_seq_no_for_voucher_no_generation("GET_TOTAL_AMONT_FOR_PAYMENT_ACCOUNT", int.Parse(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString()));
                                        if (ds_seq_no.Tables.Count > 0)
                                        {
                                            insert_payment_voucher_no(ds_check1.Tables[0].Rows[i]["SEQ_NO"].ToString());
                                        }
                                    }
                                }
                            }
                           

                        }
                        if (chk_onaccount.Checked == true)
                        {
                            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", ViewState["row1_receipt_glcode"].ToString(), row1_drp_invoice.Text, txt_amount.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_received.Text, row1_txt_received.Text, drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, row1_drp_status.Text, txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 1);
                            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", drp_gl_code.Text, "", txt_amount.Text, drpvoucher_type.Text, drp_currency.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, lbl_row2_credit.Text, "", drppayment_mode.Text, "Remarks", txtcash_receipt.Text, drpsupplier_type.Text, drpsupplier.Text, "", txt_payment_date.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, on_acc_flag, 2);

                            ObjPaymentStoredProcedure.inserBankFees("UPDATE_BANK_CHARGE_FOR_PAYMENT", txtBankFees.Text);

                            Response.Redirect("~/Views/Account/SearchPaymentVoucher.aspx");
                        }
                    }

              
                UpdatePanel2.Update();
                if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                {
                    if (drpvoucher_type.Text == "PAYMENT")
                    {
                        Master.DisplayMessage("Payment voucher updated successfully.", "successMessage", 5000);
                        clear_and_hide();
                    }
                }
                else
                {
                    if (drpvoucher_type.Text == "RECEIPT")
                    {
                        Master.DisplayMessage("Receipt voucher save successfully.", "successMessage", 5000);
                        clear_and_hide();
                    }
                    else if (drpvoucher_type.Text == "PAYMENT")
                    {
                       
                        Master.DisplayMessage("Payment voucher save successfully.", "successMessage", 5000);
                        clear_and_hide();
                    }
                }
            }
        }
        }
        #endregion

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String invoice_no)
        {
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", invoice_no , ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

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

                objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), voucher_no);
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

                objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), voucher_no);
            }
            }


            if (ds_vn_check.Tables[1].Rows[0]["ORDER_STATUS"].ToString() == "7")
            {
                insertCanellationDebitnoteVoucherNo(invoice_no, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString()); // Debit Note
                insertCanellationDebitnoteVoucherNo(invoice_no, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString()); // Purchase Cancellation
            }


        }
        #endregion

        protected void insert_payment_voucher_no(string seq_no)
        {
            bool flag = true;
            DataSet ds = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("GET_PAYMENT_VOUCHER_DETAILS_FOR_VOUCHER_NO", seq_no);

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
                DataSet ds1 = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("CHECK_VOUCHER_NO_FOR_PURCHASE_PAYMENT", seq_no);

                if (ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
                {
                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString());
                    DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO_FROM_PURCHASE_PAYMENT", ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString());
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

                        objVouchersStoredProcedure.insert_accounts_voucher_no_purchase_payment(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seq_no, voucher_no);
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
                        objVouchersStoredProcedure.insert_accounts_voucher_no_purchase_payment(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seq_no, voucher_no);
                    }
                    
                }
            }
        }

     

        #region ON ACCOUNT CHECKBOX EVENT
        protected void chk_onaccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_onaccount.Checked == true)
            {
                txt_amount.Enabled = true;
                row1.Attributes.Add("style", "display:none");
                btnadd2.Attributes.Add("style", "display:none");

                clear_and_hide();
            }
            else
            {
                txt_amount.Enabled = false;
                row1.Attributes.Add("style", "display");
                btnadd2.Attributes.Add("style", "display");

            }
            updategrid.Update();
            UpdatePanel2.Update();
            update_bedit_select.Update();
            update_forex.Update();
            update_payments.Update();
            update_voucher.Update();
        }
        #endregion

        protected void txt_amount_TextChanged(object sender, EventArgs e)
        {
                            grid2_table.Visible = true;

                DataSet dss = ObjPaymentStoredProcedure.fetch_supplier("FETCH_PURCHASE_INVOICE_NO_SUPPLIER", drpsupplier.Text, drpsupplier_type.Text);
                DataSet ds66 = objVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dss.Tables[0].Rows[0]["SUPPLIER_SR_NO"].ToString(), "S");
                ViewState["row1_receipt_glcode"] = ds66.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                
                lbl_gl_code.Text = ViewState["row1_receipt_glcode"].ToString();
                if (txtBankFees.Text == "")
                {
                    txtBankFees.Text = "0";
                }

                
                lbl_row1_debit.Text = txt_amount.Text;
                
                lbl_row2_credit.Text = (decimal.Parse(lbl_row1_debit.Text) + decimal.Parse(txtBankFees.Text)).ToString();
            
            update_bedit_select.Update();
        }

        protected void txtBankFees_TextChanged(object sender, EventArgs e)
        {
                if (txtBankFees.Text == "")
                {
                    txtBankFees.Text = "0";
                }

                update_forex.Update();
                
                lbl_row2_credit.Text = (decimal.Parse(lbl_row1_debit.Text) + decimal.Parse(txtBankFees.Text)).ToString();
                drp_gl_code.Focus();
                update_bedit_select.Update();
            
        }

        #region STILL LEFT TO CHANGE


        #region WILL NEVER USE

        protected void drpbank_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objVouchersStoredProcedure.fetch_branch("FETCH_BRANCH_NAME", drpbank_name.Text);
            binddropdownlist(drpbranch, ds);

            btnadd3.Attributes.Add("style", "display");
            update_payments.Update();

        }

        protected void txtcashreceipt_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtcashreceipt_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered date is not in valid format.", "successMessage", 8000);
                flag_date = false;
            }
            update_payments.Update();
        }

        protected void txtcheque_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtcheque_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered date is not in valid format.", "successMessage", 8000);
                flag_date = false;
            }
            update_payments.Update();
        }

        protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear_and_hide();
            if (drpvoucher_type.Text == "PAYMENT" || drpvoucher_type.Text == "RECEIPT")
            {

                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(row1_drp_invoice, ds7);

                    //DataSet ds8 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_AGENT_COMPANY_NAME");
                    //binddropdownlist(drpagent_company_name, ds8);
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    DataSet ds6 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO_PURCHASE");
                    binddropdownlist(row1_drp_invoice, ds6);

                    DataSet ds7 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(row1_drp_invoice, ds7);


                }
                update_voucher.Update();
                updategrid.Update();

            }
        }
        #endregion


        public string get_total_amount()
        {
            decimal total_amount;
            lbl_total_amount.Text = "";
            if (row1_txt_received.Text == "")
            {
                row1_txt_received.Text = "0";
            }
            if (row2_txt_received.Text == "")
            {
                row2_txt_received.Text = "0";
            }
            if (row3_txt_received.Text == "")
            {
                row3_txt_received.Text = "0";
            }
            if (row4_txt_received.Text == "")
            {
                row4_txt_received.Text = "0";
            }
            if (row5_txt_received.Text == "")
            {
                row5_txt_received.Text = "0";
            }
            if (row6_txt_received.Text == "")
            {
                row6_txt_received.Text = "0";
            }
            if (row7_txt_received.Text == "")
            {
                row7_txt_received.Text = "0";
            }
            if (row8_txt_received.Text == "")
            {
                row8_txt_received.Text = "0";
            }
            if (row9_txt_received.Text == "")
            {
                row9_txt_received.Text = "0";
            }
            if (row10_txt_received.Text == "")
            {
                row10_txt_received.Text = "0";
            }
            total_amount = decimal.Parse(row1_txt_received.Text) + decimal.Parse(row2_txt_received.Text) + decimal.Parse(row3_txt_received.Text) + decimal.Parse(row4_txt_received.Text) + decimal.Parse(row5_txt_received.Text) + decimal.Parse(row6_txt_received.Text) + decimal.Parse(row7_txt_received.Text) + decimal.Parse(row8_txt_received.Text) + decimal.Parse(row9_txt_received.Text) + decimal.Parse(row10_txt_received.Text);

            if (chk_onaccount.Checked == true)
            {

            }
            else
            {
             //   txt_amount.Text = total_amount.ToString();
            }
            return total_amount.ToString();
        }

        public string get_balance_be_paid(String row1_lbl, String invoice_no, String lbl_currency)
        {
            decimal amount = decimal.Parse(row1_lbl);
            string lbl_be_paid;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", invoice_no);
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
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["DR_AMOUNT"].ToString());
                    }
                }
                lbl_be_paid = amount.ToString() + " " + lbl_currency;
            }
            else
            {
                lbl_be_paid = row1_lbl + " " + lbl_currency;
            }
            return lbl_be_paid;
        }

        #region ALL REMOVE BUTTONS
        protected void row2_btn_remove_Click(object sender, EventArgs e)
        {
            row2_drp_invoice.Items.Clear();
            row2_txt_received.Text = "";
            row2_drp_status.Text = "";
            row2_lbl_bal_paid.Text = "";
            row2_lbl_amount.Text = "";
            row2_lbl_currency.Text = "";
            row2.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display:none");
            btnadd2.Attributes.Add("style", "display");

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row3_btn_remove_Click(object sender, EventArgs e)
        {
            row3_drp_invoice.Items.Clear();
            row3_txt_received.Text = "";
            row3_drp_status.Text = "";
            row3_lbl_bal_paid.Text = "";
            row3_lbl_amount.Text = "";
            row3_lbl_currency.Text = "";
            row3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display");

            row2_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row4_btn_remove_Click(object sender, EventArgs e)
        {
            row4_drp_invoice.Items.Clear();
            row4_txt_received.Text = "";
            row4_drp_status.Text = "";
            row4_lbl_bal_paid.Text = "";
            row4_lbl_amount.Text = "";
            row4_lbl_currency.Text = "";
            row4.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display");

            row3_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row5_btn_remove_Click(object sender, EventArgs e)
        {
            row5_drp_invoice.Items.Clear();
            row5_txt_received.Text = "";
            row5_drp_status.Text = "";
            row5_lbl_bal_paid.Text = "";
            row5_lbl_amount.Text = "";
            row5_lbl_currency.Text = "";
            row5.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display");

            row4_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row6_btn_remove_Click(object sender, EventArgs e)
        {
            row6_drp_invoice.Items.Clear();
            row6_txt_received.Text = "";
            row6_drp_status.Text = "";
            row6_lbl_bal_paid.Text = "";
            row6_lbl_amount.Text = "";
            row6_lbl_currency.Text = "";
            row6.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display");

            row5_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row7_btn_remove_Click(object sender, EventArgs e)
        {
            row7_drp_invoice.Items.Clear();
            row7_txt_received.Text = "";
            row7_drp_status.Text = "";
            row7_lbl_bal_paid.Text = "";
            row7_lbl_amount.Text = "";
            row7_lbl_currency.Text = "";
            row7.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display");

            row6_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row8_btn_remove_Click(object sender, EventArgs e)
        {
            row8_drp_invoice.Items.Clear();
            row8_txt_received.Text = "";
            row8_drp_status.Text = "";
            row8_lbl_bal_paid.Text = "";
            row8_lbl_amount.Text = "";
            row8_lbl_currency.Text = "";
            row8.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display");

            row8_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row9_btn_remove_Click(object sender, EventArgs e)
        {
            row9_drp_invoice.Items.Clear();
            row9_txt_received.Text = "";
            row9_drp_status.Text = "";
            row9_lbl_bal_paid.Text = "";
            row9_lbl_amount.Text = "";
            row9_lbl_currency.Text = "";
            row9.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display");

            row9_btn_remove.Visible = true;

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();

            updategrid.Update();
        }

        protected void row10_btn_remove_Click(object sender, EventArgs e)
        {
            row10_drp_invoice.Items.Clear();
            row10_txt_received.Text = "";
            row10_drp_status.Text = "";
            row10_lbl_bal_paid.Text = "";
            row10_lbl_amount.Text = "";
            row10_lbl_currency.Text = "";
            row10.Attributes.Add("style", "display:none");
            
            btnadd10.Attributes.Add("style", "display");

            lbl_total_amount.Text = get_total_amount();
            txt_amount.Text = lbl_total_amount.Text;
            grid_amount();
            
            updategrid.Update();
        }
        #endregion

        protected void drpagent_company_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #region VIEW BUTTONS
       
        protected void row1_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");


            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row1_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row1_drp_invoice.Text;
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";
           
            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row1_btn_view";

            modalPop.DropShadow = true;

            //  modalPop.BackgroundCssClass = "modalBackground";

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            // this.ModalPanel.Controls.Add(lstchecks);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row2_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row2_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row2_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row2_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row3_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row3_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row3_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row3_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row4_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");
            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row4_drp_invoice.Text;

            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row4_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row4_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row5_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row5_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row5_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row5_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row6_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row6_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row6_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row6_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row7_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row7_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row7_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row7_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row8_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row8_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row8_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row8_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row9_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row9_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row9_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row9_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }

        protected void row10_btn_view_onclick(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");

            lblTitleAlert.Text = "PAYMENT DETAILS OF " + row10_drp_invoice.Text;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", row10_drp_invoice.Text);

            GridView1.DataSource = ds2;
            GridView1.DataBind();
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

            modalPop.ID = "popUp";

            modalPop.PopupControlID = "pnlCompanyRoleSelection";

            modalPop.TargetControlID = "row10_btn_view";

            modalPop.DropShadow = true;

            modalPop.CancelControlID = "ImageButton1";

            this.pnlCompanyRoleSelection.Controls.Add(modalPop);

            modalPop.Show();

            updategrid.Update();

        }
        #endregion

        #endregion

        #region CLEAR AND HIDE COTROL FUNCTIONS
        protected void diabled_controls()
        {
            btnadd2.Visible = false;
            btnadd3.Visible = false;
            btnadd4.Visible = false;
            btnadd5.Visible = false;
            btnadd6.Visible = false;
            btnadd7.Visible = false;
            btnadd8.Visible = false;
            btnadd9.Visible = false;
            btnadd10.Visible = false;

            row1_drp_invoice.Enabled = false;
            row1_drp_status.Enabled = false;

            row1_txt_received.Enabled = false;
            
            row2_drp_invoice.Enabled = false;
            row2_drp_status.Enabled = false;

            row2_txt_received.Enabled = false;
            
            row3_drp_invoice.Enabled = false;
            row3_drp_status.Enabled = false;

            row3_txt_received.Enabled = false;
            
            row4_drp_invoice.Enabled = false;
            row4_drp_status.Enabled = false;

            row4_txt_received.Enabled = false;
            
            row5_drp_invoice.Enabled = false;
            row5_drp_status.Enabled = false;

            row5_txt_received.Enabled = false;
            
            row6_drp_invoice.Enabled = false;
            row6_drp_status.Enabled = false;

            row6_txt_received.Enabled = false;
            
            row7_drp_invoice.Enabled = false;
            row7_drp_status.Enabled = false;

            row7_txt_received.Enabled = false;
            
            row8_drp_invoice.Enabled = false;
            row8_drp_status.Enabled = false;

            row8_txt_received.Enabled = false;
            
            row9_drp_invoice.Enabled = false;
            row9_drp_status.Enabled = false;

            row9_txt_received.Enabled = false;
            
            row10_drp_invoice.Enabled = false;
            row10_drp_status.Enabled = false;

            row10_txt_received.Enabled = false;
            
            txt_payment_date.Enabled = false;
            drppayment_mode.Enabled = false;
            drpbank_name.Enabled = false;
            drpbranch.Enabled = false;
            txtcheque_no.Enabled = false;
            txtcheque_date.Enabled = false;
            txtcash_receipt.Enabled = false;
            drp_currency.Enabled = false;
            txt_amount.Enabled = false;
            txtBankFees.Enabled = false;

            
            drp_gl_code.Enabled = false;
            
            drpvoucher_status.Enabled = false;
            
            txt_narration.Enabled = false;

            drpsupplier_type.Enabled = false;

            drpsupplier.Enabled = false;

            Button2.Visible = false;

            updategrid.Update();
            UpdatePanel2.Update();
            update_bedit_select.Update();
            update_forex.Update();
            update_payments.Update();
            update_voucher.Update();
        }

        protected void clear_and_hide()
        {
            row1_drp_invoice.Text = "";
            row1_drp_status.Text = "";

            row1_txt_received.Text = "";
            row1_lbl_bal_paid.Text = "";

            row1_lbl_amount.Text = "";
            row1_lbl_currency.Text = "";

            row2.Attributes.Add("style", "display:none");
            btnadd2.Attributes.Add("style", "display:none");
            row2_drp_invoice.Text = "";
            row2_drp_status.Text = "";

            row2_txt_received.Text = "";
            row2_lbl_bal_paid.Text = "";

            row2_lbl_amount.Text = "";
            row2_lbl_currency.Text = "";

            row3.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display:none");
            row3_drp_invoice.Text = "";
            row3_drp_status.Text = "";

            row3_txt_received.Text = "";
            row3_lbl_bal_paid.Text = "";

            row3_lbl_amount.Text = "";
            row3_lbl_currency.Text = "";

            row4.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display:none");
            row4_drp_invoice.Text = "";
            row4_drp_status.Text = "";

            row4_txt_received.Text = "";
            row4_lbl_bal_paid.Text = "";

            row4_lbl_amount.Text = "";
            row4_lbl_currency.Text = "";

            row5.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display:none");
            row5_drp_invoice.Text = "";
            row5_drp_status.Text = "";

            row5_txt_received.Text = "";
            row5_lbl_bal_paid.Text = "";

            row5_lbl_amount.Text = "";
            row5_lbl_currency.Text = "";

            row6.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display:none");
            row6_drp_invoice.Text = "";
            row6_drp_status.Text = "";

            row6_txt_received.Text = "";
            row6_lbl_bal_paid.Text = "";

            row6_lbl_amount.Text = "";
            row6_lbl_currency.Text = "";

            row7.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display:none");
            row7_drp_invoice.Text = "";
            row7_drp_status.Text = "";

            row7_txt_received.Text = "";
            row7_lbl_bal_paid.Text = "";

            row7_lbl_amount.Text = "";
            row7_lbl_currency.Text = "";

            row8.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display:none");
            row8_drp_invoice.Text = "";
            row8_drp_status.Text = "";

            row8_txt_received.Text = "";
            row8_lbl_bal_paid.Text = "";

            row8_lbl_amount.Text = "";
            row8_lbl_currency.Text = "";

            row9.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display:none");
            row9_drp_invoice.Text = "";
            row9_drp_status.Text = "";

            row9_txt_received.Text = "";
            row9_lbl_bal_paid.Text = "";

            row9_lbl_amount.Text = "";
            row9_lbl_currency.Text = "";

            row10.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display:none");
            row10_drp_invoice.Text = "";
            row10_drp_status.Text = "";

            row10_txt_received.Text = "";
            row10_lbl_bal_paid.Text = "";

            row10_lbl_amount.Text = "";
            row10_lbl_currency.Text = "";

            txt_payment_date.Text = "";
            drppayment_mode.Text = "";
            drpbank_name.Text = "";
            drpbranch.Text = "";
            txtcheque_no.Text = "";
            txtcheque_date.Text = "";
            txtcash_receipt.Text = "";
            drp_currency.Text = "";
            txt_amount.Text = "";
            txtBankFees.Text = "";

            grid2_table.Visible = false;
            lbl_gl_code.Text = "";
            drp_gl_code.Text = "";
            lbl_row2_debit.Text = "";
            lbl_row2_credit.Text = "";
            lbl_row1_debit.Text = "";
            lbl_row1_credit.Text = "";

            lbl_total_amount.Text = "";
            drpvoucher_status.Text = "";
            
            txt_narration.Text = "";
            updategrid.Update();
            UpdatePanel2.Update();
            update_bedit_select.Update();
            update_forex.Update();
            update_payments.Update();
            update_voucher.Update();
        }
        #endregion


        public string on_account_status(string invoice_no)
        {
            DataSet ds = objVouchersStoredProcedure.get_invoice_status("CHECK_INVOICE_POSTED", invoice_no);

            return ds.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
        }

        public string get_balance_paid(String row1_lbl, String invoice_no, String lbl_currency)
        {
            decimal amount = decimal.Parse(row1_lbl);
            string lbl_be_paid;
            DataSet ds2 = objVouchersStoredProcedure.get_invoice_amount("FETCH_AMOUNT_INVOICE_PAYMENT_VOUCHER_DETAILS", invoice_no);
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
                        amount = amount - decimal.Parse(ds2.Tables[0].Rows[i]["DR_AMOUNT"].ToString());
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

        public void on_account_invoice_select(string invoice_no, int srno)
        {
            if (chk_onaccount.Checked == true)
            {
                DataSet ds = ObjPaymentStoredProcedure.getSupplierInvoice("FETCH_PURCHASE_INVOICE_NO_NOT_POSTED_FOR_PAYMENT", drpsupplier.Text, drpsupplier_type.Text);

                if (ViewState["editModerows"] != null)
                {
                    DataSet ds_for_edit = objVouchersStoredProcedure.get_records_for_edit("GET_EDIT_PAYMENT_VOUCHER_DETAILS", Request["VN"].ToString());
                    if (int.Parse(ViewState["editModerows"].ToString()) - 1 <= srno)
                    {
                        srno = srno - int.Parse(ViewState["editModerows"].ToString()) + 1;
                    }

                    for (int i = 0; i < ds_for_edit.Tables[0].Rows.Count; i++)
                    {
                        if (ds_for_edit.Tables[0].Rows[i]["INVOICE_NO"].ToString() != "" && (ds_for_edit.Tables[0].Rows[i]["VOUCHER_STATUS_ID"].ToString() == "2" || ds_for_edit.Tables[0].Rows[i]["VOUCHER_STATUS_ID"].ToString() == "5"))
                        {
                            srno = srno + 1;
                        }
                    }
                }
                
                    if (invoice_no == ds.Tables[0].Rows[srno]["AutoSearchResult"].ToString())
                    {
                        ViewState["invoice_check"] = null;

                    }

                    else
                    {
                        ViewState["invoice_check"] = ds.Tables[0].Rows[srno]["AutoSearchResult"].ToString();
                    }

                

            }
            else
            {
                ViewState["invoice_check"] = null;
            }


        }

        protected void insertCanellationDebitnoteVoucherNo(string invoice_no, string voucher_type)
        {
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", voucher_type); //ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

            DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", voucher_type);  //ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

            DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", invoice_no, voucher_type); //  ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());

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

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), voucher_type, voucher_no);
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

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), voucher_type, voucher_no);
                }

            }
        }
    }
}