
#region importsassemblies

using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.DataAccess;
using System.Data;
using CRM.DataAccess.Dashboard;
using CRM.DataAccess.SecurityDAL;

#endregion



namespace CRM.WebApp.Views.Workplace
{
    public partial class AccountDashBoard : System.Web.UI.Page
    {

        private int _userID;
        AuthorizationBDto objAuthorizationBDto;
        DefaultGadgetsDAL objDefaultGadgetsDAL;
        deshboardentity objdeshboardentity = new deshboardentity();
        private string skin = "Sitefinity";

        #region Page Events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            WebHelper.WebManager.CheckSessionIsActive();
            objAuthorizationBDto = (AuthorizationBDto)Session[PageConstants.ssnUserAuthorization];
            if (objAuthorizationBDto != null)
                _userID = objAuthorizationBDto.UserProfile.UserId;
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

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                DataTable _DT = objdeshboardentity.getfilter(_userID).Tables[0];
                if (_DT.Rows.Count <= 0)
                {
                    _DT.Rows.Add(_DT.NewRow());
                    _DT.Rows[0]["FROM_DATE"] = System.DateTime.Now.ToString("MM/dd/yyyy");
                    _DT.Rows[0]["TO_DATE"] = System.DateTime.Now.ToString("MM/dd/yyyy");
                }
                txtfrom.Text = _DT.Rows[0]["FROM_DATE"].ToString();
                txtTo.Text = _DT.Rows[0]["TO_DATE"].ToString();
                //  if(_DT.Rows[0]["FROM_DATE"].ToString() != "")
                // radcalfrom.SelectedDate = Convert.ToDateTime(_DT.Rows[0]["FROM_DATE"]);
                //  if(_DT.Rows[0]["TO_DATE"].ToString() != "")
                // radcalto.SelectedDate = Convert.ToDateTime(_DT.Rows[0]["TO_DATE"]);

                DataTable dtfilter = objdeshboardentity.fillfilterdata().Tables[0];
                DataTable dtEmployee = objdeshboardentity.fillEmployeedata(_userID).Tables[0];

                // fill employee data
                ddlEmployee.DataTextField = "EMP_NAME";
                ddlEmployee.DataValueField = "EMP_ID";
                ddlEmployee.DataSource = dtEmployee;
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "-Select Employee-");
                if (_DT.Rows[0]["EMP_ID"].ToString() != "" && _DT.Rows[0]["EMP_ID"].ToString() != "0")
                    ddlEmployee.Items.FindByValue(_DT.Rows[0]["EMP_ID"].ToString()).Selected = true;
                //fill filter data
                ddlfilter.DataTextField = "CRITERIA_NAME";
                ddlfilter.DataValueField = "CRITERIA_ID";
                ddlfilter.DataSource = dtfilter;
                ddlfilter.DataBind();
                ddlfilter.Items.Insert(0, "-Select Filter-");
                if (_DT.Rows[0]["FILTER_ID"].ToString() != "" && _DT.Rows[0]["FILTER_ID"].ToString() != "0")
                    ddlfilter.Items.FindByValue(_DT.Rows[0]["FILTER_ID"].ToString()).Selected = true;

