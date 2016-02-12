using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using SSRSReporting;
using System.Web.Configuration;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.WebApp.Views.Sales
{
    public partial class Booking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CRM.DataAccess.AdministratorEntity.NewBookingInformation newbookinginfo = new CRM.DataAccess.AdministratorEntity.NewBookingInformation();
            DataTable dt=new DataTable();
            dt= newbookinginfo.getBookingCode(Convert.ToInt32(Session["usersid"]));
            BOOKING_CODE.DataSource = dt;
            BOOKING_CODE.DataTextField = dt.Columns[0].ToString();
            BOOKING_CODE.DataValueField = dt.Columns[1].ToString();
            BOOKING_CODE.DataBind();
            BOOKING_CODE.Items.Insert(0,new ListItem("--Select--","0"));
            
            if (!IsPostBack)
            {
                
            }
            
        }

        protected void Print_Click(object sender, EventArgs e)
        {

            //ReportParameter[] parm = new ReportParameter[1];
            //parm[0] = new ReportParameter("BOOKING_ID", txtBOOKING_ID.Text.Trim());
            //rptViewer.ShowCredentialPrompts = false;
            //rptViewer.ShowParameterPrompts = false;

            //rptViewer.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            //rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //rptViewer.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            //rptViewer.ServerReport.ReportPath = "/FlamingoSSRSReports/rdlTourBooking";
            //rptViewer.ServerReport.SetParameters(parm);
            //rptViewer.ServerReport.Refresh();
        }

        protected void BOOKING_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSavebookinginfo_Click(object sender, EventArgs e)
        {

        }
    }
}