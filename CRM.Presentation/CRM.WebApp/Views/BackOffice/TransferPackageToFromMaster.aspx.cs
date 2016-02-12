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


namespace CRM.WebApp.Views.BackOffice
{
    public partial class TransferPackageToFromMaster : System.Web.UI.Page
    {
        TransferPackageToFromMaterDal objTransferPackageToFromMater = new TransferPackageToFromMaterDal();

        #region Variables
        DataSet ds = null;
        string Transfer_id = "0";
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
            string transfer = txtTransferName.Text;
                        
            
            

            if (ViewState["l"] == null)
            {
                // Insert The New Record
                objTransferPackageToFromMater.insertTransferPackageToFrom(Transfer_id, transfer);
                txtTransferName.Text = "";
                bindGrid();
                Master.DisplayMessage("Transfer Name Save Succesfully.", "successMessage", 5000);

            }
            else
            {
                //Update The Existing Record
                objTransferPackageToFromMater.insertTransferPackageToFrom(ViewState["l"].ToString(), transfer);
                txtTransferName.Text = "";
                ViewState["l"] = null;
                bindGrid();
                Master.DisplayMessage("Transfer Name Updated Succesfully.", "successMessage", 5000);

            }
            upTransfer.Update();
            upTransfergrid.Update();
        }

        protected void bindGrid()
        {
            //Bind the grid.
            ds = objTransferPackageToFromMater.bindTransferNameGrid();
            gridTransferName.DataSource = ds;
            gridTransferName.DataBind();
        }

        protected void GV_time_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridTransferName.PageIndex = e.NewPageIndex;
            bindGrid();
            upTransfergrid.Update();

        }

        #region Check Chage Event Of Grid
        public void CheckChanged(object sender, EventArgs e)
        {
            ViewState["l"] = null;
            //on each item checked, remove any other items checked
            Label l = new Label();
            foreach (GridViewRow item in gridTransferName.Rows)
            {
                RadioButton rb = (RadioButton)item.FindControl("rb1");
                if (rb != sender)
                {
                    rb.Checked = false;
                }

                if (rb.Checked == true)
                {
                    l = (Label)item.FindControl("lblTransferID");
                    ViewState["l"] = l.Text;                    
                }
            }
            upTransfergrid.Update();
            upTransfer.Update();
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
                ds = objTransferPackageToFromMater.editTransferName(ViewState["l"].ToString());
                Transfer_id = ds.Tables[0].Rows[0][0].ToString();
                txtTransferName.Text = ds.Tables[0].Rows[0]["TRANSFER_NAME"].ToString();
                bindGrid();
            }
            upTransfer.Update();
            upTransfergrid.Update();

        }
        #endregion

        #region Delete Button Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["l"] != null)
            {
                //Delete the selected record.
                objTransferPackageToFromMater.deleteTransferName(ViewState["l"].ToString());
                Master.DisplayMessage("Transfer Package Name Deleted Succesfully.", "successMessage", 5000);
                ViewState["l"] = null;
                bindGrid();
            }
            else
            {
                //Message will display if no record is selected from grid.
                Master.DisplayMessage("Select Record For Delete.", "successMessage", 5000);
            }
            upTransfer.Update();
            upTransfergrid.Update();
        }
        #endregion

        #region Cancel Button Click
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            txtTransferName.Text = "";
            ViewState["l"] = null;
            upTransfer.Update();
        }
        #endregion
    }
}