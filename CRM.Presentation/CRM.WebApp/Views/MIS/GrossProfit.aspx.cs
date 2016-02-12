using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.MIS
{
    public partial class GrossProfit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/MIS/GrossProfitRpt.aspx");
        }
    }
}