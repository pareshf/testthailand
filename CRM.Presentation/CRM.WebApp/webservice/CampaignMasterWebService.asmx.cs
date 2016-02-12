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
    /// Summary description for CampaignMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CampaignMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_COMPAIGN_MASTER> CAMPAIGN = new CampaignDataContext().VIEW_COMPAIGN_MASTERs.AsQueryable<VIEW_COMPAIGN_MASTER>();
        IQueryable<VIEW_COMPAIGN_MARKETING_MAPPING> MAPPING = new Campaign2DataContext().VIEW_COMPAIGN_MARKETING_MAPPINGs.AsQueryable<VIEW_COMPAIGN_MARKETING_MAPPING>();
        IQueryable<VIEW_COMAPAIGN_TARGETLIST_DETAIL> TargetList = new Campaign2DataContext().VIEW_COMAPAIGN_TARGETLIST_DETAILs.AsQueryable<VIEW_COMAPAIGN_TARGETLIST_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPAIGN_MASTER> GetCompaign(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CAMPAIGN = CAMPAIGN.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CAMPAIGN = CAMPAIGN.OrderBy(sortExpression);
            }
            else
            {
                CAMPAIGN = CAMPAIGN.OrderBy("CAMPAIGN_ID ASC");
            }

            return CAMPAIGN.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCompaignCount()
        {
            return (int)CAMPAIGN.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetComapaingTargetCount()
        {
            return (int)TargetList.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetMappingCount()
        {
            return (int)MAPPING.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCampaign(ArrayList Campaign)
        {

            CampaignStoredProcedureNew objcomapign = new CampaignStoredProcedureNew();
            Campaign.Insert(14, Session["usersid"].ToString());
            objcomapign.InsertUpdateCampaign(Campaign);

        }
        [WebMethod(EnableSession = true)]
        public void delCampaign(int compdel)
        {
            CampaignStoredProcedureNew objdelcompaign = new CampaignStoredProcedureNew();
            objdelcompaign.delCampaign(compdel);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_COMAPAIGN_TARGETLIST_DETAIL> GetTargetList(string CAMPAIGN_ID)
        {
            TargetList = TargetList.Where(String.Format(@"CAMPAIGN_ID == {0}", CAMPAIGN_ID));
            return TargetList.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPAIGN_MARKETING_MAPPING> GetMapping(string CAMPAIGN_ID)
        {
            MAPPING = MAPPING.Where(String.Format(@"CAMPAIGN_ID == {0}", CAMPAIGN_ID));
            return MAPPING.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCampaignTargetListDetail(ArrayList CampaignDetail)
        {

            CampaignStoredProcedureNew objinserttarget = new CampaignStoredProcedureNew();
            CampaignDetail.Insert(2, Session["usersid"].ToString());
            objinserttarget.InsertUpdateCampaignTargetListDetail(CampaignDetail);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateforMapping(ArrayList mapping)
        {

            CampaignStoredProcedureNew objinsertmapping = new CampaignStoredProcedureNew();
            mapping.Insert(4, Session["usersid"].ToString());
            objinsertmapping.InsertUpdateforMapping(mapping);

        }
        [WebMethod(EnableSession = true)]
        public void InsertNewRow(string CAMPAIGN_ID)
        {

            CampaignStoredProcedureNew objinsertnewrow = new CampaignStoredProcedureNew();
            objinsertnewrow.InsertNewRow(CAMPAIGN_ID);

        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTargetList(string CAMPAIGN_ID)
        {

            CampaignStoredProcedureNew objinsertnewtarget = new CampaignStoredProcedureNew();
            objinsertnewtarget.InsertNewTargetList(CAMPAIGN_ID);

        }

       
    }
}
