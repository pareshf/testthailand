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


namespace CRM.WebApp.Views.MIS
{
    public partial class PaymentToBeReceivedReport : System.Web.UI.Page
    {
        string fromdate = "";
        string todate = "";
        string tourfromdate = "";
        string tourtodate = "";
        string suppliercompanyname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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

            fromdate = Page.Request.QueryString["key"].ToString();
            todate = Page.Request.QueryString["key1"].ToString();
            tourfromdate = Page.Request.QueryString["key2"].ToString();
            tourtodate = Page.Request.QueryString["key3"].ToString();
            suppliercompanyname = Page.Request.QueryString["key4"].ToString();
            ReportParameter[] parm = new ReportParameter[5];
            if (suppliercompanyname != "")
            {
                parm[0] = new ReportParameter("COMPANY_NAME", suppliercompanyname.ToString());
            }
            else
            {
                parm[0] = new ReportParameter("COMPANY_NAME"," ");
            }
            if (fromdate == "")
            {
                parm[1] = new ReportParameter("FROM_INVOICE_DATE", value: null);
            }
            else
            {
                DateTime dt = DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null);
                parm[1] = new ReportParameter("FROM_INVOICE_DATE", dt.ToString());
            }
            //
            
            //
            if (todate == "")
            {
                parm[2] = new ReportParameter("TO_INVOICE_DATE",value:null);
            }
            else
            {
                DateTime dt1 = DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null);
                parm[2] = new ReportParameter("TO_INVOICE_DATE", dt1.ToString());
            }

            if (tourfromdate == "")
            {
                parm[3] = new ReportParameter("FROM_TOUR_DATE", value: null);
            }
            else
            {
                DateTime dt2 = DateTime.ParseExact(tourfromdate.ToString(), "dd/MM/yyyy", null);
                parm[3] = new ReportParameter("FROM_TOUR_DATE", dt2.ToString());
            }
            if (tourtodate == "")
            {
                parm[4] = new ReportParameter("TO_TOUR_DATE", value: null);
            }
            else
            {
                DateTime dt3 = DateTime.ParseExact(tourtodate.ToString(), "dd/MM/yyyy", null);
                parm[4] = new ReportParameter("TO_TOUR_DATE", dt3.ToString());
            }
            rptViewer1.ShowCredentialPrompts = true;
            rptViewer1.ShowParameterPrompts = true;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/PaymentsToBeReceived";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename = PaymentsToBeReceived." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();

        }
    }
}