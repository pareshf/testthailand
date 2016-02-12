//Developer:      Nitesh Parmar 
//Created Date:   9/6/2009

using System;
using System.Web.UI;

namespace CRM.WebApp.Views.Shared.Controls.Message
{
    public partial class FlashMessage : System.Web.UI.UserControl
    {
        #region Variables

        string _Interval = "3000";
        int _FadeInDuration = 500;
        int _FadeInSteps = 20;
        int _FadeOutDuration = 500;
        int _FadeOutSteps = 20;
        bool _InsertJavascript = true;

        #endregion

        #region Properties

        public string CssClass
        {
            get
            { return lblMessage.CssClass; }
            set
            { lblMessage.CssClass = value; }
        }

        public string MessageText
        {
            get
            { return lblMessage.Text; }
            set
            { lblMessage.Text = value.ToString(); }
        }

        public int Interval
        {
            get
            { return int.Parse(_Interval); }
            set
            { _Interval = value.ToString(); }
        }

        public int FadeInDuration
        {
            get
            { return _FadeInDuration; }
            set
            { _FadeInDuration = value; }
        }

        public int FadeInSteps
        {
            get
            { return _FadeInSteps; }
            set
            { _FadeInSteps = value; }
        }

        public int FadeOutDuration
        {
            get
            { return _FadeOutDuration; }
            set
            { _FadeOutDuration = value; }
        }

        public int FadeOutSetps
        {
            get
            { return _FadeOutSteps; }
            set
            { _FadeOutSteps = value; }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        public void Display()
        {
            string js = "fadeIn('" + lblMessage.ClientID + "', " + FadeInSteps + ", " + FadeInDuration + ", " + Interval + ", " + FadeOutSetps + ", " + FadeOutDuration + ");";

            ScriptManager sm = ScriptManager.GetCurrent(Page);
            UpdatePanel up = (UpdatePanel)GetParentOfType(lblMessage, typeof(UpdatePanel));
            if ((sm != null) & (up != null))
            {
                //The user control is nested in an update panel, register the javascript with the script manager and 
                //attach it to the update panel in order to fire it after the async postback 
                if (sm.IsInAsyncPostBack == true)
                {
                    ScriptManager.RegisterClientScriptBlock(up, typeof(UpdatePanel), "jscript1", js, true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "jscript1", js, true);
                }
            }
            else
            {
                //The user control is not in an update panel (or there is no script manager on the page), 
                //so register the javascript for a normal postback             
               Page.ClientScript.RegisterStartupScript(this.GetType(), "jscript1", js, true);
             
            }
        }

        private Control GetParentOfType(Control root, System.Type Type)
        {
            Control Parent = root.Parent;
            if ((Parent == null))
            {
                return null;
            }
            if (Parent.GetType().ToString() == Type.ToString())
            {
                return root.Parent;
            }
            Control p = GetParentOfType(Parent, Type);
            if ((p != null))
            {
                return p;
            }
            return null;
        }

        #endregion
    }
}