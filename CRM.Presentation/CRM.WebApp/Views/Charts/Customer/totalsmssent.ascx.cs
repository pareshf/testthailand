using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using CRM.DataAccess.Dashboard;

namespace CRM.WebApp.Views.Charts.Customer
{
    public partial class totalsmssent : System.Web.UI.UserControl
    {
        deshboardentity objdeshboardentity = new deshboardentity();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Create new data series and set it's visual attributes
            Series series = new Series("Spline");
            series.ChartType = SeriesChartType.Column;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;

            //general setting

            DataTable dt = objdeshboardentity.Getbirthdatedata("1", "1").Tables[0];
            // Add series into the chart's series collection
            Chart1.Series.Add(series);
            series.XValueMember = "DATE";
            series.YValueMembers = "TOTAL";
            series.IsValueShownAsLabel = true;
            Chart1.ChartAreas[0].AxisY.Maximum = 20;
            Chart1.DataSource = dt;
            Chart1.DataBind();
            //general setting over
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}