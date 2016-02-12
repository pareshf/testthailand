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
    /// Summary description for SpecialOfferWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SpecialOfferWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_SPECIAL_OFFER> Special_offer = new SpecialOfferDataContext().VIEW_SPECIAL_OFFERs.AsQueryable<VIEW_SPECIAL_OFFER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_SPECIAL_OFFER> GetOffer(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Special_offer = Special_offer.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Special_offer = Special_offer.OrderBy(sortExpression);
            }
            else
            {
                Special_offer = Special_offer.OrderBy("OFFER_ID ASC");
            }

            return Special_offer.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetOfferCount()
        {
            return (int)Special_offer.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSpecialOffer(ArrayList offer)
        {

            CRM.DataAccess.AdministratorEntity.SpecialOfferStoredProcedure objinsertoffer = new CRM.DataAccess.AdministratorEntity.SpecialOfferStoredProcedure();
            objinsertoffer.InsertUpdateSpecialOffer(offer);

        }
        [WebMethod(EnableSession = true)]
        public void delSpecialOffer(int offerid)
        {
            CRM.DataAccess.AdministratorEntity.SpecialOfferStoredProcedure objdeloffer = new CRM.DataAccess.AdministratorEntity.SpecialOfferStoredProcedure();
            objdeloffer.delSpecialOffer(offerid);
        }
    }
}
