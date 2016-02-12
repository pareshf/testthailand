using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
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
    public partial class AllInvoice : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.Account.GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 255);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objgenerateInvoiceStoredProcedure.fetchallData("FETCH_DATA_FOR_INVOICE_GRID");
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    //   Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    
                    Bindcompanyname();
                    bindinvoicedropdown();
                }
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

                    DropDownList2_new.Items.Clear();
                    DropDownList2_new.DataSource = dsval;
                    DropDownList2_new.DataTextField = "AutoSearchResult";
                    DropDownList2_new.DataBind();
                    DropDownList2_new.Items.Insert(0, new RadComboBoxItem("", "0"));
                }
                else
                {
                    DropDownList2_new.Items.Clear();
                    DropDownList2_new.DataSource = null;
                    DropDownList2_new.Items.Insert(0, new RadComboBoxItem("", "0"));
                }
            }
        
        }
        
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            //r.SelectedValue = "0";
        }
        
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string quoteid = GV_Result.Rows[newindex].Cells[1].Text;
            string tourid = GV_Result.Rows[newindex].Cells[7].Text;

            DataSet ds = objgenerateInvoiceStoredProcedure.checkInvoiceType(int.Parse(tourid));

            if (ds.Tables[0].Rows[0]["IS_THB_INVOICE"].ToString() == "True")
            {
                Response.Redirect("~/Views/Account/THBInvoice.aspx?TOURID=" + tourid);
            }
            else if (ds.Tables[0].Rows[0]["IS_GIT_INVOICE"].ToString() == "True")
            {
                Response.Redirect("~/Views/GIT/AgentInvoicesGIT.aspx?TOURID=" + tourid + "&QUOTEID=" + quoteid);
            }
            else
            {

                Response.Redirect("~/Views/Account/GenerateInvoice.aspx?TOURID=" + tourid + "&QUOTEID=" + quoteid);
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
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objgenerateInvoiceStoredProcedure.fetchallDatasearch("FETCH_DATA_FOR_INVOICE_GRID_SERACH", Convert.ToString(drpAgent.SelectedValue), txtrefrence.Text, DropDownList2_new.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
      
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objgenerateInvoiceStoredProcedure.fetchallDatasearch("FETCH_DATA_FOR_INVOICE_GRID_SERACH", Convert.ToString(drpAgent.SelectedValue), txtrefrence.Text, DropDownList2_new.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text);

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