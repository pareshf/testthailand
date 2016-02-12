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
    /// Summary description for VisaTypeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VisaTypeWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_COMMON_VISA_TYPE> VISA = new VisaTypeMasterDataContext().VIEW_COMMON_VISA_TYPEs.AsQueryable<VIEW_COMMON_VISA_TYPE>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_COMMON_VISA_TYPE> GetVisaType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                VISA = VISA.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                VISA = VISA.OrderBy(sortExpression);
            }
            else
            {
                VISA = VISA.OrderBy("VISA_TYPE_ID ASC");
            }

            return VISA.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetVisaTypeCount()
        {
            return (int)VISA.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateVisaType(ArrayList visatype)
        {
            VisaTypeStoredProcedure objvisatype = new VisaTypeStoredProcedure();
            objvisatype.InsertUpdateVisaType(visatype);
        }
        [WebMethod(EnableSession = true)]
        public void deleteVisaType(int delvisatype)
        {
            VisaTypeStoredProcedure objdelvisatype = new VisaTypeStoredProcedure();
            objdelvisatype.deleteVisaType(delvisatype);
        }
    }
}
