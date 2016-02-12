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
    public partial class EmailAlertMaster : System.Web.UI.Page
    {
        EmailAlertDal objE_AlertDal = new EmailAlertDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                ds = objE_AlertDal.getexistingdata();
                lblBirthdateofemp.Text = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                txtsubBirthdateofemp.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                txtBirthdateofemp.Text = ds.Tables[0].Rows[0]["EMAIL_MESSAGE"].ToString();

                lblCustomerBirthdate.Text = ds.Tables[0].Rows[1]["EMAIL_TYPE"].ToString();
                txtsubCustomerBirthdate.Text = ds.Tables[0].Rows[1]["EMAIL_SUBJECT"].ToString();
                txtCustomerBirthdate.Text = ds.Tables[0].Rows[1]["EMAIL_MESSAGE"].ToString();

                lblcustAnnvsry.Text = ds.Tables[0].Rows[2]["EMAIL_TYPE"].ToString();
                txtsubcustAnnvsry.Text = ds.Tables[0].Rows[2]["EMAIL_SUBJECT"].ToString();
                txtcustAnnvsry.Text = ds.Tables[0].Rows[2]["EMAIL_MESSAGE"].ToString();

                lbltotalbookedinq.Text = ds.Tables[0].Rows[3]["EMAIL_TYPE"].ToString();
                txtsubtotalbookedinq.Text = ds.Tables[0].Rows[3]["EMAIL_SUBJECT"].ToString();
                txttotalbookedinq.Text = ds.Tables[0].Rows[3]["EMAIL_MESSAGE"].ToString();

                lbltotalcnclinq.Text = ds.Tables[0].Rows[4]["EMAIL_TYPE"].ToString();
                txtsubtotalcnclinq.Text = ds.Tables[0].Rows[4]["EMAIL_SUBJECT"].ToString();
                txttotalcnclinq.Text = ds.Tables[0].Rows[4]["EMAIL_MESSAGE"].ToString();

                lblnewcst.Text = ds.Tables[0].Rows[5]["EMAIL_TYPE"].ToString();
                txtsubnewcst.Text = ds.Tables[0].Rows[5]["EMAIL_SUBJECT"].ToString();
                txtnewcst.Text = ds.Tables[0].Rows[5]["EMAIL_MESSAGE"].ToString();

                lblnewinq.Text = ds.Tables[0].Rows[6]["EMAIL_TYPE"].ToString();
                txtsubnewinq.Text = ds.Tables[0].Rows[6]["EMAIL_SUBJECT"].ToString();
                txtnewinq.Text = ds.Tables[0].Rows[6]["EMAIL_MESSAGE"].ToString();

                lblAddingNewemp.Text = ds.Tables[0].Rows[7]["EMAIL_TYPE"].ToString();
                txtsubAddingNewemp.Text = ds.Tables[0].Rows[7]["EMAIL_SUBJECT"].ToString();
                txtAddingNewemp.Text = ds.Tables[0].Rows[7]["EMAIL_MESSAGE"].ToString();

                lblnewadditionof_D_tour.Text = ds.Tables[0].Rows[8]["EMAIL_TYPE"].ToString();
                txtsubnewadditionof_D_tour.Text = ds.Tables[0].Rows[8]["EMAIL_SUBJECT"].ToString();
                txtnewadditionof_D_tour.Text = ds.Tables[0].Rows[8]["EMAIL_MESSAGE"].ToString();

                lblnewadditionof_I_tour.Text = ds.Tables[0].Rows[9]["EMAIL_TYPE"].ToString();
                txtsubnewadditionof_I_tour.Text = ds.Tables[0].Rows[9]["EMAIL_SUBJECT"].ToString();
                txtnewadditionof_I_tour.Text = ds.Tables[0].Rows[9]["EMAIL_MESSAGE"].ToString();

                lblcond1.Text = ds.Tables[0].Rows[10]["EMAIL_TYPE"].ToString();
                txtsubcond1.Text = ds.Tables[0].Rows[10]["EMAIL_SUBJECT"].ToString();
                txtcond1.Text = ds.Tables[0].Rows[10]["EMAIL_MESSAGE"].ToString();

                lblcond2.Text = ds.Tables[0].Rows[11]["EMAIL_TYPE"].ToString();
                txtsubcond2.Text = ds.Tables[0].Rows[11]["EMAIL_SUBJECT"].ToString();
                txtcond2.Text = ds.Tables[0].Rows[11]["EMAIL_MESSAGE"].ToString();

                lblcoond3.Text = ds.Tables[0].Rows[12]["EMAIL_TYPE"].ToString();
                txtsubcoond3.Text = ds.Tables[0].Rows[12]["EMAIL_SUBJECT"].ToString();
                txtcoond3.Text = ds.Tables[0].Rows[12]["EMAIL_MESSAGE"].ToString();

            }
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {
                Session["currentevent"] = "EmailAlert";
            }
        }
        protected void btnSaveAlertMsgFormat_Click(object sender, EventArgs e)
        {
            try
            {
                objE_AlertDal.DeleteAlertMessages();
                objE_AlertDal.InsertAlertMessages(lblBirthdateofemp.Text, txtsubBirthdateofemp.Text.ToString(), txtBirthdateofemp.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblCustomerBirthdate.Text, txtsubCustomerBirthdate.Text.ToString(), txtCustomerBirthdate.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblcustAnnvsry.Text, txtsubcustAnnvsry.Text.ToString(), txtcustAnnvsry.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lbltotalbookedinq.Text, txtsubtotalbookedinq.Text.ToString(), txttotalbookedinq.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lbltotalcnclinq.Text, txtsubtotalcnclinq.Text.ToString(), txttotalcnclinq.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblnewcst.Text, txtsubnewcst.Text.ToString(), txtnewcst.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblnewinq.Text, txtsubnewinq.Text.ToString(), txtnewinq.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblAddingNewemp.Text, txtsubAddingNewemp.Text.ToString(), txtAddingNewemp.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblnewadditionof_D_tour.Text.ToString(), txtsubnewadditionof_D_tour.Text.ToString(), txtnewadditionof_D_tour.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblnewadditionof_I_tour.Text.ToString(), txtsubnewadditionof_I_tour.Text.ToString(), txtnewadditionof_I_tour.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblcond1.Text, txtsubcond1.Text.ToString(), txtcond1.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblcond2.Text, txtsubcond2.Text.ToString(), txtcond2.Text.ToString());
                objE_AlertDal.InsertAlertMessages(lblcoond3.Text, txtsubcoond3.Text.ToString(), txtcoond3.Text.ToString());

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