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


namespace CRM.WebApp.Views.MIS
{
    public partial class AgentWiseLedgerRpt : System.Web.UI.Page
    {
        string fromdate = "";
        string todate = "";
        string travelfromdate = "";
        string traveltodate = "";
        string suppliercompanyname = "";
        Boolean notsettled;
        protected void Page_Load(object sender, EventArgs e)
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
            notsettled = Convert.ToBoolean(Page.Request.QueryString["key3"]);

            travelfromdate = Page.Request.QueryString["key4"].ToString();
            traveltodate = Page.Request.QueryString["key5"].ToString();



            ReportParameter[] parm = new ReportParameter[6];

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
                suppliercompanyname = Convert.ToString(Session["spnamereport"]);
                parm[2] = new ReportParameter("GL_DESCRIPTION", suppliercompanyname);
            }
            else
            {
                parm[2] = new ReportParameter("GL_DESCRIPTION", " ");
            }


            parm[3] = new ReportParameter("SETTLED", Convert.ToString(notsettled));


            if (travelfromdate == "")
            {
                parm[4] = new ReportParameter("TRAVEL_FROM_DATE", value: null);
            }
            else
            {
                DateTime dt1 = DateTime.ParseExact(travelfromdate.ToString(), "dd/MM/yyyy", null);
                parm[4] = new ReportParameter("TRAVEL_FROM_DATE", dt1.ToString());
            }


            if (traveltodate == "")
            {
                parm[5] = new ReportParameter("TRAVEL_TO_DATE", value: null);
            }
            else
            {
                DateTime dt1 = DateTime.ParseExact(traveltodate.ToString(), "dd/MM/yyyy", null);
                parm[5] = new ReportParameter("TRAVEL_TO_DATE", dt1.ToString());
            }

            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/ThailandReport/LedgerReportForAgent";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            //renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename = AgentWiseLedgerReport." + fileNameExtension);
            //Response.BinaryWrite(renderedBytes);
            //Response.End();
            //rptViewer1.Visible = false;
            //FileStream fs;
            //fs =  HttpContext.Current.Server.MapPath("~/Views/EXCELS/LedgerReports/" + suppliercompanyname.ToString() + "Quotation.xls");

            //using (fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/MIS/LedgerReports/" + "Report.pdf")))
            //{

            //    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
            //}
        }
    }
}