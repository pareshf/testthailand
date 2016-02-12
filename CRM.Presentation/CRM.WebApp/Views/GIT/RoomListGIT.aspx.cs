using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using CRM.DataAccess.GIT;
using System.Data;
using System.Data.Common;
using System.Collections;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using CRM.WebApp.WebHelper;
using CRM.DataAccess.AdministratorEntity;
using CRM.Model.AdministrationModel;
using CRM.DataAccess;
using System.Globalization;
using CRM.DataAccess.SecurityDAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CRM.WebApp.Views.GIT
{
    public partial class RoomListGIT : System.Web.UI.Page
    {

        #region VARIABLES
        string tourId;
        #endregion

        #region CLASS OBJECT
        GitDetail objGitDetail = new GitDetail();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();

                    DataSet dtroomlist = objGitDetail.GETROOMLIST("FETCH_ROOM_LIST_GIT", int.Parse(tourId));
                    if (dtroomlist.Tables[0].Rows.Count != 0)
                    {
                        lblView.Text = dtroomlist.Tables[0].Rows[0]["ROOM_LIST_FILE"].ToString();
                        ViewRoomList.HRef = "~/RoomList/" + tourId + "/" + lblView.Text;
                        lblView.Visible = true;
                        ViewRoomList.Visible = true;
                        DeleteRoomlist.Visible = true;
                        btnSave.Enabled = false;
                        btnSkip.Visible = true;
                    }
                }
            }
            else
            {
                if (Request["TOURID"] != null && !string.IsNullOrEmpty(Request["TOURID"].ToString()))
                {
                    tourId = Request.QueryString["TOURID"].ToString();

                }
            }
        }

        #region SAVE FILE
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/RoomList/" + tourId + "/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/RoomList/" + tourId + "/"));
            }
            if (FileUpload1.HasFile)
            {
                string filename = System.IO.Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/RoomList/" + tourId + "/") + filename);
                lblView.Text = filename;
                objGitDetail.INSERTROOMLIST("INSERT_UPDATE_ROOM_LIST_GIT", int.Parse(tourId), 0, filename);
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Room List Save Successfully.')", true);
                ViewRoomList.HRef = "~/RoomList/" + tourId + "/" + filename;
                lblView.Visible = true;
                ViewRoomList.Visible = true;
                DeleteRoomlist.Visible = true;
                btnSave.Enabled = false;
                upRoomList.Update();
                Response.Redirect("~/Views/GIT/GITPayment.aspx?TOURID=" + tourId);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Please Select File for Upload.')", true);
            }
            
        }
        #endregion

        #region DOWNLOAD FILE

        protected void btnDownloadRoomList_Click(object sender, EventArgs e)
        {
            try
            {
                downloadFile(tourId);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }

        }

        protected void downloadFile(string tourId)
        {
            try
            {
                string filename = lblView.Text;
                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(HttpContext.Current.Server.MapPath("~/RoomList/" + tourId + "/" + filename), System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.TransmitFile(HttpContext.Current.Server.MapPath("~/RoomList/" + tourId + "/" + filename));
                HttpContext.Current.ApplicationInstance.CompleteRequest();  
                fs = null;


                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/GIT/GITPayment.aspx?TOURID=" + tourId);
        }
        
        #endregion

        #region DELETE FILE

        protected void btnDeleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                deleteFile(tourId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }

        }

        protected void deleteFile(string tourId)
        {
            try
            {
                if (lblView.Text != "")
                {
                    string filename = lblView.Text;
                    FileInfo myfileinf = new FileInfo(HttpContext.Current.Server.MapPath("~/RoomList/" + tourId + "/" + filename));
                    myfileinf.Delete();
                    objGitDetail.DELETEROOMLIST("DELETE_ROOM_LIST_GIT", int.Parse(tourId));
                    
                    lblView.Visible = false;
                    ViewRoomList.Visible = false;
                    DeleteRoomlist.Visible = false;
                    btnSave.Enabled = true;
                    btnSkip.Visible = false;
                    upRoomList.Update();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), new Random(123).ToString(), "alert('Room List Deleted Successfully..')", true);                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
        }

        #endregion
    }
}