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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;



namespace CRM.WebApp.Views.Account
{
    public partial class PurchaseVoucherApproved : System.Web.UI.Page
    {

        SearchPurchaseVoucherStoredProcedure ObjSearchPurchaseVoucher = new SearchPurchaseVoucherStoredProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 269);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
               // Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds77 = ObjSearchPurchaseVoucher.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drp_sales_invoice, ds77);

                DataSet ds7 = ObjSearchPurchaseVoucher.fetch_invoice_no("FETCH_ALL_INVOICE_NO_FOR_PURCHASE", "PURCHASE");
                binddropdownlist(drp_invoice_no, ds7);

                DataSet ds1 = ObjSearchPurchaseVoucher.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                drp_voucher_status.Text = ds1.Tables[0].Rows[2]["AutoSearchResult"].ToString();
                drp_voucher_status.Enabled = false;
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds = ObjSearchPurchaseVoucher.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                drp_voucher_type.Text = ds.Tables[0].Rows[7]["AutoSearchResult"].ToString();
                drp_voucher_type.Enabled = false;
                binddropdownlist(drp_voucher_type, ds);

                //for (int i = drp_voucher_type.Items.Count - 1; i > 0; i--)
                //{
                //    ListItem item = drp_voucher_type.Items[i];
                //    if (item.ToString() == "PURCHASE")
                //    {

                //    }
                //    else
                //    {
                //        drp_voucher_type.Items.Remove(item);
                //    }
                //}

                DataSet ds2 = ObjSearchPurchaseVoucher.fetchallDatasearch("FETCH_ALL_PURCHASE_VOUCHER_BY_STATUS", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, drp_sales_invoice.Text);
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
            DataSet ds = ObjSearchPurchaseVoucher.fetchallDatasearch("FETCH_ALL_PURCHASE_VOUCHER_BY_STATUS", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, drp_sales_invoice.Text);
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
            string invoiceno = GV_Result.Rows[newindex].Cells[2].Text;
            string vouchertype = GV_Result.Rows[newindex].Cells[4].Text;
           
            Response.Redirect("~/Views/Account/AccountsVoucher.aspx?IV=" + invoiceno + "&VT=" + vouchertype);
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

            DataSet ds = ObjSearchPurchaseVoucher.fetchallDatasearch("FETCH_ALL_PURCHASE_VOUCHER_BY_STATUS", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, drp_sales_invoice.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}