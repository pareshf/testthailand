using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

namespace CRM.WebApp.Views.FIT
{
    public partial class ApprovedAgent : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm objagentmaster = new CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        string CUST_ID = "0";
        string passord = "";
        string email1 = "";
        string agentname = "";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objagentmaster.fetchALLDATA();
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                   
                }
               
                
            }
        }
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //   r.SelectedValue = "0";
        }

        protected void chkBankCharge_CheckChnaged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chkBankCharge = (CheckBox)item.FindControl("chkBankCharge");

                if (chkBankCharge == sender)
                {
                    if (chkBankCharge.Checked == true)
                    {
                        TextBox txtCharge = (TextBox)item.FindControl("txtCharge");
                        txtCharge.Enabled = true;
                    }
                    else
                    {
                        TextBox txtCharge = (TextBox)item.FindControl("txtCharge");
                        txtCharge.Text = "";
                        txtCharge.Enabled = false;
                    }
                }
                else
                {
                    TextBox txtCharge = (TextBox)item.FindControl("txtCharge");
                    txtCharge.Enabled = false;
                }
            }
            Updateconfirm.Update();
        }
       

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string celltext = e.Row.Cells[1].Text;    // give the another cell index
                DropDownList ddl = (DropDownList)e.Row.FindControl("drptype");
                DropDownList ddl2 = (DropDownList)e.Row.FindControl("drpbank");
                //DropDownList ddl3 = (DropDownList)e.Row.FindControl("drpbranch");


                DataSet dsval = objagentmaster.fetchagenttype();
                DataSet dsval2 = objagentmaster.fetCompanyBank();
               
                binddropdownlist(ddl, dsval);
                binddropdownlist(ddl2, dsval2);
               
                // use celltext from another cell to generate the data source to bind ddl
            }
        }
       
        protected void Drp_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            DropDownList ddl1 = (DropDownList)sender;
            int repeaterItemIndex = ((GridViewRow)ddl1.NamingContainer).DataItemIndex;

            foreach (GridViewRow item in GV_Result.Rows)
            {
                if (item.DataItemIndex == repeaterItemIndex)
                {
                    DropDownList ddl2 = (DropDownList)item.FindControl("drpbank");
                    DataSet dsval2 = objagentmaster.fetchBankBranch(ddl2.Text);


                    DropDownList ddl3 = (DropDownList)item.FindControl("drpbranch");
                    binddropdownlist(ddl3, dsval2);

                    
                }
            }
            Updateconfirm.Update();
            
        }
   
        protected void DrpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl2 = (DropDownList)sender;
            int repeaterItemIndex = ((GridViewRow)ddl2.NamingContainer).DataItemIndex;

            foreach (GridViewRow item in GV_Result.Rows)
            {
                if (item.DataItemIndex == repeaterItemIndex)
                {
                    DropDownList ddl3 = (DropDownList)item.FindControl("drpbranch");
                    DataSet dsval3 = objagentmaster.fetchAccountName(ddl3.Text);


                    DropDownList ddl4 = (DropDownList)item.FindControl("drpaccountname");
                    binddropdownlist(ddl4, dsval3);


                }
            }
            Updateconfirm.Update();

        }

        protected void sendmail(string email, string pwd)
        {


            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[13]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");
            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();




            if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                string fromemail = "";
                if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        fromemail = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Supplier")
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string toemail1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        toemail1 = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        toemail1 = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string cc = "";
                if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                    if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        cc = "reservation@travelzunlimited.com";
                    }

                  
                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }


                }

                string bcc1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc1 = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        bcc1 = "reservation@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }

                    else
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string body = "";

                //    body = "Dear " + agentname +"," + "<br><br>Your Username is : " + email + "<br><br>Your Password is : " + pwd + "<br><br>Your Payment Terms is Cash on Arrival or Credit Card.<br>If we find only inquiries in or a/c in comparison to the production generated We might discontinue your registration.<br><br>Looking towords your support For mutual growth<br><br>Best Regards,<br>Mr. Sudhir Punjabi<br>Director<br>Travelz Unlimited<br>A Division of 'Jaiveer Group Co. Ltd.'<br>82/50 Supalai View<br>Soi City Home,Rattanathibet<br>Amphur Muang Nonthaburi,Thailand - 11000<br>HandPhone:(0066)817267538<br>HandPhone:(0066)819322461<br>Phone: 025271839/46/76/86<br>Fax:025271894 ";
                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                //  body = FormatEmail(strEmailTemplate, Tourname, AgentName);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", agentname);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%USERNAME%&gt;", email);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%PASSWORD%&gt;", pwd);


                body = strEmailTemplate;
                try
                {
                    MailMessage message = new MailMessage();
                    //   message.From = new MailAddress(cc);
                    //  message.To.Add(new MailAddress(dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString()));
                    message.From = new MailAddress(fromemail);
                    // message.To.Add(agentEmail.ToString());
                    message.To.Add(new MailAddress(toemail1));
                    if (cc != "")
                    {
                        message.CC.Add(new MailAddress(cc));
                    }

                    if (bcc1 != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc1));
                    }

                    string subjct = "";
                    subjct = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                    //  message.Subject = "Booking for : " + clientname + " Check in date : " + fromdate + " Check out date : " + todate;
                    //subjct = subjct.Replace("<%CLIENTNAME%>", pessanger_name);
                    //subjct = subjct.Replace("<%CHECKINDATE%>", check_in_date);
                    //subjct = subjct.Replace("<%CHECKOUTDATE%>", check_out_date);

                    message.Subject = subjct;

                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    client.Credentials = info;
                    client.Host = smtphost;
                    client.Port = int.Parse(smtpport);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
                //try
                //{
                //    string smtpemail = "kushal@flamingotravels.co.in";
                //    string smtppass = "dadashri";
                //    string smtphost = "smtpcorp.com";

                //    string fromemail = "sudhir@travelzunlimited.com";

                //    string toemail2 = email;

                //    MailMessage message = new MailMessage();
                //    message.From = new MailAddress(fromemail);

                //    message.To.Add(new MailAddress(toemail2.ToString()));
                //    message.Subject = "New Registration";

                //    message.Body = body;
                //    message.IsBodyHtml = true;

                //    SmtpClient client = new SmtpClient();
                //    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                //    client.Credentials = info;
                //    client.Host = smtphost;
                //    client.Port = 587;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(message);
                //}
                //catch (Exception ex)
                //{

                //}
            }
        }

        protected void sendmailcredit(string email, string pwd, string creditlimit)
        {

            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[13]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");
            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();




            if (ds_mailTemplate1.Tables[0].Rows[1]["IS_ON"].ToString() != "False")
            {
                string fromemail = "";
                if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        fromemail = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Supplier")
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string toemail1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        toemail1 = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        toemail1 = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string cc = "";
                if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());
                    if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        cc = "reservation@travelzunlimited.com";
                    }


                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }


                }

                string bcc1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc1 = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        bcc1 = "reservation@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }

                    else
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string body = "";

                //     body = "Dear " + agentname + "," +"<br><br>Your Username is : " + email + "<br><br>Your Password is : " + pwd + "<br><br>Your Approved Credit limit for FIT/GIT is - " + creditlimit + " USD <br><br>Your Payment terms is settled every 15 days<br>If we find only inquiries in or a/c in comparison to the production generated We might discontinue your registration.<br><br>Best Regards,<br>Mr. Sudhir Punjabi<br>Director<br>Travelz Unlimited<br>A Division of 'Jaiveer Group Co. Ltd.'<br>82/50 Supalai View<br>Soi City Home,Rattanathibet<br>Amphur Muang Nonthaburi,Thailand - 11000<br>HandPhone:(0066)817267538<br>HandPhone:(0066)819322461<br>Phone: 025271839/46/76/86<br>Fax:025271894 ";

                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();

                strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", agentname);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%USERNAME%&gt;", email);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%PASSWORD%&gt;", pwd);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%CREDITLIMIT%&gt;", creditlimit);

                body = strEmailTemplate;
                try
                {
                    MailMessage message = new MailMessage();
                    //   message.From = new MailAddress(cc);
                    //  message.To.Add(new MailAddress(dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString()));
                    message.From = new MailAddress(fromemail);
                    // message.To.Add(agentEmail.ToString());
                    message.To.Add(new MailAddress(toemail1));
                    if (cc != "")
                    {
                        message.CC.Add(new MailAddress(cc));
                    }

                    if (bcc1 != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc1));
                    }

                    string subjct = "";
                    subjct = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_SUBJECT"].ToString();

                    //  message.Subject = "Booking for : " + clientname + " Check in date : " + fromdate + " Check out date : " + todate;
                    //subjct = subjct.Replace("<%CLIENTNAME%>", pessanger_name);
                    //subjct = subjct.Replace("<%CHECKINDATE%>", check_in_date);
                    //subjct = subjct.Replace("<%CHECKOUTDATE%>", check_out_date);

                    message.Subject = subjct;

                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    client.Credentials = info;
                    client.Host = smtphost;
                    client.Port = int.Parse(smtpport);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);
                }

                catch (Exception ex)
                {

                }


                //try
                //{
                //    string smtpemail = "kushal@flamingotravels.co.in";
                //    string smtppass = "dadashri";
                //    string smtphost = "smtpcorp.com";
                //    string fromemail = "sudhir@travelzunlimited.com";

                //    string toemail2 = email;

                //    MailMessage message = new MailMessage();
                //    message.From = new MailAddress(fromemail);

                //    message.To.Add(new MailAddress(toemail2.ToString()));
                //    message.Subject = "New Registration";
                //    //message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
                //    message.Body = body;
                //    message.IsBodyHtml = true;

                //    SmtpClient client = new SmtpClient();
                //    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                //    client.Credentials = info;
                //    client.Host = smtphost;
                //    client.Port = 587;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(message);
                //}
            }
        }

        protected void sendmail1(string email)
        {
            

            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[13]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");
            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();




            if (ds_mailTemplate1.Tables[0].Rows[2]["IS_ON"].ToString() != "False")
            {
                string fromemail = "";
                if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        fromemail = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[2]["FROM_ROLE_NAME"].ToString() == "Supplier")
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string toemail1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Agent")
                    {
                        toemail1 = email;
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        toemail1 = "reservation@travelzunlimited.com";
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[2]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                    else
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string cc = "";
                if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString());
                    if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[2]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        cc = "reservation@travelzunlimited.com";
                    }


                    else
                    {
                        cc = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }


                }

                string bcc1 = "";
                if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc1 = email;
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        bcc1 = "reservation@travelzunlimited.com";
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[2]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }

                    else
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                string body = "";

               
              //  body = "Dear " + agentname + "," + "<br><br>Thank you for your Request for Agent.<br>Your account registration was rejected.<br>Best Regards,<br><br>Mr. Sudhir Punjabi<br><br>Director<br><br>Travelz Unlimited<br><br>A Division of 'Jaiveer Group Co. Ltd.'<br><br>82/50 Supalai View<br><br>Soi City Home,Rattanathibet<br><br>Amphur Muang Nonthaburi,Thailand - 11000<br><br>HandPhone:(0066)817267538<br><br>HandPhone:(0066)819322461<br><br>Phone: 025271839/46/76/86<br><br>Fax:025271894 ";

                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_CONTENT"].ToString();

                strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", agentname);
                //strEmailTemplate = strEmailTemplate.Replace("&lt;%USERNAME%&gt;", email);
                //strEmailTemplate = strEmailTemplate.Replace("&lt;%PASSWORD%&gt;", pwd);
                //strEmailTemplate = strEmailTemplate.Replace("&lt;%CREDITLIMIT%&gt;", creditlimit);

                body = strEmailTemplate;
                try
                {
                    MailMessage message = new MailMessage();
                    //   message.From = new MailAddress(cc);
                    //  message.To.Add(new MailAddress(dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString()));
                    message.From = new MailAddress(fromemail);
                    // message.To.Add(agentEmail.ToString());
                    message.To.Add(new MailAddress(toemail1));
                    if (cc != "")
                    {
                        message.CC.Add(new MailAddress(cc));
                    }

                    if (bcc1 != "")
                    {
                        message.Bcc.Add(new MailAddress(bcc1));
                    }

                    string subjct = "";
                    subjct = ds_mailTemplate1.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();

                    //  message.Subject = "Booking for : " + clientname + " Check in date : " + fromdate + " Check out date : " + todate;
                    //subjct = subjct.Replace("<%CLIENTNAME%>", pessanger_name);
                    //subjct = subjct.Replace("<%CHECKINDATE%>", check_in_date);
                    //subjct = subjct.Replace("<%CHECKOUTDATE%>", check_out_date);

                    message.Subject = subjct;

                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    client.Credentials = info;
                    client.Host = smtphost;
                    client.Port = int.Parse(smtpport);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(message);
                }

                catch (Exception ex)
                {

                }

                //     try
                //{
                //    string smtpemail = "kushal@flamingotravels.co.in";
                //    string smtppass = "dadashri";
                //    string smtphost = "smtpcorp.com";
                //    //string smtpport = "587";
                //    string fromemail = "sudhir@travelzunlimited.com";
                //    // string toemail1 = Session["usersname"].ToString();
                //    string toemail2 = email;

                //    MailMessage message = new MailMessage();
                //    message.From = new MailAddress(fromemail);
                //    //message.To.Add(new MailAddress(toemail1.ToString()));
                //    message.To.Add(new MailAddress(toemail2.ToString()));
                //    message.Subject = "Reject Registration";
                //    //message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
                //    message.Body = body;
                //    message.IsBodyHtml = true;

                //    SmtpClient client = new SmtpClient();
                //    NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                //    client.Credentials = info;
                //    client.Host = smtphost;
                //    client.Port = 587;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(message);
                //}
            }
        }

        protected void GV_Result_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approved")
            {
                int newindex = Convert.ToInt32(e.CommandArgument);
                CUST_ID = GV_Result.Rows[newindex].Cells[2].Text;
                agentname = GV_Result.Rows[newindex].Cells[3].Text;

                string email = GV_Result.Rows[newindex].Cells[6].Text;
                DataSet ds = objagentmaster.fetchSINGLUSERDATA(CUST_ID);
                TextBox txt = (TextBox)GV_Result.Rows[newindex].FindControl("txtcredit");
                string creditlimit = txt.Text;
                DropDownList ddl = (DropDownList)GV_Result.Rows[newindex].FindControl("drptype");

                DropDownList ddlbank = (DropDownList)GV_Result.Rows[newindex].FindControl("drpbank");
                DropDownList ddlbranch = (DropDownList)GV_Result.Rows[newindex].FindControl("drpbranch");
                DropDownList ddlaccname = (DropDownList)GV_Result.Rows[newindex].FindControl("drpaccountname");
                TextBox txtcharge = (TextBox)GV_Result.Rows[newindex].FindControl("txtCharge");
                CheckBox chkBankCharge = (CheckBox)GV_Result.Rows[newindex].FindControl("chkBankCharge");
                //if (ddlbank.Text != "" && ddlbank.Text != "" && ddlaccname.Text != "")
                //{

                //}
                //else
                //{
                //    Master.DisplayMessage("Agent Approved Successfully", "successMessage", 5000);
                //}
                if (ddl.Text != "" && ddlbank.Text != "" && ddlbank.Text != "" && ddlaccname.Text != "")
                {
                    DataSet ds3 = objagentmaster.UpdateBankAgnetMaster(ddlbank.Text, ddlbranch.Text, ddlaccname.Text, CUST_ID);
                    DataSet ds1 = objagentmaster.InsertUpdatesysusermaster(email, ds.Tables[0].Rows[0]["PASSWORD"].ToString(), ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString());
                    if(txtcharge.Text != "")
                    {
                        objagentmaster.InsertUpdateBankCharge(chkBankCharge.Checked,Convert.ToDecimal(txtcharge.Text), CUST_ID);
                    }
                    else
                    {
                        objagentmaster.InsertUpdateBankCharge(chkBankCharge.Checked,0, CUST_ID);
                    }
                    
                    passord = ds1.Tables[0].Rows[0]["PASSWORD"].ToString();
                    email1 = ds1.Tables[0].Rows[0]["USER_NAME"].ToString();
                    if (creditlimit == "")
                    {
                        //sendmail(email1, passord);
                        DataSet ds2 = objagentmaster.UpdateCreditlimitincust(ddl.Text, creditlimit, CUST_ID);
                        Master.DisplayMessage("Agent Approved Successfully", "successMessage", 5000);
                    }
                    else
                    {
                        DataSet ds2 = objagentmaster.UpdateCreditlimitincust(ddl.Text, creditlimit, CUST_ID);
                        string credit = ds2.Tables[0].Rows[0]["CREDIT_LIMIT"].ToString();
                        sendmailcredit(email1, passord, creditlimit);
                        Master.DisplayMessage("Agent Approved Successfully", "successMessage", 5000);
                    }



                    DataSet dsdta = objagentmaster.fetchALLDATA();
                    GV_Result.DataSource = dsdta.Tables[0];
                    GV_Result.DataBind();
                }
                else
                {
                    CUST_ID = GV_Result.Rows[newindex].Cells[2].Text;
                    Master.DisplayMessage("Please Select Agent Type.", "successMessage", 5000);
                    Updateconfirm.Update();
                }
            }
            else
            {
                int newindex = Convert.ToInt32(e.CommandArgument);
                // string email = GV_Result.Rows[newindex].Cells[6].Text;
                //sendmail1(email);
                CUST_ID = GV_Result.Rows[newindex].Cells[2].Text;
                objagentmaster.DISAPPROVED(CUST_ID);
                Master.DisplayMessage("Agent DisApproved Successfully", "successMessage", 5000);
                Updateconfirm.Update();
                DataSet dsdta = objagentmaster.fetchALLDATA();
                GV_Result.DataSource = dsdta.Tables[0];
                GV_Result.DataBind();
            }
            Updateconfirm.Update();
        }
    }
}