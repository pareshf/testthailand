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
    public partial class AllBookingsToBeReconfirmed : System.Web.UI.Page
    {
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 227);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED", usrid);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    //   Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    //DataSet ds0 = objfitquote.fillagentdropdown("FETCH_AGENT_NAME");
                    //binddropdownlist(DropDownList1, ds0);
                    DataSet dsval = objBookingFitStoreProcedure.fetchComboData("FETCH_ORDER_STATUS");
                    binddropdownlist(DropDownList2, dsval);
                    DropDownList2.SelectedValue = "To Be Reconfirmed";
                    //DropDownList2.Enabled = false;
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
            //   object PID=GV_Result.DataKeys[newindex].Value;
            //GV_Result.Rows[newindex].Cells[1].Visible = true;
            string quoteid = GV_Result.Rows[newindex].Cells[1].Text;
            string tourid = GV_Result.Rows[newindex].Cells[9].Text;
            string usrid = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED", usrid);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (quoteid == ds.Tables[0].Rows[i]["QUOTE_ID"].ToString() && tourid == ds.Tables[0].Rows[i]["TOUR_ID"].ToString())
                {
                    Session["editorderstatus"] = ds.Tables[0].Rows[i]["ORDER_STATUS"].ToString();
                }
            }

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
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallDatasearch("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_SEARCH", USERID, txtrefrence.Text, TextBox5.Text, TextBox1.Text, DropDownList2.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objfitquote.fetchallDatasearch("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED_SEARCH", USERID, txtrefrence.Text, TextBox5.Text, TextBox1.Text, DropDownList2.Text, TextBox2.Text, TextBox3.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}