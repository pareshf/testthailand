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
    /// Summary description for SupplierMarginMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierMarginMaster : System.Web.Services.WebService
    {
        
       // IQueryable<VIEW_SUPPLIER_MARGIN_MASTER> margin = new SupplierMarginMasterDataContext().VIEW_SUPPLIER_MARGIN_MASTERs.AsQueryable<VIEW_SUPPLIER_MARGIN_MASTER>();
        IQueryable<VIEW_SUPPLIER_MARGIN_MASTER> Margin = new SupplierMarginMasterDataContext().VIEW_SUPPLIER_MARGIN_MASTERs.AsQueryable<VIEW_SUPPLIER_MARGIN_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_MARGIN_MASTER> GetSupplierMargin(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Margin = Margin.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Margin = Margin.OrderBy(sortExpression);
            }
            else
            {
                Margin = Margin.OrderBy("SUPPLIER_MARGIN_ID ASC");
            }

            return Margin.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetSupplierMarginCount()
        {
            return (int)Margin.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteSupplierMargin(int SUPPLIERMARGINID)
        {
            CRM.DataAccess.AdministratorEntity.SupplierMarginMaster objSupplierMargin = new DataAccess.AdministratorEntity.SupplierMarginMaster();
            objSupplierMargin.deleteSupplierMarginMaster(SUPPLIERMARGINID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSupplierMargin(ArrayList Margin)
        {
            CRM.DataAccess.AdministratorEntity.SupplierMarginMaster objSupplierMargin = new DataAccess.AdministratorEntity.SupplierMarginMaster();
            Margin.Insert(5, Session["usersid"].ToString());
            objSupplierMargin.InsertUpdateSupplierMarginMaster(Margin);
        }
    }
       
    
}
