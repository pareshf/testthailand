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
using AjaxControlToolkit;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.SecurityDAL;
using CRM.DataAccess.Account;

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.Account
{
    public partial class TransferServiceVoucher : System.Web.UI.Page
    {

        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        CRM.DataAccess.FIT.TransferServiceDetail objTransferService = new CRM.DataAccess.FIT.TransferServiceDetail();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        CRM.DataAccess.FIT.HotelServiceVoucher objhotelSeviceVoucher = new CRM.DataAccess.FIT.HotelServiceVoucher();
        GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        CRM.DataAccess.Account.SearchHotelServiceVoucher ObjServiceVoucher = new CRM.DataAccess.Account.SearchHotelServiceVoucher();
        string viewstate;
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
                //Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()))
                {
                    string ino = Request["IN"].ToString();
                    DataSet ds5 = ObjServiceVoucher.TransferVoucherEdit("FETCH_DATA_FOR_TRANSFER_SERVICE_VOUCEHR_EDIT", ino);


                    txtpaxname.Text = ds5.Tables[0].Rows[0]["CLINT_NAME"].ToString();
                    drpAgent.Text = ds5.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    //agent fill invoice
                    DataSet ds = objhotelSeviceVoucher.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", ds5.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString());
                    ViewState["cust_rel_no"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();

                    //binddropdownlist(drpvoucher_type, ds);
                    ViewState["cust_sr_no"] = ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    DropDownList3.Items.Clear();

                    DataSet ds12 = objhotelSeviceVoucher.get_invoice_left("GET_INVOICE_NO", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                    //    DataSet ds1 = objVouchersStoredProcedure.get_invoice_no_agent_vise("FETCH_INVOICE_NO_AGENT_VISE", int.Parse(ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString()));
                    binddropdownlist(DropDownList3, ds12);

                    DropDownList3.Text = ds5.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", ds5.Tables[0].Rows[0]["INVOICE_NO"].ToString());
                    txtpaxname.Text = DSNAME.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
                    Session["fromdate"] = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
                    Session["todate"] = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
                    drpsupplier.Text = ds5.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    filltransferpackage(ds5.Tables[0].Rows[0]["CHAIN_NAME"].ToString());
                    FillTransferPackageDetails(ds5.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                    Session["qid"] = DSNAME.Tables[0].Rows[0]["QUOTE_ID"].ToString();
                    Session["uid"] = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                    lnkbtn.HRef = "~/Views/FIT/TransferManualvoucher/" + ds5.Tables[0].Rows[0]["TRANSFER_SERVICE_VOUCHER_ID"].ToString() + "/TransferVoucherManual.pdf";
                    lnkbtn.Attributes.Add("style", "display");
                }

                DataSet ds1 = objTransferService.GetAgent("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(drpAgent, ds1);
                DataSet ds4 = objTransferService.GetPlacename("FETCH_TRANSFER_PACKAGE_FOR_MANUAL");
                binddropdownlist(drpsupplier, ds4);

                Updatetransfervoucher.Update();
            }
        }

        protected void dlhoteldetails_ItemDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label l1 = (Label)e.Row.FindControl("lbltp_priceid");
                string strval = l1.Text;

                string title = viewstate;
                if (title == strval)
                {
                    Label l2 = (Label)e.Row.FindControl("lblTPsrno");
                    l2.Visible = false;

                    RadioButton btn = (RadioButton)e.Row.FindControl("rbtnTPselect");
                    btn.Visible = false;
                }
                else
                {
                    title = strval;
                    viewstate = title;

                    Label l2 = (Label)e.Row.FindControl("lblTPsrno");
                    l2.Visible = true;

                    RadioButton btn = (RadioButton)e.Row.FindControl("rbtnTPselect");
                    btn.Visible = true;
                }
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataValueField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            //r.SelectedValue = "0";
        }

        protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            filltransferpackage(drpsupplier.Text);
            Updatetransfervoucher.Update();
        }

        public void drptransfer_SelectedIndexChanged(Object sender, EventArgs e)
        {

            #region new coding dropdown in grid

            DropDownList ddl1 = (DropDownList)sender;
            int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;

            foreach (GridViewRow item in GridView10.Rows)
            {
                if (repeaterItemIndex == item.DataItemIndex)
                {
                    Label chk = (Label)item.FindControl("lbltp_flag");
                    Label tp_fromto_id = (Label)item.FindControl("lbltp_detialid");
                    DropDownList ddl = (DropDownList)item.FindControl("drptransfer");
                    DropDownList drp_time = (DropDownList)item.FindControl("tpdrp_time");
                    if (ddl.Text == "SIC")
                    {
                        DataSet ds0 = objHotelStoreProcedure.transfer_gettime("FETCH_TRANSFER_PACKAGE_TIME", int.Parse(tp_fromto_id.Text), chk.Text, "SIC");
                        binddropdownlist(drp_time, ds0);
                        Updatetransfervoucher.Update();
                    }
                    else if (ddl.Text == "PVT")
                    {
                        DataSet ds0 = objHotelStoreProcedure.transfer_gettime("FETCH_TRANSFER_PACKAGE_TIME", int.Parse(tp_fromto_id.Text), chk.Text, "PVT");
                        binddropdownlist(drp_time, ds0);
                        Updatetransfervoucher.Update();
                    }
                    else if (ddl.Text == "")
                    {
                        drp_time.Items.Clear();
                        Updatetransfervoucher.Update();
                    }

                }
            }
            #endregion
        }

        protected void drp_Agent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpAgent.Text != "")
            {
                DataSet ds = objhotelSeviceVoucher.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", drpAgent.Text);
                ViewState["cust_rel_no"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();

                //binddropdownlist(drpvoucher_type, ds);
                ViewState["cust_sr_no"] = ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                DropDownList3.Items.Clear();

                DataSet ds1 = objhotelSeviceVoucher.get_invoice_left("GET_INVOICE_NO", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                //    DataSet ds1 = objVouchersStoredProcedure.get_invoice_no_agent_vise("FETCH_INVOICE_NO_AGENT_VISE", int.Parse(ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString()));
                binddropdownlist(DropDownList3, ds1);


                string name = drpAgent.Text;
                DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();

                DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
                Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();
                //ViewState["row1_receipt_glcode"] = ds.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
            }
            else
            {

            }
            Updatetransfervoucher.Update();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);
            txtpaxname.Text = DSNAME.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
            Session["fromdate"] = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
            Session["todate"] = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
            Session["qid"] = DSNAME.Tables[0].Rows[0]["QUOTE_ID"].ToString();
            Updatetransfervoucher.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int serivcecartid;
            int packageid;
            string packageflag;
            string date;
            string time;
            string pack_name;
            int transferpackgeid;
            string instpid = "";
            int service_cart_id = 0;
            int userid;
            string fromtoid;
            if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()))
            {
                CRM.DataAccess.FIT.TransferServiceDetail objInsertTransferService = new CRM.DataAccess.FIT.TransferServiceDetail();
                // objInsertTransferService.InsertTransferService(0, txtpaxname.Text, drpAgent.SelectedValue, txtdate.Text, txtPackage.Text, drpPickTime.SelectedValue, drpsicpvt.SelectedValue, drpfromplace.SelectedValue, drpToPlace.SelectedValue, drparrivaldeparture.SelectedValue, txtVoucherNo.Text, int.Parse(Session["usersid"].ToString()));
                DataSet ds5 = ObjServiceVoucher.TransferVoucherEdit("FETCH_DATA_FOR_TRANSFER_SERVICE_VOUCEHR_EDIT", Request["IN"].ToString());
                int count = 0;
                foreach (GridViewRow item in GridView10.Rows)
                {
                    DropDownList drptime = (DropDownList)item.FindControl("tpdrp_time");
                    RadioButton rbtn = (RadioButton)item.FindControl("rbtnTPselect");
                    Label id = (Label)item.FindControl("lbltp_priceid");
                    DataSet ds = null;
                    if (rbtn.Checked)
                    {
                        
                        instpid = id.Text;
                        Label tpid = (Label)item.FindControl("lbltp_priceid");
                        transferpackgeid = int.Parse(tpid.Text);


                        TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                        TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                        Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                        DropDownList drp = (DropDownList)item.FindControl("drptransfer");
                        //DropDownList flag = (DropDownList)item.FindControl("lbltp_flag");
                        if (datebkk.Text == "" || datebkk.Text == "dd/MM/yyyy")
                        {
                            Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                        }
                        //else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                        //{
                        //    Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                        //}
                        else
                        {
                            serivcecartid = int.Parse(ds5.Tables[0].Rows[count]["TRANSFER_SERVICE_VOUCHER_ID"].ToString());

                            packageid = transferpackgeid;
                            packageflag = "TRANSFER";
                            time = drptime.Text;
                            date = datebkk.Text;
                            fromtoid = tp_fromtoid.Text;
                            userid = int.Parse(Session["uid"].ToString());
                            pack_name = "";

                            ds = objInsertTransferService.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, date, time, drp.Text, transferpackgeid, fromtoid, "", userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                            report(serivcecartid.ToString());
                        }
                    }
                    else if (instpid == id.Text)
                    {
                        Label tpid = (Label)item.FindControl("lbltp_priceid");
                        transferpackgeid = int.Parse(tpid.Text);
                        TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                        TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                        Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                        DropDownList drp = (DropDownList)item.FindControl("drptransfer");

                        serivcecartid = int.Parse(ds5.Tables[0].Rows[count]["TRANSFER_SERVICE_VOUCHER_ID"].ToString());
                        packageid = transferpackgeid;
                        packageflag = "TRANSFER";
                        time = drptime.Text;
                        date = datebkk.Text;
                        fromtoid = tp_fromtoid.Text;
                        userid = int.Parse(Session["uid"].ToString());
                        pack_name = "";

                        ds = objInsertTransferService.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, date, time, drp.Text, transferpackgeid, fromtoid, "", userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                        report(serivcecartid.ToString());
                    }

                    count++;
                }
                Master.DisplayMessage("Record Updated Successfully.", "successMessage", 3000);
                Updatetransfervoucher.Update();
            }
            else
            {

                CRM.DataAccess.FIT.TransferServiceDetail objInsertTransferService = new CRM.DataAccess.FIT.TransferServiceDetail();
                // objInsertTransferService.InsertTransferService(0, txtpaxname.Text, drpAgent.SelectedValue, txtdate.Text, txtPackage.Text, drpPickTime.SelectedValue, drpsicpvt.SelectedValue, drpfromplace.SelectedValue, drpToPlace.SelectedValue, drparrivaldeparture.SelectedValue, txtVoucherNo.Text, int.Parse(Session["usersid"].ToString()));

                foreach (GridViewRow item in GridView10.Rows)
                {
                    DropDownList drptime = (DropDownList)item.FindControl("tpdrp_time");
                    RadioButton rbtn = (RadioButton)item.FindControl("rbtnTPselect");
                    Label id = (Label)item.FindControl("lbltp_priceid");
                    DataSet ds = null;
                    if (rbtn.Checked)
                    {
                        instpid = id.Text;
                        Label tpid = (Label)item.FindControl("lbltp_priceid");
                        transferpackgeid = int.Parse(tpid.Text);


                        TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                        TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                        Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                        DropDownList drp = (DropDownList)item.FindControl("drptransfer");
                        //DropDownList flag = (DropDownList)item.FindControl("lbltp_flag");
                        if (datebkk.Text == "" || datebkk.Text == "dd/MM/yyyy")
                        {
                            Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                        }
                        //else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                        //{
                        //    Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                        //}
                        else
                        {
                            serivcecartid = service_cart_id;

                            packageid = transferpackgeid;
                            packageflag = "TRANSFER";
                            time = drptime.Text;
                            date = datebkk.Text;
                            fromtoid = tp_fromtoid.Text;
                            userid = int.Parse(Session["uid"].ToString());
                            pack_name = "";

                            ds = objInsertTransferService.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, date, time, drp.Text, transferpackgeid, fromtoid, "", userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                            report(ds.Tables[0].Rows[0]["ID"].ToString());
                        }
                    }
                    else if (instpid == id.Text)
                    {
                        Label tpid = (Label)item.FindControl("lbltp_priceid");
                        transferpackgeid = int.Parse(tpid.Text);
                        TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                        TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                        Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                        DropDownList drp = (DropDownList)item.FindControl("drptransfer");
                        if (datebkk.Text == "" || datebkk.Text == "dd/MM/yyyy")
                        {
                            Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                        }
                        else
                        {
                            serivcecartid = service_cart_id;

                            packageid = transferpackgeid;
                            packageflag = "TRANSFER";
                            time = drptime.Text;
                            date = datebkk.Text;
                            fromtoid = tp_fromtoid.Text;
                            userid = int.Parse(Session["uid"].ToString());
                            pack_name = "";

                            ds = objInsertTransferService.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, date, time, drp.Text, transferpackgeid, fromtoid, "", userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                            report(ds.Tables[0].Rows[0]["ID"].ToString());
                        }
                    }
                }





                /**********************************Transfer Package Vouchers *************************************/
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
                        result = "0" + w[0] + "/" + "0" + w[1] + "/" + t1[0];
                        //  lbl_voucher_date.Text = result;
                    }
                    else
                    {
                        result = w[0] + "/" + "0" + w[1] + "/" + t1[0];
                        //  lbl_voucher_date.Text = result;
                    }
                }
                else
                {
                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                    {
                        result = "0" + w[0] + "/" + w[1] + "/" + t1[0];
                        //  lbl_voucher_date.Text = result;
                    }
                    else
                    {
                        result = w[0] + "/" + w[1] + "/" + t1[0];
                        // lbl_voucher_date.Text = result;
                    }
                }



                DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
                DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
                DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);

                DataSet ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");

                DataSet ds_tp_supplier = objFITPaymentStoreProcedure.fetch_common_data("FETCH_TP_SUPPLIER_NO_MANUAL", DropDownList3.Text);
                if (ds_tp_supplier.Tables[0].Rows.Count != 0)
                {
                    DataSet ds_tp_data = objFITPaymentStoreProcedure.fetch_transfer_package("FETCH_TRANSFER_RATE_FROM_SERVICE_CART_MANUAL", DropDownList3.Text, ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString());
                    DataSet dsgl_tp = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_tp_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");

                    string totalamount = "0";
                    string final_total = "0";


                    DataSet dspurchasetp = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", final_total, "", final_total, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    for (int itp = 0; itp < ds_tp_data.Tables[0].Rows.Count; itp++)
                    {

                        string txt = "0";

                        objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_tp_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", txt, "", txt, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[3]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", int.Parse(ds_tp_data.Tables[0].Rows[itp]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString()), ds_tp_data.Tables[0].Rows[itp]["SIC_PVT_FLAG"].ToString(), "", result, "", txt, int.Parse(Session["CompanyId"].ToString()), 2);
                    }
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_tp.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", final_total, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_tp.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, final_total, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[5]["AutoSearchResult"].ToString(), dspurchasetp.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", final_total, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                }

                insert_update_ss_tp_tp();

                Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);
                Updatetransfervoucher.Update();
                Clear();
            }
        }
        protected void Clear()
        {
            txtpaxname.Text = "";
            drpAgent.SelectedValue = "0";
            drpsupplier.SelectedValue = "0";
            DropDownList3.SelectedValue = "0";
        }

        protected void filltransferpackage(string SUPPLIER)
        {
            DataTable dttp = objTransferService.fetchTransferPackage(SUPPLIER);
            //dlhoteldetails.DataSource = dttp;

            //dlhoteldetails.DataBind();
            GridView10.DataSource = dttp;
            GridView10.DataBind();

            foreach (GridViewRow item in GridView10.Rows)
            {

                Label chk = (Label)item.FindControl("lbltp_flag");
                Label tp_fromto_id = (Label)item.FindControl("lbltp_detialid");
                DropDownList drp_list = (DropDownList)item.FindControl("tpdrp_time");
                if (chk.Text == "A")
                {
                    TextBox date = (TextBox)item.FindControl("txtTPdate");
                    date.Text = Session["fromdate"].ToString();
                    date.Enabled = false;
                }
                if (chk.Text == "D")
                {
                    TextBox date = (TextBox)item.FindControl("txtTPdate");
                    date.Text = Session["todate"].ToString();
                    date.Enabled = false;
                }
                DataSet ds0 = objTransferService.transfer_gettime("FETCH_TRANSFER_PACKAGE_TIME", int.Parse(tp_fromto_id.Text), chk.Text, "SIC");
                binddropdownlist(drp_list, ds0);
            }
        }

        public void CheckChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GridView10.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbtnTPselect");
                if (rb != sender)
                {
                    rb.Checked = false;
                }
            }
            Updatetransfervoucher.Update();
        }

        #region transferPackage sachin

        protected void FillTransferPackageDetails(string quoteid)
        {
            int i = 0;
            foreach (GridViewRow item in GridView10.Rows)
            {
                DataTable dttra = objHotelStoreProcedure.fetchTransferPackageforedit("FETCH_FOR_TRANSFERPACKAGE_EDIT_NEW", quoteid);
                if (dttra.Rows.Count != 0)
                {
                    Label priceid = (Label)item.FindControl("lbltp_priceid");
                    // int i = item.DataItemIndex;
                    DropDownList drpsic = (DropDownList)item.FindControl("drptransfer");
                    TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                    DropDownList drptime = (DropDownList)item.FindControl("tpdrp_time");
                    Label tp_fromto_id = (Label)item.FindControl("lbltp_detialid");

                    if (priceid.Text == dttra.Rows[0]["TRANSFER_PACKAGE_PRICE_ID"].ToString())
                    {
                        RadioButton rbtn = (RadioButton)item.FindControl("rbtnTPselect");
                        rbtn.Checked = true;
                        drpsic.SelectedValue = dttra.Rows[i]["SIC_PVT_FLAG"].ToString();
                        datebkk.Text = dttra.Rows[i]["START_DATE"].ToString();
                        //drptime.SelectedValue = dttra.Rows[i]["START_TIME"].ToString();
                        if (drpsic.Text == "SIC")
                        {
                            DropDownList drp_time = (DropDownList)item.FindControl("tpdrp_time");
                            DataSet ds0 = objHotelStoreProcedure.transfer_gettime("FETCH_TRANSFER_PACKAGE_TIME", int.Parse(tp_fromto_id.Text), dttra.Rows[i]["FLAG"].ToString(), "SIC");
                            binddropdownlist(drptime, ds0);
                          
                        }
                        else if (drpsic.Text == "PVT")
                        {
                            DropDownList drp_time = (DropDownList)item.FindControl("tpdrp_time");
                            DataSet ds0 = objHotelStoreProcedure.transfer_gettime("FETCH_TRANSFER_PACKAGE_TIME", int.Parse(tp_fromto_id.Text), dttra.Rows[i]["FLAG"].ToString(), "PVT");
                            binddropdownlist(drptime, ds0);
                           
                        }
                        else
                        {
                            DropDownList drp_time = (DropDownList)item.FindControl("tpdrp_time");
                            drp_time.Items.Clear();
                            
                        }
                        Updatetransfervoucher.Update();
                        i = i + 1;
                    }
                }
            }
        }

        #endregion

        protected void report(string ID1)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/TransferManualvoucher/" + ID1 + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/TransferManualvoucher/" + ID1 + "/"));

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
            parm[0] = new ReportParameter("TRANSFER_SERVICE_VOUCHER_ID", ID1);
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/TransferVoucherManual";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            rptViewer1.Visible = false;
            Response.Clear();
            using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/TransferManualvoucher/" + ID1 + "/TransferVoucherManual.pdf")))
            {
                fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
            }

            lnkbtn.HRef = "~/Views/FIT/TransferManualvoucher/" + ID1 + "/TransferVoucherManual.pdf";
            lnkbtn.Attributes.Add("style", "display");
        }


        public void insert_update_ss_tp_tp()
        {

            int serivcecartid;
            int packageid;
            string packageflag;
            string date;
            string time;
            string pack_name;
            int transferpackgeid;
            string instpid = "";
            int service_cart_id = 0;
            int userid;
            string fromtoid;
            string orderstatus;


            foreach (GridViewRow item in GridView10.Rows)
            {
                DropDownList drptime = (DropDownList)item.FindControl("tpdrp_time");
                RadioButton rbtn = (RadioButton)item.FindControl("rbtnTPselect");
                Label id = (Label)item.FindControl("lbltp_priceid");

                if (rbtn.Checked)
                {
                    instpid = id.Text;
                    Label tpid = (Label)item.FindControl("lbltp_priceid");
                    transferpackgeid = int.Parse(tpid.Text);


                    TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                    TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                    Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                    DropDownList drp = (DropDownList)item.FindControl("drptransfer");

                    if (datebkk.Text == "" || datebkk.Text == "dd/MM/yyyy")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                    }
                    else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                    }
                    else
                    {
                        serivcecartid = service_cart_id;

                        packageid = transferpackgeid;
                        packageflag = "TRANSFER";
                        time = drptime.Text;
                        date = datebkk.Text;
                        orderstatus = "To Be Reconfirmed";
                        userid = int.Parse(Session["uid"].ToString());
                        pack_name = "";


                        objhotelSeviceVoucher.insert_update_sight_seeing(serivcecartid, packageid, packageflag, date, time, orderstatus, userid, "0", pack_name, "", int.Parse(tp_fromtoid.Text), drp.Text, true, int.Parse(Session["qid"].ToString()));
                    }
                }
                else if (instpid == id.Text)
                {
                    Label tpid = (Label)item.FindControl("lbltp_priceid");
                    transferpackgeid = int.Parse(tpid.Text);
                    TextBox datebkk = (TextBox)item.FindControl("txtTPdate");
                    TextBox timebkk = (TextBox)item.FindControl("txtTPtime");
                    Label tp_fromtoid = (Label)item.FindControl("lbltp_detialid");
                    DropDownList drp = (DropDownList)item.FindControl("drptransfer");
                    if (datebkk.Text == "" || datebkk.Text == "dd/MM/yyyy")
                    {
                    }
                    else
                    {
                        serivcecartid = service_cart_id;

                        packageid = transferpackgeid;
                        packageflag = "TRANSFER";
                        time = drptime.Text;
                        date = datebkk.Text;
                        orderstatus = "To Be Reconfirmed";
                        userid = int.Parse(Session["uid"].ToString());
                        pack_name = "";

                        objhotelSeviceVoucher.insert_update_sight_seeing(serivcecartid, packageid, packageflag, date, time, orderstatus, userid, "0", pack_name, "", int.Parse(tp_fromtoid.Text), drp.Text, true, int.Parse(Session["qid"].ToString()));
                    }
                }
            }

        }
    }
}
    