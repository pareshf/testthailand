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

namespace CRM.WebApp.Views.GIT
{
    public partial class FlightDetails : System.Web.UI.Page
    {
        FlightDetailsGIT objFlightDetails = new FlightDetailsGIT();

        #region VARIABLES
        string tourId;
        int FlightId;
        bool flightFlag = true;
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
                }
            }
        }

        #region FILL DETAILS FIRST TIME

        protected void fillDeatils()
        {
            AddArrival(GridArrival, upFlight);
            AddDeparture(GridDeparture, upFlight);
        }

        #endregion

        #region FILL DETAILS WHILE EDIT MODE

        protected void fillDetailsEditMode()
        {
            fillArrivalFlightDetailsEditMode(GridArrival, upFlight);
            fillDepartureFlightDetailsEditMode(GridDeparture, upFlight);
        }

        #endregion

        #region ARRIVAL

        protected void AddArrival(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();

            foreach (GridViewRow item in gv.Rows)
            {

                TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Flightname");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["Flightname"] = txtFlightname.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["NoOfPassenger"] = txtNoOfPassenger.Text;
                dr["Date"] = txtdate.Text;
                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Flightname");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["Flightname"] = "";
                dr["Time"] = "";
                dr["NoOfPassenger"] = "";
                dr["Date"] = "";

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

                TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");
 
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        txtFlightname.Text = dt.Rows[itm]["Flightname"].ToString();

                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                        txtNoOfPassenger.Text = dt.Rows[itm]["NoOfPassenger"].ToString();
                        txtdate.Text = dt.Rows[itm]["Date"].ToString();
                    }
                }
            }
            uppanel.Update();
        }

        protected void fillArrivalFlightDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            DataSet ds = objFlightDetails.GetFlightDetails(int.Parse(tourId), "A");
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (j == item.DataItemIndex)
                    {                       
                        TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                        TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");


                        txtFlightname.Text = ds.Tables[0].Rows[j]["FLIGHT_NAME"].ToString();
                        if (ds.Tables[0].Rows[j]["FLIGHT_TIME"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[j]["FLIGHT_TIME"].ToString());
                        }
                        txtNoOfPassenger.Text = ds.Tables[0].Rows[j]["NO_OF_PASSENGER"].ToString();
                        txtdate.Text = ds.Tables[0].Rows[j]["FLIGHT_DATE"].ToString();
                    }

                }
                if (j < ds.Tables[0].Rows.Count - 1)
                {
                    AddArrival(gv,uppanel);
                }
            }

        }

        #region ADD BUTTONS
        protected void btnArrivalAdd_Click(object sender, EventArgs e)
        {
            AddArrival(GridArrival, upFlight);
        }

        #endregion

        #region REMOVE BUTTONS
        protected void btnArrRemove_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveArrival(GridArrival, upFlight, rowID);

        }
        protected void RemoveArrival(GridView gv, UpdatePanel uppanel, int rowIndex)
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

                TextBox txtArrivalFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("FlightName");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["FlightName"] = txtArrivalFlightName.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["NoOfPassenger"] = txtNoOfPassenger.Text;
                dr["Date"] = txtdate.Text;
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

                TextBox txtArrivalFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        txtArrivalFlightName.Text = dt.Rows[itm]["FlightName"].ToString();
                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                        txtNoOfPassenger.Text = dt.Rows[itm]["NoOfPassenger"].ToString();
                        txtdate.Text = dt.Rows[itm]["Date"].ToString();
                    }
                }
            }
            uppanel.Update();
        }
        #endregion

        #endregion

        #region DEPARTURE

        protected void AddDeparture(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();

            foreach (GridViewRow item in gv.Rows)
            {

                TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Flightname");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["Flightname"] = txtFlightname.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["NoOfPassenger"] = txtNoOfPassenger.Text;
                dr["Date"] = txtdate.Text;
                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Flightname");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["Flightname"] = "";
                dr["Time"] = "";
                dr["NoOfPassenger"] ="";
                dr["Date"] = "";
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

                TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        txtFlightname.Text = dt.Rows[itm]["Flightname"].ToString();

                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                        txtNoOfPassenger.Text = dt.Rows[itm]["NoOfPassenger"].ToString();
                        txtdate.Text = dt.Rows[itm]["Date"].ToString();
                    }
                }
            }
            uppanel.Update();
        }

        protected void fillDepartureFlightDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            DataSet ds = objFlightDetails.GetFlightDetails(int.Parse(tourId), "D");
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (j == item.DataItemIndex)
                    {

                        TextBox txtFlightname = (TextBox)item.FindControl("txtFlightName");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                        TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");


                        txtFlightname.Text = ds.Tables[0].Rows[j]["FLIGHT_NAME"].ToString();
                        if (ds.Tables[0].Rows[j]["FLIGHT_TIME"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[j]["FLIGHT_TIME"].ToString());
                        }
                        txtNoOfPassenger.Text = ds.Tables[0].Rows[j]["NO_OF_PASSENGER"].ToString();
                        txtdate.Text = ds.Tables[0].Rows[j]["FLIGHT_DATE"].ToString();
                    }

                }
                if (j < ds.Tables[0].Rows.Count - 1)
                {
                    AddDeparture(gv, uppanel);
                }
            }

        }

        #region ADD BUTTONS
        protected void btnDepartureAdd_Click(object sender, EventArgs e)
        {
            AddDeparture(GridDeparture, upFlight);
        }

        #endregion

        #region REMOVE BUTTONS
        protected void btndepRemove_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int rowID = Convert.ToInt16(row.RowIndex);
            RemoveDeparture(GridDeparture, upFlight, rowID);

        }
        protected void RemoveDeparture(GridView gv, UpdatePanel uppanel, int rowIndex)
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

                TextBox txtDepartureFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");


                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("FlightName");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("NoOfPassenger");
                    dt.Columns.Add("Date");
                }

                DataRow dr = dt.NewRow();
                dr["FlightName"] = txtDepartureFlightName.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["NoOfPassenger"] = txtNoOfPassenger.Text;
                dr["Date"] = txtdate.Text;
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

                TextBox txtDepartureFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");
                
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        txtDepartureFlightName.Text = dt.Rows[itm]["FlightName"].ToString();
                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                        txtNoOfPassenger.Text = dt.Rows[itm]["NoOfPassenger"].ToString();
                        txtdate.Text = dt.Rows[itm]["Date"].ToString();
                    }
                }
            }
            uppanel.Update();
        }
        #endregion

        #endregion

        #region Save

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objFlightDetails.DeleteFlightDetails(int.Parse(Request.QueryString["TOURID"].ToString()));
            insertArrivalFlight(GridArrival);
            insertDepartureFlight(GridDeparture);
            if (flightFlag == true)
            {
               // ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Flight Details save successfully.')", true);
                Response.Redirect ("~/Views/GIT/RoomListGIT.aspx?TOURID=" + Request.QueryString["TOURID"].ToString());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Flight Name.')", true);
            }
        }

        protected void insertArrivalFlight(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                TextBox txtArrivalFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                if (txtArrivalFlightName.Text != "")
                {
                    objFlightDetails.InsertArrivalFlightDetails(FlightId, txtArrivalFlightName.Text, txttime.SelectedDate.ToString(), int.Parse(Request.QueryString["TOURID"].ToString()), "A", int.Parse(txtNoOfPassenger.Text),txtdate.Text);
                    
                }
                else
                {
                    flightFlag = false;
                    
                }
            }

        }

        protected void insertDepartureFlight(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                TextBox txtDepartureFlightName = (TextBox)item.FindControl("txtFlightName");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                TextBox txtNoOfPassenger = (TextBox)item.FindControl("txtNoOfPassenger");
                TextBox txtdate = (TextBox)item.FindControl("txtFlightDate");

                if (txtDepartureFlightName.Text != "")
                {
                    objFlightDetails.InsertDepartureFlightDetails(FlightId, txtDepartureFlightName.Text, txttime.SelectedDate.ToString(), int.Parse(Request.QueryString["TOURID"].ToString()), "D", int.Parse(txtNoOfPassenger.Text),txtdate.Text);
                    
                }
                else
                {
                    flightFlag = false;
                    
                }
            }

        }

        #endregion
        
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/GitDetails.aspx?TOURID=" + int.Parse(Request.QueryString["TOURID"].ToString()));
        }

        
    }
}