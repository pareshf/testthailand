<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PanelBarModule.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.PanelBarModule" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link rel="stylesheet" type="text/css" href="Views/Shared/StyleSheet/CommonStyle.css" />

<table id="tblSideMenuContainer" border="0" cellspacing="0" cellpadding="0" width="100%"
    style="height: 31px;">
    <tr>
        <td>
            <asp:Menu ID="menuMainModule" runat="server" Orientation="Horizontal" Height="31px" OnClientClick="change_width();"
                MaximumDynamicDisplayLevels="0">
                <StaticMenuItemStyle CssClass="topmenulink" />
            </asp:Menu>
        </td>
    </tr>
</table>
