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
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace CRM.WebApp.Views.FIT
{
    public partial class AdminBookingFit : System.Web.UI.Page
    {
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        CRM.DataAccess.Account.GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        
        #region VARIABLES

        public int ipr = 0;

        string id;
        public string tourid;
        public string quoteid;

        bool flag_room = true;
        bool flag_flight = true;
        bool flag_time = true;
        bool fly_land = true;
        bool land_flag = true;
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 242);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    trpac.Attributes.Add("style", "display");
                    btnback.Visible = true;
                    tourid = Request.QueryString["TOURID"].ToString();
                    quoteid = Request.QueryString["QUOTEID"].ToString();
                    Session["tourid"] = tourid;
                    Session["quoteid"] = quoteid;
                    DataSet ds = objfitquote.FetchForFitBooking("FETCH_DATA_FOR_BOOKING_FIT_USING_TOURID", tourid);
                    DataTable dt1 = ds.Tables[0];
                    txtNo_CWB.Text = dt1.Rows[0]["NO_OF_CWB"].ToString();
                    txtNo_CNB.Text = dt1.Rows[0]["NO_OF_CNB"].ToString();
                    txtNo_Adult.Text = dt1.Rows[0]["NO_OF_ADULTS"].ToString();
                    txtNo_OfNights.Text = dt1.Rows[0]["NO_OF_NIGHTS"].ToString();
                    txtNo_Infant.Text = dt1.Rows[0]["NO_OF_INFANT"].ToString();
                    txtClientname.Text = dt1.Rows[0]["CLIENT_NAME"].ToString();
                    txtDeparture_Flight.Text = dt1.Rows[0]["DEPARTURE_FLIGHT"].ToString();
                    txtArrival_Flight.Text = dt1.Rows[0]["ARRIVAL_FLIGHT"].ToString();
                    txtTourname.Text = dt1.Rows[0]["TOUR_SHORT_NAME"].ToString();
                    txtFrom_Date.Text = dt1.Rows[0]["TOUR_FROM_DATE"].ToString();
                    txtTo_Date.Text = dt1.Rows[0]["TOUR_TO_DATE"].ToString();
                    txtRemarks.Text = dt1.Rows[0]["REMARKS"].ToString();
                    DropDownList2.SelectedValue = dt1.Rows[0]["CUST_COMPANY_NAME"].ToString();

                    txtlanddate.Text = dt1.Rows[0]["LANDING_DATE"].ToString();


                    txtflydate.Text = dt1.Rows[0]["FLYING_DATE"].ToString();

                    if (dt1.Rows[0]["PACKAGE_TYPE"].ToString() != "")
                    {
                        DropDownList3.Text = dt1.Rows[0]["PACKAGE_TYPE"].ToString();
                    }
                    DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", dt1.Rows[0]["CUST_COMPANY_NAME"].ToString());
                    if (DS.Tables[0].Rows.Count != 0)
                    {
                        DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()));
                        if (ds_SubAgent.Tables[0].Rows.Count != 0)
                        {
                            binddropdownlist(drpSubagent, ds_SubAgent);

                            drpSubagent.Text = dt1.Rows[0]["BOOK_BY_AGENT"].ToString();
                            if (drpSubagent.Text != "")
                            {
                                Session["rel_sr_no"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                            }
                            else
                            {
                                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                            }
                        }
                        else
                        {
                            Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                        }
                    }
                    
                    if (dt1.Rows[0]["ARRIVAL_TIME"].ToString() != "")
                    {
                        RadTimePicker1.SelectedDate = DateTime.Parse(dt1.Rows[0]["ARRIVAL_TIME"].ToString());
                    }
                    if (dt1.Rows[0]["DEPARTURE_TIME"].ToString() != "")
                    {
                        RadTimePicker2.SelectedDate = DateTime.Parse(dt1.Rows[0]["DEPARTURE_TIME"].ToString());
                    }
                    DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "12");
                    if (Session["editorderstatus"].ToString() == DTORDER.Rows[0]["ORDER_STATUS_NAME"].ToString())
                    {
                        CANCELLAETION.Attributes.Add("style", "display");
                    }
                    if (dt1.Rows[0]["MY_FAVOURITE_PACKAGE"].ToString() == "True")
                    {
                        chkpakage.Checked = true;
                    }
                    else
                    {
                        chkpakage.Checked = false;
                    }

                    txtClientlastname.Text = dt1.Rows[0]["CLIENT_SURNAME"].ToString();


                    DataSet ds22 = objBookingFitStoreProcedure.fetchTitle();
                    binddropdownlist(drpTitle, ds22);
               

                    drpTitle.Text = dt1.Rows[0]["CLIENT_TITLE"].ToString();
                    
                    DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                    binddropdownlist(DropDownList1, dsval);
                    DropDownList1.SelectedValue = dt1.Rows[0]["ORDER_STATUS_NAME"].ToString();
                    
                    DropDownList1.Enabled = false;
                    dt1.Rows[0]["FIT_PACKAGE_NAME"].ToString();
                    Session["Packgename"] = dt1.Rows[0]["FIT_PACKAGE_NAME"].ToString();
                    DataTable dt2 = objBookingFitStoreProcedure.fetchPackages();
                    datalist_packages.DataSource = dt2;
                    datalist_packages.DataBind();

                    foreach (DataListItem item in datalist_packages.Items)
                    {
                        Label packname = (Label)item.FindControl("Label1");

                       
                        if (packname.Text == dt1.Rows[0]["FIT_PACKAGE_NAME"].ToString())
                        {
                            Label hotelid = (Label)item.FindControl("Label2");
                            RadioButton rbtn = (RadioButton)item.FindControl("rbtnpackage");
                            rbtn.Checked = true;
                            Session["packageid"] = hotelid.Text;
                            break;
                        }


                    }
                    DataSet ds1 = objfitquote.FetchForFitBookingquote("FETCH_DATA_FOR_BOOKING_FIT_USING_QUOTEID", quoteid);
                   
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        DataTable dtquote = ds1.Tables[0];
                        dtquote.Rows[0]["NO_OF_ROOMS"].ToString();
                        dtquote.Rows[0]["ROOM_TYPE"].ToString();
                        for (int i = 0; i < dtquote.Rows.Count; i++)
                        {
                            if (dtquote.Rows[i]["ROOM_TYPE"].ToString() == "1")
                            {
                                txtNoroomSingle.Text = "";
                                txtNoroomSingle.Text = dtquote.Rows[i]["NO_OF_ROOMS"].ToString();
                            }
                            if (dtquote.Rows[i]["ROOM_TYPE"].ToString() == "2")
                            {
                                txtNoroomDouble.Text = "";
                                txtNoroomDouble.Text = dtquote.Rows[i]["NO_OF_ROOMS"].ToString();
                            }
                            if (dtquote.Rows[i]["ROOM_TYPE"].ToString() == "3")
                            {
                                txtNoroomTriple.Text = "";
                                txtNoroomTriple.Text = dtquote.Rows[i]["NO_OF_ROOMS"].ToString();
                            }
                        }
                     
                    }


                    if (DropDownList1.Text == "Booked")
                    {
                        landing.Attributes.Add("style", "display");
                        fly.Attributes.Add("style", "display");
                        spanFly.Visible = false;
                        spanLanding.Visible = false;
                    }
                    if (DropDownList1.Text == "To Be Reconfirmed")
                    {
                        discount.Attributes.Add("Style", "display");
                      
                        landing.Attributes.Add("style", "display");
                        fly.Attributes.Add("style", "display");
                        
                    }
                    if (DropDownList1.Text == "Reconfirmed")
                    {
                        discount.Attributes.Add("Style", "display");
                   
                    }


                }
                else
                {
                    DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "1");
                    Session["editorderstatus"] = DTORDER.Rows[0]["ORDER_STATUS_NAME"].ToString();
                    DropDownList1.SelectedValue = "In Process";
                   
                    DropDownList1.Enabled = false;
                    DataTable dt = objBookingFitStoreProcedure.fetchPackages();
                    datalist_packages.DataSource = dt;
                    datalist_packages.DataBind();


                    DataSet ds22 = objBookingFitStoreProcedure.fetchTitle();
                    binddropdownlist(drpTitle, ds22);
               
                
                }
                DataSet dsstatus = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                binddropdownlist(DropDownList1, dsstatus);
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(DropDownList2, ds3);

                DataSet ds_fit_pack_type = objBookingFitStoreProcedure.fetchComboData("FETCH_FIT_PACKAGE_TYPE");
                binddropdownlist(DropDownList3, ds_fit_pack_type);

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            DataSet ds_fit_pack_type = objBookingFitStoreProcedure.fetchComboData("FETCH_FIT_PACKAGE_TYPE");
            updatehotel_details();
            //room_validation();
            bool falg_room = true;

            if (DropDownList3.Text == ds_fit_pack_type.Tables[0].Rows[0]["AutoSearchResult"].ToString() || DropDownList3.Text == ds_fit_pack_type.Tables[0].Rows[1]["AutoSearchResult"].ToString() || DropDownList3.Text == ds_fit_pack_type.Tables[0].Rows[4]["AutoSearchResult"].ToString() || DropDownList3.Text == ds_fit_pack_type.Tables[0].Rows[5]["AutoSearchResult"].ToString())
            {
                room_validation();
                 if (txtNoroomSingle.Text == "" && txtNoroomDouble.Text == "" && txtNoroomTriple.Text == "")
                 {
                      falg_room = false;
                    
                 }
            }
            landingdate();
            if (Session["packageid"] == null)
            {
                Master.DisplayMessage("Select At Least Tour Package.", "successMessage", 3000);
            }
          
            else if (falg_room == false)
            {
                Master.DisplayMessage("Select At Least One Room Type.", "successMessage", 3000);
            }  
            else if (flag_room == false)
            {
                Master.DisplayMessage("Insufficient rooms, please select more rooms.", "successMessage", 3000);
            }
            else if (DropDownList3.Text  == "")
            {
                Master.DisplayMessage("FIT Package Type is required.", "successMessage", 3000);
            }

            else if (fly_land == false)
            {
                Master.DisplayMessage("Please Fill Arrival Flight Date And Departure Flight Date.", "successMessage", 3000);
            }
            else
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    DataSet ds_tour_date = objHotelStoreProcedure.fetch_tour_Dates("GET_TOUR_DATE", int.Parse(Request["TOURID"].ToString()));

                    if ((DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds_tour_date.Tables[0].Rows[0]["FROM_DATE"].ToString(), "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds_tour_date.Tables[0].Rows[0]["TO_DATE"].ToString(), "dd/MM/yyyy", null)))
                    {
                        Session["date_change"] = null;
                    }
                    else
                    {
                        Session["date_change"] = "y";
                    }
                }
                try
                {
                    bool flag_amt = true;
                    DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "12");
                    if (Session["editorderstatus"].ToString() == DTORDER.Rows[0]["ORDER_STATUS_NAME"].ToString())
                    {

                        if (Session["RoleId"].ToString() == "37" || Session["RoleId"].ToString() == "38" || Session["RoleId"].ToString() == "39" || Session["RoleId"].ToString() == "40")
                        {
                            if (txtfees.Text == "" || txtfees.Text == "0")
                            {
                                Master.DisplayMessage("Please enter amount greater then zero.", "successMessage", 3000);
                                flag_amt = false;
                            }
                            else if (decimal.Parse(txtfees.Text) < 0)
                            {
                                Master.DisplayMessage("Please enter amount greater then zero.", "successMessage", 3000);
                                flag_amt = false;
                            }
                            else
                            {

                            }
                            
                        }
                        else if (Session["RoleId"].ToString() == "18")
                        {
                            if (txtfees.Text == "")
                            {
                                Master.DisplayMessage("Atleast Fill 0 in Agent Cancellation Fees.", "successMessage", 3000);
                                flag_amt = false;
                            }
                            else if (decimal.Parse(txtfees.Text) < 0)
                            {
                                Master.DisplayMessage("Please enter amount greater then zero.", "successMessage", 3000);
                                flag_amt = false;
                            }
                            else
                            {

                            }
                        }
                       
                        if (flag_amt == true)
                        {
                            System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                            System.DateTime today1 = DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null);

                            Session["fit_select_type"] = DropDownList3.Text;

                            Session["agentfees"] = txtfees.Text;
                            Session["clientname"] = txtClientname.Text;
                            Session["clientsurnameadmin"] = txtClientlastname.Text;
                            Session["clienttitleadmin"] = drpTitle.Text;
                            DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
                            if (DS.Tables[0].Rows.Count != 0)
                            {
                                if (drpSubagent.Text != "")
                                {
                                    DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT_REL_NO(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()), drpSubagent.Text);
                                    if (ds_SubAgent.Tables[0].Rows.Count != 0)
                                    {
                                        Session["rel_sr_no"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                                    }
                                }
                                else
                                {
                                    Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                                }
                            }
                            Session["noofadult"] = txtNo_Adult.Text;
                            Session["noofcwb"] = txtNo_CWB.Text;
                            Session["noofcnb"] = txtNo_CNB.Text;
                            Session["noofinfant"] = txtNo_Infant.Text;
                            Session["singleroom"] = txtNoroomSingle.Text;
                            Session["doubleroom"] = txtNoroomDouble.Text;
                            if (txtNoroomTriple.Text == "")
                            {
                                Session["tripleroom"] = "0";
                            }
                            else
                            {
                                Session["tripleroom"] = txtNoroomTriple.Text;
                            }
                            Session["nights"] = txtNo_OfNights.Text;
                            Session["orderstatus"] = DropDownList1.Text;
                            Session["fromdate"] = txtFrom_Date.Text;
                            Session["todate"] = txtTo_Date.Text;
                            Session["arrivalflight"] = txtArrival_Flight.Text;
                            Session["departureflight"] = txtDeparture_Flight.Text;

                            Session["landingDate"] = txtlanddate.Text;
                            Session["flyingDate"] = txtflydate.Text;

                            if (chkpakage.Checked == true)
                            {
                                Session["favouritepackage"] = "True";
                            }
                            else
                            {
                                Session["favouritepackage"] = "False";
                            }
                            if (RadTimePicker1.SelectedDate == null)
                            {
                                Session["arrivaltime"] = "";
                            }
                            else
                            {
                                Session["arrivaltime"] = RadTimePicker1.SelectedDate;
                            }

                            if (RadTimePicker2.SelectedDate == null)
                            {
                                Session["departuretime"] = "";
                            }
                            else
                            {
                                Session["departuretime"] = RadTimePicker2.SelectedDate;
                            }
                            Session["tourname"] = txtTourname.Text;
                            Session["remarks"] = txtRemarks.Text;
                            String tid = null;
                            String qid = null;
                            if (Session["tourid"] != null)
                            {
                                tid = Session["tourid"].ToString();
                            }
                            if (Session["quoteid"] != null)
                            {
                                qid = Session["quoteid"].ToString();
                            }
                            if (tid == null && qid == null)
                            {
                                Response.Redirect("~/Views/FIT/AdminHotels.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Views/FIT/AdminHotels.aspx?TOURID=" + Session["tourid"].ToString() + "&QUOTEID=" + Session["quoteid"].ToString());
                            }
                        }
                    }
                    else
                    {
                        System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                        System.DateTime today1 = DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null);

                        Session["fit_select_type"] = DropDownList3.Text;

                        Session["agentfees"] = txtfees.Text;
                        Session["clientname"] = txtClientname.Text;
                        Session["clientsurnameadmin"] = txtClientlastname.Text;
                        Session["clienttitleadmin"] = drpTitle.Text;
                        DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
                        if (DS.Tables[0].Rows.Count != 0)
                        {
                            if (drpSubagent.Text != "")
                            {
                                DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT_REL_NO(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()), drpSubagent.Text);
                                if (ds_SubAgent.Tables[0].Rows.Count != 0)
                                {
                                    Session["rel_sr_no"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                                }
                            }
                            else
                            {
                                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                            }
                        }
                        Session["noofadult"] = txtNo_Adult.Text;
                        if (txtNo_CWB.Text == "")
                        {
                            Session["noofcwb"] = "0";
                        }
                        else
                        {

                            Session["noofcwb"] = txtNo_CWB.Text;
                        }

                        if (txtNo_CNB.Text == "")
                        {
                            Session["noofcnb"] = "0";
                        }
                        else
                        {
                            Session["noofcnb"] = txtNo_CNB.Text;
                        }
                        Session["noofinfant"] = txtNo_Infant.Text;
                        Session["singleroom"] = txtNoroomSingle.Text;
                        Session["doubleroom"] = txtNoroomDouble.Text;
                        Session["tripleroom"] = txtNoroomTriple.Text;
                        Session["nights"] = txtNo_OfNights.Text;
                        Session["orderstatus"] = DropDownList1.Text;
                        Session["fromdate"] = txtFrom_Date.Text;
                        Session["todate"] = txtTo_Date.Text;
                        Session["arrivalflight"] = txtArrival_Flight.Text;
                        Session["departureflight"] = txtDeparture_Flight.Text;

                        Session["landingDate"] = txtlanddate.Text;
                        Session["flyingDate"] = txtflydate.Text;

                        if (chkpakage.Checked == true)
                        {
                            Session["favouritepackage"] = "True";
                        }
                        else
                        {
                            Session["favouritepackage"] = "False";
                        }
                        if (RadTimePicker1.SelectedDate == null)
                        {
                            Session["arrivaltime"] = "";
                        }
                        else
                        {
                            Session["arrivaltime"] = RadTimePicker1.SelectedDate;
                        }

                        if (RadTimePicker2.SelectedDate == null)
                        {
                            Session["departuretime"] = "";
                        }
                        else
                        {
                            Session["departuretime"] = RadTimePicker2.SelectedDate;
                        }
                        Session["tourname"] = txtTourname.Text;
                        Session["remarks"] = txtRemarks.Text;
                        String tid = null;
                        String qid = null;
                        if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                        {
                            Session["quoteid"] = Request["QUOTEID"].ToString();
                            Session["tourid"] = Request["TOURID"].ToString();

                            
                            DataTable dttra = objHotelStoreProcedure.fetchTransferPackageforedit("FETCH_ALL_DATA_FOR_TRANSFERPACKAGE_EDIT_NEW", Request["QUOTEID"].ToString());
                            if (dttra.Rows.Count == 0)
                            {
                                Session["TP"] =  null;
                            }
                            else
                            {
                                Session["TP"] = "TRansfer";
                            }
                            if (DropDownList1.SelectedValue == "To Be Reconfirmed")
                            {
                                if (fly_land == true)
                                {
                                    System.DateTime FlyingDate = DateTime.ParseExact(txtflydate.Text, "dd/MM/yyyy", null);
                                    System.DateTime LandingDate = DateTime.ParseExact(txtlanddate.Text, "dd/MM/yyyy", null);
                                    objBookingFitStoreProcedure.UpdateTourFlyingAndLandingDate(int.Parse(Request["TOURID"].ToString()), txtflydate.Text, txtlanddate.Text);
                                    Response.Redirect("~/Views/FIT/AdminHotels.aspx?TOURID=" + Request["TOURID"].ToString() + "&QUOTEID=" + Request["QUOTEID"].ToString());
                                }
                                else
                                {
                                    Master.DisplayMessage("Please Enter Arrival & Departure Flight Dates.", "successMessage", 3000);
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Views/FIT/AdminHotels.aspx?TOURID=" + Request["TOURID"].ToString() + "&QUOTEID=" + Request["QUOTEID"].ToString());
                            }
                            
                        }
                        else
                        {

                            Response.Redirect("~/Views/FIT/AdminHotels.aspx");

                        }

                    }
                }
                catch
                {
                    Master.DisplayMessage("Date is not in correct format", "successMessage", 3000);
                }
                finally
                {
                }
            }
        }

        #region FETCH DATA IN ORDER STATUS COMBO BOX
       
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "";
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

        #endregion

        #region GET PACKAGE ID
        protected void updatehotel_details()
        {
            RadioButton checkedButton = null;

            foreach (DataListItem item in datalist_packages.Items)
            {


                Label hotelid = (Label)item.FindControl("Label2");
                RadioButton rbtn = (RadioButton)item.FindControl("rbtnpackage");
                if (rbtn.Checked)
                {
                    Label packname = (Label)item.FindControl("Label1");
                    Session["packageid"] = hotelid.Text;
                    Session["Packgename"] = packname.Text;

                    DataSet ds = objBookingFitStoreProcedure.fetch_no_of_nights("FETCH_MIN_NIGHT_OF_PACKAGE", int.Parse(Session["packageid"].ToString()));
                    int night = 0;
                    if (ds.Tables[0].Rows[0]["MINIMUM_NIGHTS"].ToString() == "")
                    {
                        night = 0;
                    }
                    else
                    {
                        night = int.Parse(ds.Tables[0].Rows[0]["MINIMUM_NIGHTS"].ToString());
                    }


                    if (int.Parse(txtNo_OfNights.Text) < night)
                    {
                        string msg = "Minimum night of" + " " + packname.Text + " " + "is" + " " + ds.Tables[0].Rows[0]["MINIMUM_NIGHTS"].ToString();
                        Master.DisplayMessage(msg, "successMessage", 3000);
                        ViewState["error"] = "done";
                        txtNo_OfNights.Text = "";
                        UpdatePanel_TourDetails.Update();
                    }
                    break;
                }
            }
        }

        public void CheckChanged(object sender, EventArgs e)
        {
            //on each item checked, remove any other items checked
            foreach (DataListItem item in datalist_packages.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbtnpackage");
                if (rb != sender)
                {
                    rb.Checked = false;
                }
                else
                {
                    Label packname = (Label)item.FindControl("Label1");
                    //Session["packageid"] = hotelid.Text;
                    Session["Packgename"] = packname.Text;
                    TournameGenerate();
                }
            }
            DropDownList2.Focus();
        }
        #endregion

        protected void txtNo_OfNights_TextChanged(object sender, EventArgs e)
        {
            updatehotel_details();
            if (Session["Packgename"] == null)
            {
                
                Master.DisplayMessage("Select a Package.", "successMessage", 3000);
            }
            else if (ViewState["error"] != null)
            {
                txtTourname.Text = "";
                ViewState["error"] = null;
                txtNo_OfNights.Focus();
            }
            else
            {
                txtTourname.Text = txtClientname.Text + " " + txtClientlastname.Text + " " + Session["Packgename"].ToString() + " " + txtNo_OfNights.Text + "Nights";

                ViewState["error"] = null;
                txtTourname.Focus();
            }

            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {
                if (txtFrom_Date.Text != "")
                {
                    System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                    today.ToString("MM-dd-yyyy");

                    System.DateTime answer1 = today.AddDays(double.Parse(txtNo_OfNights.Text));

                    txtTo_Date.Text = answer1.ToString("dd/MM/yyyy");
                }
                
            }
            else if (txtFrom_Date.Text != "dd/MM/yyyy")
            {
                if (txtFrom_Date.Text != "")
                {
                    System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                    today.ToString("MM-dd-yyyy");

                    System.DateTime answer1 = today.AddDays(double.Parse(txtNo_OfNights.Text));

                    txtTo_Date.Text = answer1.ToString("dd/MM/yyyy");
                }
            }
            
            UpdatePanel_TourDetails.Update();

        }

        protected void txtFrom_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);

                string result;
                string ydresult;
                string valid = txtFrom_Date.Text;

                string[] validte = valid.Split('/');

                int lenght = validte.Count();
               
                DateTime Result;
                DateTimeFormatInfo info = new DateTimeFormatInfo();
                CultureInfo culture;
                culture = CultureInfo.CreateSpecificCulture("en-US");
                info.ShortDatePattern = "dd/MM/yyyy";
                if (Session["FLAG"].ToString() == "A")
                {
                    today.ToString("MM-dd-yyyy");

                    System.DateTime answer = today.AddDays(double.Parse(txtNo_OfNights.Text));
                    
                    DateTime yesterday1 = DateTime.Today;

                    System.DateTime yesterday = yesterday1.AddDays(double.Parse("3"));
                    yesterday.ToString("MM-dd-yyyy");
                   

                    // FOR YESTERDAY
                    string ydsource = yesterday.ToString();
                    string ydstr1 = ydsource;
                    string[] ydw = ydstr1.Split('/');

                    string ydt = ydw[2];
                    string[] ydt1 = ydt.Split(' ');

                    if (ydw[1] == "1" || ydw[1] == "2" || ydw[1] == "3" || ydw[1] == "4" || ydw[1] == "5" || ydw[1] == "6" || ydw[1] == "7" || ydw[1] == "8" || ydw[1] == "9")
                    {
                        if (ydw[0] == "1" || ydw[0] == "2" || ydw[0] == "3" || ydw[0] == "4" || ydw[0] == "5" || ydw[0] == "6" || ydw[0] == "7" || ydw[0] == "8" || ydw[0] == "9")
                        {
                            ydresult = "0" + ydw[1] + "/" + "0" + ydw[0] + "/" + ydt1[0];
                            
                        }
                        else
                        {
                            ydresult = "0" + ydw[1] + "/" + ydw[0] + "/" + ydt1[0];
                            
                        }
                    }
                    else
                    {
                        if (ydw[0] == "1" || ydw[0] == "2" || ydw[0] == "3" || ydw[0] == "4" || ydw[0] == "5" || ydw[0] == "6" || ydw[0] == "7" || ydw[0] == "8" || ydw[0] == "9")
                        {
                            ydresult = ydw[1] + "/" + "0" + ydw[0] + "/" + ydt1[0];
                            
                        }
                        else
                        {
                            ydresult = ydw[1] + "/" + ydw[0] + "/" + ydt1[0];
                            
                        }
                    }

                    if (Session["FLAG"].ToString() == "A")
                    {
                        if (DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null) <= DateTime.ParseExact(ydresult, "dd/MM/yyyy", null))
                        {
                            Master.DisplayMessage("Booking date is allowed after 3 days of todays's date", "successMessage", 5000);
                            txtFrom_Date.Text = "";
                            UpdatePanel_TourDetails.Update();

                        }
                        else
                        {
                            if (lenght != 3)
                            {
                                Master.DisplayMessage("Date is not in correct format", "successMessage", 3000);
                            }
                            else if (!DateTime.TryParseExact(txtFrom_Date.Text, "dd/MM/yyyy", info, DateTimeStyles.None, out Result))
                            {
                                Master.DisplayMessage("Date is not in correct format", "successMessage", 3000);
                            }
                            else if (txtNo_OfNights.Text == "")
                            {
                                Master.DisplayMessage("No of nights is required", "successMessage", 5000);
                                txtFrom_Date.Text = "";
                                UpdatePanel_TourDetails.Update();
                            }
                            else
                            {

                                today.ToString("MM-dd-yyyy");

                                System.DateTime answer1 = today.AddDays(double.Parse(txtNo_OfNights.Text));

                                string source = answer1.ToString();
                                string str1 = source;
                                string[] w = str1.Split('/');

                                string t = w[2];
                                string[] t1 = t.Split(' ');

                                if (w[1] == "1" || w[1] == "2" || w[1] == "3" || w[1] == "4" || w[1] == "5" || w[1] == "6" || w[1] == "7" || w[1] == "8" || w[1] == "9")
                                {
                                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                                    {
                                        result = "0" + w[1] + "/" + "0" + w[0] + "/" + t1[0];
                                        txtTo_Date.Text = result;
                                    }
                                    else
                                    {
                                        result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                                        txtTo_Date.Text = result;
                                    }
                                }
                                else
                                {
                                    if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                                    {
                                        result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                                        txtTo_Date.Text = result;
                                    }
                                    else
                                    {
                                        result = w[1] + "/" + w[0] + "/" + t1[0];
                                        txtTo_Date.Text = result;
                                    }
                                }

                                txtTo_Date.Text = answer1.ToString("dd/MM/yyyy");
                                txtArrival_Flight.Focus();
                                UpdatePanel_TourDetails.Update();
                            }
                        }
                    }
                  
                }
                else if (Session["FLAG"].ToString() == "E")
                {
                    today.ToString("MM-dd-yyyy");

                    System.DateTime answer1 = today.AddDays(double.Parse(txtNo_OfNights.Text));

                    string source = answer1.ToString();
                    string str1 = source;
                    string[] w = str1.Split('/');

                    string t = w[2];
                    string[] t1 = t.Split(' ');

                    if (w[1] == "1" || w[1] == "2" || w[1] == "3" || w[1] == "4" || w[1] == "5" || w[1] == "6" || w[1] == "7" || w[1] == "8" || w[1] == "9")
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = "0" + w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            txtTo_Date.Text = result;
                        }
                        else
                        {
                            result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                            txtTo_Date.Text = result;
                        }
                    }
                    else
                    {
                        if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                        {
                            result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                            txtTo_Date.Text = result;
                        }
                        else
                        {
                            result = w[1] + "/" + w[0] + "/" + t1[0];
                            txtTo_Date.Text = result;
                        }
                    }

                    txtTo_Date.Text = answer1.ToString("dd/MM/yyyy");
                    txtArrival_Flight.Focus();
                    UpdatePanel_TourDetails.Update();
                }


            }
            catch
            {
                Master.DisplayMessage("Date is not in correct format", "successMessage", 3000);
                txtFrom_Date.Text = "";
            }

            finally
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/FIT/MyBooking.aspx");
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.Text != "")
            {
                string name = DropDownList2.Text;
                DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();

                if (DS.Tables[0].Rows.Count != 0)
                {
                    DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()));
                    binddropdownlist(drpSubagent, ds_SubAgent);
                }
            }
            UpdatePanel_TourDetails.Update();
        }

        protected void drpSubagent_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
            if (DS.Tables[0].Rows.Count != 0 && drpSubagent.Text != "")
            {
                DataSet ds_SubAgent = objBookingFitStoreProcedure.GET_SUB_AGENT_REL_NO(int.Parse(DS.Tables[0].Rows[0]["CUST_ID"].ToString()),drpSubagent.Text);
                if (ds_SubAgent.Tables[0].Rows.Count != 0)
                {
                    Session["rel_sr_no"] = ds_SubAgent.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                }
            }
        }

        protected void room_validation()
        {
            int no_of_adult;
            int no_of_cnb;
            int no_of_cwb = 0;


            int no_of_single_room;
            int no_of_double_room;
            int no_of_triple_rom;

            if (txtNo_Adult.Text != "")
            {
                no_of_adult = int.Parse(txtNo_Adult.Text);
            }
            else
            {
                no_of_adult = 0;
            }

            if (txtNo_CNB.Text != "")
            {
                no_of_cnb = int.Parse(txtNo_CNB.Text);
            }
            else
            {
                no_of_cnb = 0;
            }

            if (txtNo_CWB.Text != "")
            {
                no_of_cwb = int.Parse(txtNo_CWB.Text);
            }
            else
            {

                no_of_cwb = 0;
            }

            int total = no_of_cwb + no_of_cnb;

            for (int a = 0; a < no_of_adult; a++)
            {
                if (txtNoroomSingle.Text != "")
                {
                    no_of_adult = no_of_adult - int.Parse(txtNoroomSingle.Text);
                }
                if (no_of_adult != 0)
                {
                    if (txtNoroomDouble.Text != "")
                    {
                        no_of_adult = no_of_adult - int.Parse(txtNoroomDouble.Text) * 2;
                    }
                }
                if (no_of_adult != 0)
                {
                    if (txtNoroomTriple.Text != "")
                    {
                        no_of_adult = no_of_adult - int.Parse(txtNoroomTriple.Text) * 3;
                    }
                }
                break;
            }

            for (int cwb = 0; cwb < no_of_cwb; cwb++)
            {
                if (no_of_cwb > 0)
                {
                    if (txtNoroomDouble.Text != "")
                    {
                        no_of_cwb = no_of_cwb - int.Parse(txtNoroomDouble.Text) * 1;
                    }
                }
            }
            for (int cnb = 0; cnb < no_of_cnb; cnb++)
            {
                if (no_of_cnb > 0)
                {
                    if (txtNoroomDouble.Text != "")
                    {
                        no_of_cnb = no_of_cnb - int.Parse(txtNoroomDouble.Text) * 1;
                    }
                }

                if (no_of_cnb > 0)
                {
                    if (txtNoroomTriple.Text != "")
                    {
                        no_of_cnb = no_of_cnb - int.Parse(txtNoroomTriple.Text) * 1;
                    }
                }
            }

            if (no_of_adult < 0 && no_of_cwb > 0)
            {
                no_of_cwb = no_of_cwb + no_of_adult;
            }

            if (no_of_adult < 0 && no_of_cnb > 0)
            {
                no_of_cnb = no_of_cnb + no_of_adult;
            }
           

            if (no_of_adult > 0)
            {
                flag_room = false;
            }
            if (no_of_cwb > 0)
            {
                flag_room = false;
            }
            if (no_of_cnb > 0)
            {
                flag_room = false;
            }
           
        }

        protected void airline_validation(String order_status)
        {
            DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
            if (order_status == dsval.Tables[0].Rows[3]["AutoSearchResult"].ToString())
            {
                string enterd_date = "";
                string arr_time;
                string tp_time = "";
                DataTable dttra = objHotelStoreProcedure.fetchTransferPackageforedit("FETCH_ALL_DATA_FOR_TRANSFERPACKAGE_EDIT_NEW", Request["QUOTEID"].ToString());

                for (int i = 0; i < dttra.Rows.Count; i++)
                {
                    if (dttra.Rows[i]["FLAG"].ToString() == "A")
                    {
                        tp_time = dttra.Rows[i]["START_TIME"].ToString();

                        break;
                    }
                }

                if (txtArrival_Flight.Text == "")
                {
                    flag_flight = false;
                }
                if (txtDeparture_Flight.Text == "")
                {
                    flag_flight = false;
                }
                if (RadTimePicker1.SelectedDate == null)
                {
                    flag_flight = false;
                }
                else
                {
                    DateTime dt = DateTime.Parse(RadTimePicker1.SelectedDate.ToString());
                    string dt1 = dt.ToString("HH:mm");

                    string str1 = dt1;
                    string[] words = str1.Split(':');

                    int i = words.Length;
                    foreach (string word in words)
                    {
                        enterd_date = enterd_date + word;
                    }

                    if (int.Parse(enterd_date) < int.Parse(tp_time))
                    {

                    }
                    else
                    {
                        flag_time = false;
                    }
                }
                if (RadTimePicker2.SelectedDate == null)
                {
                    flag_flight = false;
                }



            }
        }

        protected void txtClientname_TextChanged(object sender, EventArgs e)
        {
            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {
                txtTourname.Text = txtClientname.Text + " " + txtClientlastname.Text + " " + Session["Packgename"].ToString() + " " + txtNo_OfNights.Text + "Nights";
                txtClientlastname.Focus();
                UpdatePanel_TourDetails.Update();
                
            }
        }

        protected void txtClientlastname_TextChanged(object sender, EventArgs e)
        {
            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {
                txtTourname.Text = txtClientname.Text + " " + txtClientlastname.Text + " " + Session["Packgename"].ToString() + " " + txtNo_OfNights.Text + "Nights";
                txtNo_Adult.Focus();
                UpdatePanel_TourDetails.Update();
            }
        }

        protected void Checked_Changed(object sender, EventArgs e)
        {
            if (chkdiscount.Checked == true)
            {
                Label29.Text = "";

                txtdiscount.Text = "";
                DataSet ds = objBookingFitStoreProcedure.GET_QUTED_COST(int.Parse(Request.QueryString["TOURID"].ToString()), int.Parse(Request.QueryString["QUOTEID"].ToString()));

                Label27.Text = ds.Tables[0].Rows[0][0].ToString();

                Paneldicount.Attributes.Add("style", "display");

                AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();



                modalPop.ID = "popUp";



                modalPop.PopupControlID = "Paneldicount";



                modalPop.TargetControlID = "chkdiscount";



                modalPop.DropShadow = true;


                modalPop.CancelControlID = "ImageButton1";

                this.Paneldicount.Controls.Add(modalPop);

           
                modalPop.Show();

                UpdatePanel_TourDetails.Update();
            }
        }

        protected void btnSave_discount(object sender, EventArgs e)
        {
            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
            {

                if (decimal.Parse(Label27.Text) < decimal.Parse(txtdiscount.Text))
                {
                    Label29.Text = "Can not give Discount greater then Invoice Amount";

                    AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();

                    modalPop.ID = "popUp";

                    modalPop.PopupControlID = "Paneldicount";

                    modalPop.TargetControlID = "chkdiscount";

                    modalPop.DropShadow = true;

                    modalPop.CancelControlID = "ImageButton1";

                    this.Paneldicount.Controls.Add(modalPop);

                    modalPop.Show();

                    UpdatePanel_TourDetails.Update();
                }
                else
                {

                    DataSet dsstatus = objBookingFitStoreProcedure.fetchComboData("FETCH_SALES_ACCOUNT_GL_CODE");
                    if (dsstatus.Tables[0].Rows.Count != 0)
                    {
                        if (txtdiscount.Text != "")
                        {
                            objBookingFitStoreProcedure.UpdateTourdiscount(txtdiscount.Text, int.Parse(Request["TOURID"].ToString()), int.Parse(Request["QUOTEID"].ToString()));
                     
                        }
                    }

                    
                    SendMail_Ondiscount();
                    Master.DisplayMessage("Discount Amount Updated Successfully.", "successMessage", 3000);
                    Paneldicount.Attributes.Add("style", "display:none");
                    UpdatePanel_TourDetails.Update();
                }
            }
        }

        public void SendMail_Ondiscount()
        {

             DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[7]["AutoSearchResult"].ToString());

            DataTable dtemail = objHotelStoreProcedure.fetch_backoffice_for_book(Request.QueryString["QUOTEID"].ToString());

            string a;
            if (dtemail.Rows.Count != 0)
            {
                if (dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString() == "1")
                {
                    
                    a = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
                }
                else
                {
                    
                    a = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
                }
            }
            else
            {
                DataTable dt1 = objHotelStoreProcedure.update_quote_for_backoffice(int.Parse(Session["updateid"].ToString()), "1");
                a = dt1.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();
            }

            if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand comm = new SqlCommand("FETCH_AGENT_QUOTATION_DETAILS", conn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["QUOTEID"].ToString());

                SqlDataReader rdr = comm.ExecuteReader();
                dt.Load(rdr);

                string   AgentName = dt.Rows[0]["CUST_REL_NAME"].ToString();
                string   Tourname = dt.Rows[0]["TOUR_SHORT_NAME"].ToString();

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();


                string fromemail = "";
                if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_boMail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());
                    if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = dt.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                     
                        if (a == "1")
                        {
                            fromemail = "reservation@travelzunlimited.com";
                     
                        }
                        else if (a == "2")
                        {
                            fromemail = "reservation1@travelzunlimited.com";
                     
                        }
                     
                    }
                    
                }


                string toemail1 = "";
                if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());
                    if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        toemail1 = dt.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                      
                        if (a == "1")
                        {

                            toemail1 = ds_Mail.Tables[0].Rows[2]["EMAIL_ADDRESS"].ToString();
                        }
                        else if (a == "2")
                        {

                            toemail1 = ds_Mail.Tables[0].Rows[1]["EMAIL_ADDRESS"].ToString();
                        }
                    }
                    else
                    {
                        
                        toemail1 = ds_Mail.Tables[0].Rows[0][0].ToString();
                    }


                }
                

                string cc = "";

                if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                {
                    if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        DataSet ds_boMail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());

                    
                        if (a == "1")
                        {
                            cc = "reservation@travelzunlimited.com";
                    
                        }
                        else if (a == "2")
                        {
                            cc = "reservation1@travelzunlimited.com";
                    
                        }
                    }
                    else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = dt.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else
                    {
                        DataSet ds_boMail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                        cc = ds_boMail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string bcc = "";
                if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());
                    if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc = dt.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }


                    else if (ds_mailTemplate.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                     
                        if (a == "1")
                        {

                            bcc = ds_Mail.Tables[0].Rows[2]["EMAIL_ADDRESS"].ToString();
                        }
                        else if (a == "2")
                        {

                            bcc = ds_Mail.Tables[0].Rows[1]["EMAIL_ADDRESS"].ToString();
                        }
                    }
                    else
                    {
                        
                        bcc = ds_Mail.Tables[0].Rows[0][0].ToString();
                    }
                }


                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(toemail1.ToString()));

                if (cc != "")
                {
                    message.CC.Add(new MailAddress(cc.ToString()));
                }
                if (bcc != "")
                {
                    message.Bcc.Add(new MailAddress(bcc.ToString()));
                }

               string  body = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
               body = body.Replace("&lt;%AGENTNAME%&gt;", AgentName);
               body = body.Replace("&lt;%AMOUNT%&gt;", txtdiscount.Text);
               body = body.Replace("&lt;%TOTALAMOUNT%&gt;", Label27.Text);
               body = body.Replace("&lt;%QUOTEID%&gt;", Request.QueryString["QUOTEID"].ToString());

                String mail_subject = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                mail_subject = mail_subject.Replace("<%QUOTEID%>",Request.QueryString["QUOTEID"].ToString());
                mail_subject = mail_subject.Replace("<%BOOKINGID%>", Request.QueryString["TOURID"].ToString());
                message.Subject = mail_subject;
                //   message.Subject = "FIT - Quotation - Reference No - " + quote_id;
              //  message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = int.Parse(smtpport);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                 
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, mail_subject, body, int.Parse( Request.QueryString["QUOTEID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc, mail_subject, body, int.Parse(Request.QueryString["QUOTEID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
            }
        }

        protected void TournameGenerate()
        {
            try
            {
                if (txtClientname.Text != "" && txtClientlastname.Text != "" && Session["Packgename"] != null && txtNo_OfNights.Text != "")
                {

                    txtTourname.Text = txtClientname.Text + " " + txtClientlastname.Text + " " + Session["Packgename"].ToString() + " " + txtNo_OfNights.Text + "Nights";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdatePanel_TourDetails.Update();
            }
        }

        protected void landingdate()
        {
            if (DropDownList1.SelectedValue == "To Be Reconfirmed")
            {
                if (txtflydate.Text == "")
                {
                    fly_land = false;
                }
                if (txtlanddate.Text == "")
                {
                    fly_land = false;
                }

            }
        }

    }
}