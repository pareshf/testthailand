#region Program Information
/**********************************************************************************************************************************************
 Class Name           : QualificationLookupDal
 Class Description    : Implementation Logic QualificationLookupDal database releated transaction.
 Author               : Priyam.
 Created Date         : Mar 12, 2010
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
public	class CountryRegionMapDal
	{

		#region InsertRegionCountryMap
		/// <summary>
		/// Insert GdsAirportDal detail.
		/// </summary>
		/// <param name="xmlData">Data that converted into xml format.</param>
		/// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
		public int InsertRegionCountryMap(CountryRegionMapBDto CountryRegionMapBDto)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_COMMON_COUNTRY_REGION_MAP_INSERT");
				db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, CountryRegionMapBDto.RegionId);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, CountryRegionMapBDto.CountryId);

				Result = db.ExecuteNonQuery(dbCmd);
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

		#region UpdateRegionCountryMap
		/// <summary>
		/// Insert GdsAirportDal detail.
		/// </summary>
		/// <param name="xmlData">Data that converted into xml format.</param>
		/// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
		public int UpdateRegionCountryMap(CountryRegionMapBDto CountryRegionMapBDto)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_COMMON_COUNTRY_REGION_MAP_UPDATE");
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, CountryRegionMapBDto.SrNO);
				db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, CountryRegionMapBDto.RegionId);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, CountryRegionMapBDto.CountryId);

				Result = db.ExecuteNonQuery(dbCmd);
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

		#region Delete CountryRegionMap
		/// <summary>
		/// Delete Title detail.
		/// </summary>
		/// <param name="idCollections">AddressType Id collection seperated by commas.</param>
		/// <returns>Returns 1 and 0; (1 indicates successful operation).</returns>
		public int DeleteCountryRegionMap(String idCollections)
		{
			Database db = null;
			DbCommand dbCmd = null;
			int Result = 0;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("USP_COMMON_COUNTRY_REGION_MAPPING_DELETE");
				db.AddInParameter(dbCmd, "@SR_NO", DbType.String, idCollections);
				Result = db.ExecuteNonQuery(dbCmd);
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

		#region FindCountryRegionMap

		public DataTable FindCountryRegionMap(string searchPara)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_COMMON_COUNTRY_REGION_MAP_SELECT");
				if (!string.IsNullOrEmpty(searchPara))
					db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, searchPara);
				else
					db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
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
			return dt;
		}

		#endregion

		#region GetCountryRegionMap


		public DataTable GetCountryRegionMap(int srNo)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_COMMON_COUNTRY_REGION_MAP_SELECT_BYID");
				db.AddInParameter(dbCmd, "@SR_NO", DbType.Int32, srNo);
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
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
			return dt;
		}

		#endregion
	}
}
