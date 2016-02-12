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
    /// Summary description for CoachMasterWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CoachMasterWeb : System.Web.Services.WebService
    {

       // IQueryable<View_Address_Type_Lookup> AddressType = new Address_Type_LookupDataContext().View_Address_Type_Lookups.AsQueryable<View_Address_Type_Lookup>();
        IQueryable<View_Coach_Master> coach = new CoachmasterDataContext().View_Coach_Masters.AsQueryable<View_Coach_Master>();   
        [WebMethod(EnableSession = true)]
        public List<View_Coach_Master> GetCoachType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                coach = coach.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                coach = coach.OrderBy(sortExpression);
            }
            else
            {
                coach = coach.OrderBy("COACH_TYPE_MASTER_ID ASC");
            }

            return coach.Skip(startIndex).Take(maximumRows).ToList();

        }
         [WebMethod(EnableSession = true)]
        public int GetCoachTypeCount()
        {
            return (int)coach.Count();
        }

         [WebMethod(EnableSession = true)]
         public void InsertUpdatecoachtype(ArrayList Coach)
         {

             CoachMaster objcoachtype = new CoachMaster();
             Coach.Insert(3, Session["usersid"].ToString());
             objcoachtype.InsertUpdatecoachtype(Coach);

         }
         [WebMethod(EnableSession = true)]
         public void deleteCoachType(int COACHTYPEID)
         {
             CoachMaster objcoach = new CoachMaster();
             objcoach.deleteCoachType(COACHTYPEID);
         }
    }
}
