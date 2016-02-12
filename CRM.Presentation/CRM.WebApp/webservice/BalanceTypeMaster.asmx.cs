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
    /// Summary description for BalanceTypeMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BalanceTypeMaster : System.Web.Services.WebService
    {

        // IQueryable<VIEW_CAR_MASTER> car = new CarMasterDataContext().VIEW_CAR_MASTERs.AsQueryable<VIEW_CAR_MASTER>();
        IQueryable<VIEW_BALANCE_TYPE_MASTER> balance = new BalanceTypeMasterDataContext().VIEW_BALANCE_TYPE_MASTERs.AsQueryable<VIEW_BALANCE_TYPE_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_BALANCE_TYPE_MASTER> GetBalanceType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                balance = balance.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                balance = balance.OrderBy(sortExpression);
            }
            else
            {
                balance = balance.OrderBy("BAL_TYPE_ID ASC");
            }

            return balance.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetBalanceTypeCount()
        {
            return (int)balance.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteBalanceType(int BALTYPEID)
        {
            CRM.DataAccess.AdministratorEntity.BalanceTypeMaster objdelbalance = new DataAccess.AdministratorEntity.BalanceTypeMaster();
            objdelbalance.deleteBalanceType(BALTYPEID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBalancetype(ArrayList Balance)
        {
            CRM.DataAccess.AdministratorEntity.BalanceTypeMaster objinsertbalance = new DataAccess.AdministratorEntity.BalanceTypeMaster();
            objinsertbalance.InsertUpdateBalanceMaster(Balance);
        }
    }
}

