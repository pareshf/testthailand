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
    /// Summary description for CustomerNextTravelPlanWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerNextTravelPlanWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW> NEXTPLAN = new TravelPlanDataContext().VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEWs.AsQueryable<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW>();
        IQueryable<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US> HISTORY = new TravelPlanDataContext().VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US.AsQueryable<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US>();
        IQueryable<VIEW_CUSTOMER_TRAVEL_WITH_OTHER> OTHER = new TravelPlanDataContext().VIEW_CUSTOMER_TRAVEL_WITH_OTHERs.AsQueryable<VIEW_CUSTOMER_TRAVEL_WITH_OTHER>();
        IQueryable<VIEW_AIRLINE_DETAIL> AIRLINE = new TravelPlanDataContext().VIEW_AIRLINE_DETAILs.AsQueryable<VIEW_AIRLINE_DETAIL>();
        IQueryable<VIEW_CUSTOMER_VISA_DETAIL> VISA = new TravelPlanDataContext().VIEW_CUSTOMER_VISA_DETAILs.AsQueryable<VIEW_CUSTOMER_VISA_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW> GetNextTravelPlan(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                NEXTPLAN = NEXTPLAN.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                NEXTPLAN = NEXTPLAN.OrderBy(sortExpression);
            }
            else
            {
                NEXTPLAN = NEXTPLAN.OrderBy("SR_NO ASC");
            }

            return NEXTPLAN.Skip(startIndex).Take(maximumRows).ToList();
        }
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW> GetNextTravelPlanwithcustid(string CUST_ID)
        {
            NEXTPLAN = NEXTPLAN.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return NEXTPLAN.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US> GetTravelHistorywithcustid(string CUST_ID)
        {
            HISTORY = HISTORY.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return HISTORY.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_TRAVEL_WITH_OTHER> GetTravelWithOtherwithcustid(string CUST_ID)
        {
            OTHER = OTHER.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return OTHER.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_AIRLINE_DETAIL> GetAirlineDetailwithcustid(string CUST_ID)
        {
            AIRLINE = AIRLINE.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return AIRLINE.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_VISA_DETAIL> GetCustomerVisawithcustid(string CUST_ID)
        {
            VISA = VISA.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return VISA.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewTravelPlan(string CUST_ID)
        {
            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objnewtravelid = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            objnewtravelid.InsertNewTravelPlansp(CUST_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTravelPlan2(string CUST_ID)
        {
            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objnewtravelid2 = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            objnewtravelid2.InsertNewTravelPlan2(CUST_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTravelPlan3(string CUST_ID)
        {
            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objnewtravelid3 = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            objnewtravelid3.InsertNewTravelPlan3(CUST_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTravelPlan4(string CUST_ID)
        {
            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objnewtravelid4 = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            objnewtravelid4.InsertNewTravelPlan4(CUST_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTravelPlan5(string CUST_ID)
        {
            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objnewtravelid5 = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            objnewtravelid5.InsertNewTravelPlan5(CUST_ID);
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US> GetTravelHistory(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                HISTORY = HISTORY.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                HISTORY = HISTORY.OrderBy(sortExpression);
            }
            else
            {
                HISTORY = HISTORY.OrderBy("SR_NO ASC");
            }

            return HISTORY.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_TRAVEL_WITH_OTHER> GetTravelWithOther(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                OTHER = OTHER.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                OTHER = OTHER.OrderBy(sortExpression);
            }
            else
            {
                OTHER = OTHER.OrderBy("SR_NO ASC");
            }

            return OTHER.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_AIRLINE_DETAIL> GetAirlineDetail(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                AIRLINE = AIRLINE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                AIRLINE = AIRLINE.OrderBy(sortExpression);
            }
            else
            {
                AIRLINE = AIRLINE.OrderBy("SR_NO ASC");
            }

            return AIRLINE.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUSTOMER_VISA_DETAIL> GetCustomerVisa(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                VISA = VISA.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                VISA = VISA.OrderBy(sortExpression);
            }
            else
            {
                VISA = VISA.OrderBy("SR_NO ASC");
            }

            return VISA.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int TravelPlanCount()
        {
            return (int)NEXTPLAN.Count();
        }
        [WebMethod(EnableSession = true)]
        public int CustomerVisaCount()
        {
            return (int)VISA.Count();
        }
        [WebMethod(EnableSession = true)]
        public int TravelHistoryCount()
        {
            return (int)HISTORY.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetAirlineCount()
        {
            return (int)AIRLINE.Count();
        }
        [WebMethod(EnableSession = true)]
        public int TravelWithOtherCount()
        {
            return (int)OTHER.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTravelPlan(ArrayList TravelPlan)
        {

            NextTravelPlanStoredProcedure objnexttravelplan = new NextTravelPlanStoredProcedure();
            TravelPlan.Insert(8, Session["usersid"].ToString());
            objnexttravelplan.InsertUpdateTravelPlan(TravelPlan);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTravelHistory(ArrayList History)
        {

            CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure objtravelHistory = new CRM.DataAccess.AdministratorEntity.NextTravelPlanStoredProcedure();
            History.Insert(6, Session["usersid"].ToString());
            objtravelHistory.InsertUpdateTravelHistory1(History);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTravelWithOther(ArrayList Other)
        {

            NextTravelPlanStoredProcedure objtravelwithother = new NextTravelPlanStoredProcedure();
            Other.Insert(8, Session["usersid"].ToString());
            objtravelwithother.InsertUpdateTravelWithOther(Other);
        }
        [WebMethod(EnableSession = true)]
        public void deleteNextTravelPlan(int NextTravelPlan)
        {
            NextTravelPlanStoredProcedure objdeltravelplan = new NextTravelPlanStoredProcedure();
            objdeltravelplan.deleteNextTravelPlan(NextTravelPlan);
        }
        [WebMethod(EnableSession = true)]
        public void deleteTravelHistory(int TravelHistory)
        {
            NextTravelPlanStoredProcedure objdeltravelHistory = new NextTravelPlanStoredProcedure();
            objdeltravelHistory.deleteTravelHistory(TravelHistory);
        }
        [WebMethod(EnableSession = true)]
        public void deleteTravelHistoryWithOther(int Historywithother)
        {
            NextTravelPlanStoredProcedure objdeltravelHistoryOther = new NextTravelPlanStoredProcedure();
            objdeltravelHistoryOther.deleteTravelHistoryWithOther(Historywithother);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateAirLineDetail(ArrayList Airline)
        {
            NextTravelPlanStoredProcedure objinsertairline = new NextTravelPlanStoredProcedure();
            objinsertairline.InsertUpdateAirLineDetail(Airline);
            
        }
        [WebMethod(EnableSession = true)]
        public void deleteAirlinedetail(int delairline)
        {

            NextTravelPlanStoredProcedure objdelairline = new NextTravelPlanStoredProcedure();
            objdelairline.deleteAirlinedetail(delairline);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateVisaDetail(ArrayList Visa)
        {

            NextTravelPlanStoredProcedure objinsertvisa = new NextTravelPlanStoredProcedure();
            objinsertvisa.InsertUpdateVisaDetail(Visa);
        }
        [WebMethod(EnableSession = true)]
        public void deleteVisaDetail(int delvisa)
        {

            NextTravelPlanStoredProcedure objdelvisa = new NextTravelPlanStoredProcedure();
            objdelvisa.deleteVisaDetail(delvisa);
        }
    }
}
