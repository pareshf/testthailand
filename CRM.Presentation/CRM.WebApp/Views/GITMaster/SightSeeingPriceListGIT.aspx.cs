using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;


namespace CRM.WebApp.Views.GITMaster
{
    public partial class SightSeeingPriceListGIT : System.Web.UI.Page
    {
        #region VARIABLES

        string Sightpriceid = "0";
        bool flag_date = true;
        #endregion
        CRM.DataAccess.GIT.SightSeeingPriceListGIT objSight = new CRM.DataAccess.GIT.SightSeeingPriceListGIT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds1 = objSight.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(drpAgent, ds1);

                DataSet ds2 = objSight.fetchComboData("GET_ALL_PAYMENT_TERMS");
                binddropdownlist(drpPaymentTerms, ds2);

                DataSet ds3 = objSight.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");
                binddropdownlist(drpStatus, ds3);

                DataSet ds4 = objSight.fetchComboData("FETCH_ALL_CURRENCY_NAME_GIT");
                binddropdownlist(drpCurrency, ds4);

                DataSet ds5 = objSight.fetchComboData("GET_SUPPLIER_NAME_FOR_SIGHT_SEEING_PRICE_LIST");
                binddropdownlist(drpSupplier, ds5);

                DataSet ds6 = objSight.fetchComboData("GET_TIME_FOR_SIGHT_SEEING_PRICELIST_GIT");
                 binddropdownlist(drpTime1, ds6);
                 binddropdownlist(drpTime2, ds6);
                 binddropdownlist(drpTime3, ds6);
                 binddropdownlist(drpTime4, ds6);
                 binddropdownlist(drpTime5, ds6);

                DataSet ds7 = objSight.fetchComboData("GET_RESTAURANT_NAME_SIGHTSEEING_PRICELIST_GIT");
                binddropdownlist(drpRestaurant, ds7);

                DataSet ds8 = objSight.fetchComboData("FETCH_ALL_CITY_FOR_MASTER");
                binddropdownlist(drp_Hotelcity, ds8);

                DataSet ds9 = objSight.fetchComboData("FETCH_YES_NO_GIT");
                binddropdownlist(drpMealApplicable, ds9);

                DataSet ds10 = objSight.fetchComboData("GET_MEAL_TYPE_SIGHT_SEEING_PRICE_LIST");
                binddropdownlist(drpMealtype, ds10);

                DataSet ds11 = objSight.fetchComboData("GET_SITE_NAME_FOR_SITE_SEEING_PRICE_LIST_GIT");
                binddropdownlist(drpSightPalce, ds11);

