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
using CRM.DataAccess.Dashboard;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.DataAccess;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM.WebApp.Views.Charts.Account
{
    public partial class ExpenceComparasion : System.Web.UI.UserControl
    {
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;

            Series series = new Series("Spline");
            series.ChartType = SeriesChartType.Column;
            series.BorderWidth = 3;
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
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(15),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate(), 103) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "13":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(15),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate()-1, 103) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "14":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate()+7, 101) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "15":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate()-7, 101) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "16":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate()+14, 101) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "17":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate()-14, 101) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                case "18":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  datepart(MONTH,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE)= '" + System.DateTime.Now.AddMonths(1).Month + "'  AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) ORDER BY EXPENSE_DATE";

                    break;
                case "19":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  datepart(MONTH,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE)= '" + System.DateTime.Now.AddMonths(-1).Month + "' AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) ORDER BY EXPENSE_DATE";
                    break;
                case "20":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  datepart(MONTH,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE)= '" + System.DateTime.Now.Month + "' AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY datename(month,PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE ) ORDER BY EXPENSE_DATE ";
                    break;
                case "21":
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT,CONVERT(VARCHAR(10),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE  CONVERT(VARCHAR(15),PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate()+1, 103) AND PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT <>0 GROUP BY PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE";
                    break;
                default:
                    query = "SELECT SUM (PURCHASE_PAYMENT_VOUCHER_DETAILS.CR_AMOUNT) AS TOTAL_EXPENCE_AMOUNT , CONVERT (VARCHAR(15), YEAR ( PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE)) AS EXPENSE_DATE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS GROUP BY CONVERT (VARCHAR(15), YEAR ( PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE)) ORDER BY EXPENSE_DATE";
                    break;
            }
            //END QUERY
            //END QUERY
            DataTable _dttemp = new DataTable();
            DataTable dt = new DataTable();
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
                foreach (DataRow dr in _dttemp.Rows)
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
                DataTable dt1 = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
                //SqlConnection con = new SqlConnection(str);
                //con.Open();

                //SqlCommand cmd = new SqlCommand("GEDJET_EXPENCE_COMPANRASION", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataReader rdr = cmd.ExecuteReader();
                //DataTable dt1 = new DataTable();
                //dt.Load(rdr);

                Chart1.Series.Add(series);
                series.XValueMember = "EXPENSE_DATE";
                series.YValueMembers = "TOTAL_EXPENCE_AMOUNT";
                series.IsValueShownAsLabel = true;
                //    Chart1.ChartAreas[0].AxisY.Maximum = int.Parse(dt.Rows[0]["TOTAL_RECEIPT_AMOUNT"].ToString());


                Chart1.DataSource = dt1;
                Chart1.DataBind();
            }
            catch
            {
            }
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