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
    /// Summary description for ProductMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProductMasterWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_PRODUCT_MASTER> PRODUCT = new ProductDataContext().VIEW_PRODUCT_MASTERs.AsQueryable<VIEW_PRODUCT_MASTER>();


        [WebMethod(EnableSession = true)]
        public List<VIEW_PRODUCT_MASTER> GetProductName(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                PRODUCT = PRODUCT.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                PRODUCT = PRODUCT.OrderBy(sortExpression);
            }
            else
            {
                PRODUCT = PRODUCT.OrderBy("PRODUCT_ID ASC");
            }

            return PRODUCT.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int ProductNameCount()
        {
            return (int)PRODUCT.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateProductName(ArrayList Product)
        {

            ProductMasterStoredProcedure objproductname = new ProductMasterStoredProcedure();
            Product.Insert(2, Session["usersid"].ToString());
            objproductname.InsertUpdateProductName(Product);

        }
        [WebMethod(EnableSession = true)]
        public void delProductName(int delproduct)
        {
            ProductMasterStoredProcedure objdelproduct = new ProductMasterStoredProcedure();
            objdelproduct.delProductName(delproduct);
        }
    }
}
