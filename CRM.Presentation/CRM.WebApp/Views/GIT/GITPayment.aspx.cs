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
using CRM.DataAccess.GIT;

namespace CRM.WebApp.Views.GIT
{
    public partial class GITPayment : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        CRM.DataAccess.GIT.GITPaymentDA objgitpayment = new CRM.DataAccess.GIT.GITPaymentDA();
        InsertGitDetails objInsertGitDetails = new InsertGitDetails();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        BookSp objBookSp = new BookSp();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        #region VARIABLE
        int ipr = 0;
        string tourId;
        string Quoteid;
        decimal SINGLE_ROOM_RATE;
        decimal DOUBLE_ROOM_RATE;
        decimal TRIPLE_ROOM_RATE;
        decimal CWB_RATE;
        decimal CNB_RATE;
        decimal TOTAL_RATE;


        decimal TOTAL_INVOICE_AMOUNT;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();
                    DataSet ds = objgitpayment.fetch_paymentmode("FETCH_PAYMENT_MODE_GIT");
                    binddropdownlist(drpPayment, ds);

                    //Calculate_Hotel_TotalRate();
                    DataSet dsInvoice = objgitpayment.FetchSalesInvoiceHeader(Request["TOURID"].ToString());
                    //InsertSalesInvoice(dsInvoice);
                    lblTotalInvoice.Text = string.Format("{0:#.00}", Calculate_Invoice_Amount());

                }
            }
            else
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();
                }
            }
        }

        #endregion

        #region BindComboBox Event

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
            DataTable DTORDER = objgitpayment.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
            DataTable DTMODE = objgitpayment.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
            DataTable DTPAYPAL = objgitpayment.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "2");

            if (drpPayment.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objgitpayment.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

                Session["CUSTID"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
                lblcreditlimitAmount.Text = ds.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                lblcurrentusableamount.Text = ds.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();

                if (lblcurrentusableamount.Text == "")
                {
                    lblcurrentusableamount.Text = "0";
                }
                lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();


                Tr_Total_invoice.Attributes.Add("style", "display");


                lblDiscount.Text = "0";


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
                DataSet ds = objgitpayment.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display:none");
                c6.Attributes.Add("style", "display:none");
                c9.Attributes.Add("style", "display:none");

                Tr_Total_invoice.Attributes.Add("style", "display");

                lblDiscount.Text = "0";


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
                DataSet ds = objgitpayment.fetch_credit_limit(int.Parse(Session["empid"].ToString()));

                c1.Attributes.Add("style", "display:none");
                c2.Attributes.Add("style", "display:none");
                c3.Attributes.Add("style", "display:none");
                c4.Attributes.Add("style", "display");
                c5.Attributes.Add("style", "display:none");
                c6.Attributes.Add("style", "display:none");
                authorisation.Attributes.Add("style", "display:none");
                c9.Attributes.Add("style", "display");
                DataSet ds1 = objgitpayment.fetch_total_invoice(int.Parse(Session["quoteid"].ToString()));

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

                lbltotalInvoiceAmount.Text = (decimal.Parse(lblTotalInvoice.Text) - decimal.Parse(lblDiscount.Text) + decimal.Parse(lblBnakCharges.Text)).ToString();

                if (lbltotalInvoiceAmount.Text == "")
                {
                    lbltotalInvoiceAmount.Text = "0.00";
                    decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                    string s = "3.5";
                    decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                    lbladditional.Text = addcharges.ToString();
                    DataTable DT = objgitpayment.fetch_currency();
                    lblcurr.Text = DT.Rows[2]["CURRENCY_NAME"].ToString();
                }
                else
                {
                    decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                    string s = "3.5";
                    decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                    lbladditional.Text = addcharges.ToString();
                    DataTable DT = objgitpayment.fetch_currency();
                    lblcurr.Text = DT.Rows[2]["CURRENCY_NAME"].ToString();
                }
                btnsend.Visible = false;
                btnpatnow.Visible = false;
                ImageButton1.Visible = true;
                updatebutton.Update();
            }
            UpdatePanel_Hotel_header.Update();

            //btnpatnow.Visible = true;
            //updatebutton.Update();

        }

        #endregion

        #region Total Rate of Hotel Conference GalaDinner Meals SightSeeing Boat Coach And Guide

        protected string Calculate_Hotel_TotalRate()
        {
            decimal single_rate = 0;
            decimal double_rate = 0;
            decimal triple_rate = 0;
            decimal cwb_rate = 0;
            decimal cnb_rate = 0;
            int no_of_single_room = 0;
            int no_of_Double_room = 0;
            int no_of_Triple_room = 0;
            int no_of_CWB_room = 0;
            int no_of_CNB_room = 0;
            int no_of_night = 0;
            string HOTEL;
            string ROOM_TYPE;

            DataSet dsrate = objgitpayment.Fetch_Hotel_Rate("GET_HOTELS_FOR_RECONFIRM", int.Parse(Request.QueryString["TOURID"].ToString()));
            if (dsrate != null)
            {
                for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
                {
                    HOTEL = dsrate.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                    ROOM_TYPE = dsrate.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();
                    if (dsrate.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString() != "")
                    {
                        single_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString() != "")
                    {
                        double_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString() != "")
                    {
                        triple_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString() != "")
                    {
                        cwb_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString() != "")
                    {
                        cnb_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString() != "")
                    {
                        no_of_single_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString() != "")
                    {
                        no_of_Double_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString() != "")
                    {
                        no_of_Triple_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_CWB"].ToString() != "")
                    {
                        no_of_CWB_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_CWB"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_CNB"].ToString() != "")
                    {
                        no_of_CNB_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_CNB"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString() != "")
                    {
                        no_of_night = int.Parse(dsrate.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString());
                    }

                    SINGLE_ROOM_RATE = (no_of_single_room) * (single_rate) * (no_of_night);
                    DOUBLE_ROOM_RATE = (no_of_Double_room) * (double_rate) * (2) * (no_of_night);
                    TRIPLE_ROOM_RATE = (no_of_Triple_room) * (triple_rate) * (3) * (no_of_night);
                    CWB_RATE = (no_of_CWB_room) * (cwb_rate) * (no_of_night);
                    CNB_RATE = (no_of_CNB_room) * (cnb_rate) * (no_of_night);

                    TOTAL_RATE += (SINGLE_ROOM_RATE) + (DOUBLE_ROOM_RATE) + (TRIPLE_ROOM_RATE) + (CWB_RATE) + (CNB_RATE);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();

        }

        protected string Calculate_Conference_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Conference_Rate("GET_CONFERENCE_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString() != "")
                    {
                        NO_OF_ADULT = int.Parse(ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    {
                        NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    }
                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);


                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();


        }

        protected string Calculate_Gala_Dinner_TotalRate()
        {

            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Gala_Dinner_Rate("GET_GALA_DINNER_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString() != "")
                    {
                        NO_OF_ADULT = int.Parse(ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    {
                        NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    }

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Restaurent_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Restaurant_Rate("GET_RESTURANTS_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString() != "")
                    {
                        NO_OF_ADULT = int.Parse(ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    {
                        NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    }

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Sight_Seeing_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_SIGHT_SEEING_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString() != "")
                    {
                        NO_OF_ADULT = int.Parse(ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    {
                        NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    }
                    if (TOTAL_RATE.ToString() == "")
                    {
                        TOTAL_RATE = 0;
                    }
                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);

                }
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Transfer_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_TRANSFER_RATE_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString() != "")
                    {
                        NO_OF_ADULT = int.Parse(ds.Tables[0].Rows[i]["NO_OF_ADULT"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    {
                        NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    }
                    if (TOTAL_RATE.ToString() == "")
                    {
                        TOTAL_RATE = 0;
                    }
                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);

                }
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Guide_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            //int NO_OF_ADULT = 0;
            //int NO_OF_CHILD = 0;
            int NO_OF_GUIDE = 0;

            DataSet ds = objgitpayment.Fetch_Guide_Rate("GET_GUIDE_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_GUIDE"].ToString() != "")
                    {
                        NO_OF_GUIDE = int.Parse(ds.Tables[0].Rows[i]["NO_OF_GUIDE"].ToString());
                    }
                    //if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    //{
                    //    NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    //}

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_GUIDE) + (CHILD_RATE_PER_PERSON * NO_OF_GUIDE);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Boat_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            //int NO_OF_ADULT = 0;
            //int NO_OF_CHILD = 0;
            int NO_OF_BOATS = 0;

            DataSet ds = objgitpayment.Fetch_Boat_Rate("GET_BOAT_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["NO_OF_BOATS"].ToString() != "")
                    {
                        NO_OF_BOATS = int.Parse(ds.Tables[0].Rows[i]["NO_OF_BOATS"].ToString());
                    }
                    //if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    //{
                    //    NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    //}

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_BOATS) + (CHILD_RATE_PER_PERSON * NO_OF_BOATS);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();
        }

        protected string Calculate_Coach_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            Decimal COACH_RATE = 0;
            //int NO_OF_ADULT = 0;
            //int NO_OF_CHILD = 0;

            DataSet ds = objgitpayment.Fetch_Coach_Rate("GET_COACH_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["COACH_RATE"].ToString() != "")
                    {
                        COACH_RATE = Convert.ToDecimal(ds.Tables[0].Rows[i]["COACH_RATE"].ToString());
                    }
                    //if (ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString() != "")
                    //{
                    //    NO_OF_CHILD = int.Parse(ds.Tables[0].Rows[i]["NO_OF_CHILD"].ToString());
                    //}

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * COACH_RATE) + (CHILD_RATE_PER_PERSON * COACH_RATE);

                }
            }
            if (TOTAL_RATE.ToString() == "")
            {
                TOTAL_RATE = 0;
            }
            return TOTAL_RATE.ToString();
        }

        #endregion

        #region Main Calculation Of Invoice

        protected decimal Calculate_Invoice_Amount()
        {
            DataSet dsInvoice = objgitpayment.FetchSalesInvoiceHeader(Request["TOURID"].ToString());
            decimal margin_amt = 0;
            decimal Exchange_rate = 0;
            if (dsInvoice != null)
            {
                if (dsInvoice.Tables[0].Rows[0]["MARGIN_AMOUNT_THB"].ToString() != "")
                {
                    margin_amt = Convert.ToDecimal(dsInvoice.Tables[0].Rows[0]["MARGIN_AMOUNT_THB"].ToString());
                    Exchange_rate = Convert.ToDecimal(dsInvoice.Tables[0].Rows[0]["EXCHANGE_RATE"].ToString());
                }
            }
            decimal single_rate = 0;
            decimal double_rate = 0;
            decimal triple_rate = 0;
            decimal cwb_rate = 0;
            decimal cnb_rate = 0;
            int no_of_single_room = 0;
            int no_of_Double_room = 0;
            int no_of_Triple_room = 0;
            int no_of_CWB_room = 0;
            int no_of_CNB_room = 0;
            int no_of_night = 0;
            int avg_single = 0;
            int avg_double = 0;
            int avg_triple = 0;
            string HOTEL;
            string ROOM_TYPE;
            int single = 0;
            int doubl = 0;
            int triple = 0;

            decimal ADULT_RATE_PER_PERSON_CON = 0;
            decimal CHILD_RATE_PER_PERSON_CON = 0;

            decimal ADULT_RATE_PER_PERSON_REST = 0;
            decimal CHILD_RATE_PER_PERSON_REST = 0;

            decimal ADULT_RATE_PER_PERSON_GALA = 0;
            decimal CHILD_RATE_PER_PERSON_GALA = 0;

            decimal ADULT_RATE_PER_PERSON_SIGHT = 0;
            decimal CHILD_RATE_PER_PERSON_SIGHT = 0;

            decimal ADULT_RATE_PER_PERSON_TRANSFER = 0;
            decimal CHILD_RATE_PER_PERSON_TRANSFER = 0;

            decimal ADULT_RATE_PER_PERSON_GUIDE = 0;
            decimal CHILD_RATE_PER_PERSON_GUIDE = 0;

            decimal ADULT_RATE_PER_PERSON_BOAT = 0;
            decimal CHILD_RATE_PER_PERSON_BOAT = 0;

            decimal ADULT_RATE_PER_PERSON_COACH = 0;
            decimal CHILD_RATE_PER_PERSON_COACH = 0;

            /*--------------------------------- Hotel Calculation ---------------------------------------------------*/

            DataSet dsrate = objgitpayment.Fetch_Hotel_Rate("GET_HOTELS_FOR_RECONFIRM", int.Parse(Request.QueryString["TOURID"].ToString()));
            if (dsrate != null)
            {
                for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
                {
                    HOTEL = dsrate.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                    ROOM_TYPE = dsrate.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();
                    if (dsrate.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString() != "")
                    {
                        single_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString() != "")
                    {
                        double_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString() != "")
                    {
                        triple_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString() != "")
                    {
                        cwb_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString() != "")
                    {
                        cnb_rate = Convert.ToDecimal(dsrate.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString() != "")
                    {
                        no_of_single_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                        avg_single += Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString() != "")
                    {
                        no_of_Double_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString());
                        avg_double += Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString() != "")
                    {
                        no_of_Triple_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                        avg_triple += Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_CWB"].ToString() != "")
                    {
                        no_of_CWB_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_CWB"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[j]["NO_OF_CNB"].ToString() != "")
                    {
                        no_of_CNB_room = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_CNB"].ToString());
                    }
                    if (dsrate.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString() != "")
                    {
                        no_of_night = int.Parse(dsrate.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString());
                    }

                    SINGLE_ROOM_RATE += (no_of_single_room) * (single_rate) * (no_of_night);
                    DOUBLE_ROOM_RATE += (no_of_Double_room) * (double_rate) * (2) * (no_of_night);
                    TRIPLE_ROOM_RATE += (no_of_Triple_room) * (triple_rate) * (3) * (no_of_night);
                    CWB_RATE += (no_of_CWB_room) * (cwb_rate) * (no_of_night);
                    CNB_RATE += (no_of_CNB_room) * (cnb_rate) * (no_of_night);
                }
            }

            //Decimal avg_single_room = 0;
            //avg_single_room = Convert.ToDecimal(decimal.Parse(avg_single.ToString()) / decimal.Parse(dsrate.Tables[0].Rows.Count.ToString()));
            //Decimal avg_double_room = 0;
            //avg_double_room = Convert.ToDecimal(decimal.Parse(avg_double.ToString()) / decimal.Parse(dsrate.Tables[0].Rows.Count.ToString()));
            //Decimal avg_triple_room = 0;
            //avg_triple_room = Convert.ToDecimal(decimal.Parse(avg_triple.ToString()) / decimal.Parse(dsrate.Tables[0].Rows.Count.ToString()));
            for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
            {
                if (dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString() != "")
                {
                    single = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                    break;
                }
            }
            for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
            {
                if (dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString() != "")
                {
                    doubl = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString()); 
                    break;
                }
            }
            for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
            {
                if (dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString() != "")
                {
                    triple = Convert.ToInt32(dsrate.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                    break;
                }
            }

            /*--------------------------------------- Transfer Package ------------------------------------------- */
            DataSet ds = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_TRANSFER_RATE_FOR_RECONFIRM", int.Parse(tourId));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_TRANSFER = Convert.ToDecimal(ds.Tables[0].Rows[i]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_TRANSFER = Convert.ToDecimal(ds.Tables[0].Rows[i]["CHILD_RATE_PER_PERSON"].ToString());
                    }

                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_TRANSFER);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_TRANSFER) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_TRANSFER) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_TRANSFER);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_TRANSFER);
                }
            }

            /*----------------------------------------- Sight Seeing ---------------------------------------------------*/
            DataSet dssight = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_SIGHT_SEEING_FOR_RECONFIRM", int.Parse(tourId));
            if (dssight != null)
            {
                for (int sight = 0; sight < dssight.Tables[0].Rows.Count; sight++)
                {
                    if (dssight.Tables[0].Rows[sight]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_SIGHT = Convert.ToDecimal(dssight.Tables[0].Rows[sight]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dssight.Tables[0].Rows[sight]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_SIGHT = Convert.ToDecimal(dssight.Tables[0].Rows[sight]["CHILD_RATE_PER_PERSON"].ToString());
                    }

                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_SIGHT);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_SIGHT) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_SIGHT) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_SIGHT);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_SIGHT);
                }
            }
            //---------------------------------------- GalaDinner -------------------------------------------------
            DataSet dsGalaDinner = objgitpayment.Fetch_Gala_Dinner_Rate("GET_GALA_DINNER_FOR_RECONFIRM", int.Parse(tourId));
            if (dsGalaDinner != null)
            {

                for (int Gala = 0; Gala < dsGalaDinner.Tables[0].Rows.Count; Gala++)
                {

                    if (dsGalaDinner.Tables[0].Rows[Gala]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_GALA = Convert.ToDecimal(dsGalaDinner.Tables[0].Rows[Gala]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsGalaDinner.Tables[0].Rows[Gala]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_GALA = Convert.ToDecimal(dsGalaDinner.Tables[0].Rows[Gala]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_GALA);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_GALA) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_GALA) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_GALA);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_GALA);
                }
            }

            //----------------------------------------- Restaurent ----------------------------------------
            DataSet dsRestaurent = objgitpayment.Fetch_Restaurant_Rate("GET_RESTURANTS_FOR_RECONFIRM", int.Parse(tourId));
            if (dsRestaurent != null)
            {
                for (int rest = 0; rest < dsRestaurent.Tables[0].Rows.Count; rest++)
                {

                    if (dsRestaurent.Tables[0].Rows[rest]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_REST = Convert.ToDecimal(dsRestaurent.Tables[0].Rows[rest]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsRestaurent.Tables[0].Rows[rest]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_REST = Convert.ToDecimal(dsRestaurent.Tables[0].Rows[rest]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_REST);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_REST) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_REST) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_REST);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_REST);
                }
            }
            //---------------------------------- Conferrence ---------------------------------------------------------
            DataSet dsConference = objgitpayment.Fetch_Conference_Rate("GET_CONFERENCE_FOR_RECONFIRM", int.Parse(tourId));
            if (dsConference != null)
            {
                for (int CON = 0; CON < dsConference.Tables[0].Rows.Count; CON++)
                {

                    if (dsConference.Tables[0].Rows[CON]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_CON = Convert.ToDecimal(dsConference.Tables[0].Rows[CON]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsConference.Tables[0].Rows[CON]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_CON = Convert.ToDecimal(dsConference.Tables[0].Rows[CON]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_CON);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_CON) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_CON) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_CON);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_CON);
                }
            }

            //----------------------------------------- Guide ----------------------------------------
            DataSet dsGuide = objgitpayment.Fetch_Guide_Rate("GET_GUIDE_FOR_RECONFIRM", int.Parse(tourId));
            if (dsGuide != null)
            {
                for (int Guide = 0; Guide < dsGuide.Tables[0].Rows.Count; Guide++)
                {

                    if (dsGuide.Tables[0].Rows[Guide]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_GUIDE = Convert.ToDecimal(dsGuide.Tables[0].Rows[Guide]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsGuide.Tables[0].Rows[Guide]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_GUIDE = Convert.ToDecimal(dsGuide.Tables[0].Rows[Guide]["CHILD_RATE_PER_PERSON"].ToString());
                    }

                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_GUIDE);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_GUIDE) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_GUIDE) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_GUIDE);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_GUIDE);
                }
            }

            //----------------------------------------- Boat ----------------------------------------
            DataSet dsBoat = objgitpayment.Fetch_Boat_Rate("GET_BOAT_FOR_RECONFIRM", int.Parse(tourId));
            if (dsBoat != null)
            {
                for (int Boat = 0; Boat < dsBoat.Tables[0].Rows.Count; Boat++)
                {

                    if (dsBoat.Tables[0].Rows[Boat]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_BOAT = Convert.ToDecimal(dsBoat.Tables[0].Rows[Boat]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsBoat.Tables[0].Rows[Boat]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_BOAT = Convert.ToDecimal(dsBoat.Tables[0].Rows[Boat]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_BOAT);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_BOAT) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_BOAT) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_BOAT);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_BOAT);
                }
            }

            //----------------------------------------- Coach ----------------------------------------
            DataSet dsCoach = objgitpayment.Fetch_Coach_Rate("GET_COACH_FOR_RECONFIRM", int.Parse(tourId));
            if (dsCoach != null)
            {
                for (int Coach = 0; Coach < dsCoach.Tables[0].Rows.Count; Coach++)
                {
                    if (dsCoach.Tables[0].Rows[Coach]["ADULT_RATE_PER_PERSON"].ToString() != "")
                    {
                        ADULT_RATE_PER_PERSON_COACH = Convert.ToDecimal(dsCoach.Tables[0].Rows[Coach]["ADULT_RATE_PER_PERSON"].ToString());
                    }
                    if (dsCoach.Tables[0].Rows[Coach]["CHILD_RATE_PER_PERSON"].ToString() != "")
                    {
                        CHILD_RATE_PER_PERSON_COACH = Convert.ToDecimal(dsCoach.Tables[0].Rows[Coach]["CHILD_RATE_PER_PERSON"].ToString());
                    }
                    SINGLE_ROOM_RATE += (single) * (ADULT_RATE_PER_PERSON_COACH);
                    DOUBLE_ROOM_RATE += (doubl) * (ADULT_RATE_PER_PERSON_COACH) * (2);
                    TRIPLE_ROOM_RATE += (triple) * (ADULT_RATE_PER_PERSON_COACH) * (3);
                    CWB_RATE += (no_of_CWB_room) * (CHILD_RATE_PER_PERSON_COACH);
                    CNB_RATE += (no_of_CNB_room) * (CHILD_RATE_PER_PERSON_COACH);
                }
            }
            decimal single_margin_amt = (single * margin_amt);
            decimal double_margin_amt = ((doubl * 2) * margin_amt);
            decimal triple_margin_amt = ((triple * 3) * margin_amt);
            decimal cwb_margin_amt = (no_of_CWB_room * margin_amt);
            decimal cnb_margin_amt = (no_of_CNB_room * margin_amt);

            SINGLE_ROOM_RATE = (SINGLE_ROOM_RATE) + single_margin_amt;
            DOUBLE_ROOM_RATE = (DOUBLE_ROOM_RATE) + double_margin_amt;
            TRIPLE_ROOM_RATE = (TRIPLE_ROOM_RATE) + triple_margin_amt;
            CWB_RATE = (CWB_RATE) + cwb_margin_amt;
            CNB_RATE = (CNB_RATE) + cnb_margin_amt;

            decimal TOTAL_RATE_ex = (SINGLE_ROOM_RATE) + (DOUBLE_ROOM_RATE) + (TRIPLE_ROOM_RATE) + (CWB_RATE) + (CNB_RATE);
            TOTAL_RATE = TOTAL_RATE_ex / Exchange_rate;
            DataSet dsAdditional = objgitpayment.Fetch_Additional_Rate("FETCH_GIT_ADDITIONAL_SERVICE_CART", int.Parse(tourId));
            if (dsAdditional != null)
            {
                
                if (dsAdditional.Tables[0].Rows.Count != 0)
                {
                    for (int add = 0; add < dsAdditional.Tables[0].Rows.Count; add++)
                    {
                        if (dsAdditional.Tables[0].Rows[0]["SALES_INVOICE_AMOUNT"].ToString() != "")
                        {
                            TOTAL_RATE += Convert.ToDecimal(dsAdditional.Tables[0].Rows[0]["SALES_INVOICE_AMOUNT"].ToString());
                        }
                    }
                }
            }



            ViewState["single_share_rate"] = SINGLE_ROOM_RATE / Exchange_rate;
            ViewState["double_share_rate"] = DOUBLE_ROOM_RATE / Exchange_rate;
            ViewState["triple_share_rate"] = TRIPLE_ROOM_RATE / Exchange_rate;
            ViewState["cwb_share_rate"] = CWB_RATE / Exchange_rate;
            ViewState["cnb_share_rate"] = CNB_RATE / Exchange_rate;
            ViewState["Total_rate"] = TOTAL_RATE;
            ViewState["single_room_per"] = single;
            ViewState["double_room_per"] = (doubl * 2).ToString();
            ViewState["triple_room_per"] = (triple * 3).ToString();
            ViewState["cwb_per"] = no_of_CWB_room;
            ViewState["cnb_per"] = no_of_CNB_room;
            return TOTAL_RATE;
        }

        #endregion

        #region Calculate Total Invoice Amount for city Wise

        protected Decimal InsertSalesInvoice()
        {

            Decimal hotel = Convert.ToDecimal(Calculate_Hotel_TotalRate());
            Decimal conference = Convert.ToDecimal(Calculate_Conference_TotalRate());
            Decimal gala_dinner = Convert.ToDecimal(Calculate_Gala_Dinner_TotalRate());
            Decimal Restaurent = Convert.ToDecimal(Calculate_Restaurent_TotalRate());
            Decimal sight = Convert.ToDecimal(Calculate_Sight_Seeing_TotalRate());
            Decimal transfer = Convert.ToDecimal(Calculate_Transfer_TotalRate());
            Decimal guide = Convert.ToDecimal(Calculate_Guide_TotalRate());
            Decimal Boat = Convert.ToDecimal(Calculate_Boat_TotalRate());
            Decimal coach = Convert.ToDecimal(Calculate_Coach_TotalRate());
            Decimal Total = hotel + conference + gala_dinner + Restaurent + sight + transfer + guide + Boat + coach;
            return Total;
        }

        #endregion

        #region Pay Now Click

        protected void btnpatnowUnlimited_Click(object sender, EventArgs e)
        {
            //------------------- ALL DATASET ------------------------------------------
            DataSet dscurrency = objgitpayment.fetch_paymentmode("FETCH_ALL_CURRENCY_NAME");

            DataSet DS = objgitpayment.FetchSalesInvoiceHeader(Request["TOURID"].ToString());
            TOTAL_INVOICE_AMOUNT = InsertSalesInvoice();
            int NO_OF_ADULTS = 0;
            int NO_OF_CWB = 0;
            int NO_OF_CNB = 0;
            int NO_OF_INFANT = 0;
            int NO_OF_NIGHTS = 0;
            if (DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString() != "")
            {
                NO_OF_ADULTS = int.Parse(DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString());
            }
            if (DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString() != "")
            {
                NO_OF_CWB = int.Parse(DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString());
            }
            if (DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString() != "")
            {
                NO_OF_CNB = int.Parse(DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString());
            }
            if (DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString() != "")
            {
                NO_OF_INFANT = int.Parse(DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString());
            }
            if (DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString() != "")
            {
                NO_OF_NIGHTS = int.Parse(DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString());
            }

            //---- FETCH ORDER STATUS NAME
            DataTable dtOrder = objgitpayment.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "5");

            //----- INSERT INTO SALES INVOICE HEADER
            //DataSet dt = objgitpayment.insert_sales_invoice_header(int.Parse(DS.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["empid"].ToString()), "", "", NO_OF_NIGHTS, TOTAL_INVOICE_AMOUNT, 0, TOTAL_INVOICE_AMOUNT, true, NO_OF_ADULTS, 0, NO_OF_CWB, NO_OF_CNB, NO_OF_INFANT, dtOrder.Rows[0]["ORDER_STATUS_NAME"].ToString(), drpPayment.Text, "", 1, dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString());

            //-------------------- CALCULATION FOR SINGLE,DOUBLE,TRIPLE SHARE START ------------------------------------
            int no_of_single_room = 0;
            int no_of_Double_room = 0;
            int no_of_Triple_room = 0;
            int no_of_CWB_room = 0;
            int no_of_CNB_room = 0;
            int no_of_nights = 0;

            string HOTEL;
            string ROOM_TYPE;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            //----- pex wise rate ----
            decimal no_of_single_pax_rate = 0;
            decimal no_of_double_pax_rate = 0;
            decimal no_of_triple_pax_rate = 0;
            decimal no_of_cwb_rate = 0;
            decimal no_of_cnb_rate = 0;
            //----------end---------

            decimal single_room_rate = 0;
            decimal double_room_rate = 0;
            decimal triple_room_rate = 0;
            decimal cwb_room_rate = 0;
            decimal cnb_room_rate = 0;

            decimal ADULT_RATE_PER_PERSON_CON = 0;
            decimal CHILD_RATE_PER_PERSON_CON = 0;

            decimal ADULT_RATE_PER_PERSON_REST = 0;
            decimal CHILD_RATE_PER_PERSON_REST = 0;

            decimal ADULT_RATE_PER_PERSON_GALA = 0;
            decimal CHILD_RATE_PER_PERSON_GALA = 0;

            decimal ADULT_RATE_PER_PERSON_SIGHT = 0;
            decimal CHILD_RATE_PER_PERSON_SIGHT = 0;

            decimal ADULT_RATE_PER_PERSON_TRANSFER = 0;
            decimal CHILD_RATE_PER_PERSON_TRANSFER = 0;

            decimal ADULT_RATE_PER_PERSON_GUIDE = 0;
            decimal CHILD_RATE_PER_PERSON_GUIDE = 0;

            decimal ADULT_RATE_PER_PERSON_BOAT = 0;
            decimal CHILD_RATE_PER_PERSON_BOAT = 0;

            decimal ADULT_RATE_PER_PERSON_COACH = 0;
            decimal CHILD_RATE_PER_PERSON_COACH = 0;


            DataSet dtCities = objgitpayment.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
            for (int i = 0; i < dtCities.Tables[0].Rows.Count; i++)
            {
                //----------------------------------------- Hotel ---------------------------------------------------------
                DataSet dsHotel = objgitpayment.Fetch_Hotel_Rate("GET_HOTELS_FOR_RECONFIRM", int.Parse(Request.QueryString["TOURID"].ToString()));
                if (dsHotel != null)
                {
                    for (int j = 0; j < dsHotel.Tables[0].Rows.Count; j++)
                    {
                        if (dsHotel.Tables[0].Rows[j]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            HOTEL = dsHotel.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            ROOM_TYPE = dsHotel.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();
                            if (dsHotel.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString() != "")
                            {
                                single_room_rate = Convert.ToDecimal(dsHotel.Tables[0].Rows[j]["SINGLE_ROOM_RATE"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString() != "")
                            {
                                double_room_rate = Convert.ToDecimal(dsHotel.Tables[0].Rows[j]["DOUBLE_ROOM_RATE"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString() != "")
                            {
                                triple_room_rate = Convert.ToDecimal(dsHotel.Tables[0].Rows[j]["TRIPLE_ROOM_RATE"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString() != "")
                            {
                                cwb_room_rate = Convert.ToDecimal(dsHotel.Tables[0].Rows[j]["EXTRA_CWB_COST"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString() != "")
                            {
                                cnb_room_rate = Convert.ToDecimal(dsHotel.Tables[0].Rows[j]["EXTRA_CNB_COST"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString() != "")
                            {
                                no_of_single_room = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString() != "")
                            {
                                no_of_Double_room = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString() != "")
                            {
                                no_of_Triple_room = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_CWB"].ToString() != "")
                            {
                                no_of_CWB_room = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_CWB"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_CNB"].ToString() != "")
                            {
                                no_of_CNB_room = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_CNB"].ToString());
                            }
                            if (dsHotel.Tables[0].Rows[j]["NO_OF_NIGHTS"].ToString() != "")
                            {
                                no_of_nights = Convert.ToInt32(dsHotel.Tables[0].Rows[j]["NO_OF_NIGHTS"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (single_room_rate) * (no_of_nights);
                            no_of_double_pax_rate += (no_of_Double_room) * (double_room_rate) * (2) * (no_of_nights);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (triple_room_rate) * (3) * (no_of_nights);
                            no_of_cwb_rate += (no_of_CWB_room) * (cwb_room_rate) * (no_of_nights);
                            no_of_cnb_rate += (no_of_CNB_room) * (cnb_room_rate) * (no_of_nights);

                        }
                    }
                }

                //---------------------------------- Conferrence ---------------------------------------------------------
                DataSet dsConference = objgitpayment.Fetch_Conference_Rate("GET_CONFERENCE_FOR_RECONFIRM", int.Parse(tourId));
                if (dsConference != null)
                {
                    for (int CON = 0; CON < dsConference.Tables[0].Rows.Count; CON++)
                    {
                        if (dsConference.Tables[0].Rows[CON]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsConference.Tables[0].Rows[CON]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_CON = Convert.ToDecimal(dsConference.Tables[0].Rows[CON]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsConference.Tables[0].Rows[CON]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_CON = Convert.ToDecimal(dsConference.Tables[0].Rows[CON]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            if (dsConference.Tables[0].Rows[CON]["NO_OF_ADULT"].ToString() != "")
                            {
                                NO_OF_ADULT = int.Parse(dsConference.Tables[0].Rows[CON]["NO_OF_ADULT"].ToString());
                            }
                            if (dsConference.Tables[0].Rows[CON]["NO_OF_CHILD"].ToString() != "")
                            {
                                NO_OF_CHILD = int.Parse(dsConference.Tables[0].Rows[CON]["NO_OF_CHILD"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_CON);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_CON) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_CON) * (3);
                        }
                    }
                }

                //---------------------------------------- GalaDinner -------------------------------------------------
                DataSet dsGalaDinner = objgitpayment.Fetch_Gala_Dinner_Rate("GET_GALA_DINNER_FOR_RECONFIRM", int.Parse(tourId));
                if (dsGalaDinner != null)
                {

                    for (int Gala = 0; Gala < dsGalaDinner.Tables[0].Rows.Count; Gala++)
                    {
                        if (dsGalaDinner.Tables[0].Rows[Gala]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsGalaDinner.Tables[0].Rows[Gala]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_GALA = Convert.ToDecimal(dsGalaDinner.Tables[0].Rows[Gala]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsGalaDinner.Tables[0].Rows[Gala]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_GALA = Convert.ToDecimal(dsGalaDinner.Tables[0].Rows[Gala]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            if (dsGalaDinner.Tables[0].Rows[Gala]["NO_OF_ADULT"].ToString() != "")
                            {
                                NO_OF_ADULT = int.Parse(dsGalaDinner.Tables[0].Rows[Gala]["NO_OF_ADULT"].ToString());
                            }
                            if (dsGalaDinner.Tables[0].Rows[Gala]["NO_OF_CHILD"].ToString() != "")
                            {
                                NO_OF_CHILD = int.Parse(dsGalaDinner.Tables[0].Rows[Gala]["NO_OF_CHILD"].ToString());
                            }

                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_GALA);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_GALA) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_GALA) * (3);
                        }
                    }
                }

                //----------------------------------------- Restaurent ----------------------------------------
                DataSet dsRestaurent = objgitpayment.Fetch_Restaurant_Rate("GET_RESTURANTS_FOR_RECONFIRM", int.Parse(tourId));
                if (dsRestaurent != null)
                {
                    for (int rest = 0; rest < dsRestaurent.Tables[0].Rows.Count; rest++)
                    {
                        if (dsRestaurent.Tables[0].Rows[rest]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsRestaurent.Tables[0].Rows[rest]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_REST = Convert.ToDecimal(dsRestaurent.Tables[0].Rows[rest]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsRestaurent.Tables[0].Rows[rest]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_REST = Convert.ToDecimal(dsRestaurent.Tables[0].Rows[rest]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            if (dsRestaurent.Tables[0].Rows[rest]["NO_OF_ADULT"].ToString() != "")
                            {
                                NO_OF_ADULT = int.Parse(dsRestaurent.Tables[0].Rows[rest]["NO_OF_ADULT"].ToString());
                            }
                            if (dsRestaurent.Tables[0].Rows[rest]["NO_OF_CHILD"].ToString() != "")
                            {
                                NO_OF_CHILD = int.Parse(dsRestaurent.Tables[0].Rows[rest]["NO_OF_CHILD"].ToString());
                            }

                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_REST);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_REST) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_REST) * (3);
                        }
                    }
                }

                //----------------------------------------- Sight Seeing ----------------------------------------
                DataSet dsSight = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_SIGHT_SEEING_FOR_RECONFIRM", int.Parse(tourId));
                if (dsSight != null)
                {
                    for (int Sight = 0; Sight < dsSight.Tables[0].Rows.Count; Sight++)
                    {
                        if (dsSight.Tables[0].Rows[Sight]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsSight.Tables[0].Rows[Sight]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_SIGHT = Convert.ToDecimal(dsSight.Tables[0].Rows[Sight]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsSight.Tables[0].Rows[Sight]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_SIGHT = Convert.ToDecimal(dsSight.Tables[0].Rows[Sight]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            if (dsSight.Tables[0].Rows[Sight]["NO_OF_ADULT"].ToString() != "")
                            {
                                NO_OF_ADULT = int.Parse(dsSight.Tables[0].Rows[Sight]["NO_OF_ADULT"].ToString());
                            }
                            if (dsSight.Tables[0].Rows[Sight]["NO_OF_CHILD"].ToString() != "")
                            {
                                NO_OF_CHILD = int.Parse(dsSight.Tables[0].Rows[Sight]["NO_OF_CHILD"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_SIGHT);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_SIGHT) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_SIGHT) * (3);
                        }

                    }
                }

                //----------------------------------------- Transfer ----------------------------------------
                DataSet dsTransfer = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_TRANSFER_RATE_FOR_RECONFIRM", int.Parse(tourId));
                if (dsTransfer != null)
                {
                    for (int Trans = 0; Trans < dsTransfer.Tables[0].Rows.Count; Trans++)
                    {
                        if (dsTransfer.Tables[0].Rows[Trans]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsTransfer.Tables[0].Rows[Trans]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_TRANSFER = Convert.ToDecimal(dsTransfer.Tables[0].Rows[Trans]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsTransfer.Tables[0].Rows[Trans]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_TRANSFER = Convert.ToDecimal(dsTransfer.Tables[0].Rows[Trans]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            if (dsTransfer.Tables[0].Rows[Trans]["NO_OF_ADULT"].ToString() != "")
                            {
                                NO_OF_ADULT = int.Parse(dsTransfer.Tables[0].Rows[Trans]["NO_OF_ADULT"].ToString());
                            }
                            if (dsTransfer.Tables[0].Rows[Trans]["NO_OF_CHILD"].ToString() != "")
                            {
                                NO_OF_CHILD = int.Parse(dsTransfer.Tables[0].Rows[Trans]["NO_OF_CHILD"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_TRANSFER);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_TRANSFER) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_TRANSFER) * (3);
                        }

                    }
                }

                //----------------------------------------- Guide ----------------------------------------
                DataSet dsGuide = objgitpayment.Fetch_Guide_Rate("GET_GUIDE_FOR_RECONFIRM", int.Parse(tourId));
                if (dsGuide != null)
                {
                    for (int Guide = 0; Guide < dsGuide.Tables[0].Rows.Count; Guide++)
                    {
                        if (dsGuide.Tables[0].Rows[Guide]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsGuide.Tables[0].Rows[Guide]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_GUIDE = Convert.ToDecimal(dsGuide.Tables[0].Rows[Guide]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsGuide.Tables[0].Rows[Guide]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_GUIDE = Convert.ToDecimal(dsGuide.Tables[0].Rows[Guide]["CHILD_RATE_PER_PERSON"].ToString());
                            }

                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_GUIDE);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_GUIDE) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_GUIDE) * (3);
                        }
                    }
                }

                //----------------------------------------- Boat ----------------------------------------
                DataSet dsBoat = objgitpayment.Fetch_Boat_Rate("GET_BOAT_FOR_RECONFIRM", int.Parse(tourId));
                if (dsBoat != null)
                {
                    for (int Boat = 0; Boat < dsBoat.Tables[0].Rows.Count; Boat++)
                    {
                        if (dsBoat.Tables[0].Rows[Boat]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsBoat.Tables[0].Rows[Boat]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_BOAT = Convert.ToDecimal(dsBoat.Tables[0].Rows[Boat]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsBoat.Tables[0].Rows[Boat]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_BOAT = Convert.ToDecimal(dsBoat.Tables[0].Rows[Boat]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_BOAT);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_BOAT) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_BOAT) * (3);
                        }

                    }
                }

                //----------------------------------------- Coach ----------------------------------------
                DataSet dsCoach = objgitpayment.Fetch_Coach_Rate("GET_COACH_FOR_RECONFIRM", int.Parse(tourId));
                if (dsCoach != null)
                {
                    for (int Coach = 0; Coach < dsCoach.Tables[0].Rows.Count; Coach++)
                    {
                        if (dsBoat.Tables[0].Rows[Coach]["CITY_NAME"].ToString() == dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString())
                        {
                            if (dsCoach.Tables[0].Rows[Coach]["ADULT_RATE_PER_PERSON"].ToString() != "")
                            {
                                ADULT_RATE_PER_PERSON_COACH = Convert.ToDecimal(dsCoach.Tables[0].Rows[Coach]["ADULT_RATE_PER_PERSON"].ToString());
                            }
                            if (dsCoach.Tables[0].Rows[Coach]["CHILD_RATE_PER_PERSON"].ToString() != "")
                            {
                                CHILD_RATE_PER_PERSON_COACH = Convert.ToDecimal(dsCoach.Tables[0].Rows[Coach]["CHILD_RATE_PER_PERSON"].ToString());
                            }
                            no_of_single_pax_rate += (no_of_single_room) * (ADULT_RATE_PER_PERSON_COACH);
                            no_of_double_pax_rate += (no_of_Double_room) * (ADULT_RATE_PER_PERSON_COACH) * (2);
                            no_of_triple_pax_rate += (no_of_Triple_room) * (ADULT_RATE_PER_PERSON_COACH) * (3);
                        }
                    }
                }
            }
            //--------------------------------- CALCULATION END ---------------------------------------------------------

            //---------------------------------- INSERT INTO SALES INVOICE DETAILS TABLE ---------------------------------
            //objgitpayment.insert_sales_invoice_details(int.Parse(dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString()), dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), no_of_single_pax_rate, no_of_double_pax_rate, no_of_triple_pax_rate, no_of_cwb_rate, no_of_cnb_rate, 0);
        }
        
        protected void btnpatnow_Click(object sender, EventArgs e)
        {

            DataSet ds22 = objgitpayment.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
            DataSet dspayment = objgitpayment.fetch_paymentmode("FETCH_PAYMENT_MODE");
            DataSet dscurrency = objgitpayment.fetch_paymentmode("FETCH_ALL_CURRENCY_NAME");
            DataSet ds = objgitpayment.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
            DataSet dspwd = objgitpayment.fetch_password(int.Parse(Session["rel_sr_no"].ToString()));
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
            else if (crdit_limit_flag == false)
            {
                Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
            }
            else
            {
                DataSet DS = objgitpayment.FetchSalesInvoiceHeader(Request["TOURID"].ToString());
                
                int NO_OF_ADULTS = 0;
                int NO_OF_CWB = 0;
                int NO_OF_CNB = 0;
                int NO_OF_INFANT = 0;
                int NO_OF_NIGHTS = 0;
                if (DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString() != "")
                {
                    NO_OF_ADULTS = int.Parse(DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString());
                }
                if (DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString() != "")
                {
                    NO_OF_CWB = int.Parse(DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString());
                }
                if (DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString() != "")
                {
                    NO_OF_CNB = int.Parse(DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString());
                }
                if (DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString() != "")
                {
                    NO_OF_INFANT = int.Parse(DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString());
                }
                if (DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString() != "")
                {
                    NO_OF_NIGHTS = int.Parse(DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString());
                }

                //---- FETCH ORDER STATUS NAME
                DataTable dtOrder = objgitpayment.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "5");

                //----- INSERT INTO SALES INVOICE HEADER
                DataSet dt = objgitpayment.insert_sales_invoice_header(int.Parse(DS.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["empid"].ToString()), Session["FromDate"].ToString(), Session["ToDate"].ToString(), NO_OF_NIGHTS, lblTotalInvoice.Text, 0, lblTotalInvoice.Text, true, NO_OF_ADULTS, 0, NO_OF_CWB, NO_OF_CNB, NO_OF_INFANT, dtOrder.Rows[0]["ORDER_STATUS_NAME"].ToString(), drpPayment.Text, "", 1, dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString());

                objgitpayment.updateInvoiceAsGIT(int.Parse(dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString()));    

                DataSet ds_rate = objFITPaymentStoreProcedure.fetch_conversion_rate();
                string total_sales_amount = (decimal.Parse(lblTotalInvoice.Text) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                insertAccountEntry(dt.Tables[0].Rows[0]["INVOICE_NO"].ToString(), total_sales_amount);

                Decimal single_share = 0;
                Decimal double_share = 0;
                Decimal triple_share = 0;
                Decimal Cwb_share = 0;
                Decimal Cnb_share = 0;
                if(ViewState["single_share_rate"] != null || ViewState["single_share_rate"].ToString() !="")
                {
                    single_share = Convert.ToDecimal(ViewState["single_share_rate"].ToString());
                }
                if (ViewState["double_share_rate"] != null || ViewState["double_share_rate"].ToString() != "")
                {
                    double_share = Convert.ToDecimal(ViewState["double_share_rate"].ToString());
                }
                if (ViewState["triple_share_rate"] != null || ViewState["triple_share_rate"].ToString() != "")
                {
                    triple_share = Convert.ToDecimal(ViewState["triple_share_rate"].ToString());
                }
                if (ViewState["cwb_share_rate"] != null || ViewState["cwb_share_rate"].ToString() != "")
                {
                    Cwb_share = Convert.ToDecimal(ViewState["cwb_share_rate"].ToString());
                }
                if (ViewState["cnb_share_rate"] != null || ViewState["cnb_share_rate"].ToString() != "")
                {
                    Cnb_share = Convert.ToDecimal(ViewState["cnb_share_rate"].ToString());
                }
                int single_per = 0;
                int double_per = 0;
                int triple_per = 0;
                int cwb_per = 0;
                int cnb_per = 0;
                if (ViewState["single_room_per"] != null || ViewState["single_room_per"].ToString() != "")
                {
                    single_per = int.Parse(ViewState["single_room_per"].ToString());
                }
                if (ViewState["double_room_per"] != null || ViewState["double_room_per"].ToString() != "")
                {
                    double_per = int.Parse(ViewState["double_room_per"].ToString());
                }
                if (ViewState["triple_room_per"] != null || ViewState["triple_room_per"].ToString() != "")
                {
                    triple_per = int.Parse(ViewState["triple_room_per"].ToString());
                }
                if (ViewState["cwb_per"] != null || ViewState["cwb_per"].ToString() != "")
                {
                    cwb_per = int.Parse(ViewState["cwb_per"].ToString());
                }
                if (ViewState["cnb_per"] != null || ViewState["cnb_per"].ToString() != "")
                {
                    cnb_per = int.Parse(ViewState["cnb_per"].ToString());
                }

                //---------------------------------- INSERT INTO SALES INVOICE DETAILS TABLE ---------------------------------
                objgitpayment.insert_sales_invoice_details(int.Parse(dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString()), dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), single_share, double_share, triple_share, Cwb_share, Cnb_share, single_per,double_per,triple_per,cwb_per,cnb_per);

               // objgitpayment.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount.Text));

                //------------------------------- Report For Invoice -------------------------------------------

                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Invoices/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/"));
                }
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Itinerary/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Itinerary/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/"));
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

                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("SALES_INVOICE_ID", dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString());
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/Invoice_Git";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();
                renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                rptViewer1.Visible = false;
                rptViewer1.Reset();

                //------------------------------ itineary -------------------------------------
                ReportParameter[] parmitieanry = new ReportParameter[1];
                parmitieanry[0] = new ReportParameter("SALES_INVOICE_ID", dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString());
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/ItineraryGit";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();

                renderedBytes1 = rptViewer1.ServerReport.Render(reportType, deviceInfo1, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                //   Response.Clear();
                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Itinerary/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/Itinerary.pdf")))
                {
                    fs.Write(renderedBytes1, 0, (int)renderedBytes1.Length);
                }

                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "/Invoice.pdf")))
                {
                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                }
                objInsertGitDetails.updateQuoteStatus(int.Parse(tourId), "Reconfirmed");
                Session["salesinvoiceid"] = dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString();
                AgentReconfirmEmail("1");
                AgentReconfirmEmail_Second("1");
                Response.Redirect("~/Views/GIT/AgentInvoicesGIT.aspx?TOURID=" + dt.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString() + "&QUOTEID=" + DS.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString());
            }
        }

        #endregion

        #region Send click

        protected void btnsend_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region  Paypal Click

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
                string responseURL = objgitpayment.getItemNameAndCost(Tourname, lblTotalInvoice.Text);
                Response.Redirect(responseURL);
            }
            else
            {
                decimal charges = decimal.Parse(lbltotalInvoiceAmount.Text);
                string s = "3.5";
                decimal addcharges = ((charges) * decimal.Parse(s)) / 100;
                string totalchargs = string.Format("{0:#.00}", charges + addcharges);
                string responseURL = objgitpayment.getItemNameAndCost(Tourname, totalchargs);
                Response.Redirect(responseURL);
            }


        }

        #endregion

        #region RECONFIRMATION EMAILS FOR ALL

        protected void HotelReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for Hotel */
                    DataSet ds1 = objBookSp.getHotelForreconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc1 = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_CONTENT"].ToString();


                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFSINGLEROOM%&gt;", ds1.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFDOUBLEROOM%&gt;", ds1.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFTRIPLEROOM%&gt;", ds1.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds1.Tables[0].Rows[j]["NO_OF_ADULTS"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCWB%&gt;", ds1.Tables[0].Rows[j]["NO_OF_CWB"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCNB%&gt;", ds1.Tables[0].Rows[j]["NO_OF_CNB"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFINFANT%&gt;", ds1.Tables[0].Rows[j]["NO_OF_INFANT"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%ARRIVALDATE%&gt;", ds1.Tables[0].Rows[j]["START_DATE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DEPARTUREDATE%&gt;", ds1.Tables[0].Rows[j]["END_DATE"].ToString());
                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void GaladinnerReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {

                    /*mail fires  for gala dinner */
                    DataSet ds1 = objBookSp.getGalaDinnerForconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[3]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[3]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[3]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[3]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[3]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[3]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[3]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[3]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[3]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[3]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[3]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[3]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[3]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[3]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[3]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_CONTENT"].ToString();

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        //strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds1.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GALADINNERTYPE%&gt;", ds1.Tables[0].Rows[j]["GALA_DINNER_TYPE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds1.Tables[0].Rows[j]["NO_OF_ADULT"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCHILD%&gt;", ds1.Tables[0].Rows[j]["NO_OF_CHILD"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DATE%&gt;", ds1.Tables[0].Rows[j]["DINNER_DATE"].ToString());


                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void ConferenceReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for Conference */
                    DataSet ds1 = objBookSp.getConferenceForconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[4]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[4]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[4]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "gitops@travelzunlimited.com"; ;

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[4]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[4]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[4]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[4]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[4]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[4]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[4]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[4]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[4]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[4]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[4]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "gitops@travelzunlimited.com"; ;

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[4]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[4]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[4]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[4]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[4]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[4]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[4]["EMAIL_CONTENT"].ToString();

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        //strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds1.Tables[0].Rows[j]["CHAIN_NAME"].ToString());                    
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%CONFERENCETYPE%&gt;", ds1.Tables[0].Rows[j]["CONFERENCE_TYPE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds1.Tables[0].Rows[j]["NO_OF_ADULT"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCHILD%&gt;", ds1.Tables[0].Rows[j]["NO_OF_CHILD"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DATE%&gt;", ds1.Tables[0].Rows[j]["CONFERENCE_DATE"].ToString());
                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[4]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[4]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[4]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void SiteseeingrReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for Siteseeing */
                    DataSet ds1 = objBookSp.getSiteSeeingForconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[5]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[5]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[5]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[5]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[5]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[5]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[5]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[5]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[5]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "gitops@travelzunlimited.com";

                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[5]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[5]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[5]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[5]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[5]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[5]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[5]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[5]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[5]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[5]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc1 = "gitops@travelzunlimited.com";

                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[5]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[5]["EMAIL_CONTENT"].ToString();

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%SITESEEINGNAME%&gt;", ds1.Tables[0].Rows[j]["SIGHT_SEEING_PACKAGE_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds1.Tables[0].Rows[j]["NO_OF_ADULT"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCHILD%&gt;", ds1.Tables[0].Rows[j]["NO_OF_CHILD"].ToString());

                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[5]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[5]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[5]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        protected void AgentReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for HOTEL  */
                    DataSet ds1 = objBookSp.GetDetailForReconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                
                                fromemail = "accounts@travelzunlimited.com";
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                
                                toemail1 = "accounts@travelzunlimited.com";
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                cc = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                
                                cc = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                bcc1 = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                
                                bcc1 = "accounts@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                        
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", ds1.Tables[0].Rows[0]["AGENT_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%PAYMENTMODE%&gt;", "PAYMENTMODE");

                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }


                        string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf");
                        Attachment attachFile = new Attachment(filename);

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void AgentReconfirmEmail_Second(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[23]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[1]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for Agent */
                    DataSet ds1 = objBookSp.GetDetailForReconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                fromemail = "gitops@travelzunlimited.com";
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                toemail1 = "gitops@travelzunlimited.com";
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string cc = "";
                        if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                cc = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                cc = "gitops@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                cc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                bcc1 = ds1.Tables[0].Rows[j]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                bcc1 = "gitops@travelzunlimited.com";
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = ds1.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string body = "";


                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", ds1.Tables[0].Rows[0]["AGENT_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%PAYMENTMODE%&gt;", "PAYMENTMODE");

                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.To.Add(new MailAddress(toemail1));
                        if (cc != "")
                        {
                            message.CC.Add(new MailAddress(cc));
                        }

                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Itinerary/" + Session["salesinvoiceid"].ToString() + "/Itinerary.pdf");
                        Attachment attachFile = new Attachment(filename);

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

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

                        if (j == 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        }
                        if (j != 0)
                        {
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }


        protected void insertAccountEntry(string invoiceNo, string totalAmount)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            //DataSet ds11 = objFITPaymentStoreProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", invoiceNo);

            DataSet ds_rate = objFITPaymentStoreProcedure.fetch_conversion_rate();

            DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();

            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds6 = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", Session["CUSTID"].ToString(), "A");

            

            DataSet ds_amount_fit_package = objFITPaymentStoreProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString());

            DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), invoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", totalAmount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), invoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", totalAmount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[11]["AutoSearchResult"].ToString(), invoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, totalAmount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
        }
    }
}