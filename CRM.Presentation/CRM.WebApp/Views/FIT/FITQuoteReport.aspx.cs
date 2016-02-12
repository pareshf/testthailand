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

namespace CRM.WebApp.Views.FIT
{
    public partial class FITQuoteReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";

                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("QOUTE_ID", Request.QueryString["QuoteId"].ToString());
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/Invoice";
                rptViewer1.ServerReport.SetParameters(parm);

                //renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename = LedgerReport." + fileNameExtension);
                //Response.BinaryWrite(renderedBytes);
                //Response.End();

                rptViewer1.ServerReport.Refresh();

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
           
            
        }
    }
}