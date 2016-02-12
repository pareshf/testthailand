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
    /// Summary description for CompanyBankAccountWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompanyBankAccountWebservice : System.Web.Services.WebService
    {
        IQueryable<VIEW_FOR_COMPANY_BANK_DETAIL> COMPANYBANKACCOUNT = new CompanyBankAccountDataContext().VIEW_FOR_COMPANY_BANK_DETAILs.AsQueryable<VIEW_FOR_COMPANY_BANK_DETAIL>();


        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_COMPANY_BANK_DETAIL> GetCompanyBankAcount(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                COMPANYBANKACCOUNT = COMPANYBANKACCOUNT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                COMPANYBANKACCOUNT = COMPANYBANKACCOUNT.OrderBy(sortExpression);
            }
            else
            {
                COMPANYBANKACCOUNT = COMPANYBANKACCOUNT.OrderBy("COMP_ACC_ID ASC");
            }

            return COMPANYBANKACCOUNT.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetBankCount()
        {
            return (int)COMPANYBANKACCOUNT.Count();
        }
        [WebMethod(EnableSession = true)]
        public string InsetUpdateCompanyBank(ArrayList CompanyBankAccount,string s)
        {
            CRM.DataAccess.AdministratorEntity.CompanyBankAccountStoreProcedure objinsertCompanyBankDetaill = new CRM.DataAccess.AdministratorEntity.CompanyBankAccountStoreProcedure();
           // objinsertCompanyBankDetaill.InsertUpdateCompanyBankAccountDetails(CompanyBankAccount);

            System.Data.DataSet ds1 = objinsertCompanyBankDetaill.CheckValidation();
            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if ((ds1.Tables[0].Rows[i]["ACC_NO"].ToString().Equals(CompanyBankAccount[5]) || CompanyBankAccount[5].Equals("")))
                {
                    j = 1;
                    break;

                }

            }
            if (j == 0)
            {
                objinsertCompanyBankDetaill.InsertUpdateCompanyBankAccountDetails(CompanyBankAccount);
            }
            else if (j == 1)
            {
                System.Data.DataSet ds = objinsertCompanyBankDetaill.InsertUpdateCompanyBankAccountDetails(CompanyBankAccount);
                s = ds.Tables[0].Rows[0]["EMAIL_CHECKER"].ToString();
                if (s == "N")
                {

                }
                if (s == "Y")
                {

                }
            }
            return s;
            
        }
       
    }
}
