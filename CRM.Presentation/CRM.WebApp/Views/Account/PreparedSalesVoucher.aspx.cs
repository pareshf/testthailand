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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.Account
{
    public partial class PreparedSalesVoucher : System.Web.UI.Page
    {
        CRM.DataAccess.Account.SalesVoucherStatus objstatus = new CRM.DataAccess.Account.SalesVoucherStatus();

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 256);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                // Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet ds7 = objstatus.fetch_voucher_type("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drp_invoice_no, ds7);

                DataSet ds1 = objstatus.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");

                drp_voucher_status.Text = ds1.Tables[0].Rows[1]["AutoSearchResult"].ToString();
                drp_voucher_status.Enabled = false;
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds = objstatus.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                drp_voucher_type.Text = ds.Tables[0].Rows[0]["AutoSearchResult"].ToString();
                drp_voucher_type.Enabled = false;
                binddropdownlist(drp_voucher_type, ds);

                //for (int i = drp_voucher_type.Items.Count - 1; i > 0; i--)
                //{
                //    ListItem item = drp_voucher_type.Items[i];
                //    if (item.ToString() == "SALES")
                //    {

                //    }
                //    else
                //    {
                //        drp_voucher_type.Items.Remove(item);
                //    }
                //}

                DataSet ds2 = objstatus.fetch_voucher_status("GET_SALES_VOUCHER_DATA", drp_voucher_status.Text);
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
        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");

            Updateconfirm.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {

            DataSet ds = objstatus.fetchallDatasearch("FETCH_ALL_SALES_PURCHASE_DATA_BY_STATUS", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);
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
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objstatus.fetchallDatasearch("FETCH_ALL_SALES_PURCHASE_DATA_BY_STATUS", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}