using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.HRModel;

namespace CRM.DataAccess.HRDAL
{
    public class EmployeeMasterDal : IDisposable
    {


        #region Employee Details
        #region Insert Employee

        public int InsertEmployee(EmployeeBDto ObjEmployeeBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_INSERT);
                db.AddInParameter(dbCmd, "@EMP_TITLE_ID", DbType.Int32, ObjEmployeeBDto.TitleId);
                db.AddInParameter(dbCmd, "@EMP_SURNAME", DbType.String, ObjEmployeeBDto.EmpSurName);
                db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, ObjEmployeeBDto.EmpName);
              //  db.AddInParameter(dbCmd, "@EMP_DESIGNATION_ID", DbType.Int32, ObjEmployeeBDto.DesignationId);
               // db.AddInParameter(dbCmd, "@EMP_DEPARTMENT_ID", DbType.Int32, ObjEmployeeBDto.DepartmentId);
                db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, ObjEmployeeBDto.DateofBirth);
                db.AddInParameter(dbCmd, "@EMP_MARITAL_STATUS", DbType.Int32, ObjEmployeeBDto.MaritalStatusId);
                db.AddInParameter(dbCmd, "@EMP_GENDER", DbType.String, ObjEmployeeBDto.Gender);
             //   db.AddInParameter(dbCmd, "@EMP_MANAGE_ID", DbType.Int32, ObjEmployeeBDto.ManageId);
                db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, ObjEmployeeBDto.Email);
                db.AddInParameter(dbCmd, "@EMP_MOBILE", DbType.String, ObjEmployeeBDto.Mobile);
                db.AddInParameter(dbCmd, "@EMP_PHONE", DbType.String, ObjEmployeeBDto.Phone);
                db.AddInParameter(dbCmd, "@EMP_QUALIFICATION_ID", DbType.Int32, ObjEmployeeBDto.QualificationId);
             //   db.AddInParameter(dbCmd, "@EMP_DOJ", DbType.DateTime, ObjEmployeeBDto.DateofJoin);
                db.AddInParameter(dbCmd, "@EMP_STATUS", DbType.Int32, ObjEmployeeBDto.StatusId);

                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, ObjEmployeeBDto.Photo);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, ObjEmployeeBDto.Phototype);
                db.AddInParameter(dbCmd, "@SIGNATURE1", DbType.String, ObjEmployeeBDto.Signature1);
                db.AddInParameter(dbCmd, "@SIGNATURE2", DbType.String, ObjEmployeeBDto.Signature2);
                db.AddInParameter(dbCmd, "@SIGNATURE3", DbType.String, ObjEmployeeBDto.Signature3);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjEmployeeBDto.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        #region Update Employee

        public int UpdateEmployee(EmployeeBDto ObjEmployeeBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_UPDATE);


                db.AddInParameter(dbCmd, "@EMP_TITLE_ID", DbType.Int32, ObjEmployeeBDto.TitleId);
                db.AddInParameter(dbCmd, "@EMP_SURNAME", DbType.String, ObjEmployeeBDto.EmpSurName);
                db.AddInParameter(dbCmd, "@EMP_NAME", DbType.String, ObjEmployeeBDto.EmpName);
               // db.AddInParameter(dbCmd, "@EMP_DESIGNATION_ID", DbType.Int32, ObjEmployeeBDto.DesignationId);
               // db.AddInParameter(dbCmd, "@EMP_DEPARTMENT_ID", DbType.Int32, ObjEmployeeBDto.DepartmentId);
                db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, ObjEmployeeBDto.DateofBirth);
                db.AddInParameter(dbCmd, "@EMP_MARITAL_STATUS", DbType.Int32, ObjEmployeeBDto.MaritalStatusId);
                db.AddInParameter(dbCmd, "@EMP_GENDER", DbType.String, ObjEmployeeBDto.Gender);
                //db.AddInParameter(dbCmd, "@EMP_MANAGE_ID", DbType.Int32, ObjEmployeeBDto.ManageId);
                db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, ObjEmployeeBDto.Email);
                db.AddInParameter(dbCmd, "@EMP_MOBILE", DbType.String, ObjEmployeeBDto.Mobile);
                db.AddInParameter(dbCmd, "@EMP_PHONE", DbType.String, ObjEmployeeBDto.Phone);
                db.AddInParameter(dbCmd, "@EMP_QUALIFICATION_ID", DbType.Int32, ObjEmployeeBDto.QualificationId);
              //  db.AddInParameter(dbCmd, "@EMP_DOJ", DbType.DateTime, ObjEmployeeBDto.DateofJoin);
                db.AddInParameter(dbCmd, "@EMP_STATUS", DbType.Int32, ObjEmployeeBDto.StatusId);


                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, ObjEmployeeBDto.Photo);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, ObjEmployeeBDto.Phototype);


                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, ObjEmployeeBDto.EmpId);
                db.AddInParameter(dbCmd, "@SIGNATURE1", DbType.String, ObjEmployeeBDto.Signature1);
                db.AddInParameter(dbCmd, "@SIGNATURE2", DbType.String, ObjEmployeeBDto.Signature2);
                db.AddInParameter(dbCmd, "@SIGNATURE3", DbType.String, ObjEmployeeBDto.Signature3);
                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjEmployeeBDto.UserId);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));


                if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (Result == 1)
                    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                else
                    return 0; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #region Get Employee
        public DataSet GetEmployee(String SEARCH_PARAMETER,int CMP_ID)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_SELECT);

                if (!String.IsNullOrEmpty(SEARCH_PARAMETER))
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, SEARCH_PARAMETER);
                else
                    db.AddInParameter(dbCmd, "@SEARCH_PARAMETER", DbType.String, DBNull.Value);

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

        public DataSet GetEmployeeById(int EmployeeId, int CompId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_SELECT_BYID);

                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EmployeeId);

                db.AddInParameter(dbCmd, "@CompanyId", DbType.Int32, CompId);


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

        public DataSet GetEmployeeIDByUserId(int UserId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet DsEmpDtl = null;

            int EmployeeId = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_SELECT_BY_USERID);

                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                DsEmpDtl = db.ExecuteDataSet(dbCmd);
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
            return DsEmpDtl;
        }




        #endregion

        #region Delete Employee

        public int DeleteEmployee(string IdList)
        {
            int result;
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_DELETE);
                db.AddInParameter(dbCmd, "@IdList", DbType.String, IdList);
                db.AddOutParameter(dbCmd, "@errorCode", DbType.Int32, 1);
                result = db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "@errorCode") != DBNull.Value)
                {
                    return Convert.ToInt32(db.GetParameterValue(dbCmd, "@errorCode"));
                }
                else
                { return 0; }
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

        #endregion

        #region Employee profile
        #region Update Employee Profile
        public int UpdateEmployeeProfile(EmployeeBDto ObjEmployeeBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_PROFILE_UPDATE);
               // db.AddInParameter(dbCmd, "@EMP_TITLE_ID", DbType.Int32, ObjEmployeeBDto.TitleId);
                db.AddInParameter(dbCmd, "@EMP_MARITAL_STATUS", DbType.Int32, ObjEmployeeBDto.MaritalStatusId);
                db.AddInParameter(dbCmd, "@EMP_EMAIL", DbType.String, ObjEmployeeBDto.Email);
                db.AddInParameter(dbCmd, "@EMP_MOBILE", DbType.String, ObjEmployeeBDto.Mobile);
                db.AddInParameter(dbCmd, "@EMP_PHONE", DbType.String, ObjEmployeeBDto.Phone);
                db.AddInParameter(dbCmd, "@EMP_QUALIFICATION_ID", DbType.Int32, ObjEmployeeBDto.QualificationId);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, ObjEmployeeBDto.EmpId);


                db.AddInParameter(dbCmd, "@EMP_DOB", DbType.DateTime, ObjEmployeeBDto.DateofBirth);

                db.AddInParameter(dbCmd, "@EMP_GENDER", DbType.String, ObjEmployeeBDto.Gender);






                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);

                Result = db.ExecuteNonQuery(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (Result == 1)
                    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                else
                    return 0; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #region Update Employee Credential

        public int UpdateEmployeeCredential(int UserId ,int SecQtnId, String SecAns, String Passsword)
        {
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {

                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_CUSTOMER_PROFILE_CREDENTIALS_UPDATE);

                if (!String.IsNullOrEmpty(Passsword))
                    db.AddInParameter(dbCmd, "@Password", DbType.String, Passsword);
                else
                    db.AddInParameter(dbCmd, "@Password", DbType.String, DBNull.Value);
                db.AddInParameter(dbCmd, "@SecurityQuestionId", DbType.Int32, SecQtnId);
                db.AddInParameter(dbCmd, "@SecurityQuestionAns", DbType.String, SecAns);
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                Result = db.ExecuteNonQuery(dbCmd);



                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                if (Result == 1)
                    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                else
                    return 0; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #endregion

        #region Contact Details


        #region Insert Contact

        public int InsertContact(EmployeeContactBDto ObjEmployeeContactBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CONTACT_INSERT);

                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, ObjEmployeeContactBDto.EmployeeId);
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, ObjEmployeeContactBDto.ContactTypeId);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, ObjEmployeeContactBDto.Address1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, ObjEmployeeContactBDto.Address2);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, ObjEmployeeContactBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, ObjEmployeeContactBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjEmployeeContactBDto.CountryId);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, ObjEmployeeContactBDto.Pincode);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, ObjEmployeeContactBDto.Phone);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjEmployeeContactBDto.UserId);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        #region Get Contact
        public DataSet GetContact(String EmployeeName, int EmployeeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CONTACT_SELECT);

                if (!String.IsNullOrEmpty(EmployeeName))
                    db.AddInParameter(dbCmd, "@EmployeeName", DbType.String, EmployeeName);
                else
                    db.AddInParameter(dbCmd, "@EmployeeName", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EmployeeId);

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
        
        #region GetEmployeeSignature

        public DataSet GetEmployeeSignature(int EmployeeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_SIGNATURE_SELECT);
                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EmployeeId);

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

        #region Update Contact

        public int UpdateContact(EmployeeContactBDto ObjEmployeeContactBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CONTACT_UPDATE);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, ObjEmployeeContactBDto.EmployeeId);
                db.AddInParameter(dbCmd, "@ADDRESS_TYPE_ID", DbType.Int32, ObjEmployeeContactBDto.ContactTypeId);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, ObjEmployeeContactBDto.Address1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, ObjEmployeeContactBDto.Address2);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, ObjEmployeeContactBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, ObjEmployeeContactBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjEmployeeContactBDto.CountryId);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, ObjEmployeeContactBDto.Pincode);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, ObjEmployeeContactBDto.Phone);

                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjEmployeeContactBDto.UserId);
                db.AddInParameter(dbCmd, "@SRNO", DbType.Int32, ObjEmployeeContactBDto.SrNo);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
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


        #region Insert Employee Company Map

        public int InsertEmployeeCompanyMap(EmployeeCompanyMapBDto objEmployeeCompanyMapBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_COMPANY_EMPLOYEE_MAP_INSERT);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, objEmployeeCompanyMapBDto.EmployeeId);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, objEmployeeCompanyMapBDto.CompanyId);
                db.AddInParameter(dbCmd, "@DEPARTMENT_ID", DbType.Int32, objEmployeeCompanyMapBDto.DepartmentId);
                db.AddInParameter(dbCmd, "@MANAGER_ID", DbType.Int32, objEmployeeCompanyMapBDto.ManagerId);
                db.AddInParameter(dbCmd, "@DESIGNATION_ID", DbType.Int32, objEmployeeCompanyMapBDto.DesignationId);

                if (objEmployeeCompanyMapBDto.JoiningDate == DateTime.MinValue)
                    db.AddInParameter(dbCmd, "@EMP_DOJ ", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@EMP_DOJ ", DbType.DateTime, objEmployeeCompanyMapBDto.JoiningDate);
                db.AddOutParameter(dbCmd, "@IS_INSERT", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_INSERT"));
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

        #region Get Employee Company Map
        public DataSet GetEmployeeCompanyMap(String SearchParameter, int EmployeeId)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                  dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_COMPANY_MAP_SELECT);

                if (!String.IsNullOrEmpty(SearchParameter))
                    db.AddInParameter(dbCmd, "@SearchParameter", DbType.String, SearchParameter);
                else
                    db.AddInParameter(dbCmd, "@SearchParameter", DbType.String, DBNull.Value);

                db.AddInParameter(dbCmd, "@EmployeeId", DbType.Int32, EmployeeId);

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


        #region Update Company Employee MAP

        public int UpdateCompanyMap(EmployeeCompanyMapBDto ObjEmployeeCompanyMapBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_COMPANY_MAP_UPDATE);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.Int32, ObjEmployeeCompanyMapBDto.EmployeeId);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, ObjEmployeeCompanyMapBDto.CompanyId);
                db.AddInParameter(dbCmd, "@DEPARTMENT_ID", DbType.Int32, ObjEmployeeCompanyMapBDto.DepartmentId);
                db.AddInParameter(dbCmd, "@MANAGER_ID", DbType.Int32, ObjEmployeeCompanyMapBDto.ManagerId);
                db.AddInParameter(dbCmd, "@DESIGNATION_ID", DbType.Int32, ObjEmployeeCompanyMapBDto.DesignationId);
                db.AddInParameter(dbCmd, "@EMP_DOJ ", DbType.DateTime, ObjEmployeeCompanyMapBDto.JoiningDate);

                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
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

        public int DeleteContact(string IdList)
        {
            int result;
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_CONTACT_DELETE);
                db.AddInParameter(dbCmd, "@IdList", DbType.String, IdList);
                db.AddOutParameter(dbCmd, "@errorCode", DbType.Int32, 1);
                result = db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "@errorCode") != DBNull.Value)
                {
                    return Convert.ToInt32(db.GetParameterValue(dbCmd, "@errorCode"));
                }
                else
                { return 0; }
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


        public int DeleteCompanyMap(string IdList,int EmpId)
        {
            int result;
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_HR_EMPLOYEE_COMPANY_MAP_DELETE);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.String, IdList);
                db.AddInParameter(dbCmd, "@EMP_ID", DbType.String, EmpId);

                db.AddOutParameter(dbCmd, "@errorCode", DbType.Int32, 1);
                result = db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "@errorCode") != DBNull.Value)
                {
                    return Convert.ToInt32(db.GetParameterValue(dbCmd, "@errorCode"));
                }
                else
                { return 0; }
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

        #region IDisposable Members
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion




    }
}
