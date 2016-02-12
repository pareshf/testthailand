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
    /// Summary description for SupplierGuidePriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierGuidePriceListWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_SUPPLIER_GUIDE_PRICE_LIST> GuidePrice = new SupplierGuidePriceDataContext().VIEW_SUPPLIER_GUIDE_PRICE_LISTs.AsQueryable<VIEW_SUPPLIER_GUIDE_PRICE_LIST>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_GUIDE_PRICE_LIST> GetGuidePrice(int startIndex, int maximumRows, string sortExpression, string filterExpression,string sfname,string scity)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                GuidePrice = GuidePrice.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                GuidePrice = GuidePrice.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                SupplierGuidePriceDataContext db = new SupplierGuidePriceDataContext();
                GuidePrice = db.VIEW_SUPPLIER_GUIDE_PRICE_LISTs.Where(p => (p.SUPPLIER_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                SupplierGuidePriceDataContext db = new SupplierGuidePriceDataContext();
                GuidePrice = db.VIEW_SUPPLIER_GUIDE_PRICE_LISTs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            else
            {
                GuidePrice = GuidePrice.OrderBy("GUIDE_PRICE_LIST_ID ASC");
            }

            return GuidePrice.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetGuidePriceCount()
        {
            return (int)GuidePrice.Count();
        }
        [WebMethod(EnableSession = true)]
        public string InsertUpdateGuidePrice(ArrayList GuidePrice,string s)
        {

            SupplierGuidePriceStoredProcedure objinsertguideprice = new SupplierGuidePriceStoredProcedure();
            GuidePrice.Insert(17, Session["usersid"].ToString());
            System.Data.DataSet ds1 = objinsertguideprice.CheckValidation();

            int j = 0;
            int k = 0;
            int l = 0;
            int n = 0;
            int q = 0;


            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(GuidePrice[12]) || GuidePrice[12].Equals(""))
                {
                    j = 1;
                    break;

                }

            }
            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(GuidePrice[5]) || GuidePrice[5].Equals(""))
                {
                    k = 1;
                    break;

                }

            }
            for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            {

                if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(GuidePrice[13]) || GuidePrice[13].Equals(""))
                {
                    l = 1;
                    break;

                }

            }
            for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            {

                if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(GuidePrice[4]) || GuidePrice[4].Equals(""))
                {
                    n = 1;
                    break;

                }

            }

            for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
            {

                if (ds1.Tables[8].Rows[i]["CITY_NAME"].ToString().Equals(GuidePrice[14]) || GuidePrice[14].Equals(""))
                {
                    q = 1;
                    break;

                }

            }

            if (j == 0)
            {
                s = "currency";
            }
            else if (j == 1 && k == 0)
            {
                s = "Guide_Company";
            }
            else if (j == 1 && k == 1 && l == 0)
            {
                s = "Payment";
            }
            else if (j == 1 && k == 1 && l == 1 && n == 0)
            {
                s = "agent";
            }
            else if (j == 1 && k == 1 && l == 1 && n == 1 && q == 0)
            {
                s = "City";
            }
            else if (j == 1 && k == 1 && l == 1 && n == 1 && q == 1)
            {
                System.Data.DataSet ds = objinsertguideprice.InsertUpdateGuidePrice(GuidePrice);
                s = ds.Tables[0].Rows[0]["ABC"].ToString();
            }
            else
            {

            }
            return s;
        }
        [WebMethod(EnableSession = true)]
        public void delGuidePrice(int GuidePriceid)
        {
            SupplierGuidePriceStoredProcedure objdelguideprice = new SupplierGuidePriceStoredProcedure();
            objdelguideprice.delGuidePrice(GuidePriceid);
        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {
            SupplierGuidePriceStoredProcedure objguidecopy = new SupplierGuidePriceStoredProcedure();
            System.Data.DataSet ds = objguidecopy.CopyData(Supplierid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();

        }
    }
}
