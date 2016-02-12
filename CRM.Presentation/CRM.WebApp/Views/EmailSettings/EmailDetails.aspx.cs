using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.EmailSettings
{
    public partial class EmailDetails : System.Web.UI.Page
    {
        CRM.DataAccess.EmailSettings.EmailMaster objemailmaster = new CRM.DataAccess.EmailSettings.EmailMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            int a = int.Parse(Session["Email_master_id"].ToString());
            //int a = int.Parse(Request["EMAIL_TRAIL_MASTER_ID"].ToString());
            DataSet ds = objemailmaster.GetEmailData("FETCH_EMAIL_DETAILS", a);
            if (ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                lblfrom.Text = ds.Tables[0].Rows[0]["FROM_ID"].ToString();
                lblto.Text = ds.Tables[0].Rows[0]["TO_ID"].ToString();
                lbldate.Text = ds.Tables[0].Rows[0]["CREATED_DATE"].ToString();
                lbltime.Text = ds.Tables[0].Rows[0]["TIME1"].ToString();
                lblcc.Text = ds.Tables[0].Rows[0]["CC_ID"].ToString();
                lblbcc.Text = ds.Tables[0].Rows[0]["BCC_ID"].ToString();
                lblSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                ltrContent.Text = ds.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
            }
        }
    }
}