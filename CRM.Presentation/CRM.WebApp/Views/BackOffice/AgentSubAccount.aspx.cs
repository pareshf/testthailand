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

namespace CRM.WebApp.Views.BackOffice
{
    public partial class AgentSubAccount : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm objagentmaster = new CRM.DataAccess.AdministratorEntity.AgentRegisttrationForm();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();


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

                DataSet ds2 = objagentmaster.fetchTitle();
                DataSet ds1 = objagentmaster.fetchdesignation();
                DataSet ds10 = objagentmaster.fetchstatus();
                //DataSet ds11 = objagentmaster.SECURITY_QUESTION();
                binddropdownlist(drpTitle, ds2);
                binddropdownlist(drpDesignation, ds1);
                binddropdownlist(drpuserstatus, ds10);
                //binddropdownlist(drpsecurity, ds11);
                if (Request["SR_NO"] != null && !string.IsNullOrEmpty(Request["SR_NO"].ToString()))
                {
                    DataSet ds3 = objagentmaster.fetchSubAgentDetail(Request.QueryString["SR_NO"].ToString());
                    drpTitle.Text = ds3.Tables[0].Rows[0]["TITLE_DESC"].ToString();
                    txtClientlastname.Text = ds3.Tables[0].Rows[0]["CUST_REL_SURNAME"].ToString();
                    txtClientname.Text = ds3.Tables[0].Rows[0]["CUST_REL_NAME"].ToString();
                    CUST_REL_MOBILE.Text = ds3.Tables[0].Rows[0]["CUST_REL_MOBILE"].ToString();
                    CUST_REL_EMAIL.Text = ds3.Tables[0].Rows[0]["CUST_REL_EMAIL"].ToString();
                    txtphone.Text = ds3.Tables[0].Rows[0]["CUST_REL_PHONE"].ToString();
                    drpDesignation.Text = ds3.Tables[0].Rows[0]["DESIGNATION_DESC"].ToString();

                    string Password = ds3.Tables[0].Rows[0]["PASSWORD"].ToString();
                    PASSWORD.Attributes.Add("value", Password);

                   // PASSWORD.Text = ds3.Tables[0].Rows[0]["PASSWORD"].ToString();
                    TextBox1.Text = ds3.Tables[0].Rows[0]["ALT_EMAIL"].ToString();
                    drpuserstatus.Text = ds3.Tables[0].Rows[0]["USER_STATUS_NAME"].ToString();
                    ViewState["mail"] = ds3.Tables[0].Rows[0]["CUST_REL_EMAIL"].ToString(); 
                }
                else
                {
                    drpuserstatus.SelectedValue = ds10.Tables[0].Rows[0]["AutoSearchResult"].ToString();
                }
                
                Updateconfirm.Update();
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

            if (Request["SR_NO"] != null && !string.IsNullOrEmpty(Request["SR_NO"].ToString()))
            {
                if (CUST_REL_EMAIL.Text == ViewState["mail"].ToString())
                {
                    objagentmaster.InsertUpdateRel(Session["cust_id"].ToString(), Request.QueryString["SR_NO"].ToString(), drpTitle.Text, txtClientlastname.Text, txtClientname.Text, CUST_REL_MOBILE.Text, CUST_REL_EMAIL.Text, txtphone.Text, drpDesignation.Text, PASSWORD.Text, TextBox1.Text, drpuserstatus.Text, Session["usersid"].ToString(), Session["empid"].ToString());
                    Master.DisplayMessage("Record Update successfully.", "successMessage", 3000);
                }
                else
                {
                    DataSet DSUSER = objagentmaster.fetchALLUSERNAME();

                    bool flag = false;
                    for (int i = 0; i < DSUSER.Tables[0].Rows.Count; i++)
                    {
                        if (CUST_REL_EMAIL.Text == DSUSER.Tables[0].Rows[i]["USER_NAME"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        objagentmaster.InsertUpdateRel(Session["cust_id"].ToString(), Request.QueryString["SR_NO"].ToString(), drpTitle.Text, txtClientlastname.Text, txtClientname.Text, CUST_REL_MOBILE.Text, CUST_REL_EMAIL.Text, txtphone.Text, drpDesignation.Text, PASSWORD.Text, TextBox1.Text, drpuserstatus.Text, Session["usersid"].ToString(), Session["empid"].ToString());
                        Master.DisplayMessage("Record Update successfully.", "successMessage", 3000);
                    }
                    else
                    {
                        lblerror.Visible = true;
                    }
                }
            }
            else
            {
                DataSet DSUSER = objagentmaster.fetchALLUSERNAME();

                bool flag = false;
                for (int i = 0; i < DSUSER.Tables[0].Rows.Count; i++)
                {
                    if (CUST_REL_EMAIL.Text == DSUSER.Tables[0].Rows[i]["USER_NAME"].ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    objagentmaster.InsertUpdateRel(Session["cust_id"].ToString(), "0", drpTitle.Text, txtClientlastname.Text, txtClientname.Text, CUST_REL_MOBILE.Text, CUST_REL_EMAIL.Text, txtphone.Text, drpDesignation.Text, PASSWORD.Text, TextBox1.Text, drpuserstatus.Text, Session["usersid"].ToString(), Session["empid"].ToString());
                    Master.DisplayMessage("Record save successfully.", "successMessage", 3000);
                    clear();
                }
                else
                {
                    lblerror.Visible = true;
                }
            }
            Updateconfirm.Update();
        }

        protected void clear()
        {
            drpTitle.Text = "0";
            txtClientlastname.Text = "";
            txtClientname.Text = "";
            CUST_REL_MOBILE.Text = "";
            CUST_REL_EMAIL.Text = "";
            txtphone.Text = "";
            drpDesignation.Text = "0";
            PASSWORD.Text = "";
            TextBox1.Text = "";
            drpuserstatus.Text = "0";
        }
    }
}