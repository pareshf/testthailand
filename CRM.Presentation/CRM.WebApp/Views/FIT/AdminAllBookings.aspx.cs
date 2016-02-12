﻿using System;
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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
namespace CRM.WebApp.Views.FIT
{
    public partial class AdminAllBookings : System.Web.UI.Page
    {
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        bool flag = false;

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 248);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                  

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                  
                    DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                    binddropdownlist(DropDownList2, dsval);
                    DropDownList2.SelectedValue = "Booked";
                    DropDownList2.Enabled = false;
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
            
        }
     
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            
            string quoteid = GV_Result.Rows[newindex].Cells[1].Text;
            string tourid = GV_Result.Rows[newindex].Cells[2].Text;
            string usrid = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (quoteid == ds.Tables[0].Rows[i]["QUOTE_ID"].ToString() && tourid == ds.Tables[0].Rows[i]["TOUR_ID"].ToString())
                {
                    Session["editorderstatus"] = ds.Tables[0].Rows[i]["ORDER_STATUS"].ToString();
                }
            }

            Response.Redirect("~/Views/FIT/AdminBookingFit.aspx?TOURID=" + tourid + "&QUOTEID=" + quoteid);
        }
     
        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            Updateconfirm.Update();
        }
    
        protected void searchnow_onclick(object sender, EventArgs e)
        {
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallDatasearch("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES_SEARCH", USERID, txtrefrence.Text, TextBox5.Text, TextBox1.Text, DropDownList2.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
       
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallDatasearch("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES_SEARCH", USERID, txtrefrence.Text, TextBox5.Text, TextBox1.Text, DropDownList2.Text, TextBox2.Text, TextBox3.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }

        protected void btnSelect_onclick(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                chk.Checked = true;

            }
            Updateconfirm.Update();
        }

        protected void validation()
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");

                if (chk.Checked)
                {
                    flag = true;
                    break;
                }
            }
        }

        protected void btnRemove_onclick(object sender, EventArgs e)
        {
            validation();
            if (flag == false)
            {
                Master.DisplayMessage("Select records for Remove", "successMessage", 5000);
            }
            else
            {
                foreach (GridViewRow item in GV_Result.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");

                    if (chk.Checked)
                    {
                        string quoteid = GV_Result.Rows[item.DataItemIndex].Cells[1].Text;
                        objfitquote.Update_Status_Quote(quoteid);
                    }
                }
                DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", Session["usersid"].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                }
                else
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                }
                Master.DisplayMessage("Record Removed Successfully.", "successMessage", 5000);
            }
            Updateconfirm.Update();
        }

    }
}