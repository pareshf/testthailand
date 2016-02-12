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
    /// Summary description for SupplierFacilityMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierFacilityMasterWebService : System.Web.Services.WebService
    {

       // IQueryable<VIEW_SUPPLIER_FACALITY_MASTER> Facility = new SupplierFacilityMasterDataContext().VIEW_SUPPLIER_FACALITY_MASTERs.AsQueryable<VIEW_SUPPLIER_FACALITY_MASTER>();
        IQueryable<VIEW_SUPPLIER_FACALITY_MASTER> Facility = new SupplierFacilityMasterDataContext().VIEW_SUPPLIER_FACALITY_MASTERs.AsQueryable<VIEW_SUPPLIER_FACALITY_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_FACALITY_MASTER> GetFacility(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Facility = Facility.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Facility = Facility.OrderBy(sortExpression);
            }
            else
            {
                Facility = Facility.OrderBy("FACELITY_ID ASC");
            }

            return Facility.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetFacilityCount()
        {
            return (int)Facility.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFacility(ArrayList Facility)
        {


            CRM.DataAccess.AdministratorEntity.SupplierFacilityMasterSp objfacility = new CRM.DataAccess.AdministratorEntity.SupplierFacilityMasterSp();
            Facility.Insert(3, Session["usersid"].ToString());
            objfacility.InsertUpdateFacility(Facility);

        }
        [WebMethod(EnableSession = true)]
        public void deleteFacility(int FACILITYID)
        {
           
            SupplierFacilityMasterSp objfacility = new SupplierFacilityMasterSp();
            objfacility.deleteFacility(FACILITYID);
        }
    }
}
