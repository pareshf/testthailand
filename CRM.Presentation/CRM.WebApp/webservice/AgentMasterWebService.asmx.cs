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
    /// Summary description for AgentMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AgentMaster : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_FARE_AGENT_MASTER> AGENT = new AgentMasterDataContext().VIEW_FOR_FARE_AGENT_MASTERs.AsQueryable<VIEW_FOR_FARE_AGENT_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_FARE_AGENT_MASTER> GetAgentName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                AGENT = AGENT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                AGENT = AGENT.OrderBy(sortExpression);
            }
            else
            {
                AGENT = AGENT.OrderBy("AGENT_ID ASC");
            }

            return AGENT.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int AgentNameCount()
        {
            return (int)AGENT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateAgentName(ArrayList Agent)
        {

            AgentMasterStoredProcedure objagent = new AgentMasterStoredProcedure();
            Agent.Insert(13, Session["usersid"].ToString());
            objagent.InsertUpdateAgentName(Agent);
        }
        [WebMethod(EnableSession = true)]
        public void delAgentName(int delagent)
        {
            AgentMasterStoredProcedure objagentdel = new AgentMasterStoredProcedure();
            objagentdel.delAgentName(delagent);
        }
    }
}
