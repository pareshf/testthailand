using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.DataAccess.SecurityDAL;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class PanelBarNavigation : System.Web.UI.UserControl
    {
        #region Declaration
        AuthorizationDal objAuthorizationDal;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        public void BindLeftMenu(int ModuleId, int RoleId, int CompId, int DeptId)
        {
            objAuthorizationDal = new AuthorizationDal();
            DataSet dsMenuItem = new DataSet();
            DataTable dtMenuItem = new DataTable();
           
            for (int i = 1; i < 24; i++)
            {
                try
                {
                    dtMenuItem = objAuthorizationDal.FetchMenuProgram(ModuleId, RoleId, i,CompId,DeptId).Tables[0];
                    createmenuinside(dtMenuItem);
                }
                catch (Exception ex) { }
               
            }
           
        }

        private void createmenuinside(DataTable dtMenuItem)
        {
            RadPanelItem newParentItem = new RadPanelItem();
            RadPanelItem newChildItem = new RadPanelItem();
            newParentItem.Text = dtMenuItem.Rows[0]["PROGRAM_TYPE_NAME"].ToString();
            newParentItem.Expanded = true;
            RadMenu myMenu = new RadMenu();
            myMenu.Width = Unit.Percentage(100);
            myMenu.Flow = ItemFlow.Vertical;
            myMenu.Skin = "Sitefinity";
            myMenu.EnableEmbeddedSkins =true;
            myMenu.DataTextField = "PROGRAM_SUB_NAME";
            myMenu.DataFieldID = "PROGRAM_SUB_ID";
            myMenu.DataFieldParentID = "PARENT_ID";
            myMenu.DataNavigateUrlField = "PROGRAM_NAME";
            myMenu.DataSource = dtMenuItem;
            myMenu.DataBind();
            newChildItem.Controls.Add(myMenu);
            newParentItem.Items.Add(newChildItem);
            RadPanelBar2.Items.Add(newParentItem);
            foreach (RadMenuItem myItems in myMenu.GetAllItems())
            {
                myItems.Width = Unit.Percentage(100);
            }
        }
    }
}

