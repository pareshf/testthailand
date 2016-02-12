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
    public partial class AllSupplier : System.Web.UI.Page
    {
        AllSupplierDal objallsuppiler = new AllSupplierDal();

        string SupplierType;
        int supplierid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds0 = objallsuppiler.fetchComboData("GET_CHAIN_NAME_FOR_GIT");
                DataSet ds1 = objallsuppiler.fetchComboData("GET_SUPPLIER_TYPE");              

                binddropdownlist(drpSupplierName, ds0);
                binddropdownlist(drpSupplierType, ds1);
                
                DataSet ds = objallsuppiler.fetchAllSupplierData(drpSupplierName.Text, drpSupplierType.Text);
                GV_Supplier.DataSource = ds;
                GV_Supplier.DataBind();
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
        
        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Supplier.PageIndex = e.NewPageIndex;

            DataSet ds = objallsuppiler.fetchAllSupplierData(drpSupplierName.Text, drpSupplierType.Text);
            GV_Supplier.DataSource = ds;
            GV_Supplier.DataBind();
            upSupplierGrid.Update();
        }

        protected void btnGITRate_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] != null)
            {
                supplierid = Convert.ToInt32(ViewState["l"].ToString());
                SupplierType = ViewState["SType"].ToString();            
                if (SupplierType == "Hotel")
                {

                    Response.Redirect("~/Views/GITMaster/SupplierHotelPriceListDetails.aspx?SUPPLIERID=" + supplierid); ////with supplier id from supplier master 
                }
                else if (SupplierType == "Sightseeing Company")
                {
                    Response.Redirect("~/Views/GITMaster/SightSeeingPriceListDetails.aspx?SUPPLIERID=" + supplierid); ////with supplier id from supplier master 
                }
                else if (SupplierType.Trim() == "Restaurant")
                {
                    Response.Redirect("~/Views/GITMaster/RestaurantPriceListDetails.aspx?SUPPLIERID=" + supplierid); ////with supplier id from supplier master 
                }
                else if (SupplierType == "Transport Company")
                {
                    Response.Redirect("~/Views/GITMaster/TransportPackagePriceListDetails.aspx?SUPPLIERID=" + supplierid); ////with supplier id from supplier master 
                }
            }

        }

        #region Check Chage Event Of Grid
        public void CheckChanged(object sender, EventArgs e)
        {
            ViewState["l"] = null;
            ViewState["SType"] = null;
            //on each item checked, remove any other items checked
            Label l = new Label();
            Label Stype = new Label();
            foreach (GridViewRow item in GV_Supplier.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rb1");
                if (rb != sender)
                {
                    rb.Checked = false;
                }

                if (rb.Checked == true)
                {
                    l = (Label)item.FindControl("lblSupplierID");
                    ViewState["l"] = l.Text;
                    Stype = (Label)item.FindControl("lblSupplierType");
                    ViewState["SType"] = Stype.Text;
                }
            }
            upButtons.Update();
            upSupplierGrid.Update();
        }
        #endregion

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            SupplierType = GV_Supplier.Rows[newindex].Cells[3].Text;      
            
        }


        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            upSupplierGrid.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {
            
            string USERID = Session["usersid"].ToString();
            DataSet ds = objallsuppiler.fetchAllSupplierData(drpSupplierName.Text, drpSupplierType.Text);
           
            
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Supplier.DataSource = ds.Tables[0];
            GV_Supplier.DataBind();
            upSupplierGrid.Update();

        }

        protected void btnFITRATE_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] != null)
            {
                supplierid = Convert.ToInt32(ViewState["l"].ToString());
                Session["supplierid"] = supplierid.ToString();
                SupplierType = ViewState["SType"].ToString();
                Response.Redirect("~/Views/FIT/FITSupplierHotelDetails.aspx?ID=" + supplierid);
            }
        }
    }
}