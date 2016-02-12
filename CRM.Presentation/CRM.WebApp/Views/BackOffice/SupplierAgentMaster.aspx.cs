using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using Telerik.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Net;
using System.Net.Mail;
using CRM.WebApp.WebHelper;
using System.IO;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;

namespace CRM.WebApp.Views.Administration
{
    public partial class CustomerMasterNew : System.Web.UI.Page
    {

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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 176);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String  Userid;
            //CompId = Session["CompanyId"].ToString();
            //DeptId = Session["DeptId"].ToString();
            //RoleId = Session["RoleId"].ToString();
            Userid = Session["usersid"].ToString();
            if (Userid == "5")
            {
                radgridcustomermaster.Enabled = true;
            }
            else
            {
                radgridcustomermaster.Enabled = false;
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string Mobile = string.Empty;

            string Msg = txtMessage.Text.Trim();

            int SuccessCnt = 0;
            int FailCnt = 0;

            try
            {
                foreach (GridDataItem dr in radgridcusmsg.Items)
                {
                    TextBox textBox1 = (TextBox)(dr.FindControl("CUST_REL_MOBILE"));
                    Mobile = textBox1.Text;

                    if (WebManager.SendSMS(Mobile, Msg))
                    {
                        SuccessCnt++;
                        Response.Write("<script>window.alert('SMS Send Successfully.')</script>");
                    }
                    else
                    {
                        FailCnt++;
                    }
                }
                Response.Write("<script>window.alert('SMS Send Successfully.')</script>");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR: " + ex.Message, "Flamingo CRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}