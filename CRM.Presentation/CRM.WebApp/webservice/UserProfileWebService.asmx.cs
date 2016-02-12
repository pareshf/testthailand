using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CRM.DataAccess.Dashboard;
using CRM.DataAccess.AdministratorEntity;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;


namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for UserProfileWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserProfileWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_FOR_MY_PROFILE> MYPROFILE = new UserProfileDataContext().VIEW_FOR_MY_PROFILEs.AsQueryable<VIEW_FOR_MY_PROFILE>();
        IQueryable<VIEW_USER_DETAIL_FOR_MYPROFILE> USERDETAIL = new UserProfileDataContext().VIEW_USER_DETAIL_FOR_MYPROFILEs.AsQueryable<VIEW_USER_DETAIL_FOR_MYPROFILE>();


        [WebMethod(EnableSession = true)]
        public ArrayList GetProfileInfo(string EMP_ID)
        {
            MYPROFILE = MYPROFILE.Where(String.Format(@"EMP_ID=={0}", EMP_ID));
            ArrayList arr = new ArrayList();
            arr.Add(MYPROFILE.ToList());
            return arr;
        }
        [WebMethod(EnableSession = true)]
        public ArrayList GetUserInfo(string EMP_ID)
        {
            USERDETAIL = USERDETAIL.Where(String.Format(@"EMP_ID=={0}", EMP_ID));
            ArrayList arr = new ArrayList();
            arr.Add(USERDETAIL.ToList());
            return arr;
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateProfileInfo(ArrayList Profile)
        {
            UserProfileStoredProcedure objeditprofile = new UserProfileStoredProcedure();
            Profile.Insert(14, Session["usersid"].ToString());
            objeditprofile.InsertUpdateProfileInfo(Profile);

        }
        [WebMethod(EnableSession = true)]
        public void UpdateUserDetail(ArrayList UserDetail)
        {
            CRM.DataAccess.AdministratorEntity.UserProfileStoredProcedure objupdateuserdetail = new CRM.DataAccess.AdministratorEntity.UserProfileStoredProcedure();
            UserDetail.Insert(6, Session["usersid"].ToString());
            objupdateuserdetail.UpdateUserDetails(UserDetail);

            
        }
        [WebMethod(EnableSession = true)]
        public string UpdateUserDetailValidation(ArrayList UserDetail, string s)
        {
            UserProfileStoredProcedure objupdateuserdetail = new UserProfileStoredProcedure();
            UserDetail.Insert(6, Session["usersid"].ToString());
            //objupdateuserdetail.UpdateUserDetail(UserDetail);

            System.Data.DataSet ds1 = objupdateuserdetail.CheckValidation();

            int j = 0;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                if (ds1.Tables[0].Rows[i]["USER_NAME"].ToString().Equals(UserDetail[0]) || UserDetail[0].Equals(""))
                {
                    j = 1;
                    break;

                }

            }

            if (j == 0)
            {


                objupdateuserdetail.UpdateUserDetailValidation(UserDetail);
            }
            else if (j == 1)
            {


                System.Data.DataSet ds = objupdateuserdetail.UpdateUserDetailValidation(UserDetail);

                s = ds.Tables[0].Rows[0]["EMAIL_CHECKER"].ToString();
                if (s == "N")
                {

                }
                if (s == "Y")
                {

                }

            }
            else
            {

            }
            return s;

        }

    }
    
}
