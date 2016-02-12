using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using CRM.WebApp.WebHelper;
using System.Configuration;
using CRM.Core.Constants;

namespace CRM.WebApp.Views.Administration
{
    public partial class ApplicationSettings : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                BindApplicationSettings();
            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                Session["currentevent"] = "Application Settings";
            }
        }

        protected void btnSaveSmtpSettings_Click(object sender, EventArgs e)
        {
            try
            {
                string smtpusername = txtSmtpUsername.Text;
                string smtppassword = txtSmtpPassword.Text;
                string smtphost = txtSmtpHost.Text;
                string smtpport = txtSmtpPort.Text;

                Dictionary<string, string> appsettings = new Dictionary<string, string>();
                appsettings.Add("smtpusername", smtpusername);
                appsettings.Add("smtppassword", smtppassword);
                appsettings.Add("smtphost", smtphost);
                appsettings.Add("smtpport", smtpport);
                WebManager.SetApplicationSetting(appsettings);

                BindApplicationSettings();

                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            catch (Exception ex)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        protected void btnSaveSmsSettings_Click(object sender, EventArgs e)
        {
            try
            {
                string smsusername = txtSmsUsername.Text;
                string smspassword = txtSmsPassword.Text;
                string smsdomain = txtSmsDomain.Text;
                string smssenderid = txtSmsSenderId.Text;                

                Dictionary<string, string> appsettings = new Dictionary<string, string>();
                appsettings.Add("smsusername", smsusername);
                appsettings.Add("smspassword", smspassword);
                appsettings.Add("smsdomain", smsdomain);
                appsettings.Add("smssenderid", smssenderid);
                WebManager.SetApplicationSetting(appsettings);                

                BindApplicationSettings();

                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            catch (Exception ex)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        protected void btnSaveAlertMsgFormat_Click(object sender, EventArgs e)
        {
            try
            {
                string newcustomersmsmessage = txtSmsNewCustomer.Text;
                string newenquirysmsmessage = txtSmsNewEnqury.Text;

                string newcustomermailmessage = txtMailNewCustomerAlt.Text;
                string newenquirymailmessage = txtMailNewEnquiry.Text;

                Dictionary<string, string> smsmessages = new Dictionary<string, string>();
                smsmessages.Add("newcustomersmsmessage", newcustomersmsmessage);
                smsmessages.Add("newenquirysmsmessage", newenquirysmsmessage);
                WebManager.SetApplicationSetting(smsmessages);

                Dictionary<string, string> mailmessages = new Dictionary<string, string>();
                mailmessages.Add("newcustomermailmessage", newcustomermailmessage);
                mailmessages.Add("newenquirymailmessage", newenquirymailmessage);
                WebManager.SetApplicationSetting(mailmessages);

                BindApplicationSettings();

                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            catch (Exception ex)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        protected void btnSaveAlertSettings_Click(object sender, EventArgs e)
        {
            try
            {
                string newcustomeralert = radNewCustomer.SelectedValue;
                string newenquiryalert = radNewEnquiry.SelectedValue;

                Dictionary<string, string> customeralertsettings = new Dictionary<string, string>();
                customeralertsettings.Add("newcustomeralert", newcustomeralert);
                WebManager.SetApplicationSetting(customeralertsettings);

                Dictionary<string, string> enquiryalertsettings = new Dictionary<string, string>();
                enquiryalertsettings.Add("newenquiryalert", newenquiryalert);
                WebManager.SetApplicationSetting(enquiryalertsettings);

                BindApplicationSettings();

                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            catch (Exception ex)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }

        private void BindApplicationSettings()
        {
            Dictionary<string, string> smtpsettings = new Dictionary<string, string>();
            smtpsettings = WebManager.GetApplicationSetting("smtpsettings", true);

            txtSmtpUsername.Text = smtpsettings["smtpusername"];
            txtSmtpPassword.Text = smtpsettings["smtppassword"];
            txtSmtpHost.Text = smtpsettings["smtphost"];
            txtSmtpPort.Text = smtpsettings["smtpport"];


            Dictionary<string, string> smssettings = new Dictionary<string, string>();
            smssettings = WebManager.GetApplicationSetting("smssettings", true);           
            
            txtSmsUsername.Text = smssettings["smsusername"];
            txtSmsPassword.Text = smssettings["smspassword"];
            txtSmsDomain.Text = smssettings["smsdomain"];
            txtSmsSenderId.Text = smssettings["smssenderid"];


            Dictionary<string, string> customeralertsettings = new Dictionary<string, string>();
            customeralertsettings = WebManager.GetApplicationSetting("customeralertsettings", true);
            radNewCustomer.Items.FindByValue(customeralertsettings["newcustomeralert"]).Selected = true;

            Dictionary<string, string> enquiryalertsettings = new Dictionary<string, string>();
            enquiryalertsettings = WebManager.GetApplicationSetting("enquiryalertsettings", true);
            radNewEnquiry.Items.FindByValue(enquiryalertsettings["newenquiryalert"]).Selected = true;


            Dictionary<string, string> smsmessages = new Dictionary<string, string>();
            smsmessages = WebManager.GetApplicationSetting("smsmessages", true);

            txtSmsNewCustomer.Text = smsmessages["newcustomersmsmessage"];
            txtSmsNewEnqury.Text = smsmessages["newenquirysmsmessage"];



            Dictionary<string, string> mailmessages = new Dictionary<string, string>();
            mailmessages = WebManager.GetApplicationSetting("mailmessages", true);

            txtMailNewCustomerAlt.Text = mailmessages["newcustomermailmessage"];
            txtMailNewEnquiry.Text = mailmessages["newenquirymailmessage"];
            
        }

       

    }
}
