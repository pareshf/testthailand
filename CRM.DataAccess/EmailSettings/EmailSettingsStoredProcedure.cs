using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.EmailSettings
{
   public  class EmailSettingsStoredProcedure
    {

       public DataSet fetch_voucher_type(String sp_name)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);
               dsData = db.ExecuteDataSet(dbCmd);
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
           return dsData;
       }

       public void insert_update_email_master(int EMAIL_TEMPLATE_MASTER_ID, String EVENT_ID, String EMAIL_DESC, String FROM_USER_TYPE, String TO_ID_USER_TYPE, String CC_ID_USER_TYPE, String BCC_ID_USER_TYPE, String EMAIL_SUBJECT, String EMAIL_CONTENT, Boolean IS_AUTO, Boolean IS_ON, int CREATED_BY, int MODIFIED_BY)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_EMAIL_TEMPLATE_MASTER");

               db.AddInParameter(dbCmd, "@EMAIL_TEMPLATE_MASTER_ID", DbType.Int32, EMAIL_TEMPLATE_MASTER_ID);
               db.AddInParameter(dbCmd, "@EVENT_ID", DbType.String, EVENT_ID);

               db.AddInParameter(dbCmd, "@EMAIL_DESC", DbType.String, EMAIL_DESC);
               db.AddInParameter(dbCmd, "@FROM_USER_TYPE", DbType.String, FROM_USER_TYPE);
               db.AddInParameter(dbCmd, "@TO_ID_USER_TYPE", DbType.String, TO_ID_USER_TYPE);
               db.AddInParameter(dbCmd, "@CC_ID_USER_TYPE", DbType.String, CC_ID_USER_TYPE);
               db.AddInParameter(dbCmd, "@BCC_ID_USER_TYPE", DbType.String, BCC_ID_USER_TYPE);
               db.AddInParameter(dbCmd, "@EMAIL_SUBJECT", DbType.String, EMAIL_SUBJECT);
               db.AddInParameter(dbCmd, "@EMAIL_CONTENT", DbType.String, EMAIL_CONTENT);

               db.AddInParameter(dbCmd, "@IS_AUTO", DbType.Boolean, IS_AUTO);
               db.AddInParameter(dbCmd, "@IS_ON", DbType.Boolean, IS_ON);

               db.AddInParameter(dbCmd, "@CREATED_BY", DbType.String, CREATED_BY);
               db.AddInParameter(dbCmd, "@MODIFIED_BY", DbType.String, MODIFIED_BY);

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

       public DataSet search_email(String sp_name, String EVENT_NAME)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);

               db.AddInParameter(dbCmd, "@EVENT_NAME", DbType.String, EVENT_NAME);
               dsData = db.ExecuteDataSet(dbCmd);
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
           return dsData;
       }

       public DataSet fetch_data_for_edit(String sp_name, int  ID)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet dsData = null;

           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand(sp_name);

               db.AddInParameter(dbCmd, "@ID", DbType.String, ID);
               dsData = db.ExecuteDataSet(dbCmd);
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
           return dsData;
       }

    }
}
