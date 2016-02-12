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
using System.Globalization;
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.GITMaster
{
    public partial class TransportPackgePriceListGIT : System.Web.UI.Page
    {
        TransportPriceListGIT objTransportPriceListGIT = new TransportPriceListGIT();
        int maxtransferpackid = 0;
        DataSet ds = null;
        int CoachId = 0;
        int GuideId = 0;
        int BoatId = 0;
        int GitTransferPackageId = 0;
        int GitTransferPackageDetailsId = 0;
        int supplierID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet dsPackage = objTransportPriceListGIT.CommonSp("GET_GIT_PACKAGE");
                binddropdownlist(drpGitPackageName, dsPackage);

                DataSet dsSupplier = objTransportPriceListGIT.CommonSp("GET_TRANSPORT_PACKAGE_SUPPLIER");
                binddropdownlist(drpSupplierName, dsSupplier);

                DataSet dsStatus = objTransportPriceListGIT.CommonSp("GET_STATUS_FOR_MASTER");
                binddropdownlist(drpStatus, dsStatus);

                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                if (Request["TransferPriceListId"] != null && !string.IsNullOrEmpty(Request["TransferPriceListId"].ToString()))
                {
                    GitTransferPackageId = Convert.ToInt32(Request.QueryString["TransferPriceListId"].ToString());

                }
                if (GitTransferPackageId != 0)
                {
                    DataSet dtTransferPackage = objTransportPriceListGIT.fetchTransferPriceListDataForEdit(GitTransferPackageId);

                    txtTrasportPackageName.Text = dtTransferPackage.Tables[0].Rows[0]["GIT_TRANSFER_PACKGE_NAME"].ToString();
                    drpGitPackageName.Text = dtTransferPackage.Tables[0].Rows[0]["GIT_PACKAGE_NAME"].ToString();
                    drpStatus.Text = dtTransferPackage.Tables[0].Rows[0]["STATUS"].ToString();
                    drpSupplierName.Text = dtTransferPackage.Tables[0].Rows[0]["CHAIN_NAME"].ToString();

                    AddTransferDetails(gridDetails, upTrasportDetails);
                    fillTransferPackageDetailsEditMode(gridDetails, upTrasportDetails);
                    lblTransferDetailsHeader.Visible = true;
                    TrasportDetails.Visible = true;
                    gridDetails.Visible = true;
                    upTrasportDetails.Update();

                    fillCoachDetailsEditMode(gridAllCoached, upCoach);
                    lblHeaderCoachDetails.Visible = true;
                    divCoach.Visible = true;
                    DataSet dsCoach = objTransportPriceListGIT.CommonSp("GET_COACH_SUPPLIER");
                    binddropdownlist(drpCoachSupplier, dsCoach);
                    upCoach.Update();

                    fillBoatDetailsEditMode(gridBoats, upBoat);
                    lblHeaderBoatDetails.Visible = true;
                    divboat.Visible = true;
                    DataSet dsBoat = objTransportPriceListGIT.CommonSp("GET_BOAT_SUPPLIER");
                    binddropdownlist(drpBoatSupplier, dsBoat);
                    upBoat.Update();

                    fillGuideDetailsEditMode(gridGuide, upGuide);
                    lblHeaderGuideDetails.Visible = true;
                    divGuide.Visible = true;
                    DataSet dsGuide = objTransportPriceListGIT.CommonSp("GET_GUIDE_SUPPLIER");
                    binddropdownlist(drpGuide, dsGuide);
                    upGuide.Update();
                }
                else
                {

                }


            }

            if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
            {
                supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
            }
            if (Request["TransferPriceListId"] != null && !string.IsNullOrEmpty(Request["TransferPriceListId"].ToString()))
            {
                GitTransferPackageId = Convert.ToInt32(Request.QueryString["TransferPriceListId"].ToString());

            }
        }


        #region BIND FUNCTION

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //   r.SelectedValue = "0";
        }

        protected void bindCoachGrid()
        {
            //Bind the grid.            
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            ds = objTransportPriceListGIT.bindCoachGrid(transferpackageid);
            gridAllCoached.DataSource = ds;
            gridAllCoached.DataBind();
        }

        protected void bindGuideGrid()
        {
            //Bind the grid.
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            ds = objTransportPriceListGIT.bindGuideGrid(transferpackageid);
            gridGuide.DataSource = ds;
            gridGuide.DataBind();
        }

        protected void bindBoatGrid()
        {
            //Bind the grid.
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            ds = objTransportPriceListGIT.bindBoatGrid(transferpackageid);
            gridBoats.DataSource = ds;
            gridBoats.DataBind();
        }

        #endregion

        protected void AddTransferDetails(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();

            //   DataSet ds = objGitDetail.fetchComboDataforHotel("FETCH_HOTEL_NAME_FOR_GIT_CITY_WISE", CityName, "01/09/2012", "11/09/2012");



            foreach (GridViewRow item in gv.Rows)
            {
                DropDownList drpFrom = (DropDownList)item.FindControl("drpFrom");
                DropDownList drpTo = (DropDownList)item.FindControl("drpTo");


                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("From");
                    dt.Columns.Add("To");


                }

                DataRow dr = dt.NewRow();
                dr["From"] = drpFrom.Text;
                dr["To"] = drpTo.Text;

                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("From");
                    dt.Columns.Add("To");

                }

                DataRow dr = dt.NewRow();
                dr["From"] = "";
                dr["To"] = "";

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
                DropDownList drpFrom = (DropDownList)item.FindControl("drpFrom");
                DropDownList drpTo = (DropDownList)item.FindControl("drpTo");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        DataSet ds = objTransportPriceListGIT.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_NAME_AUTOSERACH");
                        binddropdownlist(drpFrom, ds);
                        binddropdownlist(drpTo, ds);
                        drpFrom.Text = dt.Rows[itm]["From"].ToString();
                        drpTo.Text = dt.Rows[itm]["To"].ToString();

                    }
                }
            }
            uppanel.Update();

        }

        #region Button Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int transferPackID = 0;
            if (ViewState["TransferPackageID"] != null)
            {
                transferPackID = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferPackID = GitTransferPackageId;
            }
            DataSet dtforvalidation = objTransportPriceListGIT.FetchCountForValidation(drpGitPackageName.Text, transferPackID);
            if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
            {
                DataSet dttransferPackage = objTransportPriceListGIT.insertTransportPackage(transferPackID, txtTrasportPackageName.Text, drpGitPackageName.Text, drpStatus.Text, drpSupplierName.Text, int.Parse(Session["usersid"].ToString()));

                maxtransferpackid = Convert.ToInt32(dttransferPackage.Tables[0].Rows[0]["GIT_TRANSFER_PACKAGE_ID"].ToString());
                ViewState["TransferPackageID"] = maxtransferpackid;
                lblTransferDetailsHeader.Visible = true;
                TrasportDetails.Visible = true;
                gridDetails.Visible = true;
                upTrasportDetails.Update();
                if (GitTransferPackageId == 0)
                {
                    AddTransferDetails(gridDetails, upTrasportDetails);
                }
                lblHeaderCoachDetails.Visible = true;
                divCoach.Visible = true;
                DataSet dsCoach = objTransportPriceListGIT.CommonSp("GET_COACH_SUPPLIER");
                binddropdownlist(drpCoachSupplier, dsCoach);
                upCoach.Update();

                lblHeaderBoatDetails.Visible = true;
                divboat.Visible = true;
                DataSet dsBoat = objTransportPriceListGIT.CommonSp("GET_BOAT_SUPPLIER");
                binddropdownlist(drpBoatSupplier, dsBoat);
                upBoat.Update();

                lblHeaderGuideDetails.Visible = true;
                divGuide.Visible = true;
                DataSet dsGuide = objTransportPriceListGIT.CommonSp("GET_GUIDE_SUPPLIER");
                binddropdownlist(drpGuide, dsGuide);
                upGuide.Update();

                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('This GIT Package is already exists.')", true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddTransferDetails(gridDetails, upTrasportDetails);
        }

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            DropDownList drpFrom = new DropDownList();
            DropDownList drpTo = new DropDownList();

            string From;
            string To;
            int transferpackageid = 0;
            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            if (gridDetails.Rows.Count != 0)
            {

                objTransportPriceListGIT.deleteTransferPackageDetails(transferpackageid);
                
                for (int i = 0; i < gridDetails.Rows.Count; i++)
                {
                    drpFrom = (DropDownList)gridDetails.Rows[i].FindControl("drpFrom");
                    drpTo = (DropDownList)gridDetails.Rows[i].FindControl("drpTo");

                    From = drpFrom.Text;
                    To = drpTo.Text;


                    if (From != "" && To != "")
                    {
                        if (GitTransferPackageId == 0)
                        {
                            objTransportPriceListGIT.insertTransportPackageDetails(GitTransferPackageDetailsId, transferpackageid, From, To, int.Parse(Session["usersid"].ToString()));
                        }
                        else
                        {
                            objTransportPriceListGIT.insertTransportPackageDetails(GitTransferPackageDetailsId, GitTransferPackageId, From, To, int.Parse(Session["usersid"].ToString()));
                        }
                    }
                }
                DataSet dtTransferPackage = objTransportPriceListGIT.fetchTransferPackageDetails(transferpackageid);
                gridDetails.DataSource = dtTransferPackage;
                gridDetails.DataBind();
                for (int i = 0; i < gridDetails.Rows.Count; i++)
                {
                    DataSet ds = objTransportPriceListGIT.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_NAME_AUTOSERACH");
                    DropDownList drFrom = (DropDownList)gridDetails.Rows[i].FindControl("drpFrom");
                    DropDownList drTo = (DropDownList)gridDetails.Rows[i].FindControl("drpTo");
                    binddropdownlist(drFrom, ds);
                    binddropdownlist(drTo, ds);
                    drFrom.Text = dtTransferPackage.Tables[0].Rows[i]["FROM_NAME"].ToString();
                    drTo.Text = dtTransferPackage.Tables[0].Rows[i]["TO_NAME"].ToString();
                }
                upTrasportDetails.Update();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('No Record Found..')", true);
            }
            
            
        }

        protected void btnSaveCoach_Click(object sender, EventArgs e)
        {
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            if (ViewState["CoachId"] != null)
            {
                CoachId = Convert.ToInt32(ViewState["CoachId"].ToString());
            }

            if (drpCoachSupplier.Text != "")
            {
                DataSet dtforvalidation = objTransportPriceListGIT.FetchCountForCoachValidation(drpCoachSupplier.Text, CoachId, transferpackageid);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                    objTransportPriceListGIT.insertCoach(CoachId, drpCoachSupplier.Text, txtCoachRate.Text, transferpackageid, 6, int.Parse(Session["usersid"].ToString()));
                    drpCoachSupplier.Text = "";
                    txtCoachRate.Text = "";
                    ViewState["CoachId"] = null;
                    bindCoachGrid();
                    upCoach.Update();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Coach is already exists.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Coach Supplier.')", true);
            }
        }

        protected void btnSaveGuide_Click(object sender, EventArgs e)
        {
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            if (ViewState["GuideId"] != null)
            {
                GuideId = Convert.ToInt32(ViewState["GuideId"].ToString());
            }
            if (drpGuide.Text != "")
            {
                DataSet dtforvalidation = objTransportPriceListGIT.FetchCountForGuideValidation(drpGuide.Text, GuideId, transferpackageid);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                objTransportPriceListGIT.insertGuide(GuideId, drpGuide.Text, txtGuideRate.Text, transferpackageid, 6, int.Parse(Session["usersid"].ToString()));
                drpGuide.Text = "";
                txtGuideRate.Text = "";
                ViewState["GuideId"] = null;
                bindGuideGrid();
                upGuide.Update();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Guide is already exists.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Guide Supplier.')", true);
            }
        }

        protected void btnSaveBoat_Click(object sender, EventArgs e)
        {
            int transferpackageid = 0;

            if (ViewState["TransferPackageID"] != null)
            {
                transferpackageid = Convert.ToInt32(ViewState["TransferPackageID"].ToString());
            }
            else
            {
                transferpackageid = GitTransferPackageId;
            }
            if (ViewState["BoatId"] != null)
            {
                BoatId = Convert.ToInt32(ViewState["BoatId"].ToString());
            }
            if (drpBoatSupplier.Text != "")
            {
                DataSet dtforvalidation = objTransportPriceListGIT.FetchCountForBoatValidation(drpBoatSupplier.Text, BoatId, transferpackageid);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                objTransportPriceListGIT.insertBoat(BoatId, drpBoatSupplier.Text, txtBoatRate.Text, transferpackageid, 6, int.Parse(Session["usersid"].ToString()));
                drpBoatSupplier.Text = "";
                txtBoatRate.Text = "";
                ViewState["BoatId"] = null;
                bindBoatGrid();
                upBoat.Update();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record Save Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Boat is already exists.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Enter Boat Supplier.')", true);
            }
        }
        #endregion

        #region Selected Index Change Event
        protected void GV_Coach_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            ViewState["CoachId"] = null;
            Label lbl = new Label();
            lbl = (Label)gridAllCoached.Rows[newindex].FindControl("lblCoachID");
            CoachId = Convert.ToInt32(lbl.Text);
            ViewState["CoachId"] = CoachId;
            string CoachName = gridAllCoached.Rows[newindex].Cells[2].Text;
            string CoachRate = gridAllCoached.Rows[newindex].Cells[3].Text;
            drpCoachSupplier.Text = CoachName;
            txtCoachRate.Text = CoachRate;
            upCoach.Update();
        }

        protected void GV_Guide_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            ViewState["GuideId"] = null;
            Label lbl = new Label();
            lbl = (Label)gridGuide.Rows[newindex].FindControl("lblGuideID");
            GuideId = Convert.ToInt32(lbl.Text);
            ViewState["GuideId"] = GuideId;
            string GuideName = gridGuide.Rows[newindex].Cells[2].Text;
            string GuideRate = gridGuide.Rows[newindex].Cells[3].Text;
            drpGuide.Text = GuideName;
            txtGuideRate.Text = GuideRate;
            upGuide.Update();
        }

        protected void GV_Boat_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            ViewState["BoatId"] = null;
            Label lbl = new Label();
            lbl = (Label)gridBoats.Rows[newindex].FindControl("lblBoatID");
            BoatId = Convert.ToInt32(lbl.Text);
            ViewState["BoatId"] = BoatId;
            string BoatName = gridBoats.Rows[newindex].Cells[2].Text;
            string BoatRate = gridBoats.Rows[newindex].Cells[3].Text;
            drpBoatSupplier.Text = BoatName;
            txtBoatRate.Text = BoatRate;
            upBoat.Update();
        }
        #endregion

        #region EDIT MODE
        protected void fillTransferPackageDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            DataSet dtTransferPackage = objTransportPriceListGIT.fetchTransferPriceListDataForEdit(GitTransferPackageId);
            for (int j = 0; j < dtTransferPackage.Tables[0].Rows.Count; j++)
            {
                foreach (GridViewRow item in gv.Rows)
                {
                    if (j == item.DataItemIndex)
                    {
                        //  String city = dtCities.Tables[0].Rows[i]["CITY_NAME"].ToString();

                        DropDownList drpFrom = (DropDownList)item.FindControl("drpFrom");
                        DropDownList drpTo = (DropDownList)item.FindControl("drpTo");

                        drpFrom.Text = dtTransferPackage.Tables[0].Rows[j]["FROM_NAME"].ToString();
                        drpTo.Text = dtTransferPackage.Tables[0].Rows[j]["TO_NAME"].ToString();

                    }

                }
                if (j < dtTransferPackage.Tables[0].Rows.Count - 1)
                {
                    AddTransferDetails(gv, uppanel);
                }
            }

        }

        protected void fillCoachDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            //Bind the grid.

            ds = objTransportPriceListGIT.bindCoachGrid(GitTransferPackageId);
            gridAllCoached.DataSource = ds;
            gridAllCoached.DataBind();
        }

        protected void fillBoatDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            //Bind the grid.

            ds = objTransportPriceListGIT.bindBoatGrid(GitTransferPackageId);
            gridBoats.DataSource = ds;
            gridBoats.DataBind();
        }

        protected void fillGuideDetailsEditMode(GridView gv, UpdatePanel uppanel)
        {
            //Bind the grid.

            ds = objTransportPriceListGIT.bindGuideGrid(GitTransferPackageId);
            gridGuide.DataSource = ds;
            gridGuide.DataBind();
        }
        #endregion
    }
}