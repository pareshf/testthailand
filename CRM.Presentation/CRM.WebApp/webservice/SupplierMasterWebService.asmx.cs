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
    /// Summary description for SupplierMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierMasterWebService : System.Web.Services.WebService
    {


        IQueryable<VIEW_FOR_SUPPLIER_MASTER_NEW> Supply = new NewSupplierDataContext().VIEW_FOR_SUPPLIER_MASTER_NEWs.AsQueryable<VIEW_FOR_SUPPLIER_MASTER_NEW>();
        IQueryable<VIEW_SUPPLIER_CONTECT_DETAIL> Contect = new NewSupplierDataContext().VIEW_SUPPLIER_CONTECT_DETAILs.AsQueryable<VIEW_SUPPLIER_CONTECT_DETAIL>();
        IQueryable<VIEW_SUPPLIER_EMPLOYEE_DETAIL> Employee = new NewSupplierDataContext().VIEW_SUPPLIER_EMPLOYEE_DETAILs.AsQueryable<VIEW_SUPPLIER_EMPLOYEE_DETAIL>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_SUPPLIER_MASTER_NEW> GetSupplier(int startIndex, int maximumRows, string sortExpression, string filterExpression, string scommode, string sfname, string slname, string semail, string smob, string stele)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                Supply = Supply.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                Supply = Supply.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(scommode))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.COMMUNICATION_MODE_NAME.Contains(scommode)));
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.SUPPLIER_COMPANY_NAME.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(slname))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.SUPPLIER_TYPE_NAME.Contains(slname)));
            }
            if (!String.IsNullOrEmpty(semail))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.SUPPLIER_REL_EMAIL.Contains(semail)));
            }
            if (!String.IsNullOrEmpty(smob))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.SUPPLIER_REL_MOBILE.Contains(smob)));
            }
            if (!String.IsNullOrEmpty(stele))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Supply = db.VIEW_FOR_SUPPLIER_MASTER_NEWs.Where(p => (p.SUPPLIER_REL_PHONE.Contains(stele)));
            }
            else
            {
                Supply = Supply.OrderBy("SUPPLIER_ID ASC");
            }

            return Supply.Skip(startIndex).Take(maximumRows).ToList();

        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_SUPPLIER_MASTER_NEW> GetCustDetailWithID(string SUPPLIER_ID)
        {
            Supply = Supply.Where(String.Format(@"SUPPLIER_ID == {0}", SUPPLIER_ID));
            return Supply.ToList();
        }

        [WebMethod(EnableSession = true)]
        public int GetSupplierCount()
        {
            return (int)Supply.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetContectDetailCount()
        {
            return (int)Contect.Count();
        }
        [WebMethod(EnableSession = true)]
        public int GetEmployeeCount()
        {
            return (int)Employee.Count();
        }

        [WebMethod(EnableSession = true)]
        public string InsertUpdateSupplier(ArrayList Supply,string s)
        {

            SupplierMasterStoredProcedure objsupplier = new SupplierMasterStoredProcedure();
            Supply.Insert(18, Session["usersid"].ToString());
            //objsupplier.InsertUpdateSupplier(Supply);

            System.Data.DataSet ds1 = objsupplier.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(Supply[15]) || Supply[15].Equals(""))
                {
                    j = 1;
                    break;

                }

            }

            if (j == 0)
            {


                objsupplier.InsertUpdateSupplier(Supply);
            }
            else if (j == 1)
            {

               
                System.Data.DataSet ds = objsupplier.InsertUpdateSupplier(Supply);

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
        public void deleteSupplier(int SUPPLIERID)
        {
            SupplierMasterStoredProcedure objsupplier = new SupplierMasterStoredProcedure();
            objsupplier.deleteSupplier(SUPPLIERID);
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_CONTECT_DETAIL> Contectdetail(string SUPPLIER_ID, string city, string state, string country)
        {

            if (!String.IsNullOrEmpty(city))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Contect = db.VIEW_SUPPLIER_CONTECT_DETAILs.Where(p => (p.CITY_NAME.Contains(city)));
            }
            if (!String.IsNullOrEmpty(state))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Contect = db.VIEW_SUPPLIER_CONTECT_DETAILs.Where(p => (p.STATE_NAME.Contains(state)));
            }
            if (!String.IsNullOrEmpty(country))
            {
                NewSupplierDataContext db = new NewSupplierDataContext();
                Contect = db.VIEW_SUPPLIER_CONTECT_DETAILs.Where(p => (p.COUNTRY_NAME.Contains(country)));
            }
            Contect = Contect.Where(String.Format(@"SUPPLIER_ID == {0}", SUPPLIER_ID));
            return Contect.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUPPLIER_EMPLOYEE_DETAIL> EmployeeDetail(string SUPPLIER_SR_NO)
        {
            Employee = Employee.Where(String.Format(@"SUPPLIER_SR_NO == {0}", SUPPLIER_SR_NO));
            return Employee.ToList();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSupplierContect(ArrayList Contect)
        {

            SupplierMasterStoredProcedure objinsertcontect = new SupplierMasterStoredProcedure();
            Contect.Insert(19, Session["usersid"].ToString());
            objinsertcontect.InsertUpdateSupplierContect(Contect);

        }
        [WebMethod(EnableSession = true)]
        public void deleteSupplierContect(int Contectid)
        {
            SupplierMasterStoredProcedure objsupplierdel = new SupplierMasterStoredProcedure();
            objsupplierdel.deleteSupplierContect(Contectid);
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewContect(string SUPPLIER_ID)
        {
            SupplierMasterStoredProcedure objaddnew = new SupplierMasterStoredProcedure();
            objaddnew.InsertNewContect(SUPPLIER_ID);
        }
        [WebMethod(EnableSession = true)]
        public string InsertUpdateSupplierEmployeeNew(ArrayList Employee,string s)
        {

            CRM.DataAccess.AdministratorEntity.SupplierMasterStoredProcedure objinsertemployee = new CRM.DataAccess.AdministratorEntity.SupplierMasterStoredProcedure();
            Employee.Insert(10, Session["usersid"].ToString());
            //objinsertemployee.InsertUpdateSupplierEmployeeNew(Employee);

            System.Data.DataSet ds1 = objinsertemployee.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(Employee[4]) || Employee[4].Equals(""))
                {
                    j = 1;
                    break;

                }

            }

            if (j == 0)
            {


                objinsertemployee.InsertUpdateSupplierEmployeeNew(Employee);
            }
            else if (j == 1)
            {

                
                System.Data.DataSet ds = objinsertemployee.InsertUpdateSupplierEmployeeNew(Employee);
                
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
        public void InsertNewEmployee(string SUPPLIER_SR_NO)
        {
            SupplierMasterStoredProcedure objaddnewemployee = new SupplierMasterStoredProcedure();
            objaddnewemployee.InsertNewEmployee(SUPPLIER_SR_NO);
        }
        [WebMethod(EnableSession = true)]
        public void deleteSupplierEmployee(int employee)
        {
            SupplierMasterStoredProcedure objsupplieremployeedel = new SupplierMasterStoredProcedure();
            objsupplieremployeedel.deleteSupplierEmployee(employee);
        }
        [WebMethod(EnableSession = true)]
        public void InsertCode(string SUPPLIER_COMPANY_NAME,string SUPPLIER_ID ,string Supplier)
        {
            SupplierMasterStoredProcedure objinsertcode = new SupplierMasterStoredProcedure();
            objinsertcode.InsertNewGenerateCode(SUPPLIER_COMPANY_NAME,SUPPLIER_ID,Supplier);
        }
    }
}
