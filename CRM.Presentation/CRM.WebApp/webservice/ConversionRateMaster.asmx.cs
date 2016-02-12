using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for ConversionRateMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ConversionRateMaster : System.Web.Services.WebService
    {

        // IQueryable<VIEW_CAR_MASTER> car = new CarMasterDataContext().VIEW_CAR_MASTERs.AsQueryable<VIEW_CAR_MASTER>();
        IQueryable<VIEW_CONVERSION_RATE_MASTER> rate = new ConvertionRateMasterDataContext().VIEW_CONVERSION_RATE_MASTERs.AsQueryable<VIEW_CONVERSION_RATE_MASTER>();
       
        [WebMethod(EnableSession = true)]
        public List<VIEW_CONVERSION_RATE_MASTER> GetConvertionRate(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                rate = rate.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                rate = rate.OrderBy(sortExpression);
            }
            else
            {
                rate = rate.OrderBy("CONVERSION_RATE_ID ASC");
            }

            return rate.Skip(startIndex).Take(maximumRows).ToList();

        }

        [WebMethod(EnableSession = true)]
        public int GetConversionRateCount()
        {
            return (int)rate.Count();
        }
       
        [WebMethod(EnableSession = true)]
        public string InsertUpdateConversionMaster(ArrayList Conversion,string s)
        {
            CRM.DataAccess.AdministratorEntity.ConversionRateMaster objinsertrate = new DataAccess.AdministratorEntity.ConversionRateMaster();
            //objinsertrate.InsertUpdateConversionMaster(Conversion);
            if (int.Parse(Conversion[0].ToString()) == 0)
            {
                System.Data.DataSet ds1 = objinsertrate.CheckValidation();
                int j = 0;

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {

                    if ((ds1.Tables[0].Rows[i]["FROM_CURRENCY"].ToString().Equals(Conversion[1]) || Conversion[1].Equals("")) && (ds1.Tables[0].Rows[i]["TO_CURRENCY"].ToString().Equals(Conversion[2]) || Conversion[2].Equals("")))
                    {
                        j = 1;
                        break;

                    }

                }
                if (j == 1)
                {
                    s = "Error";
                }
                else if (j == 0)
                {
                    objinsertrate.InsertUpdateConversionMaster(Conversion);
                }

            }
            else
            {
                objinsertrate.InsertUpdateConversionMaster(Conversion);
            }
            return s;
        }
       
        [WebMethod(EnableSession = true)]
        public void deleteConversionRate(int CONVERSIONRATEID)
        {
            CRM.DataAccess.AdministratorEntity.ConversionRateMaster objdeleterate = new DataAccess.AdministratorEntity.ConversionRateMaster();
            objdeleterate.deleteConversionRate(CONVERSIONRATEID);
        }
    }
}
