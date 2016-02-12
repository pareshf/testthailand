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
using CRM.DataAccess.SecurityDAL;
using CRM.DataAccess.Account;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;

namespace CRM.WebApp.Views.Account
{
    public partial class SightSeeingVoucher : System.Web.UI.Page
    {
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();

        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.FIT.ServiceVoucherDetail objservicevoucher = new CRM.DataAccess.FIT.ServiceVoucherDetail();
        CRM.DataAccess.FIT.HotelServiceVoucher objhotelSeviceVoucher = new CRM.DataAccess.FIT.HotelServiceVoucher();
        GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        CRM.DataAccess.Account.SearchHotelServiceVoucher ObjServiceVoucher = new CRM.DataAccess.Account.SearchHotelServiceVoucher();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 216);
            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                // Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()) && Request["CT"] != null && !string.IsNullOrEmpty(Request["CT"].ToString()))
                {
                    string ino = Request["IN"].ToString();
                    string city = Request["CT"].ToString();
                    DataSet ds3 = ObjServiceVoucher.SightSeeingVoucherEdit("FETCH_DATA_FOR_SIGHT_SEEING_SERVICE_VOUCHER_EDIT", ino, city);
                    //txtdate.Text = ds3.Tables[0].Rows[0]["DATE"].ToString();
                    txtpaxname.Text = ds3.Tables[0].Rows[0]["CLINT_NAME"].ToString();
                    // txtVoucherNo.Text = ds3.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    drpAgent.Text = ds3.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    drpCity.Text = ds3.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    FillAllDetaiils(ds3.Tables[0].Rows[0]["CITY_NAME"].ToString(), ds3.Tables[0].Rows[0]["INVOICE_NO"].ToString());
                    DataSet ds0 = objservicevoucher.GetCityWiseSightSeeing("CITY_WISE_SIGHT_SEEING", ds3.Tables[0].Rows[0]["CITY_NAME"].ToString());

                    //agent fill invoice
                    DataSet ds = objhotelSeviceVoucher.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", ds3.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString());
                    ViewState["cust_rel_no"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();

                    //binddropdownlist(drpvoucher_type, ds);
                    ViewState["cust_sr_no"] = ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    DropDownList3.Items.Clear();

                    DataSet ds12 = objhotelSeviceVoucher.get_invoice_left("GET_INVOICE_NO", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                    //    DataSet ds1 = objVouchersStoredProcedure.get_invoice_no_agent_vise("FETCH_INVOICE_NO_AGENT_VISE", int.Parse(ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString()));
                    binddropdownlist(DropDownList3, ds12);
                    DropDownList3.Text = ds3.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);
                    Session["fromdate"] = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
                    Session["todate"] = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
                    Session["qid"] = DSNAME.Tables[0].Rows[0]["QUOTE_ID"].ToString();
                    Session["uid"] = ds.Tables[0].Rows[0]["USER_ID"].ToString();

                    lnkbtn.HRef = "~/Views/FIT/SightSeeingManualvoucher/" + ds3.Tables[0].Rows[0]["SERVICE_VOUCHER_ID"].ToString() + "/SightSeeingVoucherManual.pdf";
                    lnkbtn.Attributes.Add("style", "display");
                    foreach (GridViewRow item in GV_Result.Rows)
                    {

                        Label meal = (Label)item.FindControl("lblSS_PtypackageDetails");
                        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                        {
                            if (meal.Text == ds3.Tables[0].Rows[i]["SIGHT_SEEING_PACKAGE_NAME"].ToString())
                            {
                                CheckBox chk = (CheckBox)item.FindControl("cb_pty_select");
                                chk.Checked = true;

                                TextBox txtdate = (TextBox)item.FindControl("txtSS_Ptydate");
                                txtdate.Text = ds3.Tables[0].Rows[i]["DATE"].ToString();
                                DropDownList drptime = (DropDownList)item.FindControl("drp_ss_time");

                                DataSet ds1 = objHotelStoreProcedure.getdate(meal.Text, txtdate.Text, ds3.Tables[0].Rows[i]["CITY_NAME"].ToString());
                                for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                                {
                                    if (ds1.Tables[1].Rows[j]["AutoSearchResult"] != "" || ds1.Tables[1].Rows[j]["AutoSearchResult"] != null)
                                    {
                                        drptime.Items.Add(ds1.Tables[1].Rows[j]["AutoSearchResult"].ToString());
                                    }
                                }

                                drptime.Text = ds3.Tables[0].Rows[i]["TIME"].ToString();
                                DropDownList drppty = (DropDownList)item.FindControl("DropDownList2");
                                drppty.SelectedValue = ds3.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString();
                                TextBox txtmeal = (TextBox)item.FindControl("txtSS_PtynoofMeals");
                            }
                            //txtmeal.Text = ds3.Tables[0].Rows[i]["NO_OF_MEALS"].ToString();
                        }
                    }
                }

                DataSet ds10 = objservicevoucher.GetAgent("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(drpAgent, ds10);

                DataSet ds2 = objservicevoucher.GetCity("FETCH_CITY_NAME_FOR_SERVICE");
                binddropdownlist(drpCity, ds2);


            }
        }

