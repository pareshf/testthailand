using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Imaging;
namespace CRM.WebApp.Views.Shared.Controller
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageController : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlDataReader dr = null;
            SqlConnection myConnection = null;
            SqlCommand cmd = null;

            string Id = context.Request.QueryString["id"].ToString();
            string PhotoType = context.Request.QueryString["phototype"].ToString();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
                string CommandString = "";
                myConnection = new SqlConnection(conn);

                myConnection.Open();

                switch (PhotoType)
                {
                    case "Customer":

                        CommandString = "SELECT PHOTO, PHOTO_CONTENT_TYPE FROM dbo.CUST_CUSTOMER_MASTER WHERE CUST_ID = @CUST_ID";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@CUST_ID", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["PHOTO_CONTENT_TYPE"].ToString();
                        context.Response.BinaryWrite((byte[])dr["PHOTO"]);
                        break;

                    case "Employee":

                        CommandString = "SELECT PHOTO, PHOTO_CONTENT_TYPE FROM dbo.EMP_EMPLOYEE_MASTER WHERE EMP_ID=@EMP_ID";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@EMP_ID", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["PHOTO_CONTENT_TYPE"].ToString();
                        context.Response.BinaryWrite((byte[])dr["PHOTO"]);
                        break;

                    case "Room":

                        CommandString = "SELECT PHOTO, PHOTO_CONTENT_TYPE FROM dbo.FARE_HOTEL_ROOM_DETAILS WHERE SR_NO=@SR_NO";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["PHOTO_CONTENT_TYPE"].ToString();
                        context.Response.BinaryWrite((byte[])dr["PHOTO"]);
                        break;


                    case "Company":

                        CommandString = "SELECT LOGO, LOGO_CONTENT_TYPE FROM dbo.COMPANY_MASTER WHERE COMPANY_ID = @COMPANY_ID";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["LOGO_CONTENT_TYPE"].ToString();
                        context.Response.BinaryWrite((byte[])dr["LOGO"]);

                        break;

                    case "Tour":

                        CommandString = "SELECT WEB_PHOTO, WEB_PHOTO_CONTENT FROM dbo.FARE_TOUR_MASTER WHERE TOUR_ID = @TOUR_ID";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@TOUR_ID", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["WEB_PHOTO_CONTENT"].ToString();
                        context.Response.BinaryWrite((byte[])dr["WEB_PHOTO"]);
                        break;



                }
            }
            catch (Exception Ex) { }
            finally
            {

                myConnection.Close();
                context.Response.Flush();
                context.Response.End();
            }


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
