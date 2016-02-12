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
    /// Summary description for SideCodeMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class SideCodeMaster : System.Web.Services.WebService
    {

       
        IQueryable<VIEW_SIDE_CODE_MASTER> side = new SideCodeMasterDataContext().VIEW_SIDE_CODE_MASTERs.AsQueryable<VIEW_SIDE_CODE_MASTER>();

        [WebMethod(EnableSession = true)]
        public List<VIEW_SIDE_CODE_MASTER> GetSideCode(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                side = side.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                side = side.OrderBy(sortExpression);
            }
            else
            {
                side = side.OrderBy("SIDE_CODE_ID ASC");
            }

            return side.Skip(startIndex).Take(maximumRows).ToList();

        }
        [WebMethod(EnableSession = true)]
        public int GetsidecodeTypeCount()
        {
            return (int)side.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdatesidecodetype(ArrayList side)
        { 
            CRM.DataAccess.AdministratorEntity.SideCodeMaster objsidecode = new DataAccess.AdministratorEntity.SideCodeMaster();
            objsidecode.InsertUpdateSideCodeMaster(side);
        }
        [WebMethod(EnableSession = true)]
        public void deletesidecode(int sidecodeid)
        {
            CRM.DataAccess.AdministratorEntity.SideCodeMaster objsidecodedel = new DataAccess.AdministratorEntity.SideCodeMaster();
            objsidecodedel.deletesidecodemaster(sidecodeid);
        }
    }
}
