using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for HotelDashboardMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class HotelDashboardMaster : System.Web.Services.WebService
    {

        IQueryable<VIEW_HOTEL_DASHBOARD_MASTER> hotel = new HotelDashboardDataContext().VIEW_HOTEL_DASHBOARD_MASTERs.AsQueryable<VIEW_HOTEL_DASHBOARD_MASTER>();
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_HOTEL_DASHBOARD_MASTER> GetHotel(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                hotel = hotel.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                hotel = hotel.OrderBy(sortExpression);
            }
            else
            {
                hotel = hotel.OrderBy("HOTEL_DASHBOARD_ID ASC");
            }

            return hotel.Skip(startIndex).Take(maximumRows).ToList();

        }

        [WebMethod(EnableSession = true)]
        public int GetNewsCount()
        {
            return (int)hotel.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdatehotels(ArrayList News)
        {

            CRM.DataAccess.AdministratorEntity.HotelDashboard objinsertNews = new CRM.DataAccess.AdministratorEntity.HotelDashboard();
            objinsertNews.InsertUpdateHotelDashboard(News);

        }

        [WebMethod(EnableSession = true)]
        public void delNews(int newsid)
        {
            CRM.DataAccess.AdministratorEntity.HotelDashboard objhotel = new CRM.DataAccess.AdministratorEntity.HotelDashboard();
            objhotel.delHotelDashboard(newsid);
        }
    }
}
