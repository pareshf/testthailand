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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.EmailSettings
{
    public partial class EmailMaster : System.Web.UI.Page
    {
        CRM.DataAccess.EmailSettings.EmailMaster objemailmaster = new CRM.DataAccess.EmailSettings.EmailMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objemailmaster.fetchallData("FETCH_ALL_EMAIL_DATA");
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    //   Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    DataSet dsval = objemailmaster.fetchallData("FETCH_AGENT_NAME_FOR_EMAIL_DROPDOWN");
                    binddropdownlist(drpagent, dsval);
                }
            }
        }
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
           // r.DataValueField = "USER_ID";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "0";
        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            string masterid = GV_Result.Rows[newindex].Cells[1].Text;

            Session["Email_master_id"] = masterid;
           // Response.Redirect("~/Views/EmailSettings/NewEmail.aspx?EMAIL_TRAIL_MASTER_ID="+ masterid);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'EmailDetails.aspx', null, 'height=1000,width=800,status=yes,toolbar=no,menubar=no,location=no' );", true);
        }
        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            Updateconfirm.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {
            if (txtQuoteId.Text == "")
            {
                txtQuoteId.Text = "0";
            }
            DataSet ds1 = objemailmaster.fetchallEmailData("FETCH_ALL_RECORDS_FOR_EMAIL", txtQuoteId.Text, txtfromdate.Text, drpagent.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds1;
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (txtQuoteId.Text == "")
            {
                txtQuoteId.Text = "0";
            }
            
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objemailmaster.fetchallEmailData("FETCH_ALL_RECORDS_FOR_EMAIL", txtQuoteId.Text, txtfromdate.Text, drpagent.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}