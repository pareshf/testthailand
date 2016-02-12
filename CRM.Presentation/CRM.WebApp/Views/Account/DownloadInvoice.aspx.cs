using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.Account
{
    public partial class DownloadInvoice : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Redirect("~/Views/FIT/Invoices/" + Session["Invoice_ID"].ToString() + "/Invoice.pdf");

            }
        }
    }
}