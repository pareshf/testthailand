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
using CRM.WebApp.WebHelper;
using System.Web.Configuration;


namespace CRM.WebApp.Views.Sales
{
    public partial class CityWiseRoomAllocation : System.Web.UI.Page
    {
        string tourid = "";
        string cityid = "";
        string countryid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            tourid = Page.Request.QueryString["key"].ToString();
            cityid = Page.Request.QueryString["key1"].ToString();
            countryid = Page.Request.QueryString["key2"].ToString();

            ReportParameter[] parm = new ReportParameter[1];
            ReportParameter[] parmcity = new ReportParameter[1];
            ReportParameter[] parmcountry = new ReportParameter[1];
            
            parm[0] = new ReportParameter("TOUR_ID", tourid);
            parmcity[0] = new ReportParameter("CITY_ID", cityid);
            parmcountry[0] = new ReportParameter("COUNTRY_ID", countryid);

            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/FlamingoSSRSReports/rdlTourHotelStdCityWise";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.SetParameters(parmcity);
            rptViewer1.ServerReport.SetParameters(parmcountry);
            rptViewer1.ServerReport.Refresh();
        }
    }
}