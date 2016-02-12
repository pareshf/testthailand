using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Ionic;
using Ionic.Zip;
using CRM.DataAccess.AdministrationDAL;
using CRM.Core.Constants;
using System.IO;

namespace CRM.WebApp.Views.Administration
{
    public partial class Backup : System.Web.UI.Page
    {       

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                txtFileName.Text = FileName();
                GetFilesAndFolders(Server.MapPath("~/Backup"));  
            }
          
            if (Session[PageConstants.ssnUserAuthorization] != null)
            {              
                Session["currentevent"] = "Manage Backup";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();

            WebHelper.WebManager.CheckUserAuthorizationForProgram("Manage Backup");
        }

        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session[PageConstants.ThemeName] == null)
                {
                    return "Default";

                }
                else
                {
                    return HttpContext.Current.Session[PageConstants.ThemeName].ToString();
                }
            }
        }

        #endregion

        #region Button Event

        protected void btnBackup_onClick(object sender, EventArgs e)
        {
            string DestinationPath = Server.MapPath("~/Backup") + "\\";            

            if (rbBackupType.SelectedValue == "1")//Only Database
            {
                if (SaveOnlyDatabase(DestinationPath))
                {                    
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[SuccessMessage.Backup].ToString());
                    Master.MessageCssClass = "successMessage";
                }
                else
                {
                    Master.DisplayMessage(ConfigurationSettings.AppSettings[FailureMessage.Backup].ToString());
                    Master.MessageCssClass = "errorMessage";
                }
                
            }
            else if (rbBackupType.SelectedValue == "2")//Full Backup
            {
                //SaveFullBackup(DestinationPath);
                SaveOnlyDatabase(DestinationPath);
            }

            //Bind Grid
            GetFilesAndFolders(DestinationPath);
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rgBackup.Items.Count; i++)
            {
                if (rgBackup.Items[i].FindControl("chkBackup") != null)
                {
                    if (((CheckBox)rgBackup.Items[i].FindControl("chkBackup")).Checked)
                    {
                        string filename = rgBackup.Items[i]["unBackupFileName"].Text.Trim();
                        string filepath = Server.MapPath("~/Backup/") + filename;

                        Response.Clear();
                        Response.ContentType = "Application/zip";
                        Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
                        Response.WriteFile(filepath);
                        Response.End();
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rgBackup.Items.Count; i++)
            {
                if (rgBackup.Items[i].FindControl("chkBackup") != null)
                {
                    if (((CheckBox)rgBackup.Items[i].FindControl("chkBackup")).Checked)
                    {
                        string filepath = Server.MapPath("~/Backup/") + rgBackup.Items[i]["unBackupFileName"].Text.Trim(); ;

                        FileInfo fInfo = new FileInfo(filepath);
                        if (fInfo.Exists)
                            fInfo.Delete();
                    }
                }
            }

            string DestinationPath = Server.MapPath("~/Backup") + "\\";
            //Bind Grid
            GetFilesAndFolders(DestinationPath);
        }

        #endregion

        #region Method

        public string FileName()
        {
            string Filename = DateTime.Now.Year + "" +
                DateTime.Now.Month + "" +
                DateTime.Now.Day + "" +
                "_" + 
                DateTime.Now.TimeOfDay.Hours + "" +
                DateTime.Now.TimeOfDay.Minutes + "" +
                DateTime.Now.TimeOfDay.Seconds;
            return Filename;
        }

        public bool SaveOnlyDatabase(string DestinationPath)
        {
            try
            {
                string GlobalFileName = txtFileName.Text.Trim();

                //string DestinationPath = ((Server.MapPath("~/Backup")).Replace("\\CRM.Presentation\\CRM.WebApp", "") + "\\" + GlobalFileName + "_Db");
                string DestinationPathTemp = DestinationPath + GlobalFileName + "\\";
                string BackupFileName = GlobalFileName + "_Db.bak";
                string ZipFileName = GlobalFileName + "_Database.zip";

                //Create Folder
                if (!Directory.Exists(@DestinationPathTemp))
                    System.IO.Directory.CreateDirectory(@DestinationPathTemp);

                //Save .bak file in folder
                using (BackupDal objBackupDal = new BackupDal())
                {
                    int Result = objBackupDal.InsertBackup(BackupFileName, DestinationPathTemp);
                }

                //Makezip file 
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(DestinationPathTemp);
                    zip.Save(DestinationPath + ZipFileName);

                    //delete original folder
                    DirectoryInfo dirDestinationPathTemp = new DirectoryInfo(DestinationPathTemp);
                    if (dirDestinationPathTemp.Exists)
                    {
                        dirDestinationPathTemp.Delete(true);
                        //OriginalBackupFile.Delete();
                    }
                }

                //Create New FileName
                GlobalFileName = FileName();
                txtFileName.Text = GlobalFileName;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SaveFullBackup(string DestinationPath)
        {
            string GlobalFileName = txtFileName.Text.Trim();
            
            string DestinationPathTemp = DestinationPath + GlobalFileName + "\\";
            string ApplicationBackupPathTemp = DestinationPathTemp + "Application\\";
            string DatabaseBackupPathTemp = DestinationPathTemp + "Database\\";
            string SourcePath = Server.MapPath("~");
            string DatabaseBackupFileName = GlobalFileName + "_Db.bak";
            string DatabaseBackupZipFileName = GlobalFileName + "_Database.zip";
            string ApplicationBackupZipFileName = GlobalFileName + "_Application.zip";
            string ZipFileName = GlobalFileName + "_Full.zip";

            //Create Folder
            if (!Directory.Exists(@DestinationPathTemp))
                Directory.CreateDirectory(@DestinationPathTemp);

            if (!Directory.Exists(@ApplicationBackupPathTemp))
                Directory.CreateDirectory(@ApplicationBackupPathTemp);

            if (!Directory.Exists(@DatabaseBackupPathTemp))
                Directory.CreateDirectory(@DatabaseBackupPathTemp);


            //Copy Application Folder            
            bool Result = CopyDirectory(SourcePath, ApplicationBackupPathTemp, true);

            string[] dirTemp = Directory.GetDirectories(ApplicationBackupPathTemp);
            foreach (string dir in dirTemp)
            {
                if (dir == "Backup")
                {
                    DirectoryInfo dirBackupInfo = new DirectoryInfo(ApplicationBackupPathTemp + dir);
                    dirBackupInfo.Delete(true);
                }
                if (dir == "ExternalAssembly")
                {
                    DirectoryInfo dirExternalAssemblyInfo = new DirectoryInfo(ApplicationBackupPathTemp + dir);
                    dirExternalAssemblyInfo.Delete(true);
                }
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(ApplicationBackupPathTemp);
                zip.Save(DestinationPathTemp + ApplicationBackupZipFileName);

                //delete original folder
                DirectoryInfo dirApplicationBackupPathTemp = new DirectoryInfo(ApplicationBackupPathTemp);
                if (dirApplicationBackupPathTemp.Exists)
                {
                    //dirApplicationBackupPathTemp.Delete(true);
                    //OriginalBackupFile.Delete();
                }
            }

            //Save .bak file in folder
            using (BackupDal objBackupDal = new BackupDal())
            {
                int ResultBackp = objBackupDal.InsertBackup(DatabaseBackupFileName, DatabaseBackupPathTemp);
            }

            //Make zip file of backup
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(DatabaseBackupPathTemp);
                zip.Save(DestinationPathTemp + DatabaseBackupZipFileName);

                //delete original folder
                DirectoryInfo dirDatabaseBackupPathTemp = new DirectoryInfo(DatabaseBackupPathTemp);
                if (dirDatabaseBackupPathTemp.Exists)
                {
                    dirDatabaseBackupPathTemp.Delete(true);
                    //OriginalBackupFile.Delete();
                }
            }

            // create Full backup zip
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(DestinationPathTemp);
                zip.Save(DestinationPath + ZipFileName);

                //delete original folder
                DirectoryInfo dirDestinationPathTemp = new DirectoryInfo(DestinationPathTemp);
                if (dirDestinationPathTemp.Exists)
                {
                    dirDestinationPathTemp.Delete(true);
                    //OriginalBackupFile.Delete();
                }
            }

            //Create New FileName
            GlobalFileName = FileName();
            txtFileName.Text = GlobalFileName;


        }

        private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);                   

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public void GetFilesAndFolders(String path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.zip", SearchOption.AllDirectories);

            DataTable dt = new DataTable();
            dt.Columns.Add("BACKUP_FILE_NAME");
            dt.Columns.Add("FILE_LENGTH");
            dt.Columns.Add("CREATED_DATE");

            DataRow dr;

            foreach (FileInfo fInfo in fileInfo)
            {
                dr = dt.NewRow();
                dr["BACKUP_FILE_NAME"] = fInfo.Name;
                dr["FILE_LENGTH"] = (fInfo.Length / 1024) / 1024;
                dr["CREATED_DATE"] = fInfo.CreationTime;

                dt.Rows.Add(dr);
            }

            dt.DefaultView.Sort = "CREATED_DATE DESC";

            rgBackup.DataSource = dt;
            rgBackup.DataBind();

            //this.GridView1.DataSource = Directory.GetFiles(path, "*.*");
            //this.GridView1.DataBind();
        }

        public void DeleteFilesAndFolders(String path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);
        }

        public void ProcessFile(string path)
        {
            FileInfo fi = new FileInfo(path);
            string[] filename = fi.FullName.Split('_');
            if (filename[filename.Length - 1] == "Application.zip")
                fi.Delete();
            if (filename[filename.Length - 1] == "Full.bak")
                fi.Delete();
            if (filename[filename.Length - 1] == "Db.bak")
                fi.Delete();
        }


        #endregion

        
        
    }
}

