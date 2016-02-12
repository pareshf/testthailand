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
    /// Summary description for MealMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MealMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_MEAL_MASTER> MEAL = new MealMasterDataContext().VIEW_MEAL_MASTERs.AsQueryable<VIEW_MEAL_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_MEAL_MASTER> GetMealType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                MEAL = MEAL.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                MEAL = MEAL.OrderBy(sortExpression);
            }
            else
            {
                MEAL = MEAL.OrderBy("MEAL_ID ASC");
            }

            return MEAL.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetMealTypeCount()
        {
            return (int)MEAL.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateMeal(ArrayList Meal)
        {

            MealMasterStoredProcedure objmealinsert = new MealMasterStoredProcedure();
            objmealinsert.InsertUpdateMeal(Meal);

        }
        [WebMethod(EnableSession = true)]
        public void deleteMeal(int delmeal)
        {
            MealMasterStoredProcedure objdelmeal = new MealMasterStoredProcedure();
            objdelmeal.deleteMeal(delmeal);
        }
    }
}
