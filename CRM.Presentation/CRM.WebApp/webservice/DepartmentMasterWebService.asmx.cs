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
    /// Summary description for DepartmentMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DepartmentMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_DEPARTMENT_MASTER> DEPARTMENT = new DepartmentMasterDataContext().VIEW_DEPARTMENT_MASTERs.AsQueryable<VIEW_DEPARTMENT_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_DEPARTMENT_MASTER> GetDepartmentName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                DEPARTMENT = DEPARTMENT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                DEPARTMENT = DEPARTMENT.OrderBy(sortExpression);
            }
            else
            {
                DEPARTMENT = DEPARTMENT.OrderBy("DEPARTMENT_ID ASC");
            }

            return DEPARTMENT.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int DepartmentNameCount()
        {
            return (int)DEPARTMENT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateDepartment(ArrayList department)
        {

            DepartmentMasterStoredProcedure objnewdeprtment = new DepartmentMasterStoredProcedure();
            department.Insert(3, Session["usersid"].ToString());
            objnewdeprtment.InsertUpdateDepartment(department);

        }
        [WebMethod(EnableSession = true)]
        public void deleteDepartmentName(int delDepartmentname)
        {
            DepartmentMasterStoredProcedure objdeldepartment = new DepartmentMasterStoredProcedure();
            objdeldepartment.deleteDepartmentName(delDepartmentname);
        }
    }
}
