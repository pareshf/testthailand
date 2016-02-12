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
    //hardik soni 
    public partial class HotelPhoto : System.Web.UI.Page
    {
        CRM.DataAccess.AdministratorEntity.HotelMaster objhotelphoto = new CRM.DataAccess.AdministratorEntity.HotelMaster();
        int Hotelphotoid = 0;
        int Hotelphotoid1 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Hotelphotoid = int.Parse(Page.Request.QueryString["key"].ToString());
            Hotelphotoid1 =int.Parse( Page.Request.QueryString["key1"].ToString());
            
            CRM.DataAccess.AdministratorEntity.HotelMaster objhotelphoto = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            DataSet ds = objhotelphoto.getphotoDetail(Hotelphotoid,Hotelphotoid1);
           photoHotel.HRef = "~/Hotelphoto/" + Hotelphotoid1 + "/" + ds.Tables[0].Rows[0]["PHOTO"].ToString();

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Hotelphoto/" + Hotelphotoid1.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Hotelphoto/" + Hotelphotoid1.ToString() + "/"));

            if (Hotelphoto.HasFile)
            {
                Hotelphoto.SaveAs(Server.MapPath("~/Hotelphoto/" + Hotelphotoid1.ToString() + "/") + Hotelphotoid1.ToString() + Hotelphoto.FileName);
                objhotelphoto.insertHotelPhoto(Hotelphotoid, Hotelphotoid1.ToString() + Hotelphoto.FileName);

            }
        }
    }
}