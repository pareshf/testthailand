using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebApp.Views.Settings
{
    public partial class SightSeeingManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void dowloadfile(string filenem)
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filenem);


            Response.TransmitFile(Server.MapPath("~/Views/Settings/SightSeeingManualPDF/" + filenem));

            Response.End();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            dowloadfile("BANGKOK_SIGHTSEEING_DESCRIPTION.pdf");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            dowloadfile("PATTAYA_SIGHTSEEING_DESCRIPTION.pdf");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            dowloadfile("PHUKET_SIGHTSEEING_DESCRIPTION.pdf");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            dowloadfile("KRABI_SIGHTSEEING_DESCRIPTION.pdf");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            dowloadfile("SAMUI_SIGHTSEEING_DESCRIPTION.pdf");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            dowloadfile("CHIANGMAI_SIGHTSEEING_DESCRIPTION.pdf");
        }
    }
}