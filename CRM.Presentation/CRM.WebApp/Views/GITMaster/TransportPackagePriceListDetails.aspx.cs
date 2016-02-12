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
    public partial class TransportPackagePriceListDetails : System.Web.UI.Page
    {
        int supplierid;

        TransferPackagePriceListDetailsDal objTransferPackagePriceListDetails = new TransferPackagePriceListDetailsDal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierid = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
                DataSet ds = objTransferPackagePriceListDetails.fetchTransferPriceListData(supplierid); //// Bind With Supplier SR.No
                GV_TransferPackagePriceList.DataSource = ds;
                GV_TransferPackagePriceList.DataBind();
            }
            else
            {
                if (Request["SUPPLIERID"] != null && !string.IsNullOrEmpty(Request["SUPPLIERID"].ToString()))
                {
                    supplierid = Convert.ToInt32(Request.QueryString["SUPPLIERID"].ToString());
                }
            }
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            string TransferPriceListId = GV_TransferPackagePriceList.Rows[newindex].Cells[1].Text; /// Price list Primary key 
            int suppid = supplierid;
            Response.Redirect("~/Views/GITMaster/TransportPackgePriceListGIT.aspx?TransferPriceListId=" + TransferPriceListId + "&SUPPLIERID=" + suppid); //Price list Primary key         
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_TransferPackagePriceList.PageIndex = e.NewPageIndex;

            DataSet ds = objTransferPackagePriceListDetails.fetchTransferPriceListData(supplierid);
            GV_TransferPackagePriceList.DataSource = ds;
            GV_TransferPackagePriceList.DataBind();
            upTransferPackagePriceList.Update();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/TransportPackgePriceListGIT.aspx?SUPPLIERID=" + supplierid);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GITMaster/AllSupplier.aspx"); // redirect with Supplier id             
        }
    }
}