                //chk for already registerd gadgets
                // gadget adding and removing method
                //end gadget add and remove method
                //end gadget
                loadalldashboard();
            }

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //Recreate the docks in order to ensure their proper operation
            for (int i = 0; i < CurrentDockStates.Count; i++)
            {
                if (CurrentDockStates[i].Closed == false)
                {
                    RadDock dock = CreateRadDockFromState(CurrentDockStates[i]);
                    RadDockLayout1.Controls.Add(dock);
                    CreateSaveStateTrigger(dock);
                    LoadWidget(dock);
                }
            }
        }
        protected void radcalfrom_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {

            txtfrom.Text = radcalfrom.SelectedDate.ToString("MM/dd/yyyy");

        }

        protected void radcalto_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {

            txtTo.Text = radcalto.SelectedDate.ToString("MM/dd/yyyy");

        }
        protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtfrom.Text = "";
            txtTo.Text = "";
        }
        protected void RadDockLayout1_LoadDockLayout(object sender, DockLayoutEventArgs e)
        {
            foreach (DockState state in CurrentDockStates)
            {
                e.Positions[state.UniqueName] = state.DockZoneID;
                e.Indices[state.UniqueName] = state.Index;
            }
        }

        protected void RadDockLayout1_SaveDockLayout(object sender, DockLayoutEventArgs e)
        {
            List<DockState> stateList = RadDockLayout1.GetRegisteredDocksState();
            StringBuilder serializedList = new StringBuilder();
            int i = 0;

            // 
            ArrayList arrCurrentDockTags = new ArrayList();
            ArrayList arrClosedDockTags = new ArrayList();
            //

            while (i < stateList.Count)
            {
                serializedList.Append(stateList[i].ToString());
                serializedList.Append("|");

                //add tag (Custom Gadget's Path) to array which are not closed.
                if (!stateList[i].Closed)
                {
                    arrCurrentDockTags.Add(stateList[i].Tag);
                }
                else
                {
                    arrClosedDockTags.Add(stateList[i].Tag);
                }
                i++;
            }

            //code to hide check box and show label 'Added' in Gadget Box

            string dockState = serializedList.ToString();
            int Result;
            if (dockState.Trim() != String.Empty)
            {
                objDefaultGadgetsDAL = new DefaultGadgetsDAL();
                Result = objDefaultGadgetsDAL.SaveDashBoardPersonalization(_userID, dockState);
            }
            loadalldashboard();
        }

        #endregion

        #region Gadget Box Events
        protected void btnload_Click(object sender, EventArgs e)
        {
            int a = _userID;
            int d = 0;
            int f = 0;

            string b = txtfrom.Text.Trim().ToString();
            string c = txtTo.Text.Trim().ToString();
            if (ddlfilter.SelectedValue.ToString() == "-Select Filter-")
                d = 0;
            else
                d = int.Parse(ddlfilter.SelectedValue.ToString());
            if (ddlEmployee.SelectedValue.ToString() == "-Select Employee-")
                f = 0;
            else
                f = int.Parse(ddlEmployee.SelectedValue.ToString());

            objdeshboardentity.insertdatefilterdata(a, b, c, d, f);
            // gadget adding and removing method
            ArrayList ary = (ArrayList)ViewState["registredlist"];
            for (int i = 0; i < dtlsgadget.Items.Count; i++)
            {
                Label controlname = (Label)dtlsgadget.Items[i].FindControl("lblgadgetname");
                Label controlurl = (Label)dtlsgadget.Items[i].FindControl("lblGadgeturl");
                if (((CheckBox)dtlsgadget.Items[i].FindControl("chk1")).Checked == true && (!ary.Contains(controlurl.Text)))
                {

                    RadDock dock = CreateRadDock();
                    //find the target zone and add the new dock there                    
                    //RadDockZone dz = (RadDockZone)RadDockLayout1.FindControl("RadDockZone1");                    
                    RadDockZone3.Controls.Add(dock);
                    CreateSaveStateTrigger(dock);

                    //Load the selected widget in the RadDock control   
                    dock.Tag = controlurl.Text.Trim().ToString();
                    dock.Title = controlname.Text.Trim().ToString();
                    LoadWidget(dock);
                    List<DockState> stateList = RadDockLayout1.GetRegisteredDocksState();
                    StringBuilder serializedList = new StringBuilder();
                    int j = 0;

                    // 
                    ArrayList arrCurrentDockTags = new ArrayList();
                    ArrayList arrClosedDockTags = new ArrayList();
                    //

                    while (j < stateList.Count)
                    {
                        serializedList.Append(stateList[j].ToString());
                        serializedList.Append("|");

                        //add tag (Custom Gadget's Path) to array which are not closed.
                        if (!stateList[j].Closed)
                        {
                            arrCurrentDockTags.Add(stateList[j].Tag);
                        }
                        else
                        {
                            arrClosedDockTags.Add(stateList[j].Tag);
                        }

                        j++;
                    }

                    //code to hide check box and show label 'Added' in Gadget Box

                    string dockState = serializedList.ToString();
                    int Result;
                    if (dockState.Trim() != String.Empty)
                    {
                        objDefaultGadgetsDAL = new DefaultGadgetsDAL();
                        Result = objDefaultGadgetsDAL.SaveDashBoardPersonalization(_userID, dockState);
                    }
                }

            }
            //end gadget add and remove method
            Response.Redirect("~/Views/Workplace/AccountDashBoard.aspx");
        }
        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    DataTable dtfilter = objdeshboardentity.fillfilterdata().Tables[0];
        //    DataTable dtEmployee = objdeshboardentity.fillEmployeedata(_userID).Tables[0];

        //    // fill employee data
        //    ddlEmployee.DataTextField = "EMP_NAME";
        //    ddlEmployee.DataValueField = "EMP_ID";
        //    ddlEmployee.DataSource = dtEmployee;
        //    ddlEmployee.DataBind();
        //    ddlEmployee.Items.Insert(0, "-Select Employee-");
        //    //fill filter data
        //    ddlfilter.DataTextField = "CRITERIA_NAME";
        //    ddlfilter.DataValueField = "CRITERIA_ID";
        //    ddlfilter.DataSource = dtfilter;
        //    ddlfilter.DataBind();
        //    ddlfilter.Items.Insert(0, "-Select Filter-");
        //    txtfrom.Text = "";
        //    txtTo.Text = "";
        //}
        protected void loadalldashboard()
        {
            DataTable dt = new DataTable();

            DataColumn dc1 = new DataColumn("GADGET_NAME");
            DataColumn dc2 = new DataColumn("GADGET_URL");

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            
            DataRow dr1 = dt.NewRow();
            dr1["GADGET_NAME"] = "Income Comparasion";
            dr1["GADGET_URL"] = "~/Views/Charts/Account/IncomeComaparasion.ascx";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["GADGET_NAME"] = "Agent By Sales";
            dr2["GADGET_URL"] = "~/Views/Charts/Account/AgentBySales.ascx";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["GADGET_NAME"] = "Expense Comparasion";
            dr3["GADGET_URL"] = "~/Views/Charts/Account/ExpenceComparasion.ascx";
            dt.Rows.Add(dr3);



            DataRow dr4 = dt.NewRow();
            dr4["GADGET_NAME"] = "Expense Breakdown";
            dr4["GADGET_URL"] = "~/Views/Charts/Account/ExpenseBreakDown.ascx";
            dt.Rows.Add(dr4);


            DataRow dr5 = dt.NewRow();
            dr5["GADGET_NAME"] = "Income Expense";
            dr5["GADGET_URL"] = "~/Views/Charts/Account/IncomeExpense.ascx";
            dt.Rows.Add(dr5);

            dtlsgadget.DataSource = dt;
            dtlsgadget.DataBind();
            ArrayList ary = new ArrayList();
            for (int i = 0; i < CurrentDockStates.Count; i++)
            {
                if (CurrentDockStates[i].Closed == false)
                {
                    for (int j = 0; j < dtlsgadget.Items.Count; j++)
                    {
                        try
                        {
                            Label controlurl = (Label)dtlsgadget.Items[j].FindControl("lblGadgeturl");
                            if (CurrentDockStates[i].Tag.Trim().ToString() == controlurl.Text.Trim().ToString())
                            {
                                ((CheckBox)dtlsgadget.Items[j].FindControl("chk1")).Checked = true;
                                ary.Add(controlurl.Text.Trim().ToString());
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            ViewState["registredlist"] = ary;
        }
        #endregion

        #region Property

        private List<DockState> CurrentDockStates
        {
            get
            {
                //Get saved state string from the database - set it to dockState variable for example 
                string dockStatesFromDB = "";
                objDefaultGadgetsDAL = new DefaultGadgetsDAL();
                DataSet ds = new DataSet();
                ds = objDefaultGadgetsDAL.FetchDashBoardPersonalization(_userID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["PERSONALIZATION_STATE"] != null)
                        dockStatesFromDB = ds.Tables[0].Rows[0]["PERSONALIZATION_STATE"].ToString();
                    else
                        dockStatesFromDB = "";
                }
                List<DockState> _currentDockStates = new List<DockState>();
                string[] stringStates = dockStatesFromDB.Split('|');
                foreach (string stringState in stringStates)
                {
                    if (stringState.Trim() != string.Empty)
                        _currentDockStates.Add(DockState.Deserialize(stringState));
                }
                return _currentDockStates;
            }
        }

        #endregion

        #region Methods

        public ArrayList GetZones()
        {
            ArrayList zones = new ArrayList();
            zones.Add(RadDockZone1);
            zones.Add(RadDockZone2);
            zones.Add(RadDockZone3);

            return zones;
        }

        private RadDock CreateRadDockFromState(DockState state)
        {
            RadDock dock = new RadDock();
            dock.DockMode = DockMode.Docked;
            dock.ID = string.Format("RadDock{0}", state.UniqueName);
            dock.Skin = skin;
            dock.EnableEmbeddedSkins = false;
            dock.CssClass = "dashboard";

            dock.ApplyState(state);
            dock.Commands.Add(new DockCloseCommand());
            //dock.Commands.Add(new DockExpandCollapseCommand());

            DockCommand customcommand = new DockCommand();
            customcommand.OnClientCommand = "MaximizeMinimize";
            customcommand.Name = "MaximizeMinimize";
            customcommand.CssClass = "DockMaximize";
            dock.Commands.Add(customcommand);

            dock.EnableAnimation = true;
            dock.EnableRoundedCorners = true;

            return dock;
        }

        private RadDock CreateRadDock()
        {
            int docksCount = CurrentDockStates.Count;

            RadDock dock = new RadDock();
            dock.DockMode = DockMode.Docked;
            dock.UniqueName = Guid.NewGuid().ToString().Replace('-', 'a');
            dock.ID = string.Format("RadDock{0}", dock.UniqueName);
            dock.Skin = skin;
            dock.EnableEmbeddedSkins = false;
            dock.CssClass = "dashboard";

            dock.Title = "Dock";
            dock.Text = string.Format("Added at {0}", DateTime.Now);
            dock.Width = Unit.Pixel(300);
            dock.EnableAnimation = true;
            dock.EnableRoundedCorners = true;

            dock.Commands.Add(new DockCloseCommand());
            //dock.Commands.Add(new DockExpandCollapseCommand());

            DockCommand customcommand = new DockCommand();
            customcommand.OnClientCommand = "MaximizeMinimize";
            customcommand.Name = "MaximizeMinimize";
            customcommand.CssClass = "DockMaximize";
            dock.Commands.Add(customcommand);

            return dock;
        }

        private void CreateSaveStateTrigger(RadDock dock)
        {
            //Ensure that the RadDock control will initiate postback
            // when its position changes on the client or any of the commands is clicked.
            //Using the trigger we will "ajaxify" that postback.
            dock.AutoPostBack = true;
            dock.CommandsAutoPostBack = true;

            AsyncPostBackTrigger saveStateTrigger = new AsyncPostBackTrigger();
            saveStateTrigger.ControlID = dock.ID;
            saveStateTrigger.EventName = "DockPositionChanged";
            UpdatePanel1.Triggers.Add(saveStateTrigger);

            saveStateTrigger = new AsyncPostBackTrigger();
            saveStateTrigger.ControlID = dock.ID;
            saveStateTrigger.EventName = "Command";
            UpdatePanel1.Triggers.Add(saveStateTrigger);
        }

        private void LoadWidget(RadDock dock)
        {
            if (string.IsNullOrEmpty(dock.Tag))
            {
                return;
            }
            Control widget = LoadControl(dock.Tag);

            dock.ContentContainer.Controls.Add(widget);
        }

        #endregion

    }
}