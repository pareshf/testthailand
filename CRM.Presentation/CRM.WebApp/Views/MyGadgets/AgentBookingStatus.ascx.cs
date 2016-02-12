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
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace CRM.WebApp.Views.MyGadgets
{
    public partial class AgentBookingStatus : System.Web.UI.UserControl
    {
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        //AuthorizationBDto objAuthorizationBDto;
        //deshboardentity objdeshboardentity = new deshboardentity();
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)

                _userID = objAuthorizationBDto.UserProfile.UserId;
            Series series = new Series("Pie");
            series.ChartType = SeriesChartType.Pie;
            series.BorderWidth = 1;
            series.ShadowOffset = 2;


            DataSet _Ds = objdeshboardentity.getfilter(_userID);
            DataTable _DT = _Ds.Tables[0];
            DataTable _DT1 = _Ds.Tables[1];
            //GET FILTER DETAILS FROM HERE
            string query = string.Empty;
            StringBuilder _employee = new StringBuilder();

            if (_DT.Rows[0]["FROM_DATE"].ToString() != "" && _DT.Rows[0]["TO_DATE"].ToString() != "")
            {
                query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)   >= '" + _DT.Rows[0]["FROM_DATE"].ToString() + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  <=  '" + _DT.Rows[0]["TO_DATE"].ToString() + "' AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17')  GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
            }

            else
            {



                switch (_DT.Rows[0]["FILTER_ID"].ToString())
                {
                    case "12":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate(), 103)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17')  GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "13":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate()-1, 103)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "14":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+7, 101)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "15":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-7, 101)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "16":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate()+14, 101)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "17":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  <= CONVERT(VARCHAR(15),getdate(), 101) AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,101)  >= CONVERT(VARCHAR(15),getdate()-14, 101)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "18":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.AddMonths(1).Month + "'  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";

                        break;
                    case "19":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.AddMonths(-1).Month + "'  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "20":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND datepart(MONTH, TOUR_QUOTE_MASTER.CREATE_DATE)  ='" + System.DateTime.Now.Month + "'  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    case "21":
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND CONVERT(VARCHAR(15), TOUR_QUOTE_MASTER.CREATE_DATE,103)  = CONVERT(VARCHAR(15),getdate()+1, 103)  AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                    default:
                        query = "SELECT  ORDER_STATUS_MASTER.ORDER_STATUS_NAME, COUNT(TOUR_QUOTE_MASTER.ORDER_STATUS) AS STATUS  FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID=TOUR_QUOTE_MASTER.ORDER_STATUS  WHERE TOUR_QUOTE_MASTER.CREATE_BY =  '" + _userID + "' AND TOUR_QUOTE_MASTER.ORDER_STATUS IS NOT NULL AND (TOUR_QUOTE_MASTER.ORDER_STATUS = '2' OR TOUR_QUOTE_MASTER.ORDER_STATUS='3' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '4' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '5' OR TOUR_QUOTE_MASTER.ORDER_STATUS= '7' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '16' OR TOUR_QUOTE_MASTER.ORDER_STATUS = '17') GROUP BY ORDER_STATUS_MASTER.ORDER_STATUS_NAME";
                        break;
                }
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
                DataTable dt1 = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
                //SqlConnection con = new SqlConnection(str);
                //con.Open();

                //SqlCommand cmd = new SqlCommand("GEDJET_EXPENCE_COMPANRASION", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataReader rdr = cmd.ExecuteReader();
                //DataTable dt1 = new DataTable();
                //dt.Load(rdr);

                Chart1.Series.Add(series);
                series.XValueMember = "ORDER_STATUS_NAME";
                series.YValueMembers = "STATUS";
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
        
    
