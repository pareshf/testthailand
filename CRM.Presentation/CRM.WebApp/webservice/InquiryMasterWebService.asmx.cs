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
    /// Summary description for InquiryMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InquiryMasterWebService : System.Web.Services.WebService
    {
        IQueryable<VIEW_INQ_MASTER_FOR_TOURS_MASTER> InqMaster = new InquiryMasterDataContext().VIEW_INQ_MASTER_FOR_TOURS_MASTERs.AsQueryable<VIEW_INQ_MASTER_FOR_TOURS_MASTER>();
        
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_INQ_MASTER_FOR_TOURS_MASTER> GetInq(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                InqMaster = InqMaster.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                InqMaster = InqMaster.OrderBy(sortExpression);
            }
            else 
            {
                InqMaster = InqMaster.OrderBy("INQUIRY_ID");
            }
            
            return InqMaster.Skip(startIndex).Take(maximumRows).ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_INQ_MASTER_FOR_TOURS_MASTER> GetInqwithcustid(string CUST_ID)
        {
            InqMaster = InqMaster.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return InqMaster.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int InqMasterCount()
        {
            return (int)InqMaster.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateInq(ArrayList inquiry)
        {
            CRM.DataAccess.AdministratorEntity.InquiryMasterStoredProcedure objinq = new CRM.DataAccess.AdministratorEntity.InquiryMasterStoredProcedure();
            objinq.InsertUpdateInqinsupd(inquiry);
        }
    }
}
