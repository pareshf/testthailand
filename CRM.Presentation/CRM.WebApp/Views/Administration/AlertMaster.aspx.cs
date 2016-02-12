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
using CRM.DataAccess.AdministrationDAL;

namespace CRM.WebApp.Views.Administration
{
    public partial class AlertMaster : System.Web.UI.Page
    {
        AlertDal objAlertDal = new AlertDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                Session["currentevent"] = "SmsAlert";
            }
            if (!IsPostBack)
            {

                DataSet ds = new DataSet();
                ds = objAlertDal.getexistingdata();
                lblBirthdateofemp.Text = ds.Tables[0].Rows[0]["SMS_TYPE"].ToString();
                txtBirthdateofemp.Text = ds.Tables[0].Rows[0]["SMS_MESSAGE"].ToString();

                lblCustomerBirthdate.Text = ds.Tables[0].Rows[1]["SMS_TYPE"].ToString();
                txtCustomerBirthdate.Text = ds.Tables[0].Rows[1]["SMS_MESSAGE"].ToString();

                lblcustAnnvsry.Text = ds.Tables[0].Rows[2]["SMS_TYPE"].ToString();
                txtcustAnnvsry.Text = ds.Tables[0].Rows[2]["SMS_MESSAGE"].ToString();

                lbltotalbookedinq.Text = ds.Tables[0].Rows[3]["SMS_TYPE"].ToString();
                txttotalbookedinq.Text = ds.Tables[0].Rows[3]["SMS_MESSAGE"].ToString();

                lbltotalcnclinq.Text = ds.Tables[0].Rows[4]["SMS_TYPE"].ToString();
                txttotalcnclinq.Text = ds.Tables[0].Rows[4]["SMS_MESSAGE"].ToString();

                lblnewcst.Text = ds.Tables[0].Rows[5]["SMS_TYPE"].ToString();
                txtnewcst.Text = ds.Tables[0].Rows[5]["SMS_MESSAGE"].ToString();

                lblnewinq.Text = ds.Tables[0].Rows[6]["SMS_TYPE"].ToString();
                txtnewinq.Text = ds.Tables[0].Rows[6]["SMS_MESSAGE"].ToString();

                lblAddingNewemp.Text = ds.Tables[0].Rows[7]["SMS_TYPE"].ToString();
                txtAddingNewemp.Text = ds.Tables[0].Rows[7]["SMS_MESSAGE"].ToString();

                lblnewadditionof_D_tour.Text = ds.Tables[0].Rows[8]["SMS_TYPE"].ToString();
                txtnewadditionof_D_tour.Text = ds.Tables[0].Rows[8]["SMS_MESSAGE"].ToString();

                lblnewadditionof_I_tour.Text = ds.Tables[0].Rows[9]["SMS_TYPE"].ToString();
                txtnewadditionof_I_tour.Text = ds.Tables[0].Rows[9]["SMS_MESSAGE"].ToString();

                lblcond1.Text = ds.Tables[0].Rows[10]["SMS_TYPE"].ToString();
                txtcond1.Text = ds.Tables[0].Rows[10]["SMS_MESSAGE"].ToString();

                lblcond2.Text = ds.Tables[0].Rows[11]["SMS_TYPE"].ToString();
                txtcond2.Text = ds.Tables[0].Rows[11]["SMS_MESSAGE"].ToString();

                lblcoond3.Text = ds.Tables[0].Rows[12]["SMS_TYPE"].ToString();
                txtcoond3.Text = ds.Tables[0].Rows[12]["SMS_MESSAGE"].ToString();

            }
           
        }

        protected void btnSaveAlertMsgFormat_Click(object sender, EventArgs e)
        {
            try
            {
                objAlertDal.DeleteAlertMessages();
                objAlertDal.InsertAlertMessages(lblBirthdateofemp.Text, txtBirthdateofemp.Text);
                objAlertDal.InsertAlertMessages(lblCustomerBirthdate.Text, txtCustomerBirthdate.Text);
                objAlertDal.InsertAlertMessages(lblcustAnnvsry.Text, txtcustAnnvsry.Text);
                objAlertDal.InsertAlertMessages(lbltotalbookedinq.Text, txttotalbookedinq.Text);
                objAlertDal.InsertAlertMessages(lbltotalcnclinq.Text, txttotalcnclinq.Text);
                objAlertDal.InsertAlertMessages(lblnewcst.Text, txtnewcst.Text);
                objAlertDal.InsertAlertMessages(lblnewinq.Text, txtnewinq.Text);
                objAlertDal.InsertAlertMessages(lblAddingNewemp.Text, txtAddingNewemp.Text);
                objAlertDal.InsertAlertMessages(lblnewadditionof_D_tour.Text, txtnewadditionof_D_tour.Text);
                objAlertDal.InsertAlertMessages(lblnewadditionof_I_tour.Text, txtnewadditionof_I_tour.Text);
                objAlertDal.InsertAlertMessages(lblcond1.Text, txtcond1.Text);
                objAlertDal.InsertAlertMessages(lblcond2.Text, txtcond2.Text);
                objAlertDal.InsertAlertMessages(lblcoond3.Text, txtcoond3.Text);

                Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Save].ToString());
                Master.MessageCssClass = "successMessage";
            }
            catch (Exception ex)
            {
                Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Save].ToString());
                Master.MessageCssClass = "errorMessage";
            }
        }
    }
}
