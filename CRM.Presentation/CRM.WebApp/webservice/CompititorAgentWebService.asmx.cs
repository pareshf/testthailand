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
    /// Summary description for CompititorAgentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompititorAgentWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_COMPITITOR_AGENT> CAGENT = new CompititorAgentNewDataContext().VIEW_COMPITITOR_AGENTs.AsQueryable<VIEW_COMPITITOR_AGENT>();


        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPITITOR_AGENT> GetCompititor(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CAGENT = CAGENT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CAGENT = CAGENT.OrderBy(sortExpression);
            }
            else
            {
                CAGENT = CAGENT.OrderBy("AGENT_ID ASC");
            }

            return CAGENT.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetCompititorCount()
        {
            return (int)CAGENT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCompititorAgent(ArrayList CompititorAgent)
        {

            CompititorAgentStoredProcedure objinsertCompititor = new CompititorAgentStoredProcedure();
            CompititorAgent.Insert(8, Session["usersid"].ToString());
            objinsertCompititor.InsertUpdateCompititorAgent(CompititorAgent);

        }
        [WebMethod(EnableSession = true)]
        public void deleteCompititor(int delCompititor)
        {
            CompititorAgentStoredProcedure objdelcompititor = new CompititorAgentStoredProcedure();
            objdelcompititor.deleteCompititor(delCompititor);
        }
    }
}
