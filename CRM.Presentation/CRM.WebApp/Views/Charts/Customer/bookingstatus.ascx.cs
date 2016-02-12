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
    public partial class bookingstatus : System.Web.UI.UserControl
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
            Series series = new Series();
            series.ChartType = SeriesChartType.Area;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;

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
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where   CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101)=CONVERT(VARCHAR(10),GETDATE(), 101) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "13":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101)=CONVERT(VARCHAR(10),GETDATE()-1, 101) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) ORDER BY TOTAL DESC";
                    break;
                case "14":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) >= CONVERT(VARCHAR(10),GETDATE(), 101) AND CONVERT(VARCHAR(10),'imft.INQUIRY_DATE', 101) <= CONVERT(VARCHAR(10),GETDATE()+7, 101)) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "15":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),'imft.INQUIRY_DATE', 101) <=CONVERT(VARCHAR(10),GETDATE()-7, 101)) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "16":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),'imft.INQUIRY_DATE', 101) <= CONVERT(VARCHAR(10),GETDATE()+14, 101)) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "17":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),'imft.INQUIRY_DATE', 101) <= CONVERT(VARCHAR(10),GETDATE()-14, 101)) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "18":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where datepart(MONTH,imft.INQUIRY_DATE)= '" + System.DateTime.Now.AddMonths(1).Month + "'  AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by  group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "19":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where datepart(MONTH,imft.INQUIRY_DATE)= '" + System.DateTime.Now.AddMonths(-1).Month + "' AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by  group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "20":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where datepart(MONTH,imft.INQUIRY_DATE)= '" + System.DateTime.Now.Month + "' AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                case "21":
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101)=CONVERT(DATE,GETDATE()+1) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
                default:
                    query = "select COUNT(*) AS TOTAL, INQUIRY_STATUS_NAME FROM INQ_MASTER_FOR_TOURS imft join dbo.COMMON_INQUIRY_STATUS_MASTER cism on imft.CURRENT_STATUS_ID=cism.INQUIRY_STATUS_ID where CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) >=CONVERT(VARCHAR(10),'" + _DT.Rows[0]["FROM_DATE"].ToString() + "', 101) and CONVERT(VARCHAR(10),imft.INQUIRY_DATE, 101) <=CONVERT(VARCHAR(10),'" + _DT.Rows[0]["TO_DATE"].ToString() + "', 101) AND imft.CREATED_BY IN (" + _employee.ToString() + ") group by cism.INQUIRY_STATUS_NAME ORDER BY TOTAL DESC";
                    break;
            }
            //END QUERY
           // Add series into the chart's series collection
            try
            {
                DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
           
                Chart1.Series.Add(series);
                series.XValueMember = "INQUIRY_STATUS_NAME";
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