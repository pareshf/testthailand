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
    /// Summary description for GroupType
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GroupType : System.Web.Services.WebService
    {
        
        IQueryable<VIEW_GROUP_TYPE_MASTER> group = new GroupTypeMasterDataContext().VIEW_GROUP_TYPE_MASTERs.AsQueryable<VIEW_GROUP_TYPE_MASTER>();
        [WebMethod(EnableSession = true)]
        public List<VIEW_GROUP_TYPE_MASTER> GetGroupType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                group = group.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                group = group.OrderBy(sortExpression);
            }
            else
            {
                group = group.OrderBy("GROUP_TYPE_ID ASC");
            }

            return group.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetGroupTypeCount()
        {
            return (int)group.Count();
        }
        [WebMethod(EnableSession = true)]
        public void deleteGroupType(int GROUPTYPEID)
        {
            CRM.DataAccess.AdministratorEntity.GroupTypeMaster objdelgroup = new DataAccess.AdministratorEntity.GroupTypeMaster();
            objdelgroup.deleteGroupType(GROUPTYPEID);
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateGroupType(ArrayList Group)
        {
            CRM.DataAccess.AdministratorEntity.GroupTypeMaster objinsertgroup = new DataAccess.AdministratorEntity.GroupTypeMaster();
            objinsertgroup.InsertUpdateGroupTypeMaster(Group);
        }
    }
        
    }

