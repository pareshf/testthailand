using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.GIT
{
    public class GITSearch
    {
        public DataSet commonsp(String SpName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(SpName);
                


                ds = db.ExecuteDataSet(dbCmd);
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
            return ds;
        }


        public DataSet SearchPackages(String QUOTATION_NO, String FROM_DATE_S, String TO_DATE_S, String STATUS)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("SEARCH_GIT_PACKAGES");
               if(QUOTATION_NO.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@QUOTATION_NO", DbType.Int32, "0");
               }
               else
                {
                db.AddInParameter(dbCmd, "@QUOTATION_NO", DbType.Int32, QUOTATION_NO);
               }

               if (FROM_DATE_S.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@FROM_DATE_S", DbType.DateTime, DateTime.ParseExact(FROM_DATE_S.ToString(), "dd/MM/yyyy", null));
               }

               if (TO_DATE_S.ToString().Equals(""))
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DBNull.Value);
               }
               else
               {
                   db.AddInParameter(dbCmd, "@TO_DATE_S", DbType.DateTime, DateTime.ParseExact(TO_DATE_S.ToString(), "dd/MM/yyyy", null));
               }
                
                db.AddInParameter(dbCmd, "@STATUS", DbType.String, STATUS);


                ds = db.ExecuteDataSet(dbCmd);
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
            return ds;
        }
    }
}
