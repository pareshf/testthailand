using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using CRM.DataAccess.Dashboard;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.DataAccess;
using System.Text;

namespace CRM.WebApp.Views.MyGadgets
{
    public partial class KrabiBestBuyHotels : System.Web.UI.UserControl
    {
        private int _empID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                _empID = Convert.ToInt32(Session["empid"]);
            }
            String query = "SELECT SUPPLIER_CONTACT_DETAILS.CHAIN_NAME , HOTEL_DASHBOARD_MASTER.DESCRIPTION FROM HOTEL_DASHBOARD_MASTER LEFT OUTER JOIN SUPPLIER_CONTACT_DETAILS ON SUPPLIER_CONTACT_DETAILS.SUPPLIER_SR_NO  = HOTEL_DASHBOARD_MASTER.HOTEL_PRICE_LIST_ID WHERE HOTEL_DASHBOARD_MASTER.CITY_ID='572' AND IS_DASHBOARD='YES'";
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + dr["CHAIN_NAME"].ToString()
                  +  "</br><div align=\"left\" style=\"font-size:11px;color:red;\"><b>Description : " + dr["DESCRIPTION"].ToString());
            }
            _sbi.AppendLine("</ul>");
            //  _sbi.AppendLine("<a href=\"../../Views/Sales/Tour.aspx\">View All</a>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}