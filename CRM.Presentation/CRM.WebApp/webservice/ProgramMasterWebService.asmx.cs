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
    /// Summary description for ProgramMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProgramMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_PROGRAM_MASTER> PROGRAM = new ProgramMasterDataContext().VIEW_PROGRAM_MASTERs.AsQueryable<VIEW_PROGRAM_MASTER>();
        IQueryable<VIEW_SUB_PROGRAM_MASTER_NEW> SUBPROGRAM = new ProgramMasterDataContext().VIEW_SUB_PROGRAM_MASTER_NEWs.AsQueryable<VIEW_SUB_PROGRAM_MASTER_NEW>();
        IQueryable<VIEW_FOR_DEPT_ROLE_GRID> DEPT_ROLE = new ProgramMasterDataContext().VIEW_FOR_DEPT_ROLE_GRIDs.AsQueryable<VIEW_FOR_DEPT_ROLE_GRID>();
        IQueryable<VIEW_PROGRAM_ACCESS_GRID> PROG_ACCESS = new ProgramMasterDataContext().VIEW_PROGRAM_ACCESS_GRIDs.AsQueryable<VIEW_PROGRAM_ACCESS_GRID>();
        IQueryable<VIEW_SUB_PROGRAM_ACCESS_GRID> SUB_PROG_ACCESS = new ProgramMasterDataContext().VIEW_SUB_PROGRAM_ACCESS_GRIDs.AsQueryable<VIEW_SUB_PROGRAM_ACCESS_GRID>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_PROGRAM_MASTER> GetProgramDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
           
            if (!String.IsNullOrEmpty(filterExpression))
            {
                PROGRAM = PROGRAM.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                PROGRAM = PROGRAM.OrderBy(sortExpression);
            }
            else
            {
                PROGRAM = PROGRAM.OrderBy("PROGRAM_ID DESC");
            }

            return PROGRAM.Skip(startIndex).Take(maximumRows).ToList();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateMyProgram(ArrayList Program)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.InsertUpdateMyProgram(Program);
            
        }

        [WebMethod(EnableSession = true)]
        public int GetProgramCount()
        {
            return (int)PROGRAM.Count();
        }
        [WebMethod(EnableSession = true)]
        public void DeleteMyProgram(int ProgramId)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.DeleteMyProgram(ProgramId);
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_FOR_DEPT_ROLE_GRID> GetDeptRoleDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {

            if (!String.IsNullOrEmpty(filterExpression))
            {
                DEPT_ROLE = DEPT_ROLE.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                DEPT_ROLE = DEPT_ROLE.OrderBy(sortExpression);
            }
            

            return DEPT_ROLE.Skip(startIndex).Take(maximumRows).ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_PROGRAM_ACCESS_GRID> GetProgDetails(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                PROG_ACCESS = PROG_ACCESS.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                PROG_ACCESS = PROG_ACCESS.OrderBy(sortExpression);
            }
            else
            {
                PROG_ACCESS = PROG_ACCESS.OrderBy("PROGRAM_ID ASC");
            }

            return PROG_ACCESS.Skip(startIndex).Take(maximumRows).ToList();
        }

        [WebMethod(EnableSession = true)]
        public List<VIEW_PROGRAM_ACCESS_GRID> GetProgAccessDetails(string dept_id,string role_id,string comp_id)
        {
            ProgramMasterDataContext db = new ProgramMasterDataContext();
            PROG_ACCESS = db.VIEW_PROGRAM_ACCESS_GRIDs.Where("COMPANY_ID == @0 && DEPT_ID == @1 && ROLE_ID == @2", Convert.ToInt32(comp_id),Convert.ToInt32(dept_id),Convert.ToInt32(role_id));
            PROG_ACCESS = PROG_ACCESS.OrderBy("PROGRAM_ID ASC");
            return PROG_ACCESS.ToList();
            
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUB_PROGRAM_ACCESS_GRID> GetSubProgAccessDetails(string dept_id, string role_id,string comp_id)
        {
            ProgramMasterDataContext db = new ProgramMasterDataContext();
            SUB_PROG_ACCESS = db.VIEW_SUB_PROGRAM_ACCESS_GRIDs.Where("COMPANY_ID == @0 && DEPT_ID == @1 && ROLE_ID == @2", Convert.ToInt32(comp_id), Convert.ToInt32(dept_id), Convert.ToInt32(role_id));
            return SUB_PROG_ACCESS.ToList();

        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_SUB_PROGRAM_MASTER_NEW> GetSubProgramDetails(string PROGRAM_ID)
        {
            SUBPROGRAM = SUBPROGRAM.Where(String.Format(@"PROGRAM_ID == {0}", PROGRAM_ID));
            return SUBPROGRAM.ToList();

        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateSubProgram(ArrayList SubProgram)
        {
            NewProgramMaster objsubprogram = new NewProgramMaster();
            objsubprogram.InsertUpdateSubProgram(SubProgram);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateProgramAccess(ArrayList ary)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.InsertUpdateProgAccess(ary);
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateSubProgramAccess(ArrayList ary)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.InsertUpdateSubProgAccess(ary);
        }

        [WebMethod(EnableSession = true)]
        public void InsertNewProgramAccess(string dept_id, string role_id)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.InsertNewProgramAccess(dept_id, role_id);
        }
        
        [WebMethod(EnableSession = true)]
        public void InsertNewSubProgramAccess(string dept_id, string role_id)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.InsertNewSubProgramAccess(dept_id, role_id);
        }

        [WebMethod(EnableSession = true)]
        public int InsertNewDeptRole(string dept, string role,string comp)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            int i = objprogram.InsertNewDeptRole(dept,role,comp);
           
            return i;
        }

        [WebMethod(EnableSession = true)]
        public int GetCount()
        {
            return (int)DEPT_ROLE.Count();
        }

        [WebMethod(EnableSession = true)]
        public int GetProgramAccessCount()
        {
            return (int)PROG_ACCESS.Count();
        }

        [WebMethod(EnableSession = true)]
        public void DeleteSubProgram(int SubProgramId)
        {
            NewProgramMaster objprogram = new NewProgramMaster();
            objprogram.DeleteSubProgram(SubProgramId);
        }
    }
    
}
