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
    /// Summary description for AccountsGroupWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AccountsGroupWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_ACCOUNT_GROUP_MASTER> AccountGroup = new AccountsGroupDataContext().VIEW_ACCOUNT_GROUP_MASTERs.AsQueryable<VIEW_ACCOUNT_GROUP_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_ACCOUNT_GROUP_MASTER> GetAccountGroup(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                AccountGroup = AccountGroup.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                AccountGroup = AccountGroup.OrderBy(sortExpression);
            }
            else
            {
                AccountGroup = AccountGroup.OrderBy("ACCOUNT_GROUP_ID ASC");
            }

            return AccountGroup.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int AccountGroupCount()
        {
            return (int)AccountGroup.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateAccountsGroup(ArrayList AccountsGroup)
        {

            AccountsGroupStoredProcedure objinsertgroup = new AccountsGroupStoredProcedure();
            objinsertgroup.InsertUpdateAccounts(AccountsGroup);
        }
        [WebMethod(EnableSession = true)]
        public void delAccountsGroup(int Group)
        {
            AccountsGroupStoredProcedure objdelgroup = new AccountsGroupStoredProcedure();
            objdelgroup.delAccounts(Group);
        }
    }
}
