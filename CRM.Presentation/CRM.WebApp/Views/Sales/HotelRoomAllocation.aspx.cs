using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Telerik.Web.Design;
using Telerik.Web.UI.Grid;
using System.Web.Configuration;
using CRM.DataAccess.AdministratorEntity;
using System.Configuration;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Data;


namespace CRM.WebApp.Views.Sales
{
    public partial class HotelRoomAllocation : System.Web.UI.Page
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

                DataTable dt = objAuthorizationDal.GetPageRights(Convert.ToInt32(CompId), Convert.ToInt32(DeptId), Convert.ToInt32(RoleId), 96);
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToBoolean(dr["READ_ACCESS"]) == true)
                    {
                        radgridmaster.Enabled = false;
                        radgridHotelDetails.Enabled = false;
                        radgridRoomDetails.Enabled = false;
                        btnFamily.Visible = false;
                        btnFamilyDev.Visible = false;
                        btnTour.Visible = false;
                        btnTourDev.Visible = false;

                    }

                    if (Convert.ToBoolean(dr["WRITE_ACCESS"]) == true)
                    {
                        radgridmaster.Enabled = true;
                        radgridHotelDetails.Enabled = true;
                        radgridRoomDetails.Enabled = true;
                        btnFamily.Visible = false;
                        btnFamilyDev.Visible = false;
                        btnTour.Visible = false;
                        btnTourDev.Visible = false;
                    }

                    if (Convert.ToBoolean(dr["DELETE_ACCESS"]) == true)
                    {

                    }

                    if (Convert.ToBoolean(dr["PRINT_ACCESS"]) == true)
                    {
                        radgridmaster.Enabled = true;
                        radgridHotelDetails.Enabled = true;
                        radgridRoomDetails.Enabled = true;
                        btnFamily.Visible = true;
                        btnFamilyDev.Visible = true;
                        btnTour.Visible = true;
                        btnTourDev.Visible = true;
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