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
    public partial class THBInvoice : System.Web.UI.Page
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


                drpOrderStatus.Text = "Reconfirmed";

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

                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {

                    DataSet DS = objInvoiceThb.FetchDataForInvoice(int.Parse(Request["TOURID"].ToString()));

                    lnkbtn.Attributes.Add("style", "display");
                    lnkbtn.HRef = "~/Views/FIT/Invoices/" + Request["TOURID"].ToString() + "/Invoice.pdf";

                    ViewState["RowCount"] = DS.Tables[1].Rows.Count - 1;

                    txtorderplacedby.Text = DS.Tables[0].Rows[0]["ORDER_PLACED_BY"].ToString();
                    txtpersonemail.Text = DS.Tables[0].Rows[0]["PERSON_EMAIL"].ToString();

                    txtNoOfNights.Text = DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
                    Session["sa"] = DS.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString();
                    drpCurrency.SelectedValue = DS.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    txtNoOfAdult.Text = DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                    //txtNoOfChild.Text = DS.Tables[0].Rows[0]["NO_OF_CHILD"].ToString();
                    txtNoOfCWB.Text = DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                    txtNoOfCNB.Text = DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                    txtNoOfInfant.Text = DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
                    txtPeriodStayFrom.Text = DS.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txtPeriodStayTo.Text = DS.Tables[0].Rows[0]["TO_DATE"].ToString();
                    txtAmount.Text = DS.Tables[0].Rows[0]["AMOUNT"].ToString();
                    txtTax.Text = DS.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();
                    txtTotalAmount.Text = DS.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    txtBook_ref_no.Text = DS.Tables[0].Rows[0]["COMPANY_BOOKING_REFERENCE_NO"].ToString();
                    txtInvoiceNo.Text = DS.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    TextBox42.Text = DS.Tables[0].Rows[0]["GL_DATE"].ToString();
                    ViewState["INVOICE"] = DS.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    DropDownList1.Text = DS.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    drpPaymentMode.Text = DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();
                    drpOrderStatus.Text = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                    txtclientname.Text = DS.Tables[0].Rows[0]["CLIENTNAME"].ToString();

                    hiddensalesid.Value = Request.QueryString["TOURID"].ToString();

                    if (DS.Tables[0].Rows[0]["EX_RATE"].ToString() != "")
                    {
                        txtExRate.Text = DS.Tables[0].Rows[0]["EX_RATE"].ToString();
                    }
                    else
                    {
                        txtExRate.Text = "1";
                    }

                    DataSet dsUname = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList1.Text);
                    Session["rel_sr_no"] = dsUname.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    Session["uid"] = dsUname.Tables[0].Rows[0]["USER_ID"].ToString();
                    Session["custid"] = DS.Tables[0].Rows[0]["CUST_ID"].ToString();

                    DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                    Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
                    Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();

                    DataTable DTMODE = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "4");
                    DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
                    if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
                    {


                        DataSet dsCl = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                        lblcreditlimitAmount.Text = dsCl.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                        lblcurrentusableamount.Text = dsCl.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
                        Label2.Text = dsCl.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                        lblcrlimitdate.Text = dsCl.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                        lblcurrentusablecurrency.Text = dsCl.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                        if (lblcurrentusableamount.Text == "")
                        {
                            lblcurrentusableamount.Text = "0";
                        }
                        lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();

                        lbltotalInvoiceAmount.Text = string.Format("{0:#.00}", (decimal.Parse(txtAmount.Text) / decimal.Parse(txtExRate.Text)));
                        lblFinalAmount.Text = lbltotalInvoiceAmount.Text;
                        ViewState["old_amount"] = lbltotalInvoiceAmount.Text;
                        t1.Attributes.Add("style", "display");
                        t2.Attributes.Add("style", "display");
                        t3.Attributes.Add("style", "display");
                        t4.Attributes.Add("style", "display");

                        Tr2.Attributes.Add("style", "display");
                    }
                    AddDescription(GridInvoice, UpdatePanel_Generate_Invoice);
                    fillDetailsEditMode(DS);
                    //fillDetailsforInvoice(DS);
                    btnSendInvoice.Visible = true;

                    DataSet dsclose = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", txtInvoiceNo.Text.Trim());
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




                }
                else
                {
                    AddDescription(GridInvoice, UpdatePanel_Generate_Invoice);
                    drpCurrency.Text = "THB";

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
            bool crdit_limit_flag = true;
            DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_PAYMENT_MODE_FOR_FIT_PAYMENT", "1");
            if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
            {
                if (txtInvoiceNo.Text == "")
                {
                    if (decimal.Parse(lblcurrentusableamount.Text) < decimal.Parse(lbltotalInvoiceAmount.Text))
                    {
                        crdit_limit_flag = false;
                    }
                }
                else
                {
                    string lbltotalInvoiceAmountCheck = "";
                    lbltotalInvoiceAmountCheck = (decimal.Parse(lbltotalInvoiceAmount.Text) - decimal.Parse(ViewState["old_amount"].ToString())).ToString();
                    if (decimal.Parse(lblcurrentusableamount.Text) < Convert.ToDecimal(lbltotalInvoiceAmountCheck))
                    {
                        crdit_limit_flag = false;
                    }

                }
            }

            if (crdit_limit_flag == false)
            {
                Master.DisplayMessage("You do not have enough Credit Limit to complete this purchase.", "successMessage", 5000);
            }
            else
            {
                if (txtInvoiceNo.Text == "")
                {
                    DataTable dt = objInvoiceThb.insert_sales_invoice_header(int.Parse("0"), int.Parse("0"), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, decimal.Parse(txtAmount.Text), decimal.Parse(txtTax.Text), decimal.Parse(txtTotalAmount.Text), txtNoOfAdult.Text, "0", txtNoOfCWB.Text, txtNoOfCNB.Text, txtNoOfInfant.Text, drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, TextBox42.Text, "THB", 1, txtclientname.Text, decimal.Parse(txtExRate.Text), txtorderplacedby.Text, txtpersonemail.Text);
                    //insertSalesDetails(int.Parse(dt.Rows[0][0].ToString()));
                    insertUpdateInoiceDetails(int.Parse(dt.Rows[0][0].ToString()));

                    insertAccountEntry(dt.Rows[0][1].ToString(), txtAmount.Text);

                    if (drpPaymentMode.Text == DTORDER.Rows[0]["AutoSearchResult"].ToString())
                    {
                        DataSet ds1 = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
                        lblcreditlimitAmount.Text = ds1.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                        lblcurrentusableamount.Text = ds1.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
                        if (lblcurrentusableamount.Text == "")
                        {
                            lblcurrentusableamount.Text = "0";
                        }
                        //lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();
                        objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), decimal.Parse(lbltotalInvoiceAmount.Text));

                    }
                    generatePdf(dt.Rows[0][0].ToString());
                    txtInvoiceNo.Text = dt.Rows[0][1].ToString();
                    hiddensalesid.Value = dt.Rows[0][0].ToString();
                    //Send_invoice_to_agent(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                    btnSendInvoice.Visible = true;
                    Master.DisplayMessage("Invoice Generated Successfully.", "successMessage", 3000);

                    Response.Redirect("~/Views/Account/AllInvoice.aspx");

                    UpdatePanel_Generate_Invoice.Update();
                }
                else
                {
                    updateSalesHeader();
                    //updateSalesDetails();
                    insertUpdateInoiceDetails(int.Parse(Request.QueryString["TOURID"].ToString()));
                    generatePdf(Request.QueryString["TOURID"].ToString());
                    //Send_invoice_to_agent(Request.QueryString["TOURID"].ToString(), txtInvoiceNo.Text);

                    if (decimal.Parse(ViewState["old_amount"].ToString()) > decimal.Parse(lbltotalInvoiceAmount.Text))
                    {
                        objFITPaymentStoreProcedure.edit_current_usable_MINUS(int.Parse(Session["rel_sr_no"].ToString()), (decimal.Parse(ViewState["old_amount"].ToString()) - decimal.Parse(lbltotalInvoiceAmount.Text)));
                        ViewState["old_amount"] = txtTotalAmount.Text;
                    }
                    else
                    {
                        objFITPaymentStoreProcedure.edit_current_usable(int.Parse(Session["rel_sr_no"].ToString()), (decimal.Parse(lbltotalInvoiceAmount.Text) - decimal.Parse(ViewState["old_amount"].ToString())));
                        ViewState["old_amount"] = txtTotalAmount.Text;
                    }

                    Master.DisplayMessage("Invoice Updated Successfully.", "successMessage", 3000);
                    Response.Redirect("~/Views/Account/AllInvoice.aspx");
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

                lbltotalInvoiceAmount.Text = string.Format("{0:#.00}", (decimal.Parse(txtAmount.Text) / decimal.Parse(txtExRate.Text)));
                lblFinalAmount.Text = lbltotalInvoiceAmount.Text;

                t1.Attributes.Add("style", "display");
                t2.Attributes.Add("style", "display");
                t3.Attributes.Add("style", "display");
                t4.Attributes.Add("style", "display");

                Tr2.Attributes.Add("style", "display");
            }
            else
            {

            }
            UpdatePanel_Generate_Invoice.Update();

        }




        #endregion

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = DropDownList1.Text;
            DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
            Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
            Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();
            Session["custid"] = DS.Tables[0].Rows[0]["CUST_ID"].ToString();


            DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
            Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
            Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();
            UpdatePanel_Generate_Invoice.Update();
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
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filename01, "Quotation.pdf", int.Parse(Session["usersid"].ToString()), 1);
                    //     objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filename01, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filename11, "Itinerary.pdf", int.Parse(Session["usersid"].ToString()), 2);

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
                            objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filename22, filename23, int.Parse(Session["usersid"].ToString()), 2);
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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, subjct, body, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);
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
                        objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 1);
                    }
                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, Confirmagentbody, int.Parse(EmailQoute_id), txtInvoiceNo.Text, filepath, filename_ss, int.Parse(Session["usersid"].ToString()), 2);

                }

            }
        }
        #endregion

        #region Manual Voucher Agent

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
            Send_invoice_to_agent(hiddensalesid.Value, txtInvoiceNo.Text);
            Master.DisplayMessage("Invoice Sent Successfully.", "successMessage", 8000);
            UpdatePanel_Generate_Invoice.Update();
        }



        protected void disable_control()
        {

            txtInvoiceNo.Enabled = false;

           // btnSave.Visible = false;
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

                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), txtInvoiceNo.Text, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 1);
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, to, cc, bcc, subjct, body, int.Parse(Session["quoteid"].ToString()), txtInvoiceNo.Text, filename1, "Invoice.pdf", int.Parse(Session["usersid"].ToString()), 2);


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





        protected void insertAccountEntry(string salesInvoiceNo, string totalAmount)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            DataSet ds11 = objFITPaymentStoreProcedure.fetch_invoice_dateials("FETCH_DESCRIPTION_FROM_INVOICE_NO", salesInvoiceNo);

            DataSet ds_rate = objFITPaymentStoreProcedure.fetch_conversion_rate();

            DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();

            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");

            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

            DataSet ds6 = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", Session["custid"].ToString(), "A");



            DataSet ds_amount_fit_package = objFITPaymentStoreProcedure.fetch_fit_package_amount("FETCH_AMOUNT_FIT_PACKAGE", ds_all_gl_code.Tables[0].Rows[0]["GL_CODE"].ToString());

            DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));

            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), salesInvoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", totalAmount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds6.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), salesInvoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", totalAmount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
            objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[11]["AutoSearchResult"].ToString(), salesInvoiceNo, date, ds_vt.Tables[0].Rows[0]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, totalAmount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

        }





        protected void updateSalesHeader()
        {
            objInvoiceThb.insert_sales_invoice_header(int.Parse(Request.QueryString["TOURID"].ToString()), int.Parse("0"), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, decimal.Parse(txtAmount.Text), decimal.Parse(txtTax.Text), decimal.Parse(txtTotalAmount.Text), txtNoOfAdult.Text, "0", txtNoOfCWB.Text, txtNoOfCNB.Text, txtNoOfInfant.Text, drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, TextBox42.Text, "THB", 1, txtclientname.Text, decimal.Parse(txtExRate.Text), txtorderplacedby.Text, txtpersonemail.Text);

            DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(txtInvoiceNo.Text, "SALES");
            objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, txtInvoiceNo.Text, int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[0]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", "", txtAmount.Text, TextBox42.Text, 1);
            for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, txtInvoiceNo.Text, int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", txtAmount.Text, txtAmount.Text, TextBox42.Text, 2);
                }
                else if (i == 1)
                {
                    objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, txtInvoiceNo.Text, int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), txtAmount.Text, "", txtAmount.Text, TextBox42.Text, 2);
                }
            }

        }

        protected void generatePdf(string salesInvoiceId)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Invoices/" + salesInvoiceId + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + salesInvoiceId + "/"));

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





            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("SALES_INVOICE_ID", salesInvoiceId);
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/ReceiptTHB";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();


            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            rptViewer1.Visible = false;


            //Response.Clear();

            using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Invoices/" + salesInvoiceId + "/Invoice.pdf")))
            {
                fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
            }
        }

        protected void Send_invoice_to_agent(string invoiceId, string invoice_no)
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
            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[25]["AutoSearchResult"].ToString());

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                MailMessage message = new MailMessage();
                string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
                DataTable dt_supplier_id = new DataTable();





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


                body = body.Replace("&lt;%CLIENTNAME%&gt;", txtclientname.Text);
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

                subjct = subjct.Replace("<%CLIENTNAME%>", txtclientname.Text);
                subjct = subjct.Replace("<%FROMDATE%>", txtPeriodStayFrom.Text);
                subjct = subjct.Replace("<%TODATE%>", txtPeriodStayTo.Text);

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



        protected void txtExRate_TextChanged(object sender, EventArgs e)
        {
            if (lbltotalInvoiceAmount.Text != "")
            {
                findTotalAmount();

            }
            UpdatePanel_Generate_Invoice.Update();
        }



        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                AddDescription(GridInvoice, UpdatePanel_Generate_Invoice);
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
                removeRow(GridInvoice, rowID, UpdatePanel_Generate_Invoice);
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
                    Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

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
                    dr["TotalAmount"] = lblTotalAmount.Text;
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
                    Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

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

                            txtDescription.Text = dt.Rows[itm]["Description"].ToString();
                            txtUnitNo.Text = dt.Rows[itm]["UnitNo"].ToString();
                            txtAPP.Text = dt.Rows[itm]["AmountPerPerson"].ToString();

                            lblDeatilsID.Text = dt.Rows[itm]["DetailID"].ToString();
                            lblTotalAmount.Text = dt.Rows[itm]["TotalAmount"].ToString();


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
                    Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

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
                    dr["TotalAmount"] = lblTotalAmount.Text;
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
                    Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

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
                    lblTotalAmount.Text = dt.Rows[itm]["TotalAmount"].ToString();
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
                unitNoChanged(GridInvoice, UpdatePanel_Generate_Invoice, repeaterItemIndex);
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
                unitNoChanged(GridInvoice, UpdatePanel_Generate_Invoice, repeaterItemIndex);
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

                        Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                        if (txtUnitNo.Text != "" && txtAPP.Text != "")
                        {
                            lblTotalAmount.Text = (decimal.Parse(txtUnitNo.Text) * decimal.Parse(txtAPP.Text)).ToString();

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

            foreach (GridViewRow item in GridInvoice.Rows)
            {
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                if (lblDeatilsID.Text == "")
                {
                    objInvoiceThb.insertInvoiceDetails(0, salesInvoiceId, txtDescription.Text, lblTotalAmount.Text, txtUnitNo.Text, "THB");
                }
                else
                {
                    objInvoiceThb.insertInvoiceDetails(int.Parse(lblDeatilsID.Text), salesInvoiceId, txtDescription.Text, lblTotalAmount.Text, txtUnitNo.Text, "THB");
                }
            }

        }


        protected void findTotalAmount()
        {
            try
            {
                decimal totalAmount = 0;
                foreach (GridViewRow item in GridInvoice.Rows)
                {

                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                    TextBox txtAPP = (TextBox)item.FindControl("txtAPP");

                    Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                    if (lblTotalAmount.Text != "")
                    {
                        totalAmount = totalAmount + decimal.Parse(lblTotalAmount.Text);
                    }


                }
                decimal exrate = 1;
                if (txtExRate.Text != "")
                {
                    exrate = decimal.Parse(txtExRate.Text);
                }
                decimal total1 = (totalAmount / exrate);
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
                    txttotalInvoiceAmount.Text = string.Format("{0:#.00}", total1);
                }
                else
                {
                    lbltotalInvoiceAmount.Text = string.Format("{0:#.00}", total1);
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
                    foreach (GridViewRow item in GridInvoice.Rows)
                    {
                        if (i == item.DataItemIndex)
                        {
                            TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                            TextBox txtUnitNo = (TextBox)item.FindControl("txtUnitNo");
                            TextBox txtAPP = (TextBox)item.FindControl("txtAPP");
                            Label lblDeatilsID = (Label)item.FindControl("lblDeatilsID");
                            Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                            lblDeatilsID.Text = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
                            txtDescription.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
                            lblTotalAmount.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
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
                        AddDescription(GridInvoice, UpdatePanel_Generate_Invoice);
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

        protected void btnclose1_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text.Trim() != string.Empty)
            {

                objgenerateInvoiceStoredProcedure.UpdateInvoiceforclose1andclose2("UPDATE_INVOICE_NO_WITH_CLOSE_1", txtInvoiceNo.Text);

                DataSet ds = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", txtInvoiceNo.Text.Trim());
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

                Master.DisplayMessage("Close-1 update sucessfully", "successMessage", 5000);
            }
            else
            {
                Master.DisplayMessage("Invoice NO. should not be empty", "successMessage", 5000);
            }

        }

        protected void btnclose2_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text.Trim() != string.Empty)
            {

                objgenerateInvoiceStoredProcedure.UpdateInvoiceforclose1andclose2("UPDATE_INVOICE_NO_WITH_CLOSE_2", txtInvoiceNo.Text);
                DataSet ds = objgenerateInvoiceStoredProcedure.fetchclosestatus("FETCH_INVOICE_CLOSE_1_CLOSE_2", txtInvoiceNo.Text.Trim());
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
                Master.DisplayMessage("Close-2 update sucessfully", "successMessage", 5000);

            }
            else
            {
                Master.DisplayMessage("Invoice NO. should not be empty", "successMessage", 5000);
            }

        }
    }
}
