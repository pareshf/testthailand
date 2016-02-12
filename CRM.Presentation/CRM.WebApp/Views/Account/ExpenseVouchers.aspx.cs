using System;
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
    public partial class ExpenseVouchers : System.Web.UI.Page
    {
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        ExpenceVoucherSp objEXPENCEVouchersStoredProcedure = new ExpenceVoucherSp();
        PaymentStoredProcedure ObjPaymentStoredProcedure = new PaymentStoredProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 276);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE_FOR_EXPENCE_VOUCHER");
                binddropdownlist(row1_drp_glcode, ds5);
                DataSet ds6 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_CASH");
                binddropdownlist(row2_drp_gl, ds6);
                //DataSet ds1 = objEXPENCEVouchersStoredProcedure.FETCH_INVOICE_EXPENCE("FETCH_ALL_INVOICE_FOR_EXPENCE");
                //binddropdownlist(drp_invoice, ds1);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds4);

                drpvoucher_type.Text = ds4.Tables[0].Rows[10]["AutoSearchResult"].ToString();
                drpvoucher_type.Enabled = false;

                DataSet ds2 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds2);

                drpvoucher_status.Text = "POSTED";

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
                        txt_expdate.Text = result;
                    }
                    else
                    {
                        result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                         txt_expdate.Text = result;
                    }
                }
                else
                {
                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    {
                        result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                        txt_expdate.Text = result;
                    }
                    else
                    {
                        result = w[1] + "/" + w[0] + "/" + t1[0];
                        txt_expdate.Text = result;
                    }
                }
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
            //  r.SelectedValue = "0";
        }
        #endregion

        //protected void drpinvoice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet ds1 = objEXPENCEVouchersStoredProcedure.fectchalldataforexpence("FETCH_DATA_FOR_EXPENCE_PAYMENT", drp_invoice.Text);
        //    row1_drp_glcode.SelectedValue=ds1.Tables[0].Rows[0]["GL_CODE"].ToString();
        //    row1_txt_debit.Text = ds1.Tables[0].Rows[0]["CR_AMOUNT"].ToString();
        //    row1_txt_credit.Text = ds1.Tables[0].Rows[0]["DR_AMOUNT"].ToString();
        //    txt_narration.Text= ds1.Tables[0].Rows[0]["NERRATION"].ToString();
        //    row2.Attributes.Add("style", "display");
        //    drpvoucher_status.SelectedValue=ds1.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
        //     row2_txt_debit.Text= ds1.Tables[0].Rows[1]["CR_AMOUNT"].ToString();
        //     row2_txt_credit.Text = ds1.Tables[0].Rows[1]["DR_AMOUNT"].ToString();
        //    update_voucher.Update();
        //    updategrid.Update();
        //    UpdatePanel2.Update();


        //}

        protected void btnsave_Click(object sender, EventArgs e)
        {

            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", "", drp_invoice.Text, row1_txt_debit.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "Remarks", "", "", "", drpvoucher_status.Text,txt_expdate.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text , false,  1);
            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", row1_drp_glcode.Text, drp_invoice.Text, row1_txt_debit.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "Remarks", "", "", "", drp_invoice.Text, txt_expdate.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, false, 2);
            ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", row2_drp_gl.Text, drp_invoice.Text, row2_txt_credit.Text, drpvoucher_type.Text, "THB", int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "Remarks", "", "", "", "", txt_expdate.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text, false, 2);

            //insert_voucher_no(drp_invoice.Text);

            DataSet fetch_seq_no = objVouchersStoredProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO");

            insert_payment_voucher_no(fetch_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString());
            Master.DisplayMessage("Expence Voucher Generated Successfully.", "successMessage", 8000);
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
            }
            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String invoice_no)
        {
            if (drpvoucher_status.Text == "POSTED")
            {
                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                DataSet ds_vn = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_MAX_VOUCHER_NO_EXPENCE");

                DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());

                DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK_EXPENCE", invoice_no, ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());

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

                        objVouchersStoredProcedure.insert_accounts_voucher_no_expense_voucher(voucher_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), invoice_no);
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

                        objVouchersStoredProcedure.insert_accounts_voucher_no_expense_voucher(voucher_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), invoice_no);
                    }
                }
            }

        }
        #endregion

        protected void insert_payment_voucher_no(string seq_no)
        {
            bool flag = true;
            //DataSet ds = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("GET_PAYMENT_VOUCHER_DETAILS_FOR_VOUCHER_NO", seq_no);

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == "" || ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == null)
            //    {
            //        flag = false;
            //        break;
            //    }
            //}

            if (flag == true)
            {
                DataSet ds1 = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("CHECK_VOUCHER_NO_FOR_PURCHASE_PAYMENT", seq_no);

                if (ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != "" || ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != null)
                {
                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());
                    DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO_FROM_PURCHASE_PAYMENT", ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());
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
    }
}