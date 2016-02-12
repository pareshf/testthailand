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


namespace CRM.WebApp.Views.FIT
{
    public partial class Vouchers : System.Web.UI.Page
    {
        CRM.DataAccess.FIT.Voucher objVouchersStoredProcedure = new CRM.DataAccess.FIT.Voucher();
        string str = ConfigurationManager.ConnectionStrings["CRM"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objVouchersStoredProcedure.fetchGridData();
            gvInvoiceData.DataSource = dt;
            gvInvoiceData.DataBind();
            upGrid.Update();
        }

        protected void gvInvoiceData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "hotel")
            {
                String Path = e.CommandArgument.ToString();
            }
        }

        protected void gvInvoiceData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int newindex = e.NewSelectedIndex;
            String hotel_path = gvInvoiceData.Rows[newindex].Cells[3].Text;
            //Response.ContentType = "application/zip";
            //Response.AddHeader("content-disposition", "attachment; filename=" + outputFileName);
            //using (ZipFile zipfile = new ZipFile())
            //{
            //    zipfile.AddSelectedFiles("*.*", folderName, includeSubFolders);
            //    zipfile.Save(response.OutputStream);
            //} 
            
        }
        protected void download_click(object sender,EventArgs e)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            byte[] renderedBytes2;
            string deviceInfo2 =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>10in</PageWidth>" +
                "  <PageHeight>9in</PageHeight>" +
                "  <MarginTop>0.50in</MarginTop>" +
                "  <MarginLeft>0.50in</MarginLeft>" +
                "  <MarginRight>0.50in</MarginRight>" +
                "  <MarginBottom>0.50in</MarginBottom>" +
                "</DeviceInfo>";
            
                string ht_vchrquoteid = "1";
                DataTable supplier_id = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();


                SqlCommand comm = new SqlCommand("FETCH_SUPPLIER_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                SqlDataReader rdr = comm.ExecuteReader();
                supplier_id.Load(rdr);
            
                for (int i_supp = 0; i_supp < supplier_id.Rows.Count; i_supp++)
                {

                    String supplierid = supplier_id.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    DataTable dt_hotelEmails = new DataTable();

                    SqlCommand email = new SqlCommand("FETCH_SUPPLIER_EMAIL_FOR_MAIL", conn);
                    email.CommandType = CommandType.StoredProcedure;
                    email.Parameters.Add("@QUOTE_ID", SqlDbType.Int).Value = int.Parse(ht_vchrquoteid);
                    SqlDataReader rdremail = email.ExecuteReader();
                    dt_hotelEmails.Load(rdremail);

                    String hotelsupplierid = dt_hotelEmails.Rows[i_supp]["SUPPLIER_ID"].ToString();
                    String supplier_email = dt_hotelEmails.Rows[i_supp]["SUPPLIER_REL_EMAIL"].ToString();


                    if (hotelsupplierid == supplierid)
                    {
                        ReportParameter[] parmhotel = new ReportParameter[1];
                        ReportParameter[] parmsuppid = new ReportParameter[1];
                        parmhotel[0] = new ReportParameter("SALSE_INVOICE_ID","1");
                        parmsuppid[0] = new ReportParameter("SUPPLIER_ID", hotelsupplierid.ToString());
                        rptViewer1.ShowCredentialPrompts = false;
                        rptViewer1.ShowParameterPrompts = false;

                        rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

                        rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
                        rptViewer1.ServerReport.ReportPath = "/ThailandReport/HotelVoucher";
                        rptViewer1.ServerReport.SetParameters(parmhotel);
                        rptViewer1.ServerReport.SetParameters(parmsuppid);
                        rptViewer1.ServerReport.Refresh();

                       // renderedBytes2 = rptViewer1.ServerReport.Render(reportType, deviceInfo2, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        //Response.Clear();
                       // Response.ContentType = "application/pdf";
                       // Response.AddHeader("content-disposition", "attachment; filename= Quotation.pdf");
                       // Response.BinaryWrite(renderedBytes2);
                        //Response.End();
                       // Response.OutputStream.Write(renderedBytes2, 0, renderedBytes2.Length);
                        upReport.Update();
                    }
                }
                conn.Close();
         }
    }
}