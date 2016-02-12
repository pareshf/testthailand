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
    public partial class SupplierCoachPriceDocument : System.Web.UI.Page
    {
        int docid = 0;
        SupplierCoachPriceStoredProcedure objinsertdoc = new SupplierCoachPriceStoredProcedure();

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

                SupplierCoachPriceStoredProcedure objinsertdoc = new SupplierCoachPriceStoredProcedure();
                DataSet ds = objinsertdoc.getCoachPriceDocument(docid);
                coachdoc.HRef = "~/SupplierCoachDocument/" + docid + "/" + ds.Tables[0].Rows[0]["UPLOAD_RATE_DOCUMENT"].ToString();
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/SupplierCoachDocument/" + docid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/SupplierCoachDocument/" + docid.ToString() + "/"));

            if (fupcoachdoc.HasFile)
            {
                fupcoachdoc.SaveAs(Server.MapPath("~/SupplierCoachDocument/" + docid.ToString() + "/") + docid.ToString() + fupcoachdoc.FileName);
                objinsertdoc.insertUpdateCoachPriceDocument(docid, docid.ToString() + fupcoachdoc.FileName);
                Response.Write("<script>alert('Document Upload Successfully.')</script>");

            }
        }
    }
}