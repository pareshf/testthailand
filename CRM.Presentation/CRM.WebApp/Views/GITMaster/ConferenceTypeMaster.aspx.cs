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
    public partial class ConferenceTypeMaster : System.Web.UI.Page
    {
        ConferenceTypeMasterDal objconferencetype = new ConferenceTypeMasterDal();

        #region Variables
        DataSet ds = null;
        string ConferenceType_id = "0";
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
            string ConferenceType = txtConferenceType.Text;
            string userid = Session["usersid"].ToString();


            if (ViewState["l"] == null)
            {
                DataSet dtforvalidation = objconferencetype.FetchCountForValidation(ConferenceType);
                if (dtforvalidation.Tables[0].Rows[0]["COUNT"].ToString() == "0")
                {
                    // Insert The New Record
                    objconferencetype.insertConferenceTypeMaster(ConferenceType_id, ConferenceType, userid);
                    txtConferenceType.Text = "";
                    bindGrid();
                    Master.DisplayMessage("Conference Type Save Succesfully.", "successMessage", 5000);
                }
                else
                {
                    Master.DisplayMessage("Record already exists.", "successMessage", 5000);
                }

            }
            else
            {
                //Update The Existing Record
                objconferencetype.insertConferenceTypeMaster(ViewState["l"].ToString(), ConferenceType,userid);
                txtConferenceType.Text = "";
                ViewState["l"] = null;
                bindGrid();
                Master.DisplayMessage("Conference Type Updated Succesfully.", "successMessage", 5000);

            }
            upConferenceType.Update();
            upConferenceTypegrid.Update();
        }

        protected void bindGrid()
        {
            //Bind the grid.
            ds = objconferencetype.bindTransferNameGrid();
            gridConferenceType.DataSource = ds;
            gridConferenceType.DataBind();
        }

        protected void GV_time_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridConferenceType.PageIndex = e.NewPageIndex;
            bindGrid();
            upConferenceTypegrid.Update();

        }

        #region Check Chage Event Of Grid
        public void CheckChanged(object sender, EventArgs e)
        {
            ViewState["l"] = null;
            //on each item checked, remove any other items checked
            Label l = new Label();
            foreach (GridViewRow item in gridConferenceType.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rb1");
                if (rb != sender)
                {
                    rb.Checked = false;
                }

                if (rb.Checked == true)
                {
                    l = (Label)item.FindControl("lblConferenceTypeID");
                    ViewState["l"] = l.Text;
                }
            }
            upConferenceTypegrid.Update();
            upConferenceType.Update();
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
                ds = objconferencetype.editConferenceType(ViewState["l"].ToString());
                ConferenceType_id = ds.Tables[0].Rows[0][0].ToString();
                txtConferenceType.Text = ds.Tables[0].Rows[0]["CONFERENCE_TYPE"].ToString();
                bindGrid();
            }
            upConferenceType.Update();
            upConferenceTypegrid.Update();

        }
        #endregion

        #region Delete Button Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] != null)
            {
                //Delete the selected record.
                objconferencetype.deleteConferenceType(ViewState["l"].ToString());
                Master.DisplayMessage("Conference Type Deleted Succesfully.", "successMessage", 5000);
                ViewState["l"] = null;
                bindGrid();
            }
            else
            {
                //Message will display if no record is selected from grid.
                Master.DisplayMessage("Select Record For Delete.", "successMessage", 5000);
            }
            upConferenceType.Update();
            upConferenceTypegrid.Update();
        }
        #endregion

        #region Cancel Button Click
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            txtConferenceType.Text = "";
            ViewState["l"] = null;
            upConferenceType.Update();
        }
        #endregion


    }
}