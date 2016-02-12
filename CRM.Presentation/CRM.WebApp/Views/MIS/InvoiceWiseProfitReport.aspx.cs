using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using System.Web.Configuration;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Data;


namespace CRM.WebApp.Views.MIS
{
    public partial class InvoiceWiseProfitReport : System.Web.UI.Page
    {

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 295);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

            string quoteno = "";
            string invoiceno = "";

            quoteno = txtQuoteRefNo.Text.ToString();
            invoiceno = txtInvoiceNo.Text.ToString();
            Response.Redirect("Grossprofitreport.aspx?key=" + invoiceno + "&key1=" + quoteno);
            //string mimeType;
            //string encoding;
            //string fileNameExtension;

            //Warning[] warnings;
            //string[] streams;
            //byte[] renderedBytes;

            //string deviceInfo =
            //"<DeviceInfo>" +
            //"  <OutputFormat>excel</OutputFormat>" +
            //"  <PageWidth>14in</PageWidth>" +
            //"  <PageHeight>8.5in</PageHeight>" +
            //"  <MarginTop>0.50in</MarginTop>" +
            //"  <MarginLeft>0.50in</MarginLeft>" +
            //"  <MarginRight>0.50in</MarginRight>" +
            //"  <MarginBottom>0.50in</MarginBottom>" +
            //"</DeviceInfo>";

            
            //ReportParameter[] parm = new ReportParameter[2];
            //if (txtInvoiceNo.Text != "")
            //{
            //    parm[0] = new ReportParameter("INVOICE_NO", txtInvoiceNo.Text.ToString());
            //}
            //else
            //{
            //    parm[0] = new ReportParameter("INVOICE_NO", " ");
            //}
            //if (txtQuoteRefNo.Text != "")
            //{
            //    parm[1] = new ReportParameter("QUOTE_REF_NO",txtQuoteRefNo.Text.ToString());
            //}
            //else
            //{
            //    parm[1] = new ReportParameter("QUOTE_REF_NO", "0");
            //}
            ////rptViewer1.ShowCredentialPrompts = false;
            ////rptViewer1.ShowParameterPrompts = false;

            ////rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            ////rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ////rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            ////rptViewer1.ServerReport.ReportPath = "/ThailandReport/GrossProfitInvoiceWise";
            ////rptViewer1.ServerReport.SetParameters(parm);
            ////rptViewer1.ServerReport.Refresh();
            ////rptViewer1.AsyncRendering = false;
            
            ////Response.End();

            ////rptViewer1.ShowCredentialPrompts = false;
            ////rptViewer1.ShowParameterPrompts = false;

            ////rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            ////rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ////rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            ////rptViewer1.ServerReport.ReportPath = "/ThailandReport/GrossProfitInvoiceWise";
            ////rptViewer1.ServerReport.SetParameters(parm);
            ////rptViewer1.ServerReport.Refresh();

            

            //string reportType = "excel";
            //string mimeType;
            //string encoding;
            //string fileNameExtension;

            //Warning[] warnings;
            //string[] streams;
            //byte[] renderedBytes;

            //string deviceInfo =
            //"<DeviceInfo>" +
            //"  <OutputFormat>excel</OutputFormat>" +
            //"  <PageWidth>14in</PageWidth>" +
            //"  <PageHeight>8.5in</PageHeight>" +
            //"  <MarginTop>0.50in</MarginTop>" +
            //"  <MarginLeft>0.50in</MarginLeft>" +
            //"  <MarginRight>0.50in</MarginRight>" +
            //"  <MarginBottom>0.50in</MarginBottom>" +
            //"</DeviceInfo>";

            //rptViewer1.ShowCredentialPrompts = true;
            //rptViewer1.ShowParameterPrompts = true;

            //rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            //rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            //rptViewer1.ServerReport.ReportPath = "/ThailandReport/GrossProfitInvoiceWise";
            //rptViewer1.ServerReport.SetParameters(parm);
            //rptViewer1.ServerReport.Refresh();

            //renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename = International Booking Form." + fileNameExtension);
            //Response.BinaryWrite(renderedBytes);
            //Response.End();

        }
    }
}