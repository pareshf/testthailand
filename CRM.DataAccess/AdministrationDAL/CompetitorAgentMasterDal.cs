using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Common;
using CRM.Model.AdministrationModel;

namespace CRM.DataAccess.AdministrationDAL
{
    public class CompetitorAgentMasterDal : IDisposable
    {




        #region Get Competitor

        public DataSet GetCompetitor(String SEARCH_PARAMETER)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPETITOR_AGENT_SELECT);
                if (!String.IsNullOrEmpty(SEARCH_PARAMETER))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, SEARCH_PARAMETER);
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

        public int InsertAgent(CompetitorAgentMasterBDto objCompetitorAgentMasterBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPETITOR_AGENT_INSERT);

                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, objCompetitorAgentMasterBDto.AgentName);
                db.AddInParameter(dbCmd, "@AGENT_ADDRESS", DbType.String, objCompetitorAgentMasterBDto.AgentAddress);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objCompetitorAgentMasterBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, objCompetitorAgentMasterBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objCompetitorAgentMasterBDto.CountryId);
                db.AddInParameter(dbCmd, "@PHONE_NO", DbType.String, objCompetitorAgentMasterBDto.Phone);
                db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, objCompetitorAgentMasterBDto.OwnerCompanyId);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objCompetitorAgentMasterBDto.UserId);
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

        #region Delete Agent

        public int DeleteAgent(String idCollections)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPETITOR_AGENT_DELETE);
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

        #region Update  Agent
       
        public int UpdateAgent(CompetitorAgentMasterBDto objCompetitorAgentMasterBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPETITOR_AGENT_UPDATE);

                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.Int32, objCompetitorAgentMasterBDto.AgentId);
                db.AddInParameter(dbCmd, "@AGENT_NAME", DbType.String, objCompetitorAgentMasterBDto.AgentName);
                db.AddInParameter(dbCmd, "@AGENT_ADDRESS", DbType.String, objCompetitorAgentMasterBDto.AgentAddress);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, objCompetitorAgentMasterBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, objCompetitorAgentMasterBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, objCompetitorAgentMasterBDto.CountryId);
                db.AddInParameter(dbCmd, "@PHONE_NO", DbType.String, objCompetitorAgentMasterBDto.Phone);
                db.AddInParameter(dbCmd, "@OWNER_COMPANY_ID", DbType.Int32, objCompetitorAgentMasterBDto.OwnerCompanyId);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, objCompetitorAgentMasterBDto.UserId);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32,1);

                db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
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



        #region Get Competitor's Customer

        public DataSet GetCompetitorCustomer(int AgentId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPETITOR_CUSTOMER_SELECT);
                db.AddInParameter(dbCmd, "@AGENT_ID", DbType.String, AgentId);
               
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


        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion

    }
}
