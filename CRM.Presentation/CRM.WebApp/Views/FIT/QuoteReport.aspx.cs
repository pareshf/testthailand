#region SystemAssembly

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data;

#endregion

#region CRMAssembly

using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;

#endregion

namespace CRM.WebApp.Views.FIT
{
    public partial class QuoteReport : System.Web.UI.Page
    {
        #region GloabalDeclaration
        string quote_id;
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        string AgentName;
        string Tourname;

        #endregion

        #region GeneratePDFReport
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
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>10in</PageWidth>" +
            "  <PageHeight>9in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>0.50in</MarginLeft>" +
            "  <MarginRight>0.50in</MarginRight>" +
            "  <MarginBottom>0.50in</MarginBottom>" +
            "</DeviceInfo>";



            quote_id = Page.Request.QueryString["QuoteId"].ToString();

            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("QOUTE_ID", quote_id);
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/FlamingoSSRSReports/Invoice";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();


            //Render the report
            //Clear the response stream and write the bytes to the outputstream
            //Set content-disposition to "attachment" so that user is prompted to take an action
            //on the file (open or save)
            renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= Quotation." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            SendMail(renderedBytes);
            Response.End();



        }
        #endregion

        #region SendEmail
        protected void SendMail(byte[] _file1)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand comm = new SqlCommand("FETCH_AGENT_QUOTATION_DETAILS", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quote_id);

            SqlDataReader rdr = comm.ExecuteReader();
            dt.Load(rdr);

            AgentName = dt.Rows[0]["CUST_REL_NAME"].ToString();
            Tourname = dt.Rows[0]["TOUR_SHORT_NAME"].ToString();


            string body = "";
            body = "Dear " + AgentName + "<br><br>Thank you for your request for quote for the Tour - " + Tourname + "<br>Kindly check the attached quotation as per your requirement.<br><br>Best Regards,<br>Travelz Unlimited";

            try
            {
                string smtpemail = "kushal@flamingotravels.co.in";
                string smtppass = "dadashri";
                string smtphost = "smtpcorp.com";
                //string smtpport = "587";
                string fromemail = "sudhir@travelzunlimited.com";
                string toemail1 = Session["usersname"].ToString();
                //string toemail2 = "hardik@ambait.com";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(toemail1.ToString()));
                //message.To.Add(new MailAddress(toemail2.ToString()));
                message.Subject = "FIT - Quotation - Reffernce No - " + quote_id;
                message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
            }
            catch (Exception ex)
            {
                //lblResult.Text = "Could not serve your request at this time. Please contact webmaster";
                //lblResult.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

        #endregion
}