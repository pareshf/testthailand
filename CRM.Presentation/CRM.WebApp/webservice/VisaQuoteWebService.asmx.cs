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
    /// Summary description for VisaQuoteWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VisaQuoteWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_FARE_VISA_QUOTE_MASTER> VISAQUOTE = new VisaQuoteDataContext().VIEW_FARE_VISA_QUOTE_MASTERs.AsQueryable<VIEW_FARE_VISA_QUOTE_MASTER>();


        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_VISA_QUOTE_MASTER> GetVisaQuote(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                VISAQUOTE = VISAQUOTE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                VISAQUOTE = VISAQUOTE.OrderBy(sortExpression);
            }
            else
            {
                VISAQUOTE = VISAQUOTE.OrderBy("VISA_QUOTE_ID ASC");
            }

            return VISAQUOTE.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetVisaQuoteCount()
        {
            return (int)VISAQUOTE.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateVisaQuote(ArrayList VisaQuote)
        {

            VisaQuoteStoredProcedure objvisaquote = new VisaQuoteStoredProcedure();
            objvisaquote.InsertUpdateVisaQuote(VisaQuote);

        }
        [WebMethod(EnableSession = true)]
        public void deleteVisaQuote(int delvisaquote)
        {
            VisaQuoteStoredProcedure objdelvisaquote = new VisaQuoteStoredProcedure();
            objdelvisaquote.deleteVisaQuote(delvisaquote);
        }
    }
}
