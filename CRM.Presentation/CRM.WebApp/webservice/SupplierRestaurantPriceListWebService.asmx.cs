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
    /// Summary description for SupplierRestaurantPriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierRestaurantPriceListWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_RESTAURATNT_PRICE_LIST> RestaurantPrice = new SupplierRestaurantDataContext().VIEW_RESTAURATNT_PRICE_LISTs.AsQueryable<VIEW_RESTAURATNT_PRICE_LIST>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_RESTAURATNT_PRICE_LIST> GetRestaurantPriceList(int startIndex, int maximumRows, string sortExpression, string filterExpression,string sfname,string scity)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                RestaurantPrice = RestaurantPrice.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                RestaurantPrice = RestaurantPrice.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                SupplierRestaurantDataContext db = new SupplierRestaurantDataContext();
                RestaurantPrice = db.VIEW_RESTAURATNT_PRICE_LISTs.Where(p => (p.CHAIN_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                SupplierRestaurantDataContext db = new SupplierRestaurantDataContext();
                RestaurantPrice = db.VIEW_RESTAURATNT_PRICE_LISTs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            else
            {
                RestaurantPrice = RestaurantPrice.OrderBy("SUPPLIER_RESTAURANT_PRICE_LIST_ID ASC");
            }

            return RestaurantPrice.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetRestaurantPriceCount()
        {
            return (int)RestaurantPrice.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateRestaurantPrice(ArrayList RestaurantPrice)
        {

            SupplierRestaurantPriceStoredProcedure objinsertpricelist = new SupplierRestaurantPriceStoredProcedure();
            RestaurantPrice.Insert(32, Session["usersid"].ToString());
            objinsertpricelist.InsertUpdateRestaurantPrice(RestaurantPrice);
            //System.Data.DataSet ds1 = objinsertpricelist.CheckValidation();
            
            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int n = 0;
            //int p = 0;
            //int q = 0;
           
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(RestaurantPrice[15]) || RestaurantPrice[15].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(RestaurantPrice[18]) || RestaurantPrice[18].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(RestaurantPrice[16]) || RestaurantPrice[16].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(RestaurantPrice[17]) || RestaurantPrice[17].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
            //{

            //    if (ds1.Tables[8].Rows[i]["CITY_NAME"].ToString().Equals(RestaurantPrice[22]) || RestaurantPrice[22].Equals(""))
            //    {
            //        p = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[9].Rows.Count; i++)
            //{

            //    if (ds1.Tables[9].Rows[i]["MEAL_TYPE"].ToString().Equals(RestaurantPrice[21]) || RestaurantPrice[21].Equals(""))
            //    {
            //        q = 1;
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
            //    s = "Payment";
            //}
            //else if (j == 1 && k == 1 && l == 1 &&  n == 0)
            //{
            //    s = "agent";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 &&  p == 0)
            //{
            //    s = "City";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && p == 1 && q == 0)
            //{
            //    s = "MealType";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && p == 1 && q == 1)
            //{

            //    System.Data.DataSet ds = objinsertpricelist.InsertUpdateRestaurantPrice(RestaurantPrice);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();
            //}
            //else
            //{

            //}
            //return s;
         
        }
        [WebMethod(EnableSession = true)]
        public void delRestaurantPrice(int delprice)
        {
            SupplierRestaurantPriceStoredProcedure objdelprice = new SupplierRestaurantPriceStoredProcedure();
            objdelprice.delRestaurantPrice(delprice);
        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {


            SupplierRestaurantPriceStoredProcedure objrestuprice = new SupplierRestaurantPriceStoredProcedure();
            //System.IO.File.Copy
            System.Data.DataSet ds = objrestuprice.CopyData(Supplierid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();


        }
    }
}
