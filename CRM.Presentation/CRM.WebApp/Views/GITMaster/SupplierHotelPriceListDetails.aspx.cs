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
    public partial class SupplierHotelPriceListDetails : System.Web.UI.Page
    {
        SupplierHotelPriceListDetailsDal objsupplierPriceListDetails = new SupplierHotelPriceListDetailsDal();

        int supplierid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds1 = objsupplierPriceListDetails.fetchComboData("GET_ROOM_TYPE_FOR_GIT");
                DataSet ds2 = objsupplierPriceListDetails.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");

                
                binddropdownlist(drpRoomType, ds1);
                binddropdownlist(drpStatus, ds2);
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierid = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                DataSet ds = objsupplierPriceListDetails.fetchSupplierPriceListData(supplierid,drpRoomType.Text,drpStatus.Text); //// Bind With Supplier SR.No
                GV_HotelPriceList.DataSource = ds;
                GV_HotelPriceList.DataBind();
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
            string supplierPRICEID = GV_HotelPriceList.Rows[newindex].Cells[1].Text;/// Price list Primary key 
            
            
            Label lbl = new Label();
            lbl = (Label)GV_HotelPriceList.Rows[newindex].FindControl("lblSUPPLIER_SR_NO");
            int suppid = supplierid;
           // string suppliersrno  = GV_HotelPriceList.Rows[newindex].Cells[2].Text;
            Response.Redirect("~/Views/GITMaster/SupplierHotelPriceListMaster.aspx?SUPPLIERPICELISTID=" + supplierPRICEID + "&SUPPLIERID=" + suppid); //Price list Primary key         
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_HotelPriceList.PageIndex = e.NewPageIndex;

            DataSet ds = objsupplierPriceListDetails.fetchSupplierPriceListData(supplierid, drpRoomType.Text, drpStatus.Text);
            GV_HotelPriceList.DataSource = ds;
            GV_HotelPriceList.DataBind();
            upPriceList.Update();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
             
            Response.Redirect("~/Views/GITMaster/SupplierHotelPriceListMaster.aspx?SUPPLIERID=" + supplierid);
        }

        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            upPriceList.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {

            string USERID = Session["usersid"].ToString();
            DataSet ds = objsupplierPriceListDetails.fetchSupplierPriceListData(supplierid, drpRoomType.Text, drpStatus.Text); 


            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_HotelPriceList.DataSource = ds.Tables[0];
            GV_HotelPriceList.DataBind();
            upPriceList.Update();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/AllSupplier.aspx"); // redirect with Supplier id             
        }
    }
}