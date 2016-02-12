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
using CRM.DataAccess.FIT;

namespace CRM.WebApp.Views.Account
{
    public partial class SearchReceiptVouchers : System.Web.UI.Page
    {
        SearchReceiptVouchersStoredProcedure objSearchReceiptVouchersStoredProcedure = new SearchReceiptVouchersStoredProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 258);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindcompanyname();
                bindinvoicedropdown();

                DataSet ds1 = objSearchReceiptVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                binddropdownlist(drp_voucher_status, ds1);

                DataSet ds = objSearchReceiptVouchersStoredProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                binddropdownlist(drp_voucher_type, ds);

                for (int i = drp_voucher_type.Items.Count - 1; i > 0; i--)
                {
                    ListItem item = drp_voucher_type.Items[i];
                    if (item.ToString() == "RECEIPT" || item.ToString() == "PAYMENT")
                    {

                    }
                    else
                    {
                        drp_voucher_type.Items.Remove(item);
                    }
                }

                //DataSet ds2 = objSearchReceiptVouchersStoredProcedure.fetchallDatasearch("SEARCH_RECEIPT_VOUCHER_NEW", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, drp_gl_code.Text, TextBox2.Text, TextBox3.Text);
                DataSet ds2 = objSearchReceiptVouchersStoredProcedure.fetchallDatasearchNew("SEARCH_RECEIPT_VOUCHER_NEW", Convert.ToString(drpAgent.SelectedValue), drp_invoice_no_new.Text, drp_voucher_status.Text, drp_voucher_type.Text, drp_gl_code.Text, TextBox2.Text, TextBox3.Text);
                //DataSet ds2 = objSearchReceiptVouchersStoredProcedure.fetch_voucher_type("SEARCH_RECORDS_SALES_PURCHASE");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                   

                    GV_Result.DataSource = ds2.Tables[0];
                    GV_Result.DataBind();
                   
                }
                DataSet ds5 = objSearchReceiptVouchersStoredProcedure.fetch_voucher_type("FETCH_ALL_GL_CODE");
                binddropdownlist(drp_gl_code, ds5);

            }
        }

        protected void bindinvoicedropdown()
        {

            DataSet dsval = objBookingFitStoreProcedure.fetchinvoicompanywise("FETCH_ALL_INVOICE_NO_COMPANY_WISE", Convert.ToString(drpAgent.SelectedValue));

            if (dsval != null)
            {
                //binddropdownlist(DropDownList2, dsval);

                if (dsval.Tables[0].Rows.Count > 0)
                {

                    drp_invoice_no_new.Items.Clear();
                    drp_invoice_no_new.DataSource = dsval;
                    drp_invoice_no_new.DataTextField = "AutoSearchResult";
                    drp_invoice_no_new.DataBind();
                    drp_invoice_no_new.Items.Insert(0, new RadComboBoxItem("", "0"));
                }
                else
                {
                    drp_invoice_no_new.Items.Clear();
                    drp_invoice_no_new.DataSource = null;
                    drp_invoice_no_new.Items.Insert(0, new RadComboBoxItem("", "0"));
                }
            }

        }


        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");

            //DataSet ds2 = objSearchSalesVoucherStoredProcedure.fetch_voucher_type("SEARCH_RECORDS_SALES_PURCHASE");
            //if (ds2.Tables[0].Rows.Count == 0)
            //{
            //}
            //else
            //{
            //    //   Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

            //    GV_Result.DataSource = ds2.Tables[0];
            //    GV_Result.DataBind();
            //    //DataSet ds0 = objfitquote.fillagentdropdown("FETCH_AGENT_NAME");
            //    //binddropdownlist(DropDownList1, ds0);
            //    //DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
            //    //binddropdownlist(DropDownList2, dsval);
            //    //DropDownList2.SelectedValue = "Booked";
            //    //DropDownList2.Enabled = false;
            //}

            Updateconfirm.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {
            //if (txtrefrence.Text == "")
            //{
            //    txtrefrence.Text = "0";
            //}
            //string USERID = Session["usersid"].ToString();
          //  DataSet ds = objSearchReceiptVouchersStoredProcedure.fetchallDatasearch("SEARCH_RECEIPT_VOUCHER_NEW", drp_invoice_no.Text, drp_voucher_status.Text, drp_voucher_type.Text, drp_gl_code.Text, TextBox2.Text, TextBox3.Text);
            DataSet ds = objSearchReceiptVouchersStoredProcedure.fetchallDatasearchNew("SEARCH_RECEIPT_VOUCHER_NEW", Convert.ToString(drpAgent.SelectedValue), drp_invoice_no_new.Text, drp_voucher_status.Text, drp_voucher_type.Text, drp_gl_code.Text, TextBox2.Text, TextBox3.Text);
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
            DataSet ds = objSearchReceiptVouchersStoredProcedure.fetch_is_THB("FECTH_IS_THB", voucherno);


            string ISTHB = ds.Tables[0].Rows[0][0].ToString();

            if (ISTHB == "True")
            {
                Response.Redirect("~/Views/Account/THBreceipt.aspx?VN=" + voucherno);
            }
            else
            {
                Response.Redirect("~/Views/Account/Vouchers.aspx?VN=" + voucherno);
            }
            //int newindex = e.NewSelectedIndex;
            ////  //   object PID=GV_Result.DataKeys[newindex].Value;
            ////  //GV_Result.Rows[newindex].Cells[1].Visible = true;
            //string voucherno = GV_Result.Rows[newindex].Cells[1].Text;
            ////string vouchertype = GV_Result.Rows[newindex].Cells[4].Text;
            ////  string usrid = Session["usersid"].ToString();
            //////  DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
            ////  for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ////  {
            ////      if (quoteid == ds.Tables[0].Rows[i]["QUOTE_ID"].ToString() && tourid == ds.Tables[0].Rows[i]["TOUR_ID"].ToString())
            ////      {
            ////          Session["editorderstatus"] = ds.Tables[0].Rows[i]["ORDER_STATUS"].ToString();
            ////      }
            ////  }

           // Response.Redirect("~/Views/Account/Vouchers.aspx?VN=" + voucherno);
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

            DataSet ds = objSearchReceiptVouchersStoredProcedure.fetchallDatasearchNew("SEARCH_RECEIPT_VOUCHER_NEW",Convert.ToString(drpAgent.SelectedValue) ,drp_invoice_no_new.Text, drp_voucher_status.Text, drp_voucher_type.Text, drp_gl_code.Text, TextBox2.Text, TextBox3.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }

        protected void Bindcompanyname()
        {

            DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME_WITH_ID");

            drpAgent.Items.Clear();

            drpAgent.DataTextField = "AutoSearchResult";
            drpAgent.DataValueField = "CUST_ID";
            drpAgent.DataSource = ds3;
            drpAgent.DataBind();
            drpAgent.Items.Insert(0, new RadComboBoxItem("Please Select", "0"));

        }

        protected void drpAgent_SelectedIndexChanged(object sender, EventArgs e)
        {

            bindinvoicedropdown();
            Updateconfirm.Update();


        }
    }
}