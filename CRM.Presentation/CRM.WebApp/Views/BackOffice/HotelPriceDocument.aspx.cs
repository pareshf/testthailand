using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;
using System.Data;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.BackOffice
{
    public partial class HotelPriceDocument : System.Web.UI.Page
    {
        SupplierHotelPriceStoredProcedure objhotelprice = new SupplierHotelPriceStoredProcedure();
        int docid = 0;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            docid = int.Parse(Page.Request.QueryString["key"].ToString());
            if (!IsPostBack)
            {

                SupplierHotelPriceStoredProcedure objhotelprice = new SupplierHotelPriceStoredProcedure();
                DataSet ds = objhotelprice.getHotelPriceDocument(docid);
                pricedoc.HRef = "~/SupplierHotelDocument/" + docid + "/" + ds.Tables[0].Rows[0]["UPLOAD_RATE_DOCUMENT"].ToString();
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/SupplierHotelDocument/" + docid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/SupplierHotelDocument/" + docid.ToString() + "/"));

            if (fupdoc.HasFile)
            {
                fupdoc.SaveAs(Server.MapPath("~/SupplierHotelDocument/" + docid.ToString() + "/") + docid.ToString() + fupdoc.FileName);
                objhotelprice.insertHotelProiceDocument(docid, docid.ToString() + fupdoc.FileName);
                Response.Write("<script>alert('Document Upload Successfully.')</script>");

            }
        }
    }
}