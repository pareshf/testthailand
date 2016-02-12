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
    /// Summary description for MoneyTransferAgent
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MoneyTransferAgent : System.Web.Services.WebService
    {
        IQueryable<VIEW_MONEY_TRANSFER_AGENT> moneytransagent = new MoneyTransferAgentDataContext().VIEW_MONEY_TRANSFER_AGENTs.AsQueryable<VIEW_MONEY_TRANSFER_AGENT>();
        //[WebMethod(EnableSession = true)]
        //public List<VIEW_MONEY_TRANSFER_AGENT> GetMoneyAgentDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        //{
        //    if (!String.IsNullOrEmpty(filterExpression))
        //    {
        //        moneytransagent = moneytransagent.Where(filterExpression);
        //    }
        //    if (!String.IsNullOrEmpty(sortExpression))
        //    {
        //        moneytransagent = moneytransagent.OrderBy(sortExpression);
        //    }
        //    else
        //    {
        //        moneytransagent = moneytransagent.OrderBy("PAYMENT_SR_NO");
        //    }

        //    return moneytransagent.Skip(startIndex).Take(maximumRows).ToList();

        //    // TASK = TASK.Where(String.Format(@"MYTASK_ID == {0}", MYTASK_ID));
        //    //return TASK.ToList();
        //}
        [WebMethod(EnableSession = true)]
        public List<VIEW_MONEY_TRANSFER_AGENT> GetMoneyAgentDetails(int PAYMENT_SR_NO)
        {
            moneytransagent = moneytransagent.Where(String.Format(@"PAYMENT_SR_NO == {0}", PAYMENT_SR_NO));
            return moneytransagent.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_MONEY_TRANSFER_AGENT> GetMoneyAgentDetails1(int TOUR_ID)
        {
            moneytransagent = moneytransagent.Where(String.Format(@"TOUR_ID == {0}", TOUR_ID));
            return moneytransagent.ToList();
        }
        [WebMethod(EnableSession = true)]
        public int GetMONEYCount()
        {

            return (int)moneytransagent.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateBookingPaymentDetails1(ArrayList paymentary)
        {
            CRM.DataAccess.AdministratorEntity.MoneyTransfer objcontdet = new CRM.DataAccess.AdministratorEntity.MoneyTransfer();
            paymentary.Insert(20, Session["usersid"].ToString());
            objcontdet.InsertUpdateBookingPaymentDetails(paymentary);
        }
    }
}
