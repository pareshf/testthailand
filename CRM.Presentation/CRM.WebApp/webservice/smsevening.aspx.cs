using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace CRM.WebApp.webservice
{
    public partial class smsevening : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            SqlCommand comm = new SqlCommand("SMS_NOTIFICATION_EVENING");
            comm.Connection = conn;
            comm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);

            foreach (DataTable dt in ds.Tables)
            {
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            WebHelper.WebManager.SendSMS(dr["mobileno"].ToString(), dr["messagealert"].ToString());
                        }
                        catch (Exception ex) { }
                    }
                }
                catch (Exception ex)
                { }
            }
        }
    }
}
