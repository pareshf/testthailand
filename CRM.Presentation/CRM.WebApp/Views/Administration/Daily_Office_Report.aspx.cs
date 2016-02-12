using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Web.Configuration;
using SSRSReporting;

namespace CRM.WebApp.Views.Administration
{
    public partial class Daily_Office_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            string fromdate;
            fromdate = txtfromdate.Text;
            Response.Redirect("~/Views/Administration/OfficeRport.aspx?key=" + fromdate); 
        }
    }
}