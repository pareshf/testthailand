using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.Settings
{
    public partial class UserManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         //   string fullURL = "window.open('" + "Register to Travelzunlimited Agent Portal.pdf" + "', '_blank', 'height=1000,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
         //   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            dowloadfile("Register to Travelzunlimited Agent Portal.pdf");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            dowloadfile("Create_sub_accounts.pdf");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            dowloadfile("Create_FIT_Package.pdf");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            dowloadfile("Update_FIT_Package.pdf");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            dowloadfile("Book_FIT_Package.pdf");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            dowloadfile("Reconfirm_FIT_Package_using_Credit_Limit.pdf");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            dowloadfile("Reconfirm_FIT_Package_using_Cash_on_Arrival.pdf");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            dowloadfile("Reconfirm_FIT_Package_using_Credit_Card.pdf");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            dowloadfile("Ledger_Report.pdf");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            dowloadfile("Credit_Limit_Usage_Report.pdf");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            dowloadfile("Cancel_FIT_Booking.pdf");
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            dowloadfile("Download_Invoice.pdf");
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            dowloadfile("Download_Service_Vouchers.pdf");
        }

        public void dowloadfile(string filenem)
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" +filenem );


            Response.TransmitFile(Server.MapPath("~/Views/Settings/UserManualPDF/" + filenem));

            Response.End();
        }
    }
}