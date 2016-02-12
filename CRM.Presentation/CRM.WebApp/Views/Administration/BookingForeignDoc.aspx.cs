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
    public partial class BookingForeignDoc : System.Web.UI.Page
    {
        BookingForeignStoredProcedure objdocument=new BookingForeignStoredProcedure();
        int documentid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            documentid = int.Parse(Page.Request.QueryString["key"].ToString());          
            if (!IsPostBack)
            {
                BookingForeignStoredProcedure objdocument = new BookingForeignStoredProcedure();
                DataSet ds = objdocument.getdocumentDetail(documentid);
                docsite.HRef = "~/DocumentForBookingForeign/" + documentid + "/" + ds.Tables[0].Rows[0]["TT_COPY_ATTACHMENT"].ToString();

            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/DocumentForBookingForeign/" + documentid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/DocumentForBookingForeign/" + documentid.ToString() + "/"));

            if (Document.HasFile)
            {
                Document.SaveAs(Server.MapPath("~/DocumentForBookingForeign/" + documentid.ToString() + "/") + documentid.ToString() + Document.FileName);
                objdocument.insertdocument(documentid,documentid.ToString() + Document.FileName);
            }
            Response.Write("<script language='javascript'> { window.close();}</script>");
        }
    }
}