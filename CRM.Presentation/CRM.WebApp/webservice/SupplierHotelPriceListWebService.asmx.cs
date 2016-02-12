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
    /// Summary description for SupplierHotelPriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierHotelPriceListWebService : System.Web.Services.WebService
    {
        

        IQueryable<VIEW_SUPPLIER_HOTEL_PRICE_LIST> HotelPrice = new HotelPriceListDataContext().VIEW_SUPPLIER_HOTEL_PRICE_LISTs.AsQueryable<VIEW_SUPPLIER_HOTEL_PRICE_LIST>();
        IQueryable<VIEW_SUPPLIER_HOTEL_ROOM_INVENTORY> Invrentory = new HotelPriceListDataContext().VIEW_SUPPLIER_HOTEL_ROOM_INVENTORies.AsQueryable<VIEW_SUPPLIER_HOTEL_ROOM_INVENTORY>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_HOTEL_PRICE_LIST> GetHotelPrice(int startIndex, int maximumRows, string sortExpression, string filterExpression,string CHAIN_NAME,string scity)
        {
            
            if (!String.IsNullOrEmpty(CHAIN_NAME))
            {
                HotelPriceListDataContext db = new HotelPriceListDataContext();
               
                HotelPrice = HotelPrice.Where(p => (p.SUPPLIER_NAME.Contains(CHAIN_NAME)));

            }
            if (!String.IsNullOrEmpty(scity))
            {
                HotelPriceListDataContext db = new HotelPriceListDataContext();
                
                HotelPrice = HotelPrice.Where(p => (p.CITY_NAME.Contains(scity)));

            }
            if (!String.IsNullOrEmpty(filterExpression))
            {
                HotelPrice = HotelPrice.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                HotelPrice = HotelPrice.OrderBy(sortExpression);
            }
            else
            {
                HotelPrice = HotelPrice.OrderBy("SUPPLIER_HOTEL_PRICE_LIST_ID ASC");
            }
            return HotelPrice.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetHotelPriceListCount()
        {
            return (int)HotelPrice.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetRoomInventoryCount()
        {
            return (int)Invrentory.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotelPrice(ArrayList HotelPrice)
        {

            SupplierHotelPriceStoredProcedure objinserthotelprice = new SupplierHotelPriceStoredProcedure();
            HotelPrice.Insert(27, Session["usersid"].ToString());
            objinserthotelprice.InsertUpdateHotelPrice(HotelPrice);
            //System.Data.DataSet ds1 = objinserthotelprice.CheckValidation();
            //return s;
            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int m = 0;
            //int n = 0;
            //int O = 0;
            //int p= 0;
            //for (int i=0; i < ds1.Tables[0].Rows.Count;i++ )
            //{
              
            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(HotelPrice[7]) || HotelPrice[7].Equals(""))
            //    {
            //        j = 1;
            //        break;
                    
            //    }
                  
            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(HotelPrice[9]) || HotelPrice[9].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(HotelPrice[8]) || HotelPrice[8].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[3].Rows.Count; i++)
            //{

            //    if (ds1.Tables[3].Rows[i]["STATUS"].ToString().Equals(HotelPrice[19]) || HotelPrice[19].Equals(""))
            //    {
            //        m = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(HotelPrice[10]) || HotelPrice[10].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[5].Rows.Count; i++)
            //{

            //    if (ds1.Tables[5].Rows[i]["ROOM_TYPE_NAME"].ToString().Equals(HotelPrice[1]) || HotelPrice[1].Equals(""))
            //    {
            //         O= 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[6].Rows.Count; i++)
            //{

            //    if (ds1.Tables[6].Rows[i]["TF_TYPE"].ToString().Equals(HotelPrice[16]) || HotelPrice[16].Equals(""))
            //    {
            //        p = 1;
            //        break;

            //    }

            //}
            //if (j == 0)
            //{
            //    s = "currency";
            //}
            //else if (j == 1 && k == 0)
            //{
            //    s = "Restaurant";
            //}
            //else if (j == 1 && k == 1 && l == 0)
            //{
            //    s= "Payment";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 0)
            //{
            //    s= "status";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n==0)
            //{
            //    s = "agent"; 
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O==0)
            //{
            //    s = "Room";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O == 1 && p==0)
            //{
            //    s = "IsQuote";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O==1 && p==1)
            //{
            //    System.Data.DataSet ds = objinserthotelprice.InsertUpdateHotelPrice(HotelPrice);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();
               
            //}
            //else
            //{

            //}
            //return s;
        }
        [WebMethod(EnableSession = true)]
        public void delHotelPrice(int delHotel)
        {
            SupplierHotelPriceStoredProcedure objdelHotelPrice = new SupplierHotelPriceStoredProcedure();
            objdelHotelPrice.delHotelPrice(delHotel);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_HOTEL_ROOM_INVENTORY> GetRoomInventory(string SUPPLIER_HOTEL_PRICE_LIST_ID)
        {
            Invrentory = Invrentory.Where(String.Format(@"SUPPLIER_HOTEL_PRICE_LIST_ID == {0}", SUPPLIER_HOTEL_PRICE_LIST_ID));
            return Invrentory.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotelRoomInventory(ArrayList RoomInventory)
        {

            SupplierHotelPriceStoredProcedure objinserthotelRoomInventory = new SupplierHotelPriceStoredProcedure();
            objinserthotelRoomInventory.InsertUpdateHotelRoomInventory(RoomInventory);

        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {


            SupplierHotelPriceStoredProcedure objempentity = new SupplierHotelPriceStoredProcedure();
            
            System.Data.DataSet ds = objempentity.CopyData(Supplierid);
            
        }
    }
}
