using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using SSRSReporting;
using System.Web.Configuration;
using CRM.DataAccess.AdministratorEntity;
using System.Configuration;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CRM.DataAccess;
using System.Data;
using Telerik.Web.UI;


namespace CRM.WebApp.Views.Sales
{
    public partial class BookingFormStep2 : System.Web.UI.Page
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
                        radgridbookingmaster.Enabled = false;
                        radgridbookinginformationdetail.Enabled = false;
                        radgridbookingpaymentdetail.Enabled = false;
                        btnprint.Visible = false;

                    }

                    if (Convert.ToBoolean(dr["WRITE_ACCESS"]) == true)
                    {
                        radgridbookingmaster.Enabled = true;
                        radgridbookinginformationdetail.Enabled = true;
                        radgridbookingpaymentdetail.Enabled = true;
                        btnprint.Visible = false;
                    }

                    if (Convert.ToBoolean(dr["DELETE_ACCESS"]) == true)
                    {

                    }

                    if (Convert.ToBoolean(dr["PRINT_ACCESS"]) == true)
                    {
                        radgridbookingmaster.Enabled = true;
                        radgridbookinginformationdetail.Enabled = true;
                        radgridbookingpaymentdetail.Enabled = true;
                        btnprint.Visible = true;
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