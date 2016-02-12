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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.MIS
{
    public partial class PaymentToBeMade : System.Web.UI.Page
    {
        string fromdate = "";
        string todate = "";
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
            suppliercompanyname = Page.Request.QueryString["key2"].ToString();
            ReportParameter[] parm = new ReportParameter[3];

            if (fromdate == "")
            {
                parm[0] = new ReportParameter("FROM_DATE", value: null);
            }
            else
            {
                DateTime dt = DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null);
                parm[0] = new ReportParameter("FROM_DATE", dt.ToString());
            }
            //
            //
            if (todate == "")
            {
                parm[1] = new ReportParameter("TO_DATE", value: null);
            }
            else
            {
                DateTime dt1 = DateTime.ParseExact(todate.ToString(), "dd/MM/yyyy", null);
                parm[1] = new ReportParameter("TO_DATE", dt1.ToString());
            }
            
            if (suppliercompanyname != "")
            {
                parm[2] = new ReportParameter("SUPPLIER_COMPANY_NAME", suppliercompanyname);
            }
            else
            {
                parm[2] = new ReportParameter("SUPPLIER_COMPANY_NAME", " ");
            }
            
            rptViewer1.ShowCredentialPrompts = true;
            rptViewer1.ShowParameterPrompts = true;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/PaymentsToBeMadeOnDate";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename = PaymentsToBeMadeOnDate." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();

        }
    }
}