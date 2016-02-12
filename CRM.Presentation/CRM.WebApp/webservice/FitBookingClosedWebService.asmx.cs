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
    /// Summary description for FitBookingClosedWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FitBookingClosedWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FIT_BOOKING_CLOSED> Fitbookingclosed = new FitBookingClosedDataContext().VIEW_FIT_BOOKING_CLOSEDs.AsQueryable<VIEW_FIT_BOOKING_CLOSED>();
        IQueryable<VIEW_FIT_BOOKING_DAY> fitBookingday = new FitBookingClosedDataContext().VIEW_FIT_BOOKING_DAYs.AsQueryable<VIEW_FIT_BOOKING_DAY>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FIT_BOOKING_CLOSED> GetFitClosed(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Fitbookingclosed = Fitbookingclosed.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Fitbookingclosed = Fitbookingclosed.OrderBy(sortExpression);
            }
            else
            {
                Fitbookingclosed = Fitbookingclosed.OrderBy("FIT_BOOKING_CLOSED_ID ASC");
            }

            return Fitbookingclosed.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FIT_BOOKING_DAY> GetFitDay(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                fitBookingday = fitBookingday.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                fitBookingday = fitBookingday.OrderBy(sortExpression);
            }
            else
            {
                fitBookingday = fitBookingday.OrderBy("FIT_BOOKING_DAY_ID ASC");
            }

            return fitBookingday.Skip(startIndex).Take(maximumRows).ToList();

        }

        [WebMethod(EnableSession = true)]
        public int GetFitClosedCount()
        {
            return (int)Fitbookingclosed.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetFitDayCount()
        {
            return (int)fitBookingday.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFitClosed(ArrayList FitClosed)
        {

            CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure objinsertFitClosed = new CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure();
            objinsertFitClosed.InsertUpdateFitBookingClosed(FitClosed);

        }
        [WebMethod(EnableSession = true)]
        public void deleteFitClosed(int defitclosed)
        {
            CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure objdelfitClosed = new CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure();
            objdelfitClosed.deleteFitBookingClosed(defitclosed);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFitDay(ArrayList FitClosed)
        {

            CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure objinsertFitDay = new CRM.DataAccess.AdministratorEntity.FitBookingClosedStoredProcedure();
            objinsertFitDay.InsertUpdateFitDay(FitClosed);

        }
    }
}
