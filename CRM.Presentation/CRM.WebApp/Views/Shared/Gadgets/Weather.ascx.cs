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

public partial class Dock_Examples_Default_Weather : System.Web.UI.UserControl
{
    private string[] cityCodes = { "INXX0001", "INXX0048", "INXX0026", "INXX0096", "INXX0028", "INXX0075" };
    private XmlNamespaceManager namespaceManager;

    public XmlNamespaceManager NamespaceManager
    {
        get
        {
            return namespaceManager;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            XmlDocument docForecasts = LoadAllForecasts();

            XPathNavigator navigator = docForecasts.CreateNavigator();
            namespaceManager = new XmlNamespaceManager(navigator.NameTable);
            namespaceManager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
            XmlNodeList channels = docForecasts.SelectNodes("/rss/channel");

            ForecastsView.DataSource = channels;
            ForecastsView.DataBind();
        }
        catch (Exception) { }
    }

    private XmlDocument Forecasts
    {
        get
        {
            XmlDocument forecasts = (XmlDocument)Session["Forecasts"];
            if (forecasts == null)
            {
                forecasts = LoadAllForecasts();
                Session["Forecasts"] = forecasts;
            }
            return forecasts;
        }
    }

    private XmlDocument LoadAllForecasts()
    {
        XmlDocument allForecasts = new XmlDocument();
        try
        {
            allForecasts.Load(new XmlTextReader("http://weather.yahooapis.com/forecastrss?p=" + cityCodes[0] + "&u=c"));
            XmlDocument temp = new XmlDocument();

            for (int i = 1; i < cityCodes.Length; i++)
            {
                temp.Load(new XmlTextReader("http://weather.yahooapis.com/forecastrss?p=" + cityCodes[i] + "&u=c"));
                XmlNode newChannel = allForecasts.ImportNode(temp.DocumentElement.FirstChild, true);
                allForecasts.DocumentElement.AppendChild(newChannel);
            }
        }
        catch (Exception) { }

        return allForecasts;
    }

    protected void ForecastsView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int currCityNumber = e.Row.RowIndex + 1;
        if (e.Row.DataItem != null)
        {
            string content = Forecasts.DocumentElement.SelectSingleNode("/rss/channel[" + currCityNumber + "]/item/description").InnerText;
            try
            {
                int urlStartIndex = content.IndexOf("http");
                int urlEndIndex = content.IndexOf("gif") + 3;
                ((Image)e.Row.FindControl("Image")).ImageUrl = content.Substring(urlStartIndex, urlEndIndex - urlStartIndex);
            }
            catch (Exception)
            {
                ((Image)e.Row.FindControl("Image")).ImageUrl = "Img/noimage.gif";
            }

        }
    }
}
