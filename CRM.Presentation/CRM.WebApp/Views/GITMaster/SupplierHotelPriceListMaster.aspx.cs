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
    public partial class SupplierHotelPriceListMaster : System.Web.UI.Page
    {
        SupplierHotelPriceListMasterDal objsupplierHotelPriceListMaster = new SupplierHotelPriceListMasterDal();

        int supplierPriceListid = 0;
        int supplierID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objsupplierHotelPriceListMaster.fetchComboData("GET_CHAIN_NAME_FOR_GIT");
                DataSet ds1 = objsupplierHotelPriceListMaster.fetchComboData("GET_ROOM_TYPE_FOR_GIT");
                DataSet ds2 = objsupplierHotelPriceListMaster.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");

                binddropdownlist(drpsuppliername, ds);
                binddropdownlist(drpRoomType, ds1);
                binddropdownlist(drpStatus, ds2);
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierID = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                if (Request["SUPPLIERPICELISTID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERPICELISTID"].ToString()))
                {
                    supplierPriceListid = Convert.ToInt32(Request.QueryString["SUPPLIERPICELISTID"].ToString());
                }

                if (supplierPriceListid != 0)
                {
                    DataSet dspricelistdata = objsupplierHotelPriceListMaster.fetchSupplierPriceListDataForEdit(supplierPriceListid); /// bind with price list primery key 

                    drpsuppliername.Text = dspricelistdata.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    drpRoomType.Text = dspricelistdata.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
                    txtSingleRoomRate.Text = dspricelistdata.Tables[0].Rows[0]["SINGLE_ROOM_RATE"].ToString();
                    txtDoubleRoomRate.Text = dspricelistdata.Tables[0].Rows[0]["DOUBLE_ROOM_RATE"].ToString();
                    txtTripleRoomRate.Text = dspricelistdata.Tables[0].Rows[0]["TRIPLE_ROOM_RATE"].ToString();
                    txtextraadultrate.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_ADULT_RATE"].ToString();
                    txtExtraCWBRate.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_CWB_COST"].ToString();
                    txtextraCNBRate.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_CNB_COST"].ToString();
                    drpStatus.Text = dspricelistdata.Tables[0].Rows[0]["STATUS"].ToString();
                    txtCity.Text = dspricelistdata.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    if (dspricelistdata.Tables[0].Rows[0]["IS_DEFAULT"].ToString() == "True")
                    {
                        chkisdefault.Checked = true;
                    }
                    else
                    {
                        chkisdefault.Checked = false;
                    }
                    if (dspricelistdata.Tables[0].Rows[0]["IS_CONFERENCE_APPLICABLE"].ToString() == "True")
                    {
                        chkisconfapplicable.Checked = true;
                    }
                    else
                    {
                        chkisconfapplicable.Checked = false;
                    }
                    if (dspricelistdata.Tables[0].Rows[0]["IS_GALA_DINNER_APPLICABLE"].ToString() == "True")
                    {
                        chkisgaladinnerApplicable.Checked = true;
                    }
                    else
                    {
                        chkisgaladinnerApplicable.Checked = false;
                    }
                }
                else
                {
                    DataSet dsChainname = objsupplierHotelPriceListMaster.FetchChainNameandCity(supplierID);
                    drpsuppliername.Text = dsChainname.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    txtCity.Text = dsChainname.Tables[0].Rows[0]["CITY_NAME"].ToString();
                }

            }
            else
            {
                if (Request["SUPPLIERPICELISTID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERPICELISTID"].ToString()))
                {
                    supplierPriceListid = Convert.ToInt32(Request.QueryString["SUPPLIERPICELISTID"].ToString());
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
            DataSet ds = objsupplierHotelPriceListMaster.FetchCity(drpsuppliername.Text);
            txtCity.Text = ds.Tables[0].Rows[0]["CITY_NAME"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string isdefault;
            string isconf;
            string isgala;
            if (chkisdefault.Checked == true)
            {
                isdefault = "true";
            }
            else
            {
                isdefault = "False";
            }
            if (chkisconfapplicable.Checked == true)
            {
                isconf = "True";
            }
            else
            {
                isconf = "False";
            }
            if (chkisgaladinnerApplicable.Checked == true)
            {
                isgala = "True";
            }
            else
            {
                isgala = "False";
            }
            
                DataSet dtforvalidation = objsupplierHotelPriceListMaster.FetchCountForValidation(drpsuppliername.Text, drpRoomType.Text,supplierPriceListid);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {

                    objsupplierHotelPriceListMaster.inserthotelPriceListMaster(drpsuppliername.Text, drpRoomType.Text, txtSingleRoomRate.Text, txtDoubleRoomRate.Text, txtTripleRoomRate.Text, txtextraadultrate.Text, txtExtraCWBRate.Text, txtextraCNBRate.Text, isdefault, drpStatus.Text, txtcurrency.Text, txtCity.Text, isconf, isgala, supplierPriceListid);
                    Clear();
                    Master.DisplayMessage("Record Save Succesfully.", "successMessage", 5000);
                    upHotelPriceList.Update();

                    Response.Redirect("~/Views/GITMaster/SupplierHotelPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id 

                }
                else
                {
                    Master.DisplayMessage("Record already exists.", "successMessage", 5000);
                }
           

        }

        protected void Clear()
        {
            drpsuppliername.Text = "";
            drpRoomType.Text = "";
            txtSingleRoomRate.Text = "";
            txtDoubleRoomRate.Text = "";
            txtTripleRoomRate.Text = "";
            txtextraadultrate.Text = "";
            txtextraCNBRate.Text = "";
            txtExtraCWBRate.Text = "";
            chkisdefault.Checked = false;
            chkisconfapplicable.Checked = false;
            chkisgaladinnerApplicable.Checked = false;
            drpStatus.Text = "";
            txtCity.Text = "";
            txtcurrency.Text = "";
            chkisconfapplicable.Checked = false;
            chkisgaladinnerApplicable.Checked = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/SupplierHotelPriceListDetails.aspx?SUPPLIERID=" + supplierID); // redirect with Supplier id 

        }
    }
}