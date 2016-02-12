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
    /// Summary description for SupplierLandmarkMasterwebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class SupplierLandmarkMasterwebservice : System.Web.Services.WebService
    {
        //IQueryable<View_Address_Type_Lookup> AddressType = new Address_Type_LookupDataContext().View_Address_Type_Lookups.AsQueryable<View_Address_Type_Lookup>();
        IQueryable<VIEW_SUPPLIER_LANDMARK_MASTER> Landmark = new SupplierLandmarkMasterDataContext().VIEW_SUPPLIER_LANDMARK_MASTERs.AsQueryable <VIEW_SUPPLIER_LANDMARK_MASTER>();  
        [WebMethod(EnableSession = true)]

        public List<VIEW_SUPPLIER_LANDMARK_MASTER> GetLandmark(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Landmark = Landmark.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Landmark = Landmark.OrderBy(sortExpression);
            }
            else
            {
                Landmark = Landmark.OrderBy("LANDMARK_ID ASC");
            }

            return Landmark.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetLandmarkCount()
        {
            return (int)Landmark.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateLandmarkNew(ArrayList LandMark)
        {

            CRM.WebApp.webservice.SupplierLandmarkMasterwebservice objlamdmark = new CRM.WebApp.webservice.SupplierLandmarkMasterwebservice();
            LandMark.Insert(3, Session["usersid"].ToString());
            objlamdmark.InsertUpdateLandmarkNew(LandMark);
            
        }
        [WebMethod(EnableSession = true)]
        public void deleteLandmark(int LAMDMARKID)
        {
            CRM.WebApp.webservice.SupplierLandmarkMasterwebservice objlandmarkdel = new CRM.WebApp.webservice.SupplierLandmarkMasterwebservice();
            objlandmarkdel.deleteLandmark(LAMDMARKID);
        }
    }
}
