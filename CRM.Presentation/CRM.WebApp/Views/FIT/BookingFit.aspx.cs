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
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;
using System.IO;
using System.Configuration;
using System.Net.Mail;


namespace CRM.WebApp.Views.FIT
{
    public partial class BookingFit : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        #region VARIABLES

        public int ipr = 0;

        string id;
        public string tourid;
        public string quoteid;
        bool flag_room = true;
        bool flag_flight = true;
        bool flag_time = true;

        bool flag_pack_date = true;

        bool nameChangeFlag = false;

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
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                {
                    DataTable DTORDER = objHotelStoreProcedure.fetchorderstatusname("FETCH_ORDER_STATUS_NAME_FOR_HOTEL", "5");
                    if ((Session["editorderstatus"].ToString() == DTORDER.Rows[0]["ORDER_STATUS_NAME"].ToString() || Session["editorderstatus"].ToString() == "Cancellation Pending Per Fees") && Session["RoleId"].ToString() != "18")
                    {
                        btnback.Visible = true;
                        favpackage.Attributes.Add("style", "display");
                        tourid = Request.QueryString["TOURID"].ToString();
                        quoteid = Request.QueryString["QUOTEID"].ToString();
                        Session["tourid"] = tourid;
                        Session["quoteid"] = quoteid;
                        DataSet ds = objfitquote.FetchForFitBooking("FETCH_DATA_FOR_BOOKING_FIT_USING_TOURID", tourid);
                        DataTable dt1 = ds.Tables[0];
                        txtNo_CWB.Text = dt1.Rows[0]["NO_OF_CWB"].ToString();
                        txtNo_CWB.ReadOnly = true;
                        txtNo_CNB.Text = dt1.Rows[0]["NO_OF_CNB"].ToString();
                        txtNo_CNB.ReadOnly = true;
                        txtNo_Adult.Text = dt1.Rows[0]["NO_OF_ADULTS"].ToString();
                        txtNo_Adult.ReadOnly = true;
                        txtNo_OfNights.Text = dt1.Rows[0]["NO_OF_NIGHTS"].ToString();
                        txtNo_OfNights.ReadOnly = true;
                        txtNo_Infant.Text = dt1.Rows[0]["NO_OF_INFANT"].ToString();
                        txtNo_Infant.ReadOnly = true;
                        txtClientname.Text = dt1.Rows[0]["CLIENT_NAME"].ToString();
                        txtClientname.ReadOnly = true;
                        txtDeparture_Flight.Text = dt1.Rows[0]["DEPARTURE_FLIGHT"].ToString();
                        txtDeparture_Flight.ReadOnly = true;
                        txtArrival_Flight.Text = dt1.Rows[0]["ARRIVAL_FLIGHT"].ToString();
                        txtArrival_Flight.ReadOnly = true;
                        txtTourname.Text = dt1.Rows[0]["TOUR_SHORT_NAME"].ToString();
                        txtTourname.ReadOnly = true;
                        txtFrom_Date.Text = dt1.Rows[0]["TOUR_FROM_DATE"].ToString();
                        txtFrom_Date.ReadOnly = true;
                        txtTo_Date.Text = dt1.Rows[0]["TOUR_TO_DATE"].ToString();
                        txtTo_Date.ReadOnly = true;
                        txtRemarks.Text = dt1.Rows[0]["REMARKS"].ToString();
                        txtRemarks.ReadOnly = true;
                        DropDownList3.Text = dt1.Rows[0]["PACKAGE_TYPE"].ToString();
                        DropDownList3.Enabled = false;

                        fly.Attributes.Add("Style", "display");
                        landing.Attributes.Add("Style", "display");

                        txtlanddate.Text = dt1.Rows[0]["LANDING_DATE"].ToString();
                        txtlanddate.Enabled = false;

                        txtflydate.Text = dt1.Rows[0]["FLYING_DATE"].ToString();
                        txtflydate.Enabled = false;

                        if (dt1.Rows[0]["ARRIVAL_TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(dt1.Rows[0]["ARRIVAL_TIME"].ToString());
                        }
                        if (dt1.Rows[0]["DEPARTURE_TIME"].ToString() != "")
                        {
                            RadTimePicker2.SelectedDate = DateTime.Parse(dt1.Rows[0]["DEPARTURE_TIME"].ToString());
                        }
                        if (dt1.Rows[0]["MY_FAVOURITE_PACKAGE"].ToString() == "True")
                        {
                            chkpakage.Checked = true;
                            chkpakage.Enabled = false;
                        }
                        else
                        {
                            chkpakage.Checked = false;
                            chkpakage.Enabled = false;
                        }

                        txtClientlastname.Text = dt1.Rows[0]["CLIENT_SURNAME"].ToString();


                        DataSet ds22 = objBookingFitStoreProcedure.fetchTitle();
                        binddropdownlist(drpTitle, ds22);
               

                        drpTitle.Text = dt1.Rows[0]["CLIENT_TITLE"].ToString();
                        DataSet ds2 = objBookingFitStoreProcedure.fetchTitle();
                        binddropdownlist(drpTitle, ds2);
                        DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                        binddropdownlist(DropDownList1, dsval);
                        DropDownList1.SelectedValue = Session["editorderstatus"].ToString();
                        
                        DropDownList1.Enabled = false;
                        
                        DataTable dt2 = objBookingFitStoreProcedure.fetchPackages();
                        datalist_packages.DataSource = dt2;
                        datalist_packages.DataBind();
                        foreach (DataListItem item in datalist_packages.Items)
                        {
                            Label packname = (Label)item.FindControl("Label1");
                            RadioButton rbtn1 = (RadioButton)item.FindControl("rbtnpackage");
                            if (packname.Text == dt1.Rows[0]["FIT_PACKAGE_NAME"].ToString())
                            {
                                Label hotelid = (Label)item.FindControl("Label2");
                                RadioButton rbtn = (RadioButton)item.FindControl("rbtnpackage");
                                rbtn.Checked = true;
                                Session["packageid"] = hotelid.Text;
                                
                            }
                            rbtn1.Enabled = false;
                        }
                        DataSet ds1 = objfitquote.FetchForFitBookingquote("FETCH_DATA_FOR_BOOKING_FIT_USING_QUOTEID", quoteid);
                        DataTable dtquote = ds1.Tables[0];
                        if (dtquote.Rows.Count != 0)
                        {
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
                        txtNoroomSingle.ReadOnly = true;
                        txtNoroomDouble.ReadOnly = true;
                        txtNoroomTriple.ReadOnly = true;
                        RadTimePicker1.Enabled = false;
                        RadTimePicker2.Enabled = false;
                        

                    }
                    else
                    {
                        string empflag = Session["FLAG"].ToString();
                        if (empflag == "E")
                        {
                            cancellationfees.Attributes.Add("style", "display");
                        }
                        btnback.Visible = true;
                        favpackage.Attributes.Add("style", "display");
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
                        txtClientlastname.Text = dt1.Rows[0]["CLIENT_SURNAME"].ToString();

                        txtlanddate.Text = dt1.Rows[0]["LANDING_DATE"].ToString();


                        txtflydate.Text = dt1.Rows[0]["FLYING_DATE"].ToString();

                        DataSet ds2 = objBookingFitStoreProcedure.fetchTitle();
                        binddropdownlist(drpTitle, ds2);
                    
               

                        drpTitle.Text = dt1.Rows[0]["CLIENT_TITLE"].ToString();
                     
                        txtDeparture_Flight.Text = dt1.Rows[0]["DEPARTURE_FLIGHT"].ToString();
                        txtArrival_Flight.Text = dt1.Rows[0]["ARRIVAL_FLIGHT"].ToString();
                        txtTourname.Text = dt1.Rows[0]["TOUR_SHORT_NAME"].ToString();
                        txtFrom_Date.Text = dt1.Rows[0]["TOUR_FROM_DATE"].ToString();
                        txtTo_Date.Text = dt1.Rows[0]["TOUR_TO_DATE"].ToString();
                        txtRemarks.Text = dt1.Rows[0]["REMARKS"].ToString();
                        DropDownList3.Text = dt1.Rows[0]["PACKAGE_TYPE"].ToString();

                       
                        if (dt1.Rows[0]["MY_FAVOURITE_PACKAGE"].ToString() == "True")
                        {
                            chkpakage.Checked = true;
                        }
                        else
                        {
                            chkpakage.Checked = false;
                        }
                        if (dt1.Rows[0]["ARRIVAL_TIME"].ToString() != "")
                        {
                            RadTimePicker1.SelectedDate = DateTime.Parse(dt1.Rows[0]["ARRIVAL_TIME"].ToString());
                        }
                        if (dt1.Rows[0]["DEPARTURE_TIME"].ToString() != "")
                        {
                            RadTimePicker2.SelectedDate = DateTime.Parse(dt1.Rows[0]["DEPARTURE_TIME"].ToString());
                        }
                        DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                        binddropdownlist(DropDownList1, dsval);
                        DropDownList1.SelectedValue = Session["editorderstatus"].ToString();
                        
                        DropDownList1.Enabled = false;
                        dt1.Rows[0]["FIT_PACKAGE_NAME"].ToString();
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
                        DataTable dtquote = ds1.Tables[0];
                        if (dtquote.Rows.Count != 0)
                        {
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
                        if (DropDownList1.SelectedValue == "To Be Reconfirmed")
                        {
                            landing.Attributes.Add("style", "display");
                            fly.Attributes.Add("style", "display");
                         }

                    }
                }
                else
                {

                    DropDownList1.SelectedValue = "In Process";
                    
                    DropDownList1.Enabled = false;
                    DataTable dt = objBookingFitStoreProcedure.fetchPackages();
                    datalist_packages.DataSource = dt;
                    datalist_packages.DataBind();
                    DataSet ds = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                    binddropdownlist(DropDownList1, ds);
                    DataSet ds2 = objBookingFitStoreProcedure.fetchTitle();
                    binddropdownlist(drpTitle, ds2);

                }
                DataSet ds_fit_pack_type = objBookingFitStoreProcedure.fetchComboData("FETCH_FIT_PACKAGE_TYPE");
                binddropdownlist(DropDownList3, ds_fit_pack_type);

                for (int i = DropDownList3.Items.Count - 1; i > 0; i--)
                {
                    ListItem item = DropDownList3.Items[i];
                    if (item.ToString() == "ALL SERVICES" || item.ToString() == "TRANSFER & SIGHTSEEING ONLY")
                    {

                    }
                    else
                    {
                        DropDownList3.Items.Remove(item);
                    }
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            DataSet ds_fit_pack_type = objBookingFitStoreProcedure.fetchComboData("FETCH_FIT_PACKAGE_TYPE");
            updatehotel_details();
            
            package_validation(int.Parse(Session["packageid"].ToString()));
            
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

            else if (noofPaxValidation() > 15)
            {
                Master.DisplayMessage("More then 15PAX is not allowed for FIT Package.", "successMessage", 3000);
            }

            else if (noOfRoomsValidation() > 5)
            {
                Master.DisplayMessage("You can select Max. 5 Rooms.", "successMessage", 3000);
            }

            else if (flag_room == false)
            {
                Master.DisplayMessage("Insufficient rooms, please select more rooms.", "successMessage", 3000);
            }
            else if (DropDownList3.Text == "")
            {
                Master.DisplayMessage("FIT Package Type is required.", "successMessage", 3000);
            }
            else if (flag_pack_date == false)
            {
                Master.DisplayMessage(ViewState["error_msg"].ToString(), "successMessage", 3000);
            }
          
            else
            {

                try
                {
                    System.DateTime today = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                    System.DateTime today1 = DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null);
                    Session["fit_select_type"] = DropDownList3.Text;
                    Session["clientname"] = txtClientname.Text;
                    Session["clientlastname"] = txtClientlastname.Text;
                    Session["clienttitle"] = drpTitle.Text;

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
                    DataTable dttp = objHotelStoreProcedure.fetchTransferPackage(Session["fromdate"].ToString(), Session["todate"].ToString(), Session["arrivaltime"].ToString(), Session["departuretime"].ToString(), Session["packageid"].ToString());
                    if (dttp.Rows.Count == 0)
                    {
                        Master.DisplayMessage("We do not have any Transfer Package for your travel dates, please change the travel dates and try again.", "successMessage", 3000);
                    }
                    else
                    {
                        Session["tourname"] = txtTourname.Text;
                        Session["remarks"] = txtRemarks.Text;
                        String tid = null;
                        String qid = null;
                        if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()) && Request["QUOTEID"] != null && !string.IsNullOrEmpty(Request["QUOTEID"].ToString()))
                        {
                            Session["quoteid"] = Request["QUOTEID"].ToString();
                            Session["tourid"] = Request["TOURID"].ToString();

                            clientNameChange();
                            if (DropDownList1.SelectedValue == "To Be Reconfirmed")
                            {
                                if (fly_land == true)
                                {
                                    System.DateTime FlyingDate = DateTime.ParseExact(txtflydate.Text, "dd/MM/yyyy", null);
                                    System.DateTime LandingDate = DateTime.ParseExact(txtlanddate.Text, "dd/MM/yyyy", null);
                                    objBookingFitStoreProcedure.UpdateTourFlyingAndLandingDate(int.Parse(Request["TOURID"].ToString()), txtflydate.Text, txtlanddate.Text);
                                    Response.Redirect("~/Views/FIT/Hotels.aspx?TOURID=" + Request["TOURID"].ToString() + "&QUOTEID=" + Request["QUOTEID"].ToString());
                                }
                                else
                                {
                                    Master.DisplayMessage("Please Enter Arrival & Departure Flight Dates.", "successMessage", 3000);
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Views/FIT/Hotels.aspx?TOURID=" + Request["TOURID"].ToString() + "&QUOTEID=" + Request["QUOTEID"].ToString());
                            }
                        }
                        else
                        {

                            Response.Redirect("~/Views/FIT/Hotels.aspx");

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
                    
                    Session["Packgename"] = packname.Text;
                    TournameGenerate();
                }
            }
            drpTitle.Focus();
        }

        #endregion

        protected void txtNo_OfNights_TextChanged(object sender, EventArgs e)
        {
            updatehotel_details();
            if (Session["Packgename"] == null)
            {
             
                Master.DisplayMessage("Select At Least Tour Package.", "successMessage", 3000);
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
                txtFrom_Date.Focus();
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
            else if (txtFrom_Date.Text != "dd/MM/yyyy" )
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
                string ydresult = "";
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

                    DataSet ds_day = objBookingFitStoreProcedure.fetch_bookig_days("GET_DAYS_FOR_AGENT_BOOKING");

                    System.DateTime yesterday;
                    if (ds_day.Tables[0].Rows.Count != 0)
                    {
                        yesterday = yesterday1.AddDays(double.Parse(ds_day.Tables[0].Rows[0]["DAY"].ToString()));

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
                    }

                    if (Session["FLAG"].ToString() == "A")
                    {
                        bool Dflag = true;
                        
                        if (ds_day.Tables[0].Rows.Count != 0)
                        {
                            if (DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null) <= DateTime.ParseExact(ydresult, "dd/MM/yyyy", null))
                            {
                                
                                Dflag = false;

                            }
                        }

                        if (Dflag == false)
                        {
                            string msg = "Booking date is allowed after" + " " + ds_day.Tables[0].Rows[0]["DAY"].ToString() + " " + "days of todays's date";
                            Master.DisplayMessage(msg, "successMessage", 5000);
                            txtFrom_Date.Text = "";
                            txtFrom_Date.Focus();
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


                                txtArrival_Flight.Focus();
                                UpdatePanel_TourDetails.Update();
                            }
                        }

                        if (txtFrom_Date.Text != "")
                        {
                            if (DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds_day.Tables[0].Rows[0]["DATE"].ToString(), "dd/MM/yyyy", null) || DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds_day.Tables[0].Rows[0]["DATE"].ToString(), "dd/MM/yyyy", null))
                            {
                                string msg = "Booking date is allowed before" + " " + ds_day.Tables[0].Rows[0]["DATE"].ToString() + ".";
                                Master.DisplayMessage(msg, "successMessage", 5000);
                                txtFrom_Date.Text = "";
                                txtTo_Date.Text = "";
                                txtFrom_Date.Focus();
                                UpdatePanel_TourDetails.Update();
                            }
                        }

                        if (txtFrom_Date.Text != "")
                        {
                            DataSet ds_ArrivalDate = objBookingFitStoreProcedure.fetch_bookig_days("FETCH_BOOKING_START_DATE");
                            if (ds_ArrivalDate.Tables[0].Rows.Count != 0)
                            {
                                if (DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ds_ArrivalDate.Tables[0].Rows[0]["BOOKING_START_DATE"].ToString(), "dd/MM/yyyy", null))
                                {
                                    string msg = "Booking is allowed after" + " " + ds_ArrivalDate.Tables[0].Rows[0]["BOOKING_START_DATE"].ToString() + ".";
                                    Master.DisplayMessage(msg, "successMessage", 5000);
                                    txtFrom_Date.Text = "";
                                    txtTo_Date.Text = "";
                                    txtFrom_Date.Focus();
                                    UpdatePanel_TourDetails.Update();
                                }
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


                    txtArrival_Flight.Focus();
                    UpdatePanel_TourDetails.Update();
                }


            }
            catch
            {
                Master.DisplayMessage("Date is not in correct format", "successMessage", 3000);
                txtFrom_Date.Text = "";
                txtFrom_Date.Focus();
            }

            finally
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/FIT/MyBooking.aspx");
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

        protected void package_validation(int package_id)
        {
            DataSet ds = objBookingFitStoreProcedure.fetch_packge_close_Date("FIT_PACKAGE_DATE", package_id);

            DataSet ds1 = objBookingFitStoreProcedure.fetchComboData("FETCH_BLOCK_DATES");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                for (int night = 0; night < int.Parse(txtNo_OfNights.Text) + 1; night++)
                {
                    DateTime dat = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                    dat = dat.AddDays(night);
                    string date = dat.ToString("dd/MM/yyyy");

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if ((DateTime.ParseExact(date, "dd/MM/yyyy", null) >= DateTime.ParseExact(ds1.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null)) && (DateTime.ParseExact(date, "dd/MM/yyyy", null) <= DateTime.ParseExact(ds1.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null)))
                        {
                            flag_pack_date = false;
                            ViewState["error_msg"] = "All Packages is block between" + " " + ds1.Tables[0].Rows[i]["FROM_DATE"].ToString() + " " + "TO" + " " + ds1.Tables[0].Rows[i]["TO_DATE"].ToString() + " " + "Dates" + "." + "E-mail Administrator to book this package.";
                            break;
                        }


                    }
                }
            }


            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int night = 0; night < int.Parse(txtNo_OfNights.Text) + 1; night++)
                {
                    string txtperiod_stay_from;
                    DateTime dat = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
                    dat = dat.AddDays(night);

                    dat.ToString("MM-dd-yyyy");
                    string source1 = dat.ToString();
                    string str11 = source1;
                    string[] w1 = str11.Split('/');

                    string t12 = w1[2];
                    string[] t11 = t12.Split(' ');

                    if (w1[1] == "1" || w1[1] == "2" || w1[1] == "3" || w1[1] == "4" || w1[1] == "5" || w1[1] == "6" || w1[1] == "7" || w1[1] == "8" || w1[1] == "9")
                    {
                        if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                        {
                            txtperiod_stay_from = "0" + w1[1] + "/" + "0" + w1[0] + "/" + t11[0];

                        }
                        else
                        {
                            txtperiod_stay_from = "0" + w1[1] + "/" + w1[0] + "/" + t11[0];

                        }
                    }
                    else
                    {
                        if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                        {
                            txtperiod_stay_from = w1[1] + "/" + "0" + w1[0] + "/" + t11[0];

                        }
                        else
                        {
                            txtperiod_stay_from = w1[1] + "/" + w1[0] + "/" + t11[0];

                        }
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if ((DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null) >= DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null) <= DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null)))
                        {
                            flag_pack_date = false;
                            ViewState["error_msg"] = "This Package is not available between" + " " + ds.Tables[0].Rows[i]["FROM_DATE"].ToString() + " " + "TO" + " " + ds.Tables[0].Rows[i]["TO_DATE"].ToString() + " " + "Dates";
                            break;
                        }
                    }
                    if (flag_pack_date == false)
                    {
                        break;
                    }
                }
            }

        }

        protected void clientNameChange()
        {


            DataSet ds = objfitquote.FetchForFitBooking("FETCH_DATA_FOR_BOOKING_FIT_USING_TOURID", Request.QueryString["TOURID"].ToString());
            DataTable dt1 = ds.Tables[0];
            if (txtClientname.Text != dt1.Rows[0]["CLIENT_NAME"].ToString())
            {
                nameChangeFlag = true;
            }
            if (txtClientlastname.Text != dt1.Rows[0]["CLIENT_SURNAME"].ToString())
            {
                nameChangeFlag = true;
            }
            if (drpTitle.Text != dt1.Rows[0]["CLIENT_TITLE"].ToString())
            {
                nameChangeFlag = true;
            }

            if (nameChangeFlag == true && (DropDownList1.Text == "Booked" || DropDownList1.Text == "To Be Reconfirmed" || DropDownList1.Text == "Reconfirmed"))
            {
                objfitquote.UpdateAgentName(Request.QueryString["TOURID"].ToString(), txtClientname.Text, txtClientlastname.Text, drpTitle.Text, txtTourname.Text);
                clientNameChangeMail();
            }
        }

        protected void clientNameChangeMail()
        {
            string hotelsupplier_id;
            string supplier_id;
            string supplier_email;
            string pessanger_name = "";

            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[20]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataTable dtemail = objHotelStoreProcedure.fetch_backoffice_for_book(Request.QueryString["QUOTEID"].ToString());

            string bcc = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            string EmailQoute_id = Request.QueryString["QUOTEID"].ToString();
            DataTable dt_supplier_id = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            DataTable dt_agent = new DataTable();

            SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
            agent_comm.CommandType = CommandType.StoredProcedure;
            agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
            SqlDataReader agent_rdr = agent_comm.ExecuteReader();
            dt_agent.Load(agent_rdr);

            SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
            SqlDataReader rdr = comm.ExecuteReader();
            dt_supplier_id.Load(rdr);

            SqlCommand cmd = new SqlCommand("FETCH_BOOK_EMAIL_ID_ON_QUOTE_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Load(dr);


            String cc = "";

            int count = 0;

            for (int i_supp = 0; i_supp < dt_supplier_id.Rows.Count; i_supp++)
            {
                count = i_supp + 1;
                supplier_id = dt_supplier_id.Rows[i_supp]["SUPPLIER_ID"].ToString();
                DataTable dt_hotelEmails = new DataTable();

                SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                email.CommandType = CommandType.StoredProcedure;
                email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                SqlDataReader rdremail = email.ExecuteReader();
                dt_hotelEmails.Load(rdremail);

                if (dt_hotelEmails.Rows.Count != 0)
                {
                    hotelsupplier_id = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


                    if (hotelsupplier_id == supplier_id)
                    {
                        DataTable dt_hotelDetails = new DataTable();

                        SqlCommand details = new SqlCommand("FETCH_HOTEL_DETAILS_FOR_MAIL", conn);
                        details.CommandType = CommandType.StoredProcedure;
                        details.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(EmailQoute_id);
                        details.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int).Value = int.Parse(hotelsupplier_id);
                        SqlDataReader rdrdetails = details.ExecuteReader();

                        dt_hotelDetails.Load(rdrdetails);

                        pessanger_name = dt_hotelDetails.Rows[0]["CLIENT_NAME"].ToString();
                        
                    }
                    if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
                    {


                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                if (bcc == "1")
                                {
                                    fromemail = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    fromemail = "reservation1@travelzunlimited.com";
                                }
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = supplier_email;
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
                                toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                if (bcc == "1")
                                {
                                    toemail1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    toemail1 = "reservation1@travelzunlimited.com";
                                }
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                            {
                                toemail1 = supplier_email;
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                if (bcc == "1")
                                {
                                    cc = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    cc = "reservation1@travelzunlimited.com";
                                }
                            }


                            else
                            {
                                cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                            {
                                if (bcc == "1")
                                {
                                    bcc1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    bcc1 = "reservation1@travelzunlimited.com";
                                }
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                bcc = supplier_email;
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }
                        string body = "";

                        
                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                      

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", EmailQoute_id);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENT_NAME%&gt;", pessanger_name);


                        body = strEmailTemplate;
                        try
                        {
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
                            subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                         
                            subjct = subjct.Replace("<%QUOTEID%>", EmailQoute_id);


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

                            if (count == 1)
                            {
                                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(EmailQoute_id), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                            }
                            if (count != 0)
                            {
                                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(EmailQoute_id), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
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
            catch(Exception ex)
            { 
                
            }
            finally
            {
                UpdatePanel_TourDetails.Update();
            }
        }

        public int noofPaxValidation()
        {
            int noOfpax = 0;

            if (txtNo_Adult.Text != "")
            {
                noOfpax=int.Parse(txtNo_Adult.Text);
            }
                
                if (txtNo_CNB.Text != "")
                {
                    noOfpax = noOfpax + int.Parse(txtNo_CNB.Text);
                }

                    if (txtNo_CWB.Text != "")
                    {
                        noOfpax = noOfpax + int.Parse(txtNo_CWB.Text);
                    }
            return noOfpax ;
        }

        public int noOfRoomsValidation()
        {

            int noOfRooms = 0;

            if (txtNoroomSingle.Text != "")
            {
                noOfRooms = int.Parse(txtNoroomSingle.Text);
            }

            if (txtNoroomDouble.Text != "")
            {
                noOfRooms = noOfRooms + int.Parse(txtNoroomDouble.Text);
            }

            if (txtNoroomTriple.Text != "")
            {
                noOfRooms = noOfRooms + int.Parse(txtNoroomTriple.Text);
            }

            return noOfRooms;

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

        protected bool LandingDateValidation()
        {
            DateTime FromTime = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);
            DateTime landtimeTime = DateTime.ParseExact(txtlanddate.Text, "dd/MM/yyyy", null);
            DateTime dateWeekBeforeWithTime = FromTime.AddDays(1);
            if (landtimeTime == FromTime || landtimeTime == dateWeekBeforeWithTime)
            {
                return true;
            }
            else
            {
                ViewState["LandingMessage"] = "Arrival Flight Date should be either From Date or 1 day after From Date";
                return false;

            }
        }

        protected bool FlyingDateValidation()
        {
            DateTime ToTime = DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null);
            DateTime FlyTime = DateTime.ParseExact(txtflydate.Text, "dd/MM/yyyy", null);
            DateTime dateWeekBeforeFlyTime = ToTime.AddDays(1);
            if (FlyTime == ToTime || FlyTime == dateWeekBeforeFlyTime)
            {
                return true;
            }
            else
            {
                ViewState["LandingMessage"] = "Departure Flight Date should be either To Date or 1 day after To Date";
                return false;
            }
        }
       

    }

}