using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CRM.WebApp
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //this.PageHead.Controls.Add(new LiteralControl(
            //    String.Format("<meta http-equiv='refresh' content='{0};url={1}'>",
            //    SessionLengthMinutes * 60, SessionExpireDestinationUrl)));

            //Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 3));
            //if (Session["usersid"] == null)
            //{ Server.Transfer("~/Login.aspx"); }
        }
    }
}
