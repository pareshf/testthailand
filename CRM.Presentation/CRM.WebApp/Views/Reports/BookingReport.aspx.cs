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

namespace FlamingoReports
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Text = "1";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("BOOKING_ID", TextBox1.Text.Trim());
            rptViewer.ShowCredentialPrompts = false;
            rptViewer.ShowParameterPrompts = false;

            rptViewer.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer.ServerReport.ReportPath = "/FlamingoSSRSReports/rdlTourBooking";
            rptViewer.ServerReport.SetParameters(parm);
            rptViewer.ServerReport.Refresh();
        }
    }
}
//"/FlamingoSSRSReports/rdlTourBooking"
//"~/CRM/CRM.Presentation/CRM.WebApp/Views/Reports/rdlTourBooking"