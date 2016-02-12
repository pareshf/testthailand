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

namespace CRM.WebApp.Views.Account
{
    public partial class AgentAllInvoice1 : System.Web.UI.Page
    {
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        CRM.DataAccess.Account.AgentInvoice objAgentInvoice = new DataAccess.Account.AgentInvoice();


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

            //DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 240);

            //if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            //{
            //    Response.Redirect("~/Views/InvalidAccess.aspx");
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objAgentInvoice.FechDataForInvoice("FETCH_AGENT_ALL_MY_INVOICE", Session["usersid"].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    //   Session["editorderstatus"] = ds.Tables[0].Rows[0]["ORDER_STATUS"].ToString();

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    DataSet dsval = objAgentInvoice.fetchallData("FETCH_ALL_INVOICE_NO");
                    binddropdownlist(DropDownList2, dsval);
                }
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", "0"));
            //r.SelectedValue = "0";
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                int newindex = e.NewSelectedIndex;
                string invoice_id = GV_Result.Rows[newindex].Cells[1].Text;
                Session["Invoice_ID"] = invoice_id;
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session["Invoice_ID"].ToString() + "/Invoice.pdf");


                Response.TransmitFile(Server.MapPath("~/Views/FIT/Invoices/" + Session["Invoice_ID"].ToString() + "/Invoice.pdf"));

                Response.End();

            }
            catch
            {

            }
            finally
            {

            }
        }

        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            Updateconfirm.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            string USERID = Session["usersid"].ToString();
            DataSet ds = objAgentInvoice.fetchallInvoice("FETCH_DATA_FOR_AGENT_MY_INVOICE_GRID_SERACH", txtrefrence.Text, DropDownList2.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text, Session["usersid"].ToString());
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = objAgentInvoice.fetchallInvoice("FETCH_DATA_FOR_AGENT_MY_INVOICE_GRID_SERACH", txtrefrence.Text, DropDownList2.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text, Session["usersid"].ToString());

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}