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
    public partial class PurchaseVoucher : System.Web.UI.Page
    {
        PurchaseVoucherStoredProcedure objPurchaseVoucherStoredProcedure = new PurchaseVoucherStoredProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        VouchersStoredProcedure objVouchersStoredProcedure = new VouchersStoredProcedure();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        CRM.DataAccess.FIT.ServiceVoucherDetail objservicevoucher = new CRM.DataAccess.FIT.ServiceVoucherDetail();
        #region VARIABLE

        bool flag_date = true;

        #endregion

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 287);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                {
                    DataSet ds_main = objPurchaseVoucherStoredProcedure.fetch_record_while_edit("FETCH_ALL_RECORDS_PURCHASE", int.Parse(Request["VN"].ToString()));
                    DataSet ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");

                    DataSet ds11 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, ds11);
                    drpsupplier_type.Text = ds_main.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString();
                    drpsupplier_type.Enabled = false;

                    DataSet ds22 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drpcurrency_payment, ds22);

                    DataSet ds3 = objPurchaseVoucherStoredProcedure.fetch_supplier("FETCH_SUPPLIER_HOTEL_SS_TP", ds_main.Tables[0].Rows[0]["SUPPLIER_TYPE_NAME"].ToString());
                    binddropdownlist(drpsupplier, ds3);
                    drpsupplier.Text = ds_main.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();
                    drpsupplier.Enabled = false;

                    txtdue_date.Text = ds_main.Tables[0].Rows[0]["DUE_DATE"].ToString();
                    txtgl_date.Text = ds_main.Tables[0].Rows[0]["GL_DATE"].ToString();

                    DataSet ds7 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(drpinvoice_no, ds7);

                    txtPurchaseVoucher.Text = ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString();

                   

                        drpinvoice_no.Text = ds_main.Tables[0].Rows[0]["SALES_INVOICE_NO"].ToString();
                        drpinvoice_no.Enabled = false;
                  

                    DataSet ds111 = objPurchaseVoucherStoredProcedure.fetch_common_data("FETCH_INVOICE_DETAILS_FOR_PURCHASE", drpinvoice_no.Text);
              
                   
                    txtinvoice_date.Text = ds111.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
                    txtno_of_adults.Text = ds111.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                    txtno_of_cnb.Text = ds111.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                    txtno_of_cwb.Text = ds111.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                    txtno_of_infant.Text = ds111.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();

                    #region HOTEL
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
                    {
                        DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();
                        lblhotel.Text = "HOTEL";
                        hotel_table.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;

                     
                        txt_room_type.Text = ds_main.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
                      
                        txtperiod_stay_from.Text = ds_main.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString();
                        txtperiod_stay_to.Text = ds_main.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString();
                
                        txtno_of_nights.Text = ds_main.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();

                     
                        txtsingle_room.Text = ds_main.Tables[0].Rows[0]["NO_OF_ROOM_SINGLE"].ToString();

                      
                        txtdouble_room.Text = ds_main.Tables[0].Rows[0]["NO_OF_ROOM_DOUBLE"].ToString();
                       
                        txttriple_room.Text = ds_main.Tables[0].Rows[0]["NO_OF_RROM_TRIPLE"].ToString();

                    
                        drpcurrency_payment.Text = "THB";
                        
                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();





                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region TRANSFER PACKAGE 
                    //TRANSFER PACKAGE
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString() || drpsupplier_type.Text == "Transport Company")
                    {
                        lblhotel.Text = "TRANSFER PACKAGE";
                        DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();

                        //*******************************************FETCH_SS_TP_FOR_PURCHASE (PREVIOUS SP USED DATA SET ds_tp_data)
                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART", drpinvoice_no.Text, drpsupplier.Text);

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            DataSet ds1 = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART_MANUAL", drpinvoice_no.Text, drpsupplier.Text);
                            dltp_ss.DataSource = ds1;
                        }
                        else
                        {
                            dltp_ss.DataSource = ds;
                        }
                        dltp_ss.DataBind();

                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                int i = item.ItemIndex;
                               
                                Label lbl_no = (Label)item.FindControl("lblsrno");
                                lbl_no.Text = (i + 1).ToString();
                                DropDownList drp = (DropDownList)item.FindControl("drpflag");
                                drp.SelectedValue = ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                                DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                                DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                                binddropdownlist(drp_currency, ds2);
                                drp_currency.Text = ds2.Tables[0].Rows[0]["AutoSearchResult"].ToString();
                                
                                if (txt_amount.Text == "")
                                {
                                    txt_amount.Text = "0";
                                }



                                TextBox txt = (TextBox)item.FindControl("txtamount");
                                txt.Text = ds_main.Tables[0].Rows[i]["TP_AMOUNT"].ToString();

                                if (txtno_of_adults.Text != "0" && txtno_of_adults.Text != "")
                                {
                                    if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                                    {
                                       
                                    }

                                    else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                                    {
                                     
                                    }

                                    else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                                    {
                                  
                                    }

                                    else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                                    {
                                     
                                    }
                                }
                                drpcurrency_payment.Text = drp_currency.Text;
                            }
                            else
                            {
                                DataSet ds1 = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART_MANUAL", drpinvoice_no.Text, drpsupplier.Text);

                                int i = item.ItemIndex;
                              
                                Label lbl_no = (Label)item.FindControl("lblsrno");
                                lbl_no.Text = (i + 1).ToString();
                                DropDownList drp = (DropDownList)item.FindControl("drpflag");
                                drp.SelectedValue = ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                                DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                                DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                                binddropdownlist(drp_currency, ds2);
                                drp_currency.Text = ds2.Tables[0].Rows[0]["AutoSearchResult"].ToString();
                              
                                if (txt_amount.Text == "")
                                {
                                    txt_amount.Text = "0";
                                }



                                TextBox txt = (TextBox)item.FindControl("txtamount");
                                txt.Text = ds_main.Tables[0].Rows[i]["TP_AMOUNT"].ToString();

                                if (txtno_of_adults.Text != "0" && txtno_of_adults.Text != "")
                                {
                                    if (ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds1.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                                    {
                                        
                                    }

                                    else if (ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds1.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                                    {
                                     
                                    }

                                    else if (ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds1.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                                    {
                                        
                                    }

                                    else if (ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds1.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                                    {
                                       
                                    }
                                }
                                drpcurrency_payment.Text = drp_currency.Text;
                            }
                           
                        }
                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                    }
                    #endregion

                    #region SITE SEEING
                    // SIGHT SEEING
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "SITE SEEING";
                        DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();
                        string sic_adult_rate;
                        string sic_child_rate;
                        string pvt_adult_rate;
                        string pvt_child_rate;
                        //***************************************************** FETCH_SIGHT_SEEING_FOR_PURCHASE (PREVIOUS SP USED DATA SET ds_ss_data)
                        
                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_RATE_FROM_SERVICE_CART", drpinvoice_no.Text, drpsupplier.Text);

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            DataSet ds1 = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_RATE_FROM_SERVICE_CART_MANUAL_EDIT", drpinvoice_no.Text, drpsupplier.Text);
                            dltp_ss.DataSource = ds1;
                        }

                        else
                        {
                            dltp_ss.DataSource = ds;
                        }
                        dltp_ss.DataBind();

                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                int i = item.ItemIndex;
                               
                                Label lbl_no = (Label)item.FindControl("lblsrno");
                                lbl_no.Text = (i + 1).ToString();

                                DropDownList drp = (DropDownList)item.FindControl("drpflag");
                                if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() != "")
                                {
                                    
                                    drp.SelectedValue = ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();
                                }
                                else
                                {
                                    drp.Visible = false;
                                }

                                

                                DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                                DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                                binddropdownlist(drp_currency, ds2);
                                drp_currency.Text = "THB";
                               

                                TextBox txt = (TextBox)item.FindControl("txtamount");
                                txt.Text = ds_main.Tables[0].Rows[i]["TP_AMOUNT"].ToString();


                                if (txtno_of_adults.Text != "0" && txtno_of_adults.Text != "")
                                {
                                    if (ds.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString() == "")
                                    {
                                        sic_adult_rate = "0";
                                    }
                                    else
                                    {
                                        sic_adult_rate = ds.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString();
                                    }
                                    if (ds.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString() == "")
                                    {
                                        sic_child_rate = "0";
                                    }
                                    else
                                    {
                                        sic_child_rate = ds.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString();
                                    }

                                    if (ds.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString() == "")
                                    {
                                        pvt_adult_rate = "0";
                                    }
                                    else
                                    {
                                        pvt_adult_rate = ds.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString();
                                    }
                                    if (ds.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString() == "")
                                    {
                                        pvt_child_rate = "0";
                                    }
                                    else
                                    {
                                        pvt_child_rate = ds.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString();
                                    }

                                    if (txt_amount.Text == "")
                                    {
                                        txt_amount.Text = "0";
                                    }


                                    if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC")
                                    {
                                       
                                    }

                                    else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT")
                                    {
                                     
                                    }
                                }
                                drpcurrency_payment.Text = drp_currency.Text;
                            }
                            else
                            {
                                DataSet ds1 = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_RATE_FROM_SERVICE_CART_MANUAL_EDIT", drpinvoice_no.Text, drpsupplier.Text);
                                int i = item.ItemIndex;
                                
                                Label lbl_no = (Label)item.FindControl("lblsrno");
                                lbl_no.Text = (i + 1).ToString();

                                DropDownList drp = (DropDownList)item.FindControl("drpflag");
                                drp.SelectedValue = ds1.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                                DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                                DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                                binddropdownlist(drp_currency, ds2);
                                drp_currency.Text = "THB";
                                

                                TextBox txt = (TextBox)item.FindControl("txtamount");
                                txt.Text = ds_main.Tables[0].Rows[i]["TP_AMOUNT"].ToString();
                            }

                           
                        }
                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();


                    }
                    #endregion

                    #region ADDITIONAL SERVICES FIT
                    // ADDITIONAL SERIVICES
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "ADDITIONAL SERVICES";
                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_ADDITIONAL_SERVICES_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text);
                        dltp_ss.DataSource = ds;
                        dltp_ss.DataBind();

                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            int i = item.ItemIndex;

                            Label lbl_no = (Label)item.FindControl("lblsrno");
                            lbl_no.Text = (i + 1).ToString();

                            DropDownList drp = (DropDownList)item.FindControl("drpflag");
                            drp.SelectedValue = ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                            binddropdownlist(drp_currency, ds2);
                            drp_currency.Text = "THB";

                            TextBox txt = (TextBox)item.FindControl("txtamount");
                            txt.Text = ds.Tables[0].Rows[i]["PURCHASE_INVOICE_AMOUNT"].ToString();
                        }

                        drpcurrency_payment.Text = "THB";
                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();
                    }
                    #endregion

                    #region RESTURANT 

                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[6]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "RESTURANT";

                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_RESTURANT_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        txtDate.Text = ds.Tables[0].Rows[0]["MEAL_DATE"].ToString();

                        lblType.Text = "Meal Type";
                        txtType.Text = ds.Tables[0].Rows[0]["MEAL_TYPE"].ToString();
                       

                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region CONFERENCE
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[12]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "CONFERENCE DETAILS";
                        

                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_CONFERENCE_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        txtDate.Text = ds.Tables[0].Rows[0]["CONFERENCE_DATE"].ToString();

                        lblType.Text = "Conference Type";
                        txtType.Text = ds.Tables[0].Rows[0]["CONFERENCE_TYPE"].ToString();

                        trTime.Visible = true;
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        }
                        

                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region GALA DINNER
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[13]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "GALA DINNER DETAILS";


                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_GALA_DINNER_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        txtDate.Text = ds.Tables[0].Rows[0]["CONFERENCE_DATE"].ToString();

                        lblType.Text = "Gala Dinner Type";
                        txtType.Text = ds.Tables[0].Rows[0]["GALA_DINNER_TYPE"].ToString();

                        trTime.Visible = true;
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        }


                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region BOAT COMPANY

                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[10]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "BOAT COMPANY";

                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_BOAT_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        txtDate.Text = ds.Tables[0].Rows[0]["CONFERENCE_DATE"].ToString();

                        lblType.Visible = false;
                        txtType.Visible = false;
                        lblType.Text = "No of Boats : ";
                        txtType.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();

                        trTime.Visible = true;
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        }

                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region COACH COMPANY
                    
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[5]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "COACH COMPANY";

                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_COACH_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        txtDate.Text = ds.Tables[0].Rows[0]["CONFERENCE_DATE"].ToString();

                        lblType.Visible = false;
                        txtType.Visible = false;
                        //lblType.Text = "No of Boats :- ";
                        //txtType.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();

                        trTime.Visible = true;
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        }

                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    #region GUIDE COMPANY

                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[7]["AutoSearchResult"].ToString())
                    {
                        lblhotel.Text = "GUIDE COMPANY";

                        tblDiffType.Attributes.Add("style", "display");
                        txt_amount.Enabled = true;



                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_GUIDE_FOR_PURCHASE", txtPurchaseVoucher.Text, drpsupplier.Text);
                        //txtDate.Text = ds.Tables[0].Rows[0]["CONFERENCE_DATE"].ToString();

                        lblDate.Visible = false;
                        txtDate.Visible = false;
                        lblType.Text = "No of Guides : ";
                        txtType.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();

                        //trTime.Visible = true;
                        //if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        //{
                        //    RadTimePicker1.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        //}

                        drpcurrency_payment.Text = "THB";

                        txt_amount.Text = ds_main.Tables[0].Rows[0]["AMOUNT"].ToString();
                        txttotal_amount.Text = ds_main.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        txttax.Text = ds_main.Tables[0].Rows[0]["TOTAL_TAX_AMOUNT"].ToString();

                        UpdatePanel1.Update();
                        UpdatePanel_Generate_Invoice.Update();
                        update_payment.Update();
                    }
                    #endregion

                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    DataSet ds_vn_check = objVouchersStoredProcedure.get_voucher_no_for_check("FETCH_VOUCHER_NO_FOR_CHECK", ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString());

                    if (ds_vn_check != null)
                    {
                        if (ds_vn_check.Tables[0].Rows[0]["VOUCHER_NO"].ToString() != "")
                        {
                            disable_control();
                        }
                    }
                    UpdatePanel_Generate_Invoice.Update();
                    UpdatePanel1.Update();
                    update_payment.Update();
                }
                else
                {
                    DataSet ds = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
                    binddropdownlist(drpsupplier_type, ds);

                    DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                    binddropdownlist(drpcurrency_payment, ds2);

                    /* DATE */
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

                }
            }
        }

        #region BIND DROP DOWN METHOD
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
          //  r.SelectedValue = "0";
        }
        #endregion

        protected void drpsupplier_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objPurchaseVoucherStoredProcedure.fetch_supplier("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier_type.Text);
            binddropdownlist(drpsupplier, ds);

            UpdatePanel_Generate_Invoice.Update();
        }

        protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_sup_type = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");
          

            if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
            {
                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_INVOICE_NO_FROM_SUPPLIER", drpsupplier.Text);
                binddropdownlist(drpinvoice_no, ds);
            }
            else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString())
            {
                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SIGHT_SEEING_INVOICE_NO_FOR_PURCHASE", drpsupplier.Text);
                binddropdownlist(drpinvoice_no, ds);
            }
            else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString())
            {
                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_INVOICE_NO_FOR_TRANSFER_PACKAGE", drpsupplier.Text);
                binddropdownlist(drpinvoice_no, ds);
            }

            else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString())
            {
                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_invoice_no("FETCH_SUPPLIER_HOTEL_SS_TP", drpsupplier.Text);
                binddropdownlist(drpinvoice_no, ds);
            }

            UpdatePanel_Generate_Invoice.Update();
        }

        protected void drpinvoice_no_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds11 = objPurchaseVoucherStoredProcedure.fetch_common_data("FETCH_INVOICE_DETAILS_FOR_PURCHASE", drpinvoice_no.Text);
            txtinvoice_date.Text = ds11.Tables[0].Rows[0]["ISSUED_DATE"].ToString();
            txtno_of_adults.Text = ds11.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
            txtno_of_cnb.Text = ds11.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
            txtno_of_cwb.Text = ds11.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
            txtno_of_infant.Text = ds11.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();

            if (drpsupplier_type.Text == "Hotel")
            {
                DataSet ds1 = objPurchaseVoucherStoredProcedure.fetch_hotel_data("FETCH_INVOICE_NO_DETAILS_FROM_SUPPLIER", drpinvoice_no.Text, drpsupplier_type.Text);
                DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();
                lblhotel.Text = "HOTEL";
                hotel_table.Attributes.Add("style", "display");
                txt_amount.Enabled = true;
                
                
                txt_room_type.Text = ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
              
                txtperiod_stay_from.Text = ds1.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString();
                txtperiod_stay_to.Text = ds1.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString();
                DateTime date1 = DateTime.ParseExact(txtperiod_stay_from.Text, "dd/MM/yyyy", null);
                DateTime date2 = DateTime.ParseExact(txtperiod_stay_to.Text, "dd/MM/yyyy", null);
                TimeSpan ts;
                ts = date2.Subtract(date1.Date);
                txtno_of_nights.Text = ts.TotalDays.ToString();

                if (txt_amount .Text == "")
                {
                    txt_amount.Text = "0";
                }

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "1")
                    {
                        txtsingle_room.Text = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();

                        txt_amount.Text = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["SINGLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights.Text)));
                    }
                    else if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "2")
                    {
                        txtdouble_room.Text = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();
                        txt_amount.Text = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["DOUBLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights.Text)));
                        
                    }
                    else if (ds1.Tables[0].Rows[i]["ROOM_TYPE"].ToString() == "3")
                    {
                        txttriple_room.Text = ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString();
                        if (ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString() != "" && ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString() != null)
                        {
                            txt_amount.Text = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["TRIPLE_ROOM_RATE"].ToString()) * decimal.Parse(txtno_of_nights.Text)));
                        }
                        else
                        {
                            txt_amount.Text = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text) + (decimal.Parse(ds1.Tables[0].Rows[i]["NO_OF_ROOMS"].ToString()) * decimal.Parse(ds1.Tables[0].Rows[i]["EXTRA_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_nights.Text)));
                        }
                    }

                }
                drpcurrency_payment.Text = "USD";
                txt_amount.Text  = string.Format("{0:#.00}", decimal.Parse(txt_amount.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString()));
                txttotal_amount.Text = txt_amount.Text;

               



                UpdatePanel1.Update();
                UpdatePanel_Generate_Invoice.Update();
                update_payment.Update();
            }

