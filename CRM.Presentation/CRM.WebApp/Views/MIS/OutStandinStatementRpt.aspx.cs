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

using CRM.DataAccess.AdministratorEntity;
using System.Configuration;



namespace CRM.WebApp.Views.MIS
{
    public partial class OutStandinStatementRpt : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        CRM.DataAccess.Account.GenerateInvoiceSp objgenerateInvoiceStoredProcedure = new CRM.DataAccess.Account.GenerateInvoiceSp();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //  Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 315);

            //if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            //{
            //    Response.Redirect("~/Views/InvalidAccess.aspx");
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["DeptId"].ToString() != "30")
                {
                    agentdrp.Attributes.Add("style", "display");
                }
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_AGENT_COMPANY_NAME");
                binddropdownlist(drpAgentType, ds3);
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

        protected void btnshow_Click(object sender, EventArgs e)
        {
            string fromdate;
            string todate;
            fromdate = DropDownList1.Text;
            todate = TextBox1.Text;
            if (Session["DeptId"].ToString() != "30")
            {
                string name = drpAgentType.Text;
                DataSet DS = objgenerateInvoiceStoredProcedure.GetEmpid("FETCH_EMPLOYEE_ID_FOR_PAYMENT", name);
                Session["rel_sr_no"] = DS.Tables[0].Rows[0]["CUST_REL_SRNO"].ToString();
                Session["uid"] = DS.Tables[0].Rows[0]["USER_ID"].ToString();
            }
            else
            {

                Session["uid"] = Session["usersid"].ToString();
            }
            Response.Redirect("~/Views/MIS/OutstandingReport.aspx?key=" + fromdate + "&key1=" + todate + "&key2=" + Session["uid"].ToString());
        }

    }
}