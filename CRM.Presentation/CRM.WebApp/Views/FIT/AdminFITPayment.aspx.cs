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
    public partial class AdminFITPayment : System.Web.UI.Page
    {
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();

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

        string addcartid;
        string addsuppcartid;
        string addcartsupp_email;
        byte[] renderedBytes5;

        string single_rooms;
        string double_rooms;
        string tripal_rooms;
        string room_type;
        string hotel_name1;
        string check_in_date;
        string check_out_date;
        string pessanger_name;

        string smtpemail = "kushal@flamingotravels.co.in";
        string smtppass = "dadashri";
        string smtphost = "smtpcorp.com";
        string fromemail = "fitops@travelzunlimited.com";



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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 241);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usrid = Session["adminuserid"].ToString();
            if (!IsPostBack)
            {
               
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
                
                UpdatePanel_Hotel_header.Update();
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

            // Credit Limit
            if (drpPayment.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
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
                  //  c1.Visible = true;
                    c1.Attributes.Add("style", "display");
                    c2.Attributes.Add("style", "display");
                    c3.Attributes.Add("style", "display");
                    c4.Attributes.Add("style", "display");
                    c5.Attributes.Add("style", "display");
                    c6.Attributes.Add("style", "display:none");
                    c9.Attributes.Add("style", "display:none");
                }
                else
                {
                    c4.Attributes.Add("style", "display");
                    c5.Attributes.Add("style", "display");
                    c6.Attributes.Add("style", "display:none");
                }
                btnpatnow.Visible = true;
                btnpay.Visible=false;
                ImageButton1.Visible = false;
                updatebutton.Update();
            }

                // Cash on arrrival
            else if (drpPayment.Text == DTMODE.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display");
                c6.Attributes.Add("style", "display:none");
                c9.Attributes.Add("style", "display:none");
                // UpdatePanel_Hotel_header.Visible = false;
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
                // authorisation.Attributes.Add("style", "display");
                btnpay.Visible = true;                
                btnpatnow.Visible = false;
                ImageButton1.Visible = false;
                updatebutton.Update();
            }
                // Crdit Card
            else if (drpPayment.Text == DTPAYPAL.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display:none");
                c6.Attributes.Add("style", "display:none");
                //authorisation.Attributes.Add("style", "display:none");
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
                    DataTable DT = objFITPaymentStoreProcedure.fetch_currency();
                    lblcurr.Text = DT.Rows[2]["CURRENCY_NAME"].ToString();
                }
                btnpatnow.Visible = false;
                btnpay.Visible = false;
                ImageButton1.Visible = true;
                updatebutton.Update();
            }
            UpdatePanel_Hotel_header.Update();
        }

        protected void btnpatnow_Click(object sender, EventArgs e)
        {
            DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
            DataSet dspayment = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_PAYMENT_MODE");
            DataSet dscurrency = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_ALL_CURRENCY_NAME");
           
            DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
            DataSet dspwd = objFITPaymentStoreProcedure.fetch_password(int.Parse(Session["rel_sr_no"].ToString()));
            bool crdit_limit_flag = true;
            if (drpPayment.Text == dspayment.Tables[0].Rows[0]["AutoSearchResult"].ToString())
            {
                if (decimal.Parse(lblcurrentusableamount.Text) < decimal.Parse(lbltotalInvoiceAmount.Text))
                {
                    crdit_limit_flag = false;
                    Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
                }
            }

            if (txtpassowrd.Text == dspwd.Tables[0].Rows[0]["PASSWORD"].ToString())
            {
                Master.DisplayMessage("Password is incorrect.", "successMessage", 3000);
            }
            else if (crdit_limit_flag==false)
            {
                Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
            }
            else
            {
                DataTable dt = null;
                dt = objFITPaymentStoreProcedure.insert_sales_invoice_header(int.Parse(Session["quoteid"].ToString()), int.Parse(usrid), int.Parse(Session["rel_sr_no"].ToString()), Session["fromdate"].ToString(), Session["todate"].ToString(), Session["nights"].ToString(), decimal.Parse(lbltotalInvoiceAmount.Text), decimal.Parse("0"), decimal.Parse(lbltotalInvoiceAmount.Text), false, int.Parse(Session["noofadult"].ToString()), 0, int.Parse(Session["noofcwb"].ToString()), int.Parse(Session["noofcnb"].ToString()), int.Parse(Session["noofinfant"].ToString()), "Reconfirmed", drpPayment.Text, txtBook_ref_no.Text, int.Parse(Session["CompanyId"].ToString()), dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString());

                objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount.Text));
                invoice_id = Convert.ToInt32(dt.Rows[0]["INVOICE_ID"].ToString());

                objFITPaymentStoreProcedure.INSERT_ORDER_STATUS("INSERT_UPDATE_ORDER_STAUS_FOR_BOOKING", int.Parse(Session["quoteid"].ToString()), "Reconfirmed");

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
                //  string voucher_amount = 

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
                 //   FETCH_INVOICE_NO_DETAILS_FROM_SUPPLIER
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

                  
                    string txtno_of_nights1="0";
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
                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, hotel_name, dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds1.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount, "", total_amount, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(ds1.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), txtperiod_stay_from, txtperiod_stay_to, int.Parse(txtno_of_nights1), int.Parse(txtsingle_room), int.Parse(txtdouble_room), int.Parse(txttriple_room), ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString(), 0, "", "", "", "", txt_amount, int.Parse(Session["CompanyId"].ToString()), 1);

                    objFITPaymentStoreProcedure.insert_purchase_entry(0, hotel_name, dt.Rows[0]["INVOICE_NO"].ToString(), int.Parse(Session["adminuserid"].ToString()), ds1.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString(), txt_amount, "", total_amount, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["adminuserid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(ds1.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(ds1.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), ds1.Tables[0].Rows[0]["HOTEL_PERIOD_STAY_FROM"].ToString(), ds1.Tables[0].Rows[0]["HOTEL_PERIOD_STAY_TO"].ToString(), int.Parse(txtno_of_nights1), int.Parse(txtsingle_room), int.Parse(txtdouble_room), int.Parse(txttriple_room), ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString(), 0, "", "", "", "", txt_amount, int.Parse(Session["CompanyId"].ToString()), 2);


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
                    //*******************************************FETCH_SS_TP_FOR_PURCHASE (PREVIOUS SP USED DATA SET ds_tp_data)
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
                        //***************************************************** FETCH_SIGHT_SEEING_FOR_PURCHASE (PREVIOUS SP USED DATA SET ds_ss_data)
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
                /*******************************************************************ADDITIONAL SERVICES**************************************************************************/
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
                // quote_id = Page.Request.QueryString["QuoteId"].ToString();
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
               // Response.Clear();
                //Response.Flush();
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
             //   Response.Clear();
           

                #region hotel voucher
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
                    if (supplierid != "")
                    {
                        DataTable dt_hotelEmails = new DataTable();

                        SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                        email.CommandType = CommandType.StoredProcedure;
                        email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                        SqlDataReader rdremail = email.ExecuteReader();
                        dt_hotelEmails.Load(rdremail);

                        hotelsupplierid = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                        supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


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
                           // Response.Clear();

                        }


                        using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/" + "HotelVoucher" + " " + invoice_id.ToString() + "-" + hotelsupplierid.ToString() + ".pdf")))
                        {
                            fs.Write(renderedBytes2, 0, (int)renderedBytes2.Length);
                        }
                      
                    }
                }//-------------------------- hotel vouchers Code Completed--------------------------------
                #endregion

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
                      //  Response.Clear();
                       
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
                     //   Response.Clear();
                        
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

                //SendMail(renderedBytes, renderedBytes1);
                rptViewer1.Visible = false;
                rptViewer1.Reset();
                
                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Itinerary/" + invoice_id.ToString() + "/Itinerary.pdf")))
                {
                    fs.Write(renderedBytes1, 0, (int)renderedBytes1.Length);
                }

                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + invoice_id.ToString() + "/Invoice.pdf")))
                {
                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                }

                /* End */
                DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();

                Master.DisplayMessage("Invoice Generate Successfully.", "successMessage", 3000);

                Response.Redirect("~/Views/Account/GenerateInvoice.aspx?TOURID=" + invoice_id + "&QUOTEID=" + Session["quoteid"].ToString());


            }

        }

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
                string totalchargs = string.Format("{0:#.00}", charges + addcharges); 
                string responseURL = objFITPaymentStoreProcedure.getItemNameAndCost(Tourname, totalchargs);
                Response.Redirect(responseURL);
            }


        }

        #region AgentMail
        protected void SendMail(byte[] _file1, byte[] _file2)
        {
            DataTable dtemail = objHotelStoreProcedure.fetch_backoffice_for_book(Session["updateid"].ToString());
            string a;
            if (dtemail.Rows.Count != 0)
            {
                if (dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString() == "1")
                {
                    //DataTable dt1 = objHotelStoreProcedure.update_quote_for_backoffice(int.Parse(Session["updateid"].ToString()), "2");
                    a = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
                }
                else
                {
                    //DataTable dt1 = objHotelStoreProcedure.update_quote_for_backoffice(int.Parse(Session["updateid"].ToString()), "1");
                    a = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
                }
            }
            else
            {
                DataTable dt1 = objHotelStoreProcedure.update_quote_for_backoffice(int.Parse(Session["updateid"].ToString()), "1");
                a = dt1.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
            }
            String cc = "";
            if (a == "1")
            {
                cc = "reservation@travelzunlimited.com";
            }
            else if (a == "2")
            {
                cc = "reservation1@travelzunlimited.com";
            }





            #region AgentEmail Variable
            string agentEmail = Session["usersname"].ToString();
            string fromdate;
            string todate;
            string clientname;
            string agentname;

            string EmailQoute_id = Session["quoteid"].ToString();
            string smtpemail = "kushal@flamingotravels.co.in";
            string smtppass = "dadashri";
            string smtphost = "smtpcorp.com";
            //string smtpport = "587";
            string fromemail = "fitops@travelzunlimited.com";
            string toemail1 = Session["email"].ToString();

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

            string addInvoiceid = invoice_id.ToString();

            DataTable addServicecartid = new DataTable();

            SqlCommand addServiceCommand = new SqlCommand("FETCH_ADDITIONAL_SERVICE_CART_ID", conn);
            addServiceCommand.CommandType = CommandType.StoredProcedure;
            addServiceCommand.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(addInvoiceid);
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

            #endregion


            string body = "";
            body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to fitops@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";

            try
            {

                //string toemail2 = "hardik@ambait.com";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(toemail1.ToString()));
                //message.To.Add(new MailAddress(toemail2.ToString()));
                message.Subject = "Booking for " + clientname + "Arrival date : " + fromdate + "Departure date : " + todate;
                message.Attachments.Add(new Attachment(new MemoryStream(_file1), "Invoice.pdf"));
                message.Attachments.Add(new Attachment(new MemoryStream(_file2), "Itinerary.pdf"));

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
                    string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/" + invoice_id.ToString() + "-" + hotelsupplierid.ToString() + "HotelVoucher.pdf");
                    Attachment attachFile = new Attachment(filename);

                    message.Attachments.Add(attachFile);
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


                    string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/SightSeeingVoucher/" + invoice_id.ToString() + "/" + invoice_id.ToString() + "-" + stseeingsupplierid.ToString() + "SightSeeingVoucher.pdf");
                    Attachment attachFile = new Attachment(filename);

                    message.Attachments.Add(attachFile);
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


                    string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/TransferVoucher/" + invoice_id.ToString() + "/" + invoice_id.ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + "TransferVoucher.pdf");
                    Attachment attachFile = new Attachment(filename);

                    message.Attachments.Add(attachFile);
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


                    string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + invoice_id.ToString() + "/" + "Additional Service Voucher" + " " + invoice_id.ToString() + "-" + addsuppcartid.ToString() + ".pdf");
                    Attachment attachFile = new Attachment(filename);

                    message.Attachments.Add(attachFile);
                }

                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
            }
            catch (Exception ex)
            {
                //lblResult.Text = "Could not serve your request at this time. Please contact webmaster";
                //lblResult.ForeColor = System.Drawing.Color.Red;
            }
        }
        #endregion
    }
}