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
    public partial class SearchSightSeeingPriceListGIT : System.Web.UI.Page
    {
        CRM.DataAccess.GIT.SightSeeingPriceListGIT objSight = new CRM.DataAccess.GIT.SightSeeingPriceListGIT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objSight.fetchComboData("FETCH_ALL_CITY_FOR_MASTER");
                binddropdownlist(drpCity, ds);

                DataSet ds2 = objSight.fetchComboData("FETCH_AGENT_COMPANY_FOR_GIT");
                binddropdownlist(drpAgent, ds2);

                DataSet ds3 = objSight.fetchComboData("FETCH_ALL_SUPPLIER_COMPANY_NAME_FOR_GIT");
                binddropdownlist(drpSupplier, ds3);

                DataSet ds5 = objSight.fetchComboData("GET_SIGHT_SEEING_DATA_FOR_GRID_GIT");
                if (ds5.Tables[0].Rows.Count == 0)
                {
                }
                else
                {


                    GV_sightPriceList.DataSource = ds5.Tables[0];
                    GV_sightPriceList.DataBind();

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
            UpSight.Update();
        }
        protected void searchnow_onclick(object sender, EventArgs e)
        {
            DataSet ds4 = objSight.GetGridData(drpCity.Text, drpAgent.Text, drpSupplier.Text, txtpackage.Text, txtfromdate.Text,txttodate.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_sightPriceList.DataSource = ds4.Tables[0];
            GV_sightPriceList.DataBind();
            UpSight.Update();
        }
        protected void GV_HotelPriceList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int newindex = e.NewSelectedIndex;

            string sightPriceid = GV_sightPriceList.Rows[newindex].Cells[6].Text;
            Response.Redirect("SightSeeingPriceListGIT.aspx?ID=" + sightPriceid);
        }
        protected void GV_HotelPriceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_sightPriceList.PageIndex = e.NewPageIndex;

            DataSet ds5 = objSight.GetGridData(drpCity.Text, drpAgent.Text, drpSupplier.Text, txtpackage.Text, txtfromdate.Text, txttodate.Text);

            GV_sightPriceList.DataSource = ds5;
            GV_sightPriceList.DataBind();
            UpSight.Update();
        }
    }
}