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
    /// Summary description for SupplierTypeMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierTypeMaster : System.Web.Services.WebService
    {
        //IQueryable<VIEW_BOOKING_BANK_MASTER> BANK = new BankDataContext().VIEW_BOOKING_BANK_MASTERs.AsQueryable<VIEW_BOOKING_BANK_MASTER>();
        IQueryable<VIEW_FOR_SUPPLIER_TYPE_MASTER> sup = new SupplierTypeMasterDataContext().VIEW_FOR_SUPPLIER_TYPE_MASTERs.AsQueryable<VIEW_FOR_SUPPLIER_TYPE_MASTER>(); 

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_SUPPLIER_TYPE_MASTER> GetSupplierType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                sup = sup.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sup = sup.OrderBy(sortExpression);
            }
            else
            {
                sup = sup.OrderBy("SUPPLIER_TYPE_ID ASC");
            }

            return sup.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int SupplierTypeCount()
        {
            return (int)sup.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateSupplier(ArrayList sup)
        {

            CRM.DataAccess.AdministratorEntity.SupplierTypeMaster objsuppllier = new CRM.DataAccess.AdministratorEntity.SupplierTypeMaster();
           
            objsuppllier.InsertUpdateSupplierType(sup);
        }
        [WebMethod(EnableSession = true)]
        public void delSupplier(int delsup)
        {
            CRM.DataAccess.AdministratorEntity.SupplierTypeMaster objdelSupplier = new CRM.DataAccess.AdministratorEntity.SupplierTypeMaster();
            objdelSupplier.delSupplierType(delsup);
        }

       
    }
}
