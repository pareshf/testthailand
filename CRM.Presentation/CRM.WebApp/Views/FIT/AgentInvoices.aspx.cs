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

namespace CRM.WebApp.Views.FIT
{
    public partial class AgentInvoices : System.Web.UI.Page
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 240);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                //Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objgenerateInvoiceStoredProcedure.fetchallData_login("FETCH_DATA_FOR_INVOICE_GRID_LOGIN",Session["usersid"].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    DataSet dsval = objBookingFitStoreProcedure.fetchComboData_LOGIN("FETCH_ALL_INVOICE_NO_LOGIN", Session["usersid"].ToString());
                    binddropdownlist(DropDownList2, dsval);
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
            
        }
      
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            
            string quoteid = GV_Result.Rows[newindex].Cells[1].Text;
            string tourid = GV_Result.Rows[newindex].Cells[7].Text;
            
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
            DataSet ds = objgenerateInvoiceStoredProcedure.fetchallDatasearch_login("FETCH_DATA_FOR_INVOICE_GRID_SERACH", txtrefrence.Text, DropDownList2.Text,TextBox1.Text, TextBox2.Text, TextBox3.Text,USERID);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
    
    }
}