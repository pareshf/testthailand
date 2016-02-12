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
    /// Summary description for FareCruiseCarbinCategoryMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class FareCruiseCarbinCategoryMaster : System.Web.Services.WebService
    {
  
        
        IQueryable<VIEW_FARE_CRUISE_CABIN_CATEGORY_MASTER> CategoryType = new FareCruiseCarbinCategoryMasterDataContext().VIEW_FARE_CRUISE_CABIN_CATEGORY_MASTERs.AsQueryable<VIEW_FARE_CRUISE_CABIN_CATEGORY_MASTER>();
        
        [WebMethod(EnableSession = true)]
        public List<VIEW_FARE_CRUISE_CABIN_CATEGORY_MASTER> GetCategoryType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                CategoryType = CategoryType.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                CategoryType = CategoryType.OrderBy(sortExpression);
            }
            else
            {
                CategoryType = CategoryType.OrderBy("CATEGORY_ID ASC");
            }

            return CategoryType.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetCategoryTypeCount()
        {
            return (int)CategoryType.Count();
        }

        [WebMethod(EnableSession = true)]
        public void InsertUpdateACategoryType(ArrayList Category)
        {

            FareCruiseCarbinCategoryMas objcategorytype = new FareCruiseCarbinCategoryMas();
        //    Category.Insert(2, Session["usersid"].ToString());
            objcategorytype.InsertUpdateACategoryType(Category);

        }
        [WebMethod(EnableSession = true)]
        public void deleteCategoryType(int CategoryId)
        {
            FareCruiseCarbinCategoryMas objcategory = new FareCruiseCarbinCategoryMas();
            objcategory.deleteCategoryType(CategoryId);
        }
    }
}
