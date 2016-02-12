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
    public partial class SightSeeingPriceListMaster : System.Web.UI.Page
    {

        SightseeingPriceListMasterDal objSightSeeingPriceListmaster = new SightseeingPriceListMasterDal();

        int Sightid = 0;
        int supplierID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objSightSeeingPriceListmaster.fetchComboData("GET_SIGHT_SEEING_NAME_FOR_GIT");

                DataSet ds2 = objSightSeeingPriceListmaster.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");

                binddropdownlist(drpSightSeeingname, ds);
                binddropdownlist(drpStatus, ds2);

                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                if (Request["SightSeeingPriceListId"] != null && !string.IsNullOrEmpty(Request["SightSeeingPriceListId"].ToString()))
                {
                    Sightid = Convert.ToInt32(Request.QueryString["SightSeeingPriceListId"].ToString());
                }

                if (Sightid != 0)
                {
                    DataSet dspricelistdata = objSightSeeingPriceListmaster.fetchSightSeeingPriceListDataForEdit(Sightid); /// bind with price list primery key 

                    drpSightSeeingname.Text = dspricelistdata.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    txtSightSeeingPackageName.Text = dspricelistdata.Tables[0].Rows[0]["SIGHT_SEEING_PACKAGE_NAME"].ToString();
                    txtAdultRate.Text = dspricelistdata.Tables[0].Rows[0]["ADULT_RATE"].ToString();
                    txtChildRate.Text = dspricelistdata.Tables[0].Rows[0]["CHILD_RATE"].ToString();

                    drpStatus.Text = dspricelistdata.Tables[0].Rows[0]["STATUS"].ToString();
                    txtCity.Text = dspricelistdata.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    if (dspricelistdata.Tables[0].Rows[0]["IS_MEAL_APPLICABLE"].ToString() == "True")
                    {
                        chkisMealApplicable.Checked = true;
                    }
                    else
                    {
                        chkisMealApplicable.Checked = false;
                    }

                }
                else
                {
                    DataSet dsChainname = objSightSeeingPriceListmaster.FetchChainNameandCity(supplierID);
                    drpSightSeeingname.Text = dsChainname.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    txtCity.Text = dsChainname.Tables[0].Rows[0]["CITY_NAME"].ToString();
                }

            }
            else
            {
                if (Request["SightSeeingPriceListId"] != null && !string.IsNullOrEmpty(Request["SightSeeingPriceListId"].ToString()))
                {
                    Sightid = Convert.ToInt32(Request.QueryString["SightSeeingPriceListId"].ToString());
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

        public void drpSupplierName_SelectedIndexChanged(Object sender, EventArgs e)
        {
            DataSet ds = objSightSeeingPriceListmaster.FetchCity(drpSightSeeingname.Text);
            txtCity.Text = ds.Tables[0].Rows[0]["CITY_NAME"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string isMeal;
            if (chkisMealApplicable.Checked == true)
            {
                isMeal = "true";
            }
            else
            {
                isMeal = "False";
            }
            
                DataSet dtforvalidation = objSightSeeingPriceListmaster.FetchCountForValidation(txtSightSeeingPackageName.Text,Sightid);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                    objSightSeeingPriceListmaster.insertSightSeeingPriceListMaster(Sightid, drpSightSeeingname.Text, txtSightSeeingPackageName.Text, txtAdultRate.Text, txtChildRate.Text, isMeal, drpStatus.Text, txtcurrency.Text, txtCity.Text);
                    Clear();
                    Master.DisplayMessage("Record Save Succesfully.", "successMessage", 5000);
                    upSightSeeingPriceList.Update();
                    Response.Redirect("~/Views/GITMaster/SightSeeingPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id 
                }
                else
                {
                    Master.DisplayMessage("Record already exists.", "successMessage", 5000);
                }
         

        }

        protected void Clear()
        {
            drpSightSeeingname.Text = "";
            txtSightSeeingPackageName.Text = "";
            txtAdultRate.Text = "";
            txtChildRate.Text = "";
            chkisMealApplicable.Checked = false;
            drpStatus.Text = "";
            txtCity.Text = "";
            txtcurrency.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect("~/Views/GITMaster/SightSeeingPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id 
           
        }

    }
}