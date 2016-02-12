using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text;

namespace CRM.WebApp
{
    public partial class ExceptionHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            Exception exception = (Exception)ctx.Application.Get("LastError");

            if (exception != null)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("<h3>ERROR</h3>");
                builder.Append("<hr>");
                builder.Append("<br /><b>Offending URL   :</b> " + exception.TargetSite);
                builder.Append("<br /><br /><b>Source      :</b> " + exception.Source);
                builder.Append("<br /><br /><b>Message     :</b> " + exception.Message);
                builder.Append("<br /><br /><b>Stack Trace :</b> <code><pre>" + exception.StackTrace + "</pre></code>");

                lblError.Text = builder.ToString();
            }
        }
    }
}
