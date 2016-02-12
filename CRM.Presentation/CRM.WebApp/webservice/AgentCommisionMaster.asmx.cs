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
    /// Summary description for AgentCommisionMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AgentCommisionMaster : System.Web.Services.WebService
    {
       
        IQueryable<VIEW_AGENT_COMMISION_MASTER> agent = new AgentCommissionMasterDataContext().VIEW_AGENT_COMMISION_MASTERs.AsQueryable<VIEW_AGENT_COMMISION_MASTER>();  

        [WebMethod(EnableSession = true)]
        public List<VIEW_AGENT_COMMISION_MASTER> GetAgentcommision(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                agent = agent.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                agent = agent.OrderBy(sortExpression);
            }
            else
            {
                agent = agent.OrderBy("AGENT_COMMISION_ID ASC");
            }

            return agent.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int AgentcommisionCount()
        {
            return (int)agent.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateAgent(ArrayList agent)
        {

           CRM.DataAccess.AdministratorEntity.AgentCommisionMaster objagent = new CRM.DataAccess.AdministratorEntity.AgentCommisionMaster();
            agent.Insert(5, Session["usersid"].ToString());
           objagent.InsertUpdateAgentcommision(agent);
             
        }
        [WebMethod(EnableSession = true)]
        public void delAgent(int delagent)
        {
            CRM.DataAccess.AdministratorEntity.AgentCommisionMaster objdelagent = new CRM.DataAccess.AdministratorEntity.AgentCommisionMaster();
            objdelagent.delAgentcommision(delagent);
        }
    }
}
