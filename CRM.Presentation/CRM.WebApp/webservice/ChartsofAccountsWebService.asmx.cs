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
    /// Summary description for ChartsofAccountsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ChartsofAccountsWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_CHARTS_OF_ACCOUNT> ChartsofAccount = new ChartsofAccountDataContext().VIEW_CHARTS_OF_ACCOUNTs.AsQueryable<VIEW_CHARTS_OF_ACCOUNT>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_CHARTS_OF_ACCOUNT> GetChartsofAccounts(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                ChartsofAccount = ChartsofAccount.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                ChartsofAccount = ChartsofAccount.OrderBy(sortExpression);
            }
            else
            {
                ChartsofAccount = ChartsofAccount.OrderBy("CHART_OF_ACCOUNTS_ID ASC");
            }

            return ChartsofAccount.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int ChartsofAccountCount()
        {
            return (int)ChartsofAccount.Count();
        }
        [WebMethod(EnableSession = true)]
        public string InsertUpdateChartAccount(ArrayList ChartsofAccount,string m)
        {

            CRM.DataAccess.AdministratorEntity.ChartsofAccountsStoredProcedure objinsertaccount = new CRM.DataAccess.AdministratorEntity.ChartsofAccountsStoredProcedure();
            ChartsofAccount.Insert(14, Session["usersid"].ToString());
            //objinsertaccount.InsertUpdateChartsofAccounts(ChartsofAccount);


            System.Data.DataSet ds1 = objinsertaccount.CheckValidation();

            int j = 0;
            int k = 0;
            for (int i = 0; i < ds1.Tables[3].Rows.Count; i++)
            {

                if ((ds1.Tables[3].Rows[i]["GL_CODE"].ToString().Equals(ChartsofAccount[1]) || (ChartsofAccount[1]).Equals("")))
                {
                    j = 1;
                    break;

                }

            }
            for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            {

                if ((ds1.Tables[4].Rows[i]["GL_DESCRIPTION"].ToString().Equals(ChartsofAccount[2]) || (ChartsofAccount[2]).Equals("")))
                {
                    k = 1;
                    break;

                }

            }
            if (j == 0 && k==0)
            {
                objinsertaccount.InsertUpdateChartsofAccounts(ChartsofAccount);
            }
            else if (j == 1 || k==1)
            {
                System.Data.DataSet ds = objinsertaccount.InsertUpdateChartsofAccounts(ChartsofAccount);
                m = ds.Tables[0].Rows[0]["SUPPLIER_CHECKER"].ToString();
                if (m == "N")
                {

                }
                if (m == "Y")
                {

                }
                if (m == "NA")
                {

                }
            }
            return m;
            
        }
        [WebMethod(EnableSession = true)]
        public void delChartsofAccount(int delchartofaccount)
        {
            CRM.DataAccess.AdministratorEntity.ChartsofAccountsStoredProcedure objchartsofaccount = new CRM.DataAccess.AdministratorEntity.ChartsofAccountsStoredProcedure();
            objchartsofaccount.delchartAccount(delchartofaccount);
        }
    }
}
