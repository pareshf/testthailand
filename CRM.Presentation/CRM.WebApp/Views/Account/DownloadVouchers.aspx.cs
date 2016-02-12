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

namespace CRM.WebApp.Views.Account
{
    public partial class DownloadVouchers : System.Web.UI.Page
    {
        DownloadVoucherStoredProcedure objDownloadVoucherStoredProcedure = new DownloadVoucherStoredProcedure();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GV_Result_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //int newindex = e.NewSelectedIndex;

            //string voucherno = GV_Result.Rows[newindex].Cells[2].Text;

            //Response.Redirect("~/Views/Account/Vouchers.aspx?VN=" + voucherno);
        }

        #region METHOD OF BIND DROP DOWNS
        protected void binddropdownlist(DropDownList r, DataSet d)
        {
            r.Items.Clear();
            r.DataSource = d;
            r.DataTextField = "AutoSearchResult";
            r.DataBind();
            r.Items.Insert(0, new ListItem("", ""));
            //  r.SelectedValue = "1";
        }
        #endregion

        protected void GV_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Result.PageIndex = e.NewPageIndex;

            DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(TextBox1.Text));

            GV_Result.DataSource = ds;
            GV_Result.DataBind();

            update_voucher.Update();
           
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(TextBox1.Text));

            GV_Result.DataSource = ds;
            GV_Result.DataBind();

            update_voucher.Update();
        }

        public void Save_Check(object sender, EventArgs e)
        {
            //GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            //  Label lblProdId = (Label)row.FindControl(“lblproductId”);
        }

    //    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //if (e.CommandName == "Edit")
    // {
    //    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
    //    // row contains current Clicked Gridview Row
    //    String VersionId = row.Cells[CellIndex].Text;
        
    //    //e.CommandArgument  -- this return Data Key Value
    //}
    //    }

        protected void GV_Result_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            Session["quote_id"] = TextBox1.Text;
            if (e.CommandName == "Download")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["id"] = index;
                DataSet ds = objDownloadVoucherStoredProcedure.fetch_purchase_vouhcer("FETCH_VOUCHER_FROM_QUOTE_ID", int.Parse(TextBox1.Text));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == index)
                    {
                        if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Hotel")
                        {
                           
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'new.aspx', null, 'height=1000,width=800,status=yes,toolbar=no,menubar=no,location=no' );", true);
                            
                      //      Response.AddHeader("Content-Disposition", "attachment; filename=7-123HotelVoucher.pdf");
                      //      Response.ContentType = "application/pdf";
                      //  //    update_voucher.Update();
                      //      Response.Write(Server.MapPath("~/Views/FIT/Vouchers/" + TextBox1.Text + "/" + TextBox1.Text + "-" + "123" + "HotelVoucher.pdf"));
                      ////      Response.TransmitFile(Server.MapPath("~/Views/FIT/Vouchers/" + TextBox1.Text + "/" + TextBox1.Text + "-" + "123" + "HotelVoucher.pdf"));

                      // //     update_voucher.Update();

                      //      Response.End();

                      //      Response.Redirect("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "HotelVoucher.pdf");
                            //string pdfPath = Server.MapPath("~/Views/FIT/Vouchers/" + TextBox1.Text + "/" + TextBox1.Text + "-" + "123" + "HotelVoucher.pdf");   //ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() 
                            //WebClient client = new WebClient();
                            //Byte[] buffer = client.DownloadData(pdfPath);
                            //Response.ContentType = "application/pdf";
                            //Response.AddHeader("content-length", buffer.Length.ToString());
                            //Response.BinaryWrite(buffer);
                           break;
                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Transfer Package Company")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'new.aspx', null, 'height=1000,width=800,status=yes,toolbar=no,menubar=no,location=no' );", true);
                            break;
                           // Response.Redirect("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "-" + ds.Tables[0].Rows[i]["TRANSFER_PACKAGE_DETAIL_ID"].ToString() + "TransferVoucher.pdf");
                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Sightseeing Company")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'new.aspx', null, 'height=1000,width=800,status=yes,toolbar=no,menubar=no,location=no' );", true);
                            break;
                          //  Response.Redirect("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "SightSeeingVoucher.pdf");
                        }
                        else if (ds.Tables[0].Rows[i]["SUPPLIER_TYPE_NAME"].ToString() == "Additional Services")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'new.aspx', null, 'height=1000,width=800,status=yes,toolbar=no,menubar=no,location=no' );", true);
                            break;
                            //  Response.Redirect("~/Views/FIT/Vouchers/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "/" + ds.Tables[0].Rows[i]["PURCHASE_INVOICE_ID"].ToString() + "-" + ds.Tables[0].Rows[i]["SUPPLIER_SR_NO"].ToString() + "SightSeeingVoucher.pdf");
                        }
                    }
                }
                
               

             //   "~/Views/FIT/Vouchers/" + invoice_id.ToString() + "/" + invoice_id.ToString() + "-" + hotelsupplierid.ToString() + "HotelVoucher.pdf")
            }
            else if (e.CommandName == "E-mail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
            }
        }
    }



}