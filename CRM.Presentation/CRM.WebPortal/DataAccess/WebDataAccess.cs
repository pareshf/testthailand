using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Common;


namespace CRM.WebPortal.DataAccess
{
	public class WebDataAccess
	{
		#region Connection Sting Constant
		protected const String CRM_CONNECTION_STRING = "CRM";
		#endregion

		public DataTable GetTourType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_WEB_SELECT_TOUR_TYPE");
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception)
			{

			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}
        public DataTable GetDomesticTourType()
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("USP_WEB_SELECT_TOUR_Domestic_type");
                ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Destroy(ref dbCmd);
            }
            return dt;
        }
		public DataTable GetTourRegion(string tourType, int tourSubTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_REGION");
				db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);

				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataTable GetTourCountry(string tourType, int tourSubTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_COUNTRY");
				db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
				//db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataTable GetTourState(string tourType, int tourSubTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_STATE");
				db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);

				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataTable GetTour(string tourType, int tourSubTypeId, int countryId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOURS");
				db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
				//db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
				db.AddInParameter(dbCmd, "@tour_country", DbType.Int32, countryId);
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataTable GetTourByState(string tourType, int tourSubTypeId, int stateId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOURS_BY_STATES");
				db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_sub_type", DbType.Int32, tourSubTypeId);
				db.AddInParameter(dbCmd, "@tour_state", DbType.Int32, stateId);

				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataTable GetTourDetail(int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataTable dt = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_TOUR_DETAILS");
				//db.AddInParameter(dbCmd, "@tour_type", DbType.String, tourType);
				db.AddInParameter(dbCmd, "@tour_id", DbType.Int32, tourId);
				//db.AddInParameter(dbCmd, "@tour_region", DbType.Int32, regionId);
				//db.AddInParameter(dbCmd, "@tour_country", DbType.Int32, countryId);
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{

			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		public DataSet GetFlightFares()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_WEB_SELECT_FLIGHT_FARES");
				ds = db.ExecuteDataSet(dbCmd);

			}
			catch (Exception)
			{

			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return ds;
		}

		public DataTable GetTourFlash()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			DataTable dt = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_FLASH_SELECT");
				ds = db.ExecuteDataSet(dbCmd);
				if (ds != null && ds.Tables.Count > 0)
				{
					dt = ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				Destroy(ref dbCmd);
			}
			return dt;
		}

		#region Destroy Command Object
		/// <summary>
		/// Closes the Command connection and destroys the Command Object.
		/// </summary>
		/// <param name="dbCmd">Command Object</param>
		public static void Destroy(ref DbCommand dbCmd)
		{
			try
			{
				if (dbCmd != null)
				{
					if (dbCmd.Connection.State == ConnectionState.Open)
					{
						dbCmd.Connection.Close();
					}
				}
				dbCmd = null;
			}
			catch (Exception) { }

		}

		#endregion

		#region Destroy DataReader Object
		/// <summary>
		/// Closes the reader and destroys the datareader Object.
		/// </summary>
		/// <param name="dread">DataReader Object</param>
		public static void Destroy(ref IDataReader dread)
		{
			try
			{
				if (dread != null)
				{
					dread.Close();
				}

				dread = null;
			}
			catch (Exception) { }
		}
		#endregion

	}
}
