using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace CRM.WebApp.Views.Sales
{
    public partial class BookingCheckList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"]; 

            foreach (GridDataItem dataItem in radgridtourchecklist.Items)
            {

                dt.Rows[dataItem.ItemIndex]["1"] = ((TextBox)dataItem["CHECKLIST_DESCRIPTION"].FindControl("CHECKLIST_DESCRIPTION")).Text;
                dt.Rows[dataItem.ItemIndex]["2"] = ((CheckBox)dataItem["CHECKED"].FindControl("CHECKED")).Checked;
                //TextBox t = (TextBox)radgridtourchecklist.MasterTableView.NamingContainer.FindControl("CHECKLIST_DESCRIPTION");
                //string S = dataItem["CHECKLIST_DESCRIPTION"].Text;
                //Response.Write("<script>alert('" + dataItem["CHECKLIST_DESCRIPTION"].Text + "')</script>");

            }
        }


    }
}