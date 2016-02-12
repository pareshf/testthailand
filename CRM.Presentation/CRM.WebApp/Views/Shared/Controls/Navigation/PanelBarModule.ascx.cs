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
using CRM.Model.Security;
using CRM.DataAccess.SecurityDAL;
using Telerik.Web.UI;

namespace CRM.WebApp.Views.Shared.Controls.Navigation
{
    public partial class PanelBarModule : System.Web.UI.UserControl
    {
        #region Declaration
        //AuthorizationBDto objAuthorizationBDto;
        AuthorizationDal objAuthorizationDal;

        //public event RadMenuEventHandler ItemClick;
        public event MenuEventHandler ItemClick;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.MainModuleMenu.ItemClick += this.ItemClick;
            this.menuMainModule.MenuItemClick += this.ItemClick;
        }

        #region Get Property
        /// <summary>
        /// Gets Module SelectedValue.
        /// </summary>
        public string SelectedValue
        {
            get { return menuMainModule.SelectedValue; }
        }

        #endregion

        #region Method
        public void BindModuleList(int RoleId,int DeptId,int CompId)
        {
            objAuthorizationDal = new AuthorizationDal();
            DataSet dsMenuItem = new DataSet();
            DataTable dtModuleName = new DataTable();
            dtModuleName = objAuthorizationDal.FetchModule(RoleId,DeptId,CompId).Tables[0];
            menuMainModule.Items.Clear();

          //  MenuItem itemHome = new MenuItem("HOME", "0");
          //  itemHome.Value = "0";
          //  menuMainModule.Items.Add(itemHome);
            for (int i = 0; i < dtModuleName.Rows.Count; i++)
            {
                MenuItem item = new MenuItem(dtModuleName.Rows[i]["MODULE_NAME"].ToString(), dtModuleName.Rows[i]["MODULE_ID"].ToString());
                menuMainModule.Items.Add(item);              
            }
        }
        #endregion
    }
}