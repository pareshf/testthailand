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
    public partial class SearchPurchaseInvoiceVoucher : System.Web.UI.Page
    {
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        PurchaseHeader objPurchaseHeader = new PurchaseHeader();

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 267);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                DataSet ds8 = objPurchaseHeader.commonSp("GET_ALL_PURCHASE_INVOICE_NO");
                binddropdownlist(drp_invoice_no, ds8);

                DataSet ds6 = objPurchaseHeader.commonSp("FETCH_SUPPLIER_TYPE");
                binddropdownlist(drp_supplier_type, ds6);

                DataSet ds7 = objPurchaseHeader.commonSp("FETCH_ALL_INVOICE_NO");
                binddropdownlist(drp_sales_invocie_no, ds7);

                DataSet ds1 = objPurchaseHeader.commonSp("FETCH_VOUCHER_STATUS_ID");
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds3 = objPurchaseHeader.commonSp("FETCH_ALL_SUPPLIER");
                binddropdownlist(drp_supplier_name, ds3);

                DataSet ds2 = objPurchaseHeader.fetchallDatasearch("SEARCH_PURCHASE_HEADER_NEW", drp_invoice_no.Text, drp_voucher_status.Text, TextBox2.Text, TextBox3.Text, drp_sales_invocie_no.Text, drp_supplier_type.Text, drp_supplier_name.Text);
                
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
            DataSet ds = objPurchaseHeader.fetchallDatasearch("SEARCH_PURCHASE_HEADER", drp_invoice_no.Text, drp_voucher_status.Text, TextBox2.Text, TextBox3.Text, drp_sales_invocie_no.Text, drp_supplier_type.Text, drp_supplier_name.Text);
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
            
            string voucherno = GV_Result.Rows[newindex].Cells[1].Text;
            
            Response.Redirect("~/Views/Account/PurchaseInvoiceVoucher.aspx?IV=" + voucherno);
        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
           
        }
        #endregion

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

             DataSet ds = objPurchaseHeader.fetchallDatasearch("SEARCH_PURCHASE_HEADER", drp_invoice_no.Text, drp_voucher_status.Text, TextBox2.Text, TextBox3.Text, drp_sales_invocie_no.Text, drp_supplier_type.Text, drp_supplier_name.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}