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
    /// Summary description for RegionMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RegionMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_FOR_REGION_MASTER> CompanyRegion = new CompanyRegionDataContext().VIEW_FOR_REGION_MASTERs.AsQueryable<VIEW_FOR_REGION_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_REGION_MASTER> GetCompanyRegion(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CompanyRegion = CompanyRegion.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CompanyRegion = CompanyRegion.OrderBy(sortExpression);
            }
            else
            {
                CompanyRegion = CompanyRegion.OrderBy("REGION_ID ASC");
            }

            return CompanyRegion.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCompanyRegionCount()
        {
            return (int)CompanyRegion.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCompanyRegion(ArrayList Cregion)
        {

            CompanyRegionMaster objinsertregion = new CompanyRegionMaster();
            Cregion.Insert(3, Session["usersid"].ToString());
            objinsertregion.InsertUpdateCompanyRegion(Cregion);

        }
        [WebMethod(EnableSession = true)]
        public void deleteCompanyRegion(int region)
        {
            CompanyRegionMaster objdelregion = new CompanyRegionMaster();
            objdelregion.deleteCompanyRegion(region);
        }
    }
}
