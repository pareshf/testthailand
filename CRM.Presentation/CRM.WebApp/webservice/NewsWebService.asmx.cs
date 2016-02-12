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
    /// Summary description for NewsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NewsWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_NEWS_MASTER> News = new NewsDataContext().VIEW_NEWS_MASTERs.AsQueryable<VIEW_NEWS_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_NEWS_MASTER> GetNews(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                News = News.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                News = News.OrderBy(sortExpression);
            }
            else
            {
                News = News.OrderBy("NEWS_ID ASC");
            }

            return News.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetNewsCount()
        {
            return (int)News.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateNews(ArrayList News)
        {

            CRM.DataAccess.AdministratorEntity.NewsStoredProcedure objinsertNews = new CRM.DataAccess.AdministratorEntity.NewsStoredProcedure();
            objinsertNews.InsertUpdateNews(News);

        }
        [WebMethod(EnableSession = true)]
        public void delNews(int newsid)
        {
            CRM.DataAccess.AdministratorEntity.NewsStoredProcedure objdelnews = new CRM.DataAccess.AdministratorEntity.NewsStoredProcedure();
            objdelnews.delNews(newsid);
        }
    }
}
