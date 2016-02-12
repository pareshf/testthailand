using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class DashboardGadgetsBox : System.Web.UI.UserControl
    {

        #region Properties
        /// <summary>
        /// Get Gadgets Box Items
        /// </summary>
        public DataList GadgetsBoxGrid
        {
            get
            {
                return dtlsgadget;
            }
        }

        /// <summary>
        /// Get Update Panel Object
        /// </summary>
       
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();

                DataColumn dc1 = new DataColumn("GADGET_NAME");
                DataColumn dc2 = new DataColumn("GADGET_URL");

                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);

                DataRow dr1 = dt.NewRow();
                dr1["GADGET_NAME"] = "Customer Birthdate";
                dr1["GADGET_URL"] = "~/Views/Charts/Customer/BirthdateCustomer.ascx";
                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2["GADGET_NAME"] = "RegionWise Booking";
                dr2["GADGET_URL"] = "~/Views/Charts/Customer/RegionwiseBooking.ascx";
                dt.Rows.Add(dr2);

                DataRow dr3 = dt.NewRow();
                dr3["GADGET_NAME"] = "Total Inquiry Crated";
                dr3["GADGET_URL"] = "~/Views/Charts/Customer/totalinquirycreated.ascx";
                dt.Rows.Add(dr3);

                DataRow dr4 = dt.NewRow();
                dr4["GADGET_NAME"] = "Inquiry Status";
                dr4["GADGET_URL"] = "~/Views/Charts/Customer/bookingstatus.ascx";
                dt.Rows.Add(dr4);

                DataRow dr5 = dt.NewRow();
                dr5["GADGET_NAME"] = "Total Customer Crated";
                dr5["GADGET_URL"] = "~/Views/Charts/Customer/customercreated.ascx";
                dt.Rows.Add(dr5);

                DataRow dr6 = dt.NewRow();
                dr6["GADGET_NAME"] = "Next FollowupDate";
                dr6["GADGET_URL"] = "~/Views/Charts/Customer/Nextfolloupdate.ascx";
                dt.Rows.Add(dr6);

                            
                DataRow dr8 = dt.NewRow();
                dr8["GADGET_NAME"] = "RegionWise Inquiry";
                dr8["GADGET_URL"] = "~/Views/Charts/Customer/regionwiseinquiry.ascx";
                dt.Rows.Add(dr8);

                DataRow dr9 = dt.NewRow();
                dr9["GADGET_NAME"] = "Total Email Sent";
                dr9["GADGET_URL"] = "~/Views/Charts/Customer/totalemailsent.ascx";
                dt.Rows.Add(dr9);

                DataRow dr10 = dt.NewRow();
                dr10["GADGET_NAME"] = "Total Sms Sent";
                dr10["GADGET_URL"] = "~/Views/Charts/Customer/totalsmssent.ascx";
                dt.Rows.Add(dr10);

                DataRow dr11 = dt.NewRow();
                dr11["GADGET_NAME"] = "Total Ticket Booking";
                dr11["GADGET_URL"] = "~/Views/Charts/Customer/totalticketbooking.ascx";
                dt.Rows.Add(dr11);

                DataRow dr12 = dt.NewRow();
                dr12["GADGET_NAME"] = "Total Tour Booking";
                dr12["GADGET_URL"] = "~/Views/Charts/Customer/totaltourbooking.ascx";
                dt.Rows.Add(dr12);


                DataRow dr13 = dt.NewRow();
                dr13["GADGET_NAME"] = "Inquiry Bounce";
                dr13["GADGET_URL"] = "~/Views/Charts/Customer/InquiryBounce.ascx";
                dt.Rows.Add(dr13);

                DataRow dr14 = dt.NewRow();
                dr14["GADGET_NAME"] = "Customer Bounce";
                dr14["GADGET_URL"] = "~/Views/Charts/Customer/CustomerBounce.ascx";
                dt.Rows.Add(dr14);

                DataRow dr15 = dt.NewRow();
                dr15["GADGET_NAME"] = "My Task";
                dr15["GADGET_URL"] = "~/Views/Charts/Customer/MyTask.ascx";
                dt.Rows.Add(dr15);

                dtlsgadget.DataSource = dt;
                dtlsgadget.DataBind();
            }
        }
    }
}