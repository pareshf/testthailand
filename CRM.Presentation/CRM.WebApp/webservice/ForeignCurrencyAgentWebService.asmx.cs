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
    /// Summary description for ForeignCurrencyAgentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ForeignCurrencyAgentWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOREIGN_CURRENCY_AGENT_MASTER> CurrencyAgent = new ForeignCurrencyAgentDataContext().VIEW_FOREIGN_CURRENCY_AGENT_MASTERs.AsQueryable<VIEW_FOREIGN_CURRENCY_AGENT_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOREIGN_CURRENCY_AGENT_MASTER> GetCurrencyAgentName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CurrencyAgent = CurrencyAgent.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CurrencyAgent = CurrencyAgent.OrderBy(sortExpression);
            }
            else
            {
                CurrencyAgent = CurrencyAgent.OrderBy("FOREIGN_CURRENCY_AGENT_ID ASC");
            }

            return CurrencyAgent.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCurrencyAgentCount()
        {
            return (int)CurrencyAgent.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateForeignCurrencyAgent(ArrayList CurrencyAgent)
        {

            ForeignCurrencyAgentStoredProcedure objCurrencyAgent = new ForeignCurrencyAgentStoredProcedure();
            CurrencyAgent.Insert(6, Session["usersid"].ToString());
            objCurrencyAgent.InsertUpdateForeignCurrencyAgent(CurrencyAgent);

        }
        [WebMethod(EnableSession = true)]
        public void deleteForeignCurrencyAgent(int delCurrencyAgent)
        {
            ForeignCurrencyAgentStoredProcedure objdelCurrencyAgent = new ForeignCurrencyAgentStoredProcedure();
            objdelCurrencyAgent.deleteForeignCurrencyAgent(delCurrencyAgent);
        }
    }
}