                if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
                {
                    int a = int.Parse(Request["ID"].ToString());
                    DataSet ds0 = objSight.EditSightPriceList("GET_SIGHT_SEEING_DATA_FOR_GRID_EDIT_GIT", a);
                    drp_Hotelcity.Text = ds0.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    drpAgent.Text = ds0.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    drpCurrency.Text = ds0.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    drpMealApplicable.Text = ds0.Tables[0].Rows[0]["MEAL_APPLICABLE"].ToString();
                    drpMealtype.Text = ds0.Tables[0].Rows[0]["MEAL_TYPE"].ToString();
                    drpPaymentTerms.Text = ds0.Tables[0].Rows[0]["PAYMENT_TERMS"].ToString();
                    drpRestaurant.Text = ds0.Tables[0].Rows[0]["RESTAURANT_NAME"].ToString();
                    drpSightPalce.Text = ds0.Tables[0].Rows[0]["SITE_NAME"].ToString();
                    drpStatus.Text = ds0.Tables[0].Rows[0]["STATUS"].ToString();
                    drpSupplier.Text = ds0.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();
                    drpTime1.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_TIME"].ToString();

                    drpTime2.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_TIME1"].ToString();
                    drpTime3.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_TIME2"].ToString();
                    drpTime4.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_TIME3"].ToString();
                    drpTime5.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_TIME4"].ToString();

                    txtA_MarginAmount.Text = ds0.Tables[0].Rows[0]["A_MARGIN_IN_AMOUNT"].ToString();
                    txtA_plusAmount.Text = ds0.Tables[0].Rows[0]["A_PLUS_MARGIN_IN_AMOUNT"].ToString();
                    txtA_plus_plusAmount.Text = ds0.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_IN_AMOUNT"].ToString();

                    txtAMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtPlusMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtAPlusPlusMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();

                    txtAdultSic.Text = ds0.Tables[0].Rows[0]["ADULT_SIC_RATE"].ToString();
                    txtChildSic.Text = ds0.Tables[0].Rows[0]["CHILD_SIC_RATE"].ToString();
                    txtAdultPvtRate.Text = ds0.Tables[0].Rows[0]["ADULT_PVT_RATE"].ToString();
                    txtChildPvtRate.Text = ds0.Tables[0].Rows[0]["CHILD_PVT_RATE"].ToString();

                    txtfromdate.Text = ds0.Tables[0].Rows[0]["EFFECTIVE_FROM_DATE"].ToString();
                    txttodate.Text = ds0.Tables[0].Rows[0]["EFFECTIVE_TO_DATE"].ToString();
                    txtSightPackage.Text = ds0.Tables[0].Rows[0]["SIGHT_SEEING_PACKAGE_NAME"].ToString();
                    txtSicRateperPerson.Text = ds0.Tables[0].Rows[0]["SICRATE_PER_PERSON"].ToString();
                    txtChildRatePerPerson.Text = ds0.Tables[0].Rows[0]["PVTRATE_PER_PERSON"].ToString();
                    
                    
                }
            }
        }
        #region BindDropdown
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            //   r.SelectedValue = "0";
        }
        #endregion

        #region Save
        protected void btnSave_Click(object Sender, EventArgs e)
        {
            if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
            {
                int a = int.Parse(Request["ID"].ToString());
                objSight.InsertUpdateSitePrice(a.ToString(), drpTime1.Text, txtfromdate.Text, txttodate.Text, drpRestaurant.SelectedValue, drpCurrency.SelectedValue, drp_Hotelcity.SelectedValue, drpPaymentTerms.SelectedValue, drpSightPalce.SelectedValue, drpAgent.SelectedValue, txtSightPackage.Text, drpMealApplicable.SelectedValue, drpSupplier.SelectedValue, txtAdultSic.Text, txtChildSic.Text, txtAdultPvtRate.Text, txtChildPvtRate.Text, txtSicRateperPerson.Text, txtChildRatePerPerson.Text, drpMealtype.SelectedValue, drpStatus.SelectedValue, txtA_MarginAmount.Text, txtA_plusAmount.Text, txtA_plus_plusAmount.Text, txtAMarginAmountPer.Text, txtPlusMarginAmountPer.Text, txtAPlusPlusMarginAmountPer.Text, drpTime2.Text, drpTime3.Text, drpTime4.Text, drpTime5.Text, int.Parse(Session["usersid"].ToString()));
                Master.DisplayMessage("Record Update Successfully.", "successMessage", 3000);
                Clear();
                UpSightSeeingPriceListGit.Update();

            }
            else
            {
                DataSet ds = objSight.fetch_date_validation(drp_Hotelcity.Text, drpSupplier.Text, drpAgent.Text,txtSightPackage.Text);

                string txtperiod_stay_from = txtfromdate.Text;
                string txtperiod_stay_to = txttodate.Text;
                DateTime date1 = DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null);
                DateTime date2 = DateTime.ParseExact(txtperiod_stay_to, "dd/MM/yyyy", null);
                TimeSpan ts;
                ts = date2.Subtract(date1.Date);
                string txtno_of_nights = ts.TotalDays.ToString();

                for (int i = 0; i < int.Parse(txtno_of_nights); i++)
                {
                    DateTime dat = DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null);
                    dat = dat.AddDays(i);

                    string date = dat.ToString("dd/MM/yyyy");

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        //string txtperiod_stay_from1 = ds.Tables[0].Rows[i]["FROM_DATE"].ToString();
                        //string txtperiod_stay_to1 = ds.Tables[0].Rows[i]["TO_DATE"].ToString();
                        //DateTime date11 = DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null);
                        //DateTime date21 = DateTime.ParseExact(txtperiod_stay_to, "dd/MM/yyyy", null);
                        //TimeSpan ts1;
                        //ts1 = date21.Subtract(date11.Date);
                        //string txtno_of_nights1 = ts.TotalDays.ToString();

                        //for (int k = 0; k < int.Parse(txtno_of_nights1); k++)
                        //{
                        if ((DateTime.ParseExact(date, "dd/MM/yyyy", null) >= DateTime.ParseExact(ds.Tables[0].Rows[j]["FROM_DATE"].ToString(), "dd/MM/yyyy", null)) && (DateTime.ParseExact(date, "dd/MM/yyyy", null) <= DateTime.ParseExact(ds.Tables[0].Rows[j]["TO_DATE"].ToString(), "dd/MM/yyyy", null)))
                        {

                            flag_date = false;
                            break;
                        }

                    }
                    if (flag_date == false)
                    {
                        break;
                    }

                }

                
            }
            if (flag_date == true)
            {
                objSight.InsertUpdateSitePrice(Sightpriceid, drpTime1.Text, txtfromdate.Text, txttodate.Text, drpRestaurant.SelectedValue, drpCurrency.SelectedValue, drp_Hotelcity.SelectedValue, drpPaymentTerms.SelectedValue, drpSightPalce.SelectedValue, drpAgent.SelectedValue, txtSightPackage.Text, drpMealApplicable.SelectedValue, drpSupplier.SelectedValue, txtAdultSic.Text, txtChildSic.Text, txtAdultPvtRate.Text, txtChildPvtRate.Text, txtSicRateperPerson.Text, txtChildRatePerPerson.Text, drpMealtype.SelectedValue, drpStatus.SelectedValue, txtA_MarginAmount.Text, txtA_plusAmount.Text, txtA_plus_plusAmount.Text, txtAMarginAmountPer.Text, txtPlusMarginAmountPer.Text, txtAPlusPlusMarginAmountPer.Text, drpTime2.Text, drpTime3.Text, drpTime4.Text, drpTime5.Text, int.Parse(Session["usersid"].ToString()));
                Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);
                Clear();
                UpSightSeeingPriceListGit.Update();
            }
            else
            {
                Master.DisplayMessage("Record is already exist of these dates please select another dates.", "successMessage", 8000);
            }
        }
        #endregion

        #region TextChange Eevent
        protected void txtfromdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", null);

                if (txtfromdate.Text != "" && txttodate.Text != "")
                {
                    if ((DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null) <= DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("from date must be less then from date.", "successMessage", 8000);
                        txtfromdate.Text = "";
                        txtfromdate.Focus();

                    }
                    else
                    {
                        txttodate.Focus();
                    }
                }
            }
            catch
            {
                Master.DisplayMessage("from date is not in correct format.", "successMessage", 8000);
                txtfromdate.Text = "";

            }
            finally
            {
                UpSightSeeingPriceListGit.Update();
            }
        }
        protected void txttodate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null);

                if (txtfromdate.Text != "" && txttodate.Text != "")
                {
                    if ((DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null) <= DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("To date must be gretaer then from date.", "successMessage", 8000);
                        txttodate.Text = "";
                        txttodate.Focus();
                    }
                    else
                    {
                        drpCurrency.Focus();
                    }
                }
            }
            catch
            {
                Master.DisplayMessage("to date date is not in correct format.", "successMessage", 8000);
                txttodate.Text = "";

            }
            finally
            {
                UpSightSeeingPriceListGit.Update();
            }
        }
        #endregion

        #region Clear
        public void Clear()
        {
            drpTime1.SelectedValue = "0";
            drpTime2.SelectedValue = "0";
            drpTime3.SelectedValue = "0";
            drpTime4.SelectedValue = "0";
            drpTime5.SelectedValue = "0";
            drpStatus.SelectedValue = "0";
            drpSightPalce.SelectedValue = "0";
            drpRestaurant.SelectedValue = "0";
            drpPaymentTerms.SelectedValue = "0";
            drpMealtype.SelectedValue = "0";
            drpMealApplicable.SelectedValue = "0";
            drpCurrency.SelectedValue = "0";
            drpAgent.SelectedValue = "0";
            drp_Hotelcity.SelectedValue = "0";
            drpSupplier.SelectedValue = "0";

            txtA_MarginAmount.Text = "";
            txtA_plus_plusAmount.Text = "";
            txtA_plusAmount.Text = "";
            txtAdultPvtRate.Text = "";
            txtAdultSic.Text = "";
            txtAMarginAmountPer.Text = "";
            txtAPlusPlusMarginAmountPer.Text = "";
            txtPlusMarginAmountPer.Text = "";
            txtfromdate.Text = "";
            txtfromdate.Text = "";
            txtChildPvtRate.Text = "";
            txtSightPackage.Text = "";
            txtChildRatePerPerson.Text = "";
            txtChildSic.Text = "";
            txtSicRateperPerson.Text = "";
            txttodate.Text = "";
            
        }
        #endregion
    }
}