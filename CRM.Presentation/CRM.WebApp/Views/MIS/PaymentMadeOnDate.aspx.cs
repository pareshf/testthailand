﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.MIS
{
    public partial class PaymentMadeOnDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            Session["accountpaymentdate"] = txtDate.Text;
            Response.Redirect("~/Views/MIS/PaymentsMadeOnDateRpt.aspx");
        }
    }
}