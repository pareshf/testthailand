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
    public partial class nexttravelplan : System.Web.UI.UserControl
    {
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;
            // Create new data series and set it's visual attributes
            Series series = new Series("FastLine");
            series.ChartType = SeriesChartType.Line;
            //general setting
            // set query according to from and to date
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
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH=CONVERT(varchar(6),CAST(GETDATE() AS DATETIME),112) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "13":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH=CONVERT(varchar(6),CAST(GETDATE()-1 AS DATETIME),112) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "14":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH >= CONVERT(varchar(6),CAST(GETDATE() AS DATETIME),112) AND PLAN_YEAR_MONTH <= CONVERT(varchar(6),GETDATE()+7 AS DATETIME),112)) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "15":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH >= CONVERT(varchar(6),CAST(GETDATE() AS DATETIME),112) AND PLAN_YEAR_MONTH <=CONVERT(varchar(6),GETDATE()-7 AS DATETIME),112)) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "16":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH >= CONVERT(varchar(6),CAST(GETDATE() AS DATETIME),112) AND PLAN_YEAR_MONTH <= CONVERT(varchar(6),GETDATE()+14 AS DATETIME),112)) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "17":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH >= CONVERT(varchar(6),CAST(GETDATE() AS DATETIME),112) AND PLAN_YEAR_MONTH <= CONVERT(varchar(6),GETDATE()+14 AS DATETIME),112)) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "18":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH= '" + System.DateTime.Now.Year + System.DateTime.Now.AddMonths(1).Month + "'  AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "19":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH= '" + System.DateTime.Now.Year + System.DateTime.Now.AddMonths(-1).Month + "' AND CREATED_BY IN (" + _employee.ToString() + ") group by  PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "20":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH= '" + System.DateTime.Now.Year + System.DateTime.Now.Month + "' AND CREATED_BY IN (" + _employee.ToString() + ") group by  PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                case "21":
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH=CONVERT(varchar(6),CAST(GETDATE()+1 AS DATETIME),112) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
                default:
                    query = "select COUNT(*) TOTAL,PLAN_YEAR_MONTH as DATE from dbo.CUST_NEXT_TRAVEL_PLAN where PLAN_YEAR_MONTH >=CONVERT(varchar(6),CAST('" + _DT.Rows[0]["FROM_DATE"].ToString() + "' AS DATETIME),112) and PLAN_YEAR_MONTH <=CONVERT(varchar(6),CAST('" + _DT.Rows[0]["TO_DATE"].ToString() + "' AS DATETIME),112) AND CREATED_BY IN (" + _employee.ToString() + ") group by PLAN_YEAR_MONTH ORDER BY TOTAL DESC";
                    break;
            }
            //END QUERY
            // Add series into the chart's series collection
            Chart1.Series.Add(series);
            try
            {
                DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
           
                series.XValueMember = "DATE";
                series.YValueMembers = "TOTAL";
                series.IsValueShownAsLabel = true;
                Chart1.ChartAreas[0].AxisY.Maximum = int.Parse(dt.Rows[0]["TOTAL"].ToString());
                Chart1.DataSource = dt;
                Chart1.DataBind();
            }
            catch (Exception ex) { }
            //general setting over
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}