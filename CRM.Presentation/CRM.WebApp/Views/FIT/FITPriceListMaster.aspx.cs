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

namespace CRM.WebApp.Views.FIT
{
    public partial class FITPriceListMaster : System.Web.UI.Page
    {
        SupplierPriceListMaster ObjSupplierPriceListMaster = new SupplierPriceListMaster();
        SupplierPriceListDetails ObjSupplierPriceListDetails = new SupplierPriceListDetails();
        //int SID ;
        //int supplierPriceListid ;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //SID = Convert.ToInt32(Session["supplierid"].ToString());
            if (!IsPostBack)
            {
                DataSet   dsSupplier = ObjSupplierPriceListMaster.GetSupplierName();
                binddropdownlist(ddl_SupplierName, dsSupplier);


                DataSet dsChainname = ObjSupplierPriceListMaster.FetchChainNameandCity(int.Parse(Session["supplierid"].ToString()));
                ddl_SupplierName.Text = dsChainname.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                txt_City.Text = dsChainname.Tables[0].Rows[0]["CITY_NAME"].ToString();
                //SID = Convert.ToInt32(Session["supplierid"].ToString());
                //BindSupplierName(Convert.ToInt32(Session["supplierid"].ToString()));
                //BindCity(Convert.ToInt32(Session["supplierid"].ToString()));
                BindRoomType();
                BindCurrency();
                BindStatus();
                
                string IU = Session["InsertUpdate"].ToString();
                if (IU == "U")
                {
                    //supplierPriceListid = Convert.ToInt32(Session["supplierPRICEID"].ToString());
                    btnSave.Text = "Update";
                }
                if (IU == "I")
                {
                    btnSave.Text = "Save";
                }
                if (Convert.ToInt32(Session["supplierPRICEID"].ToString()) != 0)
                {
                    DataSet dspricelistdata = ObjSupplierPriceListMaster.GetSupplierDetailsForEdit(Convert.ToInt32(Session["supplierPRICEID"].ToString())); /// bind with price list primery key 
                    ddl_SupplierName.Text = dspricelistdata.Tables[0].Rows[0]["CHAIN_NAME"].ToString();
                    txtFromDate.Text = dspricelistdata.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txtToDate.Text =dspricelistdata.Tables[0].Rows[0]["TO_DATE"].ToString();
                    ddl_RoomType.Text = dspricelistdata.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
                    txtSingleRoom.Text = dspricelistdata.Tables[0].Rows[0]["SINGLE_ROOM_RATE"].ToString();
                    txtDoubleRoom.Text = dspricelistdata.Tables[0].Rows[0]["DOUBLE_ROOM_RATE"].ToString();
                    txtTripleRoom.Text = dspricelistdata.Tables[0].Rows[0]["TRIPLE_ROOM_RATE"].ToString();
                    txtExtraAdult.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_ADULT_RATE"].ToString();
                    txtExtraCWB.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_CWB_COST"].ToString();
                    txtExtraCNB.Text = dspricelistdata.Tables[0].Rows[0]["EXTRA_CNB_COST"].ToString();
                    ddl_Status.Text = dspricelistdata.Tables[0].Rows[0]["STATUS"].ToString();
                    txtAMargin.Text = dspricelistdata.Tables[0].Rows[0]["A_MARGIN_IN_AMOUNT"].ToString();
                    txtAPMargin.Text = dspricelistdata.Tables[0].Rows[0]["A_PLUS_MARGIN_IN_AMOUNT"].ToString();
                    txtAPPMargin.Text = dspricelistdata.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_IN_AMOUNT"].ToString();
                    txtAMarginP.Text = dspricelistdata.Tables[0].Rows[0]["A_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtAPMarginP.Text = dspricelistdata.Tables[0].Rows[0]["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    txtAPPmarginP.Text = dspricelistdata.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                    BindCity(Convert.ToInt32(Session["supplierid"].ToString()));
                    if (dspricelistdata.Tables[0].Rows[0]["IS_DEFAULT"].ToString() == "True")
                    {
                        chbx_IsDefault.Checked = true;
                    }
                    else
                    {
                        chbx_IsDefault.Checked = false;
                    }
                }
                else
                {
                    //BindSupplierName(Convert.ToInt32(Session["supplierid"].ToString()));
                    //BindCity(Convert.ToInt32(Session["supplierid"].ToString()));
                    //BindRoomType();
                    //BindCurrency();
                    //BindStatus();
                }
            }
        }
        #endregion

