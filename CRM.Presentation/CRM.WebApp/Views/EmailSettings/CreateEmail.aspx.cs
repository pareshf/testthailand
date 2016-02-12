using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using CRM.DataAccess.EmailSettings;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.EmailSettings
{
    public partial class CreateEmail : System.Web.UI.Page
    {
        EmailSettingsStoredProcedure objEmailSettingsStoredProcedure = new EmailSettingsStoredProcedure();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
                {
                    DataSet ds = objEmailSettingsStoredProcedure.fetch_data_for_edit("GET_EMAIL_DETAILS_FOR_UPDATE", int.Parse(Request["ID"].ToString()));

                    drp_eventname.Text =ds.Tables[0].Rows[0]["EVENT_NAME"].ToString();

                    txt_description.Text = ds.Tables[0].Rows[0]["EMAIL_DESC"].ToString();
                    drp_toadresstype.Text = ds.Tables[0].Rows[0]["TO_ROLE_NAME"].ToString();
                    drp_fromadresstype.Text = ds.Tables[0].Rows[0]["FROM_ROLE_NAME"].ToString();

                    if (ds.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString() != "")
                    {
                        drp_ccadresstype.Text = ds.Tables[0].Rows[0]["CC_ROLE_NAME"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString() != "")
                    {
                        drp_bccadresstype.Text = ds.Tables[0].Rows[0]["BCC_ROLE_NAME"].ToString();
                    }
                    txt_subject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    CKEditorControl1.Text = ds.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();

                    if (ds.Tables[0].Rows[0]["IS_AUTO"].ToString() == "True")
                    {
                        RadioButton1.Checked = true;

                    }
                    else
                    {
                        RadioButton2.Checked = true;
                    }

                    if (ds.Tables[0].Rows[0]["IS_ON"].ToString() == "True")
                    {
                        RadioButton3.Checked = true;

                    }
                    else
                    {
                        RadioButton4.Checked = true;
                    }

                }
                else
                {
                    
                }
                DataSet ds1 = objEmailSettingsStoredProcedure.fetch_voucher_type("FETCH_EVENT_NAME_FOR_EMAIL");
                binddropdownlist(drp_eventname, ds1);

                DataSet ds_role = objEmailSettingsStoredProcedure.fetch_voucher_type("FETCH_ROLE_FOR_EMAIL");
                binddropdownlist(drp_fromadresstype, ds_role);
                binddropdownlist(drp_toadresstype, ds_role);
                binddropdownlist(drp_ccadresstype, ds_role);
                binddropdownlist(drp_bccadresstype, ds_role);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            bool isauto = true ;
            bool ison = true;
            if (RadioButton1.Checked != true )
            {
                isauto=false ;
            }

            if (RadioButton3.Checked != true )
            {
                ison=false ;
            }

            if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
            {
                objEmailSettingsStoredProcedure.insert_update_email_master(int.Parse(Request["ID"].ToString()), drp_eventname.Text, txt_description.Text, drp_fromadresstype.Text, drp_toadresstype.Text, drp_ccadresstype.Text, drp_bccadresstype.Text, txt_subject.Text, CKEditorControl1.Text, isauto, ison, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()));

                Master.DisplayMessage("Email Template Updated Successfully.", "successMessage", 8000);
            }
            else
            {
                objEmailSettingsStoredProcedure.insert_update_email_master(0, drp_eventname.Text, txt_description.Text, drp_fromadresstype.Text, drp_toadresstype.Text, drp_ccadresstype.Text, drp_bccadresstype.Text, txt_subject.Text, CKEditorControl1.Text, isauto, ison, int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()));

                clear();

                Master.DisplayMessage("Email Template Generated Successfully.", "successMessage", 8000);
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "";
        }

        public void clear()
        {
            drp_bccadresstype.Text = "";
            drp_ccadresstype.Text = "";
            drp_eventname.Text = "";
            drp_fromadresstype.Text = "";
            drp_toadresstype.Text = "";

            CKEditorControl1.Text = "";
            txt_description.Text = "";
            txt_subject.Text = "";

            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
            RadioButton4.Checked = false;

            update_voucher.Update();
        }
    }
}