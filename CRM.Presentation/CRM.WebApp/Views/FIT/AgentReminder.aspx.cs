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
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.FIT
{
    public partial class AgentReminder : System.Web.UI.Page
    {
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        bool flag = false;
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 252);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_NEXT2_DAYS_AGENT_REMINDER", usrid);
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

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_NEXT2_DAYS_AGENT_REMINDER", Session["usersid"].ToString());
            if (ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {


                GV_Result.DataSource = ds.Tables[0];
                GV_Result.DataBind();

            }
            Updateconfirm.Update();
        }

        protected void btnSelect_onclick(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                chk.Checked = true;

            }
            Updateconfirm.Update();
        }

        protected void btnReminder_onclick(object sender, EventArgs e)
        {
            validation();
            if (flag == false)
            {
                Master.DisplayMessage("Select records for reminder", "successMessage", 5000);
            }
            else
            {
                foreach (GridViewRow item in GV_Result.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");

                    if (chk.Checked)
                    {
                        string quoteid = GV_Result.Rows[item.DataItemIndex].Cells[1].Text;
                        SendMail(quoteid);
                    }
                }
                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_NEXT2_DAYS_AGENT_REMINDER", Session["usersid"].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                }
                else
                {


                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();

                }
                Master.DisplayMessage("Reminder Mail sent to an agent", "successMessage", 5000);
            }
            Updateconfirm.Update();
        }

        protected void SendMail(string quoteId)
        {
            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[19]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            //string EmailQoute_id = Session["updateid"].ToString();
         
            DataTable dt11 = new DataTable();
            if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
            {
                DataTable dtemail = objHotelStoreProcedure.fetch_backoffice_for_book(quoteId);
                string bcc;
                if (dtemail.Rows.Count != 0)
                {
                    if (dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString() == "1")
                    {
                        
                        bcc = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();

                    }
                    else
                    {
                        
                        bcc = dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString();

                    }
                }
                else
                {
                    
                    bcc = "1";

                }


                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();

                SqlCommand comm1 = new SqlCommand("FETCH_AGENT_QUOTATION_DETAILS", conn1);
                comm1.CommandType = CommandType.StoredProcedure;

                comm1.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = Convert.ToInt32(quoteId);

                SqlDataReader rdr11 = comm1.ExecuteReader();
                dt11.Load(rdr11);

                //quote_user_id = dt1.Rows[0]["USER_ID"].ToString();

                string fromemail = "";
                if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        if (bcc == "1")
                        {
                            fromemail = "reservation@travelzunlimited.com";
                        }
                        else if (bcc == "2")
                        {
                            fromemail = "reservation1@travelzunlimited.com";
                        }
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                    {
                      //  fromemail = supplier_email;
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
                        toemail1 = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        if (bcc == "1")
                        {
                            toemail1 = "reservation@travelzunlimited.com";
                        }
                        else if (bcc == "2")
                        {
                            toemail1 = "reservation1@travelzunlimited.com";
                        }
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                       // toemail1 = supplier_email;
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
                        cc = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        if (bcc == "1")
                        {
                            cc = "reservation@travelzunlimited.com";
                        }
                        else if (bcc == "2")
                        {
                            cc = "reservation1@travelzunlimited.com";
                        }
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
                        bcc1 = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Backoffice")
                    {
                        if (bcc == "1")
                        {
                            bcc1 = "reservation@travelzunlimited.com";
                        }
                        else if (bcc == "2")
                        {
                            bcc1 = "reservation1@travelzunlimited.com";
                        }
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                      //  bcc = supplier_email;
                    }

                    else
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }
                string body = "";

             
                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
             
                strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENT_NAME%&gt;", dt11.Rows[0]["CUST_REL_NAME"].ToString());
                strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTE_ID%&gt;", quoteId);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%RECONFIRMATION_DATE%&gt;", dt11.Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString());

                body = strEmailTemplate;
                try
                {
                    MailMessage message = new MailMessage();
                 
                    message.From = new MailAddress(fromemail);
                   
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

                  
                    //subjct = subjct.Replace("<%QUOTE_ID%>", quoteId);

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



                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(quoteId), "", "", "", int.Parse(Session["usersid"].ToString()), 1);


                    objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(quoteId), "", "", "", int.Parse(Session["usersid"].ToString()), 2);

                }
                catch (Exception ex)
                {

                }

                conn1.Close();
            }
            objfitquote.Agent_Reminder_Entry(quoteId);
        }

        protected void validation()
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");

                if (chk.Checked)
                {
                    flag = true;
                    break;
                }
            }
        }
    }
}