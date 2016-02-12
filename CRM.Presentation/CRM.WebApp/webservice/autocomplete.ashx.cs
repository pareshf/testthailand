using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CRM.DataAccess.AdministratorEntity;
using System.Runtime.Remoting.Contexts;

namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for autocomplete
    /// </summary>
    public class autocomplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string prefix = context.Request.QueryString["q"];
            
            Autosearch objautosearch = new Autosearch();
            DataTable dt = new DataTable();
            string[] query = context.Request.QueryString["key"].Split('?');

            if (query.Length > 1 && query.Length <= 2)
            {
                CRM.WebApp.webservice.AutoComplete autocmp=new AutoComplete();
                query[1] = autocmp.returnsession();
                dt = objautosearch.ReturnAutoSearchResult(prefix, query[0],query[1]);
            }
            else if (query.Length > 2)
            {
                CRM.WebApp.webservice.AutoComplete autocmp = new AutoComplete();
                query[1] = autocmp.returnsessionFromSecondFile();
                dt = objautosearch.ReturnAutoSearchResult(prefix, query[0], query[1]);
            }
            else
            {
                dt = objautosearch.ReturnAutoSearchResult(prefix, query[0]);
            }

            foreach (DataRow dr in dt.Rows)
            {
                context.Response.Write(dr["AutoSearchResult"].ToString() + Environment.NewLine);
            }
        }
        public void savevalue(HttpContext sessionval)
        {
            HttpContext.Current.Session.Add("Sessiontest", sessionval);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}