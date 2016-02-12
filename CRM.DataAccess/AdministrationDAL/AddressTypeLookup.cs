#region Program Information
/**********************************************************************************************************************************************
 Class Name           : AddressTypeLookup
 Class Description    : Implementation Logic AddressTypeLookup database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 19, 2010
***********************************************************************************************************************************************/
#endregion


#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
#endregion


namespace CRM.DataAccess.AdministrationDAL
{
    public class AddressTypeLookupDal : IDisposable
    {

        #region Get AddressType
        /// <summary>
        /// Gets AddressType list.
        /// </summary>
        /// <returns>Returns dataset contains AddressType data.</returns>
        public DataSet GetAddressType(String AddressName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ADDRESS_TYPE_SELECT);
                if (!String.IsNullOrEmpty(AddressName))
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_NAME", DbType.String, AddressName);
                else
                    db.AddInParameter(dbCmd, "@ADDRESS_TYPE_NAME", DbType.String, DBNull.Value);
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

        #endregion

        #region Insert Address
        /// <summary>
        /// Insert Address detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAddressType(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ADDRESS_TYPE_INSERT);
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
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
            return Result;
        }
        #endregion

        #region DeleteAddressType
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">AddressType Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAddressType(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ADDRESS_TYPE_DELETE);
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }
        #endregion

        #region Update  Address
        /// <summary>
        /// Update Address detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateAddress(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ADDRESS_TYPE_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlData);
                db.AddOutParameter(dbCmd, "@IS_EXIST", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_EXIST"));
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
            return Result;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion

        #region Added By Pratik Trainee from 17/1/2011

        #region City Master, 21/1/2011

            #region Get CityName
        /// <summary>
        /// Gets City list.
        /// </summary>
        /// <returns>Returns dataset contains City data.</returns>
        public DataSet GetCityName(String CityName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATION_COMMON_TEST_MASTER_SELECT");
                if (!String.IsNullOrEmpty(CityName))
                    db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, CityName);
                else
                    db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, DBNull.Value);
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
        #endregion

            #region Insert City
        /// <summary>
        /// Insert City detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertCity(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_TEST_MASTER_INSERT");
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
                return Result = db.ExecuteNonQuery(dbCmd);
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
            return Result;
        }
        #endregion

            #region Update City
        /// <summary>
        /// Update City detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateCity(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_TEST_MASTER_UPDATE");
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objLookupBDto.LookupId);
                db.AddInParameter(dbCmd, "@CITY_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@MODIFY_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
                return Result = db.ExecuteNonQuery(dbCmd);
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
            return Result;
        }

        #endregion

            #region Delete City
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">City collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteCity(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATION_COMMON_TEST_MASTER_DELETE");
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ERRORCODE"));
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
            return Result;
        }

        #endregion

          #endregion

        #region Report Master, 24/1/2011

        #region Get ReportName
        /// <summary>
        /// Gets Report list.
        /// </summary>
        /// <returns>Returns dataset contains Report data.</returns>
        public DataSet GetReportName(String ReportName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_REPORT_MASTER_SELECT");
                if (!String.IsNullOrEmpty(ReportName))
                    db.AddInParameter(dbCmd, "@REPORT_NAME", DbType.String, ReportName);
                else
                    db.AddInParameter(dbCmd, "@REPORT_NAME", DbType.String, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
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

        #endregion

        #region Insert Report
        /// <summary>
        /// Insert Report detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>

        public int InsertReport(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int result = 0;
            
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_REPORT_MASTER_INSERT");
                db.AddInParameter(dbCmd, "@REPORT_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.LookupId);
                result = db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return result;
        }

        #endregion

        #region Update Report
        /// <summary>
        /// Update Report detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateReport(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_REPORT_MASTER_UPDATE");
                db.AddInParameter(dbCmd, "@REPORT_ID", DbType.Int32, objLookupBDto.LookupId);
                db.AddInParameter(dbCmd, "@REPORT_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@MODIFY_BY", DbType.Int32, objLookupBDto.UserProfile.EmployeeId);
                db.ExecuteNonQuery(dbCmd);
                result = db.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
                {
                    throw ex;
                }
            }
            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return result;
        }
        #endregion

        #region Delete Report
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">Report collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteReport(String idCollections)
        {
        Database db = null;
        DbCommand dbCmd = null;
        int result = 0;
        try
        {
            db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
            dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_REPORT_MASTER_DELETE");
            db.AddInParameter(dbCmd, "@REPORT_ID", DbType.String, idCollections);
            db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCmd);
            result = Convert.ToInt32(db.GetParameterValue(dbCmd,"@ERRORCODE"));
        }
        catch(Exception ex)
        {
            bool reThrow=ExceptionPolicy.HandleException(ex,DALHelper.DAL_EXP_POLICYNAME);
            if(reThrow)
            {
                throw ex;
            }
        }
        finally
        {
            DALHelper.Destroy(ref dbCmd);
        }
        return result;
        }
        #endregion

        #endregion

        #region Output CheckList, 28/1/2011

        #region Get Output CheckList
        /// <summary>
        /// Gets Output CheckList.
        /// </summary>
        /// <returns>Returns dataset contains Output CheckLits data.</returns>

        public DataSet GetOutputCheckList(string OutputCheckListName)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_OP_CL_SELECT");
                if (!string.IsNullOrEmpty(OutputCheckListName))
                    db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_NAME", DbType.String, OutputCheckListName);
                else
                    db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_NAME", DbType.String, DBNull.Value);
                ds = db.ExecuteDataSet(dbCmd);
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
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

        #endregion

        #region Insert Output CheckList
        /// <summary>
        /// Inserts OutPut CheckList.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertOutputCheckList(LookupBDto objLookupBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int result = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_OP_CL_INSERT");
                db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objLookupBDto.LookupId);
                result = db.ExecuteNonQuery(dbCmd);
            }

            catch (Exception ex)
            {
                bool reThrow;
                reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
                {
                    throw ex;
                }
            }

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }

            return result;
        }
        #endregion

        #region Update Output CheckList
        /// <summary>
        /// Update Output CheckList detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public  int UpdateOutputCheckList(LookupBDto objLookupBDto)
        { 
            Database db = null;
            DbCommand dbCmd = null;
            int result = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATOR_COMMON_OP_CL_UPDATE");
                db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_ID", DbType.Int32, objLookupBDto.LookupId);
                db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_NAME", DbType.String, objLookupBDto.LookupName);
                db.AddInParameter(dbCmd, "@MODIFIED_BY", DbType.Int32, objLookupBDto.UserProfile.UserId);
                result = db.ExecuteNonQuery(dbCmd);
            }

            catch (Exception ex)
            {
                bool reThrow;
                reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (reThrow)
                {
                    throw ex;
                }
            }

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return result;
        }
        #endregion

        #region Delete Output CheckList
        /// <summary>
        /// Delete Title detail.
        /// </summary>
        /// <param name="idCollections">Report collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteOutputCheckList(string idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int result=0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USD_ADMINISTRATOR_COMMON_OP_CL_DELETE");
                db.AddInParameter(dbCmd, "@OUTPUT_CHECK_LIST_ID", DbType.String, idCollections);
                db.AddOutParameter(dbCmd, "@ERRORCODE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                result = Convert.ToInt32(db.GetParameterValue(dbCmd,"@ERRORCODE"));
            }

            catch(Exception ex)
            {
                bool reThrow;
                reThrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if(reThrow)
                {
                    throw ex;
                    return 0;
                }
            }

            finally
            {
                DALHelper.Destroy(ref dbCmd);
            }
            return result;
        }
        #endregion

        #endregion

        #endregion
    }
}
