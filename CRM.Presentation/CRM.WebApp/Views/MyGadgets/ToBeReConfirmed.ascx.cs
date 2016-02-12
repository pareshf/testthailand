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
    public partial class ToBeReConfirmed : System.Web.UI.UserControl
    {

        private int _empID;
        private int _userID;
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
                _userID = objAuthorizationBDto.UserProfile.UserId;
                _empID = Convert.ToInt32(Session["empid"]);
            }

            DataSet _Ds = objdeshboardentity.getfilter(_userID);
            DataTable _DT = _Ds.Tables[0];
            DataTable _DT1 = _Ds.Tables[1];
            DataTable dt = null;
            string query = string.Empty;


            if (_DT.Rows[0]["FROM_DATE"].ToString() != "" && _DT.Rows[0]["TO_DATE"].ToString() != "")
            {
                query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  >=  '" + _DT.Rows[0]["FROM_DATE"].ToString() + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  <=  '" + _DT.Rows[0]["TO_DATE"].ToString() + "'";
            }

            else
            {

                switch (_DT.Rows[0]["FILTER_ID"].ToString())
                {
                    case "12":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate(), 103) ";
                        break;
                    case "13":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate()-1, 103) ";
                        break;
                    case "14":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+7, 101) ";
                        break;
                    case "15":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-7, 101) ";
                        break;
                    case "16":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+14, 101) ";
                        break;
                    case "17":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-14, 101) ";
                        break;
                    case "18":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.AddMonths(1).Month + "' ";

                        break;
                    case "19":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.AddMonths(-1).Month + "' ";
                        break;
                    case "20":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.Month + "' ";
                        break;
                    case "21":
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate()+1, 103) ";
                        break;
                    default:
                        query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4' AND TOUR_QUOTE_MASTER.CREATE_BY= '" + _userID + "' ";
                        break;
                }
            }

            if (_userID == 5)
            {
               query= "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4'";
                dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            }
            else
            {
                //            String query = "SELECT TOP 5 TOUR_SHORT_NAME,TOUR_QUOTE_MASTER.QUOTE_ID,TOUR_QUOTE_MASTER.TOUR_ID FROM FARE_TOUR_MASTER LEFT OUTER JOIN TOUR_QUOTE_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID WHERE ORDER_STATUS='4'";
                 dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            }

            

            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {

                DataSet ds = objfitquote.fetchallData("FETCH_ALL_BOOKING_TO_BE_RECONFIRMED", usrid);
                Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();
                
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + " " + dr["TOUR_SHORT_NAME"].ToString() + " " + "<a href=\"../../Views/FIT/BookingFit.aspx?TOURID=" + dr["TOUR_ID"].ToString() + "&QUOTEID=" + dr["QUOTE_ID"].ToString() + "" + "\"><br/>View</a>");

               
            }
            _sbi.AppendLine("</ul>");
           // _sbi.AppendLine("<a href=\"../../Views/FIT/AllBookingsToBeReconfirmed.aspx\">View All</a>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}