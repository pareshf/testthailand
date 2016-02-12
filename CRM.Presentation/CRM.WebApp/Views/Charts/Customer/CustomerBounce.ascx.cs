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
    public partial class CustomerBounce : System.Web.UI.UserControl
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
            Series series = new Series("Pie");
            series.ChartType = SeriesChartType.Pie;
            series.BorderWidth = 1;
            series.ShadowOffset = 2;

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
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101)=CONVERT(VARCHAR(10),GETDATE(), 101) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "13":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101)=CONVERT(VARCHAR(10),GETDATE()-1, 101) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "14":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101) >= CONVERT(VARCHAR(10),GETDATE(), 101) AND CONVERT(VARCHAR(10),INQUIRY_DATE, 101) <= CONVERT(VARCHAR(10),GETDATE()+7, 101)) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "15":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),INQUIRY_DATE, 101) <=CONVERT(VARCHAR(10),GETDATE()-7, 101)) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "16":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),INQUIRY_DATE, 101) <= CONVERT(VARCHAR(10),GETDATE()+14, 101)) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "17":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101) >= CONVERT(DATE,GETDATE()) AND CONVERT(VARCHAR(10),INQUIRY_DATE, 101) <= CONVERT(VARCHAR(10),GETDATE()-14, 101)) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                case "18":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where datepart(MONTH,INQUIRY_DATE)= '" + System.DateTime.Now.AddMonths(1).Month + "'  AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL";
                    break;
                case "19":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where datepart(MONTH,INQUIRY_DATE)= '" + System.DateTime.Now.AddMonths(-1).Month + "' AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL";
                    break;
                case "20":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where datepart(MONTH,INQUIRY_DATE)= '" + System.DateTime.Now.Month + "' AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL";
                    break;
                case "21":
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101)=CONVERT(DATE,GETDATE()+1) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
                default:
                    query = "select COUNT(*) TOTAL, CUSTOMER_ID from dbo.INQ_INQUIRY_MAIN_HEAD where CONVERT(VARCHAR(10),INQUIRY_DATE, 101) >=CONVERT(VARCHAR(10),'" + _DT.Rows[0]["FROM_DATE"].ToString() + "', 101) and CONVERT(VARCHAR(10),INQUIRY_DATE, 101) <=CONVERT(VARCHAR(10),'" + _DT.Rows[0]["TO_DATE"].ToString() + "', 101) AND SALES_PERSON_ID IN (" + _employee.ToString() + ") group by CUSTOMER_ID ORDER BY TOTAL DESC";
                    break;
            }
            //END QUERY
            //END QUERY
            DataTable _dttemp = new DataTable();
             DataTable dt=new DataTable();
            try
            {
                dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
               
                _dttemp.Columns.Add("CUSTOMER_TYPE", typeof(string));
                _dttemp.Columns.Add("TOTAL", typeof(int));
                _dttemp.Rows.Add(_dttemp.NewRow());
                _dttemp.Rows.Add(_dttemp.NewRow());
                _dttemp.Rows[0]["TOTAL"] = 0;
                _dttemp.Rows[1]["TOTAL"] = 0;
                // Add series into the chart's series collection
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (dr["TOTAL"].ToString() == "1")
                        {
                            _dttemp.Rows[0]["CUSTOMER_TYPE"] = "New";
                            _dttemp.Rows[0]["TOTAL"] = int.Parse(_dttemp.Rows[0]["TOTAL"].ToString()) + int.Parse(dr["TOTAL"].ToString());
                        }
                        else
                        {
                            _dttemp.Rows[1]["CUSTOMER_TYPE"] = "Old";
                            _dttemp.Rows[1]["TOTAL"] = int.Parse(_dttemp.Rows[0]["TOTAL"].ToString()) + int.Parse(dr["TOTAL"].ToString());

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex) { }
            try
            {
                Chart1.Series.Add(series);
                series.XValueMember = "CUSTOMER_TYPE";
                series.YValueMembers = "TOTAL";
                series.IsValueShownAsLabel = true;
                Chart1.ChartAreas[0].AxisY.Maximum = int.Parse(_dttemp.Rows[0]["TOTAL"].ToString());
                Chart1.DataSource = _dttemp;
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