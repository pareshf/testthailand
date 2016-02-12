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
    public partial class InvoiceThb : System.Web.UI.Page
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
                    DropDownList1.Text  = DS.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    drpPaymentMode.Text = DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();
                    drpOrderStatus.Text = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
                    txtclientname.Text = DS.Tables[0].Rows[0]["CLIENTNAME"].ToString();

                    hiddensalesid.Value = Request.QueryString["TOURID"].ToString();

                    if (DS.Tables[0].Rows[0]["EX_RATE"].ToString() != "" )
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


                    fillDetailsforInvoice(DS);
                    btnSendInvoice.Visible = true;
                }
                else
                {
                    drpCurrency.Text = "THB";
                    row1_drp_currency.Text = "THB";
                    row2_drp_currency.Text = "THB";
                    row3_drp_currency.Text = "THB";
                    row4_drp_currency.Text = "THB";
                    row5_drp_currency.Text = "THB";
                    row6_drp_currency.Text = "THB";
                    row7_drp_currency.Text = "THB";
                    row8_drp_currency.Text = "THB";
                    row9_drp_currency.Text = "THB";
                    row10_drp_currency.Text = "THB";
                    row11_drp_currency.Text = "THB";
                    row12_drp_currency.Text = "THB";
                    row13_drp_currency.Text = "THB";
                    row14_drp_currency.Text = "THB";
                    row15_drp_currency.Text = "THB";
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
                if (decimal.Parse(lblcurrentusableamount.Text) < decimal.Parse(lbltotalInvoiceAmount.Text))
                {
                    crdit_limit_flag = false;
                   
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
                    DataTable dt = objInvoiceThb.insert_sales_invoice_header(int.Parse("0"), int.Parse("0"), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, decimal.Parse(txtAmount.Text), decimal.Parse(txtTax.Text), decimal.Parse(txtTotalAmount.Text), txtNoOfAdult.Text, "0", txtNoOfCWB.Text, txtNoOfCNB.Text, txtNoOfInfant.Text, drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, TextBox42.Text, "THB", 1, txtclientname.Text, decimal.Parse(txtExRate.Text), "","");
                    insertSalesDetails(int.Parse(dt.Rows[0][0].ToString()));

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
                    UpdatePanel_Generate_Invoice.Update();
                }
                else
                {
                    updateSalesHeader();
                    updateSalesDetails();
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
        }

        //protected void txtQuote_TextChanged(object sender, EventArgs e)
        //{
        //    string name = txtQuote.Text;
        //    string name1 = txtInvoiceNo.Text;
        //    DataSet DS = objgenerateInvoiceStoredProcedure.FetchDataForInvoice("FETCH_DATA_FOR_GENREATE_INVOICE", name, name1);
        //    if (DS.Tables[0].Rows.Count != 0)
        //    {
        //        drpOrderStatus.SelectedValue = DS.Tables[0].Rows[0]["ORDER_STATUS_NAME"].ToString();
        //        txtclientname.Text = DS.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
        //        txtNoOfNights.Text = DS.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
        //        sa = DS.Tables[0].Rows[0]["SALES_INVOICE_ID"].ToString();
        //        drpCurrency.SelectedValue = DS.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
        //        txtNoOfAdult.Text = DS.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
        //        txtNoOfChild.Text = DS.Tables[0].Rows[0]["NO_OF_CHILD"].ToString();
        //        txtNoOfCWB.Text = DS.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
        //        txtNoOfCNB.Text = DS.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
        //        txtNoOfInfant.Text = DS.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
        //        txtPeriodStayFrom.Text = DS.Tables[0].Rows[0]["FROM_DATE"].ToString();
        //        txtPeriodStayTo.Text = DS.Tables[0].Rows[0]["TO_DATE"].ToString();
        //        txtAmount.Text = DS.Tables[0].Rows[0]["AMOUNT"].ToString();
        //        txtTax.Text = DS.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();
        //        txtTotalAmount.Text = DS.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
        //        txtBook_ref_no.Text = DS.Tables[0].Rows[0]["COMPANY_BOOKING_REFERENCE_NO"].ToString();
        //        txtInvoiceNo.Text = DS.Tables[0].Rows[0]["INVOICE_NO"].ToString();
        //        drpPaymentMode.SelectedValue = DS.Tables[0].Rows[0]["PAYMENT_MODE_NAME"].ToString();
        //        for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
        //        {
        //            if (TextBox1.Text == "SINGLE SHARE" && row1_drp_currency.Text == "" && row1_txt_debit.Text == "")
        //            {
        //                TextBox1.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row1_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                row1_txt_debit.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s1"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();

        //            }
        //            else if (TextBox2.Text == "DOUBLE SHARE" && row2_drp_currency.Text == "" && TextBox3.Text == "")
        //            {
        //                row2.Attributes.Add("style", "display");
        //                btnadd2.Attributes.Add("style", "display:none");
        //                btnadd3.Attributes.Add("style", "display");
        //                TextBox2.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row2_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox3.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s2"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox4.Text == "TRIPLE SHARE" && row3_drp_currency.Text == "" && TextBox5.Text == "")
        //            {
        //                row3.Attributes.Add("style", "display");
        //                btnadd3.Attributes.Add("style", "display:none");
        //                btnadd4.Attributes.Add("style", "display");
        //                TextBox4.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row3_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox5.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s3"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();

        //            }
        //            else if (TextBox6.Text == "CHILD WITH BED" && row4_drp_currency.Text == "" && TextBox7.Text == "")
        //            {
        //                row4.Attributes.Add("style", "display");
        //                btnadd4.Attributes.Add("style", "display:none");
        //                btnadd5.Attributes.Add("style", "display");
        //                TextBox6.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row4_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox7.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s4"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();

        //            }
        //            else if (TextBox8.Text == "CHILD WITH NO BED" && row5_drp_currency.Text == "" && TextBox9.Text == "")
        //            {
        //                row5.Attributes.Add("style", "display");
        //                btnadd5.Attributes.Add("style", "display:none");
        //                btnadd6.Attributes.Add("style", "display");
        //                TextBox8.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row5_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox9.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s5"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox10.Text == "" && row6_drp_currency.Text == "" && TextBox11.Text == "")
        //            {
        //                row6.Attributes.Add("style", "display");
        //                btnadd6.Attributes.Add("style", "display:none");
        //                btnadd7.Attributes.Add("style", "display");

        //                TextBox10.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row6_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox11.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s6"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();

        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox12.Text == "" && row7_drp_currency.Text == "" && TextBox13.Text == "")
        //            {
        //                row7.Attributes.Add("style", "display");
        //                btnadd7.Attributes.Add("style", "display:none");
        //                btnadd8.Attributes.Add("style", "display");
        //                TextBox12.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row7_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox13.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s7"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox14.Text == "" && row8_drp_currency.Text == "" && TextBox15.Text == "")
        //            {
        //                row8.Attributes.Add("style", "display");
        //                btnadd8.Attributes.Add("style", "display:none");
        //                btnadd9.Attributes.Add("style", "display");
        //                TextBox14.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row8_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox15.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s8"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox17.Text == "" && row9_drp_currency.Text == "" && TextBox16.Text == "")
        //            {
        //                row9.Attributes.Add("style", "display");
        //                btnadd9.Attributes.Add("style", "display:none");
        //                btnadd10.Attributes.Add("style", "display");

        //                TextBox17.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row9_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox16.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s9"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else if (TextBox18.Text == "" && row10_drp_currency.Text == "" && TextBox19.Text == "")
        //            {
        //                row10.Attributes.Add("style", "display");
        //                btnadd10.Attributes.Add("style", "display:none");
        //                TextBox18.Text = DS.Tables[1].Rows[i]["INVOICE_DESCRIPTION"].ToString();
        //                row10_drp_currency.SelectedValue = DS.Tables[1].Rows[i]["CURRENCY_NAME"].ToString();
        //                TextBox19.Text = DS.Tables[1].Rows[i]["AMOUNT"].ToString();
        //                ViewState["s10"] = DS.Tables[1].Rows[i]["SALES_INVOICE_DETAILS_ID"].ToString();
        //                UpdatePanel_Generate_Invoice.Update();
        //            }
        //            else
        //            {
        //            }

        //        }
        //        DataSet ds = objFITPaymentStoreProcedure.fetch_credit_limit(int.Parse(Session["rel_sr_no"].ToString()));
        //        lblcreditlimitAmount.Text = ds.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
        //        lblcurrentusableamount.Text = ds.Tables[0].Rows[0]["CURRENT_USABLE_CREDIT_LIMIT"].ToString();
        //        Label2.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
        //        lblcrlimitdate.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
        //        lblcurrentusablecurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
        //        if (lblcurrentusableamount.Text == "")
        //        {
        //            lblcurrentusableamount.Text = "0";
        //        }
        //        lblcrlimitdate.Text = (decimal.Parse(lblcreditlimitAmount.Text) - decimal.Parse(lblcurrentusableamount.Text)).ToString();

        //        DataSet ds1 = objFITPaymentStoreProcedure.fetch_total_invoice(int.Parse(txtQuote.Text));
        //        //int.Parse(Session["quote_id"].ToString())
        //        lbltotalInvoiceAmount.Text = ds1.Tables[0].Rows[0]["TOTAL_QUOTED_COST"].ToString();
        //        // Session["currancy"] = ds1.Tables[0].Rows[0]["CURRANCY_NAME"].ToString();
        //        UpdatePanel_Generate_Invoice.Update();
        //    }

        //}

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

        #region textchanged

        protected void row1_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m  = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row2_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row3_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row4_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row5_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row6_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row7_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row8_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row9_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
            textchange();
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void row10_textchanged(object sender, EventArgs e)
        {
            //decimal a;
            //decimal b;
            //decimal c;
            //decimal d;
            //decimal g;
            //decimal h;
            //decimal i;
            //decimal j;
            //decimal k;
            //decimal l;
            //decimal m;
            //decimal o;
            //decimal total;
            //if (row1_txt_debit.Text == "")
            //{
            //    a = 0;
            //}
            //else
            //{
            //    a = decimal.Parse(row1_txt_debit.Text);
            //}
            //if (TextBox3.Text == "")
            //{
            //    b = 0;
            //}
            //else
            //{
            //    b = decimal.Parse(TextBox3.Text);
            //}
            //if (TextBox5.Text == "")
            //{
            //    c = 0;
            //}
            //else
            //{
            //    c = decimal.Parse(TextBox5.Text);
            //}
            //if (TextBox7.Text == "")
            //{
            //    d = 0;
            //}
            //else
            //{
            //    d = decimal.Parse(TextBox7.Text);
            //}
            //if (TextBox9.Text == "")
            //{
            //    g = 0;
            //}
            //else
            //{
            //    g = decimal.Parse(TextBox9.Text);
            //}
            //if (TextBox11.Text == "")
            //{
            //    j = 0;
            //}
            //else
            //{
            //    j = decimal.Parse(TextBox11.Text);
            //}
            //if (TextBox13.Text == "")
            //{
            //    k = 0;
            //}
            //else
            //{
            //    k = decimal.Parse(TextBox13.Text);
            //}
            //if (TextBox15.Text == "")
            //{
            //    l = 0;
            //}
            //else
            //{
            //    l = decimal.Parse(TextBox15.Text);
            //}
            //if (TextBox16.Text == "")
            //{
            //    m = 0;
            //}
            //else
            //{
            //    m = decimal.Parse(TextBox16.Text);
            //}
            //if (TextBox19.Text == "")
            //{
            //    o = 0;
            //}
            //else
            //{
            //    o = decimal.Parse(TextBox19.Text);
            //}

            //total = a + b + c + d + g + j + k + l + m + o;
            //txtAmount.Text = total.ToString();
            //if (txtAmount.Text == "")
            //{
            //    h = 0;
            //}
            //else
            //{
            //    h = decimal.Parse(txtAmount.Text);
            //}
            //if (txtTax.Text == "")
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i = decimal.Parse(txtTax.Text);
            //}
            //decimal n = h + i;
            //txtTotalAmount.Text = n.ToString();
            //if (lbltotalInvoiceAmount.Text == "")
            //{
            //    txttotalInvoiceAmount.Text = n.ToString();
            //}
            //else
            //{
            //    lbltotalInvoiceAmount.Text = n.ToString();
            //}
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
            TextBox50.Text = c.ToString();
            textchange();
            TextBox50.Focus();
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
            TextBox50.Text = c.ToString();
            textchange();
            TextBox50.Focus();
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

            decimal exrate = 1;
            if (txtExRate.Text != "")
            {
                exrate = decimal.Parse(txtExRate.Text); 
            }
            decimal total1 = ((a + b + c + d + g + j + k + l + m + o + p + q + r + s + t) / exrate);
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
                txttotalInvoiceAmount.Text =string.Format("{0:#.00}", total1);
            }
            else
            {
                lbltotalInvoiceAmount.Text = string.Format("{0:#.00}",total1);
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
            if (lbltotalInvoiceAmount.Text != "")
            {
                lblFinalAmount.Text = lbltotalInvoiceAmount.Text;
            }
            else
            {
                lblFinalAmount.Text = txttotalInvoiceAmount.Text;
            }
            //}

            UpdatePanel_Generate_Invoice.Update();
        }

        #endregion

        protected void disable_control()
        {
            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            TextBox4.Enabled = false;
            TextBox5.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox13.Enabled = false;
            TextBox14.Enabled = false;
            TextBox15.Enabled = false;
            TextBox16.Enabled = false;
            TextBox17.Enabled = false;
            TextBox18.Enabled = false;
            TextBox19.Enabled = false;
            txtInvoiceNo.Enabled = false;

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
                else if (DateTime.ParseExact(txtPeriodStayTo.Text, "dd/MM/yyyy", null) <= DateTime.ParseExact(txtPeriodStayFrom.Text, "dd/MM/yyyy", null))
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



        protected void insertSalesDetails(int salesInvoiceId)
        {
            for (int i = 0; i < 14; i++)
            {
                if (i == 0)
                {
                    //if (TextBox1.Text == "SINGLE SHARE" && row1_drp_currency.Text != "" && row1_txt_debit.Text != "") //&& TextBox23.Text == "" && TextBox22.Text == "" )
                    //{
                    objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox1.Text, row1_txt_debit.Text, TextBox22.Text);
                    //}
                }

                else if (i == 1)
                {
                    //if (TextBox2.Text == "DOUBLE SHARE" && row2_drp_currency.Text != "" && TextBox3.Text != "") //&& TextBox24.Text == "" && TextBox25.Text == "")
                    //{
                    objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox2.Text, TextBox3.Text, TextBox24.Text);
                    // }
                }

                else if (i == 2)
                {
                    //if (TextBox4.Text == "TRIPLE SHARE" && row3_drp_currency.Text != "" && TextBox5.Text != "")//&& TextBox26.Text == "" && TextBox27.Text == "")
                    //{
                    objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox4.Text, TextBox5.Text, TextBox26.Text);

                    // }
                }

                else if (i == 3)
                {
                    //if (TextBox6.Text == "CHILD WITH BED" && row4_drp_currency.Text != "" && TextBox7.Text != "") //&& TextBox28.Text == "" && TextBox29.Text == "")
                    //{
                    objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox6.Text, TextBox7.Text, TextBox28.Text);
                    //}
                }

                else if (i == 4)
                {
                    //if (TextBox8.Text == "CHILD WITH NO BED" && row5_drp_currency.Text != "" && TextBox9.Text != "") //&& TextBox30.Text == "" && TextBox31.Text == "")
                    //{
                    objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox8.Text, TextBox9.Text, TextBox30.Text);
                    //}
                }
                else if (i == 5)
                {
                    if (TextBox10.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox10.Text, TextBox11.Text, TextBox32.Text);
                    }
                }
                else if (i == 6)
                {
                    if (TextBox12.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox12.Text, TextBox13.Text, TextBox34.Text);
                    }
                }
                else if (i == 7)
                {
                    if (TextBox14.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox14.Text, TextBox15.Text, TextBox36.Text);
                    }
                }
                else if (i == 8)
                {
                    if (TextBox17.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox17.Text, TextBox16.Text, TextBox38.Text);
                    }
                }
                else if (i == 9)
                {
                    if (TextBox18.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox18.Text, TextBox19.Text, TextBox40.Text);
                    }
                }
                else if (i == 10)
                {
                    if (TextBox43.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox43.Text, TextBox44.Text, TextBox46.Text);
                    }
                }
                else if (i == 11)
                {
                    if (TextBox47.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox47.Text, TextBox50.Text, TextBox48.Text);
                    }
                }
                else if (i == 12)
                {
                    if (TextBox51.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox51.Text, TextBox54.Text, TextBox52.Text);
                    }
                }
                else if (i == 13)
                {
                    if (TextBox55.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox55.Text, TextBox58.Text, TextBox56.Text);
                    }
                }
                else if (i == 14)
                {
                    if (TextBox59.Text != "")
                    {
                        objInvoiceThb.insertSalesDetails(salesInvoiceId, TextBox59.Text, TextBox62.Text, TextBox60.Text);
                    }
                }
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

        protected void fillDetailsforInvoice(DataSet DS)
        {
            for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
            {
                if (i == 0)
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
                        TextBox22.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                        decimal b = 1;
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                        {
                            b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
                        }
                        if (a == 0 && b == 0)
                        {
                            TextBox23.Text = "0";
                        }
                        else
                        {
                            TextBox23.Text = string.Format("{0:#.00}", a / b);

                        }
                        UpdatePanel_Generate_Invoice.Update();
                    }
                }

                else if (i == 1)
                {
                    if (TextBox2.Text == "DOUBLE SHARE" && row2_drp_currency.Text == "" && TextBox3.Text == "" && TextBox24.Text == "" && TextBox25.Text == "")
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
                        TextBox24.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                        
                        decimal b = 1;
                        
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                        {
                            b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
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
                }

                else if (i == 2)
                {
                    if (TextBox4.Text == "TRIPLE SHARE" && row3_drp_currency.Text == "" && TextBox5.Text == "" && TextBox26.Text == "" && TextBox27.Text == "")
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
                        TextBox26.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                        decimal b = 1;
                       
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                        {
                            b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
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
                }
                else if (i == 3)
                {
                    if (TextBox6.Text == "CHILD WITH BED" && row4_drp_currency.Text == "" && TextBox7.Text == "" && TextBox28.Text == "" && TextBox29.Text == "")
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
                        TextBox28.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();
                        decimal b = 1;
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                        {
                            b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
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
                }
                else if (i == 4)
                {
                    if (TextBox8.Text == "CHILD WITH NO BED" && row5_drp_currency.Text == "" && TextBox9.Text == "" && TextBox30.Text == "" && TextBox31.Text == "")
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
                        TextBox30.Text = DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString();

                        decimal b = 1;
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
                        {
                            b = decimal.Parse(DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString());
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
                        decimal b = 1;
                        if (DS.Tables[1].Rows[i]["NO_OF_PERSON"].ToString() != "")
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
        }

        protected void updateSalesDetails()
        {
            if (ViewState["s6"] == null)
            {
                ViewState["s6"] = "0";
            }
            if (ViewState["s7"] == null)
            {
                ViewState["s7"] = "0";
            }
            if (ViewState["s8"] == null)
            {
                ViewState["s8"] = "0";
            }
            if (ViewState["s9"] == null)
            {
                ViewState["s9"] = "0";
            }
            if (ViewState["s10"] == null)
            {
                ViewState["s10"] = "0";
            }
            if (ViewState["s11"] == null)
            {
                ViewState["s11"] = "0";
            }
            if (ViewState["s12"] == null)
            {
                ViewState["s12"] = "0";
            }
            if (ViewState["s13"] == null)
            {
                ViewState["s13"] = "0";
            }
            if (ViewState["s14"] == null)
            {
                ViewState["s14"] = "0";
            }
            if (ViewState["s15"] == null)
            {
                ViewState["s15"] = "0";
            }
            DataSet dso = objInvoiceThb.insert_sales_invoice_details(int.Parse(Request.QueryString["TOURID"].ToString()), int.Parse(ViewState["s1"].ToString()), int.Parse(ViewState["s2"].ToString()), int.Parse(ViewState["s3"].ToString()), int.Parse(ViewState["s4"].ToString()), int.Parse(ViewState["s5"].ToString()), int.Parse(ViewState["s6"].ToString()), int.Parse(ViewState["s7"].ToString()), int.Parse(ViewState["s8"].ToString()), int.Parse(ViewState["s9"].ToString()), int.Parse(ViewState["s10"].ToString()), int.Parse(ViewState["s11"].ToString()), int.Parse(ViewState["s12"].ToString()), int.Parse(ViewState["s13"].ToString()), int.Parse(ViewState["s14"].ToString()), int.Parse(ViewState["s15"].ToString()), row1_txt_debit.Text, TextBox3.Text, TextBox5.Text, TextBox7.Text, TextBox9.Text, TextBox11.Text, TextBox13.Text, TextBox15.Text, TextBox16.Text, TextBox19.Text, TextBox46.Text, TextBox50.Text, TextBox54.Text, TextBox58.Text, TextBox62.Text, TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox6.Text, TextBox8.Text, TextBox10.Text, TextBox12.Text, TextBox14.Text, TextBox17.Text, TextBox18.Text, TextBox43.Text, TextBox47.Text, TextBox51.Text, TextBox55.Text, TextBox59.Text, drpCurrency.Text, TextBox22.Text, TextBox24.Text, TextBox26.Text, TextBox28.Text, TextBox30.Text, TextBox32.Text, TextBox34.Text, TextBox36.Text, TextBox38.Text, TextBox40.Text, TextBox44.Text, TextBox48.Text, TextBox52.Text, TextBox56.Text, TextBox60.Text);
        }

        protected void updateSalesHeader()
        {
            objInvoiceThb.insert_sales_invoice_header(int.Parse(Request.QueryString["TOURID"].ToString()), int.Parse("0"), int.Parse(Session["uid"].ToString()), int.Parse(Session["rel_sr_no"].ToString()), txtPeriodStayFrom.Text, txtPeriodStayTo.Text, txtNoOfNights.Text, decimal.Parse(txtAmount.Text), decimal.Parse(txtTax.Text), decimal.Parse(txtTotalAmount.Text), txtNoOfAdult.Text, "0", txtNoOfCWB.Text, txtNoOfCNB.Text, txtNoOfInfant.Text, drpOrderStatus.Text, drpPaymentMode.Text, txtBook_ref_no.Text, TextBox42.Text, "THB", 1, txtclientname.Text, decimal.Parse(txtExRate.Text),"","");

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
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Invoices/" + salesInvoiceId  + "/"));

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

                string subjct = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                subjct = subjct.Replace("<%CLIENTNAME%>", txtclientname.Text);
                subjct = subjct.Replace("<%FROMDATE%>", txtPeriodStayFrom.Text );
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
                textchange();

            }
            UpdatePanel_Generate_Invoice.Update();
        }
    }
}
