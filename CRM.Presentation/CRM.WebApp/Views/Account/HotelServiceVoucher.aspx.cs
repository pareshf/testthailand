using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
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
using CRM.DataAccess.Account;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.WebApp.Views.Account
{
    public partial class HotelServiceVoucher : System.Web.UI.Page
    {
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.FIT.HotelServiceVoucher objhotelSeviceVoucher = new CRM.DataAccess.FIT.HotelServiceVoucher();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();
        GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
        CRM.DataAccess.Account.SearchHotelServiceVoucher objesearchHotelService = new CRM.DataAccess.Account.SearchHotelServiceVoucher();

        #region VARIABLES
        int hotelcartid;
        string cityid;
        string hotelid;
        string fromdate;
        string todate;
        int noofroms;
        string orderstatus;
        int userid;
        int roomtype;
        string roomtypeid;
        string startdate;
        string enddate;

        #endregion


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 216);
            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                // Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(DropDownList2, ds3);
                if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()))
                {
                    string vno = Request["IN"].ToString();
                    

                    DataSet ds2 = objesearchHotelService.fetchHotelVoucherEdit("FETCH_DATA_FOR_HOTEL_SERVICE_VOUCHER_FOR_EDIT", vno);

                    txtClientname.Text = ds2.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
                    txtnonights.Text = ds2.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
                    txtNoroomSingle.Text = ds2.Tables[0].Rows[0]["NO_OF_SINGLE_ROOMS"].ToString();
                    txtNoroomDouble.Text = ds2.Tables[0].Rows[0]["NO_OF_DOUBLE_ROOMS"].ToString();
                    txtNoroomTriple.Text = ds2.Tables[0].Rows[0]["NO_OF_TRIPAL_ROOMS"].ToString();
                    DropDownList1.Text = ds2.Tables[0].Rows[0]["CITY_NAME"].ToString();
                    DataSet ds0 = objhotelSeviceVoucher.fetchDataforHotel("GET_HOTEL_NAME_CITY_WISE", ds2.Tables[0].Rows[0]["CITY_NAME"].ToString());
                    if (ds0.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        binddropdownlist(ddrpty_HotelName, ds0);

                    }
                    ddrpty_HotelName.Text = ds2.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();
                    DataSet ds = objhotelSeviceVoucher.fetchDataforHotelroomtype("GET_ROOM_TYPE_FROM_HOTEL_NAME", ds2.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString());
                    binddropdownlist(ddrpty_RoomType, ds);
                    ddrpty_RoomType.Text = ds2.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();
                    DropDownList2.Text = ds2.Tables[0].Rows[0]["CUST_COMPANY_NAME"].ToString();
                    DataSet dscom = objhotelSeviceVoucher.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
                    ViewState["cust_rel_no"] = dscom.Tables[0].Rows[0]["CUST_ID"].ToString();

                    //binddropdownlist(drpvoucher_type, ds);
                    ViewState["cust_sr_no"] = dscom.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                    DropDownList3.Items.Clear();

                    DataSet ds1 = objhotelSeviceVoucher.get_invoice_left("GET_INVOICE_NO", int.Parse(dscom.Tables[0].Rows[0]["USER_ID"].ToString()));
                    //    DataSet ds1 = objVouchersStoredProcedure.get_invoice_no_agent_vise("FETCH_INVOICE_NO_AGENT_VISE", int.Parse(ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString()));
                    binddropdownlist(DropDownList3, ds1);
                    DropDownList3.Text = ds2.Tables[0].Rows[0]["INVOICE_NO"].ToString();
                    txtpty_CheckIn.Text = ds2.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txtpty_CheckOut.Text = ds2.Tables[0].Rows[0]["TO_DATE"].ToString();
                    txttotalroom.Text = ds2.Tables[0].Rows[0]["NO_OF_ROOMS"].ToString();
                    txtVoucherNo.Text = ds2.Tables[0].Rows[0]["VOUCHER_NO"].ToString();
                    Session["uid"] = dscom.Tables[0].Rows[0]["USER_ID"].ToString();
                    ddrpty_HotelName.Text = ds2.Tables[0].Rows[0]["SUPPLIER_COMPANY_NAME"].ToString();
                    lnkbtn.HRef = "~/Views/FIT/Manualvoucher/" + ds2.Tables[0].Rows[0]["HOTEL_VOUCHER_ID"].ToString() + "/HotelVoucherManual.pdf";
                    lnkbtn.Attributes.Add("style", "display");
                }
                DataSet dsstatus = objBookingFitStoreProcedure.fetchComboData("FETCH_ALL_CITY_FOR_MASTER");
                binddropdownlist(DropDownList1, dsstatus);
               
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
       
        protected void ddr_CityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds0 = objhotelSeviceVoucher.fetchDataforHotel("GET_HOTEL_NAME_CITY_WISE", DropDownList1.Text);
            if (ds0.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                binddropdownlist(ddrpty_HotelName, ds0);

            }
            upHotelPty.Update();
        }
        
        protected void ddr_HotelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddrpty_RoomType.Items.Clear();
            DataSet ds = objhotelSeviceVoucher.fetchDataforHotelroomtype("GET_ROOM_TYPE_FROM_HOTEL_NAME", ddrpty_HotelName.SelectedValue);
            binddropdownlist(ddrpty_RoomType, ds);
            upHotelPty.Update();
        }
        
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.Text != "")
            {
                DataSet ds = objhotelSeviceVoucher.get_cust_rel_sr_no("FETCH_EMPLOYEE_ID_FOR_PAYMENT", DropDownList2.Text);
                ViewState["cust_rel_no"] = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
                
                //binddropdownlist(drpvoucher_type, ds);
                ViewState["cust_sr_no"] = ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                DropDownList3.Items.Clear();

                DataSet ds1 = objhotelSeviceVoucher.get_invoice_left("GET_INVOICE_NO", int.Parse(ds.Tables[0].Rows[0]["USER_ID"].ToString()));
                //    DataSet ds1 = objVouchersStoredProcedure.get_invoice_no_agent_vise("FETCH_INVOICE_NO_AGENT_VISE", int.Parse(ds.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString()));
                binddropdownlist(DropDownList3, ds1);


                string name = DropDownList2.Text;
                DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();

                DataTable dtad = objHotelStoreProcedure.objfetchusername("FETCH_USER_NAME_FOR_MAIL", Session["rel_sr_no"].ToString());
                Session["email"] = dtad.Rows[0]["CUST_REL_EMAIL"].ToString();
                Session["agentname"] = dtad.Rows[0]["CUST_REL_NAME"].ToString();
                //ViewState["row1_receipt_glcode"] = ds.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
            }
            else
            {
                
            }
            upHotelPty.Update();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DSNAME=objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);
            txtClientname.Text = DSNAME.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
            txtnonights.Text = DSNAME.Tables[0].Rows[0]["NO_OF_NIGHTS"].ToString();
            txtpty_CheckIn.Text = DSNAME.Tables[0].Rows[0]["TOUR_FROM_DATE"].ToString();
            txtpty_CheckOut.Text = DSNAME.Tables[0].Rows[0]["TOUR_TO_DATE"].ToString();
            ViewState["user_id"] = DSNAME.Tables[0].Rows[0]["ORDER_BY_ID"].ToString();
            ViewState["quote_id"] = DSNAME.Tables[0].Rows[0]["QUOTE_ID"].ToString();
            upHotelPty.Update();
        }

        protected void txtpty_CheckIn_TextChanged(object sender, EventArgs e)
        {

        }
        
        protected void txtpty_CheckOut_TextChanged(object sender, EventArgs e)
        {

        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = "";
                if (Request["IN"] != null && !string.IsNullOrEmpty(Request["IN"].ToString()))
                {

                    CRM.DataAccess.FIT.HotelServiceVoucher objinserthotelSeviceVoucher = new CRM.DataAccess.FIT.HotelServiceVoucher();
                    if (txttotalroom.Text == "")
                    {
                        txttotalroom.Text = "0";
                    }
                    if (txtNoroomSingle.Text == "")
                    {
                        txtNoroomSingle.Text = "0";
                    }
                    if (txtNoroomDouble.Text == "")
                    {
                        txtNoroomDouble.Text = "0";
                    }
                    if (txtNoroomTriple.Text == "")
                    {
                        txtNoroomTriple.Text = "0";
                    }
                    if (txtnonights.Text == "")
                    {
                        txtnonights.Text = "0";
                    }

                    DataSet ds = objinserthotelSeviceVoucher.InsertHotelServiceVoucher(int.Parse(Request["IN"]), txtClientname.Text, DropDownList1.SelectedValue, ddrpty_HotelName.SelectedValue, ddrpty_RoomType.SelectedValue, txtpty_CheckIn.Text, txtpty_CheckOut.Text, int.Parse(txttotalroom.Text), int.Parse(txtNoroomSingle.Text), int.Parse(txtNoroomDouble.Text), int.Parse(txtNoroomTriple.Text), DropDownList2.SelectedValue, txtVoucherNo.Text, int.Parse(txtnonights.Text), int.Parse(Session["uid"].ToString()), DropDownList3.Text);
                    Master.DisplayMessage("Record Update Successfully.", "successMessage", 3000);
                    //Clear();
                    ID = Request["IN"].ToString();
                    upHotelPty.Update();
                }
                else
                {
                    CRM.DataAccess.FIT.HotelServiceVoucher objinserthotelSeviceVoucher = new CRM.DataAccess.FIT.HotelServiceVoucher();
                    if (txttotalroom.Text == "")
                    {
                        txttotalroom.Text = "0";
                    }
                    if (txtNoroomSingle.Text == "")
                    {
                        txtNoroomSingle.Text = "0";
                    }
                    if (txtNoroomDouble.Text == "")
                    {
                        txtNoroomDouble.Text = "0";
                    }
                    if (txtNoroomTriple.Text == "")
                    {
                        txtNoroomTriple.Text = "0";
                    }
                    if (txtnonights.Text == "")
                    {
                        txtnonights.Text = "0";
                    }

                    DataSet ds = objinserthotelSeviceVoucher.InsertHotelServiceVoucher(0, txtClientname.Text, DropDownList1.SelectedValue, ddrpty_HotelName.SelectedValue, ddrpty_RoomType.SelectedValue, txtpty_CheckIn.Text, txtpty_CheckOut.Text, int.Parse(txttotalroom.Text), int.Parse(txtNoroomSingle.Text), int.Parse(txtNoroomDouble.Text), int.Parse(txtNoroomTriple.Text), DropDownList2.SelectedValue, txtVoucherNo.Text, int.Parse(txtnonights.Text), int.Parse(Session["uid"].ToString()), DropDownList3.Text);
                    ID = ds.Tables[0].Rows[0]["HOTEL_VOUCHER_ID"].ToString();

                    DataSet DSNAME = objhotelSeviceVoucher.fetchDataforCLIENTNAME("FETCH_CLIENT_NAME_FOR_HOTEL_SERVICE_VOUCHER", DropDownList3.Text);
                    string txtno_of_nights1 = "0";
                    string txtperiod_stay_from1 = txtpty_CheckIn.Text;
                    string txtperiod_stay_to1 = txtpty_CheckOut.Text;
                    DateTime date11 = DateTime.ParseExact(txtperiod_stay_from1, "dd/MM/yyyy", null);
                    DateTime date21 = DateTime.ParseExact(txtperiod_stay_to1, "dd/MM/yyyy", null);
                    TimeSpan ts1;
                    ts1 = date21.Subtract(date11.Date);
                    txtno_of_nights1 = ts1.TotalDays.ToString();

                    if (txtNoroomSingle.Text == "")
                    {
                        txtNoroomSingle.Text = "0";
                    }

                    if (txtNoroomDouble.Text == "")
                    {
                        txtNoroomDouble.Text = "0";
                    }

                    if (txtNoroomTriple.Text == "")
                    {
                        txtNoroomTriple.Text = "0";
                    }

                    DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
                    DataSet ds_sup_type = objFITPaymentStoreProcedure.fetch_supplier_type("FETCH_SUPPLIER_TYPE");

                    DataSet dspurchase = objFITPaymentStoreProcedure.insert_purchase_entry(0, ddrpty_HotelName.Text, DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", "0", "", "0", ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse("2"), int.Parse("0"), int.Parse("0"), int.Parse("0"), txtperiod_stay_from1, txtperiod_stay_to1, int.Parse(txtno_of_nights1), int.Parse(txtNoroomSingle.Text), int.Parse(txtNoroomDouble.Text), int.Parse(txtNoroomTriple.Text), ddrpty_RoomType.Text, 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 1);

                    objFITPaymentStoreProcedure.insert_purchase_entry(0, ddrpty_HotelName.Text, DropDownList3.Text, int.Parse(Session["uid"].ToString()), "", "0", "", "0", ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), "", int.Parse(Session["uid"].ToString()), ds_sup_type.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 0, "DESCRIPTION", int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_ADULTS"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CWB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_CNB"].ToString()), int.Parse(DSNAME.Tables[0].Rows[0]["NO_OF_INFANT"].ToString()), txtperiod_stay_from1, txtperiod_stay_to1, int.Parse(txtno_of_nights1), int.Parse(txtNoroomSingle.Text), int.Parse(txtNoroomDouble.Text), int.Parse(txtNoroomTriple.Text), ddrpty_RoomType.Text, 0, "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), 2);

                    DataSet dssupplier = objhotelSeviceVoucher.fetchDataforsupid("FETCH_HOTEL_SUPPLIER_ID", ddrpty_HotelName.Text);

                    DataSet dsgl_hotel = objFITPaymentStoreProcedure.set_gl_code("FETCH_GENERAL_LEGER_CODE", dssupplier.Tables[0].Rows[0]["SUPPLIER_ID"].ToString(), "S");
                    DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
                    DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();

                    DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), "", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", "0", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, dsgl_hotel.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), "", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "0", "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), dspurchase.Tables[0].Rows[0]["PURCHASE_INVOICE_NO"].ToString(), "", ds_vt.Tables[0].Rows[7]["AutoSearchResult"].ToString(), int.Parse(Session["uid"].ToString()), "", int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), int.Parse(Session["uid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", "0", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                }
            
           
                // create report
                if (!System.IO.Directory.Exists(Server.MapPath("~/Views/FIT/Manualvoucher/" + ID + "/")))
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Views/FIT/Manualvoucher/" + ID + "/"));

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>9in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";



                // quote_id = Page.Request.QueryString["QuoteId"].ToString();

                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("HOTEL_VOUCHER_ID", ID);
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowParameterPrompts = false;

                rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                rptViewer1.ServerReport.ReportPath = "/ThailandReport/HotelVoucherManual";
                rptViewer1.ServerReport.SetParameters(parm);
                rptViewer1.ServerReport.Refresh();

                renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                rptViewer1.Visible = false;
                Response.Clear();
                using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath("~/Views/FIT/Manualvoucher/" + ID + "/HotelVoucherManual.pdf")))
                {
                    fs.Write(renderedBytes, 0, (int)renderedBytes.Length);
                }

                lnkbtn.HRef = "~/Views/FIT/Manualvoucher/" + ID + "/HotelVoucherManual.pdf";
                lnkbtn.Attributes.Add("style", "display");


                if (txtNoroomSingle.Text != "0")
                {
                    insert_hotel_OS(1, int.Parse(txtNoroomSingle.Text));
                }

                if (txtNoroomDouble.Text != "0")
                {
                    insert_hotel_OS(2, int.Parse(txtNoroomDouble.Text));
                }

                if (txtNoroomTriple.Text != "0")
                {
                    insert_hotel_OS(3, int.Parse(txtNoroomTriple.Text));
                }


                Master.DisplayMessage("Record Save Successfully.", "successMessage", 3000);
                Clear();
                upHotelPty.Update();
            }
        
            catch
            {
            }

            
        }

        protected void Clear()
        {
            txttotalroom.Text = "";
            txtClientname.Text = "";
            txtNoroomSingle.Text = "";
            txtNoroomDouble.Text = "";
            txtNoroomTriple.Text = "";
            txtnonights.Text = "";
            txtpty_CheckIn.Text = "";
            txtpty_CheckOut.Text = "";
            txtVoucherNo.Text = "";

            DropDownList1.SelectedValue = "0";
            ddrpty_HotelName.SelectedValue = "0";
            ddrpty_RoomType.SelectedValue = "0";
            DropDownList2.SelectedValue = "0";
            DropDownList3.SelectedValue = "0";
           
        }

        protected void insert_hotel_OS(int room_type, int no_of_rrom)
        {
            hotelcartid = 0;

            cityid = DropDownList1.Text;

            hotelid = ddrpty_HotelName.Text;
            DateTime date11 = DateTime.ParseExact(txtpty_CheckIn.Text, "dd/MM/yyyy", null);
            string test = date11.ToShortDateString();
            fromdate = txtpty_CheckIn.Text;

            todate = txtpty_CheckOut.Text;

            roomtype = room_type;

            noofroms = no_of_rrom; 

            roomtypeid = ddrpty_RoomType.Text;

            orderstatus = "To Be Reconfirmed";

            userid = int.Parse(ViewState["user_id"].ToString());

            startdate = txtpty_CheckIn.Text;

            enddate = txtpty_CheckOut.Text;

            objhotelSeviceVoucher.insertupdate_Hotels(hotelcartid, cityid, hotelid, fromdate, todate, noofroms, orderstatus, userid, roomtype, roomtypeid, startdate, enddate, true, int.Parse(ViewState["quote_id"].ToString()));





            /* hotel sub category*/
            string old_id = "";
            string new_id = "";
            bool flag_new = true;
            DataSet ds_hotel_detials = objFITPaymentStoreProcedure.fetch_hotel_detials("FETCH_HOTEL_DETAILS", ddrpty_HotelName.Text, ddrpty_RoomType.Text);
            string txtperiod_stay_from = txtpty_CheckIn.Text;
            string txtperiod_stay_to = txtpty_CheckOut.Text;
            DateTime date1 = DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null);
            DateTime date2 = DateTime.ParseExact(txtperiod_stay_to, "dd/MM/yyyy", null);
            TimeSpan ts;
            ts = date2.Subtract(date1.Date);
            string txtno_of_nights = ts.TotalDays.ToString();

            for (int night = 0; night < int.Parse(txtno_of_nights) + 1; night++)
            {
                DateTime dat = DateTime.ParseExact(fromdate, "dd/MM/yyyy", null);
                dat = dat.AddDays(night);
                // txtperiod_stay_from = dat.ToString();

                dat.ToString("MM/dd/yyyy");
                string source1 = dat.ToString();
                string str11 = source1;
                string[] w1 = str11.Split('/');

                string t12 = w1[2];
                string[] t11 = t12.Split(' ');

                if (w1[1] == "1" || w1[1] == "2" || w1[1] == "3" || w1[1] == "4" || w1[1] == "5" || w1[1] == "6" || w1[1] == "7" || w1[1] == "8" || w1[1] == "9")
                {
                    if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                    {
                        txtperiod_stay_from = "0" + w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                        //  lbl_voucher_date.Text = result;
                    }
                    else
                    {
                        txtperiod_stay_from = w1[0] + "/" + "0" + w1[1] + "/" + t11[0];
                        //  lbl_voucher_date.Text = result;
                    }
                }
                else
                {
                    if (w1[0] == "1" || w1[0] == "2" || w1[0] == "3" || w1[0] == "4" || w1[0] == "5" || w1[0] == "6" || w1[0] == "7" || w1[0] == "8" || w1[0] == "9")
                    {
                        txtperiod_stay_from = "0" + w1[0] + "/" + w1[1] + "/" + t11[0];
                        //  lbl_voucher_date.Text = result;
                    }
                    else
                    {
                        txtperiod_stay_from = w1[0] + "/" + w1[1] + "/" + t11[0];
                        // lbl_voucher_date.Text = result;
                    }
                }

                for (int k = 0; k < ds_hotel_detials.Tables[0].Rows.Count; k++)
                {
                    if ((DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null) >= DateTime.ParseExact(ds_hotel_detials.Tables[0].Rows[k]["FROM_DATE"].ToString(), "dd/MM/yyyy", null)) && (DateTime.ParseExact(txtperiod_stay_from, "dd/MM/yyyy", null) <= DateTime.ParseExact(ds_hotel_detials.Tables[0].Rows[k]["TO_DATE"].ToString(), "dd/MM/yyyy", null)))
                    {
                        if (night == 0)
                        {
                            old_id = ds_hotel_detials.Tables[0].Rows[k]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString();
                            new_id = ds_hotel_detials.Tables[0].Rows[k]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString();
                        }
                        if (night != 0)
                        {
                            new_id = ds_hotel_detials.Tables[0].Rows[k]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString();
                        }
                        if (old_id != new_id)
                        {
                            flag_new = false;
                            objHotelStoreProcedure.insertupdate_Hotels_sub_details(hotelcartid, cityid, hotelid, fromdate, ds_hotel_detials.Tables[0].Rows[k - 1]["TO_DATE"].ToString(), noofroms, orderstatus, userid, roomtype, roomtypeid, startdate, enddate, true);
                            old_id = ds_hotel_detials.Tables[0].Rows[k]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString();
                            new_id = ds_hotel_detials.Tables[0].Rows[k]["SUPPLIER_HOTEL_PRICE_LIST_ID"].ToString();

                        }
                        if (night == int.Parse(txtno_of_nights))
                        {
                            if (flag_new == false)
                            {
                                objHotelStoreProcedure.insertupdate_Hotels_sub_details(hotelcartid, cityid, hotelid, ds_hotel_detials.Tables[0].Rows[k]["FROM_DATE"].ToString(), todate, noofroms, orderstatus, userid, roomtype, roomtypeid, startdate, enddate, true);
                            }
                            if (flag_new == true)
                            {
                                objHotelStoreProcedure.insertupdate_Hotels_sub_details(hotelcartid, cityid, hotelid, fromdate, todate, noofroms, orderstatus, userid, roomtype, roomtypeid, startdate, enddate, true);
                            }
                        }
                    
                    }

                }

            }
        }
    }
}