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
using CRM.DataAccess.FIT;

namespace CRM.WebApp.Views.Account
{
    public partial class PurchaseCancellation : System.Web.UI.Page
    {
        CRM.DataAccess.Account.PurchaseCancellation objpurchaseCancellation = new DataAccess.Account.PurchaseCancellation();
        CRM.DataAccess.Account.AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new CRM.DataAccess.Account.AcoountVouchersStoredProcedure();
        CRM.DataAccess.Account.CreditNote objcreditnote = new CRM.DataAccess.Account.CreditNote();
        CRM.DataAccess.FIT.FITPaymentStoreProcedure objFITPaymentStoreProcedure = new CRM.DataAccess.FIT.FITPaymentStoreProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();


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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 304);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds1 = objpurchaseCancellation.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds1);

                //drpvoucher_type.Text = ds1.Tables[0].Rows[8]["AutoSearchResult"].ToString();
                //drpvoucher_type.Enabled = false;

                DataSet ds2 = objpurchaseCancellation.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds2);

                DataSet ds3 = objpurchaseCancellation.fetch_voucher_type("FETCH_ACCOUNT_GROUP_NAME");
                binddropdownlist(drp_account_grp, ds3);

                DataSet ds4 = objpurchaseCancellation.fetch_voucher_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row1_drp_currency, ds4);

                DataSet ds6 = objpurchaseCancellation.fetch_voucher_type("GET_ALL_GL_CODE_FOR_CREDIT_NOTE");
                binddropdownlist(row1_drp_glcode, ds6);

                DataSet ds5 = objpurchaseCancellation.fetch_voucher_type("FECH_COMPANYS_BANK");
                // DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE");
                binddropdownlist(row2_drp_gl, ds5);

                DataSet ds8 = objcreditnote.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drpsales_Invoice, ds8);

                row1_drp_currency.Text = "THB";
                row1_drp_currency.Enabled = false;

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
                txt_voucher_date.Text = result;
                update_PurcahseCancellation.Update();


                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
                {
                    invoice_no_tr.Visible = true;


                    DataSet ds0 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    binddropdownlist(drpvoucher_type, ds0);

                    drpvoucher_type.Text = Request["VT"].ToString();

  
                    if (drpvoucher_type.Text == "PURCHASE CANCELLATION")
                    {
                        drp_invoice_no.Visible = true;
                        DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_INVOICE_NO_FOR_PURCHASE_CANCELLATION");
                        binddropdownlist(drp_invoice_no, ds7);
                        drp_invoice_no.Text = Request["IV"].ToString();

                        DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);

                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(Request["IV"].ToString(), Request["VT"].ToString());

                        row2.Attributes.Add("style", "visible");
                        
                        id_1.Visible = true;
                        id_2.Visible = true;
                        id_3.Visible = false;
                        id_4.Visible = false;
                        id_5.Visible = true;
                        id_6.Visible = true;
                        Tr3.Visible = true;
                        lbl_gl.Visible = false;
                        trAgainstSalesInvoice.Visible = false;

                        lbl_client_name.Visible = true;
                        Label64.Text = "Client Name";
                        drp_client_name.Visible = false;

                        DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                        Label65.Text = "Supplier Type";
                        //Label67.Text = "Against Invoice No";
                        Label68.Text = "Due Date";

                        lbl_client_name.Text = ds.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                        lbl_tour_short_name.Text = ds.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();
                        lbl_package_name.Text = ds.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                        //       lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                        lbl_inovice_date.Text = ds.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                        lbl_inovice_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        lbl_currency_name.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                        txtgl_date.Text = ds.Tables[0].Rows[0]["GL_DATE"].ToString();
                        drpsales_Invoice.Text = ds.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                        
                        
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
                }
            }
            btnSave.Visible = false;
            update_PurcahseCancellation.Update();
            updategrid.Update();
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
            if (drpvoucher_type.Text == "PURCHASE CANCELLATION")
            {
                DataSet ds5 = objpurchaseCancellation.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drpsales_Invoice, ds5);
                update_PurcahseCancellation.Update();

                
                //  invoice_no_tr.Attributes.Add("style", "display");

                //invoice_no_tr.Visible = true;
            }
            update_PurcahseCancellation.Update();

        }
        protected void drpsales_Invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds6 = objpurchaseCancellation.Fetch_Invoice_No("FETCH_ALL_PURCHASE_INVOICE_NO_FOR_DEBIT_NOTE", drpsales_Invoice.Text);
            binddropdownlist(drp_invoice_no, ds6);
            update_PurcahseCancellation.Update();
        }
        
        protected void drp_account_grp_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void drp_gl_code_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void drp_invoice_no_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);
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

            DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(drp_invoice_no.Text, "PURCHASE");

            row1_drp_glcode.Text = row1_drp_glcode.Text = ds_check.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

            update_PurcahseCancellation.Update();
            updategrid.Update();
        }

        protected void row1_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            row2_txt_credit.Text = row1_txt_debit.Text;

            row2_txt_credit.Enabled = true;
            row2_txt_debit.Enabled = false;

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;

            row2_drp_gl.Focus();

            ViewState["old_amount"] = row1_txt_debit.Text;
            updategrid.Update();
        }

        protected void row1_txt_credit_TextChanged(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            row2_txt_debit.Text = row1_txt_credit.Text;

            row2_txt_debit.Enabled = false;
            row2_txt_credit.Enabled = true;

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;
            row2_drp_gl.Focus();


            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }

        protected void row1_cb_CheckedChanged(object sender, EventArgs e)
        {


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

            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
            {
                if (drpvoucher_type.Text == "PURCHASE CANCELLATION")
                {

                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 1);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);
                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);

                    insert_voucher_no(drp_invoice_no.Text, drpvoucher_type.Text);
                    Master.DisplayMessage("Purchase Cancellation updated successfully.", "successMessage", 8000);
                    Clear();
                    update_PurcahseCancellation.Update();
                    updategrid.Update();
                }
            }
            else
            {

                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));

                objFITPaymentStoreProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, result, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                objFITPaymentStoreProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, result, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                objFITPaymentStoreProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, result, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                DataSet ds_seq_no = objcreditnote.fetch_seq_no("FETCH_SEQ_NO_FROM_INVOICE_NO", drp_invoice_no.Text, "PURCHASE");
                objFITPaymentStoreProcedure.updte_voucher_status_on_cancel(ds_vsstatus.Tables[0].Rows[4]["AutoSearchResult"].ToString(), int.Parse(ds_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString()));

                insert_voucher_no(drp_invoice_no.Text, drpvoucher_type.Text);
                Master.DisplayMessage("Purchase Cancellation succesfully Done.", "successMessage", 8000);
                Clear();
                update_PurcahseCancellation.Update();
                updategrid.Update();
            }

        }
        public void Clear()
        {

            row1_txt_credit.Text = "";
            row1_txt_debit.Text = "";
            row2_txt_credit.Text = "";
            row2_txt_debit.Text = "";
            row1_drp_glcode.Text = "";
            row2_drp_gl.Text = "";
            drpvoucher_type.Text = "";
            drpvoucher_status.Text = "";
            txt_voucher_date.Text = "";
            txt_narration.Text = "";
            
            drpsales_Invoice.Text = "";
        }
        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(string invoice_no, string voucher_type)
        {
            if (drpvoucher_status.Text == "POSTED")
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
        #endregion
    }
}