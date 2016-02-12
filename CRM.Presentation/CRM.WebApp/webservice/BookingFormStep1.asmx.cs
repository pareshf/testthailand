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
    /// Summary description for BookingFormStep1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BookingFormStep1 : System.Web.Services.WebService
    {
        IQueryable<VIEW_FOR_CUSTOMER_DETAIL_NEW> Customer = new BookingFormStep1DataContext().VIEW_FOR_CUSTOMER_DETAIL_NEWs.AsQueryable<VIEW_FOR_CUSTOMER_DETAIL_NEW>();
        //IQueryable<VIEW_FOR_CUSTOMER_RELATION_NEW> Relation = new BookingFormStep1DataContext().VIEW_FOR_CUSTOMER_RELATION_NEWs.AsQueryable<VIEW_FOR_CUSTOMER_RELATION_NEW>();
        IQueryable<VIEW_FOR_CUSTOMER_RELATION_NEW> Relation = new BookingFormStep1DataContext().VIEW_FOR_CUSTOMER_RELATION_NEWs.AsQueryable<VIEW_FOR_CUSTOMER_RELATION_NEW>();   

        IQueryable<VIEW_FOR_CUSTOMER_VISA_DETAIL_NEW> Visa = new BookingFormStep1DataContext().VIEW_FOR_CUSTOMER_VISA_DETAIL_NEWs.AsQueryable<VIEW_FOR_CUSTOMER_VISA_DETAIL_NEW>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_CUSTOMER_DETAIL_NEW> GetCustomer(string inquiry_id)
        {
            Customer = Customer.Where(String.Format(@"INQUIRY_ID == {0}", inquiry_id));
            return Customer.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_CUSTOMER_RELATION_NEW> GetCustomerRelative(string cust_id)
        {
            Relation = Relation.Where(String.Format(@"CUST_ID == {0}", cust_id));
            return Relation.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_CUSTOMER_VISA_DETAIL_NEW> GetVisaDetail(string cust_rel_srno)
        {
            Visa = Visa.Where(String.Format(@"CUST_REL_SRNO == {0}", cust_rel_srno));
            return Visa.ToList();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCustomerDetail(ArrayList Customer)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep1 objBooking = new CRM.DataAccess.AdministratorEntity.BookingFormStep1();
            objBooking.UpdateCustomerDetail(Customer);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateCustomerRelationDetail(ArrayList Relation)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep1 objBooking = new CRM.DataAccess.AdministratorEntity.BookingFormStep1();
            objBooking.UpdateRelationDetail(Relation);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateVisaDetail(ArrayList Visa)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep1 objBooking = new CRM.DataAccess.AdministratorEntity.BookingFormStep1();
            objBooking.InsertUpdateVisaDetail(Visa);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewVisa(string cust_id,string cust_rel_id,string cust_rel_srno)
        {
            CRM.DataAccess.AdministratorEntity.BookingFormStep1 objBooking = new CRM.DataAccess.AdministratorEntity.BookingFormStep1();
            objBooking.NewvisaforCustomer(cust_id,cust_rel_id,cust_rel_srno);
        
        }
    }
}
