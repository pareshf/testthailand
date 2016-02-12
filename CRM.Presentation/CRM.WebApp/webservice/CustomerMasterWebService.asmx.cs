using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.Dashboard;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for CustomerMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CustomerMasterWebService : System.Web.Services.WebService {
        IQueryable<VIEW_CUST_CUSTOMER_MASTER> Cust = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_MASTERs.AsQueryable<VIEW_CUST_CUSTOMER_MASTER>();
        IQueryable<VIEW_CUST_CUSTOMER_CONTACT_DETAIL> Contdet = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_CONTACT_DETAILs.AsQueryable<VIEW_CUST_CUSTOMER_CONTACT_DETAIL>();
        IQueryable<VIEW_CUST_CUSTOMER_RELATION> rel = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_RELATIONs.AsQueryable<VIEW_CUST_CUSTOMER_RELATION>();
        IQueryable<VIEW_CUST_CUSTOMER_MESSAGE> Message = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_MESSAGEs.AsQueryable<VIEW_CUST_CUSTOMER_MESSAGE>();
        IQueryable<VIEW_CUST_CUSTOMER_DOCUMENT> Document = new CustomerMasterDataContext().VIEW_CUST_CUSTOMER_DOCUMENTs.AsQueryable<VIEW_CUST_CUSTOMER_DOCUMENT>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_MASTER> GetCust(int startIndex, int maximumRows, string sortExpression, string filterExpression, string scomapany, string scity, string scode, string stype, string sbranch, string semp, string scommode, string suniquetid, string sfname, string slname, string semail, string smob, string stele)
        {
                                                                                                                                                            
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Cust = Cust.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Cust = Cust.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(scomapany))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_COMPANY_NAME.Contains(scomapany)));
            }
            if (!String.IsNullOrEmpty(scity))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CITY_NAME.Contains(scity)));
            }
            if (!String.IsNullOrEmpty(scode))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_CODE_NAME.Contains(scode)));
            }
            if (!String.IsNullOrEmpty(stype))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_TYPE_NAME.Contains(stype)));
            }
            if (!String.IsNullOrEmpty(sbranch))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.BRANCH.Contains(sbranch)));
            }
            if (!String.IsNullOrEmpty(semp))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.EMPLOYEE.Contains(semp)));
            }
            if (!String.IsNullOrEmpty(scommode))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.COMMUNICATION_MODE_NAME.Contains(scommode)));
            }
            //if (!String.IsNullOrEmpty(srelation))
            //{
            //    CustomerMasterDataContext db = new CustomerMasterDataContext();
            //    Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.RELATION_DESC.Contains(srelation)));
            //}
            if (!String.IsNullOrEmpty(suniquetid))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_UNQ_ID.Contains(suniquetid)));
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_SURNAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(slname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_NAME.Contains(slname)));
            }
            if (!String.IsNullOrEmpty(semail))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_EMAIL.Contains(semail)));
            }
            if (!String.IsNullOrEmpty(smob))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_MOBILE.Contains(smob)));
            }
            if (!String.IsNullOrEmpty(stele))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_PHONE.Contains(stele)));
            }
            // if ((!String.IsNullOrEmpty(sfname)) || (!String.IsNullOrEmpty(slname)) || (!String.IsNullOrEmpty(semail)) || (!String.IsNullOrEmpty(smob)) || (!String.IsNullOrEmpty(stele)))
            //{
                 
            //    CustomerMasterDataContext db = new CustomerMasterDataContext();
            //    Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_SURNAME.Contains(sfname)) || (p.CUST_NAME.Contains(slname)) || (p.CUST_REL_EMAIL.Contains(semail)) || (p.CUST_REL_PHONE.Contains(stele)) || (p.CUST_REL_MOBILE.Contains(smob)));
            //}
                //DateTime startDateRange = new DateTime();
                //DateTime endDateRange = new DateTime(); 

                // if (chkdate.Checked && DateTime.TryParse(filterBirthDateStart.Text, out startDateRange) && DateTime.TryParse(filterBirthDateEnd.Text, out endDateRange)) {
                //      Cust = Cust.And(e => e.BirthDate.Value >= startDateRange && e.BirthDate.Value <= endDateRange);


            else
            {
                Cust = Cust.OrderBy("CUST_ID");
            }
            return Cust.Skip(startIndex).Take(maximumRows).ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_MASTER> GetCustwithID(string CUST_ID)
        {
            Cust = Cust.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return Cust.ToList();
        }
        [WebMethod(EnableSession=true)]
        public string InsertUpdateCust(ArrayList Cust,string s)
        {
            
            CustomerMasterStoredProcedure objcust = new CustomerMasterStoredProcedure();
            Cust.Insert(35, Session["usersid"].ToString());
            //objcust.InsertUpdateCust(Cust);
            System.Data.DataSet ds1 = objcust.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(Cust[6]) || Cust[6].Equals(""))
                {
                    j = 1;
                    break;

                }

            }
            if (j == 0)
            {

                objcust.InsertUpdateCust(Cust);

            }
            else if (j == 1)
            {

               
                System.Data.DataSet ds = objcust.InsertUpdateCust(Cust);

                s = ds.Tables[0].Rows[0]["EMAIL_CHECKER"].ToString();
                if (s == "N")
                {

                }
                if (s == "Y")
                {

                }

            }
            else
            {

            }
            return s;
            
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateContdet(ArrayList Contdetail)
        {
            CustomerMasterStoredProcedure objcontdet = new CustomerMasterStoredProcedure();
            Contdetail.Insert(13, Session["usersid"].ToString());
            objcontdet.InsertUpdateContdet(Contdetail);
        }

        [WebMethod(EnableSession = true)]
        public string InsertUpdateRelation(ArrayList Cust,string s)
        {
            CustomerMasterStoredProcedure objrel = new CustomerMasterStoredProcedure();
            Cust.Insert(20, Session["usersid"].ToString());
            //objrel.InsertUpdateRel(Cust);
            System.Data.DataSet ds1 = objrel.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(Cust[8]) || Cust[8].Equals(""))
                {
                    j = 1;
                    break;

                }

            }
            if (j == 0)
            {
               
                 objrel.InsertUpdateRel(Cust);
               
            }
            else if (j == 1)
            {

                System.Data.DataSet ds = objrel.InsertUpdateRel(Cust);
                s = ds.Tables[0].Rows[0]["EMAIL_CHECKER"].ToString();
                if (s == "N")
                {

                }
                if (s == "Y")
                {

                }
            }
            else
            {

            }
            return s;
     
        }

        [WebMethod(EnableSession = true)]
        public int GetCustCount()
        {
            return (int)Cust.Count();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_CONTACT_DETAIL> ContdetGrid(string CUST_ID)
        {
            Contdet = Contdet.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return Contdet.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_MESSAGE> sms(string CUST_ID)
        {
            Message = Message.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return Message.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_RELATION> RelGrid(string CHAIN_SR_NO, string scity, string suniquetid, string sfname,string slname,string semail,string smob,string stele)
        {
            if (!String.IsNullOrEmpty(suniquetid))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_UNQ_ID.Contains(suniquetid)));
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_SURNAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(slname))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_NAME.Contains(slname)));
            }
            if (!String.IsNullOrEmpty(semail))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_EMAIL.Contains(semail)));
            }
            if (!String.IsNullOrEmpty(smob))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_MOBILE.Contains(smob)));
            }
            if (!String.IsNullOrEmpty(stele))
            {
                CustomerMasterDataContext db = new CustomerMasterDataContext();
                Cust = db.VIEW_CUST_CUSTOMER_MASTERs.Where(p => (p.CUST_REL_PHONE.Contains(stele)));
            }
            else
            {
                //rel = rel.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
               
            }
            rel = rel.Where(String.Format(@"CHAIN_SR_NO == {0}", CHAIN_SR_NO));
            return rel.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int GetCustomerCount()
        {

            return (int)Contdet.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetCustomerMsgCount()
        {

            return (int)Message.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetRelCount()
        {

            return (int)rel.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewDetail(string CUST_ID, string CUST_REL_ID)
        {
            CustomerMasterStoredProcedure objnewid = new CustomerMasterStoredProcedure();
            objnewid.InsertNewDetail(CUST_ID, CUST_REL_ID);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewRel(string CUST_ID, string CUST_REL_ID,string CHAIN_SR_NO)
        {
            CustomerMasterStoredProcedure objnewRel = new CustomerMasterStoredProcedure();
            objnewRel.InsertNewRelation(CUST_ID, CUST_REL_ID,CHAIN_SR_NO);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteCustomer(int CUST_ID)
        {
            CustomerMasterStoredProcedure objempentity = new CustomerMasterStoredProcedure();
            objempentity.deletecust(CUST_ID);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteContact(int SR_NO)
        {
            CustomerMasterStoredProcedure objempentity = new CustomerMasterStoredProcedure();
            objempentity.DeleteContact(SR_NO);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteRel(int CUST_REL_SRNO)
        {
            CustomerMasterStoredProcedure objempentity = new CustomerMasterStoredProcedure();
            objempentity.DeleteRel(CUST_REL_SRNO);
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_CUST_CUSTOMER_DOCUMENT> doc(string CUST_ID)
        {
            Document = Document.Where(String.Format(@"CUST_ID == {0}", CUST_ID));
            return Document.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void insertupdatedoc(ArrayList doc)
        {
            CustomerMasterStoredProcedure objdocditail = new CustomerMasterStoredProcedure();
            objdocditail.insertdoc(doc);
        }
        [WebMethod(EnableSession = true)]
        public void insertnewDocument(string docid)
        {
            CustomerMasterStoredProcedure objdocditail = new CustomerMasterStoredProcedure();
            objdocditail.insertnewdoc(docid);
        }
        [WebMethod(EnableSession = true)]
        public void AgentLogin(string empid)
        {
            CustomerMasterStoredProcedure objAgentlogin = new CustomerMasterStoredProcedure();
            //System.IO.File.Copy
            System.Data.DataSet ds = objAgentlogin.AgentLogin(empid);
            string CUST_ID = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
            Session["CUST_ID"] = CUST_ID;
        }
        [WebMethod(EnableSession = true)]
        public void InsertCode(string CUST_COMPANY_NAME, string CUST_ID, string Agent)
        {
            CustomerMasterStoredProcedure objinsertGlCode = new CustomerMasterStoredProcedure();
            objinsertGlCode.InsertNewGenerateCode(CUST_COMPANY_NAME, CUST_ID, Agent);
        }
    }
}
