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
    public partial class CoachRate : System.Web.UI.Page
    {
        EditUpdateGITInformation objEditUpdateGITInformation = new EditUpdateGITInformation();

        #region VARIABLES

        int TourId;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                    int GIT_TRANSFER_PACKGE_ID = int.Parse(Session["TransferPackgeId"].ToString());
                    string suppilername = Session["SupplierName"].ToString();
                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                    {
                        //lblHoteName.Text = Session["HotelName"].ToString();
                        TourId = int.Parse(Request.QueryString["TOURID"].ToString());
                        DataSet ds = objEditUpdateGITInformation.GetCoachRate(TourId, GIT_TRANSFER_PACKGE_ID, suppilername);
                        gridCoach.DataSource = ds;
                        gridCoach.DataBind();
                    }
                }
                else
                {
                    if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                    {
                        TourId = int.Parse(Request.QueryString["TOURID"].ToString());
                    }
                }

            }
        

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/TransportRate.aspx?TOURID=" + Request.QueryString["TOURID"].ToString());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in gridCoach.Rows)
            {
                TextBox txtAdultRate = (TextBox)item.FindControl("txtAdultRate");
                TextBox txtChildRate = (TextBox)item.FindControl("txtChildRate");

                Label cartID = (Label)item.FindControl("lblCartID");

                objEditUpdateGITInformation.saveCoachRate(int.Parse(cartID.Text), txtAdultRate.Text, txtChildRate.Text);

                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Record updated Successfully.')", true);
            }

        }
    }
}