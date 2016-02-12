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
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.BackOffice
{
    public partial class SiteSeeingPrice : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objsight = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();
                lblPrice.Text = Request.QueryString["name"].ToString();
            }

        }
        #endregion

        #region Function
        protected void FillData()
        {
            DataSet ds = objsight.FetchSlabData();
            DataTable dt = new DataTable();
            dt.Columns.Add("SLAB_ID");
            dt.Columns.Add("NO_OF_PAX");
            dt.Columns.Add("SITE_SEEING_SLAB_MASTER_ID");
            dt.Columns.Add("ADULT_SIC_RATE");
            dt.Columns.Add("CHILD_SIC_RATE");
            dt.Columns.Add("ADULT_PVT_RATE");
            dt.Columns.Add("CHILD_PVT_RATE");
            dt.Columns.Add("A_MARGIN_IN_AMOUNT");
            dt.Columns.Add("A_PLUS_MARGIN_IN_AMOUNT");
            dt.Columns.Add("A_PLUS_PLUS_MARGIN_IN_AMOUNT");
            dt.Columns.Add("A_MARGIN_AMOUNT_IN_PERCENTAGE");
            dt.Columns.Add("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE");
            dt.Columns.Add("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE");
            dt.Columns.Add("SIGHT_SEEING_PRICE_ID");
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet ds1 = objsight.Fetch_Data_for_Price(Request.QueryString["PriceId"].ToString(), int.Parse(ds.Tables[0].Rows[i]["SLAB_ID"].ToString()));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["SLAB_ID"] = ds.Tables[0].Rows[i]["SLAB_ID"].ToString();
                            dr["NO_OF_PAX"] = ds.Tables[0].Rows[i]["AutoSearchResult"].ToString();
                            dr["SITE_SEEING_SLAB_MASTER_ID"] = "0";
                            dr["ADULT_SIC_RATE"] = "0";
                            dr["CHILD_SIC_RATE"] = "0";
                            dr["ADULT_PVT_RATE"] = "0";
                            dr["CHILD_PVT_RATE"] = "0";
                            dr["A_MARGIN_IN_AMOUNT"] = "0";
                            dr["A_PLUS_MARGIN_IN_AMOUNT"] = "0";
                            dr["A_PLUS_PLUS_MARGIN_IN_AMOUNT"] = "0";
                            dr["A_MARGIN_AMOUNT_IN_PERCENTAGE"] = "0";
                            dr["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"] = "0";
                            dr["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"] = "0";
                            dr["SIGHT_SEEING_PRICE_ID"] = Request.QueryString["PriceId"].ToString();
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr["SLAB_ID"] = ds1.Tables[0].Rows[0]["SLAB_ID"].ToString();
                            dr["NO_OF_PAX"] = ds1.Tables[0].Rows[0]["NO_OF_PAX"].ToString();
                            dr["SITE_SEEING_SLAB_MASTER_ID"] = ds1.Tables[0].Rows[0]["SITE_SEEING_SLAB_MASTER_ID"].ToString();
                            dr["ADULT_SIC_RATE"] = ds1.Tables[0].Rows[0]["ADULT_SIC_RATE"].ToString();
                            dr["CHILD_SIC_RATE"] = ds1.Tables[0].Rows[0]["CHILD_SIC_RATE"].ToString();
                            dr["ADULT_PVT_RATE"] = ds1.Tables[0].Rows[0]["ADULT_PVT_RATE"].ToString();
                            dr["CHILD_PVT_RATE"] = ds1.Tables[0].Rows[0]["CHILD_PVT_RATE"].ToString();
                            dr["A_MARGIN_IN_AMOUNT"] = ds1.Tables[0].Rows[0]["A_MARGIN_IN_AMOUNT"].ToString();
                            dr["A_PLUS_MARGIN_IN_AMOUNT"] = ds1.Tables[0].Rows[0]["A_PLUS_MARGIN_IN_AMOUNT"].ToString();
                            dr["A_PLUS_PLUS_MARGIN_IN_AMOUNT"] = ds1.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_IN_AMOUNT"].ToString();
                            dr["A_MARGIN_AMOUNT_IN_PERCENTAGE"] = ds1.Tables[0].Rows[0]["A_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                            dr["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"] = ds1.Tables[0].Rows[0]["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                            dr["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"] = ds1.Tables[0].Rows[0]["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                            dr["SIGHT_SEEING_PRICE_ID"] = ds1.Tables[0].Rows[0]["SIGHT_SEEING_PRICE_ID"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }

                }

                GridSiteSeeingPrice.DataSource = dt;
                GridSiteSeeingPrice.DataBind();

                foreach (GridViewRow item in GridSiteSeeingPrice.Rows)
                {
                    int itm = item.DataItemIndex;
                    Label lblslabid = (Label)item.FindControl("lblslabid");
                    Label lblmasterid = (Label)item.FindControl("lblmasterid");
                    Label lblPriceid = (Label)item.FindControl("lblPriceid");
                    Label noofpax = (Label)item.FindControl("lblNoOfPax");
                    TextBox txtadultsic = (TextBox)item.FindControl("txtAdultSIC");
                    TextBox txtChildsic = (TextBox)item.FindControl("txtChildSIC");
                    TextBox txtadultpvt = (TextBox)item.FindControl("txtAdultPVT");
                    TextBox txtChildpvt = (TextBox)item.FindControl("txtChildPVT");
                    TextBox txtAmargin = (TextBox)item.FindControl("txtAmargin");
                    TextBox txtAmarginplus = (TextBox)item.FindControl("txtAPlusmargin");
                    TextBox txtAmarginplus2 = (TextBox)item.FindControl("txtAPlusPlusmargin");
                    TextBox txtAmarginPer = (TextBox)item.FindControl("txtAmargininper");
                    TextBox txtAmarginPerplus = (TextBox)item.FindControl("txtAPlusmarginInper");
                    TextBox txtAmarginPerplus2 = (TextBox)item.FindControl("txtAPlusPlusmargininper");
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (itm == k)
                        {
                            lblslabid.Text = dt.Rows[itm]["SLAB_ID"].ToString();
                            lblmasterid.Text = dt.Rows[itm]["SITE_SEEING_SLAB_MASTER_ID"].ToString();
                            lblPriceid.Text = dt.Rows[itm]["SIGHT_SEEING_PRICE_ID"].ToString();
                            noofpax.Text = dt.Rows[itm]["NO_OF_PAX"].ToString();
                            txtadultsic.Text = dt.Rows[itm]["ADULT_SIC_RATE"].ToString();
                            txtChildsic.Text = dt.Rows[itm]["CHILD_SIC_RATE"].ToString();
                            txtadultpvt.Text = dt.Rows[itm]["ADULT_PVT_RATE"].ToString();
                            txtChildpvt.Text = dt.Rows[itm]["CHILD_PVT_RATE"].ToString();
                            txtAmargin.Text = dt.Rows[itm]["A_MARGIN_IN_AMOUNT"].ToString();
                            txtAmarginplus.Text = dt.Rows[itm]["A_PLUS_MARGIN_IN_AMOUNT"].ToString();
                            txtAmarginplus2.Text = dt.Rows[itm]["A_PLUS_PLUS_MARGIN_IN_AMOUNT"].ToString();
                            txtAmarginPer.Text = dt.Rows[itm]["A_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                            txtAmarginPerplus.Text = dt.Rows[itm]["A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                            txtAmarginPerplus2.Text = dt.Rows[itm]["A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE"].ToString();
                        }
                    }
                }
            }
            upHotel1.Update();
        }
        #endregion

        #region Save Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool amargin1 = false;
            bool amargin2 = false;
            bool amargin3 = false;
            try
            {

                foreach (GridViewRow item in GridSiteSeeingPrice.Rows)
                {
                    Label lblslabid = (Label)item.FindControl("lblslabid");
                    Label lblmasterid = (Label)item.FindControl("lblmasterid");
                    Label lblPriceid = (Label)item.FindControl("lblPriceid");
                    Label noofpax = (Label)item.FindControl("lblNoOfPax");
                    TextBox txtadultsic = (TextBox)item.FindControl("txtAdultSIC");
                    TextBox txtChildsic = (TextBox)item.FindControl("txtChildSIC");
                    TextBox txtadultpvt = (TextBox)item.FindControl("txtAdultPVT");
                    TextBox txtChildpvt = (TextBox)item.FindControl("txtChildPVT");
                    TextBox txtAmargin = (TextBox)item.FindControl("txtAmargin");
                    TextBox txtAmarginplus = (TextBox)item.FindControl("txtAPlusmargin");
                    TextBox txtAmarginplus2 = (TextBox)item.FindControl("txtAPlusPlusmargin");
                    TextBox txtAmarginPer = (TextBox)item.FindControl("txtAmargininper");
                    TextBox txtAmarginPerplus = (TextBox)item.FindControl("txtAPlusmarginInper");
                    TextBox txtAmarginPerplus2 = (TextBox)item.FindControl("txtAPlusPlusmargininper");
                    
                    decimal amargin=0;
                    decimal amarginplus=0;
                    decimal amarginplus2=0;
                    decimal amarginper=0;
                    decimal amarginperplus=0;
                    decimal amarginperplus2=0;
                    if (txtAmargin.Text != "")
                    {
                        amargin = Convert.ToDecimal(txtAmargin.Text);
                    }
                    if (txtAmarginplus.Text != "")
                    {
                        amarginplus = Convert.ToDecimal(txtAmarginplus.Text);
                    }
                    if (txtAmarginplus2.Text != "")
                    {
                        amarginplus2 = Convert.ToDecimal(txtAmarginplus2.Text);
                    }
                    if (txtAmarginPer.Text != "")
                    {
                        amarginper = Convert.ToDecimal(txtAmarginPer.Text);
                    }
                    if (txtAmarginPerplus.Text != "")
                    {
                        amarginperplus = Convert.ToDecimal(txtAmarginPerplus.Text);
                    }
                    if (txtAmarginPerplus2.Text != "")
                    {
                        amarginperplus2 = Convert.ToDecimal(txtAmarginPerplus2.Text);
                    }
                 
                    if (amargin == 0 && amarginper == 0)
                    {
                        amargin1 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Enter Either A Margin Or A Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }
                    else if (amargin != 0 && amarginper != 0)
                    {
                        amargin1 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('You Can not Enter Both A Margin Or A Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }
                    if (amarginplus == 0 && amarginperplus == 0)
                    {
                        amargin2 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Enter Either A+ Margin Or A+ Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }
                    else if (amarginplus != 0 && amarginperplus != 0)
                    {
                        amargin2 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('You Cant Enter Both A+ Margin Or A+ Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }
                    if (amarginplus2 == 0 && amarginperplus2 == 0)
                    {
                        amargin3 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Enter Either A++ Margin Or A++ Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }
                    else if (amarginplus2 != 0 && amarginperplus2 != 0)
                    {
                        amargin3 = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('You Cant Enter Both A++ Margin Or A++ Margin in[%] in No of Pax " + noofpax.Text + ".')", true);
                        break;
                    }


                    if (amargin1 == false && amargin2 == false && amargin3 == false)
                    {
                        objsight.InsertUpdateSightSeeingPrice(lblmasterid.Text, lblslabid.Text, lblPriceid.Text, int.Parse(noofpax.Text), txtadultsic.Text, txtChildsic.Text, txtadultpvt.Text, txtChildpvt.Text, txtAmargin.Text, txtAmarginplus.Text, txtAmarginplus2.Text, txtAmarginPer.Text, txtAmarginPerplus.Text, txtAmarginPerplus2.Text);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (amargin1 == false && amargin2 == false && amargin3 == false)
                {
                    FillData();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Price Save Successfully.')", true);
                }
            }
        }
        #endregion

        #region Back Click
        public void btnBack_Click(Object sender, EventArgs e)
        {
            Response.Redirect("~/Views/BackOffice/SiteSeeingPriceListMaster.aspx");
        }
        #endregion
    }
}