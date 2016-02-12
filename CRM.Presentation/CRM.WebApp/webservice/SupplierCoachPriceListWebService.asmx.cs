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
    /// Summary description for SupplierCoachPriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierCoachPriceListWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_SUPPLIER_COACH_PRICE_LIST> CoachPriceList = new SupplierCoachPriceDataContext().VIEW_SUPPLIER_COACH_PRICE_LISTs.AsQueryable<VIEW_SUPPLIER_COACH_PRICE_LIST>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_COACH_PRICE_LIST> GetCoachPriceList(int startIndex, int maximumRows, string sortExpression, string filterExpression,string sfname,string scity)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CoachPriceList = CoachPriceList.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CoachPriceList = CoachPriceList.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                SupplierCoachPriceDataContext db = new SupplierCoachPriceDataContext();
                CoachPriceList = db.VIEW_SUPPLIER_COACH_PRICE_LISTs.Where(p => (p.SUPPLIER_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                SupplierCoachPriceDataContext db = new SupplierCoachPriceDataContext();
                CoachPriceList = db.VIEW_SUPPLIER_COACH_PRICE_LISTs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            else
            {
                CoachPriceList = CoachPriceList.OrderBy("COACH_PRICE_LIST_ID ASC");
            }

            return CoachPriceList.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCoachCount()
        {
            return (int)CoachPriceList.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCoachPrice(ArrayList CoachPrice)
        {

            SupplierCoachPriceStoredProcedure objinsertcoachprice = new SupplierCoachPriceStoredProcedure();
            CoachPrice.Insert(20, Session["usersid"].ToString());
            objinsertcoachprice.InsertUpdateCoachPrice(CoachPrice);
            //System.Data.DataSet ds1 = objinsertcoachprice.CheckValidation();

            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int n = 0;
            //int p = 0;
            //int q = 0;
            

            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(CoachPrice[12]) || CoachPrice[12].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(CoachPrice[5]) || CoachPrice[5].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(CoachPrice[13]) || CoachPrice[13].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(CoachPrice[4]) || CoachPrice[4].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
            //{

            //    if (ds1.Tables[8].Rows[i]["CITY_NAME"].ToString().Equals(CoachPrice[17]) || CoachPrice[17].Equals(""))
            //    {
            //        p = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[19].Rows.Count; i++)
            //{

            //    if (ds1.Tables[19].Rows[i]["COACH_NAME"].ToString().Equals(CoachPrice[3]) || CoachPrice[3].Equals(""))
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
            //    s = "Coach_Company";
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
            //    s = "Coach";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && p == 1 && q == 1)
            //{

            //    System.Data.DataSet ds = objinsertcoachprice.InsertUpdateCoachPrice(CoachPrice);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();
            //}
            //else
            //{

            //}
            //return s;

            
        }
        [WebMethod(EnableSession = true)]
        public void delCoachPrice(int Coachpriceeid)
        {
            CRM.DataAccess.AdministratorEntity.SupplierCoachPriceStoredProcedure objdelcoachprice = new CRM.DataAccess.AdministratorEntity.SupplierCoachPriceStoredProcedure();
            objdelcoachprice.delCoachPrice(Coachpriceeid);
        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {


            CRM.DataAccess.AdministratorEntity.SupplierCoachPriceStoredProcedure objcoachprice = new CRM.DataAccess.AdministratorEntity.SupplierCoachPriceStoredProcedure();
           
            //System.IO.File.Copy
            System.Data.DataSet ds = objcoachprice.CopyData(Supplierid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();


        }
    }
}
