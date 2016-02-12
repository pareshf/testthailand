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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.Account
{
    public partial class SalaryVouchers : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        SalaryVoucherStoredProcedure objSalaryVoucherStoredProcedure = new SalaryVoucherStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();

        int emp_id;
        string gl_desc;

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
                DataSet ds = objSalaryVoucherStoredProcedure.fetch_employee_details_dalary("FETCH_EMPLOYEE_SALARY");

                GV_Result.DataSource = ds;
                GV_Result.DataBind();
            }
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                     //   txt_expdate.Text = result;
                    }
                    else
                    {
                        result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                      //   txt_expdate.Text = result;
                    }
                }
                else
                {
                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    {
                        result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                     //   txt_expdate.Text = result;
                    }
                    else
                    {
                        result = w[1] + "/" + w[0] + "/" + t1[0];
                     //   txt_expdate.Text = result;
                    }
                }

            int newindex = e.NewSelectedIndex;

            
            string txt_amount = GV_Result.Rows[newindex].Cells[2].Text;
      

            DataSet ds = objSalaryVoucherStoredProcedure.fetch_employee_details_dalary("FETCH_EMPLOYEE_SALARY");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
              {
                 if (i == newindex)
                {

                     emp_id=int.Parse(ds.Tables[0].Rows[i]["EMP_ID"].ToString());
                     DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["EMP_ID"].ToString(), "E");
                     gl_desc = dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                 }
              }


            DataSet ds_EMP = objSalaryVoucherStoredProcedure.fetch_employee_details_dalary("FETCH_GL_CODE_FOR_EMPLOYEE_SALARY");

            DataSet ds_bank = objSalaryVoucherStoredProcedure.fetch_employee_bank("FETCH_GL_CODE_FROM_COMP_ACC", 1);

            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", txt_amount, txt_amount, int.Parse(Session["CompanyId"].ToString()), 1);
            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, gl_desc, "", txt_amount,  int.Parse(Session["CompanyId"].ToString()), 2);
            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, ds_EMP.Tables[0].Rows[1]["GL_DESCRIPTION"].ToString(), txt_amount, "", int.Parse(Session["CompanyId"].ToString()), 2);

            objBookingFitStoreProcedure.update_amount_COA(txt_amount, ds_EMP.Tables[0].Rows[1]["GL_CODE"].ToString());

            DataSet ds_seq_no = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO_FOR_SALARY");

            insert_voucher_no(ds_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString());

            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", txt_amount, txt_amount, int.Parse(Session["CompanyId"].ToString()), 1);
            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, gl_desc, txt_amount, "", int.Parse(Session["CompanyId"].ToString()), 2);
            objSalaryVoucherStoredProcedure.insert_salary_entry(0, emp_id, txt_amount, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, ds_bank.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), "", txt_amount, int.Parse(Session["CompanyId"].ToString()), 2);

            objSalaryVoucherStoredProcedure.insert_last_salary_date(result, emp_id);

            DataSet ds_seq_no1 = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO_FOR_SALARY");

            insert_voucher_no(ds_seq_no1.Tables[0].Rows[0]["SEQ_NO"].ToString());
            //    string vouchertype = GV_Result.Rows[newindex].Cells[4].Text;
            //  string usrid = Session["usersid"].ToString();
            ////  DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
            //  for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //  {
            //      if (quoteid == ds.Tables[0].Rows[i]["QUOTE_ID"].ToString() && tourid == ds.Tables[0].Rows[i]["TOUR_ID"].ToString())
            //      {
            //          Session["editorderstatus"] = ds.Tables[0].Rows[i]["ORDER_STATUS"].ToString();
            //      }
            //  }
            //DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE", TextBox1.Text);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (i == newindex)
            //    {
            //        DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");
            //        supplier_id = ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString();
            //        gl_desc = dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
            //        bank_name = ds.Tables[0].Rows[i]["BANK_NAME"].ToString();
            //        //if (ds.Tables[0].Rows[i]["BANK_NAME"].ToString() != "" || ds.Tables[0].Rows[i]["BANK_NAME"].ToString() != null)
            //        //{
            //        //    DataSet ds_bank_gl = objPaymentVoucherStoredProcedure.fetch_bank_gl_code("FETCH_ALL_GL_CODE_FROM_DESCRIPTION", ds.Tables[0].Rows[i]["BANK_NAME"].ToString());
            //        //    //DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
            //        //    //bank_name = dsgl_hotel.Tables[0].Rows[i]["BANK_NAME"].ToString();
            //        //    bank_name = ds_bank_gl.Tables[0].Rows[0]["GL_CODE"].ToString();
            //        //}
            //        break;

            //    }
            //}


           // ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", "", purchase_invoiceno, txt_amount, "PAYMENT", "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", "", "", "Remarks", "", drpsupplier_type, drpsupplier, "", 1);
           // ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", bank_name, purchase_invoiceno, txt_amount, "PAYMENT", "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", txt_amount, "", "Remarks", "", drpsupplier_type, drpsupplier, sales_invoiceno, 2);
         //   ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", gl_desc, "", txt_amount, "PAYMENT", "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, txt_amount, "", "", "REMARKS", "", drpsupplier_type, drpsupplier, sales_invoiceno, 2);


            DataSet ds11 = objSalaryVoucherStoredProcedure.fetch_employee_details_dalary("FETCH_EMPLOYEE_SALARY");

            GV_Result.DataSource = ds11;
            GV_Result.DataBind();
            Updateconfirm.Update();

            Master.DisplayMessage("Salary Paid Successfully.", "successMessage", 8000);

        }

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String invoice_no)
        {
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds_vn = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_MAX_VOUCHER_NO_SALARY");

            DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[12]["AutoSearchResult"].ToString());

         //   DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", invoice_no, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            //if (ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == "" || ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() == null)
            //{

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

                    objVouchersStoredProcedure.update_accounts_voucher_no_salary(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), invoice_no, voucher_no);
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

                    objVouchersStoredProcedure.update_accounts_voucher_no_salary(ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), invoice_no, voucher_no);
                }
            }
      //  }
        #endregion
    }
}