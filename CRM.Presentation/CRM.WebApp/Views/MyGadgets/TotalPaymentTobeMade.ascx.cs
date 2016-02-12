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
    public partial class TotalPaymentTobeMade : System.Web.UI.UserControl
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
            //String query = "SELECT PURCHASE_PAYMENT_VOUCHER_DETAILS.GL_CODE,SUM(CR_AMOUNT) AS TOTAL_PAYMENT_MADE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE CONVERT (VARCHAR(10), PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE,103)=CONVERT(VARCHAR(10),GETDATE(),103) GROUP BY GL_CODE";
            String query = "SELECT PURCHASE_PAYMENT_VOUCHER_DETAILS.GL_CODE,CR_AMOUNT AS TOTAL_PAYMENT_MADE FROM PURCHASE_PAYMENT_VOUCHER_DETAILS WHERE PURCHASE_PAYMENT_VOUCHER_DETAILS.POSTED_DATE BETWEEN GETDATE() AND GETDATE()+7 ";
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:12px;padding:3px;\">" + dr["GL_CODE"].ToString()+" " + dr["TOTAL_PAYMENT_MADE"].ToString() + " " + "THB");
            }
            _sbi.AppendLine("</ul>");
            _sbi.AppendLine("<a href=\"../../Views/Account/Payment.aspx\">View All</a>");
            divanc.InnerHtml = _sbi.ToString();
            
        }
    }
}