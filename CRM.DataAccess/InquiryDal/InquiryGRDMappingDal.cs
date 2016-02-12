using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CRM.Model.AdministrationModel;
using CRM.Model.Inquiry;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess.InquiryDal
{
    public class InquiryGRDMappingDal 
    {
        #region Get Inquiry GRD Mapping
        public DataSet GetInquiryGRDMapping(String searchParameter)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_SELECT);
                if (!String.IsNullOrEmpty(searchParameter))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchParameter);
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

        #region delete InquiryGRDMapping
        public int DeleteInquiryGRDMapping(string SrNo)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_DELETE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.String, SrNo);
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

        #region Insert InquiryGRDMapping

        public int InsertInquiryGRDMapping(InquiryGRDMappingBDto objInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_INSERT);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@PREFERED_AIRLINES_ID", DbType.Int32, objInquiryGRDMappingBDto.PreferedAirlinesId);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.Int32, objInquiryGRDMappingBDto.GDSAirportCode);
                db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, objInquiryGRDMappingBDto.TimeDetails);
                db.AddOutParameter(dbCmd, "@ISEXIST", DbType.Int32, 1);
                int r = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@ISEXIST"));
                return Result;
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
            return 0;
        }
        #endregion

        #region Update InquiryGRDMapping

        public int UpdateInquiryGRDMapping(InquiryGRDMappingBDto objInquiryGRDMappingBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQ_INQUIRY_AIRLINE_GDS_MAPPING_UPDATE);
                db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, objInquiryGRDMappingBDto.SrNo);
                db.AddInParameter(dbCmd, "@INQUIRY_ID", DbType.Int32, objInquiryGRDMappingBDto.InquiryId);
                db.AddInParameter(dbCmd, "@PREFERED_AIRLINES_ID", DbType.Int32, objInquiryGRDMappingBDto.PreferedAirlinesId);
                db.AddInParameter(dbCmd, "@GDS_AIRPORT_CODE", DbType.Int32, objInquiryGRDMappingBDto.GDSAirportCode);
                db.AddInParameter(dbCmd, "@TIME_DETAILS", DbType.String, objInquiryGRDMappingBDto.TimeDetails);

                Result = db.ExecuteNonQuery(dbCmd);
                return Result;
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

       

    }
}
