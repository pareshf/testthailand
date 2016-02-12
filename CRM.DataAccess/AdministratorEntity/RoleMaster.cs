using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;  


namespace CRM.DataAccess.AdministratorEntity
{
    public class RoleMaster
    {
        public void insertupdateRole(ArrayList Role)
        {

            Database db = null;
            DbCommand dbCmd = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_ROLE_FOR_ROLE_MASTER");
                db.AddInParameter(dbCmd,"@ROLEID",DbType.Int32,Role[0]);
                db.AddInParameter(dbCmd, "@ROLENAME", DbType.String, Role[1]);
                db.AddInParameter(dbCmd, "@BASEID", DbType.Int32, Role[2]);
                
            }
            catch(Exception ex)
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
