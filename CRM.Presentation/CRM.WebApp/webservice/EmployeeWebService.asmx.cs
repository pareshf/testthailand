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

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for EmployeeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeeWebService : System.Web.Services.WebService
    {
       
        //IQueryable<VIEW_USER_MASTER_FOR_EMPLOYEE> user = new EmployeeDataBaseDataContext().VIEW_USER_MASTER_FOR_EMPLOYEEs.AsQueryable<VIEW_USER_MASTER_FOR_EMPLOYEE>();
        //IQueryable<VIEW_ROLE_MASTER_NEW> Role = new EmployeeDataBaseDataContext().VIEW_ROLE_MASTER_NEWs.AsQueryable<VIEW_ROLE_MASTER_NEW>();
        //IQueryable<VIEW_CONTACT_DETAIL_FOR_EMPLOYEE> EmpContect = new EmployeeDataBaseDataContext().VIEW_CONTACT_DETAIL_FOR_EMPLOYEEs.AsQueryable<VIEW_CONTACT_DETAIL_FOR_EMPLOYEE>();
        //IQueryable<VIEW_COMPANY_OF_EMPLOYEE_NEW> Company = new EmployeeDataBaseDataContext().VIEW_COMPANY_OF_EMPLOYEE_NEWs.AsQueryable<VIEW_COMPANY_OF_EMPLOYEE_NEW>();
        //IQueryable<SYS_ROLE_MASTER> AllRole = new EmployeeDataBaseDataContext().SYS_ROLE_MASTERs.AsQueryable<SYS_ROLE_MASTER>();
        //IQueryable<COMPANY_MASTER> AllCompany = new EmployeeDataBaseDataContext().COMPANY_MASTERs.AsQueryable<COMPANY_MASTER>();
        //IQueryable<VIEW_EMPLOYEE_MASTER_NEW> employee = new EmployeeDataBaseDataContext().VIEW_EMPLOYEE_MASTER_NEWs.AsQueryable<VIEW_EMPLOYEE_MASTER_NEW>();
        
        IQueryable<VIEW_USER_MASTER_FOR_EMPLOYEE> user = new NewEmployeeDbmlDataContext().VIEW_USER_MASTER_FOR_EMPLOYEEs.AsQueryable<VIEW_USER_MASTER_FOR_EMPLOYEE>();
        IQueryable<VIEW_ROLE_MASTER_NEW> Role = new NewEmployeeDbmlDataContext().VIEW_ROLE_MASTER_NEWs.AsQueryable<VIEW_ROLE_MASTER_NEW>();
        IQueryable<VIEW_CONTACT_DETAIL_FOR_EMPLOYEE> EmpContect = new NewEmployeeDbmlDataContext().VIEW_CONTACT_DETAIL_FOR_EMPLOYEEs.AsQueryable<VIEW_CONTACT_DETAIL_FOR_EMPLOYEE>();
        IQueryable<VIEW_COMPANY_OF_EMPLOYEE_NEW> Company = new NewEmployeeDbmlDataContext().VIEW_COMPANY_OF_EMPLOYEE_NEWs.AsQueryable<VIEW_COMPANY_OF_EMPLOYEE_NEW>();
        IQueryable<SYS_ROLE_MASTER> AllRole = new NewEmployeeDbmlDataContext().SYS_ROLE_MASTERs.AsQueryable<SYS_ROLE_MASTER>();
        IQueryable<COMPANY_MASTER> AllCompany = new NewEmployeeDbmlDataContext().COMPANY_MASTERs.AsQueryable<COMPANY_MASTER>();
        IQueryable<VIEW_EMPLOYEE_MASTER_NEW> employee = new NewEmployeeDbmlDataContext().VIEW_EMPLOYEE_MASTER_NEWs.AsQueryable<VIEW_EMPLOYEE_MASTER_NEW>();
        IQueryable<VIEW_EMP_ROLE_MAPPING> EMP_ACCESS = new NewEmployeeDbmlDataContext().VIEW_EMP_ROLE_MAPPINGs.AsQueryable<VIEW_EMP_ROLE_MAPPING>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_EMPLOYEE_MASTER_NEW> GetEmployee(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                employee = employee.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                employee = employee.OrderBy(sortExpression);
            }
            else
            {
                employee = employee.OrderBy("EMP_ID DESC");
            }
            int cnt = employee.Count();
            HttpContext.Current.Session["CustomersCount"] = employee.Count();
            return employee.Skip(startIndex).Take(maximumRows).ToList();
        }
        [WebMethod(EnableSession = true)]
        public int GetCustomersCount()
        {
            return (int)employee.Count();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_USER_MASTER_FOR_EMPLOYEE> GetDetailsByEMP_ID(string EMP_ID)
        {
            user = user.Where(String.Format(@"EMP_ID == {0}", EMP_ID));
            HttpContext.Current.Session["OrdersByCustomerCount"] = user.Count();
            return user.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_CONTACT_DETAIL_FOR_EMPLOYEE> GetContectByEMP_ID(string EMP_ID)
        {
                EmpContect = EmpContect.Where(String.Format(@"EMP_ID == {0}", EMP_ID));
                return EmpContect.ToList();
        }
        [WebMethod(EnableSession = true)]
        public int GetOrdersByEMP_IDCount()
        {
            return (int)HttpContext.Current.Session["OrdersByCustomerCount"];
        }

        // newly added ---getting all role list 
        [WebMethod(EnableSession = true)]
        public List<VIEW_ROLE_MASTER_NEW> GetRolebyEmpID(string EMP_ID)
        {
            Role = Role.Where(String.Format(@"EMP_ID == {0}", EMP_ID));
            HttpContext.Current.Session["RoleCount"] = Role.Count();
            return Role.ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_COMPANY_OF_EMPLOYEE_NEW> GetCompanybyEmpID(string EMP_ID)
        {
            Company = Company.Where(String.Format(@"EMP_ID == {0}", EMP_ID));
            HttpContext.Current.Session["CompanyCount"] = Company.Count();
            return Company.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_EMP_ROLE_MAPPING> GetMyRole(string EMP_ID)
        {
            EMP_ACCESS = EMP_ACCESS.Where(String.Format(@"EMP_ID=={0}", EMP_ID));
            return EMP_ACCESS.ToList();

        }
        [WebMethod(EnableSession = true)]
        public List<SYS_ROLE_MASTER> GetAllRole()
        {
            return AllRole.ToList();
        }
        [WebMethod(EnableSession = true)]
        public List<COMPANY_MASTER> GetAllCompany()
        {
            return AllCompany.ToList();
        }

        //newly Added function from ronak adding new record at last
        [WebMethod(EnableSession = true)]
        public void DeleteEmployeeByEmployeeID(int employeeID)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.deleteemployee(employeeID);
        }

        [WebMethod(EnableSession = true)]
        public string InsertUpdateEmployee(ArrayList employee ,string m)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            employee.Insert(14, Session["usersid"].ToString());
            //objempentity.InsertUpdateEmployee(employee);

            System.Data.DataSet ds1 = objempentity.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            {

                if ((ds1.Tables[2].Rows[i]["EMP_NAME"].ToString().Equals(employee[3]+" "+employee[2]) || (employee[3]+" "+employee[2]).Equals("")))
                {
                    j = 1;
                    break;

                }

            }
            if (j == 0)
            {
                objempentity.InsertUpdateEmployee(employee);
            }
            else if (j == 1)
            {
                System.Data.DataSet ds = objempentity.InsertUpdateEmployee(employee);
                m = ds.Tables[0].Rows[0]["SUPPLIER_CHECKER"].ToString();
                if (m == "N")
                {

                }
                if (m == "Y")
                {

                }
            }
            return m;
        }
        [WebMethod(EnableSession=true)]
        public void InsertUserRoleForEmployee(ArrayList MyRole)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.InsertUserRoleForEmployee(MyRole);

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateEmployeeContectDetails(ArrayList contect)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            contect.Insert(10, Session["usersid"].ToString());
            objempentity.InsertUpdateEmployeeContectDetails(contect);
        }

        [WebMethod(EnableSession = true)]
        public string UpdateUserDetails(ArrayList employee,string s)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            employee.Insert(6, Session["usersid"].ToString());
           // objempentity.UpdateUserDetails(employee);

            System.Data.DataSet ds1 = objempentity.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(employee[1]) || employee[1].Equals(""))
                {
                    j = 1;
                    break;

                }

            }
            if (j == 0)
            {


                objempentity.UpdateUserDetails(employee);
            }
            else if (j == 1)
            {


                System.Data.DataSet ds = objempentity.UpdateUserDetails(employee);

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
        
        //get count of records for roles and company
        [WebMethod(EnableSession = true)]
        public int rolecount()
        {
            return (int)Role.Count();
        }
        [WebMethod(EnableSession = true)]
        public int companycount()
        {
            return (int)Company.Count();
        }
        [WebMethod(EnableSession = true)]
        public int allrolecount()
        {
            return (int)AllRole.Count();
        }
        [WebMethod(EnableSession = true)]
        public int allcompanycount()
        {
            return (int)AllCompany.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertAssignCompany(string CompanyId, int UserID)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.InsertAssignCompany(CompanyId, UserID, int.Parse(Session["usersid"].ToString()));

        }
        [WebMethod(EnableSession = true)]
        public void UnAssignCompany(string CompanyId, int UserID)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.UnAssignCompany(CompanyId, UserID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertAssignRole(int UserId, int CompanyId, int RoleId)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.InsertAssignRole(UserId, CompanyId, RoleId);
        }
        [WebMethod(EnableSession = true)]
        public void UnAssignRole(int UserID, int CompanyId, int RoleId)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.UnAssignRole(UserID, CompanyId, RoleId);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteEmployeeRole(int user_id, string comp_name, string dept_name, string role_name)
        {
            EmployeemasterStoreprocedures objempentity = new EmployeemasterStoreprocedures();
            objempentity.DeleteEmployeeRole(user_id, comp_name, dept_name, role_name);
        }
    }
}
