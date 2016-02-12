using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text.RegularExpressions;

public partial class Dock_Examples_Default_Horoscopes : System.Web.UI.UserControl
{
    private XmlDocument Horoscopes
    {
        get
        {
            XmlDocument horoscopes = (XmlDocument)Cache["Dock_Examples_Default_Horoscopes"];
            if (horoscopes == null)
            {
                try
                {
                    horoscopes = new XmlDocument();
                    horoscopes.Load(new XmlTextReader("http://feeds.astrology.com/dailyoverview"));
                    Cache["Dock_Examples_Default_Horoscopes"] = horoscopes;
                }
                catch (Exception) { }
            }
            return horoscopes;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //You need to databind the dropdown before Page_Load 
        // to properly raise its SelectedIndexChanged event.
        try
        {
            XmlNodeList titles = Horoscopes.DocumentElement.SelectNodes("/rss/channel/item/title");
            DropDownSigns.DataSource = titles;
            DropDownSigns.DataValueField = "InnerText";
            DropDownSigns.DataBind();
        }
        catch (Exception) { }
    }

    private void DisplayHoroscope()
    {
        try
        {
            string horoscopeText = Horoscopes.DocumentElement.SelectSingleNode(
                string.Format("/rss/channel/item[{0}]/description", DropDownSigns.SelectedIndex + 1)).FirstChild.Value;
            Horoscope.Text = Regex.Match(horoscopeText, "<p>([^\r\n]*)").Value;
        }
        catch { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownSigns.SelectedIndex = 0;
        }
        DisplayHoroscope();
    }

    protected void DropDownSigns_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayHoroscope();
    }
}
