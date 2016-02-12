using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;
using System.Data.SqlClient;

namespace CRM.DataAccess.AdministratorEntity
{
   public  class BusinessCompanyStoredProcedure
    {
       public void InsertUpdateBusinessCompany(ArrayList Bcompany)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_FOR_COMPANY_MASTER");
               db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, Bcompany[1]);
               db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, Bcompany[0]);
               db.AddInParameter(dbCmd, "@IS_COMPANY_FRANCHISE", DbType.String, Bcompany[2]);
               db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, Bcompany[3]);
               db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, Bcompany[4]);
               db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, Bcompany[5]);
               db.AddInParameter(dbCmd, "@STATE_NAME", DbType.String, Bcompany[6]);
               db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, Bcompany[7]);
               db.AddInParameter(dbCmd, "@PINCODE", DbType.String, Bcompany[8]);
               db.AddInParameter(dbCmd, "@MOBILE", DbType.String, Bcompany[9]);
               db.AddInParameter(dbCmd, "@PHONE", DbType.String, Bcompany[10]);
               db.AddInParameter(dbCmd, "@FAX", DbType.String, Bcompany[11]);
               db.AddInParameter(dbCmd, "@EMAIL_ID", DbType.String, Bcompany[12]);
               db.AddInParameter(dbCmd, "@REGION_NAME", DbType.String, Bcompany[13]);
               db.AddInParameter(dbCmd, "@PARENT_COMPANY", DbType.String, Bcompany[14]);
               db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Bcompany[15]));
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
       public void delBusinessCompany(int delCompany)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("DELETE_FROM_COMPANY_MASTER");
               db.AddInParameter(dbCmd, "@COMPANYID", DbType.Int32, delCompany);
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
