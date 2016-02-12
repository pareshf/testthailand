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
//using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.WebApp.Views.FIT
{
    public partial class OrderSummary : System.Web.UI.Page
    {
        #region VARIABLES
        string id;
        int cartid;
        DataTable dthotel;
        DataTable dtcruise;
        DataTable dtsight;
        DataTable dttransfer;
        public int ipr = 0;
        string citynames;
        #endregion

        OrderSummaryStoreProcedure objOrderSummaryStoreProcedure = new OrderSummaryStoreProcedure();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                id = Session["usersid"].ToString();

                DataSet ds = objOrderSummaryStoreProcedure.fetchComboData("FETCH_ORDER_STATUS", "");
                bindComboBox(RadCmbClass, ds);

               
                fetchHotelrecords();
                fetchCruiserecords();
                fetchSightrecords();
                fetchTransferrecords();

                Get_No_Of_Rooms();
                //DataSet  ds1 = objOrderSummaryStoreProcedure.fetchno_of_rooms(int.Parse(id));
                //txtNoroomSingle.Text = ds1.Tables[0].Rows[0]["singlerooms"].ToString();
                //txtNoroomDouble.Text = ds1.Tables[1].Rows[0]["doublerooms"].ToString();

                DataSet ds2 = objOrderSummaryStoreProcedure.fetch_dates(int.Parse(id));
                txtFrom_Date.Text = ds2.Tables[0].Rows[0]["MIN_DATE"].ToString();
                txtTo_Date.Text = ds2.Tables[1].Rows[0]["MAX_DATE"].ToString();

                DateTime dt1 = DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null);

                DateTime dt2 = DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null);

                TimeSpan x = dt2.Subtract(dt1);

                int No_Of_Days = x.Days;

                txtNo_OfNights.Text = No_Of_Days.ToString();

                DataTable city = objOrderSummaryStoreProcedure.fetch_city_names(int.Parse(id));
                for (int i = 0; i < city.Rows.Count; i++)
                {
                    citynames += city.Rows[i]["CITY_NAME"];
                }

                    txtTourname.Text =citynames + " " + "Package" + " " + txtFrom_Date.Text + " " + txtTo_Date.Text + " " + txtNo_OfNights.Text + "nights";
            }
            
        }

        #region FETCH DATA IN ORDER STATUS COMBO BOX
        protected void bindComboBox(RadComboBox r, DataSet d)
        {
            r.Items.Clear();
            r.DataTextField = "AutoSearchResult";
            r.DataSource = d;
            r.DataBind();
            r.Items.Insert(0, new RadComboBoxItem("", "0"));
            r.SelectedValue = "0";
        }
      

        protected void RadCmbclass_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataSet ds = objOrderSummaryStoreProcedure.fetchComboData("FETCH_ORDER_STATUS", e.Text);
            DataTable dt = ds.Tables[0];
            onitemrequest(RadCmbClass, dt, e.NumberOfItems);
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

        #region UPDATE AND DELETE ITEM COMMAND EVENT OF DATALIST
        protected void dlhoteldetails_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string sortByAndArrangeBy = (e.CommandArgument).ToString();
            char[] separator = { '|' };
            string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);

            if (e.CommandName == "UpdateHD_Record")
            {
               
                cartid=int.Parse(sortByAndArrangeByArray[1].ToString());
                updatehotel_details(cartid);
                Get_No_Of_Rooms();
          //      Update_All_Panels();
           //    UpdatePanel_TourDetails.Update();
          //      Master.DisplayMessage("Record Updated Successfully.", "successMessage",1000);
         
            }
            else if (e.CommandName == "DeleteHD_Record")
            {

                objOrderSummaryStoreProcedure.deleteHotelDetails(int.Parse(sortByAndArrangeByArray[0].ToString()), Convert.ToInt32(sortByAndArrangeByArray[1].ToString()));

                fetchHotelrecords();
                Get_No_Of_Rooms();
          //      UpdatePanel_TourDetails.Update();
                Master.DisplayMessage("Record Deleted Successfully.", "successMessage", 1000);
            }
        }
        protected void dlcruisedetails_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string sortByAndArrangeBy = (e.CommandArgument).ToString();
            char[] separator = { '|' };
            string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);

            if (e.CommandName == "UpdateCD_Record")
            {
                cartid = int.Parse(sortByAndArrangeByArray[1].ToString());
                updatecruise_details(cartid);
          //      Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
             
            }
            else if (e.CommandName == "DeleteCD_Record")
            {
     
                objOrderSummaryStoreProcedure.deleteCruiseDetails(int.Parse(sortByAndArrangeByArray[0].ToString()), Convert.ToInt32(sortByAndArrangeByArray[1].ToString()));
                fetchCruiserecords();
               
                Master.DisplayMessage("Record Deleted Successfully.", "successMessage", 1000);
            
            }
        }

        protected void dlsightseeing_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string sortByAndArrangeBy = (e.CommandArgument).ToString();
            char[] separator = { '|' };
            string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);

             if (e.CommandName == "UpdateSD_Record")
            {
                cartid = int.Parse(sortByAndArrangeByArray[1].ToString());
                updatesight_details(cartid);
         //       Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
            
            }
             else if (e.CommandName == "DeleteSD_Record")
             {
      
                 objOrderSummaryStoreProcedure.deleteSightDetails(int.Parse(sortByAndArrangeByArray[0].ToString()), Convert.ToInt32(sortByAndArrangeByArray[1].ToString()));
                 fetchSightrecords();
                 Master.DisplayMessage("Record Deleted Successfully.", "successMessage", 1000);   
             }
             else
             {
             }
        }

        protected void dltransferpackage_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string sortByAndArrangeBy = (e.CommandArgument).ToString();
            char[] separator = { '|' };
            string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);

            if (e.CommandName == "UpdateTP_Record")
            {
                cartid = int.Parse(sortByAndArrangeByArray[1].ToString());
                updatetransfer_details(cartid);
        //        Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
         
            }
            else if (e.CommandName == "DeleteTP_Record")
            {
           
                objOrderSummaryStoreProcedure.deleteTransferDetails(int.Parse(sortByAndArrangeByArray[0].ToString()), Convert.ToInt32(sortByAndArrangeByArray[1].ToString()));
                fetchTransferrecords();
                Master.DisplayMessage("Record Deleted Successfully.", "successMessage", 1000);
            }
            else
            {

            }
        }
        #endregion

        #region GET ALL RECORDS AND FILL DATALIST

        protected void fetchHotelrecords()
        {
             dthotel = objOrderSummaryStoreProcedure.fetchHotelDetails(int.Parse(id));
            dlhoteldetails.DataSource = dthotel;
            dlhoteldetails.DataBind();
            if (dthotel.Rows[0]["TOUR_ID"] != DBNull.Value)
            {
                ViewState["tourid"] = Convert.ToInt32(dthotel.Rows[0]["TOUR_ID"].ToString());
                RadCmbClass.Enabled = false;

                RadCmbClass.Text = "Quote";

            }
            else
            {
                ViewState["tourid"] = "0";
                RadCmbClass.Enabled = false;

                RadCmbClass.Text = "In Cart";
            }
            //if (tourid == null || tourid.ToString() == "" || tourid.ToString() == " ")
            //{
            //    tourid = 0;
            //}
            if (dthotel.Rows.Count == 0)
            {
             //   UpdatePanel4.Visible = false;
                hotelheader.Visible = false;
                lblHotelDetail.Visible = false;
                
            }
        }

        protected void fetchCruiserecords()
        {
             dtcruise = objOrderSummaryStoreProcedure.fetchCruiseDetails(int.Parse(id));
            dlcruisedetails.DataSource = dtcruise;
            dlcruisedetails.DataBind();
           
            if (dtcruise.Rows.Count == 0)
            {
              
                cruiseheader.Visible = false;
                Label24.Visible = false;
                Label25.Attributes.Add("style", "display:none"); 
            }
        }

        protected void fetchSightrecords()
        {
             dtsight = objOrderSummaryStoreProcedure.fetchSightseeDetails(int.Parse(id));
            dlsightseeing.DataSource = dtsight;
            dlsightseeing.DataBind();

            if (dtsight.Rows.Count == 0)
            {
               sightheader.Visible = false;
                Label33.Visible = false;
                UpdatePanel_Sight_Data.Update();
            }
        }

        protected void fetchTransferrecords()
        {
             dttransfer = objOrderSummaryStoreProcedure.fetchTransferPackageDetails(int.Parse(id));
            dltransferpackage.DataSource = dttransfer;
            dltransferpackage.DataBind();

            if (dttransfer.Rows.Count == 0)
            {
                transferheader.Visible = false;
                Label42.Visible = false;
            }
        }

        #endregion

        #region ALL UPADATE METHODS

        protected void updatehotel_details(int hotelorderid)
        {
            foreach (DataListItem item in dlhoteldetails.Items)
            {
                Label hotelid = (Label)item.FindControl("lblHotelcart_id");
                if (Convert.ToInt32(hotelid.Text) == hotelorderid)
                {
                    TextBox NoOfRooms = (TextBox)item.FindControl("txtHD_qty");
                    TextBox txtFromDate = (TextBox)item.FindControl("txtHD_fromdate");
                    TextBox txtTodate = (TextBox)item.FindControl("txtHD_todate");

                    if(txtFromDate.Text=="")
                    {
                        Master.DisplayMessage("From Date Is Required.", "successMessage", 3000);
                    }
                    else if (txtTodate.Text == "")
                    {
                        Master.DisplayMessage("To Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("From Date Must Between From Date to To Date.", "successMessage", 3000); 
                    }
                    else if ((DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("To Date Must Between From Date to To Date.", "successMessage", 3000);
                    }
                     else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null))))
                     {
                         Master.DisplayMessage("To Date Must Be After From Date.", "successMessage", 3000);
                     }
                    else
                    {
                        objOrderSummaryStoreProcedure.updateHotelDetails(hotelorderid, txtFromDate.Text, txtTodate.Text, int.Parse(NoOfRooms.Text));
                        Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
                     
                    }
                    break;
                }
            }
        }

        protected void updatecruise_details(int cruiseorderid)
        {
            foreach (DataListItem item in dlcruisedetails.Items)
            {
                Label cruiseid = (Label)item.FindControl("lblCruisecart_id");
                if (Convert.ToInt32(cruiseid.Text) == cruiseorderid)
                {
                    TextBox NoOfRooms = (TextBox)item.FindControl("txtCD_qty");
                    TextBox txtFromDate = (TextBox)item.FindControl("txtCD_fromdate");
                    TextBox txtTodate = (TextBox)item.FindControl("txtCD_todate");

                    if(txtFromDate.Text=="")
                    {
                        Master.DisplayMessage("From Date Is Required.", "successMessage", 3000);
                    }
                    else if (txtTodate.Text == "")
                    {
                        Master.DisplayMessage("To Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("From Date Must Between From Date to To Date.", "successMessage", 3000); 
                    }
                    else if ((DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("To Date Must Between From Date to To Date.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null))))
                    {
                        Master.DisplayMessage("To Date Must Be After From Date.", "successMessage", 3000);
                    }
                    else
                    {
                        objOrderSummaryStoreProcedure.updateCruiseDetails(cruiseorderid, txtFromDate.Text, txtTodate.Text, int.Parse(NoOfRooms.Text));
                        Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
                    }
                    break;
                }
            }
        }

        protected void updatesight_details(int sightorderid)
        {
            foreach (DataListItem item in dlsightseeing.Items)
            {
                Label sightid = (Label)item.FindControl("lblSightcart_id");
                if (Convert.ToInt32(sightid.Text) == sightorderid)
                {
                    TextBox txtFromDate = (TextBox)item.FindControl("txtSD_date");
                    TextBox txtTodate = (TextBox)item.FindControl("txtSD_time");

                    if (txtFromDate.Text == "")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 3000);
                    }
                    else
                    {
                        objOrderSummaryStoreProcedure.updateSightDetails(sightorderid, txtFromDate.Text, txtTodate.Text);
                        Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
                    }
                    break;
                }
            }
        }

        protected void updatetransfer_details(int transferorderid)
        {
            foreach (DataListItem item in dltransferpackage.Items)
            {
                Label transferid = (Label)item.FindControl("lblTransfercart_id");
                if (Convert.ToInt32(transferid.Text) == transferorderid)
                {
                    TextBox txtFromDate = (TextBox)item.FindControl("txtTP_date");
                    TextBox txtTodate = (TextBox)item.FindControl("txtTP_time");

                     if (txtFromDate.Text=="")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 3000);
                    }
                     else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFrom_Date.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtTo_Date.Text, "dd/MM/yyyy", null)))
                     {
                         Master.DisplayMessage("Date Must Between From Date to To Date.", "successMessage", 3000);
                     }
                     else
                     {

                         objOrderSummaryStoreProcedure.updateTransferDetails(transferorderid, txtFromDate.Text, txtTodate.Text);
                         Master.DisplayMessage("Record Updated Successfully.", "successMessage", 1000);
                     }
                    break;
                }
            }
        }

        #endregion

        #region INSERT UPADTE QUOTES
        protected void btnGet_Quote_Click(object sender, EventArgs e)
        {
            dthotel = objOrderSummaryStoreProcedure.fetchHotelDetails(int.Parse(id));
            dtcruise = objOrderSummaryStoreProcedure.fetchCruiseDetails(int.Parse(id));
            dtsight = objOrderSummaryStoreProcedure.fetchSightseeDetails(int.Parse(id));
            dttransfer = objOrderSummaryStoreProcedure.fetchTransferPackageDetails(int.Parse(id));
            if (dthotel.Rows.Count != 0)
            {
                if (dtsight.Rows.Count == 0 && dttransfer.Rows.Count == 0)
                {
                    Master.DisplayMessage("First Select Either Sight Seeing Or Transfer Package", "successMessage", 3000);
                }

                else
                {
                    //DateTime date1 = DateTime.Parse("10/01/2004 10:00PM");
                    //DateTime date2 = DateTime.Parse("10/01/2004 09:50PM");
                    //TimeSpan ts = date1.Subtract(date2);

                    //Console.WriteLine(ts.Days.ToString());
                    if (txtNo_Adult.Text == "")
                    {
                        txtNo_Adult.Text = "0";
                    }

                    if (txtNo_CWB.Text == "")
                    {
                        txtNo_CWB.Text = "0";
                    }

                    if (txtNo_CNB.Text == "")
                    {
                        txtNo_CNB.Text = "0";
                    }

                    if (txtNo_Infant.Text == "")
                    {
                        txtNo_Infant.Text = "0";
                    }




                    int i = validatePersonAndRooms();
                    if (i == 1)
                    {
                        objOrderSummaryStoreProcedure.insertupadtegetquotes(ViewState["tourid"].ToString(), txtTourname.Text, txtFrom_Date.Text, txtTo_Date.Text, "3", txtNo_OfNights.Text, id, txtNo_Adult.Text, txtNo_CWB.Text, txtNo_CNB.Text, txtNo_Infant.Text, txtFrom_Time.Text, txtTo_Time.Text, txtArrival_Flight.Text, txtDeparture_Flight.Text, txtClientname.Text, txtRemarks.Text);

                        DataTable dt;

                        dt = objOrderSummaryStoreProcedure.generate_quote_for_hotel(int.Parse(txtNo_Adult.Text), int.Parse(txtNo_CWB.Text), int.Parse(txtNo_CNB.Text), int.Parse(txtNo_Infant.Text), int.Parse(id), RadCmbClass.Text);
                        int quote_id;
                        quote_id = Convert.ToInt32(dt.Rows[0]["QUOTE_ID"]);
                        Response.Redirect("~/Views/FIT/QuoteReport.aspx?QuoteId=" + quote_id);

                        Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);

                    }
                }
            }
        }
        #endregion

        #region CONTINUE PURCHASE BUTTON
        protected void btnContinue_Purchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/FIT/FitBooking.aspx");
        }
        #endregion

        #region GET NO OF ROOMS
        protected void Get_No_Of_Rooms()
        {
            DataSet ds1 = objOrderSummaryStoreProcedure.fetchno_of_rooms(int.Parse(id));
            txtNoroomSingle.Text = ds1.Tables[0].Rows[0]["singlerooms"].ToString();
            txtNoroomDouble.Text = ds1.Tables[1].Rows[0]["doublerooms"].ToString();
        }
        #endregion

        //protected void Update_All_Panels()
        //{
        //    UpdatePanel_Hotel_header.Update();
        //  //  getordersummary.Update();
        //    UpdatePanel_Cruise_Header.Update();
        //    UpdatePanel_TourDetails.Update();
        ////  UpdatePanel_TourDetails_1.Update();
        //}

       //protected void UpdatePanel_TourDetails_Load(object sender, EventArgs e)
       // {
       //     Get_No_Of_Rooms();
       //     //DataSet ds1 = objOrderSummaryStoreProcedure.fetchno_of_rooms(int.Parse(id));
       //     //txtNoroomSingle.Text = ds1.Tables[0].Rows[0]["singlerooms"].ToString();
       //     //txtNoroomDouble.Text = ds1.Tables[1].Rows[0]["doublerooms"].ToString();
       // }

        #region Validation Of Rooms & Person

        protected int validatePersonAndRooms()
        {
            int no_of_adult;
            int no_of_cwb;
            int flag = 1;

            if (txtNo_Adult.Text == "")
            {
                no_of_adult = 0;
            }
            else
            {
                no_of_adult = Convert.ToInt32(txtNo_Adult.Text);
            }
            if (txtNo_CWB.Text == "")
            {
                no_of_cwb = 0;
            }
            else
            {
                no_of_cwb = Convert.ToInt32(txtNo_CWB.Text);
            }

            int no_of_person = no_of_adult + no_of_cwb;

            foreach (DataListItem item in dlhoteldetails.Items)
            {
                Label lbl = (Label)item.FindControl("lblhotelpricelistid");
                Label lbl_hotel_name = (Label)item.FindControl("lblHD_productdesc");
                int supplier_hotel_id = Convert.ToInt32(lbl.Text);

                DataTable dt = objOrderSummaryStoreProcedure.fetch_no_of_rooms_for_persons(Convert.ToInt32(Session["usersid"].ToString()), supplier_hotel_id);
                int no_of_room = Convert.ToInt32(dt.Rows[0]["PERSON"].ToString());

                if (no_of_person > no_of_room)
                {
                    String msg = "Increase No. Of Rooms In " + lbl_hotel_name.Text;
                    Master.DisplayMessage(msg.ToString(), "successMessage", 3000);
                    flag = 0;
                    break;
                }
            }

            return flag;
        }

        #endregion
    }
}