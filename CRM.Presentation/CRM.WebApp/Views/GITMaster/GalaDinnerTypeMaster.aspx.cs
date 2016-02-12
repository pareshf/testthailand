using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.GIT;
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
using CRM.DataAccess.AdministrationDAL;


namespace CRM.WebApp.Views.GITMaster
{
    public partial class GalaDinnerTypeMaster : System.Web.UI.Page
    {
        GalaDinnerTypeMasterDal objGalaDinnerType = new GalaDinnerTypeMasterDal();

        #region Variables
        DataSet ds = null;
        string GalaDinnerType_id = "0";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string GalaDinnerType = txtGalaDinnerType.Text;
            string userid = Session["usersid"].ToString();

            if (ViewState["l"] == null)
            {
                DataSet dtforvalidation = objGalaDinnerType.FetchCountForValidation(GalaDinnerType);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                // Insert The New Record
                objGalaDinnerType.insertGalaDinnerTypeMaster(GalaDinnerType_id, GalaDinnerType, userid);
                txtGalaDinnerType.Text = "";
                bindGrid();
                Master.DisplayMessage("GalaDinner Type Save Succesfully.", "successMessage", 5000);
                }
                else
                {
                    Master.DisplayMessage("Record already exists.", "successMessage", 5000);
                }

            }
            else
            {
                //Update The Existing Record
                objGalaDinnerType.insertGalaDinnerTypeMaster(ViewState["l"].ToString(), GalaDinnerType, userid);
                txtGalaDinnerType.Text = "";
                ViewState["l"] = null;
                bindGrid();
                Master.DisplayMessage("GalaDinner Type Updated Succesfully.", "successMessage", 5000);

            }
            upGalaDinnerType.Update();
            upGalaDinnerTypegrid.Update();
        }

        protected void bindGrid()
        {
            //Bind the grid.
            ds = objGalaDinnerType.bindGalaDinnerTypeGrid();
            gridGalaDinnerType.DataSource = ds;
            gridGalaDinnerType.DataBind();
        }

        protected void GV_time_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridGalaDinnerType.PageIndex = e.NewPageIndex;
            bindGrid();
            upGalaDinnerTypegrid.Update();

        }

        #region Check Chage Event Of Grid
        public void CheckChanged(object sender, EventArgs e)
        {
            ViewState["l"] = null;
            //on each item checked, remove any other items checked
            Label l = new Label();
            foreach (GridViewRow item in gridGalaDinnerType.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rb1");
                if (rb != sender)
                {
                    rb.Checked = false;
                }

                if (rb.Checked == true)
                {
                    l = (Label)item.FindControl("lblGalaDinnerTypeID");
                    ViewState["l"] = l.Text;
                }
            }
            upGalaDinnerTypegrid.Update();
            upGalaDinnerType.Update();
        }
        #endregion

        #region Edit Button Click
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] == null)
            {
                //Message will display if no record is selected from grid.
                Master.DisplayMessage("Select Record For Update.", "successMessage", 5000);
            }
            else
            {
                //Fill data in controls of selected record.
                ds = objGalaDinnerType.editGalaDinnerType(ViewState["l"].ToString());
                GalaDinnerType_id = ds.Tables[0].Rows[0][0].ToString();
                txtGalaDinnerType.Text = ds.Tables[0].Rows[0]["GALA_DINNER_TYPE"].ToString();
                bindGrid();
            }
            upGalaDinnerType.Update();
            upGalaDinnerTypegrid.Update();

        }
        #endregion

        #region Delete Button Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] != null)
            {
                //Delete the selected record.
                objGalaDinnerType.deleteGalaDinnerType(ViewState["l"].ToString());
                Master.DisplayMessage("Gala Dinner Type Deleted Succesfully.", "successMessage", 5000);
                ViewState["l"] = null;
                bindGrid();
            }
            else
            {
                //Message will display if no record is selected from grid.
                Master.DisplayMessage("Select Record For Delete.", "successMessage", 5000);
            }
            upGalaDinnerType.Update();
            upGalaDinnerTypegrid.Update();
        }
        #endregion

        #region Cancel Button Click
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            txtGalaDinnerType.Text = "";
            ViewState["l"] = null;
            upGalaDinnerType.Update();
        }
        #endregion
    }
}