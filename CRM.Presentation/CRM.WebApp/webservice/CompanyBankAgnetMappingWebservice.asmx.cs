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
    /// Summary description for CompanyBankAgnetMappingWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompanyBankAgnetMappingWebservice : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_COMP_BANK_MAPPING_WITH_AGENT> AGENTBANKMAPPING = new CompanyBankAgentDataContext().VIEW_FOR_COMP_BANK_MAPPING_WITH_AGENTs.AsQueryable<VIEW_FOR_COMP_BANK_MAPPING_WITH_AGENT>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_COMP_BANK_MAPPING_WITH_AGENT> GetCompanyBankAgent(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                AGENTBANKMAPPING = AGENTBANKMAPPING.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                AGENTBANKMAPPING = AGENTBANKMAPPING.OrderBy(sortExpression);
            }
            else
            {
                AGENTBANKMAPPING = AGENTBANKMAPPING.OrderBy("BANK_MAPPING_ID ASC");
            }

            return AGENTBANKMAPPING.Skip(startIndex).Take(maximumRows).ToList();
            

        }
        [WebMethod(EnableSession = true)]
        public int GetAgentBankCount()
        {
            return (int)AGENTBANKMAPPING.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsetUpdateCompanyBankAgent(ArrayList CompanyBankAccount)
        {
            CompanyBankAgentMapping objinsertCompanyBankDetaill = new CompanyBankAgentMapping();
            objinsertCompanyBankDetaill.InsertUpdateCompanyBankAgentMapping(CompanyBankAccount);
        }

    }
}
