using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Data;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Sales
{
    public partial class Tour2 : System.Web.UI.Page
    {
        AuthorizationDal objAuthorizationDal = new AuthorizationDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String CompId, DeptId, RoleId;
                CompId = Session["CompanyId"].ToString();
                DeptId = Session["DeptId"].ToString();
                RoleId = Session["RoleId"].ToString();

                DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 128);
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToBoolean(dr["READ_ACCESS"]) == true)
                    {
                        radgridAirlineDetail.Enabled = false;
                        radgridCruiseDetail.Enabled = false;
                        radgridHotelDetails.Enabled = false;
                    }

                    if (Convert.ToBoolean(dr["WRITE_ACCESS"]) == true)
                    {
                        radgridAirlineDetail.Enabled = true;
                        radgridCruiseDetail.Enabled = true;
                        radgridHotelDetails.Enabled = true;
                    }

                    if (Convert.ToBoolean(dr["DELETE_ACCESS"]) == true)
                    {
                        
                    }

                    if (Convert.ToBoolean(dr["PRINT_ACCESS"]) == true)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, DALHelper.DAL_EXP_POLICYNAME);
                if (rethrow)
                {
                    throw ex;
                }
            }
        }
    }
}