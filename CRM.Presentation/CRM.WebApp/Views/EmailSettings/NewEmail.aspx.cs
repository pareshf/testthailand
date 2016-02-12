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
    public partial class NewEmail : System.Web.UI.Page
    {
        CRM.DataAccess.EmailSettings.EmailMaster objemailmaster = new CRM.DataAccess.EmailSettings.EmailMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request["EMAIL_TRAIL_MASTER_ID"] != null && !string.IsNullOrEmpty(Request["EMAIL_TRAIL_MASTER_ID"].ToString()))
                //{
                int a = int.Parse(Session["Email_master_id"].ToString());
                    //int a = int.Parse(Request["EMAIL_TRAIL_MASTER_ID"].ToString());
                    DataSet ds = objemailmaster.GetEmailData("FETCH_EMAIL_DETAILS", a);
                    if (ds.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        txtFrom.Text = ds.Tables[0].Rows[0]["FROM_ID"].ToString();
                        txtTo.Text = ds.Tables[0].Rows[0]["TO_ID"].ToString();
                        txtcc.Text = ds.Tables[0].Rows[0]["CC_ID"].ToString();
                        txtBcc.Text = ds.Tables[0].Rows[0]["BCC_ID"].ToString();
                        txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                        ltrContent.Text = ds.Tables[0].Rows[0]["EMAIL_CONTENT"].ToString();
                    }
                //}
            }
        }
    }
}