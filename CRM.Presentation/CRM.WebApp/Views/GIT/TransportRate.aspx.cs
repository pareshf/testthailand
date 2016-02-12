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
    public partial class TransportRate : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        GitDetail objGitDetail = new GitDetail();
        EditUpdateGITInformation objEditUpdateGITInformation = new EditUpdateGITInformation();
        InsertGitDetails objInsertGitDetails = new InsertGitDetails(); 

        #region VARIABLES
        int TourId;
        int GIT_TRANSFER_PACKGE_ID; 
#endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TourId = int.Parse(Request.QueryString["TOURID"].ToString());
                GIT_TRANSFER_PACKGE_ID = int.Parse(Session["TransferPackgeId"].ToString());

                DataSet dsTrasport = objGitDetail.fetchTransportPackage("FETCH_TRANSPORT_PACKAGE_NAME", int.Parse(Session["packgeId"].ToString()));

                gridCoachRate.DataSource = dsTrasport;
                gridBoat.DataSource = dsTrasport;
                gridGuide.DataSource = dsTrasport;

                gridCoachRate.DataBind();
                gridBoat.DataBind();
                gridGuide.DataBind();

                fillCoachSupplier(gridCoachRate, 0);

                fillBoatSupplier(gridBoat, 0);

                fillGuideSupplier(gridGuide);

                DataSet dsCoach = objEditUpdateGITInformation.GetCoach(TourId, GIT_TRANSFER_PACKGE_ID);
                if (dsCoach.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < dsCoach.Tables[0].Rows.Count; j++)
                    {
                        foreach (GridViewRow item in gridCoachRate.Rows)
                        {
                            if (j == item.DataItemIndex)
                            {
                                
                                DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                                Label lblPackid = (Label)item.FindControl("lblPackid");
                                drpSupplier.Text = dsCoach.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                                fillCoachSupplier(gridCoachRate, item.DataItemIndex);
                                txtDate.Text = dsCoach.Tables[0].Rows[j]["DATE"].ToString();
                                //lblPackid.Text = dsCoach.Tables[0].Rows[j]["GIT_COACH_CART_ID"].ToString();
                                if (dsCoach.Tables[0].Rows[j]["COACH_TIME"].ToString() != "")
                                {
                                    txttime.SelectedDate = DateTime.Parse(dsCoach.Tables[0].Rows[j]["COACH_TIME"].ToString());
                                }
                            }
                        }
                        if (j < dsCoach.Tables[0].Rows.Count - 1)
                        {
                            AddCoach(gridCoachRate, upCoach);
                        }
                    }
                 
                }

                DataSet dsBoat = objEditUpdateGITInformation.GetBoat(TourId, GIT_TRANSFER_PACKGE_ID);
                if (dsBoat.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < dsBoat.Tables[0].Rows.Count; j++)
                    {
                        foreach (GridViewRow item in gridBoat.Rows)
                        {
                            if (j == item.DataItemIndex)
                            {
                                DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                                drpSupplier.Text = dsBoat.Tables[0].Rows[j]["CHAIN_NAME"].ToString();
                                
                                
                                TextBox txtBoats = (TextBox)item.FindControl("txtBoats");
                                txtBoats.Text = dsBoat.Tables[0].Rows[j]["NO_OF_BOATS"].ToString();

                                TextBox txtdate = (TextBox)item.FindControl("txtDate");
                                txtdate.Text = dsBoat.Tables[0].Rows[j]["DATE"].ToString();

                                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                                if (dsBoat.Tables[0].Rows[j]["TIME"].ToString() != "")
                                {
                                    txttime.SelectedDate = DateTime.Parse(dsBoat.Tables[0].Rows[j]["TIME"].ToString());
                                }
                                Label lblPackid = (Label)item.FindControl("lblPackid");
                               // lblPackid.Text = dsBoat.Tables[0].Rows[j]["GIT_BOAT_CART_ID"].ToString();

                            }
                        }
                        if (j < dsBoat.Tables[0].Rows.Count - 1)
                        {
                            AddBoat(gridBoat, upboat);
                        }
                    }

                }


                DataSet dsGuide = objEditUpdateGITInformation.GetGuide(TourId, GIT_TRANSFER_PACKGE_ID);
                if (dsGuide.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < dsGuide.Tables[0].Rows.Count; j++)
                    {
                        foreach (GridViewRow item in gridGuide.Rows)
                        {
                            if (j == item.DataItemIndex)
                            {
                                DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                                drpSupplier.Text = dsGuide.Tables[0].Rows[j]["CHAIN_NAME"].ToString();

                                TextBox txtGuides = (TextBox)item.FindControl("txtGuides");
                                txtGuides.Text = dsGuide.Tables[0].Rows[j]["NO_OF_GUIDE"].ToString();
                            }
                        }
                        if (j < dsGuide.Tables[0].Rows.Count - 1)
                        {
                            AddGuide(gridGuide, Upguide);
                        }
                    }

                }
            }
            else
            {
                TourId = int.Parse(Request.QueryString["TOURID"].ToString());
                GIT_TRANSFER_PACKGE_ID = int.Parse(Session["TransferPackgeId"].ToString());
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

        #endregion

        #region Back and Update

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/GitDetails.aspx?TOURID=" + TourId);
        }

        //protected void fillCoachSupplier(GridView gv)
        //{
        //    DataSet ds = objGitDetail.fetchTransportPackageCoach("GET_COACH_FOR_GIT", int.Parse(Session["packgeId"].ToString())); 
        //    foreach (GridViewRow item in gv.Rows)
        //    {
                
        //        DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");

        //    }
        //}

        //protected void fillBoatSupplier(GridView gv)
        //{
        //    DataSet ds = objGitDetail.fetchTransportPackageBoat("GET_BOAT_FOR_GIT", int.Parse(Session["packgeId"].ToString()));
        //    foreach (GridViewRow item in gv.Rows)
        //    {

        //        DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");

        //    }
        //}

        //protected void fillGuideSupplier(GridView gv)
        //{
        //    DataSet ds = objGitDetail.fetchTransportPackageGuide("GET_GUIDE_FOR_GIT", int.Parse(Session["packgeId"].ToString()));
        //    foreach (GridViewRow item in gv.Rows)
        //    {

        //        DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");

        //    }
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DataSet dsId = objEditUpdateGITInformation.GetTransferCartId(int.Parse(Request.QueryString["TOURID"].ToString()));

            objInsertGitDetails.insertMiceExp(int.Parse(Request.QueryString["TOURID"].ToString()), txtMisc.Text);

            for (int j = 0; j < dsId.Tables[0].Rows.Count; j++)
            {
              
                int user_id = Convert.ToInt32(Session["AgentId"].ToString());
                DropDownList drpsupplier = new DropDownList();
                TextBox no_of_guide = new TextBox();
                TextBox no_of_boat = new TextBox();
                string supplier;
                string noofnight;
                string noofboat;

                for (int i = 0; i < gridCoachRate.Rows.Count; i++)
                {
                    drpsupplier = (DropDownList)gridCoachRate.Rows[i].FindControl("drpSupplier");
                    supplier = drpsupplier.Text;
                    TextBox txtDate = (TextBox)gridCoachRate.Rows[i].FindControl("txtDate");
                    RadTimePicker txttime = (RadTimePicker)gridCoachRate.Rows[i].FindControl("rdtpTime");
                    Label lblPackid = (Label)gridCoachRate.Rows[i].FindControl("lblPackid");
                    DataSet DS = null;
                    if (drpsupplier.Text != "")
                    {
                       DS = objInsertGitDetails.insertCoach(int.Parse(lblPackid.Text), int.Parse(dsId.Tables[0].Rows[j]["GIT_TRANSFER_CART_ID"].ToString()), supplier, user_id, GIT_TRANSFER_PACKGE_ID,txtDate.Text,txttime.SelectedDate.ToString());
                    }
                    //if(DS.Tables.Count != 0)
                    //{
                    //    lblPackid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                    //}
                }

                for (int i = 0; i < gridGuide.Rows.Count; i++)
                {
                    drpsupplier = (DropDownList)gridGuide.Rows[i].FindControl("drpSupplier");
                    no_of_guide = (TextBox)gridGuide.Rows[i].FindControl("txtGuides");
                    supplier = drpsupplier.Text;
                    noofnight = no_of_guide.Text;
                   
                    if (drpsupplier.Text != "")
                    {
                        objInsertGitDetails.insertGuide(0, int.Parse(dsId.Tables[0].Rows[j]["GIT_TRANSFER_CART_ID"].ToString()), supplier, noofnight, user_id, GIT_TRANSFER_PACKGE_ID);
                    }
                }


                for (int i = 0; i < gridBoat.Rows.Count; i++)
                {
                    drpsupplier = (DropDownList)gridBoat.Rows[i].FindControl("drpSupplier");
                    no_of_boat = (TextBox)gridBoat.Rows[i].FindControl("txtBoats");
                    supplier = drpsupplier.Text;
                    noofboat = no_of_boat.Text;
                    TextBox txtDate = (TextBox)gridBoat.Rows[i].FindControl("txtDate");
                    RadTimePicker txttime = (RadTimePicker)gridBoat.Rows[i].FindControl("rdtpTime");
                    Label lblPackid = (Label)gridBoat.Rows[i].FindControl("lblPackid");
                    DataSet DS1 = null;
                    if (drpsupplier.Text != "")
                    {
                        DS1 = objInsertGitDetails.insertBoat(int.Parse(lblPackid.Text), int.Parse(dsId.Tables[0].Rows[j]["GIT_TRANSFER_CART_ID"].ToString()), supplier, noofboat, user_id, GIT_TRANSFER_PACKGE_ID,txtDate.Text,txttime.SelectedDate.ToString());
                    }
                    //if(DS1.Tables.Count !=0)
                    //{
                    //    lblPackid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                    //}
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);

      
        }

        protected void btnUpdateTime_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridCoachRate.Rows.Count; i++)
            {
                DropDownList drpsupplier = (DropDownList)gridCoachRate.Rows[i].FindControl("drpSupplier");
                
                TextBox txtDate = (TextBox)gridCoachRate.Rows[i].FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)gridCoachRate.Rows[i].FindControl("rdtpTime");
                DataSet DS = null;
                
                if (drpsupplier.Text != "")
                {
                    DataSet dsre = objInsertGitDetails.FetchCartId(TourId);
                    if (dsre.Tables[0].Rows.Count != 0)
                    {
                        for (int j = 0; j < dsre.Tables[0].Rows.Count; j++)
                        {
                            DS = objInsertGitDetails.UpdateCoach(int.Parse(dsre.Tables[0].Rows[j]["GIT_TRANSFER_CART_ID"].ToString()), drpsupplier.Text, GIT_TRANSFER_PACKGE_ID, txtDate.Text, txttime.SelectedDate.ToString());
                        }
                    }
                }
                
            }
            for (int i = 0; i < gridBoat.Rows.Count; i++)
            {
                DropDownList drpsupplier = (DropDownList)gridCoachRate.Rows[i].FindControl("drpSupplier");

                TextBox txtDate = (TextBox)gridCoachRate.Rows[i].FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)gridCoachRate.Rows[i].FindControl("rdtpTime");
                DataSet DS = null;

                if (drpsupplier.Text != "")
                {
                    DataSet dsre = objInsertGitDetails.FetchCartId(TourId);
                    if (dsre.Tables[0].Rows.Count != 0)
                    {
                        for (int j = 0; j < dsre.Tables[0].Rows.Count; j++)
                        {
                            DS = objInsertGitDetails.UpdateBoat(int.Parse(dsre.Tables[0].Rows[j]["GIT_TRANSFER_CART_ID"].ToString()), drpsupplier.Text, GIT_TRANSFER_PACKGE_ID, txtDate.Text, txttime.SelectedDate.ToString());
                        }
                    }
                }

            }
        }

        #endregion

        #region Coach

        protected void btnCoach_Click(object sender, EventArgs e)
        {
            DropDownList drpsupplier = new DropDownList();
            Button btncoachRate = sender as Button;
            int repeaterItemIndex = ((GridViewRow)btncoachRate.NamingContainer).DataItemIndex;
            int Itemindex = repeaterItemIndex;
            foreach (GridViewRow item in gridCoachRate.Rows)
            {
                if (Itemindex == item.DataItemIndex)
                {
                    DropDownList drpSupplierName = (DropDownList)item.FindControl("drpSupplier");
                    Session["SupplierName"] = drpSupplierName.Text;
                    Response.Redirect("~/Views/GIT/CoachRate.aspx?TOURID="+ TourId);
                }
            }

        }

        protected void fillCoachSupplier(GridView gv, int Index)
        {
            DataSet ds = objGitDetail.fetchTransportPackageCoach("GET_COACH_FOR_GIT", int.Parse(Session["TransferPackgeId"].ToString()));
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                if (i == Index)
                {
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    binddropdownlist(drpSupplier, ds);
                }
            }
        }

        protected void AddCoach(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();


            foreach (GridViewRow item in gv.Rows)
            {

                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                Label lblPackid = (Label)item.FindControl("lblPackid");
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofNights");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("Id");
                }

                DataRow dr = dt.NewRow();

                dr["GIT_TRANSFER_PACKGE_NAME"] = txtPackagename.Text;
                dr["NoofNights"] = txtNoofNights.Text;
                dr["Supplier"] = drpsupplier.Text;
                dr["Date"] = txtDate.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["Id"] = lblPackid.Text;
                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofNights");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("Id");
                    
                }


                DataRow dr = dt.NewRow();
                dr["GIT_TRANSFER_PACKGE_NAME"] = "";
                dr["NoofNights"] = "";
                dr["Supplier"] = "";
                dr["Date"] = "";
                dr["Time"] = "";
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
                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                TextBox txtDate = (TextBox)item.FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                Label lblPackid = (Label)item.FindControl("lblPackid");
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        fillCoachSupplier(gridCoachRate, itm);

                        txtPackagename.Text = dt.Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                        txtNoofNights.Text = dt.Rows[itm]["NoofNights"].ToString();
                        drpsupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                        txtDate.Text = dt.Rows[itm]["Date"].ToString();
                        lblPackid.Text = dt.Rows[itm]["Id"].ToString();
                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                    }
                }
            }
            uppanel.Update();

        }

        protected void btnAddCoach_Click(object sender, EventArgs e)
        {
            AddCoach(gridCoachRate, upCoach);
        }

        protected void CoachremoveRow(GridView gv, int rowIndex,UpdatePanel uppanel)
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
                    if (rowIndex != item.DataItemIndex)
                    {
                        Label txtPackagename = (Label)item.FindControl("lblPackName");
                        TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                        DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                        TextBox txtDate = (TextBox)item.FindControl("txtDate");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        Label lblPackid = (Label)item.FindControl("lblPackid");
                        if (dt.Columns.Count == 0)
                        {
                            dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                            dt.Columns.Add("NoofNights");
                            dt.Columns.Add("Supplier");
                            dt.Columns.Add("Date");
                            dt.Columns.Add("Time");
                            dt.Columns.Add("Id");
                        }

                        DataRow dr = dt.NewRow();
                        dr["GIT_TRANSFER_PACKGE_NAME"] = txtPackagename.Text;
                        dr["NoofNights"] = txtNoofNights.Text;
                        dr["Supplier"] = drpsupplier.Text;
                        dr["Date"] = txtDate.Text;
                        dr["Time"] = txttime.SelectedDate;
                        dr["Id"] = lblPackid.Text;
                        dt.Rows.Add(dr);

                    }
                }

                gv.DataSource = dt;
                gv.DataBind();

                foreach (GridViewRow item in gv.Rows)
                {
                    int itm = item.DataItemIndex;
                    if (itm >= rowIndex)
                    {
                        itm = itm + 1;
                    }
                    Label txtPackagename = (Label)item.FindControl("lblPackName");
                    TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                    DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                    TextBox txtDate = (TextBox)item.FindControl("txtDate");
                    RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                    Label lblPackid = (Label)item.FindControl("lblPackid");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            fillCoachSupplier(gridCoachRate, itm);

                            txtPackagename.Text = dt.Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                            txtNoofNights.Text = dt.Rows[itm]["NoofNights"].ToString();
                            drpsupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                            txtDate.Text = dt.Rows[itm]["Date"].ToString();
                            lblPackid.Text = dt.Rows[itm]["Id"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
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

        protected void btnCoachRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                CoachremoveRow(gridCoachRate, rowID, upCoach);
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

        #region Guide
        protected void btnGuide_Click(object sender, EventArgs e)
        {
            DropDownList drpsupplier = new DropDownList();
            Button btnGuideRate = sender as Button;
            int repeaterItemIndex = ((GridViewRow)btnGuideRate.NamingContainer).DataItemIndex;
            int Itemindex = repeaterItemIndex;
            foreach (GridViewRow item in gridGuide.Rows)
            {
                if (Itemindex == item.DataItemIndex)
                {
                    DropDownList drpSupplierName = (DropDownList)item.FindControl("drpSupplier");
                    Session["SupplierName"] = drpSupplierName.Text;
                    Response.Redirect("~/Views/GIT/GuideRate.aspx?TOURID="+ TourId);
                }
            }

        }
        protected void fillGuideSupplier(GridView gv)
        {
            DataSet ds = objGitDetail.fetchTransportPackageGuide("GET_GUIDE_FOR_GIT", int.Parse(Session["TransferPackgeId"].ToString()));
            foreach (GridViewRow item in gv.Rows)
            {

                DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                binddropdownlist(drpSupplier, ds);
            }
        }
        protected void AddGuide(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();


            foreach (GridViewRow item in gv.Rows)
            {

                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                TextBox txtNoofGuides = (TextBox)item.FindControl("txtGuides");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");


                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofNights");
                    dt.Columns.Add("NoofGuides");
                    dt.Columns.Add("Supplier");

                }

                DataRow dr = dt.NewRow();

                dr["GIT_TRANSFER_PACKGE_NAME"] = txtPackagename.Text;
                dr["NoofNights"] = txtNoofNights.Text;
                dr["NoofGuides"] = txtNoofGuides.Text;
                dr["Supplier"] = drpsupplier.Text;


                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofNights");
                    dt.Columns.Add("NoofGuides");
                    dt.Columns.Add("Supplier");
                }


                DataRow dr = dt.NewRow();
                dr["GIT_TRANSFER_PACKGE_NAME"] = "";
                dr["NoofNights"] = "";
                dr["NoofGuides"] = "";
                dr["Supplier"] = "";

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
                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofNights = (TextBox)item.FindControl("txtNights");
                TextBox txtNoofGuides = (TextBox)item.FindControl("txtGuides");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        fillGuideSupplier(gridGuide);

                        txtPackagename.Text = dt.Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                        txtNoofNights.Text = dt.Rows[itm]["NoofNights"].ToString();
                        txtNoofGuides.Text = dt.Rows[itm]["NoofGuides"].ToString();
                        drpsupplier.Text = dt.Rows[itm]["Supplier"].ToString();


                    }
                }
            }
            uppanel.Update();

        }
        protected void btnAddGuide_Click(object sender, EventArgs e)
        {
            AddGuide(gridGuide, Upguide);
        }
        #endregion

        #region Boat
        protected void fillBoatSupplier(GridView gv, int index)
        {
            DataSet ds = objGitDetail.fetchTransportPackageBoat("GET_BOAT_FOR_GIT", int.Parse(Session["TransferPackgeId"].ToString()));
            foreach (GridViewRow item in gv.Rows)
            {
                int i = item.DataItemIndex;

                if (i == index)
                {
                    DropDownList drpSupplier = (DropDownList)item.FindControl("drpSupplier");
                    binddropdownlist(drpSupplier, ds);
                }
            }
        }

        protected void btnBoat_Click(object sender, EventArgs e)
        {
            DropDownList drpsupplier = new DropDownList();
            Button btnBoatRate = sender as Button;
            int repeaterItemIndex = ((GridViewRow)btnBoatRate.NamingContainer).DataItemIndex;
            int Itemindex = repeaterItemIndex;
            foreach (GridViewRow item in gridBoat.Rows)
            {
                if (Itemindex == item.DataItemIndex)
                {
                    DropDownList drpSupplierName = (DropDownList)item.FindControl("drpSupplier");
                    Session["SupplierName"] = drpSupplierName.Text;
                    Response.Redirect("~/Views/GIT/BoatRate.aspx?TOURID=" + TourId);
                }
            }

        }

        protected void AddBoat(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();


            foreach (GridViewRow item in gv.Rows)
            {

                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofBoats = (TextBox)item.FindControl("txtBoats");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                TextBox txtdate = (TextBox)item.FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                Label lblPackid = (Label)item.FindControl("lblPackid");
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofBoats");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("Id");
                }

                DataRow dr = dt.NewRow();

                dr["GIT_TRANSFER_PACKGE_NAME"] = txtPackagename.Text;
                dr["NoofBoats"] = txtNoofBoats.Text;
                dr["Supplier"] = drpsupplier.Text;
                dr["Date"] = txtdate.Text;
                dr["Time"] = txttime.SelectedDate;
                dr["Id"] = lblPackid.Text;
                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                    dt.Columns.Add("NoofBoats");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Time");
                    dt.Columns.Add("Id");
                }


                DataRow dr = dt.NewRow();
                dr["GIT_TRANSFER_PACKGE_NAME"] = "";
                dr["NoofBoats"] = "";
                dr["Supplier"] = "";
                dr["Date"] = "";
                dr["Time"] = "";
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
                Label txtPackagename = (Label)item.FindControl("lblPackName");
                TextBox txtNoofBoats = (TextBox)item.FindControl("txtBoats");
                DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                TextBox txtdate = (TextBox)item.FindControl("txtDate");
                RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        fillBoatSupplier(gridBoat, itm);

                        txtPackagename.Text = dt.Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                        txtNoofBoats.Text = dt.Rows[itm]["NoofBoats"].ToString();
                        drpsupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                        txtdate.Text = dt.Rows[itm]["Date"].ToString();
                        if (dt.Rows[itm]["Time"].ToString() != "")
                        {
                            txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
                        }
                    }
                }
            }
            uppanel.Update();

        }

        protected void btnAddBoat_Click(object sender, EventArgs e)
        {
            AddBoat(gridBoat, upboat);
        }

        protected void btnBoatRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
                int rowID = Convert.ToInt16(row.RowIndex);
                BoatremoveRow(gridBoat, rowID ,upboat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            
        }

        protected void BoatremoveRow(GridView gv, int rowIndex, UpdatePanel uppanel)
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
                    if (rowIndex != item.DataItemIndex)
                    {
                        Label txtPackagename = (Label)item.FindControl("lblPackName");
                        TextBox txtNoofBoats = (TextBox)item.FindControl("txtBoats");
                        DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                        TextBox txtdate = (TextBox)item.FindControl("txtDate");
                        RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");
                        Label lblPackid = (Label)item.FindControl("lblPackid");

                        if (dt.Columns.Count == 0)
                        {
                            dt.Columns.Add("GIT_TRANSFER_PACKGE_NAME");
                            dt.Columns.Add("NoofBoats");
                            dt.Columns.Add("Supplier");
                            dt.Columns.Add("Date");
                            dt.Columns.Add("Time");
                            dt.Columns.Add("Id");
                        }

                        DataRow dr = dt.NewRow();
                        dr["GIT_TRANSFER_PACKGE_NAME"] = txtPackagename.Text;
                        dr["NoofBoats"] = txtNoofBoats.Text;
                        dr["Supplier"] = drpsupplier.Text;
                        dr["Date"] = txtdate.Text;
                        dr["Time"] = txttime.SelectedDate;
                        dr["Id"] = lblPackid.Text;

                        dt.Rows.Add(dr);
                    }
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
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            Label txtPackagename = (Label)item.FindControl("lblPackName");
                            TextBox txtNoofBoats = (TextBox)item.FindControl("txtBoats");
                            DropDownList drpsupplier = (DropDownList)item.FindControl("drpSupplier");
                            TextBox txtdate = (TextBox)item.FindControl("txtDate");
                            RadTimePicker txttime = (RadTimePicker)item.FindControl("rdtpTime");

                            fillBoatSupplier(gridBoat, itm);

                            txtPackagename.Text = dt.Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                            txtNoofBoats.Text = dt.Rows[itm]["NoofBoats"].ToString();
                            drpsupplier.Text = dt.Rows[itm]["Supplier"].ToString();
                            txtdate.Text = dt.Rows[itm]["Date"].ToString();
                            if (dt.Rows[itm]["Time"].ToString() != "")
                            {
                                txttime.SelectedDate = DateTime.Parse(dt.Rows[itm]["Time"].ToString());
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
        #endregion

    }
}