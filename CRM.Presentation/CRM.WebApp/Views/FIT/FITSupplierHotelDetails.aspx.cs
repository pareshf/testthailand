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

namespace CRM.WebApp.Views.FIT
{
    public partial class FITSupplierHotelDetails : System.Web.UI.Page
    {
        SupplierPriceListDetails ObjSupplierPriceListDetails = new SupplierPriceListDetails();
        //int SID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SID = Convert.ToInt32(Session["supplierid"].ToString());
                BindRoomType();
                BindStatus();
                string ROOM = "";
                string Status = "";
                DataSet ds = ObjSupplierPriceListDetails.GetSupplierDetails(Convert.ToInt32(Session["supplierid"].ToString()), ROOM, Status );
                dgvSupplierPriceDetails.DataSource = ds;
                dgvSupplierPriceDetails.DataBind();
            }
        }
        #region Grid View Functions
        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            string supplierPRICEID = dgvSupplierPriceDetails.Rows[newindex].Cells[1].Text;/// Price list Primary key 

            Session["supplierPRICEID"] = supplierPRICEID;
            Label lbl = new Label();
            lbl = (Label)dgvSupplierPriceDetails.Rows[newindex].FindControl("lblSUPPLIER_SR_NO");
            //int suppid = SID;
            // string suppliersrno  = GV_HotelPriceList.Rows[newindex].Cells[2].Text;
            Session["InsertUpdate"] = "U";
            Response.Redirect("~/Views/FIT/FITPriceListMaster.aspx?SUPPLIERPICELISTID=" + Convert.ToInt32(Session["supplierPRICEID"].ToString()) + "&SUPPLIERID=" + Convert.ToInt32(Session["supplierid"].ToString())); //Price list Primary key
            
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSupplierPriceDetails.PageIndex = e.NewPageIndex;
            string ROOM = ddlRoomType.Text;
            string Status = ddlStatus.Text;
            DataSet ds = ObjSupplierPriceListDetails.GetSupplierDetails(Convert.ToInt32(Session["supplierid"].ToString()), ROOM, Status);
            dgvSupplierPriceDetails.DataSource = ds;
            dgvSupplierPriceDetails.DataBind();
            upPriceList.Update();
        }
        #endregion

        #region Button Click Events
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["InsertUpdate"] = "I";
            Session["supplierPRICEID"] = 0;
            Response.Redirect("~/Views/FIT/FITPriceListMaster.aspx?SUPPLIERID=" + Convert.ToInt32(Session["supplierid"].ToString())); //Price list Primary key
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["supplierid"] = 0;
            Response.Redirect("~/Views/GITMaster/AllSupplier.aspx");
        }
        protected void btn_Search_OnClick(object sender, EventArgs e)
        {
            pnlMainHead.Attributes.Add("style", "display");
            btn_SearchNow.Attributes.Add("style", "display");
            btn_Search.Attributes.Add("style", "display:none");
            upPriceList.Update();
        }
        protected void btn_SearchNow_Onclick(object sender, EventArgs e)
        {
            string ROOM = ddlRoomType.Text; 
            string Status = ddlStatus.Text;
            DataSet ds = ObjSupplierPriceListDetails.GetSupplierDetails(Convert.ToInt32(Session["supplierid"].ToString()), ROOM, Status);
            pnlMainHead.Attributes.Add("style", "display:none");
            btn_SearchNow.Attributes.Add("style", "display:none");
            btn_Search.Attributes.Add("style", "display");
            dgvSupplierPriceDetails.DataSource = ds.Tables[0];
            dgvSupplierPriceDetails.DataBind();
            upPriceList.Update();
        }
        #endregion

        #region Bind Functions
        public void BindRoomType()
        {
            DataTable dt = ObjSupplierPriceListDetails.GetRoomType();
            ddlRoomType.DataSource = dt;
            ddlRoomType.DataTextField = "AutoSearchResult";
            ddlRoomType.DataBind();
            ddlRoomType.Items.Insert(0, new ListItem("", ""));
        }
        public void BindStatus()
        {
            DataTable dt = ObjSupplierPriceListDetails.GetStatus();
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "AutoSearchResult";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}