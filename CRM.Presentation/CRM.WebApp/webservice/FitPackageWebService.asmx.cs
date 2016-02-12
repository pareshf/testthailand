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
    /// Summary description for FitPackageWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FitPackageWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_FOR_FIT_PACKAGE> FIT = new FitPackageDataContext().VIEW_FOR_FIT_PACKAGEs.AsQueryable<VIEW_FOR_FIT_PACKAGE>();
        IQueryable<VIEW_FOR_FIT_PACKAGE_CITY_MAPPING> MAPPING = new FitPackageDataContext().VIEW_FOR_FIT_PACKAGE_CITY_MAPPINGs.AsQueryable<VIEW_FOR_FIT_PACKAGE_CITY_MAPPING>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_FIT_PACKAGE> GetFit(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                FIT = FIT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                FIT = FIT.OrderBy(sortExpression);
            }
            else
            {
                FIT = FIT.OrderBy("FIT_PACKAGE_ID ASC");
            }

            return FIT.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int FitCount()
        {
            return (int)FIT.Count();
        }
        [WebMethod(EnableSession = true)]
        public int FitMappingCount()
        {
            return (int)MAPPING.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFitPackage(ArrayList FIT)
        {

            FitPackageStoredProcedure objinsertfit = new FitPackageStoredProcedure();
            objinsertfit.InsertUpdateFitPackage(FIT);
        }
        [WebMethod(EnableSession = true)]
        public void delFitPackage(int delfit)
        {
            FitPackageStoredProcedure objdelfit = new FitPackageStoredProcedure();
            objdelfit.delFitPackage(delfit);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_FIT_PACKAGE_CITY_MAPPING> GetCity(string FIT_PACKAGE_ID)
        {
            MAPPING = MAPPING.Where(String.Format(@"FIT_PACKAGE_ID == {0}", FIT_PACKAGE_ID));
            return MAPPING.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateFitPackageCityMapping(ArrayList city)
        {

            FitPackageStoredProcedure objinsertfitcity = new FitPackageStoredProcedure();
            objinsertfitcity.InsertUpdateFitPackageCityMapping(city);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewCity(string FIT_PACKAGE_CITY_ID)
        {
            FitPackageStoredProcedure objinsetnewcity = new FitPackageStoredProcedure();
            objinsetnewcity.InsertNewCity(FIT_PACKAGE_CITY_ID);
        }
        [WebMethod(EnableSession = true)]
        public void delCities(int delCity)
        {
            CRM.DataAccess.AdministratorEntity.FitPackageStoredProcedure objdelCity = new CRM.DataAccess.AdministratorEntity.FitPackageStoredProcedure();
            objdelCity.deleteCity(delCity);
        }
    }
}
