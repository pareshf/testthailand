using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.Dashboard;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;

namespace CRM.WebApp.webservice
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CruiseMasterWebService : System.Web.Services.WebService
    {
        IQueryable<CRUISE_MASTER> Cruise = new Cruise_MasterDataContext().CRUISE_MASTERs.AsQueryable<CRUISE_MASTER>();
        IQueryable<VIEW_CRUISE_SCHEDULE_MASTER> Schedule = new Cruise_MasterDataContext().VIEW_CRUISE_SCHEDULE_MASTERs.AsQueryable<VIEW_CRUISE_SCHEDULE_MASTER>();
        IQueryable<VIEW_CRUISE_CURRENCY_PRICE_MASTER> price = new Cruise_MasterDataContext().VIEW_CRUISE_CURRENCY_PRICE_MASTERs.AsQueryable<VIEW_CRUISE_CURRENCY_PRICE_MASTER>();
        IQueryable<VIEW_CRUISE_COUNRY_VISA_MASTER> visa = new Cruise_MasterDataContext().VIEW_CRUISE_COUNRY_VISA_MASTERs.AsQueryable<VIEW_CRUISE_COUNRY_VISA_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<CRUISE_MASTER> GetCruise(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Cruise = Cruise.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Cruise = Cruise.OrderBy(sortExpression);
            }
            else
            {
                Cruise = Cruise.OrderBy("CRUISE_COMPANY_ID");
            }

            return Cruise.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruise(ArrayList Cruise)
        {
            CruiseMasterStoreprocedure objcruise = new CruiseMasterStoreprocedure();
            Cruise.Insert(2, Session["usersid"].ToString());
            objcruise.InsertUpdateCruise(Cruise);

        }

        [WebMethod(EnableSession = true)]
        public int GetCruiseCount()
        {
            return (int)Cruise.Count();

        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruiseScheduleDetail(ArrayList Schedule)
        {
            CruiseMasterStoreprocedure objcruiseschedule = new CruiseMasterStoreprocedure();
            objcruiseschedule.InsertUpdateCruiseScheduleDetail(Schedule);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruiseprice(ArrayList Price)
        {
            CruiseMasterStoreprocedure objcruiseprice = new CruiseMasterStoreprocedure();
            objcruiseprice.InsertUpdateCruiseprice(Price);

        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruisevisa(ArrayList Visa)
        {
            CruiseMasterStoreprocedure objcruisevisa = new CruiseMasterStoreprocedure();
            objcruisevisa.InsertUpdateCruisevisa(Visa);

        }

        [WebMethod(EnableSession = true)]
        public int GetCruiseScheduleCount()
        {

            return (int)Schedule.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetCruisePricecmdCount()
        {

            return (int)price.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetCruisePriceCount()
        {

            return (int)price.Count();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_SCHEDULE_MASTER> CruiseScheduleGrid(string CRUISE_COMPANY_ID)
        {
            Schedule = Schedule.Where(String.Format(@"CRUISE_COMPANY_ID == {0}", CRUISE_COMPANY_ID));
            return Schedule.ToList();
        }


        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_CURRENCY_PRICE_MASTER> CruisepriceGrid(string CRUISE_ID)
        {
            price = price.Where(String.Format(@"CRUISE_ID == {0}", CRUISE_ID));
            return price.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_COUNRY_VISA_MASTER> CruisevisaGrid(string CRUISE_ID)
        {
            visa = visa.Where(String.Format(@"CRUISE_ID == {0}", CRUISE_ID));
            return visa.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_SCHEDULE_MASTER> GetCruiseSchedule(string CRUISE_COMPANY_ID)
        {
            Schedule = Schedule.Where(String.Format(@"CRUISE_COMPANY_ID == {0}", CRUISE_COMPANY_ID));
            return Schedule.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewCruise(string CRUISE_COMPANY_ID)
        {
            CruiseMasterStoreprocedure objnewid = new CruiseMasterStoreprocedure();
            objnewid.InsertNewCruise(CRUISE_COMPANY_ID);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewCruiseprice(string CRUISE_ID)
        {
            CruiseMasterStoreprocedure objnewpriceid = new CruiseMasterStoreprocedure();
            objnewpriceid.InsertNewCruiseprice(CRUISE_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewCruisevisa(string CRUISE_ID)
        {
            CruiseMasterStoreprocedure objnewvisa = new CruiseMasterStoreprocedure();
            objnewvisa.InsertNewCruisevisa(CRUISE_ID);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteCruise(int CRUISE_COMPANY_ID)
        {
            CruiseMasterStoreprocedure objdelcruise = new CruiseMasterStoreprocedure();
            objdelcruise.DeleteCruise(CRUISE_COMPANY_ID);
        }
        [WebMethod(EnableSession = true)]
        public void DeleteCruiseSchedule(int CRUISE_ID)
        {
            CruiseMasterStoreprocedure objcrusieShedule = new CruiseMasterStoreprocedure();
            objcrusieShedule.DeleteCruiseSchedule(CRUISE_ID);
        }
    }
}
