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
    /// Summary description for EventMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EventMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_EVENT_MASTER_FOR_EMAIL> EVENT = new EventMasterDataContext().VIEW_EVENT_MASTER_FOR_EMAILs.AsQueryable<VIEW_EVENT_MASTER_FOR_EMAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_EVENT_MASTER_FOR_EMAIL> GetEvent(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                EVENT = EVENT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                EVENT = EVENT.OrderBy(sortExpression);
            }
            else
            {
                EVENT = EVENT.OrderBy("EVENT_ID ASC");
            }

            return EVENT.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int EventCount()
        {
            return (int)EVENT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateEvent(ArrayList Event)
        {

            CRM.DataAccess.AdministratorEntity.EventStoredProcedure objinsertevent = new CRM.DataAccess.AdministratorEntity.EventStoredProcedure();
            Event.Insert(3, Session["usersid"].ToString());
            objinsertevent.InsertUpdateEventMaster(Event);
        }
        [WebMethod(EnableSession = true)]
        public void delEvent(int delEvent)
        {
            CRM.DataAccess.AdministratorEntity.EventStoredProcedure objdelEvent = new CRM.DataAccess.AdministratorEntity.EventStoredProcedure();
            objdelEvent.delEventMaster(delEvent);
        }
    }
}
