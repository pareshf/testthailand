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
    /// Summary description for AddressTypeMasterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AddressTypeMasterWebService : System.Web.Services.WebService
    {

        IQueryable<View_Address_Type_Lookup> AddressType = new Address_Type_LookupDataContext().View_Address_Type_Lookups.AsQueryable<View_Address_Type_Lookup>();

        [WebMethod(EnableSession = true)]
        public List<View_Address_Type_Lookup> GetAddressType(int startIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                AddressType = AddressType.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                AddressType = AddressType.OrderBy(sortExpression);
            }
            else
            {
                AddressType = AddressType.OrderBy("ADDRESS_TYPE_ID ASC");
            }

            return AddressType.Skip(startIndex).Take(maximumRows).ToList();


        }
        [WebMethod(EnableSession = true)]
        public int GetAddressTypeCount()
        {
            return (int)AddressType.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateAddressType(ArrayList Address)
        {

            Addresstypelookupsp objaddresstype = new Addresstypelookupsp();
            Address.Insert(2, Session["usersid"].ToString());
            objaddresstype.InsertUpdateAddressType(Address);

        }
        [WebMethod(EnableSession = true)]
        public void deleteAddressType(int ADDRESSTYPEID)
        {
            Addresstypelookupsp objaddress = new Addresstypelookupsp();
            objaddress.deleteAddressType(ADDRESSTYPEID);
        }
    }
}
