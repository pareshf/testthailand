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
    public partial class SearchEmail : System.Web.UI.Page
    {
        EmailSettingsStoredProcedure objEmailSettingsStoredProcedure = new EmailSettingsStoredProcedure();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds1 = objEmailSettingsStoredProcedure.fetch_voucher_type("FETCH_EVENT_NAME_FOR_EMAIL");
                binddropdownlist(drp_invoice_no, ds1);

                DataSet ds = objEmailSettingsStoredProcedure.search_email("SEARCH_RECORDS_FOR_EMAIL_GENERATION",drp_invoice_no.Text);
                GV_Result.DataSource = ds;
                GV_Result.DataBind();
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
            
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");

            DataSet ds = objEmailSettingsStoredProcedure.search_email("SEARCH_RECORDS_FOR_EMAIL_GENERATION", drp_invoice_no.Text);
            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();

        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            //  //   object PID=GV_Result.DataKeys[newindex].Value;
            //  //GV_Result.Rows[newindex].Cells[1].Visible = true;
            string invoiceno = GV_Result.Rows[newindex].Cells[5].Text;
            //string vouchertype = GV_Result.Rows[newindex].Cells[4].Text;
            //  string usrid = Session["usersid"].ToString();
            ////  DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
            //  for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //  {
            //      if (quoteid == ds.Tables[0].Rows[i]["QUOTE_ID"].ToString() && tourid == ds.Tables[0].Rows[i]["TOUR_ID"].ToString())
            //      {
            //          Session["editorderstatus"] = ds.Tables[0].Rows[i]["ORDER_STATUS"].ToString();
            //      }
            //  }

            Response.Redirect("~/Views/EmailSettings/CreateEmail.aspx?ID=" + invoiceno );
        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //  r.SelectedValue = "1";
        }
        #endregion

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            //DataSet ds = ObjSearchPurchaseVoucher.fetchallDatasearch("FETCH_ALL_PURCHASE_VOUCHER", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);
            DataSet ds = objEmailSettingsStoredProcedure.search_email("SEARCH_RECORDS_FOR_EMAIL_GENERATION", drp_invoice_no.Text);
            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}