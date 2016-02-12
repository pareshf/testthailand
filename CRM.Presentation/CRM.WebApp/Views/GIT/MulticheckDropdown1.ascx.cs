using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;

namespace CRM.WebApp.Views.FIT
{
    public partial class MulticheckDropdown : System.Web.UI.UserControl
    {
        private string delimiter = ",";

        public string Delimiter
        {
            get { return delimiter; }
            set { delimiter = value; }
        }

        public RadComboBox DDList
        {
            get { return this.CB1; }
        }

        protected override void OnInit(EventArgs e)
        {
            // Create the javascript functions with the client id appended to them.
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append(@"var cancelDropDownClosing");
            sb.Append(CB1.ClientID);
            sb.Append(@"=false; function onDropDownClosing");
            sb.Append(CB1.ClientID);
            sb.Append(@"(){cancelDropDownClosing");
            sb.Append(CB1.ClientID);
            sb.Append(@"= false;}");
            sb.Append(@"function onCheckBoxClick");
            sb.Append(CB1.ClientID);
            sb.Append(@"(){var combo=$find('");
            sb.Append(CB1.ClientID);
            sb.Append(@"');var chkall=$get(combo.get_id() + '_Header_SelectAll');if(AllSelected");
            sb.Append(CB1.ClientID);
            sb.Append(@"()==true){chkall.checked=true;}else{chkall.checked=false;}var text='';var values='';var items=combo.get_items();");
            sb.Append(@"for(var i=0;i<items.get_count();i++){var item=items.getItem(i);var chk1=$get(combo.get_id()+'_i'+i+'_chk1');");
            sb.Append(@"if(chk1.checked){text+=item.get_text()+'");
            sb.Append(Delimiter);
            sb.Append("';values+=item.get_value()+'");
            sb.Append(Delimiter);
            sb.Append("';}}text=removeLastDelimiter(text);values=removeLastDelimiter(values);if(text.length>0){combo.set_text(text);}else{combo.set_text('');}}");
            sb.Append(@"function AnyOneSelected");
            sb.Append(CB1.ClientID);
            sb.Append(@"(){var combo=$find('");
            sb.Append(CB1.ClientID);
            sb.Append(@"');var items=combo.get_items();for(var i=0;i<items.get_count();i++){var item=items.getItem(i);");
            sb.Append(@"var chk1=$get(combo.get_id()+'_i'+i+'_chk1');if(chk1.checked){return true;}}return false;}");
            sb.Append(@"function AllSelected");
            sb.Append(CB1.ClientID);
            sb.Append(@"(){var combo=$find('");
            sb.Append(CB1.ClientID);
            sb.Append(@"');var items=combo.get_items();for(var i=0;i<items.get_count();i++){var item=items.getItem(i);");
            sb.Append(@"var chk1=$get(combo.get_id()+'_i'+i+'_chk1');if(chk1.checked==false){return false;}}return true;}");
            sb.Append(@"function SelectAllClick");
            sb.Append(CB1.ClientID);
            sb.Append(@"(chk){var selectAll=true; if(AnyOneSelected");
            sb.Append(CB1.ClientID);
            sb.Append(@"()==true)selectAll=true;if(AllSelected");
            sb.Append(CB1.ClientID);
            sb.Append(@"()==true)selectAll=false;var text='';var values='';var combo = $find('");
            sb.Append(CB1.ClientID);
            sb.Append(@"');var items=combo.get_items();for(var i=0;i<items.get_count();i++){var item=items.getItem(i);");
            sb.Append(@"var chk1=$get(combo.get_id()+'_i'+i+'_chk1');if(selectAll)chk1.checked=true;else chk1.checked=false;");
            sb.Append(@"if(chk1.checked){text += item.get_text()+'");
            sb.Append(Delimiter);
            sb.Append("';values+=item.get_value()+'");
            sb.Append(Delimiter);
            sb.Append("';}}text=removeLastDelimiter(text);values=removeLastDelimiter(values);if(text.length>0)combo.set_text(text);else ");
            sb.Append(@"combo.set_text('');}</script>");
            string js = sb.ToString();

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Register the javascript on the page with the client id appended.
            page.ClientScript.RegisterClientScriptBlock(typeof(MulticheckDropdown), "DDC_" + CB1.ClientID, js);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Create the javascript functions.
            if (!this.IsPostBack)
            {
                // Add the events
                this.CB1.OnClientDropDownClosed = "onDropDownClosing" + CB1.ClientID;
                (CB1.Header.FindControl("SelectAll") as CheckBox).Attributes["onclick"] = "SelectAllClick" + CB1.ClientID + "(this)";
            }
        }

        protected void CB1_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            (e.Item.FindControl("chk1") as CheckBox).Attributes["onclick"] = "onCheckBoxClick" + CB1.ClientID + "(this)";
        }

        public string GetCheckBoxValues()
        {
            StringBuilder sbValues = new StringBuilder();
            foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in DDList.Items)
            {
                //If the box is checked return a value
                CheckBox cb = (CheckBox)rcbItem.FindControl("chk1");
                if (null != cb && cb.Checked)
                {
                    sbValues.Append(rcbItem.Text);
                    sbValues.Append(Delimiter);
                }
            }
            //Remove Trailing comma
            string str = sbValues.ToString();
            if (str.EndsWith(Delimiter))
                return str.Remove(sbValues.Length - 1, 1);
            return str;
        }

        public void SetCheckBoxValues(string csv)
        {
            // First clear all checks.
            foreach (RadComboBoxItem item in DDList.Items)
            {
                CheckBox chkbox = (CheckBox)item.FindControl("chk1");
                if (null != chkbox)
                    chkbox.Checked = false;
            }

            // Find each item in the list and set the check and combo text value.
            string[] values = csv.Split(',');
            string text = string.Empty;
            for (int i = 0; i <= values.Length - 1; i++)
            {
                RadComboBoxItem item = DDList.FindItemByValue(values[i]);
                CheckBox chkbox = (CheckBox)item.FindControl("chk1");
                if (text.Equals(string.Empty))
                    text = string.Format("{0}", item.Text);
                else
                    text = string.Format("{0}{1} {2}", text, Delimiter, item.Text);
                chkbox.Checked = true;
            }
            DDList.Text = text;     // This doesn't do anything!
        }

        public void clearcoreskill()
        {
            CB1.Text = "";
        }
    }
}