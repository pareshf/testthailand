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
    /// Summary description for SupplierCruisePriceWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class SupplierCruisePriceWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_SUPPLIER_CRUISE_PRICE_LIST> crusie =new CruisePriceSupplierDataContext().VIEW_SUPPLIER_CRUISE_PRICE_LISTs.AsQueryable<VIEW_SUPPLIER_CRUISE_PRICE_LIST>();
        IQueryable<VIEW_SUPPLIER_CRUISE_CABINE_INVENTORY> Cabin = new CruisePriceSupplierDataContext().VIEW_SUPPLIER_CRUISE_CABINE_INVENTORies.AsQueryable<VIEW_SUPPLIER_CRUISE_CABINE_INVENTORY>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_CRUISE_PRICE_LIST> GetCrusiePriceList(int startIndex, int maximumRows, string sortExpression, string filterExpression,string sfname)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                crusie = crusie.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                crusie = crusie.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                CruisePriceSupplierDataContext db = new CruisePriceSupplierDataContext();
                crusie = db.VIEW_SUPPLIER_CRUISE_PRICE_LISTs.Where(p => (p.SUPPLIER_NAME.Contains(sfname)));
            }
            else
            {
                crusie = crusie.OrderBy("SUPPLIER_CRUISE_PRICE_LIST_ID ASC");
            }

            return crusie.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCrusieCount()
        {
            return (int)crusie.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetCrusieCabin()
        {
            return (int)Cabin.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruisePrice(ArrayList CruisePrice)
        {

            SupplierCruisePriceStoredProcedure objInsertCruisePrice = new SupplierCruisePriceStoredProcedure();
            CruisePrice.Insert(24, Session["usersid"].ToString());
            objInsertCruisePrice.InsertUpdateCruisePrice(CruisePrice);
            //System.Data.DataSet ds1 = objInsertCruisePrice.CheckValidation();

            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int n = 0;
            //int q = 0;


            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(CruisePrice[12]) || CruisePrice[12].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(CruisePrice[16]) || CruisePrice[16].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(CruisePrice[13]) || CruisePrice[13].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(CruisePrice[15]) || CruisePrice[15].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            
            //for (int i = 0; i < ds1.Tables[20].Rows.Count; i++)
            //{

            //    if (ds1.Tables[20].Rows[i]["CATEGORY_CODE"].ToString().Equals(CruisePrice[3]) || CruisePrice[3].Equals(""))
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
            //    s = "Cruise_Company";
            //}
            //else if (j == 1 && k == 1 && l == 0)
            //{
            //    s = "Payment";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 0)
            //{
            //    s = "agent";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && q == 0)
            //{
            //    s = "Cabin_category";
            //}
            //else if (j == 1 && k == 1 && l == 1 && n == 1 && q == 1)
            //{
            //    System.Data.DataSet ds=objInsertCruisePrice.InsertUpdateCruisePrice(CruisePrice);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();
            //}
            //else
            //{

            //}
            //return s;
            

        }
        [WebMethod(EnableSession = true)]
        public void delCruisePrice(int cruisepriceid)
        {
            SupplierCruisePriceStoredProcedure objdelcrusieprice = new SupplierCruisePriceStoredProcedure();
            objdelcrusieprice.delCruisePrice(cruisepriceid);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_CRUISE_CABINE_INVENTORY> GetCruiseCabin(string SUPPLIER_CRUISE_PRICE_LIST_ID)
        {
            Cabin = Cabin.Where(String.Format(@"SUPPLIER_CRUISE_PRICE_LIST_ID == {0}", SUPPLIER_CRUISE_PRICE_LIST_ID));
            return Cabin.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruiseCabinCategory(ArrayList Inventory)
        {

            SupplierCruisePriceStoredProcedure objInsertCruiseCabinInventory = new SupplierCruisePriceStoredProcedure();
            objInsertCruiseCabinInventory.InsertUpdateCruiseCabinCategory(Inventory);

        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int Supplierid)
        {


            SupplierCruisePriceStoredProcedure objCruise = new SupplierCruisePriceStoredProcedure();
            System.Data.DataSet ds = objCruise.CopyData(Supplierid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();


        }
    }
}
