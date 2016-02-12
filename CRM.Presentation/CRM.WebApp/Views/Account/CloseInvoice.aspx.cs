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
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Core.Utility;

namespace CRM.WebApp.Views.Account
{
    public partial class CloseInvoice : System.Web.UI.Page
    {
        CloseInvoiceDal objCloseInvoiceDal = new CloseInvoiceDal();

        #region VARIABLES


        string CompId;
        string DeptId;
        string RoleId;

        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Check Page Authorization

            CompId = Session["CompanyId"].ToString();
            DeptId = Session["DeptId"].ToString();
            RoleId = Session["RoleId"].ToString();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsval = objCloseInvoiceDal.commonSp("FETCH_ALL_INVOICE_NO");
                binddropdownlist(DropDownList2, dsval);
                gridCloseInvoiceBind();
            }
        }

        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnRate = sender as Button;
                int repeaterItemIndex = ((GridViewRow)btnRate.NamingContainer).DataItemIndex;
                foreach (GridViewRow item in gridCloseinvoice.Rows)
                {
                    if (repeaterItemIndex == item.DataItemIndex)
                    {
                        Label lblInvoiceno = (Label)item.FindControl("lblInvoiceno");

                        objCloseInvoiceDal.closeInvoice("SET_INVOICE_STATUS_CLOSE", lblInvoiceno.Text);

                        gridCloseInvoiceBind();
                        upCloseInvoice.Update();
                        Master.DisplayMessage("Invoice Close Successfully.", "successMessage", 3000);

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }


        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCloseinvoice.PageIndex = e.NewPageIndex;


            gridCloseInvoiceBind();
            upCloseInvoice.Update();
        }

        protected void gridCloseInvoiceBind()
        {
            DataSet ds = objCloseInvoiceDal.commonSp("GET_ALL_NOT_CLOSE_INVOICE");
            gridCloseinvoice.DataSource = ds;
            gridCloseinvoice.DataBind();
        }

        protected void search_onclick(object sender, EventArgs e)
        {

            pnlMainHead.Attributes.Add("style", "display");
            Button4.Attributes.Add("style", "display");
            Button3.Attributes.Add("style", "display:none");
            upCloseInvoice.Update();
        }

        protected void searchnow_onclick(object sender, EventArgs e)
        {
          
            
            DataSet ds = objCloseInvoiceDal.fetchallDatasearch("FETCH_DATA_FOR_CLOSE_INVOICE_SEARCH", DropDownList2.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            gridCloseinvoice.DataSource = ds.Tables[0];
            gridCloseinvoice.DataBind();
            upCloseInvoice.Update();

        }

    }
}