//TRANSFER PACKAGE
            if (drpsupplier_type.Text == "Transfer Package Company")
            {
                lblhotel.Text = "TRANSFER PACKAGE";
                DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();

                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text );
                dltp_ss.DataSource = ds;
                dltp_ss.DataBind();

                foreach (DataListItem item in dltp_ss.Items)
                {
                    int i = item.ItemIndex;
                    
                    Label lbl_no = (Label)item.FindControl("lblsrno");
                    lbl_no.Text = (i + 1).ToString();
                        DropDownList drp = (DropDownList)item.FindControl("drpflag");
                        drp.SelectedValue = ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                        DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                        DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                        binddropdownlist(drp_currency, ds2);
                        drp_currency.Text = ds.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                   
                        if (txt_amount.Text == "")
                        {
                            txt_amount.Text = "0";
                        }

                        TextBox txt = (TextBox)item.FindControl("txtamount");
                        if (txtno_of_adults.Text != "0" && txtno_of_adults.Text != "")
                        {
                            if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(ds.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString()) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(ds.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));

                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }

                            else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() != "S")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(ds.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString()) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(ds.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));

                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }

                            else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(ds.Tables[0].Rows[i]["SIC_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(ds.Tables[0].Rows[i]["SIC_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));
                         
                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }

                            else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT" && ds.Tables[0].Rows[i]["ARRIVAL_DEPARTURE_FLAG"].ToString() == "S")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(ds.Tables[0].Rows[i]["PVT_ADULT_RATE"].ToString()) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(ds.Tables[0].Rows[i]["PVT_CHILD_RATE"].ToString()) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));
                              
                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }
                        }
                        drpcurrency_payment.Text = drp_currency.Text;

                }
                txttotal_amount.Text = txt_amount.Text;
               
               
            }

            // SIGHT SEEING
           if (drpsupplier_type.Text == "Sightseeing Company")
            {
                lblhotel.Text = "SIGHT SEEING";
                DataSet ds_rate = objPurchaseVoucherStoredProcedure.fetch_conversion_rate();
                string sic_adult_rate;
                string sic_child_rate;
                string pvt_adult_rate;
                string pvt_child_rate;

                DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SIGHT_SEEING_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text );
                dltp_ss.DataSource = ds;
                dltp_ss.DataBind();

                foreach (DataListItem item in dltp_ss.Items)
                {
                    int i = item.ItemIndex;
                    
                    Label lbl_no = (Label)item.FindControl("lblsrno");
                    lbl_no.Text = (i + 1).ToString();

                        DropDownList drp = (DropDownList)item.FindControl("drpflag");
                        drp.SelectedValue = ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();

                        DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                        DataSet ds2 = objPurchaseVoucherStoredProcedure.fetch_supplier_type("FETCH_CURRENCY_NAME_FOR_VOUCHER");
                        binddropdownlist(drp_currency, ds2);
                        drp_currency.Text = ds.Tables[0].Rows[i]["CURRENCY_NAME"].ToString();
                   

                        TextBox txt = (TextBox)item.FindControl("txtamount");
                        if (txtno_of_adults.Text != "0" && txtno_of_adults.Text != "")
                        {
                            if (ds.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString() == "")
                            {
                                sic_adult_rate = "0";
                            }
                            else
                            {
                                sic_adult_rate = ds.Tables[0].Rows[i]["ADULT_SIC_RATE"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString() == "")
                            {
                                sic_child_rate  = "0";
                            }
                            else
                            {
                                sic_child_rate = ds.Tables[0].Rows[i]["CHILD_SIC_RATE"].ToString();
                            }

                            if (ds.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString() == "")
                            {
                                pvt_adult_rate  = "0";
                            }
                            else
                            {
                                pvt_adult_rate = ds.Tables[0].Rows[i]["ADULT_PVT_RATE"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString() == "")
                            {
                                pvt_child_rate  = "0";
                            }
                            else
                            {
                                pvt_child_rate = ds.Tables[0].Rows[i]["CHILD_PVT_RATE"].ToString();
                            }

                            if (txt_amount.Text == "")
                            {
                                txt_amount.Text = "0";
                            }


                            if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "SIC")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(sic_adult_rate) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(sic_child_rate) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));


                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }

                            else if (ds.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString() == "PVT")
                            {
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(pvt_adult_rate) * decimal.Parse(txtno_of_adults.Text) + decimal.Parse(pvt_child_rate) * (decimal.Parse(txtno_of_cnb.Text) + decimal.Parse(txtno_of_cwb.Text) + decimal.Parse(txtno_of_infant.Text))));
                                txt.Text = string.Format("{0:#.00}", (decimal.Parse(txt.Text) / decimal.Parse(ds_rate.Tables[0].Rows[0]["CONVERSION_RATE"].ToString())));

                                txt_amount.Text = string.Format("{0:#.00}", (decimal.Parse(txt_amount.Text) + decimal.Parse(txt.Text)));
                            }
                        }
                        drpcurrency_payment.Text = drp_currency.Text;
                   
                }
                txttotal_amount.Text = txt_amount.Text;

               
            }
             UpdatePanel_Generate_Invoice.Update();
           UpdatePanel1.Update();
           update_payment.Update();
            }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (txtdue_date.Text == "")
            {
                Master.DisplayMessage("Enter Due Date.", "successMessage", 5000);
            }
            else if (txtgl_date.Text == "")
            {
                Master.DisplayMessage("GL Date is Required..", "successMessage", 5000);
            }
            else if (flag_date == false)
            {
                Master.DisplayMessage("Entered due date is not in valid format", "successMessage", 5000);
            }
            else if (drpsupplier_type.Text == "")
            {
                Master.DisplayMessage("Supplier Type can not be empty", "successMessage", 5000);
            }
            else if (drpsupplier.Text == "")
            {
                Master.DisplayMessage("Supplier can not be empty", "successMessage", 5000);
            }
            else if (drpinvoice_no.Text == "")
            {
                Master.DisplayMessage("Against Slaes Invoice can not be empty", "successMessage", 5000);
            }


            else
            {
                DataSet ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");

                if (Request["VN"] != null && !string.IsNullOrEmpty(Request["VN"].ToString()))
                {
                    DataSet ds_main = objPurchaseVoucherStoredProcedure.fetch_record_while_edit("FETCH_ALL_RECORDS_PURCHASE", int.Parse(Request["VN"].ToString()));
                    DataSet ds_check = objAcoountVouchersStoredProcedure.fetch_account_records(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), "PURCHASE");

                    #region HOTEL
                    if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse(txtno_of_nights.Text), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txt_room_type.Text, 0, "", "", "", "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse(txtno_of_nights.Text), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txt_room_type.Text, 0, "", "", "", "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region TRANSFER + TRANSPORT PACKAGE
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString() || drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[9]["AutoSearchResult"].ToString())
                    {
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), txt_room_type.Text, 0, "", "", "", "", "", txtgl_date.Text, 1);
                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            int i = item.ItemIndex;

                            Label lbl_desc = (Label)item.FindControl("lbldescription");
                            DropDownList drp_flag = (DropDownList)item.FindControl("drpflag");
                            TextBox txt_amount1 = (TextBox)item.FindControl("txtamount"); //(TextBox)item.FindControl("txtamount");
                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            TextBox txt_date = (TextBox)item.FindControl("txtdate");

                            DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text);

                            if (ds.Tables[0].Rows.Count != 0)
                            {

                                
                                objPurchaseVoucherStoredProcedure.update_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[i]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), drp_flag.Text, "", txt_date.Text, "", txt_amount1.Text, txtgl_date.Text, 2);
                            }
                            else
                            {
                                DataSet ds1 = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART_MANUAL", drpinvoice_no.Text, drpsupplier.Text);
                                objPurchaseVoucherStoredProcedure.update_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[i]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds1.Tables[0].Rows[i]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), drp_flag.Text, "", txt_date.Text, "", txt_amount1.Text, txtgl_date.Text, 2);
                            }
                        }
                    }
                    #endregion

                    #region SITE SEEING 
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString())
                    {
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), 0, 0, 0, txt_room_type.Text, 0, "", "", "", "", "", txtgl_date.Text, 1);
                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            int i = item.ItemIndex;
                            Label lbl_desc = (Label)item.FindControl("lbldescription");
                            DropDownList drp_flag = (DropDownList)item.FindControl("drpflag");
                            TextBox txt_amount1 = (TextBox)item.FindControl("txtamount");
                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            TextBox txt_date = (TextBox)item.FindControl("txtdate");

                            
                            objPurchaseVoucherStoredProcedure.update_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[i]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", lbl_desc.Text, txt_date.Text, drp_flag.Text, txt_amount1.Text, txtgl_date.Text, 2);
                        }
                    }
                    #endregion

                    #region ADDITIONAL SERVICE FOR FIT 
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[8]["AutoSearchResult"].ToString())
                    {
                        DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_ADDITIONAL_SERVICES_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), 0, 0, 0, txt_room_type.Text, 0, "", "", "", "", "", txtgl_date.Text, 1);
                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            int i = item.ItemIndex;
                            Label lbl_desc = (Label)item.FindControl("lbldescription");
                            DropDownList drp_flag = (DropDownList)item.FindControl("drpflag");
                            TextBox txt_amount1 = (TextBox)item.FindControl("txtamount");
                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            TextBox txt_date = (TextBox)item.FindControl("txtdate");

                            
                            objPurchaseVoucherStoredProcedure.update_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[i]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", lbl_desc.Text, txt_date.Text, drp_flag.Text, txt_amount1.Text, txtgl_date.Text, 2);
                            objPurchaseVoucherStoredProcedure.update_additional_service_cart_amount(txt_amount1.Text, int.Parse(ds.Tables[0].Rows[i]["ADDITIONAL_SERVICE_CART_ID"].ToString()));

                        }
                    }
                    #endregion

                    #region RESTURANT 
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[6]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region CONFERENCE
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[12]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region GALA DINNER 
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[13]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region BOAT
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[10]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region COACH
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[5]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    #region GUIDE
                    else if (drpsupplier_type.Text == ds_sup_type.Tables[0].Rows[7]["AutoSearchResult"].ToString())
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 1);
                        objPurchaseVoucherStoredProcedure.update_purchase_entry(int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_ID"].ToString()), drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, int.Parse(ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_DETAILS_ID"].ToString()), "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txtType.Text, 0, "", "", txtDate.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);


                    }
                    #endregion

                    objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[0]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), txttotal_amount.Text, txttotal_amount.Text, txttotal_amount.Text, txtgl_date.Text, 1);
                    for (int i = 0; i < ds_check.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), txttotal_amount.Text, "", txttotal_amount.Text, txtgl_date.Text, 2);
                        }
                        else if (i == 1)
                        {
                            objPurchaseVoucherStoredProcedure.update_account_voucher_amount(0, ds_main.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), int.Parse(ds_check.Tables[0].Rows[0]["SEQ_NO"].ToString()), int.Parse(ds_check.Tables[0].Rows[i]["ACCOUNT_VOUCHER_DETAILS_ID"].ToString()), "", txttotal_amount.Text, txttotal_amount.Text, txtgl_date.Text, 2);
                        }
                    }
                    Master.DisplayMessage("Record Updated Successfully", "successMessage", 5000);
                }







                else
                {
                    if (drpsupplier_type.Text == "Hotel")
                    {
                        if (txtsingle_room.Text == "")
                        {
                            txtsingle_room.Text = "0";
                        }
                        if (txtdouble_room.Text == "")
                        {
                            txtdouble_room.Text = "0";
                        }
                        if (txttriple_room.Text == "")
                        {
                            txttriple_room.Text = "0";
                        }

                        objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse(txtno_of_nights.Text), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txt_room_type.Text, 0, "", "", "", "", "", 1);
                        objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse(txtno_of_nights.Text), int.Parse(txtsingle_room.Text), int.Parse(txtdouble_room.Text), int.Parse(txttriple_room.Text), txt_room_type.Text, 0, "", "", "", "", "", 2);
                    }
                    else if (drpsupplier_type.Text == "Transfer Package Company")
                    {
                        objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), txtperiod_stay_from.Text, txtperiod_stay_to.Text, int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), txt_room_type.Text, 0, "", "", "", "", "", 1);
                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            int i = item.ItemIndex;

                            Label lbl_desc = (Label)item.FindControl("lbldescription");
                            DropDownList drp_flag = (DropDownList)item.FindControl("drpflag");
                            TextBox txt_amount1 = (TextBox)item.FindControl("txtamount");
                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            TextBox txt_date = (TextBox)item.FindControl("txtdate");

                            DataSet ds = objPurchaseVoucherStoredProcedure.fetch_transfer_package("FETCH_SS_TP_FOR_PURCHASE", drpinvoice_no.Text, drpsupplier.Text);

                            
                            objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), drp_flag.Text, "", txt_date.Text, "", txt_amount1.Text, 2);
                        }
                    }
                    else if (drpsupplier_type.Text == "Sightseeing Company")
                    {
                        objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), txt_room_type.Text, 0, "", "", "", "", "", 1);
                        foreach (DataListItem item in dltp_ss.Items)
                        {
                            Label lbl_desc = (Label)item.FindControl("lbldescription");
                            DropDownList drp_flag = (DropDownList)item.FindControl("drpflag");
                            TextBox txt_amount1 = (TextBox)item.FindControl("txtamount");
                            DropDownList drp_currency = (DropDownList)item.FindControl("drpcurrency");
                            TextBox txt_date = (TextBox)item.FindControl("txtdate");

                            
                            objPurchaseVoucherStoredProcedure.insert_purchase_entry(0, drpsupplier.Text, drpinvoice_no.Text, int.Parse(Session["usersid"].ToString()), txtdue_date.Text, txt_amount.Text, txttax.Text, txttotal_amount.Text, drpcurrency_payment.Text, "", int.Parse(Session["usersid"].ToString()), drpsupplier_type.Text, 0, "DESCRIPTION", int.Parse(txtno_of_adults.Text), int.Parse(txtno_of_cwb.Text), int.Parse(txtno_of_cnb.Text), int.Parse(txtno_of_infant.Text), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", lbl_desc.Text, txt_date.Text, drp_flag.Text, txt_amount1.Text, 2);
                        }
                    }

                    Master.DisplayMessage("Record Save Successfully", "successMessage", 5000);

                }
            }
        }

        protected void txttax_TextChanged(object sender, EventArgs e)
        {
            txttotal_amount.Text = string.Format("{0:#.00}", decimal.Parse(txttax.Text) + decimal.Parse(txt_amount.Text));
            update_payment.Update();
        }

        protected void txtamount_TextChanged(object sender, EventArgs e)
        {
            txt_amount.Text = "";
            foreach (DataListItem item in dltp_ss.Items)
            {
                
                TextBox txt_amt = (TextBox)item.FindControl("txtamount");
                if (txt_amount.Text == "")
                {
                    txt_amount.Text = "0";
                }
                txt_amount.Text = string.Format("{0:#.00}", decimal.Parse(txt_amt.Text) + decimal.Parse(txt_amount.Text));
               
            }
            if (txttax.Text == "")
            {
                txttax.Text = "0";
            }
            txttotal_amount.Text = string.Format("{0:#.00}", decimal.Parse(txttax.Text) + decimal.Parse(txt_amount.Text));
           
            update_payment.Update();
        }

        protected void txtdue_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtdue_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered due date is not in valid format.", "successMessage", 8000);
                flag_date = false;
            }
            UpdatePanel_Generate_Invoice .Update();
        }

        protected void txtgl_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtgl_date.Text, "dd/MM/yyyy", null);
            }
            catch
            {
                Master.DisplayMessage("Entered GL date is not in valid format.", "successMessage", 8000);
                flag_date = false;
            }
            UpdatePanel_Generate_Invoice.Update();
        }

        protected void txt_amount_TextChanged(object sender, EventArgs e)
        {
            if (txttax.Text == "")
            {
                txttax.Text = "0";
            }
            txttotal_amount.Text = string.Format("{0:#.00}", decimal.Parse(txttax.Text) + decimal.Parse(txt_amount.Text));
            update_payment.Update();
        }

        protected void disable_control()
        {
            btn_save.Enabled = false;

            txttax.Enabled = false;

            txtperiod_stay_from.Enabled = false;

            txtperiod_stay_to.Enabled = false;

            txtno_of_nights.Enabled = false;

            txt_room_type.Enabled = false;

            txtsingle_room.Enabled = false;

            txtdouble_room.Enabled = false;

            txttriple_room.Enabled = false;

            dltp_ss.Enabled = false;

            txtinvoice_date.Enabled = false;
                        
            txtdue_date.Enabled = false;

            drpcurrency_payment.Enabled = false;
        }
          
        }
    }
