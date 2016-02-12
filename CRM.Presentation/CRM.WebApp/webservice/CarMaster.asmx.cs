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
    /// Summary description for CarMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CarMaster : System.Web.Services.WebService
    {


        
        IQueryable<VIEW_CAR_MASTER> car = new CarMasterDataContext().VIEW_CAR_MASTERs.AsQueryable<VIEW_CAR_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_CAR_MASTER> GetCarType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                car = car.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                car = car.OrderBy(sortExpression);
            }
            else
            {
                car = car.OrderBy("CAR_MASTER_ID ASC");
            }

            return car.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetCarTypeCount()
        {
            return (int)car.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteCarType(int CARTYPEID)
        {
            CRM.DataAccess.AdministratorEntity.CarMaster objdelcar = new DataAccess.AdministratorEntity.CarMaster();
            objdelcar.deleteCarType(CARTYPEID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCartype(ArrayList Car)
        {
            CRM.DataAccess.AdministratorEntity.CarMaster objinsertcar = new DataAccess.AdministratorEntity.CarMaster();
            Car.Insert(3, Session["usersid"].ToString());
            objinsertcar.InsertUpdateCarMaster(Car);
        }
    }
}
