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
    public partial class PaymentVouchers : System.Web.UI.Page
    {
        string gl_desc;
        string bank_name;
        string supplier_id;

        PaymentVoucherStoredProcedure objPaymentVoucherStoredProcedure = new PaymentVoucherStoredProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();

        PaymentStoredProcedure ObjPaymentStoredProcedure = new PaymentStoredProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 275);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds5 = objVouchersStoredProcedure.fetch_voucher_type("FECH_COMPANYS_BANK");
                binddropdownlist(DropDownList2, ds5);

                TextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");

             

                ViewState["gldate"] = TextBox1.Text;
                //DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("FETCH_DECSRIPTION_INVOICE_NO_PURCHASE_DUE_DATE", TextBox1.Text);
                DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("GET_PAYMENT_VOUCHERS_BY_DUE_DATE", TextBox1.Text);
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("GET_PAYMENT_VOUCHERS_BY_DUE_DATE", TextBox1.Text);
            try
            {
                System.DateTime today = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);

             
                if (ds.Tables[0].Rows.Count != 0)
                {

                
                  

                    GV_Result.DataSource = ds;
                    GV_Result.DataBind();
                    download.Attributes.Add("style", "display");
                    
                    Updateconfirm.Update();

                        
                }
                else
                {
                 //   Master.DisplayMessage("No Records found for this Due Date", "successMessage", 8000);
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

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            System.DateTime today = DateTime.Now;
            string source = today.ToString("dd/MM/yyyy");

            DataSet ds_vt = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

            DataSet ds_vsstatus = objVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            int newindex = e.NewSelectedIndex;
            DropDownList ddl = (DropDownList)GV_Result.Rows[newindex].FindControl("drp_bank");
            bank_name = ddl.Text;
            if (bank_name == "")
            {
                Master.DisplayMessage("First select bank to do payment.", "successMessage", 8000);
            }
            else
            {
                
                string txt_amount = GV_Result.Rows[newindex].Cells[2].Text;
                string drpsupplier_type = GV_Result.Rows[newindex].Cells[3].Text;
                string drpsupplier = GV_Result.Rows[newindex].Cells[1].Text;
                string purchase_invoiceno = GV_Result.Rows[newindex].Cells[4].Text;

                DataSet ds = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("GET_PAYMENT_VOUCHERS_BY_DUE_DATE", TextBox1.Text);
                supplier_id = ds.Tables[0].Rows[newindex]["SUPPLIER_ID"].ToString();
                gl_desc = ds.Tables[0].Rows[newindex]["GL_DESCRIPTION"].ToString();
                
                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", "", purchase_invoiceno, txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", "", "TRANSFER", "Remarks", "", drpsupplier_type, drpsupplier, "", TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), true , 1);
                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", gl_desc, purchase_invoiceno, txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, "", txt_amount, "TRANSFER", "Remarks", "", drpsupplier_type, drpsupplier, "", TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), true, 2);
                ObjPaymentStoredProcedure.insert_accounts_entry(0, "0", bank_name, "", txt_amount, ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString(), "THB", int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), "POSTED", 0, txt_amount, "", "TRANSFER", "REMARKS", "", drpsupplier_type, drpsupplier, "", TextBox1.Text, int.Parse(Session["CompanyId"].ToString()), ViewState["gldate"].ToString(), true, 2);

                ObjPaymentStoredProcedure.inserBankFees("UPDATE_BANK_CHARGE_FOR_PAYMENT", "0");

                DataSet fetch_seq_no = objVouchersStoredProcedure.fetch_voucher_type("FETCH_MAX_SEQ_NO");
                
                insert_voucher_no(purchase_invoiceno);

                insert_payment_voucher_no(fetch_seq_no.Tables[0].Rows[0]["SEQ_NO"].ToString());

                DataSet ds11 = objPaymentVoucherStoredProcedure.fetch_invoice_dateials("GET_PAYMENT_VOUCHERS_BY_DUE_DATE", TextBox1.Text);
                GV_Result.DataSource = ds11;
                GV_Result.DataBind();
                Updateconfirm.Update();
                Master.DisplayMessage("Payment done Successfully.", "successMessage", 8000);

                
            }
        }
    

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER
        protected void insert_voucher_no(String invoice_no)
        {
         
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", invoice_no, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

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

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), voucher_no);
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

                    objVouchersStoredProcedure.update_accounts_voucher_no(invoice_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), voucher_no);
                }

            }

            if (ds_vn_check.Tables[1].Rows[0]["ORDER_STATUS"].ToString() == "7")
            {
                insertCanellationDebitnoteVoucherNo(invoice_no, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString()); // Debit Note Vocueher No
                insertCanellationDebitnoteVoucherNo(invoice_no, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString()); // Purchase Cancellation Voucher No
            }

        }
        #endregion

        protected void insert_payment_voucher_no(string seq_no)
        {
            bool flag = true;
            DataSet ds = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("GET_PAYMENT_VOUCHER_DETAILS_FOR_VOUCHER_NO", seq_no);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == "" || ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString() == null)
                {
                    flag = false;
                    break;
                }
            }

            if (flag == true)
            {
                DataSet ds1 = objVouchersStoredProcedure.get_voucher_no_for_paymnet_check("CHECK_VOUCHER_NO_FOR_PURCHASE_PAYMENT", seq_no);

                if (ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != "" || ds1.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != null)
                {
                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString());
                    DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO_FROM_PURCHASE_PAYMENT", ds_vt.Tables[0].Rows[5]["AutoSearchResult"].ToString());
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

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            // r.SelectedValue = "0";
        }

        #region INSERT VOUCHER NO IN ACCOUNT VOUCHER HEADER FOR CONTRA VOUCHER
        protected void insert_voucher_no_contra(String seq_no)
        {
            //if (drpvoucher_status.Text == "POSTED")
            //{
                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                DataSet ds_vn = objVouchersStoredProcedure.get_max_voucher_no("FETCH_MAX_VOUCHER_NO", ds_vt.Tables[0].Rows[1]["AutoSearchResult"].ToString());

                DataSet ds_vn_code = objVouchersStoredProcedure.get_max_voucher_no("FETCH_VOUCHER_NO_CODE", ds_vt.Tables[0].Rows[1]["AutoSearchResult"].ToString());

                DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK_CONTRA_JOURNAL", seq_no, ds_vt.Tables[0].Rows[1]["AutoSearchResult"].ToString());

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

                        objVouchersStoredProcedure.update_accounts_voucher_no_contra_journal(seq_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[1]["AutoSearchResult"].ToString(), voucher_no);
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

                        objVouchersStoredProcedure.update_accounts_voucher_no_contra_journal(seq_no, ds_vsstatus.Tables[0].Rows[0]["AutoSearchResult"].ToString(), ds_vt.Tables[0].Rows[1]["AutoSearchResult"].ToString(), voucher_no);
                    }
                }
            }
        //}
        #endregion

        protected void GV_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string celltext = e.Row.Cells[1].Text;    // give the another cell index
                DropDownList ddl = (DropDownList)e.Row.FindControl("drp_bank");
                
                //DataSet dsval = objVouchersStoredProcedure.fetch_voucher_type("FETCH_OUTWARD_BANK");

                DataSet dsval = objVouchersStoredProcedure.fetch_voucher_type("FETCH_BANK_FOR_PAYMENT");
                binddropdownlist(ddl, dsval);
              
            }
        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            string fromdate;
            fromdate = TextBox1.Text;
            string url = "AutoPaymentsReport.aspx?key=" + fromdate;
            string fullURL = "window.open('" + url + "', '_blank', 'height=1000,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }

        protected void insertCanellationDebitnoteVoucherNo(string invoice_no, string voucher_type)
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
}