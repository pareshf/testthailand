using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using System.Web.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CRM.WebApp.Views.MIS
{
    public partial class OnAccountsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string suppliername = "";
            string fromdate = "";
            string todate = "";
          
            string reportType = "excel";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>excel</OutputFormat>" +
            "  <PageWidth>14in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>0.50in</MarginLeft>" +
            "  <MarginRight>0.50in</MarginRight>" +
            "  <MarginBottom>0.50in</MarginBottom>" +
            "</DeviceInfo>";

            fromdate = Page.Request.QueryString["key1"].ToString();
            todate = Page.Request.QueryString["key2"].ToString();
            suppliername = Page.Request.QueryString["key"].ToString();
            ReportParameter[] parm = new ReportParameter[3];

            if (suppliername == "")
            {
                parm[0] = new ReportParameter("GL_DESCRIPTION", " ");
            }
            else
            {
               // DateTime dt = DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null);
                parm[0] = new ReportParameter("GL_DESCRIPTION", suppliername);
            }
            //
            //
            if (fromdate == "")
            {
                parm[1] = new ReportParameter("FROM_DATE", " ");
            }
            else
            {
                DateTime dt = DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null);
                parm[1] = new ReportParameter("FROM_DATE", dt.ToString());
               // parm[1] = new ReportParameter("FROM_DATE", fromdate);
            }

            if (todate != "")
            {
                DateTime dt = DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null);
                parm[2] = new ReportParameter("TO_DATE", dt.ToString());
            }
            else
            {
                parm[2] = new ReportParameter("TO_DATE", " ");
            }

            rptViewer1.ShowCredentialPrompts = true;
            rptViewer1.ShowParameterPrompts = true;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/OnAccount";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename = OnAccountReport." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();

        }
    }
}