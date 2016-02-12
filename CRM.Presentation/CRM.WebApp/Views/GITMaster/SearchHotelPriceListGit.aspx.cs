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

namespace CRM.WebApp.Views.GITMaster
{
    public partial class SearchHotelPriceListGit : System.Web.UI.Page
    {
        CRM.DataAccess.GIT.HotelPriceListGIT objHotelPriceList = new CRM.DataAccess.GIT.HotelPriceListGIT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objHotelPriceList.fetchComboData("FETCH_ALL_CITY_FOR_MASTER");
                binddropdownlist(drp_Hotelcity, ds);

                DataSet ds1 = objHotelPriceList.fetchComboData("GET_HOTEL_ROOM_TYPE_FOR_GIT");
                binddropdownlist(drpRoomType, ds1);

                DataSet ds2 = objHotelPriceList.fetchComboData("FETCH_AGENT_COMPANY_FOR_GIT");
                binddropdownlist(drpAgent, ds2);

                DataSet ds3 = objHotelPriceList.fetchComboData("FETCH_ALL_SUPPLIER_COMPANY_NAME_FOR_GIT");
                binddropdownlist(drp_Hotel, ds3);

                DataSet ds5 = objHotelPriceList.fetchComboData("GET_DATA_SUPPLIER_HOTEL_PRICE_LIST_GIT");
                if (ds5.Tables[0].Rows.Count == 0)
                {
                }
                else
                {


                    GV_HotelPriceList.DataSource = ds5.Tables[0];
                    GV_HotelPriceList.DataBind();

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
            UpHotelPrice.Update();
        }
        protected void searchnow_onclick(object sender, EventArgs e)
        {
            DataSet ds4 = objHotelPriceList.GetGridData(drp_Hotelcity.Text, drpAgent.Text, drp_Hotel.Text, drpRoomType.Text, txtfromdate.Text, txttodate.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_HotelPriceList.DataSource = ds4.Tables[0];
            GV_HotelPriceList.DataBind();
            UpHotelPrice.Update();
        }
        protected void GV_HotelPriceList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int newindex = e.NewSelectedIndex;

            string hotelpriceid = GV_HotelPriceList.Rows[newindex].Cells[7].Text;
            Response.Redirect("~/Views/GITMaster/HotelPriceListGIT.aspx?ID=" + hotelpriceid);
        }
        protected void GV_HotelPriceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_HotelPriceList.PageIndex = e.NewPageIndex;

            DataSet ds5 = objHotelPriceList.GetGridData(drp_Hotelcity.Text, drpAgent.Text, drp_Hotel.Text, drpRoomType.Text, txtfromdate.Text, txttodate.Text);

            GV_HotelPriceList.DataSource = ds5;
            GV_HotelPriceList.DataBind();
            UpHotelPrice.Update();
        }
    }
}