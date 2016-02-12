using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess.AdministrationDAL;


namespace CRM.WebApp.Views.GITMaster
{
    public partial class RestaurantPriceListMaster : System.Web.UI.Page
    {
        RestaurantPriceListMasterDal objRestaurantPriceListmaster = new RestaurantPriceListMasterDal();

        int Restaurantid = 0;
        int supplierID = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objRestaurantPriceListmaster.fetchComboData("GET_SUPPLIER_FOR_RESTURANTS");

                DataSet ds2 = objRestaurantPriceListmaster.fetchComboData("GET_MEAL_TYPE");

                binddropdownlist(drpRestaurantname, ds);
                binddropdownlist(drpMeal, ds2);

                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                if (Request["RestuarantPriceListId"] != null && !string.IsNullOrEmpty(Request["RestuarantPriceListId"].ToString()))
                {
                    Restaurantid = Convert.ToInt32(Request.QueryString["RestuarantPriceListId"].ToString());
                }

                if (Restaurantid != 0)
                {
                    DataSet dspricelistdata = objRestaurantPriceListmaster.fetchRestaurantPriceListDataForEdit(Restaurantid); /// bind with price list primery key 

                    drpRestaurantname.Text = dspricelistdata.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    drpMeal.Text = dspricelistdata.Tables[0].Rows[0]["MEAL_DESC"].ToString();
                    txtAdultRate.Text = dspricelistdata.Tables[0].Rows[0]["ADULT_RATE"].ToString();
                    txtChildRate.Text = dspricelistdata.Tables[0].Rows[0]["CHILD_RATE"].ToString();
                    
                }
                else
                {
                    DataSet dsChainname = objRestaurantPriceListmaster.FetchChainNameandCity(supplierID);
                    drpRestaurantname.Text = dsChainname.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                }

            }
            else
            {
                if (Request["RestuarantPriceListId"] != null && !string.IsNullOrEmpty(Request["RestuarantPriceListId"].ToString()))
                {
                    Restaurantid = Convert.ToInt32(Request.QueryString["RestuarantPriceListId"].ToString());
                }
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userid = Session["usersid"].ToString();

            DataSet dtforvalidation = objRestaurantPriceListmaster.FetchCountForValidation(drpRestaurantname.Text, drpMeal.Text, Restaurantid);
                    if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                    {
                    objRestaurantPriceListmaster.insertRestaurantPriceListMaster(Restaurantid, drpRestaurantname.Text, drpMeal.Text, txtAdultRate.Text, txtChildRate.Text, txtcurrency.Text, userid);
                    Clear();
                    Master.DisplayMessage("Record Save Succesfully.", "successMessage", 5000);
                    upRestaurantPriceList.Update();
                    Response.Redirect("~/Views/GITMaster/RestaurantPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id 
                    }
                    else
                    {
                        Master.DisplayMessage("Record already exists.", "successMessage", 5000);
                    }
              

           
        }

        protected void Clear()
        {
            drpRestaurantname.Text = "";
            drpMeal.Text = "";
            txtAdultRate.Text = "";
            txtChildRate.Text = "";
            txtcurrency.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/RestaurantPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id             
        }
    }
}