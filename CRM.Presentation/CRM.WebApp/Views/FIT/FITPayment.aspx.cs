using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


namespace CRM.WebApp.Views.FIT
{
    public partial class FITPayment : System.Web.UI.Page
    {
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        #region variable
        int ipr = 0;
        string usrid;
        int invoice_id = 0;

        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();

        string supplierid;
        string hotelsupplierid;
        string hotelsupplieridupdate;
        string supplieridupdate;
        string supplier_email;
        string supplier_emailupdate;
        byte[] renderedBytes2;

        string adults;
        string cwb;
        string cnb;
        string infant;

        string stsupplierid;
        string stseeingsupplierid;
        string stsupplieridupdate;
        string stsupplier_email;
        string stsupplier_emailupdate;
        byte[] renderedBytes3;

        string trsupplierid;
        string trseeingsupplierid;
        string trsupplieridupdate;
        string trsupplier_email;
        string trsupplier_tfdetailid;
        byte[] renderedBytes4;

        string smtpemail = "support@travelzunlimited.com";
        string smtppass = "30112011";
        string smtphost = "mail.travelzunlimited.com";
        string fromemail = "sudhir@travelzunlimited.com";

        string addcartid;
        string addsuppcartid;
        string addcartsupp_email;
        byte[] renderedBytes5;

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 216);
            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataTable dsORDERq = objFITPaymentStoreProcedure.Fetch_Order_Status_from_quoteid("FETCH_ORDER_STATUS_FROM_QUOTE_ID", Session["quoteid"].ToString());
                DataSet dsorder = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                DataSet ds = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
                binddropdownlist(drpPayment, ds);

                for (int i = drpPayment.Items.Count - 1; i > 0; i--)
                {
                    ListItem item = drpPayment.Items[i];
                    if (item.ToString() == "CASH" || item.ToString() == "CHEQUE" || item.ToString() == "TRANSFER")
                    {
                        drpPayment.Items.Remove(item);
                    }
                    else
                    {

                    }
                }
                

                if (dsORDERq.Rows[0]["AutoSearchResult"].ToString() == dsorder.Tables[0].Rows[15]["AutoSearchResult"].ToString())
                {
                    drpPayment.SelectedValue = ds.Tables[0].Rows[3]["AutoSearchResult"].ToString();
                    drpPayment.Enabled = false;
                    DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(Session["quoteid"].ToString()));
                    lbltotalInvoiceAmount.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                    c1.Attributes.Add("style", "display:none");
                    c2.Attributes.Add("style", "display:none");
                    c3.Attributes.Add("style", "display:none");
                    authorisation.Attributes.Add("style", "display");
                    c6.Attributes.Add("style", "display");
                    btnsend.Visible = false;

                    btnpatnow.Visible = true;

