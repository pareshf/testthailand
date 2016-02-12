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
    public partial class Adminhotelrate : System.Web.UI.Page
    {
        HotelsStoreProcedure objHotelStoreProcedure = new HotelsStoreProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string City = Request.QueryString["city"];
                if (City != "" && City != null)
                {
                    Bindrepeater(City);
                }

                
            }
        }
        private void Bindrepeater(string cityname)
        {

            string Fromdate = Session["fromdate"].ToString();
            string todate = Session["todate"].ToString();
            int Cust_Type = Convert.ToInt32(Session["CustTypeID"]);
            DataSet dshotelprice = objHotelStoreProcedure.fetchComboPriceforHotel("FETCH_HOTEL_WISE_PRICE_LIST", cityname, Fromdate, todate, Cust_Type);
            rpthotelpricelist.DataSource = dshotelprice;
            rpthotelpricelist.DataBind();

        }

    }
}