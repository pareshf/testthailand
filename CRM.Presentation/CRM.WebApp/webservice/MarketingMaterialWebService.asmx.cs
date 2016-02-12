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
    /// Summary description for MarketingMaterialWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MarketingMaterialWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_MARKETING_MATERIAL_NEW> MATERIAL = new MarketingMaterialNewDataContext().VIEW_MARKETING_MATERIAL_NEWs.AsQueryable<VIEW_MARKETING_MATERIAL_NEW>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_MARKETING_MATERIAL_NEW> GetMarketingMaterail(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                MATERIAL = MATERIAL.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                MATERIAL = MATERIAL.OrderBy(sortExpression);
            }
            else
            {
                MATERIAL = MATERIAL.OrderBy("MAR_ID ASC");
            }

            return MATERIAL.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetMarketingCount()
        {
            return (int)MATERIAL.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateMarketingMaterial(ArrayList MAR)
        {

            MarketingMaterialStoredProcedure objinsertmar = new MarketingMaterialStoredProcedure();
            objinsertmar.InsertUpdateMarketingMaterial(MAR);

        }
        [WebMethod(EnableSession = true)]
        public void delMarketingMaterial(int delMar)
        {
            MarketingMaterialStoredProcedure objdelmar = new MarketingMaterialStoredProcedure();
            objdelmar.delMarketingMaterial(delMar);
        }
    }
}
