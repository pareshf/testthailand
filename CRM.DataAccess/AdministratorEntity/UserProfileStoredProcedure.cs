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
    public class UserProfileStoredProcedure
    {
        public void InsertUpdateProfileInfo(ArrayList Profile)
        {
            Database db = null;
            DbCommand dbCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_USER_PROFILE");

                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(Profile[14]));
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, Convert.ToInt32(Profile[13]));
                db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, Profile[12]);
                db.AddInParameter(dbCmd, "@SIGNATURE_PW", DbType.String, Profile[11]);
                db.AddInParameter(dbCmd, "@STATUS_NAME1", DbType.String, Profile[10]);
                db.AddInParameter(dbCmd, "@EMP_PHONE", DbType.String, Profile[9]);
                db.AddInParameter(dbCmd, "@EMP_MOBILE", DbType.String, Profile[8]);
                db.AddInParameter(dbCmd, "@QUALIFICATION_NAME1", DbType.String, Profile[7]);
                db.AddInParameter(dbCmd, "@GENDER_NAME1", DbType.String, Profile[6]);
                db.AddInParameter(dbCmd, "@MARITAL_STATUS_NAME1", DbType.String, Profile[5]);
                if (Profile[4].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@DATE_OF_JOINING", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@DATE_OF_JOINING", DbType.DateTime, DateTime.ParseExact(Profile[4].ToString(), "dd/MM/yyyy", null));
                }
                if (Profile[3].ToString().Equals("0"))
                {
                    db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, DateTime.ParseExact(Profile[3].ToString(), "dd/MM/yyyy", null));
                }
               
                db.AddInParameter(dbCmd, "@EMP_SURNAME", DbType.String, Profile[2]);
                db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, Profile[1]);
                db.AddInParameter(dbCmd, "@TITLE_DESC1", DbType.String, Profile[0]);
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
        public void UpdateUserDetails(ArrayList UserDetail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            //DataSet ds = null; 
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_USER_DETAIL_FOR_MY_PROFILE_NEW");

                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, UserDetail[0]);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, UserDetail[1]);
                db.AddInParameter(dbCmd, "@SEQ_QUESTION", DbType.String, UserDetail[2]);
                db.AddInParameter(dbCmd, "@SEQ_ANSWER", DbType.String, UserDetail[3]);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, Convert.ToInt32(UserDetail[4]));
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, UserDetail[5]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(UserDetail[6]));
                db.ExecuteNonQuery(dbCmd);
                //ds = db.ExecuteDataSet(dbCmd);
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
            //return ds;
        }
        public DataSet CheckValidation()
        {
            Database db = null;
            DbCommand dbCmd = null;

            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("EMAIL_VALIDATION_FOR_LOGIN");
            DataSet ds1 = db.ExecuteDataSet(dbCmd);
            return ds1;

        }
        public DataSet UpdateUserDetailValidation(ArrayList UserDetail)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("UPDATE_USER_DETAIL_FOR_MY_PROFILE");

                db.AddInParameter(dbCmd, "@USER_NAME", DbType.String, UserDetail[0]);
                db.AddInParameter(dbCmd, "@PASSWORD", DbType.String, UserDetail[1]);
                db.AddInParameter(dbCmd, "@SEQ_QUESTION", DbType.String, UserDetail[2]);
                db.AddInParameter(dbCmd, "@SEQ_ANSWER", DbType.String, UserDetail[3]);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, Convert.ToInt32(UserDetail[4]));
                db.AddInParameter(dbCmd, "@FLAG", DbType.String, UserDetail[5]);
                db.AddInParameter(dbCmd, "@USER", DbType.Int32, Convert.ToInt32(UserDetail[6]));
                //db.ExecuteNonQuery(dbCmd);
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
