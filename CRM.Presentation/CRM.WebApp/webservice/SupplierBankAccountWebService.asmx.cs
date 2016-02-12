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
    /// Summary description for SupplierBankAccountWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierBankAccountWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_SUPPLIER_BANK_ACCOUNT_DETAIL> BANKACCOUNT = new SupplierBankAccountDataContext().VIEW_SUPPLIER_BANK_ACCOUNT_DETAILs.AsQueryable<VIEW_SUPPLIER_BANK_ACCOUNT_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_BANK_ACCOUNT_DETAIL> GetSupplierBankAcount(int startIndex, int maximumRows, string sortExpression, string filterExpression, string sfname)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                BANKACCOUNT = BANKACCOUNT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                BANKACCOUNT = BANKACCOUNT.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                SupplierBankAccountDataContext db = new SupplierBankAccountDataContext();
                BANKACCOUNT = db.VIEW_SUPPLIER_BANK_ACCOUNT_DETAILs.Where(p => (p.COMPANY_NAME.Contains(sfname)));
            }
            else
            {
                BANKACCOUNT = BANKACCOUNT.OrderBy("SUPPLIER_BANK_ACCOUNT_ID ASC");
            }

            return BANKACCOUNT.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetSupplierBankCount()
        {
            return (int)BANKACCOUNT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSupplierBankAccount(ArrayList SupplierAccount)
        {

            CRM.DataAccess.AdministratorEntity.SupplierBankAccountStoredProcedure objinsertbankdetail = new CRM.DataAccess.AdministratorEntity.SupplierBankAccountStoredProcedure();
            SupplierAccount.Insert(9, Session["usersid"].ToString());
            objinsertbankdetail.InsertUpdateSupplierBank(SupplierAccount);

        }
        [WebMethod(EnableSession = true)]
        public void delSupplierBankAccount(int delSupplierbank)
        {
            CRM.DataAccess.AdministratorEntity.SupplierBankAccountStoredProcedure objdelbankaccount = new SupplierBankAccountStoredProcedure();
            objdelbankaccount.SupplierBankAccount(delSupplierbank);
        }
    }
}
