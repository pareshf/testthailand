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
    /// Summary description for BookingCheckListWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BookingCheckListWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_BOOKING_CHECKLIST_MASTER> CheckList = new BookingCheckListDataContext().VIEW_FOR_BOOKING_CHECKLIST_MASTERs.AsQueryable<VIEW_FOR_BOOKING_CHECKLIST_MASTER>();
        IQueryable<VIEW_FOR_BOOKING_CHECKLIST_DETAIL> CheckListDetail = new BookingCheckListDataContext().VIEW_FOR_BOOKING_CHECKLIST_DETAILs.AsQueryable<VIEW_FOR_BOOKING_CHECKLIST_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_BOOKING_CHECKLIST_MASTER> GetCheckList(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CheckList = CheckList.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CheckList = CheckList.OrderBy(sortExpression);
            }
            else
            {
                CheckList = CheckList.OrderBy("CHECKLIST_ID ASC");
            }

            return CheckList.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCheckListCount()
        {
            return (int)CheckList.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetCheckListDetailCount()
        {
            return (int)CheckListDetail.Count();
        }
        [WebMethod(EnableSession = true)]

        public List<VIEW_FOR_BOOKING_CHECKLIST_DETAIL> GetCheckListDetails(string CHECKLIST_ID)
        {
            CheckListDetail = CheckListDetail.Where(String.Format(@"CHECKLIST_ID == {0}", CHECKLIST_ID));
            return CheckListDetail.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateforCheckList(ArrayList CheckList)
        {

            BookingCheckListStoredProcedure objchecklist = new BookingCheckListStoredProcedure();
            CheckList.Insert(4, Session["usersid"].ToString());
            objchecklist.InsertUpdateforCheckList(CheckList);

        }
        [WebMethod(EnableSession = true)]
        public void deleteMyCheckList(int delChecklist)
        {
            BookingCheckListStoredProcedure objdelchecklist = new BookingCheckListStoredProcedure();
            objdelchecklist.deleteMyCheckList(delChecklist);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateCheckListDetails(ArrayList CheckListDetails)
        {
            BookingCheckListStoredProcedure objchklistdetail = new BookingCheckListStoredProcedure();
            CheckListDetails.Insert(4, Session["usersid"].ToString());
            objchklistdetail.InsertUpdateCheckListDetails(CheckListDetails);

        }
        [WebMethod(EnableSession = true)]
        public void deleteChkdetails(int deleteChkdetails)
        {
            BookingCheckListStoredProcedure objdelchklistdetail = new BookingCheckListStoredProcedure();
            objdelchklistdetail.deleteChkdetails(deleteChkdetails);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewCheckList(string ChkListId)
        {
            BookingCheckListStoredProcedure objinsertnew = new BookingCheckListStoredProcedure();
            objinsertnew.InsertNewCheckList(ChkListId);
        }
    }
}
