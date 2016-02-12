using System;
using System.Data;
using System.Data.Common;
using CRM.Model.AdministrationModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections;

namespace CRM.DataAccess.AdministratorEntity
{
   public  class MarketingMaterial
    {
       public void InsertUpdateMarketing(string txtTour, string txtTitle, string txtType, string txtExpirationdate, string txtdescription, string txtEmbadedcode, string txtWeburl, string lblcheck)
       {
           Database db = null;
           DbCommand dbCmd = null;
           try
           {
               db = DatabaseFactory.CreateDatabase(DALHelper.CRM_CONNECTION_STRING);
               dbCmd = db.GetStoredProcCommand("INSERT_UPDATE_MARKETING_MATERIAL");
               db.AddInParameter(dbCmd, "@MAR_ID", DbType.Int32, lblcheck);
               db.AddInParameter(dbCmd, "@TOUR_NAME", DbType.String, txtTour);
               db.AddInParameter(dbCmd, "@TITLE", DbType.String, txtTitle);
               db.AddInParameter(dbCmd, "@TYPE", DbType.String,txtType);
               db.AddInParameter(dbCmd, "@EXPIRATION_DATE", DbType.DateTime, DateTime.ParseExact(txtExpirationdate.ToString(), "dd/MM/yyyy", null));
               db.AddInParameter(dbCmd, "@DESCRIPTION", DbType.String, txtdescription);
               db.AddInParameter(dbCmd, "@EMBEDCODE", DbType.String, txtEmbadedcode);
               db.AddInParameter(dbCmd, "@WEBURL", DbType.String, txtWeburl);
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
    }
}
