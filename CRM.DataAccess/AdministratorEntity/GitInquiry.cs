using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
namespace CRM.DataAccess.AdministratorEntity
{
    public class GitInquiry
    {
        public void insert_Git_Inquiry(string requirments, string noofpax,int userid)
        {
            Database db = null;
            DbCommand dbCmd = null;


            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_GIT_INQUIRY_DETAIL");

                db.AddInParameter(dbCmd, "@REQUIREMENTS", DbType.String, requirments);
                db.AddInParameter(dbCmd, "@NOOFPAX", DbType.String, noofpax);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, userid);
                db.ExecuteNonQuery(dbCmd); 
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
        }
    }
}
