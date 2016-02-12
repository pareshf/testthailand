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
    /// Summary description for SiteSeeingPriceListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class SiteSeeingPriceListWebService : System.Web.Services.WebService
    {


       
        IQueryable<VIEW_SIGHT_SEEING_PRICE_LIST_MASTER_LATEST> SITE = new NewSiteSeeingPriceListDataContext().VIEW_SIGHT_SEEING_PRICE_LIST_MASTER_LATESTs.AsQueryable<VIEW_SIGHT_SEEING_PRICE_LIST_MASTER_LATEST>();
       IQueryable<VIEW_SIGHT_NOT_OPERATED_DATE> DATE = new NewSiteSeeingPriceListDataContext().VIEW_SIGHT_NOT_OPERATED_DATEs.AsQueryable<VIEW_SIGHT_NOT_OPERATED_DATE>();
       IQueryable<VIEW_SITE_SEEING_NOT_OPERATED_DAY> DAY = new NewSiteSeeingPriceListDataContext().VIEW_SITE_SEEING_NOT_OPERATED_DAYs.AsQueryable<VIEW_SITE_SEEING_NOT_OPERATED_DAY>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SIGHT_SEEING_PRICE_LIST_MASTER_LATEST> GetSitePrice(int startIndex, int maximumRows, string sortExpression, string filterExpression, string sfname, string scity)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                SITE = SITE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                SITE = SITE.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                NewSiteSeeingPriceListDataContext db = new NewSiteSeeingPriceListDataContext();
                //SITE = db.VIEW_FOR_SITE_SEEING_PRICE_LISTs.Where(p => (p.PACKAGE_NAME.Contains(sfname)));
                SITE = SITE.Where(p => (p.PACKAGE_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                NewSiteSeeingPriceListDataContext db = new NewSiteSeeingPriceListDataContext();
                SITE = SITE.Where(p => (p.CITY_NAME.Contains(scity)));

            }
           
            else
            {
                SITE = SITE.OrderBy("SIGHT_SEEING_PRICE_ID ASC");
            }

            return SITE.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetSitePriceCount()
        {
            return (int)SITE.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetdayCount()
        {
            return (int)DAY.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetdateCount()
        {
            return (int)DATE.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSitePrice(ArrayList SitePrice)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objinsertsiteprice = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            SitePrice.Insert(34, Session["usersid"].ToString());
            objinsertsiteprice.InsertUpdateSitePrice(SitePrice);
            //System.Data.DataSet ds1 = objinsertsiteprice.CheckValidation();

            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int m = 0;
            //int n = 0;
            ////int O = 0;
            //int p = 0;
            //int q = 0;
            //int r = 0;
            //int s1 = 0;
            //int s2 = 0;
            //int s3 = 0;
            
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(SitePrice[7]) || SitePrice[7].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(SitePrice[14]) || SitePrice[14].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(SitePrice[9]) || SitePrice[9].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[3].Rows.Count; i++)
            //{

            //    if (ds1.Tables[3].Rows[i]["STATUS"].ToString().Equals(SitePrice[22]) || SitePrice[22].Equals(""))
            //    {
            //        m = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(SitePrice[11]) || SitePrice[11].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            ////for (int i = 0; i < ds1.Tables[7].Rows.Count; i++)
            ////{

            ////    if (ds1.Tables[7].Rows[i]["FIT_PACKAGE_NAME"].ToString().Equals(SitePrice[12]) || SitePrice[12].Equals(""))
            ////    {
            ////        O = 1;
            ////        break;

            ////    }

            ////}
            //for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
            //{

            //    if (ds1.Tables[8].Rows[i]["CITY_NAME"].ToString().Equals(SitePrice[8]) || SitePrice[8].Equals(""))
            //    {
            //        p = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[9].Rows.Count; i++)
            //{

            //    if (ds1.Tables[9].Rows[i]["MEAL_TYPE"].ToString().Equals(SitePrice[21]) || SitePrice[21].Equals(""))
            //    {
            //        q = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[10].Rows.Count; i++)
            //{

            //    if (ds1.Tables[10].Rows[i]["SITE_NAME"].ToString().Equals(SitePrice[10]) || SitePrice[10].Equals(""))
            //    {
            //        r = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[11].Rows.Count; i++)
            //{

            //    if (ds1.Tables[11].Rows[i]["NAME"].ToString().Equals(SitePrice[13]) || SitePrice[13].Equals(""))
            //    {
            //        s1 = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(SitePrice[4]) || SitePrice[4].Equals(""))
            //    {
            //        s2 = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[12].Rows.Count; i++)
            //{

            //    if (ds1.Tables[12].Rows[i]["TIME"].ToString().Equals(SitePrice[1]) || SitePrice[1].Equals(""))
            //    {
            //        s3 = 1;
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
            //else if (j == 1 && k == 1 && l == 1 && m == 0)
            //{
            //    s = "status";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 0)
            //{
            //    s = "agent";
            //}
            ////else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O == 0)
            ////{
            ////    s = "Package";
            ////}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1  && p==0)
            //{
            //    s = "City";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 &&  p == 1 && q==0)
            //{
            //    s = "MealType";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && p == 1 && q == 1 && r==0)
            //{
            //    s = "SiteName";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 &&  p == 1 && q == 1 && r == 1 && s1==0)
            //{
            //    s = "yes_no";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && p == 1 && q == 1 && r == 1 && s1 == 1 && s2==0)
            //{
            //    s = "Restaurant";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 &&  p == 1 && q == 1 && r == 1 && s1 == 1 && s2 == 1 && s3==0)
            //{
            //    s = "Time";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && p == 1 && q == 1 && r == 1 && s1 == 1 && s2 == 1 && s3==1)
            //{

            //    System.Data.DataSet ds = objinsertsiteprice.InsertUpdateSitePrice(SitePrice);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();

            //}
            //else
            //{

            //}
            //return s;

        }
        [WebMethod(EnableSession = true)]
        public void deleteSitePrice(int delsiteprice)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objdelsiteprice = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objdelsiteprice.deleteSitePrice(delsiteprice);
        }
        [WebMethod(EnableSession = true)]
        public void CopyData(int siteid)
        {


            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objempentity = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            //System.IO.File.Copy
            System.Data.DataSet ds = objempentity.CopyData(siteid);
            // string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();


        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateday(ArrayList Day)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objinsertdays = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objinsertdays.InsertUpdateSightDay(Day);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdatedate(ArrayList date)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objinsertdate = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objinsertdate.InsertUpdateSightDate(date);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SIGHT_NOT_OPERATED_DATE> Getdate(string SIGHT_SEEING_PRICE_ID)
        {
            DATE = DATE.Where(String.Format(@"SIGHT_SEEING_PRICE_ID == {0}", SIGHT_SEEING_PRICE_ID));
            return DATE.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SITE_SEEING_NOT_OPERATED_DAY> Getday(string SIGHT_SEEING_PRICE_ID)
        {
            DAY = DAY.Where(String.Format(@"SIGHT_SEEING_PRICE_ID == {0}", SIGHT_SEEING_PRICE_ID));
            return DAY.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewDay(string SIGHT_SEEING_PRICE_ID)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objinsertNewDay = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objinsertNewDay.InsertNewdays(SIGHT_SEEING_PRICE_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewDates(string SIGHT_SEEING_PRICE_ID)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objinsertNewDate = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objinsertNewDate.InsertNewdate(SIGHT_SEEING_PRICE_ID);
        }
        [WebMethod(EnableSession = true)]
        public void deleteSiteDay(int delsiteprice)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objdelday = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objdelday.deleteday(delsiteprice);
        }
        [WebMethod(EnableSession = true)]
        public void deleteSiteDate(int delsiteprice)
        {
            CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure objdeldate = new CRM.DataAccess.AdministratorEntity.SiteSeeingPriceStoredProcedure();
            objdeldate.deletedate(delsiteprice);
        }
    }
}