        protected void FillAllDetaiils(String CITY, String NO)
        {
            DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", NO);
            Session["fromdate"] = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
            Session["todate"] = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
            DataTable dtpty = objservicevoucher.fetchsightseeing(Session["fromdate"].ToString(), Session["todate"].ToString(), CITY);
            if (dtpty.Rows.Count != 0)
            {
                GV_Result.DataSource = dtpty;
                GV_Result.DataBind();
                foreach (GridViewRow item in GV_Result.Rows)
                {


                    int i = item.DataItemIndex;
                    DropDownList drp = (DropDownList)item.FindControl("DropDownList2");

                    Label meal = (Label)item.FindControl("lblpty_meal");
                    if (meal.Text == "0" || meal.Text == "" || meal.Text == "False")
                    {
                        TextBox txtmeal = (TextBox)item.FindControl("txtSS_PtynoofMeals");
                        txtmeal.Visible = false;
                    }
                    if (dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != "" && dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != null && dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != "0.00")
                    {

                    }
                    else
                    {
                        drp.Items.Remove("SIC");
                    }

                    if (dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != "" && dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != null && dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != "0.00")
                    {

                    }
                    else
                    {
                        drp.Items.Remove("PVT");
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
            r.Items.Insert(0, new ListItem("", "0"));
            //r.SelectedValue = "0";
        }

        protected void drp_City_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds0 = objservicevoucher.GetCityWiseSightSeeing("CITY_WISE_SIGHT_SEEING", drpCity.Text);
            //if (ds0.Tables[0].Rows.Count == 0)
            //{

            //}
            //else
            //{
            //    binddropdownlist(drpsight, ds0);

            //}
            DataTable dtpty = objservicevoucher.fetchsightseeing(Session["fromdate"].ToString(), Session["todate"].ToString(), drpCity.Text);
            if (dtpty.Rows.Count != 0)
            {
                GV_Result.DataSource = dtpty;
                GV_Result.DataBind();
                foreach (GridViewRow item in GV_Result.Rows)
                {


                    int i = item.DataItemIndex;
                    DropDownList drp = (DropDownList)item.FindControl("DropDownList2");

                    Label meal = (Label)item.FindControl("lblpty_meal");
                    if (meal.Text == "0" || meal.Text == "" || meal.Text == "False")
                    {
                        TextBox txtmeal = (TextBox)item.FindControl("txtSS_PtynoofMeals");
                        txtmeal.Visible = false;
                    }
                    if (dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != "" && dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != null && dtpty.Rows[i]["ADULT_SIC_RATE"].ToString() != "0.00")
                    {

                    }
                    else
                    {
                        drp.Items.Remove("SIC");
                    }

                    if (dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != "" && dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != null && dtpty.Rows[i]["ADULT_PVT_RATE"].ToString() != "0.00")
                    {

                    }
                    else
                    {
                        drp.Items.Remove("PVT");
                    }
                }
            }
            else
            {
                Master.DisplayMessage("No Record For This Perticular City.", "successMessage", 8000);
            }
            Updateservicevoucher.Update();
        }

        protected void drp_SicPvt_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            Updateservicevoucher.Update();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);
            txtpaxname.Text = DSNAME.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
            Session["fromdate"] = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
            Session["todate"] = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
            Session["qid"] = DSNAME.Tables[0].Rows[0]["QUOTE_ID"].ToString();
            Updateservicevoucher.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CRM.DataAccess.FIT.ServiceVoucherDetail objinsertsightseeing = new CRM.DataAccess.FIT.ServiceVoucherDetail();
            //variable
            int serivcecartid;
            int packageid;
            string packageflag;
            string date;
            string time;
            string pack_name;

            // save for sight seeing
            if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()) && Request["CT"] != null && !string.IsNullOrEmpty(Request["CT"].ToString()))
            {
                string ino = Request["IN"].ToString();
                    string city = Request["CT"].ToString();

                    foreach (GridViewRow row1 in GV_Result.Rows)
                    {
                        CheckBox rbtn = (CheckBox)row1.FindControl("cb_pty_select");
                        if (rbtn.Checked)
                        {

                            Label packagename = (Label)row1.FindControl("lblSS_PtypackageDetails");
                            DataSet ds3 = ObjServiceVoucher.SightSeeingVoucherEdit("FETCH_DATA_FOR_SIGHT_SEEING_SERVICE_VOUCHER_EDIT", ino, city);

                            for (int j = 0; j < ds3.Tables[0].Rows.Count; j++)
                            {
                                if (ds3.Tables[0].Rows[j]["SIGHT_SEEING_PACKAGE_NAME"].ToString() == packagename.Text)
                                {
                                    TextBox datebkk = (TextBox)row1.FindControl("txtSS_Ptydate");
                                    DropDownList timebkk = (DropDownList)row1.FindControl("drp_ss_time");
                                    TextBox txtmeal = (TextBox)row1.FindControl("txtSS_PtynoofMeals");
                                    DropDownList sicpvt_flag = (DropDownList)row1.FindControl("DropDownList2");

                                    if (datebkk.Text == "")
                                    {
                                        Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                                    }
                                    //else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                                    //{
                                    //    Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                                    //}
                                    else
                                    {
                                        serivcecartid = int.Parse(ds3.Tables[0].Rows[j]["SERVICE_VOUCHER_ID"].ToString());

                                        packageid = 0;
                                        packageflag = "SIGHT";
                                        time = timebkk.Text;
                                        date = datebkk.Text;
                                        int userid = int.Parse(Session["uid"].ToString());
                                        pack_name = packagename.Text;
                                        string txtperiod_stay_from = "";
                                        for (int i = 0; i < int.Parse(txtmeal.Text); i++)
                                        {
                                            DateTime dat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                                            dat = dat.AddDays(i);

                                            dat.ToString("MM/dd/yyyy");
                                            string source1 = dat.ToString();
                                            string str11 = source1;
                                            string[] w1 = str11.Split('/');

                                            string t12 = w1[2];
                                            string[] t11 = t12.Split(' ');

                                            if (w1[1] == "1" || w1[1] == "2" || w1[1] == "3" || w1[1] == "4" || w1[1] == "5" || w1[1] == "6" || w1[1] == "7" || w1[1] == "8" || w1[1] == "9")
                                            {
                                                if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                                {
                                                    txtperiod_stay_from = "0" + w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                                }
                                                else
                                                {
                                                    txtperiod_stay_from = w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                                }
                                            }
                                            else
                                            {
                                                if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                                {
                                                    txtperiod_stay_from =  "0" + w1[0] + "/"+ w1[1] + "/" + t11[0];
                                                }
                                                else
                                                {
                                                    txtperiod_stay_from = w1[0] + "/" + w1[1] + "/" + t11[0];
                                                }
                                            }
                                        }

                                        //objHotelStoreProcedure.insert_update_sight_seeing(serivcecartid, packageid, packageflag, txtperiod_stay_from, time, orderstatus, userid, txtmeal.Text, pack_name, "Pattaya", 0, sicpvt_flag.Text, s_flag);
                                        DataSet DS = objinsertsightseeing.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, txtperiod_stay_from, time, sicpvt_flag.Text, pack_name, drpCity.SelectedValue, txtVoucherNo.Text, userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                                        String ID = serivcecartid.ToString();

                                        // create report
                                        if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/")))
                                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/"));

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
                                        parm[0] = new ReportParameter("SERVICE_VOUCHER_ID", ID);
                                        rptViewer1.ShowCredentialPrompts = false;
                                        rptViewer1.ShowParameterPrompts = false;

                                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/SightSeeingVoucherManual";
                                        rptViewer1.ServerReport.SetParameters(parm);
                                        rptViewer1.ServerReport.Refresh();

                                        renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                                        rptViewer1.Visible = false;
                                        Response.Clear();
                                        using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/SightSeeingVoucherManual.pdf")))
                                        {
                                            fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                                        }

                                        lnkbtn.HRef = "~/Views/FIT/SightSeeingManualvoucher/" + ID + "/SightSeeingVoucherManual.pdf";
                                        lnkbtn.Attributes.Add("style", "display");
                                    }
                                }

                            }
                           
                        }
                    }
                    Master.DisplayMessage("Record Update Successfully.", "successMessage", 3000);
                    Updateservicevoucher.Update();
                    Clear();
            }
            else
            {
                foreach (GridViewRow row1 in GV_Result.Rows)
                {
                    CheckBox rbtn = (CheckBox)row1.FindControl("cb_pty_select");
                    if (rbtn.Checked)
                    {

                        Label packagename = (Label)row1.FindControl("lblSS_PtypackageDetails");

                        TextBox datebkk = (TextBox)row1.FindControl("txtSS_Ptydate");
                        DropDownList timebkk = (DropDownList)row1.FindControl("drp_ss_time");
                        TextBox txtmeal = (TextBox)row1.FindControl("txtSS_PtynoofMeals");
                        DropDownList sicpvt_flag = (DropDownList)row1.FindControl("DropDownList2");

                        if (datebkk.Text == "")
                        {
                            Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                        }
                        //else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                        //{
                        //    Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                        //}
                        else
                        {
                            serivcecartid = 0;

                            packageid = 0;
                            packageflag = "SIGHT";
                            time = timebkk.Text;
                            date = datebkk.Text;
                            int userid = int.Parse(Session["uid"].ToString());
                            pack_name = packagename.Text;

                            for (int i = 0; i < int.Parse(txtmeal.Text); i++)
                            {
                                DateTime dat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                                dat = dat.AddDays(i);

                                dat.ToString("MM-dd-yyyy");
                                string source1 = dat.ToString();
                                string str11 = source1;
                                string[] w1 = str11.Split('/');

                                string t12 = w1[2];
                                string[] t11 = t12.Split(' ');
                                string txtperiod_stay_from = "";
                                if (w1[1] == "1" || w1[1] == "2" || w1[1] == "3" || w1[1] == "4" || w1[1] == "5" || w1[1] == "6" || w1[1] == "7" || w1[1] == "8" || w1[1] == "9")
                                {
                                    if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                    {
                                        txtperiod_stay_from = "0" + w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                    }
                                    else
                                    {
                                        txtperiod_stay_from = w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                    }
                                }
                                else
                                {
                                    if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                    {
                                        txtperiod_stay_from = "0" + w1[0] + "/" + w1[1] + "/" + t11[0];
                                    }
                                    else
                                    {
                                        txtperiod_stay_from = w1[0] + "/" + w1[1] + "/" + t11[0];
                                    }
                                }
                                
                                //objHotelStoreProcedure.insert_update_sight_seeing(serivcecartid, packageid, packageflag, txtperiod_stay_from, time, orderstatus, userid, txtmeal.Text, pack_name, "Pattaya", 0, sicpvt_flag.Text, s_flag);
                                DataSet DS = objinsertsightseeing.InsertSightSeeingVoucher(serivcecartid, txtpaxname.Text, drpAgent.SelectedValue, txtperiod_stay_from, time, sicpvt_flag.Text, pack_name, drpCity.SelectedValue, txtVoucherNo.Text, userid, DropDownList3.Text, int.Parse(Session["qid"].ToString()));
                                String ID = DS.Tables[0].Rows[0]["ID"].ToString();

                                // create report
                                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/")))
                                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/"));

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
                                parm[0] = new ReportParameter("SERVICE_VOUCHER_ID", ID);
                                rptViewer1.ShowCredentialPrompts = false;
                                rptViewer1.ShowParameterPrompts = false;

                                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                                rptViewer1.ServerReport.ReportPath = "/ThailandReport/SightSeeingVoucherManual";
                                rptViewer1.ServerReport.SetParameters(parm);
                                rptViewer1.ServerReport.Refresh();

                                renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                                rptViewer1.Visible = false;
                                Response.Clear();
                                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/SightSeeingManualvoucher/" + ID + "/SightSeeingVoucherManual.pdf")))
                                {
                                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                                }

                                lnkbtn.HRef = "~/Views/FIT/SightSeeingManualvoucher/" + ID + "/SightSeeingVoucherManual.pdf";
                                lnkbtn.Attributes.Add("style", "display");
                            }
                        }
                    }
                }



                /**********************************sight seeing *************************************/





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

                DataSet ds_ss_supplier = objservicevoucher.fetch_common_data("FETCH_SS_SUPPLIER_NO_MANUAL_VOUCHER", DropDownList3.Text, drpCity.Text);

                if (ds_ss_supplier.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < ds_ss_supplier.Tables[0].Rows.Count; j++)
                    {

                        DataSet ds_ss_data = objservicevoucher.fetch_transfer_package("FETCH_SIGHT_SEEING_RATE_FROM_SERVICE_CART_MANUAL", DropDownList3.Text, ds_ss_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), drpCity.Text);
                        DataSet dsgl_SS = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", ds_ss_data.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");



                        string txt = "0";
                        string sic_adult_rate;
                        string sic_child_rate;
                        string pvt_adult_rate;
                        string pvt_child_rate;
                        string txt_amount1 = "0";
                        for (int i = 0; i < ds_ss_data.Tables[0].Rows.Count; i++)
                        {


                        }
                        DataSet dspurchasess = objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_ss_supplier.Tables[0].Rows[j]["CHAIN_NAME"].ToString(), DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", txt_amount1, "", txt_amount1, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);
                        txt_amount1 = "0";
                        for (int i = 0; i < ds_ss_data.Tables[0].Rows.Count; i++)
                        {


                            objFITPaymentStoreProcedure.insert_purchase_entry(0, ds_ss_supplier.Tables[0].Rows[0]["CHAIN_NAME"].ToString(), DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", txt, "", txt, ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), "", "", int.Parse("0"), int.Parse("0"), int.Parse("0"), int.Parse("0"), "", 0, "", ds_ss_data.Tables[0].Rows[i]["TRANSFER_SIGHT_SEEING_PACKAGE_FLAG"].ToString(), ds_ss_data.Tables[0].Rows[i]["DATE"].ToString(), ds_ss_data.Tables[0].Rows[i]["SIC_PVT_FLAG"].ToString(), "0", int.Parse(Session["CompanyId"].ToString()), 2);
                        }

                        objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount1, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_SS.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txt_amount1, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[3]["AutoSearchResult"].ToString(), dspurchasess.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), result, ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txt_amount1, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);


                        insert_update_ss_tp_bkk();
                    }
                }
                Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);
                Updateservicevoucher.Update();
                Clear();
            }
        }

        protected void Clear()
        {
            //txtdate.Text = "";
            txtpaxname.Text = "";
            txtVoucherNo.Text = "";
            drpAgent.SelectedValue = "0";
            drpCity.SelectedValue = "0";
            //drpPickTime.SelectedValue = "0";
            //drpsight.SelectedValue = "0";
        }

        protected void txtSS_Ptydate_TextChanged(object sender, EventArgs e)
        {
            //    TextBox ddl1 = sender as TextBox;

            TextBox tx = (TextBox)sender;

            GridViewRow row = (GridViewRow)tx.NamingContainer;

            int indx = row.DataItemIndex;
            foreach (GridViewRow item in GV_Result.Rows)
            {
                if (indx == item.DataItemIndex)
                {

                    TextBox date = (TextBox)item.FindControl("txtSS_Ptydate");


                    if (date.Text != "dd/MM/yyyy" && date.Text != "")
                    {
                        ////DateTime Result;
                        //DateTimeFormatInfo info = new DateTimeFormatInfo();
                        //CultureInfo culture;
                        //culture = CultureInfo.CreateSpecificCulture("en-US");
                        //info.ShortDatePattern = "dd/MM/yyyy";

                        Label packagename = (Label)item.FindControl("lblSS_PtypackageDetails");

                        try
                        {
                            System.DateTime today = DateTime.ParseExact(date.Text, "dd/MM/yyyy", null);

                            DataSet ds = objHotelStoreProcedure.getdate(packagename.Text, date.Text, drpCity.SelectedValue);
                            if (ds.Tables[0].Rows[0]["ANSWER"].ToString() == "0")
                            {
                                Master.DisplayMessage(packagename.Text + " " + "Sight see is not operated on this day", "successMessage", 8000);
                            }

                            //else if (txtpty_CheckIn.Text != "" && txtpty_CheckOut.Text != "")
                            //{
                            //    if ((DateTime.ParseExact(date.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtpty_CheckIn.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(date.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtpty_CheckOut.Text, "dd/MM/yyyy", null)))
                            //    {
                            //        Master.DisplayMessage("Date must be with in check-in & check-out dates of Pattya", "successMessage", 8000);
                            //        date.Text = "";
                            //    }
                            //    else
                            //    {

                            DropDownList drptime = (DropDownList)item.FindControl("drp_ss_time");
                            drptime.Items.Clear();
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                if (ds.Tables[1].Rows[i]["AutoSearchResult"] != "" || ds.Tables[1].Rows[i]["AutoSearchResult"] != null)
                                {
                                    drptime.Items.Add(ds.Tables[1].Rows[i]["AutoSearchResult"].ToString());
                                }
                            }
                            //}
                            //upSSPty.Update();
                            //}
                            Updateservicevoucher.Update();
                            break;
                        }
                        catch
                        {
                            Master.DisplayMessage("Date is not in correct format for" + " " + packagename.Text, "successMessage", 8000);
                            date.Text = "";
                            Updateservicevoucher.Update();
                            break;
                        }
                        finally
                        {

                        }
                    }
                }
            }
        }

        public void insert_update_ss_tp_bkk()
        {


            int serivcecartid;
            int packageid;
            string packageflag;
            string date;
            string time;
            string pack_name;
            string orderstatus;
            int userid;

            foreach (GridViewRow row1 in GV_Result.Rows)
            {
                CheckBox rbtn = (CheckBox)row1.FindControl("cb_pty_select");
                if (rbtn.Checked)
                {

                    Label packagename = (Label)row1.FindControl("lblSS_PtypackageDetails");

                    TextBox datebkk = (TextBox)row1.FindControl("txtSS_Ptydate");
                    DropDownList timebkk = (DropDownList)row1.FindControl("drp_ss_time");
                    TextBox txtmeal = (TextBox)row1.FindControl("txtSS_PtynoofMeals");
                    DropDownList sicpvt_flag = (DropDownList)row1.FindControl("DropDownList2");

                    if (datebkk.Text == "")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 8000);
                    }
                    else if ((DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(datebkk.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 8000);
                    }
                    else
                    {

                        serivcecartid = 0;

                        packageid = 0;
                        packageflag = "SIGHT";
                        time = timebkk.Text;
                        date = datebkk.Text;
                        orderstatus = "To Be Reconfirmed";
                        userid = int.Parse(Session["uid"].ToString());
                        pack_name = packagename.Text;

                        for (int i = 0; i < int.Parse(txtmeal.Text); i++)
                        {
                            DateTime dat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            dat = dat.AddDays(i);

                            dat.ToString("MM-dd-yyyy");
                            string source1 = dat.ToString();
                            string str11 = source1;
                            string[] w1 = str11.Split('/');

                            string t12 = w1[2];
                            string[] t11 = t12.Split(' ');
                            string txtperiod_stay_from = "";
                            if (w1[1] == "1" || w1[1] == "2" || w1[1] == "3" || w1[1] == "4" || w1[1] == "5" || w1[1] == "6" || w1[1] == "7" || w1[1] == "8" || w1[1] == "9")
                            {
                                if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                {
                                    txtperiod_stay_from = "0" + w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                }
                                else
                                {
                                    txtperiod_stay_from = w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                                }
                            }
                            else
                            {
                                if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                                {
                                    txtperiod_stay_from = "0" + w1[0] + "/" + w1[1] + "/" + t11[0];
                                }
                                else
                                {
                                    txtperiod_stay_from = w1[0] + "/" + w1[1] + "/" + t11[0];
                                }
                            }
                            objhotelSeviceVoucher.insert_update_sight_seeing(serivcecartid, packageid, packageflag, txtperiod_stay_from, time, orderstatus, userid, txtmeal.Text, pack_name, drpCity.Text, 0, sicpvt_flag.Text, true, int.Parse(Session["qid"].ToString()));
                        }
                    }
                }
            }
        }
    }
}