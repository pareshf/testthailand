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
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;


namespace CRM.WebApp.Views.BackOffice
{
    public partial class SearchAgentSubAccount : System.Web.UI.Page
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
                DataSet ds = objagentmaster.fetchSUBAGENTDETAIL(Session["cust_id"].ToString(), "", "", "");
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();

                }
                DataSet ds10 = objagentmaster.fetchstatus();
                //DataSet ds11 = objagentmaster.SECURITY_QUESTION();
                binddropdownlist(DropDownList2, ds10);
            }
        }
        
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "1";
        }

        protected void GV_Result_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            String SR_NO = "";
            if (e.CommandName == "Approved")
            {
                int newindex = Convert.ToInt32(e.CommandArgument);
                SR_NO = GV_Result.Rows[newindex].Cells[1].Text;
                Response.Redirect("AgentSubAccount.aspx?SR_NO=" + SR_NO);
            }
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
            //if (txtrefrence.Text == "")
            //{
            //    txtrefrence.Text = "0";
            //}
            //string USERID = Session["usersid"].ToString();
            DataSet ds = objagentmaster.fetchSUBAGENTDETAIL(Session["cust_id"].ToString(),TextBox3.Text, DropDownList2.Text,TextBox1.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objagentmaster.fetchSUBAGENTDETAIL(Session["cust_id"].ToString(), TextBox3.Text, DropDownList2.Text, TextBox1.Text);
            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}