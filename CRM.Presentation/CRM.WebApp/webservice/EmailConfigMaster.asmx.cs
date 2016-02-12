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
    /// Summary description for EmailConfigMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmailConfigMaster : System.Web.Services.WebService
    {

        IQueryable<VIEW_EMAIL_CONFIG_MASTER> Config = new EmailConfigMasterDataContext().VIEW_EMAIL_CONFIG_MASTERs.AsQueryable<VIEW_EMAIL_CONFIG_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_EMAIL_CONFIG_MASTER> GetEmailConfig(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Config = Config.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Config = Config.OrderBy(sortExpression);
            }
            else
            {
                Config = Config.OrderBy("EMAIL_CONFIG_ID ASC");
            }

            return Config.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetEmailConfigCount()
        {
            return (int)Config.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateEmailConfig(ArrayList Config)
        {

            CRM.DataAccess.AdministratorEntity.EmailConfigStoredProcedure objemailConfig = new CRM.DataAccess.AdministratorEntity.EmailConfigStoredProcedure();
            Config.Insert(5, Session["usersid"].ToString());
            objemailConfig.InsertUpdateEmailConfig(Config);
        }
        [WebMethod(EnableSession = true)]
        public void delEmailConfig(int delConfig)
        {
            CRM.DataAccess.AdministratorEntity.EmailConfigStoredProcedure objdelConfig = new CRM.DataAccess.AdministratorEntity.EmailConfigStoredProcedure();
            objdelConfig.delEmailConfig(delConfig);
        }
    }
}
