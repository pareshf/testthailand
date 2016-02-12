using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;
using System.Data;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Administration
{
    public partial class VisaQuoteDocument : System.Web.UI.Page
    {
        VisaQuoteStoredProcedure objvisadoc = new VisaQuoteStoredProcedure();

        int visadocid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            visadocid = int.Parse(Page.Request.QueryString["key"].ToString());
            if (!IsPostBack)
            {
                VisaQuoteStoredProcedure objvisadoc = new VisaQuoteStoredProcedure();
                DataSet ds = objvisadoc.GetdocumentDetail(visadocid);

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    reqdoc.HRef = "~/visadoc/" + visadocid + "/" + ds.Tables[0].Rows[k]["REQ_DOC_PATH"].ToString();
                    questiondoc.HRef = "~/visadoc/" + visadocid + "/" + ds.Tables[0].Rows[k]["QUE_DOC_PATH"].ToString();
                }
            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/visadoc/" + visadocid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/visadoc/" + visadocid.ToString() + "/"));

            if (Require.HasFile)
            {
                Require.SaveAs(Server.MapPath("~/visadoc/" + visadocid.ToString() + "/") + visadocid.ToString() + Require.FileName);
                objvisadoc.insertnewvisadoc(visadocid, visadocid.ToString() + Require.FileName);
            }

            if (Question.HasFile)
            {
                Question.SaveAs(Server.MapPath("~/visadoc/" + visadocid.ToString() + "/") + visadocid.ToString() + Question.FileName);
                objvisadoc.insertnewQuestion(visadocid, visadocid.ToString() + Question.FileName);


            }
        }
    }
}