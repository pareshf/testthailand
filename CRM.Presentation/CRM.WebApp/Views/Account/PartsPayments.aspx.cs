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
    public partial class PartsPayments : System.Web.UI.Page
    {
       
        string supplier_id;

        PaymentVoucherStoredProcedure objPaymentVoucherStoredProcedure = new PaymentVoucherStoredProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();

        PaymentStoredProcedure ObjPaymentStoredProcedure = new PaymentStoredProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

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
                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE_PART_PAYMENTS", TextBox1.Text);
                try
                {
                   
                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        GV_Result.DataSource = ds;
                        GV_Result.DataBind();
                        Updateconfirm.Update();
                    }
                    else
                    {
                       
                        GV_Result.DataSource = "";
                        GV_Result.DataBind();
                    }
                }
                catch
                {
                    Master.DisplayMessage("Date is not in correct format", "successMessage", 8000);
                    Updateconfirm.Update();
                }
                finally
                {
                    Updateconfirm.Update();
                }
            
            }
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int newindex = e.NewSelectedIndex;

            string voucherno = GV_Result.Rows[newindex].Cells[7].Text;
            Response.Redirect("~/Views/Account/Payment.aspx?VN=" + voucherno);
            //System.DateTime today = DateTime.Now;
            //string source = today.ToString("dd/MM/yyyy");

            //DataSet ds_vt = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");


            //DataSet ds_vsstatus = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");


          
            //int newindex = e.NewSelectedIndex;
            //DropDownList ddl = (DropDownList)GV_Result.Rows[newindex].FindControl("drp_bank");
            //bank_name = ddl.Text;
            //if (bank_name == "")
            //{
            //    Master.DisplayMessage("First select bank to do payment.", "successMessage", 8000);
            //}
            //else
            //{
            //    string sales_invoiceno = GV_Result.Rows[newindex].Cells[4].Text;
            //    string txt_amount = GV_Result.Rows[newindex].Cells[2].Text;
            //    string drpsupplier_type = GV_Result.Rows[newindex].Cells[3].Text;
            //    string drpsupplier = GV_Result.Rows[newindex].Cells[1].Text;
            //    string purchase_invoiceno = GV_Result.Rows[newindex].Cells[5].Text;

            //    DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE", TextBox1.Text);
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (i == newindex)
            //        {
            //            DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");
            //            supplier_id = ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString();
            //            gl_desc = dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
            //            //  bank_name = ds.Tables[0].Rows[i]["BANK_NAME"].ToString();
            //            //if (ds.Tables[0].Rows[i]["BANK_NAME"].ToString() != "" || ds.Tables[0].Rows[i]["BANK_NAME"].ToString() != null)
            //            //{
            //            //    DataSet ds_bank_gl = objPaymentVoucherStoredProcedure.fetch_bank_gl_code("FETCH_ALL_GL_CODE_FROM_DESCRIPTION", ds.Tables[0].Rows[i]["BANK_NAME"].ToString());
            //            //    //DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
            //            //    //bank_name = dsgl_hotel.Tables[0].Rows[i]["BANK_NAME"].ToString();
            //            //    bank_name = ds_bank_gl.Tables[0].Rows[0]["GL_CODE"].ToString();
            //            //}
            //            break;

            //        }
            //    }


            
                //ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", "", purchase_invoiceno, txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", "", "TRANSFER", "Remarks", "", drpsupplier_type, drpsupplier, "", TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), false, 1);
                //ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", gl_desc, purchase_invoiceno, txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", txt_amount, "TRANSFER", "Remarks", "", drpsupplier_type, drpsupplier, sales_invoiceno, TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), false, 2);
                //ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", bank_name, "", txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, txt_amount, "", "TRANSFER", "REMARKS", "", drpsupplier_type, drpsupplier, sales_invoiceno, TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), false, 2);

                //DataSet fetch_seq_no = objVouchersStoredProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO");
               
                //insert_voucher_no(purchase_invoiceno);


               // insert_payment_voucher_no(fetch_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString());

                //DataSet ds11 = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE", TextBox1.Text);
                //GV_Result.DataSource = ds11;
                //GV_Result.DataBind();
                //Updateconfirm.Update();
                //Master.DisplayMessage("Payment done Successfully.", "successMessage", 8000);

              
          //  }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE_PART_PAYMENTS", TextBox1.Text);
            try
            {
                System.DateTime today = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);

                
                if (ds.Tables[0].Rows.Count != 0)
                {

                  
                    GV_Result.DataSource = ds;
                    GV_Result.DataBind();
                    Updateconfirm.Update();
                }
                else
                {
                    
                    GV_Result.DataSource = "";
                    GV_Result.DataBind();
                }
            }
            catch
            {
                Master.DisplayMessage("Date is not in correct format", "successMessage", 8000);
                Updateconfirm.Update();
            }
            finally
            {
                Updateconfirm.Update();
            }
        }

    }
}