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
    public partial class MyTask : System.Web.UI.UserControl
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
            String query = "SELECT TITLE,EMP_NAME,CONVERT(varchar(10),START_DATE,101) AS DATE FROM MYTASK_MASTER,EMP_EMPLOYEE_MASTER WHERE MYTASK_MASTER.ASSIGN_BY = EMP_EMPLOYEE_MASTER.EMP_ID AND MYTASK_MASTER.STATUS_ID != 1 AND DATEDIFF(dd,GETDATE(),MYTASK_MASTER.START_DATE) <= 15 AND MYTASK_MASTER.ASSIGN_TO = " + _empID;
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:15px;padding:3px;\">" + dr["TITLE"].ToString() + "</br><div align=\"right\" style=\"font-size:11px;color:red\"><b>Started on : " + dr["DATE"].ToString() + " | By : " + dr["EMP_NAME"].ToString() + "</b></div></li>");
            }
            _sbi.AppendLine("</ul>");
            _sbi.AppendLine("<a href=\"../../Views/Sales/Tour.aspx\">View All</a>");  
            divanc.InnerHtml = _sbi.ToString();

        }
    }
}