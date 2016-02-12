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
using System.Globalization;
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

namespace CRM.WebApp
{
    public partial class AgentRegistrationForm : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm objagentmaster = new CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet ds2 = objagentmaster.fetchTitle();
                DataSet ds3 = objagentmaster.fetchagenttype();
                DataSet ds4 = objagentmaster.fetchcountry();
                DataSet ds5 = objagentmaster.fetchcity();
                DataSet ds6 = objagentmaster.fetchSTATE();
                DataSet ds7 = objagentmaster.fetchaddresstype();
                DataSet ds8 = objagentmaster.fetchcommunicationmode();
                DataSet ds1 = objagentmaster.fetchdesignation();
                DataSet ds9 = objagentmaster.fetchpaymentterms();
                DataSet ds10 = objagentmaster.fetchstatus();
                binddropdownlist(drpTitle, ds2);
                binddropdownlist(drpAgentType, ds3);
                binddropdownlist(drpcountry, ds4);
                binddropdownlist(drpcity, ds5);
                binddropdownlist(drpstate, ds6);
                binddropdownlist(drpaddtype, ds7);
                binddropdownlist(drpmode, ds8);
                binddropdownlist(drpTerms, ds9);
                binddropdownlist(drpDesignation, ds1);
                binddropdownlist(drpuserstatus, ds10);
                drpuserstatus.SelectedValue = ds10.Tables[0].Rows[2]["AutoSearchResult"].ToString();
                drpuserstatus.Enabled = false;
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            //r.SelectedValue = "1";
        }

        protected void register_onclick(object sender, EventArgs e)
        {
           DataSet DS= objagentmaster.InsertUpdateCustnewregistraion("0", txtClientlastname.Text, drpTitle.Text, txtClientname.Text, CUST_REL_MOBILE.Text, CUST_REL_EMAIL.Text, drpAgentType.Text, drpmode.Text, CUST_REL_PHONE.Text, drpDesignation.Text, CUST_COMPANY_NAME.Text, PASSWORD.Text, drpTerms.Text, drpaddtype.Text, CUST_ADDRESS_LINE1.Text, CUST_ADDRESS_LINE2.Text, drpcity.Text, drpstate.Text, drpcountry.Text, CUST_PINCODE.Text, WEBSITE.Text, CHAIN_NAME.Text,drpuserstatus.Text);
          CUST_UNQ_ID.Text= DS.Tables[0].Rows[0]["UNIQUE_ID"].ToString();
          Updateconfirm.Update();
          sendmail(CUST_REL_EMAIL.Text);
          Master.DisplayMessage("Record save successfully.", "successMessage", 3000);
          
        }
        protected void sendmail(string email)
        {
            string body = "";
            body = "Dear Bhavinvora <br><br>Thank you for your Request for Git-Inquiry your requirements is - <br>your noofpax is - <br>Best Regards,<br>Travelz Unlimited";

            try
            {
                string smtpemail = "kushal@flamingotravels.co.in";
                string smtppass = "dadashri";
                string smtphost = "smtpcorp.com";
                //string smtpport = "587";
                string fromemail = "kushal@flamingotravels.co.in";
                // string toemail1 = Session["usersname"].ToString();
                string toemail2 = email;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                //message.To.Add(new MailAddress(toemail1.ToString()));
                message.To.Add(new MailAddress(toemail2.ToString()));
                message.Subject = "New Registration";
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