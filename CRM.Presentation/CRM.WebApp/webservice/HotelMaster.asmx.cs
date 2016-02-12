using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using System.Data;
using CRM.DataAccess.AdministratorEntity;



namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for HotelMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HotelMaster : System.Web.Services.WebService
    {
        IQueryable<VIEW_FARE_HOTEL_MASTER> Hotel = new HotelMasterDataContext().VIEW_FARE_HOTEL_MASTERs.AsQueryable<VIEW_FARE_HOTEL_MASTER>();
        //IQueryable<VIEW_FARE_HOTEL_CONTACT_DETAIL> HotelContactDetail = new HotelContactDetailDataContext().VIEW_FARE_HOTEL_CONTACT_DETAILs.AsQueryable<VIEW_FARE_HOTEL_CONTACT_DETAIL>();
        IQueryable<VIEW_FARE_CONTACT_DETAIL> HotelContactDetail = new HotelMasterDataContext().VIEW_FARE_CONTACT_DETAILs.AsQueryable<VIEW_FARE_CONTACT_DETAIL>();
        IQueryable<VIEW_FARE_HOTEL_CURRENCY_MASTER> HotelCurrencyPrice= new HotelMasterDataContext().VIEW_FARE_HOTEL_CURRENCY_MASTERs.AsQueryable<VIEW_FARE_HOTEL_CURRENCY_MASTER>();
        IQueryable<VIEW_FARE_HOTEL_SERVICE_TYPE_MASTER> HotelServiceType = new HotelMasterDataContext().VIEW_FARE_HOTEL_SERVICE_TYPE_MASTERs.AsQueryable<VIEW_FARE_HOTEL_SERVICE_TYPE_MASTER>();
        IQueryable<VIEW_FARE_HOTEL_PHOTO> HotelPhoto = new HotelMasterDataContext().VIEW_FARE_HOTEL_PHOTOs.AsQueryable<VIEW_FARE_HOTEL_PHOTO>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_HOTEL_MASTER> getHotelDetail(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
           
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Hotel = Hotel.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Hotel = Hotel.OrderBy(sortExpression);
            }
            else
            {
                Hotel = Hotel.OrderBy("HOTEL_ID");
            }

            return Hotel.Skip(startIndex).Take(maximumRows).ToList();

            // TASK = TASK.Where(String.Format(@"MYTASK_ID == {0}", MYTASK_ID));
            //return TASK.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotel(ArrayList Hotel)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objhotelmaster = new CRM.DataAccess.AdministratorEntity.HotelMaster();
           Hotel.Insert(16, Session["empid"].ToString());

            objhotelmaster.InsertUpdateHoteldetail(Hotel);

        }
        [WebMethod(EnableSession = true)]
        public void Deletehotel(int MyHotelId)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objdeletehotel = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objdeletehotel.DeleteMyHotel(MyHotelId);
        }

        [WebMethod(EnableSession = true)]
        public int GetHotelCount()
        {
            return (int)Hotel.Count();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_HOTEL_CURRENCY_MASTER> GetHotelCurrencyPriceMasterByHotel_ID(int HOTEL_ID)
        {
            HotelCurrencyPrice = HotelCurrencyPrice.Where(String.Format(@"HOTEL_ID == {0}", HOTEL_ID));
            return HotelCurrencyPrice.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_CONTACT_DETAIL> GetHotelContectDetailByHotel_ID(int HOTEL_ID)
        {
            HotelContactDetail = HotelContactDetail.Where(String.Format(@"HOTEL_ID == {0}", HOTEL_ID));
            return HotelContactDetail.ToList();
        }
        
        [WebMethod(EnableSession = true)]
        public ArrayList GetServiceType(string HOTEL_ID)
        {
            HotelServiceType = HotelServiceType.Where(String.Format(@"HOTEL_ID == {0}", HOTEL_ID));
            
            ArrayList a = new ArrayList();
            a.Add(HotelServiceType.ToList());
            return a;
            // debugger
        }
        [WebMethod(EnableSession = true)]
        public int GetPhotoCount()
        {
            return (int)HotelPhoto.Count();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_HOTEL_PHOTO> GetHotelPhotoDetails(string HOTEL_ID)
        {
            HotelPhoto = HotelPhoto.Where(String.Format(@"HOTEL_ID == {0}", HOTEL_ID));
            return HotelPhoto.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotelContactDetail(ArrayList HotelContactDeail)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objhotelmaster = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objhotelmaster.InsertUpdateHotelContactDetail(HotelContactDeail);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotelCurrencyPriceMaster(ArrayList Currency)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objhotelCurrency = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objhotelCurrency.InsertUpdateHotelCurrencyPriceMaster(Currency);

        }
        [WebMethod(EnableSession = true)]
        public void InsertNewRoomType(string HOTEL_ID)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objhotelroomtype = new CRM.DataAccess.AdministratorEntity.HotelMaster ();
            objhotelroomtype.InsertNewRoomType(HOTEL_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSrviceType(ArrayList arr)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objservicetype = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objservicetype.InsertUpdateSrviceType(arr);
        }
        [WebMethod(EnableSession = true)]
        public void insertnewPhoto(string HOTEL_ID)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objnewphoto = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objnewphoto.insertnewPhoto(HOTEL_ID);
        }
        [WebMethod(EnableSession = true)]
        public void insertupdatePhotoDetail(ArrayList Photo)
        {
            CRM.DataAccess.AdministratorEntity.HotelMaster objinsertupdatephoto = new CRM.DataAccess.AdministratorEntity.HotelMaster();
            objinsertupdatephoto.insertupdatePhotoDetail(Photo);
        }

    }

}
