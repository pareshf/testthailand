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
using System.IO;
using System.Text;

namespace CRM.WebApp.Views.MyGadgets
{
    public partial class PackageToBeReConfirmed : System.Web.UI.UserControl
    {
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        CRM.DataAccess.FIT.FitQuotes objfitquote = new CRM.DataAccess.FIT.FitQuotes();
        protected void Page_Load(object sender, EventArgs e)
        {
            string usrid = Session["usersid"].ToString();
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;
            // Create new data series and set it's visual attributes
            //Series series = new Series("Spline");
            //series.ChartType = SeriesChartType.Column;
            //series.BorderWidth = 3;
            //series.ShadowOffset = 2;

            //general setting
            // set query according to from and to date
            DataSet _Ds = objdeshboardentity.getfilter(_userID);
            DataTable _DT = _Ds.Tables[0];
            DataTable _DT1 = _Ds.Tables[1];
            //GET FILTER DETAILS FROM HERE
            string query = string.Empty;
            StringBuilder _employee = new StringBuilder();
            if (_DT.Rows[0]["EMP_ID"].ToString() == null || _DT.Rows[0]["EMP_ID"].ToString() == "0" || _DT.Rows[0]["EMP_ID"].ToString() == "")
            {
                foreach (DataRow dr in _DT1.Rows)
                {
                    _employee.Append(dr["EMP_ID"].ToString() + ",");
                }
                _employee.Append("0");
            }
            else
            {
                foreach (DataRow dr in _DT1.Rows)
                {
                    if (dr["RESULT"].ToString() == _DT.Rows[0]["EMP_ID"].ToString())
                        _employee.Append(dr["EMP_ID"].ToString() + ",");
                }
                _employee.Append("0");

            }
            switch (_DT.Rows[0]["FILTER_ID"].ToString())
            {
                case "12":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101)= CONVERT(VARCHAR(5),getdate(), 101)";
                    break;
                case "13":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) = CONVERT(VARCHAR(5),dateadd(DAY,-1,getdate()), 101) ";
                    break;
                case "14":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) >= CONVERT(VARCHAR(5),getdate()+ 7, 101)";
                    break;
                case "15":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) <= CONVERT(VARCHAR(5),getdate()-7, 101)";
                    break;
                case "16":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) >= CONVERT(VARCHAR(5),getdate()+14, 101)";
                    break;
                case "17":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) <= CONVERT(VARCHAR(5),getdate()-14, 101)";
                    break;
                case "18":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) >= CONVERT(VARCHAR(5),DATEADD(MONTH,+1,GETDATE()),101)";
                    break;
                case "19":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) <= CONVERT(VARCHAR(5),DATEADD(MONTH,-1,GETDATE()),101)";

                    break;
                case "20":
                    //query = "SELECT CR_AMOUNT AS TOTAL_PAYMENT_MADE,GL_CODE  FROM PURCHASE_PAYMENT_VOUCHER_DETAILS where datepart(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE) = datepart(month,getdate())";  /*Running */
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),MONTH(TOUR_QUOTE_MASTER.RECONFIRMATION_DATE), 101) = CONVERT(VARCHAR(5),MONTH(getdate()), 101)";
                    break;
                case "21":
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) = CONVERT(VARCHAR(5),GETDATE()+1, 101)";
                    break;
                default:
                    query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID,CONVERT(varchar(10),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE,103) AS DATE FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) >=CONVERT(VARCHAR(5),'" + _DT.Rows[0]["FROM_DATE"].ToString() + "', 101) and CONVERT(VARCHAR(5),TOUR_QUOTE_MASTER.RECONFIRMATION_DATE, 101) <=CONVERT(VARCHAR(5),'" + _DT.Rows[0]["TO_DATE"].ToString() + "', 101)";
                    break;
            }

           
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