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
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Settings
{
    public partial class AgentProfile : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm objagentmaster = new CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet dsprofile=objagentmaster.FetchProfileData(Session["usersid"].ToString());
                radCmbAgent.Text = dsprofile.Tables[0].Rows[0]["TITLE_DESC"].ToString();
                txtClientlastname.Text = dsprofile.Tables[0].Rows[0]["CUST_REL_SURNAME"].ToString();
                txtClientname.Text = dsprofile.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                txteditMobile.Text = dsprofile.Tables[0].Rows[0]["CUST_REL_MOBILE"].ToString();
                txtEditEmail.Text = dsprofile.Tables[0].Rows[0]["CUST_REL_EMAIL"].ToString();
                ViewState["mail"] = dsprofile.Tables[0].Rows[0]["CUST_REL_EMAIL"].ToString();
                txtEditPhone.Text = dsprofile.Tables[0].Rows[0]["CUST_REL_PHONE"].ToString();
                radCmbDesignation.Text = dsprofile.Tables[0].Rows[0]["DESIGNATION_DESC"].ToString();
                Cmbdrpuserstatus.Text = dsprofile.Tables[0].Rows[0]["USER_STATUS_NAME"].ToString();
                radCmbMaritalStatusAdd.Text = dsprofile.Tables[0].Rows[0]["MARITAL_STATUS_NAME"].ToString();
                txtbirthdate.Text = dsprofile.Tables[0].Rows[0]["CUST_BIRTH_DATE"].ToString(); 
                DataSet ds2 = objagentmaster.fetchTitle();
                DataSet ds1 = objagentmaster.fetchdesignation();
                DataSet ds10 = objagentmaster.fetchstatus();
                DataSet ds11 = objagentmaster.GetMaritalStatus();
                binddropdownlist(radCmbAgent, ds2);
                binddropdownlist(radCmbDesignation, ds1);
                binddropdownlist(Cmbdrpuserstatus, ds10);
                binddropdownlist(radCmbMaritalStatusAdd, ds11);
                UpProfile.Update();
            }
        }
        protected void binddropdownlist(RadComboBox r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new RadComboBoxItem("", "0"));
            //r.SelectedValue = "1";
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (txtEditEmail.Text == ViewState["mail"].ToString())
            {
                objagentmaster.InsertUpdateProfile(Session["empid"].ToString(), radCmbAgent.Text, txtClientlastname.Text, txtClientname.Text, txteditMobile.Text, txtEditEmail.Text, txtEditPhone.Text, radCmbDesignation.Text, Cmbdrpuserstatus.Text, radCmbMaritalStatusAdd.Text, txtbirthdate.Text);
                Master.DisplayMessage("Record Update successfully.", "successMessage", 3000);
            }
            else
            {
                DataSet DSUSER = objagentmaster.fetchALLUSERNAME();

                bool flag = false;
                for (int i = 0; i < DSUSER.Tables[0].Rows.Count; i++)
                {
                    if (txtEditEmail.Text == DSUSER.Tables[0].Rows[i]["USER_NAME"].ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    objagentmaster.InsertUpdateProfile(Session["empid"].ToString(), radCmbAgent.Text, txtClientlastname.Text, txtClientname.Text, txteditMobile.Text, txtEditEmail.Text, txtEditPhone.Text, radCmbDesignation.Text, Cmbdrpuserstatus.Text, radCmbMaritalStatusAdd.Text, txtbirthdate.Text);
                    Master.DisplayMessage("Record Updated Successfully.", "successMessage", 8000);
                }
                else 
                {
                    Master.DisplayMessage("Email already exist. Enter another E-mail.", "successMessage", 8000);
                }
            }
            
            UpProfile.Update();
        }


        protected void txtOldPassword_TextChanged(object sender, EventArgs e)
        {
            DataSet dsprofile = objagentmaster.FetchProfileData(Session["usersid"].ToString());
            if (txtOldPassword.Text != dsprofile.Tables[0].Rows[0]["PASSWORD"].ToString())
            {
                txtOldPassword.Text = "";
                Master.DisplayMessage("Password is incorrect.", "successMessage", 3000);
                txtOldPassword.Focus();
            }
            else
            {
                string Password = txtOldPassword.Text;
                txtOldPassword.Attributes.Add("value", Password);
                txtNewPassword.Focus();
            }
            UpdatePanel1.Update();
        }

        protected void Change_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                Master.DisplayMessage("Confirm New Pasword is not same as New password.", "successMessage", 3000);
                string Password = txtOldPassword.Text;
                txtOldPassword.Attributes.Add("value", Password);

                string Password1 = txtNewPassword.Text;
                txtNewPassword.Attributes.Add("value", Password1);

                txtConfirmNewPassword.Focus();
            }
            else
            {

                objagentmaster.UpdatePassword(int.Parse(Session["usersid"].ToString()), txtNewPassword.Text);
                Master.DisplayMessage("Pasword changed successfully.", "successMessage", 3000);
                txtNewPassword.Text = "";
                txtOldPassword.Text = "";
                txtConfirmNewPassword.Text = "";
            }
            UpdatePanel1.Update();
        }
    }
}