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

namespace CRM.WebApp.Views.Account
{
    public partial class GenerateInvoice : System.Web.UI.Page
    {
        CRM.DataAccess.Account.GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();

        CRM.DataAccess.Account.InvoiceThb objInvoiceThb = new CRM.DataAccess.Account.InvoiceThb();

        PurchaseVoucherStoredProcedure objPurchaseVoucherStoredProcedure = new PurchaseVoucherStoredProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();

        string sa = "0";
        int accid = 0;
        int detailid = 0;

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
        string adults;
        string cwb;
        string cnb;
        string infant;

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 254);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataSet ds = objgenerateInvoiceStoredProcedure.GetPaymentMode("FETCH_PAYMENT_MODE");
                binddropdownlist(drpPaymentMode, ds);
                DataSet ds2 = objgenerateInvoiceStoredProcedure.GetOrderStatus("FETCH_ORDER_STATUS");
                binddropdownlist(drpOrderStatus, ds2);
                DataSet ds3 = objgenerateInvoiceStoredProcedure.GetOrderStatus("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(DropDownList1, ds3);
                DataSet ds1 = objgenerateInvoiceStoredProcedure.GetCurrency("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                binddropdownlist(drpCurrency, ds1);

                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    DataSet ds_manual = objgenerateInvoiceStoredProcedure.fetchismanual("FETCH_IS_MANUAL", Request.QueryString["QUOTEID"].ToString());
                    if (ds_manual.Tables[0].Rows[0]["IS_MANUAL"].ToString() == "True")
                    {
                        btnCopy.Visible = false;
                    }

                    string saleid = Request.QueryString["TOURID"].ToString();
                    string quoteid = Request.QueryString["QUOTEID"].ToString();
                    Session["quoteid"] = Request.QueryString["QUOTEID"].ToString();
                    ViewState["quote"] = Request.QueryString["QUOTEID"].ToString();
                    txtQuote.Text = quoteid;
                    txtQuote.ReadOnly = true;
                    TextBox20.ReadOnly = true;
                    Session["salesinvoiceid"] = Request.QueryString["TOURID"].ToString();
                    DataSet DS = objgenerateInvoiceStoredProcedure.FetchDataForInvoice("FETCH_DATA_FOR_GENREATE_INVOICE", quoteid, saleid);
                    drpOrderStatus.SelectedValue = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();

                    if (DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString() == "Canceled")
                    {
                        btnSave.Visible = false;
                        btnSendInvoice.Visible = false;
                        UpdatePanel_Generate_Invoice.Update();
                    }

                    txtorderplacedby.Text = DS.Tables[0].Rows[0]["ORDER_PLACED_BY"].ToString();
                    txtpersonemail.Text = DS.Tables[0].Rows[0]["PERSON_EMAIL"].ToString();

                    

                    lnkbtn.Attributes.Add("style", "display");
                    lnkbtn.HRef = "~/Views/FIT/Invoices/" + saleid + "/Invoice.pdf";


                    txtclientname.Text = DS.Tables[0].Rows[0]["CLIENT_NAME"].ToString();

                    txtDiscount.Text = DS.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString();

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

                    ViewState["RowCount"] = DS.Tables[1].Rows.Count - 1;
                    AddDescription(GridDetails, UpdatePanel_Generate_Invoice);
                    fillDetailsEditMode(DS);

                    String name = DS.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    DataSet DSFOREMP = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                    Session["rel_sr_no"] = DSFOREMP.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    Session["uid"] = DSFOREMP.Tables[0].Rows[0]["USER_ID"].ToString();
                    DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                    Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
                    DataSet dscredit = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                    lblcreditlimitAmount.Text = dscredit.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                    lblcurrentusableamount.Text = dscredit.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();

                    DataTable DTMODE = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
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

                    DataSet dsin = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(txtQuote.Text));

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

                    if (txtDiscount.Text != "")
                    {
                        lblFinalAmount.Text = (decimal.Parse(lbltotalInvoiceAmount.Text) + decimal.Parse(txtDiscount.Text)).ToString();
                    }
                    else
                    {
                        lblFinalAmount.Text = lbltotalInvoiceAmount.Text;
                    }

                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", TextBox20.Text, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString());
                    //if (ds_vn_check != null)
                    //{
                    //    if (ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != "")
                    //    {
                    //        //disable_control();
                    //    }
                    //}

                    DataSet dsclose = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", TextBox20.Text.Trim());
                    if (dsclose != null)
                    {

                        if (Convert.ToString(dsclose.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                        {
                            btnclose1.Visible = false;
                            btnSave.Visible = false;
                        }
                        else
                        {
                            btnclose1.Visible = true;
                        }

                        if (Convert.ToString(dsclose.Tables[0].Rows[0]["CLOSE_2"]) == "True")
                        {
                            btnclose2.Visible = false;
                        }
                        else
                        {
                            if (Convert.ToString(dsclose.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                            {
                                btnclose2.Visible = true;
                            }
                            else
                            {
                                btnclose2.Visible = false;
                            }
                        }



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
                    ViewState["RowCount"] = 4;
                    for (int i = 0; i < 5; i++)
                    {
                        AddDescription(GridDetails, UpdatePanel_Generate_Invoice);
                    }

                    drpOrderStatus.Text = "Reconfirmed";
                    txtQuote.ReadOnly = true;
                    TextBox20.ReadOnly = true;
                    Session["sa"] = "0";
                    drpCurrency.SelectedValue = "USD";
                    ViewState["INVOICE"] = "";

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
            r.SelectedValue = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpOrderStatus.Text == "Trash")
            {
                bool flag = true;
                DataSet ds_purchase = objgenerateInvoiceStoredProcedure.fetch_purchase_voucher("FETCH_VOUCHER_FROM_INVOICE_NO", TextBox20.Text);

                for (int i = 0; i < ds_purchase.Tables[0].Rows.Count; i++)
                {
                    if (ds_purchase.Tables[0].Rows[i]["VOUCHER_NO"].ToString() != "")
                    {
                        flag = false;
                        break;

                    }
                }

                if (flag == true)
                {
                    DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    for (int i = 0; i < ds_purchase.Tables[0].Rows.Count; i++)
                    {
                        objgenerateInvoiceStoredProcedure.update_voucher_status_account_header("UPDATE_VOUCHER_STATUS_ON_TRASH", ds_vsstatus.Tables[0].Rows[7]["AutoSearchResult"].ToString(), ds_purchase.Tables[0].Rows[i]["PURCHASE_INVOICE_NO"].ToString(), 7);

                    }
                    objgenerateInvoiceStoredProcedure.update_voucher_status_account_header("UPDATE_VOUCHER_STATUS_ON_TRASH", ds_vsstatus.Tables[0].Rows[7]["AutoSearchResult"].ToString(), TextBox20.Text, 9);
                    objgenerateInvoiceStoredProcedure.update_sales_status_account_header("UPDATE_INVOICE_STATUS", drpOrderStatus.Text, TextBox20.Text);

                    objgenerateInvoiceStoredProcedure.update_quote_status_quote_master("UPDATE_QUOTE_STATUS_ON_TRASH", int.Parse(txtQuote.Text));

                    objFITPaymentStoreProcedure.edit_current_usable_MINUS(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount.Text));

                    btnSave.Visible = false;
                    btnSendInvoice.Visible = false;
                    btnCopy.Visible = true;
                    Master.DisplayMessage("Invoice Trashed Successfully.", "successMessage", 3000);
                }
                else
                {
                    Master.DisplayMessage("Invoice can not trash due to Voucher No. is generated.", "successMessage", 3000);
                }
                UpdatePanel_Generate_Invoice.Update();


            }
            else
            {

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
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                DataTable DTMODE = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
                if (drpPaymentMode.Text == DTMODE.Rows[0]["AutoSearchResult"].ToString())
                {
                    cash_flag = false;
                }

                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    //New code 27th june
                    string lbltotalInvoiceAmountCheck = "";
                    lbltotalInvoiceAmountCheck = (decimal.Parse(txtTotalAmount.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                    if (cash_flag == true)
                    {
                        if (decimal.Parse(lblcurrentusableamount.Text) < Convert.ToDecimal(lbltotalInvoiceAmountCheck))
                        {
                            Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
                            error_flag = false;
                        }
                    }


                }
                else
                {

                    if (cash_flag == true)
                    {
                        if (decimal.Parse(lblcurrentusableamount.Text) < g)
                        {
                            Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 3000);
                            error_flag = false;
                        }
                    }

                }






                //change

                if (error_flag == true)
                {
                    if (txtNoOfAdult.Text == "")
                    {
                        txtNoOfAdult.Text = "0";
                    }
                    if (txtNoOfChild.Text == "")
                    {
                        txtNoOfChild.Text = "0";
                    }
                    if (txtNoOfCWB.Text == "")
                    {
                        txtNoOfCWB.Text = "0";
                    }
                    if (txtNoOfCNB.Text == "")
                    {
                        txtNoOfCNB.Text = "0";
                    }
                    if (txtNoOfInfant.Text == "")
                    {
                        txtNoOfInfant.Text = "0";
                    }
                    if (txtNoOfNights.Text == "")
                    {
                        txtNoOfNights.Text = "0";
                    }


                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                    {
                        string QUTE = Request.QueryString["QUOTEID"].ToString();
                        DataTable DTTOUR = objFITPaymentStoreProcedure.FETCH_TOUR_ID_FROM_QUOTE_ID(QUTE);
                        int TOUR = int.Parse(DTTOUR.Rows[0]["TOUR_ID"].ToString());
                        CRM.DataAccess.Account.GenerateInvoiceSp objinsertupdatetour = new CRM.DataAccess.Account.GenerateInvoiceSp();

                        string[] descAmount = new string[5];
                        descAmount = defaultAmount();
                        DataSet dstour = objinsertupdatetour.insert_sales_fare_tour(int.Parse(QUTE), TOUR, int.Parse(descAmount[0].ToString()), int.Parse(descAmount[1].ToString()), int.Parse(descAmount[2].ToString()), int.Parse(descAmount[3].ToString()), int.Parse(descAmount[4].ToString()), txtclientname.Text, txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, txtTotalAmount.Text, int.Parse(txtNoOfAdult.Text), int.Parse(txtNoOfChild.Text), int.Parse(txtNoOfCWB.Text), int.Parse(txtNoOfCNB.Text), int.Parse(txtNoOfInfant.Text), Session["uid"].ToString(), drpOrderStatus.Text);

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
                                objFITPaymentStoreProcedure.edit_current_usable_MINUS(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount1));
                                ViewState["old_amount"] = txtTotalAmount.Text;
                            }
                            else
                            {
                                objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount1));
                                ViewState["old_amount"] = txtTotalAmount.Text;
                            }
                        }
                    }
                    else
                    {
                        CRM.DataAccess.Account.GenerateInvoiceSp objinsertupdatetourupdate = new CRM.DataAccess.Account.GenerateInvoiceSp();

                        string[] descAmount = new string[5];
                        descAmount = defaultAmount();

                        DataSet dstour = objinsertupdatetourupdate.insert_sales_fare_tour(0, 0, int.Parse(descAmount[0].ToString()), int.Parse(descAmount[1].ToString()), int.Parse(descAmount[2].ToString()), int.Parse(descAmount[3].ToString()), int.Parse(descAmount[4].ToString()), txtclientname.Text, txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, txtTotalAmount.Text, int.Parse(txtNoOfAdult.Text), int.Parse(txtNoOfChild.Text), int.Parse(txtNoOfCWB.Text), int.Parse(txtNoOfCNB.Text), int.Parse(txtNoOfInfant.Text), Session["uid"].ToString(), drpOrderStatus.Text);
                        txtQuote.Text = dstour.Tables[0].Rows[0]["QUOTE"].ToString();
                        ViewState["quote"] = dstour.Tables[0].Rows[0]["QUOTE"].ToString();
                        objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(txttotalInvoiceAmount.Text));



                    }

