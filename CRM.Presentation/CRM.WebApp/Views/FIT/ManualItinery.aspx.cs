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
using System.Linq;

namespace CRM.WebApp.Views.FIT
{
    public partial class ManualItinery : System.Web.UI.Page
    {
        ManualItineryDa objmanualitinery = new ManualItineryDa();

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        string values;
        string Svalues;
        string Pvalues;
        string CompId;
        string DeptId;
        string RoleId;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objmanualitinery.CommonSp("FETCH_AGENT_COMPANY_NAME_WITH_ID");
                binddropdownlistmain(drpAgent, ds);
                fillDeatils();

                if (Request.QueryString["manual_itinery_id"] != null)
                {
                    ViewState["ID"] = int.Parse(Request.QueryString["manual_itinery_id"].ToString());
                    lnkbtn.Attributes.Add("style", "display");
                    lnkbtn.HRef = "~/Views/FIT/ManualItinery/" + ViewState["ID"].ToString() + "/ManualItinery.pdf";
                    if (ViewState["ID"].ToString() != "")
                    {
                        /* -------------- Main  ----------------- */

                        DataSet dsmain = objmanualitinery.FetchAllDetail("FETCH_MAIN_MANUAL_ITINERY_FOR_EDIT", int.Parse(ViewState["ID"].ToString()));
                        DataSet dsagent = objmanualitinery.CommonSp("FETCH_AGENT_COMPANY_NAME_WITH_ID");
                        binddropdownlistmain(drpAgent, dsagent);
                        drpAgent.Text = dsmain.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                        drpAgent.SelectedValue = dsmain.Tables[0].Rows[0]["CUST_ID"].ToString();
                        DataSet dsinvoice = objmanualitinery.fetchInvoice("FETCH_INVOICE_FOR_MANUAL_ITINERY", int.Parse(drpAgent.SelectedValue.ToString()));
                        binddropdownlist(drpInvoice, dsinvoice);
                        drpInvoice.Text = dsmain.Tables[0].Rows[0]["INVOICE_NO"].ToString();

                        txtArrival_Flight.Text = dsmain.Tables[0].Rows[0]["ARRIVAL_FLIGHT"].ToString();
                        if (dsmain.Tables[0].Rows[0]["ARRIVAL_TIME"].ToString() != "")
                        {
                            rtparrival.SelectedDate = DateTime.Parse(dsmain.Tables[0].Rows[0]["ARRIVAL_TIME"].ToString());
                        }
                        txtDeparture_Flight.Text = dsmain.Tables[0].Rows[0]["DEPARTURE_FLIGHT"].ToString();
                        if (dsmain.Tables[0].Rows[0]["DEPARTURE_TIME"].ToString() != "")
                        {
                            rtpDeparture.SelectedDate = DateTime.Parse(dsmain.Tables[0].Rows[0]["DEPARTURE_TIME"].ToString());
                        }
                        
                        txtNoroomSingle.Text = dsmain.Tables[0].Rows[0]["NO_OF_SINGLE_ROOM"].ToString();
                        txtNoroomDouble.Text = dsmain.Tables[0].Rows[0]["NO_OF_DOUBLE_ROOM"].ToString();
                        txtNoroomTriple.Text = dsmain.Tables[0].Rows[0]["NO_OF_TRIPLE_ROOM"].ToString();
                        txtmeeting.Text = dsmain.Tables[0].Rows[0]["MEETING_POINT"].ToString();
                        txtRemarks.Text = dsmain.Tables[0].Rows[0]["REMARKS"].ToString();

                        /*------------ Sales Invoice Header ------------*/

                        DataSet dssales = objmanualitinery.fetchInvoiceDetail("FETCH_INVOICE_DETAILS_FOR_MANUAL_ITINERY", drpInvoice.Text);
                        lblClientName.Text = dssales.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
                        lblNoOfAdult.Text = dssales.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
                        lblNoOfChild.Text = dssales.Tables[0].Rows[0]["NO_OF_CHILD"].ToString();
                        lblNOofCWB.Text = dssales.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
                        lblNoOfCNB.Text = dssales.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
                        lblNoOfInfant.Text = dssales.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
                        lblArrival.Text = dssales.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString();
                        lblDeparture.Text = dssales.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString();
                        txtArrival_Flight.Focus();




                        /* ----------------  Hotel  ------------------------- */

                        DataSet dsCity = objmanualitinery.CommonSp("GET_CITY_NAME_FOR_MANUAL_ITINERY");
                        DataSet dshotel = objmanualitinery.FetchAllDetail("FETCH_HOTEL_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));
                        if (dshotel.Tables[0].Rows.Count != 0)
                        {
                            for (int j = 0; j < dshotel.Tables[0].Rows.Count; j++)
                            {
                                foreach (GridViewRow item in GridHotel.Rows)
                                {
                                    if (j == item.DataItemIndex)
                                    {
                                        Label lblmain = (Label)item.FindControl("lblmain");
                                        DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                                        //DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                                        //DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                                        TextBox txtHotelName = (TextBox)item.FindControl("txtHotelName");
                                        TextBox txtRoomType = (TextBox)item.FindControl("txtRoomType");
                                        TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                                        TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");

                                        drpCity.Text = dshotel.Tables[0].Rows[j]["CITY_NAME"].ToString();
                                        binddropdownlist(drpCity, dsCity);
                                        //if (drpCity.Text != "")
                                        //{
                                        //    DataSet dshotel1 = objmanualitinery.fetchComboDataforHotel("GET_HOTEL_NAME_CITY_WISE", drpCity.Text);
                                        //    binddropdownlist(drpHotelName, dshotel1);
                                        //    drpHotelName.Text = dshotel.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                                        //}

                                        //if (drpHotelName.Text != "")
                                        //{
                                        //    DataSet ds1 = objmanualitinery.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_FIT_HOTEL_CITY_WISE", drpHotelName.Text, drpCity.Text);

                                        //    binddropdownlist(drpRoomType, ds1);

                                        //    drpRoomType.Text = dshotel.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();
                                        //}
                                        txtHotelName.Text = dshotel.Tables[0].Rows[j]["HOTEL_NAME"].ToString();
                                        txtRoomType.Text = dshotel.Tables[0].Rows[j]["ROOM_TYPE_NAME"].ToString();
                                        txtCheckInDate.Text = dshotel.Tables[0].Rows[j]["CHECKIN"].ToString();
                                        txtCheckOutDate.Text = dshotel.Tables[0].Rows[j]["CHECKOUT"].ToString();
                                        lblmain.Text = dshotel.Tables[0].Rows[j]["HOTEL_ITINERY_ID"].ToString();
                                    }
                                }
                                if (j < dshotel.Tables[0].Rows.Count - 1)
                                {
                                    AddHotels(GridHotel, upHotel1);
                                }
                            }
                        }

                        /* ----------------  Schedule  ------------------------- */


                        DataSet dsschedule = objmanualitinery.FetchAllDetail("FETCH_SCHEDULE_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));
                        if (dsschedule.Tables[0].Rows.Count != 0)
                        {
                            for (int j = 0; j < dsschedule.Tables[0].Rows.Count; j++)
                            {
                                foreach (GridViewRow item in grdSchedule.Rows)
                                {
                                    if (j == item.DataItemIndex)
                                    {
                                        Label lblSchedule = (Label)item.FindControl("lblSchedule");
                                        DropDownList drpCitySchedule = (DropDownList)item.FindControl("drpCitySchedule");
                                        DropDownList drpSICPVT = (DropDownList)item.FindControl("drpSICPVT");
                                        TextBox txtDate = (TextBox)item.FindControl("txtDate");
                                        TextBox txtTSM = (TextBox)item.FindControl("txtTSM");
                                        RadTimePicker rtpPick = (RadTimePicker)item.FindControl("rtpPick");
                                        TextBox txtsignature = (TextBox)item.FindControl("txtsignature");


                                        drpCitySchedule.Text = dsschedule.Tables[0].Rows[j]["CITY_NAME"].ToString();
                                        binddropdownlist(drpCitySchedule, dsCity);
                                        if (dsschedule.Tables[0].Rows[j]["SIC_PVT_NAME"].ToString() != "")
                                        {
                                            drpSICPVT.Text = dsschedule.Tables[0].Rows[j]["SIC_PVT_NAME"].ToString();
                                            DataSet dsSIC = objmanualitinery.CommonSp("GET_SIC_PVT_FOR_MANUAL_ITINERY");
                                            binddropdownlist(drpSICPVT, dsSIC);
                                        }
                                        

                                        txtDate.Text = dsschedule.Tables[0].Rows[j]["SCHEDULE_DATE"].ToString();
                                        txtTSM.Text = dsschedule.Tables[0].Rows[j]["TSM"].ToString();
                                        if (dsschedule.Tables[0].Rows[j]["PICKTIME"].ToString() != "")
                                        {
                                            rtpPick.SelectedDate = DateTime.Parse(dsschedule.Tables[0].Rows[j]["PICKTIME"].ToString());
                                        }
                                        txtsignature.Text = dsschedule.Tables[0].Rows[j]["SIGNATURE"].ToString();
                                        lblSchedule.Text = dsschedule.Tables[0].Rows[j]["SCHEDULE_ITINERY_ID"].ToString();
                                    }
                                }
                                if (j < dsschedule.Tables[0].Rows.Count - 1)
                                {
                                    AddSchedule(grdSchedule, UpSchedule);
                                }
                            }
                        }

                        /* ----------------  Passenger  ------------------------- */

                        DataSet dspassenger = objmanualitinery.FetchAllDetail("FETCH_TRAVELLING_PASSENGERS_FOR_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));
                        if (dspassenger.Tables[0].Rows.Count != 0)
                        {
                            for (int j = 0; j < dspassenger.Tables[0].Rows.Count; j++)
                            {
                                foreach (GridViewRow item in gvpass.Rows)
                                {
                                    if (j == item.DataItemIndex)
                                    {
                                        Label lblPassenger = (Label)item.FindControl("lblPassenger");
                                        TextBox txtname = (TextBox)item.FindControl("txtName");
                                        TextBox txtpasspot = (TextBox)item.FindControl("txtPassportno");
                                        DropDownList drpNationality = (DropDownList)item.FindControl("drpNationality");

                                        txtname.Text = dspassenger.Tables[0].Rows[j]["NAME"].ToString();
                                        txtpasspot.Text = dspassenger.Tables[0].Rows[j]["PASSOPRT_NO"].ToString();
                                        if (dspassenger.Tables[0].Rows[j]["NATIONALITY_NAME"].ToString() != "")
                                        {
                                            drpNationality.Text = dspassenger.Tables[0].Rows[j]["NATIONALITY_NAME"].ToString();
                                            DataSet dsnationality = objmanualitinery.CommonSp("FETCH_NATIONALITY");
                                            binddropdownlist(drpNationality, dsnationality);
                                        }
                                        lblPassenger.Text = dspassenger.Tables[0].Rows[j]["TRAVELLING_PASSENGERS_ID"].ToString();
                                    }

                                }
                                if (j < dspassenger.Tables[0].Rows.Count - 1)
                                {
                                    AddPassenger(gvpass, upPassenger);
                                }
                            }
                        }

                        upHotel1.Update();
                        UpSchedule.Update();
                        upPassenger.Update();
                    }

                }
            }
            UpdatePanel_Manual_Itinery.Update();
        }

        #region Function

        protected void fillDeatils()
        {
            try
            {
                AddHotels(GridHotel, upHotel1);
                AddSchedule(grdSchedule, UpSchedule);
                AddPassenger(gvpass, upPassenger);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

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

        protected void binddropdownlistmain(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataValueField = "CUST_ID";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }

        #endregion

        #region Add function

        protected void AddHotels(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet ds = objmanualitinery.CommonSp("GET_CITY_NAME_FOR_MANUAL_ITINERY");

                foreach (GridViewRow item in gv.Rows)
                {
                    Label lblmain = (Label)item.FindControl("lblmain");
                    DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                    //DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    //DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtHotelName = (TextBox)item.FindControl("txtHotelName");
                    TextBox txtRoomType = (TextBox)item.FindControl("txtRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("City");
                        dt.Columns.Add("HotelName");
                        dt.Columns.Add("RoomType");
                        dt.Columns.Add("CheckInDate");
                        dt.Columns.Add("CheckOutDate");
                        dt.Columns.Add("Id");

                    }

                    DataRow dr = dt.NewRow();
                    dr["City"] = drpCity.Text;
                    dr["HotelName"] = txtHotelName.Text;
                    dr["RoomType"] = txtRoomType.Text;
                    dr["CheckInDate"] = txtCheckInDate.Text;
                    dr["CheckOutDate"] = txtCheckOutDate.Text;
                    dr["Id"] = lblmain.Text;
                    dt.Rows.Add(dr);
                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("City");
                        dt.Columns.Add("HotelName");
                        dt.Columns.Add("RoomType");
                        dt.Columns.Add("CheckInDate");
                        dt.Columns.Add("CheckOutDate");
                        dt.Columns.Add("Id");
                    }

                    DataRow dr = dt.NewRow();
                    dr["City"] = "";
                    dr["HotelName"] = "";
                    dr["RoomType"] = "";
                    dr["CheckInDate"] = "";
                    dr["CheckOutDate"] = "";
                    dr["Id"] = "0";
                    dt.Rows.Add(dr);
                    gv.DataSource = dt;
                    gv.DataBind();
                    uppanel.Update();
                }

                if (count != 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["Id"] = "0";
                    dt.Rows.Add(dr1);
                }

                gv.DataSource = dt;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    Label lblmain = (Label)item.FindControl("lblmain");
                    DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                    //DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    //DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtHotelName = (TextBox)item.FindControl("txtHotelName");
                    TextBox txtRoomType = (TextBox)item.FindControl("txtRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {


                            binddropdownlist(drpCity, ds);
                            drpCity.Text = dt.Rows[itm]["City"].ToString();
                            //if (drpCity.Text != "")
                            //{
                            //    DataSet dshotel = objmanualitinery.fetchComboDataforHotel("GET_HOTEL_NAME_CITY_WISE", drpCity.Text);
                            //    binddropdownlist(drpHotelName, dshotel);
                                txtHotelName.Text = dt.Rows[itm]["HotelName"].ToString();
                            //}

                            //if (drpHotelName.Text != "")
                            //{
                            //    DataSet ds1 = objmanualitinery.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_FIT_HOTEL_CITY_WISE", drpHotelName.Text, drpCity.Text);

                            //    binddropdownlist(drpRoomType, ds1);

                                txtRoomType.Text = dt.Rows[itm]["RoomType"].ToString();
                            //}
                            txtCheckInDate.Text = dt.Rows[itm]["CheckInDate"].ToString();
                            txtCheckOutDate.Text = dt.Rows[itm]["CheckOutDate"].ToString();
                            lblmain.Text = dt.Rows[itm]["Id"].ToString();
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

        protected void AddSchedule(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet dsCity = objmanualitinery.CommonSp("GET_CITY_NAME_FOR_MANUAL_ITINERY");
                DataSet dsSIC = objmanualitinery.CommonSp("GET_SIC_PVT_FOR_MANUAL_ITINERY");

                foreach (GridViewRow item in gv.Rows)
                {
                    Label lblSchedule = (Label)item.FindControl("lblSchedule");
                    DropDownList drpCitySchedule = (DropDownList)item.FindControl("drpCitySchedule");
                    DropDownList drpSICPVT = (DropDownList)item.FindControl("drpSICPVT");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtTSM = (TextBox)item.FindControl("txtTSM");
                    RadTimePicker rtpPick = (RadTimePicker)item.FindControl("rtpPick");
                    TextBox txtsignature = (TextBox)item.FindControl("txtsignature");
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("CitySchedule");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("TSM");
                        dt.Columns.Add("PickupTime");
                        dt.Columns.Add("SICPVT");
                        dt.Columns.Add("Signature");
                        dt.Columns.Add("Id");
                    }

                    DataRow dr = dt.NewRow();
                    dr["CitySchedule"] = drpCitySchedule.Text;
                    dr["Date"] = txtDate.Text;
                    dr["TSM"] = txtTSM.Text;
                    dr["PickupTime"] = rtpPick.SelectedDate;
                    dr["SICPVT"] = drpSICPVT.Text;
                    dr["Signature"] = txtsignature.Text;
                    dr["Id"] = lblSchedule.Text;
                    dt.Rows.Add(dr);
                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("CitySchedule");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("TSM");
                        dt.Columns.Add("PickupTime");
                        dt.Columns.Add("SICPVT");
                        dt.Columns.Add("Signature");
                        dt.Columns.Add("Id");
                    }

                    DataRow dr = dt.NewRow();
                    dr["CitySchedule"] = "";
                    dr["Date"] = "";
                    dr["TSM"] = "";
                    dr["PickupTime"] = "";
                    dr["SICPVT"] = "";
                    dr["Signature"] = "";
                    dr["Id"] = "0";
                    dt.Rows.Add(dr);
                    gv.DataSource = dt;
                    gv.DataBind();
                    uppanel.Update();
                }

                if (count != 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["Id"] = "0";
                    dt.Rows.Add(dr1);
                }

                gv.DataSource = dt;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    Label lblSchedule = (Label)item.FindControl("lblSchedule");
                    DropDownList drpCitySchedule = (DropDownList)item.FindControl("drpCitySchedule");
                    DropDownList drpSICPVT = (DropDownList)item.FindControl("drpSICPVT");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtTSM = (TextBox)item.FindControl("txtTSM");
                    RadTimePicker rtpPick = (RadTimePicker)item.FindControl("rtpPick");
                    TextBox txtsignature = (TextBox)item.FindControl("txtsignature");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            binddropdownlist(drpCitySchedule, dsCity);
                            drpCitySchedule.Text = dt.Rows[itm]["CitySchedule"].ToString();
                            binddropdownlist(drpSICPVT, dsSIC);
                            drpSICPVT.Text = dt.Rows[itm]["SICPVT"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtTSM.Text = dt.Rows[itm]["TSM"].ToString();
                            if (dt.Rows[itm]["PickupTime"].ToString() != "")
                            {
                                rtpPick.SelectedDate = DateTime.Parse(dt.Rows[itm]["PickupTime"].ToString());
                            }
                            txtsignature.Text = dt.Rows[itm]["Signature"].ToString();
                            lblSchedule.Text = dt.Rows[itm]["Id"].ToString();
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

        protected void AddPassenger(GridView gv, UpdatePanel uppanel)
        {
            try
            {

                int count = gv.Rows.Count;
                int count1 = count + 1;
                DataTable dt = new DataTable();

                DataSet dsnationality = objmanualitinery.CommonSp("FETCH_NATIONALITY");


                foreach (GridViewRow item in gv.Rows)
                {
                    Label lblPassenger = (Label)item.FindControl("lblPassenger");
                    TextBox txtname = (TextBox)item.FindControl("txtName");
                    TextBox txtpasspot = (TextBox)item.FindControl("txtPassportno");
                    DropDownList drpNationality = (DropDownList)item.FindControl("drpNationality");

                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Nationality");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("PassportNo");
                        dt.Columns.Add("Id");

                    }

                    DataRow dr = dt.NewRow();
                    dr["Nationality"] = drpNationality.Text;
                    dr["Name"] = txtname.Text;
                    dr["PassportNo"] = txtpasspot.Text;
                    dr["Id"] = lblPassenger.Text;
                    dt.Rows.Add(dr);
                }

                if (count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Nationality");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("PassportNo");
                        dt.Columns.Add("Id");
                    }

                    DataRow dr = dt.NewRow();
                    dr["Nationality"] = "";
                    dr["Name"] = "";
                    dr["PassportNo"] = "";
                    dr["Id"] = "0";
                    dt.Rows.Add(dr);
                    gv.DataSource = dt;
                    gv.DataBind();
                    uppanel.Update();
                }

                if (count != 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["Id"] = "0";
                    dt.Rows.Add(dr1);
                }

                gv.DataSource = dt;
                gv.DataBind();


                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    Label lblPassenger = (Label)item.FindControl("lblPassenger");
                    TextBox txtname = (TextBox)item.FindControl("txtName");
                    TextBox txtpasspot = (TextBox)item.FindControl("txtPassportno");
                    DropDownList drpNationality = (DropDownList)item.FindControl("drpNationality");

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            txtname.Text = dt.Rows[itm]["Name"].ToString();
                            txtpasspot.Text = dt.Rows[itm]["PassportNo"].ToString();
                            binddropdownlist(drpNationality, dsnationality);
                            drpNationality.Text = dt.Rows[itm]["Nationality"].ToString();
                            lblPassenger.Text = dt.Rows[itm]["Id"].ToString();
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

        #endregion

        #region Remove Function

        protected void RemoveHotel(GridView gv, int rowIndex, UpdatePanel uppanel)
        {
            try
            {

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                DataSet ds = objmanualitinery.CommonSp("GET_CITY_NAME_FOR_MANUAL_ITINERY");
                int count = gv.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    dt1.Rows.Add();
                }

                foreach (GridViewRow item in gv.Rows)
                {
                    if (rowIndex != item.DataItemIndex)
                    {
                        Label lblmain = (Label)item.FindControl("lblmain");
                        DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                       // DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                       // DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                        TextBox txtHotelName = (TextBox)item.FindControl("txtHotelName");
                        TextBox txtRoomType = (TextBox)item.FindControl("txtRoomType");
                        TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                        TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");

                        if (dt.Columns.Count == 0)
                        {
                            dt.Columns.Add("City");
                            dt.Columns.Add("HotelName");
                            dt.Columns.Add("RoomType");
                            dt.Columns.Add("CheckInDate");
                            dt.Columns.Add("CheckOutDate");
                            dt.Columns.Add("Id");
                        }

                        DataRow dr = dt.NewRow();
                        dr["City"] = drpCity.Text;
                        dr["HotelName"] = txtHotelName.Text;
                        dr["RoomType"] = txtRoomType.Text;
                        dr["CheckInDate"] = txtCheckInDate.Text;
                        dr["CheckOutDate"] = txtCheckOutDate.Text;
                        dr["Id"] = lblmain.Text;

                        values += "," + lblmain.Text;
                        dt.Rows.Add(dr);

                    }
                }
                ViewState["Hotel_ID"] = values;
                gv.DataSource = dt;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    //if (itm >= rowIndex)
                    //{
                    //    itm = itm + 1;
                    //}
                    Label lblmain = (Label)item.FindControl("lblmain");
                    DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                   // DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                    //DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");
                    TextBox txtHotelName = (TextBox)item.FindControl("txtHotelName");
                    TextBox txtRoomType = (TextBox)item.FindControl("txtRoomType");
                    TextBox txtCheckInDate = (TextBox)item.FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)item.FindControl("txtCheckOutDate");
                    //for (int k = 0; k < dt.Rows.Count; k++)
                    //{
                    //    if (itm == k)
                    //    {
                            binddropdownlist(drpCity, ds);
                            drpCity.Text = dt.Rows[itm]["City"].ToString();
                            //if (drpCity.Text != "")
                            //{
                            //    DataSet dshotel = objmanualitinery.fetchComboDataforHotel("GET_HOTEL_NAME_CITY_WISE", drpCity.Text);
                            //    binddropdownlist(drpHotelName, dshotel);
                                txtHotelName.Text = dt.Rows[itm]["HotelName"].ToString();
                            //}

                            //if (drpHotelName.Text != "")
                            //{
                            //    DataSet ds1 = objmanualitinery.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_FIT_HOTEL_CITY_WISE", drpHotelName.Text, drpCity.Text);

                            //    binddropdownlist(drpRoomType, ds1);

                                txtRoomType.Text = dt.Rows[itm]["RoomType"].ToString();
                            //}
                            txtCheckInDate.Text = dt.Rows[itm]["CheckInDate"].ToString();
                            txtCheckOutDate.Text = dt.Rows[itm]["CheckOutDate"].ToString();
                            lblmain.Text = dt.Rows[itm]["Id"].ToString();
                    //    }
                    //}
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

        protected void RemoveSchedule(GridView gv, int rowIndex, UpdatePanel uppanel)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                DataSet dsCity = objmanualitinery.CommonSp("GET_CITY_NAME_FOR_MANUAL_ITINERY");
                DataSet dsSIC = objmanualitinery.CommonSp("GET_SIC_PVT_FOR_MANUAL_ITINERY");

                int count = gv.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    dt1.Rows.Add();
                }
                foreach (GridViewRow item in gv.Rows)
                {
                    if (rowIndex != item.DataItemIndex)
                    {
                        Label lblSchedule = (Label)item.FindControl("lblSchedule");
                        DropDownList drpCitySchedule = (DropDownList)item.FindControl("drpCitySchedule");
                        DropDownList drpSICPVT = (DropDownList)item.FindControl("drpSICPVT");
                        TextBox txtDate = (TextBox)item.FindControl("txtDate");
                        TextBox txtTSM = (TextBox)item.FindControl("txtTSM");
                        RadTimePicker rtpPick = (RadTimePicker)item.FindControl("rtpPick");
                        TextBox txtsignature = (TextBox)item.FindControl("txtsignature");

                        if (dt.Columns.Count == 0)
                        {
                            dt.Columns.Add("CitySchedule");
                            dt.Columns.Add("Date");
                            dt.Columns.Add("TSM");
                            dt.Columns.Add("PickupTime");
                            dt.Columns.Add("SICPVT");
                            dt.Columns.Add("Signature");
                            dt.Columns.Add("Id");
                        }

                        DataRow dr = dt.NewRow();
                        dr["CitySchedule"] = drpCitySchedule.Text;
                        dr["Date"] = txtDate.Text;
                        dr["TSM"] = txtTSM.Text;
                        dr["PickupTime"] = rtpPick.SelectedDate;
                        dr["SICPVT"] = drpSICPVT.Text;
                        dr["Signature"] = txtsignature.Text;
                        dr["Id"] = lblSchedule.Text;
                        Svalues += "," + lblSchedule.Text;
                        dt.Rows.Add(dr);

                    }
                }
                ViewState["SID"] = Svalues;

                gv.DataSource = dt;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    //if (itm >= rowIndex)
                    //{
                    //    itm = itm + 1;
                    //}
                    Label lblSchedule = (Label)item.FindControl("lblSchedule");
                    DropDownList drpCitySchedule = (DropDownList)item.FindControl("drpCitySchedule");
                    DropDownList drpSICPVT = (DropDownList)item.FindControl("drpSICPVT");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    TextBox txtTSM = (TextBox)item.FindControl("txtTSM");
                    RadTimePicker rtpPick = (RadTimePicker)item.FindControl("rtpPick");
                    TextBox txtsignature = (TextBox)item.FindControl("txtsignature");
                    //for (int k = 0; k < dt.Rows.Count; k++)
                    //{
                    //    if (itm == k)
                    //    {
                            binddropdownlist(drpCitySchedule, dsCity);
                            drpCitySchedule.Text = dt.Rows[itm]["CitySchedule"].ToString();
                            binddropdownlist(drpSICPVT, dsSIC);
                            drpSICPVT.Text = dt.Rows[itm]["SICPVT"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            txtTSM.Text = dt.Rows[itm]["TSM"].ToString();
                            if (dt.Rows[itm]["PickupTime"].ToString() != "")
                            {
                                rtpPick.SelectedDate = DateTime.Parse(dt.Rows[itm]["PickupTime"].ToString());
                            }
                            txtsignature.Text = dt.Rows[itm]["Signature"].ToString();
                            lblSchedule.Text = dt.Rows[itm]["Id"].ToString();
                    //    }
                    //}
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

        protected void RemovePassenger(GridView gv, int rowIndex, UpdatePanel uppanel)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                DataSet dsnationality = objmanualitinery.CommonSp("FETCH_NATIONALITY");

                int count = gv.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    dt1.Rows.Add();
                }
                foreach (GridViewRow item in gv.Rows)
                {
                    if (rowIndex != item.DataItemIndex)
                    {
                        Label lblPassenger = (Label)item.FindControl("lblPassenger");
                        TextBox txtname = (TextBox)item.FindControl("txtName");
                        TextBox txtpasspot = (TextBox)item.FindControl("txtPassportno");
                        DropDownList drpNationality = (DropDownList)item.FindControl("drpNationality");

                        if (dt.Columns.Count == 0)
                        {
                            dt.Columns.Add("Nationality");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("PassportNo");
                            dt.Columns.Add("Id");

                        }

                        DataRow dr = dt.NewRow();
                        dr["Nationality"] = drpNationality.Text;
                        dr["Name"] = txtname.Text;
                        dr["PassportNo"] = txtpasspot.Text;
                        dr["Id"] = lblPassenger.Text;
                        Pvalues += "," + lblPassenger.Text;
                        dt.Rows.Add(dr);
                    }
                }
                ViewState["PID"] = Pvalues;

                gv.DataSource = dt;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    //if (itm >= rowIndex)
                    //{
                    //    itm = itm + 1;
                    //}
                    Label lblPassenger = (Label)item.FindControl("lblPassenger");
                    TextBox txtname = (TextBox)item.FindControl("txtName");
                    TextBox txtpasspot = (TextBox)item.FindControl("txtPassportno");
                    DropDownList drpNationality = (DropDownList)item.FindControl("drpNationality");
                    //for (int k = 0; k < dt.Rows.Count; k++)
                    //{
                    //    if (itm == k)
                    //    {
                            txtname.Text = dt.Rows[itm]["Name"].ToString();
                            txtpasspot.Text = dt.Rows[itm]["PassportNo"].ToString();
                            binddropdownlist(drpNationality, dsnationality);
                            drpNationality.Text = dt.Rows[itm]["Nationality"].ToString();
                            lblPassenger.Text = dt.Rows[itm]["Id"].ToString();
                    //    }
                    //}
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

        #endregion

        #region All Remove Button

        protected void btnHotelRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveHotel(GridHotel, rowID, upHotel1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }

        protected void btnScheduleRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemoveSchedule(grdSchedule, rowID, UpSchedule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        protected void btnPassengerRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                RemovePassenger(gvpass, rowID, upPassenger);
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

        #region DropDown Load

        protected void HotelLoad(GridView gv, int Index)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                        DropDownList drpCity = (DropDownList)item.FindControl("drpCity");

                        DataSet ds = objmanualitinery.fetchComboDataforHotel("GET_HOTEL_NAME_CITY_WISE", drpCity.Text);
                        binddropdownlist(drpHotelName, ds);

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

        protected void roomTypeLoad(GridView gv, int Index)
        {
            try
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (Index == item.DataItemIndex)
                    {
                        DropDownList drpCity = (DropDownList)item.FindControl("drpCity");
                        DropDownList drpHotelName = (DropDownList)item.FindControl("drpHotelName");
                        DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");

                        DataSet ds = objmanualitinery.fetchComboDataforHotelroomtype("FETCH_ROOM_TYPE_FOR_FIT_HOTEL_CITY_WISE", drpHotelName.Text, drpCity.Text);
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

        public void drpCity_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                HotelLoad(GridHotel, repeaterItemIndex);
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

        public void drpHotelName_SelectedIndexChanged(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = sender as DropDownList;
                int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;
                roomTypeLoad(GridHotel, repeaterItemIndex);
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

        protected void drpAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = drpAgent.Text;
            if (drpAgent.Text != "")
            {
                DataSet ds = objmanualitinery.fetchInvoice("FETCH_INVOICE_FOR_MANUAL_ITINERY", int.Parse(drpAgent.SelectedValue.ToString()));
                binddropdownlist(drpInvoice, ds);
            }
        }

        protected void drpInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objmanualitinery.fetchInvoiceDetail("FETCH_INVOICE_DETAILS_FOR_MANUAL_ITINERY", drpInvoice.Text);
            lblClientName.Text = ds.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
            lblNoOfAdult.Text = ds.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString();
            lblNoOfChild.Text = ds.Tables[0].Rows[0]["NO_OF_CHILD"].ToString();
            lblNOofCWB.Text = ds.Tables[0].Rows[0]["NO_OF_CWB"].ToString();
            lblNoOfCNB.Text = ds.Tables[0].Rows[0]["NO_OF_CNB"].ToString();
            lblNoOfInfant.Text = ds.Tables[0].Rows[0]["NO_OF_INFANT"].ToString();
            lblArrival.Text = ds.Tables[0].Rows[0]["PERIOD_STAY_FROM"].ToString();
            lblDeparture.Text = ds.Tables[0].Rows[0]["PERIOD_STAY_TO"].ToString();
            txtArrival_Flight.Focus();
            UpdatePanel_Manual_Itinery.Update();
        }

        #endregion

        #region All ADD BUTTONS
        protected void btnAddHotel_Click(object sender, EventArgs e)
        {
            try
            {
                AddHotels(GridHotel, upHotel1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                AddSchedule(grdSchedule, UpSchedule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void btnName_Click(object sender, EventArgs e)
        {
            try
            {
                AddPassenger(gvpass, upPassenger);
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

        #region Save Button

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSet ds;

            bool main = false;
            bool hotel = false;
            bool schedule = false;
            bool pass = false;
            string cust = "0";
            if (drpAgent.SelectedValue != "")
            {
                cust = drpAgent.SelectedValue.ToString();
            }
            try
            {
                if (ViewState["ID"] == null )
                {
                    
                    ds = objmanualitinery.InsertMainDetails(0, cust, lblArrival.Text, lblDeparture.Text, txtArrival_Flight.Text, rtparrival.SelectedDate.ToString(), txtDeparture_Flight.Text, rtpDeparture.SelectedDate.ToString(), drpInvoice.Text, lblClientName.Text, lblNoOfAdult.Text, lblNoOfChild.Text, lblNOofCWB.Text, lblNoOfCNB.Text, lblNoOfInfant.Text, txtNoroomSingle.Text, txtNoroomDouble.Text, txtNoroomTriple.Text, txtmeeting.Text, txtRemarks.Text);
                    ViewState["ID"] = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    main = true;
                }
                else
                {
                    objmanualitinery.InsertMainDetails(int.Parse(ViewState["ID"].ToString()), cust.ToString(), lblArrival.Text, lblDeparture.Text, txtArrival_Flight.Text, rtparrival.SelectedDate.ToString(), txtDeparture_Flight.Text, rtpDeparture.SelectedDate.ToString(), drpInvoice.Text, lblClientName.Text, lblNoOfAdult.Text, lblNoOfChild.Text, lblNOofCWB.Text, lblNoOfCNB.Text, lblNoOfInfant.Text, txtNoroomSingle.Text, txtNoroomDouble.Text, txtNoroomTriple.Text, txtmeeting.Text, txtRemarks.Text);
                    main = true;
                }

                for (int i = 0; i < GridHotel.Rows.Count; i++)
                {
                    DropDownList drpCity = (DropDownList)GridHotel.Rows[i].FindControl("drpCity");
                    TextBox txtHotelName = (TextBox)GridHotel.Rows[i].FindControl("txtHotelName");
                    TextBox txtRoomType = (TextBox)GridHotel.Rows[i].FindControl("txtRoomType");
                    //DropDownList drpHotelName = (DropDownList)GridHotel.Rows[i].FindControl("drpHotelName");
                    //DropDownList drpRoomType = (DropDownList)GridHotel.Rows[i].FindControl("drpRoomType");
                    TextBox txtCheckInDate = (TextBox)GridHotel.Rows[i].FindControl("txtCheckInDate");
                    TextBox txtCheckOutDate = (TextBox)GridHotel.Rows[i].FindControl("txtCheckOutDate");
                    Label lblmain = (Label)GridHotel.Rows[i].FindControl("lblmain");


                    DataSet dshotel = objmanualitinery.FetchAllDetail("FETCH_HOTEL_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));

                    if (GridHotel.Rows.Count < dshotel.Tables[0].Rows.Count)
                    {
                        string str1 = ViewState["Hotel_ID"].ToString();
                        string[] words = str1.Split(new char[] { ',' });
                        words = words.Where(w => w != words[0]).ToArray();

                        if (ViewState["ID"].ToString() != "")
                        {
                            for (int j = 0; j < dshotel.Tables[0].Rows.Count; j++)
                            {

                                if (words.Contains(dshotel.Tables[0].Rows[j]["HOTEL_ITINERY_ID"].ToString()))
                                {

                                }
                                else
                                {
                                    objmanualitinery.DeleteHotelItinery("DELETE_HOTEL_MANUAL_ITINERY", int.Parse(dshotel.Tables[0].Rows[j]["HOTEL_ITINERY_ID"].ToString()));
                                }
                            }
                        }
                    }
                    if (drpCity.Text != "" && txtHotelName.Text != "" && txtRoomType.Text != "")
                    {
                        objmanualitinery.InsertHotelDetails(int.Parse(lblmain.Text), int.Parse(ViewState["ID"].ToString()), drpCity.Text, txtHotelName.Text, txtRoomType.Text, txtCheckInDate.Text, txtCheckOutDate.Text);
                        hotel = true;
                    }
                    else {
                        hotel = true;
                    }
                }
                for (int i = 0; i < grdSchedule.Rows.Count; i++)
                {
                    Label lblSchedule = (Label)grdSchedule.Rows[i].FindControl("lblSchedule");
                    DropDownList drpCitySchedule = (DropDownList)grdSchedule.Rows[i].FindControl("drpCitySchedule");
                    DropDownList drpSICPVT = (DropDownList)grdSchedule.Rows[i].FindControl("drpSICPVT");
                    TextBox txtDate = (TextBox)grdSchedule.Rows[i].FindControl("txtDate");
                    TextBox txtTSM = (TextBox)grdSchedule.Rows[i].FindControl("txtTSM");
                    RadTimePicker rtpPick = (RadTimePicker)grdSchedule.Rows[i].FindControl("rtpPick");
                    TextBox txtsignature = (TextBox)grdSchedule.Rows[i].FindControl("txtsignature");


                    DataSet dsschedule = objmanualitinery.FetchAllDetail("FETCH_SCHEDULE_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));
                    if (grdSchedule.Rows.Count < dsschedule.Tables[0].Rows.Count)
                    {
                        string str1 = ViewState["SID"].ToString();
                        string[] words = str1.Split(new char[] { ',' });
                        words = words.Where(w => w != words[0]).ToArray();

                        if (ViewState["ID"].ToString() != "")
                        {
                            for (int j = 0; j < dsschedule.Tables[0].Rows.Count; j++)
                            {

                                if (words.Contains(dsschedule.Tables[0].Rows[j]["SCHEDULE_ITINERY_ID"].ToString()))
                                {

                                }
                                else
                                {
                                    objmanualitinery.DeleteScheduleItinery("DELETE_SCHEDULE_MANUAL_ITINERY", int.Parse(dsschedule.Tables[0].Rows[j]["SCHEDULE_ITINERY_ID"].ToString()));
                                }
                            }
                        }
                    }

                    if (drpCitySchedule.Text != "" && txtTSM.Text != "")
                    {
                        objmanualitinery.InsertScheduleDetails(int.Parse(lblSchedule.Text), int.Parse(ViewState["ID"].ToString()), drpCitySchedule.Text, txtTSM.Text, drpSICPVT.Text, txtsignature.Text, txtDate.Text, rtpPick.SelectedDate.ToString());
                        schedule = true;
                    }
                    else
                    {
                        schedule = true;
                    }
                }

                for (int i = 0; i < gvpass.Rows.Count; i++)
                {
                    Label lblPassenger = (Label)gvpass.Rows[i].FindControl("lblPassenger");
                    TextBox txtname = (TextBox)gvpass.Rows[i].FindControl("txtName");
                    TextBox txtpasspot = (TextBox)gvpass.Rows[i].FindControl("txtPassportno");
                    DropDownList drpNationality = (DropDownList)gvpass.Rows[i].FindControl("drpNationality");

                    DataSet dspassenger = objmanualitinery.FetchAllDetail("FETCH_TRAVELLING_PASSENGERS_FOR_MANUAL_ITINERY_EDIT", int.Parse(ViewState["ID"].ToString()));
                    if (gvpass.Rows.Count < dspassenger.Tables[0].Rows.Count)
                    {
                        string str1 = ViewState["PID"].ToString();
                        string[] words = str1.Split(new char[] { ',' });
                        words = words.Where(w => w != words[0]).ToArray();

                        if (ViewState["ID"].ToString() != "")
                        {
                            for (int j = 0; j < dspassenger.Tables[0].Rows.Count; j++)
                            {

                                if (words.Contains(dspassenger.Tables[0].Rows[j]["TRAVELLING_PASSENGERS_ID"].ToString()))
                                {

                                }
                                else
                                {
                                    objmanualitinery.DeletePasslItinery("DELETE_TRAVELLING_PASSENGERS_FOR_MANUAL_ITINERY", int.Parse(dspassenger.Tables[0].Rows[j]["TRAVELLING_PASSENGERS_ID"].ToString()));
                                }
                            }
                        }
                    }

                    if (txtname.Text != "" && txtpasspot.Text != "")
                    {
                        objmanualitinery.InsertPassengerDetails(int.Parse(lblPassenger.Text), int.Parse(ViewState["ID"].ToString()), txtname.Text, txtpasspot.Text, drpNationality.Text);
                        pass = true;
                    }
                    else 
                    {
                        pass = true;
                    }

                }
                if (hotel == true && pass == true && main == true && schedule == true)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Record Save Successfully');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully..')", true);

                    report(int.Parse(ViewState["ID"].ToString()));

                    Response.Redirect("~/Views/FIT/ManualItinerySearch.aspx");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                upHotel1.Update();
                UpdatePanel_Manual_Itinery.Update();
                upPassenger.Update();
                UpSchedule.Update();
            }

        }


        protected void report(int manual_itinery_id1)
        {
            Response.Clear();
            if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/ManualItinery/" + manual_itinery_id1.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/ManualItinery/" + manual_itinery_id1.ToString() + "/"));

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
            "  <MarginTop>0.25in</MarginTop>" +
            "  <MarginLeft>0.10in</MarginLeft>" +
            "  <MarginRight>0.10in</MarginRight>" +
            "  <MarginBottom>0.50in</MarginBottom>" +
            "</DeviceInfo>";


            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("MANUAL_ITINERY_ID", manual_itinery_id1.ToString());
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/ManualItinery";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();


            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            rptViewer1.Visible = false;

            using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/ManualItinery/" + manual_itinery_id1 + "/ManualItinery.pdf")))
            {
                fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
            }
        }

        #endregion

    }
}