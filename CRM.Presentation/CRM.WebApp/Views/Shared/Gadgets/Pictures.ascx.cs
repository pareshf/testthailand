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
using Telerik.Web.UI;
using System.Xml;

public partial class Dock_Examples_Default_Pictures : System.Web.UI.UserControl
{
    private XmlDocument Images
    {
        get
        {
            XmlDocument images = (XmlDocument)Cache["Dock_Examples_Default_Pictures"];
            if (images == null)
            {
                images = new XmlDocument();
                try
                {
                    images.Load(new XmlTextReader("http://community.webshots.com/scripts/openSearch.cgi?searchTerms=bulgaria+sofia"));
                }
                catch (Exception) { }
                Cache["Dock_Examples_Default_Pictures"] = images;
            }
            return images;
        }
    }

    private DataTable ImagesUrl
    {
        get
        {
            DataTable url = (DataTable)Session["ImagesUrl"];
            if (url == null)
            {
                url = new DataTable();
                url.Columns.Add("ThumbURL");
                url.Columns.Add("ImageURL");
                if (!Object.Equals(Images.DocumentElement, null))
                {
                    XmlNodeList pictures = Images.DocumentElement.SelectNodes("/rss/channel/item/description");
                    int i = 0;
                    if (!Object.Equals(pictures, null))
                    {
                        foreach (XmlNode node in pictures)
                        {
                            string content = node.InnerText;
                            int thumbUrlStartIndex = content.IndexOf("thumb") - 7;
                            int thumbUrlEndIndex = content.IndexOf("jpg") + 3;
                            DataRow row = url.NewRow();
                            row["ThumbURL"] = content.Substring(thumbUrlStartIndex, thumbUrlEndIndex - thumbUrlStartIndex);
                            int imgUrlStartIndex = content.IndexOf("http");
                            int imgUrlEndIndex = content.IndexOf(">") - 1;
                            row["ImageURL"] = content.Substring(imgUrlStartIndex, imgUrlEndIndex - imgUrlStartIndex);
                            url.Rows.Add(row);
                            i++;
                        }
                    }
                }
                Session["ImagesUrl"] = url;
            }
            return url;
        }
        set
        {
            Session["ImagesUrl"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImagesView.DataSource = ImagesUrl;
        ImagesView.DataBind();
    }

    protected void ImagesView_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {
        ImagesView.PageIndex = e.NewPageIndex;
        ImagesView.DataSource = ImagesUrl;
        ImagesView.DataBind();
    }
}
