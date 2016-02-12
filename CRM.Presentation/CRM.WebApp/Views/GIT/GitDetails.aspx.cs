using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Core.Utility;


namespace CRM.WebApp.Views.GIT
{
    public partial class GitDetails : System.Web.UI.Page
    {
        string viewstate;

        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();


        GitDetail objGitDetail = new GitDetail();
        InsertGitDetails objInsertGitDetails = new InsertGitDetails();
        EditUpdateGITInformation objEditUpdateGITInformation = new EditUpdateGITInformation();
        BookSp objBookSp = new BookSp();
        TobeReconform objTobereconform = new TobeReconform();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Check Page Authorization

            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();


        }

        #region VARIABLES
        bool f_rooms = true;
        String cityName;
        DataSet dsallcomb;
        DataSet dsallsupmails;
        DataSet dtAllBookedHotels;
        string currencyName = "THB";
        string tourId;
        string CompId;
        string DeptId;
        string RoleId;
        string startdate;
        string enddate;
        bool ischeckindateCheckoutDatecurrect = true;

        bool MealDateFlag = true;
        bool GalaDinnerFlag = true;
        bool Conferenceflag = true;

        bool hotelDateFlag = true;
        bool timeforall = true;
        bool sameHotelFlag = true;

        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();
                    fillDeatils();
                    fillDetailsEditMode();

                    startdate = Session["FromDate"].ToString();
                    enddate = Session["ToDate"].ToString();

                    DataSet dsId = objEditUpdateGITInformation.GetTransferCartId(int.Parse(Request.QueryString["TOURID"].ToString()));

                    if (dsId.Tables[0].Rows.Count != 0)
                    {
                        RadioButton rbtnTPselect = (RadioButton)GridTrasport.Rows[0].FindControl("rbtnTPselect");
                        rbtnTPselect.Checked = true;
                    }
                    else
                    {
                        RadioButton rbtnTPselect = (RadioButton)GridTrasport.Rows[0].FindControl("rbtnTPselect");
                        rbtnTPselect.Visible = false;


                        Button btnTrasportRate = (Button)GridTrasport.Rows[0].FindControl("btnTrasportRate");
                        btnTrasportRate.Visible = false;
                    }


                    if (Session["OrderStatus"].ToString() == "Quoted")
                    {
                        btnQuoteRequest.Visible = true;
                        btnGenerateQuote.Visible = false;
                        btnBook.Visible = true;
                        btnconformPaymentdate.Visible = false;
                        btnDownloadQuote.Visible = true;
                        btnSendQuote.Visible = true;
                        btnreconform.Visible = false;
                        btnback.Visible = true;
                        btnSaveRooms.Visible = false;
                    }
                    else if (Session["OrderStatus"].ToString() == "Booked")
                    {
                        btnQuoteRequest.Visible = true;
                        btnGenerateQuote.Visible = false;
                        btnBook.Visible = false;
                        btnconformPaymentdate.Visible = true;
                        btnDownloadQuote.Visible = true;
                        btnSendQuote.Visible = true;
                        btnreconform.Visible = false;
                        btnback.Visible = true;
                        btnSaveRooms.Visible = false;
                    }
                    else if (Session["OrderStatus"].ToString() == "In Process")
                    {
                        btnQuoteRequest.Visible = true;
                        btnGenerateQuote.Visible = false;
                        btnBook.Visible = false;
                        btnconformPaymentdate.Visible = false;
                        btnDownloadQuote.Visible = false;
                        btnSendQuote.Visible = false;
                        btnreconform.Visible = false;
                        btnback.Visible = true;
                        btnSaveRooms.Visible = false;
                    }
                    else if (Session["OrderStatus"].ToString() == "Request for Quote")
                    {
                        btnQuoteRequest.Visible = true;
                        btnGenerateQuote.Visible = true;
                        btnBook.Visible = false;
                        btnconformPaymentdate.Visible = false;
                        btnDownloadQuote.Visible = false;
                        btnSendQuote.Visible = false;
                        btnreconform.Visible = false;
                        btnback.Visible = true;
                        btnSaveRooms.Visible = false;
                    }

