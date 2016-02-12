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
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using CRM.DataAccess.AdministrationDAL; 

namespace CRM.WebApp.Views.Administration
{
    public partial class TransferPackageTimes : System.Web.UI.Page
    {
        TransferPackageTimings objTransferPackageTimings = new TransferPackageTimings();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objTransferPackageTimings.GetAllTrasferpackage();
                GV_Result.DataSource = ds;
                GV_Result.DataBind();
            }
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objTransferPackageTimings.GetAllTrasferpackage();
            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;

            string TransferPackageId = GV_Result.Rows[newindex].Cells[2].Text;
            //string vouchertype = GV_Result.Rows[newindex].Cells[4].Text;

            Response.Redirect("~/Views/Administration/TransferPackageTimingMaster.aspx?ID=" + TransferPackageId);

        }
    }
}