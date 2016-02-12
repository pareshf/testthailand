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
    /// Summary description for FareHotelRoomTypeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FareHotelRoomTypeWebService : System.Web.Services.WebService
    {
      //   IQueryable<View_Address_Type_Lookup> AddressType = new Address_Type_LookupDataContext().View_Address_Type_Lookups.AsQueryable<View_Address_Type_Lookup>();
        IQueryable<VIEW_FARE_HOTEL_ROOM_TYPE> HotelType = new FareHotelRoomTypeMasterDataContext().VIEW_FARE_HOTEL_ROOM_TYPEs.AsQueryable<VIEW_FARE_HOTEL_ROOM_TYPE>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_HOTEL_ROOM_TYPE> GetHotelType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                HotelType = HotelType.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                HotelType = HotelType.OrderBy(sortExpression);
            }
            else
            {
                HotelType = HotelType.OrderBy("ROOM_TYPE_ID ASC");
            }

            return HotelType.Skip(startIndex).Take(maximumRows).ToList();


        }
         [WebMethod(EnableSession = true)]
        public int GetHotelTypeCount()
        {
            return (int)HotelType.Count();
        }
        [WebMethod(EnableSession = true)]
         public void InsertUpdateHotelRoom(ArrayList Hotel)
         {

             FareHotelRoomTypeSp objhoteltype = new FareHotelRoomTypeSp();
             Hotel.Insert(3, Session["usersid"].ToString());
             objhoteltype.InsertUpdateHotelRoom(Hotel);

         }
         [WebMethod(EnableSession = true)]
         public void deleteHotelType(int HOTELID)
         {
             FareHotelRoomTypeSp objhotel = new FareHotelRoomTypeSp();
             objhotel.deleteHotelType(HOTELID);
         }
    }
}
