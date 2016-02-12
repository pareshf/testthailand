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
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.MIS
{
    public partial class ProdcutProfitLoss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
            if (!IsPostBack)
            {
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_PRODUCT");
                binddropdownlist(drp_agent, ds3);
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["Agent_Name"] = drp_agent.Text;
            Session["From_Date"] = txt_fromdate.Text;
            Session["To_date"] = txt_todate.Text;
            Session["Flag"] = "PL";
            
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'AgentReportExcel.aspx', null, 'height=1000,width=1400,status=yes,toolbar=no,menubar=no,location=no' );", true);
            
        }

    }
}