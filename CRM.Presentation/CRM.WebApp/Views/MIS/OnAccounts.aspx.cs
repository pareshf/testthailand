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
using Telerik.Web.UI;


namespace CRM.WebApp.Views.MIS
{
    public partial class OnAccounts : System.Web.UI.Page
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
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 314);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds3 = objBookingFitStoreProcedure.fetchComboData("FETCH_ALL_GL_CODE_FOR_LEDGER_REPORT_ON_ACCOUNT");
                DropDownList1.Items.Clear();
                DropDownList1.DataTextField = "AutoSearchResult";
                DropDownList1.DataValueField = "AutoSearchResult";
                DropDownList1.DataSource = ds3;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new RadComboBoxItem("", ""));
               // binddropdownlist(DropDownList1 , ds3);
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //r.SelectedValue = "1";
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            string fromdate;
            string todate;
            string supliercompany;
            fromdate = txt_fromdate.Text;
            todate = txt_todate.Text;
            supliercompany = DropDownList1.Text;
            Response.Redirect("~/Views/MIS/OnAccountsReport.aspx?key=" + supliercompany + "&key1=" + fromdate + "&key2=" + todate);
        }
    }
}