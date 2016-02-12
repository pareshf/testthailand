using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using CRM.DataAccess.FIT ;
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
    public partial class DebitNote : System.Web.UI.Page
    {
        CRM.DataAccess.Account.DebitNote objdebitnote = new CRM.DataAccess.Account.DebitNote();
        CRM.DataAccess.Account.AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new CRM.DataAccess.Account.AcoountVouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        CRM.DataAccess.Account.CreditNote objcreditnote = new CRM.DataAccess.Account.CreditNote();

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 239);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds1 = objdebitnote.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds1);

                DataSet ds2 = objdebitnote.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds2);

                DataSet ds3 = objdebitnote.fetch_voucher_type("GET_ALL_GL_CODE_FOR_CREDIT_NOTE");
                binddropdownlist(row1_drp_glcode, ds3);

                DataSet ds4 = objdebitnote.fetch_voucher_type("FECH_COMPANYS_BANK");
                binddropdownlist(row2_drp_gl, ds4);

                
            }
        }
        #region BIND DROP DOWN METHOD
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            // r.SelectedValue = "0";
        }
        #endregion
        protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpvoucher_type.Text == "DEBIT NOTE")
            {
                DataSet ds5 = objdebitnote.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drpsales_Invoice, ds5);
                update_debitNote.Update();
                
            }
        }
        protected void drpsales_Invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds6 = objdebitnote.Fetch_Invoice_No("FETCH_ALL_PURCHASE_INVOICE_NO_FOR_DEBIT_NOTE",drpsales_Invoice.Text);
            binddropdownlist(drpPurchaseInvoice, ds6);
            update_debitNote.Update();
        }
        protected void drpPurchaseInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drpPurchaseInvoice.Text);
            invoice_no_tr.Attributes.Add("style", "display");

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

            DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(drpPurchaseInvoice.Text, "PURCHASE");

            row1_drp_glcode.Text = row1_drp_glcode.Text = ds_check.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

            update_debitNote.Update();
            updategrid.Update();
            //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);

        }
        protected void drp_account_grp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void drp_gl_code_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void drp_invoice_no_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void row1_cb_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void row1_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            row2_txt_credit.Text = row1_txt_debit.Text;

            row2_txt_credit.Enabled = true;
            row2_txt_debit.Enabled = false;

            row2_drp_currency.Text = "USD";
            row2_drp_currency.Enabled = false;

            row2_drp_gl.Focus();

            ViewState["old_amount"] = row1_txt_debit.Text;
            updategrid.Update();
        }
        protected void row1_txt_credit_TextChanged(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            row2_txt_debit.Text = row1_txt_credit.Text;

            row2_txt_debit.Enabled = true;
            row2_txt_credit.Enabled = false;

            row2_drp_currency.Text = "USD";
            row2_drp_currency.Enabled = false;
            row2_drp_gl.Focus();


            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
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
                    //  lbl_voucher_date.Text = result;
                }
                else
                {
                    result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                    //  lbl_voucher_date.Text = result;
                }
            }
            else
            {
                if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                {
                    result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                    //  lbl_voucher_date.Text = result;
                }
                else
                {
                    result = w[1] + "/" + w[0] + "/" + t1[0];
                    // lbl_voucher_date.Text = result;
                }
            }
            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));

            objFITPaymentStoreProcedure.insert_accounts_entry(0, row1_drp_glcode.Text , drpPurchaseInvoice.Text, result, drpvoucher_type.Text , int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row1_txt_credit.Text , row1_txt_debit.Text , "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drpPurchaseInvoice.Text, result, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drpPurchaseInvoice.Text, result, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

            DataSet ds_seq_no = objcreditnote.fetch_seq_no("FETCH_SEQ_NO_FROM_INVOICE_NO", drpPurchaseInvoice.Text, "PURCHASE");
            objFITPaymentStoreProcedure.updte_voucher_status_on_cancel(ds_vsstatus.Tables[0].Rows[4]["AutoSearchResult"].ToString(), int.Parse(ds_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString()));

            Master.DisplayMessage("Debit Note generated succesfully.", "successMessage", 8000);
            Clear();
        }
        public void Clear()
        {

            //row1_txt_credit.Text = "";
            //row1_txt_debit.Text = "";
            //row2_txt_credit.Text = "";
            //row2_txt_debit.Text = "";
            //row1_drp_glcode.SelectedValue = "";
            //row2_drp_gl.SelectedValue = "0";
            //drpvoucher_type.SelectedValue = "0";
            //drpvoucher_status.SelectedValue = "0";
        }
    }
}