using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using System.Data;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.MIS
{
    public partial class AgentCreditLimitLedgerReport : System.Web.UI.Page
    {
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization

            //String CompId, DeptId, RoleId;
            //CompId = Session["CompanyId"].ToString();
            //DeptId = Session["DeptId"].ToString();
            //RoleId = Session["RoleId"].ToString();

            //DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 307);

            //if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            //{
            //    Response.Redirect("~/Views/InvalidAccess.aspx");
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {
            string fromdate;
            string todate;
            string supliercompany;
            fromdate = DropDownList1.Text;
            todate = TextBox1.Text;
            DataSet ds3 = objBookingFitStoreProcedure.GET_GL_DESCRIPTION(int.Parse(Session["cust_id"].ToString()), Session["FLAG"].ToString());
            supliercompany = ds3.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString();
            Response.Redirect("~/Views/MIS/AgentCreditLimitReport.aspx?key=" + fromdate + "&key1=" + todate + "&key2=" + supliercompany);
        }
    }
}