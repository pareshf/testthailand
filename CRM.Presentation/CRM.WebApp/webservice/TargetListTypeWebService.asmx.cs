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
    /// Summary description for TargetListTypeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TargetListTypeWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_TARGETLIST_TYPE> TARGETLISTTYPE = new TargetListTypeDataContext().VIEW_TARGETLIST_TYPEs.AsQueryable<VIEW_TARGETLIST_TYPE>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_TARGETLIST_TYPE> GetTargetListType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                TARGETLISTTYPE = TARGETLISTTYPE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                TARGETLISTTYPE = TARGETLISTTYPE.OrderBy(sortExpression);
            }
            else
            {
                TARGETLISTTYPE = TARGETLISTTYPE.OrderBy("CUST_TYPE_ID ASC");
            }

            return TARGETLISTTYPE.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetTargetListTypeCount()
        {
            return (int)TARGETLISTTYPE.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTargetListType(ArrayList ttype)
        {

            TargetListTypeStoredProcedure objinserttype = new TargetListTypeStoredProcedure();
            ttype.Insert(2, Session["usersid"].ToString());
            objinserttype.InsertUpdateTargetListType(ttype);

        }
        [WebMethod(EnableSession = true)]
        public void delTargetListType(int deltype)
        {
            TargetListTypeStoredProcedure objdeltype = new TargetListTypeStoredProcedure();
            objdeltype.delTargetListType(deltype);
        }
    }
}
