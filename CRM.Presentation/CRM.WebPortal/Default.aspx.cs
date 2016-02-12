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
using CRM.WebPortal.DataAccess;

namespace CRM.WebPortal
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebDataAccess webDataAccess = new WebDataAccess();
            DataSet ds = webDataAccess.GetFlightFares();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtIntFares = ds.Tables[0];
                lstInternationalFares.DataSource = dtIntFares;
                lstInternationalFares.DataBind();
            }

            if (ds != null && ds.Tables.Count > 1)
            {
                DataTable dtDomeFares = ds.Tables[1];
                lstDomesticFares.DataSource = dtDomeFares;
                lstDomesticFares.DataBind();
            }

            DataTable dt = webDataAccess.GetTourFlash();
            if (dt != null && dt.Rows.Count > 0)
            {
                dvTopLeft.InnerHtml = dt.Rows[0]["TOP_LEFT"].ToString().Trim();
                dvTopRight.InnerHtml = dt.Rows[0]["TOP_RIGHT"].ToString().Trim();
                dvBottomLeft.InnerHtml = dt.Rows[0]["BOTTOM_LEFT"].ToString().Trim();
                dvBottomRight.InnerHtml = dt.Rows[0]["BOTTOM_RIGHT"].ToString().Trim();
            }
        }
    }
}
