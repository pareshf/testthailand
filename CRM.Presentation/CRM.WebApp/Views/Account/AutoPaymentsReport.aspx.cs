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
using System.IO;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.WebApp.Views.Account
{
    public partial class AutoPaymentsReport : System.Web.UI.Page
    {
        string fromdate = "";
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
            ReportParameter[] parm = new ReportParameter[1];

            if (fromdate == "")
            {
                parm[0] = new ReportParameter("DUE_DATE", value: null);
            }
            else
            {
              //  DateTime dt = DateTime.ParseExact(fromdate.ToString(), "dd/MM/yyyy", null);
                parm[0] = new ReportParameter("DUE_DATE", fromdate);
            }
           
            rptViewer1.ShowCredentialPrompts = true;
            rptViewer1.ShowParameterPrompts = true;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/AutoPaymentsToBeMade";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename = AutoPaymentsToBeMade." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();
        }
    }
}