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
    /// Summary description for SupplierHotelServiceTypeMasterWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class SupplierHotelServiceTypeMasterWebservice : System.Web.Services.WebService
    {

       // IQueryable<View_Address_Type_Lookup> AddressType = new Address_Type_LookupDataContext().View_Address_Type_Lookups.AsQueryable<View_Address_Type_Lookup>();
        IQueryable<VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER> ServiceType = new Supplier_Hotel_Service_Type_MasterDataContext().VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTERs.AsQueryable<VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER>();
    
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER> GetServiceType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                ServiceType = ServiceType.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                ServiceType = ServiceType.OrderBy(sortExpression);
            }
            else
            {
                ServiceType = ServiceType.OrderBy("SERVICE_TYPE_ID ASC");
            }

            return ServiceType.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetServiceTypeCount()
        {
            return (int)ServiceType.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateServiceType(ArrayList Service)
        {

            SupplierHotelServiceTypeMaster objservicetype = new SupplierHotelServiceTypeMaster();
            Service.Insert(3, Session["usersid"].ToString());
            objservicetype.InsertUpdateServiceType(Service);

        }
        [WebMethod(EnableSession = true)]
        public void deleteServiceType(int SERVICEID)
        {
            SupplierHotelServiceTypeMaster objservicetype = new SupplierHotelServiceTypeMaster();
            objservicetype.deleteServiceType(SERVICEID);
        }
    }
}
