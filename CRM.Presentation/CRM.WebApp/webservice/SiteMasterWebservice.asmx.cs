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
    /// Summary description for SiteMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SiteMasterWebService : System.Web.Services.WebService {

        
        IQueryable<VIEW_SITE_SEEING_MASTER> SITE = new SiteMasterDataContext().VIEW_SITE_SEEING_MASTERs.AsQueryable<VIEW_SITE_SEEING_MASTER>();
        IQueryable<VIEW_FARE_SITE_SEEING_PHOTO_DETAIL> PHOTO = new SiteMasterDataContext().VIEW_FARE_SITE_SEEING_PHOTO_DETAILs.AsQueryable<VIEW_FARE_SITE_SEEING_PHOTO_DETAIL>();
        

        [WebMethod(EnableSession = true)]
        public List<VIEW_SITE_SEEING_MASTER> GetSiteDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                SITE = SITE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                SITE = SITE.OrderBy(sortExpression);
            }
            else
            {
                SITE = SITE.OrderBy("SIGHT_SEEING_SRNO ASC");
            }

            return SITE.Skip(startIndex).Take(maximumRows).ToList();

           
        }
        [WebMethod(EnableSession = true)]
        public int GetSiteCount()
        {
            return (int)SITE.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetPhotoCount()
        {
            return (int)PHOTO.Count();
        }
        [WebMethod(EnableSession = true)]
        public void insertupdateSite(ArrayList Site)
        {
           
            SiteSeeingMasterStoredProcedure objsite = new SiteSeeingMasterStoredProcedure();
            objsite.insertupdateSite(Site);

        }
        [WebMethod(EnableSession = true)]
        public void DeleteMySite(int delsite)
        {
            SiteSeeingMasterStoredProcedure objsitedel = new SiteSeeingMasterStoredProcedure();
            objsitedel.DeleteMySite(delsite);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_SITE_SEEING_PHOTO_DETAIL> GetSitePhotoDetails(string SIGHT_SEEING_SRNO)
        {
            PHOTO = PHOTO.Where(String.Format(@"SIGHT_SEEING_SRNO == {0}", SIGHT_SEEING_SRNO));
            return PHOTO.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void insertupdatePhoto(ArrayList Photo)
        {
            SiteSeeingMasterStoredProcedure objephoto = new SiteSeeingMasterStoredProcedure();
            objephoto.insertupdatePhoto(Photo);
        }
        [WebMethod(EnableSession = true)]
        public void insertnewPhoto(string siteid)
        {
            SiteSeeingMasterStoredProcedure objnewphoto = new SiteSeeingMasterStoredProcedure();
            objnewphoto.insertnewPhoto(siteid);
        }
        [WebMethod(EnableSession = true)]
        public void DeleteSitePhoto(int photoid)
        {
            
            SiteSeeingMasterStoredProcedure objdelphoto = new SiteSeeingMasterStoredProcedure();
            objdelphoto.DeleteSitePhoto(photoid);
           
        }
    }
     
}
