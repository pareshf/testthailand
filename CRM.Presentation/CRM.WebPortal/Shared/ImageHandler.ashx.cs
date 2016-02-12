using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

namespace CRM.WebPortal.Shared
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlDataReader dr = null;
            SqlConnection myConnection = null;
            SqlCommand cmd = null;

            string Id = context.Request.QueryString["id"].ToString();
            string PhotoType = context.Request.QueryString["phototype"].ToString();

            //string binary = context.Request.QueryString["Binary"].ToString();
            //object o = context.Request.QueryString["Binary"];
            //string contentType = context.Request.QueryString["ContentType"].ToString();

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
                string CommandString = "";
                myConnection = new SqlConnection(conn);
                myConnection.Open();

                switch (PhotoType)
                {
                    case "tour":
                        CommandString = "SELECT WEB_PHOTO, WEB_PHOTO_CONTENT FROM dbo.FARE_TOUR_MASTER WHERE TOUR_ID = @TOUR_ID";
                        cmd = new SqlCommand(CommandString, myConnection);
                        cmd.Parameters.Add("@TOUR_ID", SqlDbType.NVarChar).Value = Id;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        context.Response.ContentType = dr["WEB_PHOTO_CONTENT"].ToString();
                        context.Response.BinaryWrite((byte[])dr["WEB_PHOTO"]);
                        break;
                }
                //if (HttpContext.Current.Items["DataRow"] != null)
                //{
                //    DataRow dr = (DataRow)context.Items["DataRow"];
                //    if (dr != null && !string.IsNullOrEmpty(Convert.ToString(dr["WEB_PHOTO_CONTENT"])))
                //    {
                //        context.Response.ContentType = dr["WEB_PHOTO_CONTENT"].ToString();
                //        context.Response.BinaryWrite((byte[])dr["WEB_PHOTO"]);
                //    }
                //}
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
