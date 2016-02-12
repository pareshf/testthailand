using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.Account;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Globalization;
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.WebApp.Views.GITMaster
{
    public partial class SightSeeingPriceListDetails : System.Web.UI.Page
    {
        SightSeeingPriceListDetailsDal objSightSeeingPriceList = new SightSeeingPriceListDetailsDal();

        int supplierid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds2 = objSightSeeingPriceList.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");

                binddropdownlist(drpStatus, ds2);
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierid = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                DataSet ds = objSightSeeingPriceList.fetchSightseeingPriceListData(supplierid,txtpackname.Text,drpStatus.Text); //// Bind With Supplier SR.No
                GV_SightSeeingPriceList.DataSource = ds;
                GV_SightSeeingPriceList.DataBind();
            }
            else
            {
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierid = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
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



        }
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            string SightSeeingPriceListId = GV_SightSeeingPriceList.Rows[newindex].Cells[1].Text; /// Price list Primary key 
            int suppid = supplierid;
            Response.Redirect("~/Views/GITMaster/SightSeeingPriceListMaster.aspx?SightSeeingPriceListId=" + SightSeeingPriceListId + "&SUPPLIERID=" + suppid); //Price list Primary key         
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SightSeeingPriceList.PageIndex = e.NewPageIndex;

            DataSet ds = objSightSeeingPriceList.fetchSightseeingPriceListData(supplierid,txtpackname.Text,drpStatus.Text);
            GV_SightSeeingPriceList.DataSource = ds;
            GV_SightSeeingPriceList.DataBind();
            upSightSeeingPriceList.Update();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/SightSeeingPriceListMaster.aspx?SUPPLIERID=" + supplierid);
        }

        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            upSightSeeingPriceList.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {

            string USERID = Session["usersid"].ToString();
            DataSet ds = objSightSeeingPriceList.fetchSightseeingPriceListData(supplierid,txtpackname.Text,drpStatus.Text);


            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_SightSeeingPriceList.DataSource = ds.Tables[0];
            GV_SightSeeingPriceList.DataBind();
            upSightSeeingPriceList.Update();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/AllSupplier.aspx"); // redirect with Supplier id             
        }
    }
}