﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace CRM.WebApp.Views.GIT
{
    public partial class GitMealRate : System.Web.UI.Page
    {
        EditUpdateGITInformation objEditUpdateGITInformation = new EditUpdateGITInformation();

        #region VARIABLES

        String TourId;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                    {
                        lblResturant.Text = Session["ResturantName"].ToString();
                        TourId = Request.QueryString["TOURID"].ToString();
                        DataSet ds = objEditUpdateGITInformation.GetResturantRate(int.Parse(TourId), Session["ResturantName"].ToString(), Session["MealType"].ToString(), Session["City"].ToString());
                        gridHotelRate.DataSource = ds;
                        gridHotelRate.DataBind();
                    }
                }
                else
                {
                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                    {
                        TourId = Request.QueryString["TOURID"].ToString();
                    }
                }

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/GitDetails.aspx?TOURID=" + Request.QueryString["TOURID"].ToString());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in gridHotelRate.Rows)
            {
                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");

                Label cartID = (Label)item.FindControl("lblCartID");

                objEditUpdateGITInformation.saveResturantRate(int.Parse(cartID.Text), txtAdultRate.Text, txtChildRate.Text);

                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
            }

        }
        
    }
}