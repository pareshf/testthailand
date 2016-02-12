using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using System.Data;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for GeographicLocationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GeographicLocationWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_COUNTRY_MASTER>Country = new GeographicLocationDataContext().VIEW_COUNTRY_MASTERs.AsQueryable<VIEW_COUNTRY_MASTER>();
        IQueryable<VIEW_COMMON_STATE_MASTER> State = new GeographicLocationDataContext().VIEW_COMMON_STATE_MASTERs.AsQueryable<VIEW_COMMON_STATE_MASTER>();
        IQueryable<VIEW_CITY_MASTER> City = new GeographicLocationDataContext().VIEW_CITY_MASTERs.AsQueryable<VIEW_CITY_MASTER>();
        

        [WebMethod(EnableSession = true)]
        public List<VIEW_COUNTRY_MASTER> GetCountryName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Country = Country.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Country = Country.OrderBy(sortExpression);
            }
            else
            {
                Country = Country.OrderBy("COUNTRY_ID ASC");
            }

            return Country.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCountryCount()
        {
            return (int)Country.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetCityCount()
        {
            return (int)City.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCountryName(ArrayList country)
        {

            GeograficLocationStoredProcedure objgeo = new GeograficLocationStoredProcedure();
            country.Insert(6, Session["usersid"].ToString());
            objgeo.InsertUpdateCountryName(country);

        }
        [WebMethod(EnableSession = true)]
        public void delCountryName(int delcountry)
        {
            GeograficLocationStoredProcedure objdelgeo = new GeograficLocationStoredProcedure();
            objdelgeo.delCountryName(delcountry);
        }
        [WebMethod(EnableSession = true)]
        public int GetStateCount()
        {
            return (int)State.Count();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_COMMON_STATE_MASTER> GetStateName(string COUNTRY_ID)
        {
            State = State.Where(String.Format(@"COUNTRY_ID == {0}", COUNTRY_ID));
            return State.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CITY_MASTER> GetCityName(string STATE_ID)
        {
            City = City.Where(String.Format(@"STATE_ID == {0}", STATE_ID));
            return City.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateStateName(ArrayList state)
        {
            GeograficLocationStoredProcedure objgeographic = new GeograficLocationStoredProcedure();
            state.Insert(2, Session["usersid"].ToString());
            objgeographic.InsertUpdateStateName(state);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewState(string COUNTRY_ID)
        {
            GeograficLocationStoredProcedure objinsertstate = new GeograficLocationStoredProcedure();
            objinsertstate.InsertNewState(COUNTRY_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewCity(string COUNTRY_ID, string STATE_ID)
        {
            GeograficLocationStoredProcedure objinsertcity = new GeograficLocationStoredProcedure();
            objinsertcity.InsertNewCity(COUNTRY_ID, STATE_ID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCityName(ArrayList City)
        {
            GeograficLocationStoredProcedure objcity = new GeograficLocationStoredProcedure();
            City.Insert(2, Session["usersid"].ToString());
            objcity.InsertUpdateCityName(City);
        }
        [WebMethod(EnableSession = true)]
        public void deleteState(int STATE_ID)
        {
            GeograficLocationStoredProcedure objdelstate = new GeograficLocationStoredProcedure();
            objdelstate.deleteState(STATE_ID);
        }
        [WebMethod(EnableSession = true)]
        public void deleteCity(int CITY_ID)
        {
            GeograficLocationStoredProcedure objdelcity = new GeograficLocationStoredProcedure();
            objdelcity.deleteCity(CITY_ID);
        }
    }
}
