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
using System.Net.Mail;
using System.Web.Mail;
using CRM.Core.Constants;
using System.Collections.Generic;
using CRM.WebApp.Views.dbmlfile;
using System.Linq;
using System.IO;
using System.Web.Services.Protocols;



namespace CRM.WebApp.Views.Administration
{

    public partial class ReportingMail : System.Web.UI.Page
    {
        #region VariableDeclaration
        IQueryable<VIEW_FOR_EMPLOYEE_EMAIL> email = new EmailDbmlDataContext().VIEW_FOR_EMPLOYEE_EMAILs.AsQueryable<VIEW_FOR_EMPLOYEE_EMAIL>();
        EmailDbmlDataContext edc = new EmailDbmlDataContext();
        DataTable dt = new DataTable();
        TextBox empname = new TextBox();
        TextBox empsurname = new TextBox();
        TextBox toemail = new TextBox();
        TextBox empusername = new TextBox();
        TextBox empdepartmentname = new TextBox();
        TextBox reportingto = new TextBox();
        TextBox fromemail = new TextBox();


        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnemail_Click(object sender, EventArgs e)
        {

            grdiemail.DataSource = edc.VIEW_FOR_EMPLOYEE_EMAILs;
            grdiemail.DataBind();



            for (int i = 0; i < grdiemail.Rows.Count; i++)
            {
                empname = (TextBox)grdiemail.Rows[i].FindControl("EMP_NAME");
                empsurname = (TextBox)grdiemail.Rows[i].FindControl("EMP_SURNAME");
                toemail = (TextBox)grdiemail.Rows[i].FindControl("TO_EMAIL");
                empusername = (TextBox)grdiemail.Rows[i].FindControl("USER_NAME");
                empdepartmentname = (TextBox)grdiemail.Rows[i].FindControl("DEPARTMENT_NAME"); ;
                reportingto = (TextBox)grdiemail.Rows[i].FindControl("REPORTING_TO"); ;
                fromemail = (TextBox)grdiemail.Rows[i].FindControl("FROM_EMAIL");
            }

            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("OWNER_NAME", empusername.Text);
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/FlamingoSSRSReports/rdlTicketInquiry";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();

            SendEmail(empname.Text, empsurname.Text, toemail.Text, empdepartmentname.Text, reportingto.Text, fromemail.Text);
            Response.End();


        }

        public void SendEmail(string empname, string empsurname, string toemail, string empdepartmentname, string reportingto, string fromemail)
        {

            Dictionary<string, string> smtpsettings = new Dictionary<string, string>();
            smtpsettings = WebManager.GetApplicationSetting("smtpsettings", true);
            SmtpClient smtpClient = new SmtpClient();

            string smtpusername = smtpsettings["smtpusername"];
            string smtppassword = smtpsettings["smtppassword"];
            string smtphost = smtpsettings["smtphost"];
            string smtpport = smtpsettings["smtpport"];
            DateTime todaysdate = DateTime.Today;



            string body = "";
            body = "Dear " + reportingto + "<br><br>Please check the attached Inquiry Status Report for" + empname + " " + empsurname + " for " + empdepartmentname + "as on Date" + todaysdate;

            try
            {


                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(toemail));

                message.Subject = "Inquiry Report For Employee - FLAMINGO TRANSWORLD PVT. LTD";
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                System.Net.NetworkCredential info = new System.Net.NetworkCredential(smtpusername, smtppassword);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);



            }
            catch (Exception ex)
            {

            }


        }

    }
}