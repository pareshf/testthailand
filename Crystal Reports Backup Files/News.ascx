<%@ Control Language="C#" AutoEventWireup="true" Inherits="Dock_Examples_Default_News" Codebehind="News.ascx.cs" %>
<asp:updatepanel runat="server" id="UpdatePanelNews">
	<contenttemplate>
        <asp:repeater id="NewsRepeater" runat="server" onitemcommand="NewsRepeater_ItemCommand" >
            <headertemplate><ul></headertemplate>
            <itemtemplate>
                <li>
                    <asp:linkbutton runat="server" text='<%#XPath(@"title")%>' id="TitleButton"  />
                    <br />
                    <asp:panel id="EventPanel" runat="server" visible="false">
                        <asp:literal runat="server" id="Event" text='<%#XPath(@"description")%>' />
                        <asp:hyperlink runat="server" id="DetailsLink" text="More info..." navigateurl='<%#XPath(@"link")%>' target="_blank" />
                    </asp:panel>
                    <br />
                </li>
            </itemtemplate>
            <footertemplate></ul></footertemplate>
        </asp:repeater>
    </contenttemplate>
</asp:updatepanel>

