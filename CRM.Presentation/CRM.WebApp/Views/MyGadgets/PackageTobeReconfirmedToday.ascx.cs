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
    public partial class PackageTobeReconfirmedToday : System.Web.UI.UserControl
    {

        private int _empID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            string usrid = Session["usersid"].ToString();
            if (objAuthorizationBDto != null)
            {
                _empID = Convert.ToInt32(Session["empid"]);
            }
            String query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND RECONFIRMATION_DATE BETWEEN GETDATE() AND GETDATE()+7";
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {

                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED", usrid);
                Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

                //_sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + " " + dr["TOUR_SHORT_NAME"].ToString() + " " + dr["DATE"].ToString() + "<a href=\"../../Views/FIT/BookingFit.aspx?TOURID=" + dr["TOUR_ID"].ToString() + "&QUOTEID=" + dr["QUOTE_ID"].ToString() + "" + "\"><br/>View</a>");

                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + " " + dr["TOUR_SHORT_NAME"].ToString() + "<div align=\"left\" style=\"font-size:11px;color:red;\">" + dr["DATE"].ToString() + "<a href=\"../../Views/FIT/BookingFit.aspx?TOURID=" + dr["TOUR_ID"].ToString() + "&QUOTEID=" + dr["QUOTE_ID"].ToString() + "" + "\"><br/>View</a>");


            }
            _sbi.AppendLine("</ul>");
            // _sbi.AppendLine("<a href=\"../../Views/FIT/AllBookingsToBeReconfirmed.aspx\">View All</a>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}