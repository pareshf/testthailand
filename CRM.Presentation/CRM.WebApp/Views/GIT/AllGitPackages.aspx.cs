using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.Account;
using CRM.DataAccess.GIT;
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

namespace CRM.WebApp.Views.GIT
{
    public partial class AllGitPackages : System.Web.UI.Page
    {
        GitGroupInforamtion objGitGroupInforamtion = new GitGroupInforamtion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objGitGroupInforamtion.CommonSp("GET_ALL_GIT_GROUP_INFORMATION");

    //            DataTable dt = ds.Tables[0];

    //            IEnumerable<DataRow> query =
    //from product in dt.AsEnumerable()
    //select product;

             //   var result = from m in ds 
              //               select m;

                GV_Result.DataSource = ds;
                GV_Result.DataBind();

                //GV_Result.DataSource = query;
                //GV_Result.DataBind();
            }
        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
          
            //string quoteid = GV_Result.Rows[newindex].Cells[1].Text;
            string tourid = GV_Result.Rows[newindex].Cells[1].Text;
          
            string usrid = Session["usersid"].ToString();
           
          //  DataSet ds = objfitquote.fetchallData("FETCH_DATA_FOR_ALL_BOOKED_FIT_QUOTES", usrid);
           
           

            Response.Redirect("~/Views/GIT/GitGroupInformation.aspx?TOURID=" + tourid );
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objGitGroupInforamtion.CommonSp("GET_ALL_GIT_GROUP_INFORMATION");
            GV_Result.DataSource = ds;
            GV_Result.DataBind();
            upda.Update();
        }
    }
}