﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using System.Web.Configuration;

namespace CRM.WebApp.Views.Sales
{
    public partial class PaymentSummary : System.Web.UI.Page
    {
        string booking_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            booking_id = Page.Request.QueryString["key"].ToString();

            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("BOOKING_ID", booking_id);
            rptViewer1.ShowCredentialPrompts = false;
            rptViewer1.ShowParameterPrompts = false;

            rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

            rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
            rptViewer1.ServerReport.ReportPath = "/FlamingoSSRSReports/PaymentSummary";
            rptViewer1.ServerReport.SetParameters(parm);
            rptViewer1.ServerReport.Refresh();
        }
    }
}