using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.Dashboard;
//using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.WebApp.webservice
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BookingForeignWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_PAYMENT_FOREIGN_SIMPLE> payment = new BookingForeignMoneyTransferAgentDataContext().VIEW_PAYMENT_FOREIGN_SIMPLEs.AsQueryable<VIEW_PAYMENT_FOREIGN_SIMPLE>();
        IQueryable<VIEW_PAYMENT_FOREIGN_AGENT> paymentagent = new BookingForeignMoneyTransferAgentDataContext().VIEW_PAYMENT_FOREIGN_AGENTs.AsQueryable<VIEW_PAYMENT_FOREIGN_AGENT>();
        IQueryable<VIEW_PAYMENT_FOREIGN_REPORT> paymentreport = new BookingForeignMoneyTransferAgentDataContext().VIEW_PAYMENT_FOREIGN_REPORTs.AsQueryable<VIEW_PAYMENT_FOREIGN_REPORT>();
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_PAYMENT_FOREIGN_SIMPLE> GetForeign(int TOUR_ID)
        {
            payment = payment.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return payment.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_PAYMENT_FOREIGN_REPORT> GetForeignreport(int TOUR_ID)
        {
            paymentreport = paymentreport.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return paymentreport.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int GetCount()
        {
            return (int)payment.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetCountreport()
        {
            return (int)paymentreport.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetCountAgent()
        {
            return (int)paymentagent.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateForeignAgent(ArrayList payment)
        {
            BookingForeignStoredProcedure objsp = new BookingForeignStoredProcedure();
            objsp.InsertUpdateForeignAgent(payment);           
        }

       

        [WebMethod(EnableSession = true)]
        public void InsertNewDetail(string TOUR_ID, string recivedby)
        {
            BookingForeignStoredProcedure objnewid = new BookingForeignStoredProcedure();
            objnewid.InsertNewDetail(TOUR_ID,recivedby);
        }

    }
}
