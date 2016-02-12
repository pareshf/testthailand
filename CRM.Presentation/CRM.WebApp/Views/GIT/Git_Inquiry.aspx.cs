using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.AdministratorEntity;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using CRM.DataAccess.FIT;
namespace CRM.WebApp.Views.GIT
{
    public partial class Git_Inquiry : System.Web.UI.Page
    {
        GitInquiry Objinquiry = new GitInquiry();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        string usrid;
        protected void Page_Load(object sender, EventArgs e)
        {
            usrid = Session["usersid"].ToString();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            Objinquiry.insert_Git_Inquiry(txtreq.Text, txtpax.Text, int.Parse(usrid));
            Master.DisplayMessage("Record Save Successfully", "successMessage", 3000);
            DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");
            if (dtemail.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dtemail.Rows.Count; i++)
                {
                    sendmail(txtreq.Text, txtpax.Text, dtemail.Rows[i]["USER_NAME"].ToString());
                }
            }
        }
        protected void sendmail(string a,string b,string c)
        {
            string body = "";
            body = "Dear Bhavinvora <br><br>Thank you for your Request for Git-Inquiry your requirements is -"+ a +"<br>your noofpax is -" + b + " <br>Best Regards,<br>Travelz Unlimited";

            try
            {
                string smtpemail = "kushal@flamingotravels.co.in";
                string smtppass = "dadashri";
                string smtphost = "smtpcorp.com";
                //string smtpport = "587";
                string fromemail = "kushal@flamingotravels.co.in";
                // string toemail1 = Session["usersname"].ToString();
                string toemail2 = c;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                //message.To.Add(new MailAddress(toemail1.ToString()));
                message.To.Add(new MailAddress(toemail2.ToString()));
                message.Subject = "GIT - Inquries - Reffernce No - ";
                //message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
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
}