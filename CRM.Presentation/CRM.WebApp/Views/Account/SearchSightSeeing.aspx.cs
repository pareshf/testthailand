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
    public partial class SearchSightSeeing : System.Web.UI.Page
    {
        CRM.DataAccess.Account.SearchHotelServiceVoucher ObjServiceVoucher = new CRM.DataAccess.Account.SearchHotelServiceVoucher();

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
                DataSet ds = ObjServiceVoucher.fetch_Invoice_no("FETCH_INVOICE_NO_FOR_SERVICE_VOUCHER_DETAIL");
                binddropdownlist(drp_invoice_no, ds);

                DataSet ds1 = ObjServiceVoucher.fetch_Invoice_no("FETCH_CITY_NAME_FOR_HOTEL_SERVICE_VOUCHER");
                binddropdownlist(drp_city, ds1);


                DataSet ds2 = ObjServiceVoucher.fetch_Invoice_no("FETCH_DATA_FOR_SIGHT_SEEING_SERVICE_VOUCHER");
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
            DataSet ds3 = ObjServiceVoucher.fetchSightSeeingVoucher("FETCH_DATA_FOR_SIGHT_SEEING_SERVICE_VOUCHER_GRID", drp_invoice_no.Text, drp_city.Text,txtClient.Text,txtAgent.Text,txtdate.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds3.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int newindex = e.NewSelectedIndex;

            string invoice = GV_Result.Rows[newindex].Cells[1].Text;
            string city = GV_Result.Rows[newindex].Cells[4].Text;
            Response.Redirect("~/Views/Account/SightSeeingVoucher.aspx?IN=" + invoice + "&CT=" + city);
        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds4 = ObjServiceVoucher.fetchSightSeeingVoucher("FETCH_DATA_FOR_SIGHT_SEEING_SERVICE_VOUCHER_GRID", drp_invoice_no.Text, drp_city.Text, txtClient.Text, txtAgent.Text, txtdate.Text);

            GV_Result.DataSource = ds4;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}