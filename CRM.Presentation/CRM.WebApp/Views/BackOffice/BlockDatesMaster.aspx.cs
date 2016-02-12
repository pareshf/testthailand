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
using CRM.DataAccess;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.BackOffice
{
    public partial class BlockDatesMaster : System.Web.UI.Page
    {
        AgentBookingClosed objAgentBookingClosed = new AgentBookingClosed();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();

        bool flag = true;
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

            DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 241);

            if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            {
                Response.Redirect("~/Views/InvalidAccess.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objAgentBookingClosed.fetchComboData("FETCH_BLOCK_DATES");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    GV_Result.DataSource = ds;
                    GV_Result.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            date_Validation();
            if (flag == false)
            {
                Master.DisplayMessage(ViewState["error"].ToString(), "successMessage", 5000);
                
            }
            else
            {
                if (ViewState["ID"] != null)
                {
                    objAgentBookingClosed.insert_block_dates(int.Parse(ViewState["ID"].ToString()), txtFromdate.Text, txtTodate.Text);
                    DataSet ds = objAgentBookingClosed.fetchComboData("FETCH_BLOCK_DATES");
                    txtFromdate.Text = "";
                    txtTodate.Text = "";
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        GV_Result.DataSource = ds;
                        GV_Result.DataBind();
                    }
                    upHotelPty.Update();
                    UpdatePanel1.Update();
                    ViewState["ID"] = null;
                    Master.DisplayMessage("Record Updated Successfully.", "successMessage", 5000);

                }
                else
                {
                    objAgentBookingClosed.insert_block_dates(0, txtFromdate.Text, txtTodate.Text);
                    DataSet ds = objAgentBookingClosed.fetchComboData("FETCH_BLOCK_DATES");
                    txtFromdate.Text = "";
                    txtTodate.Text = "";
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        GV_Result.DataSource = ds;
                        GV_Result.DataBind();
                    }
                    upHotelPty.Update();
                    UpdatePanel1.Update();
                    Master.DisplayMessage("Record Save Successfully.", "successMessage", 5000);
                }

            }
        }

        protected void date_Validation()
        {
            if ((DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", null) >= (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null))))
            {
                flag = false;
                ViewState["error"] = "From date must be greater then To date.";
            }
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
           
          
              DataSet ds = objAgentBookingClosed.fetchComboData("FETCH_BLOCK_DATES");
              for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
              {
                  if (i == newindex)
                  {
                      ViewState["ID"] = ds.Tables[0].Rows[i]["ADMIN_BLOCK_DATE_ID"].ToString();
                      txtFromdate.Text  = ds.Tables[0].Rows[i]["FROM_DATE"].ToString();
                      txtTodate.Text  = ds.Tables[0].Rows[i]["TO_DATE"].ToString();

                  }
              }
              UpdatePanel1.Update();
              upHotelPty.Update();
           
        }
    }
}