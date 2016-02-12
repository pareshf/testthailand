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

namespace CRM.WebApp.Views.Administration
{
    public partial class GuidePriceDocument : System.Web.UI.Page
    {
        int docid = 0;

        SupplierGuidePriceStoredProcedure objinsertdoc = new SupplierGuidePriceStoredProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {
            docid = int.Parse(Page.Request.QueryString["key"].ToString());
            if (!IsPostBack)
            {

                SupplierGuidePriceStoredProcedure objinsertdoc = new SupplierGuidePriceStoredProcedure();
                DataSet ds = objinsertdoc.getGuidePriceDocument(docid);
                guidedoc.HRef = "~/SupplierGuideDocument/" + docid + "/" + ds.Tables[0].Rows[0]["UPLOAD_RATE_DOCUMENT"].ToString();
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/SupplierGuideDocument/" + docid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/SupplierGuideDocument/" + docid.ToString() + "/"));

            if (fupguidedoc.HasFile)
            {
                fupguidedoc.SaveAs(Server.MapPath("~/SupplierGuideDocument/" + docid.ToString() + "/") + docid.ToString() + fupguidedoc.FileName);
                objinsertdoc.insertUpdateGuidePriceDocument(docid, docid.ToString() + fupguidedoc.FileName);
                Response.Write("<script>alert('Document Upload Successfully.')</script>");

            }
        }
    }
}