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


namespace CRM.WebApp.Views.AccountMaster
{
    public partial class ChartsOfAccountSearch : System.Web.UI.Page
    {
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        CRM.DataAccess.AccountMaster.ChartofAccountGrid Objchartsofaccount = new CRM.DataAccess.AccountMaster.ChartofAccountGrid();
        CRM.DataAccess.AccountMaster.GroupTypeMasterStoredProcedure objGroupTypeMasterStoredProcedure = new CRM.DataAccess.AccountMaster.GroupTypeMasterStoredProcedure();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 329);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // DataSet ds = Objchartsofaccount.FechDataForGrid("FETCH_ALL_CHARTS_OF_ACCOUNT_DATA");
                DataSet ds = Objchartsofaccount.fetchallInvoice("FETCH_CHARTS_OF_ACCOUNT_GRID_SEARCH", "0", txtglcode.Text, txtgldescription.Text, drpQuestion.Text, txtSidecode.Text);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();
                    //DataSet dsval = objAgentInvoice.fetchallData("FETCH_ALL_INVOICE_NO");
                    //binddropdownlist(DropDownList2, dsval);
                }
                DataSet ds2 = objGroupTypeMasterStoredProcedure.get_account_code("GET_ACCOUNT_TYPE");
                bindComboBoxforquestion(drpQuestion, ds2);
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
            int newindex = e.NewSelectedIndex;
            string ID = GV_Result.Rows[newindex].Cells[1].Text;
            Response.Redirect("~/Views/AccountMaster/ChartofAccount.aspx?CAID=" + ID);
            
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
            if (txtaccountid.Text == "")
            {
                txtaccountid.Text = "0";
            }
            //string USERID = Session["usersid"].ToString();
            DataSet ds = Objchartsofaccount.fetchallInvoice("FETCH_CHARTS_OF_ACCOUNT_GRID_SEARCH", txtaccountid.Text,txtglcode.Text,txtgldescription.Text,drpQuestion.Text,txtSidecode.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (txtaccountid.Text == "")
            {
                txtaccountid.Text = "0";
            }
            
            GV_Result.PageIndex = e.NewPageIndex;
            DataSet ds = Objchartsofaccount.fetchallInvoice("FETCH_CHARTS_OF_ACCOUNT_GRID_SEARCH", txtaccountid.Text,txtglcode.Text,txtgldescription.Text,drpQuestion.Text,txtSidecode.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }

        protected void bindComboBoxforquestion(RadComboBox r, DataSet d)
        {
            r.Items.Clear();
            r.DataTextField = "MAIN_GROUP";
            // r.DataValueField = "TEST_QUESTION_SRNO";
            r.DataSource = d;
            r.DataBind();
            r.Items.Insert(0, new RadComboBoxItem("", ""));
            r.SelectedValue = "";
        }

        protected void RadComboBox1_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //set the Text and Value property of every item
            //here you can set any other properties like Enabled, ToolTip, Visible, etc.
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MAIN_GROUP"].ToString();
            //   e.Item.Value = ((DataRowView)e.Item.DataItem)["TEST_QUESTION_SRNO"].ToString();
        }
    }
}