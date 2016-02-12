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

namespace CRM.WebApp.Views.Sales
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SiteSeeingMasterStoredProcedure objsitephoto =new SiteSeeingMasterStoredProcedure();
       
        int sitephotoid = 0;
        int sitephotoid1 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            sitephotoid = int.Parse(Page.Request.QueryString["key"].ToString());
            sitephotoid1 = int.Parse(Page.Request.QueryString["key1"].ToString());
            if (!IsPostBack)
            {
                
                SiteSeeingMasterStoredProcedure objsitephoto = new SiteSeeingMasterStoredProcedure();
                DataSet ds = objsitephoto.getphotoDetail(sitephotoid1, sitephotoid);
                photosite.HRef = "~/sitephoto/" + sitephotoid1 + "/" + ds.Tables[0].Rows[0]["PHOTO_FILE"].ToString();
            }
           
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/sitephoto/" + sitephotoid1.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/sitephoto/" + sitephotoid1.ToString() + "/"));

            if (sitephoto.HasFile)
            {
                sitephoto.SaveAs(Server.MapPath("~/sitephoto/" + sitephotoid1.ToString() + "/") + sitephotoid1.ToString() + sitephoto.FileName);
                objsitephoto.insertSitePhoto(sitephotoid,sitephotoid1.ToString() + sitephoto.FileName);
                
              
            }
        }
        
    }
    
}