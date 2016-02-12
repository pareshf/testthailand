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
    public partial class Journal : System.Web.UI.Page
    {

        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
       
        CRM.DataAccess.FIT.FITPaymentStoreProcedure objFITPaymentStoreProcedure = new CRM.DataAccess.FIT.FITPaymentStoreProcedure();
        CRM.DataAccess.Account.VouchersStoredProcedure objVouchersStoredProcedure = new CRM.DataAccess.Account.VouchersStoredProcedure();
        CRM.DataAccess.Account.Journal objjournal = new DataAccess.Account.Journal();


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

                DataSet ds5 = objjournal.fetch_voucher_type("FETCH_ALL_GEL_CODE_FOR_JOURNAL");
                binddropdownlist(row1_drp_glcode, ds5);

                DataSet ds6 = objjournal.fetch_voucher_type("FETCH_ALL_GEL_CODE_FOR_JOURNAL");
                binddropdownlist(row2_drp_gl, ds6);

                DataSet ds1 = objjournal.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds1);

                drpvoucher_type.Text = ds1.Tables[0].Rows[4]["AutoSearchResult"].ToString();
                drpvoucher_type.Enabled = false;

                DataSet ds2 = objjournal.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds2);

                drpvoucher_status.Text = ds2.Tables[0].Rows[5]["AutoSearchResult"].ToString();
                drpvoucher_status.Enabled = false;

                lblGLDate.Visible = false;
                txtgldate.Visible = false;


                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
                {
                    lblGLDate.Visible = true;
                    txtgldate.Visible = true;
                    if (Request["VT"].ToString() == "JOURNAL")
                    {
                        int a = int.Parse(Request["IV"].ToString());
                        DataSet ds0 = objjournal.GetAllData("GET_ALL_JOURNAL_VOUCHER_DETAIL", a, Request["VT"].ToString());
                        lbl_voucher_no.Text = ds0.Tables[0].Rows[0]["VOUCHER_NO"].ToString();
                        txtgldate.Text = ds0.Tables[0].Rows[0]["GL_DATE"].ToString();
                        for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                ViewState["seq_no"] = ds0.Tables[0].Rows[i]["SEQ_NO"].ToString();

                                row1_drp_glcode.Text = ds0.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                row1_txt_credit.Text = ds0.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                row1_txt_debit.Text = ds0.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                                ViewState["old_amount"] = row1_txt_debit.Text;

                                if (row1_txt_credit.Text == "0.00")
                                {
                                    row1_txt_credit.Enabled = true;
                                    
                                }
                                if (row1_txt_debit.Text == "0.00")
                                {
                                    row1_txt_debit.Enabled = true;

                                     
                                }
                                txt_narration.Text = ds0.Tables[0].Rows[0]["NARRATION"].ToString();
                                drpvoucher_status.Text = ds0.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                                ViewState["row1_details_id_1"] = ds0.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            }
                            else if (i == 1)
                            {
                                row2.Attributes.Add("style", "display");
                                row2_drp_gl.Text = ds0.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                                row2_txt_credit.Text = ds0.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                                row2_txt_debit.Text = ds0.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                                if (row2_txt_credit.Text == "0.00")
                                {
                                    row2_txt_credit.Enabled = false;
                                }
                                if (row2_txt_debit.Text == "0.00")
                                {
                                    row2_txt_debit.Enabled = false;
                                }
                                ViewState["row1_details_id_2"] = ds0.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            }
                            txt_narration.Text = ds0.Tables[0].Rows[0]["NARRATION"].ToString();
                            drpvoucher_status.Text = ds0.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
                        }

                    }
                }
            }
            updategrid.Update();
            update_contra.Update();
            UpdatePanel2.Update();
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

        }
        protected void row1_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            row2_txt_credit.Text = row1_txt_debit.Text;

            row2_txt_credit.Enabled = true;
            row2_txt_debit.Enabled = false;
            

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;

            if (row1_txt_debit.Text != "0.00")
            {
                row1_txt_credit.Text = "0.00";
                row2_txt_debit.Text = "0.00";
            }

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

            if (row1_txt_credit.Text != "0.00")
            {
                row1_txt_debit.Text = "0.00";
                row2_txt_credit.Text = "0.00";
            }

            row2_drp_currency.Text = "THB";
            row2_drp_currency.Enabled = false;
            row2_drp_gl.Focus();


            ViewState["old_amount"] = row1_txt_credit.Text;
            updategrid.Update();
        }
        protected void row1_cb_CheckedChanged(object sender, EventArgs e)
        {

        }

        #region Save
        protected void btnsave_Click(object sender, EventArgs e)
        {
            System.DateTime today = DateTime.Now;
            string source = today.ToString("dd/MM/yyyy");

            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
            {
               
                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgldate.Text, 1);
                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgldate.Text, 2);
                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), txtgldate.Text, 2);

                insert_voucher_no(Request["IV"].ToString());

                Master.DisplayMessage("Journal Voucher updated Successfully.", "successMessage", 8000);
            }
            else
            {


                objAcoountVouchersStoredProcedure.insert_accounts_entry_CONTRA_JOURNAL(0, row1_drp_glcode.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), "THB", 1);
                objAcoountVouchersStoredProcedure.insert_accounts_entry_CONTRA_JOURNAL(0, row1_drp_glcode.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), "THB", 2);
                objAcoountVouchersStoredProcedure.insert_accounts_entry_CONTRA_JOURNAL(0, row2_drp_gl.Text, "", source, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), "THB", 2);

                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO_FOR_ACCOUNT_VOUCHER");
                insert_voucher_no(ds_vt.Tables[0].Rows[0]["SEQ_NO"].ToString());
                Master.DisplayMessage("Contra Voucher Generated Successfully.", "successMessage", 8000);

                Master.DisplayMessage("Journal Voucher Generated Successfully.", "successMessage", 8000);
            }
        }
        #endregion

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String seq_no)
        {
            if (drpvoucher_status.Text == "POSTED")
            {
                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", ds_vt.Tables[0].Rows[4]["AutoSearchResult"].ToString());

                DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[4]["AutoSearchResult"].ToString());

                DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK_CONTRA_JOURNAL", seq_no, ds_vt.Tables[0].Rows[4]["AutoSearchResult"].ToString());

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

                        objVouchersStoredProcedure.update_accounts_voucher_no_contra_journal(seq_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[4]["AutoSearchResult"].ToString(), voucher_no);
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

                        objVouchersStoredProcedure.update_accounts_voucher_no_contra_journal(seq_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[4]["AutoSearchResult"].ToString(), voucher_no);
                    }
                }
            }
        }
        #endregion
    }

}