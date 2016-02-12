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
    public partial class AccountsVoucher : System.Web.UI.Page
    {
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        #region VARIABLES
        bool flag_valid = true;
        bool flag_date = true;
        bool flag_require = true;
        bool flag_amount = true;
        #endregion

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 240);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //txt_ex_rate.Attributes.Add("onBlur", "__doPostBack('TextBox1','');");

            if (!IsPostBack)
            {
                DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_group_name("FETCH_ACCOUNT_GROUP_NAME");
                binddropdownlist(drp_account_grp, ds6);


                DataSet ds = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drpvoucher_type, ds);

                for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                {
                    ListItem item = drpvoucher_type.Items[i];
                    if (item.ToString() == "SALES" || item.ToString() == "PURCHASE")
                    {

                    }
                    else
                    {
                        drpvoucher_type.Items.Remove(item);
                    }
                }



                DataSet ds1 = objAcoountVouchersStoredProcedure.fetch_voucher_status("FETCH_VOUCHER_STATUS_ID");
                binddropdownlist(drpvoucher_status, ds1);

                DataSet ds2 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_BANK_NAME");
                binddropdownlist(drpbank_name, ds2);

                DataSet ds3 = objAcoountVouchersStoredProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
                binddropdownlist(drppayment_mode, ds3);

                //       DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");
                //       binddropdownlist(drp_invoice_no, ds7);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row1_drp_currency, ds5);

                row1_drp_currency.Text = "THB";
                row1_drp_currency.Enabled = false;
                //  txt_voucher_date.Text  = DateTime.Now.ToShortDateString();
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
                        txt_voucher_date.Text = result;
                    }
                    else
                    {
                        result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                        txt_voucher_date.Text = result;
                    }
                }
                else
                {
                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    {
                        result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                        txt_voucher_date.Text = result;
                    }
                    else
                    {
                        result = w[1] + "/" + w[0] + "/" + t1[0];
                        txt_voucher_date.Text = result;
                    }
                }
                txt_voucher_date.Enabled = false;


                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()) && Request["VT"] != null && !string.IsNullOrEmpty(Request["VT"].ToString()))
                {
                    if (Request["VT"].ToString() == "SALES")
                    {

                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(Request["IV"].ToString(), Request["VT"].ToString());
                        DataSet ds_gl_code = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                        drpvoucher_type.Text = Request["VT"].ToString();

                        DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");
                        binddropdownlist(drp_invoice_no, ds7);

                        invoice_no_tr.Visible = true;
                        drp_invoice_no.Text = Request["IV"].ToString();
                        row2.Attributes.Add("style", "visible");

                        drp_invoice_no.Visible = true;

                        DataSet ds52 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", Request["IV"].ToString());

                        id_1.Visible = true;
                        id_2.Visible = true;
                        id_3.Visible = true;
                        id_4.Visible = true;
                        id_5.Visible = true;
                        id_6.Visible = true;

                        lbl_client_name.Visible = true;
                        Label64.Text = "Client Name";
                        drp_client_name.Visible = false;

                        //   DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                        //DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                        lblVoucher_no.Text = ds_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString();
                        lblgl_date.Text = ds_check.Tables[0].Rows[0]["GL_DATE"].ToString();

                        lbl_client_name.Text = ds52.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                        lbl_tour_short_name.Text = ds52.Tables[0].Rows[0]["TOUR_SHORT_NAME"].ToString();
                        lbl_package_name.Text = ds52.Tables[0].Rows[0]["FIT_PACKAGE_NAME"].ToString();
                        lbl_created_by.Text = ds52.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                        lbl_inovice_date.Text = ds52.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                        lbl_inovice_amount.Text = ds52.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        lbl_currency_name.Text = ds52.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();


                        if (ds_check.Tables[0].Rows.Count != 0)
                        {
                            if (drpvoucher_type.Text == "PURCHASE" || drpvoucher_type.Text == "SALES")
                            {
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
                        }
                    }
                    else if (Request["VT"].ToString() == "PURCHASE")
                    {
                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(Request["IV"].ToString(), Request["VT"].ToString());
                        DataSet ds_gl_code = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                        drpvoucher_type.Text = Request["VT"].ToString();

                        DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", Request["IV"].ToString());
                   //     DataSet ds_tp_supplier = objAcoountVouchersStoredProcedure.fetch_common_data("FETCH_TP_SUPPLIER_NO", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString());
                  //      DataSet ds_tp_data = objAcoountVouchersStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString(), ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString());

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

                        lblVoucher_no.Text = ds_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString();
                        lblgl_date.Text = ds_check.Tables[0].Rows[0]["GL_DATE"].ToString();
                        lbl_client_name.Text = ds11.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                        lbl_tour_short_name.Text = ds11.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();
                        lbl_package_name.Text = ds11.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                        //       lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                        lbl_inovice_date.Text = ds11.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                        lbl_inovice_amount.Text = ds11.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        lbl_currency_name.Text = ds11.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();

                        if (ds_check.Tables[0].Rows.Count != 0)
                        {
                            if (drpvoucher_type.Text == "PURCHASE" || drpvoucher_type.Text == "SALES")
                            {
                                for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        DataSet ds51 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                                        binddropdownlist(row1_drp_glcode, ds51);

                                        ViewState["seq_no"] = ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString();
                                        row1_drp_glcode.Enabled = false;
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
                                        row2.Attributes.Add("style", "visible");
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
                                updategrid.Update();


                            }
                        }
                    }
                    btnSave.Visible = false;
                    update_btn.Update();
                }
                else
                {
                    //    DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_group_name("FETCH_ACCOUNT_GROUP_NAME");
                    //    binddropdownlist(drp_account_grp, ds6);


                    //    DataSet ds = objAcoountVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    //    binddropdownlist(drpvoucher_type, ds);

                    //    for (int i = drpvoucher_type.Items.Count - 1; i > 0; i--)
                    //    {
                    //        ListItem item = drpvoucher_type.Items[i];
                    //        if (item.ToString() == "SALES" || item.ToString() == "PURCHASE")
                    //        {

                    //        }
                    //        else
                    //        {
                    //            drpvoucher_type.Items.Remove(item);
                    //        }
                    //    }



                    //    DataSet ds1 = objAcoountVouchersStoredProcedure.fetch_voucher_status("FETCH_VOUCHER_STATUS_ID");
                    //    binddropdownlist(drpvoucher_status, ds1);

                    //    DataSet ds2 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_BANK_NAME");
                    //    binddropdownlist(drpbank_name, ds2);

                    //    DataSet ds3 = objAcoountVouchersStoredProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
                    //    binddropdownlist(drppayment_mode, ds3);

                    //    //       DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");
                    //    //       binddropdownlist(drp_invoice_no, ds7);

                    //    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    //    binddropdownlist(row1_drp_currency, ds5);

                    //    row1_drp_currency.Text = "THB";
                    //    row1_drp_currency.Enabled = false;
                    //    //  txt_voucher_date.Text  = DateTime.Now.ToShortDateString();
                    //    string result;
                    //    System.DateTime today = DateTime.Now;
                    //    today.ToString("MM-dd-yyyy");
                    //    string source = today.ToString();
                    //    string str1 = source;
                    //    string[] w = str1.Split('/');

                    //    string t = w[2];
                    //    string[] t1 = t.Split(' ');

                    //    if (w[1] == "1" || w[1] == "2" || w[1] == "3" || w[1] == "4" || w[1] == "5" || w[1] == "6" || w[1] == "7" || w[1] == "8" || w[1] == "9")
                    //    {
                    //        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    //        {
                    //            result = "0" + w[1] + "/" + "0" + w[0] + "/" + t1[0];
                    //            txt_voucher_date.Text = result;
                    //        }
                    //        else
                    //        {
                    //            result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                    //            txt_voucher_date.Text = result;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    //        {
                    //            result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                    //            txt_voucher_date.Text = result;
                    //        }
                    //        else
                    //        {
                    //            result = w[1] + "/" + w[0] + "/" + t1[0];
                    //            txt_voucher_date.Text = result;
                    //        }
                    //    }
                    //    txt_voucher_date.Enabled = false;

                    //}
                }
            }
        }

        #region SAVE BUTTON
        protected void btnSave_Click(object sender, EventArgs e)
        {
            validation();
            validation_required();
            validation_amount();
            if (flag_valid == false)
            {
                Master.DisplayMessage(ViewState["error_msg"].ToString(), "successMessage", 8000);
            }
            else if (flag_date == false)
            {
                Master.DisplayMessage("Entered date is not in valid format", "successMessage", 8000);
            }
            else if (flag_require == false)
            {
                Master.DisplayMessage(ViewState["error_require"].ToString(), "successMessage", 8000);
            }
            else if (flag_amount == false)
            {
                Master.DisplayMessage("Can not take more amount then Invoice amount.", "successMessage", 8000);
            }
            else
            {
                if (ViewState["seq_no"] != null)
                {

                    
                    if (row1_txt_credit.Text != "" && (row1_txt_debit.Text == "" || row1_txt_debit.Text == "0.00"))
                    {

                        if (drpvoucher_type.Text == "PURCHASE" || drpvoucher_type.Text == "RECEIPT")
                        {
                            if (drpvoucher_type.Text == "RECEIPT")
                            {
                                if (ViewState["rb1"] != null && ViewState["row1_details_id_1"] != null)
                                {
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                }
                                else
                                {
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                }
                            }

                            else
                            {
                                if (drpvoucher_type.Text == "PURCHASE")
                                {
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);

                                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_balance_type();
                                    DataSet ds_all_gl_code = objAcoountVouchersStoredProcedure.fetch_all_gl_code();

                                    DataSet ds_sup_type = objAcoountVouchersStoredProcedure.fetch_group_name("FETCH_SUPPLIER_TYPE");
                                    if (lbl_tour_short_name.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
                                    {
                                        DataSet ds1 = objAcoountVouchersStoredProcedure.fetch_hotel_data("FETCH_INVOICE_NO_DETAILS_FROM_SUPPLIER", lbl_package_name.Text, lbl_client_name.Text );
                                       // DataSet dsgl_hotel = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds1.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");

                                        DataSet ds_hotel_amount = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds1.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");
                                        //if (ds_hotel_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds_hotel_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                        //{
                                        //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds1.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                        //}
                                        //else
                                        //{
                                        //    string total_cl_amount = (decimal.Parse(ds_hotel_amount.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_credit.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                        //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds1.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", ds_hotel_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds_hotel_amount.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                        //}
                                    }
                                    if (lbl_tour_short_name.Text == "Transfer Package Company")
                                    {
                                   
                                         DataSet ds_tp_data = objAcoountVouchersStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", lbl_package_name.Text,  lbl_client_name.Text);

                                         DataSet ds_tp_amount = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds_tp_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");
                                         //if (ds_tp_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds_tp_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                         //{
                                         //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds_tp_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                         //}
                                         //else
                                         //{
                                         //    string total_cl_amount1 = (decimal.Parse(ds_tp_amount.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_credit.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                         //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds_tp_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", ds_tp_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds_tp_amount.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount1, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                         //}
                                    }
                                    if (lbl_tour_short_name.Text == "Sightseeing Company")
                                    {
                                        DataSet ds_ss_data = objAcoountVouchersStoredProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_FOR_PURCHASE", lbl_package_name.Text, lbl_client_name.Text);
                                        DataSet ds_ss_amount = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds_ss_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");
                                        if (ds_ss_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds_ss_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                        {
                                            objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds_ss_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                        }
                                        else
                                        {
                                            string total_cl_amount1 = (decimal.Parse(ds_ss_amount.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_credit.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                            objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds_ss_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString()), "S", ds_ss_amount.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds_ss_amount.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount1, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                        }
                                    }
                                        
                                        DataSet ds_amount_fit_package_hotel = objAcoountVouchersStoredProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString());
                                        //  DataSet ds_amount_fit_package_hotel = objFITPaymentStoreProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[2]["GL_CODE"].ToString());
                                        //if (ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                        //{
                                        //    objAcoountVouchersStoredProcedure.update_fit_amount(ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                        //}
                                        //else
                                        //{
                                        //    string total_cl_amount = (decimal.Parse(ds_amount_fit_package_hotel.Tables[0].Rows[0]["CL_BALANCE"].ToString()) - decimal.Parse(row1_txt_credit.Text) + decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                        //    objAcoountVouchersStoredProcedure.update_fit_amount(ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString(), ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                        //}
                                    }
                                    //if (ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                    //{
                                    //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                    //}
                                    //else
                                    //{
                                    //    string total_cl_amount = (decimal.Parse(ds6.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_debit.Text)).ToString();
                                    //    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds6.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                    //}
                                    //   Master.DisplayMessage("Purchase Voucher entry save successfully.", "successMessage", 8000);
                              //  }


                            }
                        }
                    }
                        else if ((row1_txt_credit.Text == "" || row1_txt_credit.Text == "0.00") && row1_txt_debit.Text != "")
                        {
                            if (drpvoucher_type.Text == "SALES")
                            {
                                DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                                DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                                DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_balance_type();

                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);

                                //     Master.DisplayMessage("Sales Voucher entry save successfully.", "successMessage", 8000);
                                if (ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                {
                                    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                }
                                else
                                {
                                    string total_cl_amount = (decimal.Parse(ds6.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_debit.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                    objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds6.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                }

                                DataSet ds_all_gl_code = objAcoountVouchersStoredProcedure.fetch_all_gl_code();
                                DataSet ds_amount_fit_package_hotel = objAcoountVouchersStoredProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString());
                                //  DataSet ds_amount_fit_package_hotel = objFITPaymentStoreProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[2]["GL_CODE"].ToString());
                                if (ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                                {
                                    objAcoountVouchersStoredProcedure.update_fit_amount(ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString(), row1_txt_credit.Text, ds7.Tables[0].Rows[0]["BAL_TYPE_NAME"].ToString());
                                }
                                else
                                {
                                    string total_cl_amount = (decimal.Parse(ds_amount_fit_package_hotel.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_credit.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                                    objAcoountVouchersStoredProcedure.update_fit_amount(ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString(), ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds_amount_fit_package_hotel.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                                }
                            }
                            else if (drpvoucher_type.Text == "PAYMENT")
                            {
                                if (ViewState["rb1"] != null)
                                {
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                }
                                else
                                {
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 1);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                    objAcoountVouchersStoredProcedure.update_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                }
                            }
                        }
                    }
                
                else
                {

                    if (drpvoucher_type.Text == "PURCHASE" || drpvoucher_type.Text == "RECEIPT")
                    {
                        drpvoucher_status.Text = "CREATED";
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["rb1"] != null)
                            {
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), 2);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), 2);
                            }
                            else
                            {
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            }
                        }

                        else
                        {
                            if (drpvoucher_type.Text == "PURCHASE")
                            {
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 2);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 2);

                                //   Master.DisplayMessage("Purchase Voucher entry save successfully.", "successMessage", 8000);
                            }


                        }
                    }
                    else if (row1_txt_credit.Text == "" && row1_txt_debit.Text != "")
                    {
                        drpvoucher_status.Text = "CREATED";
                        if (drpvoucher_type.Text == "SALES")
                        {
                            DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);
                            DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());

                            DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_balance_type();

                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 2);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 2);

                            if (ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "" || ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString() == "0.00")
                            {
                                objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString(), row1_txt_debit.Text, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                            }
                            else
                            {
                                string total_cl_amount = (decimal.Parse(ds6.Tables[0].Rows[0]["CL_BALANCE"].ToString()) + decimal.Parse(row1_txt_debit.Text)).ToString();
                                objAcoountVouchersStoredProcedure.update_chart_of_account(int.Parse(ds.Tables[0].Rows[0]["CUST_ID"].ToString()), ds.Tables[0].Rows[0]["FLAG"].ToString(), ds6.Tables[0].Rows[0]["OP_BALANCE"].ToString(), ds6.Tables[0].Rows[0]["OP_TYPE"].ToString(), total_cl_amount, ds7.Tables[0].Rows[1]["BAL_TYPE_NAME"].ToString());
                            }
                            //     Master.DisplayMessage("Sales Voucher entry save successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["rb1"] != null)
                            {
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), 2);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, ViewState["payment_mode_1"].ToString(), ViewState["cheque_no_1"].ToString(), ViewState["cheque_date_1"].ToString(), ViewState["bank_name_1"].ToString(), ViewState["branch_1"].ToString(), "Remarks", ViewState["receipt_no_1"].ToString(), ViewState["ex_rate_1"].ToString(), ViewState["amount_1"].ToString(), ViewState["currency_1"].ToString(), 2);
                            }
                            else
                            {
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row2_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row2_txt_credit.Text, row2_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            }
                        }
                    }
                }

                

                    // SECOND ENTRY
                    if (row3_txt_credit.Text != "" && (row3_txt_debit.Text == "" || row3_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_3"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_3"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_4"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb3"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row3_txt_credit.Text, row3_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row4_txt_credit.Text, row4_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row3_txt_credit.Text == "" || row3_txt_credit.Text == "0.00") && row3_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_3"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_3"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_4"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb3"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), 2);
                                }
                                else
                                {
                                    //   objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row3_txt_credit.Text, row3_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row4_txt_credit.Text, row4_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }

                    // THIRD ENTRY
                    if (row5_txt_credit.Text != "" && (row5_txt_debit.Text == "" || row5_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_5"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_3"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_5"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_4"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_6"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb5"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row5_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row5_txt_credit.Text, row5_txt_debit.Text, ViewState["payment_mode_5"].ToString(), ViewState["cheque_no_5"].ToString(), ViewState["cheque_date_5"].ToString(), ViewState["bank_name_5"].ToString(), ViewState["branch_5"].ToString(), "Remarks", ViewState["receipt_no_5"].ToString(), ViewState["ex_rate_5"].ToString(), ViewState["amount_5"].ToString(), ViewState["currency_5"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row6_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row6_txt_credit.Text, row6_txt_debit.Text, ViewState["payment_mode_5"].ToString(), ViewState["cheque_no_5"].ToString(), ViewState["cheque_date_5"].ToString(), ViewState["bank_name_5"].ToString(), ViewState["branch_5"].ToString(), "Remarks", ViewState["receipt_no_5"].ToString(), ViewState["ex_rate_5"].ToString(), ViewState["amount_5"].ToString(), ViewState["currency_5"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row5_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row5_txt_credit.Text, row5_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row6_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row6_txt_credit.Text, row6_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row5_txt_credit.Text == "" || row5_txt_credit.Text == "0.00") && row5_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_5"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_5"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_6"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb5"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row5_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row5_txt_credit.Text, row5_txt_debit.Text, ViewState["payment_mode_5"].ToString(), ViewState["cheque_no_5"].ToString(), ViewState["cheque_date_5"].ToString(), ViewState["bank_name_5"].ToString(), ViewState["branch_5"].ToString(), "Remarks", ViewState["receipt_no_5"].ToString(), ViewState["ex_rate_5"].ToString(), ViewState["amount_5"].ToString(), ViewState["currency_5"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row6_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row6_txt_credit.Text, row6_txt_debit.Text, ViewState["payment_mode_5"].ToString(), ViewState["cheque_no_5"].ToString(), ViewState["cheque_date_5"].ToString(), ViewState["bank_name_5"].ToString(), ViewState["branch_5"].ToString(), "Remarks", ViewState["receipt_no_5"].ToString(), ViewState["ex_rate_5"].ToString(), ViewState["amount_5"].ToString(), ViewState["currency_5"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row5_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row5_txt_credit.Text, row5_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row6_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row6_txt_credit.Text, row6_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }

                    // FOURTH ENTRY
                    if (row7_txt_credit.Text != "" && (row7_txt_debit.Text == "" || row7_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_7"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_7"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_8"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb7"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row7_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row7_txt_credit.Text, row7_txt_debit.Text, ViewState["payment_mode_7"].ToString(), ViewState["cheque_no_7"].ToString(), ViewState["cheque_date_7"].ToString(), ViewState["bank_name_7"].ToString(), ViewState["branch_7"].ToString(), "Remarks", ViewState["receipt_no_7"].ToString(), ViewState["ex_rate_7"].ToString(), ViewState["amount_7"].ToString(), ViewState["currency_7"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row8_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row8_txt_credit.Text, row8_txt_debit.Text, ViewState["payment_mode_7"].ToString(), ViewState["cheque_no_7"].ToString(), ViewState["cheque_date_7"].ToString(), ViewState["bank_name_7"].ToString(), ViewState["branch_7"].ToString(), "Remarks", ViewState["receipt_no_7"].ToString(), ViewState["ex_rate_7"].ToString(), ViewState["amount_7"].ToString(), ViewState["currency_7"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row7_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row7_txt_credit.Text, row7_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row8_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row8_txt_credit.Text, row8_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row7_txt_credit.Text == "" || row7_txt_credit.Text == "0.00") && row7_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_7"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_7"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_8"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb7"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row7_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row7_txt_credit.Text, row7_txt_debit.Text, ViewState["payment_mode_7"].ToString(), ViewState["cheque_no_7"].ToString(), ViewState["cheque_date_7"].ToString(), ViewState["bank_name_7"].ToString(), ViewState["branch_7"].ToString(), "Remarks", ViewState["receipt_no_7"].ToString(), ViewState["ex_rate_7"].ToString(), ViewState["amount_7"].ToString(), ViewState["currency_7"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row8_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row8_txt_credit.Text, row8_txt_debit.Text, ViewState["payment_mode_7"].ToString(), ViewState["cheque_no_7"].ToString(), ViewState["cheque_date_7"].ToString(), ViewState["bank_name_7"].ToString(), ViewState["branch_7"].ToString(), "Remarks", ViewState["receipt_no_7"].ToString(), ViewState["ex_rate_7"].ToString(), ViewState["amount_7"].ToString(), ViewState["currency_7"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row7_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row7_txt_credit.Text, row7_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row8_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row8_txt_credit.Text, row8_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }

                    // FIFTH ENTRY

                    if (row9_txt_credit.Text != "" && (row9_txt_debit.Text == "" || row9_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_9"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_9"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_10"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb9"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row9_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row9_txt_credit.Text, row9_txt_debit.Text, ViewState["payment_mode_9"].ToString(), ViewState["cheque_no_9"].ToString(), ViewState["cheque_date_9"].ToString(), ViewState["bank_name_9"].ToString(), ViewState["branch_9"].ToString(), "Remarks", ViewState["receipt_no_9"].ToString(), ViewState["ex_rate_9"].ToString(), ViewState["amount_9"].ToString(), ViewState["currency_9"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row10_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row10_txt_credit.Text, row10_txt_debit.Text, ViewState["payment_mode_9"].ToString(), ViewState["cheque_no_9"].ToString(), ViewState["cheque_date_9"].ToString(), ViewState["bank_name_9"].ToString(), ViewState["branch_9"].ToString(), "Remarks", ViewState["receipt_no_9"].ToString(), ViewState["ex_rate_9"].ToString(), ViewState["amount_9"].ToString(), ViewState["currency_9"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row9_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row9_txt_credit.Text, row9_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row10_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row10_txt_credit.Text, row10_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row9_txt_credit.Text == "" || row9_txt_credit.Text == "0.00") && row9_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_9"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_9"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_11"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb9"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row9_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row9_txt_credit.Text, row9_txt_debit.Text, ViewState["payment_mode_9"].ToString(), ViewState["cheque_no_9"].ToString(), ViewState["cheque_date_9"].ToString(), ViewState["bank_name_9"].ToString(), ViewState["branch_9"].ToString(), "Remarks", ViewState["receipt_no_9"].ToString(), ViewState["ex_rate_9"].ToString(), ViewState["amount_9"].ToString(), ViewState["currency_9"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row10_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row10_txt_credit.Text, row10_txt_debit.Text, ViewState["payment_mode_9"].ToString(), ViewState["cheque_no_9"].ToString(), ViewState["cheque_date_9"].ToString(), ViewState["bank_name_9"].ToString(), ViewState["branch_9"].ToString(), "Remarks", ViewState["receipt_no_9"].ToString(), ViewState["ex_rate_9"].ToString(), ViewState["amount_9"].ToString(), ViewState["currency_9"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row9_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row9_txt_credit.Text, row9_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row10_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row10_txt_credit.Text, row10_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }

                    // SIXTH ENTRY

                    if (row11_txt_credit.Text != "" && (row11_txt_debit.Text == "" || row11_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_11"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_11"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_12"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb11"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row11_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row11_txt_credit.Text, row11_txt_debit.Text, ViewState["payment_mode_11"].ToString(), ViewState["cheque_no_11"].ToString(), ViewState["cheque_date_11"].ToString(), ViewState["bank_name_11"].ToString(), ViewState["branch_11"].ToString(), "Remarks", ViewState["receipt_no_11"].ToString(), ViewState["ex_rate_11"].ToString(), ViewState["amount_11"].ToString(), ViewState["currency_11"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row12_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row12_txt_credit.Text, row12_txt_debit.Text, ViewState["payment_mode_11"].ToString(), ViewState["cheque_no_11"].ToString(), ViewState["cheque_date_11"].ToString(), ViewState["bank_name_11"].ToString(), ViewState["branch_11"].ToString(), "Remarks", ViewState["receipt_no_11"].ToString(), ViewState["ex_rate_11"].ToString(), ViewState["amount_11"].ToString(), ViewState["currency_11"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row11_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row11_txt_credit.Text, row11_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row12_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row12_txt_credit.Text, row12_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row11_txt_credit.Text == "" || row11_txt_credit.Text =="0.00" ) && row11_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_11"] != null )
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_11"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_12"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb11"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row11_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row11_txt_credit.Text, row11_txt_debit.Text, ViewState["payment_mode_11"].ToString(), ViewState["cheque_no_11"].ToString(), ViewState["cheque_date_11"].ToString(), ViewState["bank_name_11"].ToString(), ViewState["branch_11"].ToString(), "Remarks", ViewState["receipt_no_11"].ToString(), ViewState["ex_rate_11"].ToString(), ViewState["amount_11"].ToString(), ViewState["currency_11"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row12_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row12_txt_credit.Text, row12_txt_debit.Text, ViewState["payment_mode_11"].ToString(), ViewState["cheque_no_11"].ToString(), ViewState["cheque_date_11"].ToString(), ViewState["bank_name_11"].ToString(), ViewState["branch_11"].ToString(), "Remarks", ViewState["receipt_no_11"].ToString(), ViewState["ex_rate_11"].ToString(), ViewState["amount_11"].ToString(), ViewState["currency_11"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row11_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row11_txt_credit.Text, row11_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row12_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row12_txt_credit.Text, row12_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    // SEVENTH ENTRY
                    if (row13_txt_credit.Text != "" && (row13_txt_debit.Text == "" || row13_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_13"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_13"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_14"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb13"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row13_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row13_txt_credit.Text, row13_txt_debit.Text, ViewState["payment_mode_13"].ToString(), ViewState["cheque_no_13"].ToString(), ViewState["cheque_date_13"].ToString(), ViewState["bank_name_13"].ToString(), ViewState["branch_13"].ToString(), "Remarks", ViewState["receipt_no_13"].ToString(), ViewState["ex_rate_13"].ToString(), ViewState["amount_13"].ToString(), ViewState["currency_13"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row14_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row14_txt_credit.Text, row14_txt_debit.Text, ViewState["payment_mode_13"].ToString(), ViewState["cheque_no_13"].ToString(), ViewState["cheque_date_13"].ToString(), ViewState["bank_name_13"].ToString(), ViewState["branch_13"].ToString(), "Remarks", ViewState["receipt_no_13"].ToString(), ViewState["ex_rate_13"].ToString(), ViewState["amount_13"].ToString(), ViewState["currency_13"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row13_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row13_txt_credit.Text, row13_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row14_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row14_txt_credit.Text, row14_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row13_txt_credit.Text == "" || row13_txt_credit.Text == "0.00" ) && row13_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_13"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_13"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_14"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb13"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row13_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row13_txt_credit.Text, row13_txt_debit.Text, ViewState["payment_mode_13"].ToString(), ViewState["cheque_no_13"].ToString(), ViewState["cheque_date_13"].ToString(), ViewState["bank_name_13"].ToString(), ViewState["branch_13"].ToString(), "Remarks", ViewState["receipt_no_13"].ToString(), ViewState["ex_rate_13"].ToString(), ViewState["amount_13"].ToString(), ViewState["currency_13"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row14_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row14_txt_credit.Text, row14_txt_debit.Text, ViewState["payment_mode_13"].ToString(), ViewState["cheque_no_13"].ToString(), ViewState["cheque_date_13"].ToString(), ViewState["bank_name_13"].ToString(), ViewState["branch_13"].ToString(), "Remarks", ViewState["receipt_no_13"].ToString(), ViewState["ex_rate_13"].ToString(), ViewState["amount_13"].ToString(), ViewState["currency_13"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row13_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row13_txt_credit.Text, row13_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row14_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row14_txt_credit.Text, row14_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }

                    // EIGHT ENTRY
                    if (row15_txt_credit.Text != "" &&( row15_txt_debit.Text == "" || row15_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_15"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_15"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_16"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb15"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row15_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row15_txt_credit.Text, row15_txt_debit.Text, ViewState["payment_mode_15"].ToString(), ViewState["cheque_no_15"].ToString(), ViewState["cheque_date_15"].ToString(), ViewState["bank_name_15"].ToString(), ViewState["branch_15"].ToString(), "Remarks", ViewState["receipt_no_15"].ToString(), ViewState["ex_rate_15"].ToString(), ViewState["amount_15"].ToString(), ViewState["currency_15"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row16_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row16_txt_credit.Text, row16_txt_debit.Text, ViewState["payment_mode_15"].ToString(), ViewState["cheque_no_15"].ToString(), ViewState["cheque_date_15"].ToString(), ViewState["bank_name_15"].ToString(), ViewState["branch_15"].ToString(), "Remarks", ViewState["receipt_no_15"].ToString(), ViewState["ex_rate_15"].ToString(), ViewState["amount_15"].ToString(), ViewState["currency_15"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row15_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row15_txt_credit.Text, row15_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row16_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row16_txt_credit.Text, row16_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row15_txt_credit.Text == "" || row15_txt_credit.Text == "0.00") && row15_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_15"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_15"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_16"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb15"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row15_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row15_txt_credit.Text, row15_txt_debit.Text, ViewState["payment_mode_15"].ToString(), ViewState["cheque_no_15"].ToString(), ViewState["cheque_date_15"].ToString(), ViewState["bank_name_15"].ToString(), ViewState["branch_15"].ToString(), "Remarks", ViewState["receipt_no_15"].ToString(), ViewState["ex_rate_15"].ToString(), ViewState["amount_15"].ToString(), ViewState["currency_15"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row16_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row16_txt_credit.Text, row16_txt_debit.Text, ViewState["payment_mode_15"].ToString(), ViewState["cheque_no_15"].ToString(), ViewState["cheque_date_15"].ToString(), ViewState["bank_name_15"].ToString(), ViewState["branch_15"].ToString(), "Remarks", ViewState["receipt_no_15"].ToString(), ViewState["ex_rate_15"].ToString(), ViewState["amount_15"].ToString(), ViewState["currency_15"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row15_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row15_txt_credit.Text, row15_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row16_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row16_txt_credit.Text, row16_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    // NINE ENTRY
                    if (row17_txt_credit.Text != "" && (row17_txt_debit.Text == "" || row17_txt_debit.Text=="0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_17"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_17"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_18"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb17"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                                }
                                else
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    else if ((row17_txt_credit.Text == "" || row17_txt_credit.Text =="0.00") && row17_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            if (ViewState["seq_no"] != null && ViewState["row1_details_id_17"] != null)
                            {
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_17"].ToString()), row3_txt_credit.Text, row3_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text , 2);
                                objAcoountVouchersStoredProcedure.update_accounts_entry(0, row4_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, int.Parse(ViewState["row1_details_id_18"].ToString()), row4_txt_credit.Text, row4_txt_debit.Text, ViewState["payment_mode_3"].ToString(), ViewState["cheque_no_3"].ToString(), ViewState["cheque_date_3"].ToString(), ViewState["bank_name_3"].ToString(), ViewState["branch_3"].ToString(), "Remarks", ViewState["receipt_no_3"].ToString(), ViewState["ex_rate_3"].ToString(), ViewState["amount_3"].ToString(), ViewState["currency_3"].ToString(), int.Parse(ViewState["seq_no"].ToString()), lblgl_date.Text, 2);
                            }
                            else
                            {
                                if (ViewState["rb19"] != null)
                                {
                                    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                                }
                                else
                                {
                                    // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                                }
                            }
                        }
                    }
                    // TEN ENTRY
                    if (row19_txt_credit.Text != "" && (row19_txt_debit.Text == "" || row19_txt_debit.Text == "0.00"))
                    {
                        if (drpvoucher_type.Text == "RECEIPT")
                        {
                            //if (ViewState["rb17"] != null)
                            //{
                            //    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row3_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                            //    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                            //    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                            //}
                            //else
                            //{
                            //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            //}
                        }
                    }
                    else if ((row19_txt_credit.Text == "" || row19_txt_credit.Text=="0.00") && row19_txt_debit.Text != "")
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            //if (ViewState["rb17"] != null)
                            //{
                            //    //  objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                            //    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                            //    objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, ViewState["payment_mode_17"].ToString(), ViewState["cheque_no_17"].ToString(), ViewState["cheque_date_17"].ToString(), ViewState["bank_name_17"].ToString(), ViewState["branch_17"].ToString(), "Remarks", ViewState["receipt_no_17"].ToString(), ViewState["ex_rate_17"].ToString(), ViewState["amount_17"].ToString(), ViewState["currency_17"].ToString(), 2);
                            //}
                            //else
                            //{
                            // objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row1_drp_glcode.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row1_txt_credit.Text, row1_txt_debit.Text, "", "", "", "", "", "", "", "", "", "", 1);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row17_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row17_txt_credit.Text, row17_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            objAcoountVouchersStoredProcedure.insert_accounts_entry(0, row18_drp_gl.Text, drp_invoice_no.Text, txt_voucher_date.Text, drpvoucher_type.Text, int.Parse(Session["usersid"].ToString()), txt_narration.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.Text, 0, row18_txt_credit.Text, row18_txt_debit.Text, drppayment_mode.Text, txtcheque_no.Text, txtcheque_date.Text, drpbank_name.Text, drpbranch.Text, "Remarks", txtcash_receipt.Text, txt_ex_rate.Text, txt_amount.Text, drp_currency.Text, 2);
                            //}
                        }
                    }

                    if (ViewState["seq_no"] != null)
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                            Master.DisplayMessage("Payment voucher record updated successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "RECEIPT")
                        {
                            Master.DisplayMessage("Receipt voucher record updated successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "PURCHASE")
                        {
                            Master.DisplayMessage("Purchase Voucher record updated successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "SALES")
                        {
                            Master.DisplayMessage("Sales Voucher record updated successfully.", "successMessage", 8000);
                        }
                    }
                    else
                    {
                        if (drpvoucher_type.Text == "PAYMENT")
                        {
                           
                            Master.DisplayMessage("Payment voucher save successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "RECEIPT")
                        {
                            
                            Master.DisplayMessage("Receipt voucher save successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "PURCHASE")
                        {
                            Master.DisplayMessage("Purchase Voucher entry save successfully.", "successMessage", 8000);
                        }
                        else if (drpvoucher_type.Text == "SALES")
                        {
                            Master.DisplayMessage("Sales Voucher entry save successfully.", "successMessage", 8000);
                        }
                    }
                   
                    UpdatePanel2.Update();
                }
            }
       
        
        #endregion

        #region HIDE SHOW IN GRID WITH ADD BUTTONS
        protected void btnadd2_Click(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            btnadd2.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display");

           

            updategrid.Update();
        }

        protected void btnadd3_Click(object sender, EventArgs e)
        {
            row3.Attributes.Add("style", "display");
            btnadd3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display:none");
          //  btnadd4.Attributes.Add("style", "display");

            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row3_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row3_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row3_drp_currency, ds5);

            row3_drp_currency.Text = "THB";
            row3_drp_currency.Enabled = false;

            row3_cb.Visible = true;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row3_txt_credit.Enabled = true;
                row3_txt_debit.Enabled = false;
                row3_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row3_txt_credit.Enabled = false ;
                row3_txt_debit.Enabled = true ;
                row3_drp_gl.Text = row1_drp_glcode.Text;
            }

            updategrid.Update();
        }

        protected void btnadd4_Click(object sender, EventArgs e)
        {
            btnremove3.Attributes.Add("style", "display:none");

            row4.Attributes.Add("style", "display");
            btnadd4.Attributes.Add("style", "display:none");

            btnadd5.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row4_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row4_drp_currency, ds5);

            row4_drp_currency.Text = "THB";
            row4_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd5_Click(object sender, EventArgs e)
        {

            ViewState["rb3"] = "check";
            ViewState["payment_date_3"] = txt_payment_date.Text;
            ViewState["payment_mode_3"] = drppayment_mode.Text;
         
            ViewState["bank_name_3"] = drpbank_name.Text;
            ViewState["branch_3"] = drpbranch.Text;
            ViewState["cheque_no_3"] = txtcheque_no.Text;
            ViewState["cheque_date_3"] = txtcheque_date.Text;

         
            ViewState["receipt_no_3"] = txtcash_receipt.Text;
            ViewState["receipt_date_3"] = txtcashreceipt_date.Text;
          
            ViewState["currency_3"] = drp_currency.Text;
            ViewState["amount_3"] = txt_amount.Text;
            ViewState["ex_rate_3"] = txt_ex_rate.Text;

            btnremove4.Attributes.Add("style", "display:none");
            btnremove3.Attributes.Add("style", "display:none");
            row5.Attributes.Add("style", "display");
            btnadd5.Attributes.Add("style", "display:none");

         //   btnadd6.Attributes.Add("style", "display");
            btnadd6.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row5_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row5_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row5_drp_currency, ds5);

            row5_drp_currency.Text = "THB";
            row5_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row5_txt_credit.Enabled = true;
                row5_txt_debit.Enabled = false;
                row5_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row5_txt_credit.Enabled = false;
                row5_txt_debit.Enabled = true;
                row5_drp_gl.Text = row1_drp_glcode.Text;
            }

            row5_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd6_Click(object sender, EventArgs e)
        {
            btnremove5.Attributes.Add("style", "display:none");

            row6.Attributes.Add("style", "display");
            btnadd6.Attributes.Add("style", "display:none");

            btnadd7.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row6_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row6_drp_currency, ds5);

            row6_drp_currency.Text = "THB";
            row6_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd7_Click(object sender, EventArgs e)
        {
            ViewState["rb5"] = "check";
            ViewState["payment_date_5"] = txt_payment_date.Text;
            ViewState["payment_mode_5"] = drppayment_mode.Text;

            ViewState["bank_name_5"] = drpbank_name.Text;
            ViewState["branch_5"] = drpbranch.Text;
            ViewState["cheque_no_5"] = txtcheque_no.Text;
            ViewState["cheque_date_5"] = txtcheque_date.Text;


            ViewState["receipt_no_5"] = txtcash_receipt.Text;
            ViewState["receipt_date_5"] = txtcashreceipt_date.Text;

            ViewState["currency_5"] = drp_currency.Text;
            ViewState["amount_5"] = txt_amount.Text;
            ViewState["ex_rate_5"] = txt_ex_rate.Text;

            btnremove6.Attributes.Add("style", "display:none");
            btnremove5.Attributes.Add("style", "display:none");

            row7.Attributes.Add("style", "display");
            btnadd7.Attributes.Add("style", "display:none");

          //  btnadd8.Attributes.Add("style", "display");
            btnadd8.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row7_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row7_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row7_drp_currency, ds5);

            row7_drp_currency.Text = "THB";
            row7_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row7_txt_credit.Enabled = true;
                row7_txt_debit.Enabled = false;
                row7_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row7_txt_credit.Enabled = false;
                row7_txt_debit.Enabled = true;
                row7_drp_gl.Text = row1_drp_glcode.Text;
            }

            row7_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd8_Click(object sender, EventArgs e)
        {
            btnremove7.Attributes.Add("style", "display:none");

            row8.Attributes.Add("style", "display");
            btnadd8.Attributes.Add("style", "display:none");

            btnadd9.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row8_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row8_drp_currency, ds5);

            row8_drp_currency.Text = "THB";
            row8_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd9_Click(object sender, EventArgs e)
        {
            ViewState["rb7"] = "check";
            ViewState["payment_date_7"] = txt_payment_date.Text;
            ViewState["payment_mode_7"] = drppayment_mode.Text;

            ViewState["bank_name_7"] = drpbank_name.Text;
            ViewState["branch_7"] = drpbranch.Text;
            ViewState["cheque_no_7"] = txtcheque_no.Text;
            ViewState["cheque_date_7"] = txtcheque_date.Text;


            ViewState["receipt_no_7"] = txtcash_receipt.Text;
            ViewState["receipt_date_7"] = txtcashreceipt_date.Text;

            ViewState["currency_7"] = drp_currency.Text;
            ViewState["amount_7"] = txt_amount.Text;
            ViewState["ex_rate_7"] = txt_ex_rate.Text;

            btnremove7.Attributes.Add("style", "display:none");
            btnremove8.Attributes.Add("style", "display:none");

            row9.Attributes.Add("style", "display");
            btnadd9.Attributes.Add("style", "display:none");

         //   btnadd10.Attributes.Add("style", "display");
            btnadd10.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row9_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row9_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row9_drp_currency, ds5);

            row9_drp_currency.Text = "THB";
            row9_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row9_txt_credit.Enabled = true;
                row9_txt_debit.Enabled = false;
                row9_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row9_txt_credit.Enabled = false;
                row9_txt_debit.Enabled = true;
                row9_drp_gl.Text = row1_drp_glcode.Text;
            }

            row9_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd10_Click(object sender, EventArgs e)
        {
            btnremove9.Attributes.Add("style", "display:none");

            row10.Attributes.Add("style", "display");
            btnadd10.Attributes.Add("style", "display:none");

            btnadd11.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row10_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row10_drp_currency, ds5);

            row10_drp_currency.Text = "THB";
            row10_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd11_Click(object sender, EventArgs e)
        {
            ViewState["rb9"] = "check";
            ViewState["payment_date_9"] = txt_payment_date.Text;
            ViewState["payment_mode_9"] = drppayment_mode.Text;

            ViewState["bank_name_9"] = drpbank_name.Text;
            ViewState["branch_9"] = drpbranch.Text;
            ViewState["cheque_no_9"] = txtcheque_no.Text;
            ViewState["cheque_date_9"] = txtcheque_date.Text;


            ViewState["receipt_no_9"] = txtcash_receipt.Text;
            ViewState["receipt_date_9"] = txtcashreceipt_date.Text;

            ViewState["currency_9"] = drp_currency.Text;
            ViewState["amount_9"] = txt_amount.Text;
            ViewState["ex_rate_9"] = txt_ex_rate.Text;

            btnremove9.Attributes.Add("style", "display:none");
            btnremove10.Attributes.Add("style", "display:none");

            row11.Attributes.Add("style", "display");
            btnadd11.Attributes.Add("style", "display:none");

           // btnadd12.Attributes.Add("style", "display");
            btnadd12.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row11_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row11_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row11_drp_currency, ds5);

            row11_drp_currency.Text = "THB";
            row11_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row11_txt_credit.Enabled = true;
                row11_txt_debit.Enabled = false;
                row11_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row11_txt_credit.Enabled = false;
                row11_txt_debit.Enabled = true;
                row11_drp_gl.Text = row1_drp_glcode.Text;
            }

            row11_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd12_Click(object sender, EventArgs e)
        {
            btnremove11.Attributes.Add("style", "display:none");

            row12.Attributes.Add("style", "display");
            btnadd12.Attributes.Add("style", "display:none");

            btnadd13.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row12_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row12_drp_currency, ds5);

            row12_drp_currency.Text = "THB";
            row12_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd13_Click(object sender, EventArgs e)
        {
            ViewState["rb11"] = "check";
            ViewState["payment_date_11"] = txt_payment_date.Text;
            ViewState["payment_mode_11"] = drppayment_mode.Text;

            ViewState["bank_name_11"] = drpbank_name.Text;
            ViewState["branch_11"] = drpbranch.Text;
            ViewState["cheque_no_11"] = txtcheque_no.Text;
            ViewState["cheque_date_11"] = txtcheque_date.Text;


            ViewState["receipt_no_11"] = txtcash_receipt.Text;
            ViewState["receipt_date_11"] = txtcashreceipt_date.Text;

            ViewState["currency_11"] = drp_currency.Text;
            ViewState["amount_11"] = txt_amount.Text;
            ViewState["ex_rate_11"] = txt_ex_rate.Text;

            btnremove11.Attributes.Add("style", "display:none");
            btnremove12.Attributes.Add("style", "display:none");

            row13.Attributes.Add("style", "display");
            btnadd13.Attributes.Add("style", "display:none");

          //  btnadd14.Attributes.Add("style", "display");
            btnadd14.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row13_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row13_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row13_drp_currency, ds5);

            row13_drp_currency.Text = "THB";
            row13_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row13_txt_credit.Enabled = true;
                row13_txt_debit.Enabled = false;
                row13_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row13_txt_credit.Enabled = false;
                row13_txt_debit.Enabled = true;
                row13_drp_gl.Text = row1_drp_glcode.Text;
            }

            row13_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd14_Click(object sender, EventArgs e)
        {
            btnremove13.Attributes.Add("style", "display:none");

            row14.Attributes.Add("style", "display");
            btnadd14.Attributes.Add("style", "display:none");

            btnadd15.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row14_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row14_drp_currency, ds5);

            row14_drp_currency.Text = "THB";
            row14_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd15_Click(object sender, EventArgs e)
        {
            ViewState["rb13"] = "check";
            ViewState["payment_date_13"] = txt_payment_date.Text;
            ViewState["payment_mode_13"] = drppayment_mode.Text;

            ViewState["bank_name_13"] = drpbank_name.Text;
            ViewState["branch_13"] = drpbranch.Text;
            ViewState["cheque_no_13"] = txtcheque_no.Text;
            ViewState["cheque_date_13"] = txtcheque_date.Text;


            ViewState["receipt_no_13"] = txtcash_receipt.Text;
            ViewState["receipt_date_13"] = txtcashreceipt_date.Text;

            ViewState["currency_13"] = drp_currency.Text;
            ViewState["amount_13"] = txt_amount.Text;
            ViewState["ex_rate_13"] = txt_ex_rate.Text;

            btnremove13.Attributes.Add("style", "display:none");
            btnremove14.Attributes.Add("style", "display:none");

            row15.Attributes.Add("style", "display");
            btnadd15.Attributes.Add("style", "display:none");

         //   btnadd16.Attributes.Add("style", "display");
            btnadd16.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row15_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row15_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row15_drp_currency, ds5);

            row15_drp_currency.Text = "THB";
            row15_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row15_txt_credit.Enabled = true;
                row15_txt_debit.Enabled = false;
                row15_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row15_txt_credit.Enabled = false;
                row15_txt_debit.Enabled = true;
                row15_drp_gl.Text = row1_drp_glcode.Text;
            }

            row15_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd16_Click(object sender, EventArgs e)
        {
            btnremove15.Attributes.Add("style", "display:none");

            row16.Attributes.Add("style", "display");
            btnadd16.Attributes.Add("style", "display:none");

            btnadd17.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row16_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row16_drp_currency, ds5);

            row16_drp_currency.Text = "THB";
            row16_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd17_Click(object sender, EventArgs e)
        {
            ViewState["rb15"] = "check";
            ViewState["payment_date_15"] = txt_payment_date.Text;
            ViewState["payment_mode_15"] = drppayment_mode.Text;

            ViewState["bank_name_15"] = drpbank_name.Text;
            ViewState["branch_15"] = drpbranch.Text;
            ViewState["cheque_no_15"] = txtcheque_no.Text;
            ViewState["cheque_date_15"] = txtcheque_date.Text;


            ViewState["receipt_no_15"] = txtcash_receipt.Text;
            ViewState["receipt_date_15"] = txtcashreceipt_date.Text;

            ViewState["currency_15"] = drp_currency.Text;
            ViewState["amount_15"] = txt_amount.Text;
            ViewState["ex_rate_15"] = txt_ex_rate.Text;

            btnremove15.Attributes.Add("style", "display:none");
            btnremove16.Attributes.Add("style", "display:none");

            row17.Attributes.Add("style", "display");
            btnadd17.Attributes.Add("style", "display:none");

         //   btnadd18.Attributes.Add("style", "display");
            btnadd18.Attributes.Add("style", "display:none");
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row17_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row17_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row17_drp_currency, ds5);

            row17_drp_currency.Text = "THB";
            row17_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row17_txt_credit.Enabled = true;
                row17_txt_debit.Enabled = false;
                row17_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row17_txt_credit.Enabled = false;
                row17_txt_debit.Enabled = true;
                row17_drp_gl.Text = row1_drp_glcode.Text;
            }

            row17_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd18_Click(object sender, EventArgs e)
        {
            btnremove17.Attributes.Add("style", "display:none");

            row18.Attributes.Add("style", "display");
            btnadd18.Attributes.Add("style", "display:none");

            btnadd19.Attributes.Add("style", "display");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row18_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row18_drp_currency, ds5);

            row18_drp_currency.Text = "THB";
            row18_drp_currency.Enabled = false;

            updategrid.Update();
        }

        protected void btnadd19_Click(object sender, EventArgs e)
        {
            ViewState["rb17"] = "check";
            ViewState["payment_date_17"] = txt_payment_date.Text;
            ViewState["payment_mode_17"] = drppayment_mode.Text;

            ViewState["bank_name_17"] = drpbank_name.Text;
            ViewState["branch_17"] = drpbranch.Text;
            ViewState["cheque_no_17"] = txtcheque_no.Text;
            ViewState["cheque_date_17"] = txtcheque_date.Text;


            ViewState["receipt_no_17"] = txtcash_receipt.Text;
            ViewState["receipt_date_17"] = txtcashreceipt_date.Text;

            ViewState["currency_17"] = drp_currency.Text;
            ViewState["amount_17"] = txt_amount.Text;
            ViewState["ex_rate_17"] = txt_ex_rate.Text;

            btnremove17.Attributes.Add("style", "display:none");
            btnremove18.Attributes.Add("style", "display:none");

            row19.Attributes.Add("style", "display");
            btnadd19.Attributes.Add("style", "display:none");

            btnadd20.Attributes.Add("style", "display");
           
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row19_drp_gl, ds4);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row19_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row19_drp_currency, ds5);

            row19_drp_currency.Text = "THB";
            row19_drp_currency.Enabled = false;

            if (drpvoucher_type.Text == "RECEIPT")
            {
                row19_txt_credit.Enabled = true;
                row19_txt_debit.Enabled = false;
                row19_drp_gl.Text = row1_drp_glcode.Text;
            }
            else if (drpvoucher_type.Text == "PAYMENT")
            {
                row19_txt_credit.Enabled = false;
                row19_txt_debit.Enabled = true;
                row19_drp_gl.Text = row1_drp_glcode.Text;
            }

            row19_cb.Visible = true;

            clear_payments_details();

            updategrid.Update();
        }

        protected void btnadd20_Click(object sender, EventArgs e)
        {
            btnremove19.Attributes.Add("style", "display:none");

            row20.Attributes.Add("style", "display");
            btnadd20.Attributes.Add("style", "display:none");

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row20_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row20_drp_currency, ds5);
           // btnadd20.Attributes.Add("style", "display");

            row20_drp_currency.Text = "THB";
            row20_drp_currency.Enabled = false;
            updategrid.Update();
        }
        #endregion

        #region HIDE SHOW IN GRID WITH REMOVE BUTTONS
        protected void btnremove3_Click(object sender, EventArgs e)
        {
            row3.Attributes.Add("style", "display:none");
            btnremove3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display");

            row3_txt_credit.Text = "";
            row3_txt_debit.Text = "";
            row3_cb.Checked = false;

            updategrid.Update();
        }
        protected void btnremove4_Click(object sender, EventArgs e)
        {
            btnremove3.Attributes.Add("style", "display");

            row4.Attributes.Add("style", "display:none");
            btnremove4.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display");

            btnadd5.Attributes.Add("style", "display:none");

            row4_txt_credit.Text = "";
            row4_txt_debit.Text = "";
           // row3_cb.Checked = false;

            updategrid.Update();
        }
        protected void btnremove5_Click(object sender, EventArgs e)
        {
            btnremove4.Attributes.Add("style", "display");

            row5.Attributes.Add("style", "display:none");
            btnremove5.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display");

            btnadd6.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove6_Click(object sender, EventArgs e)
        {
            btnremove5.Attributes.Add("style", "display");

            row6.Attributes.Add("style", "display:none");
            btnremove6.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display");

            btnadd7.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove7_Click(object sender, EventArgs e)
        {
            btnremove6.Attributes.Add("style", "display");

            row7.Attributes.Add("style", "display:none");
            btnremove7.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display");

            btnadd8.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove8_Click(object sender, EventArgs e)
        {
            btnremove7.Attributes.Add("style", "display");

            row8.Attributes.Add("style", "display:none");
            btnremove8.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display");

            btnadd9.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove9_Click(object sender, EventArgs e)
        {
            btnremove8.Attributes.Add("style", "display");

            row9.Attributes.Add("style", "display:none");
            btnremove9.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display");

            btnadd10.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove10_Click(object sender, EventArgs e)
        {
            btnremove9.Attributes.Add("style", "display");

            row10.Attributes.Add("style", "display:none");
            btnremove10.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display");

            btnadd11.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove11_Click(object sender, EventArgs e)
        {
            btnremove10.Attributes.Add("style", "display");

            row11.Attributes.Add("style", "display:none");
            btnremove11.Attributes.Add("style", "display:none");
            btnadd11.Attributes.Add("style", "display");

            btnadd12.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove12_Click(object sender, EventArgs e)
        {
            btnremove11.Attributes.Add("style", "display");

            row12.Attributes.Add("style", "display:none");
            btnremove12.Attributes.Add("style", "display:none");
            btnadd12.Attributes.Add("style", "display");

            btnadd13.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove13_Click(object sender, EventArgs e)
        {
            btnremove12.Attributes.Add("style", "display");

            row13.Attributes.Add("style", "display:none");
            btnremove13.Attributes.Add("style", "display:none");
            btnadd13.Attributes.Add("style", "display");

            btnadd14.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove14_Click(object sender, EventArgs e)
        {
            btnremove13.Attributes.Add("style", "display");

            row14.Attributes.Add("style", "display:none");
            btnremove14.Attributes.Add("style", "display:none");
            btnadd14.Attributes.Add("style", "display");

            btnadd15.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove15_Click(object sender, EventArgs e)
        {
            btnremove14.Attributes.Add("style", "display");

            row15.Attributes.Add("style", "display:none");
            btnremove15.Attributes.Add("style", "display:none");
            btnadd15.Attributes.Add("style", "display");

            btnadd16.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove16_Click(object sender, EventArgs e)
        {
            btnremove15.Attributes.Add("style", "display");

            row16.Attributes.Add("style", "display:none");
            btnremove16.Attributes.Add("style", "display:none");
            btnadd16.Attributes.Add("style", "display");

            btnadd17.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove17_Click(object sender, EventArgs e)
        {
            btnremove16.Attributes.Add("style", "display");

            row17.Attributes.Add("style", "display:none");
            btnremove17.Attributes.Add("style", "display:none");
            btnadd17.Attributes.Add("style", "display");

            btnadd18.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove18_Click(object sender, EventArgs e)
        {
            btnremove17.Attributes.Add("style", "display");

            row18.Attributes.Add("style", "display:none");
            btnremove18.Attributes.Add("style", "display:none");
            btnadd18.Attributes.Add("style", "display");

            btnadd19.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove19_Click(object sender, EventArgs e)
        {
            btnremove18.Attributes.Add("style", "display");

            row19.Attributes.Add("style", "display:none");
            btnremove19.Attributes.Add("style", "display:none");
            btnadd19.Attributes.Add("style", "display");

            btnadd20.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        protected void btnremove20_Click(object sender, EventArgs e)
        {
            btnremove19.Attributes.Add("style", "display");

            row20.Attributes.Add("style", "display:none");
            btnremove20.Attributes.Add("style", "display:none");
            btnadd20.Attributes.Add("style", "display");

          //  btnadd20.Attributes.Add("style", "display:none");

            updategrid.Update();
        }
        #endregion

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

        #region FILL BRANCH NAME DROP DOWN CONDITIONALLY 
        protected void drpbank_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objAcoountVouchersStoredProcedure.fetch_branch("FETCH_BRANCH_NAME", drpbank_name.Text);
                binddropdownlist(drpbranch, ds);

                btnadd3.Attributes.Add("style", "display");
                update_payments.Update();
                
        }
        #endregion

        #region TEXT CHANGED OF FIRST ROW IN GRID TAB INDEX
        protected void row1_txt_debit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_debit.Text))
            //{
            //    Master.DisplayMessage("Can not take more amount then invoice amount", "successMessage", 8000);
            //}
            //else
            //{

                row2.Attributes.Add("style", "display");
                row2_txt_credit.Text = row1_txt_debit.Text;

                row2_txt_credit.Enabled = true;
                row2_txt_debit.Enabled = false;
                //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                //binddropdownlist(row2_drp_gl, ds4);

                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row2_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row2_drp_currency, ds5);

                row2_drp_currency.Text = "THB";
                row2_drp_currency.Enabled = false;

                row2_drp_gl.Focus();
                if (drpvoucher_type.Text != "PURCHASE" && drpvoucher_type.Text != "SALES")
                {
                    btnadd3.Attributes.Add("style", "display");
                }
                ViewState["old_amount"] = row1_txt_debit.Text;
                updategrid.Update();
            }
      //  }

        protected void row1_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row2.Attributes.Add("style", "display");
                row2_txt_debit.Text = row1_txt_credit.Text;

                row2_txt_debit.Enabled = true;
                row2_txt_credit.Enabled = false;
                //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                //binddropdownlist(row2_drp_gl, ds4);

                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row2_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row2_drp_currency, ds5);

                row2_drp_currency.Text = "THB";
                row2_drp_currency.Enabled = false;
                row2_drp_gl.Focus();

                if (drpvoucher_type.Text != "PURCHASE" && drpvoucher_type.Text != "SALES")
                {
                    btnadd3.Attributes.Add("style", "display");
                }
                ViewState["old_amount"] = row1_txt_credit.Text;
                updategrid.Update();
            }
     //   }
        // ROW 3
        protected void row3_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row4.Attributes.Add("style", "display");
            row4_txt_credit.Text = row3_txt_debit.Text;

            row4_txt_credit.Enabled = true;
            row4_txt_debit.Enabled = false;

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row4_drp_gl, ds4);
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row4_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row4_drp_currency, ds5);

            row4_drp_currency.Text = "THB";
            row4_drp_currency.Enabled = false;

            row4_drp_gl.Focus();

            btnremove3.Attributes.Add("style", "display:none");

            btnadd4.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row3_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row4.Attributes.Add("style", "display");
                row4_txt_debit.Text = row3_txt_credit.Text;

                row4_txt_credit.Enabled = false;
                row4_txt_debit.Enabled = true;

                //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                //binddropdownlist(row4_drp_gl, ds4);

                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row4_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row4_drp_currency, ds5);

                btnremove3.Attributes.Add("style", "display:none");

                row4_drp_currency.Text = "THB";
                row4_drp_currency.Enabled = false;
                row4_drp_gl.Focus();

                btnadd5.Attributes.Add("style", "display");
                btnadd4.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
     //   }

        // ROW 5
        protected void row5_txt_debit_TextChanged(object sender, EventArgs e)
        {

            row6.Attributes.Add("style", "display");
            row6_txt_credit.Text = row5_txt_debit.Text;

            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row6_drp_gl, ds4);

            row6_txt_credit.Enabled = true;
            row6_txt_debit.Enabled = false;

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row6_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row6_drp_currency, ds5);

            row6_drp_currency.Text = "THB";
            row6_drp_currency.Enabled = false;

            btnremove5.Attributes.Add("style", "display:none");

            row6_drp_gl.Focus();
            btnadd6.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row5_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row6.Attributes.Add("style", "display");
                row6_txt_debit.Text = row5_txt_credit.Text;


                row6_txt_credit.Enabled = false;
                row6_txt_debit.Enabled = true;
                //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                //binddropdownlist(row6_drp_gl, ds4);

                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row6_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row6_drp_currency, ds5);

                btnremove5.Attributes.Add("style", "display:none");

                row6_drp_currency.Text = "THB";
                row6_drp_currency.Enabled = false;

                row6_drp_gl.Focus();

                btnadd7.Attributes.Add("style", "display");
                btnadd6.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
       // }
        // ROW 7
        protected void row7_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row8.Attributes.Add("style", "display");
            row8_txt_credit.Text = row7_txt_debit.Text;

            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            //binddropdownlist(row8_drp_gl, ds4);

            row8_txt_credit.Enabled = true;
            row8_txt_debit.Enabled = false;

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row8_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row8_drp_currency, ds5);

            btnremove7.Attributes.Add("style", "display:none");

            row8_drp_currency.Text = "THB";
            row8_drp_currency.Enabled = false;

            row8_drp_gl.Focus();
            btnadd8.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row7_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row8.Attributes.Add("style", "display");
                row8_txt_debit.Text = row7_txt_credit.Text;

                row8_txt_credit.Enabled = false;
                row8_txt_debit.Enabled = true;
                //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                //binddropdownlist(row8_drp_gl, ds4);

                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row8_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row8_drp_currency, ds5);

                btnremove7.Attributes.Add("style", "display:none");

                row8_drp_currency.Text = "THB";
                row8_drp_currency.Enabled = false;

                row8_drp_gl.Focus();

                btnadd9.Attributes.Add("style", "display");
                btnadd8.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
      //  }

        // ROW 9
        protected void row9_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row10.Attributes.Add("style", "display");
            row10_txt_credit.Text = row9_txt_debit.Text;

            row10_txt_credit.Enabled = true;
            row10_txt_debit.Enabled = false;
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

            binddropdownlist(row10_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row10_drp_currency, ds5);

            row10_drp_currency.Text = "THB";
            row10_drp_currency.Enabled = false;

            btnremove9.Attributes.Add("style", "display:none");

            row10_drp_gl.Focus();
            btnadd10.Attributes.Add("style", "display:none");
            btnadd11.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row9_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row10.Attributes.Add("style", "display");
                row10_txt_debit.Text = row9_txt_credit.Text;

                row10_txt_credit.Enabled = false;
                row10_txt_debit.Enabled = true;
                // DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                binddropdownlist(row10_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row10_drp_currency, ds5);

                btnremove9.Attributes.Add("style", "display:none");

                row10_drp_currency.Text = "THB";
                row10_drp_currency.Enabled = false;

                row10_drp_gl.Focus();

                btnadd11.Attributes.Add("style", "display");
                btnadd10.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
        //}

        // ROW 11
        protected void row11_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row12.Attributes.Add("style", "display");
            row12_txt_credit.Text = row11_txt_debit.Text;


            row12_txt_credit.Enabled = true;
            row12_txt_debit.Enabled = false;
           // DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row12_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row12_drp_currency, ds5);

            btnremove11.Attributes.Add("style", "display:none");

            row12_drp_currency.Text = "THB";
            row12_drp_currency.Enabled = false ;

            row12_drp_gl.Focus();
            btnadd12.Attributes.Add("style", "display:none");
            btnadd13.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row11_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row12.Attributes.Add("style", "display");
                row12_txt_debit.Text = row11_txt_credit.Text;

                row12_txt_credit.Enabled = false;
                row12_txt_debit.Enabled = true;
                //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row12_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row12_drp_currency, ds5);

                btnremove11.Attributes.Add("style", "display:none");

                row12_drp_currency.Text = "THB";
                row12_drp_currency.Enabled = false;

                row12_drp_gl.Focus();

                btnadd13.Attributes.Add("style", "display");
                btnadd12.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
     //   }

        // ROW 13
        protected void row13_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row14.Attributes.Add("style", "display");
            row14_txt_credit.Text = row13_txt_debit.Text;

            row14_txt_credit.Enabled = true;
            row14_txt_debit.Enabled = false;
          //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row14_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row14_drp_currency, ds5);

            btnremove13.Attributes.Add("style", "display:none");

            row14_drp_currency.Text = "THB";
            row14_drp_currency.Enabled = false ;

            row14_drp_gl.Focus();
            btnadd14.Attributes.Add("style", "display:none");
            btnadd15.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row13_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row14.Attributes.Add("style", "display");
                row14_txt_debit.Text = row13_txt_credit.Text;

                row14_txt_credit.Enabled = false;
                row14_txt_debit.Enabled = true;
                //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row14_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row14_drp_currency, ds5);

                btnremove13.Attributes.Add("style", "display:none");

                row14_drp_currency.Text = "THB";
                row14_drp_currency.Enabled = false;

                row14_drp_gl.Focus();

                btnadd15.Attributes.Add("style", "display");
                btnadd14.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
       // }
        // ROW 15
        protected void row15_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row16.Attributes.Add("style", "display");
            row16_txt_credit.Text = row15_txt_debit.Text;

            row16_txt_credit.Enabled = true;
            row16_txt_debit.Enabled = false;
            //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row16_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row16_drp_currency, ds5);

            btnremove15.Attributes.Add("style", "display:none");

            row16_drp_currency.Text = "THB";
            row16_drp_currency.Enabled = false;

            row16_drp_gl.Focus();
            btnadd16.Attributes.Add("style", "display:none");
            btnadd17.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row15_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + +decimal.Parse(row15_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row16.Attributes.Add("style", "display");
                row16_txt_debit.Text = row15_txt_credit.Text;

                row16_txt_credit.Enabled = false;
                row16_txt_debit.Enabled = true;
                //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row16_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row16_drp_currency, ds5);

                btnremove15.Attributes.Add("style", "display:none");

                row16_drp_currency.Text = "THB";
                row16_drp_currency.Enabled = false;

                row16_drp_gl.Focus();

                btnadd17.Attributes.Add("style", "display");
                btnadd16.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
      //  }
        // ROW 17
        protected void row17_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row18.Attributes.Add("style", "display");
            row18_txt_credit.Text = row17_txt_debit.Text;

            row18_txt_credit.Enabled = true;
            row18_txt_debit.Enabled = false;
           // DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row18_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row18_drp_currency, ds5);

            row18_drp_currency.Text = "THB";
            row18_drp_currency.Enabled = false;

            btnremove17.Attributes.Add("style", "display:none");

            row18_drp_gl.Focus();
            btnadd18.Attributes.Add("style", "display:none");
            btnadd19.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row17_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row18.Attributes.Add("style", "display");
                row18_txt_debit.Text = row17_txt_credit.Text;

                row18_txt_credit.Enabled = false;
                row18_txt_debit.Enabled = true;
                // DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row18_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row18_drp_currency, ds5);

                btnremove17.Attributes.Add("style", "display:none");

                row18_drp_currency.Text = "THB";
                row18_drp_currency.Enabled = false;

                row18_drp_gl.Focus();

                btnadd19.Attributes.Add("style", "display");
                btnadd18.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
       // }
        // ROW 19
        protected void row19_txt_debit_TextChanged(object sender, EventArgs e)
        {
            row20.Attributes.Add("style", "display");
            row20_txt_credit.Text = row19_txt_debit.Text;

            row20_txt_credit.Enabled = true;
            row20_txt_debit.Enabled = false;
          //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
            binddropdownlist(row20_drp_gl, ds4);

            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
            binddropdownlist(row20_drp_currency, ds5);

            btnremove19.Attributes.Add("style", "display:none");

            row20_drp_currency.Text = "THB";
            row20_drp_currency.Enabled = false;

            row20_drp_gl.Focus();
            btnadd19.Attributes.Add("style", "display:none");
          //  btnadd19.Attributes.Add("style", "display");
            updategrid.Update();

        }

        protected void row19_txt_credit_TextChanged(object sender, EventArgs e)
        {
            //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text) + decimal.Parse(row19_txt_credit.Text))
            //{
            //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
            //}
            //else
            //{
                row20.Attributes.Add("style", "display");
                row20_txt_debit.Text = row19_txt_credit.Text;

                row20_txt_credit.Enabled = false;
                row20_txt_debit.Enabled = true;
                //  DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
                DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row20_drp_gl, ds4);

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(row20_drp_currency, ds5);

                btnremove19.Attributes.Add("style", "display:none");

                row20_drp_currency.Text = "THB";
                row20_drp_currency.Enabled = false;

                row20_drp_gl.Focus();

                // btnadd19.Attributes.Add("style", "display");
                btnadd19.Attributes.Add("style", "display:none");
                updategrid.Update();
            }
      //  }
        #endregion

        protected void drppayment_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drppayment_mode.Text == "CHEQUE")
            {
                bank_name_tr.Attributes.Add("style", "display");
                branch_tr.Attributes.Add("style", "display");
                cheque_no_tr.Attributes.Add("style", "display");
                cheque_date_tr.Attributes.Add("style", "display");

                cash_receipt_no_tr.Attributes.Add("style", "display:none");
                cash_receipt_date_tr.Attributes.Add("style", "display:none");

                update_payments.Update();
            }
            else if (drppayment_mode.Text == "CASH ON ARRIVAL")
            {
                cash_receipt_no_tr.Attributes.Add("style", "display");
                cash_receipt_date_tr.Attributes.Add("style", "display");

                bank_name_tr.Attributes.Add("style", "display:none");
                branch_tr.Attributes.Add("style", "display:none");
                cheque_no_tr.Attributes.Add("style", "display:none");
                cheque_date_tr.Attributes.Add("style", "display:none");

                update_payments.Update();
            }
        }

        #region NOT IN USE NOW
        protected void drp_account_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(drp_gl_code, ds6);

            DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_GL_CODE", drp_account_grp.Text);
            binddropdownlist(row1_drp_glcode, ds4);

            update_voucher.Update();
            updategrid.Update();
        }

        protected void drp_gl_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_gl_code("FETCH_INVOICE_NO", drp_gl_code.Text);
            binddropdownlist(drp_invoice_no, ds6);
            update_voucher.Update();
        }
        #endregion

        #region SHOWING PAYMENT DETAILS ON RADIO BUTTON SELECTED
        protected void row1_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row1_cb.Checked == true)
            {
                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(drp_currency, ds5);

                DataSet ds3 = objAcoountVouchersStoredProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
                binddropdownlist(drppayment_mode, ds3);

                if (ViewState["rb1"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text= ViewState["payment_date_1"] .ToString();
                    drppayment_mode.Text =  ViewState["payment_mode_1"].ToString() ;
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");

                        

                        drpbank_name.Text = ViewState["bank_name_1"].ToString();
                        drpbranch.Text = ViewState["branch_1"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_1"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_1"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                     txtcash_receipt.Text = ViewState["receipt_no_1"].ToString();
                     txtcashreceipt_date.Text = ViewState["receipt_date_1"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_1"].ToString();
                     txt_amount.Text = ViewState["amount_1"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_1"].ToString();
                }
                else
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    //DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    //binddropdownlist(drp_currency, ds5);
                }
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row3_cb_CheckedChanged(object sender, EventArgs e)
        {

            if (row3_cb.Checked == true)
            {

                if (ViewState["rb3"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_3"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_3"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_3"].ToString();
                        drpbranch.Text = ViewState["branch_3"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_3"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_3"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_3"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_3"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_3"].ToString();
                    txt_amount.Text = ViewState["amount_3"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_3"].ToString();
                }


                else
                {

                ViewState["rb1"] = "check";
                ViewState["payment_date_1"] = txt_payment_date.Text;
                ViewState["payment_mode_1"]=drppayment_mode.Text ;
              //  if (drppayment_mode.Text == "CHEQUE")
             //   {
                    ViewState["bank_name_1"]=drpbank_name.Text ;
                    ViewState["branch_1"]=drpbranch.Text ;
                    ViewState["cheque_no_1"]=txtcheque_no.Text ;
                    ViewState["cheque_date_1"]=txtcheque_date.Text ;
           
            //    }
            //    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
            //    {
                    ViewState ["receipt_no_1"]=txtcash_receipt.Text ;
                        ViewState["receipt_date_1"]=txtcashreceipt_date.Text ;
            //    }
                        ViewState["currency_1"] = drp_currency.Text;
                        ViewState["amount_1"] = txt_amount.Text;
                 ViewState["ex_rate_1"] = txt_ex_rate.Text;
              
                Label8.Visible = true;
                AccountsVoucher3.Visible = true;

                Label5.Visible = true;
                Table1.Visible = true;

                drp_currency.Text = "";


                //DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                //binddropdownlist(drp_currency, ds5);
              

                drppayment_mode.Text = "";
                drpbank_name.Text = "";
                drpbranch.Text = "";
                txtcheque_no.Text = "";
                txtcheque_date.Text = "";
                txtcash_receipt.Text = "";
                txtcashreceipt_date.Text = "";

                drp_currency.Text = "";
                txt_amount.Text = "";
                txt_ex_rate.Text = "";

                bank_name_tr.Attributes.Add("style", "display:none");
                branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");

                //updategrid.Update();
                //update_payments.Update();
                //update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                //updategrid.Update();
                //update_payments.Update();
                //update_forex.Update();
            }
            updategrid.Update();
            update_payments.Update();
            update_forex.Update();
        }

        protected void row5_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row5_cb.Checked == true)
            {
                if (ViewState["rb5"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_5"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_5"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_5"].ToString();
                        drpbranch.Text = ViewState["branch_5"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_5"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_5"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_5"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_5"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_5"].ToString();
                    txt_amount.Text = ViewState["amount_5"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_5"].ToString();
                }
                else
                {
                    ViewState["rb3"] = "check";
                    ViewState["payment_date_3"] = txt_payment_date.Text;
                    ViewState["payment_mode_3"] = drppayment_mode.Text;
                    //   if (drppayment_mode.Text == "CHEQUE")
                    //    {
                    ViewState["bank_name_3"] = drpbank_name.Text;
                    ViewState["branch_3"] = drpbranch.Text;
                    ViewState["cheque_no_3"] = txtcheque_no.Text;
                    ViewState["cheque_date_3"] = txtcheque_date.Text;

                    //    }
                    //    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //    {
                    ViewState["receipt_no_3"] = txtcash_receipt.Text;
                    ViewState["receipt_date_3"] = txtcashreceipt_date.Text;
                    //     }
                    ViewState["currency_3"] = drp_currency.Text;
                    ViewState["amount_3"] = txt_amount.Text;
                    ViewState["ex_rate_3"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row7_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row7_cb.Checked == true)
            {
                if (ViewState["rb7"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_7"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_7"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_7"].ToString();
                        drpbranch.Text = ViewState["branch_7"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_7"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_7"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_7"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_7"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_7"].ToString();
                    txt_amount.Text = ViewState["amount_7"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_7"].ToString();
                }
            
                else 
                {
                ViewState["rb5"] = "check";
                ViewState["payment_date_5"] = txt_payment_date.Text;
                ViewState["payment_mode_5"] = drppayment_mode.Text;
            //    if (drppayment_mode.Text == "CHEQUE")
            //    {
                    ViewState["bank_name_5"] = drpbank_name.Text;
                    ViewState["branch_5"] = drpbranch.Text;
                    ViewState["cheque_no_5"] = txtcheque_no.Text;
                    ViewState["cheque_date_5"] = txtcheque_date.Text;
                   
            //    }
             //   else if (drppayment_mode.Text == "CASH ON ARRIVAL")
             //   {
                    ViewState["receipt_no_5"] = txtcash_receipt.Text;
                    ViewState["receipt_date_5"] = txtcashreceipt_date.Text;
             //   }
                    ViewState["currency_5"] = drp_currency.Text;
                    ViewState["amount_5"] = txt_amount.Text;
                    ViewState["ex_rate_5"] = txt_ex_rate.Text;

                Label8.Visible = true;
                AccountsVoucher3.Visible = true;

                Label5.Visible = true;
                Table1.Visible = true;

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(drp_currency, ds5);

                drppayment_mode.Text = "";
                drpbank_name.Text = "";
                drpbranch.Text = "";
                txtcheque_no.Text = "";
                txtcheque_date.Text = "";
                txtcash_receipt.Text = "";
                txtcashreceipt_date.Text = "";

                drp_currency.Text = "";
                txt_amount.Text = "";
                txt_ex_rate.Text = "";

                bank_name_tr.Attributes.Add("style", "display:none");
                branch_tr.Attributes.Add("style", "display:none");
                cheque_no_tr.Attributes.Add("style", "display:none");
                cheque_date_tr.Attributes.Add("style", "display:none");
                cash_receipt_no_tr.Attributes.Add("style", "display:none");
                cash_receipt_date_tr.Attributes.Add("style", "display:none");

                updategrid.Update();
                update_payments.Update();
                update_forex.Update();

                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }}
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row9_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row9_cb.Checked == true)
            {
                if (ViewState["rb9"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_9"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_9"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_9"].ToString();
                        drpbranch.Text = ViewState["branch_9"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_9"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_9"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_9"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_9"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_9"].ToString();
                    txt_amount.Text = ViewState["amount_9"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_9"].ToString();
                }
                else
                {
                    ViewState["rb7"] = "check";
                    ViewState["payment_date_7"] = txt_payment_date.Text;
                    ViewState["payment_mode_7"] = drppayment_mode.Text;
                    //       if (drppayment_mode.Text == "CHEQUE")
                    //       {
                    ViewState["bank_name_7"] = drpbank_name.Text;
                    ViewState["branch_7"] = drpbranch.Text;
                    ViewState["cheque_no_7"] = txtcheque_no.Text;
                    ViewState["cheque_date_7"] = txtcheque_date.Text;

                    //      }
                    //        else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //        {
                    ViewState["receipt_no_7"] = txtcash_receipt.Text;
                    ViewState["receipt_date_7"] = txtcashreceipt_date.Text;
                    //         }
                    ViewState["currency_7"] = drp_currency.Text;
                    ViewState["amount_7"] = txt_amount.Text;
                    ViewState["ex_rate_7"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row11_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row11_cb.Checked == true)
            {
                if (ViewState["rb11"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_11"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_11"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_11"].ToString();
                        drpbranch.Text = ViewState["branch_11"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_11"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_11"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_11"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_11"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_11"].ToString();
                    txt_amount.Text = ViewState["amount_11"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_11"].ToString();
                }
                else
                {
                    ViewState["rb9"] = "check";
                    ViewState["payment_date_9"] = txt_payment_date.Text;
                    ViewState["payment_mode_9"] = drppayment_mode.Text;
                    //        if (drppayment_mode.Text == "CHEQUE")
                    //        {
                    ViewState["bank_name_9"] = drpbank_name.Text;
                    ViewState["branch_9"] = drpbranch.Text;
                    ViewState["cheque_no_9"] = txtcheque_no.Text;
                    ViewState["cheque_date_9"] = txtcheque_date.Text;

                    //          }
                    //         else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //          {
                    ViewState["receipt_no_9"] = txtcash_receipt.Text;
                    ViewState["receipt_date_9"] = txtcashreceipt_date.Text;
                    //          }
                    ViewState["currency_9"] = drp_currency.Text;
                    ViewState["amount_9"] = txt_amount.Text;
                    ViewState["ex_rate_9"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row13_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row13_cb.Checked == true)
            {
                if (ViewState["rb3"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_13"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_13"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_13"].ToString();
                        drpbranch.Text = ViewState["branch_13"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_13"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_13"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_13"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_13"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_13"].ToString();
                    txt_amount.Text = ViewState["amount_13"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_13"].ToString();
                }
                else
                {
                    ViewState["rb11"] = "check";
                    ViewState["payment_date_11"] = txt_payment_date.Text;
                    ViewState["payment_mode_11"] = drppayment_mode.Text;
                    //           if (drppayment_mode.Text == "CHEQUE")
                    //           {
                    ViewState["bank_name_11"] = drpbank_name.Text;
                    ViewState["branch_11"] = drpbranch.Text;
                    ViewState["cheque_no_11"] = txtcheque_no.Text;
                    ViewState["cheque_date_11"] = txtcheque_date.Text;

                    //          }
                    //          else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //          {
                    ViewState["receipt_no_11"] = txtcash_receipt.Text;
                    ViewState["receipt_date_11"] = txtcashreceipt_date.Text;
                    //            }
                    ViewState["currency_11"] = drp_currency.Text;
                    ViewState["amount_11"] = txt_amount.Text;
                    ViewState["ex_rate_11"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row15_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row15_cb.Checked == true)
            {
                if (ViewState["rb3"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_15"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_15"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_15"].ToString();
                        drpbranch.Text = ViewState["branch_15"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_15"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_15"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_15"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_15"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_15"].ToString();
                    txt_amount.Text = ViewState["amount_15"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_15"].ToString();
                }
                else
                {

                    ViewState["rb13"] = "check";
                    ViewState["payment_date_13"] = txt_payment_date.Text;
                    ViewState["payment_mode_13"] = drppayment_mode.Text;
                    //         if (drppayment_mode.Text == "CHEQUE")
                    //         {
                    ViewState["bank_name_13"] = drpbank_name.Text;
                    ViewState["branch_13"] = drpbranch.Text;
                    ViewState["cheque_no_13"] = txtcheque_no.Text;
                    ViewState["cheque_date_13"] = txtcheque_date.Text;

                    //         }
                    //          else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //          {
                    ViewState["receipt_no_13"] = txtcash_receipt.Text;
                    ViewState["receipt_date_13"] = txtcashreceipt_date.Text;
                    //          }
                    ViewState["currency_13"] = drp_currency.Text;
                    ViewState["amount_13"] = txt_amount.Text;
                    ViewState["ex_rate_13"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row17_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row17_cb.Checked == true)
            {
                if (ViewState["rb17"] != null)
                {
                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    txt_payment_date.Text = ViewState["payment_date_17"].ToString();
                    drppayment_mode.Text = ViewState["payment_mode_17"].ToString();
                    if (drppayment_mode.Text == "CHEQUE")
                    {
                        bank_name_tr.Attributes.Add("style", "display");
                        branch_tr.Attributes.Add("style", "display");
                        cheque_no_tr.Attributes.Add("style", "display");
                        cheque_date_tr.Attributes.Add("style", "display");

                        cash_receipt_no_tr.Attributes.Add("style", "display:none");
                        cash_receipt_date_tr.Attributes.Add("style", "display:none");



                        drpbank_name.Text = ViewState["bank_name_17"].ToString();
                        drpbranch.Text = ViewState["branch_17"].ToString();
                        txtcheque_no.Text = ViewState["cheque_no_17"].ToString();
                        txtcheque_date.Text = ViewState["cheque_date_17"].ToString();
                    }
                    else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    {
                        cash_receipt_no_tr.Attributes.Add("style", "display");
                        cash_receipt_date_tr.Attributes.Add("style", "display");

                        bank_name_tr.Attributes.Add("style", "display:none");
                        branch_tr.Attributes.Add("style", "display:none");
                        cheque_no_tr.Attributes.Add("style", "display:none");
                        cheque_date_tr.Attributes.Add("style", "display:none");


                        txtcash_receipt.Text = ViewState["receipt_no_17"].ToString();
                        txtcashreceipt_date.Text = ViewState["receipt_date_17"].ToString();
                    }

                    drp_currency.Text = ViewState["currency_17"].ToString();
                    txt_amount.Text = ViewState["amount_17"].ToString();
                    txt_ex_rate.Text = ViewState["ex_rate_17"].ToString();
                }
                else
                {

                    ViewState["rb15"] = "check";
                    ViewState["payment_date_15"] = txt_payment_date.Text;
                    ViewState["payment_mode_15"] = drppayment_mode.Text;
                    //         if (drppayment_mode.Text == "CHEQUE")
                    //         {
                    ViewState["bank_name_15"] = drpbank_name.Text;
                    ViewState["branch_15"] = drpbranch.Text;
                    ViewState["cheque_no_15"] = txtcheque_no.Text;
                    ViewState["cheque_date_15"] = txtcheque_date.Text;
                    //  ViewState[]
                    //          }
                    //         else if (drppayment_mode.Text == "CASH ON ARRIVAL")
                    //          {
                    ViewState["receipt_no_15"] = txtcash_receipt.Text;
                    ViewState["receipt_date_15"] = txtcashreceipt_date.Text;
                    //          }
                    ViewState["currency_15"] = drp_currency.Text;
                    ViewState["amount_15"] = txt_amount.Text;
                    ViewState["ex_rate_15"] = txt_ex_rate.Text;

                    Label8.Visible = true;
                    AccountsVoucher3.Visible = true;

                    Label5.Visible = true;
                    Table1.Visible = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    drppayment_mode.Text = "";
                    drpbank_name.Text = "";
                    drpbranch.Text = "";
                    txtcheque_no.Text = "";
                    txtcheque_date.Text = "";
                    txtcash_receipt.Text = "";
                    txtcashreceipt_date.Text = "";

                    drp_currency.Text = "";
                    txt_amount.Text = "";
                    txt_ex_rate.Text = "";

                    bank_name_tr.Attributes.Add("style", "display:none");
                    branch_tr.Attributes.Add("style", "display:none");
                    cheque_no_tr.Attributes.Add("style", "display:none");
                    cheque_date_tr.Attributes.Add("style", "display:none");
                    cash_receipt_no_tr.Attributes.Add("style", "display:none");
                    cash_receipt_date_tr.Attributes.Add("style", "display:none");

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();

                    updategrid.Update();
                    update_payments.Update();
                    update_forex.Update();
                }
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }

        protected void row19_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (row19_cb.Checked == true)
            {

                ViewState["rb17"] = "check";
                ViewState["payment_date_17"] = txt_payment_date.Text;
                ViewState["payment_mode_17"] = drppayment_mode.Text;
       //         if (drppayment_mode.Text == "CHEQUE")
       //         {
                    ViewState["bank_name_17"] = drpbank_name.Text;
                    ViewState["branch_17"] = drpbranch.Text;
                    ViewState["cheque_no_17"] = txtcheque_no.Text;
                    ViewState["cheque_date_17"] = txtcheque_date.Text;
                   
       //         }
       //         else if (drppayment_mode.Text == "CASH ON ARRIVAL")
       //         {
                    ViewState["receipt_no_17"] = txtcash_receipt.Text;
                    ViewState["receipt_date_17"] = txtcashreceipt_date.Text;
      //          }
                    ViewState["currency_17"] = drp_currency.Text;
                    ViewState["amount_17"] = txt_amount.Text;
                    ViewState["ex_rate_17"] = txt_ex_rate.Text;

                Label8.Visible = true;
                AccountsVoucher3.Visible = true;

                Label5.Visible = true;
                Table1.Visible = true;

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(drp_currency, ds5);

                drppayment_mode.Text = "";
                drpbank_name.Text = "";
                drpbranch.Text = "";
                txtcheque_no.Text = "";
                txtcheque_date.Text = "";
                txtcash_receipt.Text = "";
                txtcashreceipt_date.Text = "";

                drp_currency.Text = "";
                txt_amount.Text = "";
                txt_ex_rate.Text = "";

                bank_name_tr.Attributes.Add("style", "display:none");
                branch_tr.Attributes.Add("style", "display:none");
                cheque_no_tr.Attributes.Add("style", "display:none");
                cheque_date_tr.Attributes.Add("style", "display:none");
                cash_receipt_no_tr.Attributes.Add("style", "display:none");
                cash_receipt_date_tr.Attributes.Add("style", "display:none");

                updategrid.Update();
                update_payments.Update();
                update_forex.Update();

                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
            else
            {
                Label8.Visible = false;
                AccountsVoucher3.Visible = false;

                Label5.Visible = false;
                Table1.Visible = false;
                updategrid.Update();
                update_payments.Update();
                update_forex.Update();
            }
        }
        #endregion

        #region VOUCHER TYPE SELECTED INDEX CHANGED
        protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpvoucher_type.Text == "PAYMENT" || drpvoucher_type.Text == "RECEIPT")
            {
                //grid_payment_cb.Style.Add("style", "display");
                //grid_payment.Style.Add("style", "display");
                //Label4.Style.Add("style", "display");
                //row1_cb.Style.Add("style", "display");
                if (drpvoucher_type.Text == "RECEIPT")
                {
                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(drp_invoice_no, ds7);

                    invoice_no_tr.Visible = true;
                    drp_invoice_no.Visible = true;
                  //  drp_invoice_no.Visible = false;

                    row1_txt_debit.Text = "";
                    row1_txt_credit.Text = "";

                    row2_txt_debit.Text = "";
                    row2_txt_credit.Text = "";

                    row1_cb.Checked = false;

                    drp_invoice_no.Text = "";
                    id_1.Visible = false;
                 //   lbl_client_name.Visible = false;
                    id_2.Visible = false;
                    id_3.Visible = false;
                    id_4.Visible = false;
                    id_5.Visible = false;
                    id_6.Visible = false;
                    //row2.Attributes.Add("style", "display:none");
                    //row3.Attributes.Add("style", "display:none");
                    //row4.Attributes.Add("style", "display:none");
                    //row5.Attributes.Add("style", "display:none");
                    //row6.Attributes.Add("style", "display:none");
                    //row7.Attributes.Add("style", "display:none");
                    //row8.Attributes.Add("style", "display:none");
                    //row9.Attributes.Add("style", "display:none");
                    //row10.Attributes.Add("style", "display:none");
                    //row11.Attributes.Add("style", "display:none");
                    //row12.Attributes.Add("style", "display:none");
                    //row13.Attributes.Add("style", "display:none");
                    //row14.Attributes.Add("style", "display:none");
                    //row15.Attributes.Add("style", "display:none");
                    //row16.Attributes.Add("style", "display:none");
                    //row17.Attributes.Add("style", "display:none");
                    //row18.Attributes.Add("style", "display:none");
                    //row19.Attributes.Add("style", "display:none");
                    //row20.Attributes.Add("style", "display:none");


                    //btnadd2.Attributes.Add("style", "display:none");
                    //btnadd3.Attributes.Add("style", "display:none");
                    //btnadd4.Attributes.Add("style", "display:none");
                    //btnadd5.Attributes.Add("style", "display:none");
                    //btnadd6.Attributes.Add("style", "display:none");
                    //btnadd7.Attributes.Add("style", "display:none");
                    //btnadd8.Attributes.Add("style", "display:none");
                    //btnadd9.Attributes.Add("style", "display:none");
                    //btnadd10.Attributes.Add("style", "display:none");
                    //btnadd11.Attributes.Add("style", "display:none");
                    //btnadd12.Attributes.Add("style", "display:none");
                    //btnadd13.Attributes.Add("style", "display:none");
                    //btnadd14.Attributes.Add("style", "display:none");
                    //btnadd15.Attributes.Add("style", "display:none");
                    //btnadd16.Attributes.Add("style", "display:none");
                    //btnadd17.Attributes.Add("style", "display:none");
                    //btnadd18.Attributes.Add("style", "display:none");
                    //btnadd19.Attributes.Add("style", "display:none");
                    //btnadd20.Attributes.Add("style", "display:none");
                    

                    clear_and_hide();
                  
                    
                    Label8.Visible = false;
                    AccountsVoucher3.Visible = false;

                    Label5.Visible = false;
                    Table1.Visible = false;

                    update_payments.Update();
                    update_forex.Update();
                }
                else if (drpvoucher_type.Text == "PAYMENT")
                {
                    DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_INVOICE_NO_PURCHASE");
                    binddropdownlist(drp_invoice_no, ds6);

                    invoice_no_tr.Visible = true;
                    drp_invoice_no.Visible = false ;
                    drp_invoice_no.Visible = true;
              //      Label64.Text = "Supplier Name";
              //      id_1.Visible = true;
              //      drp_client_name.Visible = true;
                    id_1.Visible = false;
                    lbl_client_name.Visible = false;
                    id_2.Visible = false;
                    id_3.Visible = false;
                    id_4.Visible = false;
                    id_5.Visible = false;
                    id_6.Visible = false;
                    drp_invoice_no.Text = "";

                    drp_invoice_no.Text = "";

                  

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_SUPPLIER_NAME");
                    binddropdownlist(drp_client_name, ds5);

                    DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row1_drp_glcode, ds4);

                    row1_drp_glcode.Text = "";
                    row1_txt_debit.Text = "";
                    row1_txt_credit.Text = "";

                    row1_txt_debit.Enabled = true;
                    row1_txt_credit.Enabled = false;

                    row1_cb.Checked = false;

                    row2_drp_gl.Text = "";
                    row2_txt_debit.Text = "";
                    row2_txt_credit.Text = "";

                    row2.Attributes.Add("style", "display:none");

                

                    Label8.Visible = false;
                    AccountsVoucher3.Visible = false;

                    Label5.Visible = false;
                    Table1.Visible = false;

                    clear_and_hide();
                    //row2.Attributes.Add("style", "display:none");
                    //row3.Attributes.Add("style", "display:none");
                    //row4.Attributes.Add("style", "display:none");
                    //row5.Attributes.Add("style", "display:none");
                    //row6.Attributes.Add("style", "display:none");
                    //row7.Attributes.Add("style", "display:none");
                    //row8.Attributes.Add("style", "display:none");
                    //row9.Attributes.Add("style", "display:none");
                    //row10.Attributes.Add("style", "display:none");
                    //row11.Attributes.Add("style", "display:none");
                    //row12.Attributes.Add("style", "display:none");
                    //row13.Attributes.Add("style", "display:none");
                    //row14.Attributes.Add("style", "display:none");
                    //row15.Attributes.Add("style", "display:none");
                    //row16.Attributes.Add("style", "display:none");
                    //row17.Attributes.Add("style", "display:none");
                    //row18.Attributes.Add("style", "display:none");
                    //row19.Attributes.Add("style", "display:none");
                    //row20.Attributes.Add("style", "display:none");

                    //btnadd2.Attributes.Add("style", "display:none");
                    //btnadd3.Attributes.Add("style", "display:none");
                    //btnadd4.Attributes.Add("style", "display:none");
                    //btnadd5.Attributes.Add("style", "display:none");
                    //btnadd6.Attributes.Add("style", "display:none");
                    //btnadd7.Attributes.Add("style", "display:none");
                    //btnadd8.Attributes.Add("style", "display:none");
                    //btnadd9.Attributes.Add("style", "display:none");
                    //btnadd10.Attributes.Add("style", "display:none");
                    //btnadd11.Attributes.Add("style", "display:none");
                    //btnadd12.Attributes.Add("style", "display:none");
                    //btnadd13.Attributes.Add("style", "display:none");
                    //btnadd14.Attributes.Add("style", "display:none");
                    //btnadd15.Attributes.Add("style", "display:none");
                    //btnadd16.Attributes.Add("style", "display:none");
                    //btnadd17.Attributes.Add("style", "display:none");
                    //btnadd18.Attributes.Add("style", "display:none");
                    //btnadd19.Attributes.Add("style", "display:none");
                    //btnadd20.Attributes.Add("style", "display:none");

                    update_payments.Update();
                    update_forex.Update();
                }
                Label4.Visible = true;
                row1_cb.Visible = true;
                update_voucher.Update();
                updategrid.Update();
            }
            else
            {
                if (drpvoucher_type.Text == "SALES")
                {
                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(drp_invoice_no, ds7);

                    invoice_no_tr.Visible = true;
                    drp_invoice_no.Visible = true;

                    row1_txt_credit.Enabled = false ;
                    row1_txt_debit.Enabled = true;

                    drp_invoice_no.Text = "";

                  
                    //row1_txt_credit.Text = "";
                    //row1_txt_debit.Text = "";

                    //row1_drp_glcode.Text = "";

                    //row2_drp_gl.Text = "";
                    //row2_txt_debit.Text = "";
                    //row2_txt_credit.Text = "";

                    //row3_drp_gl.Text = "";
                    //row3_txt_debit.Text = "";
                    //row3_txt_credit.Text = "";

                    //row4_drp_gl.Text = "";
                    //row4_txt_debit.Text = "";
                    //row4_txt_credit.Text = "";

                    //row5_drp_gl.Text = "";
                    //row5_txt_debit.Text = "";
                    //row5_txt_credit.Text = "";

                    //row6_drp_gl.Text = "";
                    //row6_txt_debit.Text = "";
                    //row6_txt_credit.Text = "";

                    //row7_drp_gl.Text = "";
                    //row7_txt_debit.Text = "";
                    //row7_txt_credit.Text = "";

                    //row8_drp_gl.Text = "";
                    //row8_txt_debit.Text = "";
                    //row8_txt_credit.Text = "";

                    //row9_drp_gl.Text = "";
                    //row9_txt_debit.Text = "";
                    //row9_txt_credit.Text = "";

                    //row10_drp_gl.Text = "";
                    //row10_txt_debit.Text = "";
                    //row10_txt_credit.Text = "";

                    //row11_drp_gl.Text = "";
                    //row11_txt_debit.Text = "";
                    //row11_txt_credit.Text = "";

                    //row12_drp_gl.Text = "";
                    //row12_txt_debit.Text = "";
                    //row12_txt_credit.Text = "";

                    //row13_drp_gl.Text = "";
                    //row13_txt_debit.Text = "";
                    //row13_txt_credit.Text = "";

                    //row14_drp_gl.Text = "";
                    //row14_txt_debit.Text = "";
                    //row14_txt_credit.Text = "";

                    //row15_drp_gl.Text = "";
                    //row15_txt_debit.Text = "";
                    //row15_txt_credit.Text = "";

                    //row16_drp_gl.Text = "";
                    //row16_txt_debit.Text = "";
                    //row16_txt_credit.Text = "";

                    //row17_drp_gl.Text = "";
                    //row17_txt_debit.Text = "";
                    //row17_txt_credit.Text = "";

                    //row18_drp_gl.Text = "";
                    //row18_txt_debit.Text = "";
                    //row18_txt_credit.Text = "";

                    //row19_drp_gl.Text = "";
                    //row19_txt_debit.Text = "";
                    //row19_txt_credit.Text = "";

                    //row20_drp_gl.Text = "";
                    //row20_txt_debit.Text = "";
                    //row20_txt_credit.Text = "";

                    id_1.Visible = false;
                    txt_invoice_no.Visible = false;

                    invoice_no_tr.Visible = true;
                    drp_invoice_no.Visible = true;
                    id_2.Visible = false;
                    id_3.Visible = false;
                    id_4.Visible = false;
                    id_5.Visible = false;
                    id_6.Visible = false;
                    clear_and_hide();

                    //row2.Attributes.Add("style", "display:none");
                    //row3.Attributes.Add("style", "display:none");
                    //row4.Attributes.Add("style", "display:none");
                    //row5.Attributes.Add("style", "display:none");
                    //row6.Attributes.Add("style", "display:none");
                    //row7.Attributes.Add("style", "display:none");
                    //row8.Attributes.Add("style", "display:none");
                    //row9.Attributes.Add("style", "display:none");
                    //row10.Attributes.Add("style", "display:none");
                    //row11.Attributes.Add("style", "display:none");
                    //row12.Attributes.Add("style", "display:none");
                    //row13.Attributes.Add("style", "display:none");
                    //row14.Attributes.Add("style", "display:none");
                    //row15.Attributes.Add("style", "display:none");
                    //row16.Attributes.Add("style", "display:none");
                    //row17.Attributes.Add("style", "display:none");
                    //row18.Attributes.Add("style", "display:none");
                    //row19.Attributes.Add("style", "display:none");
                    //row20.Attributes.Add("style", "display:none");

                    //btnadd2.Attributes.Add("style", "display:none");
                    //btnadd3.Attributes.Add("style", "display:none");
                    //btnadd4.Attributes.Add("style", "display:none");
                    //btnadd5.Attributes.Add("style", "display:none");
                    //btnadd6.Attributes.Add("style", "display:none");
                    //btnadd7.Attributes.Add("style", "display:none");
                    //btnadd8.Attributes.Add("style", "display:none");
                    //btnadd9.Attributes.Add("style", "display:none");
                    //btnadd10.Attributes.Add("style", "display:none");
                    //btnadd11.Attributes.Add("style", "display:none");
                    //btnadd12.Attributes.Add("style", "display:none");
                    //btnadd13.Attributes.Add("style", "display:none");
                    //btnadd14.Attributes.Add("style", "display:none");
                    //btnadd15.Attributes.Add("style", "display:none");
                    //btnadd16.Attributes.Add("style", "display:none");
                    //btnadd17.Attributes.Add("style", "display:none");
                    //btnadd18.Attributes.Add("style", "display:none");
                    //btnadd19.Attributes.Add("style", "display:none");
                    //btnadd20.Attributes.Add("style", "display:none");
                    Label8.Visible = false;
                    AccountsVoucher3.Visible = false;

                    Label5.Visible = false;
                    Table1.Visible = false;

                    update_payments.Update();
                    update_forex.Update();

                }
                else if (drpvoucher_type.Text == "PURCHASE")
                {
                    DataSet ds6 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_INVOICE_NO_PURCHASE");
                    binddropdownlist(drp_invoice_no, ds6);

                    invoice_no_tr.Visible = true;
                    txt_invoice_no.Visible = false;
                //    Label64.Text = "Supplier Name";
                //    lbl_client_name.Visible = false;

              //      id_1.Visible = true;
                //    drp_client_name.Visible = true;
                    id_1.Visible = false;
                    id_2.Visible = false;
                    id_3.Visible = false;
                    id_4.Visible = false;
                    id_5.Visible = false;
                    id_6.Visible = false;

                    drp_invoice_no.Text = "";
                    drp_invoice_no.Visible = true;

                  

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_SUPPLIER_NAME");
                    binddropdownlist(drp_client_name, ds5);

                    DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row1_drp_glcode, ds4);
                    drp_invoice_no.Text = "";

                    row1_drp_glcode.Text = "";
                    row1_txt_debit.Text = "";
                    row1_txt_credit.Text = "";

                    row1_txt_credit.Enabled = true;
                    row1_txt_debit.Enabled = false;



                    row2_drp_gl.Text = "";
                    row2_txt_debit.Text = "";
                    row2_txt_credit.Text = "";

                    clear_and_hide();
                  
                    //row2.Attributes.Add("style", "display:none");
                    //row3.Attributes.Add("style", "display:none");
                    //row4.Attributes.Add("style", "display:none");
                    //row5.Attributes.Add("style", "display:none");
                    //row6.Attributes.Add("style", "display:none");
                    //row7.Attributes.Add("style", "display:none");
                    //row8.Attributes.Add("style", "display:none");
                    //row9.Attributes.Add("style", "display:none");
                    //row10.Attributes.Add("style", "display:none");
                    //row11.Attributes.Add("style", "display:none");
                    //row12.Attributes.Add("style", "display:none");
                    //row13.Attributes.Add("style", "display:none");
                    //row14.Attributes.Add("style", "display:none");
                    //row15.Attributes.Add("style", "display:none");
                    //row16.Attributes.Add("style", "display:none");
                    //row17.Attributes.Add("style", "display:none");
                    //row18.Attributes.Add("style", "display:none");
                    //row19.Attributes.Add("style", "display:none");
                    //row20.Attributes.Add("style", "display:none");

                    //btnadd2.Attributes.Add("style", "display:none");
                    //btnadd3.Attributes.Add("style", "display:none");
                    //btnadd4.Attributes.Add("style", "display:none");
                    //btnadd5.Attributes.Add("style", "display:none");
                    //btnadd6.Attributes.Add("style", "display:none");
                    //btnadd7.Attributes.Add("style", "display:none");
                    //btnadd8.Attributes.Add("style", "display:none");
                    //btnadd9.Attributes.Add("style", "display:none");
                    //btnadd10.Attributes.Add("style", "display:none");
                    //btnadd11.Attributes.Add("style", "display:none");
                    //btnadd12.Attributes.Add("style", "display:none");
                    //btnadd13.Attributes.Add("style", "display:none");
                    //btnadd14.Attributes.Add("style", "display:none");
                    //btnadd15.Attributes.Add("style", "display:none");
                    //btnadd16.Attributes.Add("style", "display:none");
                    //btnadd17.Attributes.Add("style", "display:none");
                    //btnadd18.Attributes.Add("style", "display:none");
                    //btnadd19.Attributes.Add("style", "display:none");
                    //btnadd20.Attributes.Add("style", "display:none");
                    //DataSet ds4 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    //binddropdownlist(row1_drp_glcode, ds4);

                   
                }
                //grid_payment_cb.Style.Add("style", "display:none");
                //grid_payment.Style.Add("style", "display:none");
                //Label4.Style.Add("style", "display:none");
                //row1_cb.Style.Add("style", "display:none");
                Label4.Visible = false;
                row1_cb.Visible = false;
                update_voucher.Update();
                updategrid.Update();
             
            }
        }
        #endregion

        #region INVOICE NO SELECTED INDEX CHANGED
        protected void drp_invoice_no_SelectedIndexChanged(object sender, EventArgs e)
        {

        
                if (drpvoucher_type.Text == "RECEIPT" || drpvoucher_type.Text == "SALES")
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

                    if (ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString() == "USD")
                    {
                        ViewState["total_amount"] = (decimal.Parse(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                    }
                    else
                    {
                        ViewState["total_amount"] = (decimal.Parse(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()));
                    }
                    update_voucher.Update();

                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO");

                    for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
                    {
                        if (drp_invoice_no.Text == ds7.Tables[0].Rows[i]["AutoSearchResult"].ToString() && drpvoucher_type.Text == "RECEIPT")
                        {
                            //        row1_txt_credit.Text = ds7.Tables[0].Rows[i]["TOTAL_AMOUNT"].ToString();
                            row1_txt_debit.Enabled = false;
                            row1_txt_credit.Enabled = true;
                            row1_txt_debit.Text = "";

                            //        row2.Attributes.Add("style", "display");
                            //        row2_txt_debit.Text = row1_txt_credit.Text;
                            //        row2_txt_credit.Enabled = false;
                            row2_txt_debit.Enabled = true;
                            row2_txt_credit.Text = "";

                            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                            binddropdownlist(row1_drp_glcode, ds5);
                            binddropdownlist(row2_drp_gl, ds5);

                            DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                            row1_drp_glcode.Text = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                            //   //     row2_drp_currency.Text = "THB";
                            //    //    row2_drp_currency.Enabled = false;

                            //    //    row2_drp_gl.Focus();
                            //   //     btnadd3.Attributes.Add("style", "display");

                            updategrid.Update();
                        }
                        else if (drp_invoice_no.Text == ds7.Tables[0].Rows[i]["AutoSearchResult"].ToString() && drpvoucher_type.Text == "SALES")
                        {
                            row1_txt_debit.Text = ViewState["total_amount"].ToString(); //ds7.Tables[0].Rows[i]["TOTAL_AMOUNT"].ToString();
                            ViewState["old_amount"] = ViewState["total_amount"].ToString();
                            row1_txt_credit.Enabled = false;
                            row1_txt_debit.Enabled = true;
                            row1_txt_credit.Text = "";

                            row2.Attributes.Add("style", "display");
                            row2_txt_credit.Text = row1_txt_debit.Text;
                            row2_txt_debit.Enabled = false;
                            row2_txt_credit.Enabled = true;
                            row2_txt_debit.Text = "";

                            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                            binddropdownlist(row1_drp_glcode, ds5);
                            binddropdownlist(row2_drp_gl, ds5);

                            DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                            row1_drp_glcode.Text = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                            row2_drp_currency.Text = "THB";
                            row2_drp_currency.Enabled = false;

                            btnadd3.Attributes.Add("style", "display:none");

                            row2_drp_gl.Focus();
                            btnadd3.Attributes.Add("style", "display:none");
                            updategrid.Update();
                        }

                    }
                }
                else if (drpvoucher_type.Text == "PAYMENT" || drpvoucher_type.Text == "PURCHASE")
                {
                    DataSet ds11 = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text );
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
                    DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE", drp_invoice_no.Text);

                    //id_1.Visible = true;
                    ////    id_2.Visible = true;
                    ////     id_3.Visible = true;
                    ////     id_4.Visible = true;
                    //id_5.Visible = true;
                    //id_6.Visible = true;

                    //lbl_client_name.Visible = true;
                    //Label64.Text = "Client Name";
                    //drp_client_name.Visible = false;

                    //DataSet ds_rate = objAcoountVouchersStoredProcedure.fetch_conversion_rate();
                    ////DataSet ds = objAcoountVouchersStoredProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", drp_invoice_no.Text);

                    //lbl_client_name.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                    ////      lbl_tour_short_name.Text = ds.Tables[0].Rows[0]["TOUR_SHORT_NAME"].ToString();
                    ////       lbl_package_name.Text = ds.Tables[0].Rows[0]["FIT_PACKAGE_NAME"].ToString();
                    ////       lbl_created_by.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                    //lbl_inovice_date.Text = ds.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                    //lbl_inovice_amount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    //lbl_currency_name.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();

                    //row1_drp_glcode.Text = ds.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();

                    if (ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString() == "USD")
                    {
                        ViewState["total_amount"] = (decimal.Parse(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                    }
                    else
                    {
                        ViewState["total_amount"] = (decimal.Parse(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()));
                    }
                    update_voucher.Update();


                    DataSet ds7 = objAcoountVouchersStoredProcedure.fetch_bank_name("FETCH_ALL_INVOICE_NO_PURCHASE");

                    for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
                    {
                        if (drp_invoice_no.Text == ds7.Tables[0].Rows[i]["AutoSearchResult"].ToString() && drpvoucher_type.Text == "PAYMENT")
                        {
                            //        row1_txt_credit.Text = ds7.Tables[0].Rows[i]["TOTAL_AMOUNT"].ToString();
                            row1_txt_debit.Enabled = true;
                            row1_txt_credit.Enabled = false;
                            row1_txt_debit.Text = "";

                            //        row2.Attributes.Add("style", "display");
                            //        row2_txt_debit.Text = row1_txt_credit.Text;
                            //        row2_txt_credit.Enabled = false;
                            row2_txt_debit.Enabled = true;
                            row2_txt_credit.Text = "";

                            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                            binddropdownlist(row1_drp_glcode, ds5);
                            binddropdownlist(row2_drp_gl, ds5);

                            //     DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                            //     row1_drp_glcode.Text = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                            //   //     row2_drp_currency.Text = "THB";
                            //    //    row2_drp_currency.Enabled = false;

                            //    //    row2_drp_gl.Focus();
                            //   //     btnadd3.Attributes.Add("style", "display");

                            updategrid.Update();
                        }
                        else if (drp_invoice_no.Text == ds7.Tables[0].Rows[i]["AutoSearchResult"].ToString() && drpvoucher_type.Text == "PURCHASE")
                        {
                            row1_txt_credit.Text = ViewState["total_amount"].ToString(); //ds7.Tables[0].Rows[i]["TOTAL_AMOUNT"].ToString();
                            ViewState["old_amount"] = ViewState["total_amount"].ToString();
                            row1_txt_credit.Enabled = true ;
                            row1_txt_debit.Enabled = false;
                            row1_txt_debit.Text = "";

                            row2.Attributes.Add("style", "display");
                            row2_txt_debit.Text = row1_txt_credit.Text;
                            row2_txt_debit.Enabled = true ;
                            row2_txt_credit.Enabled = false;
                            row2_txt_credit.Text = "";

                            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                            binddropdownlist(row1_drp_glcode, ds5);
                            binddropdownlist(row2_drp_gl, ds5);

                            //   DataSet ds6 = objAcoountVouchersStoredProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[0]["CUST_ID"].ToString(), ds.Tables[0].Rows[0]["FLAG"].ToString());
                            //   row1_drp_glcode.Text = ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();

                            row2_drp_currency.Text = "THB";
                            row2_drp_currency.Enabled = false;

                            btnadd3.Attributes.Add("style", "display:none");

                            row2_drp_gl.Focus();
                            btnadd3.Attributes.Add("style", "display:none");
                            updategrid.Update();
                        }

                    }
                }
            DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(drp_invoice_no.Text, drpvoucher_type.Text);
            DataSet ds_gl_code = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
          
            if (ds_check.Tables[0].Rows.Count != 0)
            {
                if (drpvoucher_type.Text == "PURCHASE" || drpvoucher_type.Text == "SALES")
                {
                    for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            ViewState["seq_no"] = ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString();

                            row1_drp_glcode.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row1_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row1_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            ViewState["row1_details_id_1"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                        }
                        else if (i == 1)
                        {
                            DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                            
                            binddropdownlist(row2_drp_gl, ds5);
                            row2_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row2_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row2_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            ViewState["row1_details_id_2"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                        }

                    }
                    txt_narration.Text = ds_check.Tables[0].Rows[0]["NARRATION"].ToString();
                    drpvoucher_status.Text = ds_check.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
                   

                }
                else if (drpvoucher_type.Text == "RECEIPT" || drpvoucher_type.Text == "PAYMENT")
                {
                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drp_currency, ds5);

                    DataSet ds3 = objAcoountVouchersStoredProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
                    binddropdownlist(drppayment_mode, ds3);
                    for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            ViewState["row1_details_id_1"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            ViewState["seq_no"] = ds_check.Tables[0].Rows[i]["SEQ_NO"].ToString();

                            row1.Attributes.Add("style", "display");

                            row1_drp_glcode.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row1_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row1_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            row1_txt_credit.Enabled = false;
                            row1_txt_debit.Enabled = false;

                            ViewState["rb1"] = "check";
                            ViewState["payment_date_1"] = ""; //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_1"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_1"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_1"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_1"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_1"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_1"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_1"] = ""; //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_1"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_1"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_1"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 1)
                        {
                            ViewState["row1_details_id_2"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            row2.Attributes.Add("style", "display");

                            row2_txt_credit.Enabled = false;
                            row2_txt_debit.Enabled = false;

                            row2_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row2_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row2_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd3.Attributes.Add("style", "display");
                            
                        }
                        else if (i == 2)
                        {
                            ViewState["row1_details_id_3"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row3_drp_gl, ds_gl_code);
                            row3.Attributes.Add("style", "display");

                            row3_txt_credit.Enabled = false;
                            row3_txt_debit.Enabled = false;

                            btnremove3.Attributes.Add("style", "display:none");
                            row3_cb.Visible = true;
                            row3_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row3_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row3_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            ViewState["rb3"] = "check";
                            ViewState["payment_date_3"] = ""; // ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_3"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_3"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_3"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_3"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_3"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_3"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_3"] = "";  //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_3"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_3"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_3"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 3)
                        {
                            ViewState["row1_details_id_4"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row4_drp_gl, ds_gl_code);
                         //   binddropdownlist(row4_drp_gl, ds_gl_code);
                            row4.Attributes.Add("style", "display");

                            btnremove4.Attributes.Add("style", "display:none");

                            row4_txt_credit.Enabled = false;
                            row4_txt_debit.Enabled = false;

                            btnremove4.Attributes.Add("style", "display:none");

                            row4_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row4_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row4_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd3.Attributes.Add("style", "display:none");
                            btnadd5.Attributes.Add("style", "display");
                        }

                        else if (i == 4)
                        {
                            ViewState["row1_details_id_5"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row5_drp_gl, ds_gl_code);
                            row5.Attributes.Add("style", "display");

                            row5_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row5_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row5_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            row5_cb.Visible = true;
                            row5_txt_credit.Enabled = false;
                            row5_txt_debit.Enabled = false;
                            btnremove5.Attributes.Add("style", "display:none");

                            ViewState["rb5"] = "check";
                            ViewState["payment_date_5"] = ""; // ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_5"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_5"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_5"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_5"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_5"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_5"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_5"] = "";  //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_5"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_5"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_5"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 5)
                        {
                            ViewState["row1_details_id_6"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row6_drp_gl, ds_gl_code);
                          
                            row6.Attributes.Add("style", "display");

                            row6_txt_credit.Enabled = false;
                            row6_txt_debit.Enabled = false;

                            btnremove6.Attributes.Add("style", "display:none");

                            row6_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row6_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row6_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd5.Attributes.Add("style", "display:none");
                            btnadd7.Attributes.Add("style", "display");
                        }

                        else if (i == 6)
                        {
                            ViewState["row1_details_id_7"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row7_drp_gl, ds_gl_code);
                            row7.Attributes.Add("style", "display");

                            row7_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row7_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row7_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            row7_cb.Visible = true;
                            row7_txt_credit.Enabled = false;
                            row7_txt_debit.Enabled = false;
                            btnremove7.Attributes.Add("style", "display:none");

                            ViewState["rb7"] = "check";
                            ViewState["payment_date_7"] = ""; // ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_7"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_7"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_7"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_7"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_7"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_7"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_7"] = "";  //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_7"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_7"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_7"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 7)
                        {
                            ViewState["row1_details_id_8"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row8_drp_gl, ds_gl_code);

                            row8.Attributes.Add("style", "display");

                            row8_txt_credit.Enabled = false;
                            row8_txt_debit.Enabled = false;

                            btnremove8.Attributes.Add("style", "display:none");

                            row8_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row8_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row8_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd7.Attributes.Add("style", "display:none");
                            btnadd9.Attributes.Add("style", "display");
                        }

                        else if (i == 8)
                        {
                            ViewState["row1_details_id_9"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row9_drp_gl, ds_gl_code);
                            row9.Attributes.Add("style", "display");

                            row9_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row9_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row9_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            row9_cb.Visible = true;
                            row9_txt_credit.Enabled = false;
                            row9_txt_debit.Enabled = false;
                            btnremove9.Attributes.Add("style", "display:none");

                            ViewState["rb9"] = "check";
                            ViewState["payment_date_9"] = ""; // ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_9"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_9"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_9"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_9"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_9"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_9"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_9"] = "";  //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_9"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_9"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_9"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 9)
                        {
                            ViewState["row1_details_id_10"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row10_drp_gl, ds_gl_code);

                            row10.Attributes.Add("style", "display");

                            row10_txt_credit.Enabled = false;
                            row10_txt_debit.Enabled = false;

                            row10_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row10_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row10_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd9.Attributes.Add("style", "display:none");
                            btnadd11.Attributes.Add("style", "display");
                        }

                        else if (i == 10)
                        {
                            ViewState["row1_details_id_11"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row11_drp_gl, ds_gl_code);
                            row9.Attributes.Add("style", "display");

                            row11_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row11_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row11_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            row11_cb.Visible = true;
                            row11_txt_credit.Enabled = false;
                            row11_txt_debit.Enabled = false;
                            btnremove9.Attributes.Add("style", "display:none");

                            ViewState["rb11"] = "check";
                            ViewState["payment_date_11"] = ""; // ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            ViewState["payment_mode_11"] = ds_check.Tables[0].Rows[i]["PAYMENT_MODE_NAME"].ToString();

                            ViewState["bank_name_11"] = ds_check.Tables[0].Rows[i]["BANK_NAME"].ToString();
                            ViewState["branch_11"] = ds_check.Tables[0].Rows[i]["BANK_BRNACH"].ToString();
                            ViewState["cheque_no_11"] = ds_check.Tables[0].Rows[i]["CHEQUE_NO"].ToString();
                            ViewState["cheque_date_11"] = ds_check.Tables[0].Rows[i]["CHEQUE_DATE"].ToString();


                            ViewState["receipt_no_11"] = ds_check.Tables[0].Rows[i]["CASH_RECIEPT_NO"].ToString();
                            ViewState["receipt_date_11"] = "";  //ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();

                            ViewState["currency_11"] = ds_check.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                            ViewState["amount_11"] = ds_check.Tables[0].Rows[i]["FOREX_AMOUNT"].ToString();
                            ViewState["ex_rate_11"] = ds_check.Tables[0].Rows[i]["EX_CHANGE_RATE"].ToString();
                        }
                        else if (i == 11)
                        {
                            ViewState["row1_details_id_12"] = ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString();
                            binddropdownlist(row12_drp_gl, ds_gl_code);

                            row12.Attributes.Add("style", "display");

                            row12_txt_credit.Enabled = false;
                            row12_txt_debit.Enabled = false;

                            row12_drp_gl.Text = ds_check.Tables[0].Rows[i]["GL_DESCRIPTION"].ToString();
                            row12_txt_credit.Text = ds_check.Tables[0].Rows[i]["CR_AMOUNT"].ToString();
                            row12_txt_debit.Text = ds_check.Tables[0].Rows[i]["DR_AMOUNT"].ToString();

                            btnadd11.Attributes.Add("style", "display:none");
                            btnadd13.Attributes.Add("style", "display");
                        }

                    }
                }
                UpdatePanel2.Update();
                updategrid.Update();
            }
        }
        #endregion

        protected void txt_ex_rate_TextChanged(object sender, EventArgs e)
        {
            if (row1_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row1_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
            //    if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text))
           //     {
           //         Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
           //     }
           //     else
           //     {
                    row2_txt_debit.Text = row1_txt_credit.Text;

                    row2.Attributes.Add("style", "display");
                    btnadd3.Attributes.Add("style", "display");

                    row1_txt_debit.Enabled = false;
                    row1_txt_credit.Enabled = true;
                    row2_txt_credit.Enabled = false ;
                    row2_txt_debit.Enabled = true;

                    updategrid.Update();
           //     }
            }
            else if (row1_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row1_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row2_txt_credit.Text = row1_txt_debit.Text;

                row1_txt_debit.Enabled = true;
                row1_txt_credit.Enabled = false;
                row2_txt_credit.Enabled = true;
                row2_txt_debit.Enabled = false;

                row2.Attributes.Add("style", "display");
                btnadd3.Attributes.Add("style", "display");

                updategrid.Update();
            }

            // 
            if (row3_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row3_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row4_txt_debit.Text = row3_txt_credit.Text;

                    row4.Attributes.Add("style", "display");
                    btnadd5.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                   
                    binddropdownlist(row4_drp_gl, ds5);

                    row3_txt_debit.Enabled = false;
                    row3_txt_credit.Enabled = true;
                    row4_txt_credit.Enabled = false;
                    row4_txt_debit.Enabled = true;

                    updategrid.Update();
                //}
            }
            else if (row3_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row3_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row4_txt_credit.Text = row3_txt_debit.Text;

                row4.Attributes.Add("style", "display");
                btnadd5.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                binddropdownlist(row4_drp_gl, ds5);

                row3_txt_debit.Enabled = true;
                row3_txt_credit.Enabled = false;
                row4_txt_credit.Enabled = true;
                row4_txt_debit.Enabled = false;

                updategrid.Update();
            }

            //
            if (row5_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row5_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row6_txt_debit.Text = row5_txt_credit.Text;

                    row6.Attributes.Add("style", "display");
                    btnadd5.Attributes.Add("style", "display:none");
                    btnadd7.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row6_drp_gl, ds5);

                    row5_txt_debit.Enabled = false ;
                    row5_txt_credit.Enabled = true ;
                    row6_txt_credit.Enabled = false ;
                    row6_txt_debit.Enabled = true ;

                    updategrid.Update();
             //   }
            }
            else if (row5_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row5_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row6_txt_credit.Text = row5_txt_debit.Text;

                row6.Attributes.Add("style", "display");
                btnadd5.Attributes.Add("style", "display:none");
                btnadd7.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                binddropdownlist(row6_drp_gl, ds5);

                row5_txt_debit.Enabled = true;
                row5_txt_credit.Enabled = false;
                row6_txt_credit.Enabled = true;
                row6_txt_debit.Enabled = false;

                updategrid.Update();
            }

            //
            if (row7_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row7_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row8_txt_debit.Text = row7_txt_credit.Text;

                    row8.Attributes.Add("style", "display");
                    btnadd7.Attributes.Add("style", "display:none");
                    btnadd9.Attributes.Add("style", "display");


                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row8_drp_gl, ds5);
                    row7_txt_debit.Enabled = false ;
                    row7_txt_credit.Enabled = true ;
                    row8_txt_credit.Enabled = false ;
                    row8_txt_debit.Enabled = true ;

                    updategrid.Update();
              //  }
            }
            else if (row7_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row7_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row8_txt_credit.Text = row7_txt_debit.Text;

                row8.Attributes.Add("style", "display");
                btnadd7.Attributes.Add("style", "display:none");
                btnadd9.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row8_drp_gl, ds5);

                row7_txt_debit.Enabled = true;
                row7_txt_credit.Enabled = false;
                row8_txt_credit.Enabled = true;
                row8_txt_debit.Enabled = false;

                updategrid.Update();
            }

            
            //
            if (row9_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {

                row9_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row10_txt_debit.Text = row9_txt_credit.Text;

                    row10.Attributes.Add("style", "display");
                    btnadd9.Attributes.Add("style", "display:none");
                    btnadd11.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row10_drp_gl, ds5);

                    row9_txt_debit.Enabled = false ;
                    row9_txt_credit.Enabled = true ;
                    row10_txt_credit.Enabled = false ;
                    row10_txt_debit.Enabled = true ;

                    updategrid.Update();
              //  }
            }
            else if (row9_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row9_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row10_txt_credit.Text = row9_txt_debit.Text;

                row10.Attributes.Add("style", "display");
                btnadd9.Attributes.Add("style", "display:none");
                btnadd11.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row10_drp_gl, ds5);

                row9_txt_debit.Enabled = true;
                row9_txt_credit.Enabled = false;
                row10_txt_credit.Enabled = true;
                row10_txt_debit.Enabled = false;

                updategrid.Update();
            }

            //
            if (row11_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row11_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row12_txt_debit.Text = row11_txt_credit.Text;

                    row12.Attributes.Add("style", "display");
                    btnadd11.Attributes.Add("style", "display:none");
                    btnadd13.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row12_drp_gl, ds5);

                    row11_txt_debit.Enabled = false ;
                    row11_txt_credit.Enabled = true ;
                    row12_txt_credit.Enabled = false ;
                    row12_txt_debit.Enabled = true ;

                    updategrid.Update();
              //  }
            }
            else if (row11_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row11_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row12_txt_credit.Text = row11_txt_debit.Text;

                row12.Attributes.Add("style", "display");

                btnadd11.Attributes.Add("style", "display:none");
                btnadd13.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row12_drp_gl, ds5);

                row11_txt_debit.Enabled = true;
                row11_txt_credit.Enabled = false;
                row12_txt_credit.Enabled = true;
                row12_txt_debit.Enabled = false;

                updategrid.Update();
            }

            //
            if (row13_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row13_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row14_txt_debit.Text = row13_txt_credit.Text;

                    row14.Attributes.Add("style", "display");
                    btnadd13.Attributes.Add("style", "display:none");
                    btnadd15.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row14_drp_gl, ds5);

                    row13_txt_debit.Enabled = false ;
                    row13_txt_credit.Enabled = true ;
                    row14_txt_credit.Enabled = false ;
                    row14_txt_debit.Enabled = true ;

                    updategrid.Update();
               // }
            }
            else if (row13_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row13_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row14_txt_credit.Text = row13_txt_debit.Text;

                row14.Attributes.Add("style", "display");
                btnadd13.Attributes.Add("style", "display:none");
                btnadd15.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row14_drp_gl, ds5);

                row13_txt_debit.Enabled = true;
                row13_txt_credit.Enabled = false;
                row14_txt_credit.Enabled = true;
                row14_txt_debit.Enabled = false;

                updategrid.Update();
            }

            // 15
            if (row15_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row15_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + +decimal.Parse(row15_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row16_txt_debit.Text = row15_txt_credit.Text;

                    row16.Attributes.Add("style", "display");
                    btnadd15.Attributes.Add("style", "display:none");
                    btnadd17.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row16_drp_gl, ds5);

                    row15_txt_debit.Enabled = false ;
                    row15_txt_credit.Enabled = true ;
                    row16_txt_credit.Enabled = false ;
                    row16_txt_debit.Enabled = true ;

                    updategrid.Update();
               // }
            }
            else if (row15_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row15_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row16_txt_credit.Text = row15_txt_debit.Text;

                row16.Attributes.Add("style", "display");
                btnadd15.Attributes.Add("style", "display:none");
                btnadd17.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row16_drp_gl, ds5);

                row15_txt_debit.Enabled = true;
                row15_txt_credit.Enabled = false;
                row16_txt_credit.Enabled = true;
                row16_txt_debit.Enabled = false;

                updategrid.Update();
            }

            // 17
            if (row17_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row17_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row18_txt_debit.Text = row17_txt_credit.Text;

                    row18.Attributes.Add("style", "display");
                    btnadd17.Attributes.Add("style", "display:none");
                    btnadd19.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row18_drp_gl, ds5);

                    row17_txt_debit.Enabled = false ;
                    row17_txt_credit.Enabled = true ;
                    row18_txt_credit.Enabled = false ;
                    row18_txt_debit.Enabled = true ;

                    updategrid.Update();
              //  }
            }
            else if (row17_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row17_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row18_txt_credit.Text = row17_txt_debit.Text;

                row18.Attributes.Add("style", "display");
                btnadd17.Attributes.Add("style", "display:none");
                btnadd19.Attributes.Add("style", "display");

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row18_drp_gl, ds5);

                row17_txt_debit.Enabled = true;
                row17_txt_credit.Enabled = false;
                row18_txt_credit.Enabled = true;
                row18_txt_debit.Enabled = false;

                updategrid.Update();
            }

            // 19
            if (row19_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
            {
                row19_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text) + decimal.Parse(row19_txt_credit.Text))
                //{
                //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                //}
                //else
                //{
                    row20_txt_debit.Text = row19_txt_credit.Text;
                    btnadd19.Attributes.Add("style", "display:none");
                    row20.Attributes.Add("style", "display");
                 //   btnadd19.Attributes.Add("style", "display");
                    row19_txt_debit.Enabled = false ;
                    row19_txt_credit.Enabled = true ;
                    row20_txt_credit.Enabled = false ;
                    row20_txt_debit.Enabled = true ;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row20_drp_gl, ds5);

                    updategrid.Update();
              //  }
            }
            else if (row19_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
            {
                row19_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                row20_txt_credit.Text = row19_txt_debit.Text;
                btnadd19.Attributes.Add("style", "display:none");
                row20.Attributes.Add("style", "display");

                row19_txt_debit.Enabled = true;
                row19_txt_credit.Enabled = false;
                row20_txt_credit.Enabled = true;
                row20_txt_debit.Enabled = false;

                DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                binddropdownlist(row20_drp_gl, ds5);

                updategrid.Update();
            }
        }

        protected void clear_and_hide()
        {
            ViewState.Clear();

            row1_txt_credit.Text = "";
            row1_txt_debit.Text = "";

            row1_drp_glcode.Text = "";

            row2_drp_gl.Text = "";
            row2_txt_debit.Text = "";
            row2_txt_credit.Text = "";

            row3_drp_gl.Text = "";
            row3_txt_debit.Text = "";
            row3_txt_credit.Text = "";

            row4_drp_gl.Text = "";
            row4_txt_debit.Text = "";
            row4_txt_credit.Text = "";

            row5_drp_gl.Text = "";
            row5_txt_debit.Text = "";
            row5_txt_credit.Text = "";

            row6_drp_gl.Text = "";
            row6_txt_debit.Text = "";
            row6_txt_credit.Text = "";

            row7_drp_gl.Text = "";
            row7_txt_debit.Text = "";
            row7_txt_credit.Text = "";

            row8_drp_gl.Text = "";
            row8_txt_debit.Text = "";
            row8_txt_credit.Text = "";

            row9_drp_gl.Text = "";
            row9_txt_debit.Text = "";
            row9_txt_credit.Text = "";

            row10_drp_gl.Text = "";
            row10_txt_debit.Text = "";
            row10_txt_credit.Text = "";

            row11_drp_gl.Text = "";
            row11_txt_debit.Text = "";
            row11_txt_credit.Text = "";

            row12_drp_gl.Text = "";
            row12_txt_debit.Text = "";
            row12_txt_credit.Text = "";

            row13_drp_gl.Text = "";
            row13_txt_debit.Text = "";
            row13_txt_credit.Text = "";

            row14_drp_gl.Text = "";
            row14_txt_debit.Text = "";
            row14_txt_credit.Text = "";

            row15_drp_gl.Text = "";
            row15_txt_debit.Text = "";
            row15_txt_credit.Text = "";

            row16_drp_gl.Text = "";
            row16_txt_debit.Text = "";
            row16_txt_credit.Text = "";

            row17_drp_gl.Text = "";
            row17_txt_debit.Text = "";
            row17_txt_credit.Text = "";

            row18_drp_gl.Text = "";
            row18_txt_debit.Text = "";
            row18_txt_credit.Text = "";

            row19_drp_gl.Text = "";
            row19_txt_debit.Text = "";
            row19_txt_credit.Text = "";

            row20_drp_gl.Text = "";
            row20_txt_debit.Text = "";
            row20_txt_credit.Text = "";

            //id_1.Visible = false;
            //txt_invoice_no.Visible = false;

            //invoice_no_tr.Visible = true;
            //drp_invoice_no.Visible = true;
            //id_2.Visible = false;
            //id_3.Visible = false;
            //id_4.Visible = false;
            //id_5.Visible = false;

            txt_amount.Text = "";
            txt_ex_rate.Text = "";
            drp_currency.Text = "";

            drppayment_mode.Text = "";
            drpbank_name.Text = "";
            drpbranch.Text = "";
            txt_payment_date.Text = "";
            txtcheque_no.Text = "";
            txtcheque_date.Text = "";
            txtcash_receipt.Text = "";
            txtcashreceipt_date.Text = "";

            txt_narration.Text = "";
            drpvoucher_status.Text = "";

            row2.Attributes.Add("style", "display:none");
            row3.Attributes.Add("style", "display:none");
            row4.Attributes.Add("style", "display:none");
            row5.Attributes.Add("style", "display:none");
            row6.Attributes.Add("style", "display:none");
            row7.Attributes.Add("style", "display:none");
            row8.Attributes.Add("style", "display:none");
            row9.Attributes.Add("style", "display:none");
            row10.Attributes.Add("style", "display:none");
            row11.Attributes.Add("style", "display:none");
            row12.Attributes.Add("style", "display:none");
            row13.Attributes.Add("style", "display:none");
            row14.Attributes.Add("style", "display:none");
            row15.Attributes.Add("style", "display:none");
            row16.Attributes.Add("style", "display:none");
            row17.Attributes.Add("style", "display:none");
            row18.Attributes.Add("style", "display:none");
            row19.Attributes.Add("style", "display:none");
            row20.Attributes.Add("style", "display:none");

            btnadd2.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display:none");
            btnadd11.Attributes.Add("style", "display:none");
            btnadd12.Attributes.Add("style", "display:none");
            btnadd13.Attributes.Add("style", "display:none");
            btnadd14.Attributes.Add("style", "display:none");
            btnadd15.Attributes.Add("style", "display:none");
            btnadd16.Attributes.Add("style", "display:none");
            btnadd17.Attributes.Add("style", "display:none");
            btnadd18.Attributes.Add("style", "display:none");
            btnadd19.Attributes.Add("style", "display:none");
            btnadd20.Attributes.Add("style", "display:none");

            update_forex.Update();
            update_payments.Update();
            update_voucher.Update();
            updategrid.Update();
            UpdatePanel2.Update();
        }

        protected void clear_payments_details()
        {
            drppayment_mode.Text = "";
            drpbank_name.Text = "";
            drpbranch.Text = "";
            txtcheque_no.Text = "";
            txtcheque_date.Text = "";
            txtcash_receipt.Text = "";
            txtcashreceipt_date.Text = "";

            drp_currency.Text = "";
            txt_amount.Text = "";
            txt_ex_rate.Text = "";

            bank_name_tr.Attributes.Add("style", "display:none");
            branch_tr.Attributes.Add("style", "display:none");
            cheque_no_tr.Attributes.Add("style", "display:none");
            cheque_date_tr.Attributes.Add("style", "display:none");
            cash_receipt_no_tr.Attributes.Add("style", "display:none");
            cash_receipt_date_tr.Attributes.Add("style", "display:none");

            update_payments.Update();
            update_forex.Update();
        }

        #region VALIDATIONS

        protected void validation()
        {
            // 1
            if (row1_txt_credit.Text != "" && row1_txt_debit.Text == "")
            {
                if (row1_drp_glcode.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 1's General Leger Code is required";
                    flag_valid = false;
                }
                if (row2_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 2's General Leger Code is required";
                    flag_valid = false;
                }
                if (row2_txt_credit.Text == "" && row2_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.1 has no debit entry ";
                    flag_valid = false;
                   
                }
               
            }
            else if (row1_txt_credit.Text == "" && row1_txt_debit.Text != "")
            {
                if (row1_drp_glcode.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 1's General Leger Code is required";
                    flag_valid = false;
                }
                if (row2_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 2's General Leger Code is required";
                    flag_valid = false;
                }
                if (row2_txt_credit.Text == "" && row2_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.1 has no debit entry ";
                    flag_valid = false;
                }
                
            }
            
            //2
            if (row3_txt_credit.Text != "" && row3_txt_debit.Text == "")
            {
                if (row3_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 3's General Leger Code is required";
                    flag_valid = false;
                }
                if (row4_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 4's General Leger Code is required";
                    flag_valid = false;
                }
                if (row4_txt_credit.Text == "" && row4_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.3 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row3_txt_credit.Text == "" && row3_txt_debit.Text != "")
            {
                if (row3_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 3's General Leger Code is required";
                    flag_valid = false;
                }
                if (row4_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 4's General Leger Code is required";
                    flag_valid = false;
                }
                if (row4_txt_credit.Text == "" && row4_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.3 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 3
            if (row5_txt_credit.Text != "" && row5_txt_debit.Text == "")
            {
                if (row5_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 5's General Leger Code is required";
                    flag_valid = false;
                }
                if (row6_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 6's General Leger Code is required";
                    flag_valid = false;
                }
                if (row6_txt_credit.Text == "" && row6_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.5 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row5_txt_credit.Text == "" && row5_txt_debit.Text != "")
            {
                if (row5_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 5's General Leger Code is required";
                    flag_valid = false;
                }
                if (row6_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 6's General Leger Code is required";
                    flag_valid = false;
                }
                if (row6_txt_credit.Text == "" && row6_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.5 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 4
            if (row7_txt_credit.Text != "" && row7_txt_debit.Text == "")
            {
                if (row7_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 7's General Leger Code is required";
                    flag_valid = false;
                }
                if (row8_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 8's General Leger Code is required";
                    flag_valid = false;
                }
                if (row8_txt_credit.Text == "" && row8_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.7 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row7_txt_credit.Text == "" && row7_txt_debit.Text != "")
            {
                if (row7_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 7's General Leger Code is required";
                    flag_valid = false;
                }
                if (row8_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 8's General Leger Code is required";
                    flag_valid = false;
                }
                if (row8_txt_credit.Text == "" && row8_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.7 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 5
            if (row9_txt_credit.Text != "" && row9_txt_debit.Text == "")
            {
                if (row9_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 9's General Leger Code is required";
                    flag_valid = false;
                }
                if (row10_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 10's General Leger Code is required";
                    flag_valid = false;
                }
                if (row10_txt_credit.Text == "" && row10_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.9 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row9_txt_credit.Text == "" && row9_txt_debit.Text != "")
            {
                if (row9_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 9's General Leger Code is required";
                    flag_valid = false;
                    
                }
                if (row10_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 10's General Leger Code is required";
                    flag_valid = false;
                }
                if (row10_txt_credit.Text == "" && row10_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.9 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 6
            if (row11_txt_credit.Text != "" && row11_txt_debit.Text == "")
            {
                if (row11_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 11's General Leger Code is required";
                    flag_valid = false;
                }
                if (row12_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 12's General Leger Code is required";
                    flag_valid = false;
                }
                if (row12_txt_credit.Text == "" && row12_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.11 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row11_txt_credit.Text == "" && row11_txt_debit.Text != "")
            {
                if (row11_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 11's General Leger Code is required";
                    flag_valid = false;
                }
                if (row12_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 12's General Leger Code is required";
                    flag_valid = false;
                }
                if (row12_txt_credit.Text == "" && row12_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.11 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 7
            if (row13_txt_credit.Text != "" && row13_txt_debit.Text == "")
            {
                if (row13_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 13's General Leger Code is required";
                    flag_valid = false;
                }
                if (row14_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 14's General Leger Code is required";
                    flag_valid = false;
                }
                if (row14_txt_credit.Text == "" && row14_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.13 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row13_txt_credit.Text == "" && row13_txt_debit.Text != "")
            {
                if (row13_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 13's General Leger Code is required";
                    flag_valid = false;
                }
                if (row14_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 14's General Leger Code is required";
                    flag_valid = false;
                }
                if (row14_txt_credit.Text == "" && row14_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.11 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 8
            if (row15_txt_credit.Text != "" && row15_txt_debit.Text == "")
            {
                if (row15_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 15's General Leger Code is required";
                    flag_valid = false;
                }
                if (row16_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 16's General Leger Code is required";
                    flag_valid = false;
                }
               
                if (row16_txt_credit.Text == "" && row16_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.15 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row15_txt_credit.Text == "" && row15_txt_debit.Text != "")
            {
                if (row15_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 15's General Leger Code is required";
                    flag_valid = false;
                }
                if (row16_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 16's General Leger Code is required";
                    flag_valid = false;
                }
                if (row16_txt_credit.Text == "" && row16_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.15 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 9
            if (row17_txt_credit.Text != "" && row17_txt_debit.Text == "")
            {
                if (row17_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 17's General Leger Code is required";
                    flag_valid = false;
                }
                if (row18_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 18's General Leger Code is required";
                    flag_valid = false;
                }
                if (row18_txt_credit.Text == "" && row18_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.17 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row17_txt_credit.Text == "" && row17_txt_debit.Text != "")
            {
                if (row17_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 17's General Leger Code is required";
                    flag_valid = false;
                }
                if (row18_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 18's General Leger Code is required";
                    flag_valid = false;
                }
                if (row18_txt_credit.Text == "" && row18_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.17 has no debit entry ";
                    flag_valid = false;
                }
            }

            // 10
            if (row19_txt_credit.Text != "" && row19_txt_debit.Text == "")
            {
                if (row19_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 19's General Leger Code is required";
                    flag_valid = false;
                }
                if (row20_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 20's General Leger Code is required";
                    flag_valid = false;
                }
                if (row20_txt_credit.Text == "" && row20_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.19 has no debit entry ";
                    flag_valid = false;

                }

            }
            else if (row19_txt_credit.Text == "" && row19_txt_debit.Text != "")
            {
                if (row19_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 19's General Leger Code is required";
                    flag_valid = false;
                }
                if (row20_drp_gl.Text == "")
                {
                    ViewState["error_msg"] = "Sr. no 20's General Leger Code is required";
                    flag_valid = false;
                }
                if (row20_txt_credit.Text == "" && row20_txt_debit.Text == "")
                {
                    ViewState["error_msg"] = "Entry no.19 has no debit entry ";
                    flag_valid = false;
                }
            }

        }

        protected void validation_required()
        {


            if (drpvoucher_type.Text == "")
            {
                ViewState["error_require"] = "Voucher Type is required";
                flag_require = false;
            }
            if (drp_invoice_no.Text == "")
            {
                ViewState["error_require"] = "Invoice no is required";
                flag_require = false;
            }

            if (row1_txt_credit.Text == "" && row1_txt_debit.Text == "")
            {

                ViewState["error_require"] = "At least one entry  is required";
                flag_require = false;


            }
        }

        protected void validation_amount()
        {
            if (drp_currency.Text == "USD")
            {
                if (row1_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row3_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row5_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row7_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row9_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row11_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row13_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row15_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row17_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(ViewState["amount_15"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
                else if (row19_cb.Checked == true)
                {
                    if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(ViewState["amount_15"].ToString()) + decimal.Parse(ViewState["amount_17"].ToString()) + decimal.Parse(txt_amount.Text))
                    {
                        Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                        flag_amount = false;
                    }
                }
            }
        }

           

        #endregion

        protected void txt_amount_TextChanged(object sender, EventArgs e)
        {
            if (drp_currency.Text == "")
            {
                Master.DisplayMessage("First Select the currency type.", "successMessage", 8000);
                txt_amount.Text = "";
                update_forex.Update();
            }
            else
            {
                exreate_amount_change();
                if (drp_currency.Text == "USD")
                {
                    if (row1_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row3_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row5_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < (decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(txt_amount.Text)))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row7_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row9_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row11_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row13_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row15_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row17_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(ViewState["amount_15"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                    else if (row19_cb.Checked == true)
                    {
                        if (decimal.Parse(lbl_inovice_amount.Text) < decimal.Parse(ViewState["amount_1"].ToString()) + decimal.Parse(ViewState["amount_3"].ToString()) + decimal.Parse(ViewState["amount_5"].ToString()) + decimal.Parse(ViewState["amount_7"].ToString()) + decimal.Parse(ViewState["amount_9"].ToString()) + decimal.Parse(ViewState["amount_11"].ToString()) + decimal.Parse(ViewState["amount_13"].ToString()) + decimal.Parse(ViewState["amount_15"].ToString()) + decimal.Parse(ViewState["amount_17"].ToString()) + decimal.Parse(txt_amount.Text))
                        {
                            Master.DisplayMessage("Entered amount is higher then invoice amount.", "successMessage", 8000);
                            flag_amount = false;
                        }
                    }
                }
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

        protected void txtcheque_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtcheque_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered date is not in valid format", "successMessage", 8000);
                flag_date = false;
            }
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
                Master.DisplayMessage("Entered date is not in valid format", "successMessage", 8000);
                flag_date = false;
            }
            update_payments.Update();
        }


        protected void exreate_amount_change()
        {
            if (txt_ex_rate.Text != "")
            {
                if (row1_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row1_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //    if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text))
                    //     {
                    //         Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //     }
                    //     else
                    //     {
                    row2_txt_debit.Text = row1_txt_credit.Text;

                    row2.Attributes.Add("style", "display");
                    btnadd3.Attributes.Add("style", "display");

                    row1_txt_debit.Enabled = false;
                    row1_txt_credit.Enabled = true;
                    row2_txt_credit.Enabled = false;
                    row2_txt_debit.Enabled = true;

                    updategrid.Update();
                    //     }
                }
                else if (row1_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row1_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row2_txt_credit.Text = row1_txt_debit.Text;

                    row1_txt_debit.Enabled = true;
                    row1_txt_credit.Enabled = false;
                    row2_txt_credit.Enabled = true;
                    row2_txt_debit.Enabled = false;

                    row2.Attributes.Add("style", "display");
                    btnadd3.Attributes.Add("style", "display");

                    updategrid.Update();
                }

                // 
                if (row3_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row3_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row4_txt_debit.Text = row3_txt_credit.Text;

                    row4.Attributes.Add("style", "display");
                    btnadd5.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row4_drp_gl, ds5);

                    row3_txt_debit.Enabled = false;
                    row3_txt_credit.Enabled = true;
                    row4_txt_credit.Enabled = false;
                    row4_txt_debit.Enabled = true;

                    updategrid.Update();
                    //}
                }
                else if (row3_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row3_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row4_txt_credit.Text = row3_txt_debit.Text;

                    row4.Attributes.Add("style", "display");
                    btnadd5.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row4_drp_gl, ds5);

                    row3_txt_debit.Enabled = true;
                    row3_txt_credit.Enabled = false;
                    row4_txt_credit.Enabled = true;
                    row4_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                //
                if (row5_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row5_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row6_txt_debit.Text = row5_txt_credit.Text;

                    row6.Attributes.Add("style", "display");
                    btnadd7.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row6_drp_gl, ds5);

                    row5_txt_debit.Enabled = false;
                    row5_txt_credit.Enabled = true;
                    row6_txt_credit.Enabled = false;
                    row6_txt_debit.Enabled = true;

                    updategrid.Update();
                    //   }
                }
                else if (row5_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row5_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row6_txt_credit.Text = row5_txt_debit.Text;

                    row6.Attributes.Add("style", "display");
                    btnadd7.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row6_drp_gl, ds5);

                    row5_txt_debit.Enabled = true;
                    row5_txt_credit.Enabled = false;
                    row6_txt_credit.Enabled = true;
                    row6_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                //
                if (row7_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row7_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row8_txt_debit.Text = row7_txt_credit.Text;

                    row8.Attributes.Add("style", "display");
                    btnadd9.Attributes.Add("style", "display");


                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");

                    binddropdownlist(row8_drp_gl, ds5);
                    row7_txt_debit.Enabled = false;
                    row7_txt_credit.Enabled = true;
                    row8_txt_credit.Enabled = false;
                    row8_txt_debit.Enabled = true;

                    updategrid.Update();
                    //  }
                }
                else if (row7_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row7_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row8_txt_credit.Text = row7_txt_debit.Text;

                    row8.Attributes.Add("style", "display");
                    btnadd9.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row8_drp_gl, ds5);

                    row7_txt_debit.Enabled = true;
                    row7_txt_credit.Enabled = false;
                    row8_txt_credit.Enabled = true;
                    row8_txt_debit.Enabled = false;

                    updategrid.Update();
                }


                //
                if (row9_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {

                    row9_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row10_txt_debit.Text = row9_txt_credit.Text;

                    row10.Attributes.Add("style", "display");
                    btnadd11.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row10_drp_gl, ds5);

                    row9_txt_debit.Enabled = false;
                    row9_txt_credit.Enabled = true;
                    row10_txt_credit.Enabled = false;
                    row10_txt_debit.Enabled = true;

                    updategrid.Update();
                    //  }
                }
                else if (row9_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row9_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row10_txt_credit.Text = row9_txt_debit.Text;

                    row10.Attributes.Add("style", "display");
                    btnadd11.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row10_drp_gl, ds5);

                    row9_txt_debit.Enabled = true;
                    row9_txt_credit.Enabled = false;
                    row10_txt_credit.Enabled = true;
                    row10_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                //
                if (row11_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row11_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row12_txt_debit.Text = row11_txt_credit.Text;

                    row12.Attributes.Add("style", "display");
                    btnadd13.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row12_drp_gl, ds5);

                    row11_txt_debit.Enabled = false;
                    row11_txt_credit.Enabled = true;
                    row12_txt_credit.Enabled = false;
                    row12_txt_debit.Enabled = true;

                    updategrid.Update();
                    //  }
                }
                else if (row11_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row11_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row12_txt_credit.Text = row11_txt_debit.Text;

                    row12.Attributes.Add("style", "display");
                    btnadd13.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row12_drp_gl, ds5);

                    row11_txt_debit.Enabled = true;
                    row11_txt_credit.Enabled = false;
                    row12_txt_credit.Enabled = true;
                    row12_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                //
                if (row13_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row13_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row14_txt_debit.Text = row13_txt_credit.Text;

                    row14.Attributes.Add("style", "display");
                    btnadd15.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row14_drp_gl, ds5);

                    row13_txt_debit.Enabled = false;
                    row13_txt_credit.Enabled = true;
                    row14_txt_credit.Enabled = false;
                    row14_txt_debit.Enabled = true;

                    updategrid.Update();
                    // }
                }
                else if (row13_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row13_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row14_txt_credit.Text = row13_txt_debit.Text;

                    row14.Attributes.Add("style", "display");
                    btnadd15.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row14_drp_gl, ds5);

                    row13_txt_debit.Enabled = true;
                    row13_txt_credit.Enabled = false;
                    row14_txt_credit.Enabled = true;
                    row14_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                // 15
                if (row15_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row15_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + +decimal.Parse(row15_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row16_txt_debit.Text = row15_txt_credit.Text;

                    row16.Attributes.Add("style", "display");
                    btnadd17.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row16_drp_gl, ds5);

                    row15_txt_debit.Enabled = false;
                    row15_txt_credit.Enabled = true;
                    row16_txt_credit.Enabled = false;
                    row16_txt_debit.Enabled = true;

                    updategrid.Update();
                    // }
                }
                else if (row15_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row15_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row16_txt_credit.Text = row15_txt_debit.Text;

                    row16.Attributes.Add("style", "display");
                    btnadd17.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row16_drp_gl, ds5);

                    row15_txt_debit.Enabled = true;
                    row15_txt_credit.Enabled = false;
                    row16_txt_credit.Enabled = true;
                    row16_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                // 17
                if (row17_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row17_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row18_txt_debit.Text = row17_txt_credit.Text;

                    row18.Attributes.Add("style", "display");
                    btnadd19.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row18_drp_gl, ds5);

                    row17_txt_debit.Enabled = false;
                    row17_txt_credit.Enabled = true;
                    row18_txt_credit.Enabled = false;
                    row18_txt_debit.Enabled = true;

                    updategrid.Update();
                    //  }
                }
                else if (row17_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row17_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row18_txt_credit.Text = row17_txt_debit.Text;

                    row18.Attributes.Add("style", "display");
                    btnadd19.Attributes.Add("style", "display");

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row18_drp_gl, ds5);

                    row17_txt_debit.Enabled = true;
                    row17_txt_credit.Enabled = false;
                    row18_txt_credit.Enabled = true;
                    row18_txt_debit.Enabled = false;

                    updategrid.Update();
                }

                // 19
                if (row19_cb.Checked == true && drpvoucher_type.Text == "RECEIPT")
                {
                    row19_txt_credit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    //if (decimal.Parse(ViewState["total_amount"].ToString()) < decimal.Parse(row1_txt_credit.Text) + decimal.Parse(row3_txt_credit.Text) + decimal.Parse(row5_txt_credit.Text) + decimal.Parse(row7_txt_credit.Text) + decimal.Parse(row9_txt_credit.Text) + decimal.Parse(row11_txt_credit.Text) + decimal.Parse(row13_txt_credit.Text) + decimal.Parse(row15_txt_credit.Text) + decimal.Parse(row17_txt_credit.Text) + decimal.Parse(row19_txt_credit.Text))
                    //{
                    //    Master.DisplayMessage("Entered credit amount is more then invoice amount.", "successMessage", 8000);
                    //}
                    //else
                    //{
                    row20_txt_debit.Text = row19_txt_credit.Text;

                    row20.Attributes.Add("style", "display");
                    //   btnadd19.Attributes.Add("style", "display");
                    row19_txt_debit.Enabled = false;
                    row19_txt_credit.Enabled = true;
                    row20_txt_credit.Enabled = false;
                    row20_txt_debit.Enabled = true;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row20_drp_gl, ds5);

                    updategrid.Update();
                    //  }
                }
                else if (row19_cb.Checked == true && drpvoucher_type.Text == "PAYMENT")
                {
                    row19_txt_debit.Text = (decimal.Parse(txt_amount.Text) * decimal.Parse(txt_ex_rate.Text)).ToString();
                    row20_txt_credit.Text = row19_txt_debit.Text;

                    row20.Attributes.Add("style", "display");

                    row19_txt_debit.Enabled = true;
                    row19_txt_credit.Enabled = false;
                    row20_txt_credit.Enabled = true;
                    row20_txt_debit.Enabled = false;

                    DataSet ds5 = objAcoountVouchersStoredProcedure.fetch_currency_name("FETCH_ALL_GL_CODE");
                    binddropdownlist(row20_drp_gl, ds5);

                    updategrid.Update();
                }
            }
        }

    
   }  
}