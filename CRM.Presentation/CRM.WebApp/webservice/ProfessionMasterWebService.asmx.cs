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
    /// Summary description for ProfessionMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProfessionMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_COMMON_PROFESSION_MASTER> PROFESSION = new ProfessionMasterDataContext().VIEW_FOR_COMMON_PROFESSION_MASTERs.AsQueryable<VIEW_FOR_COMMON_PROFESSION_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_COMMON_PROFESSION_MASTER> GetProfessionName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                PROFESSION = PROFESSION.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                PROFESSION = PROFESSION.OrderBy(sortExpression);
            }
            else
            {
                PROFESSION = PROFESSION.OrderBy("PROFESSION_ID ASC");
            }

            return PROFESSION.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int ProfessionNameCount()
        {
            return (int)PROFESSION.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateProfessionName(ArrayList Prof)
        {

            ProfessionMasterStoredProcedure objprof = new ProfessionMasterStoredProcedure();
            Prof.Insert(2, Session["usersid"].ToString());
            objprof.InsertUpdateProfessionName(Prof);
        }
        [WebMethod(EnableSession = true)]
        public void delProfessionName(int delProfession)
        {
            ProfessionMasterStoredProcedure objdelprof = new ProfessionMasterStoredProcedure();
            objdelprof.delProfessionName(delProfession);
        }
    }
}
