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
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using CRM.DataAccess.AdministratorEntity;
using System.Configuration;

namespace CRM.WebApp.Views.MIS
{
    public partial class Agentwiseprofit : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //  Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 315);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME_WITH_ID");

                drpAgent.Items.Clear();

                drpAgent.DataTextField = "AutoSearchResult";
                drpAgent.DataValueField = "CUST_ID";
                drpAgent.DataSource = ds3;
                drpAgent.DataBind();
                drpAgent.Items.Insert(0, new RadComboBoxItem("Please Select", "0"));
                bindinvoicedropdown();

                // drpAgent.Items.Insert(0, new ListItem("", ""));

                //  binddropdownlist(drpAgent, ds3);

            }


        }

        //protected void binddropdownlist(DropDownList r, DataSet d)
        //{
        //    r.Items.Clear();
        //    r.DataSource = d;
        //    r.DataTextField = "AutoSearchResult";
        //    r.DataBind();
        //    r.Items.Insert(0, new ListItem("", ""));
        //    //r.SelectedValue = "";
        //}

        //btnclear_Click

        protected void btnclear_Click(object sender, EventArgs e)
        {
            drpAgent.SelectedValue = "0";
            rbtninvoicetype.SelectedValue = "2";
            txtfromdate.Text = string.Empty;
            TextBox1.Text = string.Empty;
            txttravelfromdate.Text = string.Empty;
            txttraveltodate.Text = string.Empty;
            drpinvoiceno.SelectedValue = "";
            Updateconfirm.Update();
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {
            string fromdate;
            string todate;

            string travelfromdate;
            string traveltodate;

            string supliercompany;
            fromdate = txtfromdate.Text;
            todate = TextBox1.Text;

            travelfromdate = txttravelfromdate.Text;
            traveltodate = txttraveltodate.Text;

            int InvoiceType;
            InvoiceType = Convert.ToInt32(rbtninvoicetype.SelectedValue);

            string InvoiceNumber = Convert.ToString(drpinvoiceno.SelectedValue);


            DataSet ds3 = objBookingFitStoreProcedure.GET_GL_DESCRIPTION(int.Parse(Session["cust_id"].ToString()), Session["FLAG"].ToString());
            if (ds3.Tables[0].Rows.Count > 0)
            {
                if (drpAgent.Text == "Please Select")
                {
                    supliercompany = "";
                }
                else
                {
                    supliercompany = ds3.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
                }
                
            }
            else
            {
                supliercompany = "";
            }
            
            Session["spnamereport"] = supliercompany;

            if (InvoiceNumber == string.Empty)
            {
                Response.Redirect("~/Views/MIS/ProfitRepotRpt.aspx?key=" + fromdate + "&key1=" + todate + "&key2=" + supliercompany + "&key3=" + InvoiceType + "&key4=" + travelfromdate + "&key5=" + traveltodate + "&key6=" + InvoiceNumber);
            }
            else
            {
                Response.Redirect("~/Views/MIS/ProfitRepotRpt.aspx?key=" + fromdate + "&key1=" + todate + "&key2=" + supliercompany + "&key3=" + InvoiceType + "&key4=" + travelfromdate + "&key5=" + traveltodate + "&key6=" + InvoiceNumber);
            }
        }

        protected void drpAgent_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds = objBookingFitStoreProcedure.GET_comapny_CUSTID(drpAgent.Text);
            if (ds.Tables[0].Rows.Count != 0)
            {
                Session["FLAG"] = "A";
                Session["cust_id"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
            }

            bindinvoicedropdown();
            Updateconfirm.Update();
        }

        protected void bindinvoicedropdown()
        {

            DataSet dsval = objBookingFitStoreProcedure.fetchinvoicompanywise("FETCH_ALL_INVOICE_NO_COMPANY_WISE", Convert.ToString(drpAgent.SelectedValue));

            if (dsval != null)
            {
                //binddropdownlist(drpinvoiceno, dsval);

                if (dsval.Tables[0].Rows.Count > 0)
                {

                    drpinvoiceno.Items.Clear();
                    drpinvoiceno.DataSource = dsval;
                    drpinvoiceno.DataTextField = "AutoSearchResult";
                    drpinvoiceno.DataValueField = "AutoSearchResult";
                    drpinvoiceno.DataBind();
                    drpinvoiceno.Items.Insert(0, new RadComboBoxItem("Please Select", ""));
                }
                else
                {
                    drpinvoiceno.Items.Clear();
                    drpinvoiceno.DataSource = null;
                    drpinvoiceno.DataBind();
                    drpinvoiceno.Items.Insert(0, new RadComboBoxItem("Please Select", ""));
                }
            }
            else
            {
                drpinvoiceno.Items.Clear();
                drpinvoiceno.DataSource = null;
                drpinvoiceno.DataBind();
                drpinvoiceno.Items.Insert(0, new RadComboBoxItem("Please Select", ""));
            }

        }
    }
}