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
    /// Summary description for FromMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FromMasterWebService : System.Web.Services.WebService
    {

        IQueryable<FROM_MASTER_FOR_EMAIL> FROM = new FromMasterDataContext().FROM_MASTER_FOR_EMAILs.AsQueryable<FROM_MASTER_FOR_EMAIL>();

        [WebMethod(EnableSession = true)]
        public List<FROM_MASTER_FOR_EMAIL> GetFromDetail(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                FROM = FROM.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                FROM = FROM.OrderBy(sortExpression);
            }
            else
            {
                FROM = FROM.OrderBy("FORM_ID ASC");
            }

            return FROM.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int FromDetailCount()
        {
            return (int)FROM.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFromMaster(ArrayList From)
        {

            CRM.DataAccess.AdministratorEntity.FromMasterStoredProcedure objfrom = new CRM.DataAccess.AdministratorEntity.FromMasterStoredProcedure();
            From.Insert(3, Session["usersid"].ToString());
            objfrom.InsertUpdateFromMaster(From);
        }
        [WebMethod(EnableSession = true)]
        public void delFromMaster(int deldetail)
        {
            CRM.DataAccess.AdministratorEntity.FromMasterStoredProcedure objdelfrom = new CRM.DataAccess.AdministratorEntity.FromMasterStoredProcedure();
            objdelfrom.deleteFromMaster(deldetail);
        }
    }
}
