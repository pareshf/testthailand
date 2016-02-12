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
using System.Net;
using CRM.DataAccess.SecurityDAL;
using CRM.DataAccess.FIT;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace CRM.WebApp.Views.Account
{
    public partial class AgentDownloadVoucher : System.Web.UI.Page
    {
        DownloadVoucherStoredProcedure objDownloadVoucherStoredProcedure = new DownloadVoucherStoredProcedure();
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        BookingFitStoreProcedure objBookingFitStoreProcedure = new BookingFitStoreProcedure();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //Check Page Authorization

            //String CompId, DeptId, RoleId;
            //CompId = Session["CompanyId"].ToString();
            //DeptId = Session["DeptId"].ToString();
            //RoleId = Session["RoleId"].ToString();

            //DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 216);
            //if (dt.Rows.Count <= 0 || Convert.ToBoolean(dt.Rows[0]["READ_ACCESS"]) == false)
            //{
            //    Response.Redirect("~/Views/InvalidAccess.aspx");
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objBookingFitStoreProcedure.GET_AGENT_QUOTEID(int.Parse(Session["usersid"].ToString()));
                binddropdownlist(drp_quote, ds);
            }
        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));

        }
        #endregion

        protected void drp_quote_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(drp_quote.Text));

            GV_Result.DataSource = ds;
            GV_Result.DataBind();

            update_voucher.Update();
        }

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(drp_quote.Text));

            GV_Result.DataSource = ds;
            GV_Result.DataBind();

            update_voucher.Update();

        }

        protected void GV_Result_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            Session["quote_id"] = drp_quote.Text;
            if (e.CommandName == "Download")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["id"] = index;
                DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(drp_quote.Text));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == index)
                    {
                        if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Hotel")
                        {



                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "HotelVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + ".pdf");


                            Response.TransmitFile(Server.MapPath("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "HotelVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + ".pdf"));

                            Response.End();
                            break;
                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Transfer Package Company")
                        {

                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "TransferVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf");


                            Response.TransmitFile(Server.MapPath("~/Views/FIT/TransferVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "TransferVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf"));

                            Response.End();


                            break;

                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Sightseeing Company")
                        {

                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "SightSeeingVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + ".pdf");


                            Response.TransmitFile(Server.MapPath("~/Views/FIT/SightSeeingVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "SightSeeingVoucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + ".pdf"));

                            Response.End();


                            break;

                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Additional Services")
                        {
                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Additional Service Voucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf");


                            Response.TransmitFile(Server.MapPath("~/Views/FIT/AdditionalServicesVoucher/" + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "/" + "Additional Service Voucher" + " " + ds.Tables[0].Rows[i]["SALES_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + ".pdf"));

                            Response.End();

                            break;

                        }
                    }
                }




            }
            else if (e.CommandName == "E-mail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
            }
        }
    }
}