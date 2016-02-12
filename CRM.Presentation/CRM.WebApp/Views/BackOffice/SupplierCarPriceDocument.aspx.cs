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
    public partial class SupplierCarPriceDocument : System.Web.UI.Page
    {
        int docid = 0;
        SupplierCarPriceStoredProcedure objinsercardoc = new SupplierCarPriceStoredProcedure();

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

                SupplierCarPriceStoredProcedure objinsercardoc = new SupplierCarPriceStoredProcedure();
                DataSet ds = objinsercardoc.getCarPriceDocument(docid);
                cardoc.HRef = "~/SupplierCarDocument/" + docid + "/" + ds.Tables[0].Rows[0]["UPLOAD_RATE_DOCUMENT"].ToString();
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/SupplierCarDocument/" + docid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/SupplierCarDocument/" + docid.ToString() + "/"));

            if (fupcardoc.HasFile)
            {
                fupcardoc.SaveAs(Server.MapPath("~/SupplierCarDocument/" + docid.ToString() + "/") + docid.ToString() + fupcardoc.FileName);
                objinsercardoc.insertUpdateCarPriceDocument(docid, docid.ToString() + fupcardoc.FileName);
                Response.Write("<script>alert('Document Upload Successfully.')</script>");

            }
        }
    }
}