#region Program Information
/**********************************************************************************************************************************************
 Class Name           : ForgotPasswordLookUp
 Class Description    : Implementation logic for get operation for password details.
 Author               : Chirag.
 Created Date         : Mar 13, 2010
***********************************************************************************************************************************************/
#endregion

#region Imports assemblies
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Core.Constants;
using CRM.Core.Utility;
using CRM.DataAccess.AdministrationDAL;
using CRM.DataAccess.SecurityDAL;
using CRM.Model.AdministrationModel;
using CRM.Model.Security;
using CRM.Model.General;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Telerik.Web.UI;
using System.Net;
using CRM.DataAccess.FIT;


#endregion
namespace CRM.WebApp
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        #region Global Declaration
        ForgotPasswords objForgotPasswords = new ForgotPasswords();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        public static string SecurityAnswer;
        public static string EmailId;
        static PasswordRecovery objPasswordRecovery = null;

        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        string smtpemail = "kushal@flamingotravels.co.in";
        string smtppass = "dadashri";
        string smtphost = "smtpcorp.com";
        string fromemail = "sudhir@travelzunlimited.com";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ForgotPasswordWizard.ActiveStepIndex = 0;
                txtUserName.Focus();
            }
        }

        protected void ForgotPasswordWizard_ActiveStepChanged(object sender, EventArgs e)
        {
            ForgotPasswordWizard.HeaderText = " " + "(" + ForgotPasswordWizard.ActiveStep.Title + ")";
        }

        protected void ForgotPasswordWizard_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            string temp = string.Empty;
            string result = string.Empty;
            txtSeqQusAns.Focus();
            ForgotPasswordWizard.HeaderText =" "+ " (" + ForgotPasswordWizard.ActiveStep.Title + ")";

            switch (ForgotPasswordWizard.WizardSteps[e.CurrentStepIndex].ID)
            {
                case "wizardStep1":
                    lblUserNameRequire.Text = temp;
                    lblEmailIdRequire.Text = temp;
                    string UserName = txtUserName.Text.Trim();
                    string EmailId = txtEmailId.Text.Trim();

                    if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(EmailId))
                        result += "Required <br />";

                    if (string.IsNullOrEmpty(result))
                    {
                        DataTable dt = objAuthorizationDal.CheckEmailId(txtEmailId.Text.Trim(), txtUserName.Text.Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            lblEmailIdNotValid.Visible = false;
                            EmailId = dt.Rows[0]["EMP_EMAIL"].ToString();
                            lblSeqQusFetch.Text = dt.Rows[0]["SECURITY_QUESTION_DESC"].ToString();
                            SecurityAnswer = dt.Rows[0]["SECURITY_ANSWERS"].ToString();
                            objPasswordRecovery = new PasswordRecovery();
                            objPasswordRecovery.UserName = dt.Rows[0]["USER_NAME"].ToString();
                            objPasswordRecovery.UserId = Convert.ToInt32(dt.Rows[0]["USER_ID"]);
                            objPasswordRecovery.EmailId = dt.Rows[0]["EMP_EMAIL"].ToString();
                            lblSeqQusEmailIdFatch.Text = EmailId.ToString();
                        }
                        else
                        {
                            if (lblSeqQusEmailIdFatch.Text == string.Empty || lblGetUsernameFetch.Text == string.Empty)
                            {
                                lblEmailIdRequire.Text = string.Empty;
                                lblEmailIdNotValid.Text = "Invalid Username or Email Id.";
                                e.Cancel = true;
                            }
                        }
                    }
                    else
                    {
                       // lblUserNotValid.Text = string.Empty;
                        lblEmailIdNotValid.Text = string.Empty;
                        //lblUserNameRequire.Text = "Required";
                       lblEmailIdRequire.Text = "Required Username or Emial Id";
                        e.Cancel = true;
                    }
                    break;
                case "wizardStep2":

                    lblSeqAnsRequire.Text = temp;
                    if (string.IsNullOrEmpty(txtSeqQusAns.Text))
                        result += "Required <br />";
                    if (string.IsNullOrEmpty(result))
                    {
                        if (SecurityAnswer.Equals(txtSeqQusAns.Text.Trim()))
                        {
                            if (objPasswordRecovery != null)
                            {
                                string password = Utility.GetRandomPassword(6);
                                if (!String.IsNullOrEmpty(password))
                                {
                                    objPasswordRecovery.Password = password;
                                    if (SendMail(objPasswordRecovery))
                                        objAuthorizationDal.ResetUserPassword(objPasswordRecovery.UserId, objPasswordRecovery.Password);
                                }
                            }
                        }
                        else
                        {
                            lblSeqAnsRequire.Text = string.Empty;
                            lblSeqAnsRequire.Text = "Incorrect answer.";
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        lblSeqAnsRequire.Text = string.Empty;
                        lblSeqAnsRequire.Text = "Required";
                        e.Cancel = true;
                    }
                    break;
                case "wizardStep3":
                    break;
            }
        }

        protected void ForgotPasswordWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

            Response.Redirect("~/Login.aspx");
            e.Cancel = true;
        }

        private Boolean SendMail(PasswordRecovery objPasswordRecovery)
        {
            bool mailSent = true;
            MailMessage objMsg = null;
            Hashtable hsTableMailInfo = new Hashtable();
            SmtpClient smtpClient = new SmtpClient();

            hsTableMailInfo["ext:PosswordRecovery"] = objPasswordRecovery;
            String templatePhysicalPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, @"Views\Shared\Templates\Email\");

            objMsg = new MailMessage();
            objMsg.From = new MailAddress(ConfigurationManager.AppSettings[PageConstants.FromAddress], ConfigurationManager.AppSettings[PageConstants.FromDisplayName], Encoding.UTF8);
            objMsg.To.Add(objPasswordRecovery.EmailId);
            objMsg.Subject = "Your lost password";
            objMsg.SubjectEncoding = Encoding.UTF8;
            objMsg.Body = Utility.TransformXsltToXhtml(templatePhysicalPath, "PasswordRecovery.xslt", hsTableMailInfo);
            objMsg.BodyEncoding = Encoding.UTF8;
            objMsg.IsBodyHtml = true;
            objMsg.Priority = MailPriority.High;
            try
            {
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings[PageConstants.SmtpUserName], ConfigurationManager.AppSettings[PageConstants.SmtpPassword]);
                smtpClient.Host = ConfigurationManager.AppSettings[PageConstants.SmtpHost];
                smtpClient.Port = int.Parse(ConfigurationManager.AppSettings[PageConstants.SmtpPort]);
                smtpClient.EnableSsl = true;
                smtpClient.Send(objMsg);
                mailSent = true;
            }
            catch (Exception)
            {
                //mailSent = false;
            }

            return mailSent;
        }

        //protected void SendMail_CodeProject(string ToEmail)
        //{
        //    //http://www.codeproject.com/KB/aspnet/SMTPGmail.aspx
        //    try
        //    {
        //        string smtpemail = "sales.tester@gmail.com";
        //        string smtppass = "ambasoft";
        //        string smtphost = "smtp.gmail.com";
        //        string smtpport = "587";


        //        string from = smtpemail; //Replace this with your own correct Gmail Address
        //        string to = ToEmail; //Replace this with the Email Address to whom you want to send the mail


        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //        mail.To.Add(to);
        //        mail.From = new MailAddress(from, "SriVani", System.Text.Encoding.UTF8);
        //        mail.Subject = "Admission Form test mail";
        //        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //        mail.Body = "This is Email Body Text";
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        mail.IsBodyHtml = true;
        //        mail.Priority = System.Net.Mail.MailPriority.High;

        //        SmtpClient client = new SmtpClient();

        //        client.Credentials = new System.Net.NetworkCredential(from, smtppass);

        //        client.Port = int.Parse(smtpport); // Gmail works on this port
        //        client.Host = smtphost;
        //        client.EnableSsl = true; //Gmail works on Server Secured Layer
        //        try
        //        {
        //            client.Send(mail);
        //        }
        //        catch (Exception ex)
        //        {
        //            Exception ex2 = ex;
        //            string errorMessage = string.Empty;
        //            while (ex2 != null)
        //            {
        //                errorMessage += ex2.ToString();
        //                ex2 = ex2.InnerException;
        //            }
        //            HttpContext.Current.Response.Write(errorMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message.ToString());
        //    }
        //}

        class PasswordRecovery
        {
            public int UserId { get; set; }
            public String UserName { get; set; }
            public String Password { get; set; }
            public String EmailId { get; set; }
        }

        protected void StartNextButton_Click(object sender, EventArgs e)
        {
            bool flage = false;
            if (txtUserName.Text != "")
            {

                DataSet dscheck = objForgotPasswords.FetchUsername();

                for (int i = 0; i < dscheck.Tables[0].Rows.Count; i++)
                {
                    if (txtUserName.Text == dscheck.Tables[0].Rows[i]["USER_NAME"].ToString() && ( dscheck.Tables[0].Rows[i]["FLAG"].ToString() == "A" || dscheck.Tables[0].Rows[i]["FLAG"].ToString() == "E"))
                    {
                        flage = true;
                    }
                   
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Enter User Name.')", true);
            }




             if (flage==true)
            {
                string email = txtUserName.Text;
                //string Username, Password, Host, Port;
                ////string student = Session["FIRST_NAME"].ToString();
                //string subject1 = "Change Password";

                objForgotPasswords.Fetch_New_Password(email);
                DataSet ds = objForgotPasswords.Fetch_UpdatedPassword(email);

                DataTable dtemail = objHotelStoreProcedure.fetchemailusingRoleid("FETCH_EMAIL_ID_FOR_ADMIN", "18");
                string Confirmagentbody = "Your New Password is: " + ds.Tables[0].Rows[0][0].ToString() + "<br><br>Best Regards<br>Travelz Unlimited";



                MailMessage message = new MailMessage();
                message.From = new MailAddress(dtemail.Rows[0][0].ToString());
                message.To.Add(txtUserName.Text);
                message.Subject = "Passwor Recovery";
                message.Body = Confirmagentbody;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                NetworkCredential info = new NetworkCredential(smtpemail, smtppass);
                client.Credentials = info;
                client.Host = smtphost;
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);

              //  DataSet eventname = objCommon.get_emailConfig("FETCH_EVENT_NAME_FOR_EMAIL");
               // DataSet dsevent = objCommon.get_email_templaet_data("FETCH_DATA_FOR_EMAIL_TEMPLATE", eventname.Tables[0].Rows[7]["AutoSearchResult"].ToString());


             //   string strEmailTemplate = dsevent.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                //strEmailTemplate = strEmailTemplate.Replace("&lt;%STUDENTNAME%&gt;", student);
              //  strEmailTemplate = strEmailTemplate.Replace("&lt;%NewPword%&gt;", ds.Tables[0].Rows[0]["PASSWORD"].ToString());

          //      DataSet ds1 = objCommon.FETCHDATAFOREMAILSEND();
           //     Username = ds1.Tables[0].Rows[0]["SMTP_USERID"].ToString();
           //     Password = ds1.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
           //     Host = ds1.Tables[0].Rows[0]["SMTP_HOST"].ToString();
           //     Port = ds1.Tables[0].Rows[0]["SMTP_PORT"].ToString();
          //      objCommon.sendemail(Username, Password, Host, Port, "bhavin@ambait.com", email, "", "", subject1, strEmailTemplate, "", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Password Sent Successfully to Your Email.')", true);
                //Response.Redirect("Default.aspx");
            //    Clear();
            }
            if (flage == false)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Enter Valid User Name.')", true);
              //  Clear();
            }
        
        }
    }
}
