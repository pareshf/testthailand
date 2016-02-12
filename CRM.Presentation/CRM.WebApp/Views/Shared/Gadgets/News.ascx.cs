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
using System.Xml.XPath;

public partial class Dock_Examples_Default_News : System.Web.UI.UserControl
{
    private XmlDocument News
    {
        get
        {
            XmlDocument news = (XmlDocument)Cache["Dock_Examples_Default_News"];
            if (news == null)
            {
                try
                {
                    news = new XmlDocument();
                    news.Load(new XmlTextReader("http://news.bbc.co.uk/rss/newsonline_world_edition/entertainment/rss.xml"));
                    Cache["Dock_Examples_Default_News"] = news;
                }
                catch (Exception) { }
            }
            return news;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            XmlNodeList events = News.DocumentElement.SelectNodes("/rss/channel/item[position()<7]");
            NewsRepeater.DataSource = events;
            NewsRepeater.DataBind();
            NewsRepeater.Items[0].FindControl("EventPanel").Visible = true;
        }
        catch (Exception) { }
    }

    protected void NewsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HideAllEvents();
        ((Panel)e.Item.FindControl("EventPanel")).Visible = true;
    }

    private void HideAllEvents()
    {
        foreach (RepeaterItem item in NewsRepeater.Items)
        {
            item.FindControl("EventPanel").Visible = false;
        }
    }
}