                    if (lblcurrentusableamount.Text == "")
                    {
                        lblcurrentusableamount.Text = "0";
                    }
                    updatebutton.Update();
                }
            }

        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            r.SelectedValue = "0";
        }

        protected void onitemrequest(RadComboBox r, DataTable dt, int noofitems)
        {
            ipr = dt.Rows.Count;
            int io = noofitems;
            int eo = Math.Min(io + ipr, dt.Rows.Count);
            r.Items.Clear();
            for (int i = io; i < eo; i++)
            {
                r.Items.Add(new RadComboBoxItem(dt.Rows[i]["AutoSearchResult"].ToString(), dt.Rows[i]["AutoSearchResult"].ToString()));
            }
        }

        public void drpPayment_SelectedIndexChanged(Object sender, EventArgs e)
        {
            DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
            DataTable DTMODE = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
            DataTable DTPAYPAL = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "2");
            if (drpPayment.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
            {
               DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

            
                lblcreditlimitAmount.Text = ds.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                lblcurrentusableamount.Text = ds.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
               
                if (lblcurrentusableamount.Text == "")
                {
                    lblcurrentusableamount.Text = "0";
                }
                lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();

                DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(Session["quoteid"].ToString()));
               
                lblTotalInvoice.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                Tr_Total_invoice.Attributes.Add("style", "display");

                // Discount Amount
                if (ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString() == "")
                {
                    lblDiscount.Text = "0";
                }
                else
                {
                    lblDiscount.Text = ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString();
                    Tr_Discount.Attributes.Add("style", "display");
                }
                
                // Bank charges
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["BANK_CHARGE_APPLICABLE"].ToString() == "True")
                    {
                        trBankCharge.Attributes.Add("style", "display");
                        lblBnakCharges.Text = ds.Tables[0].Rows[0]["BANK_CHARGES"].ToString();

                        if (lblBnakCharges.Text == "")
                        {
                            lblBnakCharges.Text = "0";
                        }
                    }
                    else
                    {
                        lblBnakCharges.Text = "0";
                    }
                }

                lbltotalInvoiceAmount.Text = (decimal.Parse(lblTotalInvoice.Text) - decimal.Parse(lblDiscount.Text) + decimal.Parse(lblBnakCharges.Text)).ToString();

                if (int.Parse(ds.Tables[0].Rows[0]["CUST_REL_ID"].ToString()) == 1)
                {
                    c1.Attributes.Add("style", "display");
                    c2.Attributes.Add("style", "display");
                    c3.Attributes.Add("style", "display");
                    c4.Attributes.Add("style", "display");
                    c5.Attributes.Add("style", "display");
                    c6.Attributes.Add("style", "display");
                    c9.Attributes.Add("style", "display:none");
                }
                else
                {
                    c4.Attributes.Add("style", "display");
                    c5.Attributes.Add("style", "display");
                    c6.Attributes.Add("style", "display");
                }
                
                btnsend.Visible = false;
                btnpatnow.Visible = true;
                ImageButton1.Visible = false;
                authorisation.Attributes.Add("style", "display:none");
                updatebutton.Update();
                
            }
            else if (drpPayment.Text == DTMODE.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display:none");
                c6.Attributes.Add("style", "display:none");
                c9.Attributes.Add("style", "display:none");
                
                DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(Session["quoteid"].ToString()));
                
                lblTotalInvoice.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                Tr_Total_invoice.Attributes.Add("style", "display");
                if (ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString() == "")
                {
                    lblDiscount.Text = "0";
                }
                else
                {
                    lblDiscount.Text = ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString();
                    Tr_Discount.Attributes.Add("style", "display");
                }

                // Bank charges
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["BANK_CHARGE_APPLICABLE"].ToString() == "True")
                    {
                        trBankCharge.Attributes.Add("style", "display");
                        lblBnakCharges.Text = ds.Tables[0].Rows[0]["BANK_CHARGES"].ToString();

                        if (lblBnakCharges.Text == "")
                        {
                            lblBnakCharges.Text = "0";
                        }
                    }
                    else
                    {
                        lblBnakCharges.Text = "0";
                    }
                }

                lbltotalInvoiceAmount.Text = (decimal.Parse(lblTotalInvoice.Text) - decimal.Parse(lblDiscount.Text) + decimal.Parse(lblBnakCharges.Text)).ToString();
                
                btnsend.Visible = true;
                btnpatnow.Visible = false;
                ImageButton1.Visible = false;
                updatebutton.Update();
                
            }
            else if (drpPayment.Text == DTPAYPAL.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display:none");
                c6.Attributes.Add("style", "display:none");
                authorisation.Attributes.Add("style", "display:none");
                c9.Attributes.Add("style", "display");
                DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(Session["quoteid"].ToString()));
               
                lblTotalInvoice.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                Tr_Total_invoice.Attributes.Add("style", "display");
                if (ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString() == "")
                {
                    lblDiscount.Text = "0";
                }
                else
                {
                    lblDiscount.Text = ds1.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString();
                    Tr_Discount.Attributes.Add("style", "display");
                }

                // Bank charges
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["BANK_CHARGE_APPLICABLE"].ToString() == "True")
                    {
                        trBankCharge.Attributes.Add("style", "display");
                        lblBnakCharges.Text = ds.Tables[0].Rows[0]["BANK_CHARGES"].ToString();

                        if (lblBnakCharges.Text == "")
                        {
                            lblBnakCharges.Text = "0";
                        }
                    }
                    else
                    {
                        lblBnakCharges.Text = "0";
                    }
                }

                lbltotalInvoiceAmount.Text = (decimal.Parse(lblTotalInvoice.Text) - decimal.Parse(lblDiscount.Text) + decimal.Parse(lblBnakCharges.Text)).ToString();
                
                if (lbltotalInvoiceAmount.Text == "")
                {
                    lbltotalInvoiceAmount.Text = "0.00";
                    decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                    string s = "3.5";
                    decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                    lbladditional.Text = addcharges.ToString();
                    DataTable DT = objFITPaymentStoreProcedure.fetch_currency();
                    lblcurr.Text = DT.Rows[2]["CURRENCY_NAME"].ToString();
                }
                else
                {
                    decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                    string s = "3.5";
                    decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                    lbladditional.Text = addcharges.ToString();
                    DataTable DT  = objFITPaymentStoreProcedure.fetch_currency();
                    lblcurr.Text = DT.Rows[2]["CURRENCY_NAME"].ToString();
                }
                btnsend.Visible = false;
                btnpatnow.Visible = false;
                ImageButton1.Visible = true;
                updatebutton.Update();
            }
            UpdatePanel_Hotel_header.Update();
        }


        protected void btnpatnow_Click(object sender, EventArgs e)
        {
            Session["adminuserid"] = Session["usersid"].ToString();

            DataSet dspayment = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
            DataTable dtauthono = objFITPaymentStoreProcedure.fetch_authorisationno("FETCH_AUTHORISATIONNO_FOR_CASH_ON_ARRIVAL", Session["quoteid"].ToString());
            DataSet dscurrency = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_ALL_CURRENCY_NAME");
            
            DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["empid"].ToString()));
            DataSet dspwd = objFITPaymentStoreProcedure.fetch_password(int.Parse(Session["empid"].ToString()));
            bool crdit_limit_flag = true;
            if (drpPayment.Text == dspayment.Tables[0].Rows[0]["AutoSearchResult"].ToString())
            {
                if (decimal.Parse(lblcurrentusableamount.Text) < decimal.Parse(lbltotalInvoiceAmount.Text))
                {
                    crdit_limit_flag = false;
                    Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
                }
            }
            if (txtpassowrd.Text != dspwd.Tables[0].Rows[0]["PASSWORD"].ToString())
            {
                Master.DisplayMessage("Password is incorrect.", "successMessage", 3000);
            }
            else if (txtauthorisation.Text != dtauthono.Rows[0]["AUTHORISATION_NO"].ToString())
            {
                Master.DisplayMessage("Authorisation Number is incorrect.", "successMessage", 3000);
            }
            else if (crdit_limit_flag == false)
            {
                Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
            }
            else
            {
                DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
                DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "5");
                DataTable dt = null;                                                                                                     //Session["rel_sr_no"].ToString()   
                dt = objFITPaymentStoreProcedure.insert_sales_invoice_header(int.Parse(Session["quoteid"].ToString()), int.Parse(usrid), int.Parse(Session["empid"].ToString()), Session["fromdate"].ToString(), Session["todate"].ToString(), Session["nights"].ToString(), decimal.Parse(lbltotalInvoiceAmount.Text), decimal.Parse("0"), decimal.Parse(lbltotalInvoiceAmount.Text), false, int.Parse(Session["noofadult"].ToString()), 0, int.Parse(Session["noofcwb"].ToString()), int.Parse(Session["noofcnb"].ToString()), int.Parse(Session["noofinfant"].ToString()), "Reconfirmed", drpPayment.Text, txtBook_ref_no.Text, int.Parse(Session["CompanyId"].ToString()), dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString());
                if (drpPayment.Text == dspayment.Tables[0].Rows[0]["AutoSearchResult"].ToString()) 
                {
                    objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["empid"].ToString()), decimal.Parse(lbltotalInvoiceAmount.Text));
                }
                invoice_id = Convert.ToInt32(dt.Rows[0]["INVOICE_ID"].ToString());
                objFITPaymentStoreProcedure.INSERT_ORDER_STATUS("INSERT_UPDATE_ORDER_STAUS_FOR_BOOKING", int.Parse(Session["quoteid"].ToString()), DTORDER.Rows[0]["ORDER_STATUS_NAME"].ToString());

                ///*********** CREATE SALES VOUCHER ********************************/
                DataSet ds11 = objFITPaymentStoreProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", dt.Rows[0]["INVOICE_NO"].ToString());

                DataSet ds_rate = objFITPaymentStoreProcedure.fetch_conversion_rate();

                DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();

                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                DataSet ds6 = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds11.Tables[0].Rows[0]["CUST_ID"].ToString(), ds11.Tables[0].Rows[0]["FLAG"].ToString());

                DataSet ds7 = objFITPaymentStoreProcedure.set_gl_code("FETCH_AMOUNT_CHART_OF_ACCOUNT", ds11.Tables[0].Rows[0]["CUST_ID"].ToString(), ds11.Tables[0].Rows[0]["FLAG"].ToString());

                DataSet ds_amount_fit_package = objFITPaymentStoreProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString());

                string total_sales_amount = (decimal.Parse(lbltotalInvoiceAmount.Text) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                

                DataSet ds_bal_type = objFITPaymentStoreProcedure.fetch_balance_type();
                // getting todays date
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
                        
                    }
                    else
                    {
                        result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                        
                    }
                }
                else
                {
                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    {
                        result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                        
                    }
                    else
                    {
                        result = w[1] + "/" + w[0] + "/" + t1[0];
                        
                    }
                }

                objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_sales_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_sales_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[11]["AutoSearchResult"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, total_sales_amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

              

                /********************** GENERATE PURCHASE VOUCHERS HOTEL*************************/

                DataSet ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                DataSet ds_p_common = objFITPaymentStoreProcedure.fetch_common_data("FETCH_HOTEL_FOR_PURCHASE_INVOICE", dt.Rows[0]["INVOICE_NO"].ToString());
                

                // HOTELS 

                for (int j = 0; j < ds_p_common.Tables[0].Rows.Count; j++)
                {
                    string hotel_name = ds_p_common.Tables[0].Rows[j]["CHAIN_NAME"].ToString();

                    string txt_amount = "0";
                    DataSet ds1 = objFITPaymentStoreProcedure.fetch_hotel_data("FETCH_HOTEL_RATE_FROM_SERVICE_CART", dt.Rows[0]["INVOICE_NO"].ToString(), hotel_name);
                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds1.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");

                    /********************************************** SERVICE VOUCHER NO INSERT*******************************/
                    DataSet ds_service_no = objFITPaymentStoreProcedure.fetch_hotel_service_no("FETCH_MAX_SERVICE_NO_FOR_HOTEL", int.Parse(ds1.Tables[0].Rows[0]["QUOTE_ID"].ToString()));

                    if (ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == "" || ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == null)
                    {
                        string serviceno = "SVH" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + "01";
                        objFITPaymentStoreProcedure.update_hotel_service_no(int.Parse(ds1.Tables[0].Rows[0]["QUOTE_ID"].ToString()), ds1.Tables[0].Rows[0]["CITY_NAME"].ToString(), serviceno);
                    }
                    else
                    {
                        string str11 = ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                        string[] words = str11.Split('-');

                        string no = (int.Parse(words[4].ToString()) + 01).ToString();

                        int len = no.Length;
                        if (len == 1)
                        {
                            no = "0" + no;
                        }

                        string serviceno = "SVH" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + no;
                        objFITPaymentStoreProcedure.update_hotel_service_no(int.Parse(ds1.Tables[0].Rows[0]["QUOTE_ID"].ToString()), ds1.Tables[0].Rows[0]["CITY_NAME"].ToString(), serviceno);
                    }
                    /************************************ SERVICE VOUCHER INSERT END ********************************************/
                    
                    string txtperiod_stay_from = ds1.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString();
                    string txtperiod_stay_to = ds1.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString();
                    DateTime date1 = DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null);
                    DateTime date2 = DateTime.ParseExact(txtperiod_stay_to, "dd/MM/yyyy", null);
                    TimeSpan ts;
                    ts = date2.Subtract(date1.Date);
                    string txtno_of_nights = ts.TotalDays.ToString();

                    DataSet ds_hotel_detials = objFITPaymentStoreProcedure.fetch_hotel_detials("FETCH_HOTEL_DETAILS", hotel_name, ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString());

                  
                    string txtno_of_nights1 = "0";
                    string txtsingle_room = "0";
                    string txtdouble_room = "0";
                    string txttriple_room = "0";

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string txtperiod_stay_from1 = ds1.Tables[0].Rows[i]["HOTEL_PERIOD_STAY_FROM"].ToString();
                        string txtperiod_stay_to1 = ds1.Tables[0].Rows[i]["HOTEL_PERIOD_STAY_TO"].ToString();
                        DateTime date11 = DateTime.ParseExact(txtperiod_stay_from1, "dd/MM/yyyy", null);
                        DateTime date21 = DateTime.ParseExact(txtperiod_stay_to1, "dd/MM/yyyy", null);
                        TimeSpan ts1;
                        ts1 = date21.Subtract(date11.Date);
                        txtno_of_nights1 = ts1.TotalDays.ToString();

                        if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "1")
                        {
                            txtsingle_room = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();


                            txt_amount = string.Format("{0:#.00}", decimal.Parse(txt_amount) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["SINGLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights1)));
                        }
                        else if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "2")
                        {
                            txtdouble_room = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();
                            txt_amount = string.Format("{0:#.00}", decimal.Parse(txt_amount) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["DOUBLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights1)));

                        }
                        else if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "3")
                        {
                            txttriple_room = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();
                            if (ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString() != "" && ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString() != null)
                            {
                                txt_amount = string.Format("{0:#.00}", decimal.Parse(txt_amount) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights1)));
                            }
                            else
                            {
                                txt_amount = string.Format("{0:#.00}", decimal.Parse(txt_amount) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["EXTRA_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_nights1)));
                            }
                        }

                    }

                  

                    string total_amount = txt_amount;
                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, hotel_name, dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds1.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount, "", total_amount, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(ds1.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), txtperiod_stay_from, txtperiod_stay_to, int.Parse(txtno_of_nights), int.Parse(txtsingle_room), int.Parse(txtdouble_room), int.Parse(txttriple_room), ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString(), 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objFITPaymentStoreProcedure.insert_purchase_entry(0, hotel_name, dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds1.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount, "", total_amount, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(ds1.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), ds1.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString(), ds1.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString(), int.Parse(txtno_of_nights), int.Parse(txtsingle_room), int.Parse(txtdouble_room), int.Parse(txttriple_room), ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString(), 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2);


                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, total_amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

               

                }

                /**************************************** TRANSFER PACKAGES ******************************************************/

                DataSet ds111 = objFITPaymentStoreProcedure.fetch_common_data("FETCH_INVOICE_DETAILS_FOR_PURCHASE_NEW", dt.Rows[0]["INVOICE_NO"].ToString());
                string txtno_of_adults;
                string txtno_of_cnb;
                string txtno_of_cwb;
                string txtno_of_infant;
                if (ds111.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString() == "")
                {
                    txtno_of_adults = "0";
                }
                else
                {
                    txtno_of_adults = ds111.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                }
                if (ds111.Tables[0].Rows[0]["NO_OF_CNB"].ToString() == "")
                {
                    txtno_of_cnb = "0";
                }
                else
                {
                    txtno_of_cnb = ds111.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                }
                if (ds111.Tables[0].Rows[0]["NO_OF_CWB"].ToString() == "")
                {
                    txtno_of_cwb = "0";
                }
                else
                {
                    txtno_of_cwb = ds111.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                }
                if (ds111.Tables[0].Rows[0]["NO_OF_INFANT"].ToString() == "")
                {
                    txtno_of_infant = "0";
                }
                else
                {
                    txtno_of_infant = ds111.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
                }
                DataSet ds_tp_supplier = objFITPaymentStoreProcedure.fetch_common_data("FETCH_TP_SUPPLIER_NO", dt.Rows[0]["INVOICE_NO"].ToString());
                if (ds_tp_supplier.Tables[0].Rows.Count != 0)
                {
                    DataSet ds_tp_data = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART", dt.Rows[0]["INVOICE_NO"].ToString(), ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString());
                    DataSet dsgl_tp = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_tp_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");
                  

                    string totalamount = "0";
                    string final_total = "0";

                    for (int it = 0; it < ds_tp_data.Tables[0].Rows.Count; it++)
                    {
                        /**************************************** SERVICE NO INSERT****************************************/
                        DataSet ds_service_no = objFITPaymentStoreProcedure.fetch_hotel_service_no("FETCH_MAX_SERVICE_NO_FOR_TRANSFER", int.Parse(ds_tp_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()));

                        if (ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == "" || ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == null)
                        {
                            string serviceno = "SVT" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + "01";
                            objFITPaymentStoreProcedure.update_transfer_service_no(int.Parse(ds_tp_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()), int.Parse(ds_tp_data.Tables[0].Rows[it]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), serviceno);
                        }
                        else
                        {
                            string str11 = ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                            string[] words = str11.Split('-');

                            string no = (int.Parse(words[4].ToString()) + 01).ToString();

                            int len = no.Length;
                            if (len == 1)
                            {
                                no = "0" + no;
                            }

                            string serviceno = "SVT" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + no;
                            objFITPaymentStoreProcedure.update_transfer_service_no(int.Parse(ds_tp_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()), int.Parse(ds_tp_data.Tables[0].Rows[it]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), serviceno);
                        }
                        /****************************** SERVICE NO INSERT END******************************************/

                        if (ds_tp_data.Tables[0].Rows[it]["SIC_PVT_FLAG"].ToString() == "SIC" && ds_tp_data.Tables[0].Rows[it]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                        {

                            totalamount = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[it]["ADULT_SIC_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[it]["CHILD_SIC_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                           

                            final_total = (decimal.Parse(final_total) + decimal.Parse(totalamount)).ToString();
                            
                        }

                        else if (ds_tp_data.Tables[0].Rows[it]["SIC_PVT_FLAG"].ToString() == "PVT" && ds_tp_data.Tables[0].Rows[it]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                        {
                            totalamount = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[it]["ADULT_PVT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[it]["CHILD_PVT_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                            final_total = (decimal.Parse(final_total) + decimal.Parse(totalamount)).ToString();
                            
                        }

                        else if (ds_tp_data.Tables[0].Rows[it]["SIC_PVT_FLAG"].ToString() == "SIC" && ds_tp_data.Tables[0].Rows[it]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                        {
                            totalamount = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[it]["SIC_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[it]["SIC_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                            final_total = (decimal.Parse(final_total) + decimal.Parse(totalamount)).ToString();
                            
                        }

                        else if (ds_tp_data.Tables[0].Rows[it]["SIC_PVT_FLAG"].ToString() == "PVT" && ds_tp_data.Tables[0].Rows[it]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                        {
                            totalamount = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[it]["PVT_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[it]["PVT_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                            final_total = (decimal.Parse(final_total) + decimal.Parse(totalamount)).ToString();
                            
                        }
                    }
                    DataSet dspurchasetp = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds_tp_data.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), final_total, "", final_total, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    for (int itp = 0; itp < ds_tp_data.Tables[0].Rows.Count; itp++)
                    {
                        string txt = "0";
                        if (ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString() == "SIC" && ds_tp_data.Tables[0].Rows[itp]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                        {

                            txt = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["ADULT_SIC_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["CHILD_SIC_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                        }

                        else if (ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString() == "PVT" && ds_tp_data.Tables[0].Rows[itp]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                        {
                            txt = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["ADULT_PVT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["CHILD_PVT_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                           
                        }

                        else if (ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString() == "SIC" && ds_tp_data.Tables[0].Rows[itp]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                        {
                            txt = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["SIC_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["SIC_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                           
                        }

                        else if (ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString() == "PVT" && ds_tp_data.Tables[0].Rows[itp]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                        {
                            txt = string.Format("{0:#.00}", (decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["PVT_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults) + decimal.Parse(ds_tp_data.Tables[0].Rows[itp]["PVT_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                           
                        }

                        
                        objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", txt, "", txt, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds_tp_data.Tables[0].Rows[itp]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString(), "", result, "", txt, int.Parse(Session["CompanyId"].ToString()), 2);
                    }
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_tp.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", final_total, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_tp.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, final_total, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[5]["AutoSearchResult"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", final_total, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

               
                }
                /**********************************sight seeing *************************************/

                DataSet ds_ss_supplier = objFITPaymentStoreProcedure.fetch_common_data("FETCH_SS_SUPPLIER_NO", dt.Rows[0]["INVOICE_NO"].ToString());
                if (ds_ss_supplier.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < ds_ss_supplier.Tables[0].Rows.Count; j++)
                    {

                        DataSet ds_ss_data = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_RATE_FROM_SERVICE_CART", dt.Rows[0]["INVOICE_NO"].ToString(), ds_ss_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
                        DataSet dsgl_SS = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_ss_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");


                      
                        /****************************** SERVICE NO INSERT END******************************************/


                        string sic_adult_rate;
                        string sic_child_rate;
                        string pvt_adult_rate;
                        string pvt_child_rate;
                        string txt_amount1 = "0";
                        for (int i = 0; i < ds_ss_data.Tables[0].Rows.Count; i++)
                        {
                            DataSet ds_service_no = objFITPaymentStoreProcedure.fetch_hotel_service_no("FETCH_MAX_SERVICE_NO_FOR_SIGHT", int.Parse(ds_ss_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()));

                            if (ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == "" || ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == null)
                            {
                                string serviceno = "SVS" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + "01";
                                objFITPaymentStoreProcedure.update_sight_service_no(int.Parse(ds_ss_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()), int.Parse(ds_ss_data.Tables[0].Rows[i]["SIGHT_SEEING_PRICE_ID"].ToString()), serviceno, int.Parse(ds_ss_data.Tables[0].Rows[i]["SERVICE_CART_ID"].ToString()));
                            }
                            else
                            {
                                string str11 = ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                                string[] words = str11.Split('-');

                                string no = (int.Parse(words[4].ToString()) + 01).ToString();

                                int len = no.Length;
                                if (len == 1)
                                {
                                    no = "0" + no;
                                }

                                string serviceno = "SVS" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + no;
                                objFITPaymentStoreProcedure.update_sight_service_no(int.Parse(ds_ss_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()), int.Parse(ds_ss_data.Tables[0].Rows[i]["SIGHT_SEEING_PRICE_ID"].ToString()), serviceno, int.Parse(ds_ss_data.Tables[0].Rows[i]["SERVICE_CART_ID"].ToString()));
                            }
                            string txt = "0";
                            if (ds_ss_data.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString() == "")
                            {
                                sic_adult_rate = "0";
                            }
                            else
                            {
                                sic_adult_rate = ds_ss_data.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString();
                            }
                            if (ds_ss_data.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString() == "")
                            {
                                sic_child_rate = "0";
                            }
                            else
                            {
                                sic_child_rate = ds_ss_data.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString();
                            }

                            if (ds_ss_data.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString() == "")
                            {
                                pvt_adult_rate = "0";
                            }
                            else
                            {
                                pvt_adult_rate = ds_ss_data.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString();
                            }
                            if (ds_ss_data.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString() == "")
                            {
                                pvt_child_rate = "0";
                            }
                            else
                            {
                                pvt_child_rate = ds_ss_data.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString();
                            }

                            if (ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC")
                            {
                                txt = string.Format("{0:#.00}", (decimal.Parse(sic_adult_rate) * decimal.Parse(txtno_of_adults) + decimal.Parse(sic_child_rate) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                                
                                txt_amount1 = string.Format("{0:#.00}", (decimal.Parse(txt_amount1) + decimal.Parse(txt)));
                            }

                            else if (ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT")
                            {
                                txt = string.Format("{0:#.00}", (decimal.Parse(pvt_adult_rate) * decimal.Parse(txtno_of_adults) + decimal.Parse(pvt_child_rate) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                                
                                txt_amount1 = string.Format("{0:#.00}", (decimal.Parse(txt_amount1) + decimal.Parse(txt)));
                            }
                        }
                        DataSet dspurchasess = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_ss_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds_ss_data.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount1, "", txt_amount1, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);
                        txt_amount1 = "0";
                        for (int i = 0; i < ds_ss_data.Tables[0].Rows.Count; i++)
                        {

                            string txt = "0";
                            if (ds_ss_data.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString() == "")
                            {
                                sic_adult_rate = "0";
                            }
                            else
                            {
                                sic_adult_rate = ds_ss_data.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString();
                            }
                            if (ds_ss_data.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString() == "")
                            {
                                sic_child_rate = "0";
                            }
                            else
                            {
                                sic_child_rate = ds_ss_data.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString();
                            }

                            if (ds_ss_data.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString() == "")
                            {
                                pvt_adult_rate = "0";
                            }
                            else
                            {
                                pvt_adult_rate = ds_ss_data.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString();
                            }
                            if (ds_ss_data.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString() == "")
                            {
                                pvt_child_rate = "0";
                            }
                            else
                            {
                                pvt_child_rate = ds_ss_data.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString();
                            }

                            if (ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC")
                            {
                                txt = string.Format("{0:#.00}", (decimal.Parse(sic_adult_rate) * decimal.Parse(txtno_of_adults) + decimal.Parse(sic_child_rate) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                                txt_amount1 = string.Format("{0:#.00}", (decimal.Parse(txt_amount1) + decimal.Parse(txt)));
                            }

                            else if (ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT")
                            {
                                txt = string.Format("{0:#.00}", (decimal.Parse(pvt_adult_rate) * decimal.Parse(txtno_of_adults) + decimal.Parse(pvt_child_rate) * (decimal.Parse(txtno_of_cnb) + decimal.Parse(txtno_of_cwb))));
                            
                                txt_amount1 = string.Format("{0:#.00}", (decimal.Parse(txt_amount1) + decimal.Parse(txt)));
                            }
                            objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_ss_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", txt, "", txt, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", ds_ss_data.Tables[0].Rows[i]["TRANSFER_SIGHT_SEEING_PACKAGE_FLAG"].ToString(), ds_ss_data.Tables[0].Rows[i]["DATE"].ToString(), ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString(), txt, int.Parse(Session["CompanyId"].ToString()), 2);
                        }

                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount1, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txt_amount1, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[3]["AutoSearchResult"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount1, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    }
                }

                /********************************* ADDITIONAL SERVICES  *******************************/
                string txt_amount2 = "0";
                DataSet ds_as_supplier = objFITPaymentStoreProcedure.fetch_common_data("FETCH_ADDITIONAL_SERVICES_SUPPLIERS_FOR_PURCHASE_INVOICE", dt.Rows[0]["INVOICE_NO"].ToString());
                if (ds_as_supplier.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < ds_as_supplier.Tables[0].Rows.Count; j++)
                    {
                        txt_amount2 = "0";
                        DataSet ds_as_data = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_ADDITIONAL_SERVICES_DESCRIPTION_FOR_PURCHASE_INVOICE", dt.Rows[0]["INVOICE_NO"].ToString(), ds_as_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
                        DataSet dsgl_SS = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_as_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");

                        /************************************* FIRST LOOP TO INSERRT PURCHASE INVOICE HEADER*********************************/
                        for (int i = 0; i < ds_as_data.Tables[0].Rows.Count; i++)
                        {
                            DataSet ds_service_no = objFITPaymentStoreProcedure.fetch_hotel_service_no("FETCH_MAX_SERVICE_NO_FOR_ADDITIONAL_SERVICE", int.Parse(ds_as_data.Tables[0].Rows[0]["QUOTE_ID"].ToString()));

                            if (ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString() == "")
                            {
                                string serviceno = "SVAS" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + "01";
                                objFITPaymentStoreProcedure.insert_additional_service(int.Parse(ds_as_data.Tables[0].Rows[i]["ADDITIONAL_SERVICE_CART_ID"].ToString()), serviceno);
                            }
                            else
                            {
                                string str11 = ds_service_no.Tables[0].Rows[0]["SERVICE_NO"].ToString();
                                string[] words = str11.Split('-');

                                string no = (int.Parse(words[4].ToString()) + 01).ToString();

                                int len = no.Length;
                                if (len == 1)
                                {
                                    no = "0" + no;
                                }

                                string serviceno = "SVAS" + "-" + dt.Rows[0]["INVOICE_NO"].ToString() + "-" + no;


                                objFITPaymentStoreProcedure.insert_additional_service(int.Parse(ds_as_data.Tables[0].Rows[i]["ADDITIONAL_SERVICE_CART_ID"].ToString()), serviceno);
                            }
                            txt_amount2 = string.Format("{0:#.00}", decimal.Parse(txt_amount2) + decimal.Parse(ds_as_data.Tables[0].Rows[i]["PURCHASE_INVOICE_AMOUNT"].ToString()));


                        }

                        DataSet dspurchasess = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_as_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds_as_data.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount2, "", txt_amount2, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                        /************************************* FIRST LOOP TO INSERRT PURCHASE INVOICE DETAILS *********************************/
                        for (int i = 0; i < ds_as_data.Tables[0].Rows.Count; i++)
                        {
                            objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_as_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", ds_as_data.Tables[0].Rows[i]["PURCHASE_INVOICE_AMOUNT"].ToString(), "", ds_as_data.Tables[0].Rows[i]["PURCHASE_INVOICE_AMOUNT"].ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(txtno_of_adults), int.Parse(txtno_of_cwb), int.Parse(txtno_of_cnb), int.Parse(txtno_of_infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds_as_data.Tables[0].Rows[i]["ADDITIONAL_SERVICE_CART_ID"].ToString()), "", "", ds_as_data.Tables[0].Rows[i]["DATE"].ToString(), ds_as_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString(), ds_as_data.Tables[0].Rows[i]["PURCHASE_INVOICE_AMOUNT"].ToString(), int.Parse(Session["CompanyId"].ToString()), 2);
                        }

                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount2, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txt_amount2, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                        //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[10]["AutoSearchResult"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["adminuserid"].ToString()), "", int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), int.Parse(Session["adminuserid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount2, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }
                }
                /******************************************************** ADDITIONAL SERVICE END*******************************************************************************/

                DataSet dsstatus = objBookingFitStoreProcedure.fetchComboData("FETCH_SALES_ACCOUNT_GL_CODE");
                if (dsstatus.Tables[0].Rows.Count != 0)
                {
                    if (lblDiscount.Text != "")
                    {
                        if (decimal.Parse(lblDiscount.Text) > 0)
                        {
                            string total_discount_amount = (decimal.Parse(lblDiscount.Text) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                            objBookingFitStoreProcedure.update_amount_in_thb(total_discount_amount, dt.Rows[0]["INVOICE_NO"].ToString());
                            objBookingFitStoreProcedure.update_amount_COA(total_discount_amount, dsstatus.Tables[0].Rows[0]["GL_CODE"].ToString());
                        }
                    }
                }



                Session["salesinvoiceid"] = invoice_id.ToString();

                /* PDF Generation Logic */

                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Invoices/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + invoice_id.ToString() + "/"));
                }

                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Itinerary/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Itinerary/" + invoice_id.ToString() + "/"));
                }
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/"));
                }
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/SightSeeingVoucher/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/SightSeeingVoucher/" + invoice_id.ToString() + "/"));
                }
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/TransferVoucher/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/TransferVoucher/" + invoice_id.ToString() + "/"));
                }
                //Additional Services Voucher
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/"));
                }

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";

                byte[] renderedBytes1;

                string deviceInfo1 =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>13in</PageWidth>" +
                "  <PageHeight>9in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";



                string deviceInfo2 =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>9in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";


                string deviceInfo3 =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>9in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";

                string deviceInfo4 =
           "<DeviceInfo>" +
           "  <OutputFormat>PDF</OutputFormat>" +
           "  <PageWidth>10in</PageWidth>" +
           "  <PageHeight>9in</PageHeight>" +
           "  <MarginTop>0.50in</MarginTop>" +
           "  <MarginLeft>0.50in</MarginLeft>" +
           "  <MarginRight>0.50in</MarginRight>" +
           "  <MarginBottom>0.50in</MarginBottom>" +
           "</DeviceInfo>";


                string deviceInfo5 =
               "<DeviceInfo>" +
               "  <OutputFormat>PDF</OutputFormat>" +
               "  <PageWidth>10in</PageWidth>" +
               "  <PageHeight>9in</PageHeight>" +
               "  <MarginTop>0.50in</MarginTop>" +
               "  <MarginLeft>0.50in</MarginLeft>" +
               "  <MarginRight>0.50in</MarginRight>" +
               "  <MarginBottom>0.50in</MarginBottom>" +
               "</DeviceInfo>";

                //------------------ invoice report----------------------------
                
                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("SALES_INVOICE_ID", invoice_id.ToString());
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/Receipt";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();
                //Render the report
                //Clear the response stream and write the bytes to the outputstream
                //Set content-disposition to "attachment" so that user is prompted to take an action
                //on the file (open or save)
                renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename= Quotation." + fileNameExtension);
                //Response.BinaryWrite(renderedBytes);

                //Response.End();


                //------------------------------ itineary -------------------------------------
                ReportParameter[] parmitieanry = new ReportParameter[1];
                parmitieanry[0] = new ReportParameter("SALES_INVOICE_ID", invoice_id.ToString());
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/Itinerary";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();

                renderedBytes1 = rptViewer1.ServerReport.Render(reportType, deviceInfo1, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //-------------------------- hotel vouchers--------------------------------
                string ht_vchrquoteid = Session["quoteid"].ToString();
                DataTable supplier_id = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();


                SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                SqlDataReader rdr = comm.ExecuteReader();
                supplier_id.Load(rdr);

                for (int i_supp = 0; i_supp < supplier_id.Rows.Count; i_supp++)
                {

                    supplierid = supplier_id.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    DataTable dt_hotelEmails = new DataTable();

                    SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                    email.CommandType = CommandType.StoredProcedure;
                    email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                    SqlDataReader rdremail = email.ExecuteReader();
                    dt_hotelEmails.Load(rdremail);

                    if (dt_hotelEmails.Rows.Count != 0)
                    {

                        hotelsupplierid = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                        supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


                        if (hotelsupplierid == supplierid)
                        {
                            ReportParameter[] parmhotel = new ReportParameter[1];
                            ReportParameter[] parmsuppid = new ReportParameter[1];
                            parmhotel[0] = new ReportParameter("SALSE_INVOICE_ID", invoice_id.ToString());
                            parmsuppid[0] = new ReportParameter("SUPPLIER_ID", hotelsupplierid.ToString());
                            rptViewer1.ShowCredentialPrompts = false;
                            rptViewer1.ShowParameterPrompts = false;

                            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                            rptViewer1.ServerReport.ReportPath = "/ThailandReport/HotelVoucher";
                            rptViewer1.ServerReport.SetParameters(parmhotel);
                            rptViewer1.ServerReport.SetParameters(parmsuppid);
                            rptViewer1.ServerReport.Refresh();

                            renderedBytes2 = rptViewer1.ServerReport.Render(reportType, deviceInfo2, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        }


                        using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/" + "HotelVoucher" + " " + invoice_id.ToString() + "-" + hotelsupplierid.ToString() + ".pdf")))
                        {
                            fs.Write(renderedBytes2, 0, (int)renderedBytes2.Length);
                        }

                       
                    }
                }
                //-------------------------- hotel vouchers Code Completed--------------------------------

                #region Sight Seeing Voucher
                //-------------------------- Sight Seeing vouchers-----------------------------------------------

                string st_invoiceid = invoice_id.ToString();
                DataTable sightsupplier_id = new DataTable();

                SqlCommand sightcomm = new SqlCommand("FETCH_SIGHT_SEEING_SUPPLIER_ID", conn);
                sightcomm.CommandType = CommandType.StoredProcedure;
                sightcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader stdr = sightcomm.ExecuteReader();
                sightsupplier_id.Load(stdr);

                for (int i_stsupp = 0; i_stsupp < sightsupplier_id.Rows.Count; i_stsupp++)
                {

                    stsupplierid = sightsupplier_id.Rows[i_stsupp]["SUPPLIER_ID"].ToString();
                    DataTable dt_sightSeeingEmails = new DataTable();

                    SqlCommand stsupplier_email = new SqlCommand("FETCH_SIGHT_SEEING_SPPLIER_EMAIL", conn);
                    stsupplier_email.CommandType = CommandType.StoredProcedure;
                    stsupplier_email.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                    SqlDataReader rdrsightemail = stsupplier_email.ExecuteReader();
                    dt_sightSeeingEmails.Load(rdrsightemail);

                    stseeingsupplierid = dt_sightSeeingEmails.Rows[i_stsupp]["SUPPLIER_ID"].ToString();
                    supplier_email = dt_sightSeeingEmails.Rows[i_stsupp]["SUPPLIER_REL_EMAIL"].ToString();

                    if (stseeingsupplierid == stsupplierid)
                    {
                        ReportParameter[] parmsight = new ReportParameter[1];
                        ReportParameter[] parmstsuppid = new ReportParameter[1];
                        parmsight[0] = new ReportParameter("SALES_INVOICE_ID", invoice_id.ToString());
                        parmstsuppid[0] = new ReportParameter("SUPPLIER_ID", stseeingsupplierid.ToString());
                        rptViewer1.ShowCredentialPrompts = false;
                        rptViewer1.ShowParameterPrompts = false;

                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/SightSeeingVoucher";
                        rptViewer1.ServerReport.SetParameters(parmsight);
                        rptViewer1.ServerReport.SetParameters(parmstsuppid);
                        rptViewer1.ServerReport.Refresh();

                        renderedBytes3 = rptViewer1.ServerReport.Render(reportType, deviceInfo3, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    }
                    using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/SightSeeingVoucher/" + invoice_id.ToString() + "/" + "SightSeeingVoucher" + " " + invoice_id.ToString() + "-" + stseeingsupplierid.ToString() + ".pdf")))
                    {
                        fs.Write(renderedBytes3, 0, (int)renderedBytes3.Length);
                    }
                }

                //-------------------------- Sight Seeing vouchers Code Completed--------------------------------
                #endregion

                #region Transfer Package Voucher
                //-------------------------- Transfer Package vouchers-----------------------------------------------

                string tr_invoiceid = invoice_id.ToString();
                DataTable transsupplier_id = new DataTable();

                SqlCommand transcomm = new SqlCommand("FETCH_TRANSFER_SUPPLIER_ID", conn);
                transcomm.CommandType = CommandType.StoredProcedure;
                transcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader transdr = transcomm.ExecuteReader();
                transsupplier_id.Load(transdr);

                for (int i_trsupp = 0; i_trsupp < transsupplier_id.Rows.Count; i_trsupp++)
                {

                    trsupplierid = transsupplier_id.Rows[i_trsupp]["SUPPLIER_ID"].ToString();
                    DataTable dt_transferEmails = new DataTable();

                    SqlCommand trsupplieremail = new SqlCommand("FETCH_TRANSFER_SPPLIER_EMAIL", conn);
                    trsupplieremail.CommandType = CommandType.StoredProcedure;
                    trsupplieremail.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                    SqlDataReader rdrtransferemail = trsupplieremail.ExecuteReader();
                    dt_transferEmails.Load(rdrtransferemail);

                    trseeingsupplierid = dt_transferEmails.Rows[i_trsupp]["SUPPLIER_ID"].ToString();
                    trsupplier_email = dt_transferEmails.Rows[i_trsupp]["SUPPLIER_REL_EMAIL"].ToString();
                    trsupplier_tfdetailid = dt_transferEmails.Rows[i_trsupp]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString();

                    if (trseeingsupplierid == trsupplierid)
                    {

                        ReportParameter[] parmtransfer = new ReportParameter[1];
                        ReportParameter[] parmsttransfer = new ReportParameter[1];
                        ReportParameter[] parmsttransfer3 = new ReportParameter[1];
                        parmtransfer[0] = new ReportParameter("SALES_INVOICE_ID", tr_invoiceid.ToString());
                        parmsttransfer[0] = new ReportParameter("SUPPLIER_ID", trseeingsupplierid.ToString());
                        parmsttransfer3[0] = new ReportParameter("TF_DETAIL_ID", trsupplier_tfdetailid.ToString());
                        rptViewer1.ShowCredentialPrompts = false;
                        rptViewer1.ShowParameterPrompts = false;

                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/TransferVoucher";
                        rptViewer1.ServerReport.SetParameters(parmtransfer);
                        rptViewer1.ServerReport.SetParameters(parmsttransfer);
                        rptViewer1.ServerReport.SetParameters(parmsttransfer3);
                        rptViewer1.ServerReport.Refresh();

                        renderedBytes4 = rptViewer1.ServerReport.Render(reportType, deviceInfo4, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    }

                    using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/TransferVoucher/" + invoice_id.ToString() + "/" + "TransferVoucher" + " " + invoice_id.ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf")))
                    {
                        fs.Write(renderedBytes4, 0, (int)renderedBytes4.Length);
                    }
                }


                //-------------------------- Transfer Package Code Completed--------------------------------
                #endregion


                #region Additional Services Voucher
                //-------------------------- Addtional Cart Service Voucher Generate-----------------------------------------------

                string addInvoiceid = invoice_id.ToString();


                DataTable addServicecartid = new DataTable();

                SqlCommand addServiceCommand = new SqlCommand("FETCH_ADDITIONAL_SERVICE_CART_ID", conn);
                addServiceCommand.CommandType = CommandType.StoredProcedure;
                addServiceCommand.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(addInvoiceid);
                SqlDataReader addServiceDr = addServiceCommand.ExecuteReader();
                addServicecartid.Load(addServiceDr);


                for (int i_addcrtid = 0; i_addcrtid < addServicecartid.Rows.Count; i_addcrtid++)
                {
                    addcartid = addServicecartid.Rows[i_addcrtid]["ADDITIONAL_SERVICE_CART_ID"].ToString();
                    DataTable dt_addcartsupplieremail = new DataTable();

                    SqlCommand addcartsupplieremail = new SqlCommand("FETCH_ADDITIONAL_SERVICE_CART_ID", conn);
                    addcartsupplieremail.CommandType = CommandType.StoredProcedure;
                    addcartsupplieremail.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                    SqlDataReader rdraddcartemail = addcartsupplieremail.ExecuteReader();
                    dt_addcartsupplieremail.Load(rdraddcartemail);

                    addsuppcartid = dt_addcartsupplieremail.Rows[i_addcrtid]["ADDITIONAL_SERVICE_CART_ID"].ToString();
                    addcartsupp_email = dt_addcartsupplieremail.Rows[i_addcrtid]["SUPPLIER_REL_EMAIL"].ToString();


                    if (addcartid == addsuppcartid)
                    {
                        ReportParameter[] parmaddcart = new ReportParameter[1];
                        ReportParameter[] parmstaddcart2 = new ReportParameter[1];
                        parmaddcart[0] = new ReportParameter("SALES_INVOICE_ID", addInvoiceid.ToString());
                        parmstaddcart2[0] = new ReportParameter("ADDCART_ID", addsuppcartid.ToString());
                        rptViewer1.ShowCredentialPrompts = false;
                        rptViewer1.ShowParameterPrompts = false;

                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/AdditionalServiceVoucher";
                        rptViewer1.ServerReport.SetParameters(parmaddcart);
                        rptViewer1.ServerReport.SetParameters(parmstaddcart2);

                        rptViewer1.ServerReport.Refresh();

                        renderedBytes5 = rptViewer1.ServerReport.Render(reportType, deviceInfo5, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        Response.Clear();

                    }
                    ///Views/FIT/AdditionalServicesVoucher/"
                    using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/" + "Additional Service Voucher" + " " + invoice_id.ToString() + "-" + addsuppcartid.ToString() + ".pdf")))
                    {
                        fs.Write(renderedBytes5, 0, (int)renderedBytes5.Length);
                    }
                }
                #endregion  

                SendMail(renderedBytes, renderedBytes1, dt.Rows[0]["INVOICE_NO"].ToString());
                rptViewer1.Visible = false;

                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Itinerary/" + invoice_id.ToString() + "/Itinerary.pdf")))
                {
                    fs.Write(renderedBytes1, 0, (int)renderedBytes1.Length);
                }

                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + invoice_id.ToString() + "/Invoice.pdf")))
                {
                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                }

                /* End */

                sendhotelmail(dt.Rows[0]["INVOICE_NO"].ToString());

                Send_invoice_to_agent(dt.Rows[0]["INVOICE_NO"].ToString());

                Master.DisplayMessage("Invoice Generate Successfully.", "successMessage", 3000);

                Response.Redirect("~/Views/FIT/AllBookngsReconformed.aspx");


            }

        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");
            DataSet dsorder = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
            objFITPaymentStoreProcedure.INSERT_ORDER_STATUS("INSERT_UPDATE_ORDER_STAUS_FOR_BOOKING", int.Parse(Session["quoteid"].ToString()), dsorder.Tables[0].Rows[14]["AutoSearchResult"].ToString());
            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[15]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();
                

                if (dtemail.Rows.Count == 0)
                {

                }
                else
                {
                    for (int i = 0; i < dtemail.Rows.Count; i++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                fromemail = Session["usersname"].ToString();
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                //  fromemail = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                fromemail = dtemail.Rows[i]["USER_NAME"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                toemail1 = Session["usersname"].ToString();
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                //   to = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                toemail1 = dtemail.Rows[i]["USER_NAME"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                cc = Session["usersname"].ToString();
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                //    cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                cc = dtemail.Rows[i]["USER_NAME"].ToString();
                            }
                        }

                        string bcc = "";
                        if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                bcc = Session["usersname"].ToString();
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                //   bcc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc = dtemail.Rows[i]["USER_NAME"].ToString();
                            }
                        }


                        string agentEmail = Session["usersname"].ToString();



                        string EmailQoute_id = Session["quoteid"].ToString();

                    
                        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                        DataTable dt_supplier_id = new DataTable();
                        SqlConnection conn = new SqlConnection(str);
                        DataTable dsdata = objHotelStoreProcedure.fetchagentdetailsforsendrequest("FETCH_DATA_FOR_FIT_PAYMENT_SEND_REQUEST", EmailQoute_id);
                        conn.Open();

                        string single_rooms = dsdata.Rows[0]["SINGLE_ROOMS"].ToString();
                        string double_rooms = dsdata.Rows[0]["DOUBLE_ROOMS"].ToString();
                        string tripal_rooms = dsdata.Rows[0]["TRIPAL_ROOMS"].ToString();
                        string pessanger_name = dsdata.Rows[0]["CLIENT_NAME"].ToString();
                        string check_in_date = dsdata.Rows[0]["CHECK_IN_DATE"].ToString();
                        string check_out_date = dsdata.Rows[0]["CHECK_OUT_DATE"].ToString();

                
                        try
                        {



                            MailMessage message = new MailMessage();
                         
                        string body = "";
                       
                        body = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                        body = body.Replace("&lt;%QUOTEID%&gt;", EmailQoute_id);
                        body = body.Replace("&lt;%CLIENTNAME%&gt;", pessanger_name);
                        body = body.Replace("&lt;%SINGLE_ROOMS%&gt;", single_rooms);
                        body = body.Replace("&lt;%DOUBLE_ROOMS%&gt;", double_rooms);
                        body = body.Replace("&lt;%TRIPLE_ROOMS%&gt;", tripal_rooms);
                        body = body.Replace("&lt;%CHECKINDATE%&gt;", check_in_date);
                        body = body.Replace("&lt;%CHECKOUTDATE%&gt;", check_out_date);

                        message.From = new MailAddress(fromemail);
                        if (cc == "")
                        {
                        }
                        else
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc));
                        }
                        message.To.Add(new MailAddress(toemail1.ToString()));

                        string subjct = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();


                        message.Subject = subjct;

                     

                        message.Body = body;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient();
                        NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                        client.Credentials = info;
                        client.Host = smtphost;
                        client.Port = int.Parse(smtpport);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(message);

                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), "", "", "", int.Parse(Session["usersid"].ToString()), 2);


                


                       
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    

                    

                }

            }
            Master.DisplayMessage("Thank you! We Have Received Your Request.", "successMessage", 8000);

        }
        

        #region ALL EMAILS
        protected void SendMail(byte[] _file1, byte[] _file2, String invoice_no)
        {


            #region AgentEmail Variable
            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[6]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                string agentEmail = Session["usersname"].ToString();
                string fromdate;
                string todate;
                string clientname;
                string agentname;

                string EmailQoute_id = Session["quoteid"].ToString();
               
                string fromemail = "";
                string toemail1 = "";
                string cc = "";

               

                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                DataTable dt_supplier_id = new DataTable();
                SqlConnection conn = new SqlConnection(str);

                conn.Open();
                SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                SqlDataReader rdr = comm.ExecuteReader();
                dt_supplier_id.Load(rdr);


                string st_invoiceid = invoice_id.ToString();
                DataTable sightsupplier_id = new DataTable();

                SqlCommand sightcomm = new SqlCommand("FETCH_SIGHT_SEEING_SUPPLIER_ID", conn);
                sightcomm.CommandType = CommandType.StoredProcedure;
                sightcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader stdr = sightcomm.ExecuteReader();
                sightsupplier_id.Load(stdr);


                string tr_invoiceid = invoice_id.ToString();
                DataTable transsupplier_id = new DataTable();

                SqlCommand transcomm = new SqlCommand("FETCH_TRANSFER_SUPPLIER_ID", conn);
                transcomm.CommandType = CommandType.StoredProcedure;
                transcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader transdr = transcomm.ExecuteReader();
                transsupplier_id.Load(transdr);


                DataTable addServicecartid = new DataTable();

                SqlCommand addServiceCommand = new SqlCommand("FETCH_ADDITIONAL_SERVICE_CART_ID", conn);
                addServiceCommand.CommandType = CommandType.StoredProcedure;
                addServiceCommand.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader addServiceDr = addServiceCommand.ExecuteReader();
                addServicecartid.Load(addServiceDr);

                DataTable dt_agent = new DataTable();

                SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
                agent_comm.CommandType = CommandType.StoredProcedure;
                agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                SqlDataReader agent_rdr = agent_comm.ExecuteReader();
                dt_agent.Load(agent_rdr);

                fromdate = dt_agent.Rows[0]["TOUR_FROM_DATE"].ToString();
                todate = dt_agent.Rows[0]["TOUR_TO_DATE"].ToString();
                clientname = dt_agent.Rows[0]["CLIENT_NAME"].ToString();
                agentname = dt_agent.Rows[0]["CUST_REL_NAME"].ToString();

                string bcc = "";

                if (dt_agent.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString() == "1")
                {
                    bcc = "1";
                }
                else
                {
                    bcc = "2";
                }


            #endregion


                string body = "";
            //    body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to reservations@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";
                body = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                body = body.Replace("&lt;%AGENTNAME%&gt;",agentname);
                body = body.Replace("&lt;%CLIENTNAME%&gt;", clientname);
                body = body.Replace("&lt;%PAYMENTMODE%&gt;", drpPayment.Text);

                try
                {
                    string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                    string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                    string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                    string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();



                    if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            if (bcc == "1")
                            {
                               
                                fromemail = "fitops@travelzunlimited.com";
                            }
                            else if (bcc == "2")
                            {
                                fromemail = "fitops@travelzunlimited.com";
                            }
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                        {
                            fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Supplier")
                        {
                            fromemail = supplier_email;
                        }
                        else
                        {
                            fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            if (bcc == "1")
                            {
                               // toemail1 = "reservation@travelzunlimited.com";
                                toemail1 = "fitops@travelzunlimited.com";
                            }
                            else if (bcc == "2")
                            {
                              //  toemail1 = "reservation1@travelzunlimited.com";
                                toemail1 = "fitops@travelzunlimited.com";
                            }
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                        {
                            toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                        {
                            toemail1 = supplier_email;
                        }
                        else
                        {
                            toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }


                    if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            if (bcc == "1")
                            {
                              //  cc  = "reservation@travelzunlimited.com";
                                cc = "fitops@travelzunlimited.com";
                            }
                            else if (bcc == "2")
                            {
                             //   cc = "reservation1@travelzunlimited.com";
                                cc = "fitops@travelzunlimited.com";
                            }
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                        {
                            cc  = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                        {
                            cc  = supplier_email;
                        }
                        else
                        {
                            cc  = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    string bcc1 = "";
                    if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            if (bcc == "1")
                            {
                              //  bcc1 = "reservation@travelzunlimited.com";
                                bcc1 = "fitops@travelzunlimited.com";
                            }
                            else if (bcc == "2")
                            {
                               // bcc1 = "reservation1@travelzunlimited.com";
                                bcc1 = "fitops@travelzunlimited.com";

                            }
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                        {
                            bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                        {
                            bcc1 = supplier_email;
                        }
                        else
                        {
                            bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromemail);
                    message.To.Add(new MailAddress(toemail1.ToString()));

                    if (cc != "")
                    {
                        message.CC.Add(new MailAddress(cc.ToString()));
                    }

                    if (bcc1 != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc1.ToString()));
                    }

               
                  //  message.Subject = "Booking for " + clientname + "Arrival date : " + fromdate + "Departure date : " + todate;
                    string subjct = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                    subjct = subjct.Replace("<%CLIENTNAME%>", clientname);
                    subjct = subjct.Replace("<%ARRIVALDATE%>", fromdate);
                    subjct = subjct.Replace("<%DEPARTUREDATE%>", todate);

                    message.Subject=subjct;
                //    message.Attachments.Add(new Attachment(new MemoryStream(_file1), "Invoice.pdf"));
                    message.Attachments.Add(new Attachment(new MemoryStream(_file2), "Itinerary.pdf"));

                    string filename11 = "~/Views/FIT/Itinerary/" + invoice_id + "/Itinerary.pdf";
                    string filename01 = "~/Views/FIT/Invoices/" + invoice_id + "/Invoice.pdf";
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filename01, "Quotation.pdf", int.Parse(Session["usersid"].ToString()), 1);
              //      objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filename01, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filename11, "Itinerary.pdf", int.Parse(Session["usersid"].ToString()), 2);

                    for (int i = 0; i < dt_supplier_id.Rows.Count; i++)
                    {
                        supplierid = dt_supplier_id.Rows[i]["SUPPLIER_ID"].ToString();
                        DataTable dt_hotelEmails = new DataTable();

                        SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                        email.CommandType = CommandType.StoredProcedure;
                        email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                        SqlDataReader rdremail = email.ExecuteReader();
                        dt_hotelEmails.Load(rdremail);

                        hotelsupplierid = dt_hotelEmails.Rows[i]["SUPPLIER_ID"].ToString();
                        supplier_email = dt_hotelEmails.Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
                        string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/" + "HotelVoucher" + " " + invoice_id.ToString() + "-" + hotelsupplierid.ToString() + ".pdf");
                        Attachment attachFile = new Attachment(filename);

                        message.Attachments.Add(attachFile);

                        string filename22 = "~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";
                        string filename23 = "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filename22, filename23, int.Parse(Session["usersid"].ToString()), 2);
                    }
                    for (int i = 0; i < sightsupplier_id.Rows.Count; i++)
                    {
                        stsupplierid = sightsupplier_id.Rows[i]["SUPPLIER_ID"].ToString();
                        DataTable dt_sightSeeingEmails = new DataTable();

                        SqlCommand stsupplier_email = new SqlCommand("FETCH_SIGHT_SEEING_SPPLIER_EMAIL", conn);
                        stsupplier_email.CommandType = CommandType.StoredProcedure;
                        stsupplier_email.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                        SqlDataReader rdrsightemail = stsupplier_email.ExecuteReader();
                        dt_sightSeeingEmails.Load(rdrsightemail);

                        stseeingsupplierid = dt_sightSeeingEmails.Rows[i]["SUPPLIER_ID"].ToString();
                        supplier_email = dt_sightSeeingEmails.Rows[i]["SUPPLIER_REL_EMAIL"].ToString();


                        string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/SightSeeingVoucher/" + invoice_id.ToString() + "/" + "SightSeeingVoucher" + " " + invoice_id.ToString() + "-" + stseeingsupplierid.ToString() + ".pdf");
                        Attachment attachFile = new Attachment(filename);

                        message.Attachments.Add(attachFile);

                        string filepath = "~/Views/FIT/SightSeeingVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "SightSeeingVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + stseeingsupplierid.ToString() + ".pdf";
                        string filename_ss = "SightSeeingVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + stseeingsupplierid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
                    }
                    for (int i = 0; i < transsupplier_id.Rows.Count; i++)
                    {
                        trsupplierid = transsupplier_id.Rows[i]["SUPPLIER_ID"].ToString();
                        DataTable dt_transferEmails = new DataTable();

                        SqlCommand trsupplieremail = new SqlCommand("FETCH_TRANSFER_SPPLIER_EMAIL", conn);
                        trsupplieremail.CommandType = CommandType.StoredProcedure;
                        trsupplieremail.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                        SqlDataReader rdrtransferemail = trsupplieremail.ExecuteReader();
                        dt_transferEmails.Load(rdrtransferemail);

                        trseeingsupplierid = dt_transferEmails.Rows[i]["SUPPLIER_ID"].ToString();
                        trsupplier_tfdetailid = dt_transferEmails.Rows[i]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString();


                        string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/TransferVoucher/" + invoice_id.ToString() + "/" + "TransferVoucher" + " " + invoice_id.ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf");
                        Attachment attachFile = new Attachment(filename);


                        message.Attachments.Add(attachFile);

                        string filepath = "~/Views/FIT/TransferVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "TransferVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf";
                        string filename_ss = "TransferVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
                    }

                    for (int i_addcrtid = 0; i_addcrtid < addServicecartid.Rows.Count; i_addcrtid++)
                    {

                        addcartid = addServicecartid.Rows[i_addcrtid]["ADDITIONAL_SERVICE_CART_ID"].ToString();
                        DataTable dt_addcartsupplieremail = new DataTable();

                        SqlCommand addcartsupplieremail = new SqlCommand("FETCH_ADDITIONAL_SERVICE_CART_ID", conn);
                        addcartsupplieremail.CommandType = CommandType.StoredProcedure;
                        addcartsupplieremail.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                        SqlDataReader rdraddcartemail = addcartsupplieremail.ExecuteReader();
                        dt_addcartsupplieremail.Load(rdraddcartemail);

                        addsuppcartid = dt_addcartsupplieremail.Rows[i_addcrtid]["ADDITIONAL_SERVICE_CART_ID"].ToString();


                        string filename6 = HttpContext.Current.Request.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/" + "Additional Service Voucher" + " " + invoice_id.ToString() + "-" + addsuppcartid.ToString() + ".pdf");
                        Attachment attachFile6 = new Attachment(filename6);

                        message.Attachments.Add(attachFile6);

                        // EMAIL STORED IN DATABASE
                        string filepath = "~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/" + "Additional Service Voucher" + " " + invoice_id.ToString() + "-" + addsuppcartid.ToString() + ".pdf";
                        string filename_ss = "Additional Service Voucher" + " " + invoice_id.ToString() + "-" + addsuppcartid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), invoice_no, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
                    }

                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    client.Credentials = info;
                    client.Host = smtphost;
                    client.Port = int.Parse(smtpport);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    //lblResult.Text = "Could not serve your request at this time. Please contact webmaster";
                    //lblResult.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
       

        protected void sendhotelmail(string invoice_no)
        {
            string single_rooms = "";
            string double_rooms = "";
            string tripal_rooms = "";
            string room_type = "";
            string hotel_name1 = "";
            string check_in_date = "";
            string check_out_date = "";
            string pessanger_name = "";
            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[6]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[1]["IS_ON"].ToString() != "False")
            {
                string EmailQoute_id = Session["quoteid"].ToString();
                DataTable dt_agent = new DataTable();
                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn1);
                agent_comm.CommandType = CommandType.StoredProcedure;
                agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                SqlDataReader agent_rdr = agent_comm.ExecuteReader();
                dt_agent.Load(agent_rdr);
                //variable-------
                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();




                //    DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[1]["AutoSearchResult"].ToString());

                //string smtpemail = "kushal@flamingotravels.co.in";
                //string smtppass = "dadashri";
                //string smtphost = "smtpcorp.com";
                //  string fromemail = "fitops@travelzunlimited.com";

                string fromemail = "";
                //
                //   string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                string ht_vchrquoteid = Session["quoteid"].ToString();
                DataTable supplier_id = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();



                SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                SqlDataReader rdr = comm.ExecuteReader();
                supplier_id.Load(rdr);

                int count = 0;
                for (int i_supp = 0; i_supp < supplier_id.Rows.Count; i_supp++)
                {

                    count = i_supp + 1;
                    string supplierid = supplier_id.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    DataTable dt_hotelEmails = new DataTable();

                    SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                    email.CommandType = CommandType.StoredProcedure;
                    email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                    SqlDataReader rdremail = email.ExecuteReader();
                    dt_hotelEmails.Load(rdremail);

                    string hotelsupplierid = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    string supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


                    if (hotelsupplierid == supplierid)
                    {
                        DataTable dt_hotelDetails = new DataTable();

                        SqlCommand details = new SqlCommand("FETCH_HOTEL_DETAILS_FOR_MAIL", conn);
                        details.CommandType = CommandType.StoredProcedure;
                        details.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                        details.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int).Value = int.Parse(hotelsupplierid);
                        SqlDataReader rdrdetails = details.ExecuteReader();

                        dt_hotelDetails.Load(rdrdetails);

                         single_rooms = dt_hotelDetails.Rows[0]["SINGLE_ROOMS"].ToString();
                       double_rooms = dt_hotelDetails.Rows[0]["DOUBLE_ROOMS"].ToString();
                       tripal_rooms = dt_hotelDetails.Rows[0]["TRIPAL_ROOMS"].ToString();
                       room_type = dt_hotelDetails.Rows[0]["ROOM_TYPE_NAME"].ToString();
                       hotel_name1 = dt_hotelDetails.Rows[0]["CHAIN_NAME"].ToString();
                       pessanger_name = dt_hotelDetails.Rows[0]["CLIENT_NAME"].ToString();
                       check_in_date = dt_hotelDetails.Rows[0]["CHECK_IN_DATE"].ToString();
                       check_out_date = dt_hotelDetails.Rows[0]["CHECK_OUT_DATE"].ToString();
                       adults = dt_hotelDetails.Rows[0]["NO_OF_ADULTS"].ToString();
                       cwb = dt_hotelDetails.Rows[0]["NO_OF_CWB"].ToString();
                       cnb = dt_hotelDetails.Rows[0]["NO_OF_CNB"].ToString();
                       infant = dt_hotelDetails.Rows[0]["NO_OF_INFANT"].ToString();
                    }


                    string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf");
                    Attachment attachFile = new Attachment(filename);

                    string filepath = "~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";
                    string filename_ss = "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";


                    if (ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
                        {
                            fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            fromemail = "fitops@travelzunlimited.com";
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
                        {
                            fromemail = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        }

                        else
                        {
                            fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    string to = "";
                    if (ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Agent")
                        {
                            to = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            to = "fitops@travelzunlimited.com";
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Supplier")
                        {
                            to = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        }

                        else
                        {
                            to = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    string cc = "";
                    if (ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
                        {
                            cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            cc = "fitops@travelzunlimited.com";
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Supplier")
                        {
                            cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        }

                        else
                        {
                            cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }
                   
                    string bcc = "";
                    if (ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
                        {
                            bcc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            bcc = "fitops@travelzunlimited.com";
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Supplier")
                        {
                            bcc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        }

                        else
                        {
                            bcc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }


                    string Confirmagentbody = "";
                    //       body = "Dear Reservation,<br><br> Please Re Confirm as Below <br><br> Our Reference Number:" + ht_vchrquoteid + "<br>Name of the passenger :" + pessanger_name + "<br>No of Single Rooms:" + single_rooms + "<br>No of Double Rooms:" + double_rooms + "<br>No of Triple Rooms:" + tripal_rooms + "<br>Check In Date : " + check_in_date + "<br>Check out Date :" + check_out_date + "<br><br> Please Send the Invoice to our account for the above booking on acccount@travelzunlimited.com <br><br> Please Check Attached Voucher for Pessenger";

                    Confirmagentbody = ds_mailTemplate.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();
                    //   Confirmagentbody = "Dear   " + Confirmagentname + ",<br><br>We are pleased to confirm the booking for  " + Confirmclientname + "  as attached.<br><br> Please Check All Booking details Stated in attached file <br><br> Please reconfirm before Cut of date :  " + AgentReconfirmation + "<br><br> Our Refrence No:" + refrenceno + " <br> Passenger Name :  " + Confirmclientname + "<br> Single Rooms :  " + singlerooms + "<br>Double Rooms : " + doublerooms + "<br> Tripal Rooms : " + triplarooms + "<br> Arival Date : " + Confirmfromdate + "<br> Departure Date : " + Confirmtodate + "<br><br>Best Regards<br>Travelz Unlimited";
                    //Confirmagentbody = Confirmagentbody.Replace("&lt;%%AGENTNAME%&gt;", Confirmagentname);
                    //Confirmagentbody = Confirmagentbody.Replace("&lt;%%CLIENTNAME%&gt;", Confirmclientname);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%QUOTEID%&gt;", ht_vchrquoteid);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%CLIENTNAME%&gt;", pessanger_name);


                    Confirmagentbody = Confirmagentbody.Replace("&lt;%SINGLEROOMS%&gt;", single_rooms);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%NOOFADULT%&gt;", adults);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%NOOFCWB%&gt;", cwb);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%NOOFCNB%&gt;", cnb);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%NOOFINFANT%&gt;", infant);


                    Confirmagentbody = Confirmagentbody.Replace("&lt;%DOUBLEROOMS%&gt;", double_rooms);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%TRIPLEROOMS%&gt;", tripal_rooms);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%ARRIVALDATE%&gt;", check_in_date);
                    Confirmagentbody = Confirmagentbody.Replace("&lt;%DEPARTUREDATE%&gt;", check_out_date);
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromemail);
                    //   message.To.Add(new MailAddress(dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString()));
                    message.To.Add(new MailAddress(to));

                    if (cc != "")
                    {
                        message.CC.Add(new MailAddress(cc));
                    }

                    if (bcc != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }

                    string subjct = ds_mailTemplate.Tables[0].Rows[1]["EMAIL_SUBJECT"].ToString();
                    message.Subject = subjct;
                    message.Body = Confirmagentbody;
                    message.Attachments.Add(attachFile);
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    client.Credentials = info;
                    client.Host = smtphost;
                    client.Port = int.Parse(smtpport);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);


                    // EMAIL STORED IN DATABASE
                    if (count == 1)
                    {
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), invoice_no, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 1);
                    }
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), invoice_no, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);

                }

            }
        }

        protected void Send_invoice_to_agent(string invoice_no)
        {
            string fromemail = "";
            string to = "";
            string cc = "";
            string bcc = "";

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[6]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[2]["IS_ON"].ToString() != "False")
            {
                MailMessage message = new MailMessage();
                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                DataTable dt_supplier_id = new DataTable();

                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
                agent_comm.CommandType = CommandType.StoredProcedure;
                agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(Session["quoteid"].ToString());
                DataTable dt_agent = new DataTable();
                SqlDataReader agent_rdr = agent_comm.ExecuteReader();

                dt_agent.Load(agent_rdr);

                string clientname = dt_agent.Rows[0]["CLIENT_NAME"].ToString();
                string agentname = dt_agent.Rows[0]["CUST_REL_NAME"].ToString();
                string fromdate = dt_agent.Rows[0]["TOUR_FROM_DATE"].ToString();
                string todate = dt_agent.Rows[0]["TOUR_TO_DATE"].ToString();

                string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf");
                Attachment attachFile = new Attachment(filename);

                if (ds_mailTemplate.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        fromemail = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Supplier")
                    {
                        //  fromemail = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                    }

                    else
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        to = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        to = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                        //   to = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                    }

                    else
                    {
                        to = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        cc = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        //    cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                    }

                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        bcc = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        //   bcc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                    }

                    else
                    {
                        bcc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string body = "";
                //   body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to fitops@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";
                body = ds_mailTemplate.Tables[0].Rows[2]["EMAIL_CONTENT"].ToString();

                body = body.Replace("&lt;%AGENTNAME%&gt;", agentname);
                body = body.Replace("&lt;%CLIENTNAME%&gt;", clientname);
                body = body.Replace("&lt;%PAYMENTMODE%&gt;", drpPayment.Text);

                message.From = new MailAddress(fromemail);
                if (cc == "")
                {
                }
                else
                {
                    message.CC.Add(new MailAddress(cc));
                }

                if (bcc != "")
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }
                message.To.Add(new MailAddress(to.ToString()));

                string subjct = ds_mailTemplate.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();

                subjct = subjct.Replace("<%CLIENTNAME%>", clientname);
                subjct = subjct.Replace("<%FROMDATE%>", fromdate);
                subjct = subjct.Replace("<%TODATE%>", todate);

                string filename1 = "~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf";
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), invoice_no, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 1);
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), invoice_no, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);
               

                message.Subject = subjct;

                message.Attachments.Add(attachFile);

                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = int.Parse(smtpport);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
            }
        }

        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand comm = new SqlCommand("FETCH_AGENT_QUOTATION_DETAILS", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = Convert.ToInt32(Session["quoteid"].ToString());

            SqlDataReader rdr = comm.ExecuteReader();
            dt.Load(rdr);

            string AgentName = dt.Rows[0]["CUST_REL_NAME"].ToString();
            string Tourname = dt.Rows[0]["TOUR_SHORT_NAME"].ToString();
            if (lbltotalInvoiceAmount.Text == "")
            {
                lbltotalInvoiceAmount.Text = "0.00";
                string responseURL = objFITPaymentStoreProcedure.getItemNameAndCost(Tourname, lbltotalInvoiceAmount.Text);
                Response.Redirect(responseURL);
            }
            else
            {
                decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                string s = "3.5";
                decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                string  totalchargs = string.Format("{0:#.00}", charges + addcharges); 
                string responseURL = objFITPaymentStoreProcedure.getItemNameAndCost(Tourname, totalchargs);
                Response.Redirect(responseURL);
            }


        }

        

    }
}
