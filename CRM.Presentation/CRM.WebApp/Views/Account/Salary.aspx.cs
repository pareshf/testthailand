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
    public partial class Salary : System.Web.UI.Page
    {
        CRM.DataAccess.Account.Salary objSalaryVoucher = new CRM.DataAccess.Account.Salary();


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

                DataSet ds1 = objSalaryVoucher.fetch_voucher_type("FETCH_VOUCHER_STATUS_FOR_EXPENCE");
                binddropdownlist(drpvoucher_status, ds1);

                DataSet ds2 = objSalaryVoucher.fetch_voucher_type("GET_EMPLOYEE_NAME_FOR_SALARY_VOUCHER");
                binddropdownlist(row1_drp_glcode, ds2);

                DataSet ds3 = objSalaryVoucher.fetch_voucher_type("GET_BANK_NAME_FOR_SALARY_VAOUCHER");
                binddropdownlist(row2_drp_gl, ds3);

                if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()))
                {

                    int a = int.Parse(Request["IV"].ToString());
                    DataSet ds0 = objSalaryVoucher.GetAllData("FETCH_ALL_SALARY_DETAIL", a);

                    lblEmployee.Text = ds0.Tables[0].Rows[0]["GL_CODE"].ToString();
                    lblSalary.Text = ds0.Tables[0].Rows[0]["CR_AMOUNT"].ToString();
                    txtgl_date.Text = ds0.Tables[0].Rows[0]["GL_DATE"].ToString();
                    lbl_voucher_no.Text = ds0.Tables[0].Rows[0]["SALARY_VOUCHER_NO"].ToString(); 

                    for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            ViewState["seq_no"] = ds0.Tables[0].Rows[i]["SEQ_NO"].ToString();

                            row1_drp_glcode.Text = ds0.Tables[0].Rows[i]["GL_CODE"].ToString();
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
                            //txt_narration.Text = ds0.Tables[0].Rows[0]["NARRATION"].ToString();
                            drpvoucher_status.Text = ds0.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();

                            ViewState["row1_details_id_1"] = ds0.Tables[0].Rows[i]["SALARY_DETAILS_ID"].ToString();
                        }
                        else if (i == 1)
                        {
                            row2.Attributes.Add("style", "display");
                            row2_drp_gl.Text = ds0.Tables[0].Rows[i]["GL_CODE"].ToString();
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
                            ViewState["row1_details_id_2"] = ds0.Tables[0].Rows[i]["SALARY_DETAILS_ID"].ToString();
                        }
                        //txt_narration.Text = ds0.Tables[0].Rows[0]["NARRATION"].ToString();
                        drpvoucher_status.Text = ds0.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString();
                    }
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

            row2_txt_debit.Enabled = true;
            row2_txt_credit.Enabled = false;

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
            if (Request["IV"] != null && !string.IsNullOrEmpty(Request["IV"].ToString()))
            {

                objSalaryVoucher.Updatesalary(0,row1_txt_credit.Text, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.SelectedValue, 0, row1_txt_credit.Text, row1_txt_debit.Text, row1_drp_glcode.SelectedValue, int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text,  1);
                objSalaryVoucher.Updatesalary(0, "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.SelectedValue, int.Parse(ViewState["row1_details_id_1"].ToString()), row1_txt_credit.Text, row1_txt_debit.Text, row1_drp_glcode.SelectedValue, int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);
                objSalaryVoucher.Updatesalary(0, "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), drpvoucher_status.SelectedValue, int.Parse(ViewState["row1_details_id_2"].ToString()), row2_txt_credit.Text, row2_txt_debit.Text, row2_drp_gl.SelectedValue, int.Parse(ViewState["seq_no"].ToString()), txtgl_date.Text, 2);
                Master.DisplayMessage("Salary Voucher Updated Successfully.", "successMessage", 5000);
            }
        }

        //protected void drpvoucher_type_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}