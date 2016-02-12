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
    public partial class TransferServicePrice : System.Web.UI.Page
    {
        #region Objects

        TransferPackageStoredProcedure objtrans = new TransferPackageStoredProcedure();
        CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objsight = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataSet ds = objsight.FetchSlabData();
                //binddropdownlistwithid(drpTransferTo, ds);
                FillData();
                lblPrice.Text = Request.QueryString["name"].ToString();
            }
        }

        #endregion

        #region Fill Data For Grid

        protected void FillData()
        {
            DataSet ds = objsight.FetchSlabData();
            DataTable dt = new DataTable();
            dt.Columns.Add("SLAB_ID");
            dt.Columns.Add("NO_OF_PAX");
            dt.Columns.Add("TRANSFER_PACKAGE_PRICE_SLAB_ID");
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
            dt.Columns.Add("TRANSFER_PACKAGE_FROM_TO_DETAIL_ID");
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet ds1 = objtrans.Fetch_Data_for_Price(Request.QueryString["transid"].ToString(), int.Parse(ds.Tables[0].Rows[i]["SLAB_ID"].ToString()));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["SLAB_ID"] = ds.Tables[0].Rows[i]["SLAB_ID"].ToString();
                            dr["NO_OF_PAX"] = ds.Tables[0].Rows[i]["AutoSearchResult"].ToString();
                            dr["TRANSFER_PACKAGE_PRICE_SLAB_ID"] = "0";
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
                            dr["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"] = Request.QueryString["transid"].ToString();
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr["SLAB_ID"] = ds1.Tables[0].Rows[0]["SLAB_ID"].ToString();
                            dr["NO_OF_PAX"] = ds1.Tables[0].Rows[0]["NO_OF_PAX"].ToString();
                            dr["TRANSFER_PACKAGE_PRICE_SLAB_ID"] = ds1.Tables[0].Rows[0]["TRANSFER_PACKAGE_PRICE_SLAB_ID"].ToString();
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
                            dr["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"] = ds1.Tables[0].Rows[0]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }

                }

                GridSiteSeeingPrice.DataSource = dt;
                GridSiteSeeingPrice.DataBind();
                
                //--- Set Data for Grid -----------------

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
                            lblmasterid.Text = dt.Rows[itm]["TRANSFER_PACKAGE_PRICE_SLAB_ID"].ToString();
                            lblPriceid.Text = dt.Rows[itm]["TRANSFER_PACKAGE_FROM_TO_DETAIL_ID"].ToString();
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

        #region Not Used Code

        //protected void binddropdownlist(DropDownList r, DataSet d)
        //{
        //    r.Items.Clear();
        //    r.DataSource = d;
        //    r.DataTextField = "AutoSearchResult";
        //    r.DataBind();
        //    r.Items.Insert(0, new ListItem("", ""));

        //}
        
        //protected void binddropdownlistwithid(DropDownList r, DataSet d)
        //{
        //    r.Items.Clear();
        //    r.DataSource = d;
        //    r.DataTextField = "AutoSearchResult";
        //    r.DataValueField = "SLAB_ID";
        //    r.DataBind();
        //    r.Items.Insert(0, new ListItem("", ""));

        //}
        //protected void AddTransServices(GridView gv, UpdatePanel uppanel)
        //{
        //    int count = gv.Rows.Count;
        //    int count1 = count + 1;
        //    DataTable dt = new DataTable();
            
        //    foreach (GridViewRow item in gv.Rows)
        //    {
        //        Label lblslabid = (Label)item.FindControl("lblslabid");
        //        Label lblmasterid = (Label)item.FindControl("lblmasterid");
        //        Label lblPriceid = (Label)item.FindControl("lblPriceid");
        //        TextBox txtadultsic = (TextBox)item.FindControl("txtAdultSIC");
        //        TextBox txtChildsic = (TextBox)item.FindControl("txtChildSIC");
        //        TextBox txtadultpvt = (TextBox)item.FindControl("txtAdultPVT");
        //        TextBox txtChildpvt = (TextBox)item.FindControl("txtChildPVT");
        //        DropDownList drpfrom = (DropDownList)item.FindControl("drpTransferFrom");
        //        DropDownList drpto = (DropDownList)item.FindControl("drpTransferTo");

        //        if (dt.Columns.Count == 0)
        //        {
        //            dt.Columns.Add("SLAB_ID");
        //            dt.Columns.Add("TRANSFER_PACKAGE_PRICE_SLAB_ID");
        //            dt.Columns.Add("FROM_PLACE");
        //            dt.Columns.Add("TO_PLACE");
        //            dt.Columns.Add("ADULT_SIC_RATE");
        //            dt.Columns.Add("CHILD_SIC_RATE");
        //            dt.Columns.Add("ADULT_PVT_RATE");
        //            dt.Columns.Add("CHILD_PVT_RATE");
        //            dt.Columns.Add("TRANSFER_PACKAGE_PRICE_ID");
        //        }

        //        DataRow dr = dt.NewRow();

        //        dr["SLAB_ID"] = lblslabid.Text;
        //        dr["TRANSFER_PACKAGE_PRICE_SLAB_ID"] = lblmasterid.Text;
        //        dr["FROM_PLACE"] = drpfrom.Text;
        //        dr["TO_PLACE"] = drpto.Text;
        //        dr["ADULT_SIC_RATE"] =txtadultsic.Text ;
        //        dr["CHILD_SIC_RATE"] =txtChildsic.Text ;
        //        dr["ADULT_PVT_RATE"] = txtadultpvt.Text;
        //        dr["CHILD_PVT_RATE"] = txtChildpvt.Text;
        //        dr["TRANSFER_PACKAGE_PRICE_ID"] = lblPriceid.Text;
        //        dt.Rows.Add(dr);

        //    }

        //    if (count == 0)
        //    {
        //        if (dt.Columns.Count == 0)
        //        {
        //            dt.Columns.Add("SLAB_ID");
        //            dt.Columns.Add("TRANSFER_PACKAGE_PRICE_SLAB_ID");
        //            dt.Columns.Add("FROM_PLACE");
        //            dt.Columns.Add("TO_PLACE");
        //            dt.Columns.Add("ADULT_SIC_RATE");
        //            dt.Columns.Add("CHILD_SIC_RATE");
        //            dt.Columns.Add("ADULT_PVT_RATE");
        //            dt.Columns.Add("CHILD_PVT_RATE");
        //            dt.Columns.Add("TRANSFER_PACKAGE_PRICE_ID");
        //        }


        //        DataRow dr = dt.NewRow();
        //        dr["SLAB_ID"] = "";
        //        dr["TRANSFER_PACKAGE_PRICE_SLAB_ID"] = "0";
        //        dr["FROM_PLACE"] = "";
        //        dr["TO_PLACE"] = "";
        //        dr["ADULT_SIC_RATE"] = "0";
        //        dr["CHILD_SIC_RATE"] = "0";
        //        dr["ADULT_PVT_RATE"] = "0";
        //        dr["CHILD_PVT_RATE"] = "0";
        //        dr["TRANSFER_PACKAGE_PRICE_ID"] = Request.QueryString["transid"].ToString();

        //        dt.Rows.Add(dr);
        //        gv.DataSource = dt;
        //        gv.DataBind();
        //        uppanel.Update();
        //    }

        //    if (count != 0)
        //    {

        //        DataRow dr1 = dt.NewRow();
        //        dt.Rows.Add(dr1);
        //    }

        //    gv.DataSource = dt;
        //    gv.DataBind();
        //    foreach (GridViewRow item in gv.Rows)
        //    {
        //        int itm = item.DataItemIndex;
        //        Label lblslabid = (Label)item.FindControl("lblslabid");
        //        Label lblmasterid = (Label)item.FindControl("lblmasterid");
        //        Label lblPriceid = (Label)item.FindControl("lblPriceid");
        //        TextBox txtadultsic = (TextBox)item.FindControl("txtAdultSIC");
        //        TextBox txtChildsic = (TextBox)item.FindControl("txtChildSIC");
        //        TextBox txtadultpvt = (TextBox)item.FindControl("txtAdultPVT");
        //        TextBox txtChildpvt = (TextBox)item.FindControl("txtChildPVT");
        //        DropDownList drpfrom = (DropDownList)item.FindControl("drpTransferFrom");
        //        DropDownList drpto = (DropDownList)item.FindControl("drpTransferTo");
        //        for (int k = 0; k < dt.Rows.Count; k++)
        //        {
        //            if (itm == k)
        //            {
        //                DataSet ds = objtrans.fetchComboData("FETCH_ALL_TRANSFER_PACKAGE_FROM_TO_MASTER");
        //                binddropdownlist(drpfrom, ds);
        //                binddropdownlist(drpto, ds);
                        
        //                drpfrom.Text = dt.Rows[itm]["FROM_PLACE"].ToString();
        //                drpto.Text = dt.Rows[itm]["TO_PLACE"].ToString();
        //                //drpTransferTo.Text = dt.Rows[itm]["NO_OF_PAX"].ToString();
        //                txtadultsic.Text = dt.Rows[itm]["ADULT_SIC_RATE"].ToString();
        //                txtChildsic.Text = dt.Rows[itm]["CHILD_SIC_RATE"].ToString();
        //                txtadultpvt.Text = dt.Rows[itm]["ADULT_PVT_RATE"].ToString();
        //                txtChildpvt.Text = dt.Rows[itm]["CHILD_PVT_RATE"].ToString();
        //                lblPriceid.Text = dt.Rows[itm]["TRANSFER_PACKAGE_PRICE_ID"].ToString();
        //                lblmasterid.Text = dt.Rows[itm]["TRANSFER_PACKAGE_PRICE_SLAB_ID"].ToString();
        //                lblslabid.Text = dt.Rows[itm]["SLAB_ID"].ToString();
        //            }
        //        }
        //    }
        //    uppanel.Update();

        //}

        //public void drpNOofPax_SelectedIndexChanged(Object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (drpTransferTo.Text != "")
        //        //{
        //        //    FillAdditionalEditMode(GridSiteSeeingPrice, upHotel1);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        upHotel1.Update();
        //    }

        //}

        //public void btnAddHotel_Click(Object sender, EventArgs e)
        //{
        //    AddTransServices(GridSiteSeeingPrice, upHotel1);
        //}
        
        //protected void FillAdditionalEditMode(GridView gv, UpdatePanel uppanel)
        //{
        //    try
        //    {
        //        DataSet ds = objtrans.FetchTransferPrice(int.Parse(Request.QueryString["transid"].ToString()),int.Parse(""));
        //        if (ds.Tables[0].Rows.Count != 0)
        //        {
        //            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        //            {
        //                foreach (GridViewRow item in gv.Rows)
        //                {
        //                    if (j == item.DataItemIndex)
        //                    {

        //                        Label lblslabid = (Label)item.FindControl("lblslabid");
        //                        Label lblmasterid = (Label)item.FindControl("lblmasterid");
        //                        Label lblPriceid = (Label)item.FindControl("lblPriceid");
        //                        TextBox txtadultsic = (TextBox)item.FindControl("txtAdultSIC");
        //                        TextBox txtChildsic = (TextBox)item.FindControl("txtChildSIC");
        //                        TextBox txtadultpvt = (TextBox)item.FindControl("txtAdultPVT");
        //                        TextBox txtChildpvt = (TextBox)item.FindControl("txtChildPVT");
        //                        DropDownList drpfrom = (DropDownList)item.FindControl("drpTransferFrom");
        //                        DropDownList drpto = (DropDownList)item.FindControl("drpTransferTo");

        //                        drpfrom.Text = ds.Tables[0].Rows[j]["FROM_PLACE"].ToString();
        //                        drpto.Text = ds.Tables[0].Rows[j]["TO_PLACE"].ToString();
        //                       // drpTransferTo.Text = ds.Tables[0].Rows[j]["NO_OF_PAX"].ToString();

        //                        txtadultsic.Text = ds.Tables[0].Rows[j]["ADULT_SIC_RATE"].ToString();
        //                        txtChildsic.Text = ds.Tables[0].Rows[j]["CHILD_SIC_RATE"].ToString();
        //                        txtadultpvt.Text = ds.Tables[0].Rows[j]["ADULT_PVT_RATE"].ToString();
        //                        txtChildpvt.Text = ds.Tables[0].Rows[j]["CHILD_PVT_RATE"].ToString();

        //                        lblPriceid.Text = ds.Tables[0].Rows[j]["TRANSFER_PACKAGE_PRICE_ID"].ToString();
        //                        lblmasterid.Text = ds.Tables[0].Rows[j]["TRANSFER_PACKAGE_PRICE_SLAB_ID"].ToString();
        //                        lblslabid.Text = ds.Tables[0].Rows[j]["SLAB_ID"].ToString();
        //                    }

        //                }
        //                if (j < ds.Tables[0].Rows.Count - 1)
        //                {
        //                    AddTransServices(gv, uppanel);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            AddTransServices(gv, uppanel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {


        //    }


        //}

        #endregion

        #region Save Click

        public void btnSave_Click(Object sender, EventArgs e)
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
                        objtrans.InsertUpdateTransferPrice(lblmasterid.Text, lblslabid.Text, lblPriceid.Text, int.Parse(noofpax.Text), txtadultsic.Text, txtChildsic.Text, txtadultpvt.Text, txtChildpvt.Text, txtAmargin.Text, txtAmarginplus.Text, txtAmarginplus2.Text, txtAmarginPer.Text, txtAmarginPerplus.Text, txtAmarginPerplus2.Text);
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
            Response.Redirect("~/Views/BackOffice/TransferPackagePriceList.aspx");
        }

        #endregion

    }
}