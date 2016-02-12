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
    /// Summary description for PaymentMode
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PaymentMode : System.Web.Services.WebService
    {


        IQueryable<VIEW_PAYMENT_MODE> PMODE = new PaymentModeDataContext().VIEW_PAYMENT_MODEs.AsQueryable<VIEW_PAYMENT_MODE>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_PAYMENT_MODE> GetPaymentModeType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                PMODE = PMODE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                PMODE = PMODE.OrderBy(sortExpression);
            }
            else
            {
                PMODE = PMODE.OrderBy("PAYMENT_MODE_ID ASC");
            }

            return PMODE.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetPMODEcount()
        {
            return (int)PMODE.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdatePayment(ArrayList Pmode)
        {
            
            CRM.DataAccess.AdministratorEntity.PaymentModeStoredProcedure objinsetpmode = new  CRM.DataAccess.AdministratorEntity.PaymentModeStoredProcedure();
            Pmode.Insert(2, Session["usersid"].ToString());
            objinsetpmode.InsertUpdatePaymentMode(Pmode);

        }
        [WebMethod(EnableSession = true)]
        public void deletepaymentmode(int delpmode)
        {
            CRM.DataAccess.AdministratorEntity.PaymentModeStoredProcedure objdelpmode = new CRM.DataAccess.AdministratorEntity.PaymentModeStoredProcedure();
            objdelpmode.delpaymentmode(delpmode);
        }


    }

}
