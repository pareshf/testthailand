using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using System.Data;

namespace CRM.WebApp.Views.FIT
{
    public partial class Adminsightseenrate : System.Web.UI.Page
    {
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                int ID = Convert.ToInt32(Request.QueryString["id"]);
                Bindrepeater(ID);
            }
        }
        private void Bindrepeater(int ID)
        {


            DataSet dshotelprice = objHotelStoreProcedure.getsightseen("SITE_SEEING_PRICE_LIST", ID);
            rptsightseenpricelist.DataSource = dshotelprice;
            rptsightseenpricelist.DataBind();

        }

    }
}