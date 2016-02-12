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
    public partial class AgentSightSeeingVoucher : System.Web.UI.Page
    {
        SearchPurchaseVoucherStoredProcedure ObjSearchPurchaseVoucher = new SearchPurchaseVoucherStoredProcedure();
        CRM.DataAccess.Account.AgentPurchaseVoucher objAgentpurchaseVoucher = new CRM.DataAccess.Account.AgentPurchaseVoucher();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet ds = ObjSearchPurchaseVoucher.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drp_voucher_type, ds);

                DataSet ds1 = ObjSearchPurchaseVoucher.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds7 = ObjSearchPurchaseVoucher.fetch_invoice_no("FETCH_ALL_INVOICE_NO_FOR_PURCHASE", "PURCHASE");
                binddropdownlist(drp_invoice_no, ds7);


                DataSet ds2 = objAgentpurchaseVoucher.fetchAgentVoucher("FETCH_AGENT_SIGHTSEEING_VOUCHER", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, int.Parse(Session["cust_id"].ToString()));
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
            DataSet ds3 = objAgentpurchaseVoucher.fetchAgentVoucher("FETCH_AGENT_SIGHTSEEING_VOUCHER", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, int.Parse(Session["cust_id"].ToString()));
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds3.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds4 = objAgentpurchaseVoucher.fetchAgentVoucher("FETCH_AGENT_SIGHTSEEING_VOUCHER", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, TextBox2.Text, TextBox3.Text, int.Parse(Session["cust_id"].ToString()));

            GV_Result.DataSource = ds4;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}