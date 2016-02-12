﻿using System;
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
    public partial class AgentBySales : System.Web.UI.UserControl
    {
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;
            Series series = new Series("Spline");
            series.ChartType = SeriesChartType.Bar;
            series.BorderWidth = 1;
            series.ShadowOffset = 1;

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
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18' LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate(), 103) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "13":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate()-1, 103) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "14":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate()+7, 101) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "15":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate()-7, 101) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "16":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate()+14, 101) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "17":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) <= CONVERT(VARCHAR(5),getdate(), 101) AND CONVERT(VARCHAR(5),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,101) >= CONVERT(VARCHAR(5),getdate()-14, 101) GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                case "18":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE ) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE datepart(MONTH,ACCOUNT_VOUCHER_HEADER.POSTED_DATE)= '" + System.DateTime.Now.AddMonths(1).Month + "' GROUP BY CHAIN_NAME , datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE )";

                    break;
                case "19":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE ) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE datepart(MONTH,ACCOUNT_VOUCHER_HEADER.POSTED_DATE)= '" + System.DateTime.Now.AddMonths(-1).Month + "' GROUP BY CHAIN_NAME , datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE )";
                    break;
                case "20":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME ,datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE ) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE datepart(MONTH,ACCOUNT_VOUCHER_HEADER.POSTED_DATE)= '" + System.DateTime.Now.Month + "' GROUP BY CHAIN_NAME , datename(month,ACCOUNT_VOUCHER_HEADER.POSTED_DATE )";
                    break;
                case "21":
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103) = CONVERT(VARCHAR(15),getdate()+1, 103)  GROUP BY CHAIN_NAME , CONVERT(VARCHAR(15),ACCOUNT_VOUCHER_HEADER.POSTED_DATE,103)";
                    break;
                default:
                    query = "SELECT SUM(ACCOUNT_VOUCHER_HEADER.VOUCHER_AMOUNT ) AS AGENT_AMOUNT, CUST_CUSTOMER_CONTACT_DETAILS.CHAIN_NAME , CONVERT (VARCHAR(15), YEAR ( ACCOUNT_VOUCHER_HEADER.POSTED_DATE)) AS YEAR FROM TOUR_QUOTE_MASTER LEFT OUTER JOIN FARE_TOUR_MASTER ON FARE_TOUR_MASTER.TOUR_ID=TOUR_QUOTE_MASTER.TOUR_ID LEFT OUTER JOIN ORDER_STATUS_MASTER ON ORDER_STATUS_MASTER.ORDER_STATUS_ID = TOUR_QUOTE_MASTER.ORDER_STATUS LEFT OUTER JOIN FIT_PACKAGE_MASTER ON FIT_PACKAGE_MASTER.FIT_PACKAGE_ID=FARE_TOUR_MASTER.FIT_PACKAGE_ID LEFT OUTER JOIN SYS_USER_MASTER ON SYS_USER_MASTER.USER_ID=TOUR_QUOTE_MASTER.CREATE_BY LEFT OUTER JOIN CUST_CUSTOMER_RELATION_DETAILS ON CUST_CUSTOMER_RELATION_DETAILS.CUST_REL_SRNO=SYS_USER_MASTER.EMP_ID LEFT OUTER JOIN CUST_CUSTOMER_CONTACT_DETAILS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CUST_CUSTOMER_RELATION_DETAILS.CUST_ID LEFT OUTER JOIN SALES_INVOICE_HEADER ON SALES_INVOICE_HEADER.QUOTE_ID=TOUR_QUOTE_MASTER.QUOTE_ID  LEFT OUTER JOIN ACCOUNT_VOUCHER_HEADER ON SALES_INVOICE_HEADER.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO LEFT OUTER JOIN ACCOUNTS_VOUCHER_STATUS_MASTER ON ACCOUNTS_VOUCHER_STATUS_MASTER.VOUCHER_STATUS_ID=ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID LEFT OUTER JOIN SALES_RECEIPT_VOUCHER_DETAILS ON SALES_RECEIPT_VOUCHER_DETAILS.INVOICE_NO=ACCOUNT_VOUCHER_HEADER.INVOICE_NO AND ACCOUNT_VOUCHER_HEADER.VOUCHER_STATUS_ID != '18'  LEFT OUTER JOIN BOOKING_PAYMENT_MODE_MASTER ON BOOKING_PAYMENT_MODE_MASTER.PAYMENT_MODE_ID=SALES_INVOICE_HEADER.PAYMENT_MODE LEFT OUTER JOIN CHART_OF_ACCOUNTS ON CUST_CUSTOMER_CONTACT_DETAILS.CUST_ID=CHART_OF_ACCOUNTS.ACCOUNT_ID AND CHART_OF_ACCOUNTS.ACCOUNT_FLAG='A' WHERE CONVERT (VARCHAR(15), YEAR ( ACCOUNT_VOUCHER_HEADER.POSTED_DATE)) = CONVERT (VARCHAR(15), YEAR (GETDATE())) GROUP BY CHAIN_NAME , CONVERT (VARCHAR(15), YEAR ( ACCOUNT_VOUCHER_HEADER.POSTED_DATE))";
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
                        throw ex;
                    }
                }
            }
            catch (Exception ex) { throw ex; }

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
                series.XValueMember = "CHAIN_NAME";
                series.YValueMembers = "AGENT_AMOUNT";
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