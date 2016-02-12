using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;


namespace CRM.WebApp.Views.GITSearch
{
    public partial class ReconfirmedGITPackages : System.Web.UI.Page
    {
        CRM.DataAccess.GIT.GITSearch objGITSearch = new CRM.DataAccess.GIT.GITSearch();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsval = objGITSearch.commonsp("FETCH_ORDER_STATUS");
            binddropdownlist(DropDownList2, dsval);
            DropDownList2.Text = "Reconfirmed";
            if (!IsPostBack)
            {
                
                DataSet ds = objGITSearch.SearchPackages(txtrefrence.Text, TextBox2.Text, TextBox3.Text, DropDownList2.Text);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {

                    GV_Result.DataSource = ds.Tables[0];
                    GV_Result.DataBind();



                }
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

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;


            string tourid = GV_Result.Rows[newindex].Cells[1].Text;

            string usrid = Session["usersid"].ToString();

            Response.Redirect("~/Views/GIT/GitGroupInformation.aspx?TOURID=" + tourid);
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
            DataSet ds = objGITSearch.SearchPackages(txtrefrence.Text, TextBox2.Text, TextBox3.Text, DropDownList2.Text);
            pnlMainHead.Attributes.Add("style", "display:none");
            Button4.Attributes.Add("style", "display:none");
            Button3.Attributes.Add("style", "display");
            GV_Result.DataSource = ds.Tables[0];
            GV_Result.DataBind();
            Updateconfirm.Update();

        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;
            if (txtrefrence.Text == "")
            {
                txtrefrence.Text = "0";
            }

            DataSet ds = objGITSearch.SearchPackages(txtrefrence.Text, TextBox2.Text, TextBox3.Text, DropDownList2.Text);

            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            Updateconfirm.Update();
        }
    }
}