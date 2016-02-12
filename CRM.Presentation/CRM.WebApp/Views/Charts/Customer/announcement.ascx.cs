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
    public partial class announcement : System.Web.UI.UserControl
    {
        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;
            string query = "SELECT TOP 5 ANNOUNCEMENT_BODY,CONVERT(varchar(10),DATE_CREATED,101) as DATE,suma.USER_NAME FROM COMMON_ANNOUNCEMENT_MASTER cam join dbo.SYS_USER_MASTER suma on suma.USER_ID=cam.CREATED_BY ORDER BY DATE_CREATED";
            DataTable dt = objdeshboardentity.ExecuteQuery(query.ToString().Trim()).Tables[0];
            StringBuilder _sbi = new StringBuilder();
            _sbi.AppendLine("<ul>");
            foreach (DataRow dr in dt.Rows)
            {
                _sbi.AppendLine("<li style=\"list-style-type:square;font-size:13px;padding:5px;\">" + dr["ANNOUNCEMENT_BODY"].ToString() + "</br><div align=\"right\" style=\"font-size:9px;\"><b>Added on : " + dr["DATE"].ToString() + " by : " + dr["USER_NAME"].ToString() + "</b></div></li>");
            }
            _sbi.AppendLine("</ul>");
            divanc.InnerHtml = _sbi.ToString();
        }
    }
}