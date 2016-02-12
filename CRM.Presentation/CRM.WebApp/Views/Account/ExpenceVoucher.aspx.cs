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
    public partial class ExpenceVoucher : System.Web.UI.Page
    {
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        ExpenceVoucherSp objEXPENCEVouchersStoredProcedure = new ExpenceVoucherSp();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();

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
                DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE_FOR_EXPENCE_VOUCHER");
                binddropdownlist(row1_drp_glcode, ds5);
                DataSet ds6 = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_CASH");
                binddropdownlist(row2_drp_gl, ds6);
                DataSet ds1 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds1);
                DataSet ds2 = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds2);
                DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ALL_INVOICE_NO");
                binddropdownlist(DropDownList1, dsval);
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

                drpvoucher_status.Text = "POSTED";
                drpvoucher_type.Text = "EXPENCE";

                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
                {
                    DataSet ds = objEXPENCEVouchersStoredProcedure.getDataForEditMode(int.Parse(Request.QueryString["IV"].ToString()));

                    DropDownList1.Text = ds.Tables[0].Rows[0]["AGAINST_SALES_INVOICE"].ToString();
                    txtgl_date.Text = ds.Tables[0].Rows[0]["GL_DATE"].ToString();
                    txt_narration.Text = ds.Tables[0].Rows[0]["NERRATION"].ToString();
                    txt_expdate.Text = ds.Tables[0].Rows[0]["VOUCHER_DATE"].ToString();
                    txt_expinvoice.Text = ds.Tables[0].Rows[0]["INVOICE_NO"].ToString();

                    ViewState["row1"] = ds.Tables[0].Rows[1]["EXPENCE_VOUCHER_DETAILS_ID"].ToString();

                    

                    row1_drp_glcode.Text = ds.Tables[0].Rows[1]["GL_DESCRIPTION"].ToString();
                    row2_drp_gl.Text = ds.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                    ViewState["row2"] = ds.Tables[0].Rows[0]["EXPENCE_VOUCHER_DETAILS_ID"].ToString();

                    row1_txt_debit.Text = ds.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();
                    row2_txt_credit.Text = ds.Tables[0].Rows[0]["VOUCHER_AMOUNT"].ToString();

                    drpvoucher_status.Text = ds.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                    row2.Attributes.Add("style", "display");

                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VOUCHER_NO"].ToString();

                    trVoucherNo.Visible = true;

                    if (drpvoucher_status.Text == "POSTED")
                    {
                        disableControls();

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
  
        }
        #endregion

        #region SAVE RECORDS
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
            {
                if (row1_drp_glcode.Text == "")
                {
                    Master.DisplayMessage("Please, Select all General Ledger Code.", "successMessage", 8000);
                }
                else if (row2_drp_gl.Text == "")
                {
                    Master.DisplayMessage("Please, Select all General Ledger Code.", "successMessage", 8000);
                }
                else
                {
                    objEXPENCEVouchersStoredProcedure.updateExpenceVoucher(int.Parse(Request.QueryString["IV"].ToString()), row1_drp_glcode.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", 1, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);
                    objEXPENCEVouchersStoredProcedure.updateExpenceVoucher(int.Parse(ViewState["row1"].ToString()), row1_drp_glcode.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", 2, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);
                    objEXPENCEVouchersStoredProcedure.updateExpenceVoucher(int.Parse(ViewState["row2"].ToString()), row2_drp_gl.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", 2, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);

                    if (drpvoucher_status.Text == "POSTED")
                    {
                        voucherNoGenerate(Request.QueryString["IV"].ToString());
                    }

                    Master.DisplayMessage("Expence Voucher Updated Successfully.", "successMessage", 8000);
                }
            }
            else
            {
                if (row1_drp_glcode.Text == "")
                {
                    Master.DisplayMessage("Please, Select all General Ledger Code.", "successMessage", 8000);
                }
                else if (row2_drp_gl.Text == "")
                {
                    Master.DisplayMessage("Please, Select all General Ledger Code.", "successMessage", 8000);
                }
                else
                {
                    objEXPENCEVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", 1, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);
                    objEXPENCEVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", 2, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);
                    objEXPENCEVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, txt_expinvoice.Text, txt_expdate.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), txt_narration.Text, drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", 2, DropDownList1.Text, int.Parse(Session["CompanyId"].ToString()), txtgl_date.Text);

                    if (drpvoucher_status.Text == "POSTED")
                    {
                        DataSet ds = objEXPENCEVouchersStoredProcedure.commonSp("FETCH_MAX_EXPENCE_VOUCHER_ID");
                        voucherNoGenerate(ds.Tables[0].Rows[0]["EXPENCE_VOUCHER_ID"].ToString());

                    }
                    


                    Master.DisplayMessage("Expence Voucher Generated Successfully.", "successMessage", 8000);
                    Response.Redirect("~/Views/Account/SearchExpenceVoucher.aspx");
                }
            }
        }
        #endregion

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

            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }

        protected void voucherNoGenerate(string seqNo)
        {
            DataSet ds1 = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("CHECK_VOUCHER_NO_FOR_EXPENCE", seqNo);

            if (ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != "" || ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != null)
            {
                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());
                DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO_FOR_EXPENCE", ds_vt.Tables[0].Rows[10]["AutoSearchResult"].ToString());
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

                    objEXPENCEVouchersStoredProcedure.insertVoucherNoforExpence("INSERT_VOUCHER_NO_FOR_EXPENCE", ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seqNo, voucher_no);
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

                    string str11 = ds_vn.Tables[0].Rows[0]["VOUCHER_NO"].ToString(); 
                    
                    string[] words = str11.Split('-');
                    string no = (int.Parse(words[2].ToString()) + 01).ToString();
                    int len = no.Length;
                    for (int i = 0; i < 5 - len; i++)
                    {
                        no = "0" + no;
                    }
                    voucher_no = "V" + year + "-" + ds_vn_code.Tables[0].Rows[0]["VOUCHER_NO_CODE"].ToString() + "-" + no;
                    objEXPENCEVouchersStoredProcedure.insertVoucherNoforExpence("INSERT_VOUCHER_NO_FOR_EXPENCE",ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), seqNo, voucher_no);
                }

            }
        }

        protected void disableControls()
        {
            btnSave.Visible = false;
            row1_drp_glcode.Enabled = false;
            row2_drp_gl.Enabled = false;

            row1_txt_debit.Enabled = false;
            row2_txt_credit.Enabled = false;

            drpvoucher_status.Enabled = false;
            DropDownList1.Enabled = false;
            txt_narration.Enabled  = false;
        }
    }
}