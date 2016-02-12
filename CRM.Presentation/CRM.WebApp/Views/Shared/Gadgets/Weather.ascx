<%@ Control Language="C#" AutoEventWireup="true" Inherits="Dock_Examples_Default_Weather" Codebehind="Weather.ascx.cs" %>
<asp:gridview runat="server" id="ForecastsView" autogeneratecolumns="false" onrowdatabound="ForecastsView_RowDataBound" gridlines="None">
    <columns>
        <asp:templatefield itemstyle-width="150px">
            <itemtemplate><%#XPath(@"yweather:location/@city", NamespaceManager)%></itemtemplate>
        </asp:templatefield>
        <asp:templatefield itemstyle-width="70px">
            <itemtemplate><%#XPath(@"item/yweather:condition/@temp", NamespaceManager)%>F</itemtemplate>
        </asp:templatefield>
        <asp:templatefield>
            <itemtemplate><asp:image runat="server" id="Image" /></itemtemplate>
        </asp:templatefield>
    </columns>
</asp:gridview>