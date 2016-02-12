using System;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using CRM.DataAccess;
using Microsoft.Practices.EnterpriseLibrary;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.Marketing
{
    
    public partial class MarketingMaterialMaster : System.Web.UI.Page
    {
        #region var
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        #endregion
        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strdata = "GET_DATA_FOR_MARKETING_MATERIAL";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand(strdata, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "COMMON_MARKETING_MATERIAL");
                gridMarketingMaterial.DataSource = ds;
                gridMarketingMaterial.DataBind();

            }
            

            
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (lblcheck.Text == "0")
            {
                CRM.DataAccess.AdministratorEntity.MarketingMaterial objmarketing = new CRM.DataAccess.AdministratorEntity.MarketingMaterial();
                objmarketing.InsertUpdateMarketing(txtTour.Text, txtTitle.Text, txtType.Text, txtExpirationdate.Text, txtdescription.Text, txtEmbadedcode.Text, txtWeburl.Text, lblcheck.Text);

                string strdata = "GET_DATA_FOR_MARKETING_MATERIAL";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand(strdata, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "COMMON_MARKETING_MATERIAL");
                gridMarketingMaterial.DataSource = ds;
                gridMarketingMaterial.DataBind();
                Clear();
                //Response.Write("<script>alert('Recored Save Successfully.')</script>");
            }
            else
            {

                CRM.DataAccess.AdministratorEntity.MarketingMaterial objmarketing = new CRM.DataAccess.AdministratorEntity.MarketingMaterial();
                objmarketing.InsertUpdateMarketing(txtTour.Text, txtTitle.Text, txtType.Text, txtExpirationdate.Text, txtdescription.Text, txtEmbadedcode.Text, txtWeburl.Text, lblcheck.Text);
                string strdata = "GET_DATA_FOR_MARKETING_MATERIAL";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand(strdata, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "COMMON_MARKETING_MATERIAL");
                gridMarketingMaterial.DataSource = ds;
                gridMarketingMaterial.DataBind();
                //Response.Write("<script>alert('Recored Save Successfully.')</script>");
                
                Clear();
                lblcheck.Text = "0";
            }
           
            
        }
        protected void Clear()
        {
            txtTitle.Text = "";
            txtTour.Text = "";
            txtdescription.Text = "";
            txtType.Text = "";
            txtEmbadedcode.Text = "";
            txtExpirationdate.Text = "";
            txtWeburl.Text = "";
            
        }
        protected void gridMarketingMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gridMarketingMaterial.DataKeys[e.RowIndex].Value.ToString();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string strdel = "DELETE_FROM_MARKETING_MATERIAL";
            SqlCommand cmd = new SqlCommand(strdel, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MARID", SqlDbType.Int).Value = int.Parse(id);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Recored Deleted.')</script>");
            gridMarketingMaterial.DataBind();
            conn.Close();
       }
        protected void gridMarketingMaterial_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            lblMarId.Visible = true;
            txtMarId.Visible = true;

            int newindex = e.NewSelectedIndex;
            object pid = gridMarketingMaterial.DataKeys[newindex].Value;
            lblcheck.Text = pid.ToString();
            SqlConnection con1 = new SqlConnection(str);
            con1.Open();
            SqlCommand cmd = new SqlCommand("GET_MARKETING_MATERIAL_FOR_GRID", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MARID", SqlDbType.Int).Value = int.Parse(lblcheck.Text);


            SqlDataReader rdr;
            DataTable dt = new DataTable();
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            txtMarId.Text = dt.Rows[0]["MAR_ID"].ToString();
            txtTour.Text = dt.Rows[0]["TOUR_ID"].ToString();
            txtTitle.Text = dt.Rows[0]["TITLE"].ToString();
            txtType.Text = dt.Rows[0]["TYPE"].ToString();
            txtExpirationdate.Text = dt.Rows[0]["EXPIRATION_DATE"].ToString();
            txtdescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            txtEmbadedcode.Text = dt.Rows[0]["EMBEDCODE"].ToString();
            txtWeburl.Text = dt.Rows[0]["WEBURL"].ToString();
        }
        protected void gridMarketingMaterial_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridMarketingMaterial.PageIndex = e.NewPageIndex;
            gridMarketingMaterial.DataBind();
        }
    }
}