using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.Account;
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
    public partial class AgentInvoicesGIT : System.Web.UI.Page
    {
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

        string todayDate;

        decimal TOTAL_INVOICE_AMOUNT;

        DataSet ds_vt = null;

        DataSet ds_vsstatus = null;

        DataSet ds22 = null;

        DataSet ds_sup_type = null;

        DataSet ds_all_gl_code = null;

        string adult = "0";
        string cwb = "0";
        string cnb = "0";
        string infant = "0";

        #endregion

        #region Objects & Variable

        AgentInvoiceGitDA objAgentInvoiceDA = new AgentInvoiceGitDA();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        BookSp objBookSp = new BookSp();
        GITPaymentDA objgitpayment = new GITPaymentDA();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            todayDate = DateTime.Today.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                DataSet ds = objAgentInvoiceDA.GetPaymentMode("FETCH_PAYMENT_MODE");
                binddropdownlist(drpPaymentMode, ds);
                DataSet ds2 = objAgentInvoiceDA.GetOrderStatus("FETCH_ORDER_STATUS");
                binddropdownlist(drpOrderStatus, ds2);
                DataSet ds3 = objAgentInvoiceDA.GetOrderStatus("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(DropDownList1, ds3);
                DataSet ds1 = objAgentInvoiceDA.GetCurrency("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(drpCurrency, ds1);
                binddropdownlist(row1_drp_currency, ds1);
                binddropdownlist(row2_drp_currency, ds1);
                binddropdownlist(row3_drp_currency, ds1);
                binddropdownlist(row4_drp_currency, ds1);
                binddropdownlist(row5_drp_currency, ds1);
                binddropdownlist(row6_drp_currency, ds1);
                binddropdownlist(row7_drp_currency, ds1);
                binddropdownlist(row8_drp_currency, ds1);
                binddropdownlist(row9_drp_currency, ds1);
                binddropdownlist(row10_drp_currency, ds1);
                binddropdownlist(row11_drp_currency, ds1);
                binddropdownlist(row12_drp_currency, ds1);
                binddropdownlist(row13_drp_currency, ds1);
                binddropdownlist(row14_drp_currency, ds1);
                binddropdownlist(row15_drp_currency, ds1);
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    DataSet ds_manual = objAgentInvoiceDA.fetchismanual("FETCH_IS_MANUAL", Request.QueryString["QUOTEID"].ToString());
                    if (ds_manual.Tables[0].Rows[0]["IS_MANUAL"].ToString() == "True")
                    {
                        // btnCopy.Visible = false;
                    }
                    string saleid = Request.QueryString["TOURID"].ToString();
                    string quoteid = Request.QueryString["QUOTEID"].ToString();
                    Session["quoteid"] = Request.QueryString["QUOTEID"].ToString();
                    ViewState["quote"] = Request.QueryString["QUOTEID"].ToString();
                    txtQuote.Text = quoteid;
                    txtQuote.ReadOnly = true;
                    TextBox20.ReadOnly = true;
                    Session["salesinvoiceid"] = Request.QueryString["TOURID"].ToString();
                    DataSet DS = objAgentInvoiceDA.FetchDataForInvoice("FETCH_DATA_FOR_GENREATE_INVOICE_GIT", quoteid, saleid);
                    drpOrderStatus.SelectedValue = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                    txtclientname.Text = DS.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString();

                    txtNoOfNights.Text = DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
                    Session["sa"] = DS.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString();
                    drpCurrency.SelectedValue = DS.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    txtNoOfAdult.Text = DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                    txtNoOfChild.Text = DS.Tables[0].Rows[0]["NO_OF_CHILD"].ToString();
                    txtNoOfCWB.Text = DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                    txtNoOfCNB.Text = DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                    txtNoOfInfant.Text = DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
                    txtPeriodStayFrom.Text = DS.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txtPeriodStayTo.Text = DS.Tables[0].Rows[0]["TO_DATE"].ToString();
                    txtAmount.Text = DS.Tables[0].Rows[0]["AMOUNT"].ToString();
                    txtTax.Text = DS.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();
                    txtTotalAmount.Text = DS.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    txtBook_ref_no.Text = DS.Tables[0].Rows[0]["COMPANY_BOOKING_REFERENCE_NO"].ToString();
                    TextBox20.Text = DS.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    TextBox42.Text = DS.Tables[0].Rows[0]["GL_DATE"].ToString();
                    ViewState["INVOICE"] = DS.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    DropDownList1.SelectedValue = DS.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    drpPaymentMode.SelectedValue = DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();

                    for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                    {
                        if (TextBox1.Text == "SINGLE SHARE" && row1_drp_currency.Text == "" && TextBox23.Text == "" && TextBox22.Text == "" && row1_txt_debit.Text == "")
                        {
                            TextBox1.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            row1_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                            row1_txt_debit.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            ViewState["s1"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            TextBox22.Text = DS.Tables[1].Rows[0]["NO_OF_PERSON"].ToString();
                            decimal b = 1;
                            if (DS.Tables[1].Rows[0]["NO_OF_PERSON"].ToString() != "")
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[0]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                TextBox23.Text = "0";
                            }
                            else
                            {
                                TextBox23.Text = string.Format("{0:#.00}", a / b);
                                //TextBox23.Text = (a / b).ToString();
                            }
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (TextBox2.Text == "DOUBLE SHARE" && row2_drp_currency.Text == "" && TextBox3.Text == "" && TextBox24.Text == "" && TextBox25.Text == "")
                        {
                            row2.Attributes.Add("style", "display");
                            btnadd2.Attributes.Add("style", "display:none");
                            btnadd3.Attributes.Add("style", "display");
                            TextBox2.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            row2_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                            TextBox3.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            ViewState["s2"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            TextBox24.Text = DS.Tables[1].Rows[1]["NO_OF_PERSON"].ToString();
                            decimal b = 1;
                            if (DS.Tables[1].Rows[1]["NO_OF_PERSON"].ToString() != "")
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[1]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                TextBox25.Text = "0";
                            }
                            else
                            {
                                TextBox25.Text = string.Format("{0:#.00}", a / b);
                            }
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (TextBox4.Text == "TRIPLE SHARE" && row3_drp_currency.Text == "" && TextBox5.Text == "" && TextBox26.Text == "" && TextBox27.Text == "")
                        {
                            row3.Attributes.Add("style", "display");
                            btnadd3.Attributes.Add("style", "display:none");
                            btnadd4.Attributes.Add("style", "display");
                            TextBox4.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            row3_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                            TextBox5.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            ViewState["s3"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            TextBox26.Text = DS.Tables[1].Rows[2]["NO_OF_PERSON"].ToString();
                            decimal b = 1;
                            if (DS.Tables[1].Rows[2]["NO_OF_PERSON"].ToString() != "")
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[2]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                TextBox27.Text = "0";
                            }
                            else
                            {
                                TextBox27.Text = string.Format("{0:#.00}", a / b);
                            }
                            UpdatePanel_Generate_Invoice.Update();

                        }
                        else if (TextBox6.Text == "CHILD WITH BED" && row4_drp_currency.Text == "" && TextBox7.Text == "" && TextBox28.Text == "" && TextBox29.Text == "")
                        {
                            row4.Attributes.Add("style", "display");
                            btnadd4.Attributes.Add("style", "display:none");
                            btnadd5.Attributes.Add("style", "display");
                            TextBox6.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            row4_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                            TextBox7.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            ViewState["s4"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            TextBox28.Text = DS.Tables[1].Rows[3]["NO_OF_PERSON"].ToString();
                            decimal b = 1;
                            if (DS.Tables[1].Rows[3]["NO_OF_PERSON"].ToString() == "")
                            {
                                b = 1;
                            }
                            else
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[3]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                TextBox29.Text = "0";
                            }
                            else
                            {
                                TextBox29.Text = string.Format("{0:#.00}", a / b);
                            }
                            UpdatePanel_Generate_Invoice.Update();

                        }
                        else if (TextBox8.Text == "CHILD WITH NO BED" && row5_drp_currency.Text == "" && TextBox9.Text == "" && TextBox30.Text == "" && TextBox31.Text == "")
                        {
                            row5.Attributes.Add("style", "display");
                            btnadd5.Attributes.Add("style", "display:none");
                            //btnadd6.Attributes.Add("style", "display");
                            TextBox8.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            row5_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                            TextBox9.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            ViewState["s5"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            TextBox30.Text = DS.Tables[1].Rows[4]["NO_OF_PERSON"].ToString();

                            decimal b;
                            if (DS.Tables[1].Rows[3]["NO_OF_PERSON"].ToString() == "")
                            {
                                b = 1;
                            }
                            else
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[3]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                TextBox31.Text = "0";
                            }
                            else
                            {
                                TextBox31.Text = string.Format("{0:#.00}", a / b);
                            }
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 5)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row6.Attributes.Add("style", "display");
                                btnadd6.Attributes.Add("style", "display:none");
                                btnadd7.Attributes.Add("style", "display");

                                TextBox10.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                TextBox11.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox32.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox33.Text = "0";
                                }
                                else
                                {
                                    TextBox33.Text = string.Format("{0:#.00}", a / b);
                                }
                                row6_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                //TextBox11.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();

                            }
                            ViewState["s6"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 6)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row7.Attributes.Add("style", "display");
                                btnadd7.Attributes.Add("style", "display:none");
                                btnadd8.Attributes.Add("style", "display");
                                TextBox12.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row7_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox13.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox34.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox35.Text = "0";
                                }
                                else
                                {
                                    TextBox35.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s7"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 7)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row8.Attributes.Add("style", "display");
                                btnadd8.Attributes.Add("style", "display:none");
                                btnadd9.Attributes.Add("style", "display");
                                TextBox14.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row8_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox15.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox36.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox37.Text = "0";
                                }
                                else
                                {
                                    TextBox37.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s8"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 8)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row9.Attributes.Add("style", "display");
                                btnadd9.Attributes.Add("style", "display:none");
                                btnadd10.Attributes.Add("style", "display");

                                TextBox17.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row9_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox16.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox38.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox39.Text = "0";
                                }
                                else
                                {
                                    TextBox39.Text = string.Format("{0:#.00}", a / b);
                                }
                            }
                            ViewState["s9"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 9)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row10.Attributes.Add("style", "display");
                                btnadd10.Attributes.Add("style", "display:none");
                                btnadd11.Attributes.Add("style", "display");
                                TextBox18.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row10_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox19.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox40.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox41.Text = "0";
                                }
                                else
                                {
                                    TextBox41.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s10"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 10)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row11.Attributes.Add("style", "display");
                                btnadd11.Attributes.Add("style", "display:none");
                                btnadd12.Attributes.Add("style", "display");
                                TextBox43.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row11_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox46.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox44.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox45.Text = "0";
                                }
                                else
                                {
                                    TextBox45.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s11"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            UpdatePanel_Generate_Invoice.Update();
                        }
                        else if (i == 11)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row12.Attributes.Add("style", "display");
                                btnadd12.Attributes.Add("style", "display:none");
                                btnadd13.Attributes.Add("style", "display");
                                TextBox47.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row12_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox50.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox48.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox49.Text = "0";
                                }
                                else
                                {
                                    TextBox49.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s12"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                        }
                        else if (i == 12)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row13.Attributes.Add("style", "display");
                                btnadd13.Attributes.Add("style", "display:none");
                                btnadd14.Attributes.Add("style", "display");
                                TextBox51.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row13_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox54.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox52.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox53.Text = "0";
                                }
                                else
                                {
                                    TextBox53.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s13"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                        }
                        else if (i == 13)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row14.Attributes.Add("style", "display");
                                btnadd14.Attributes.Add("style", "display:none");
                                btnadd15.Attributes.Add("style", "display");
                                TextBox55.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row14_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox58.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox56.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox57.Text = "0";
                                }
                                else
                                {
                                    TextBox57.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s14"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                        }
                        else if (i == 14)
                        {
                            if (DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "" && DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "0.00")
                            {
                                row15.Attributes.Add("style", "display");
                                btnadd15.Attributes.Add("style", "display:none");
                                TextBox59.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                                row15_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
                                TextBox62.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                                decimal a = 0;
                                if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                                {
                                    a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                                }
                                TextBox60.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                                decimal b;
                                if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() == "")
                                {
                                    b = 1;
                                }
                                else
                                {
                                    b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                                }
                                if (a == 0 && b == 0)
                                {
                                    TextBox61.Text = "0";
                                }
                                else
                                {
                                    TextBox61.Text = string.Format("{0:#.00}", a / b);
                                }

                            }
                            ViewState["s15"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                        }
                    }
                    String name = DS.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    DataSet DSFOREMP = objAgentInvoiceDA.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                    Session["rel_sr_no"] = DSFOREMP.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    Session["uid"] = DSFOREMP.Tables[0].Rows[0]["USER_ID"].ToString();
                    DataTable dtad = objAgentInvoiceDA.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                    Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
                    DataSet dscredit = objAgentInvoiceDA.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                    lblcreditlimitAmount.Text = dscredit.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                    lblcurrentusableamount.Text = dscredit.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();

                    DataTable DTMODE = objAgentInvoiceDA.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
                    if (DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString() == DTMODE.Rows[0]["AutoSearchResult"].ToString())
                    {
                        t1.Attributes.Add("style", "display:none");
                        t2.Attributes.Add("style", "display:none");
                        t3.Attributes.Add("style", "display:none");
                        t4.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        t1.Attributes.Add("style", "display");
                        t2.Attributes.Add("style", "display");
                        t3.Attributes.Add("style", "display");
                        t4.Attributes.Add("style", "display");
                    }

                    if (lblcurrentusableamount.Text == "")
                    {
                        lblcurrentusableamount.Text = "0";
                    }
                    lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();

                    DataSet dsin = objAgentInvoiceDA.fetch_total_invoice(int.Parse(txtQuote.Text));

                    if (dsin.Tables[0].Rows.Count == 0)
                    {
                        txttotalInvoiceAmount.Text = DS.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttotalInvoiceAmount.Visible = true;
                        lbltotalInvoiceAmount.Visible = false;
                    }
                    else
                    {
                        lbltotalInvoiceAmount.Text = DS.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        ViewState["old_amount"] = lbltotalInvoiceAmount.Text;

                    }

                    UpdatePanel_Generate_Invoice.Update();
                }
                else
                {
                    for (int i = drpPaymentMode.Items.Count - 1; i > 0; i--)
                    {
                        ListItem item = drpPaymentMode.Items[i];
                        if (item.ToString() == "CASH" || item.ToString() == "CHEQUE" || item.ToString() == "TRANSFER")
                        {
                            drpPaymentMode.Items.Remove(item);
                        }
                        else
                        {

                        }
                    }

                    drpOrderStatus.Text = "Reconfirmed";
                    txtQuote.ReadOnly = true;
                    TextBox20.ReadOnly = true;
                    Session["sa"] = "0";
                    drpCurrency.SelectedValue = "USD";
                    ViewState["INVOICE"] = "";
                    ViewState["s1"] = "0";
                    ViewState["s2"] = "0";
                    ViewState["s3"] = "0";
                    ViewState["s4"] = "0";
                    ViewState["s5"] = "0";
                    ViewState["s6"] = "0";
                    ViewState["s7"] = "0";
                    ViewState["s8"] = "0";
                    ViewState["s9"] = "0";
                    ViewState["s10"] = "0";
                    ViewState["s11"] = "0";
                    ViewState["s11"] = "0";
                    ViewState["s11"] = "0";
                    ViewState["s11"] = "0";
                    ViewState["s11"] = "0";
                    row1_drp_currency.SelectedValue = "USD";
                    row2_drp_currency.SelectedValue = "USD";
                    row3_drp_currency.SelectedValue = "USD";
                    row4_drp_currency.SelectedValue = "USD";
                    row5_drp_currency.SelectedValue = "USD";
                    row6_drp_currency.SelectedValue = "USD";
                    row7_drp_currency.SelectedValue = "USD";
                    row8_drp_currency.SelectedValue = "USD";
                    row9_drp_currency.SelectedValue = "USD";
                    row10_drp_currency.SelectedValue = "USD";
                    row11_drp_currency.SelectedValue = "USD";
                    row12_drp_currency.SelectedValue = "USD";
                    row13_drp_currency.SelectedValue = "USD";
                    row14_drp_currency.SelectedValue = "USD";
                    row15_drp_currency.SelectedValue = "USD";
                }
            }
            else
            {
                if (drpOrderStatus.Text == "Closed")
                {
                    ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

                    ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                    ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));

                    ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");

                    ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
                }
            }


        }

        #endregion

        #region Dropdown Event

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            r.SelectedValue = "0";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = DropDownList1.Text;
            DataSet DS = objAgentInvoiceDA.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
            Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
            Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();

            DataTable dtad = objAgentInvoiceDA.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
            Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
            Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();
        }

        #endregion

        #region TextChange Event

        protected void txtQuote_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTax_TextChanged(object sender, EventArgs e)
        {
            Decimal a;
            Decimal b;
            if (txtAmount.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(txtAmount.Text);
            }
            if (txtTax.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(txtTax.Text);
            }
            decimal c = a + b;
            txtTotalAmount.Text = c.ToString();
            txttotalInvoiceAmount.Text = c.ToString();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void tax_onblur(object sender, EventArgs e)
        {
            Decimal a;
            Decimal b;
            if (txtAmount.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(txtAmount.Text);
            }
            if (txtTax.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(txtTax.Text);
            }
            decimal c = a + b;
            txtTotalAmount.Text = c.ToString();
            txttotalInvoiceAmount.Text = c.ToString();
            UpdatePanel_Generate_Invoice.Update();
        }

        #endregion

        #region Send Invoice to Agent

        protected void btnSendInvoice_Click(object sender, EventArgs e)
        {
            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {
                DataSet ds = objAgentInvoiceDA.fetchismanual("FETCH_IS_MANUAL", Session["quoteid"].ToString());
                SendInvoiceAgent("");
                //if (ds.Tables[0].Rows[0]["IS_MANUAL"].ToString() == "True")
                //{
                //    //SendMail1();
                //    SendInvoiceAgent();
                //}
                //else
                //{
                //    //SendMail();
                //    //Send_invoice_to_agent();
                //    //sendhotelmail();

                //}
            }
            else
            {
                //SendMail1();
            }
            Master.DisplayMessage("Invoice Sent Successfully.", "successMessage", 8000);
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void SendInvoiceAgent(string bcc)
        {
            try
            {

                string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf");
                Attachment attachFile = new Attachment(filename);

                string filename1 = HttpContext.Current.Request.MapPath("~/Views/FIT/Itinerary/" + Session["salesinvoiceid"].ToString() + "/Itinerary.pdf");
                Attachment attachFile1 = new Attachment(filename1);


                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[24]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();
                string QUTE = Request.QueryString["QUOTEID"].ToString();
                DataTable DTTOUR = objAgentInvoiceDA.FETCH_TOUR_ID_FROM_QUOTE_ID(QUTE);
                int TOUR = int.Parse(DTTOUR.Rows[0]["GIT_TOUR_ID"].ToString());
                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for HOTEL  */
                    DataSet ds1 = objBookSp.GetDetailForReconfirmEmail(TOUR);
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                //if (bcc == "1")
                                //{
                                //    fromemail = "reservation@travelzunlimited.com";
                                //}
                                //else if (bcc == "2")
                                //{
                                //    fromemail = "reservation1@travelzunlimited.com";
                                //}
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
                                //if (bcc == "1")
                                //{
                                //    toemail1 = "reservation@travelzunlimited.com";
                                //}
                                //else if (bcc == "2")
                                //{
                                //    toemail1 = "reservation1@travelzunlimited.com";
                                //}
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
                                //if (bcc == "1")
                                //{
                                //    cc = "reservation@travelzunlimited.com";
                                //}
                                //else if (bcc == "2")
                                //{
                                //    cc = "reservation1@travelzunlimited.com";
                                //}
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
                                //if (bcc == "1")
                                //{
                                //    bcc1 = "reservation@travelzunlimited.com";
                                //}
                                //else if (bcc == "2")
                                //{
                                //    bcc1 = "reservation1@travelzunlimited.com";
                                //}
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
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%PAYMENTMODE%&gt;", drpPaymentMode.Text);

                        body = strEmailTemplate;

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(fromemail);

                        message.Attachments.Add(attachFile);
                        message.Attachments.Add(attachFile1);
                        message.To.Add(new MailAddress(toemail1));
                        if (TextBox21.Text != "")
                        {
                            if (cc != "")
                            {
                                cc = cc + "," + TextBox21.Text;
                            }
                            else
                            {
                                cc = TextBox21.Text;
                            }
                        }
                        message.From = new MailAddress(fromemail);
                        if (cc == "")
                        {
                        }
                        else
                        {
                            if (cc.Contains(","))
                            {
                                string[] ccs = cc.Split(',');
                                for (int i = 0; i < ccs.Length; i++)
                                {
                                    message.CC.Add(new MailAddress(ccs[i]));
                                }
                            }
                            else
                            {
                                message.CC.Add(new MailAddress(cc));
                            }
                        }
                        if (bcc1 != "")
                        {
                            message.Bcc.Add(new MailAddress(bcc1));
                        }

                        string subjct = "";
                        subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();


                        subjct = subjct.Replace("<%GROUPNAME%>", ds1.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                        subjct = subjct.Replace("<%ARRIVALDATE%>", ds1.Tables[0].Rows[0]["START_DATE"].ToString());
                        subjct = subjct.Replace("<%DEPARTUREDATE%>", ds1.Tables[0].Rows[0]["END_DATE"].ToString());

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

        #endregion

        #region paymentdetails button
        public void drpPayment_SelectedIndexChanged(Object sender, EventArgs e)
        {

        }

        protected void btnadd2_Click(object sender, EventArgs e)
        {
            row2.Attributes.Add("style", "display");
            btnadd2.Attributes.Add("style", "display:none");
            btnadd3.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd3_Click(object sender, EventArgs e)
        {
            row3.Attributes.Add("style", "display");
            btnadd3.Attributes.Add("style", "display:none");
            btnadd4.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd4_Click(object sender, EventArgs e)
        {

            row4.Attributes.Add("style", "display");
            btnadd4.Attributes.Add("style", "display:none");
            btnadd5.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd5_Click(object sender, EventArgs e)
        {

            row5.Attributes.Add("style", "display");
            btnadd5.Attributes.Add("style", "display:none");
            btnadd6.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd6_Click(object sender, EventArgs e)
        {
            row6.Attributes.Add("style", "display");
            btnadd6.Attributes.Add("style", "display:none");
            btnadd7.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd7_Click(object sender, EventArgs e)
        {
            row7.Attributes.Add("style", "display");
            btnadd7.Attributes.Add("style", "display:none");
            btnadd8.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd8_Click(object sender, EventArgs e)
        {

            row8.Attributes.Add("style", "display");
            btnadd8.Attributes.Add("style", "display:none");
            btnadd9.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd9_Click(object sender, EventArgs e)
        {
            row9.Attributes.Add("style", "display");
            btnadd9.Attributes.Add("style", "display:none");
            btnadd10.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd10_Click(object sender, EventArgs e)
        {
            row10.Attributes.Add("style", "display");
            btnadd10.Attributes.Add("style", "display:none");
            btnadd11.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd11_Click(object sender, EventArgs e)
        {
            row11.Attributes.Add("style", "display");
            btnadd11.Attributes.Add("style", "display:none");
            btnadd12.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd12_Click(object sender, EventArgs e)
        {
            row12.Attributes.Add("style", "display");
            btnadd12.Attributes.Add("style", "display:none");
            btnadd13.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd13_Click(object sender, EventArgs e)
        {
            row13.Attributes.Add("style", "display");
            btnadd13.Attributes.Add("style", "display:none");
            btnadd14.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd14_Click(object sender, EventArgs e)
        {
            row14.Attributes.Add("style", "display");
            btnadd14.Attributes.Add("style", "display:none");
            btnadd15.Attributes.Add("style", "display");
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void btnadd15_Click(object sender, EventArgs e)
        {
            row15.Attributes.Add("style", "display");
            btnadd15.Attributes.Add("style", "display:none");
            UpdatePanel_Generate_Invoice.Update();
        }
        #endregion

        #region Save and Copy

        protected void btnSave_Click(object sender, EventArgs e)
        {
            /* calculation of all total amount */
            Decimal Row1;
            Decimal Row2;
            Decimal Row3;
            Decimal Row4;
            Decimal Row5;
            Decimal Row6;
            Decimal Row7;
            Decimal Row8;
            Decimal Row9;
            Decimal Row10;
            Decimal Row11;
            Decimal Row12;
            Decimal Row13;
            Decimal Row14;
            Decimal Row15;
            Decimal total_rate;
            if (row1_txt_debit.Text == "")
            {
                Row1 = decimal.Parse("0");
            }
            else
            {
                Row1 = decimal.Parse(row1_txt_debit.Text);
            }
            if (TextBox3.Text == "")
            {
                Row2 = decimal.Parse("0");
            }
            else
            {
                Row2 = decimal.Parse(TextBox3.Text);
            }
            if (TextBox5.Text == "")
            {
                Row3 = decimal.Parse("0");
            }
            else
            {
                Row3 = decimal.Parse(TextBox5.Text);
            }
            if (TextBox7.Text == "")
            {
                Row4 = decimal.Parse("0");
            }
            else
            {
                Row4 = decimal.Parse(TextBox7.Text);
            }
            if (TextBox9.Text == "")
            {
                Row5 = decimal.Parse("0");
            }
            else
            {
                Row5 = decimal.Parse(TextBox9.Text);
            }
            if (TextBox11.Text == "")
            {
                Row6 = 0;
            }
            else
            {
                Row6 = decimal.Parse(TextBox11.Text);
            }
            if (TextBox13.Text == "")
            {
                Row7 = 0;
            }
            else
            {
                Row7 = decimal.Parse(TextBox13.Text);
            }
            if (TextBox15.Text == "")
            {
                Row8 = 0;
            }
            else
            {
                Row8 = decimal.Parse(TextBox15.Text);
            }
            if (TextBox16.Text == "")
            {
                Row9 = 0;
            }
            else
            {
                Row9 = decimal.Parse(TextBox16.Text);
            }
            if (TextBox19.Text == "")
            {
                Row10 = 0;
            }
            else
            {
                Row10 = decimal.Parse(TextBox19.Text);
            }
            if (TextBox46.Text == "")
            {
                Row11 = 0;
            }
            else
            {
                Row11 = decimal.Parse(TextBox46.Text);
            }
            if (TextBox50.Text == "")
            {
                Row12 = 0;
            }
            else
            {
                Row12 = decimal.Parse(TextBox50.Text);
            }

            if (TextBox54.Text == "")
            {
                Row13 = 0;
            }
            else
            {
                Row13 = decimal.Parse(TextBox54.Text);
            }
            if (TextBox58.Text == "")
            {
                Row14 = 0;
            }
            else
            {
                Row14 = decimal.Parse(TextBox58.Text);
            }
            if (TextBox62.Text == "")
            {
                Row15 = 0;
            }
            else
            {
                Row15 = decimal.Parse(TextBox62.Text);
            }

            total_rate = Row1 + Row2 + Row3 + Row4 + Row5 + Row6 + Row7 + Row8 + Row9 + Row10 + Row11 + Row12 + Row13 + Row14 + Row15;


            Decimal g;
            if (txtQuote.Text == "" || txtQuote.Text == "0")
            {
                g = decimal.Parse(txttotalInvoiceAmount.Text);
                txtQuote.Text = "0";
            }
            else
            {
                g = decimal.Parse(lbltotalInvoiceAmount.Text);
                Session["qid"] = txtQuote.Text;
            }
            bool cash_flag = true;
            bool error_flag = true;
            DataSet ds = objAgentInvoiceDA.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
            DataTable DTMODE = objAgentInvoiceDA.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
            if (drpPaymentMode.Text == DTMODE.Rows[0]["AutoSearchResult"].ToString())
            {
                cash_flag = false;
            }
            if (cash_flag == true)
            {
                if (decimal.Parse(lblcurrentusableamount.Text) < g)
                {
                    Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
                    error_flag = false;
                }
            }
            if (error_flag == true)
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    string QUTE = Request.QueryString["QUOTEID"].ToString();
                    DataTable DTTOUR = objAgentInvoiceDA.FETCH_TOUR_ID_FROM_QUOTE_ID(QUTE);
                    int TOUR = int.Parse(DTTOUR.Rows[0]["GIT_TOUR_ID"].ToString());
                    DataSet dscurrency = objAgentInvoiceDA.GetPaymentMode("FETCH_ALL_CURRENCY_NAME");
                    DataSet dstour = objAgentInvoiceDA.insert_sales_git_fare_tour(int.Parse(QUTE), TOUR, txtclientname.Text, txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, txtTotalAmount.Text, int.Parse(txtNoOfAdult.Text), int.Parse(txtNoOfChild.Text), int.Parse(txtNoOfCWB.Text), int.Parse(txtNoOfCNB.Text), int.Parse(txtNoOfInfant.Text), Session["uid"].ToString(), drpOrderStatus.Text);
                    objAgentInvoiceDA.insert_sales_invoice_header(Session["sa"].ToString(), int.Parse(txtQuote.Text), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, int.Parse(txtNoOfNights.Text), txtAmount.Text, decimal.Parse(txtTax.Text), txtTotalAmount.Text, true, int.Parse(txtNoOfAdult.Text), int.Parse(txtNoOfChild.Text), int.Parse(txtNoOfCWB.Text), int.Parse(txtNoOfCNB.Text), int.Parse(txtNoOfInfant.Text), drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, 1, dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox42.Text);
                    DataSet DS = objAgentInvoiceDA.FetchDataForInvoice("FETCH_DATA_FOR_GENREATE_INVOICE_GIT", Request["QUOTEID"].ToString(), Request["TOURID"].ToString());
                    for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objAgentInvoiceDA.insert_sales_invoice_details(int.Parse(ViewState["s1"].ToString()), "SINGLE SHARE", dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox22.Text, Row1.ToString());
                        }
                        else if (i == 1)
                        {
                            objAgentInvoiceDA.insert_sales_invoice_details(int.Parse(ViewState["s2"].ToString()), "DOUBLE SHARE", dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox24.Text, Row2.ToString());
                        }
                        else if (i == 2)
                        {
                            objAgentInvoiceDA.insert_sales_invoice_details(int.Parse(ViewState["s3"].ToString()), "TRIPLE SHARE", dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox26.Text, Row3.ToString());
                        }
                        else if (i == 3)
                        {
                            objAgentInvoiceDA.insert_sales_invoice_details(int.Parse(ViewState["s4"].ToString()), "CHILD WITH BED", dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox28.Text, Row4.ToString());
                        }
                        else
                        {
                            objAgentInvoiceDA.insert_sales_invoice_details(int.Parse(ViewState["s5"].ToString()), "CHILD WITH NO BED", dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox30.Text, Row5.ToString());
                        }

                    }


                    string lbltotalInvoiceAmount1 = "";
                    if (decimal.Parse(ViewState["old_amount"].ToString()) > decimal.Parse(txtTotalAmount.Text))
                    {
                        lbltotalInvoiceAmount1 = (decimal.Parse(ViewState["old_amount"].ToString()) - decimal.Parse(txtTotalAmount.Text)).ToString();

                    }
                    else
                    {
                        lbltotalInvoiceAmount1 = (decimal.Parse(txtTotalAmount.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                    }

                    if (drpPaymentMode.Text == DTMODE.Rows[0]["AutoSearchResult"].ToString())
                    { }
                    else
                    {
                        if (decimal.Parse(ViewState["old_amount"].ToString()) > decimal.Parse(txtTotalAmount.Text))
                        {
                            objAgentInvoiceDA.edit_current_usable_MINUS(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount1));
                            ViewState["old_amount"] = txtTotalAmount.Text;
                        }
                        else
                        {
                            objAgentInvoiceDA.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount1));
                            ViewState["old_amount"] = txtTotalAmount.Text;
                        }
                    }

                    Response.Clear();
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/")))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/"));

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
                    "  <PageHeight>9in</PageHeight>" +
                    "  <MarginTop>0.50in</MarginTop>" +
                    "  <MarginLeft>0.50in</MarginLeft>" +
                    "  <MarginRight>0.50in</MarginRight>" +
                    "  <MarginBottom>0.50in</MarginBottom>" +
                    "</DeviceInfo>";



                    // quote_id = Page.Request.QueryString["QuoteId"].ToString();

                    ReportParameter[] parm = new ReportParameter[1];
                    parm[0] = new ReportParameter("SALES_INVOICE_ID", Session["salesinvoiceid"].ToString());
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


                    Response.Clear();
                    //Response.Flush();
                    using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf")))
                    {
                        fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                    }
                    DataTable DTORDER = objAgentInvoiceDA.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
                    if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
                    {
                        DataSet ds1 = objAgentInvoiceDA.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                        lblcreditlimitAmount.Text = ds1.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                        lblcurrentusableamount.Text = ds1.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
                        if (lblcurrentusableamount.Text == "")
                        {
                            lblcurrentusableamount.Text = "0";
                        }
                        lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();
                        if (txtQuote.Text == "")
                        {
                            lbltotalInvoiceAmount.Visible = false;
                            txttotalInvoiceAmount.Visible = true;
                        }
                        else
                        {
                            //  DataSet ds10 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(txtQuote.Text));
                            //  lbltotalInvoiceAmount.Text = ds10.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                        }
                    }
                    //Response.Clear();
                    //Response.End();
                    Master.DisplayMessage("Invoice Updated Successfully.", "successMessage", 3000);
                }

                UpdatePanel_Generate_Invoice.Update();
            }
        }



        #endregion

        #region TEXTCHENGED

        protected void row1_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row2_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row3_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row4_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row5_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row6_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row7_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row8_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row9_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row10_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row11_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row12_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row13_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row14_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row15_textchanged(object sender, EventArgs e)
        {
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox22_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox22.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox22.Text);
            }
            if (TextBox23.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox23.Text);
            }
            decimal c = a * b;
            row1_txt_debit.Text = c.ToString();
            textchange();
            TextBox23.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox23_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox22.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox22.Text);
            }
            if (TextBox23.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox23.Text);
            }
            decimal c = a * b;
            row1_txt_debit.Text = c.ToString();
            textchange();
            row1_txt_debit.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox24_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox24.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox24.Text);
            }
            if (TextBox25.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox25.Text);
            }
            decimal c = a * b;
            TextBox3.Text = c.ToString();
            textchange();
            TextBox25.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox25_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox24.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox24.Text);
            }
            if (TextBox25.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox25.Text);
            }
            decimal c = a * b;
            TextBox3.Text = c.ToString();
            textchange();
            TextBox3.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox26_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox26.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox26.Text);
            }
            if (TextBox27.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox27.Text);
            }
            decimal c = a * b;
            TextBox5.Text = c.ToString();
            textchange();
            TextBox27.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox27_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox26.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox26.Text);
            }
            if (TextBox27.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox27.Text);
            }
            decimal c = a * b;
            TextBox5.Text = c.ToString();
            textchange();
            TextBox5.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox28_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox28.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox28.Text);
            }
            if (TextBox29.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox29.Text);
            }
            decimal c = a * b;
            TextBox7.Text = c.ToString();
            textchange();
            TextBox29.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox29_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox28.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox28.Text);
            }
            if (TextBox29.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox29.Text);
            }
            decimal c = a * b;
            TextBox7.Text = c.ToString();
            textchange();
            TextBox7.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox30_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox30.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox30.Text);
            }
            if (TextBox31.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox31.Text);
            }
            decimal c = a * b;
            TextBox9.Text = c.ToString();
            textchange();
            TextBox31.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox31_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox30.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox30.Text);
            }
            if (TextBox31.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox31.Text);
            }
            decimal c = a * b;
            TextBox9.Text = c.ToString();
            textchange();
            TextBox9.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox32_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox32.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox32.Text);
            }
            if (TextBox33.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox33.Text);
            }
            decimal c = a * b;
            TextBox11.Text = c.ToString();
            textchange();
            TextBox11.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox33_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox32.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox32.Text);
            }
            if (TextBox33.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox33.Text);
            }
            decimal c = a * b;
            TextBox11.Text = c.ToString();
            textchange();
            TextBox11.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox34_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox34.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox34.Text);
            }
            if (TextBox35.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox35.Text);
            }
            decimal c = a * b;
            TextBox13.Text = c.ToString();
            textchange();
            TextBox13.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox35_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox34.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox34.Text);
            }
            if (TextBox35.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox35.Text);
            }
            decimal c = a * b;
            TextBox13.Text = c.ToString();
            textchange();
            TextBox13.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox36_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox36.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox36.Text);
            }
            if (TextBox37.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox37.Text);
            }
            decimal c = a * b;
            TextBox15.Text = c.ToString();
            textchange();
            TextBox15.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox37_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox36.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox36.Text);
            }
            if (TextBox37.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox37.Text);
            }
            decimal c = a * b;
            TextBox15.Text = c.ToString();
            textchange();
            TextBox15.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox38_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox38.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox38.Text);
            }
            if (TextBox39.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox39.Text);
            }
            decimal c = a * b;
            TextBox16.Text = c.ToString();
            textchange();
            TextBox16.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox39_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox38.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox38.Text);
            }
            if (TextBox39.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox39.Text);
            }
            decimal c = a * b;
            TextBox16.Text = c.ToString();
            textchange();
            TextBox16.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox40_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox40.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox40.Text);
            }
            if (TextBox41.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox41.Text);
            }
            decimal c = a * b;
            TextBox19.Text = c.ToString();
            textchange();
            TextBox19.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox41_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox40.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox40.Text);
            }
            if (TextBox41.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox41.Text);
            }
            decimal c = a * b;
            TextBox19.Text = c.ToString();
            textchange();
            TextBox19.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox44_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox44.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox44.Text);
            }
            if (TextBox45.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox45.Text);
            }
            decimal c = a * b;
            TextBox46.Text = c.ToString();
            textchange();
            TextBox46.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox45_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox44.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox44.Text);
            }
            if (TextBox45.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox45.Text);
            }
            decimal c = a * b;
            TextBox46.Text = c.ToString();
            textchange();
            TextBox46.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox48_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox48.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox48.Text);
            }
            if (TextBox49.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox49.Text);
            }
            decimal c = a * b;
            TextBox49.Text = c.ToString();
            textchange();
            TextBox49.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox49_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox48.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox48.Text);
            }
            if (TextBox49.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox49.Text);
            }
            decimal c = a * b;
            TextBox49.Text = c.ToString();
            textchange();
            TextBox49.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox52_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox52.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox52.Text);
            }
            if (TextBox53.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox53.Text);
            }
            decimal c = a * b;
            TextBox54.Text = c.ToString();
            textchange();
            TextBox54.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox53_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox52.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox52.Text);
            }
            if (TextBox53.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox53.Text);
            }
            decimal c = a * b;
            TextBox54.Text = c.ToString();
            textchange();
            TextBox54.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox56_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox56.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox56.Text);
            }
            if (TextBox57.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox57.Text);
            }
            decimal c = a * b;
            TextBox58.Text = c.ToString();
            textchange();
            TextBox58.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox57_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox56.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox56.Text);
            }
            if (TextBox57.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox57.Text);
            }
            decimal c = a * b;
            TextBox58.Text = c.ToString();
            textchange();
            TextBox58.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void TextBox60_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox60.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox60.Text);
            }
            if (TextBox61.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox61.Text);
            }
            decimal c = a * b;
            TextBox62.Text = c.ToString();
            textchange();
            TextBox62.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }
        protected void TextBox61_textchanged(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (TextBox60.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(TextBox60.Text);
            }
            if (TextBox61.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox61.Text);
            }
            decimal c = a * b;
            TextBox62.Text = c.ToString();
            textchange();
            TextBox62.Focus();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void textchange()
        {
            decimal a;
            decimal b;
            decimal c;
            decimal d;
            decimal g;
            decimal h;
            decimal i;
            decimal j;
            decimal k;
            decimal l;
            decimal m;
            decimal o;
            decimal p;
            decimal q;
            decimal r;
            decimal s;
            decimal t;
            decimal u;
            decimal total;
            if (row1_txt_debit.Text == "")
            {
                a = 0;
            }
            else
            {
                a = decimal.Parse(row1_txt_debit.Text);
            }
            if (TextBox3.Text == "")
            {
                b = 0;
            }
            else
            {
                b = decimal.Parse(TextBox3.Text);
            }
            if (TextBox5.Text == "")
            {
                c = 0;
            }
            else
            {
                c = decimal.Parse(TextBox5.Text);
            }
            if (TextBox7.Text == "")
            {
                d = 0;
            }
            else
            {
                d = decimal.Parse(TextBox7.Text);
            }
            if (TextBox9.Text == "")
            {
                g = 0;
            }
            else
            {
                g = decimal.Parse(TextBox9.Text);
            }
            if (TextBox11.Text == "")
            {
                j = 0;
            }
            else
            {
                j = decimal.Parse(TextBox11.Text);
            }
            if (TextBox13.Text == "")
            {
                k = 0;
            }
            else
            {
                k = decimal.Parse(TextBox13.Text);
            }
            if (TextBox15.Text == "")
            {
                l = 0;
            }
            else
            {
                l = decimal.Parse(TextBox15.Text);
            }
            if (TextBox16.Text == "")
            {
                m = 0;
            }
            else
            {
                m = decimal.Parse(TextBox16.Text);
            }
            if (TextBox19.Text == "")
            {
                o = 0;
            }
            else
            {
                o = decimal.Parse(TextBox19.Text);
            }
            if (TextBox46.Text == "")
            {
                p = 0;
            }
            else
            {
                p = decimal.Parse(TextBox46.Text);
            }
            if (TextBox50.Text == "")
            {
                q = 0;
            }
            else
            {
                q = decimal.Parse(TextBox50.Text);
            }

            if (TextBox54.Text == "")
            {
                r = 0;
            }
            else
            {
                r = decimal.Parse(TextBox54.Text);
            }
            if (TextBox58.Text == "")
            {
                s = 0;
            }
            else
            {
                s = decimal.Parse(TextBox58.Text);
            }
            if (TextBox62.Text == "")
            {
                t = 0;
            }
            else
            {
                t = decimal.Parse(TextBox62.Text);
            }

            //if (txtDiscount.Text == "")
            //{
            //    u = 0;
            //}
            //else
            //{
            //    u = decimal.Parse(txtDiscount.Text);
            //}

            total = a + b + c + d + g + j + k + l + m + o + p + q + r + s + t;
            txtAmount.Text = total.ToString();
            if (txtAmount.Text == "")
            {
                h = 0;
            }
            else
            {
                h = decimal.Parse(txtAmount.Text);
            }
            if (txtTax.Text == "")
            {
                i = 0;
            }
            else
            {
                i = decimal.Parse(txtTax.Text);
            }
            decimal n = h + i;
            txtTotalAmount.Text = n.ToString();
            if (lbltotalInvoiceAmount.Text == "")
            {
                txttotalInvoiceAmount.Text = n.ToString();
            }
            else
            {
                lbltotalInvoiceAmount.Text = n.ToString();
            }

            //if (txtDiscount.Text != "")
            //{
            //    if (lbltotalInvoiceAmount.Text != "")
            //    {
            //        lblFinalAmount.Text = (decimal.Parse(lbltotalInvoiceAmount.Text) + decimal.Parse(txtDiscount.Text)).ToString();
            //    }
            //    else
            //    {
            //        if (txttotalInvoiceAmount.Text != "")
            //        {
            //            lblFinalAmount.Text = (decimal.Parse(txttotalInvoiceAmount.Text) + decimal.Parse(txtDiscount.Text)).ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    if (lbltotalInvoiceAmount.Text != "")
            //    {
            //        lblFinalAmount.Text = lbltotalInvoiceAmount.Text;
            //    }
            //    else
            //    {
            //        lblFinalAmount.Text = txttotalInvoiceAmount.Text;
            //    }
            //}

            UpdatePanel_Generate_Invoice.Update();
        }

        #endregion

        protected void btnPostVoucher_Click(object sender, EventArgs e)
        {
            Calculate_Hotel_TotalRate();

            Calculate_Restaurent_TotalRate();

            Calculate_Sight_Seeing_TotalRate();

            Calculate_Conference_TotalRate();

            Calculate_Gala_Dinner_TotalRate();

            Calculate_Guide_TotalRate();

            Calculate_Boat_TotalRate();

            Calculate_Coach_TotalRate();

               Calculate_Transfer_TotalRate();

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Voucher Posted Successfully..')", true);
        }

        protected void drpOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpOrderStatus.Text == "Closed")
                {
                    //if (DateTime.ParseExact(txtPeriodStayTo.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(todayDate, "dd/MM/yyyy", null))
                    //{
                    //    Master.DisplayMessage("Booking can be closed after tour To Date is gone.", "successMessage", 3000);
                    //}
                    //else
                    //{
                    btnPostVoucher.Visible = true;
                    // }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UpdatePanel_Generate_Invoice.Update();
            }

        }


        #region VOUCHERS

        #region HOTELS VOUCHER
        protected void Calculate_Hotel_TotalRate()
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



            DataSet dsrate = objgitpayment.Fetch_Hotel_Rate("GET_HOTELS_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (dsrate != null)
            {
                for (int j = 0; j < dsrate.Tables[0].Rows.Count; j++)
                {
                    HOTEL = dsrate.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                    ROOM_TYPE = dsrate.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();

                    adult = dsrate.Tables[0].Rows[j]["NO_OF_ADULTS"].ToString();
                    cwb = dsrate.Tables[0].Rows[j]["NO_OF_CWB"].ToString();
                    cnb = dsrate.Tables[0].Rows[j]["NO_OF_CNB"].ToString();
                    infant = dsrate.Tables[0].Rows[j]["NO_OF_INFANT"].ToString();

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
                        no_of_night = int.Parse(dsrate.Tables[0].Rows[j]["NO_OF_NIGHTS"].ToString());
                    }

                    SINGLE_ROOM_RATE = (no_of_single_room) * (single_rate) * (no_of_night);
                    DOUBLE_ROOM_RATE = (no_of_Double_room) * (double_rate) * (2) * (no_of_night);
                    TRIPLE_ROOM_RATE = (no_of_Triple_room) * (triple_rate) * (3) * (no_of_night);
                    CWB_RATE = (no_of_CWB_room) * (cwb_rate) * (no_of_night);
                    CNB_RATE = (no_of_CNB_room) * (cnb_rate) * (no_of_night);

                    TOTAL_RATE += (SINGLE_ROOM_RATE) + (DOUBLE_ROOM_RATE) + (TRIPLE_ROOM_RATE) + (CWB_RATE) + (CNB_RATE);


                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dsrate.Tables[0].Rows[j]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), dsrate.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), dsrate.Tables[0].Rows[j]["FROM_DATE"].ToString(), dsrate.Tables[0].Rows[j]["TO_DATE"].ToString(), no_of_night, no_of_single_room, no_of_Double_room, no_of_Triple_room, ROOM_TYPE, 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objFITPaymentStoreProcedure.insert_purchase_entry(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), dsrate.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), dsrate.Tables[0].Rows[j]["FROM_DATE"].ToString(), dsrate.Tables[0].Rows[j]["TO_DATE"].ToString(), no_of_night, no_of_single_room, no_of_Double_room, no_of_Triple_room, ROOM_TYPE, 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2);


                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }

            }



        }
        #endregion

        #region MEALS/RESTURANT VOUCHERS
        protected void Calculate_Restaurent_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;



            DataSet ds = objgitpayment.Fetch_Restaurant_Rate("GET_RESTURANTS_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string RESTURANT = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

                    adult = ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString();

                    cwb = ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString();

                    cnb = ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString();

                    infant = ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString();

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


                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[6]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);



                    objFITPaymentStoreProcedure.insert_purchase_entry(0, ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), "", TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[6]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), ds.Tables[0].Rows[i]["MEAL_DESC"].ToString(), int.Parse("0"), "", "", ds.Tables[0].Rows[i]["DATE"].ToString(), "", TOTAL_RATE.ToString(), int.Parse(Session["CompanyId"].ToString()), 2);



                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }
            }


        }
        #endregion

        #region SITE SEEING VOUCER
        protected void Calculate_Sight_Seeing_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds_SiteSupplier = objgitpayment.Fetch_Sight_Seeing_Supplier("FETCH_SITE_SUPPLIER_FOR_ACCOUNT", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds_SiteSupplier != null)
            {
                for (int j = 0; j < ds_SiteSupplier.Tables[0].Rows.Count; j++)
                {
                    DataSet ds = objgitpayment.Fetch_Site_Seeing_Rate("GET_SIGHT_SEEING_FOR_VOUCHERS", ds_SiteSupplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), int.Parse(Request.QueryString["QUOTEID"].ToString()));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string SITE = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

                        adult = ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString();

                        cwb = ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString();

                        cnb = ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString();

                        infant = ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString();

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

                    ViewState["TOATA_SITE_RATE"] = TOTAL_RATE;

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_SiteSupplier.Tables[0].Rows[j]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_SiteSupplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds_SiteSupplier.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    TOTAL_RATE = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string SITE = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

                        adult = ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString();

                        cwb = ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString();

                        cnb = ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString();

                        infant = ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString();

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

                        TOTAL_RATE = (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);


                        objFITPaymentStoreProcedure.insert_purchase_entry(0, ds.Tables[0].Rows[i]["SIGHT_SEEING_PACKAGE_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), "", TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse("0"), "", "", ds.Tables[0].Rows[i]["DATE"].ToString(), "", TOTAL_RATE.ToString(), int.Parse(Session["CompanyId"].ToString()), 2);
                        TOTAL_RATE = 0;
                    }




                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                    ViewState["TOATA_SITE_RATE"] = 0;
                }
            }

        }
        #endregion

        #region CONFERECNCE VOUCHER
        protected void Calculate_Conference_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;
            string HOTEL = "";
            string conferenceType = "";

            DataSet ds = objgitpayment.Fetch_Conference_Rate("GET_CONFERENCE_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HOTEL = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();
                    conferenceType = ds.Tables[0].Rows[i]["CONFERENCE_TYPE"].ToString();

                    adult = ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString();
                    cwb = ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString();
                    cnb = ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString();
                    infant = ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString();

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

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[12]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objgitpayment.conferencePurchaseInvoice(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[12]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2, ds.Tables[0].Rows[i]["CONFERENCE_DATE"].ToString(), conferenceType, "", ds.Tables[0].Rows[i]["TIME"].ToString());


                   // objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                   // objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                   // objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }
            }



        }
        #endregion

        #region GALA DINNER VOUCHER
        protected void Calculate_Gala_Dinner_TotalRate()
        {

            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            string HOTEL = "";
            string galaDinnerType = "";

            DataSet ds = objgitpayment.Fetch_Gala_Dinner_Rate("GET_GALA_DINNER_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HOTEL = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();
                    galaDinnerType = ds.Tables[0].Rows[i]["GALA_DINNER_TYPE"].ToString();

                    adult = ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString();
                    cwb = ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString();
                    cnb = ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString();
                    infant = ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString();

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

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[13]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objgitpayment.galaDinnerPurchaseDetails(0, HOTEL, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[13]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2, ds.Tables[0].Rows[i]["DINNER_DATE"].ToString(), galaDinnerType, "", ds.Tables[0].Rows[i]["TIME"].ToString());


                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }
            }

        }
        #endregion

        #region GUIDE VOUCHERS
        protected void Calculate_Guide_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
           
            int NO_OF_GUIDE = 0;
            string GUIDE = "";

            DataSet ds = objgitpayment.Fetch_Guide_Rate("GET_GUIDE_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GUIDE = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

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


                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_GUIDE) + (CHILD_RATE_PER_PERSON * NO_OF_GUIDE);

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, GUIDE, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[7]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objgitpayment.conferencePurchaseInvoice(0, GUIDE, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[7]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2, "", "", ds.Tables[0].Rows[i]["NO_OF_GUIDE"].ToString(), ds.Tables[0].Rows[i]["TIME"].ToString());


                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }
            }


        }
        #endregion

        #region BOAT VOUCHERS
        protected void Calculate_Boat_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;

            int NO_OF_BOATS = 0;

            string BOAT = "";

            DataSet ds = objgitpayment.Fetch_Boat_Rate("GET_BOAT_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BOAT = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

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


                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * NO_OF_BOATS) + (CHILD_RATE_PER_PERSON * NO_OF_BOATS);

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, BOAT, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[10]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objgitpayment.conferencePurchaseInvoice(0, BOAT, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[10]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2, ds.Tables[0].Rows[i]["DATE"].ToString(), "", ds.Tables[0].Rows[i]["NO_OF_BOATS"].ToString(), ds.Tables[0].Rows[i]["TIME"].ToString());


                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;

                }
            }

        }
        #endregion

        #region COACH VOUCHERS
        protected void Calculate_Coach_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            Decimal COACH_RATE = 0;

            string COACH = "";

            DataSet ds = objgitpayment.Fetch_Coach_Rate("GET_COACH_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    COACH = ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString();

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

                    TOTAL_RATE += (ADULT_RATE_PER_PERSON * Convert.ToDecimal(ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString()));

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds.Tables[0].Rows[i]["SUPPLIER_ID"].ToString(), "S");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, COACH, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[5]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objgitpayment.conferencePurchaseInvoice(0, COACH, TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds.Tables[0].Rows[i]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[5]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", 0, 0, 0, 0, "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2, ds.Tables[0].Rows[i]["DATE"].ToString(), "", ds.Tables[0].Rows[i]["NO_OF_COACHS"].ToString(), ds.Tables[0].Rows[i]["TIME"].ToString());


                   // objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                   // objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", TOTAL_RATE.ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    TOTAL_RATE = 0;
                }
            }

        }
        #endregion

        #region TRANSPORT RATE
        protected void Calculate_Transfer_TotalRate()
        {
            Decimal ADULT_RATE_PER_PERSON = 0;
            Decimal CHILD_RATE_PER_PERSON = 0;
            int NO_OF_ADULT = 0;
            int NO_OF_CHILD = 0;

            DataSet ds_Supplier = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_TRANSFER_SUPPLIER", int.Parse(Request.QueryString["QUOTEID"].ToString()));

            DataSet ds = objgitpayment.Fetch_Sight_Seeing_Supplier("GET_TRANSFER_FOR_VOUCHERS", int.Parse(Request.QueryString["QUOTEID"].ToString()));
            if (ds != null)
            {
                for (int j=0;j<ds_Supplier.Tables[0].Rows.Count;j++)
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

                ViewState["TOATA_SITE_RATE"] = TOTAL_RATE;

                DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_Supplier.Tables[0].Rows[j]["SUPPLIER_ID"].ToString(), "S");

                DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_Supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), ds_Supplier.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString(), TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[9]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                TOTAL_RATE = 0;

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
                    TOTAL_RATE = (ADULT_RATE_PER_PERSON * NO_OF_ADULT) + (CHILD_RATE_PER_PERSON * NO_OF_CHILD);

                    objFITPaymentStoreProcedure.insert_purchase_entry(0, ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString(), TextBox20.Text, int.Parse(Session["usersid"].ToString()), "", TOTAL_RATE.ToString(), "", TOTAL_RATE.ToString(), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["usersid"].ToString()), ds_sup_type.Tables[0].Rows[9]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(adult), int.Parse(cwb), int.Parse(cnb), int.Parse(infant), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds.Tables[0].Rows[i]["GIT_TRANSFER_PACKAGE_DETAIL_ID"].ToString()), "", "", todayDate, "", TOTAL_RATE.ToString(), int.Parse(Session["CompanyId"].ToString()), 2);
                }
                //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                //objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                //objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), todayDate, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", ViewState["TOATA_SITE_RATE"].ToString(), "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                TOTAL_RATE = 0;
                ViewState["TOATA_SITE_RATE"] = 0;
            }

        }
        }
        #endregion

        #endregion
    }
}