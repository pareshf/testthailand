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
    public partial class CancelledBookings : System.Web.UI.Page
    {
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        #region VARIABLES
        bool flag = false;
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        string supplier_email;
        string supplier_emailupdate;
        string single_rooms;
        string double_rooms;
        string tripal_rooms;
        string room_type;
        string hotel_name;
        string check_in_date;
        string check_out_date;
        string single_rooms1;
        string double_rooms1;
        string tripal_rooms1;
        string room_type1;
        string hotel_name1;
        string check_in_date1;
        string check_out_date1;
        string pessanger_name;
        string pessanger_name1;
        string supplier_id;
        string hotelsupplier_id;
        string hotelsupplier_idupdate;
        string supplier_idupdate;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_FOR_CANCELLATION", usrid);
                if (ds.Tables[0].Rows.Count == 0)
                {

                }
                else
                {


                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();


                    //DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                    //binddropdownlist(DropDownList2, dsval);
                    //DropDownList2.SelectedValue = "To Be Reconfirmed";
                    //DropDownList2.Enabled = false;
                }
            }
        }


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

        protected void btnSelect_onclick(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GV_Result.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                chk.Checked = true;

            }
            Updateconfirm.Update();
        }

        protected void btnCancel_onclick(object sender, EventArgs e)
        {
            validation();
            if (flag == false)
            {
                Master.DisplayMessage("Select records for cancellation.", "successMessage", 5000);
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
            DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_FOR_CANCELLATION", Session["usersid"].ToString());
            if (ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {


                GV_Result.DataSource = ds.Tables[0];
                GV_Result.DataBind();

            }
            Master.DisplayMessage("Booking has been Canceled.", "successMessage", 5000);
            }
            Updateconfirm.Update();
        }

        protected void SendMail(string quoteId)
        {
            DataSet ds_eventName1 = objHotelStoreProcedure.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");

            DataSet ds_mailTemplate1 = objHotelStoreProcedure.get_email_templaet_data("GET_EMAIL_DESCRIPTION_TO_SEND_EMAIL", ds_eventName1.Tables[0].Rows[18]["AutoSearchResult"].ToString());

            DataSet ds_mailconfig = objHotelStoreProcedure.get_emailConfig("FETCH_EMAIL_CONFIGURATION");

            string smtpemail = ds_mailconfig.Tables[0].Rows[0]["SMTP_USERID"].ToString();
            string smtppass = ds_mailconfig.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
            string smtphost = ds_mailconfig.Tables[0].Rows[0]["SMTP_HOST"].ToString();
            string smtpport = ds_mailconfig.Tables[0].Rows[0]["SMTP_PORT"].ToString();

            //string EmailQoute_id = Session["updateid"].ToString();
            DataTable dt_supplier_id = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            DataTable dt_agent = new DataTable();

            SqlCommand agent_comm = new SqlCommand("AGENT_BOOKING_EMAIL", conn);
            agent_comm.CommandType = CommandType.StoredProcedure;
            agent_comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quoteId);
            SqlDataReader agent_rdr = agent_comm.ExecuteReader();
            dt_agent.Load(agent_rdr);

            SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quoteId);
            SqlDataReader rdr = comm.ExecuteReader();
            dt_supplier_id.Load(rdr);

            SqlCommand cmd = new SqlCommand("FETCH_BOOK_EMAIL_ID_ON_QUOTE_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quoteId);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Load(dr);


            DataTable dtemail = objHotelStoreProcedure.fetch_backoffice_for_book(quoteId);
            string bcc;
            if (dtemail.Rows.Count != 0)
            {
                if (dtemail.Rows[0]["BOOK_EMAIL_TO_BACKOFFICE"].ToString() == "1")
                {
                    //DataTable dt1 = objHotelStoreProcedure.update_quote_for_backoffice(int.Parse(Session["updateid"].ToString()), "2");
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


            String cc = "";

            int count = 0;

            for (int i_supp = 0; i_supp < dt_supplier_id.Rows.Count; i_supp++)
            {
                count = i_supp + 1;
                supplier_id = dt_supplier_id.Rows[i_supp]["SUPPLIER_ID"].ToString();
                DataTable dt_hotelEmails = new DataTable();

                SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                email.CommandType = CommandType.StoredProcedure;
                email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quoteId);
                SqlDataReader rdremail = email.ExecuteReader();
                dt_hotelEmails.Load(rdremail);

                if (dt_hotelEmails.Rows.Count != 0)
                {
                    hotelsupplier_id = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


                    if (hotelsupplier_id == supplier_id)
                    {
                        DataTable dt_hotelDetails = new DataTable();

                        SqlCommand details = new SqlCommand("FETCH_HOTEL_DETAILS_FOR_MAIL", conn);
                        details.CommandType = CommandType.StoredProcedure;
                        details.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(quoteId);
                        details.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int).Value = int.Parse(hotelsupplier_id);
                        SqlDataReader rdrdetails = details.ExecuteReader();

                        dt_hotelDetails.Load(rdrdetails);

                        single_rooms = dt_hotelDetails.Rows[0]["SINGLE_ROOMS"].ToString();
                        double_rooms = dt_hotelDetails.Rows[0]["DOUBLE_ROOMS"].ToString();
                        tripal_rooms = dt_hotelDetails.Rows[0]["TRIPAL_ROOMS"].ToString();
                        room_type = dt_hotelDetails.Rows[0]["ROOM_TYPE_NAME"].ToString();
                        hotel_name = dt_hotelDetails.Rows[0]["CHAIN_NAME"].ToString();
                        pessanger_name = dt_hotelDetails.Rows[0]["CLIENT_NAME"].ToString();
                        check_in_date = dt_hotelDetails.Rows[0]["CHECK_IN_DATE"].ToString();
                        check_out_date = dt_hotelDetails.Rows[0]["CHECK_OUT_DATE"].ToString();

                    }
                    if (ds_mailTemplate1.Tables[0].Rows[0]["IS_ON"].ToString() != "False")
                    {


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
                                fromemail = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
                            }
                            else if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Supplier")
                            {
                                fromemail = supplier_email;
                            }
                            else
                            {
                                fromemail = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        string toemail1 = "";
                        if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString() == "Agent")
                            {
                                toemail1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
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
                                toemail1 = supplier_email;
                            }
                            else
                            {
                                toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }

                        if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                        {
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString());
                            if (ds_mailTemplate1.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() == "Agent")
                            {
                                cc = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
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
                            DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                            if (ds_mailTemplate1.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() == "Agent")
                            {
                                bcc1 = dt_agent.Rows[0]["CUST_REL_EMAIL"].ToString();
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
                                bcc = supplier_email;
                            }

                            else
                            {
                                bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }
                        string body = "";

                        //  body = "Dear Reservation,<br><br> Please Confirm as Below <br><br>" + hotel_name + "<br><br> Our Reference Number:" + EmailQoute_id + "<br>Name of the passenger :" + pessanger_name + "<br>Room Type:" + room_type + "<br>No of Single Rooms:" + single_rooms + "<br>No of Double Rooms:" + double_rooms + "<br>No of Triple Rooms:" + tripal_rooms + "<br>Check In Date : " + check_in_date + "<br>Check out Date :" + check_out_date + "<br><br>Please advise the Cut of Date : <br><br>Please advice the Last Payment Date : <br><br>Please send Confirmation No : <br><br> Best Reagrds <br>Travelz Unlmited";
                        string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                        //  body = FormatEmail(strEmailTemplate, Tourname, AgentName);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%HOTEL_NAME%&gt;", hotel_name);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTE_ID%&gt;", quoteId);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%CLIENT_NAME%&gt;", pessanger_name);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%ROOM_TYPE%&gt;", room_type);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%SINGLE_ROOMS%&gt;", single_rooms);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%DOUBLE_ROOMS%&gt;", double_rooms);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%TRIPLE_ROOMS%&gt;", tripal_rooms);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%FROM_DATE%&gt;", check_in_date);
                        strEmailTemplate = strEmailTemplate.Replace("&lt;%TO_DATE%&gt;", check_out_date);

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
                            subjct = subjct.Replace("<%QUOTE_ID%>", quoteId);

                            message.Subject = subjct;
                            //     message.Subject = "Booking for : " + pessanger_name + " Check in date : " + check_in_date + " Check out date : " + check_out_date;
                            //      message.CC.Add(new MailAddress(cc.ToString()));
                            message.Body = body;
                            message.IsBodyHtml = true;

                            SmtpClient client = new SmtpClient();
                            NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                            client.Credentials = info;
                            client.Host = smtphost;
                            client.Port = int.Parse(smtpport);
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Send(message);

                            if (count == 1)
                            {
                                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(quoteId), "", "", "", int.Parse(Session["usersid"].ToString()), 1);
                            }
                            if (count != 0)
                            {
                                objHotelStoreProcedure.insert_email_trail(int.Parse(ds_mailTemplate1.Tables[0].Rows[0]["EMAIL_TEMPLATE_MASTER_ID"].ToString()), fromemail, toemail1, cc, bcc1, subjct, body, int.Parse(quoteId), "", "", "", int.Parse(Session["usersid"].ToString()), 2);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            objHotelStoreProcedure.change_quote_status(int.Parse(quoteId));
            DataTable dt11 = new DataTable();
            if (ds_mailTemplate1.Tables[0].Rows[1]["IS_ON"].ToString() != "False")
            {

                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();

                SqlCommand comm1 = new SqlCommand("FETCH_AGENT_QUOTATION_DETAILS", conn);
                comm1.CommandType = CommandType.StoredProcedure;

                comm1.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = Convert.ToInt32(quoteId);

                SqlDataReader rdr11 = comm1.ExecuteReader();
                dt11.Load(rdr11);

                //quote_user_id = dt1.Rows[0]["USER_ID"].ToString();

                string fromemail = "";
                if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Backoffice")
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
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["FROM_ROLE_NAME"].ToString() == "Agent")
                    {
                        fromemail = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        fromemail = supplier_email;
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
                        toemail1 = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Backoffice")
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
                    else if (ds_mailTemplate1.Tables[0].Rows[1]["TO_ROLE_NAME"].ToString() == "Supplier")
                    {
                        toemail1 = supplier_email;
                    }
                    else
                    {
                        toemail1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }

                if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString());
                    if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Agent")
                    {
                        cc = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["CC_ROLE_NAME"].ToString() == "Backoffice")
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
                if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() != "")
                {
                    DataSet ds_Mail = objHotelStoreProcedure.get_email_adress_backoffice("FETCH_EMAIL_ADRESS_DEPARTMENT_VISE", ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString());

                    if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Agent")
                    {
                        bcc1 = dt11.Rows[0]["CUST_REL_EMAIL"].ToString();
                    }

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Backoffice")
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

                    else if (ds_mailTemplate1.Tables[0].Rows[1]["BCC_ROLE_NAME"].ToString() == "Supplier")
                    {
                        bcc = supplier_email;
                    }

                    else
                    {
                        bcc1 = ds_Mail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                    }
                }
                string body = "";

                //  body = "Dear Reservation,<br><br> Please Confirm as Below <br><br>" + hotel_name + "<br><br> Our Reference Number:" + EmailQoute_id + "<br>Name of the passenger :" + pessanger_name + "<br>Room Type:" + room_type + "<br>No of Single Rooms:" + single_rooms + "<br>No of Double Rooms:" + double_rooms + "<br>No of Triple Rooms:" + tripal_rooms + "<br>Check In Date : " + check_in_date + "<br>Check out Date :" + check_out_date + "<br><br>Please advise the Cut of Date : <br><br>Please advice the Last Payment Date : <br><br>Please send Confirmation No : <br><br> Best Reagrds <br>Travelz Unlmited";
                string strEmailTemplate = ds_mailTemplate1.Tables[0].Rows[1]["EMAIL_CONTENT"].ToString();
                //  body = FormatEmail(strEmailTemplate, Tourname, AgentName);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%AGENT_NAME%&gt;", dt11.Rows[0]["CUST_REL_NAME"].ToString());
                strEmailTemplate = strEmailTemplate.Replace("&lt;%QUOTE_ID%&gt;", quoteId);
                strEmailTemplate = strEmailTemplate.Replace("&lt;%RECONFIRMATION_DATE%&gt;", dt11.Rows[0]["AGENT_RECONFIRMATION_DATE"].ToString());

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
                    subjct = subjct.Replace("<%QUOTE_ID%>", quoteId);

                    message.Subject = subjct;
                    //     message.Subject = "Booking for : " + pessanger_name + " Check in date : " + check_in_date + " Check out date : " + check_out_date;
                    //      message.CC.Add(new MailAddress(cc.ToString()));
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

                conn.Close();
            }
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

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_FOR_CANCELLATION", Session["usersid"].ToString());
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
    }
}