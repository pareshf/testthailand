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

public partial class Dock_Examples_Default_ExchangeRates : System.Web.UI.UserControl
{
    private XmlDocument Rates
    {
        get
        {
            XmlDocument rates = (XmlDocument)Cache["Dock_Examples_Default_ExchangeRates"];
            if (rates == null)
            {
                try
                {
                    rates = new XmlDocument();
                    rates.Load(new XmlTextReader("http://www.czechinfocenter.com/currency.rss"));
                    Cache["Dock_Examples_Default_ExchangeRates"] = rates;
                }
                catch (Exception) { }
            }
            return rates;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        BindLists();
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string baseCurrency = (BaseCurrencyList.SelectedValue.Split('-'))[1];
        string foreignCurrency = (ForeignCurrencyList.SelectedValue.Split('-'))[1];

        XmlNodeList rates = Rates.DocumentElement.SelectNodes("/rss/channel/item/description");
        double baseRate = Double.Parse((rates[BaseCurrencyList.SelectedIndex].InnerText.Split(' '))[3]);
        double foreignNode = Double.Parse((rates[ForeignCurrencyList.SelectedIndex].InnerText.Split(' '))[3]);
        double result = baseRate / foreignNode;
        Result.Text = baseCurrency + " /" + foreignCurrency + " = " + Math.Round(result, 4);
    }

    private void BindLists()
    {
        try
        {
            XmlNodeList rates = Rates.DocumentElement.SelectNodes("/rss/channel/item");
            ArrayList currencies = new ArrayList();

            foreach (XmlNode rate in rates)
            {
                currencies.Add(rate.SelectSingleNode("title").InnerText + " - " + rate.SelectSingleNode("link").InnerText.Split('=')[1]);
            }
            BaseCurrencyList.DataSource = currencies;
            BaseCurrencyList.DataBind();
            ForeignCurrencyList.DataSource = currencies;
            ForeignCurrencyList.DataBind();
        }
        catch (Exception) { }
    }
}
