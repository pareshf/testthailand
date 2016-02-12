using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;


namespace CRM.WebApp.Views.Account
{
    public partial class SearchJournal : System.Web.UI.Page
    {
        CRM.DataAccess.Account.SearchAllJournal objJournal = new CRM.DataAccess.Account.SearchAllJournal();

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

                DataSet ds1 = objJournal.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds = objJournal.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drp_voucher_type, ds);

                DataSet ds2 = objJournal.fetchallDatasearch("FETCH_ALL_JOURNAL_VOUCHER", drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);
                if (ds2.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                   
                    GV_Result.DataSource = ds2.Tables[0];
                    GV_Result.DataBind();
                    
                }
            }
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

        protected void searchnow_onclick(object sender, EventArgs e)
        {
            DataSet ds = objJournal.fetchallDatasearch("FETCH_ALL_JOURNAL_VOUCHER", drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");

            Updateconfirm.Update();
        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds2 = objJournal.fetchallDatasearch("FETCH_ALL_JOURNAL_VOUCHER", drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);
            GV_Result.DataSource = ds2;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string seqno = GV_Result.Rows[newindex].Cells[2].Text;
            string vouchertype = GV_Result.Rows[newindex].Cells[5].Text;

            Response.Redirect("~/Views/Account/Journal.aspx?IV=" + seqno + "&VT=" + vouchertype);

        }
    }
}