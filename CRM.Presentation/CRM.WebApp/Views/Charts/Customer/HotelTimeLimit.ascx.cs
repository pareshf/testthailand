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

namespace CRM.WebApp.Views.Charts.Customer
{
    public partial class HotelTimeLimit : System.Web.UI.UserControl
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
            String query = "SELECT TOP 15 FARE_HOTEL_MASTER.HOTEL_NAME,CONVERT(varchar(10),FARE_TOUR_HOTEL_DETAILS.TIME_LIMIT,101) AS DATE,FARE_TOUR_MASTER.TOUR_SHORT_NAME FROM FARE_TOUR_HOTEL_DETAILS,FARE_HOTEL_MASTER,FARE_TOUR_MASTER WHERE FARE_TOUR_HOTEL_DETAILS.TOUR_ID = FARE_TOUR_MASTER.TOUR_ID AND FARE_TOUR_HOTEL_DETAILS.HOTEL_ID = FARE_HOTEL_MASTER.HOTEL_ID AND FARE_TOUR_HOTEL_DETAILS.BOOKING_STATUS != 1 AND DATEDIFF(dd,GETDATE(),FARE_TOUR_HOTEL_DETAILS.TIME_LIMIT) <= 15 AND FARE_TOUR_HOTEL_DETAILS.BOOKING_REQUEST_TO = " + _empID;
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:15px;padding:3px;\">" + dr["HOTEL_NAME"].ToString() + "</br><div align=\"left\" style=\"font-size:11px;color:red;\"><b>Time Limit : " + dr["DATE"].ToString() + "</b></br> Tour : " + dr["TOUR_SHORT_NAME"].ToString() + "</div></li>");
            }
            _sbi.AppendLine("</ul>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}