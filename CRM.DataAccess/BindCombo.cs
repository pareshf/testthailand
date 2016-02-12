using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.DataAccess
{
	public class BindCombo
	{
		#region Get Title
		/// <summary>
		/// Get details of title
		/// </summary>
		/// <returns>Dataset of Title data</returns>
		public DataSet GetTitle()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_TITLE_SELECT);
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
		#endregion

		#region Get Profession
		/// <summary>
		/// Get details of Profession
		/// </summary>
		/// <returns>Dataset of Profession data</returns>
		public DataSet GetProfession()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_PROFESSION_SELECT);
				db.AddInParameter(dbCmd, "@PROFESSION_DESC", DbType.String, DBNull.Value);
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
		#endregion

		#region Get Relation
		/// <summary>
		/// Gets Relation list.
		/// </summary>
		/// <returns>Returns dataset contains Relation data.</returns>
		public DataSet GetRelation()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_RELATION_BYNAME);
				db.AddInParameter(dbCmd, "@RELATION_DESC", DbType.String, DBNull.Value);
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

		#region Get Customer Type
		/// <summary>
		/// Gets Customer Type.
		/// </summary>
		/// <returns>Returns dataset contains Customer Type.</returns>
		public DataSet GetCustomerType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_TYPE_MASTER_SELECT_KEYVALUE);
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

		public DataSet GetInquiryRating()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_RATING_SELECT_KEYVALUE");
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

		#region Get Customer code
		/// <summary>
		/// Gets Customer Type.
		/// </summary>
		/// <returns>Returns dataset contains Customer code.</returns>
		public DataSet GetCustomerCode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUST_CODE_MASTER_SELECT_KEYVALUE);
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


		


		

		#region Get Marital Status
		/// <summary>
		/// Gets Relation list.
		/// </summary>
		/// <returns>Returns dataset contains Marital Status data.</returns>
		public DataSet GetMaritalStatus()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MARITAL_STATUS_SELECT);
				db.AddInParameter(dbCmd, "@MARITAL_STATUS_NAME", DbType.String, DBNull.Value);
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

		#region Get Religion
		/// <summary>
		/// Gets Relation list.
		/// </summary>
		/// <returns>Returns dataset contains Relation data.</returns>
		public DataSet GetReligion()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_RELIGION_SELECT);
				db.AddInParameter(dbCmd, "@RELIGION_NAME", DbType.String, DBNull.Value);
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

		#region Get Inquire and FollowUp Mode
		/// <summary>
		/// Gets Inquire and FollowUp Mode.
		/// </summary>
		/// <returns>Returns dataset contains Inquire and FollowUp Mode data.</returns>
		public DataSet GetInquireFollowUpMode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRIES_FOLLOWUP_MODE_SELECT_KEYVALUE);
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

		#region Get Status
		/// <summary>
		/// Gets Status list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetStatus()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATUS_SELECT_KEYVALUE);
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

		#region Get Competitor Agent
		/// <summary>
		/// Gets Competitor Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Competitor Agent data.</returns>
		public DataSet GetCompetitorAgent()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_COMPETITOR_AGENT_SELECT_KEYVALUE);
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

		#region Get Tour
		/// <summary>
		/// Gets Tour list.
		/// </summary>
		/// <returns>Returns dataset contains Tour data.</returns>
		public DataSet GetTour(int tourTypeId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TOUR_SELECT_KEYVALUE);
				db.AddInParameter(dbCmd, "@TOUR_TYPE_ID", DbType.Int32, tourTypeId);
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

		#region Get Tour Type
		/// <summary>
		/// Gets Tour list.
		/// </summary>
		/// <returns>Returns dataset contains Tour data.</returns>
		public DataSet GetTourType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_TOUR_TYPE_SELECT_KEYVALUE);
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

		#region Get Role Key value
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetRoleKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ROLE_SELECT_KEYVALUE);
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

		#region Get Country Key value
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetCountryKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COUNTRY_GETKEYVALUE);
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

		#region Get State Key value
		/// <summary>
		/// Gets State list.
		/// </summary>
		/// <returns>Returns dataset contains State data.</returns>
		public DataSet GetStateKeyValue(int CounteryId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_STATE_GETKEYVALUE);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, CounteryId);
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

		#region Get City Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains City data.</returns>
		public DataSet GetCityKeyValue(int CounteryId, int StateId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_GETKEYVALUE);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, CounteryId);
				db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, StateId);
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

		#region Get City Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains City data.</returns>
		public DataSet GetCityKeyValueByCountriesName(string CounteriesName)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_CITY_BY_COUNTRY);
				db.AddInParameter(dbCmd, "@COUNTRY_NAME", DbType.String, CounteriesName);
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

		#region Get Region Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains City data.</returns>
		public DataSet GetRegionKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_REGION_GETKEYVALUE);

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

		#region Get Company Key value
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetCompanyKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_SELECT_KEYVALUE);
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

		#region Get Company Name Type
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetCompanyNameType(string Type)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_SELECT_NAME_TYPE);
				db.AddInParameter(dbCmd, "@CompanyType", DbType.String, Type);


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

		#region Get Employee CompanyMap
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetEmployeeCompanyMap(int Emp_Id)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_COMPANY_EMPLOYEE_MAP_KEYVALUE);
				db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, Emp_Id);


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

		#region Get Department keyvalue

		public DataSet GetDepartmentKeyValue(String DepartmentName)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DEPARTMENT_SELECT);
				if (!String.IsNullOrEmpty(DepartmentName))
					db.AddInParameter(dbCmd, "@DEPARTMENT_NAME", DbType.String, DepartmentName);
				else
					db.AddInParameter(dbCmd, "@DEPARTMENT_DESC", DbType.String, DBNull.Value);
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


		public DataSet GetDepartmentByCompanyId(int CompanyId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_DEPARTMENT_SELECT_BY_COMPANY_ID);
				db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CompanyId);

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

		#region Get Region Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains City data.</returns>
		public DataSet GetDesignationKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_DESIGNATION_GETKEYVALUE);

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

		#region Get Qualification Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains Qualification data.</returns>
		public DataSet GetQualificationKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_QUALIFICATION_GETKEYVALUE);

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

		#region Get Module Name Key value
		/// <summary>
		/// Gets MOdule name list.
		/// </summary>
		/// <returns>Returns dataset contains Module name data.</returns>
		public DataSet GetModuleKeyValue()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MODULE_MASTER_KEYVALUE);
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

		#region Get Manager Name Key value
		/// <summary>
		/// Gets City list.
		/// </summary>
		/// <returns>Returns dataset contains Qualification data.</returns>
		public DataSet GetManagerKeyValue(int CMP_ID)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_MANAGER_GETKEYVALUE);
				db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, CMP_ID);
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
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_ADDRESS_TYPE_SELECT_KEYVALUE);
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

		#region Security Question
		public DataSet GetSecurityQuestion(String securityQuestion)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_SECURITY_QUESTION_SELECT);
				if (!String.IsNullOrEmpty(securityQuestion))
					db.AddInParameter(dbCmd, "@SECURITY_QUESTION_DESC", DbType.String, securityQuestion);
				else
					db.AddInParameter(dbCmd, "@SECURITY_QUESTION_DESC", DbType.String, DBNull.Value);
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

		#region Get Referance
		/// <summary>
		/// Gets Referance list.
		/// </summary>
		/// <returns>Returns dataset contains City data.</returns>
		public DataSet GetReferance()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_REFERENCE_SELECT_KEYVALUE);
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

		#region Get Inquiry Status
		/// <summary>
		/// Gets Status list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetInquiryStatus()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_INQUIRY_STATUS_SELECT_KEYVALUE);
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

		#region Get Company Department MAP
		/// <summary>
		/// Gets Country list.
		/// </summary>
		/// <returns>Returns dataset contains Country data.</returns>
		public DataSet GetCompanyDepartmentMap(int Company_Id)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_KEYVALUE);
				db.AddInParameter(dbCmd, "@CMP_ID", DbType.Int32, Company_Id);


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

		#region Get Product
		/// <summary>
		/// Gets Product list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetProduct()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_PRODUCT_SELECT_KEYVALUE);
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

		#region Get Agent
		/// <summary>
		/// Gets Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetAgent()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_AGENT_SELECT_KEYVALUE);
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

		#region Get Meal
		/// <summary>
		/// Gets Meal list.
		/// </summary>
		/// <returns>Returns dataset contains Meal data.</returns>
		public DataSet GetMeal()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_MEAL_SELECT_KEYVALUE");
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

		#region Get Currency
		/// <summary>
		/// Gets Currency list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetCurrency()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_BOOKING_CURRENCY_SELECT_KEYVALUE);
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

		#region Get Employee
		/// <summary>
		/// Gets Employee list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetEmployee()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_SELECT_KEYVALUE);
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

		#region Get Employee by Company
		/// <summary>
		/// Gets Employee list by Company.
		/// </summary>
		/// <returns>Returns dataset contains Employee.</returns>
		public DataSet GetEmployeeByCompany(int companyId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_HR_EMPLOYEE_LIST_BY_COMPANY_ID");
				if (companyId != 0)
					db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, companyId);
				else
					db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, DBNull.Value);
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


        #region BY SUNIL Get Employee Email,Mobile by EMP ID
        /// <summary>
        /// Gets Employee Email and Mobile list by EMP ID
        /// </summary>
        /// <returns>Returns dataset contains Employee.</returns>
        public DataSet GetEmployeeEmailByCompany(int UserId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand("dbo.USP_HR_EMPLOYEE_EMAIL_MOBILE_SELECT");
                if (UserId != 0)
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, UserId);
                else
                    db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, DBNull.Value);
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

		#region Get RoomType
		/// <summary>
		/// Gets RoomType list.
		/// </summary>
		/// <returns>Returns dataset contains RoomType data.</returns>
		public DataSet GetRoomType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_HOTEL_ROOM_TYPE_SELECT_KEYVALUE");
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

		#region Get RoomType
		/// <summary>
		/// Gets RoomType list.
		/// </summary>
		/// <returns>Returns dataset contains RoomType data.</returns>
		public DataSet GetRoomType(int hotelId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_ROOM_TYPE_SELECT_BY_HOTEL");
				db.AddInParameter(dbCmd, "@HOTEL_ID", DbType.Int32, hotelId);
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

		#region Get Frequent AirLine
		/// <summary>
		/// Get Frequent AirLine.
		/// </summary>
		/// <returns>Returns dataset contains RoomType data.</returns>
		public DataSet GetFrequentAirLine()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.FARE_FREQUENTLY_FLY_AIRLINE_SELECT_KEYVALUE");
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

		#region Get Travelling Cities
		/// <summary>
		/// Gets Travelling Cities list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Cities data.</returns>
		public DataSet GetTravellingCities(int tourId, int countryId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TRAVELLING_CITY_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, countryId);
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

		#region Get Travelling Countries
		/// <summary>
		/// Gets Travelling Countries list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Countries data.</returns>
		public DataSet GetTravellingCountries(int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TRAVELLING_COUNTRY_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

		#region DeleteGet Travelling Cities
		/// <summary>
		/// Gets Travelling Cities list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Cities data.</returns>
		public DataSet DeleteGetTravellingCities(int tourId, int countryId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.DELETE_USP_BOOKING_TRAVELLING_CITY_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, countryId);
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

		#region DeleteGet Travelling Countries
		/// <summary>
		/// Gets Travelling Countries list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Countries data.</returns>
		public DataSet DeleteGetTravellingCountries(int tourId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.DELETE_USP_BOOKING_TRAVELLING_COUNTRY_SELECT");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
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

		#region Get Travelling Hotels
		/// <summary>
		/// Gets Travelling Hotels list.
		/// </summary>
		/// <returns>Returns dataset contains Travelling Hotels data.</returns>
		public DataSet GetTravellingHotels(int cityId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_TRAVELLING_HOTELS_SELECT");
				db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, cityId);
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

		#region Get Payment Mode
		/// <summary>
		/// Gets Payment Mode list.
		/// </summary>
		/// <returns>Returns dataset contains Payment Mode data.</returns>
		public DataSet GetPaymentMode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_PAYMENT_MODE_MASTER_SELECT_KEYVALUE");
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

		#region Get Bank
		/// <summary>
		/// Gets Bank list.
		/// </summary>
		/// <returns>Returns dataset contains Bank data.</returns>
		public DataSet GetBank()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_BANK_MASTER_SELECT_KEYVALUE");
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

		#region Get Foreign Currency Agent
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetForeignCurrencyAgent()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_FOREIGN_CURRENCY_AGENT_SELECT_KEYVALUE");
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

		#region Get Inquiry Id
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetInquiryId()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_AIRLINE_GDS_INQUIRY_SELECT_KEYVALUE");
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

		#region Get AirLine
		/// <summary>
		/// Gets AirLine list.
		/// </summary>
		/// <returns>Returns dataset contains AirLine data.</returns>
		public DataSet GetAirLine()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_SELECT_KEYVALUE");
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

		#region Get AirLine
		/// <summary>
		/// Gets AirLine list.
		/// </summary>
		/// <returns>Returns dataset contains AirLine data.</returns>
		public DataSet GetContinent()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.CONTINENT_MASTER_SELECT_KEYVALUE");
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

		#region Get Flights
		/// <summary>
		/// Gets Flights list.
		/// </summary>
		/// <returns>Returns dataset contains Flights data.</returns>
		public DataSet GetFlights()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_MASTER_SELECT_KEYVALUE");
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

		#region Get AirLine by source and destination
		/// <summary>
		/// Gets AirLine list by source and destination
		/// </summary>
		/// <param name="tourId">Tour Id</param>
		/// <param name="source">Source city id</param>
		/// <param name="destination">Destination city id</param>
		/// <returns>Returns dataset contains AirLine list.</returns>
		public DataSet GetAirLine(int tourId, int source, int destination)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_SELECT_KEYVALUE_BY_SOURCE_DESTI");
				db.AddInParameter(dbCmd, "@TOUR_ID", DbType.Int32, tourId);
				db.AddInParameter(dbCmd, "@SOURCE", DbType.Int32, source);
				db.AddInParameter(dbCmd, "@DESTINATION", DbType.Int32, destination);
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

		#region Get Flight Class
		/// <summary>
		/// Gets Foreign Flight Class list.
		/// </summary>
		/// <returns>Returns dataset contains Flight Class data.</returns>
		public DataSet GetFlightClass()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_FLIGHT_CLASS_SELECT_KEYVALUE");
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

		#region Get Airport
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetAirport()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_INQ_INQUIRY_AIRPORT_CODE_SELECT_KEYVALUE");
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

		#region Get Ticket Type
		/// <summary>
		/// Gets Ticket Type list.
		/// </summary>
		/// <returns>Returns dataset contains Ticket Type data.</returns>
		public DataSet GetTicketType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TICKET_TYPE_SELECT_KEYVALUE");
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

		#region Get Transpot Mode
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetTransportMode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("[dbo].USP_FARE_TRANSPORT_SELECT_KEYVALUE");
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

		#region Get Transpot Name
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetTransportName(string TransportMode)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("[dbo].USP_FARE_TRANSPORT_NAME_SELECT_KEYVALUE");
				db.AddInParameter(dbCmd, "@TRANSPORTMODE", DbType.String, TransportMode);
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

		#region Get Transpot Name
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetFareTicketTypeName(string TicketTypeName)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("[dbo].USP_FARE_TICKET_TYPE_SELECT");
				if (!string.IsNullOrEmpty(TicketTypeName))
				{
					db.AddInParameter(dbCmd, "@TICKET_TYPE_NAME", DbType.String, TicketTypeName);
				}
				else
				{
					db.AddInParameter(dbCmd, "@TICKET_TYPE_NAME", DbType.String, DBNull.Value);
				}

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

		#region Get Travel Mode
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetTravelMode(string TravelModeName)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

				dbCmd = db.GetStoredProcCommand("[dbo].USP_FARE_TRAVEL_MODE_SELECT");
				if (!string.IsNullOrEmpty(TravelModeName))
				{
					db.AddInParameter(dbCmd, "@TRAVEL_MODE_NAME", DbType.String, TravelModeName);
				}
				else
				{
					db.AddInParameter(dbCmd, "@TRAVEL_MODE_NAME", DbType.String, DBNull.Value);
				}
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

		#region Get TravelKey Value Mode
		/// <summary>
		/// Gets Travel Modet list.
		/// </summary>
		/// <returns>Returns dataset contains Travel Mode data.</returns>
		public DataSet GetTravelMode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TRAVEL_MODE_SELECT_KEYVALUE");
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

		#region Get REgion Name
		/// <summary>
		/// Gets Foreign Currency Agent list.
		/// </summary>
		/// <returns>Returns dataset contains Foreign Currency Agent data.</returns>
		public DataSet GetRegionName(string regionShortName, string regionLongName)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

				dbCmd = db.GetStoredProcCommand("[dbo].USP_ADMINISTRATION_REGION_SELECT");
				if (!string.IsNullOrEmpty(regionShortName))
				{
					db.AddInParameter(dbCmd, "@REGION_SHORT_NAME", DbType.String, regionShortName);
				}
				else
				{
					db.AddInParameter(dbCmd, "@REGION_SHORT_NAME", DbType.String, DBNull.Value);
				}
				if (!string.IsNullOrEmpty(regionLongName))
				{
					db.AddInParameter(dbCmd, "@REGION_LONG_NAME", DbType.String, regionShortName);
				}
				else
				{
					db.AddInParameter(dbCmd, "@REGION_LONG_NAME", DbType.String, DBNull.Value);
				}



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

		#region Get GDSCode Airline Key value
		/// <summary>
		/// Gets GDSCode Airline Key value list.
		/// </summary>
		/// <returns>Returns dataset contains State data.</returns>
		public DataSet GetGDSCode(int AirlineId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_INQUIRY_AIRLINE_GDSCODE_KEYVALUE);
				db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, AirlineId);
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

		#region Get Flight NO
		/// <summary>
		/// Gets Flight Airline Key value list.
		/// </summary>
		/// <returns>Returns dataset contains State data.</returns>
		public DataSet GetFlightNo(int AirlineId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand(DALHelper.USP_FARE_TRAVEL_FLIGHT_KEYVALUE);
				db.AddInParameter(dbCmd, "@AIRLINE_ID", DbType.Int32, AirlineId);
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

		#region Get CheckList
		/// <summary>
		/// Gets Currency list.
		/// </summary>
		/// <returns>Returns dataset contains Status data.</returns>
		public DataSet GetCheckList()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet ds = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_BOOKING_CHECKLIST_MST_SELECT_KEYVALUE");
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

		public DataSet GetAirportGdsCode(int airlineId)
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_AIRLINE_GDSCODE_SELECT_KEYVALUE");
				db.AddInParameter(dbCmd, "AIRLINE_ID", DbType.Int32, airlineId);
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

		public DataSet GetItenaryType()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_FARE_TOUR_ITENARY_TYPE_SELECT");
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

		public DataSet GetEmailTemplate()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_EMAIL_TEMPLATE_MASTER_SELECT");
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


		public DataSet GetSentMode()
		{
			Database db = null;
			DbCommand dbCmd = null;
			DataSet dsData = null;
			try
			{
				db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
				dbCmd = db.GetStoredProcCommand("dbo.USP_COMMON_SENT_MODE_SELECT");
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
