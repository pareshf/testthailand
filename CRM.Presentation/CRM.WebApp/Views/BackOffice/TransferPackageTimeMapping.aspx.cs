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
using CRM.DataAccess.AdministrationDAL;

namespace CRM.WebApp.Views.BackOffice
{
    public partial class TransferPackageTimeMapping : System.Web.UI.Page
    {
        TransferPackageTimeMappingDal objTransferPackageTimeMapping = new TransferPackageTimeMappingDal();

        #region Variables
        DataSet ds = null;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet ds = objTransferPackageTimeMapping.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_FROM_TO_MASTER");

                binddropdownlist(drpTransferFrom, ds);
                binddropdownlist(drpTransferTo, ds);
                //binddropdownlist(drpTime, ds1);
                bindGrid("", "");
                AddTime(gridTransferTime, upTransferTime);
            }
        }
        #endregion


        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }

        
        #region Grid
        protected void bindGrid(string from, string to)
        {
            //Bind the grid.

            ds = objTransferPackageTimeMapping.bindSelectedTransferTimmingGrid(from, to);
            gridTransferTime.DataSource = ds;
            gridTransferTime.DataBind();
        }

        protected void GV_time_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridTransferTime.PageIndex = e.NewPageIndex;
            //bindGrid();
            uptimegrid.Update();

        }
        #endregion

        #region all function
        protected void Clear()
        {
            //drpTime.Text = "";
            drpTransferFrom.Text = "";
            drpTransferTo.Text = "";
        }

        protected void AddTime(GridView gv, UpdatePanel uppanel)
        {
            int count = gv.Rows.Count;
            int count1 = count + 1;
            DataTable dt = new DataTable();


            foreach (GridViewRow item in gv.Rows)
            {

                Label lblfrom = (Label)item.FindControl("lblFrom");
                Label lblto = (Label)item.FindControl("lblTo");
                DropDownList drptime = (DropDownList)item.FindControl("drpTime");


                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("FROM_NAME");
                    dt.Columns.Add("TO_NAME");
                    dt.Columns.Add("TIME");

                }

                DataRow dr = dt.NewRow();

                dr["FROM_NAME"] = lblfrom.Text;
                dr["TO_NAME"] = lblto.Text;
                dr["TIME"] = drptime.Text;


                dt.Rows.Add(dr);

            }

            if (count == 0)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("FROM_NAME");
                    dt.Columns.Add("TO_NAME");
                    dt.Columns.Add("TIME");
                }


                DataRow dr = dt.NewRow();
                dr["FROM_NAME"] = "";
                dr["TO_NAME"] = "";
                dr["TIME"] = "";

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
                Label lblfrom = (Label)item.FindControl("lblFrom");
                Label lblto = (Label)item.FindControl("lblTo");
                DropDownList drptime = (DropDownList)item.FindControl("drpTime");

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (itm == k)
                    {
                        DataSet ds = objTransferPackageTimeMapping.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_TIMING");
                        binddropdownlist(drptime, ds);

                        lblfrom.Text = dt.Rows[0]["FROM_NAME"].ToString();
                        lblto.Text = dt.Rows[0]["TO_NAME"].ToString();
                        drptime.Text = dt.Rows[itm]["TIME"].ToString();


                    }
                }
            }
            uppanel.Update();

        }
        #endregion   

        #region Button Click

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddTime(gridTransferTime, uptimegrid);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DropDownList drpTime = new DropDownList();
            Label lblFrom = new Label();
            Label lblTo = new Label();
            string From;
            string To;
            string Time;
            if (gridTransferTime.Rows.Count != 0)
            {
                lblFrom = (Label)gridTransferTime.Rows[0].FindControl("lblFrom");
                lblTo = (Label)gridTransferTime.Rows[0].FindControl("lblTo");

                From = lblFrom.Text;
                To = lblTo.Text;

                objTransferPackageTimeMapping.deleteTransferTime(From, To);
                for (int i = 0; i < gridTransferTime.Rows.Count; i++)
                {
                    lblFrom = (Label)gridTransferTime.Rows[i].FindControl("lblFrom");
                    lblTo = (Label)gridTransferTime.Rows[i].FindControl("lblTo");
                    drpTime = (DropDownList)gridTransferTime.Rows[i].FindControl("drpTime");
                    From = lblFrom.Text;
                    To = lblTo.Text;
                    Time = drpTime.Text;
                    if (Time != "")
                    {
                        objTransferPackageTimeMapping.insertTransferPackageTimmingMapping(From, To, Time);
                    }
                }
                string from = drpTransferFrom.Text;
                string to = drpTransferTo.Text;
                DataSet ds1 = objTransferPackageTimeMapping.bindSelectedTransferTimmingGrid(from, to);



                gridTransferTime.DataSource = ds1;
                gridTransferTime.DataBind();
                Master.DisplayMessage("Record Save Successfully.", "successMessage", 5000);
                uptimegrid.Update();

                for (int i = 0; i < gridTransferTime.Rows.Count; i++)
                {
                    DataSet ds = objTransferPackageTimeMapping.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_TIMING");
                    DropDownList drTime = (DropDownList)gridTransferTime.Rows[i].FindControl("drpTime");
                    binddropdownlist(drTime, ds);
                    drTime.Text = ds1.Tables[0].Rows[i]["TIME"].ToString();
                }
            }
            else
            {
                Master.DisplayMessage("No Record Found..", "successMessage", 5000);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string from = drpTransferFrom.Text;
            string to = drpTransferTo.Text;
            DataSet ds1 = objTransferPackageTimeMapping.bindSelectedTransferTimmingGrid(from, to);

            if (ds1.Tables[0].Rows.Count != 0)
            {

            
            gridTransferTime.DataSource = ds1;
            gridTransferTime.DataBind();
            uptimegrid.Update();

            for (int i = 0; i < gridTransferTime.Rows.Count; i++)
            {
                DataSet ds = objTransferPackageTimeMapping.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_TIMING");
                DropDownList drpTime = (DropDownList)gridTransferTime.Rows[i].FindControl("drpTime");
                binddropdownlist(drpTime, ds);
                drpTime.Text = ds1.Tables[0].Rows[i]["TIME"].ToString();
            }
            }
            else
            {
                bindGrid("", "");
                AddTime(gridTransferTime, uptimegrid);
                foreach (GridViewRow item in gridTransferTime.Rows)
                {
                    int itm = item.DataItemIndex;
                    Label lblfrom = (Label)item.FindControl("lblFrom");
                    Label lblto = (Label)item.FindControl("lblTo");
                    DropDownList drptime = (DropDownList)item.FindControl("drpTime");

                    lblfrom.Text = from;
                    lblto.Text = to;
                        
                   
                }
                upTransferTime.Update();
                uptimegrid.Update();
            }

        }

        #endregion
    }
}