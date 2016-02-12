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
using System.Net;

namespace CRM.WebApp.Views.Account
{
    public partial class _new : System.Web.UI.Page
    {
        DownloadVoucherStoredProcedure objDownloadVoucherStoredProcedure = new DownloadVoucherStoredProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {

            int index = int.Parse(Session["id"].ToString());
            DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(Session["quote_id"].ToString()));


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == index)
                {
                    if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Hotel")
                    {
                        Response.Redirect("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "HotelVoucher" +" "+ ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() +".pdf");
                             break;
                    }
                    else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Transfer Package Company")
                    {
                        Response.Redirect("~/Views/FIT/TransferVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "TransferVoucher"+" "+ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf");
                        break;
                    }
                    else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Sightseeing Company")
                    {
                        Response.Redirect("~/Views/FIT/SightSeeingVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "SightSeeingVoucher" +" "+ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + ".pdf");
                        break;
                    }
                    else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Additional Services")
                    {
                        Response.Redirect("~/Views/FIT/AdditionalServicesVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "Additional Service Voucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf");
                        break;
                    }
                }
            }
        }
    }
}