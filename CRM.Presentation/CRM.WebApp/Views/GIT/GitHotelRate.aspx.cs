using System;
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
    public partial class GitHotelRate : System.Web.UI.Page
    {
        EditUpdateGITInformation objEditUpdateGITInformation = new EditUpdateGITInformation();
        // BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();

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
                        lblHoteName.Text = Session["HotelName"].ToString();
                        TourId = Request.QueryString["TOURID"].ToString();
                        DataSet ds = objEditUpdateGITInformation.GetHotelRate(int.Parse(TourId), Session["HotelName"].ToString(), Session["RoomType"].ToString(), Session["City"].ToString());
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
                TextBox txtSingleRate = (TextBox)item.FindControl("txtSingleRate");
                TextBox txtDoubleRate = (TextBox)item.FindControl("txtDoubleRate");
                TextBox txtTripleRate = (TextBox)item.FindControl("txtTripleRate");
                TextBox txtAdultRate = (TextBox)item.FindControl("txtExtraAdultRate");
                TextBox txtCWBRate = (TextBox)item.FindControl("txtCWBRate");
                TextBox txtCNBRate = (TextBox)item.FindControl("txtCNBRate");

                Label cartID = (Label)item.FindControl("lblCartID");

                objEditUpdateGITInformation.saveHotelRate(int.Parse(cartID.Text), txtSingleRate.Text, txtDoubleRate.Text, txtTripleRate.Text, txtAdultRate.Text, txtCWBRate.Text, txtCNBRate.Text);

                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
            }
        }
    }

     
}