        protected void ddl_SupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_RoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/FIT/FITSupplierHotelDetails.aspx?ID=" + Convert.ToInt32(Session["supplierid"].ToString()));
        }
        #region Save & Update
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string SName = ddl_SupplierName.Text.ToString();
            string FROM_DATE = txtFromDate.Text;
            string TO_DATE = txtToDate.Text;
            //DateTime DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) = DateTime.ParseExact(txtFromDate.Text.ToString(), null);
            //DateTime DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
            //DateTime DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
            if(txtFromDate.Text != "" || txtToDate.Text !="")
            {
                //DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
                //DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
            }
            //DateTime DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null)_C;
            //DateTime DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null)_C; 
            string ROOM_TYPE = ddl_RoomType.SelectedItem.ToString();
            DataSet ds = ObjSupplierPriceListDetails.GetSupplierDetails(Convert.ToInt32(Session["supplierid"].ToString()), "", "");
            int j = ds.Tables[0].Rows.Count;
            int i = 0;
            int k = 0;
            int l = 0;
            int Flag = 0;
            int SPID = Convert.ToInt32(Session["supplierPRICEID"].ToString());
            int SPIDC = 0;
            if (btnSave.Text == "Save")
            {
                for (i = 0; i < j; i++ )
                {
                    if (ROOM_TYPE.Equals(ds.Tables[0].Rows[i]["ROOM_TYPE_NAME"].ToString()))
                    {
                        if (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) || DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                        {
                            Flag = 1;
                            break;
                        }
                        else
                        {
                            if (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) && DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                        }
                        if (DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) || DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                        {
                            Flag = 1;
                            break;
                        }
                        else
                        {
                            if (DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) > DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) && DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) < DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                        }
                        if (DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) || DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                        {
                            Flag = 1;
                            break;
                        }
                        else
                        {
                            if (DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) && DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                        }
                        if (DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) || DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                        {
                            Flag = 1;
                            break;
                        }
                        else
                        {
                            if (DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) > DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) && DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) < DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                        }
                    }
                }
            }
            if (btnSave.Text == "Update")
            {
                for (i = 0; i < j; i++)
                {
                    SPIDC = Convert.ToInt32(ds.Tables[0].Rows[i]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString());
                    if (ROOM_TYPE.Equals(ds.Tables[0].Rows[i]["ROOM_TYPE_NAME"].ToString())) 
                    {
                        if (SPIDC != SPID)
                        {
                            if (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) || DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                            else
                            {
                                if (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) && DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                                {
                                    Flag = 1;
                                    break;
                                }
                            }
                            if (DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) || DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                            else
                            {
                                if (DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) > DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) && DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) < DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                                {
                                    Flag = 1;
                                    break;
                                }
                            }
                            if (DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) || DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) == DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                            else
                            {
                                if (DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(ds.Tables[0].Rows[i]["FROM_DATE"].ToString(), "dd/MM/yyyy", null) && DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null))
                                {
                                    Flag = 1;
                                    break;
                                }
                            }
                            if (DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) || DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) == DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                            {
                                Flag = 1;
                                break;
                            }
                            else
                            {
                                if (DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) > DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) && DateTime.ParseExact(ds.Tables[0].Rows[i]["TO_DATE"].ToString(), "dd/MM/yyyy", null) < DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null))
                                {
                                    Flag = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            string SINGLE_ROOM_RATE = txtSingleRoom.Text;
            string DOUBLE_ROOM_RATE = txtDoubleRoom.Text;
            string TRIPLE_ROOM_RATE = txtTripleRoom.Text;
            string EXTRA_CNB_COST = txtExtraCNB.Text;
            string EXTRA_CWB_COST = txtExtraCWB.Text;
            string IS_DEFAULT = "";
            string EXTRA_ADULT_RATE = txtExtraAdult.Text;
            string AMA = txtAMargin.Text;
            string APMA = txtAPMargin.Text;
            string APPMA = txtAPPMargin.Text;
            string AMP = txtAMarginP.Text;
            string APMP = txtAPMarginP.Text;
            string APPMP = txtAPPmarginP.Text;
            if (txtSingleRoom.Text == "")
            {
                SINGLE_ROOM_RATE = "0";
            }
            if (txtDoubleRoom.Text == "")
            {
                DOUBLE_ROOM_RATE = "0";
            }
            if (txtTripleRoom.Text == "")
            {
                TRIPLE_ROOM_RATE = "0";
            }
            if(txtExtraAdult.Text == "")
            {
                EXTRA_ADULT_RATE = "0";
            }
            if (txtExtraCNB.Text == "")
            {
                EXTRA_CNB_COST = "0";
            }
            if (txtExtraCWB.Text == "")
            {
                EXTRA_CWB_COST = "0";
            }
            if (txtAMargin.Text == "")
            {
                AMA = "0.00";
            }
            if (txtAPMargin.Text == "")
            {
                APMA = "0.00";
            }
            if (txtAPPMargin.Text == "")
            {
                APPMA = "0.00";
            }
            if (txtAMarginP.Text == "")
            {
                AMP = "0.00";
            }
            if (txtAPMarginP.Text == "")
            {
                APMP = "0.00";
            }
            if (txtAPPmarginP.Text == "")
            {
                APPMP = "0.00";
            }
            string CURRENCY_ID = ddl_Currency.SelectedItem.ToString();
            string CITY_ID = txt_City.Text;
            string PRICE_LIST_STATUS_ID = ddl_Status.SelectedItem.ToString();
            if (chbx_IsDefault.Checked)
            {
                IS_DEFAULT = "True";
            }
            else
                IS_DEFAULT = "False";
            if (Flag == 1)
            {
                Master.DisplayMessage("Price for this Dates Have Already Been Filled.", "successMessage", 7000);
            }
            if (Flag == 0)
            {

                if (AMA != "0.00" && AMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A Margin & A Margin %", "successMessage", 5000);
                }
                else if (AMA != "0.00" && APMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A Margin & A+ Margin %", "successMessage", 5000);
                }
                else if (AMA != "0.00" && APPMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A Margin & A++ Margin %", "successMessage", 5000);
                }


                else if (APMA != "0.00" && AMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A+ Margin & A Margin %", "successMessage", 5000);
                }
                else if (APMA != "0.00" && APMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A+ Margin & A+ Margin %", "successMessage", 5000);
                }
                else if (APMA != "0.00" && APPMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A+ Margin & A++ Margin %", "successMessage", 5000);
                }

                else if (APPMA != "0.00" && AMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A++ Margin & A Margin %", "successMessage", 5000);
                }
                else if (APPMA != "0.00" && APMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A++ Margin & A+ Margin %", "successMessage", 5000);
                }
                else if (APPMA != "0.00" && APPMP != "0.00")
                {
                    Master.DisplayMessage("You Can not Enter Both A++ Margin & A++ Margin %", "successMessage", 5000);
                }
                else if (AMA == "0.00" && APMA == "0.00" && APPMA == "0.00" && APMP == "0.00" && AMP == "0.00" && APPMP == "0.00")
                {
                    Master.DisplayMessage("Please enter Margin amount or Percentage.", "successMessage", 5000);
                }

                else if (DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null))
                {
                    Master.DisplayMessage("To Date Must Be Greater Than From Date.", "successMessage", 5000);
                }
                else
                {
                    if (btnSave.Text == "Save")
                    {
                        ObjSupplierPriceListMaster.INSERTFITPRICE(SName, ROOM_TYPE, FROM_DATE, TO_DATE, SINGLE_ROOM_RATE, DOUBLE_ROOM_RATE, TRIPLE_ROOM_RATE, EXTRA_ADULT_RATE,
                        EXTRA_CWB_COST, EXTRA_CNB_COST, IS_DEFAULT, PRICE_LIST_STATUS_ID, AMA, APMA, APPMA, AMP, APMP, APPMP, CURRENCY_ID, CITY_ID);
                        Master.DisplayMessage("Record Save Succesfully.", "successMessage", 5000);
                        Clear();
                        upHotelPrice.Update();
                    }
                    if (btnSave.Text == "Update")
                    {
                        ObjSupplierPriceListMaster.UPDATEFITPRICE(SName, ROOM_TYPE, FROM_DATE, TO_DATE, SINGLE_ROOM_RATE, DOUBLE_ROOM_RATE, TRIPLE_ROOM_RATE, EXTRA_ADULT_RATE,
                        EXTRA_CWB_COST, EXTRA_CNB_COST, IS_DEFAULT, PRICE_LIST_STATUS_ID, AMA, APMA, APPMA, AMP, APMP, APPMP, CURRENCY_ID, CITY_ID, Session["supplierPRICEID"].ToString());
                        Master.DisplayMessage("Record Updated Succesfully.", "successMessage", 5000);
                        Clear();
                        upHotelPrice.Update();
                        Response.Redirect("~/Views/FIT/FITSupplierHotelDetails.aspx?SUPPLIERID=" + Convert.ToInt32(Session["supplierid"].ToString()));
                    }
                }
            }
        }
        #endregion

        #region Bind
        public void BindSupplierName(int SID)
        {
            //DataTable dt = ObjSupplierPriceListMaster.GetSupplierName(Convert.ToInt32(Session["supplierid"].ToString()));
            //ddl_SupplierName.DataSource = dt;
            //ddl_SupplierName.DataTextField = "SUPPLIER";
            //ddl_SupplierName.DataBind();
        }
        public void BindCity(int SID)
        {
            //DataTable dt = ObjSupplierPriceListMaster.GetCity(Convert.ToInt32(Session["supplierid"].ToString()));
            //txt_City.Text = dt.Rows[0].ItemArray[0].ToString();
        }
        public void BindRoomType()
        {
            DataTable dt = ObjSupplierPriceListMaster.GetRoomType();
            ddl_RoomType.DataSource = dt;
            ddl_RoomType.DataTextField = "AutoSearchResult";
            ddl_RoomType.DataBind();
            ddl_RoomType.Items.Insert(0, new ListItem("", ""));
        }
        public void BindCurrency()
        {
            DataTable dt = ObjSupplierPriceListMaster.GetCurrency();
            ddl_Currency.DataSource = dt;
            ddl_Currency.DataTextField = "CURRENCY";
            ddl_Currency.SelectedValue = "THB";
            ddl_Currency.DataBind();
        }
        public void BindStatus()
        {
            DataTable dt = ObjSupplierPriceListMaster.GetStatus();
            ddl_Status.DataSource = dt;
            ddl_Status.DataTextField = "AutoSearchResult";
            ddl_Status.DataBind();
            ddl_Status.Items.Insert(0, new ListItem("", ""));
        }
        public void Clear()
        {
            ddl_RoomType.SelectedIndex = 0;
            ddl_Status.SelectedIndex = 0;
            chbx_IsDefault.Checked = false;
            txtAMargin.Text = "";
            txtAMarginP.Text = "";
            txtAPMargin.Text = "";
            txtAPMarginP.Text = "";
            txtAPPMargin.Text = "";
            txtAPPmarginP.Text = "";
            txtDoubleRoom.Text = "";
            txtExtraAdult.Text = "";
            txtExtraCNB.Text = "";
            txtExtraCWB.Text = "";
            txtFromDate.Text = "";
            txtSingleRoom.Text = "";
            txtToDate.Text = "";
            txtTripleRoom.Text = "";
        }
        #endregion


        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
        }
    }
}