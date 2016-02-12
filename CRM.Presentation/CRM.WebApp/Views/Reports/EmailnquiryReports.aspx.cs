using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
using System.Xml;
using System.Data;
using System.Configuration;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Reports
{
    public partial class EmailnquiryReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();
            rview.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ReportServer"]);

            System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();
            paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Param1", "Value1"));
            paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Param2", "Value2"));

            rview.ServerReport.ReportPath = "/FlamingoSSRSReports/CustomerwiseInquiryReport";
            rview.ServerReport.SetParameters(paramList);

            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;

            string format = "Excel";

            deviceInfo = "<DeviceInfo>" + "<Head1>True</Head1>" + "</DeviceInfo>";
            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            Response.Clear();

            Response.ContentType = "application/excel";
            Response.AddHeader("Content-disposition", "filename=output.xls");

            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.Close();
        }
    }
}