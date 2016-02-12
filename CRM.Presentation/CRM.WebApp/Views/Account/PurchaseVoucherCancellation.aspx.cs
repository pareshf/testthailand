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
using CRM.DataAccess.Account;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.Core.Utility;

namespace CRM.WebApp.Views.Account
{
    public partial class PurchaseVoucherCancellation : System.Web.UI.Page
    {
        PurchaseCancellationSP objPurchaseCancellationSP = new PurchaseCancellationSP();
        FITPaymentStoreProcedure objFITPaymentStoreProcedure = new FITPaymentStoreProcedure();
        AcoountVouchersStoredProcedure objAcoountVouchersStoredProcedure = new AcoountVouchersStoredProcedure();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsInvoie = objPurchaseCancellationSP.commonSp("GET_CANCELLED_INVOICE");
                binddropdownlist(drpInvoice, dsInvoie);
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

        protected void drpInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsPurchaseVoucher = objPurchaseCancellationSP.getPurchaseVoucherInvoiceVise("GET_PURCHASE_VOUCHER_FROM_INVOICE_NO", drpInvoice.Text);
            GridInvoice.DataSource = dsPurchaseVoucher;
            GridInvoice.DataBind();
            bindGrid();
            update_PurcahseCancellation.Visible = true;
            update_PurcahseCancellation.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            DataSet ds_all_gl_code = objFITPaymentStoreProcedure.fetch_all_gl_code();
            DataSet ds_vt = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_TYPE");
            DataSet ds_vsstatus = objFITPaymentStoreProcedure.fetch_voucher_type("FETCH_VOUCHER_STATUS_ID");
            DataSet ds22 = objFITPaymentStoreProcedure.fetch_currency_for_company("FETCH_CURRENCY_FROM_COMPANY", int.Parse(Session["CompanyId"].ToString()));
            DataSet dsCancellation = objFITPaymentStoreProcedure.fetch_voucher_type("GET_CANCELLATION_GL_CODE");

            foreach (GridViewRow item in GridInvoice.Rows)
            {
                Label lblGLDescription = (Label)item.FindControl("lblGLDescription");
                Label lblInvoice = (Label)item.FindControl("lblPurchaseVoucherNo");
                TextBox txtCancellationFees = (TextBox)item.FindControl("txtCancellationFees");
                Label lblVoucherAmount = (Label)item.FindControl("lblVoucherAmount");
                Label lblSupplierType = (Label)item.FindControl("lblSupplierType");
                Label lblPurchaseVoucherStatus = (Label)item.FindControl("lblPurchaseVoucherStatus");

                if (txtCancellationFees.Text == "")
                {
                    txtCancellationFees.Text = "0";
                }

                string amount = (decimal.Parse(lblVoucherAmount.Text) - decimal.Parse(txtCancellationFees.Text)).ToString();


                DataSet dsPurchaseVoucher = objPurchaseCancellationSP.getPurchaseVoucherInvoiceVise("CHECK_PURCHASE_VOUCHER_STATUS", lblInvoice.Text);

                if (dsPurchaseVoucher.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString() == "7" || dsPurchaseVoucher.Tables[0].Rows[0]["VOUCHER_STATUS"].ToString() == "3")
                {
                    if (lblPurchaseVoucherStatus.Text != "3")
                    {
                        objPurchaseCancellationSP.updatePurchaseHeaderOnCancellation(lblInvoice.Text, txtCancellationFees.Text);
                        objAcoountVouchersStoredProcedure.updateCreditNote(lblInvoice.Text, 6, decimal.Parse(txtCancellationFees.Text));
                        objAcoountVouchersStoredProcedure.updateCreditNote(lblInvoice.Text, 3, decimal.Parse(amount));
                    }
                }
                else
                {

                    objPurchaseCancellationSP.updatePurchaseHeaderOnCancellation(lblInvoice.Text, txtCancellationFees.Text);

                    objFITPaymentStoreProcedure.insert_accounts_entry(0, lblGLDescription.Text, lblInvoice.Text, date, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, amount, amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, lblGLDescription.Text, lblInvoice.Text, date, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", amount, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    if (lblSupplierType.Text == "Hotel")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }
                    else if (lblSupplierType.Text == "Transfer Package Company")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[5]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }
                    else if (lblSupplierType.Text == "Sightseeing Company")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[3]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[3]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, amount, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }

                    objFITPaymentStoreProcedure.insert_accounts_entry(0, lblGLDescription.Text, lblInvoice.Text, date, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txtCancellationFees.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 1);
                    objFITPaymentStoreProcedure.insert_accounts_entry(0, dsCancellation.Tables[0].Rows[0]["GL_DESCRIPTION"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, "", txtCancellationFees.Text, "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);

                    if (lblSupplierType.Text == "Hotel")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[2]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txtCancellationFees.Text, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }
                    else if (lblSupplierType.Text == "Transfer Package Company")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[5]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txtCancellationFees.Text, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }
                    else if (lblSupplierType.Text == "Sightseeing Company")
                    {
                        objFITPaymentStoreProcedure.insert_accounts_entry(0, ds_all_gl_code.Tables[0].Rows[3]["AutoSearchResult"].ToString(), lblInvoice.Text, date, ds_vt.Tables[0].Rows[6]["AutoSearchResult"].ToString(), int.Parse(Session["usersid"].ToString()), "", int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), int.Parse(Session["usersid"].ToString()), ds_vsstatus.Tables[0].Rows[2]["AutoSearchResult"].ToString(), 0, txtCancellationFees.Text, "", "", "", "", "", "", "", "", "", "", "", int.Parse(Session["CompanyId"].ToString()), ds22.Tables[0].Rows[0]["AutoSearchResult"].ToString(), 2);
                    }

                }
            }

            Master.DisplayMessage("Record Saved Successfully.", "successMessage", 8000);
        }


        protected void bindGrid()
        {
            try
            {
                int i = 0;
                foreach (GridViewRow item in GridInvoice.Rows)
                {
                    Label lblPurchaseVoucherStatus = (Label)item.FindControl("lblPurchaseVoucherStatus");

                    if (lblPurchaseVoucherStatus.Text == "3")
                    {
                        TextBox txtCancellationFees = (TextBox)item.FindControl("txtCancellationFees");
                        txtCancellationFees.Enabled = false;
                        i = i + 1;
                    }
                }

                if (i == GridInvoice.Rows.Count)
                {
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                update_PurcahseCancellation.Update();
            }

        }


    }
}