                    else if (Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                    {
                        btnQuoteRequest.Visible = true;
                        btnGenerateQuote.Visible = false;
                        btnBook.Visible = false;
                        btnconformPaymentdate.Visible = false;
                        btnDownloadQuote.Visible = true;
                        btnSendQuote.Visible = false;
                        btnreconform.Visible = true;
                        btnback.Visible = true;
                        btnSaveRooms.Visible = true;

                        
                    }
                    TransportPackage();
                }
                else
                {
                    startdate = Session["FromDate"].ToString();
                    enddate = Session["ToDate"].ToString();

                    fillDeatils();
                    TransportPackage();
                }
                if (RoleId != "18")
                {
                    lblExRate.Visible = false;
                    lblmarginAmount.Visible = false;
                    txtExRate.Visible = false;
                    txtMarginAmount.Visible = false;

                    btnGenerateQuote.Visible = false;


                    btnSendQuote.Visible = false;
                }
            }
            else
            {
                startdate = Session["FromDate"].ToString();
                enddate = Session["ToDate"].ToString();
                if (RoleId != "18")
                {
                    lblExRate.Visible = false;
                    lblmarginAmount.Visible = false;
                    txtExRate.Visible = false;
                    txtMarginAmount.Visible = false;

                    btnGenerateQuote.Visible = false;
                    
                    btnDownloadQuote.Visible = false;
                    btnSendQuote.Visible = false;
                }
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();
                    startdate = Session["FromDate"].ToString();
                    enddate = Session["ToDate"].ToString();
                }
            }
        }

        #region FILL DETAILS FIRST TIME

        protected void fillDeatils()
        {
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());

            for (int i = 0; i < dtCities.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel1.Visible = true;
                    fillHotels(lblHotel1, GridHotel1, upHotel1, city);

                    divMeals1.Visible = true;
                    lblMealCity1.Text = city;
                    addMeals(gridMeal1, city, upMeal1);

                    divConf1.Visible = true;
                    lblConfCity1.Text = city;
                    addConference(gridConf1, city, upConf1);

                    divGala1.Visible = true;
                    lblGalaCity1.Text = city;
                    addGalaDinner(gridGala1, city, upGala1);

                    divSite1.Visible = true;
                    lblSiteCity1.Text = city;
                    addSite(gridSite1, upSite1, city);

                }

                if (i == 1)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel2.Visible = true;
                    fillHotels(lblHotel2, GridHotel2, upHotel2, city);

                    divMeals2.Visible = true;
                    lblMealCity2.Text = city;
                    addMeals(gridMeal2, city, upMeal2);

                    divConf2.Visible = true;
                    lblConfCity2.Text = city;
                    addConference(gridConf2, city, upConf2);

                    divGala2.Visible = true;
                    lblGalaCity2.Text = city;
                    addGalaDinner(gridGala2, city, upGala2);

                    divSite2.Visible = true;
                    lblSiteCity2.Text = city;
                    addSite(gridSite2, upSite2, city);
                }

                if (i == 2)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel3.Visible = true;
                    fillHotels(lblHotel3, GridHotel3, upHotel3, city);

                    divMeals3.Visible = true;
                    lblMealCity3.Text = city;
                    addMeals(gridMeal3, city, upMeal3);

                    divConf3.Visible = true;
                    lblConfCity3.Text = city;
                    addConference(gridConf3, city, upConf3);

                    divGala3.Visible = true;
                    lblGalaCity3.Text = city;
                    addGalaDinner(gridGala3, city, upGala3);

                }

                if (i == 3)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel4.Visible = true;
                    fillHotels(lblHotel4, GridHotel4, upHotel4, city);

                    divMeals4.Visible = true;
                    lblMealCity4.Text = city;
                    addMeals(gridMeal4, city, upMeal4);

                    divConf4.Visible = true;
                    lblConfCity4.Text = city;
                    addConference(gridConf4, city, upConf4);

                    divGala4.Visible = true;
                    lblGalaCity4.Text = city;
                    addGalaDinner(gridGala4, city, upGala4);

                }
                if (i == 4)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel5.Visible = true;
                    fillHotels(lblHotel5, GridHotel5, upHotel5, city);

                    divMeals5.Visible = true;
                    lblMealCity5.Text = city;
                    addMeals(gridMeal5, city, upMeal5);

                    divConf5.Visible = true;
                    lblConfCity5.Text = city;
                    addConference(gridConf5, city, upConf5);

                    divGala5.Visible = true;
                    lblGalaCity5.Text = city;
                    addGalaDinner(gridGala5, city, upGala5);

                }
                if (i == 5)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel6.Visible = true;
                    fillHotels(lblHotel6, GridHotel6, upHotel6, city);

                    divMeals6.Visible = true;
                    lblMealCity6.Text = city;
                    addMeals(gridMeal6, city, upMeal6);

                    divConf6.Visible = true;
                    lblConfCity6.Text = city;
                    addConference(gridConf6, city, upConf6);

                    divGala6.Visible = true;
                    lblGalaCity6.Text = city;
                    addGalaDinner(gridGala6, city, upGala6);

                }
                if (i == 6)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel7.Visible = true;
                    fillHotels(lblHotel7, GridHotel7, upHotel7, city);

                    divMeals7.Visible = true;
                    lblMealCity7.Text = city;
                    addMeals(gridMeal7, city, upMeal7);

                    divConf7.Visible = true;
                    lblConfCity7.Text = city;
                    addConference(gridConf7, city, upConf7);

                    divGala7.Visible = true;
                    lblGalaCity7.Text = city;
                    addGalaDinner(gridGala7, city, upGala7);

                }

                if (i == 7)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel8.Visible = true;
                    fillHotels(lblHotel8, GridHotel8, upHotel8, city);

                    divMeals8.Visible = true;
                    lblMealCity8.Text = city;
                    addMeals(gridMeal8, city, upMeal8);

                    divConf8.Visible = true;
                    lblConfCity8.Text = city;
                    addConference(gridConf8, city, upConf8);

                    divGala8.Visible = true;
                    lblGalaCity8.Text = city;
                    addGalaDinner(gridGala8, city, upGala8);

                }
                if (i == 8)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel9.Visible = true;
                    fillHotels(lblHotel9, GridHotel9, upHotel9, city);

                    divMeals9.Visible = true;
                    lblMealCity9.Text = city;
                    addMeals(gridMeal9, city, upMeal9);

                    divConf9.Visible = true;
                    lblConfCity9.Text = city;
                    addConference(gridConf9, city, upConf9);

                    divGala9.Visible = true;
                    lblGalaCity9.Text = city;
                    addGalaDinner(gridGala9, city, upGala9);

                }

                if (i == 9)
                {

                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    divHotel10.Visible = true;
                    fillHotels(lblHotel10, GridHotel10, upHotel10, city);

                    divMeals10.Visible = true;
                    lblMealCity10.Text = city;
                    addMeals(gridMeal10, city, upMeal10);

                    divConf10.Visible = true;
                    lblConfCity10.Text = city;
                    addConference(gridConf10, city, upConf10);

                    divGala10.Visible = true;
                    lblGalaCity10.Text = city;
                    addGalaDinner(gridGala10, city, upGala10);

                }
            }

            addServices(gridAddServices, upAddServices);



            DataSet dsTrasport = objGitDetail.fetchTransportPackage("FETCH_TRANSPORT_PACKAGE_FOR_GIT", int.Parse(Session["packgeId"].ToString()));
            GridTrasport.DataSource = dsTrasport;
            GridTrasport.DataBind();

            if (dsTrasport.Tables[0].Rows.Count != 0)
            {
                Session["TransferPackgeId"] = dsTrasport.Tables[0].Rows[0]["GIT_TRANSFER_PACKGE_ID"].ToString();
            }

        }

        #endregion

        #region FILL DETAILS WHILE EDIT MODE

        protected void fillDetailsEditMode()
        {
            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());

            for (int i = 0; i < dtCities.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel1, city, upHotel1);

                    fillResturantsEditMode(gridMeal1, city, upMeal1);

                    fillConferenceEditMode(gridConf1, city, upConf1);

                    fillGalaDinnerEditMode(gridGala1, city, upGala1);

                    fillSiteSeeingEditMode(gridSite1, city, upSite1);

                }

                if (i == 1)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel2, city, upHotel2);

                    fillResturantsEditMode(gridMeal2, city, upMeal2);

                    fillConferenceEditMode(gridConf2, city, upConf2);

                    fillGalaDinnerEditMode(gridGala2, city, upGala2);

                    fillSiteSeeingEditMode(gridSite2, city, upSite2);

                }

                if (i == 2)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel3, city, upHotel3);

                    fillResturantsEditMode(gridMeal3, city, upMeal3);

                    fillConferenceEditMode(gridConf3, city, upConf3);

                    fillGalaDinnerEditMode(gridGala3, city, upGala3);

                    fillSiteSeeingEditMode(gridSite3, city, upSite3);

                }

                if (i == 3)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel4, city, upHotel4);

                    fillResturantsEditMode(gridMeal4, city, upMeal4);

                    fillConferenceEditMode(gridConf4, city, upConf4);

                    fillGalaDinnerEditMode(gridGala4, city, upGala4);

                    fillSiteSeeingEditMode(gridSite4, city, upSite4);

                }

                if (i == 4)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel5, city, upHotel5);

                    fillResturantsEditMode(gridMeal5, city, upMeal5);

                    fillConferenceEditMode(gridConf5, city, upConf5);

                    fillGalaDinnerEditMode(gridGala5, city, upGala5);

                    fillSiteSeeingEditMode(gridSite5, city, upSite5);

                }

                if (i == 5)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel6, city, upHotel6);

                    fillResturantsEditMode(gridMeal6, city, upMeal6);

                    fillConferenceEditMode(gridConf6, city, upConf6);

                    fillGalaDinnerEditMode(gridGala6, city, upGala6);

                    fillSiteSeeingEditMode(gridSite6, city, upSite6);

                }

                if (i == 6)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel7, city, upHotel7);

                    fillResturantsEditMode(gridMeal7, city, upMeal7);

                    fillConferenceEditMode(gridConf7, city, upConf7);

                    fillGalaDinnerEditMode(gridGala7, city, upGala7);

                    fillSiteSeeingEditMode(gridSite7, city, upSite7);

                }

                if (i == 7)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel8, city, upHotel8);

                    fillResturantsEditMode(gridMeal8, city, upMeal8);

                    fillConferenceEditMode(gridConf8, city, upConf8);

                    fillGalaDinnerEditMode(gridGala8, city, upGala8);

                    fillSiteSeeingEditMode(gridSite8, city, upSite8);

                }

                if (i == 8)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel9, city, upHotel9);

                    fillResturantsEditMode(gridMeal9, city, upMeal9);

                    fillConferenceEditMode(gridConf9, city, upConf9);

                    fillGalaDinnerEditMode(gridGala9, city, upGala9);

                    fillSiteSeeingEditMode(gridSite9, city, upSite9);

                }

                if (i == 9)
                {
                    String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                    fillHotelsEditMode(GridHotel10, city, upHotel10);

                    fillResturantsEditMode(gridMeal10, city, upMeal10);

                    fillConferenceEditMode(gridConf10, city, upConf10);

                    fillGalaDinnerEditMode(gridGala10, city, upGala10);

                    fillSiteSeeingEditMode(gridSite10, city, upSite10);

                }
            }
            FillAdditionalEditMode(gridAddServices, upAddServices);
        }

        #endregion

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

                    Button btnRate = (Button)e.Row.FindControl("btnTrasportRate");
                    btnRate.Visible = false;

                }
                else
                {
                    title = strval;
                    viewstate = title;

                    Label l2 = (Label)e.Row.FindControl("lblTPsrno");
                    l2.Visible = true;

                    RadioButton btn = (RadioButton)e.Row.FindControl("rbtnTPselect");
                    btn.Visible = true;

                    if (int.Parse(RoleId) == 18 && Session["OrderStatus"].ToString() == "In Process")
                    {
                        Button btnRate = (Button)e.Row.FindControl("btnTrasportRate");
                        btnRate.Visible = false;
                    }

                    if (int.Parse(RoleId) != 18)
                    {
                        Button btnRate = (Button)e.Row.FindControl("btnTrasportRate");
                        btnRate.Visible = false;
                    }
                    if (Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                    {
                        Button btnTime = (Button)e.Row.FindControl("btnTrasportTime");
                        btnTime.Visible = true;
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
            r.Items.Insert(0, new ListItem("", ""));

        }

        #region HOTELS

        protected void AddHotels(GridView gv, string CityName, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objGitDetail.fetchComboDataforHotel("FETCH_HOTEL_NAME_FOR_GIT_CITY_WISE", CityName);

                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    TextBox txtNights = (TextBox)item.FindControl("txtNights");
                    CheckBox chk = (CheckBox)item.FindControl("chkAddToCart");
                    TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                    TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                    TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("HotelName");
                        dt.Columns.Add("RoomType");
                        dt.Columns.Add("CheckInDate");
                        dt.Columns.Add("CheckOutDate");
                        dt.Columns.Add("Nights");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("SingleRoom");
                        dt.Columns.Add("DoubleRoom");
                        dt.Columns.Add("TripleRoom");

                    }

                    DataRow dr = dt.NewRow();
                    dr["HotelName"] = drpHotelName.Text;
                    dr["RoomType"] = drpRoomType.Text;
                    dr["CheckInDate"] = txtCheckInDate.Text;
                    dr["CheckOutDate"] = txtCheckOutDate.Text;
                    dr["Nights"] = txtNights.Text;
                    dr["SingleRoom"] = txtSingleRoom.Text;
                    dr["DoubleRoom"] = txtDoubleRoom.Text;
                    dr["TripleRoom"] = txtTripleRoom.Text;

                    if (chk.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("HotelName");
                        dt.Columns.Add("RoomType");
                        dt.Columns.Add("CheckInDate");
                        dt.Columns.Add("CheckOutDate");
                        dt.Columns.Add("Nights");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("SingleRoom");
                        dt.Columns.Add("DoubleRoom");
                        dt.Columns.Add("TripleRoom");
                    }

                    DataRow dr = dt.NewRow();
                    dr["HotelName"] = "";
                    dr["RoomType"] = "";
                    dr["CheckInDate"] = "";
                    dr["CheckOutDate"] = "";
                    dr["Nights"] = "";
                    dr["AddCart"] = "";
                    dr["SingleRoom"] = "";
                    dr["DoubleRoom"] = "";
                    dr["TripleRoom"] = "";
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
                    DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    TextBox txtNights = (TextBox)item.FindControl("txtNights");
                    CheckBox chk = (CheckBox)item.FindControl("chkAddToCart");
                    TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                    TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                    TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            binddropdownlist(drpHotelName, ds);
                            drpHotelName.Text = dt.Rows[itm]["HotelName"].ToString();

                            if (drpHotelName.Text != "")
                            {
                                DataSet ds1 = objGitDetail.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_GIT_HOTEL_WISE", drpHotelName.Text, CityName);

                                binddropdownlist(drpRoomType, ds1);

                                drpRoomType.Text = dt.Rows[itm]["RoomType"].ToString();
                            }
                            txtCheckInDate.Text = dt.Rows[itm]["CheckInDate"].ToString();
                            txtCheckOutDate.Text = dt.Rows[itm]["CheckOutDate"].ToString();
                            txtNights.Text = dt.Rows[itm]["Nights"].ToString();

                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chk.Checked = true;
                            }
                            else
                            {
                                chk.Checked = false;
                            }
                            txtSingleRoom.Text = dt.Rows[itm]["SingleRoom"].ToString();
                            txtDoubleRoom.Text = dt.Rows[itm]["DoubleRoom"].ToString();
                            txtTripleRoom.Text = dt.Rows[itm]["TripleRoom"].ToString();
                        }
                    }
                }

                if (RoleId != "18")
                {
                    Button RateButton = new Button();


                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnHotelRate");
                        RateButton.Visible = false;

                    }

                }

                if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                {
                    Button RateButton = new Button();


                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnHotelRate");
                        RateButton.Visible = false;

                    }
                }
                if (RoleId == "18" && (Session["OrderStatus"].ToString() == "Booked" || Session["OrderStatus"].ToString() == "To Be Reconfirmed"))
                {
                    if (ViewState["RadioBtnVsible"] != null)
                    {
                        RadioButton ConfirmrdoButton = new RadioButton();

                        for (int i = 0; i < gv.Rows.Count - 1; i++)
                        {
                            if (i == int.Parse(ViewState["RadioBtnVsible"].ToString()))
                            {
                                ConfirmrdoButton = (RadioButton)gv.Rows[i].FindControl("rdoConfirm");
                                ConfirmrdoButton.Visible = true;
                                ViewState["RadioBtnVsible"] = null;
                                break;
                            }

                        }
                    }
                }
                if (Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                {
                    if (ViewState["RoomtxtVsible"] != null)
                    {
                        TextBox txtSingleRoom = new TextBox();
                        TextBox txtDoubleRoom = new TextBox();
                        TextBox txtTripleRoom = new TextBox();

                        for (int i = 0; i < gv.Rows.Count - 1; i++)
                        {
                            if (i == int.Parse(ViewState["RoomtxtVsible"].ToString()))
                            {
                                txtSingleRoom = (TextBox)gv.Rows[i].FindControl("txtSingleRoom");
                                txtSingleRoom.Visible = true;
                                txtDoubleRoom = (TextBox)gv.Rows[i].FindControl("txtDoubleRoom");
                                txtDoubleRoom.Visible = true;
                                txtTripleRoom = (TextBox)gv.Rows[i].FindControl("txtTripleRoom");
                                txtTripleRoom.Visible = true;

                                ViewState["RoomtxtVsible"] = null;
                                break;
                            }
                        }
                        if (gv.HeaderRow != null)
                        {
                            Label lblSingleRoom = (Label)gv.HeaderRow.FindControl("lblSingleRoom");
                            lblSingleRoom.Visible = true;
                            Label lblDoubleRoom = (Label)gv.HeaderRow.FindControl("lblDoubleRoom");
                            lblDoubleRoom.Visible = true;
                            Label lblTripleRoom = (Label)gv.HeaderRow.FindControl("lblTripleRoom");
                            lblTripleRoom.Visible = true;
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

        protected void roomTypeLoad(GridView gv, String city, int Index)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                        DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");

                        DataSet ds = objGitDetail.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_GIT_HOTEL_WISE", drpHotelName.Text, city);
                        binddropdownlist(drpRoomType, ds);

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

        protected void insertHotelDetails(GridView gv, string City_Name, String currency_Name)
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {


                        DropDownList drpHotel = (DropDownList)item.FindControl("drpHotelName");
                        DropDownList drpRoomtype = (DropDownList)item.FindControl("drpRoomType");

                        TextBox txtFromdate = (TextBox)item.FindControl("txtCheckInDate");
                        TextBox txtTodate = (TextBox)item.FindControl("txtCheckOutDate");
                        TextBox txtNights = (TextBox)item.FindControl("txtNights");

                        CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkAddToCart");

                        bool hotelFlag = false;
                        if (chkPackgeFlag.Checked)
                        {
                            hotelFlag = true;
                        }
                        objInsertGitDetails.insertHotelsDetails(0, drpHotel.Text, drpRoomtype.Text, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), txtFromdate.Text, txtTodate.Text, txtNights.Text, City_Name, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), currency_Name, hotelFlag, int.Parse(Session["AgentId"].ToString()));
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

        protected void fillHotelsEditMode(GridView gv, String City, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objEditUpdateGITInformation.GetHotelName(int.Parse(tourId), City);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {

                            DropDownList drpHotel = (DropDownList)item.FindControl("drpHotelName");

                            DropDownList drpRoomtype = (DropDownList)item.FindControl("drpRoomType");

                            TextBox txtFromdate = (TextBox)item.FindControl("txtCheckInDate");
                            TextBox txtTodate = (TextBox)item.FindControl("txtCheckOutDate");
                            TextBox txtNights = (TextBox)item.FindControl("txtNights");

                            CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkAddToCart");

                            TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                            TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                            TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");

                            drpHotel.Text = ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            DataSet ds1 = objGitDetail.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_GIT_HOTEL_WISE", drpHotel.Text, City);
                            binddropdownlist(drpRoomtype, ds1);

                            drpRoomtype.Text = ds.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();

                            txtFromdate.Text = ds.Tables[0].Rows[j]["FROM_DATE"].ToString();
                            txtTodate.Text = ds.Tables[0].Rows[j]["TO_DATE"].ToString();
                            txtNights.Text = ds.Tables[0].Rows[j]["NO_OF_NIGHTS"].ToString();

                            if (ds.Tables[0].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                            {
                                chkPackgeFlag.Checked = true;
                            }

                            RadioButton rdoConfirm = (RadioButton)item.FindControl("rdoConfirm");

                            if (ds.Tables[0].Rows[j]["IS_BOOKED"].ToString() == "True" && (Session["OrderStatus"].ToString() == "Booked" || Session["OrderStatus"].ToString() == "To Be Reconfirmed"))
                            {
                                ViewState["RadioBtnVsible"] = j;
                                rdoConfirm.Visible = true;
                            }
                          
                            if (ds.Tables[0].Rows[j]["IS_BOOKED"].ToString() == "True" && Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                            {
                                ViewState["RoomtxtVsible"] = j;
                                txtSingleRoom.Visible = true;
                                txtDoubleRoom.Visible = true;
                                txtTripleRoom.Visible = true;

                                Label lblSingleRoom = (Label)gv.HeaderRow.FindControl("lblSingleRoom");
                                lblSingleRoom.Visible = true;
                                Label lblDoubleRoom = (Label)gv.HeaderRow.FindControl("lblDoubleRoom");
                                lblDoubleRoom.Visible = true;
                                Label lblTripleRoom = (Label)gv.HeaderRow.FindControl("lblTripleRoom");
                                lblTripleRoom.Visible = true;
                            }
                            txtSingleRoom.Text = ds.Tables[0].Rows[j]["NO_OF_SINGLE_ROOM"].ToString();
                            txtDoubleRoom.Text = ds.Tables[0].Rows[j]["NO_OF_DOUBLE_ROOM"].ToString();
                            txtTripleRoom.Text = ds.Tables[0].Rows[j]["NO_OF_TRIPLE_ROOM"].ToString();
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        AddHotels(gv, City, uppanel);
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
                ViewState["RadioBtnVsible"] = null;
                ViewState["RoomtxtVsible"] = null;
            }
        }

        #region HOTEL'S SELECTED INDEX CHANGED

        public void drpHotelName_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel1, lblHotel1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel1.Update();
            }

        }

        public void drpHotelName2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel2, lblHotel2.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel2.Update();
            }

        }

        public void drpHotelName3_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel3, lblHotel3.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel3.Update();
            }

        }

        public void drpHotelName4_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel4, lblHotel4.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel4.Update();
            }

        }

        public void drpHotelName5_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel5, lblHotel5.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel5.Update();
            }
        }

        public void drpHotelName6_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel6, lblHotel6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel6.Update();
            }
        }

        public void drpHotelName7_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel7, lblHotel7.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel7.Update();
            }
        }

        public void drpHotelName8_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel8, lblHotel8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel8.Update();
            }
        }

        public void drpHotelName9_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel9, lblHotel9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel9.Update();
            }
        }

        public void drpHotelName10_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel10, lblHotel10.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel10.Update();
            }
        }
        #endregion

        #region ALL ADD BUTTONS
        protected void btnAddHotel_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel1, lblHotel1.Text, upHotel1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel2_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel2, lblHotel2.Text, upHotel2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel3_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel3, lblHotel3.Text, upHotel3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel4_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel4, lblHotel4.Text, upHotel4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel5_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel5, lblHotel5.Text, upHotel5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel6_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel6, lblHotel6.Text, upHotel6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel7_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel7, lblHotel7.Text, upHotel7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel8_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel8, lblHotel8.Text, upHotel8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel9_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel9, lblHotel9.Text, upHotel9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnAddHotel10_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel10, lblHotel10.Text, upHotel10);
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

        #region RATE BUTTONS
        protected void btnHotelRate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel1, lblHotel1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate2_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel2, lblHotel2.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate3_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel3, lblHotel3.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate4_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel4, lblHotel4.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate5_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel5, lblHotel5.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate6_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel6, lblHotel6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel7, lblHotel7.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate8_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel8, lblHotel8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate9_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel9, lblHotel9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnHotelRate10_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                hotelRate(GridHotel10, lblHotel10.Text, repeaterItemIndex);
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

        #region HOTELS REMOVE BUTTONS



        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel1, rowID, lblHotel1.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel1.Update();
            }

        }

        protected void btnRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel2, rowID, lblHotel2.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel2.Update();
            }


        }

        protected void btnRemove3_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel3, rowID, lblHotel3.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel3.Update();
            }

        }

        protected void btnRemove4_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel4, rowID, lblHotel4.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel4.Update();
            }


        }

        protected void btnRemove5_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel5, rowID, lblHotel5.Text);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel5.Update();
            }


        }

        protected void btnRemove6_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel6, rowID, lblHotel6.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel6.Update();
            }


        }

        protected void btnRemove7_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel7, rowID, lblHotel7.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel7.Update();
            }


        }

        protected void btnRemove8_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel8, rowID, lblHotel8.Text);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel8.Update();
            }


        }

        protected void btnRemove9_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel9, rowID, lblHotel9.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel9.Update();
            }


        }

        protected void btnRemove10_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);

                removeRow(GridHotel10, rowID, lblHotel10.Text);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel10.Update();
            }


        }

        #endregion

        protected void removeRow(GridView gv, int rowIndex, String CityName)
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
                    DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    TextBox txtNights = (TextBox)item.FindControl("txtNights");
                    CheckBox chk = (CheckBox)item.FindControl("chkAddToCart");

                    TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                    TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                    TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");

                    RadioButton rdoConfirm = (RadioButton)item.FindControl("rdoConfirm");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("HotelName");
                        dt.Columns.Add("RoomType");
                        dt.Columns.Add("CheckInDate");
                        dt.Columns.Add("CheckOutDate");
                        dt.Columns.Add("Nights");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("SingleRoom");
                        dt.Columns.Add("DoubleRoom");
                        dt.Columns.Add("TripleRoom");
                    }

                    DataRow dr = dt.NewRow();
                    dr["HotelName"] = drpHotelName.Text;
                    dr["RoomType"] = drpRoomType.Text;
                    dr["CheckInDate"] = txtCheckInDate.Text;
                    dr["CheckOutDate"] = txtCheckOutDate.Text;
                    dr["Nights"] = txtNights.Text;

                    if (chk.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }
                    dr["SingleRoom"] = txtSingleRoom.Text;
                    dr["DoubleRoom"] = txtDoubleRoom.Text;
                    dr["TripleRoom"] = txtTripleRoom.Text;

                    dt.Rows.Add(dr);

                }

                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }

                    DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    TextBox txtNights = (TextBox)item.FindControl("txtNights");
                    CheckBox chk = (CheckBox)item.FindControl("chkAddToCart");
                    TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                    TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                    TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");

                    DataSet ds = objGitDetail.fetchComboDataforHotel("FETCH_HOTEL_NAME_FOR_GIT_CITY_WISE", CityName);

                    binddropdownlist(drpHotelName, ds);
                    drpHotelName.Text = dt.Rows[itm]["HotelName"].ToString();

                    if (drpHotelName.Text != "")
                    {
                        DataSet ds1 = objGitDetail.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_GIT_HOTEL_WISE", drpHotelName.Text, CityName);

                        binddropdownlist(drpRoomType, ds1);

                        drpRoomType.Text = dt.Rows[itm]["RoomType"].ToString();
                    }
                    txtCheckInDate.Text = dt.Rows[itm]["CheckInDate"].ToString();
                    txtCheckOutDate.Text = dt.Rows[itm]["CheckOutDate"].ToString();
                    txtNights.Text = dt.Rows[itm]["Nights"].ToString();

                    if (dt.Rows[itm]["AddCart"].ToString() == "True")
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                    txtSingleRoom.Text = dt.Rows[itm]["SingleRoom"].ToString();
                    txtDoubleRoom.Text = dt.Rows[itm]["DoubleRoom"].ToString();
                    txtTripleRoom.Text = dt.Rows[itm]["TripleRoom"].ToString();
                }
                if (Session["OrderStatus"].ToString() == "In Process")
                {
                    Button RateButton = new Button();


                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnHotelRate");
                        RateButton.Visible = false;

                    }
                }
                else if (Session["OrderStatus"].ToString() == "Booked")
                {
                    RadioButton ConfirmrdoButton = new RadioButton();

                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        ConfirmrdoButton = (RadioButton)gv.Rows[i].FindControl("rdoConfirm");
                        ConfirmrdoButton.Visible = true;

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

        protected void hotelRate(GridView gv, String City, int Itemindex)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Itemindex == item.DataItemIndex)
                    {
                        DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                        DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                        Session["HotelName"] = drpHotelName.Text;
                        Session["RoomType"] = drpRoomType.Text;
                        Session["City"] = City;
                        Response.Redirect("~/Views/GIT/GitHotelRate.aspx?TOURID=" + tourId);
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

        protected void fillHotels(Label lbl, GridView gv, UpdatePanel up, string city)
        {
            try
            {
                lbl.Text = city;
                AddHotels(gv, lbl.Text, up);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #region HOTEL ROOM TYPE DROP DOWN SELECTED INDEX CHANGED

        public void drpRoomtype_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel1, upHotel1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel2, upHotel2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype3_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel3, upHotel3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype4_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel4, upHotel4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype5_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {

                HotelValidation(GridHotel5, upHotel5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype6_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel6, upHotel6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype7_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel7, upHotel7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype8_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel8, upHotel8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype9_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel9, upHotel9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }
        public void drpRoomtype10_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                HotelValidation(GridHotel10, upHotel10);
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

        #region HOTEL CHECK IN DATE TEXTBOX TEXT CHANGED

        public void CheckInDate_SelectedIndexChanged(Object sender, EventArgs e)
        {

        }
        public void CheckInDate2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            checkindatecheckoutdatevalidation(GridHotel2);
        }
        public void CheckInDate3_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //  checkindatecheckoutdatevalidation(GridHotel3);
        }
        public void CheckInDate4_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //  checkindatecheckoutdatevalidation(GridHotel4);
        }
        public void CheckInDate5_SelectedIndexChanged(Object sender, EventArgs e)
        {
            // checkindatecheckoutdatevalidation(GridHotel5);
        }
        public void CheckInDate6_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //  checkindatecheckoutdatevalidation(GridHotel6);
        }
        public void CheckInDate7_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //  checkindatecheckoutdatevalidation(GridHotel7);
        }
        public void CheckInDate8_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //   checkindatecheckoutdatevalidation(GridHotel8);
        }
        public void CheckInDate9_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //   checkindatecheckoutdatevalidation(GridHotel9);
        }
        public void CheckInDate10_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //  checkindatecheckoutdatevalidation(GridHotel10);
        }

        #endregion

        #region HOTELS CHECK OUT DATE TEXT CHANGED

        protected void txtCheckOutDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel1);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel1, lblHotel1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel1.Update();
            }

        }
        protected void txtCheckOutDate2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel2);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel2, lblHotel2.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel2.Update();
            }
        }
        protected void txtCheckOutDate3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel3);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel3, lblHotel3.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel3.Update();
            }
        }
        protected void txtCheckOutDate4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel4);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel4, lblHotel4.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel4.Update();
            }
        }
        protected void txtCheckOutDate5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel5);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel5, lblHotel5.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel5.Update();
            }
        }
        protected void txtCheckOutDate6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel6);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel6, lblHotel6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel6.Update();
            }
        }
        protected void txtCheckOutDate7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel7);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel7, lblHotel7.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel7.Update();
            }
        }
        protected void txtCheckOutDate8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel8);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel8, lblHotel8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel8.Update();
            }
        }
        protected void txtCheckOutDate9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel9);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel9, lblHotel9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel9.Update();
            }

        }
        protected void txtCheckOutDate10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkindatecheckoutdatevalidation(GridHotel10);
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                FillNoOfNights(GridHotel10, lblHotel10.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel10.Update();
            }

        }

        #endregion

        protected void ReconfirmHotelCheckChanged(GridView gv, UpdatePanel up, object sender)
        {
            ViewState["Hotel_Name"] = null;
            ViewState["Room_Type"] = null;
            foreach (GridViewRow item in gv.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rdoConfirm");



                if (rb != sender)
                {
                    rb.Checked = false;
                }
                if (rb.Checked == true)
                {
                    DropDownList dhotel = (DropDownList)item.FindControl("drpHotelName");
                    ViewState["Hotel_Name"] = dhotel.Text;
                    DropDownList drtype = (DropDownList)item.FindControl("drpRoomType");
                    ViewState["Room_Type"] = drtype.Text;
                    btnConfirmHotel.Visible = true;
                }

            }
            pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
            up.Update();
            Updateconfirm.Update();
        }

        #region RADIO BUTTON FOR INSERT PAYMENT DATES
        protected void rdoConfirm1_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel1, upHotel1, sender);
        }

        protected void rdoConfirm2_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel2, upHotel2, sender);

        }

        protected void rdoConfirm3_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel3, upHotel3, sender);

        }

        protected void rdoConfirm4_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel4, upHotel4, sender);

        }

        protected void rdoConfirm5_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel5, upHotel5, sender);

        }

        protected void rdoConfirm6_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel6, upHotel6, sender);

        }

        protected void rdoConfirm7_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel7, upHotel7, sender);

        }

        protected void rdoConfirm8_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel8, upHotel8, sender);

        }

        protected void rdoConfirm9_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
            ReconfirmHotelCheckChanged(GridHotel9, upHotel9, sender);

        }

        protected void rdoConfirm10_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel1, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            ReconfirmHotelCheckChanged(GridHotel10, upHotel10, sender);

        }

        protected void allUnchecked(object sender)
        {
            UncheckAllotherOnCheckChanged(GridHotel1, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel2, upHotel2, sender);
            UncheckAllotherOnCheckChanged(GridHotel3, upHotel3, sender);
            UncheckAllotherOnCheckChanged(GridHotel4, upHotel4, sender);
            UncheckAllotherOnCheckChanged(GridHotel5, upHotel5, sender);
            UncheckAllotherOnCheckChanged(GridHotel6, upHotel6, sender);
            UncheckAllotherOnCheckChanged(GridHotel7, upHotel7, sender);
            UncheckAllotherOnCheckChanged(GridHotel8, upHotel8, sender);
            UncheckAllotherOnCheckChanged(GridHotel9, upHotel9, sender);
            UncheckAllotherOnCheckChanged(GridHotel10, upHotel10, sender);
        }
        #endregion


        #endregion

        #region RESTURANS MELAS

        // ADD BUTTON FUNCTION
        protected void addMeals(GridView gv, string CITY_NAME, UpdatePanel uppanel)
        {
            try
            {
                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objGitDetail.fetchResturants("GET_RESTRANTS_NAMES_FOR_GIT", CITY_NAME);

                foreach (GridViewRow item in gv.Rows)
                {

                    DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                    DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                    CheckBox chkMeal = (CheckBox)item.FindControl("chkMeal");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtVage = (TextBox)item.FindControl("txtVage");
                    TextBox txtNonVage = (TextBox)item.FindControl("txtNonVag");
                    TextBox txtJain = (TextBox)item.FindControl("txtJain");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("MealType");
                        dt.Columns.Add("Resturant");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("Time");
                        dt.Columns.Add("Veg");
                        dt.Columns.Add("NonVeg");
                        dt.Columns.Add("Jain");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = txtDate.Text;
                    dr["MealType"] = drpMealType.Text;
                    dr["Resturant"] = drpResturant.Text;
                    dr["ChildRate"] = txtChildRate.Text;
                    dr["AdultRate"] = txtAdultRate.Text;
                    dr["Time"] = txttime.SelectedDate;
                    dr["Veg"] = txtVage.Text;
                    dr["NonVeg"] = txtNonVage.Text;
                    dr["Jain"] = txtJain.Text;
                    if (chkMeal.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }

                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("MealType");
                        dt.Columns.Add("Resturant");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("Time");
                        dt.Columns.Add("Veg");
                        dt.Columns.Add("NonVeg");
                        dt.Columns.Add("Jain");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = "";
                    dr["MealType"] = "";
                    dr["Resturant"] = "";
                    dr["AddCart"] = "";
                    dr["ChildRate"] = "";
                    dr["AdultRate"] = "";
                    dr["Time"] = "";
                    dr["Veg"] = "";
                    dr["NonVeg"] = "";
                    dr["Jain"] = "";
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

                    DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                    DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                    CheckBox chkMeal = (CheckBox)item.FindControl("chkMeal");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtVage = (TextBox)item.FindControl("txtVage");
                    TextBox txtNonVage = (TextBox)item.FindControl("txtNonVag");
                    TextBox txtJain = (TextBox)item.FindControl("txtJain");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            binddropdownlist(drpResturant, ds);
                            drpResturant.Text = dt.Rows[itm]["Resturant"].ToString();
                            if (drpResturant.Text != "")
                            {
                                DataSet dsMealType = objGitDetail.fetchMealType("GET_MEAL_TYPE_OF_RESTURANTS", drpResturant.Text);
                                binddropdownlist(drpMealType, dsMealType);

                                drpMealType.Text = dt.Rows[itm]["MealType"].ToString();
                            }

                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtChildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtAdultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                            }

                            txtVage.Text = dt.Rows[itm]["Veg"].ToString();
                            txtNonVage.Text = dt.Rows[itm]["NonVeg"].ToString();
                            txtJain.Text = dt.Rows[itm]["Jain"].ToString();
                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chkMeal.Checked = true;
                            }
                            else
                            {
                                chkMeal.Checked = false;
                            }
                        }
                    }
                }

                if (RoleId != "18")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblChildrate = new Label();
                    Label lblAdultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {

                        RateButton = (Button)gv.Rows[i].FindControl("btnMealRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                        //gv.Columns[4].Visible = false;
                        //gv.Columns[5].Visible = false;

                    }
                }

                if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblChildrate = new Label();
                    Label lblAdultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnMealRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                       
                    }
                }
                if (Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                {
                   
                    gv.Columns[6].Visible = true;
                    gv.Columns[7].Visible = true;
                    gv.Columns[8].Visible = true;
                    gv.Columns[9].Visible = true;
                    btnmealsave.Visible = true;
                }
                else
                {
                   
                    gv.Columns[6].Visible = false;
                    gv.Columns[7].Visible = false;
                    gv.Columns[8].Visible = false;
                    gv.Columns[9].Visible = false;
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


        #region RESTURANT DROPDOWN SELECTED INDEX CHANGED
        public void drpResturant1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal1, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal1.Update();
            }

        }

        public void drpResturant2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal2, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal2.Update();
            }

        }

        public void drpResturant3_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal3, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal3.Update();
            }

        }

        public void drpResturant4_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal4, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal4.Update();
            }

        }

        public void drpResturant5_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal5, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal5.Update();
            }

        }

        public void drpResturant6_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal6, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal6.Update();
            }

        }

        public void drpResturant7_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal7, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal7.Update();
            }

        }

        public void drpResturant8_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal8, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal8.Update();
            }

        }

        public void drpResturant9_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal9, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal9.Update();
            }

        }

        public void drpResturant10_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                mealTypeLoad(gridMeal10, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal10.Update();
            }

        }
        #endregion

        // LOADS MEAL TYPES ACCORDING TO HOTEL FUNCTION
        protected void mealTypeLoad(GridView gv, int Index)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                        DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");

                        DataSet ds = objGitDetail.fetchMealType("GET_MEAL_TYPE_OF_RESTURANTS", drpResturant.Text);
                        binddropdownlist(drpMealType, ds);

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

        #region ADD BUTTONS

        protected void btnMealAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal1, lblMealCity1.Text, upMeal1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal2, lblMealCity2.Text, upMeal2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd3_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal3, lblMealCity3.Text, upMeal3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd4_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal4, lblMealCity4.Text, upMeal4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd5_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal5, lblMealCity5.Text, upMeal5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd6_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal6, lblMealCity6.Text, upMeal6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd7_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal7, lblMealCity7.Text, upMeal7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd8_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal8, lblMealCity8.Text, upMeal8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd9_Click(object sender, EventArgs e)
        {
            try
            {

                addMeals(gridMeal9, lblMealCity9.Text, upMeal9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnMealAdd10_Click(object sender, EventArgs e)
        {
            try
            {
                addMeals(gridMeal10, lblMealCity10.Text, upMeal10);
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

        #region RATE BUTTON
        protected void btnMealRate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal1, lblMealCity1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate2_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal2, lblMealCity2.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate3_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal3, lblMealCity3.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate4_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal4, lblMealCity4.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate5_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal5, lblMealCity5.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate6_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal6, lblMealCity6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal7, lblMealCity7.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate8_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal8, lblMealCity8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate9_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal9, lblMealCity9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnMealRate10_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                mealRate(gridMeal10, lblMealCity10.Text, repeaterItemIndex);
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

        // REDIRECT WHILE CLICK ON RATE FUNCTION
        protected void mealRate(GridView gv, String City, int Itemindex)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Itemindex == item.DataItemIndex)
                    {
                        DropDownList drpResturantName = (DropDownList)item.FindControl("drpResturant");
                        DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                        Session["ResturantName"] = drpResturantName.Text;
                        Session["MealType"] = drpMealType.Text;
                        Session["City"] = City;
                        //Response.Redirect("~/Views/GIT/GitMealRate.aspx?TOURID=" + tourId);
                        DataSet ds = new DataSet();
                        if (tourId != null)
                        {
                            ds = objEditUpdateGITInformation.GetResturantRate(int.Parse(tourId), Session["ResturantName"].ToString(), Session["MealType"].ToString(), Session["City"].ToString());

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                                int cartID = 0;
                                cartID = Convert.ToInt32(ds.Tables[0].Rows[i]["GIT_RESTAURENT_CART_ID"].ToString());
                                objEditUpdateGITInformation.saveResturantRate(cartID, txtAdultRate.Text, txtChildRate.Text);
                                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
                            }
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

        // GET DATA IN EDIT MODE
        protected void fillResturantsEditMode(GridView gv, String City, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objEditUpdateGITInformation.GetResturantName(int.Parse(tourId), City);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {
                            //  String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();



                            DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");

                            DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");

                            TextBox txtDate = (TextBox)item.FindControl("txtDate");
                            //TextBox txtTodate = (TextBox)item.FindControl("txtCheckOutDate");
                            //TextBox txtNights = (TextBox)item.FindControl("txtNights");

                            CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkMeal");

                            drpResturant.Text = ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            DataSet dsMealType = objGitDetail.fetchMealType("GET_MEAL_TYPE_OF_RESTURANTS", drpResturant.Text);
                            binddropdownlist(drpMealType, dsMealType);

                            drpMealType.Text = ds.Tables[0].Rows[j]["MEAL_DESC"].ToString();

                            txtDate.Text = ds.Tables[0].Rows[j]["DATE"].ToString();
                            if (ds.Tables[0].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                            {
                                chkPackgeFlag.Checked = true;
                            }
                            TextBox txtchildrate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultrate = (TextBox)item.FindControl("txtAdultRate");

                            txtchildrate.Text = ds.Tables[0].Rows[j]["CHILD_RATE_PER_PERSON"].ToString();
                            txtAdultrate.Text = ds.Tables[0].Rows[j]["ADULT_RATE_PER_PERSON"].ToString();

                            RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                            TextBox txtVage = (TextBox)item.FindControl("txtVage");
                            TextBox txtNonVage = (TextBox)item.FindControl("txtNonVag");
                            TextBox txtJain = (TextBox)item.FindControl("txtJain");
                            if (ds.Tables[0].Rows[j]["RESTAURENT_TIME"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[j]["RESTAURENT_TIME"].ToString());
                            }
                            txtVage.Text = ds.Tables[0].Rows[j]["VEG"].ToString();
                            txtNonVage.Text = ds.Tables[0].Rows[j]["NONVEG"].ToString();
                            txtJain.Text = ds.Tables[0].Rows[j]["JAIN"].ToString();
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        addMeals(gv, City, uppanel);
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

        // INSERT RECORD
        protected void insertResturantDetails(GridView gv, string City_Name, String currency_Name)
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {


                        DropDownList drpReturant = (DropDownList)item.FindControl("drpResturant");
                        DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");


                        TextBox txtDate = (TextBox)item.FindControl("txtDate");

                        CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkMeal");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        TextBox txtVage = (TextBox)item.FindControl("txtVage");
                        TextBox txtNonVage = (TextBox)item.FindControl("txtNonVag");
                        TextBox txtJain = (TextBox)item.FindControl("txtJain");

                        bool mealflag = false;
                        if (chkPackgeFlag.Checked)
                        {
                            mealflag = true;
                        }

                        if (drpReturant.Text != "" && drpMealType.Text != "" && txtDate.Text != "")
                        {
                            objInsertGitDetails.insertResturantsDetails(0, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), drpReturant.Text, drpMealType.Text, txtDate.Text, currency_Name, mealflag, int.Parse(Session["AgentId"].ToString()), City_Name, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), txttime.SelectedDate.ToString(), txtVage.Text, txtNonVage.Text, txtJain.Text);
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

        #region VALIDATION FOR DATE ON TEXT CHANGED

        protected void txtMeal_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel1, gridMeal1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }


        }
        protected void txtMeal_Date_TextChanged2(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel2, gridMeal2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged3(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel3, gridMeal3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged4(object sender, EventArgs e)
        {
            try
            {

                MealValidation(GridHotel4, gridMeal4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }


        }
        protected void txtMeal_Date_TextChanged5(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel5, gridMeal5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged6(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel6, gridMeal6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged7(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel7, gridMeal7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged8(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel8, gridMeal8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged9(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel9, gridMeal9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtMeal_Date_TextChanged10(object sender, EventArgs e)
        {
            try
            {
                MealValidation(GridHotel10, gridMeal10);
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

        #region MEAL REMOVE BUTTONS



        protected void btnMealRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal1, lblMealCity1.Text, upMeal1, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal2, lblMealCity2.Text, upMeal2, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove3_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal3, lblMealCity3.Text, upMeal3, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove4_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal4, lblMealCity4.Text, upMeal4, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove5_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal5, lblMealCity5.Text, upMeal5, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove6_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal6, lblMealCity6.Text, upMeal6, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove7_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal7, lblMealCity7.Text, upMeal7, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove8_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal8, lblMealCity8.Text, upMeal8, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove9_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal9, lblMealCity9.Text, upMeal9, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnMealRemove10_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveMeals(gridMeal10, lblMealCity10.Text, upMeal10, rowID);
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

        protected void RemoveMeals(GridView gv, string CITY_NAME, UpdatePanel uppanel, int rowIndex)
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
                DataSet ds = objGitDetail.fetchResturants("GET_RESTRANTS_NAMES_FOR_GIT", CITY_NAME);



                foreach (GridViewRow item in gv.Rows)
                {

                    DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                    DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                    CheckBox chkMeal = (CheckBox)item.FindControl("chkMeal");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("MealType");
                        dt.Columns.Add("Resturant");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = txtDate.Text;
                    dr["MealType"] = drpMealType.Text;
                    dr["Resturant"] = drpResturant.Text;
                    dr["ChildRate"] = txtChildRate.Text;
                    dr["AdultRate"] = txtAdultRate.Text;
                    if (chkMeal.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }

                    dt.Rows.Add(dr);

                }


                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }



                    DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                    DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                    CheckBox chkMeal = (CheckBox)item.FindControl("chkMeal");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            binddropdownlist(drpResturant, ds);
                            drpResturant.Text = dt.Rows[itm]["Resturant"].ToString();
                            if (drpResturant.Text != "")
                            {
                                DataSet dsMealType = objGitDetail.fetchMealType("GET_MEAL_TYPE_OF_RESTURANTS", drpResturant.Text);
                                binddropdownlist(drpMealType, dsMealType);

                                drpMealType.Text = dt.Rows[itm]["MealType"].ToString();
                            }

                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtChildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtAdultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chkMeal.Checked = true;
                            }
                            else
                            {
                                chkMeal.Checked = false;
                            }
                        }
                    }
                }

                //if (RoleId != "18")
                //{
                //    Button RateButton = new Button();
                //    TextBox ChildRate = new TextBox();
                //    TextBox AdultRate = new TextBox();
                //    Label lblchildrate = new Label();
                //    Label lbladultrate = new Label();
                //    for (int i = 0; i < gv.Rows.Count; i++)
                //    {

                //        RateButton = (Button)gv.Rows[i].FindControl("btnMealRate");
                //        RateButton.Visible = false;

                //        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                //        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                //        ChildRate.Visible = false;
                //        AdultRate.Visible = false;

                //        gv.Columns[4].Visible = false;
                //        gv.Columns[5].Visible = false;
                //        gv.Columns[6].Visible = false;

                //    }
                //}

                //if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                //{
                //    Button RateButton = new Button();
                //    TextBox ChildRate = new TextBox();
                //    TextBox AdultRate = new TextBox();
                //    Label lblchildrate = new Label();
                //    Label lbladultrate = new Label();
                //    for (int i = 0; i < gv.Rows.Count; i++)
                //    {
                //        RateButton = (Button)gv.Rows[i].FindControl("btnMealRate");
                //        RateButton.Visible = false;

                //        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                //        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                //        ChildRate.Visible = false;
                //        AdultRate.Visible = false;


                //        gv.Columns[4].Visible = false;
                //        gv.Columns[5].Visible = false;
                //        gv.Columns[6].Visible = false;
                //    }
                //}
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
        #endregion

        #region CONFERENCE


        protected void addConference(GridView gv, string CityName, UpdatePanel uppanel)
        {
            try
            {
                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objGitDetail.fetchConfGala("GET_CONFERENCE_HOTEL", CityName);



                foreach (GridViewRow item in gv.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkConf");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("ConferenceType");
                        dt.Columns.Add("Hotel");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("Time");
                        dt.Columns.Add("ReconfirmedDate");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = txtDate.Text;
                    dr["ConferenceType"] = drpConfType.Text;
                    dr["Hotel"] = drpHotel.Text;
                    dr["ChildRate"] = txtchildRate.Text;
                    dr["AdultRate"] = txtadultRate.Text;
                    dr["Time"] = txttime.SelectedDate;
                    dr["ReconfirmedDate"] = txtReconfirmedDate.Text;
                    if (chk.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("ConferenceType");
                        dt.Columns.Add("Hotel");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("Time");
                        dt.Columns.Add("ReconfirmedDate");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = "";
                    dr["ConferenceType"] = "";
                    dr["Hotel"] = "";
                    dr["AddCart"] = "";
                    dr["ChildRate"] = "";
                    dr["AdultRate"] = "";
                    dr["Time"] = "";
                    dr["ReconfirmedDate"] = "";
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
                    CheckBox chk = (CheckBox)item.FindControl("chkConf");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            //DropDownList drpMeal = (DropDownList)item.FindControl("drpMeal1");
                            binddropdownlist(drpHotel, ds);
                            drpHotel.Text = dt.Rows[itm]["Hotel"].ToString();
                            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                            {
                                DataSet ds_ConfType = objGitDetail.CommonSp("GET_CONFERENCE_TYPE");
                                binddropdownlist(drpConfType, ds_ConfType);
                                if (drpHotel.Text != "")
                                {


                                    drpConfType.Text = dt.Rows[itm]["ConferenceType"].ToString();
                                }
                            }
                            else
                            {
                                DataSet ds_ConfType = objGitDetail.CommonSp("GET_CONFERENCE_TYPE");
                                binddropdownlist(drpConfType, ds_ConfType);

                                drpConfType.Text = dt.Rows[itm]["ConferenceType"].ToString();
                            }


                            txtDate.Text = dt.Rows[itm]["Date"].ToString();



                            txtchildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtadultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                            }
                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chk.Checked = true;
                            }
                            else
                            {
                                chk.Checked = false;
                            }
                            txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();
                        }
                    }
                }

                if (RoleId != "18")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnConferenceRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;

                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                        //gv.Columns[4].Visible = false;
                        //gv.Columns[5].Visible = false;

                    }

                }

                if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnConferenceRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;

                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                        //gv.Columns[4].Visible = false;
                        //gv.Columns[5].Visible = false;

                    }
                }

                if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
                {

                    if (ViewState["PaymentDatetxtVsible"] != null)
                    {
                        TextBox txtReconfirmedDate = new TextBox();
                        Label lblReconfirmedDate = new Label();

                        for (int i = 0; i < gv.Rows.Count - 1; i++)
                        {
                            if (i == int.Parse(ViewState["PaymentDatetxtVsible"].ToString()))
                            {
                                txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                                txtReconfirmedDate.Visible = true;

                                lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                                lblReconfirmedDate.Visible = true;
                                ViewState["PaymentDatetxtVsible"] = null;
                                break;
                            }
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

        #region ADD BUTTONS
        protected void btnConf1_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf1, lblConfCity1.Text, upConf1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf2_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf2, lblConfCity2.Text, upConf2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf3_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf3, lblConfCity3.Text, upConf3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf4_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf4, lblConfCity4.Text, upConf4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf5_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf5, lblConfCity5.Text, upConf5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf6_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf6, lblConfCity6.Text, upConf6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf7_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf7, lblConfCity7.Text, upConf7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf8_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf8, lblConfCity8.Text, upConf8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf9_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf9, lblConfCity9.Text, upConf9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConf10_Click(object sender, EventArgs e)
        {
            try
            {
                addConference(gridConf10, lblConfCity10.Text, upConf10);
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

        #region RATE BUTTONS
        protected void btnConferenceRate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf1, lblConfCity1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate2_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf2, lblConfCity2.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate3_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf3, lblConfCity3.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate4_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf4, lblConfCity4.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate5_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf5, lblConfCity5.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate6_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf6, lblConfCity6.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf7, lblConfCity7.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate8_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf8, lblConfCity8.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate9_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf9, lblConfCity9.Text, repeaterItemIndex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnConferenceRate10_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                ConferenceRate(gridConf10, lblConfCity10.Text, repeaterItemIndex);

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


        protected void ConferenceRate(GridView gv, String City, int Itemindex)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Itemindex == item.DataItemIndex)
                    {
                        DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                        DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                        Session["HotelName"] = drpHotel.Text;
                        Session["ConferenceType"] = drpConfType.Text;
                        Session["City"] = City;
                        //Response.Redirect("~/Views/GIT/GitConferenceRate.aspx?TOURID=" + tourId);
                        DataSet ds = new DataSet();
                        if (tourId != null)
                        {
                            ds = objEditUpdateGITInformation.GetConferenceRate(int.Parse(tourId), Session["HotelName"].ToString(), Session["ConferenceType"].ToString(), Session["City"].ToString());
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                                int cartID = 0;
                                cartID = Convert.ToInt32(ds.Tables[0].Rows[i]["GIT_CONFERENCE_CART_ID"].ToString());
                                objEditUpdateGITInformation.saveConferenceRate(cartID, txtAdultRate.Text, txtChildRate.Text);

                            }
                            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
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

        protected void fillConferenceEditMode(GridView gv, String City, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objEditUpdateGITInformation.GetConferenceHotelName(int.Parse(tourId), City);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {
                            DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");

                            DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                            RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");


                            TextBox txtDate = (TextBox)item.FindControl("txtDate");

                            CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkConf");

                            drpHotel.Text = ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            DataSet ds_ConfType = objGitDetail.CommonSp("GET_CONFERENCE_TYPE");
                            binddropdownlist(drpConfType, ds_ConfType);

                            drpConfType.Text = ds.Tables[0].Rows[j]["CONFERENCE_TYPE"].ToString();

                            txtDate.Text = ds.Tables[0].Rows[j]["DATE"].ToString();
                            if (ds.Tables[0].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                            {
                                chkPackgeFlag.Checked = true;
                            }
                            if (ds.Tables[0].Rows[j]["CONFERENCE_TIME"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[j]["CONFERENCE_TIME"].ToString());
                            }
                            TextBox txtchildrate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultrate = (TextBox)item.FindControl("txtAdultRate");

                            string childrate = ds.Tables[0].Rows[j]["CHILD_RATE_PER_PERSON"].ToString();
                            string adultrate = ds.Tables[0].Rows[j]["ADULT_RATE_PER_PERSON"].ToString();
                            txtchildrate.Text = childrate;
                            txtAdultrate.Text = adultrate;

                            TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                            txtReconfirmedDate.Text = ds.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString();

                            if (ds.Tables[0].Rows[j]["IS_BOOKED"].ToString() == "True" && Session["OrderStatus"].ToString() == "Booked")
                            {
                                ViewState["PaymentDatetxtVsible"] = j;
                                txtReconfirmedDate.Visible = true;

                                Label lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                                lblReconfirmedDate.Visible = true;
                            }
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        addConference(gv, City, uppanel);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ViewState["PaymentDatetxtVsible"] = null;
            }
        }

        protected void insertConferenceDetails(GridView gv, string City_Name, String currency_Name)
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {


                        DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                        DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");

                        TextBox txtDate = (TextBox)item.FindControl("txtDate");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkConf");

                        bool mealflag = false;
                        if (chkPackgeFlag.Checked)
                        {
                            mealflag = true;
                        }

                        if (drpHotel.Text != "" && drpConfType.Text != "" && txtDate.Text != "")
                        {
                            objInsertGitDetails.insertConferenceDetails(0, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), drpHotel.Text, drpConfType.Text, txtDate.Text, currency_Name, mealflag, int.Parse(Session["AgentId"].ToString()), City_Name, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), txttime.SelectedDate.ToString());
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

        #region CONFERENCE DATE VALIDATION ON DATE

        protected void txtConf_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel1, gridConf1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged2(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel2, gridConf2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged3(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel3, gridConf3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged4(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel4, gridConf4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged5(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel5, gridConf5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged6(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel6, gridConf6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        protected void txtConf_Date_TextChanged7(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel7, gridConf7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged8(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel8, gridConf8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged9(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel9, gridConf9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        protected void txtConf_Date_TextChanged10(object sender, EventArgs e)
        {
            try
            {
                ConfValidation(GridHotel10, gridConf10);
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

        protected void InsertPaymentDateConference(GridView gv, string City_Name)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");

                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtPaymentDate = (TextBox)item.FindControl("txtReconfirmedDate");

                    if (drpHotel.Text != "" && drpConfType.Text != "" && txtDate.Text != "")
                    {
                        objBookSp.insertPaymentDateConference(int.Parse(tourId), txtPaymentDate.Text, drpHotel.Text, drpConfType.Text, City_Name);
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



        protected void RemoveConference(GridView gv, string CityName, UpdatePanel uppanel, int rowIndex)
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
                DataSet ds = objGitDetail.fetchConfGala("GET_CONFERENCE_HOTEL", CityName);



                foreach (GridViewRow item in gv.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkConf");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("ConferenceType");
                        dt.Columns.Add("Hotel");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("Time");
                        dt.Columns.Add("ReconfirmedDate");

                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = txtDate.Text;
                    dr["ConferenceType"] = drpConfType.Text;
                    dr["Hotel"] = drpHotel.Text;
                    dr["ChildRate"] = txtchildRate.Text;
                    dr["AdultRate"] = txtadultRate.Text;
                    dr["Time"] = txttime.SelectedDate;
                    dr["ReconfirmedDate"] = txtReconfirmedDate.Text;

                    if (chk.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }
                    dt.Rows.Add(dr);

                }



                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }

                    CheckBox chk = (CheckBox)item.FindControl("chkConf");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            binddropdownlist(drpHotel, ds);
                            drpHotel.Text = dt.Rows[itm]["Hotel"].ToString();
                            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                            {
                                if (drpHotel.Text != "")
                                {
                                    DataSet ds_ConfType = objGitDetail.CommonSp("GET_CONFERENCE_TYPE");
                                    binddropdownlist(drpConfType, ds_ConfType);

                                    drpConfType.Text = dt.Rows[itm]["ConferenceType"].ToString();
                                }
                            }
                            else
                            {
                                DataSet ds_ConfType = objGitDetail.CommonSp("GET_CONFERENCE_TYPE");
                                binddropdownlist(drpConfType, ds_ConfType);

                                drpConfType.Text = dt.Rows[itm]["ConferenceType"].ToString();
                            }


                            txtDate.Text = dt.Rows[itm]["Date"].ToString();



                            txtchildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtadultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                            }
                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chk.Checked = true;
                            }
                            else
                            {
                                chk.Checked = false;
                            }
                            txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();


                        }
                    }
                }
                if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
                {

                    TextBox txtReconfirmedDate = new TextBox();
                    Label lblReconfirmedDate = new Label();

                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                        txtReconfirmedDate.Visible = true;

                        lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                        lblReconfirmedDate.Visible = true;
                    }
                }
            
                if (Session["OrderStatus"].ToString() == "In Process")
                {
                    Button RateButton = new Button();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnConferenceRate");
                        RateButton.Visible = false;
                        TextBox ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        TextBox AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
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

        #region CONFERENCE REMOVE BUTTONS
        protected void btnConfRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf1, lblConfCity1.Text, upConf1, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnConfRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf2, lblConfCity2.Text, upConf2, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnConfRemove3_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf3, lblConfCity3.Text, upConf3, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnConfRemove4_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf4, lblConfCity4.Text, upConf4, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void btnConfRemove5_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf5, lblConfCity5.Text, upConf5, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void btnConfRemove6_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf6, lblConfCity6.Text, upConf6, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void btnConfRemove7_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf7, lblConfCity7.Text, upConf7, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void btnConfRemove8_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf8, lblConfCity8.Text, upConf8, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void btnConfRemove9_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf9, lblConfCity9.Text, upConf9, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnConfRemove10_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveConference(gridConf10, lblConfCity10.Text, upConf10, rowID);

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


        #endregion

        #region GALA DINNER

        protected void addGalaDinner(GridView gv, string CityName, UpdatePanel uppanel)
        {
            try
            {
                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objGitDetail.fetchConfGala("GET_GALA_DINNER_HOTEL", CityName);



                foreach (GridViewRow item in gv.Rows)
                {
                    CheckBox chkGala = (CheckBox)item.FindControl("chkGala");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("GalaType");
                        dt.Columns.Add("Hotel");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("ReconfirmedDate");
                        dt.Columns.Add("time");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = txtDate.Text;
                    dr["GalaType"] = drpGalaType.Text;
                    dr["Hotel"] = drpHotel.Text;
                    dr["ChildRate"] = txtchildRate.Text;
                    dr["AdultRate"] = txtadultRate.Text;
                    dr["ReconfirmedDate"] = txtReconfirmedDate.Text;
                    if (chkGala.Checked)
                    {
                        dr["AddCart"] = "True";
                    }
                    else
                    {
                        dr["AddCart"] = "False";
                    }
                    dr["time"] = txttime.SelectedDate.ToString();
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Date");
                        dt.Columns.Add("GalaType");
                        dt.Columns.Add("Hotel");
                        dt.Columns.Add("AddCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("ReconfirmedDate");
                        dt.Columns.Add("time");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Date"] = "";
                    dr["GalaType"] = "";
                    dr["Hotel"] = "";
                    dr["AddCart"] = "";
                    dr["ChildRate"] = "";
                    dr["AdultRate"] = "";
                    dr["ReconfirmedDate"] = "";
                    dr["time"] = "";
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
                    CheckBox chkGala = (CheckBox)item.FindControl("chkGala");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {

                            binddropdownlist(drpHotel, ds);
                            drpHotel.Text = dt.Rows[itm]["Hotel"].ToString();
                            if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                            {
                                DataSet dsGalaType = objGitDetail.CommonSp("GET_GALA_DINNER_TYPE");
                                binddropdownlist(drpGalaType, dsGalaType);

                                if (drpHotel.Text != "")
                                {

                                    drpGalaType.Text = dt.Rows[itm]["GalaType"].ToString();
                                }
                            }
                            else
                            {
                                DataSet dsGalaType = objGitDetail.CommonSp("GET_GALA_DINNER_TYPE");
                                binddropdownlist(drpGalaType, dsGalaType);

                                drpGalaType.Text = dt.Rows[itm]["GalaType"].ToString();
                            }

                            txtDate.Text = dt.Rows[itm]["Date"].ToString();


                            txtchildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtadultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["AddCart"].ToString() == "True")
                            {
                                chkGala.Checked = true;
                            }
                            else
                            {
                                chkGala.Checked = false;
                            }
                            txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                            }
                        }
                    }
                }

                if (RoleId != "18")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnGalaDinnerRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                        //gv.Columns[4].Visible = false;
                        //gv.Columns[5].Visible = false;

                    }

                }
                if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnGalaDinnerRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;

                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }

                        //gv.Columns[4].Visible = false;
                        //gv.Columns[5].Visible = false;

                    }
                }

                if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
                {
                    if (ViewState["PaymentDatetxtVsible"] != null)
                    {
                        TextBox txtReconfirmedDate = new TextBox();
                        Label lblReconfirmedDate = new Label();

                        for (int i = 0; i < gv.Rows.Count - 1; i++)
                        {
                            if (i == int.Parse(ViewState["PaymentDatetxtVsible"].ToString()))
                            {
                                txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                                txtReconfirmedDate.Visible = true;

                                lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                                lblReconfirmedDate.Visible = true;
                                ViewState["PaymentDatetxtVsible"] = null;
                                break;
                            }
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

        #region ADD BUTTONS
        protected void btnGala_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala1, lblGalaCity1.Text, upGala1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala2_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala2, lblGalaCity2.Text, upGala2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala3_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala3, lblGalaCity3.Text, upGala3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala4_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala4, lblGalaCity4.Text, upGala4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala5_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala5, lblGalaCity5.Text, upGala5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala6_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala6, lblGalaCity6.Text, upGala6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala7_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala7, lblGalaCity7.Text, upGala7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala8_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala8, lblGalaCity8.Text, upGala8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala9_Click(object sender, EventArgs e)
        {
            try
            {

                addGalaDinner(gridGala9, lblGalaCity9.Text, upGala9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGala10_Click(object sender, EventArgs e)
        {
            try
            {
                addGalaDinner(gridGala10, lblGalaCity10.Text, upGala10);
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

        #region RATE BUTTONS
        protected void btnGalaDinnerRate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala1, lblGalaCity1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate2_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala2, lblGalaCity2.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate3_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala3, lblGalaCity3.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate4_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala4, lblGalaCity4.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate5_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala5, lblGalaCity5.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate6_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala6, lblGalaCity6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala7, lblGalaCity7.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate8_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala8, lblGalaCity8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate9_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala9, lblGalaCity9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnGalaDinnerRate10_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                galaDinnerRate(gridGala10, lblGalaCity10.Text, repeaterItemIndex);
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

        #region GALA DINNER REMOVE BUTTONS




        protected void btnGalaRemove_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala1, lblGalaCity1.Text, upGala1, rowID);
        }

        protected void btnGalaRemove2_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala2, lblGalaCity2.Text, upGala2, rowID);
        }

        protected void btnGalaRemove3_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala3, lblGalaCity3.Text, upGala3, rowID);
        }

        protected void btnGalaRemove4_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala4, lblGalaCity4.Text, upGala4, rowID);
        }

        protected void btnGalaRemove5_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala5, lblGalaCity5.Text, upGala5, rowID);
        }

        protected void btnGalaRemove6_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala6, lblGalaCity6.Text, upGala6, rowID);
        }

        protected void btnGalaRemove7_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala7, lblGalaCity7.Text, upGala7, rowID);
        }

        protected void btnGalaRemove8_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala8, lblGalaCity8.Text, upGala8, rowID);
        }

        protected void btnGalaRemove9_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala9, lblGalaCity9.Text, upGala9, rowID);
        }

        protected void btnGalaRemove10_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveGalaDinner(gridGala10, lblGalaCity10.Text, upGala10, rowID);
        }

        #endregion

        protected void fillGalaDinnerEditMode(GridView gv, String City, UpdatePanel uppanel)
        {
            try
            {

                DataSet ds = objEditUpdateGITInformation.GetGalaDinnerHotelName(int.Parse(tourId), City);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {
                            DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");

                            DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");

                            TextBox txtDate = (TextBox)item.FindControl("txtDate");

                            CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkGala");

                            drpHotel.Text = ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            DataSet ds_ConfType = objGitDetail.CommonSp("GET_GALA_DINNER_TYPE");
                            binddropdownlist(drpGalaType, ds_ConfType);

                            drpGalaType.Text = ds.Tables[0].Rows[j]["GALA_DINNER_TYPE"].ToString();

                            txtDate.Text = ds.Tables[0].Rows[j]["DATE"].ToString();
                            if (ds.Tables[0].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                            {
                                chkPackgeFlag.Checked = true;
                            }

                            RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                            if (ds.Tables[0].Rows[j]["GALA_DINNER_TIME"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[j]["GALA_DINNER_TIME"].ToString());
                            }

                            TextBox txtchildrate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultrate = (TextBox)item.FindControl("txtAdultRate");

                            string childrate = ds.Tables[0].Rows[j]["CHILD_RATE_PER_PERSON"].ToString();
                            string adultrate = ds.Tables[0].Rows[j]["ADULT_RATE_PER_PERSON"].ToString();
                            txtchildrate.Text = childrate;
                            txtAdultrate.Text = adultrate;

                            TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                            txtReconfirmedDate.Text = ds.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString();

                            if (ds.Tables[0].Rows[j]["IS_BOOKED"].ToString() == "True" && Session["OrderStatus"].ToString() == "Booked")
                            {
                                ViewState["PaymentDatetxtVsible"] = j;
                                txtReconfirmedDate.Visible = true;

                                Label lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                                lblReconfirmedDate.Visible = true;
                            }
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        addGalaDinner(gv, City, uppanel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ViewState["PaymentDatetxtVsible"] = null;
            }

        }

        protected void insertGalaDinnerDetails(GridView gv, string City_Name, String currency_Name)
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {


                        DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                        DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");

                        TextBox txtDate = (TextBox)item.FindControl("txtDate");

                        CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkGala");

                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");

                        bool mealflag = false;
                        if (chkPackgeFlag.Checked)
                        {
                            mealflag = true;
                        }

                        if (drpHotel.Text != "" && drpGalaType.Text != "" && txtDate.Text != "")
                        {
                            objInsertGitDetails.insertGalaDinnerDetails(0, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), drpHotel.Text, drpGalaType.Text, txtDate.Text, currency_Name, mealflag, int.Parse(Session["AgentId"].ToString()), City_Name, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), txttime.SelectedDate.ToString());
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

        protected void RemoveGalaDinner(GridView gv, string CityName, UpdatePanel uppanel, int rowIndex)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            int count = gv.Rows.Count;

            for (int i = 0; i < count - 1; i++)
            {
                dt1.Rows.Add();
            }

            DataSet ds = objGitDetail.fetchConfGala("GET_GALA_DINNER_HOTEL", CityName);



            foreach (GridViewRow item in gv.Rows)
            {
                CheckBox chkGala = (CheckBox)item.FindControl("chkGala");
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Date");
                    dt.Columns.Add("GalaType");
                    dt.Columns.Add("Hotel");
                    dt.Columns.Add("AddCart");
                    dt.Columns.Add("ChildRate");
                    dt.Columns.Add("AdultRate");
                    dt.Columns.Add("ReconfirmedDate");

                }

                DataRow dr = dt.NewRow();
                dr["Date"] = txtDate.Text;
                dr["GalaType"] = drpGalaType.Text;
                dr["Hotel"] = drpHotel.Text;
                dr["ChildRate"] = txtchildRate.Text;
                dr["AdultRate"] = txtadultRate.Text;
                dr["ReconfirmedDate"] = txtReconfirmedDate.Text;

                if (chkGala.Checked)
                {
                    dr["AddCart"] = "True";
                }
                else
                {
                    dr["AddCart"] = "False";
                }
                dt.Rows.Add(dr);

            }



            gv.DataSource = dt1;
            gv.DataBind();


            foreach (GridViewRow item in gv.Rows)
            {
                int itm = item.DataItemIndex;
                if (itm >= rowIndex)
                {
                    itm = itm + 1;
                }
                CheckBox chkGala = (CheckBox)item.FindControl("chkGala");
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                TextBox txtchildRate = (TextBox)item.FindControl("txtChildRate");
                TextBox txtadultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {

                        binddropdownlist(drpHotel, ds);
                        drpHotel.Text = dt.Rows[itm]["Hotel"].ToString();
                        if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                        {
                            if (drpHotel.Text != "")
                            {
                                DataSet dsGalaType = objGitDetail.CommonSp("GET_GALA_DINNER_TYPE");
                                binddropdownlist(drpGalaType, dsGalaType);

                                drpGalaType.Text = dt.Rows[itm]["GalaType"].ToString();
                            }
                        }
                        else
                        {
                            DataSet dsGalaType = objGitDetail.CommonSp("GET_GALA_DINNER_TYPE");
                            binddropdownlist(drpGalaType, dsGalaType);

                            drpGalaType.Text = dt.Rows[itm]["GalaType"].ToString();
                        }

                        txtDate.Text = dt.Rows[itm]["Date"].ToString();


                        txtchildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                        txtadultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                        if (dt.Rows[itm]["AddCart"].ToString() == "True")
                        {
                            chkGala.Checked = true;
                        }
                        else
                        {
                            chkGala.Checked = false;
                        }
                        txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();

                    }
                }
            }
            if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
            {

                TextBox txtReconfirmedDate = new TextBox();
                Label lblReconfirmedDate = new Label();

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                    txtReconfirmedDate.Visible = true;

                    lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                    lblReconfirmedDate.Visible = true;
                }
            }
            //if (RoleId != "18")
            //{
            //    Button RateButton = new Button();
            //    TextBox ChildRate = new TextBox();
            //    TextBox AdultRate = new TextBox();
            //    Label lblchildrate = new Label();
            //    Label lbladultrate = new Label();
            //    for (int i = 0; i < gv.Rows.Count; i++)
            //    {
            //        RateButton = (Button)gv.Rows[i].FindControl("btnGalaDinnerRate");
            //        RateButton.Visible = false;

            //        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
            //        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
            //        ChildRate.Visible = false;
            //        AdultRate.Visible = false;


            //        gv.Columns[4].Visible = false;
            //        gv.Columns[5].Visible = false;
            //        gv.Columns[6].Visible = false;
            //    }

            //}
            //if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
            //{
            //    Button RateButton = new Button();
            //    TextBox ChildRate = new TextBox();
            //    TextBox AdultRate = new TextBox();
            //    Label lblchildrate = new Label();
            //    Label lbladultrate = new Label();
            //    for (int i = 0; i < gv.Rows.Count; i++)
            //    {
            //        RateButton = (Button)gv.Rows[i].FindControl("btnGalaDinnerRate");
            //        RateButton.Visible = false;

            //        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
            //        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
            //        ChildRate.Visible = false;
            //        AdultRate.Visible = false;


            //        gv.Columns[4].Visible = false;
            //        gv.Columns[5].Visible = false;
            //        gv.Columns[6].Visible = false;
            //    }
            //}
            if (Session["OrderStatus"].ToString() == "In Process")
            {
                Button RateButton = new Button();
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    RateButton = (Button)gv.Rows[i].FindControl("btnGalaDinnerRate");
                    RateButton.Visible = false;
                    TextBox ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                    TextBox AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                    ChildRate.Visible = false;
                    AdultRate.Visible = false;
                    if (gv.HeaderRow != null)
                    {
                        Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                        lblAdultRate.Visible = false;
                        Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                        lblchildRate.Visible = false;
                    }
                }
            }
            uppanel.Update();
        }

        protected void galaDinnerRate(GridView gv, String City, int Itemindex)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                if (Itemindex == item.DataItemIndex)
                {
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                    Session["HotelName"] = drpHotel.Text;
                    Session["galaType"] = drpGalaType.Text;
                    Session["City"] = City;
                    //Response.Redirect("~/Views/GIT/GitGalaDinnerRate.aspx?TOURID=" + tourId);

                    DataSet ds = new DataSet();
                    if (tourId != null)
                    {
                        ds = objEditUpdateGITInformation.GetGalaDinnerRate(int.Parse(tourId), Session["HotelName"].ToString(), Session["galaType"].ToString(), Session["City"].ToString());
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                            int cartID = 0;
                            cartID = Convert.ToInt32(ds.Tables[0].Rows[i]["GIT_GALA_DINNER_CART_ID"].ToString());
                            objEditUpdateGITInformation.saveGalaDinnerRate(cartID, txtAdultRate.Text, txtChildRate.Text);

                        }
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
                    }
                }
            }
        }

        #region   GALA DINNER DATE VALIDATION


        protected void txtGalaDinner_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel1, gridGala1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void txtGalaDinner_Date_TextChanged2(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel2, gridGala2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged3(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel3, gridGala3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged4(object sender, EventArgs e)
        {
            try
            {

                GalaDinnerValidation(GridHotel4, gridGala4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged5(object sender, EventArgs e)
        {
            try
            {

                GalaDinnerValidation(GridHotel5, gridGala5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged6(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel6, gridGala6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged7(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel7, gridGala7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged8(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel8, gridGala8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged9(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel9, gridGala9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void txtGalaDinner_Date_TextChanged10(object sender, EventArgs e)
        {
            try
            {
                GalaDinnerValidation(GridHotel10, gridGala10);
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

        protected void InsertPaymentDateGalaDinner(GridView gv, string City_Name)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {


                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                    DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");

                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtPaymentDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkGala");

                    bool mealflag = false;
                    if (chkPackgeFlag.Checked)
                    {
                        mealflag = true;
                    }

                    if (drpHotel.Text != "" && drpGalaType.Text != "" && txtDate.Text != "")
                    {
                        objBookSp.insertPaymentDateGalaDinner(int.Parse(tourId), txtPaymentDate.Text, drpHotel.Text, drpGalaType.Text, City_Name);
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

        #region SITE SEEING

        protected void addSite(GridView gv, UpdatePanel uppanel, String city)
        {
            try
            {
                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objGitDetail.fetchSiteSeeing("FETCH_SIGHT_SEEING_FOR_GIT", city);



                foreach (GridViewRow item in gv.Rows)
                {

                    CheckBox chk = (CheckBox)item.FindControl("chkSiteSelect");
                    DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    TextBox txtDate = (TextBox)item.FindControl("txtSiteDate");
                    DropDownList drpSiteTime = (DropDownList)item.FindControl("drpSiteTime");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("SiteDetail");
                        dt.Columns.Add("AddtoCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("ReconfirmedDate");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("Time");
                    }

                    DataRow dr = dt.NewRow();
                    dr["SiteDetail"] = drpSiteDetails.Text;
                    dr["ChildRate"] = txtChildRate.Text;
                    dr["AdultRate"] = txtAdultRate.Text;
                    dr["ReconfirmedDate"] = txtReconfirmedDate.Text;
                    if (chk.Checked)
                    {
                        dr["AddtoCart"] = "True";
                    }
                    else
                    {
                        dr["AddtoCart"] = "False";
                    }
                    dr["Date"] = txtDate.Text;
                    dr["Time"] = drpSiteTime.Text;

                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("SiteDetail");
                        dt.Columns.Add("AddtoCart");
                        dt.Columns.Add("ChildRate");
                        dt.Columns.Add("AdultRate");
                        dt.Columns.Add("ReconfirmedDate");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("Time");
                    }

                    DataRow dr = dt.NewRow();
                    dr["SiteDetail"] = "";
                    dr["AddtoCart"] = "";
                    dr["ChildRate"] = "";
                    dr["AdultRate"] = "";
                    dr["ReconfirmedDate"] = "";
                    dr["Date"] = "";
                    dr["Time"] = "";
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

                    CheckBox chk = (CheckBox)item.FindControl("chkSiteSelect");
                    DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                    TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                    TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                    TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                    TextBox txtDate = (TextBox)item.FindControl("txtSiteDate");
                    DropDownList drpSiteTime = (DropDownList)item.FindControl("drpSiteTime");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {


                            binddropdownlist(drpSiteDetails, ds);

                            drpSiteDetails.Text = dt.Rows[itm]["SiteDetail"].ToString();
                            if (drpSiteDetails.Text != "")
                            {
                                DataSet ds1 = objGitDetail.getTimeForSiteSeeing("GET_TIME_FOR_SITE_SEEING_GIT", drpSiteDetails.Text, city);
                                binddropdownlist(drpSiteTime, ds1);

                                drpSiteTime.Text = dt.Rows[itm]["Time"].ToString();

                            }
                            txtChildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                            txtAdultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                            if (dt.Rows[itm]["AddtoCart"].ToString() == "True")
                            {
                                chk.Checked = true;
                            }
                            else
                            {
                                chk.Checked = false;
                            }
                            txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                        }
                    }
                }
                if (RoleId != "18")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnSiteRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                        
                    }

                }
                if (RoleId == "18" && Session["OrderStatus"].ToString() != "Request for Quote")
                {
                    Button RateButton = new Button();
                    TextBox ChildRate = new TextBox();
                    TextBox AdultRate = new TextBox();
                    Label lblchildrate = new Label();
                    Label lbladultrate = new Label();
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        RateButton = (Button)gv.Rows[i].FindControl("btnSiteRate");
                        RateButton.Visible = false;

                        ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                        AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                        ChildRate.Visible = false;
                        AdultRate.Visible = false;
                        if (gv.HeaderRow != null)
                        {
                            Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                            lblAdultRate.Visible = false;
                            Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                            lblchildRate.Visible = false;
                        }
                       
                    }
                    if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
                    {

                        TextBox txtReconfirmedDate = new TextBox();
                        Label lblReconfirmedDate = new Label();

                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                            txtReconfirmedDate.Visible = true;

                            lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                            lblReconfirmedDate.Visible = true;
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

        #region ADD BUTTONS
        protected void btnSiteAdd_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite1, upSite1, lblSiteCity1.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnSiteAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite2, upSite2, lblSiteCity2.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd3_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite3, upSite3, lblSiteCity3.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd4_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite4, upSite4, lblSiteCity4.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd5_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite5, upSite5, lblSiteCity5.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd6_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite6, upSite6, lblSiteCity6.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd7_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite7, upSite7, lblSiteCity7.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd8_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite8, upSite8, lblSiteCity8.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd9_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite9, upSite9, lblSiteCity9.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        protected void btnSiteAdd10_Click(object sender, EventArgs e)
        {
            try
            {
                addSite(gridSite10, upSite10, lblSiteCity10.Text);
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

        #region RATE BUTTONS

        protected void btnSiteRate_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite1, lblSiteCity1.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate2_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite2, lblSiteCity2.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate3_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite3, lblSiteCity3.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate4_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite4, lblSiteCity4.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate5_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite5, lblSiteCity5.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate6_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite6, lblSiteCity6.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite7, lblSiteCity7.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate8_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite8, lblSiteCity8.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate9_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite9, lblSiteCity9.Text, repeaterItemIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        protected void btnSiteRate10_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                siteSeeingRate(gridSite10, lblSiteCity10.Text, repeaterItemIndex);
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

        protected void siteSeeingRate(GridView gv, String City, int Itemindex)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                if (Itemindex == item.DataItemIndex)
                {
                    DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                    //   DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                    Session["SiteName"] = drpSiteDetails.Text;
                    //    Session["galaType"] = drpGalaType.Text;
                    Session["City"] = City;
                    //Response.Redirect("~/Views/GIT/GitSiteSeeingRate.aspx?TOURID=" + tourId);

                    DataSet ds = new DataSet();
                    if (tourId != null)
                    {
                        ds = objEditUpdateGITInformation.GetSiteSeeingRate(int.Parse(tourId), Session["SiteName"].ToString(), Session["City"].ToString());
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                            int cartID = 0;
                            cartID = Convert.ToInt32(ds.Tables[0].Rows[i]["GIT_SIGHT_CART_ID"].ToString());
                            objEditUpdateGITInformation.saveSiteSeeingRate(cartID, txtAdultRate.Text, txtChildRate.Text);
                        }
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
                    }
                }
            }
        }

        protected void insertSiteSeeing(GridView gv, string City_Name, String currency_Name)
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {


                        DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");

                        CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkSiteSelect");

                        TextBox  txtSiteDate = (TextBox)item.FindControl("txtSiteDate");

                        DropDownList drpSiteTime = (DropDownList)item.FindControl("drpSiteTime");

                        bool mealflag = false;
                        if (chkPackgeFlag.Checked)
                        {
                            mealflag = true;
                        }

                        if (drpSiteDetails.Text != "")
                        {
                            objInsertGitDetails.insertSiteSeeingDetails(0, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), drpSiteDetails.Text, currency_Name, mealflag, int.Parse(Session["AgentId"].ToString()), City_Name, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), txtSiteDate.Text, drpSiteTime.Text);
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

        protected void fillSiteSeeingEditMode(GridView gv, String City, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objEditUpdateGITInformation.GetSiteSeeing(int.Parse(tourId), City);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {
                            DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");


                            CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkSiteSelect");

                            drpSiteDetails.Text = ds.Tables[0].Rows[j]["SIGHT_SEEING_PACKAGE_NAME"].ToString();
                            if (ds.Tables[0].Rows[j]["PACKAGE_FLAG"].ToString() == "True")
                            {
                                chkPackgeFlag.Checked = true;
                            }
                            TextBox txtchildrate = (TextBox)item.FindControl("txtChildRate");
                            TextBox txtAdultrate = (TextBox)item.FindControl("txtAdultRate");

                            string childrate = ds.Tables[0].Rows[j]["CHILD_RATE_PER_PERSON"].ToString();
                            string adultrate = ds.Tables[0].Rows[j]["ADULT_RATE_PER_PERSON"].ToString();
                            txtchildrate.Text = childrate;
                            txtAdultrate.Text = adultrate;

                            TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");
                            txtReconfirmedDate.Text = ds.Tables[0].Rows[j]["PAYMENT_DUE_DATE"].ToString();


                            TextBox txtDate = (TextBox)item.FindControl("txtSiteDate");
                            DropDownList drpSiteTime = (DropDownList)item.FindControl("drpSiteTime");
                            DataSet ds1 = objGitDetail.getTimeForSiteSeeing("GET_TIME_FOR_SITE_SEEING_GIT", drpSiteDetails.Text, City);
                            binddropdownlist(drpSiteTime, ds1);
                            txtDate.Text = ds.Tables[0].Rows[j]["DATE"].ToString();
                            drpSiteTime.Text = ds.Tables[0].Rows[j]["TIME"].ToString();
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        addSite(gv, uppanel, City);
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

        protected void RemoveSite(GridView gv, UpdatePanel uppanel, String city, int rowIndex)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            int count = gv.Rows.Count;

            for (int i = 0; i < count - 1; i++)
            {
                dt1.Rows.Add();
            }
            DataSet ds = objGitDetail.fetchSiteSeeing("FETCH_SIGHT_SEEING_FOR_GIT", city);



            foreach (GridViewRow item in gv.Rows)
            {

                CheckBox chk = (CheckBox)item.FindControl("chkSiteSelect");
                DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("SiteDetail");
                    dt.Columns.Add("AddtoCart");
                    dt.Columns.Add("ChildRate");
                    dt.Columns.Add("AdultRate");
                    dt.Columns.Add("ReconfirmedDate");

                }

                DataRow dr = dt.NewRow();
                dr["SiteDetail"] = drpSiteDetails.Text;
                dr["ChildRate"] = txtChildRate.Text;
                dr["AdultRate"] = txtAdultRate.Text;
                dr["ReconfirmedDate"] = txtReconfirmedDate.Text;

                if (chk.Checked)
                {
                    dr["AddtoCart"] = "True";
                }
                else
                {
                    dr["AddtoCart"] = "False";
                }

                dt.Rows.Add(dr);

            }



            gv.DataSource = dt1;
            gv.DataBind();


            foreach (GridViewRow item in gv.Rows)
            {
                int itm = item.DataItemIndex;
                if (itm >= rowIndex)
                {
                    itm = itm + 1;
                }

                CheckBox chk = (CheckBox)item.FindControl("chkSiteSelect");
                DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");
                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtReconfirmedDate = (TextBox)item.FindControl("txtReconfirmedDate");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {


                        binddropdownlist(drpSiteDetails, ds);

                        drpSiteDetails.Text = dt.Rows[itm]["SiteDetail"].ToString();
                        txtChildRate.Text = dt.Rows[itm]["ChildRate"].ToString();
                        txtAdultRate.Text = dt.Rows[itm]["AdultRate"].ToString();
                        if (dt.Rows[itm]["AddtoCart"].ToString() == "True")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                        txtReconfirmedDate.Text = dt.Rows[itm]["ReconfirmedDate"].ToString();


                    }
                }
            }
            if (RoleId == "18" && Session["OrderStatus"].ToString() == "Booked")
            {

                TextBox txtReconfirmedDate = new TextBox();
                Label lblReconfirmedDate = new Label();

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    txtReconfirmedDate = (TextBox)gv.Rows[i].FindControl("txtReconfirmedDate");
                    txtReconfirmedDate.Visible = true;

                    lblReconfirmedDate = (Label)gv.HeaderRow.FindControl("lblReconfirmedDate");
                    lblReconfirmedDate.Visible = true;
                }
            }
          
            if (Session["OrderStatus"].ToString() == "In Process")
            {
                Button RateButton = new Button();
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    RateButton = (Button)gv.Rows[i].FindControl("btnSiteRate");
                    RateButton.Visible = false;
                    TextBox ChildRate = (TextBox)gv.Rows[i].FindControl("txtAdultRate");
                    TextBox AdultRate = (TextBox)gv.Rows[i].FindControl("txtChildRate");
                    ChildRate.Visible = false;
                    AdultRate.Visible = false;
                    if (gv.HeaderRow != null)
                    {
                        Label lblAdultRate = (Label)gv.HeaderRow.FindControl("lblAdultRate");
                        lblAdultRate.Visible = false;
                        Label lblchildRate = (Label)gv.HeaderRow.FindControl("lblchildRate");
                        lblchildRate.Visible = false;
                    }
                }
            }
            uppanel.Update();
        }

        #region SITE SEEING SELECTION DROP DOWN SELETED INDEX CHANGED
        public void drpSite_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite1, repeaterItemIndex, lblSiteCity1.Text);
                SiteSeeingValidation(gridSite1, upSite1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite1.Update();
            }
        }
        public void drpSite2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite2, repeaterItemIndex, lblSiteCity2.Text);
                SiteSeeingValidation(gridSite2, upSite2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite2.Update();
            }
        }
        public void drpSite3_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite3, repeaterItemIndex, lblSiteCity3.Text);
                SiteSeeingValidation(gridSite3, upSite3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite3.Update();
            }
        }
        public void drpSite4_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite4, repeaterItemIndex, lblSiteCity4.Text);
                SiteSeeingValidation(gridSite4, upSite4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite4.Update();
            }
        }
        public void drpSite5_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite5, repeaterItemIndex, lblSiteCity5.Text);
                SiteSeeingValidation(gridSite5, upSite5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite5.Update();
            }
        }
        public void drpSite6_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite6, repeaterItemIndex, lblSiteCity6.Text);
                SiteSeeingValidation(gridSite6, upSite6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite6.Update();
            }
        }
        public void drpSite7_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite7, repeaterItemIndex, lblSiteCity7.Text);
                SiteSeeingValidation(gridSite7, upSite7);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite7.Update();
            }
        }
        public void drpSite8_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite8, repeaterItemIndex, lblSiteCity8.Text);
                SiteSeeingValidation(gridSite8, upSite8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite8.Update();
            }
        }
        public void drpSite9_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite9, repeaterItemIndex, lblSiteCity9.Text);
                SiteSeeingValidation(gridSite9, upSite9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite9.Update();
            }
        }
        public void drpSite10_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                loadTimeForSiteSeeing(gridSite10, repeaterItemIndex, lblSiteCity10.Text);
                SiteSeeingValidation(gridSite10, upSite10);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upSite10.Update();
            }
        }
        #endregion

        #region SITE SEEING REMOVE BUTTONS




        protected void btnSiteRemove_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite1, upSite1, lblSiteCity1.Text, rowID);
        }

        protected void btnSiteRemove2_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite2, upSite2, lblSiteCity2.Text, rowID);
        }

        protected void btnSiteRemove3_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite3, upSite3, lblSiteCity3.Text, rowID);
        }

        protected void btnSiteRemove4_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite4, upSite4, lblSiteCity4.Text, rowID);
        }

        protected void btnSiteRemove5_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite5, upSite5, lblSiteCity5.Text, rowID);
        }

        protected void btnSiteRemove6_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite6, upSite6, lblSiteCity6.Text, rowID);
        }

        protected void btnSiteRemove7_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite7, upSite7, lblSiteCity7.Text, rowID);
        }

        protected void btnSiteRemove8_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite8, upSite8, lblSiteCity8.Text, rowID);
        }

        protected void btnSiteRemove9_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite9, upSite9, lblSiteCity9.Text, rowID);
        }

        protected void btnSiteRemove10_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveSite(gridSite10, upSite10, lblSiteCity10.Text, rowID);
        }



        #endregion
        #endregion

        #region TRANSPORT PACKAGE

        protected void insertTrasporPackage()
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    objInsertGitDetails.insertTrasportPackage(0, int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), int.Parse(Session["AgentId"].ToString()), int.Parse(Session["TransferPackgeId"].ToString()));


                }

                foreach (GridViewRow item in GridTrasport.Rows)
                {
                    Label lblId = (Label)item.FindControl("lbltp_detialid");
                    objInsertGitDetails.insertTrasportPackageDetails(int.Parse(lblId.Text),int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()));
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

        protected void btnTrasportRate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/GIT/TransportRate.aspx?TOURID=" + tourId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btnTrasportTime_Click(object sender, EventArgs e)
        {
            //string instpid = "";
            try
            {
                foreach (GridViewRow item in GridTrasport.Rows)
                {
                    //RadioButton btn = (RadioButton)item.FindControl("rbtnTPselect");
                    //Label id = (Label)item.FindControl("lbltp_priceid");
                    //if (btn.Checked)
                    //{
                    //    instpid = id.Text;
                    //    Label tpid = (Label)item.FindControl("lbltp_priceid");
                    //    Label Detailid = (Label)item.FindControl("lbltp_detialid");
                    //    TextBox date = (TextBox)item.FindControl("txtDate");
                    //    RadTimePicker time = (RadTimePicker)item.FindControl("rdtpTime");
                    //    objInsertGitDetails.UpadateTrasportPackageDetails(int.Parse(Detailid.Text), int.Parse(tourId), date.Text, time.SelectedDate.ToString());
                    //}
                    //else if (instpid == id.Text)
                    //{
                    //    Label tpid = (Label)item.FindControl("lbltp_priceid");
                    //    Label Detailid = (Label)item.FindControl("lbltp_detialid");
                    //    TextBox datebkk = (TextBox)item.FindControl("txtDate");
                    //    RadTimePicker timebkk = (RadTimePicker)item.FindControl("rdtpTime");
                    //    objInsertGitDetails.UpadateTrasportPackageDetails(int.Parse(Detailid.Text), int.Parse(tourId), date.Text, time.SelectedDate.ToString());
                    //}
                    Label Detailid = (Label)item.FindControl("lbltp_detialid");
                    TextBox date = (TextBox)item.FindControl("txtDate");
                    RadTimePicker time = (RadTimePicker)item.FindControl("rdtpTime");
                    objInsertGitDetails.UpadateTrasportPackageDetails(int.Parse(Detailid.Text), int.Parse(tourId), date.Text, time.SelectedDate.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Time And Date updated Successfully..')", true);
            }

        }

        #endregion

        #region ADDITIONAL SERVICES

        protected void addServices(GridView gv, UpdatePanel uppanel)
        {
            try
            {
                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();



                foreach (GridViewRow item in gv.Rows)
                {

                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");
                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");
                    TextBox txtPassenger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkaditional = (CheckBox)item.FindControl("chkAditional");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Seriveces");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("NoPax");
                        dt.Columns.Add("Supplier");
                        dt.Columns.Add("NetPrice");
                        dt.Columns.Add("SellPrice");
                        dt.Columns.Add("From");
                        dt.Columns.Add("To");
                        dt.Columns.Add("NoOfPassanger");
                        dt.Columns.Add("chkAditional");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Seriveces"] = txtServices.Text;
                    dr["Date"] = txtDate.Text;
                    dr["NoPax"] = txtNoPax.Text;
                    dr["Supplier"] = drpSupplier.Text;
                    dr["NetPrice"] = txtNetPrice.Text;
                    dr["SellPrice"] = txtSellPrice.Text;
                    dr["From"] = txtFrom.Text;
                    dr["To"] = txtTo.Text;
                    dr["NoOfPassanger"] = txtPassenger.Text;
                    dr["chkAditional"] = chkaditional.Checked;
                    dt.Rows.Add(dr);

                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Seriveces");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("NoPax");
                        dt.Columns.Add("Supplier");
                        dt.Columns.Add("NetPrice");
                        dt.Columns.Add("SellPrice");
                        dt.Columns.Add("From");
                        dt.Columns.Add("To");
                        dt.Columns.Add("NoOfPassanger");
                        dt.Columns.Add("chkAditional");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Seriveces"] = "";
                    dr["Date"] = "";
                    dr["NoPax"] = "";
                    dr["Supplier"] = "";
                    dr["NetPrice"] = "";
                    dr["SellPrice"] = "";
                    dr["From"] = "";
                    dr["To"] = "";
                    dr["NoOfPassanger"] = "";
                    dr["chkAditional"] = "";

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
                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");
                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");
                    TextBox txtPassenger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkaditional = (CheckBox)item.FindControl("chkAditional");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            txtServices.Text = dt.Rows[itm]["Seriveces"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtNoPax.Text = dt.Rows[itm]["NoPax"].ToString();
                            DataSet dsadditional = objHotelStoreProcedure.fetchComboData("GET_CHAIN_NAME_FOR_ADDITIONAL_SERVICES", "");
                            binddropdownlist(drpSupplier, dsadditional);
                            drpSupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                            txtNetPrice.Text = dt.Rows[itm]["NetPrice"].ToString(); ;
                            txtSellPrice.Text = dt.Rows[itm]["SellPrice"].ToString();
                            txtFrom.Text = dt.Rows[itm]["From"].ToString();
                            txtTo.Text = dt.Rows[itm]["To"].ToString();
                            txtPassenger.Text = dt.Rows[itm]["NoOfPassanger"].ToString();
                            if (dt.Rows[itm]["chkAditional"].ToString() == "True")
                            {
                                chkaditional.Checked = true;
                            }
                            else
                            {
                                chkaditional.Checked = false;
                            }
                        }
                    }
                }
                if (RoleId != "18")
                {
                    divAddServ.Visible = false;
                    lbladdservice.Visible = false;

                }
                if (Session["OrderStatus"].ToString() == "To Be Reconfirmed")
                {

                    TextBox txtNoOfPassanger = new TextBox();
                    Label lblpassenger = new Label();
                    Label lbladd = new Label();
                    CheckBox chkadd = new CheckBox();
                    btnpessSave.Visible = true;
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        lblpassenger = (Label)gv.HeaderRow.FindControl("lblpassenger");
                        lblpassenger.Visible = true;

                        txtNoOfPassanger = (TextBox)gv.Rows[i].FindControl("txtNoOfPassanger");
                        txtNoOfPassanger.Visible = true;

                        lbladd = (Label)gv.HeaderRow.FindControl("lblchkad");
                        lbladd.Visible = true;

                        chkadd = (CheckBox)gv.Rows[i].FindControl("chkAditional");
                        chkadd.Visible = true;
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

        protected void btnAdditionalRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveService(gridAddServices, upAddServices, rowID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void RemoveService(GridView gv, UpdatePanel uppanel, int rowIndex)
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
                //  DataSet ds = objBookingFitStoreProcedure.fetchComboData("GET_MEALS");

                foreach (GridViewRow item in gv.Rows)
                {

                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");
                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");
                    TextBox txtPassenger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkaditional = (CheckBox)item.FindControl("chkAditional");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Seriveces");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("NoPax");
                        dt.Columns.Add("Supplier");
                        dt.Columns.Add("NetPrice");
                        dt.Columns.Add("SellPrice");
                        dt.Columns.Add("From");
                        dt.Columns.Add("To");
                        dt.Columns.Add("NoOfPassanger");
                        dt.Columns.Add("chkAditional");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Seriveces"] = txtServices.Text;
                    dr["Date"] = txtDate.Text;
                    dr["NoPax"] = txtNoPax.Text;
                    dr["Supplier"] = drpSupplier.Text;
                    dr["NetPrice"] = txtNetPrice.Text;
                    dr["SellPrice"] = txtSellPrice.Text;
                    dr["From"] = txtFrom.Text;
                    dr["To"] = txtTo.Text;
                    dr["NoOfPassanger"] = txtPassenger.Text;
                    dr["chkAditional"] = chkaditional.Checked;
                    dt.Rows.Add(dr);

                }
                gv.DataSource = dt1;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }

                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");
                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");
                    TextBox txtPassenger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkaditional = (CheckBox)item.FindControl("chkAditional");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            txtServices.Text = dt.Rows[itm]["Seriveces"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtNoPax.Text = dt.Rows[itm]["NoPax"].ToString();
                            drpSupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                            DataSet dsadditional = objHotelStoreProcedure.fetchComboData("GET_CHAIN_NAME_FOR_ADDITIONAL_SERVICES", "");
                            binddropdownlist(drpSupplier, dsadditional);

                            txtNetPrice.Text = dt.Rows[itm]["NetPrice"].ToString(); ;
                            txtSellPrice.Text = dt.Rows[itm]["SellPrice"].ToString();
                            txtFrom.Text = dt.Rows[itm]["From"].ToString();
                            txtTo.Text = dt.Rows[itm]["To"].ToString();
                            txtPassenger.Text = dt.Rows[itm]["NoOfPassanger"].ToString();
                            if (dt.Rows[itm]["chkAditional"].ToString() == "True")
                            {
                                chkaditional.Checked = true;
                            }
                            else
                            {
                                chkaditional.Checked = false;
                            }
                        }
                    }
                }
                if (RoleId != "18")
                {
                    divAddServ.Visible = false;
                    lbladdservice.Visible = false;

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

        protected void FillAdditionalEditMode(GridView gv, UpdatePanel uppanel)
        {
            try
            {
                DataSet ds = objEditUpdateGITInformation.GetAdditionalServices(int.Parse(tourId));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    foreach (GridViewRow item in gv.Rows)
                    {
                        if (j == item.DataItemIndex)
                        {

                            TextBox txtServices = (TextBox)item.FindControl("txtServices");
                            TextBox txtDate = (TextBox)item.FindControl("txtDate");
                            TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");
                            DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                            TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                            TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");
                            TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                            TextBox txtTo = (TextBox)item.FindControl("txtTo");
                            TextBox txtPassenger = (TextBox)item.FindControl("txtNoOfPassanger");
                            CheckBox chkaditional = (CheckBox)item.FindControl("chkAditional");



                            txtServices.Text = ds.Tables[0].Rows[j]["SERVICE_DESCRIPTION"].ToString();
                            drpSupplier.Text = ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                            DataSet dsadditional = objHotelStoreProcedure.fetchComboData("GET_CHAIN_NAME_FOR_ADDITIONAL_SERVICES", "");
                            binddropdownlist(drpSupplier, dsadditional);

                            txtDate.Text = ds.Tables[0].Rows[j]["DATE"].ToString();
                            if (ds.Tables[0].Rows[j]["ADITIONAL_FLAG"].ToString() == "True")
                            {
                                chkaditional.Checked = true;
                            }

                            txtNoPax.Text = ds.Tables[0].Rows[j]["NO_OF_PAX"].ToString();
                            txtNetPrice.Text = ds.Tables[0].Rows[j]["NET_PRICE"].ToString();
                            txtSellPrice.Text = ds.Tables[0].Rows[j]["SELL_PRICE"].ToString();
                            txtFrom.Text = ds.Tables[0].Rows[j]["FROM_DETAIL"].ToString();
                            txtTo.Text = ds.Tables[0].Rows[j]["TO_DETAIL"].ToString();
                            txtPassenger.Text = ds.Tables[0].Rows[j]["NO_OF_PASSANGER"].ToString();
                        }

                    }
                    if (j < ds.Tables[0].Rows.Count - 1)
                    {
                        addServices(gv, uppanel);
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

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                addServices(gridAddServices, upAddServices);
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

        #region GENERATE QUOTE + REQUSET FOR QUOTE

        protected void btnQuoteRequest_Click(object sender, EventArgs e)
        {
            try
            {

                allValidationAtRequestForQuote();


                DataSet dtCities1 = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
                for (int j = 0; j < dtCities1.Tables[0].Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        samehotelValdation(GridHotel1);
                        sameResturantValidation(gridMeal1);
                        sameConferenceValidation(gridConf1);
                        SameGaladinnerValidation(gridGala1);
                        SameSiteSeeingValidation(gridSite1);
                        if (sameHotelFlag == false)
                        {

                            break;

                        }
                        TimeValidationForAll(gridConf1, lblConfCity1.Text);

                        if (timeforall == false)
                        {
                            break;
                        }
                        ResturantDateValidation(gridMeal1, GridHotel1, lblHotel1.Text);

                        if (MealDateFlag == true)
                        {
                            ConferenceDateValidation(gridConf1, GridHotel1, lblHotel1.Text);
                            if (Conferenceflag == true)
                            {
                                GalaDinnerDateValidation(gridGala1, GridHotel1, lblHotel1.Text);
                                if (GalaDinnerFlag == false)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (j == 1)
                    {
                        samehotelValdation(GridHotel2);
                        sameResturantValidation(gridConf2);
                        sameConferenceValidation(gridConf2);
                        SameGaladinnerValidation(gridGala2);
                        SameSiteSeeingValidation(gridSite2);
                        if (sameHotelFlag == false)
                        {
                            Master.DisplayMessage(ViewState["sameHotelMesage"].ToString(), "successMessage", 5000);
                            break;

                        }
                        TimeValidationForAll(gridConf2, lblConfCity2.Text);
                        if (timeforall == false)
                        {
                            break;
                        }
                        HotelWithTwoCity_DateValidation(GridHotel1, GridHotel2, lblHotel1.Text);
                        if (hotelDateFlag == true)
                        {
                            ResturantDateValidation(gridMeal2, GridHotel2, lblHotel2.Text);
                            if (MealDateFlag == true)
                            {
                                ConferenceDateValidation(gridConf2, GridHotel2, lblHotel2.Text);
                                if (Conferenceflag == true)
                                {
                                    GalaDinnerDateValidation(gridGala2, GridHotel2, lblHotel2.Text);
                                    if (GalaDinnerFlag == false)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }


                    }
                    else if (j == 2)
                    {

                        samehotelValdation(GridHotel3);
                        sameResturantValidation(gridConf3);
                        sameConferenceValidation(gridConf3);
                        SameGaladinnerValidation(gridGala3);
                        SameSiteSeeingValidation(gridSite3);
                        if (sameHotelFlag == false)
                        {
                            Master.DisplayMessage(ViewState["sameHotelMesage"].ToString(), "successMessage", 5000);
                            break;

                        }
                        TimeValidationForAll(gridConf3, lblConfCity3.Text);
                        if (timeforall == false)
                        {
                            break;
                        }
                        ResturantDateValidation(gridMeal3, GridHotel3, lblHotel3.Text);
                        if (MealDateFlag == true)
                        {
                            ConferenceDateValidation(gridConf3, GridHotel3, lblHotel3.Text);
                            if (Conferenceflag == true)
                            {
                                GalaDinnerDateValidation(gridGala3, GridHotel3, lblHotel3.Text);
                                if (GalaDinnerFlag == false)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                if (ischeckindateCheckoutDatecurrect == false)
                {
                    Master.DisplayMessage(ViewState["HotelValidation"].ToString(), "successMessage", 5000);
                }
                else if (MealDateFlag == false)
                {
                    Master.DisplayMessage(ViewState["ResturantDateMessage"].ToString(), "successMessage", 5000);
                }
                else if (Conferenceflag == false)
                {
                    Master.DisplayMessage(ViewState["ConferenceDateMessage"].ToString(), "successMessage", 5000);
                }
                else if (GalaDinnerFlag == false)
                {
                    Master.DisplayMessage(ViewState["GalaDinnerDateMessage"].ToString(), "successMessage", 5000);
                }
                else if (sameHotelFlag == false)
                {
                    Master.DisplayMessage(ViewState["sameHotelMesage"].ToString(), "successMessage", 5000);
                }
                else if (hotelDateFlag == false)
                {
                    if (ViewState["HotelDateMessage"] != null)
                    {
                        Master.DisplayMessage(ViewState["HotelDateMessage"].ToString(), "successMessage", 5000);
                    }
                    else
                    {
                        Master.DisplayMessage("Hotel Date Validation Fire........", "successMessage", 5000);
                    }
                }
                else if (timeforall == false)
                {
                    Master.DisplayMessage(ViewState["TimeForAll"].ToString(), "successMessage", 5000);
                }
                else
                {

                    insertTour_Quote();
                    insertTourSlabs();
                    //  insertHotelMap();
                    InsertHotelsforMap();
                    insertQuotationDetails();

                    DataTable dsSlab = (DataTable)Session["SlabID"];

                    DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
                    for (int j = 0; j < dtCities.Tables[0].Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            insertHotelDetails(GridHotel1, lblHotel1.Text, currencyName);
                            insertResturantDetails(gridMeal1, lblMealCity1.Text, currencyName);
                            insertConferenceDetails(gridConf1, lblConfCity1.Text, currencyName);
                            insertGalaDinnerDetails(gridGala1, lblGalaCity1.Text, currencyName);
                            insertSiteSeeing(gridSite1, lblSiteCity1.Text, currencyName);
                        }
                        if (j == 1)
                        {
                            insertHotelDetails(GridHotel2, lblHotel2.Text, currencyName);
                            insertResturantDetails(gridMeal2, lblMealCity2.Text, currencyName);
                            insertConferenceDetails(gridConf2, lblConfCity2.Text, currencyName);
                            insertGalaDinnerDetails(gridGala2, lblGalaCity2.Text, currencyName);
                            insertSiteSeeing(gridSite2, lblSiteCity2.Text, currencyName);
                        }
                        if (j == 2)
                        {
                            insertHotelDetails(GridHotel3, lblHotel3.Text, currencyName);
                            insertResturantDetails(gridMeal3, lblMealCity3.Text, currencyName);
                            insertConferenceDetails(gridConf3, lblConfCity3.Text, currencyName);
                            insertGalaDinnerDetails(gridGala3, lblGalaCity3.Text, currencyName);
                            insertSiteSeeing(gridSite3, lblSiteCity3.Text, currencyName);
                        }
                        if (j == 3)
                        {
                            insertHotelDetails(GridHotel4, lblHotel4.Text, currencyName);
                            insertResturantDetails(gridMeal4, lblMealCity4.Text, currencyName);
                            insertConferenceDetails(gridConf4, lblConfCity4.Text, currencyName);
                            insertGalaDinnerDetails(gridGala4, lblGalaCity4.Text, currencyName);
                            insertSiteSeeing(gridSite4, lblSiteCity4.Text, currencyName);
                        }
                        if (j == 4)
                        {
                            insertHotelDetails(GridHotel5, lblHotel5.Text, currencyName);
                            insertResturantDetails(gridMeal5, lblMealCity5.Text, currencyName);
                            insertConferenceDetails(gridConf5, lblConfCity5.Text, currencyName);
                            insertGalaDinnerDetails(gridGala5, lblGalaCity5.Text, currencyName);
                            insertSiteSeeing(gridSite5, lblSiteCity5.Text, currencyName);
                        }
                        if (j == 5)
                        {
                            insertHotelDetails(GridHotel6, lblHotel6.Text, currencyName);
                            insertResturantDetails(gridMeal6, lblMealCity6.Text, currencyName);
                            insertConferenceDetails(gridConf6, lblConfCity6.Text, currencyName);
                            insertGalaDinnerDetails(gridGala6, lblGalaCity6.Text, currencyName);
                            insertSiteSeeing(gridSite6, lblSiteCity6.Text, currencyName);
                        }
                        if (j == 6)
                        {
                            insertHotelDetails(GridHotel7, lblHotel7.Text, currencyName);
                            insertResturantDetails(gridMeal7, lblMealCity7.Text, currencyName);
                            insertConferenceDetails(gridConf7, lblConfCity7.Text, currencyName);
                            insertGalaDinnerDetails(gridGala7, lblGalaCity7.Text, currencyName);
                            insertSiteSeeing(gridSite7, lblSiteCity7.Text, currencyName);
                        }
                        if (j == 7)
                        {
                            insertHotelDetails(GridHotel8, lblHotel8.Text, currencyName);
                            insertResturantDetails(gridMeal8, lblMealCity8.Text, currencyName);
                            insertConferenceDetails(gridConf8, lblConfCity8.Text, currencyName);
                            insertGalaDinnerDetails(gridGala8, lblGalaCity8.Text, currencyName);
                            insertSiteSeeing(gridSite8, lblSiteCity8.Text, currencyName);
                        }
                        if (j == 8)
                        {
                            insertHotelDetails(GridHotel9, lblHotel9.Text, currencyName);
                            insertResturantDetails(gridMeal9, lblMealCity9.Text, currencyName);
                            insertConferenceDetails(gridConf9, lblConfCity9.Text, currencyName);
                            insertGalaDinnerDetails(gridGala9, lblGalaCity9.Text, currencyName);
                            insertSiteSeeing(gridSite9, lblSiteCity9.Text, currencyName);
                        }
                        if (j == 9)
                        {
                            insertHotelDetails(GridHotel10, lblHotel10.Text, currencyName);
                            insertResturantDetails(gridMeal10, lblMealCity10.Text, currencyName);
                            insertConferenceDetails(gridConf10, lblConfCity10.Text, currencyName);
                            insertGalaDinnerDetails(gridGala10, lblGalaCity10.Text, currencyName);
                            insertSiteSeeing(gridSite10, lblSiteCity10.Text, currencyName);
                        }


                    }

                    RadioButton rbtnTPselect = (RadioButton)GridTrasport.Rows[0].FindControl("rbtnTPselect");
                    if (rbtnTPselect.Checked == true)
                    {
                        insertTrasporPackage();
                    }
                    insertAdditionalServices();
                    //     RequestforQuoteMail();
                    

                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Quote Request Generated Successfully..')", true);

                    Response.Redirect("~/Views/GIT/AllGitPackages.aspx");
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

        protected void btnGenerateQuote_Click(object sender, EventArgs e)
        {

            try
            {


                if (tourId != "" && txtExRate.Text != "")
                {
                    objInsertGitDetails.insertExRate(int.Parse(tourId), txtMarginAmount.Text, txtExRate.Text);
                    objInsertGitDetails.GenerateQuote(tourId);
                    objInsertGitDetails.updateQuoteStatus(int.Parse(tourId), "Quoted");

                    Report(tourId);
                    btnBook.Visible = true;
                    btnDownloadQuote.Visible = true;
                    //comment temp 20-03-2014
                   // RequestforQuoteMail();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Quote Generate Save Successfully.')", true);
                }
                else
                {
                    Master.DisplayMessage("Please Enter Exchange Rate.", "successMessage", 8000);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            finally
            {
                upExRate.Update();
            }
        }

        protected void insertTour_Quote()
        {
            try
            {
                objInsertGitDetails.insertTour_Quote(0, int.Parse(Session["packgeId"].ToString()), Session["GroupName"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), int.Parse(Session["NoofRooms"].ToString()), int.Parse(Session["AgentId"].ToString()), int.Parse(Session["usersid"].ToString()), "Request for Quote", int.Parse(Session["Nights"].ToString()), Session["Adults"].ToString(), Session["CWB"].ToString(), Session["CNB"].ToString(), Session["infants"].ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }

        protected void insertTourSlabs()
        {
            try
            {
                DataTable dsSlab = (DataTable)Session["SlabID"];

                for (int i = 0; i < dsSlab.Rows.Count; i++)
                {
                    objInsertGitDetails.insertSlab(0, int.Parse(dsSlab.Rows[i]["SlabID"].ToString()), int.Parse(Session["AgentId"].ToString()));
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

        protected void insertHotelMap()
        {
            try
            {

                if (GridHotel1.Rows.Count != GridHotel2.Rows.Count)
                {
                    if (GridHotel2.Rows.Count == 0)
                    {
                        foreach (GridViewRow hotel1 in GridHotel1.Rows)
                        {
                            DropDownList drpHotel1 = (DropDownList)hotel1.FindControl("drpHotelName");
                            DropDownList drpRoomType1 = (DropDownList)hotel1.FindControl("drpRoomType");

                            objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
                        }
                    }
                    else if (GridHotel1.Rows.Count > GridHotel2.Rows.Count || GridHotel2.Rows.Count > GridHotel1.Rows.Count)
                    {
                        foreach (GridViewRow hotel1 in GridHotel1.Rows)
                        {
                            DropDownList drpHotel1 = (DropDownList)hotel1.FindControl("drpHotelName");
                            DropDownList drpRoomType1 = (DropDownList)hotel1.FindControl("drpRoomType");

                            foreach (GridViewRow hotel2 in GridHotel2.Rows)
                            {
                                DropDownList drpHotel2 = (DropDownList)hotel2.FindControl("drpHotelName");
                                DropDownList drpRoomType2 = (DropDownList)hotel2.FindControl("drpRoomType");


                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                            }
                        }
                    }


                }
                else if (GridHotel1.Rows.Count == GridHotel2.Rows.Count)
                {
                    foreach (GridViewRow hotel1 in GridHotel1.Rows)
                    {
                        DropDownList drpHotel1 = (DropDownList)hotel1.FindControl("drpHotelName");
                        DropDownList drpRoomType1 = (DropDownList)hotel1.FindControl("drpRoomType");


                        foreach (GridViewRow hotel2 in GridHotel2.Rows)
                        {
                            DropDownList drpHotel2 = (DropDownList)hotel2.FindControl("drpHotelName");
                            DropDownList drpRoomType2 = (DropDownList)hotel2.FindControl("drpRoomType");


                            objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
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

        protected void insertQuotationDetails()
        {
            try
            {
                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                DataSet dsHotelQuoteId = objGitDetail.fetchGitHotelQuoteid("FETCH_GIT_QUOTE_HOTEL_MAP_ID", int.Parse(ds.Tables[2].Rows[0]["GIT_QUOTE_ID"].ToString()));

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    for (int j = 0; j < dsHotelQuoteId.Tables[0].Rows.Count; j++)
                    {
                        objInsertGitDetails.insertQuoteDetails(0, int.Parse(ds.Tables[2].Rows[0]["GIT_QUOTE_ID"].ToString()), int.Parse(ds.Tables[1].Rows[i]["GIT_TOUR_SLAB_ID"].ToString()), int.Parse(dsHotelQuoteId.Tables[0].Rows[j]["GIT_QUOTE_HOTEL_ID"].ToString()), int.Parse(Session["AgentId"].ToString()));
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

        protected void insertAdditionalServices()
        {
            try
            {

                DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

                foreach (GridViewRow item in gridAddServices.Rows)
                {
                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");

                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");

                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");

                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");

                    TextBox txtPassanger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkAditional = (CheckBox)item.FindControl("chkAditional");

                    if (txtServices.Text != "" && drpSupplier.Text != "")
                    {
                        objInsertGitDetails.insertupdate_Additional_Services_details(0, txtServices.Text, drpSupplier.Text, txtDate.Text, int.Parse(ds.Tables[0].Rows[0]["GIT_TOUR_ID"].ToString()), int.Parse(Session["AgentId"].ToString()), txtNoPax.Text, true, txtNetPrice.Text, txtSellPrice.Text, txtFrom.Text, txtTo.Text, txtPassanger.Text, chkAditional.Checked);
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

        #region QUOTE EMAIL + PDF

        protected void RequestforQuoteMail()
        {

            DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[0][0].ToString());
            string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();

            //if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            //{

            String cc = "";

            string body = "";


            DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

            body = "Dear <br><br>Thank you for your request for quote for the Tour - <br> Your Quotation Reference no is :- " + ds.Tables[2].Rows[0]["GIT_QUOTE_ID"].ToString() + " <br>Kindly check the attached quotation as per your requirement.<br><br>Best Regards,<br>Travelz Unlimited";

            string strEmailTemplate = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();


            try
            {
                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                // string fromemail = dtemail.Rows[0]["USER_NAME"].ToString();
                string fromemail = "info@travelzunlimted.com";
                string toemail1 = Session["AgentEmail"].ToString();


                string bcc = "";

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


                //  String mail_subject = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                String mail_subject = "GIT QUOTATION"; //REFERENCE NO " + ds.Tables[2].Rows[0]["GIT_QUOTE_ID"].ToString() + " ";


                message.Subject = mail_subject;

                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = int.Parse(smtpport);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
               // client.Send(message);



                //     Master.DisplayMessage("Quote Sent To Agent Successfully.", "successMessage", 8000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GenerateQuoteMail(byte[] _file1)
        {

            DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            DataSet ds_eventName = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName.Tables[0].Rows[0][0].ToString());
            string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();

            //if (ds_mailTemplate.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            //{

            String cc = "";

            string body = "";


            DataSet ds = objGitDetail.CommonSp("GET_MAX_TOUR_ID");

            body = "Dear <br><br>Your Quote is generated for Quotation Reference no " + Request.QueryString["TOURID"].ToString() + "- <br> Kindly check the attached quotation as per your requirement.<br><br>Best Regards,<br>Travelz Unlimited";

            string strEmailTemplate = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();


            try
            {
                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                // string fromemail = dtemail.Rows[0]["USER_NAME"].ToString();
                string fromemail = "kushal@flamingotravels.co.in";
                string toemail1 = Session["AgentEmail"].ToString();


                string bcc = "";

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

                string filename = "~/Views/FIT/GITQuotes/" + tourId + "/GITQuotation.pdf";
                //  String mail_subject = ds_mailTemplate.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                String mail_subject = "GIT QUOTATION"; //REFERENCE NO " + ds.Tables[2].Rows[0]["GIT_QUOTE_ID"].ToString() + " ";


                message.Subject = mail_subject;
                message.Attachments.Add(new Attachment(new MemoryStream(_file1), "GITQUOTATION" + ".pdf"));
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = int.Parse(smtpport);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.Send(message);



                //     Master.DisplayMessage("Quote Sent To Agent Successfully.", "successMessage", 8000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Report(String QuoteID)
        {
            try
            {
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/GITQuotes/" + QuoteID + "/")))
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/GITQuotes/" + QuoteID + "/"));

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



                // quote_id = Page.Request.QueryString["QuoteId"].ToString();

                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("GIT_QUOTE_ID", QuoteID);
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/GitQuote";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();



                renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                rptViewer1.Visible = false;


                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/GITQuotes/" + QuoteID + "/GITQuotation.pdf")))
                {
                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                }

                //comment temp 20-3-2014
                //GenerateQuoteMail(renderedBytes);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }

        protected void btnDownloadQuote_Click(object sender, EventArgs e)
        {
            try
            {
                downloadFile(tourId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }

        }

        protected void downloadFile(string QuoteId)
        {
            try
            {
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "GITQuotation.pdf");


                Response.TransmitFile(HttpContext.Current.Server.MapPath("~/Views/FIT/GITQuotes/" + QuoteId + "/GITQuotation.pdf"));

                Response.End();
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

        #region HOTEL MAPPING FOR COMBITATION

        //protected void InsertHotelsforMap()
        //{
        //    try
        //    {

        //        if (GridHotel2.Rows.Count == 0)
        //        {

        //            for (int i = 0; i < GridHotel1.Rows.Count; i++)
        //            {
        //                DropDownList drpHotel1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
        //                DropDownList drpRoomType1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");
        //                TextBox txtCheckInDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
        //                TextBox txtCheckOutDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");


        //                if (i == 0)
        //                {
        //                    objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                }
        //                else
        //                {
        //                    TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
        //                    TextBox txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");
        //                    if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)))
        //                    {
        //                        objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                    }
        //                    else if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate.Text, "dd/MM/yyyy", null)))
        //                    {
        //                        objInsertGitDetails.insertSubHotels(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                    }
        //                }
        //            }
        //        }

        //        else if (GridHotel2.Rows.Count != 0)
        //        {
        //            for (int i = 0; i < GridHotel1.Rows.Count; i++)
        //            {
        //                DropDownList drpHotel1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
        //                DropDownList drpRoomType1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");
        //                TextBox txtCheckInDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
        //                TextBox txtCheckOutDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");


        //                if (i == 0)
        //                {
        //                    for (int j = 0; j < GridHotel2.Rows.Count; j++)
        //                    {
        //                        DropDownList drpHotel2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpHotelName");
        //                        DropDownList drpRoomType2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpRoomType");
        //                        TextBox txtCheckInDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckInDate");
        //                        TextBox txtCheckOutDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckOutDate");

        //                        if (j == 0)
        //                        {
        //                            objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));

        //                        }

        //                        else
        //                        {
        //                            TextBox txtPreviousCheckOutDate = new TextBox();
        //                            if (GridHotel1.Rows.Count > j)
        //                            {


        //                                i = j;
        //                                TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
        //                                txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");

        //                                TextBox txtCheckInDate1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
        //                                TextBox txtCheckOutDate1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");

        //                                TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                DropDownList drpHotel1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
        //                                DropDownList drpRoomType1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");

        //                                if ((DateTime.ParseExact(txtCheckInDate1.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                {
        //                                    objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));

        //                                    i = 0;
        //                                    //  objInsertGitDetails.insertSubHotels(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                }



        //                                else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                {
        //                                    objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    i = 0;
        //                                }
        //                                else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                {
        //                                    objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    i = 0;
        //                                }

        //                                else
        //                                {
        //                                    i = 0;
        //                                    break;
        //                                }
        //                            }

        //                            else
        //                            {
        //                                TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                {
        //                                    objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    i = 0;
        //                                }
        //                                else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                {
        //                                    objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                }
        //                                else
        //                                {
        //                                    i = 0;
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                else
        //                {

        //                    int count = i;
        //                    TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
        //                    TextBox txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");
        //                    if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)))
        //                    {
        //                        for (int j = 0; j < GridHotel2.Rows.Count; j++)
        //                        {
        //                            DropDownList drpHotel2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpHotelName");
        //                            DropDownList drpRoomType2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpRoomType");
        //                            TextBox txtCheckInDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckInDate");
        //                            TextBox txtCheckOutDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckOutDate");

        //                            if (j == 0)
        //                            {
        //                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                i = count;

        //                                //for (int k = count; k < GridHotel1.Rows.Count; k++)
        //                                //{
        //                                //    DropDownList drpHotel1_1 = (DropDownList)GridHotel1.Rows[k].FindControl("drpHotelName");
        //                                //    DropDownList drpRoomType1_1 = (DropDownList)GridHotel1.Rows[k].FindControl("drpRoomType");

        //                                //    TextBox txtCheckInDate1_1 = (TextBox)GridHotel1.Rows[k].FindControl("txtCheckInDate");
        //                                //    TextBox txtCheckOutDate1_1 = (TextBox)GridHotel1.Rows[k].FindControl("txtCheckOutDate");

        //                                //    TextBox txtPreviousCheckOutDate1_1 = (TextBox)GridHotel1.Rows[k - 1].FindControl("txtCheckOutDate");

        //                                //    if ((DateTime.ParseExact(txtCheckInDate1_1.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate1_1.Text, "dd/MM/yyyy", null)))
        //                                //    {


        //                                //        objInsertGitDetails.insertSubHotels(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                                //    }
        //                                //}
        //                            }


        //                            else
        //                            {
        //                                if (GridHotel1.Rows.Count > j)
        //                                {

        //                                    //  i = j;

        //                                    // TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
        //                                    //   TextBox txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");

        //                                    TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                    TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                    DropDownList drpHotel1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
        //                                    DropDownList drpRoomType1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");

        //                                    TextBox txtCheckInDate1_1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
        //                                    TextBox txtCheckOutDate1_1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");

        //                                    TextBox txtPreviousCheckOutDate1_1 = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");



        //                                    if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;

        //                                        //  objInsertGitDetails.insertSubHotels(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    }


        //                                    else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }
        //                                    else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }

        //                                    else
        //                                    {
        //                                        i = count;
        //                                        break;
        //                                    }


        //                                }

        //                                else
        //                                {
        //                                    TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                    TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                    if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }
        //                                    else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    }
        //                                    else
        //                                    {
        //                                        i = count;
        //                                        break;
        //                                    }
        //                                }

        //                            }
        //                            //else
        //                            //{
        //                            //    TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                            //    TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");
        //                            //    if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                            //    {
        //                            //        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                            //    }
        //                            //    else
        //                            //    {
        //                            //        break;
        //                            //    }
        //                            //}
        //                        }


        //                    }



        //                    else if ((DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate.Text, "dd/MM/yyyy", null)))
        //                    {
        //                        for (int j = 0; j < GridHotel2.Rows.Count; j++)
        //                        {
        //                            DropDownList drpHotel2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpHotelName");
        //                            DropDownList drpRoomType2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpRoomType");
        //                            TextBox txtCheckInDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckInDate");
        //                            TextBox txtCheckOutDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckOutDate");

        //                            if (j == 0)
        //                            {
        //                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                i = count;

        //                                for (int k = count; k < GridHotel1.Rows.Count; k++)
        //                                {
        //                                    DropDownList drpHotel1_1 = (DropDownList)GridHotel1.Rows[k].FindControl("drpHotelName");
        //                                    DropDownList drpRoomType1_1 = (DropDownList)GridHotel1.Rows[k].FindControl("drpRoomType");

        //                                    TextBox txtCheckInDate1_1 = (TextBox)GridHotel1.Rows[k].FindControl("txtCheckInDate");
        //                                    TextBox txtCheckOutDate1_1 = (TextBox)GridHotel1.Rows[k].FindControl("txtCheckOutDate");

        //                                    TextBox txtPreviousCheckOutDate1_1 = (TextBox)GridHotel1.Rows[k - 1].FindControl("txtCheckOutDate");

        //                                    if ((DateTime.ParseExact(txtCheckInDate1_1.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate1_1.Text, "dd/MM/yyyy", null)))
        //                                    {


        //                                        objInsertGitDetails.insertSubHotels(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                                    }
        //                                }
        //                            }


        //                            else
        //                            {
        //                                if (GridHotel1.Rows.Count > j)
        //                                {

        //                                    i = j;

        //                                    // TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
        //                                    //   TextBox txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");

        //                                    TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                    TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                    DropDownList drpHotel1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
        //                                    DropDownList drpRoomType1_1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");

        //                                    TextBox txtCheckInDate1_1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
        //                                    TextBox txtCheckOutDate1_1 = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");

        //                                    TextBox txtPreviousCheckOutDate1_1 = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");



        //                                    if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;

        //                                        //  objInsertGitDetails.insertSubHotels(0, drpHotel1_1.Text, drpRoomType1_1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                    }


        //                                    else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }
        //                                    else if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }

        //                                    else
        //                                    {
        //                                        i = count;
        //                                        break;
        //                                    }


        //                                }

        //                                else
        //                                {
        //                                    TextBox txtPreviousCheckInDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckInDate");
        //                                    TextBox txtPreviousCheckOutDate2 = (TextBox)GridHotel2.Rows[j - 1].FindControl("txtCheckOutDate");

        //                                    if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate2.Text, "dd/MM/yyyy", null)))
        //                                    {
        //                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
        //                                        i = count;
        //                                    }
        //                                    else
        //                                    {
        //                                        i = count;
        //                                        break;
        //                                    }
        //                                }

        //                            }

        //                        }


        //                    }
        //                    else if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate.Text, "dd/MM/yyyy", null)))
        //                    {
        //                        objInsertGitDetails.insertSubHotels(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //}

        #endregion

        #region Hotel Mapping
        protected void InsertHotelsforMap()
        {
            try
            {

                if (GridHotel2.Rows.Count == 0)
                {

                    for (int i = 0; i < GridHotel1.Rows.Count; i++)
                    {
                        DropDownList drpHotel1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
                        DropDownList drpRoomType1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");
                        TextBox txtCheckInDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
                        TextBox txtCheckOutDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");


                        if (i == 0)
                        {
                            objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
                        }
                        else
                        {
                            TextBox txtPreviousCheckInDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
                            TextBox txtPreviousCheckOutDate = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");
                            if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)))
                            {
                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
                            }
                            else if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtPreviousCheckOutDate.Text, "dd/MM/yyyy", null)))
                            {
                                objInsertGitDetails.insertSubHotels(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < GridHotel1.Rows.Count; i++)
                    {
                        DropDownList drpHotel1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpHotelName");
                        DropDownList drpRoomType1 = (DropDownList)GridHotel1.Rows[i].FindControl("drpRoomType");
                        TextBox txtCheckInDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckInDate");
                        TextBox txtCheckOutDate = (TextBox)GridHotel1.Rows[i].FindControl("txtCheckOutDate");

                         DropDownList drpHotel1_pre = new DropDownList();
                             DropDownList drpRoomType1_pre = new DropDownList();
                                 TextBox txtCheckInDate_pre = new TextBox();
                                 TextBox txtCheckOutDate_pre = new TextBox();
                        if (i != 0)
                        {
                             drpHotel1_pre = (DropDownList)GridHotel1.Rows[i - 1].FindControl("drpHotelName");
                            drpRoomType1_pre = (DropDownList)GridHotel1.Rows[i - 1].FindControl("drpRoomType");
                            txtCheckInDate_pre = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckInDate");
                            txtCheckOutDate_pre = (TextBox)GridHotel1.Rows[i - 1].FindControl("txtCheckOutDate");
                        }

                        if (i == 0 || DateTime.ParseExact(txtCheckInDate_pre.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null))
                        {
                            for (int j = 0; j < GridHotel2.Rows.Count; j++)
                            {
                                DropDownList drpHotel2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpHotelName");
                                DropDownList drpRoomType2 = (DropDownList)GridHotel2.Rows[j].FindControl("drpRoomType");
                                TextBox txtCheckInDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckInDate");
                                TextBox txtCheckOutDate2 = (TextBox)GridHotel2.Rows[j].FindControl("txtCheckOutDate");

                                if (j == 0)
                                {
                                    objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                                }
                                else
                                {
                                    DropDownList drpHotel2_pre = (DropDownList)GridHotel2.Rows[j-1].FindControl("drpHotelName");
                                    DropDownList drpRoomType2_pre = (DropDownList)GridHotel2.Rows[j-1].FindControl("drpRoomType");
                                    TextBox txtCheckInDate2_pre = (TextBox)GridHotel2.Rows[j-1].FindControl("txtCheckInDate");
                                    TextBox txtCheckOutDate2_pre = (TextBox)GridHotel2.Rows[j-1].FindControl("txtCheckOutDate");
                                    if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtCheckInDate2_pre.Text, "dd/MM/yyyy", null)))
                                    {
                                        if (i < GridHotel1.Rows.Count - 1)
                                        {
                                            TextBox txtchekindate_Ahead = (TextBox)GridHotel1.Rows[i + 1].FindControl("txtCheckInDate");
                                            DropDownList drpHotel1_Ahead = (DropDownList)GridHotel1.Rows[i + 1].FindControl("drpHotelName");
                                            DropDownList drpRoomType1_Ahead = (DropDownList)GridHotel1.Rows[i + 1].FindControl("drpRoomType");

                                            TextBox txtCheckOutDate_Ahead = (TextBox)GridHotel1.Rows[i + 1].FindControl("txtCheckOutDate");
                                            if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(txtchekindate_Ahead.Text, "dd/MM/yyyy", null)))
                                            {
                                                objInsertGitDetails.insertSubHotels(0, drpHotel1_Ahead.Text, drpRoomType1_Ahead.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));
                                            }
                                            if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtCheckInDate2_pre.Text, "dd/MM/yyyy", null)))
                                            {
                                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if ((DateTime.ParseExact(txtCheckInDate2.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(txtCheckInDate2_pre.Text, "dd/MM/yyyy", null)))
                                            {
                                                objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                                            }
                                            else
                                            {
                                                objInsertGitDetails.insertQuoteHotelMap(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                                            }
                                        }
                                        
                                        //break;
                                    }
                                    else
                                    {
                                        objInsertGitDetails.insertSubHotels(0, "", "", "", drpHotel2.Text, drpRoomType2.Text, lblHotel2.Text, int.Parse(Session["AgentId"].ToString()));
                                    }

                                }
                            }
                        }
                        else
                        {
                            objInsertGitDetails.insertSubHotels(0, drpHotel1.Text, drpRoomType1.Text, lblHotel1.Text, "", "", "", int.Parse(Session["AgentId"].ToString()));

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

        #endregion

        #region VALIDATION FUNCTION

        #region HOTEL

        protected void HotelValidation(GridView gv_Hotel, UpdatePanel upPanel)
        {
            DropDownList hotel = new DropDownList();
            DropDownList roomtype = new DropDownList();
            hotel = (DropDownList)gv_Hotel.Rows[0].FindControl("drpHotelName");
            roomtype = (DropDownList)gv_Hotel.Rows[0].FindControl("drpRoomType");
            DropDownList hotel0 = new DropDownList();
            DropDownList roomtype0 = new DropDownList();
            try
            {
                for (int i = 0; i < gv_Hotel.Rows.Count; i++)
                {
                    hotel = (DropDownList)gv_Hotel.Rows[i].FindControl("drpHotelName");
                    roomtype = (DropDownList)gv_Hotel.Rows[i].FindControl("drpRoomType");

                    for (int j = 1; j < gv_Hotel.Rows.Count; j++)
                    {
                        hotel0 = (DropDownList)gv_Hotel.Rows[j].FindControl("drpHotelName");
                        roomtype0 = (DropDownList)gv_Hotel.Rows[j].FindControl("drpRoomType");
                        if ((hotel0.Text == hotel.Text) && (roomtype0.Text == roomtype.Text) && (i != j))
                        {
                            roomtype0.Text = "";
                            Master.DisplayMessage("Hotel Not Allowed Same Room Type.", "successMessage", 5000);
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
                upPanel.Update();
            }
        }

        protected void HotelDateValidation(GridView gv_hotel)
        {
            for (int i = 0; i < gv_hotel.Rows.Count; i++)
            {

                DropDownList drpHotelName = (DropDownList)gv_hotel.Rows[i].FindControl("drpHotelName");
                TextBox CheckinDate = (TextBox)gv_hotel.Rows[i].FindControl("txtCheckInDate");
                TextBox CheckOutDate = (TextBox)gv_hotel.Rows[i].FindControl("txtCheckOutDate");

                if (CheckinDate.Text == "" && CheckOutDate.Text == "")
                {
                    ischeckindateCheckoutDatecurrect = false;
                    ViewState["HotelValidation"] = "Please Fill the dates for " + " " + drpHotelName.Text;
                }
                else if ((DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                {
                    ischeckindateCheckoutDatecurrect = false;
                    ViewState["HotelValidation"] = drpHotelName.Text + "CheckIn Date and CheckOut Date Should be Between Start Date and End Date.";
                }

            }
        }

        protected void SamedateinhotelValisation(GridView gv)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    foreach (GridViewRow item1 in gv.Rows)
                    {
                        if (item.DataItemIndex < item1.DataItemIndex)  //|| item.DataItemIndex != item1.DataItemIndex)
                        {

                            TextBox txtCheckInDate1 = (TextBox)item1.FindControl("txtCheckInDate");
                            TextBox txtCheckOutDate1 = (TextBox)item1.FindControl("txtCheckOutDate");
                            DropDownList drpHotelName1 = (DropDownList)item1.FindControl("drpHotelName");
                            DateTime date1 = DateTime.ParseExact(txtCheckInDate1.Text, "dd/MM/yyyy", null);
                            DateTime date2 = DateTime.ParseExact(txtCheckOutDate1.Text, "dd/MM/yyyy", null);
                            string nights = "";
                            if (date1 < date2)
                            {
                                TimeSpan ts;
                                ts = date2.Subtract(date1.Date);
                                nights = ts.TotalDays.ToString();
                            }

                            if (nights != "")
                            {
                                for (int i = 0; i < int.Parse(nights); i++)
                                {

                                    date1 = date1.AddDays(i);
                                    string date = date1.ToString("dd/MM/yyyy");
                                    if ((DateTime.ParseExact(date, "dd/MM/yyyy", null) > DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null)) && (DateTime.ParseExact(date, "dd/MM/yyyy", null) < DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null)))
                                    {
                                        ischeckindateCheckoutDatecurrect = false;
                                        ViewState["HotelValidation"] = drpHotelName1.Text + " dates Clash check in and check out date of " + drpHotelName.Text;
                                        break;
                                    }
                                }



                            }
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

        protected void DateValidationofHotel(GridView gv, String city, UpdatePanel uppanel)
        {
            try
            {

                GetMinimumDate(gv, city);
                GetMaximumDate(gv, city);

                for (int i = 0; i < gv.Rows.Count; i++)
                {

                    TextBox txtCheckInDate = (TextBox)gv.Rows[i].FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)gv.Rows[i].FindControl("txtCheckOutDate");

                    DropDownList drpHotelName = (DropDownList)gv.Rows[i].FindControl("drpHotelName");
                    DropDownList drpRoomType = (DropDownList)gv.Rows[i].FindControl("drpRoomType");

                    if (i == 0)
                    {

                        if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null)))
                        {
                            //  txtCheckInDate.Text = ViewState["minDate"].ToString();
                            ViewState["HotelDateMessage"] = drpHotelName.Text + "'s Check in date is not valid at ROW" + " " + (i + 1);
                            hotelDateFlag = false;
                        }

                    }
                    else
                    {
                        if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null)))
                        {
                            TextBox txtPreviousCheckOutDate = (TextBox)gv.Rows[i - 1].FindControl("txtCheckOutDate");
                            if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(txtPreviousCheckOutDate.Text, "dd/MM/yyyy", null)))
                            {
                                //  txtCheckInDate.Text = txtPreviousCheckOutDate.Text;

                                ViewState["HotelDateMessage"] = drpHotelName.Text + "'s Check in date is not valid at ROW" + " " + (i + 1);
                                hotelDateFlag = false;
                            }
                        }

                    }


                    if (i + 1 != gv.Rows.Count)
                    {

                        if ((DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                        {
                            TextBox txtPreviousCheckInDate = (TextBox)gv.Rows[i + 1].FindControl("txtCheckInDate");
                            if ((DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(txtPreviousCheckInDate.Text, "dd/MM/yyyy", null)))
                            {
                                // txtCheckOutDate.Text = txtPreviousCheckInDate.Text;

                                ViewState["HotelDateMessage"] = drpHotelName.Text + "'s Check out date is not valid at ROW" + " " + (i + 1);
                                hotelDateFlag = false;
                            }
                        }

                    }
                    else
                    {
                        if ((DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                        {
                            // txtCheckOutDate.Text = ViewState["maxDate"].ToString();

                            ViewState["HotelDateMessage"] = drpHotelName.Text + "'s Check out date is not valid at ROW" + " " + (i + 1);
                            hotelDateFlag = false;
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

        protected void samehotelValdation(GridView gv)
        {

            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                //  int j = gv.Rows.Count;

                DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");

                foreach (GridViewRow item1 in gv.Rows)
                {
                    DropDownList drpHotelName1 = (DropDownList)item1.FindControl("drpHotelName");
                    DropDownList drpRoomType1 = (DropDownList)item1.FindControl("drpRoomType");
                    TextBox txtCheckInDate1 = (TextBox)item1.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate1 = (TextBox)item1.FindControl("txtCheckOutDate");

                    int j = item1.DataItemIndex;

                    if (i != j)
                    {
                        if (drpHotelName.Text == drpHotelName1.Text && drpRoomType.Text == drpRoomType1.Text && DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtCheckInDate1.Text, "dd/MM/yyyy", null) && DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtCheckOutDate1.Text, "dd/MM/yyyy", null))
                        {
                            sameHotelFlag = false;
                            ViewState["sameHotelMesage"] = drpHotelName.Text + " 's  Hotel has same entry more then once with same dates with same room type. ";
                            break;
                        }
                    }
                }
                if (sameHotelFlag == false)
                {
                    break;
                }
            }


        }

        protected void HotelWithTwoCity_DateValidation(GridView gvHotel, GridView gvHotel2, String city)
        {
            GetMinimumDate(gvHotel, city);
            GetMaximumDate(gvHotel, city);

            foreach (GridViewRow item in gvHotel2.Rows)
            {
                int i = item.DataItemIndex;
                DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");

                if (i == 0)
                {
                    if (drpHotelName.Text != "" && txtCheckInDate.Text != "")
                    {
                        if ((DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) != DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                        {
                            ViewState["HotelDateMessage"] = drpHotelName.Text + "'s Check in date is not valid at ROW" + " " + i;
                            hotelDateFlag = false;
                        }

                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        protected void checkindatecheckoutdatevalidation(GridView gv_hotel)
        {
            System.DateTime StartDate = new DateTime();
            System.DateTime EndDate = new DateTime();
            StartDate = DateTime.ParseExact(startdate, "dd/MM/yyyy", null);
            EndDate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null);
            TextBox CheckinDate = new TextBox();
            TextBox CheckOutDate = new TextBox();
            System.DateTime MinInDate = new DateTime();
            System.DateTime MaxoutDate = new DateTime();
            CheckinDate = (TextBox)gv_hotel.Rows[0].FindControl("txtCheckInDate");
            CheckOutDate = (TextBox)gv_hotel.Rows[0].FindControl("txtCheckOutDate");
            MinInDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
            MaxoutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
            DropDownList drpHotelName = new DropDownList();
            for (int i = 0; i < gv_hotel.Rows.Count; i++)
            {

                drpHotelName = (DropDownList)gv_hotel.Rows[i].FindControl("drpHotelName");
                CheckinDate = (TextBox)gv_hotel.Rows[i].FindControl("txtCheckInDate");
                CheckOutDate = (TextBox)gv_hotel.Rows[i].FindControl("txtCheckOutDate");
                System.DateTime InDate = new DateTime();
                System.DateTime OutDate = new DateTime();
                if (CheckinDate.Text != "" && CheckOutDate.Text != "")
                {
                    InDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                    OutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
                }
                else
                {
                    Master.DisplayMessage("Plase Fill all checkin check out dates..", "successMessage", 5000);
                    break;

                }

                if (MinInDate > InDate)
                {
                    MinInDate = InDate;
                }
                if (MaxoutDate < OutDate)
                {
                    MaxoutDate = OutDate;
                }

            }


            if (CheckinDate.Text != "" && CheckOutDate.Text != "")
            {
                if ((StartDate <= MinInDate) && (MinInDate <= EndDate) && (StartDate <= MaxoutDate) && (MaxoutDate <= EndDate))
                {

                    ischeckindateCheckoutDatecurrect = true;
                }
                else
                {
                    ischeckindateCheckoutDatecurrect = false;
                    ViewState["HotelValidation"] = drpHotelName.Text + "CheckIn Date and CheckOut Date Should be Between Start Date and End Date.";
                    Master.DisplayMessage("CheckIn Date and CheckOut Date Should be Between Start Date and End Date .", "successMessage", 5000);

                }
            }

        }

        #endregion

        #region MELAS

        protected void MealDateValidation(GridView gv_meal)
        {
            TextBox mealdate = new TextBox();
            TextBox mealdate0 = new TextBox();
            mealdate = (TextBox)gv_meal.Rows[0].FindControl("txtDate");

            try
            {
                for (int i = 1; i < gv_meal.Rows.Count; i++)
                {
                    mealdate = (TextBox)gv_meal.Rows[i].FindControl("txtDate");
                    for (int j = 0; j < gv_meal.Rows.Count; j++)
                    {
                        mealdate0 = (TextBox)gv_meal.Rows[j].FindControl("txtDate");

                        if ((mealdate0.Text == mealdate.Text) && (i != j))
                        {
                            Master.DisplayMessage("Meal Not Allowed Same Date.", "successMessage", 5000);
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

        protected void MealValidation(GridView gv_Hotel, GridView gv_Meal)
        {
            System.DateTime MinInDate = new DateTime();
            System.DateTime MaxoutDate = new DateTime();
            System.DateTime MealDt = new DateTime();
            TextBox CheckinDate = new TextBox();
            TextBox CheckOutDate = new TextBox();
            TextBox MealDate = new TextBox();
            System.DateTime MaxMealDt = new DateTime();
            try
            {

                CheckinDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckInDate");
                CheckOutDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckOutDate");
                MinInDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                MaxoutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
                for (int i = 0; i < gv_Hotel.Rows.Count; i++)
                {
                    CheckinDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckInDate");
                    CheckOutDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckOutDate");
                    System.DateTime InDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                    System.DateTime OutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);

                    if (MinInDate > InDate)
                    {
                        MinInDate = InDate;
                    }
                    if (MaxoutDate < OutDate)
                    {
                        MaxoutDate = OutDate;
                    }

                }
                for (int j = 0; j < gv_Meal.Rows.Count; j++)
                {
                    MealDate = (TextBox)gv_Meal.Rows[j].FindControl("txtDate");
                    MealDt = DateTime.ParseExact(MealDate.Text, "dd/MM/yyyy", null);
                    if (MaxMealDt < MealDt)
                    {
                        MaxMealDt = MealDt;
                    }
                }
                if (CheckinDate.Text != "" && CheckOutDate.Text != "")
                {
                    if ((MinInDate <= MealDt) && (MealDt <= MaxoutDate))
                    {

                    }
                    else
                    {
                        MealDateFlag = false;
                        Master.DisplayMessage("Meal Date Should be Between CheckIn Date and CheckOut Date.", "successMessage", 5000);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal1.Update();
            }
        }

        protected void ResturantDateValidation(GridView gvResturant, GridView gvHotel, String city)
        {
            GetMinimumDate(gvHotel, city);
            GetMaximumDate(gvHotel, city);

            foreach (GridViewRow item in gvResturant.Rows)
            {
                DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");

                TextBox txtDate = (TextBox)item.FindControl("txtDate");

                if (drpResturant.Text != "")
                {
                    if (drpMealType.Text == "")
                    {
                        ViewState["ResturantDateMessage"] = "Please select Meal type for" + " " + drpResturant.Text;
                        MealDateFlag = false;
                        break;
                    }
                    else if (txtDate.Text == "")
                    {
                        ViewState["ResturantDateMessage"] = "Please select Meal date for" + " " + drpResturant.Text;
                        MealDateFlag = false;
                        break;
                    }
                    else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                    {

                        ViewState["ResturantDateMessage"] = drpResturant.Text + "'s date is between check in and check out date of" + " " + city;
                        MealDateFlag = false;
                        break;
                    }
                }
            }


        }

        protected void sameResturantValidation(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                //  int j = gv.Rows.Count;
                DropDownList drpResturant = (DropDownList)item.FindControl("drpResturant");
                DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");
                CheckBox chkMeal = (CheckBox)item.FindControl("chkMeal");
                TextBox txtDate = (TextBox)item.FindControl("txtDate");

                foreach (GridViewRow item1 in gv.Rows)
                {
                    int j = item1.DataItemIndex;
                    DropDownList drpResturant1 = (DropDownList)item1.FindControl("drpResturant");
                    DropDownList drpMealType1 = (DropDownList)item1.FindControl("drpMealType");

                    TextBox txtDate1 = (TextBox)item1.FindControl("txtDate");

                    if (i != j)
                    {
                        if (drpResturant.Text == drpResturant1.Text && drpMealType.Text == drpMealType1.Text && DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null))
                        {
                            sameHotelFlag = false;
                            ViewState["sameHotelMesage"] = drpResturant1.Text + " 's  Resturant has same entry more then once with same date and meal type. ";
                            break;
                        }
                    }
                }
                if (sameHotelFlag == false)
                {
                    break;
                }
            }
        }

        protected void MmealDateValidation(GridView gv)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {

                DropDownList drpResturant = (DropDownList)gv.Rows[i].FindControl("drpResturant");
                TextBox txtDate = (TextBox)gv.Rows[i].FindControl("txtDate");
                // TextBox CheckOutDate = (TextBox)gv.Rows[i].FindControl("txtCheckOutDate");
                if (drpResturant.Text != "")
                {
                    if (txtDate.Text == "")
                    {
                        MealDateFlag = false;
                        ViewState["MealValidation"] = "Please enter date for " + drpResturant.Text;
                    }
                    else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
                    {
                        MealDateFlag = false;
                        ViewState["MealValidation"] = drpResturant.Text + " " + "CheckIn Date and CheckOut Date Should be Between Start Date and End Date.";
                    }
                }

            }
        }

        #endregion

        #region CONFERENCE

        protected void ConfDateValidation(GridView gv_conf)
        {
            TextBox confdate = new TextBox();
            TextBox confdate0 = new TextBox();
            confdate = (TextBox)gv_conf.Rows[0].FindControl("txtDate");

            try
            {
                for (int i = 1; i < gv_conf.Rows.Count; i++)
                {
                    confdate = (TextBox)gv_conf.Rows[i].FindControl("txtDate");
                    for (int j = 0; j < gv_conf.Rows.Count; j++)
                    {
                        confdate0 = (TextBox)gv_conf.Rows[j].FindControl("txtDate");

                        if ((confdate0.Text == confdate.Text) && (i != j))
                        {
                            Master.DisplayMessage("Conference Not Allowed Same Date.", "successMessage", 5000);
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

        protected void ConfValidation(GridView gv_Hotel, GridView gv_Conf)
        {
            System.DateTime MinInDate = new DateTime();
            System.DateTime MaxoutDate = new DateTime();
            System.DateTime MealDt = new DateTime();
            TextBox CheckinDate = new TextBox();
            TextBox CheckOutDate = new TextBox();
            TextBox ConfDate = new TextBox();
            System.DateTime MaxConfDt = new DateTime();
            try
            {

                CheckinDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckInDate");
                CheckOutDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckOutDate");
                MinInDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                MaxoutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
                for (int i = 0; i < gv_Hotel.Rows.Count; i++)
                {
                    CheckinDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckInDate");
                    CheckOutDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckOutDate");
                    System.DateTime InDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                    System.DateTime OutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);

                    if (MinInDate > InDate)
                    {
                        MinInDate = InDate;
                    }
                    if (MaxoutDate < OutDate)
                    {
                        MaxoutDate = OutDate;
                    }

                }
                for (int j = 0; j < gv_Conf.Rows.Count; j++)
                {
                    ConfDate = (TextBox)gv_Conf.Rows[j].FindControl("txtDate");
                    MealDt = DateTime.ParseExact(ConfDate.Text, "dd/MM/yyyy", null);
                    if (MaxConfDt < MealDt)
                    {
                        MaxConfDt = MealDt;
                    }
                }
                if (CheckinDate.Text != "" && CheckOutDate.Text != "")
                {
                    if ((MinInDate <= MealDt) && (MealDt <= MaxoutDate))
                    {

                    }
                    else
                    {
                        Master.DisplayMessage("Conference Date Should be Between CheckIn Date and CheckOut Date.", "successMessage", 5000);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal1.Update();
            }
        }

        protected void ConferenceDateValidation(GridView gvConference, GridView gvHotel, String city)
        {
            GetMinimumDate(gvHotel, city);
            GetMaximumDate(gvHotel, city);

            foreach (GridViewRow item in gvConference.Rows)
            {
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                if (drpHotel.Text != "")
                {
                    if (drpConfType.Text == "")
                    {
                        ViewState["ConferenceDateMessage"] = "Please select Conference type for" + " " + drpHotel.Text;
                        MealDateFlag = false;
                        break;
                    }
                    else if (txtDate.Text == "")
                    {
                        ViewState["ConferenceDateMessage"] = "Please select Conference date for" + " " + drpHotel.Text;
                        MealDateFlag = false;
                        break;
                    }
                    if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                    {

                        ViewState["ConferenceDateMessage"] = drpHotel.Text + "'s date for Conference type" + " " + drpConfType.Text + " is between check in and check out date of" + " " + city;
                        Conferenceflag = false;
                        break;
                    }
                }
            }


        }

        protected void sameConferenceValidation(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                //  int j = gv.Rows.Count;


                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");

                foreach (GridViewRow item1 in gv.Rows)
                {
                    int j = item1.DataItemIndex;
                    TextBox txtDate1 = (TextBox)item1.FindControl("txtDate");
                    DropDownList drpConfType1 = (DropDownList)item1.FindControl("drpConfType");
                    DropDownList drpHotel1 = (DropDownList)item1.FindControl("drpHotel");

                    if (i != j)
                    {
                        if (drpHotel.Text == drpHotel1.Text && drpConfType.Text == drpConfType1.Text && DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null))
                        {
                            sameHotelFlag = false;
                            ViewState["sameHotelMesage"] = drpHotel.Text + " 's  Conference has same entry more then once with same dates with same room type. ";
                            break;
                        }
                    }
                }

                if (sameHotelFlag == false)
                {
                    break;
                }
            }
        }

        protected void TimeValidationForAll(GridView gv, string City_Name)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                if (drpHotel.Text != "" && drpConfType.Text != "")
                {
                    if (txttime.SelectedDate == null)
                    {
                        ViewState["TimeForAll"] = "Please select Time in Conference";
                        timeforall = false;
                    }
                }
            }
        }

        #endregion

        #region GALA DINNER

        protected void GalaDinnerValidation(GridView gv_Hotel, GridView gv_GalaDinner)
        {


            System.DateTime MinInDate = new DateTime();
            System.DateTime MaxoutDate = new DateTime();
            System.DateTime GalaDinnerDt = new DateTime();
            TextBox CheckinDate = new TextBox();
            TextBox CheckOutDate = new TextBox();
            TextBox GalaDinnerDate = new TextBox();
            System.DateTime MaxGalaDinnerDt = new DateTime();
            try
            {

                CheckinDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckInDate");
                CheckOutDate = (TextBox)gv_Hotel.Rows[0].FindControl("txtCheckOutDate");
                MinInDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                MaxoutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
                for (int i = 0; i < gv_Hotel.Rows.Count; i++)
                {
                    CheckinDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckInDate");
                    CheckOutDate = (TextBox)gv_Hotel.Rows[i].FindControl("txtCheckOutDate");
                    System.DateTime InDate = DateTime.ParseExact(CheckinDate.Text, "dd/MM/yyyy", null);
                    System.DateTime OutDate = DateTime.ParseExact(CheckOutDate.Text, "dd/MM/yyyy", null);
                    // GalaDinnerDate = (TextBox)gv_GalaDinner.Rows[i].FindControl("txtDate");
                    // GalaDinnerDt = DateTime.ParseExact(GalaDinnerDate.Text, "dd/MM/yyyy", null);
                    if (MinInDate > InDate)
                    {
                        MinInDate = InDate;
                    }
                    if (MaxoutDate < OutDate)
                    {
                        MaxoutDate = OutDate;
                    }

                }
                for (int j = 0; j < gv_GalaDinner.Rows.Count; j++)
                {
                    GalaDinnerDate = (TextBox)gv_GalaDinner.Rows[j].FindControl("txtDate");
                    GalaDinnerDt = DateTime.ParseExact(GalaDinnerDate.Text, "dd/MM/yyyy", null);
                    if (MaxGalaDinnerDt < GalaDinnerDt)
                    {
                        MaxGalaDinnerDt = GalaDinnerDt;
                    }
                }
                if (CheckinDate.Text != "" && CheckOutDate.Text != "")
                {
                    if ((MinInDate <= GalaDinnerDt) && (GalaDinnerDt <= MaxoutDate))
                    {

                    }
                    else
                    {
                        GalaDinnerFlag = false;
                        ViewState["ValidationMeassage"] = "Gala Dinner Date Should be Between CheckIn Date and CheckOut Date.";
                        Master.DisplayMessage("Gala Dinner Date Should be Between CheckIn Date and CheckOut Date.", "successMessage", 5000);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upMeal1.Update();
            }
        }

        protected void GalaDinnerDateValidation(GridView gvGala, GridView gvHotel, String city)
        {
            GetMinimumDate(gvHotel, city);
            GetMaximumDate(gvHotel, city);

            foreach (GridViewRow item in gvGala.Rows)
            {
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                if (drpHotel.Text != "")
                {
                    if (drpGalaType.Text == "")
                    {
                        ViewState["GalaDinnerDateMessage"] = "Please select Gala Dinner type for" + " " + drpHotel.Text;
                        MealDateFlag = false;
                        break;
                    }
                    else if (txtDate.Text == "")
                    {
                        ViewState["GalaDinnerDateMessage"] = "Please select Gala Dinner date for" + " " + drpHotel.Text;
                        MealDateFlag = false;
                        break;
                    }
                    if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null)))
                    {

                        ViewState["GalaDinnerDateMessage"] = drpHotel.Text + "'s date for Gala Dinner type" + " " + drpGalaType.Text + " is between check in and check out date of" + " " + city;
                        GalaDinnerFlag = false;
                        break;
                    }
                }
            }


        }

        protected void SameGaladinnerValidation(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                //  int j = gv.Rows.Count;

                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");


                foreach (GridViewRow item1 in gv.Rows)
                {
                    int j = item1.DataItemIndex;
                    TextBox txtDate1 = (TextBox)item1.FindControl("txtDate");
                    DropDownList drpGalaType1 = (DropDownList)item1.FindControl("drpGalaType");
                    DropDownList drpHotel1 = (DropDownList)item1.FindControl("drpHotel");

                    if (i != j)
                    {
                        if (drpHotel.Text == drpHotel1.Text && drpGalaType.Text == drpGalaType1.Text && DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null))
                        {
                            sameHotelFlag = false;
                            ViewState["sameHotelMesage"] = drpHotel.Text + " 's  Gala Dinner has same entry more then once with same dates with same room type. ";
                            break;
                        }
                    }
                }
                if (sameHotelFlag == false)
                {
                    break;
                }

            }
        }

        #endregion

        #region SITE SEEING

        protected void SiteSeeingValidation(GridView gv_SiteSeeing, UpdatePanel up_site)
        {
            DropDownList drpSiteSeeing = new DropDownList();
            DropDownList drpSiteSeeing1 = new DropDownList();
            string site;
            string site1;
            try
            {

                for (int i = 0; i < gv_SiteSeeing.Rows.Count; i++)
                {
                    drpSiteSeeing = (DropDownList)gv_SiteSeeing.Rows[i].FindControl("drpSiteDetails");
                    site = drpSiteSeeing.Text;
                    for (int j = i+1; j < gv_SiteSeeing.Rows.Count; j++)
                    {
                        drpSiteSeeing1 = (DropDownList)gv_SiteSeeing.Rows[j].FindControl("drpSiteDetails");
                        site1 = drpSiteSeeing1.Text;

                        if (site == site1 && (i != j))
                        {
                            Master.DisplayMessage("Same Siteseeing Not Allowed.", "successMessage", 5000);
                            drpSiteSeeing1.Text = "";
                            up_site.Update();
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

        protected void SameSiteSeeingValidation(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                //    int j = gv.Rows.Count;

                DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");

                foreach (GridViewRow item1 in gv.Rows)
                {
                    int j = item1.DataItemIndex;
                    DropDownList drpSiteDetails1 = (DropDownList)item1.FindControl("drpSiteDetails");


                    if (i != j)
                    {
                        if (drpSiteDetails.Text == drpSiteDetails1.Text)
                        {
                            sameHotelFlag = false;
                            ViewState["sameHotelMesage"] = drpSiteDetails.Text + " Site Seeing selected multiple times.";
                            break;
                        }
                    }
                }

                if (sameHotelFlag == false)
                {
                    break;
                }
            }
        }

        #endregion

        #region COMMON

        protected void allValidationAtRequestForQuote()
        {
            try
            {

                DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
                for (int j = 0; j < dtCities.Tables[0].Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        DateValidationofHotel(GridHotel1, lblHotel1.Text, upHotel1);

                        HotelDateValidation(GridHotel1);
                        MmealDateValidation(gridMeal1);
                        //  ConferenceDateValidation(gridConf1);
                        //  GalaDiinerDateValidation(gridGala1);

                    }
                    if (j == 1)
                    {
                        DateValidationofHotel(GridHotel2, lblHotel2.Text, upHotel2);

                        HotelDateValidation(GridHotel2);
                        MmealDateValidation(gridMeal2);
                        //   ConferenceDateValidation(gridConf2);
                        //   GalaDiinerDateValidation(gridGala2);

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

        protected void Validation(GridView gv, String city, int Index, GridView firstcitygv, String firstcity)
        {
            try
            {

                GetMinimumDate(firstcitygv, firstcity);
                GetMaximumDate(firstcitygv, firstcity);
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                        if (Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(ViewState["minDate"].ToString()) && Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(ViewState["maxdate"].ToString()))
                        {

                        }
                        else
                        {
                            Master.DisplayMessage("Chack-In Date of " + city + " should not between chackin and checkout date of " + firstcity, "successMessage", 8000);
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

        #endregion

        #region NORMAL USE FUNCTIONS

        protected void txtCheckInDate2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtdate = sender as TextBox;
                int repeaterItemIndex = ((GridViewRow)txtdate.NamingContainer).DataItemIndex;
                //  Validation(GridHotel2, lblHotel2.Text, repeaterItemIndex, GridHotel1, lblHotel1.Text);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel2.Update();
            }
        }

        protected void FillNoOfNights(GridView gv, String city, int Index)
        {
            try
            {

                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                        TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                        TextBox txtNights = (TextBox)item.FindControl("txtNights");
                        if (txtCheckInDate.Text != "")
                        {

                            DateTime date1 = DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null);
                            DateTime date2 = DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null);
                            if (date1 < date2)
                            {
                                TimeSpan ts;
                                ts = date2.Subtract(date1.Date);
                                txtNights.Text = ts.TotalDays.ToString();
                            }
                            else
                            {
                                Master.DisplayMessage("Chack-In should not be larger than Chackout date", "successMessage", 8000);
                            }
                        }
                        else
                        {
                            Master.DisplayMessage("Chack-In date Should not be blank", "successMessage", 8000);
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

        protected void GetMinimumDate(GridView gv, String city)
        {
            try
            {

                ArrayList list = new ArrayList();
                foreach (GridViewRow item in gv.Rows)
                {
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    if (item.DataItemIndex == 0)
                    {
                        ViewState["minDate"] = txtCheckInDate.Text;
                    }
                    else
                    {
                        if (DateTime.ParseExact(txtCheckInDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ViewState["minDate"].ToString(), "dd/MM/yyyy", null))
                        {
                            ViewState["minDate"] = txtCheckInDate.Text;
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

        public void GetMaximumDate(GridView gv, String city)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    if (item.DataItemIndex == 0)
                    {
                        ViewState["maxDate"] = txtCheckOutDate.Text;
                    }
                    else
                    {
                        if (DateTime.ParseExact(txtCheckOutDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ViewState["maxDate"].ToString(), "dd/MM/yyyy", null))
                        {
                            ViewState["maxDate"] = txtCheckOutDate.Text;
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

        #region TO BE RECONFIRMED STAGE
        protected void btnConfPaymentDate_Click(object sender, EventArgs e)
        {
            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
            ViewState["Paymentmessage"] = null;
            ViewState["blank"] = null;
            for (int j = 0; j < dtCities.Tables[0].Rows.Count; j++)
            {
                if (j == 0)
                {
                    PaymentDateValidation(gridConf1, lblConfCity1.Text, "Conference");
                    PaymentDateValidation(gridGala1, lblGalaCity1.Text, "GalaDinner");
                    PaymentDateValidation(gridSite1, lblSiteCity1.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf1, lblConfCity1.Text);
                        insertGalaDinnerpaymentDate(gridGala1, lblGalaCity1.Text);
                        insertsitepaymentDate(gridSite1, lblSiteCity1.Text);
                    }


                }
                if (j == 1)
                {
                    PaymentDateValidation(gridConf2, lblConfCity2.Text, "Conference");
                    PaymentDateValidation(gridGala2, lblGalaCity2.Text, "GalaDinner");
                    PaymentDateValidation(gridSite2, lblSiteCity2.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf2, lblConfCity2.Text);
                        insertGalaDinnerpaymentDate(gridGala2, lblGalaCity2.Text);
                        insertsitepaymentDate(gridSite2, lblSiteCity2.Text);
                    }

                }
                if (j == 2)
                {
                    PaymentDateValidation(gridConf3, lblConfCity3.Text, "Conference");
                    PaymentDateValidation(gridGala3, lblGalaCity3.Text, "GalaDinner");
                    PaymentDateValidation(gridSite3, lblSiteCity3.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf3, lblConfCity3.Text);
                        insertGalaDinnerpaymentDate(gridGala3, lblGalaCity3.Text);
                        insertsitepaymentDate(gridSite3, lblSiteCity3.Text);
                    }

                }
                if (j == 3)
                {
                    PaymentDateValidation(gridConf4, lblConfCity4.Text, "Conference");
                    PaymentDateValidation(gridGala4, lblGalaCity4.Text, "GalaDinner");
                    PaymentDateValidation(gridSite4, lblSiteCity4.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf4, lblConfCity4.Text);
                        insertGalaDinnerpaymentDate(gridGala4, lblGalaCity4.Text);
                        insertsitepaymentDate(gridSite4, lblSiteCity4.Text);
                    }

                }
                if (j == 4)
                {
                    PaymentDateValidation(gridConf5, lblConfCity5.Text, "Conference");
                    PaymentDateValidation(gridGala5, lblGalaCity5.Text, "GalaDinner");
                    PaymentDateValidation(gridSite5, lblSiteCity5.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf5, lblConfCity5.Text);
                        insertGalaDinnerpaymentDate(gridGala5, lblGalaCity5.Text);
                        insertsitepaymentDate(gridSite5, lblSiteCity5.Text);
                    }

                }
                if (j == 5)
                {
                    PaymentDateValidation(gridConf6, lblConfCity6.Text, "Conference");
                    PaymentDateValidation(gridGala6, lblGalaCity6.Text, "GalaDinner");
                    PaymentDateValidation(gridSite6, lblSiteCity6.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf6, lblConfCity6.Text);
                        insertGalaDinnerpaymentDate(gridGala6, lblGalaCity6.Text);
                        insertsitepaymentDate(gridSite6, lblSiteCity6.Text);
                    }

                }
                if (j == 6)
                {
                    PaymentDateValidation(gridConf7, lblConfCity7.Text, "Conference");
                    PaymentDateValidation(gridGala7, lblGalaCity7.Text, "GalaDinner");
                    PaymentDateValidation(gridSite7, lblSiteCity7.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf7, lblConfCity7.Text);
                        insertGalaDinnerpaymentDate(gridGala7, lblGalaCity7.Text);
                        insertsitepaymentDate(gridSite7, lblSiteCity7.Text);
                    }

                }
                if (j == 7)
                {
                    PaymentDateValidation(gridConf8, lblConfCity8.Text, "Conference");
                    PaymentDateValidation(gridGala8, lblGalaCity8.Text, "GalaDinner");
                    PaymentDateValidation(gridSite8, lblSiteCity8.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf8, lblConfCity8.Text);
                        insertGalaDinnerpaymentDate(gridGala8, lblGalaCity8.Text);
                        insertsitepaymentDate(gridSite8, lblSiteCity8.Text);
                    }
                }
                if (j == 8)
                {
                    PaymentDateValidation(gridConf9, lblConfCity9.Text, "Conference");
                    PaymentDateValidation(gridGala9, lblGalaCity9.Text, "GalaDinner");
                    PaymentDateValidation(gridSite9, lblSiteCity9.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf9, lblConfCity9.Text);
                        insertGalaDinnerpaymentDate(gridGala9, lblGalaCity9.Text);
                        insertsitepaymentDate(gridSite9, lblSiteCity9.Text);
                    }
                }
                if (j == 9)
                {
                    PaymentDateValidation(gridConf10, lblConfCity10.Text, "Conference");
                    PaymentDateValidation(gridGala10, lblGalaCity10.Text, "GalaDinner");
                    PaymentDateValidation(gridSite10, lblSiteCity10.Text, "Siteseeing");
                    if (ViewState["Paymentmessage"] == null)
                    {
                        insertConferencepaymentDate(gridConf10, lblConfCity10.Text);
                        insertGalaDinnerpaymentDate(gridGala10, lblGalaCity10.Text);
                        insertsitepaymentDate(gridSite10, lblSiteCity10.Text);
                    }
                }

            }
            if (ViewState["blank"] == null)
            {
                if (ViewState["Paymentmessage"] == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Payment date save successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('" + ViewState["Paymentmessage"].ToString() + "')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('" + ViewState["blank"].ToString() + "')", true);
            }
        }

        protected void insertConferencepaymentDate(GridView gv, string City_Name)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                DropDownList drpConfType = (DropDownList)item.FindControl("drpConfType");
                TextBox txtconfpaymentdate = (TextBox)item.FindControl("txtReconfirmedDate");

                if (txtconfpaymentdate.Visible == true)
                {
                    if (txtconfpaymentdate.Text != "")
                    {
                        objTobereconform.InsertConferencePaymentdate(int.Parse(Request.QueryString["TOURID"].ToString()), txtconfpaymentdate.Text, drpHotel.Text, drpConfType.Text, City_Name);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Payment date save successfully.')", true);
                    }
                    else
                    {
                        ViewState["blank"] = "Please Enter Payment date in Conference.";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Payment date in Conference.')", true);
                    }
                }
            }

        }

        protected void insertGalaDinnerpaymentDate(GridView gv, string City_Name)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotel");
                DropDownList drpGalaType = (DropDownList)item.FindControl("drpGalaType");
                TextBox txtdinnerpaymentdate = (TextBox)item.FindControl("txtReconfirmedDate");

                if (txtdinnerpaymentdate.Visible == true)
                {
                    if (txtdinnerpaymentdate.Text != "")
                    {
                        objTobereconform.InsertGalaDinnerPaymentdate(int.Parse(Request.QueryString["TOURID"].ToString()), txtdinnerpaymentdate.Text, drpHotel.Text, drpGalaType.Text, City_Name);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Payment date save successfully.')", true);
                    }
                    else
                    {
                        ViewState["blank"] = "Please Enter Payment date in Galadinner.";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Payment date in Galadinner.')", true);
                    }
                }
            }

        }

        protected void insertsitepaymentDate(GridView gv, string City_Name)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                DropDownList drpSiteDetails = (DropDownList)item.FindControl("drpSiteDetails");
                TextBox txtSitepaymentdate = (TextBox)item.FindControl("txtReconfirmedDate");

                if (txtSitepaymentdate.Visible == true)
                {
                    if (txtSitepaymentdate.Text != "")
                    {
                        objTobereconform.InsertSitePaymentdate(int.Parse(Request.QueryString["TOURID"].ToString()), txtSitepaymentdate.Text, drpSiteDetails.Text);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Payment date save successfully.')", true);
                    }
                    else
                    {
                        ViewState["blank"] = "Please Enter Payment date in Siteseeing.";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Payment date in Siteseeing.')", true);
                    }
                }
            }

        }

        protected void btnEnterFlightDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/FlightDetails.aspx?TOURID=" + tourId);
        }

        #region Reconfirm Click

        protected void btnReconform_Click(object sender, EventArgs e)
        {
            
            //int count = Convert.ToInt32(dtFlightDetails.Tables[0].Rows[0]["COUNT"].ToString());
            //if (count == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Flight Details.')", true);
            //}
            //else
            //{
            int Tourid = Convert.ToInt32(tourId);
            DataSet dsslab = objGitDetail.fetchSlabAndCount("GET_SLAB_FROM_GIT_TOUR_SLAB_MASTER", Tourid);
            if (dsslab != null)
            {
                if (dsslab.Tables[0].Rows.Count > 1)
                {
                    bindSlabGrid();

                    //DataSet ds_SelectedSlab = objGitDetail.fetchSlabAndCount("GET_SLAB_FROM_GIT_TOUR_SLAB_MASTER", Tourid);

                    
                }
                else
                {
                    if (dsslab.Tables[0].Rows.Count == 1)
                    {
                        DataSet dtslabtails = objGitDetail.UpdateIsSlabFinal("UPDATE_GIT_TOUR_SLAB_MASTER_ON_CONFIRM_SLAB", int.Parse(dsslab.Tables[0].Rows[0]["GIT_TOUR_SLAB_ID"].ToString()));
                        Response.Redirect("~/Views/GIT/FlightDetails.aspx?TOURID=" + tourId);
                    }
                }
            }
            //DataSet dtFlightDetails = objGitDetail.fetchAllFlightDetails("FETCH_GIT_FLIGHT_DETAILS", int.Parse(Request.QueryString["TOURID"].ToString()));
            //int count = Convert.ToInt32(dtFlightDetails.Tables[0].Rows[0]["COUNT"].ToString());
            //if (count == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Flight Details.')", true);
            //}
            //else
            //{
            //    Response.Redirect("~/Views/GIT/GITPayment.aspx?TOURID=" + tourId);
            //}
            upExRate.Update();
        }

        protected void bindSlabGrid()
        {
            pnlSelectSlab.Attributes.Add("style", "display");
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
            modalPop.ID = "Modalpopupextender2";
            modalPop.PopupControlID = "pnlSelectSlab";

            modalPop.CancelControlID = "ImageButton3";
            this.pnlSelectSlab.Controls.Add(modalPop);

            modalPop.TargetControlID = "btnreconform";
            modalPop.Show();
            int Tourid = Convert.ToInt32(tourId);
            DataSet dsslab = objGitDetail.fetchSlabAndCount("GET_SLAB_FROM_GIT_TOUR_SLAB_MASTER", Tourid);
            gvSelectSlab.DataSource = dsslab;
            gvSelectSlab.DataBind();
            upExRate.Update();
        }

        protected void SlabCheckChanged(object sender, EventArgs e)
        {
            try
            {

                ViewState["Slab"] = null;
                //on each item checked, remove any other items checked
                Label l = new Label();
                RadioButton selectButton = (RadioButton)sender;
                GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
                int a = row.RowIndex;
                foreach (GridViewRow item in gvSelectSlab.Rows)
                {
                    RadioButton rb = (RadioButton)item.FindControl("rbSelectSlab");
                    if (rb != sender)
                    {
                        rb.Checked = false;
                    }
                    if (rb.Checked == true)
                    {
                        l = (Label)item.FindControl("lblslabid");
                        ViewState["Slab"] = l.Text;

                    }
                    upExRate.Update();
                }

                pnlSelectSlab.Attributes.Add("style", "display");
                AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
                modalPop.ID = "Modalpopupextender2";
                modalPop.PopupControlID = "pnlSelectSlab";
                modalPop.CancelControlID = "ImageButton3";
                this.pnlSelectSlab.Controls.Add(modalPop);
                modalPop.TargetControlID = "btnreconform";
                modalPop.Show();
                upExRate.Update();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void ButtonConfirmSlab_Click(object sender, EventArgs e)
        {
            try
            {
                int Tourid = Convert.ToInt32(tourId);
                int slabid = Convert.ToInt32(ViewState["Slab"].ToString());
                DataSet dtFlightDetails = objGitDetail.UpdateIsSlabFinal("UPDATE_GIT_TOUR_SLAB_MASTER_ON_CONFIRM_SLAB", slabid);
                //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Slab Confirm Successfully...')", true);
                Response.Redirect("~/Views/GIT/FlightDetails.aspx?TOURID=" + tourId);
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

        #region Hotel confirm

        protected void btnConfirmHotel_click(object sender, EventArgs e)
        {

            bool flag_popup = true;

            try
            {
                System.DateTime today1 = DateTime.ParseExact(txtreconfirmdate.Text, "dd/MM/yyyy", null);
                System.DateTime today4 = DateTime.ParseExact(txtpayment.Text, "dd/MM/yyyy", null);
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
                if (DateTime.ParseExact(txtreconfirmdate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(result, "dd/MM/yyyy", null))
                {
                    if (DateTime.ParseExact(txtreconfirmdate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["FromDate"].ToString(), "dd/MM/yyyy", null))
                    {
                        flag_popup = true;
                    }
                    else
                    {
                        flag_popup = false;
                    }

                }
                else
                {
                    flag_popup = false;
                }
                if (flag_popup == false)
                {
                    labelerror.Text = "Please enter date between today's date and Tour date";
                    pnlCompanyRoleSelection.Attributes.Add("style", "display");
                    txtreconfirmdate.Text = "";
                    AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
                    modalPop.ID = "popUp";
                    modalPop.PopupControlID = "pnlCompanyRoleSelection";
                    modalPop.TargetControlID = "btnhotelconfirm";
                    modalPop.CancelControlID = "ImageButton1";
                    this.pnlCompanyRoleSelection.Controls.Add(modalPop);
                    modalPop.Show();
                    modalPop.TargetControlID = "btnConfirmHotel";
                    Updateconfirm.Update();
                }
                else
                {
                    if (ViewState["Hotel_Name"] != null && ViewState["Room_Type"] != null)
                    {
                        objGitDetail.UpdateHotelReconfirmationDate(int.Parse(tourId), ViewState["Hotel_Name"].ToString(), ViewState["Room_Type"].ToString(), txtreconfirmdate.Text, txtpayment.Text, txtconfirmnumber.Text);
                       // ViewState["Hotel_Name"] = "";
                       // ViewState["Room_Type"] = "";
                        txtreconfirmdate.Text = "";
                        txtpayment.Text = "";
                        txtconfirmnumber.Text = "";
                        label63.Text = "";
                        labelerror.Text = "";
                        pnlCompanyRoleSelection.Attributes.Add("style", "display:none");
                        allUnchecked(sender);
                        btnConfirmHotel.Visible = false;
                        InsertReconformationDateToQuoteMaster();
                        Updateconfirm.Update();
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Hotel Confirmed Successfully.')", true);

                    }

                }
            }
            catch
            {
                labelerror.Text = "Enter Date Is not in valid Format";
                pnlCompanyRoleSelection.Attributes.Add("style", "display");
                txtreconfirmdate.Text = "";
                AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
                modalPop.ID = "popUp";
                modalPop.PopupControlID = "pnlCompanyRoleSelection";
                modalPop.TargetControlID = "btnhotelconfirm";
                modalPop.CancelControlID = "ImageButton1";
                this.pnlCompanyRoleSelection.Controls.Add(modalPop);
                modalPop.Show();
                modalPop.TargetControlID = "btnConfirmHotel";
                Updateconfirm.Update();

            }

        }

        protected void btnConfirm_click(object sender, EventArgs e)
        {
            pnlCompanyRoleSelection.Attributes.Add("style", "display");
            txtreconfirmdate.Text = "";
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
            modalPop.ID = "popUp";
            modalPop.PopupControlID = "pnlCompanyRoleSelection";
            modalPop.TargetControlID = "btnhotelconfirm";
            modalPop.CancelControlID = "ImageButton1";
            this.pnlCompanyRoleSelection.Controls.Add(modalPop);
            modalPop.Show();
            modalPop.TargetControlID = "btnConfirmHotel";
            FillHotelConformPopup();
            Updateconfirm.Update();
        }

        protected void FillHotelConformPopup()
        {
            if (ViewState["Hotel_Name"] != null)
            {
                DataSet dtConformdates = objGitDetail.fetchHotelsConformdateForPopup("GET_PAYMENT_DATE_HOTEL_FOR_POPUP", int.Parse(Request.QueryString["TOURID"].ToString()), ViewState["Hotel_Name"].ToString(), ViewState["Room_Type"].ToString());

                txtreconfirmdate.Text = dtConformdates.Tables[0].Rows[0]["RECONFIRMATION_DATE"].ToString();
                txtconfirmnumber.Text = dtConformdates.Tables[0].Rows[0]["CONFIRMATION_NUMBER"].ToString();
                txtpayment.Text = dtConformdates.Tables[0].Rows[0]["PAYMENT_DUE_DATE"].ToString();
            }

        }


        #endregion

        protected void PaymentDateValidation(GridView gv, string City_Name, string type)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    TextBox txtreconfirmdate = (TextBox)item.FindControl("txtReconfirmedDate");
                    if (txtreconfirmdate.Text != "")
                    {
                        System.DateTime today1 = DateTime.ParseExact(txtreconfirmdate.Text, "dd/MM/yyyy", null);
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
                                //lbl_voucher_date.Text = result;
                            }
                            else
                            {
                                result = "0" + w[1] + "/" + w[0] + "/" + t1[0];
                                //lbl_voucher_date.Text = result;
                            }
                        }
                        else
                        {
                            if (w[0] == "1" || w[0] == "2" || w[0] == "3" || w[0] == "4" || w[0] == "5" || w[0] == "6" || w[0] == "7" || w[0] == "8" || w[0] == "9")
                            {
                                result = w[1] + "/" + "0" + w[0] + "/" + t1[0];
                                //lbl_voucher_date.Text = result;
                            }
                            else
                            {
                                result = w[1] + "/" + w[0] + "/" + t1[0];
                                //lbl_voucher_date.Text = result;
                            }
                        }

                        if (DateTime.ParseExact(txtreconfirmdate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(result, "dd/MM/yyyy", null))
                        {
                            ViewState["Paymentmessage"] = null;
                        }
                        else
                        {
                            ViewState["Paymentmessage"] = "Payment Date should be larger than Todays Date in " + type;
                        }

                    }
                }

            }
            catch
            {
                Master.DisplayMessage("Enter Date Is not in valid Format.", "successMessage", 5000);
            }
        }

        protected void InsertReconformationDateToQuoteMaster()
        {
            bool flag_date = true;

            dtAllBookedHotels = objGitDetail.fetchAllBookedHotels("FETCH_ALL_BOOKED_HOTELS", int.Parse(Request.QueryString["TOURID"].ToString()));
            string minreconformdate = "";
            if (dtAllBookedHotels != null)
            {
                for (int i = 0; i < dtAllBookedHotels.Tables[0].Rows.Count; i++)
                {
                    if (dtAllBookedHotels.Tables[0].Rows[i]["RECONFIRMATION_DATE"].ToString() == "")
                    {
                        flag_date = false;
                    }
                }
                if (flag_date != false)
                {

                    minreconformdate = dtAllBookedHotels.Tables[0].Rows[0]["RECONFIRMATION_DATE"].ToString();
                    for (int j = 0; j < dtAllBookedHotels.Tables[0].Rows.Count; j++)
                    {

                        if (dtAllBookedHotels.Tables[0].Rows.Count == 1)
                        {
                            minreconformdate = dtAllBookedHotels.Tables[0].Rows[0]["RECONFIRMATION_DATE"].ToString();
                        }
                        else
                        {
                            if (dtAllBookedHotels.Tables[0].Rows[j]["RECONFIRMATION_DATE"].ToString() != "")
                            {
                                if (DateTime.ParseExact(dtAllBookedHotels.Tables[0].Rows[j]["RECONFIRMATION_DATE"].ToString(), "dd/MM/yyyy", null) < DateTime.ParseExact(minreconformdate, "dd/MM/yyyy", null))
                                {
                                    minreconformdate = dtAllBookedHotels.Tables[0].Rows[j]["RECONFIRMATION_DATE"].ToString();
                                }
                            }
                        }
                    }

                    DataSet ds_fromEmail = objTobereconform.getFromEmailAddress(int.Parse(tourId));
                    if (ds_fromEmail.Tables[0].Rows.Count != 0)
                    {
                        HotelTobeReconfirmEmail(ds_fromEmail.Tables[0].Rows[0]["BOOK_EMAIL"].ToString());
                    }
                    else
                    {
                        HotelTobeReconfirmEmail("1");
                    }

                    objGitDetail.UpdateReconforamtiondate("UPDATE_RECONFIRMATION_DATE_GIT_QUOTE_MASTER", int.Parse(Request.QueryString["TOURID"].ToString()), minreconformdate);
                }
            }

        }

        //--------------Hotel To be Reconfirm mail to Agent---------------------//
        protected void HotelTobeReconfirmEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[22]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for HOTEL  */
                    DataSet ds1 = objBookSp.getHotelForTobereconfirmEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
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
                                toemail1 = ds1.Tables[0].Rows[0]["AGENT_EMAIL"].ToString();
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
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
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
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
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
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENTNAME%&gt;", ds1.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                        //strEmailTemplate = strEmailTemplate.Replace("&lt;%ROOMTYPE%&gt;", ds1.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%ARRIVALDATE%&gt;", ds1.Tables[0].Rows[0]["START_DATE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DEPARTUREDATE%&gt;", ds1.Tables[0].Rows[0]["END_DATE"].ToString());
                        //strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%RECONFIRMDATE%&gt;", ds1.Tables[0].Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString());


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
                        subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();


                        subjct = subjct.Replace("<%GROUPNAME%>", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        subjct = subjct.Replace("<%ARRIVALDATE%>", ds1.Tables[0].Rows[j]["START_DATE"].ToString());
                        subjct = subjct.Replace("<%DEPARTUREDATE%>", ds1.Tables[0].Rows[j]["END_DATE"].ToString());

                        message.Subject = subjct;

                        message.Body = body;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient();
                        NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                        client.Credentials = info;
                        client.Host = smtphost;
                        client.Port = int.Parse(smtpport);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                       // client.Send(message);

                    
                           // objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        
                       
                            //objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        


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

        #region BOOK STAGE + BOOK BUTTON

        protected void btnBook_Click(object sender, EventArgs e)
        {
            if (Session["Adults"].ToString() == "") //&& Session["CWB"].ToString() == "" && Session["CNB"].ToString() == "" && Session["Infants"].ToString() == "")
            {
                Master.DisplayMessage("please enter no of passengers Details.", "successMessage", 5000);
            }
            else
            {
                bindAllcobminationGrid();
                objBookSp.insertPassengerDetails(int.Parse(Request.QueryString["TOURID"].ToString()), Session["Adults"].ToString(), Session["CWB"].ToString(), Session["CNB"].ToString(), Session["infants"].ToString());
                objInsertGitDetails.changeQuotestatus(int.Parse(Request.QueryString["TOURID"].ToString()), "Booked");

            }
        }

        protected void BookCheckChanged(object sender, EventArgs e)
        {
            try
            {

                ViewState["l"] = null;
                //on each item checked, remove any other items checked
                Label l = new Label();
                RadioButton selectButton = (RadioButton)sender;
                GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
                int a = row.RowIndex;
                foreach (GridViewRow item in gvallcomb.Rows)
                {
                    RadioButton rb = (RadioButton)item.FindControl("rbselect");
                    if (rb != sender)
                    {
                        rb.Checked = false;
                    }

                    if (rb.Checked == true)
                    {
                        l = (Label)item.FindControl("lblallcombinationID");
                        ViewState["l"] = l.Text;

                    }
                    upExRate.Update();
                }


                pnlAllcombinationSelection.Attributes.Add("style", "display");
                AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
                modalPop.ID = "Modalpopupextender1";
                modalPop.PopupControlID = "pnlAllcombinationSelection";


                modalPop.CancelControlID = "ImageButton2";
                this.pnlAllcombinationSelection.Controls.Add(modalPop);

                modalPop.TargetControlID = "btnBook";
                modalPop.Show();
                upExRate.Update();

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void bindAllcobminationGrid()
        {
            pnlAllcombinationSelection.Attributes.Add("style", "display");
            AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
            modalPop.ID = "Modalpopupextender1";
            modalPop.PopupControlID = "pnlAllcombinationSelection";

            modalPop.CancelControlID = "ImageButton2";
            this.pnlAllcombinationSelection.Controls.Add(modalPop);

            modalPop.TargetControlID = "btnBook";
            modalPop.Show();
            int Tourid = Convert.ToInt32(tourId);
            dsallcomb = objGitDetail.fetchAllCombination("GET_ALL_COMBITATION", Tourid);
            gvallcomb.DataSource = dsallcomb;
            gvallcomb.DataBind();

            upExRate.Update();
        }

        protected void CombinationEmails(string bcc)
        {

            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[21]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
            {

                DataSet ds = new DataSet();

                for (int r = 0; r < dsallsupmails.Tables[3].Rows.Count; r++)
                {
                    for (int c = 0; c < dsallsupmails.Tables[3].Columns.Count; c++)
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
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
                            {
                                //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
                            {
                                //fromemail = ds.Tables[0].Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
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

                                if (r == 0)
                                {
                                    if (dsallsupmails.Tables[3].Rows[r][c].ToString() != "")
                                    {
                                        toemail1 = dsallsupmails.Tables[3].Rows[r][c].ToString();

                                        ds = objBookSp.getHotelForEmail(int.Parse(Request.QueryString["TOURID"].ToString()), int.Parse(dsallsupmails.Tables[4].Rows[r][c].ToString()));
                                    }
                                }
                                else
                                {
                                    if (dsallsupmails.Tables[3].Rows[r][2].ToString() != "")
                                    {
                                        toemail1 = dsallsupmails.Tables[3].Rows[r][2].ToString();
                                        c = 2;
                                    }
                                }
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

                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {

                            }

                            else
                            {

                            }


                        }

                        string bcc1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {

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

                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        try
                        {
                            if (toemail1 != "")
                            {

                                string body = "";


                                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                                if (ds != null)
                                {
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds.Tables[0].Rows[0]["CHAIN_NAME"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENTNAME%&gt;", ds.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%ROOMTYPE%&gt;", ds.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString());
                                    //strEmailTemplate = strEmailTemplate.Replace("&lt;%SINGLEROOMS%&gt;", single_rooms);
                                    //strEmailTemplate = strEmailTemplate.Replace("&lt;%DOUBLEROOMS%&gt;", double_rooms);
                                    // strEmailTemplate = strEmailTemplate.Replace("&lt;%TRIPLEROOMS%&gt;", tripal_rooms);
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFROOMS%&gt;", ds.Tables[0].Rows[0]["TOTAL_NO_OF_ROOMS"].ToString());

                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%CHECKINDATE%&gt;", ds.Tables[0].Rows[0]["FROM_DATE"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%CHECKOUTDATE%&gt;", ds.Tables[0].Rows[0]["TO_DATE"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCWB%&gt;", ds.Tables[0].Rows[0]["NO_OF_CWB"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCNB%&gt;", ds.Tables[0].Rows[0]["NO_OF_CNB"].ToString());
                                    strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFINFANT%&gt;", ds.Tables[0].Rows[0]["NO_OF_INFANT"].ToString());
                                }

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
                                subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();


                                subjct = subjct.Replace("<%CLIENTNAME%>", ds.Tables[0].Rows[0]["GIT_GROUP_NAME"].ToString());
                                subjct = subjct.Replace("<%CHECKINDATE%>", ds.Tables[0].Rows[0]["FROM_DATE"].ToString());
                                subjct = subjct.Replace("<%CHECKOUTDATE%>", ds.Tables[0].Rows[0]["TO_DATE"].ToString());

                                message.Subject = subjct;

                                message.Body = body;
                                message.IsBodyHtml = true;

                                SmtpClient client = new SmtpClient();
                                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                                client.Credentials = info;
                                client.Host = smtphost;
                                client.Port = int.Parse(smtpport);
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                               // client.Send(message);

                            
                                 //   objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                              
                                   // objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[0]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                                
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
                }
            }
        }

        protected void GalaDinnerEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[21]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();
                if (ds_mailTemplate1.Tables[0].Rows[1]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for gala dinner */
                    DataSet ds1 = objBookSp.getGalaDinnerForEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
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
                                //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
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
                                // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
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
                                // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }

                            else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
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

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds1.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GALADINNERTYPE%&gt;", ds1.Tables[0].Rows[j]["GALA_DINNER_TYPE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DATE%&gt;", ds1.Tables[0].Rows[j]["DINNER_DATE"].ToString());
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
                        subjct = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_SUBJECT"].ToString();


                        subjct = subjct.Replace("<%CLIENTNAME%>", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        //subjct = subjct.Replace("<%CHECKINDATE%>", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
                        //subjct = subjct.Replace("<%CHECKOUTDATE%>", ds.Tables[0].Rows[i]["TO_DATE"].ToString());

                        message.Subject = subjct;

                        message.Body = body;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient();
                        NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                        client.Credentials = info;
                        client.Host = smtphost;
                        client.Port = int.Parse(smtpport);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                       // client.Send(message);

                        //if (j == 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        //}
                        //if (j != 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        //}


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


        protected void ConferenceEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[21]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();
                if (ds_mailTemplate1.Tables[0].Rows[2]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for Conference */
                    DataSet ds1 = objBookSp.getConferenceForEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Backoffice")
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
                                if (bcc == "1")
                                {
                                    toemail1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    toemail1 = "reservation1@travelzunlimited.com";
                                }
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
                                if (bcc == "1")
                                {
                                    cc = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    cc = "reservation1@travelzunlimited.com";
                                }
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
                                if (bcc == "1")
                                {
                                    bcc1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    bcc1 = "reservation1@travelzunlimited.com";
                                }
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

                        strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds1.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%GROUPNAME%&gt;", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%CONFERENCETYPE%&gt;", ds1.Tables[0].Rows[j]["CONFERENCE_TYPE"].ToString());
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DATE%&gt;", ds1.Tables[0].Rows[j]["CONFERENCE_DATE"].ToString());
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
                        subjct = ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();


                        subjct = subjct.Replace("<%GROUPNAME%>", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
                        //subjct = subjct.Replace("<%CHECKINDATE%>", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
                        //subjct = subjct.Replace("<%CHECKOUTDATE%>", ds.Tables[0].Rows[i]["TO_DATE"].ToString());

                        message.Subject = subjct;

                        message.Body = body;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient();
                        NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                        client.Credentials = info;
                        client.Host = smtphost;
                        client.Port = int.Parse(smtpport);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //client.Send(message);

                        //if (j == 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        //}
                        //if (j != 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        //}


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

        protected void ButtonPopupmail_Click(object sender, EventArgs e)
        {
            int Tourid = Convert.ToInt32(tourId);
            int combid = Convert.ToInt32(ViewState["l"].ToString());

            DataSet ds1 = objGitDetail.UpdateBookCombination(Tourid, combid, Session["usersid"].ToString());

            DataSet ds = objGitDetail.CommonSp("GET_BOOK_EMAIL");

            dsallsupmails = objGitDetail.fetchAllSupplierMail("GET_ALL_COMBITATION_HOTEL_EMAIL", Tourid, combid);

            for (int i = 0; i < dsallsupmails.Tables[0].Rows.Count; i++)
            {

                objBookSp.insertBookHotel(int.Parse(tourId), int.Parse(dsallsupmails.Tables[0].Rows[0]["HOTEL_CART_ID_1"].ToString()));
                objBookSp.insertBookHotel(int.Parse(tourId), int.Parse(dsallsupmails.Tables[0].Rows[0]["HOTEL_CART_ID_2"].ToString()));
            }

            for (int i = 0; i < dsallsupmails.Tables[1].Rows.Count; i++)
            {

                objBookSp.insertBookHotel(int.Parse(tourId), int.Parse(dsallsupmails.Tables[0].Rows[0]["COMBINATION_HOTEL_TABLE_ID"].ToString()));
                //objBookSp.insertBookHotel(int.Parse(tourId), int.Parse(dsallsupmails.Tables[0].Rows[0]["HOTEL_CART_ID_2"].ToString()));
            }

            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["BOOK_EMAIL"].ToString() == "1")
                {
                    objBookSp.insertBookEmail(int.Parse(Request.QueryString["TOURID"].ToString()), "2");
                    CombinationEmails("2");
                    ConferenceEmail("2");
                    GalaDinnerEmail("2");
                    SiteseeingBookEmail("2");

                }
                else if (ds.Tables[0].Rows[0]["BOOK_EMAIL"].ToString() == "2")
                {
                    objBookSp.insertBookEmail(int.Parse(Request.QueryString["TOURID"].ToString()), "1");
                    CombinationEmails("1");
                    ConferenceEmail("1");
                    GalaDinnerEmail("1");
                    SiteseeingBookEmail("1");
                }
                else
                {
                    objBookSp.insertBookEmail(int.Parse(Request.QueryString["TOURID"].ToString()), "1");
                    CombinationEmails("1");
                    ConferenceEmail("1");
                    GalaDinnerEmail("1");
                    SiteseeingBookEmail("1");
                }
            }
            else
            {
                objBookSp.insertBookEmail(int.Parse(Request.QueryString["TOURID"].ToString()), "1");
                CombinationEmails("1");
                ConferenceEmail("1");
                GalaDinnerEmail("1");
                SiteseeingBookEmail("1");
            }
            pnlAllcombinationSelection.Attributes.Add("style", "display:none");
            btnBook.Visible = false;
            upExRate.Update();
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Slab booked Successfully.')", true);
        }

        //--------------siteseeing book mail---------------------//    
        protected void SiteseeingBookEmail(string bcc)
        {
            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[21]["AutoSearchResult"].ToString());

                DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

                string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
                string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
                string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

                if (ds_mailTemplate1.Tables[0].Rows[3]["IS_ON"].ToString() == "True")
                {
                    /*mail fires  for gala dinner */
                    DataSet ds1 = objBookSp.getSiteSeeingForEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        string fromemail = "";
                        if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[3]["FROM_ROLE_NAME"].ToString() == "Backoffice")
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
                                if (bcc == "1")
                                {
                                    toemail1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    toemail1 = "reservation1@travelzunlimited.com";
                                }
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
                                if (bcc == "1")
                                {
                                    cc = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    cc = "reservation1@travelzunlimited.com";
                                }
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
                                if (bcc == "1")
                                {
                                    bcc1 = "reservation@travelzunlimited.com";
                                }
                                else if (bcc == "2")
                                {
                                    bcc1 = "reservation1@travelzunlimited.com";
                                }
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
                        subjct = ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_SUBJECT"].ToString();


                        subjct = subjct.Replace("<%GROUPNAME%>", ds1.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());

                        message.Subject = subjct;

                        message.Body = body;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient();
                        NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                        client.Credentials = info;
                        client.Host = smtphost;
                        client.Port = int.Parse(smtpport);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                      //  client.Send(message);

                        //if (j == 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                        //}
                        //if (j != 0)
                        //{
                        //    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[3]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds1.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                        //}


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

        #region ALL COMMENTED AND UNUSE CODES

        protected void UncheckAllotherOnCheckChanged(GridView gv, UpdatePanel up, object sender)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rdoConfirm");
                rb.Checked = false;
            }
            up.Update();
        }

        protected void UncheckAll(object sender)
        {
            var cntls = GetAll(this, typeof(GridView));
            foreach (Control cntrl in cntls)
            {
                GridView gv = (GridView)cntrl;

                foreach (GridViewRow item in gv.Rows)
                {
                    RadioButton rb = (RadioButton)item.FindControl("rdoConfirm");
                    if (rb != sender && rb != null)
                    {
                        rb.Checked = false;
                    }
                }
            }

        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrls => GetAll(ctrls, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        #region OLD EMAILS
        //   protected void HotelEmails(string bcc)
        //   {

        //       DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

        //       DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[21]["AutoSearchResult"].ToString());

        //       DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

        //       string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
        //       string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
        //       string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
        //       string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

        ////       DataSet ds = objBookSp.getHotelForEmail(int.Parse(Request.QueryString["TOURID"].ToString()));

        //       string Adults = ds.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
        //       string cwb = ds.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
        //       string cnb = ds.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
        //       string infants = ds.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();

        //       for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //       {
        //           string fromemail = "";
        //           if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
        //           {
        //               DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

        //               if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
        //               {
        //                   if (bcc == "1")
        //                   {
        //                       fromemail = "reservation@travelzunlimited.com";
        //                   }
        //                   else if (bcc == "2")
        //                   {
        //                       fromemail = "reservation1@travelzunlimited.com";
        //                   }
        //               }
        //               else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
        //               {
        //                   //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //               }
        //               else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
        //               {
        //                   fromemail = ds.Tables[0].Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
        //               }
        //               else
        //               {
        //                   fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //               }
        //           }

        //           string toemail1 = "";
        //           if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
        //           {
        //               DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

        //               if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
        //               {
        //                   //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //               }
        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
        //               {
        //                   if (bcc == "1")
        //                   {
        //                       toemail1 = "reservation@travelzunlimited.com";
        //                   }
        //                   else if (bcc == "2")
        //                   {
        //                       toemail1 = "reservation1@travelzunlimited.com";
        //                   }
        //               }
        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
        //               {
        //                   toemail1 = ds.Tables[0].Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
        //               }
        //               else
        //               {
        //                   toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //               }
        //           }

        //           string cc = "";
        //           if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
        //           {
        //               DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
        //               if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
        //               {
        //                   // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //               }

        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
        //               {
        //                   if (bcc == "1")
        //                   {
        //                       cc = "reservation@travelzunlimited.com";
        //                   }
        //                   else if (bcc == "2")
        //                   {
        //                       cc = "reservation1@travelzunlimited.com";
        //                   }
        //               }

        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
        //               {
        //                   cc = ds.Tables[0].Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
        //               }

        //               else
        //               {
        //                   // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //               }


        //           }

        //           string bcc1 = "";
        //           if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
        //           {
        //               DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

        //               if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
        //               {
        //                   // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //               }

        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
        //               {
        //                   if (bcc == "1")
        //                   {
        //                       bcc1 = "reservation@travelzunlimited.com";
        //                   }
        //                   else if (bcc == "2")
        //                   {
        //                       bcc1 = "reservation1@travelzunlimited.com";
        //                   }
        //               }

        //               else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
        //               {
        //                   bcc = ds.Tables[0].Rows[i]["SUPPLIER_REL_EMAIL"].ToString();
        //               }

        //               else
        //               {
        //                   bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //               }
        //           }
        //           string body = "";


        //           string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds.Tables[0].Rows[i]["CHAIN_NAME"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds.Tables[0].Rows[i]["GIT_QUOTE_ID"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENTNAME%&gt;", ds.Tables[0].Rows[i]["GIT_GROUP_NAME"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%ROOMTYPE%&gt;", ds.Tables[0].Rows[i]["ROOM_TYPE_NAME"].ToString());
        //           //strEmailTemplate = strEmailTemplate.Replace("&lt;%SINGLEROOMS%&gt;", single_rooms);
        //           //strEmailTemplate = strEmailTemplate.Replace("&lt;%DOUBLEROOMS%&gt;", double_rooms);
        //           // strEmailTemplate = strEmailTemplate.Replace("&lt;%TRIPLEROOMS%&gt;", tripal_rooms);
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%CHECKINDATE%&gt;", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%CHECKOUTDATE%&gt;", ds.Tables[0].Rows[i]["TO_DATE"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", ds.Tables[0].Rows[i]["NO_OF_ADULTS"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCWB%&gt;", ds.Tables[0].Rows[i]["NO_OF_CWB"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCNB%&gt;", ds.Tables[0].Rows[i]["NO_OF_CNB"].ToString());
        //           strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFINFANT%&gt;", ds.Tables[0].Rows[i]["NO_OF_INFANT"].ToString());

        //           body = strEmailTemplate;
        //           try
        //           {
        //               MailMessage message = new MailMessage();

        //               message.From = new MailAddress(fromemail);

        //               message.To.Add(new MailAddress(toemail1));
        //               if (cc != "")
        //               {
        //                   message.CC.Add(new MailAddress(cc));
        //               }

        //               if (bcc1 != "")
        //               {
        //                   message.Bcc.Add(new MailAddress(bcc1));
        //               }

        //               string subjct = "";
        //               subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();


        //               subjct = subjct.Replace("<%CLIENTNAME%>", ds.Tables[0].Rows[i]["GIT_GROUP_NAME"].ToString());
        //               subjct = subjct.Replace("<%CHECKINDATE%>", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
        //               subjct = subjct.Replace("<%CHECKOUTDATE%>", ds.Tables[0].Rows[i]["TO_DATE"].ToString());

        //               message.Subject = subjct;

        //               message.Body = body;
        //               message.IsBodyHtml = true;

        //               SmtpClient client = new SmtpClient();
        //               NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
        //               client.Credentials = info;
        //               client.Host = smtphost;
        //               client.Port = int.Parse(smtpport);
        //               client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //               client.Send(message);

        //               if (i == 0)
        //               {
        //                   objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[i]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
        //               }
        //               if (i != 0)
        //               {
        //                   objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[i]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
        //               }

        //           }
        //           catch
        //           {

        //           }
        //           finally
        //           {

        //           }
        //       }


        //       try
        //       {
        //           /*mail fires  for gala dinner */
        //           DataSet ds1 = objBookSp.getConferenceForEmail(int.Parse(Request.QueryString["TOURID"].ToString()));
        //           for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        //           {
        //               string fromemail = "";
        //               if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
        //               {
        //                   DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

        //                   if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
        //                   {
        //                       if (bcc == "1")
        //                       {
        //                           fromemail = "reservation@travelzunlimited.com";
        //                       }
        //                       else if (bcc == "2")
        //                       {
        //                           fromemail = "reservation1@travelzunlimited.com";
        //                       }
        //                   }
        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
        //                   {
        //                       //fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //                   }
        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
        //                   {
        //                       fromemail = ds.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
        //                   }
        //                   else
        //                   {
        //                       fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //                   }
        //               }

        //               string toemail1 = "";
        //               if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() != "")
        //               {
        //                   DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

        //                   if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Agent")
        //                   {
        //                       //toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //                   }
        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
        //                   {
        //                       if (bcc == "1")
        //                       {
        //                           toemail1 = "reservation@travelzunlimited.com";
        //                       }
        //                       else if (bcc == "2")
        //                       {
        //                           toemail1 = "reservation1@travelzunlimited.com";
        //                       }
        //                   }
        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Supplier")
        //                   {
        //                       toemail1 = ds.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
        //                   }
        //                   else
        //                   {
        //                       toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //                   }
        //               }

        //               string cc = "";
        //               if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
        //               {
        //                   DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());
        //                   if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
        //                   {
        //                       // cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //                   }

        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
        //                   {
        //                       if (bcc == "1")
        //                       {
        //                           cc = "reservation@travelzunlimited.com";
        //                       }
        //                       else if (bcc == "2")
        //                       {
        //                           cc = "reservation1@travelzunlimited.com";
        //                       }
        //                   }

        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Supplier")
        //                   {
        //                       cc = ds.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
        //                   }

        //                   else
        //                   {
        //                       // cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //                   }


        //               }

        //               string bcc1 = "";
        //               if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
        //               {
        //                   DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

        //                   if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
        //                   {
        //                       // bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
        //                   }

        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
        //                   {
        //                       if (bcc == "1")
        //                       {
        //                           bcc1 = "reservation@travelzunlimited.com";
        //                       }
        //                       else if (bcc == "2")
        //                       {
        //                           bcc1 = "reservation1@travelzunlimited.com";
        //                       }
        //                   }

        //                   else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Supplier")
        //                   {
        //                       bcc = ds.Tables[0].Rows[j]["SUPPLIER_REL_EMAIL"].ToString();
        //                   }

        //                   else
        //                   {
        //                       bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
        //                   }
        //               }

        //               string body = "";


        //               string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();

        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTELNAME%&gt;", ds.Tables[0].Rows[j]["CHAIN_NAME"].ToString());
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTEID%&gt;", ds.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString());
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENTNAME%&gt;", ds.Tables[0].Rows[j]["GIT_GROUP_NAME"].ToString());
        //               //strEmailTemplate = strEmailTemplate.Replace("&lt;%ROOMTYPE%&gt;", ds.Tables[0].Rows[i]["ROOM_TYPE_NAME"].ToString());
        //               //strEmailTemplate = strEmailTemplate.Replace("&lt;%SINGLEROOMS%&gt;", single_rooms);
        //               //strEmailTemplate = strEmailTemplate.Replace("&lt;%DOUBLEROOMS%&gt;", double_rooms);
        //               // strEmailTemplate = strEmailTemplate.Replace("&lt;%TRIPLEROOMS%&gt;", tripal_rooms);
        //               //strEmailTemplate = strEmailTemplate.Replace("&lt;%CHECKINDATE%&gt;", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%DATE%&gt;", ds.Tables[0].Rows[j]["DATE"].ToString());
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFADULT%&gt;", Adults);
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCWB%&gt;", cwb);
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFCNB%&gt;", cnb);
        //               strEmailTemplate = strEmailTemplate.Replace("&lt;%NOOFINFANT%&gt;", infants);

        //               body = strEmailTemplate;

        //               MailMessage message = new MailMessage();

        //               message.From = new MailAddress(fromemail);

        //               message.To.Add(new MailAddress(toemail1));
        //               if (cc != "")
        //               {
        //                   message.CC.Add(new MailAddress(cc));
        //               }

        //               if (bcc1 != "")
        //               {
        //                   message.Bcc.Add(new MailAddress(bcc1));
        //               }

        //               string subjct = "";
        //               subjct = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_SUBJECT"].ToString();


        //               //subjct = subjct.Replace("<%CLIENTNAME%>", ds.Tables[0].Rows[i]["GIT_GROUP_NAME"].ToString());
        //               //subjct = subjct.Replace("<%CHECKINDATE%>", ds.Tables[0].Rows[i]["FROM_DATE"].ToString());
        //               //subjct = subjct.Replace("<%CHECKOUTDATE%>", ds.Tables[0].Rows[i]["TO_DATE"].ToString());

        //               message.Subject = subjct;

        //               message.Body = body;
        //               message.IsBodyHtml = true;

        //               SmtpClient client = new SmtpClient();
        //               NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
        //               client.Credentials = info;
        //               client.Host = smtphost;
        //               client.Port = int.Parse(smtpport);
        //               client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //               client.Send(message);

        //               if (j == 0)
        //               {
        //                   objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
        //               }
        //               if (j != 0)
        //               {
        //                   objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(ds.Tables[0].Rows[j]["GIT_QUOTE_ID"].ToString()), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
        //               }


        //           }
        //       }
        //       catch (Exception ex)
        //       {
        //           throw ex;
        //       }
        //       finally
        //       {

        //       }

        //   }
        #endregion

        //protected void ConferenceDateValidation(GridView gv)
        //{
        //    for (int i = 0; i < gv.Rows.Count; i++)
        //    {

        //        DropDownList drpResturant = (DropDownList)gv.Rows[i].FindControl("drpHotel");
        //        TextBox txtDate = (TextBox)gv.Rows[i].FindControl("txtDate");
        //        // TextBox CheckOutDate = (TextBox)gv.Rows[i].FindControl("txtCheckOutDate");
        //        if (drpResturant.Text != "")
        //        {
        //            if (txtDate.Text == "")
        //            {
        //                Conferenceflag = false;
        //                ViewState["ConferenceValidation"] = "Please enter date for " + drpResturant.Text;
        //            }
        //            else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
        //            {
        //                Conferenceflag = false;
        //                ViewState["ConferenceValidation"] = drpResturant.Text + " " + "CheckIn Date and CheckOut Date Should be Between Start Date and End Date.";
        //            }

        //        }
        //    }
        //}

        //protected void GalaDiinerDateValidation(GridView gv)
        //{
        //    for (int i = 0; i < gv.Rows.Count; i++)
        //    {

        //        DropDownList drpResturant = (DropDownList)gv.Rows[i].FindControl("drpHotel");
        //        TextBox txtDate = (TextBox)gv.Rows[i].FindControl("txtDate");

        //        if (drpResturant.Text != "")
        //        {
        //            if (txtDate.Text == "")
        //            {
        //                GalaDinnerFlag = false;
        //                ViewState["GaladinnerValidation"] = "Please enter date for " + drpResturant.Text;
        //            }
        //            else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(Session["fromdate"].ToString(), "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(Session["todate"].ToString(), "dd/MM/yyyy", null)))
        //            {
        //                GalaDinnerFlag = false;
        //                ViewState["GaladinnerValidation"] = drpResturant.Text + " " + "CheckIn Date and CheckOut Date Should be Between Start Date and End Date.";
        //            }

        //        }
        //    }
        //}

        //protected void galadinnerDateValidation(GridView gv_galadinner)
        //{
        //    TextBox galadinnerdate = new TextBox();
        //    TextBox galadinnerdate0 = new TextBox();
        //    galadinnerdate = (TextBox)gv_galadinner.Rows[0].FindControl("txtDate");

        //    try
        //    {
        //        for (int i = 1; i < gv_galadinner.Rows.Count; i++)
        //        {
        //            galadinnerdate = (TextBox)gv_galadinner.Rows[i].FindControl("txtDate");
        //            for (int j = 0; j < gv_galadinner.Rows.Count; j++)
        //            {
        //                galadinnerdate0 = (TextBox)gv_galadinner.Rows[j].FindControl("txtDate");

        //                if ((galadinnerdate0.Text == galadinnerdate.Text) && (i != j))
        //                {
        //                    Master.DisplayMessage("Gala Dinner Not Allowed Same Date.", "successMessage", 5000);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}

        //protected void galadinnerDateValidation(GridView gv_galadinner)
        //{
        //    TextBox galadinnerdate = new TextBox();
        //    TextBox galadinnerdate0 = new TextBox();
        //    galadinnerdate = (TextBox)gv_galadinner.Rows[0].FindControl("txtDate");

        //    try
        //    {
        //        for (int i = 1; i < gv_galadinner.Rows.Count; i++)
        //        {
        //            galadinnerdate = (TextBox)gv_galadinner.Rows[i].FindControl("txtDate");
        //            for (int j = 0; j < gv_galadinner.Rows.Count; j++)
        //            {
        //                galadinnerdate0 = (TextBox)gv_galadinner.Rows[j].FindControl("txtDate");

        //                if ((galadinnerdate0.Text == galadinnerdate.Text) && (i != j))
        //                {
        //                    Master.DisplayMessage("Gala Dinner Not Allowed Same Date.", "successMessage", 5000);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        #endregion

        #region NO OF ROOMS
    
        protected void btnSaverooms_Click(object sender, EventArgs e)
        {
            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
            for (int j = 0; j < dtCities.Tables[0].Rows.Count; j++)
            {
                if (j == 0)
                {
                    insertNoofRoomsHotelDetails(GridHotel1, lblHotel1.Text, j);
                }
                else if (j == 1)
                {
                    insertNoofRoomsHotelDetails(GridHotel2, lblHotel2.Text, j);
                }
                else if (j == 2)
                {
                    insertNoofRoomsHotelDetails(GridHotel3, lblHotel3.Text, j);
                }
                else if (j == 3)
                {
                    insertNoofRoomsHotelDetails(GridHotel4, lblHotel4.Text, j);
                }
                else if (j == 4)
                {
                    insertNoofRoomsHotelDetails(GridHotel5, lblHotel5.Text, j);
                }
                else if (j == 5)
                {
                    insertNoofRoomsHotelDetails(GridHotel6, lblHotel6.Text, j);
                }
                else if (j == 6)
                {
                    insertNoofRoomsHotelDetails(GridHotel7, lblHotel7.Text, j);
                }
                else if (j == 7)
                {
                    insertNoofRoomsHotelDetails(GridHotel8, lblHotel8.Text, j);
                }
                else if (j == 8)
                {
                    insertNoofRoomsHotelDetails(GridHotel9, lblHotel9.Text, j);
                }
                else if (j == 9)
                {
                    insertNoofRoomsHotelDetails(GridHotel10, lblHotel10.Text, j);
                }

            }
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Rooms Save Successfully..')", true);
        }

        protected void insertNoofRoomsHotelDetails(GridView gv, string City_Name, int i)
        {
            DataSet ds = objEditUpdateGITInformation.GetHotelName(int.Parse(tourId), City_Name);
            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
            if (i == 0)
            {
                RoomValidation(gv, ds);
            }
            else
            {
                HotelWithTwoCity_RoomValidation(GridHotel1, gv, lblHotel1.Text, City_Name, ds);
            }
            if (f_rooms == true)
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    DropDownList drpHotel = (DropDownList)item.FindControl("drpHotelName");
                    DropDownList drpRoomtype = (DropDownList)item.FindControl("drpRoomType");

                    TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                    TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                    TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");

                    int SingleRoom = 0;
                    int DoubleRoom = 0;
                    int TripleRoom = 0;
                    if (txtSingleRoom.Text != "")
                    {
                        SingleRoom = int.Parse(txtSingleRoom.Text);
                    }
                    if (txtDoubleRoom.Text != "")
                    {
                        DoubleRoom = int.Parse(txtDoubleRoom.Text);
                    }
                    if (txtTripleRoom.Text != "")
                    {
                        TripleRoom = int.Parse(txtTripleRoom.Text);
                    }

                    objInsertGitDetails.insertNoOfRooms(drpHotel.Text, drpRoomtype.Text, int.Parse(Request.QueryString["TOURID"].ToString()), SingleRoom, DoubleRoom, TripleRoom);
                }
            }
        }

        protected void RoomValidation(GridView gv, DataSet ds)
        {
            int adult = 0;
            int CWB = 0;
            int CNB = 0;
            int row = 0;
            DataSet dsRooms = objGitDetail.fetchAllHotelsForRoomValidation("GET_HOTELS_FOR_VALIDATION", int.Parse(Request.QueryString["TOURID"].ToString()));
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                DropDownList drpHotel = (DropDownList)item.FindControl("drpHotelName");
                DropDownList drpRoomtype = (DropDownList)item.FindControl("drpRoomType");

                TextBox txtSingleRoom = (TextBox)item.FindControl("txtSingleRoom");
                TextBox txtDoubleRoom = (TextBox)item.FindControl("txtDoubleRoom");
                TextBox txtTripleRoom = (TextBox)item.FindControl("txtTripleRoom");
                int SingleRoom = 0;
                int DoubleRoom = 0;
                int TripleRoom = 0;

                if (txtSingleRoom.Text != "")
                {
                    SingleRoom = int.Parse(txtSingleRoom.Text);
                }
                if (txtDoubleRoom.Text != "")
                {
                    DoubleRoom = int.Parse(txtDoubleRoom.Text);
                }
                if (txtTripleRoom.Text != "")
                {
                    TripleRoom = int.Parse(txtTripleRoom.Text);
                }
                adult = (1 * (SingleRoom)) + (2 * (DoubleRoom)) + (3 * (TripleRoom));
                CWB = (1 * (DoubleRoom));
                CNB = (1 * (DoubleRoom)) + (1 * (TripleRoom));
                if (ds.Tables[0].Rows[i]["IS_BOOKED"].ToString() == "True")
                {
                    if ((adult < int.Parse(dsRooms.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()))
                        || (CWB < int.Parse(dsRooms.Tables[0].Rows[0]["NO_OF_CWB"].ToString()))
                        || (CNB < int.Parse(dsRooms.Tables[0].Rows[0]["NO_OF_CNB"].ToString())))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Rooms are not match in ROW " + (i + 1) + " of " + ds.Tables[0].Rows[0]["CITY_NAME"].ToString() + "')", true);
                        f_rooms = false;
                        break;
                    }
                    ViewState["single_old"] = txtSingleRoom.Text;
                    ViewState["double_old"] = txtDoubleRoom.Text;
                    ViewState["triple_old"] = txtTripleRoom.Text;
                }
                for (int item1 = i + 1; item1 < gv.Rows.Count; item1++)
                {

                    if (ds.Tables[0].Rows[item1]["IS_BOOKED"].ToString() == "True")
                    {
                        TextBox Single_Room1 = (TextBox)gv.Rows[item1].FindControl("txtSingleRoom");
                        TextBox Double_Room1 = (TextBox)gv.Rows[item1].FindControl("txtDoubleRoom");
                        TextBox Triple_Room1 = (TextBox)gv.Rows[item1].FindControl("txtTripleRoom");

                        if ((SingleRoom.ToString() != Single_Room1.Text) && (DoubleRoom.ToString() != Double_Room1.Text) && (TripleRoom.ToString() != Triple_Room1.Text) && (item1 != i))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Rooms are not match in ROW " + (item1 + 1) + " of " + ds.Tables[0].Rows[0]["CITY_NAME"].ToString() + "')", true);
                            f_rooms = false;
                            break;
                        }
                    }
                }

            }


        }

        protected void HotelWithTwoCity_RoomValidation(GridView gvHotel, GridView gvHotel2, String City_Name, String City_Name1, DataSet dsBook)
        {
            DataSet ds = objEditUpdateGITInformation.GetHotelName(int.Parse(tourId), City_Name);
            RoomValidation(gvHotel, ds);
            string single = "";
            string doubl = "";
            string triple = "";
            if (ViewState["single_old"] != null)
            {
                single = ViewState["single_old"].ToString();
            }
            if (ViewState["double_old"] != null)
            {
                doubl = ViewState["double_old"].ToString();
            }
            if (ViewState["triple_old"] != null)
            {
                triple = ViewState["triple_old"].ToString();
            }


            foreach (GridViewRow item in gvHotel2.Rows)
            {

                int i = item.DataItemIndex;

                TextBox txtSingle = (TextBox)item.FindControl("txtSingleRoom");
                TextBox txtdouble = (TextBox)item.FindControl("txtDoubleRoom");
                TextBox txttriple = (TextBox)item.FindControl("txtTripleRoom");
                if (dsBook.Tables[0].Rows[i]["IS_BOOKED"].ToString() == "True")
                {
                    if (single != txtSingle.Text || doubl != txtdouble.Text || triple != txttriple.Text)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Rooms are not match in ROW " + (i + 1) + " Of " + City_Name1 + "')", true);
                        f_rooms = false;
                    }
                }
            }
        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/GitGroupInformation.aspx?TOURID=" + tourId);
        }

        #region meal and additional services Update

        protected void btnMealsSave_Click(object sender, EventArgs e)
        {
            DataSet dtCities = objGitDetail.fetchGitCities("FETCH_GIT_PACKAGE_CITY", Session["packgeId"].ToString());
            for (int j = 0; j < dtCities.Tables[0].Rows.Count; j++)
            {
                if (j == 0)
                {
                    insertResturantDetails_VEG(gridMeal1, lblMealCity1.Text, upMeal1);
                }
                if (j == 1)
                {
                    insertResturantDetails_VEG(gridMeal2, lblMealCity2.Text, upMeal2);
                }
                if (j == 2)
                {
                    insertResturantDetails_VEG(gridMeal3, lblMealCity3.Text, upMeal3);
                }
                if (j == 3)
                {
                    insertResturantDetails_VEG(gridMeal4, lblMealCity4.Text, upMeal4);
                }
                if (j == 4)
                {
                    insertResturantDetails_VEG(gridMeal5, lblMealCity5.Text, upMeal5);
                }
                if (j == 5)
                {
                    insertResturantDetails_VEG(gridMeal6, lblMealCity6.Text, upMeal6);
                }
                if (j == 6)
                {
                    insertResturantDetails_VEG(gridMeal7, lblMealCity7.Text, upMeal7);
                }
                if (j == 7)
                {
                    insertResturantDetails_VEG(gridMeal8, lblMealCity8.Text, upMeal8);
                }
                if (j == 8)
                {
                    insertResturantDetails_VEG(gridMeal9, lblMealCity9.Text, upMeal9);
                }
                if (j == 9)
                {
                    insertResturantDetails_VEG(gridMeal10, lblMealCity10.Text, upMeal10);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Meals Save Successfully..')", true);
        }

        protected void insertResturantDetails_VEG(GridView gv, string CITY_NAME, UpdatePanel uppanel)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {

                    DropDownList drpReturant = (DropDownList)item.FindControl("drpResturant");
                    DropDownList drpMealType = (DropDownList)item.FindControl("drpMealType");


                    TextBox txtDate = (TextBox)item.FindControl("txtDate");

                    CheckBox chkPackgeFlag = (CheckBox)item.FindControl("chkMeal");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    TextBox txtVage = (TextBox)item.FindControl("txtVage");
                    TextBox txtNonVage = (TextBox)item.FindControl("txtNonVag");
                    TextBox txtJain = (TextBox)item.FindControl("txtJain");

                    bool mealflag = false;
                    if (chkPackgeFlag.Checked)
                    {
                        mealflag = true;
                    }

                    if (drpReturant.Text != "" && drpMealType.Text != "" && txtDate.Text != "")
                    {
                        objInsertGitDetails.UPDATE_TIME_VEG_NON_VEG_JAIN_IN_MEALS(drpReturant.Text, drpMealType.Text, txtDate.Text, txtVage.Text, txtNonVage.Text, txtJain.Text, txttime.SelectedDate.ToString());
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

        protected void btnAdditionalSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in gridAddServices.Rows)
                {
                    TextBox txtServices = (TextBox)item.FindControl("txtServices");
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");

                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtNoPax = (TextBox)item.FindControl("txtNoPax");

                    TextBox txtNetPrice = (TextBox)item.FindControl("txtNetPrice");
                    TextBox txtSellPrice = (TextBox)item.FindControl("txtSellPrice");

                    TextBox txtFrom = (TextBox)item.FindControl("txtFrom");
                    TextBox txtTo = (TextBox)item.FindControl("txtTo");

                    TextBox txtPassanger = (TextBox)item.FindControl("txtNoOfPassanger");
                    CheckBox chkAditional = (CheckBox)item.FindControl("chkAditional");

                    if (txtServices.Text != "" && drpSupplier.Text != "")
                    {
                        objInsertGitDetails.UpdateAdditionalService_flag(txtServices.Text, drpSupplier.Text, txtDate.Text, txtFrom.Text, txtTo.Text, txtPassanger.Text, chkAditional.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Save Passenger Successfully..')", true);
            }
        }
        #endregion

       

        protected void browserInformation()
        {
             System.Web.HttpBrowserCapabilities browser = Request.Browser;
            
        }

        protected void loadTimeForSiteSeeing(GridView gv, int Index, string city)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                if (Index == item.DataItemIndex)
                {
                    DropDownList drpSiteName = (DropDownList)item.FindControl("drpSiteDetails");
                    DropDownList drpSiteTime = (DropDownList)item.FindControl("drpSiteTime");

                    DataSet ds = objGitDetail.getTimeForSiteSeeing("GET_TIME_FOR_SITE_SEEING_GIT", drpSiteName.Text, city);
                    binddropdownlist(drpSiteTime, ds);

                }

            }
        }

        protected void TransportPackage()
        {
            foreach (GridViewRow item in GridTrasport.Rows)
            {
                if (Session["OrderStatus"].ToString() == "To Be Reconfirmed" || Session["OrderStatus"].ToString() == "Reconfirmed")
                {
                    if (GridTrasport.HeaderRow != null)
                    {
                        Label lblAdultRate = (Label)GridTrasport.HeaderRow.FindControl("lblDate");
                        lblAdultRate.Visible = true;
                        Label lblchildRate = (Label)GridTrasport.HeaderRow.FindControl("lblTime");
                        lblchildRate.Visible = true;

                        TextBox txtDate = (TextBox)item.FindControl("txtDate");
                        txtDate.Visible = true;

                        RadTimePicker rtpBox = (RadTimePicker)item.FindControl("rdtpTime");
                        rtpBox.Visible = true;
                    }
                }
                else
                {
                    Label lblAdultRate = (Label)GridTrasport.HeaderRow.FindControl("lblDate");
                    lblAdultRate.Visible = false;
                    Label lblchildRate = (Label)GridTrasport.HeaderRow.FindControl("lblTime");
                    lblchildRate.Visible = false;

                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    txtDate.Visible = false;

                    RadTimePicker rtpBox = (RadTimePicker)item.FindControl("rdtpTime");
                    rtpBox.Visible = false;
                }

                if (Session["OrderStatus"].ToString() == "To Be Reconfirmed" || Session["OrderStatus"].ToString() == "Reconfirmed")
                {
                    Label Detailid = (Label)item.FindControl("lbltp_detialid");
                    DataSet ds = objInsertGitDetails.FetchDateOfTransfer(int.Parse(tourId), int.Parse(Detailid.Text));
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows[0]["DATE"].ToString() != "")
                        {
                            txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                        }
                        RadTimePicker rtpBox = (RadTimePicker)item.FindControl("rdtpTime");
                        if (ds.Tables[0].Rows[0]["TIME"].ToString() != "")
                        {
                            rtpBox.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["TIME"].ToString());
                        }
                    }
                }
            }
        }

        protected void btnCancellationRequest_Click(object sender, EventArgs e)
        {
            objInsertGitDetails.changeQuotestatus(int.Parse(Request.QueryString["TOURID"].ToString()), "Request For Cancellation");
            Master.DisplayMessage("Your Request for cancellation has been sent for approval.", "successMessage", 5000);
        }

        protected void btnApproveCancellation_Click(object sender, EventArgs e)
        {
            objInsertGitDetails.changeQuotestatus(int.Parse(Request.QueryString["TOURID"].ToString()), "Canceled");
            objInsertGitDetails.insertCancelltionFees(int.Parse(Request.QueryString["TOURID"].ToString()), Session["CancellationFees"].ToString());
            Master.DisplayMessage("Your Request for cancellation has been Approved.", "successMessage", 5000);
        }

        protected void btnDisApproveCancellation_Click(object sender, EventArgs e)
        {
            objInsertGitDetails.changeQuotestatus(int.Parse(Request.QueryString["TOURID"].ToString()), "Reconfirmed");
            Master.DisplayMessage("Your Request for cancellation has been Disapproved.", "successMessage", 5000);
        }
    }
}




