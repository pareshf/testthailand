#region Program Information
/**********************************************************************************************************************************************
 Class Name           : AgentLookupDal
 Class Description    : Implementation Logic DepartmentLookupDal database releated transaction.
 Author               : Priyam.
 Created Date         : july 28, 2010
***********************************************************************************************************************************************/
#endregion

#region Impoerts assemblies
using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace CRM.DataAccess.AdministrationDAL
{
    public class AgentDal
    {
        #region Get Agent
        /// <summary>
        /// Gets Agent list.
        /// </summary>
        /// <returns>Returns dataset contains Agent data.</returns>
        public DataSet GetAgent(String SearchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
             try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AGENT_SELECT);
                if (!String.IsNullOrEmpty(SearchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String ,SearchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
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

        #region Insert Agent
        /// <summary>
        /// Insert Agent detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int InsertAgent(AgentBDto objAgentBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AGENT_INSERT);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, objAgentBDto.AgentName);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, objAgentBDto.Email);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, objAgentBDto.Mobile);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, objAgentBDto.Phone);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, objAgentBDto.Fax);
                db.AddInParameter(dbCmd, "@CREATED_BY", DbType.Int32, objAgentBDto.UserId);
                db.AddOutParameter(dbCmd,"@ISEXIST", DbType.Int32, 1);
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

        #region DeleteAgent
        /// <summary>
        /// Delete Agent detail.
        /// </summary>
        /// <param name="idCollections">Agent Id collection seperated by commas.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int DeleteAgent(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AGENT_DELETE);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String, idCollections);
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

        #region Update Agent
        /// <summary>
        /// Update Agent detail.
        /// </summary>
        /// <param name="xmlData">Data that converted into xml format.</param>
        /// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
        public int UpdateAgent(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AGENT_UPDATE);
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


        #region Added By Sunil

        
        #region get country

        public DataSet GetCountry(String SearchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATION_COMMON_COUNTRY_TEST_MASTER_SELECT");
                if (!String.IsNullOrEmpty(SearchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, SearchParameter);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
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

        #region INSERT COUNTRY

        public int InsertCountry(AgentBDto objagentDDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_ADMINISTRATION_COMMON_COUNTRY_TEST_MASTER_INSERT");
                db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, objagentDDto.CountryName1);
                db.AddInParameter(dbCmd, " @CREATED_BY", DbType.Int32, objagentDDto.CtreatedBy1);
                db.AddInParameter(dbCmd, "@CREATED_DATE", DbType.DateTime, objagentDDto.CreatedDate1);
                db.AddInParameter(dbCmd, "@MODIFY_BY", DbType.Int32, objagentDDto.ModifyBy1);
                db.AddInParameter(dbCmd, "MODIFIED_DATE", DbType.DateTime, objagentDDto.ModifyDate1);
                db.ExecuteNonQuery(dbCmd);
                Result=Convert.ToInt32(db.GetParameterValue(dbCmd,"@ISEXIT"));
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

        #region update country






        #endregion




        #endregion







    }
}
