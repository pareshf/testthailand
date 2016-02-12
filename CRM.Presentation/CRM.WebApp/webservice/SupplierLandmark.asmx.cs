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
    /// Summary description for SupplierLandmark
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierLandmark : System.Web.Services.WebService
    {

        IQueryable<VIEW_SUPPLIER_LANDMARK_MASTER> Landmark = new SupplierLandmarkMasterDataContext().VIEW_SUPPLIER_LANDMARK_MASTERs.AsQueryable<VIEW_SUPPLIER_LANDMARK_MASTER>();

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
        public void InsertUpdateLandmark(ArrayList LandMark)
        {

          
            CRM.DataAccess.AdministratorEntity.SupplierLandmarkMaster objinsertlandmark = new CRM.DataAccess.AdministratorEntity.SupplierLandmarkMaster();
            objinsertlandmark.InsertUpdateLandmarkNew(LandMark);

        }
        [WebMethod(EnableSession = true)]
        public void deleteLandmarkMaster(int LAMDMARKID)
        {
            CRM.DataAccess.AdministratorEntity.SupplierLandmarkMaster objdellandmark = new CRM.DataAccess.AdministratorEntity.SupplierLandmarkMaster();
            objdellandmark.deleteLandmark(LAMDMARKID);
        }
    }
}
