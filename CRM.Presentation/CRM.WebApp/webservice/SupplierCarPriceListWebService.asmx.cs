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
    /// Summary description for SupplierCarPriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierCarPriceListWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_SUPPLIER_CAR_PRICE_LIST> Car = new CarPriceListDataContext().VIEW_SUPPLIER_CAR_PRICE_LISTs.AsQueryable<VIEW_SUPPLIER_CAR_PRICE_LIST>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_CAR_PRICE_LIST> GetCarPriceList(int startIndex, int maximumRows, string sortExpression, string filterExpression,string sfname ,string scity)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Car = Car.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Car = Car.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                CarPriceListDataContext db = new CarPriceListDataContext();
                Car = db.VIEW_SUPPLIER_CAR_PRICE_LISTs.Where(p => (p.SUPPLIER_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                CarPriceListDataContext db = new CarPriceListDataContext();
                Car = db.VIEW_SUPPLIER_CAR_PRICE_LISTs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            else
            {
                Car = Car.OrderBy("CAR_PRICE_LIST_MASTER_ID ASC");
            }

            return Car.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCarPriceCount()
        {
            return (int)Car.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCarPrice(ArrayList CarPrice)
        {

            SupplierCarPriceStoredProcedure objinsertcar = new SupplierCarPriceStoredProcedure();
            CarPrice.Insert(20, Session["usersid"].ToString());
            objinsertcar.InsertUpdateCarPrice(CarPrice);
            //System.Data.DataSet ds1 = objinsertcar.CheckValidation();

            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int n = 0;
            //int p = 0;
            //int q = 0;
            //int r = 0;
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(CarPrice[12]) || CarPrice[12].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(CarPrice[5]) || CarPrice[5].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(CarPrice[13]) || CarPrice[13].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(CarPrice[4]) || CarPrice[4].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
            //{

            //    if (ds1.Tables[8].Rows[i]["CITY_NAME"].ToString().Equals(CarPrice[18]) || CarPrice[18].Equals(""))
            //    {
            //        p = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[17].Rows.Count; i++)
            //{

            //    if (ds1.Tables[17].Rows[i]["CAR_NAME"].ToString().Equals(CarPrice[3]) || CarPrice[3].Equals(""))
            //    {
            //        q = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[18].Rows.Count; i++)
            //{

            //    if (ds1.Tables[18].Rows[i]["RATE_UNIT_NAME"].ToString().Equals(CarPrice[19]) || CarPrice[19].Equals(""))
            //    {
            //        r = 1;
            //        break;

            //    }

            //}

            //if (j == 0)
            //{
            //    s = "currency";
            //}
            //else if (j == 1 && k == 0)
            //{
            //    s = "Car_Company";
            //}
            //else if (j == 1 && k == 1 && l == 0)
            //{
            //    s = "Payment";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 0)
            //{
            //    s = "agent";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && p == 0)
            //{
            //    s = "City";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && p == 1 && q == 0)
            //{
            //    s = "Car";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1  && p == 1 && q == 1 && r == 0)
            //{
            //    s = "Rate_Unit";
            //}
            //else if (j == 1 && k == 1 && l == 1  && n == 1 && p == 1 && q == 1 && r == 1)
            //{

            //   System.Data.DataSet ds=objinsertcar.InsertUpdateCarPrice(CarPrice);
            //   s = ds.Tables[0].Rows[0]["ABC"].ToString();

            //}
            //else
            //{

            //}
            //return s;
            

        }
        [WebMethod(EnableSession = true)]
        public void delCarPrice(int Carpriceid)
        {
            SupplierCarPriceStoredProcedure objdelcarprice = new SupplierCarPriceStoredProcedure();
            objdelcarprice.delCarPrice(Carpriceid);
        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {


            SupplierCarPriceStoredProcedure objcarprice = new SupplierCarPriceStoredProcedure();
            //System.IO.File.Copy
            System.Data.DataSet ds = objcarprice.CopyData(Supplierid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();


        }
    }
}
