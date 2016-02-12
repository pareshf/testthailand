using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.Account;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.AccountMaster;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.AccountMaster
{
    public partial class GroupTypeMaster : System.Web.UI.Page
    {
        GroupTypeMasterStoredProcedure objGroupTypeMasterStoredProcedure = new GroupTypeMasterStoredProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Check Page Authorization
            String CompId, DeptId, RoleId;
            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 204);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                get_value();
                fill_grid();
              
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_ACCOUNT_CODE_DIGIT");

            DataSet ds_GTM = objGroupTypeMasterStoredProcedure.get_account_code("GET_GROUP_TYPE_MASTER");

            if (ViewState["GTMID"] == null)
            {

                if (ds_GTM.Tables[0].Rows.Count == 0)
                {

                    objGroupTypeMasterStoredProcedure.insert_accounts_entry(txtGroupName.Text, int.Parse(txtFromDigit.Text), int.Parse(txtInterval.Text), int.Parse(ds.Tables[0].Rows[0]["STARTING_DIGIT"].ToString()));
                }

                else
                {
                    objGroupTypeMasterStoredProcedure.insert_accounts_entry(txtGroupName.Text, int.Parse(txtFromDigit.Text), int.Parse(txtInterval.Text), int.Parse(ds_GTM.Tables[0].Rows[0]["STARTING_DIGIT"].ToString()) + 1);
                }
                Master.DisplayMessage("Record Save SuccesFully.", "successMessage", 5000);
            }
            else
            {
                objGroupTypeMasterStoredProcedure.update_accounts_entry(txtGroupName.Text, int.Parse(txtInterval.Text), int.Parse(ViewState["GTMID"].ToString()));
                Master.DisplayMessage("Record Updated SuccesFully.", "successMessage", 5000);
                ViewState["GTMID"] = null;
            }
            hide_clear();
            fill_grid();
           
        }

        protected void hide_clear()
        {
            txtFromDigit.Text = "";
            txtGroupName.Text = "";
            txtInterval.Text = "";
            get_value();
            Updateconfirm.Update();

        }

        protected void get_value()
        {
            DataSet ds_GTM = objGroupTypeMasterStoredProcedure.get_account_code("GET_GROUP_TYPE_MASTER");

            if (ds_GTM.Tables[0].Rows.Count == 0)
            {
                DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_ACCOUNT_CODE_DIGIT");

                for (int i = 0; i < int.Parse(ds.Tables[0].Rows[0]["NO_OF_DIGITS"].ToString()); i++)
                {
                    if (i == 0)
                    {
                        txtFromDigit.Text = ds.Tables[0].Rows[0]["STARTING_DIGIT"].ToString();
                    }
                    else
                    {
                        txtFromDigit.Text = txtFromDigit.Text + "0";
                    }
                }
                // txtFromDigit.Text = 
            }
            else
            {
                DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_ACCOUNT_CODE_DIGIT");

                for (int i = 0; i < int.Parse(ds.Tables[0].Rows[0]["NO_OF_DIGITS"].ToString()); i++)
                {
                    if (i == 0)
                    {
                        txtFromDigit.Text = (int.Parse(ds_GTM.Tables[0].Rows[0]["STARTING_DIGIT"].ToString()) + 1).ToString();
                    }
                    else
                    {
                        txtFromDigit.Text = txtFromDigit.Text + "0";
                    }
                }
            }
        }

        protected void fill_grid()
        {
            DataSet ds = objGroupTypeMasterStoredProcedure.get_account_code("GET_GROUP_TYPE_MASTER");

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            UpdatePanel1.Update();
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string seqno = GV_Result.Rows[newindex].Cells[1].Text;
           // string vouchertype = GV_Result.Rows[newindex].Cells[5].Text;

            DataSet ds = objGroupTypeMasterStoredProcedure.get_record_edit(int.Parse(seqno));

            txtFromDigit.Text = ds.Tables[0].Rows[0]["FROM_DIGIT"].ToString();
            txtGroupName.Text = ds.Tables[0].Rows[0]["GROUP_TYPE_NAME"].ToString();
            txtInterval.Text = ds.Tables[0].Rows[0]["INTERVAL"].ToString();
            ViewState["GTMID"] = seqno;
            UpdatePanel1.Update();
            Updateconfirm.Update();
            

        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds2 = objGroupTypeMasterStoredProcedure.get_account_code("GET_GROUP_TYPE_MASTER");
            GV_Result.DataSource = ds2;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}