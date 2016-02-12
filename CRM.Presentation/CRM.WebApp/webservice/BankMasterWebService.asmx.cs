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
    /// Summary description for BankMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BankMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_BOOKING_BANK_MASTER> BANK = new BankDataContext().VIEW_BOOKING_BANK_MASTERs.AsQueryable<VIEW_BOOKING_BANK_MASTER>();


        [WebMethod(EnableSession = true)]
        public List<VIEW_BOOKING_BANK_MASTER> GetBankName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                BANK = BANK.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                BANK = BANK.OrderBy(sortExpression);
            }
            else
            {
                BANK = BANK.OrderBy("BANK_ID ASC");
            }

            return BANK.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int BankNameCount()
        {
            return (int)BANK.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBankName(ArrayList bank)
        {

            BankMasterStoredProcedure objbank = new BankMasterStoredProcedure();
            bank.Insert(2, Session["usersid"].ToString());
            objbank.InsertUpdateBankName(bank);
        }
        [WebMethod(EnableSession = true)]
        public void delBankName(int delbank)
        {
            BankMasterStoredProcedure objdelbank = new BankMasterStoredProcedure();
            objdelbank.delBankName(delbank);
        }
    }
}
