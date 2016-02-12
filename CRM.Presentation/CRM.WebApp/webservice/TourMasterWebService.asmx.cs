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
    /// Summary description for TourWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class TourMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_TOUR_MASTER> Tour = new TourMasterDataBaseDataContext().VIEW_TOUR_MASTERs.AsQueryable<VIEW_TOUR_MASTER>();
        IQueryable<TOUR_QUOTE_MASTER> Quote = new TourMasterDataBaseDataContext().TOUR_QUOTE_MASTERs.AsQueryable<TOUR_QUOTE_MASTER>();
        IQueryable<COMMON_MARKETING_MATERIAL> Marketing = new TourMasterDataBaseDataContext().COMMON_MARKETING_MATERIALs.AsQueryable<COMMON_MARKETING_MATERIAL>();
        IQueryable<VIEW_TOUR_HOTEL_DETAIL> Hotel = new TourMasterDataBaseDataContext().VIEW_TOUR_HOTEL_DETAILs.AsQueryable<VIEW_TOUR_HOTEL_DETAIL>();
        IQueryable<VIEW_TOUR_TRANSPORTATION_DETAIL> Transport = new TourMasterDataBaseDataContext().VIEW_TOUR_TRANSPORTATION_DETAILs.AsQueryable<VIEW_TOUR_TRANSPORTATION_DETAIL>();
        IQueryable<VIEW_FLIGHT_DETAIL> Flight = new TourMasterDataBaseDataContext().VIEW_FLIGHT_DETAILs.AsQueryable<VIEW_FLIGHT_DETAIL>();
        IQueryable<VIEW_CRUISE_DETAIL> Cruise = new TourMasterDataBaseDataContext().VIEW_CRUISE_DETAILs.AsQueryable<VIEW_CRUISE_DETAIL>();
        IQueryable<VIEW_DECK_CABINE_DETAIL> Deck = new TourMasterDataBaseDataContext().VIEW_DECK_CABINE_DETAILs.AsQueryable<VIEW_DECK_CABINE_DETAIL>();
        IQueryable<VIEW_COUNTRY_FOR_VISA> Country = new TourMasterDataBaseDataContext().VIEW_COUNTRY_FOR_VISAs.AsQueryable<VIEW_COUNTRY_FOR_VISA>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_TOUR_MASTER> GetTour(int startIndex, int maximumRows, string sortExpression, string filterExpression,string searchExpression)
        {

            if (!String.IsNullOrEmpty(filterExpression))
            {
                Tour = Tour.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Tour = Tour.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(searchExpression))
            {
                TourMasterDataBaseDataContext db = new TourMasterDataBaseDataContext();
                Tour = db.VIEW_TOUR_MASTERs.Where(p => (p.COUNTRIES_FOR_VISA_TEXT.Contains(searchExpression) || p.TOUR_LONG_DESC.Contains(searchExpression)));
             }
            else
            {
                Tour = Tour.OrderBy("TOUR_ID DESC");
            }
            
            return Tour.Skip(startIndex).Take(maximumRows).ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_TOUR_MASTER> GetSearchTour(string param)
        {
            TourMasterDataBaseDataContext db=new TourMasterDataBaseDataContext();
            Tour = db.VIEW_TOUR_MASTERs.Where(p => p.COUNTRIES_FOR_VISA_TEXT.Contains(param));
            return Tour.ToList();            
        }

        [WebMethod(EnableSession = true)]
        public List<TOUR_QUOTE_MASTER> GetCurrencyByTOUR_ID(string TOUR_ID)
        {
            Quote = Quote.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return Quote.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<COMMON_MARKETING_MATERIAL> GetMarketingByTOUR_ID(string TOUR_ID)
        {
            Marketing = Marketing.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return Marketing.ToList();
        }


        [WebMethod(EnableSession = true)]
        public List<VIEW_TOUR_HOTEL_DETAIL> GetHotelDetailsByTOUR_ID(string TOUR_ID)
        {
            Hotel = Hotel.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return Hotel.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_TOUR_TRANSPORTATION_DETAIL> GetTransportDetailsByTOUR_ID(string TOUR_ID)
        {
            Transport = Transport.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return Transport.ToList();
        }

        [WebMethod(EnableSession = true)]
        public ArrayList GetDeckDetail(string cruise_id)
        {
            Deck = Deck.Where(String.Format(@"TOUR_CRUISE_ID == {0}", cruise_id));
            Deck = Deck.OrderBy("DECK_NO ASC");
            ArrayList a = new ArrayList();
            a.Add(Deck.ToList());
            return a;
            // debugger
        }

        [WebMethod(EnableSession = true)]
        public ArrayList GetCountryVisaDetail(string cruise_id)
        {
            Country = Country.Where(String.Format(@"TOUR_CRUISE_ID == {0}", cruise_id));
            ArrayList a = new ArrayList();
            a.Add(Country.ToList());
            return a;
        }

        [WebMethod(EnableSession = true)]
        public int GetCustomersCount()
        {
            return (int)Tour.Count();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FLIGHT_DETAIL> GetFlightDetailOnTourId(String Tour_Id)
        {
            Flight = Flight.Where(String.Format(@"TOUR_ID == {0}", Tour_Id));
            return Flight.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CRUISE_DETAIL> GetCruiseDetailOnTourId(String Tour_Id)
        {
            Cruise = Cruise.Where(String.Format(@"TOUR_ID == {0}", Tour_Id));
            return Cruise.ToList();
        }

        //[WebMethod(EnableSession = true)]
        //public List<VIEW_USER_MASTER_FOR_Tour> GetDetailsByTOUR_ID(string TOUR_ID)
        //{
        //    tuser = tuser.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
        //    HttpContext.Current.Session["OrdersByCustomerCount"] = tuser.Count();
        //    return tuser.ToList();
        //}
        //[WebMethod(EnableSession = true)]
        //public List<VIEW_Tour_CONTECT_DETAILS_NEW> GetContectByTOUR_ID(string TOUR_ID)
        //{
        //    tEmpContect = tEmpContect.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
        //    return tEmpContect.ToList();
        //}


        //[WebMethod(EnableSession = true)]
        //public int GetOrdersByTOUR_IDCount()
        //{
        //    return (int)HttpContext.Current.Session["OrdersByCustomerCount"];
        //}

        //// newly added ---getting all role list 
        //[WebMethod(EnableSession = true)]
        //public List<VIEW_ROLE_MASTER_NEW> GetRolebyEmpID(string TOUR_ID)
        //{
        //    tRole = tRole.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
        //    HttpContext.Current.Session["RoleCount"] = tRole.Count();
        //    return tRole.ToList();
        //}

        //[WebMethod(EnableSession = true)]
        //public List<VIEW_COMPANY_OF_Tour_NEW> GetCompanybyEmpID(string TOUR_ID)
        //{
        //    tCompany = tCompany.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
        //    HttpContext.Current.Session["CompanyCount"] = tCompany.Count();
        //    return tCompany.ToList();
        //}


        //[WebMethod(EnableSession = true)]
        //public List<SYS_ROLE_MASTER> GetAllRole()
        //{
        //    return tAllRole.ToList();
        //}
        //[WebMethod(EnableSession = true)]
        //public List<COMPANY_MASTER> GetAllCompany()
        //{
        //    return tAllCompany.ToList();
        //}

        //newly Added function from ronak adding new record at last abcd
        [WebMethod(EnableSession = true)]
        public void DeleteTourByTourID(int TourID)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.deleteTour(TourID);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateTour(ArrayList tTour)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            tTour.Insert(15, Session["usersid"].ToString());
            objempentity.InsertUpdateTour(tTour);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTourQuote(ArrayList tTour)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            tTour.Insert(20, Session["usersid"].ToString());
            objempentity.InsertUpdateTourQuote(tTour);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateHotelDetails(ArrayList tTour)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            tTour.Insert(33, Session["empid"].ToString());
            objempentity.InsertUpdateHotelDetails(tTour);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateTransportDetails(ArrayList tTour)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.InsertUpdateTransportDetails(tTour);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateFlight(ArrayList arr)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            arr.Insert(15, Session["empid"].ToString());
            objempentity.InsertUpdateFlightDetail(arr);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCruise(ArrayList arr)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            arr.Insert(15, Session["empid"].ToString());
            objempentity.InsertUpdateTourCruiseDetail(arr);
        }
        //[WebMethod(EnableSession = true)]
        //public void InsertUpdateTourContectDetails(ArrayList tcontect)
        //{
        //    TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
        //    tcontect.Insert(10, Session["usersid"].ToString());
        //    objempentity.InsertUpdateTourContectDetails(tcontect);
        //}

        //[WebMethod(EnableSession = true)]
        //public void UpdateUserDetails(ArrayList tTour)
        //{
        //    TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
        //    tTour.Insert(6, Session["usersid"].ToString());
        //    objempentity.UpdateUserDetails(tTour);
        //}

        ////get count of records for roles and company
        //[WebMethod(EnableSession = true)]
        //public int rolecount()
        //{
        //    return (int)tRole.Count();
        //}
        //[WebMethod(EnableSession = true)]
        //public int companycount()
        //{
        //    return (int)tCompany.Count();
        //}
        //[WebMethod(EnableSession = true)]
        //public int allrolecount()
        //{
        //    return (int)tAllRole.Count();
        //}
        //[WebMethod(EnableSession = true)]
        //public int allcompanycount()
        //{
        //    return (int)tAllCompany.Count();
        //}
        [WebMethod(EnableSession = true)]
        public void AssignCountry(int TourId, ArrayList Country)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignCountry(TourId, Country.ToString());

        }

        [WebMethod(EnableSession = true)]
        public void AssignCity(int TourId, ArrayList CITY)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignCountry(TourId, CITY.ToString());
        }

        [WebMethod(EnableSession = true)]
        public void AssignStartEndCity(int TourId, ArrayList STARTENDCITY)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignCountry(TourId, STARTENDCITY.ToString());
        }

        [WebMethod(EnableSession = true)]
        public void CopyData(int TourId)
        {

            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();

            //System.IO.File.Copy
            System.Data.DataSet ds = objempentity.CopyData(TourId);
            string N_TOUR_ID = ds.Tables[0].Rows[0]["TOUR_ID"].ToString();
            if (!System.IO.Directory.Exists(Server.MapPath("~/marketingdocuments/" + N_TOUR_ID.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/marketingdocuments/" + N_TOUR_ID.ToString() + "/"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    //System.IO.File.Copy(Server.MapPath("~/marketingdocuments/" + TourId.ToString() + ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), Server.MapPath("~/marketingdocuments/" + ds.Tables[0].Rows[i]["ATTACHMENT"].ToString()));
                    System.IO.File.Copy(Server.MapPath("~/marketingdocuments/" + TourId.ToString() + "/" + TourId.ToString() + ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), Server.MapPath("~/marketingdocuments/" + N_TOUR_ID.ToString() + "/" + ds.Tables[0].Rows[i]["ATTACHMENT"].ToString()));
                }
                catch (Exception ex) { }
            }

        }

        //[WebMethod(EnableSession = true)]
        //public void UnAssignCompany(string CompanyId, int UserID)
        //{
        //    TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
        //    objempentity.UnAssignCompany(CompanyId, UserID);
        //}
        //[WebMethod(EnableSession = true)]
        //public void InsertAssignRole(int UserId, int CompanyId, int RoleId)
        //{
        //    TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
        //    objempentity.InsertAssignRole(UserId, CompanyId, RoleId);
        //}
        //[WebMethod(EnableSession = true)]
        //public void UnAssignRole(int UserID, int CompanyId, int RoleId)
        //{
        //    TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
        //    objempentity.UnAssignRole(UserID, CompanyId, RoleId);
        //}
        [WebMethod(EnableSession = true)]
        public void AssignCountry(int TourId, string Country)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignCountry(TourId, Country);
        }

        [WebMethod(EnableSession = true)]
        public void AssignCITY(int TourId, string AssignCITY)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignCITY(TourId, AssignCITY);
        }

        [WebMethod(EnableSession = true)]
        public void AssignStartEndCity(int TourId, string AssignStartEndCity)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.AssignStartEndCity(TourId, AssignStartEndCity);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateDeck(ArrayList ary)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.InsertUpdateDeckDetail(ary);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewAirline(string Tour_Id)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.InsertNewAirline(Tour_Id);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewCruise(string Tour_Id)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.InsertNewCruise(Tour_Id);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewHotel(string Tour_Id)
        {
            TourmasterStoreprocedures objempentity = new TourmasterStoreprocedures();
            objempentity.InsertNewHotel(Tour_Id);
        }
    }
}