                    //txtAmount.Text = F.ToString();

                    /*NEW MANUALLY CREATE INVOICE*/
                    CRM.DataAccess.Account.GenerateInvoiceSp objinsertupdateheader = new CRM.DataAccess.Account.GenerateInvoiceSp();

                    DataTable dt = objinsertupdateheader.insert_sales_invoice_header(int.Parse(Session["sa"].ToString()), int.Parse(txtQuote.Text), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, decimal.Parse(txtAmount.Text), decimal.Parse(txtTax.Text), decimal.Parse(txtTotalAmount.Text), int.Parse(txtNoOfAdult.Text), int.Parse(txtNoOfChild.Text), int.Parse(txtNoOfCWB.Text), int.Parse(txtNoOfCNB.Text), int.Parse(txtNoOfInfant.Text), drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, TextBox42.Text,txtorderplacedby.Text,txtpersonemail.Text);
                    if (dt.Rows[0]["INVOICE_ID"].ToString() == "" || dt.Rows[0]["INVOICE_ID"].ToString() == "null")
                    {
                    }
                    else
                    {
                        TextBox20.Text = dt.Rows[0]["INVOICE_ID"].ToString();
                        ViewState["INVOICE"] = dt.Rows[0]["INVOICE_ID"].ToString();


                        string result;
                        System.DateTime today = DateTime.Now;

                        result = today.ToString("dd/MM/yyyy");



                        DataSet ds11 = objFITPaymentStoreProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", dt.Rows[0]["INVOICE_ID"].ToString());
                        DataSet ds6 = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds11.Tables[0].Rows[0]["CUST_ID"].ToString(), ds11.Tables[0].Rows[0]["FLAG"].ToString());
                        DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
                        DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                        DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                        DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));

                        DataSet ds_rate = objFITPaymentStoreProcedure.fetch_conversion_rate();
                        string total_sales_amount = (decimal.Parse(txttotalInvoiceAmount.Text) * decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();


                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dt.Rows[0]["INVOICE_ID"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_sales_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dt.Rows[0]["INVOICE_ID"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", total_sales_amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[11]["AutoSearchResult"].ToString(), dt.Rows[0]["INVOICE_ID"].ToString(), result, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, total_sales_amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    }
                    if (int.Parse(Session["sa"].ToString()) == 0)
                    {
                        Session["sa"] = dt.Rows[0]["sales_invoice"].ToString();
                    }
                    else
                    {

                    }


                    // DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
                    DataSet dscurrency = objFITPaymentStoreProcedure.fetch_paymentmode("FETCH_ALL_CURRENCY_NAME");

                    if (lbltotalInvoiceAmount.Text != "")
                    {
                        objgenerateInvoiceStoredProcedure.update_discount("UPDATE_SALES_INVOICE_DISCOUNT_AMOUNT", TextBox20.Text, lbltotalInvoiceAmount.Text, txtDiscount.Text);

                        if (txtDiscount.Text != "")
                        {
                            if (Request.QueryString["QUOTEID"].ToString() != null)
                            {
                                objgenerateInvoiceStoredProcedure.update_discount_tour_quote("INSERT_DISCOUNT_AMOUNT_GENERATE_INVOICE", int.Parse(Request.QueryString["QUOTEID"].ToString()), int.Parse(Request.QueryString["TOURID"].ToString()), txtDiscount.Text, decimal.Parse(lblFinalAmount.Text));
                            }
                        }
                    }


                    /*Insert Sales Invoice detail Manually*/

                    insertUpdateInoiceDetails(int.Parse(Session["sa"].ToString()));

                    //DataSet dso = objinsertupdateheader.insert_sales_invoice_details(int.Parse(Session["sa"].ToString()), int.Parse(ViewState["s1"].ToString()), int.Parse(ViewState["s2"].ToString()), int.Parse(ViewState["s3"].ToString()), int.Parse(ViewState["s4"].ToString()), int.Parse(ViewState["s5"].ToString()), int.Parse(ViewState["s6"].ToString()), int.Parse(ViewState["s7"].ToString()), int.Parse(ViewState["s8"].ToString()), int.Parse(ViewState["s9"].ToString()), int.Parse(ViewState["s10"].ToString()), int.Parse(ViewState["s11"].ToString()), int.Parse(ViewState["s12"].ToString()), int.Parse(ViewState["s13"].ToString()), int.Parse(ViewState["s14"].ToString()), int.Parse(ViewState["s15"].ToString()), row1_txt_debit.Text, TextBox3.Text, TextBox5.Text, TextBox7.Text, TextBox9.Text, TextBox11.Text, TextBox13.Text, TextBox15.Text, TextBox16.Text, TextBox19.Text, TextBox46.Text, TextBox50.Text, TextBox54.Text, TextBox58.Text, TextBox62.Text, TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox6.Text, TextBox8.Text, TextBox10.Text, TextBox12.Text, TextBox14.Text, TextBox17.Text, TextBox18.Text, TextBox43.Text, TextBox47.Text, TextBox51.Text, TextBox55.Text, TextBox59.Text, dscurrency.Tables[0].Rows[2]["CURRENCY_NAME"].ToString(), TextBox22.Text, TextBox24.Text, TextBox26.Text, TextBox28.Text, TextBox30.Text, TextBox32.Text, TextBox34.Text, TextBox36.Text, TextBox38.Text, TextBox40.Text, TextBox44.Text, TextBox48.Text, TextBox52.Text, TextBox56.Text, TextBox60.Text);

                    DataSet dso = objgenerateInvoiceStoredProcedure.getSalesInvoiceAmount(int.Parse(Session["sa"].ToString()));

                    DataSet cv_rate = objinsertupdateheader.fetch_conversion_rate();

                    string amount = (decimal.Parse(dso.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()) * decimal.Parse(cv_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())).ToString();
                    ViewState["AMT"] = amount;
                    txtAmount.Text = dso.Tables[0].Rows[0]["AMOUNT"].ToString();
                    txtTax.Text = dso.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();
                    txtTotalAmount.Text = dso.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    if (txtQuote.Text == "")
                    {
                        objFITPaymentStoreProcedure.INSERT_ORDER_STATUS("INSERT_UPDATE_ORDER_STAUS_FOR_BOOKING", int.Parse(Session["qid"].ToString()), "Reconfirmed");
                    }






                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                    {
                        DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(ViewState["INVOICE"].ToString(), "SALES");
                        objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ViewState["INVOICE"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[0]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", "", amount, TextBox42.Text, 1);
                        for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ViewState["INVOICE"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", amount, amount, TextBox42.Text, 2);
                            }
                            else if (i == 1)
                            {
                                objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ViewState["INVOICE"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), amount, "", amount, TextBox42.Text, 2);
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
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.10in</MarginTop>" +
                        "  <MarginLeft>0.10in</MarginLeft>" +
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
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/Receipt";
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
                        DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
                        if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
                        {
                            DataSet ds1 = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
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

                            }
                        }

                        Master.DisplayMessage("Invoice Updated Successfully.", "successMessage", 3000);
                    }
                    else
                    {

                        if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Invoices/" + Session["sa"].ToString() + "/")))
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + Session["sa"].ToString() + "/"));

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
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.50in</MarginTop>" +
                        "  <MarginLeft>0.10in</MarginLeft>" +
                        "  <MarginRight>0.10in</MarginRight>" +
                        "  <MarginBottom>0.50in</MarginBottom>" +
                        "</DeviceInfo>";



                        // quote_id = Page.Request.QueryString["QuoteId"].ToString();

                        ReportParameter[] parm = new ReportParameter[1];
                        parm[0] = new ReportParameter("SALES_INVOICE_ID", Session["sa"].ToString());
                        rptViewer1.ShowCredentialPrompts = false;
                        rptViewer1.ShowParameterPrompts = false;

                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/Receipt";
                        rptViewer1.ServerReport.SetParameters(parm);
                        rptViewer1.ServerReport.Refresh();

                        renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        rptViewer1.Visible = false;
                        Response.Clear();
                        using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + Session["sa"].ToString() + "/Invoice.pdf")))
                        {
                            fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                        }
                        Master.DisplayMessage("Invoice Created Successfully.", "successMessage", 3000);
                        Response.Redirect("~/Views/Account/AllInvoice.aspx");
                    }

                    UpdatePanel_Generate_Invoice.Update();

                }

            }
        }

        #region paymentdetails button

        public void drpPayment_SelectedIndexChanged(Object sender, EventArgs e)
        {
            DataTable DTMODE = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
            DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
            if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                lblcreditlimitAmount.Text = ds.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                lblcurrentusableamount.Text = ds.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                lblcrlimitdate.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                lblcurrentusablecurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
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
                    DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(txtQuote.Text));
                    //int.Parse(Session["quote_id"].ToString())
                    lbltotalInvoiceAmount.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
                    // Session["currancy"] = ds1.Tables[0].Rows[0]["CURRANCY_NAME"].ToString();
                }
                t1.Attributes.Add("style", "display");
                t2.Attributes.Add("style", "display");
                t3.Attributes.Add("style", "display");
                t4.Attributes.Add("style", "display");
            }
            else
            {

            }
            UpdatePanel_Generate_Invoice.Update();
            // credit_limit.Visible = true;
        }




        #endregion

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = DropDownList1.Text;
            DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
            Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
            Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();

            DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
            Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
            Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();
        }

        protected void txtQuote_TextChanged(object sender, EventArgs e)
        {
            string name = txtQuote.Text;
            string name1 = TextBox20.Text;
            DataSet DS = objgenerateInvoiceStoredProcedure.FetchDataForInvoice("FETCH_DATA_FOR_GENREATE_INVOICE", name, name1);
            if (DS.Tables[0].Rows.Count != 0)
            {
                drpOrderStatus.SelectedValue = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                txtclientname.Text = DS.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
                txtNoOfNights.Text = DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
                sa = DS.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString();
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
                drpPaymentMode.SelectedValue = DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();

                ViewState["RowCount"] = DS.Tables[1].Rows.Count - 1;
                AddDescription(GridDetails, UpdatePanel_Generate_Invoice);
                fillDetailsEditMode(DS);

                DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                lblcreditlimitAmount.Text = ds.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                lblcurrentusableamount.Text = ds.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                lblcrlimitdate.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                lblcurrentusablecurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                if (lblcurrentusableamount.Text == "")
                {
                    lblcurrentusableamount.Text = "0";
                }
                lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();

                DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(txtQuote.Text));

                lbltotalInvoiceAmount.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();

                UpdatePanel_Generate_Invoice.Update();
            }

        }

        #region AGENT'S AND SUPPLIER E-MAILS

        protected void SendMail()
        {
            #region AgentEmail Variable

            string supplierid;
            string hotelsupplierid;
            string hotelsupplieridupdate;
            string supplieridupdate;
            string supplier_email;
            string supplier_emailupdate;

            string stsupplierid;
            string stseeingsupplierid;
            string stsupplieridupdate;
            string stsupplier_email;
            string stsupplier_emailupdate;

            string trsupplierid;
            string trseeingsupplierid;
            string trsupplieridupdate;
            string trsupplier_email;
            string trsupplier_tfdetailid;

            string agentEmail = Session["email"].ToString();
            string fromdate;
            string todate;
            string clientname;
            string agentname;

            string EmailQoute_id = Session["quoteid"].ToString();

            string cc = ""; ;
            //string smtpport = "587";
            string fromemail = "";
            string toemail1 = "";
            //            string toemail1 = Session["email"].ToString();
            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[1]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                DataTable dt_supplier_id = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                SqlDataReader rdr = comm.ExecuteReader();
                dt_supplier_id.Load(rdr);


                string st_invoiceid = Session["salesinvoiceid"].ToString();
                DataTable sightsupplier_id = new DataTable();

                SqlCommand sightcomm = new SqlCommand("FETCH_SIGHT_SEEING_SUPPLIER_ID", conn);
                sightcomm.CommandType = CommandType.StoredProcedure;
                sightcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader stdr = sightcomm.ExecuteReader();
                sightsupplier_id.Load(stdr);


                string tr_invoiceid = Session["salesinvoiceid"].ToString();
                DataTable transsupplier_id = new DataTable();

                SqlCommand transcomm = new SqlCommand("FETCH_TRANSFER_SUPPLIER_ID", conn);
                transcomm.CommandType = CommandType.StoredProcedure;
                transcomm.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                SqlDataReader transdr = transcomm.ExecuteReader();
                transsupplier_id.Load(transdr);

                string invoice_id = Session["salesinvoiceid"].ToString();
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



                //string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf");
                //Attachment attachFile = new Attachment(filename);
                string filename01 = "~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf";

                string filename1 = HttpContext.Current.Request.MapPath("~/Views/FIT/Itinerary/" + Session["salesinvoiceid"].ToString() + "/Itinerary.pdf");
                Attachment attachFile1 = new Attachment(filename1);
                string filename11 = "~/Views/FIT/Itinerary/" + Session["salesinvoiceid"].ToString() + "/Itinerary.pdf";




            #endregion


                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                //  DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");


                //string body = "";
                //body = " Dear " + agentname.ToString() + "<br><br>Our Reference No. is : " + EmailQoute_id.ToString() + "<br><br>We are pleased to Reconfirm the booking  as attached .<br><br> Best Reagrds <br>Travelz Unlmited";
                string body = "";
                //   body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to fitops@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";
                body = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                body = body.Replace("&lt;%AGENTNAME%&gt;", agentname);
                body = body.Replace("&lt;%CLIENTNAME%&gt;", clientname);
                body = body.Replace("&lt;%PAYMENTMODE%&gt;", drpPaymentMode.Text);

                try
                {

                    MailMessage message = new MailMessage();

                    if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            fromemail = "fitops@travelzunlimited.com";
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                        {
                            fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else
                        {
                            fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                        {
                            toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }
                        else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            toemail1 = "fitops@travelzunlimited.com";
                        }
                        else
                        {
                            toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }

                    if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                        if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                        {
                            cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            cc = "fitops@travelzunlimited.com";
                        }

                        //else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                        //{
                        //    cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        //}

                        else
                        {
                            cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }

                        //cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString() + "," + TextBox21.Text;
                    }

                    string bcc = "";
                    if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                    {
                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                        if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                        {
                            bcc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                        }

                        else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                        {
                            bcc = "fitops@travelzunlimited.com";
                        }

                        //else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                        //{
                        //    bcc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        //}

                        else
                        {
                            bcc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }
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

                    if (bcc != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                    message.To.Add(new MailAddress(toemail1.ToString()));

                    string subjct = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                    subjct = subjct.Replace("<%CLIENTNAME%>", clientname);
                    subjct = subjct.Replace("<%FROMDATE%>", fromdate);
                    subjct = subjct.Replace("<%TODATE%>", todate);

                    message.Subject = subjct;
                    //   message.Subject = "Booking for " + clientname + "Arrival date : " + fromdate + "Departure date : " + todate;
                    //   message.Attachments.Add(attachFile);
                    message.Attachments.Add(attachFile1);

                    // EMAILS STORED IN DATABASE
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filename01, "Quotation.pdf", int.Parse(Session["usersid"].ToString()), 1);
                    //     objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filename01, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filename11, "Itinerary.pdf", int.Parse(Session["usersid"].ToString()), 2);

                    for (int i = 0; i < dt_supplier_id.Rows.Count; i++)
                    {
                        supplierid = dt_supplier_id.Rows[i]["SUPPLIER_ID"].ToString();
                        DataTable dt_hotelEmails = new DataTable();

                        SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                        email.CommandType = CommandType.StoredProcedure;
                        email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                        SqlDataReader rdremail = email.ExecuteReader();
                        dt_hotelEmails.Load(rdremail);

                        if (dt_hotelEmails.Rows.Count != 0)
                        {

                            hotelsupplierid = dt_hotelEmails.Rows[i]["SUPPLIER_ID"].ToString();
                            supplier_email = dt_hotelEmails.Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
                            string filename2 = HttpContext.Current.Request.MapPath("~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf");
                            Attachment attachFile2 = new Attachment(filename2);

                            message.Attachments.Add(attachFile2);

                            // EMAIL STORED IN DATABASE
                            string filename22 = "~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";
                            string filename23 = "HotelVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + ".pdf";
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filename22, filename23, int.Parse(Session["usersid"].ToString()), 2);
                        }
                    }
                    for (int i = 0; i < sightsupplier_id.Rows.Count; i++)
                    {
                        stsupplierid = sightsupplier_id.Rows[i]["SUPPLIER_ID"].ToString();
                        DataTable dt_sightSeeingEmails = new DataTable();

                        SqlCommand stsupplier_email1 = new SqlCommand("FETCH_SIGHT_SEEING_SPPLIER_EMAIL", conn);
                        stsupplier_email1.CommandType = CommandType.StoredProcedure;
                        stsupplier_email1.Parameters.Add("@INVOICE_ID", SqlDbType.Int).Value = int.Parse(st_invoiceid);
                        SqlDataReader rdrsightemail = stsupplier_email1.ExecuteReader();
                        dt_sightSeeingEmails.Load(rdrsightemail);

                        stseeingsupplierid = dt_sightSeeingEmails.Rows[i]["SUPPLIER_ID"].ToString();
                        supplier_email = dt_sightSeeingEmails.Rows[i]["SUPPLIER_REL_EMAIL"].ToString();


                        string filename4 = HttpContext.Current.Request.MapPath("~/Views/FIT/SightSeeingVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "SightSeeingVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + stseeingsupplierid.ToString() + ".pdf");
                        Attachment attachFile4 = new Attachment(filename4);

                        message.Attachments.Add(attachFile4);

                        // EMAIL STORED IN DATABASE
                        string filepath = "~/Views/FIT/SightSeeingVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "SightSeeingVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + stseeingsupplierid.ToString() + ".pdf";
                        string filename_ss = "SightSeeingVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + stseeingsupplierid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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


                        string filename5 = HttpContext.Current.Request.MapPath("~/Views/FIT/TransferVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "TransferVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf");
                        Attachment attachFile5 = new Attachment(filename5);

                        message.Attachments.Add(attachFile5);

                        // EMAIL STORED IN DATABASE
                        string filepath = "~/Views/FIT/TransferVoucher/" + Session["salesinvoiceid"].ToString() + "/" + "TransferVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf";
                        string filename_ss = "TransferVoucher" + " " + Session["salesinvoiceid"].ToString() + "-" + trseeingsupplierid.ToString() + "-" + trsupplier_tfdetailid.ToString() + ".pdf";
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), TextBox20.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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
                conn.Close();
            }
        }

        protected void sendhotelmail()
        {
            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[1]["AutoSearchResult"].ToString());

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

                    string filepath = "~/Views/FIT/Vouchers/" + Session["salesinvoiceid"].ToString() + "/" + Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + "HotelVoucher.pdf ";
                    string filename_ss = Session["salesinvoiceid"].ToString() + "-" + hotelsupplierid.ToString() + "HotelVoucher.pdf ";


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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), TextBox20.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 1);
                    }
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), TextBox20.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);

                }

            }
        }
        #endregion

        #region Manual Voucher Agent

        protected void sendInvoicetoAgentManually(string invoiceId, string invoice_no)
        {
            string fromemail = "";
            string to = "";
            string cc = "";
            string bcc = "";

            string fromdate;
            string todate;
            string clientname;
            string agentname;

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[26]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                MailMessage message = new MailMessage();
                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                DataTable dt_supplier_id = new DataTable();

                DataTable dt_agent = new DataTable();

                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL_MANUALLY", conn);
                agent_comm.CommandType = CommandType.StoredProcedure;
                agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(txtQuote.Text);
                SqlDataReader agent_rdr = agent_comm.ExecuteReader();
                dt_agent.Load(agent_rdr);

                fromdate =
                todate = dt_agent.Rows[0]["TOUR_TO_DATE"].ToString();
                clientname = dt_agent.Rows[0]["CLIENT"].ToString();
                agentname = dt_agent.Rows[0]["CUST_REL_NAME"].ToString();



                string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + invoiceId + "/Invoice.pdf");
                Attachment attachFile = new Attachment(filename);

                if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = Session["email"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        fromemail = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Supplier")
                    {

                    }

                    else
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        to = Session["email"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        to = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {

                    }

                    else
                    {
                        to = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = Session["email"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        cc = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                    {

                    }

                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }


                if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc = Session["email"].ToString();
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        bcc = "accounts@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {

                    }

                    else
                    {
                        bcc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }



                string body = "";

                body = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                body = body.Replace("&lt;%AGENTNAME%&gt;", agentname);
                body = body.Replace("&lt;%CLIENTNAME%&gt;", clientname);
                body = body.Replace("&lt;%PAYMENTMODE%&gt;", drpPaymentMode.Text);

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

                if (bcc != "")
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }
                message.To.Add(new MailAddress(to.ToString()));

                string subjct = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                subjct = subjct.Replace("<%CLIENTNAME%>", clientname);
                subjct = subjct.Replace("<%FROMDATE%>", fromdate);
                subjct = subjct.Replace("<%TODATE%>", todate);

                string filename1 = "~/Views/FIT/Invoices/" + invoiceId + "/Invoice.pdf";
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse("0"), invoice_no, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 1);
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse("0"), invoice_no, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);


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



        protected void SendMail1()
        {


            #region AgentEmail Variable
            string agentEmail = Session["email"].ToString();
            string fromdate;
            string todate;
            string clientname;
            string agentname;

            string EmailQoute_id = ViewState["quote"].ToString();
            string smtpemail = "kushal@flamingotravels.co.in";
            string smtppass = "dadashri";
            string smtphost = "smtpcorp.com";
            //string smtpport = "587";
            string fromemail = "accounts@travelzunlimited.com";
            string toemail1 = Session["email"].ToString();
            string cc = TextBox21.Text;
            string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
            DataTable dt_supplier_id = new DataTable();
            SqlConnection conn = new SqlConnection(str);

            conn.Open();
            string filename = HttpContext.Current.Request.MapPath("~/Views/FIT/Invoices/" + Session["sa"].ToString() + "/Invoice.pdf");
            Attachment attachFile = new Attachment(filename);


            DataTable dt_agent = new DataTable();

            SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
            agent_comm.CommandType = CommandType.StoredProcedure;
            agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ViewState["quote"].ToString());
            SqlDataReader agent_rdr = agent_comm.ExecuteReader();
            dt_agent.Load(agent_rdr);

            fromdate = dt_agent.Rows[0]["TOUR_FROM_DATE"].ToString();
            todate = dt_agent.Rows[0]["TOUR_TO_DATE"].ToString();
            clientname = dt_agent.Rows[0]["CLIENT"].ToString();
            agentname = dt_agent.Rows[0]["CUST_REL_NAME"].ToString();

            #endregion


            string body = "";
            body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to reservations@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                if (cc == "")
                {
                }
                else
                {
                    message.CC.Add(new MailAddress(cc));
                }
                message.To.Add(new MailAddress(toemail1.ToString()));
                //message.To.Add(new MailAddress(toemail2.ToString()));
                message.Subject = "Booking for " + clientname + "Arrival date : " + fromdate + "Departure date : " + todate;
                message.Attachments.Add(attachFile);
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
            conn.Close();
        }

        protected void SendMailformanualvoucher()
        {
            #region AgentEmail Variable
            string agentEmail = Session["email"].ToString();
            string fromdate;
            string todate;
            string clientname;
            string agentname;

            string EmailQoute_id = ViewState["quote"].ToString();
            string smtpemail;
            string smtppass;
            string smtphost;
            string smtpport;
            string fromemail = "gitops@travelzunlimited.com";
            string toemail1 = Session["email"].ToString();
            string cc = TextBox21.Text;
            string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
            DataTable dt_supplier_id = new DataTable();
            SqlConnection conn = new SqlConnection(str);

            conn.Open();

            DataTable dt_agent = new DataTable();

            SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
            agent_comm.CommandType = CommandType.StoredProcedure;
            agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ViewState["quote"].ToString());
            SqlDataReader agent_rdr = agent_comm.ExecuteReader();
            dt_agent.Load(agent_rdr);

            fromdate = dt_agent.Rows[0]["TOUR_FROM_DATE"].ToString();
            todate = dt_agent.Rows[0]["TOUR_TO_DATE"].ToString();
            clientname = dt_agent.Rows[0]["CLIENT"].ToString();
            agentname = dt_agent.Rows[0]["CUST_REL_NAME"].ToString();

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();
            #endregion

            string body = "";
            body = " Dear " + agentname + ",<br><br>We are pleased to Reconfirm the booking for " + clientname + " as attached . Vouchers for Services and Service Itenary is attached with the mail. <br> We have received the payments from your credit account or credit card Payment details to be mentioned <br> If any amendments or changes in  the below please send by email to reservations@travelzunlimited.com <br>No Changes would be accepted 48 hours before the arrival of the booking. <br>Cancellation Policy Applicable as per the Hotels Policy.<br><br> Best Reagrds <br>Travelz Unlmited";

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                if (cc == "")
                {
                }
                else
                {
                    message.CC.Add(new MailAddress(cc));
                }
                message.To.Add(new MailAddress(toemail1.ToString()));
                DataSet dt_hotel_id = objgenerateInvoiceStoredProcedure.FetchDataForHotelManual("FETCH_ID_FOR_HOTEL_MANUAL_VOUCHER", ViewState["INVOICE"].ToString());
                for (int i = 0; i < dt_hotel_id.Tables[0].Rows.Count; i++)
                {
                    string filename2 = HttpContext.Current.Request.MapPath("~/Views/FIT/Manualvoucher/" + dt_hotel_id.Tables[0].Rows[0]["HOTEL_VOUCHER_ID"].ToString() + "/HotelVoucherManual.pdf");
                    Attachment attachFile2 = new Attachment(filename2);

                    message.Attachments.Add(attachFile2);

                }
                DataSet dt_sight_id = objgenerateInvoiceStoredProcedure.FetchDataForManualVoucher("FETCH_ID_FOR_SIGHTSEEING_MANUAL_VOUCHER", Session["quoteid"].ToString(), ViewState["INVOICE"].ToString());
                for (int i = 0; i < dt_sight_id.Tables[0].Rows.Count; i++)
                {
                    string filename4 = HttpContext.Current.Request.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + dt_sight_id.Tables[0].Rows[0]["SERVICE_VOUCHER_ID"].ToString() + "/SightSeeingVoucherManual.pdf");
                    Attachment attachFile4 = new Attachment(filename4);

                    message.Attachments.Add(attachFile4);

                }
                DataSet dt_transfer_id = objgenerateInvoiceStoredProcedure.FetchDataForManualVoucher("FETCH_ID_FOR_TRANSFER_MANUAL_VOUCHER", Session["quoteid"].ToString(), ViewState["INVOICE"].ToString());
                for (int i = 0; i < dt_transfer_id.Tables[0].Rows.Count; i++)
                {

                    string filename5 = HttpContext.Current.Request.MapPath("~/Views/FIT/TransferManualvoucher/" + dt_transfer_id.Tables[0].Rows[0]["TRANSFER_SERVICE_VOUCHER_ID"].ToString() + "/TransferVoucherManual.pdf");
                    Attachment attachFile5 = new Attachment(filename5);

                    message.Attachments.Add(attachFile5);

                }
                message.Subject = "Booking for " + clientname + "Arrival date : " + fromdate + "Departure date : " + todate;
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
            conn.Close();
        }

        #endregion

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

        protected void btnSendInvoice_Click(object sender, EventArgs e)
        {
            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {
                DataSet ds = objgenerateInvoiceStoredProcedure.fetchismanual("FETCH_IS_MANUAL", Session["quoteid"].ToString());
                if (ds.Tables[0].Rows[0]["IS_MANUAL"].ToString() == "True")
                {
                    sendInvoicetoAgentManually(Request.QueryString["TOURID"].ToString(), TextBox20.Text);

                    //SendMail1();
                    //SendMailformanualvoucher();
                }
                else
                {
                    SendMail();
                    Send_invoice_to_agent();
                    sendhotelmail();
                }
            }
            else
            {
                SendMail1();
            }
            Master.DisplayMessage("Invoice Sent Successfully.", "successMessage", 8000);
            UpdatePanel_Generate_Invoice.Update();
        }



        protected void disable_control()
        {
            btnSave.Visible = false;
        }

        protected void Send_invoice_to_agent()
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
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[1]["AutoSearchResult"].ToString());

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

                string filename1 = "~/Views/FIT/Invoices/" + Session["salesinvoiceid"].ToString() + "/Invoice.pdf";

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

                    }

                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }
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
                body = body.Replace("&lt;%PAYMENTMODE%&gt;", drpPaymentMode.Text);

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

                if (bcc != "")
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }
                message.To.Add(new MailAddress(to.ToString()));

                string subjct = ds_mailTemplate.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();

                subjct = subjct.Replace("<%CLIENTNAME%>", clientname);
                subjct = subjct.Replace("<%FROMDATE%>", fromdate);
                subjct = subjct.Replace("<%TODATE%>", todate);

                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), TextBox20.Text, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 1);
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), TextBox20.Text, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);


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

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (lblFinalAmount.Text == "")
            {
                lblFinalAmount.Text = "0";
            }
            lbltotalInvoiceAmount.Text = (decimal.Parse(lblFinalAmount.Text) - decimal.Parse(txtDiscount.Text)).ToString();
            UpdatePanel_Generate_Invoice.Update();
        }


        protected void btnCopy_Click(object sender, EventArgs e)
        {
            DataSet ds = objgenerateInvoiceStoredProcedure.fetch_all_data_Quote_id(int.Parse(txtQuote.Text));

            string recon_date = "";
            if (ds.Tables[0].Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString() != "")
            {
                DateTime RECONFIRM_dATE = DateTime.Parse(ds.Tables[0].Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString());
                recon_date = RECONFIRM_dATE.ToString("dd/MM/yyyy");
            }


            DataSet ds_quote_id = objgenerateInvoiceStoredProcedure.INSERT_TOUR_QUOTE_COPY(int.Parse(ds.Tables[0].Rows[0]["CURRANCY_ID"].ToString()), ds.Tables[0].Rows[0]["TOTAL_QUOTED_ADULT"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_QUOTED_CWB"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_QUOTED_CNB"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_QUOTED_INFANT"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString(),
                // int.Parse(ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString()),
                               3,
                                ds.Tables[0].Rows[0]["CANCELLATION_FEES"].ToString(),
                               int.Parse(ds.Tables[0].Rows[0]["CREATE_BY"].ToString()),
                // ds.Tables[0].Rows[0]["CREATE_DATE"].ToString(),
                                ds.Tables[0].Rows[0]["MODIFY_BY"].ToString(),
                //  ds.Tables[0].Rows[0]["MODIFY_DATE"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_ADULT_COST_ON_SINGLE_SHARE"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_ADULT_COST_ON_DOUBLE_SHARE"].ToString(),
                                ds.Tables[0].Rows[0]["TOTAL_ADULT_COST_ON_TRIPLE_SHARE"].ToString(),
                                ds.Tables[0].Rows[0]["NO_OF_PERSON_ON_SINGLE_SHARE"].ToString(),
                                ds.Tables[0].Rows[0]["NO_OF_PERSON_ON_DOUBLE_SHARE"].ToString(),
                                ds.Tables[0].Rows[0]["NO_OF_PERSON_ON_TRIPLE_SHARE"].ToString(),
                // ds.Tables[0].Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString(),
                               recon_date,
                                ds.Tables[0].Rows[0]["AUTHORISATION_NO"].ToString(),
                                ds.Tables[0].Rows[0]["NO_OF_PERSON_ON_CHILD_WITH_BED"].ToString(),
                                ds.Tables[0].Rows[0]["NO_OF_PERSON_ON_CHILD_WITH_NO_BED"].ToString(),
                                ds.Tables[0].Rows[0]["DISCOUNT_AMOUNT"].ToString()
                                );
            string tour_from_date = "";
            if (ds.Tables[1].Rows[0]["TOUR_FROM_DATE"].ToString() != "")
            {
                DateTime FROM_DATE = DateTime.Parse(ds.Tables[1].Rows[0]["TOUR_FROM_DATE"].ToString());
                tour_from_date = FROM_DATE.ToString("dd/MM/yyyy");
            }

            string tour_to_date = "";
            if (ds.Tables[1].Rows[0]["TOUR_TO_DATE"].ToString() != "")
            {
                DateTime TO_DATE = DateTime.Parse(ds.Tables[1].Rows[0]["TOUR_TO_DATE"].ToString());
                tour_to_date = TO_DATE.ToString("dd/MM/yyyy");
            }



            string arr_time = "";
            if (ds.Tables[1].Rows[0]["ARRIVAL_TIME"].ToString() != "")
            {
                DateTime ARRIVAL_TIME = DateTime.Parse(ds.Tables[1].Rows[0]["ARRIVAL_TIME"].ToString());
                arr_time = ARRIVAL_TIME.ToShortTimeString();
            }

            string dep_time = "";
            if (ds.Tables[1].Rows[0]["DEPARTURE_TIME"].ToString() != "")
            {
                DateTime DEPARTURE_TIME = DateTime.Parse(ds.Tables[1].Rows[0]["DEPARTURE_TIME"].ToString());
                dep_time = DEPARTURE_TIME.ToShortTimeString();
            }

            bool fav_package = false;
            if (ds.Tables[1].Rows[0]["MY_FAVOURITE_PACKAGE"].ToString() == "True")
            {
                fav_package = true;
            }

            DataSet ds_tour_id = objgenerateInvoiceStoredProcedure.insert_FARE_TOUR_MASTER(
                ds.Tables[1].Rows[0]["TOUR_SHORT_NAME"].ToString(),
                ds.Tables[1].Rows[0]["TOUR_SUB_TYPE_ID"].ToString(),

              tour_from_date,

              tour_to_date,
             ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString(),
               ds.Tables[1].Rows[0]["NO_OF_NIGHTS"].ToString(),
               ds.Tables[1].Rows[0]["CREATED_BY"].ToString(),

                ds.Tables[1].Rows[0]["TOUR_ITENARY_TYPE_ID"].ToString(),
                ds.Tables[1].Rows[0]["TOUR_TYPE_ID"].ToString(),
                ds.Tables[1].Rows[0]["NO_OF_ADULTS"].ToString(),
                ds.Tables[1].Rows[0]["NO_OF_CHILD"].ToString(),
                ds.Tables[1].Rows[0]["NO_OF_CWB"].ToString(),
                ds.Tables[1].Rows[0]["NO_OF_CNB"].ToString(),
                ds.Tables[1].Rows[0]["NO_OF_INFANT"].ToString(),

           arr_time,

           dep_time,
                ds.Tables[1].Rows[0]["ARRIVAL_FLIGHT"].ToString(),
                ds.Tables[1].Rows[0]["DEPARTURE_FLIGHT"].ToString(),
                ds.Tables[1].Rows[0]["CLIENT_NAME"].ToString(),

                 ds.Tables[1].Rows[0]["REMARKS"].ToString(),
                ds.Tables[1].Rows[0]["FIT_PACKAGE_ID"].ToString(),

               fav_package,
                ds.Tables[1].Rows[0]["CLIENT_SURNAME"].ToString(),
                ds.Tables[1].Rows[0]["CLIENT_TITLE"].ToString(),
                ds.Tables[1].Rows[0]["PACKAGE_FLAG_ID"].ToString()
                );




            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {

                string date = "";
                if (ds.Tables[4].Rows[i]["DATE"].ToString() != "")
                {
                    DateTime D_DATE = DateTime.Parse(ds.Tables[4].Rows[i]["DATE"].ToString());
                    date = D_DATE.ToString("dd/MM/yyyy");
                }

                string ss_pay_date = "";
                if (ds.Tables[4].Rows[i]["PAYMENT_DUE_DATE"].ToString() != "")
                {
                    DateTime PAYMENT_DUE_dATE = DateTime.Parse(ds.Tables[4].Rows[i]["PAYMENT_DUE_DATE"].ToString());
                    ss_pay_date = PAYMENT_DUE_dATE.ToString("dd/MM/yyyy");
                }

                bool ss_package_flag = false;
                if (ds.Tables[4].Rows[i]["PACKAGE_FLAG"].ToString() == "True")
                {
                    ss_package_flag = true;
                }

                objgenerateInvoiceStoredProcedure.COPY_SERVICE_CART(ds.Tables[4].Rows[i]["TRANSFER_SIGHT_SEEING_PACKAGE_ID"].ToString(),
                    ds.Tables[4].Rows[i]["TRANSFER_SIGHT_SEEING_PACKAGE_FLAG"].ToString(),

                    date,
                    ds.Tables[4].Rows[i]["TIME"].ToString(),
                    //ds.Tables[4].Rows[i]["ORDER_STATUS"].ToString(),
                    "2",
                    ds.Tables[4].Rows[i]["CREATED_BY"].ToString(),
                    ds.Tables[4].Rows[i]["NO_OF_MEALS"].ToString(),
                    int.Parse(ds_quote_id.Tables[0].Rows[0][0].ToString()),
                    ds.Tables[4].Rows[i]["SIC_PVT_FLAG"].ToString(),
                    ds.Tables[4].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString(),

                  ss_pay_date,
                    ds.Tables[4].Rows[i]["CANCELLATION_FEE"].ToString(),

                     ds.Tables[4].Rows[i]["SERVICE_VOUCHER_NO"].ToString(),

                   ss_package_flag,
                    ds.Tables[4].Rows[i]["ADULT_PVT_RATE"].ToString(),
                    ds.Tables[4].Rows[i]["ADULT_SIC_RATE"].ToString(),
                    ds.Tables[4].Rows[i]["CHILD_PVT_RATE"].ToString(),
                    ds.Tables[4].Rows[i]["CHILD_SIC_RATE"].ToString()

                    );
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {

                string from_date = "";
                if (ds.Tables[2].Rows[i]["FROM_DATE"].ToString() != "")
                {
                    DateTime FRM_DATE = DateTime.Parse(ds.Tables[2].Rows[i]["FROM_DATE"].ToString());
                    from_date = FRM_DATE.ToString("dd/MM/yyyy");
                }

                string to_date = "";
                if (ds.Tables[2].Rows[i]["TO_DATE"].ToString() != "")
                {
                    DateTime TO_DAT = DateTime.Parse(ds.Tables[2].Rows[i]["TO_DATE"].ToString());
                    to_date = TO_DAT.ToString("dd/MM/yyyy");
                }

                bool ss_package_flag = false;
                if (ds.Tables[2].Rows[i]["PACKAGE_FLAG"].ToString() == "True")
                {
                    ss_package_flag = true;
                }

                string PAYMENT_date = "";
                if (ds.Tables[2].Rows[i]["PAYMENT_DUE_DATE"].ToString() != "")
                {
                    DateTime PAYMEN_date = DateTime.Parse(ds.Tables[2].Rows[i]["PAYMENT_DUE_DATE"].ToString());
                    PAYMENT_date = PAYMEN_date.ToString("dd/MM/yyyy");
                }


                string RECON_DATE = "";

                if (ds.Tables[2].Rows[i]["HOTEL_RECONFIRMATATION_DATE"].ToString() != "")
                {
                    DateTime PAYMEN_date = DateTime.Parse(ds.Tables[2].Rows[i]["HOTEL_RECONFIRMATATION_DATE"].ToString());
                    RECON_DATE = PAYMEN_date.ToString("dd/MM/yyyy");
                }

                objgenerateInvoiceStoredProcedure.COPY_HOTEL_CART(
                    ds.Tables[2].Rows[i]["CITY_ID"].ToString(),
                    ds.Tables[2].Rows[i]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString(),

                    from_date,
                    to_date,
                    ds.Tables[2].Rows[i]["NO_OF_ROOMS"].ToString(),
                    // ds.Tables[2].Rows[i]["ORDER_STATUS"].ToString(),
                   "2",
                    ds.Tables[2].Rows[i]["CREATED_BY"].ToString(),
                    ds.Tables[2].Rows[i]["ROOM_TYPE"].ToString(),
                    ds.Tables[2].Rows[i]["ROOM_TYPE_ID"].ToString(),
                    int.Parse(ds_quote_id.Tables[0].Rows[0][0].ToString()),
                    RECON_DATE,
                    //ds.Tables[2].Rows[i]["HOTEL_STATUS"].ToString(),
                   "2",

                    ds.Tables[2].Rows[i]["CONFIRMATION_NUMBER"].ToString(),
                    PAYMENT_date,


                     ds.Tables[2].Rows[i]["CANCELLATION_FEE"].ToString(),
                     ds.Tables[2].Rows[0]["SERVICE_VOUCHER_NO"].ToString(),
                   ss_package_flag);

                for (int j = 0; j < ds.Tables[3].Rows.Count; j++)
                {
                    if (int.Parse(ds.Tables[2].Rows[i]["HOTEL_CART_ID"].ToString()) == int.Parse(ds.Tables[3].Rows[j]["HOTEL_CART_ID"].ToString()))
                    {
                        string from_date1 = "";
                        if (ds.Tables[3].Rows[j]["FROM_DATE"].ToString() != "")
                        {
                            DateTime FRM_DATE = DateTime.Parse(ds.Tables[3].Rows[j]["FROM_DATE"].ToString());
                            from_date1 = FRM_DATE.ToString("dd/MM/yyyy");
                        }

                        string to_date1 = "";
                        if (ds.Tables[3].Rows[j]["TO_DATE"].ToString() != "")
                        {
                            DateTime TO_DAT = DateTime.Parse(ds.Tables[3].Rows[j]["TO_DATE"].ToString());
                            to_date1 = TO_DAT.ToString("dd/MM/yyyy");
                        }

                        bool ss_package_flag1 = false;
                        if (ds.Tables[3].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                        {
                            ss_package_flag = true;
                        }
                        objgenerateInvoiceStoredProcedure.COPY_HOTEL_CART_SUB_DETAILS(ds.Tables[3].Rows[j]["HOTEL_CART_ID"].ToString(),
                            from_date1,
                            to_date1,
                            // ds.Tables[3].Rows[j]["CREATED_BY"].ToString(),
                            ss_package_flag1,
                            ds.Tables[3].Rows[j]["SINGLE_ROOM_RATE"].ToString(),
                            ds.Tables[3].Rows[j]["DOUBLE_ROOM_RATE"].ToString(),
                            ds.Tables[3].Rows[j]["TRIPLE_ROOM_RATE"].ToString(),
                            ds.Tables[3].Rows[j]["EXTRA_ADULT_RATE"].ToString(),
                            ds.Tables[3].Rows[j]["EXTRA_CWB_RATE"].ToString(),
                            ds.Tables[3].Rows[j]["EXTRA_CNB_RATE"].ToString()
                            );
                    }
                }


            }

            objgenerateInvoiceStoredProcedure.update_tour_id
                (int.Parse(ds_quote_id.Tables[0].Rows[0][0].ToString()), int.Parse(ds_tour_id.Tables[0].Rows[0][0].ToString()));

            Master.DisplayMessage("Copy generated Successfully & New Quotaion Refference no is  :" + ds_quote_id.Tables[0].Rows[0][0].ToString(), "successMessage", 8000);


        }

        protected void txtPeriodStayTo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodStayFrom.Text == "")
                {
                    Master.DisplayMessage("Please first select tour from Date.", "successMessage", 8000);
                    txtPeriodStayTo.Text = "";
                }
                else if (DateTime.ParseExact(txtPeriodStayTo.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtPeriodStayFrom.Text, "dd/MM/yyyy", null))
                {
                    Master.DisplayMessage("Tour to date must be after tour from date.", "successMessage", 8000);
                    txtPeriodStayTo.Text = "";
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


        #region GRID VIEW FOR DETAILS

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                AddDescription(GridDetails, UpdatePanel_Generate_Invoice);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                removeRow(GridDetails, rowID, UpdatePanel_Generate_Invoice);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void AddDescription(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                foreach (GridViewRow item in gv.Rows)
                {

                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");

                    Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                    Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Description");
                        dt.Columns.Add("UnitNo");
                        dt.Columns.Add("AmountPerPerson");
                        dt.Columns.Add("TotalAmount");
                        dt.Columns.Add("DetailID");

                    }

                    DataRow dr = dt.NewRow();
                    dr["Description"] = txtDescription.Text;
                    dr["UnitNo"] = txtUnitNo.Text;
                    dr["AmountPerPerson"] = txtAPP.Text;
                    dr["TotalAmount"] = lblGridTotalAmount.Text;
                    dr["DetailID"] = lblDeatilsID.Text;
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Description");
                        dt.Columns.Add("UnitNo");
                        dt.Columns.Add("AmountPerPerson");
                        dt.Columns.Add("TotalAmount");
                        dt.Columns.Add("DetailID");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Description"] = "";
                    dr["UnitNo"] = "";
                    dr["AmountPerPerson"] = "";
                    dr["TotalAmount"] = "";
                    dr["DetailID"] = "";
                    dt.Rows.Add(dr);
                    gv.DataSource = dt;
                    gv.DataBind();
                    uppanel.Update();
                }

                if (count != 0)
                {
                    DataRow dr1 = dt.NewRow();

                    dt.Rows.Add(dr1);
                }

                gv.DataSource = dt;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");

                    Button btnHotelRemove = (Button)item.FindControl("btnHotelRemove");

                    Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                    Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                    if (ViewState["RowCount"] != null)
                    {
                        if (itm <= int.Parse(ViewState["RowCount"].ToString()))
                        {
                            btnHotelRemove.Visible = false;
                        }
                        else
                        {

                        }
                    }

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            //bindInvoiceFromSupplier(k);
                            if (k == 0)
                            {
                                txtDescription.Text = "SINGLE SHARE";
                                //txtDescription.Enabled = false;
                            }
                            else if (k == 1)
                            {
                                txtDescription.Text = "DOUBLE SHARE";
                                //txtDescription.Enabled = false;
                            }
                            else if (k == 2)
                            {
                                txtDescription.Text = "TRIPLE SHARE";
                                //txtDescription.Enabled = false;
                            }
                            else if (k == 3)
                            {
                                txtDescription.Text = "CHILD WITH BED";
                            }
                            else if (k == 4)
                            {
                                txtDescription.Text = "CHILD WITH NO BED";
                            }
                            else
                            {
                                txtDescription.Text = dt.Rows[itm]["Description"].ToString();
                            }
                            txtUnitNo.Text = dt.Rows[itm]["UnitNo"].ToString();
                            txtAPP.Text = dt.Rows[itm]["AmountPerPerson"].ToString();

                            lblDeatilsID.Text = dt.Rows[itm]["DetailID"].ToString();
                            lblGridTotalAmount.Text = dt.Rows[itm]["TotalAmount"].ToString();


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
                uppanel.Update();
            }

        }



        protected void removeRow(GridView gv, int rowIndex, UpdatePanel uppanel)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                int count = gv.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    dt1.Rows.Add();
                }
                foreach (GridViewRow item in gv.Rows)
                {
                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                    Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                    Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                    if (dt.Columns.Count == 0)
                    {

                        dt.Columns.Add("Description");
                        dt.Columns.Add("UnitNo");
                        dt.Columns.Add("AmountPerPerson");
                        dt.Columns.Add("TotalAmount");
                        dt.Columns.Add("DetailID");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Description"] = txtDescription.Text;
                    dr["UnitNo"] = txtUnitNo.Text;
                    dr["AmountPerPerson"] = txtAPP.Text;
                    dr["TotalAmount"] = lblGridTotalAmount.Text;
                    dr["DetailID"] = lblDeatilsID.Text;
                    dt.Rows.Add(dr);

                }

                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    int itm1 = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }


                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                    Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                    Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                    Button btnHotelRemove = (Button)item.FindControl("btnHotelRemove");
                    if (ViewState["RowCount"] != null)
                    {
                        if (itm <= int.Parse(ViewState["RowCount"].ToString()))
                        {
                            btnHotelRemove.Visible = false;
                        }
                        else
                        {

                        }
                    }



                    txtDescription.Text = dt.Rows[itm]["Description"].ToString();
                    txtUnitNo.Text = dt.Rows[itm]["UnitNo"].ToString();
                    txtAPP.Text = dt.Rows[itm]["AmountPerPerson"].ToString();
                    lblDeatilsID.Text = dt.Rows[itm]["DetailID"].ToString();
                    lblGridTotalAmount.Text = dt.Rows[itm]["TotalAmount"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uppanel.Update();
            }
        }

        protected void txtUnitNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtUnitNo = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtUnitNo.NamingContainer).DataItemIndex;
                unitNoChanged(GridDetails, UpdatePanel_Generate_Invoice, repeaterItemIndex);
                findTotalAmount();

                btnAddRow.Focus();
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

        protected void txtAPP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtApp = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtApp.NamingContainer).DataItemIndex;
                unitNoChanged(GridDetails, UpdatePanel_Generate_Invoice, repeaterItemIndex);
                findTotalAmount();
                btnAddRow.Focus();
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


        protected void unitNoChanged(GridView gv, UpdatePanel uppanel, int rowIndex)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    int Index = item.DataItemIndex;

                    if (Index == rowIndex)
                    {
                        TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                        TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                        TextBox txtAPP = (TextBox)item.FindControl("txtAPP");

                        Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                        if (txtUnitNo.Text != "" && txtAPP.Text != "")
                        {
                            lblGridTotalAmount.Text = (decimal.Parse(txtUnitNo.Text) * decimal.Parse(txtAPP.Text)).ToString();

                            uppanel.Update();
                        }

                    }
                }
                //findTotalAmount();
            }
            catch
            {
            }
            finally
            {
                uppanel.Update();
            }

        }

        protected void insertUpdateInoiceDetails(int salesInvoiceId)
        {

            foreach (GridViewRow item in GridDetails.Rows)
            {
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                if (lblDeatilsID.Text == "")
                {
                    objInvoiceThb.insertInvoiceDetails(0, salesInvoiceId, txtDescription.Text, lblGridTotalAmount.Text, txtUnitNo.Text, "USD");
                }
                else
                {
                    objInvoiceThb.insertInvoiceDetails(int.Parse(lblDeatilsID.Text), salesInvoiceId, txtDescription.Text, lblGridTotalAmount.Text, txtUnitNo.Text, "USD");
                }
            }

        }


        protected void findTotalAmount()
        {
            try
            {
                decimal totalAmount = 0;
                foreach (GridViewRow item in GridDetails.Rows)
                {

                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");

                    Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                    if (lblGridTotalAmount.Text != "")
                    {
                        totalAmount = totalAmount + decimal.Parse(lblGridTotalAmount.Text);
                    }


                }
                //decimal exrate = 1;
                //if (txtExRate.Text != "")
                //{
                //    exrate = decimal.Parse(txtExRate.Text);
                //}
                //decimal total1 = (totalAmount / exrate);
                txtAmount.Text = totalAmount.ToString();
                decimal h = 0;
                decimal i = 0;
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
                    txttotalInvoiceAmount.Text = string.Format("{0:#.00}", totalAmount);
                }
                else
                {
                    lbltotalInvoiceAmount.Text = string.Format("{0:#.00}", totalAmount);
                }

                if (lbltotalInvoiceAmount.Text != "")
                {
                    lblFinalAmount.Text = lbltotalInvoiceAmount.Text;
                }
                else
                {
                    lblFinalAmount.Text = txttotalInvoiceAmount.Text;
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

        protected void fillDetailsEditMode(DataSet DS)
        {
            try
            {

                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in GridDetails.Rows)
                    {
                        if (i == item.DataItemIndex)
                        {
                            TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                            TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                            TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                            Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                            Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");

                            lblDeatilsID.Text = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            txtDescription.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            lblGridTotalAmount.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
                            decimal a = 0;
                            if (DS.Tables[1].Rows[i]["AMOUNT"].ToString() != "")
                            {
                                a = decimal.Parse(DS.Tables[1].Rows[i]["AMOUNT"].ToString());
                            }
                            txtUnitNo.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                            decimal b = 1;
                            if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                            {
                                b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                            }
                            if (a == 0 && b == 0)
                            {
                                txtAPP.Text = "0";
                            }
                            else
                            {
                                txtAPP.Text = string.Format("{0:#.00}", a / b);

                            }
                        }

                    }
                    if (i < DS.Tables[1].Rows.Count - 1)
                    {
                        AddDescription(GridDetails, UpdatePanel_Generate_Invoice);
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

        protected string[] defaultAmount()
        {
            string amount1 = "0";
            string amount2 = "0";
            string amount3 = "0";
            string amount4 = "0";
            string amount5 = "0";


            foreach (GridViewRow item in GridDetails.Rows)
            {
                int i = item.DataItemIndex;
                Label lblGridTotalAmount = (Label)item.FindControl("lblGridTotalAmount");
                TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                if (i == 0)
                {
                    if (txtUnitNo.Text != "")
                    {
                        amount1 = txtUnitNo.Text;
                    }
                }
                else if (i == 1)
                {
                    if (txtUnitNo.Text != "")
                    {
                        amount2 = txtUnitNo.Text;
                    }

                }
                else if (i == 2)
                {
                    if (txtUnitNo.Text != "")
                    {
                        amount3 = txtUnitNo.Text;
                    }
                }
                else if (i == 3)
                {
                    if (txtUnitNo.Text != "")
                    {
                        amount4 = txtUnitNo.Text;
                    }
                }
                else if (i == 4)
                {
                    if (txtUnitNo.Text != "")
                    {
                        amount5 = txtUnitNo.Text;
                    }
                }
                else
                {
                    break;
                }



            }
            return new string[5] { amount1, amount2, amount3, amount4, amount5 };


        }


        #endregion

        protected void btnclose1_Click(object sender, EventArgs e)
        {
            if (TextBox20.Text.Trim() != string.Empty)
            {

                objgenerateInvoiceStoredProcedure.UpdateInvoiceforclose1andclose2("UPDATE_INVOICE_NO_WITH_CLOSE_1", TextBox20.Text);

                DataSet ds = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", TextBox20.Text.Trim());
                if (ds != null)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                    {
                        btnclose1.Visible = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        btnclose1.Visible = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_2"]) == "True")
                    {
                        btnclose2.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                        {
                            btnclose2.Visible = true;
                        }
                        else
                        {
                            btnclose2.Visible = false;
                        }
                    }
                }
                UpdatePanel_Generate_Invoice.Update();

                Master.DisplayMessage("Close-1 update sucessfully", "successMessage", 8000);
            }
            else
            {
                Master.DisplayMessage("Invoice NO. should not be empty", "successMessage", 8000);
            }
                     
        }

        protected void btnclose2_Click(object sender, EventArgs e)
        {
            if (TextBox20.Text.Trim() != string.Empty)
            {

                objgenerateInvoiceStoredProcedure.UpdateInvoiceforclose1andclose2("UPDATE_INVOICE_NO_WITH_CLOSE_2", TextBox20.Text);
                DataSet ds = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", TextBox20.Text.Trim());
                if (ds != null)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                    {
                        btnclose1.Visible = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        btnclose1.Visible = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_2"]) == "True")
                    {
                        btnclose2.Visible = false;
                        
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["CLOSE_1"]) == "True")
                        {
                            btnclose2.Visible = true;
                        }
                        else
                        {
                            btnclose2.Visible = false;
                        }
                    }
                }
                UpdatePanel_Generate_Invoice.Update();
                Master.DisplayMessage("Close-2 update sucessfully", "successMessage", 8000);

            }
            else
            {
                Master.DisplayMessage("Invoice NO. should not be empty", "successMessage", 8000);
            }

        }
    }
}