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


namespace CRM.WebApp.Views.FIT
{
    public partial class AgentClosedInvoice : System.Web.UI.Page
    {
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        CRM.DataAccess.FIT.AgentBookingClosed objclosedInvoice = new CRM.DataAccess.FIT.AgentBookingClosed();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 226);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                //Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objclosedInvoice.fetchallData("FETCH_DATA_FOR_CLOSED_BOOKING_1", usrid);
                if (ds.Tables[1].Rows.Count == 0)
                {
                }
                else
                {
                    

                    GV_ClosedBooking.DataSource = ds.Tables[1];
                    GV_ClosedBooking.DataBind();
                    

                }
                DataSet dsval = objclosedInvoice.fetchComboData("FETCH_ORDER_STATUS");
                binddropdownlist(drpstatus, dsval);
                drpstatus.SelectedValue = "Reconfirmed";
                drpstatus.Enabled = false;
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
        
            string quoteid = GV_ClosedBooking.Rows[newindex].Cells[1].Text;
            string tourid = GV_ClosedBooking.Rows[newindex].Cells[9].Text;
           Session["editorderstatus"]=  GV_ClosedBooking.Rows[newindex].Cells[5].Text;
            

            Response.Redirect("~/Views/FIT/BookingFit.aspx?TOURID=" + tourid + "&QUOTEID=" + quoteid);
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
            if (txtquotaion.Text == "")
            {
                txtquotaion.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objclosedInvoice.fetchallDatasearch("FETCH_DATA_FOR_CLOSED_BOOKING_GRID_SEARCH", USERID,txtquotaion.Text, txttour.Text, drpstatus.Text, txtfromdate.Text, txttodate.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_ClosedBooking.DataSource = ds.Tables[1];
            GV_ClosedBooking.DataBind();
            Updateconfirm.Update();

        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_ClosedBooking.PageIndex = e.NewPageIndex;
            if (txtquotaion.Text == "")
            {
                txtquotaion.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objclosedInvoice.fetchallDatasearch("FETCH_DATA_FOR_CLOSED_BOOKING_GRID_SEARCH", USERID,txtquotaion.Text,txttour.Text,drpstatus.Text,txtfromdate.Text,txttodate.Text);

            GV_ClosedBooking.DataSource = ds;
            GV_ClosedBooking.DataBind();
            Updateconfirm.Update();
        }
    }
}