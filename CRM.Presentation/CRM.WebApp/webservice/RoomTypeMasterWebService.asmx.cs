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
    /// Summary description for RoomTypeMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RoomTypeMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_HOTEL_ROOM_TYPE_MASTER> ROOM = new HotelRoomTypeDataContext().VIEW_HOTEL_ROOM_TYPE_MASTERs.AsQueryable<VIEW_HOTEL_ROOM_TYPE_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_HOTEL_ROOM_TYPE_MASTER> GetHotelRoomType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                ROOM = ROOM.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                ROOM = ROOM.OrderBy(sortExpression);
            }
            else
            {
                ROOM = ROOM.OrderBy("ROOM_TYPE_ID ASC");
            }

            return ROOM.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetRoomTypeCount()
        {
            return (int)ROOM.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateRoom(ArrayList Room)
        {

            HotelRoomTypeStoredProcedure objroominsert = new HotelRoomTypeStoredProcedure();
            Room.Insert(3, Session["usersid"].ToString());
            objroominsert.InsertUpdateRoom(Room);

        }
        [WebMethod(EnableSession = true)]
        public void deleteRoomType(int delRoom)
        {
            HotelRoomTypeStoredProcedure delroom = new HotelRoomTypeStoredProcedure();
            delroom.deleteRoomType(delRoom);
        }
    }
}
