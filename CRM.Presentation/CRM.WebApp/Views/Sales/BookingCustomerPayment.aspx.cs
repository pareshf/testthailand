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
    public partial class BookingCustomerPayment : System.Web.UI.Page
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
                        radgridcustomer.Enabled = false;
                        radgridbookingpaymentdetail.Enabled = false;
                        btnsummary.Enabled = false;
                    }

                    if (Convert.ToBoolean(dr["WRITE_ACCESS"]) == true)
                    {
                        radgridcustomer.Enabled = true;
                        radgridbookingpaymentdetail.Enabled = true;
                        btnsummary.Enabled = false;

                    }
                    if (Convert.ToBoolean(dr["PRINT_ACCESS"]) == true)
                    {
                        radgridcustomer.Enabled = true;
                        radgridbookingpaymentdetail.Enabled = true;
                        btnsummary.Enabled = true;
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