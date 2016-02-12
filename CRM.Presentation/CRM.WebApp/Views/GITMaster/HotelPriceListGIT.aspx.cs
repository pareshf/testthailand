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
    public partial class HotelPriceListGIT : System.Web.UI.Page
    {
        #region VARIABLES
      
        string hotelpriceid = "0";
        bool flag_date = true;
        #endregion

        CRM.DataAccess.GIT.HotelPriceListGIT objHotelPriceList = new CRM.DataAccess.GIT.HotelPriceListGIT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet ds = objHotelPriceList.fetchComboData("FETCH_ALL_CITY_FOR_MASTER");
                binddropdownlist(drp_Hotelcity, ds);

                DataSet ds1 = objHotelPriceList.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(drpAgent, ds1);

                DataSet ds4 = objHotelPriceList.fetchComboData("GET_ALL_PAYMENT_TERMS");
                binddropdownlist(drpPaymentTerms, ds4);

                DataSet ds5 = objHotelPriceList.fetchComboData("GET_PRICE_LIST_STATUS_FOR_GIT");
                binddropdownlist(drpStatus, ds5);

                DataSet ds6 = objHotelPriceList.fetchComboData("FETCH_YES_NO_GIT");
                binddropdownlist(drpIsDefaultQuote, ds6);

                DataSet ds7 = objHotelPriceList.fetchComboData("FETCH_ALL_CURRENCY_NAME_GIT");
                binddropdownlist(drpCurrency, ds7);

                drpCurrency.Text = "THB";
                if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
                {
                    int a = int.Parse(Request["ID"].ToString());
                    DataSet ds0 = objHotelPriceList.EditHotelPriceList("GET_DATA_SUPPLIER_HOTEL_PRICE_LIST_EDIT_GIT", a);
                    drp_Hotelcity.Text = ds0.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    DataSet ds2 = objHotelPriceList.fetchDataforHotel("GET_HOTEL_NAME_CITY_WISE", ds0.Tables[0].Rows[0]["CITY_NAME"].ToString());
                    if (ds2.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        binddropdownlist(drp_Hotel, ds2);

                    }
                    drp_Hotel.Text = ds0.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();
                    drpRoomType.Items.Clear();
                    DataSet ds3 = objHotelPriceList.fetchDataforHotelroomtype("GET_ROOM_TYPE_FROM_HOTEL_NAME", ds0.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString());
                    binddropdownlist(drpRoomType, ds3);
                    drpRoomType.Text = ds0.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
                    drpAgent.Text = ds0.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    txtfromdate.Text = ds0.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txttodate.Text = ds0.Tables[0].Rows[0]["TO_DATE"].ToString();
                    drpCurrency.Text = ds0.Tables[0].Rows[0]["CURRENCY_NAME"].ToString();
                    txtSurCharge.Text = ds0.Tables[0].Rows[0]["SURCHARGE"].ToString();
                    txtSurChargeUnit.Text = ds0.Tables[0].Rows[0]["SURCHARGE_UNIT"].ToString();

                    txtSingleRoomRate.Text = ds0.Tables[0].Rows[0]["SINGLE_ROOM_RATE"].ToString();
                    txtDoubleRoomRate.Text = ds0.Tables[0].Rows[0]["DOUBLE_ROOM_RATE"].ToString();
                    txtTrippleRoomRate.Text = ds0.Tables[0].Rows[0]["TRIPLE_ROOM_RATE"].ToString();
                    txtExtraAdultRate.Text = ds0.Tables[0].Rows[0]["EXTRA_ADULT_RATE"].ToString();
                    txtExtraCWBRate.Text = ds0.Tables[0].Rows[0]["EXTRA_CWB_COST"].ToString();
                    txtExtraCNBRate.Text = ds0.Tables[0].Rows[0]["EXTRA_CNB_COST"].ToString();

                    drpPaymentTerms.Text = ds0.Tables[0].Rows[0]["PAYMENT_TERMS"].ToString();
                    drpIsDefaultQuote.Text = ds0.Tables[0].Rows[0]["ISDEFULT"].ToString();
                    drpStatus.Text = ds0.Tables[0].Rows[0]["STATUS"].ToString();

                    txtA_MarginAmount.Text = ds0.Tables[0].Rows[0]["A_MARGIN_IN_AMOUNT"].ToString();
                    txtA_plusAmount.Text = ds0.Tables[0].Rows[0]["A_PLUS_MARGIN_IN_AMOUNT"].ToString();
                    txtA_plus_plusAmount.Text = ds0.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_IN_AMOUNT"].ToString();

                    txtAMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtPlusMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtAPlusPlusMarginAmountPer.Text = ds0.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    UpHotelPriceListGit.Update();
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
            //   r.SelectedValue = "0";
        }

        protected void drp_Hotelcity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = objHotelPriceList.fetchDataforHotel("GET_HOTEL_NAME_CITY_WISE", drp_Hotelcity.SelectedValue);
            if (ds2.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                binddropdownlist(drp_Hotel, ds2);

            }
            UpHotelPriceListGit.Update();
        }
        protected void drp_Hotel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            drpRoomType.Items.Clear();
            DataSet ds3 = objHotelPriceList.fetchDataforHotelroomtype("GET_ROOM_TYPE_FROM_HOTEL_NAME", drp_Hotel.SelectedValue);
            binddropdownlist(drpRoomType, ds3);
            UpHotelPriceListGit.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request["ID"] != null && !string.IsNullOrEmpty(Request["ID"].ToString()))
            {
                int a = int.Parse(Request["ID"].ToString());
                objHotelPriceList.InsertUpdaetHotelPriceList(a.ToString(), drpRoomType.SelectedValue, txtSingleRoomRate.Text, txtDoubleRoomRate.Text, txtExtraAdultRate.Text, drpCurrency.SelectedValue, drpPaymentTerms.SelectedValue, drp_Hotel.SelectedValue, drpAgent.SelectedValue, txtExtraCWBRate.Text,
                   txtExtraCNBRate.Text, txtfromdate.Text, txttodate.Text, txtSurCharge.Text, drpIsDefaultQuote.Text, txtSurChargeUnit.Text, txtTrippleRoomRate.Text, drpStatus.SelectedValue, txtA_MarginAmount.Text, txtA_plusAmount.Text, txtA_plus_plusAmount.Text, txtAMarginAmountPer.Text, txtPlusMarginAmountPer.Text, txtAPlusPlusMarginAmountPer.Text, drp_Hotelcity.SelectedValue, int.Parse(Session["usersid"].ToString()));
                Clear();
                Master.DisplayMessage("Record Update Successfully.", "successMessage", 3000);
                UpHotelPriceListGit.Update();
            }
            else
            {
                DataSet ds = objHotelPriceList.fetch_date_validation(drp_Hotelcity.Text, drp_Hotel.Text, drpRoomType.Text, drpAgent.Text);

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

                if (flag_date == true)
                {
                    objHotelPriceList.InsertUpdaetHotelPriceList(hotelpriceid, drpRoomType.SelectedValue, txtSingleRoomRate.Text, txtDoubleRoomRate.Text, txtExtraAdultRate.Text, drpCurrency.SelectedValue, drpPaymentTerms.SelectedValue, drp_Hotel.SelectedValue, drpAgent.SelectedValue, txtExtraCWBRate.Text,
                    txtExtraCNBRate.Text, txtfromdate.Text, txttodate.Text, txtSurCharge.Text, drpIsDefaultQuote.Text, txtSurChargeUnit.Text, txtTrippleRoomRate.Text, drpStatus.SelectedValue, txtA_MarginAmount.Text, txtA_plusAmount.Text, txtA_plus_plusAmount.Text, txtAMarginAmountPer.Text, txtPlusMarginAmountPer.Text, txtAPlusPlusMarginAmountPer.Text, drp_Hotelcity.SelectedValue, int.Parse(Session["usersid"].ToString()));
                    Clear();
                    Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);
                    UpHotelPriceListGit.Update();
                }
                else
                {
                    Master.DisplayMessage("Record is already exist of these dates please select another dates.", "successMessage", 8000);
                }
            }
        }
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
                UpHotelPriceListGit.Update();
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
                UpHotelPriceListGit.Update();
            }
        }

        #region Clear
        public void Clear()
        {
            drp_Hotelcity.SelectedValue = "0";
            drp_Hotel.SelectedValue = "0";
            drpAgent.SelectedValue = "0";
            drpCurrency.SelectedValue = "0";
            drpIsDefaultQuote.SelectedValue = "0";
            drpRoomType.SelectedValue = "0";
            drpStatus.SelectedValue = "0";
            drpPaymentTerms.SelectedValue = "0";
            txtA_MarginAmount.Text = "";
            txtA_plus_plusAmount.Text = "";
            txtA_plusAmount.Text = "";
            txtAMarginAmountPer.Text = "";
            txtAPlusPlusMarginAmountPer.Text = "";
            txtDoubleRoomRate.Text = "";
            txtExtraAdultRate.Text = "";
            txtExtraCNBRate.Text = "";
            txtExtraCWBRate.Text = "";
            txtfromdate.Text = "";
            txtPlusMarginAmountPer.Text = "";
            txtSingleRoomRate.Text = "";
            txtSurCharge.Text = "";
            txtSurChargeUnit.Text = "";
            txttodate.Text = "";
            txtTrippleRoomRate.Text = "";

        }
        #endregion
    }
}