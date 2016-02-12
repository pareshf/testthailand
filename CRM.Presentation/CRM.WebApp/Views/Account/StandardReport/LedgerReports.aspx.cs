using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using System.Web.Configuration;
using CRM.DataAccess.Account;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Account.StandardReport
{
    public partial class LedgerReports : System.Web.UI.Page
    {
        string id = "";
        string id1 = "";
        string id2 = "";
        public int ipr = 0;
        GlReports objglcode = new GlReports();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = objglcode.fetchComboData("FETCH_ALL_GL_CODE_FOR_REPORT", "");
                bindComboBox(drpBoard, ds);

            }
        }

        protected void bindComboBox(RadComboBox r, DataSet d)
        {
            r.Items.Clear();
            r.DataTextField = "AutoSearchResult";
            r.DataSource = d;
            r.DataBind();
            r.Items.Insert(0, new RadComboBoxItem("", "0"));
            r.SelectedValue = "0";
        }

        protected void drpBoard_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataSet ds = objglcode.fetchComboData("FETCH_ALL_GL_CODE_FOR_REPORT", e.Text);

            DataTable dt = ds.Tables[0];
            onitemrequest(drpBoard, dt, e.NumberOfItems);


        }
        protected void onitemrequest(RadComboBox r, DataTable dt, int noofitems)
        {

            ipr = dt.Rows.Count;
            int io = noofitems;
            int eo = Math.Min(io + ipr, dt.Rows.Count);
            r.Items.Clear();
            for (int i = io; i < eo; i++)
            {
                r.Items.Add(new RadComboBoxItem(dt.Rows[i]["AutoSearchResult"].ToString(), dt.Rows[i]["AutoSearchResult"].ToString()));
            }
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {
            
            id = drpBoard.Text;
            id1 = txtfromdate.Text;
            id2 = TextBox1.Text;
            Response.Redirect("~/Views/Account/StandardReport/LedgerViewReport.aspx?key=" + id + "&key1=" + id1 + "&key2=" + id2);
          //  ReportParameter[] parm = new ReportParameter[3];
          //  if (id != "")
          //  {
          //      parm[0] = new ReportParameter("GL_CODE", id);
          //  }
          //  else
          //  {
          //      parm[0] = new ReportParameter("GL_CODE", " ");
          //  }
          //  if (id1 == "")
          //  {
          //      parm[1] = new ReportParameter("FROM_DATE", value: null);
          //  }
          //  else
          //  {
          //      DateTime dt = DateTime.ParseExact(id1.ToString(), "dd/MM/yyyy", null);
          //      parm[1] = new ReportParameter("FROM_DATE", dt.ToString());
          //  }

          //  if (id2 == "")
          //  {
          //      parm[2] = new ReportParameter("TO_DATE", value: null);
          //  }
          //  else
          //  {
          //      DateTime dt1 = DateTime.ParseExact(id2.ToString(), "dd/MM/yyyy", null);
          //      parm[2] = new ReportParameter("TO_DATE", dt1.ToString());
          //  }
          //  rptViewer1.Visible = true;
          //  rptViewer1.ShowCredentialPrompts = false;
          //  rptViewer1.ShowParameterPrompts = false;

          //  rptViewer1.ServerReport.ReportServerCredentials = new ReportCredentials(WebConfigurationManager.AppSettings["ReportServerUsername"].ToString(), WebConfigurationManager.AppSettings["ReportServerPassword"].ToString(), WebConfigurationManager.AppSettings["ReportServerDomain"].ToString());

          //  rptViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
          //  rptViewer1.ServerReport.ReportServerUrl = new System.Uri(WebConfigurationManager.AppSettings["ReportServer"].ToString());
          //  rptViewer1.ServerReport.ReportPath = "/ThailandReport/LedgerReport";
          //  rptViewer1.ServerReport.SetParameters(parm);
          //  rptViewer1.ServerReport.Refresh();
            
          //  //renderedBytes = rptViewer1.ServerReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
          //  //Response.Clear();
          //  //Response.ContentType = mimeType;
          //  //Response.AddHeader("content-disposition", "attachment; filename = International Booking Form." + fileNameExtension);
          //  //Response.BinaryWrite(renderedBytes);
          //  if (!rptViewer1.Enabled) { rptViewer1.CurrentPage = 1; } 
          //  upnlReport.Update();
            
          ////  Response.End();
            
        }
    }
}