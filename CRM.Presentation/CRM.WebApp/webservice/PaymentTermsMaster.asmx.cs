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
    /// Summary description for PaymentTermsMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PaymentTermsMaster : System.Web.Services.WebService
    {

        
        IQueryable<VIEW_PAYMENT_TERMS_MASTER> payment = new PaymentTermMasterDataContext().VIEW_PAYMENT_TERMS_MASTERs.AsQueryable<VIEW_PAYMENT_TERMS_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_PAYMENT_TERMS_MASTER> GetPaymentterms(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                payment = payment.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                payment = payment.OrderBy(sortExpression);
            }
            else
            {
                payment = payment.OrderBy("PAYMENT_TERMS_ID ASC");
            }

            return payment.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetPaymentTermsCount()
        {
            return (int)payment.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deletePaymentTerms(int PAYMENTTERMSID)
        {
            CRM.DataAccess.AdministratorEntity.PaymentTermsMaster objdelpayment = new DataAccess.AdministratorEntity.PaymentTermsMaster();
            objdelpayment.deletePaymentTerms(PAYMENTTERMSID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdatePayment(ArrayList Payment)
        {
            CRM.DataAccess.AdministratorEntity.PaymentTermsMaster objinsertpayment = new DataAccess.AdministratorEntity.PaymentTermsMaster();
            
            objinsertpayment.InsertUpdatePaymentMaster(Payment);
        }
    }
}
