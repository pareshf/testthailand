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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.Account
{
    public partial class DebitCreditNote : System.Web.UI.Page
    {
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
                {
                    invoice_no_tr.Visible = true;
                  
                  
                    DataSet ds1 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds1);

                    DataSet ds_vs = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    binddropdownlist(drpvoucher_status, ds_vs);

                    drpvoucher_type.Text = Request["VT"].ToString();

                    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpvoucher_type.Items[i];
                        if (item.ToString() == "CREDIT NOTE" || item.ToString() == "DEBIT NOTE")
                        {

                        }
                        else
                        {
                            drpvoucher_type.Items.Remove(item);
                        }
                    }

                    if (drpvoucher_type.Text == "CREDIT NOTE")
                    {
                        drp_invoice_no.Visible = true;
                        DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_CREDIT_NOTE");
                        binddropdownlist(drp_invoice_no, ds7);
                        drp_invoice_no.Text = Request["IV"].ToString();

                        DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);

                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(Request["IV"].ToString(), Request["VT"].ToString());

                        row2.Attributes.Add("style", "visible");
                        Tr3.Visible = true;
                        id_1.Visible = true;
                        id_2.Visible = true;
                        id_3.Visible = true;
                        id_4.Visible = true;
                        id_5.Visible = true;
                        id_6.Visible = true;
                        Tr5.Visible = true;
                        lbl_client_name.Visible = true;
                        Label64.Text = "Client Name";
                        drp_client_name.Visible = false;

                        DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                        //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                        txtgl_date.Text = ds_check.Tables[0].Rows[0]["GL_DATE"].ToString();
                        lbl_client_name.Text = ds.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                        lbl_tour_short_name.Text = ds.Tables[0].Rows[0]["TOUR_SHORT_NAME"].ToString();
                        lbl_package_name.Text = ds.Tables[0].Rows[0]["FIT_PACKAGE_NAME"].ToString();
                        lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                        lbl_inovice_date.Text = ds.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                        
                        lbl_currency_name.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                        
                        lblInvoiceAmount.Text = ds.Tables[0].Rows[0]["INVOICE_AMOUNT"].ToString();

                        lbl_inovice_amount.Text = (decimal.Parse(lblInvoiceAmount.Text) - decimal.Parse(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString())).ToString();

                        lblInvoiceAmountCurrency.Text = "USD";
                        lbl_currency_name.Text = "USD";

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["VOUCHER_TYPE_ID"].ToString() == "2")
                            {
                                lblVoucherNo.Text = ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString();
                                break;
                            }
                        }

                            for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    lbl_row1.Text = "THB";
                                    DataSet ds51 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                                    binddropdownlist(row1_drp_glcode, ds51);

                                    ViewState["seq_no"] = ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString();

                                    row1_drp_glcode.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                    row1_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                    row1_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                                    ViewState["total_amount_old"] = row1_txt_debit.Text;

                                    if (row1_txt_credit.Text == "0.00")
                                    {
                                        row1_txt_credit.Enabled = false;
                                    }
                                    if (row1_txt_debit.Text == "0.00")
                                    {
                                        row1_txt_debit.Enabled = false;
                                    }


                                    ViewState["row1_details_id_1"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                                }
                                else if (i == 1)
                                {
                                    DataSet ds50 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                                    Label44.Text = "THB";
                                    binddropdownlist(row2_drp_gl, ds50);
                                    row2_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                    row2_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                    row2_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                    if (row2_txt_credit.Text == "0.00")
                                    {
                                        row2_txt_credit.Enabled = false;
                                    }
                                    if (row2_txt_debit.Text == "0.00")
                                    {
                                        row2_txt_debit.Enabled = false;
                                    }
                                    ViewState["row1_details_id_2"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                                }

                            }
                        txt_narration.Text = ds_check.Tables[0].Rows[0]["NARRATION"].ToString();
                        drpvoucher_status.Text = ds_check.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                    }
                    else if (drpvoucher_type.Text == "DEBIT NOTE")
                    {
                        drp_invoice_no.Visible = true;
                        DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_DEBIT_NOTE");
                        binddropdownlist(drp_invoice_no, ds7);
                        drp_invoice_no.Text = Request["IV"].ToString();

                        row2.Attributes.Add("style", "display");

                        DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);
                        //DataSet ds_tp_supplier = objAcoountVouchersStoredProcedure.fetch_common_data("FETCH_TP_SUPPLIER_NO", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString());
                        //DataSet ds_tp_data = objAcoountVouchersStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString(), ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString());

                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(Request["IV"].ToString(), Request["VT"].ToString());
                        txtgl_date.Text = ds_check.Tables[0].Rows[0]["GL_DATE"].ToString();
                        Tr3.Visible = true;
                        id_1.Visible = true;
                        id_2.Visible = true;
                        id_3.Visible = false;
                        //     id_4.Visible = true;
                        id_5.Visible = true;
                        id_6.Visible = true;

                        lbl_client_name.Visible = true;
                        Label64.Text = "Client Name";
                        drp_client_name.Visible = false;

                        DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                        //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                        Label65.Text = "Supplier Type";
                        //Label67.Text = "Against Invoice No";
                        Label68.Text = "Due Date";
                        Label82.Text = "Voucher Amount";

                        lbl_client_name.Text = ds11.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                        lbl_tour_short_name.Text = ds11.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();
                        lbl_package_name.Text = ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                        
                        lbl_inovice_date.Text = ds11.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                        lbl_inovice_amount.Text = ds11.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        lbl_currency_name.Text = ds11.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();

                        for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                DataSet ds51 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                                binddropdownlist(row1_drp_glcode, ds51);

                                ViewState["seq_no"] = ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString();

                                row1_drp_glcode.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                row1_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                row1_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                                ViewState["total_amount_old"] = row1_txt_debit.Text;

                                if (row1_txt_credit.Text == "0.00")
                                {
                                    row1_txt_credit.Enabled = false;
                                }
                                if (row1_txt_debit.Text == "0.00")
                                {
                                    row1_txt_debit.Enabled = false;
                                }


                                ViewState["row1_details_id_1"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            }
                            else if (i == 1)
                            {
                                DataSet ds50 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                                binddropdownlist(row2_drp_gl, ds50);
                                row2_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                row2_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                row2_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();
                                if (row2_txt_credit.Text == "0.00")
                                {
                                    row2_txt_credit.Enabled = false;
                                }
                                if (row2_txt_debit.Text == "0.00")
                                {
                                    row2_txt_debit.Enabled = false;
                                }
                                ViewState["row1_details_id_2"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            }

                        }
                        txt_narration.Text = ds_check.Tables[0].Rows[0]["NARRATION"].ToString();
                        drpvoucher_status.Text = ds_check.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
                    }
                    btnSave.Visible = false;
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
            //  r.SelectedValue = "1";
        }
        #endregion

        protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpvoucher_type.Text == "CREDIT NOTE")
            {
                DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_CREDIT_NOTE");
                binddropdownlist(drp_invoice_no, ds7);
            }
       
        }

        protected void drp_invoice_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpvoucher_type.Text == "CREDIT NOTE")
            {
                DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);

                id_1.Visible = true;
                id_2.Visible = true;
                id_3.Visible = true;
                id_4.Visible = true;
                id_5.Visible = true;
                id_6.Visible = true;

                lbl_client_name.Visible = true;
                Label64.Text = "Client Name";
                drp_client_name.Visible = false;

                DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);

                lbl_client_name.Text = ds.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                lbl_tour_short_name.Text = ds.Tables[0].Rows[0]["TOUR_SHORT_NAME"].ToString();
                lbl_package_name.Text = ds.Tables[0].Rows[0]["FIT_PACKAGE_NAME"].ToString();
                lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                lbl_inovice_date.Text = ds.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                lbl_inovice_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                lbl_currency_name.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
            }
            else if (drpvoucher_type.Text == "DEBIT NOTE")
            {
                DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);
                DataSet ds_tp_supplier = objAcoountVouchersStoredProcedure.fetch_common_data("FETCH_TP_SUPPLIER_NO", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString());
                DataSet ds_tp_data = objAcoountVouchersStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString(), ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString());

                id_1.Visible = true;
                id_2.Visible = true;
                id_3.Visible = true;
                //     id_4.Visible = true;
                id_5.Visible = true;
                id_6.Visible = true;

                lbl_client_name.Visible = true;
                Label64.Text = "Client Name";
                drp_client_name.Visible = false;

                DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                Label65.Text = "Supplier Type";
                Label67.Text = "Against Invoice No";
                Label68.Text = "Due Date";

                lbl_client_name.Text = ds11.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                lbl_tour_short_name.Text = ds11.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();
                lbl_package_name.Text = ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                //       lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                lbl_inovice_date.Text = ds11.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                lbl_inovice_amount.Text = ds11.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                lbl_currency_name.Text = ds11.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
            {
                if (drpvoucher_type.Text == "CREDIT NOTE")
                {
                    DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                    DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_balance_type();

                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text,  1);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);

                    Master.DisplayMessage("Credit Note entry updated successfully.", "successMessage", 8000);
                }
                else if (drpvoucher_type.Text == "DEBIT NOTE")
                {
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 1);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);

                    Master.DisplayMessage("Debit Note entry updated successfully.", "successMessage", 8000);
                }
            }

        }

        protected void row1_txt_debit_TextChanged(object sender, EventArgs e)
        {
            
            row2.Attributes.Add("style", "display");
            row2_txt_credit.Text = row1_txt_debit.Text;

            row2_txt_credit.Enabled = true;
            row2_txt_debit.Enabled = false;
        
            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row2_drp_currency, ds5);

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;

            row2_drp_gl.Focus();
            if (drpvoucher_type.Text != "PURCHASE" && drpvoucher_type.Text != "SALES")
            {
               // btnadd3.Attributes.Add("style", "display");
            }
            ViewState["old_amount"] = row1_txt_debit.Text;
            updategrid.Update();
        }
     
        protected void row1_txt_credit_TextChanged(object sender, EventArgs e)
        {
        
            row2.Attributes.Add("style", "display");
            row2_txt_debit.Text = row1_txt_credit.Text;

            row2_txt_debit.Enabled = true;
            row2_txt_credit.Enabled = false;
           
            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row2_drp_currency, ds5);

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;
            row2_drp_gl.Focus();

            if (drpvoucher_type.Text != "PURCHASE" && drpvoucher_type.Text != "SALES")
            {
               // btnadd3.Attributes.Add("style", "display");
            }
            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }
    }
}