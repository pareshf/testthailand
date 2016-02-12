using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;

namespace CRM.DataAccess.AdministrationDAL
{
   public class CompanyMasterDal :IDisposable
   {


        #region Company Details
        #region Insert Company

       public int InsertCompany(CompanyBDto ObjCompanyBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_INSERT);
                db.AddInParameter(dbCmd,"@IS_COMPANY_FRANCHISE",DbType.String,ObjCompanyBDto.CompanyType);
                db.AddInParameter(dbCmd,"@COMPANY_NAME",DbType.String,ObjCompanyBDto.CompanyName);
                db.AddInParameter(dbCmd,"@ADDRESS_LINE1",DbType.String,ObjCompanyBDto.AddressLine1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, ObjCompanyBDto.AddressLine2);
                db.AddInParameter(dbCmd,"@CITY_ID",DbType.Int32,ObjCompanyBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, ObjCompanyBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjCompanyBDto.CountryId);
                db.AddInParameter(dbCmd,"@PINCODE",DbType.String,ObjCompanyBDto.Pincode);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, ObjCompanyBDto.Mobile);
                db.AddInParameter(dbCmd,"@PHONE",DbType.String,ObjCompanyBDto.Phone);
                db.AddInParameter(dbCmd,"@FAX",DbType.String,ObjCompanyBDto.Fax);
                db.AddInParameter(dbCmd,"@EMAIL_ID",DbType.String,ObjCompanyBDto.Email);
                db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, ObjCompanyBDto.RegionId);

                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, ObjCompanyBDto.Photo);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, ObjCompanyBDto.Phototype);


                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjCompanyBDto.UserId);
                db.AddOutParameter(dbCmd,"@IS_INSERT", DbType.Int32, 1);
                db.AddInParameter(dbCmd, "@PARENT_COMPANY_ID", DbType.Int32, ObjCompanyBDto.ParentCompanyId);



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



       #region Insert Department Company Map

       public int InsertDepartmentCompanyMap(int Company_Id, String Dept_Id)
       {
           Database db = null;
           DbCommand dbCmd = null;
           DataSet ds = null;
           int Result = 0;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);

               dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_DEPARTMENT_MAP_INSERT);
               db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, Company_Id);
               db.AddInParameter(dbCmd, "@DEPARTMENT_ID", DbType.String, Dept_Id);
             
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



        #region Update Company

        public int UpdateCompany(CompanyBDto ObjCompanyBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_UPDATE);
                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.String, ObjCompanyBDto.CompanyId);
                db.AddInParameter(dbCmd, "@IS_COMPANY_FRANCHISE", DbType.String, ObjCompanyBDto.CompanyType);
                db.AddInParameter(dbCmd, "@COMPANY_NAME", DbType.String, ObjCompanyBDto.CompanyName);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE1", DbType.String, ObjCompanyBDto.AddressLine1);
                db.AddInParameter(dbCmd, "@ADDRESS_LINE2", DbType.String, ObjCompanyBDto.AddressLine2);
                db.AddInParameter(dbCmd, "@CITY_ID", DbType.Int32, ObjCompanyBDto.CityId);
                db.AddInParameter(dbCmd, "@STATE_ID", DbType.Int32, ObjCompanyBDto.StateId);
                db.AddInParameter(dbCmd, "@COUNTRY_ID", DbType.Int32, ObjCompanyBDto.CountryId);
                db.AddInParameter(dbCmd, "@PINCODE", DbType.String, ObjCompanyBDto.Pincode);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, ObjCompanyBDto.Mobile);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, ObjCompanyBDto.Phone);
                db.AddInParameter(dbCmd, "@FAX", DbType.String, ObjCompanyBDto.Fax);
                db.AddInParameter(dbCmd, "@EMAIL_ID", DbType.String, ObjCompanyBDto.Email);
                db.AddInParameter(dbCmd, "@REGION_ID", DbType.Int32, ObjCompanyBDto.RegionId);

                db.AddInParameter(dbCmd, "@PHOTO", DbType.Binary, ObjCompanyBDto.Photo);
                db.AddInParameter(dbCmd, "@PHOTO_CONTENT_TYPE", DbType.String, ObjCompanyBDto.Phototype);


                db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjCompanyBDto.UserId);
                db.AddInParameter(dbCmd, "@PARENT_COMPANY_ID", DbType.Int32, ObjCompanyBDto.ParentCompanyId);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);



                ds = db.ExecuteDataSet(dbCmd);
                Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));


                if (db.GetParameterValue(dbCmd, "@IS_UPDATE") != DBNull.Value)
                    Result = Convert.ToInt32(db.GetParameterValue(dbCmd, "@IS_UPDATE"));
                //if (Result == 1)
                //    return 1; // SUCCESSFUL INSERTION RETURN TRUE
                //else
                return Result; // UNSUCCESSFUL INSERTION RETUN FALSE ( ALREADY EXISTS )

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

        #region Get Company
        public DataSet GetCompany(String SEARCH_PARAMETER)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_SELECT);

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

        #region Delete Company

        public int DeleteCompany(string IdList)
        {
            int result;
            Database db = null;
            DbCommand dbCmd = null;
         
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_DELETE);
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

        #region Get Contact Details
        public DataSet GetContactDetails(int CompanyId,String SEARCH_PARAMETER)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null; 
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_CONTACT_SELECT);
                db.AddInParameter(dbCmd, "@Company_Id", DbType.Int32, CompanyId);


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

        #region Insert Company Contact

        public int InsertCompanyContact(CompanyContactBDto ObjCompanyContactBDto)
        {
            Database db = null;
            DbCommand dbCmd = null;
            DataSet ds = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_CONTACT_INSERT);

                db.AddInParameter(dbCmd, "@COMPANY_ID", DbType.Int32, ObjCompanyContactBDto.CompanyId);
                db.AddInParameter(dbCmd, "@TITLE_ID", DbType.Int32, ObjCompanyContactBDto.TitleId);
                db.AddInParameter(dbCmd, "@NAME", DbType.String, ObjCompanyContactBDto.Name);
                db.AddInParameter(dbCmd, "@EMAIL", DbType.String, ObjCompanyContactBDto.Email);
                db.AddInParameter(dbCmd, "@MOBILE", DbType.String, ObjCompanyContactBDto.Mobile);
                db.AddInParameter(dbCmd, "@PHONE", DbType.String, ObjCompanyContactBDto.Phone);




             //   db.AddInParameter(dbCmd, "@USER_ID", DbType.Int32, ObjCompanyContactBDto.UserId);
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

        public int DeleteContact(string IdList)
        {
            int result;
            Database db = null;
            DbCommand dbCmd = null;

            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_CONTACT_DELETE);
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

        #region Update Contact Details

        public int UpdateContact(String xmlData)
        {
            Database db = null;
            DbCommand dbCmd = null;
            int Result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
                dbCmd = db.GetStoredProcCommand(DALHelper.USP_ADMINISTRATION_COMPANY_CONTACT_UPDATE);
                db.AddInParameter(dbCmd, "@XML_DATA", DbType.Xml, xmlData);
                db.AddOutParameter(dbCmd, "@IS_UPDATE", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCmd);
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
            return Result;
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
