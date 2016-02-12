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
    /// Summary description for CommonMealMasterWebservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CommonMealMasterWebservice : System.Web.Services.WebService
    {
           
             IQueryable<VIEW_COMMON_MEAL_MASTER> CommonMeal = new CommonMealMasterDataContext().VIEW_COMMON_MEAL_MASTERs.AsQueryable<VIEW_COMMON_MEAL_MASTER>();

            [WebMethod(EnableSession = true)]
            public List<VIEW_COMMON_MEAL_MASTER> GetCommonMeal(int startIndex, int maximumRows, string sortExpression, string filterExpression)
            {
                if (!String.IsNullOrEmpty(filterExpression))
                {
                    CommonMeal = CommonMeal.Where(filterExpression);
                }
                if (!String.IsNullOrEmpty(sortExpression))
                {
                    CommonMeal = CommonMeal.OrderBy(sortExpression);
                }
                else
                {
                    CommonMeal = CommonMeal.OrderBy("Meal_ID ASC");
                }

                return CommonMeal.Skip(startIndex).Take(maximumRows).ToList();


            }
            [WebMethod(EnableSession = true)]
            public int GetCommonMealCount()
            {
                return (int)CommonMeal.Count();
            }
            [WebMethod(EnableSession = true)]
            public void InsertUpdateCommonMeal(ArrayList CommonMeal)
            {

               
                CommonMealMasterSp objCommonMeal = new CommonMealMasterSp();
                CommonMeal.Insert(2, Session["usersid"].ToString());
                objCommonMeal.InsertUpdateCommonMeal(CommonMeal);

            }
            [WebMethod(EnableSession = true)]
            public void deleteCommonMeal(int MEALID)
            {
                
              
                CommonMealMasterSp objCommonMeal = new CommonMealMasterSp();
                objCommonMeal.deleteCommonMeal(MEALID);
            }
        }
    
}
