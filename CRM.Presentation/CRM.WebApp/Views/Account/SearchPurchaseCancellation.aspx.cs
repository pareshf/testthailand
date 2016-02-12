﻿using System;
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
    public partial class SearchPurchaseCancellation : System.Web.UI.Page
    {
        CRM.DataAccess.Account.SearchPurchaseCancellation objSearchPurchaseCancellation = new CRM.DataAccess.Account.SearchPurchaseCancellation();

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
                DataSet ds0 = objSearchPurchaseCancellation.fetch_voucher_type("FETCH_INVOICE_NO_FOR_PURCHASE_CANCELLATION");
                binddropdownlist(drp_invoice_no, ds0);

                DataSet ds2 = objSearchPurchaseCancellation.fetchallDatasearch("SEARCH_PURCHASE_CANCELLATION", drp_invoice_no.Text, TextBox2.Text, TextBox3.Text);
                //DataSet ds2 = objSearchReceiptVouchersStoredProcedure.fetch_voucher_type("SEARCH_RECORDS_SALES_PURCHASE");
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
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //  r.SelectedValue = "1";
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
            DataSet ds = objSearchPurchaseCancellation.fetchallDatasearch("SEARCH_PURCHASE_CANCELLATION", drp_invoice_no.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string invoiceno = GV_Result.Rows[newindex].Cells[1].Text;
            string vouchertype = GV_Result.Rows[newindex].Cells[2].Text;


            Response.Redirect("~/Views/Account/PurchaseCancellation.aspx?IV=" + invoiceno + "&VT=" + vouchertype);
        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objSearchPurchaseCancellation.fetchallDatasearch("SEARCH_PURCHASE_CANCELLATION", drp_invoice_no.Text, TextBox2.Text, TextBox3.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}