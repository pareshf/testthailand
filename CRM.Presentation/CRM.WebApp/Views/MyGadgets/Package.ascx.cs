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
    public partial class TransferPackage : System.Web.UI.UserControl
    {
        private int _userID;
        private int _empID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
            {
                _userID = objAuthorizationBDto.UserProfile.UserId;
                _empID = Convert.ToInt32(Session["empid"]);
            }

            DataSet _Ds = objdeshboardentity.getfilter(_userID);
            DataTable _DT = _Ds.Tables[0];
            DataTable _DT1 = _Ds.Tables[1];

            string query = string.Empty;


            if (_DT.Rows[0]["FROM_DATE"].ToString() != "" && _DT.Rows[0]["TO_DATE"].ToString() != "")
            {
                query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,103)  >= '" + _DT.Rows[0]["FROM_DATE"].ToString() + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,103)  <=  '" + _DT.Rows[0]["TO_DATE"].ToString() + "'";
            }

            else
            {

                switch (_DT.Rows[0]["FILTER_ID"].ToString())
                {
                    case "12":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,103)  = CONVERT(VARCHAR(15),getdate(), 103)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "13":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,103)  = CONVERT(VARCHAR(15),getdate()-1, 103)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "14":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+7, 101)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "15":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-7, 101)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "16":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+14, 101)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "17":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-14, 101)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "18":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND datepart(MONTH, FARE_TOUR_MASTER.CREATED_DATE)  ='" + System.DateTime.Now.AddMonths(1).Month + "'  ORDER BY FIT_PACKAGE_NAME";

                        break;
                    case "19":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND datepart(MONTH, FARE_TOUR_MASTER.CREATED_DATE)  ='" + System.DateTime.Now.AddMonths(-1).Month + "'  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "20":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND datepart(MONTH, FARE_TOUR_MASTER.CREATED_DATE)  ='" + System.DateTime.Now.Month + "'  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    case "21":
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY   = '" + _userID + "' AND CONVERT(VARCHAR(15), FARE_TOUR_MASTER.CREATED_DATE,103)  = CONVERT(VARCHAR(15),getdate()+1, 103)  ORDER BY FIT_PACKAGE_NAME";
                        break;
                    default:
                        query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY =   '" + _userID + "' ORDER BY FIT_PACKAGE_NAME";
                        break;
                }

            }

            //      String query = "SELECT DISTINCT TOP 5 FIT_PACKAGE_NAME FROM FARE_TOUR_MASTER LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID WHERE FIT_PACKAGE_MASTER.FIT_PACKAGE_NAME IS NOT NULL AND FARE_TOUR_MASTER.CREATED_BY =  = '" + _userID + "' ORDER BY FIT_PACKAGE_NAME";
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + dr["FIT_PACKAGE_NAME"].ToString());
            }
            _sbi.AppendLine("</ul>");
            //  _sbi.AppendLine("<a href=\"../../Views/Sales/Tour.aspx\">View All</a>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}