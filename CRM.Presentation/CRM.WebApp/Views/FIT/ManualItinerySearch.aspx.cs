using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Linq;

namespace CRM.WebApp.Views.FIT
{
    public partial class ManualItinerySearch : System.Web.UI.Page
    {
        ManualItineryDa objmanualitinery = new ManualItineryDa();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objmanualitinery.CommonSp("FETCH_AGENT_COMPANY_NAME_WITH_ID");
                binddropdownlistmain(drpAgent, ds);
                DataSet ds1 = objmanualitinery.fetchallDatasearch("FETCH_MAIN_MANUAL_ITINERY_FOR_GRID", "0", "", "", "");
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    GV_Result.DataSource = ds1.Tables[0];
                    GV_Result.DataBind();
                }
            }
        }

        protected void drpAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = drpAgent.Text;
            if (drpAgent.Text != "")
            {
                DataSet ds = objmanualitinery.fetchInvoice("FETCH_INVOICE_FOR_MANUAL_ITINERY", int.Parse(drpAgent.SelectedValue.ToString()));
                binddropdownlist(drpInvoice, ds);
            }
            Updateconfirm.Update();
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }

        protected void binddropdownlistmain(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataValueField = "CUST_ID";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string manual = GV_Result.Rows[newindex].Cells[1].Text;

            Response.Redirect("~/Views/FIT/ManualItinery.aspx?manual_itinery_id=" + manual);
            
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
            DataSet ds;
            if (drpAgent.SelectedValue != "")
            {
                 ds = objmanualitinery.fetchallDatasearch("FETCH_MAIN_MANUAL_ITINERY_FOR_GRID", Convert.ToString(drpAgent.SelectedValue), drpInvoice.Text, txtarrival.Text, txtDeparture.Text);
            }
            else
            {
                 ds = objmanualitinery.fetchallDatasearch("FETCH_MAIN_MANUAL_ITINERY_FOR_GRID", "0", drpInvoice.Text, txtarrival.Text, txtDeparture.Text);
            }
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
            DataSet ds;
            if (drpAgent.SelectedValue != "")
            {
                ds = objmanualitinery.fetchallDatasearch("FETCH_MAIN_MANUAL_ITINERY_FOR_GRID", Convert.ToString(drpAgent.SelectedValue), drpInvoice.Text, txtarrival.Text, txtDeparture.Text);
            }
            else
            {
                ds = objmanualitinery.fetchallDatasearch("FETCH_MAIN_MANUAL_ITINERY_FOR_GRID", "0", drpInvoice.Text, txtarrival.Text, txtDeparture.Text);
            }

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}