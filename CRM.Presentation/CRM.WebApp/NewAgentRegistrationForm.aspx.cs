using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
using CRM.DataAccess.FIT;
namespace CRM.WebApp
{
    public partial class NewAgentRegistrationForm : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm objagentmaster = new CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
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
                //DataSet ds11 = objagentmaster.SECURITY_QUESTION();
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
                //binddropdownlist(drpsecurity, ds11);
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

            if (Session["CAPTCHA"].ToString().Equals(txtword.Text.Trim()))
            {
                DataSet DSUSER = objagentmaster.fetchALLUSERNAME();

                bool flag = false;
                for (int i = 0; i < DSUSER.Tables[0].Rows.Count; i++)
                {
                    if (CUST_REL_EMAIL.Text != "")
                    {
                        if (CUST_REL_EMAIL.Text == DSUSER.Tables[0].Rows[i]["USER_NAME"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag == false)
                {
                    bool offline = false;
                    string CustEmail;
                    string Password;
                    if (chkoffline.Checked == true)
                    {
                        offline = true;
                        CustEmail = "support@travelzunlimited.com";
                        Password = "support@123";
                    }
                    else
                    {
                        offline = false;
                        CustEmail = CUST_REL_EMAIL.Text;
                        Password = PASSWORD.Text;
                    }

                    DataSet DS = objagentmaster.InsertUpdateCustnewregistraion("0", txtClientlastname.Text, drpTitle.Text, txtClientname.Text, CUST_REL_MOBILE.Text, CustEmail, drpAgentType.Text, drpmode.Text, CUST_REL_PHONE.Text, drpDesignation.Text, CUST_COMPANY_NAME.Text, Password, drpTerms.Text, "Office", CUST_ADDRESS_LINE1.Text, CUST_ADDRESS_LINE2.Text, drpcity.Text, drpstate.Text, drpcountry.Text, CUST_PINCODE.Text, WEBSITE.Text, CHAIN_NAME.Text, drpuserstatus.Text, offline);
                    CUST_UNQ_ID.Text = DS.Tables[0].Rows[0]["UNIQUE_ID"].ToString();
                    lbl1.Text = "";
                    if (chkoffline.Checked != true)
                    {
                        sendmail(CUST_REL_EMAIL.Text);
                    }
                    DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");
                    if (dtemail.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int i = 0; i < dtemail.Rows.Count; i++)
                        {
                            // string body = "";
                            //  body = "Dear Admin, <br><br>We have received Agent registration.<br><br>Please review the Agent details.<br><br>After verification, Kindly Approve or Disapprove Agent Registration.<br><br><br>Best Regards,<br><br>Travelz Unlimited.";


                            try
                            {
                                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[12]["AutoSearchResult"].ToString());




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

                                            if (chkoffline.Checked == true)
                                            {
                                                fromemail = "support@travelzunlimited.com";
                                            }
                                            else
                                            {
                                                fromemail = CUST_REL_EMAIL.Text;
                                            }


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
                                            if (chkoffline.Checked == true)
                                            {
                                                toemail1 = "support@travelzunlimited.com";
                                            }
                                            else
                                            {
                                                toemail1 = CUST_REL_EMAIL.Text;
                                            }
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
                                            toemail1 = dtemail.Rows[i]["USER_NAME"].ToString();
                                        }
                                    }

                                    string cc = "";
                                    if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
                                    {
                                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());
                                        if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
                                        {
                                            cc = CUST_REL_EMAIL.Text;
                                        }

                                        else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
                                        {
                                            cc = "reservation@travelzunlimited.com";
                                        }

                                        //else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                                        //{
                                        //    cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                                        //}

                                        else
                                        {
                                            cc = dtemail.Rows[i]["USER_NAME"].ToString();
                                        }


                                    }

                                    string bcc1 = "";
                                    if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
                                    {
                                        DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                                        if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
                                        {
                                            bcc1 = CUST_REL_EMAIL.Text;
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
                                            bcc1 = dtemail.Rows[i]["USER_NAME"].ToString();
                                        }
                                    }

                                    string body = "";
                                    //   body = "Dear " + txtClientname.Text + "," + "<br><br>We have received your request for registration.<br><br>We shall review your detail.<br><br>After verification, your account shall be activated.<br><br>Verification mail will be sent you shortly. <br><br><br><br>Best Regards,<br><br>Mr. Sudhir Punjabi<br>Director<br>Travelz Unlimited<br>A Division of 'Jaiveer Group Co. Ltd.'<br>82/50 Supalai View<br>Soi City Home,Rattanathibet<br>Amphur Muang Nonthaburi,Thailand - 11000<br>HandPhone:(0066)817267538<br>HandPhone:(0066)819322461<br>Phone: 025271839/46/76/86<br>Fax:025271894";

                                    //  body = "Dear Reservation,<br><br> Please Confirm as Below <br><br>" + hotel_name + "<br><br> Our Reference Number:" + EmailQoute_id + "<br>Name of the passenger :" + pessanger_name + "<br>Room Type:" + room_type + "<br>No of Single Rooms:" + single_rooms + "<br>No of Double Rooms:" + double_rooms + "<br>No of Triple Rooms:" + tripal_rooms + "<br>Check In Date : " + check_in_date + "<br>Check out Date :" + check_out_date + "<br><br>Please advise the Cut of Date : <br><br>Please advice the Last Payment Date : <br><br>Please send Confirmation No : <br><br> Best Reagrds <br>Travelz Unlmited";
                                    string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();
                                    //  body = FormatEmail(strEmailTemplate, Tourname, AgentName);
                                    // strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", txtClientname.Text);


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
                                    //    string toemail2 = dtemail.Rows[i]["USER_NAME"].ToString();

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
                                    //    //lblResult.Text = "Could not serve your request at this time. Please contact webmaster";
                                    //    //lblResult.ForeColor = System.Drawing.Color.Red;
                                    //}
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            //  Master.DisplayMessage("Record save successfully.", "successMessage", 3000);
                            //  Response.Write("<script>alert('Your Registration Save Successfully.')</script>");
                            //Response.Write("<script>alert('Your registration is submitted successfully. We will E-mail you with updates very soon.')<script>"); 

                        }
                        Response.Redirect("~/ThankYou.aspx");
                    }
                }
                else
                {
                    lblerror.Visible = true;
                }
                Updateconfirm.Update();
            }
            else
            {
                lbl1.Text = "Invalid Captcha";
            }
        }

        protected void sendmail(string email)
        {


            try
            {
                DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

                DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[12]["AutoSearchResult"].ToString());

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

                        //else if (ds_mailTemplate.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                        //{
                        //    cc = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();
                        //}

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
                    //   body = "Dear " + txtClientname.Text + "," + "<br><br>We have received your request for registration.<br><br>We shall review your detail.<br><br>After verification, your account shall be activated.<br><br>Verification mail will be sent you shortly. <br><br><br><br>Best Regards,<br><br>Mr. Sudhir Punjabi<br>Director<br>Travelz Unlimited<br>A Division of 'Jaiveer Group Co. Ltd.'<br>82/50 Supalai View<br>Soi City Home,Rattanathibet<br>Amphur Muang Nonthaburi,Thailand - 11000<br>HandPhone:(0066)817267538<br>HandPhone:(0066)819322461<br>Phone: 025271839/46/76/86<br>Fax:025271894";

                    //  body = "Dear Reservation,<br><br> Please Confirm as Below <br><br>" + hotel_name + "<br><br> Our Reference Number:" + EmailQoute_id + "<br>Name of the passenger :" + pessanger_name + "<br>Room Type:" + room_type + "<br>No of Single Rooms:" + single_rooms + "<br>No of Double Rooms:" + double_rooms + "<br>No of Triple Rooms:" + tripal_rooms + "<br>Check In Date : " + check_in_date + "<br>Check out Date :" + check_out_date + "<br><br>Please advise the Cut of Date : <br><br>Please advice the Last Payment Date : <br><br>Please send Confirmation No : <br><br> Best Reagrds <br>Travelz Unlmited";
                    string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                    //  body = FormatEmail(strEmailTemplate, Tourname, AgentName);
                    strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENTNAME%&gt;", txtClientname.Text);


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
                    //  string fromemail = "sudhir@travelzunlimited.com";
                    // string toemail1 = Session["usersname"].ToString();
                    //   string toemail2 = email;

                    //     MailMessage message = new MailMessage();
                    //     message.From = new MailAddress(fromemail);
                    //message.To.Add(new MailAddress(toemail1.ToString()));
                    //       message.To.Add(new MailAddress(toemail2.ToString()));
                    //        message.Subject = "New Registration";
                    //message.Attachments.Add(new Attachment(new MemoryStream(_file1), Tourname + ".pdf"));
                    //message.Body = body;
                    //message.IsBodyHtml = true;

                    //SmtpClient client = new SmtpClient();
                    //NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                    //client.Credentials = info;
                    //client.Host = smtphost;
                    //client.Port = 587;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.Send(message);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void drpcity_selectedindexchanged(object sender, EventArgs e)
        {
            DataSet ds6 = objagentmaster.fetchSTATEFROMCITY(drpcity.Text);

            if (ds6.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                drpstate.SelectedValue = ds6.Tables[0].Rows[0]["STATE_NAME"].ToString();
                DataSet ds7 = objagentmaster.fetchCONTRYFROMSTATE(ds6.Tables[0].Rows[0]["STATE_NAME"].ToString());
                drpcountry.SelectedValue = ds7.Tables[0].Rows[0]["COUNTRY_NAME"].ToString();
                CUST_PINCODE.Focus();
            }

            string Password = PASSWORD.Text;
            PASSWORD.Attributes.Add("value", Password);


            Updateconfirm.Update();

        }

        protected void chkoffline_CheckedChanged(object sender, EventArgs e)
        {
            if (chkoffline.Checked == true)
            {
                reqEmail.ValidationGroup = "";
                reqPassword.ValidationGroup = "";
                spanemail.InnerText = "";
                spanpass.InnerText = "";

            }
            else
            {
                reqEmail.ValidationGroup = "valid";
                reqPassword.ValidationGroup = "valid";
                spanemail.InnerText = "*";
                spanpass.InnerText = "*";
            }
        }


    }
}