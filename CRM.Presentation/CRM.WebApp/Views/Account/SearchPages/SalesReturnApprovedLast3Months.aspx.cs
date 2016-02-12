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

namespace CRM.WebApp.Views.Account.SearchPages
{
    public partial class SalesReturnApprovedLast3Months : System.Web.UI.Page
    {

        SearchPurchaseInvoiceStoredProcedure objSearchPurchaseInvoice = new SearchPurchaseInvoiceStoredProcedure();

        SearchCreditNoteStoredProcedure objSearchCreditNoteStoredProcedure = new SearchCreditNoteStoredProcedure();

        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        public string d1;
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 272);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds7 = objSearchPurchaseInvoice.fetch_supplier_type("FETCH_ALL_INVOICE_SALES_CANCELATION");
                binddropdownlist(drp_invoice_no, ds7);

                DataSet ds8 = objSearchPurchaseInvoice.fetch_supplier_type("FETCH_VOUCHER_TYPE");
                drp_Voucher_type.Text = ds8.Tables[0].Rows[9]["AutoSearchResult"].ToString();
                drp_Voucher_type.Enabled = false;
                binddropdownlist(drp_Voucher_type, ds8);

                DataSet ds9 = objSearchPurchaseInvoice.fetch_supplier_type("FETCH_VOUCHER_STATUS_ID");
                drp_Voucher_status.Text = ds9.Tables[0].Rows[2]["AutoSearchResult"].ToString();
                drp_Voucher_status.Enabled = false;
                binddropdownlist(drp_Voucher_status, ds9);


                DateTime currentDateWithCurrentTime = System.DateTime.Now;
                d1 = currentDateWithCurrentTime.ToString("dd/MM/yyyy");
                DateTime datebefore3Months = currentDateWithCurrentTime.AddMonths(-3);
                string date3 = datebefore3Months.ToString("dd/MM/yyyy");
                ViewState["Duration_From"] = date3;
                DataSet ds2 = objSearchCreditNoteStoredProcedure.fetchallDatasearch("SEARCH_SALES_RETURN_INVOICE", drp_invoice_no.Text, ViewState["Duration_From"].ToString(), d1, drp_Voucher_type.Text, drp_Voucher_status.Text);

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
             if (TextBox3.Text == "")
             {
                DateTime currentDateWithCurrentTime = System.DateTime.Now;

                TextBox3.Text = currentDateWithCurrentTime.ToString("dd/MM/yyyy");
             }
             if (TextBox2.Text != "")
             {
                 
                 DataSet ds = objSearchCreditNoteStoredProcedure.fetchallDatasearch("SEARCH_SALES_RETURN_INVOICE", drp_invoice_no.Text, TextBox2.Text, TextBox3.Text, drp_Voucher_type.Text, drp_Voucher_status.Text);
                 pnlMainHead.Attributes.Add("style", "display:none");
                 Button4.Attributes.Add("style", "display:none");
                 Button3.Attributes.Add("style", "display");
                 GV_Result.DataSource = ds.Tables[0];
                 GV_Result.DataBind();
                 Updateconfirm.Update();
             }
             else
             {

                 DateTime currentDateWithCurrentTime = System.DateTime.Now;
                 DateTime datebefore3Months = currentDateWithCurrentTime.AddMonths(-3);
                 string date3 = datebefore3Months.ToString("dd/MM/yyyy");
                 ViewState["Duration_From"] = date3;

                 DataSet ds = objSearchCreditNoteStoredProcedure.fetchallDatasearch("SEARCH_SALES_RETURN_INVOICE", drp_invoice_no.Text, ViewState["Duration_From"].ToString(), TextBox3.Text, drp_Voucher_type.Text, drp_Voucher_status.Text);
                 pnlMainHead.Attributes.Add("style", "display:none");
                 Button4.Attributes.Add("style", "display:none");
                 Button3.Attributes.Add("style", "display");
                 GV_Result.DataSource = ds.Tables[0];
                 GV_Result.DataBind();
                 Updateconfirm.Update();
             }

        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //int newindex = e.NewSelectedIndex;

            //string invoiceno = GV_Result.Rows[newindex].Cells[1].Text;
            //string vouchertype = GV_Result.Rows[newindex].Cells[2].Text;
            //Response.Redirect("~/Views/Account/DebitCreditNote.aspx?IV=" + invoiceno + "&VT=" + vouchertype);
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objSearchCreditNoteStoredProcedure.fetchallDatasearch("SEARCH_SALES_RETURN_INVOICE", drp_invoice_no.Text, TextBox2.Text, TextBox3.Text, drp_Voucher_type.Text, drp_Voucher_status.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}