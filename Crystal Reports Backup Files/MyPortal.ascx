<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPortal.ascx.cs" Inherits="CRM.WebApp.Views.Shared.Gadgets.MyPortal" %>
<link href="../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<table class="" cellspacing="0" cellpadding="4">
    <tr>
        <td>
            <asp:Label ID="lblLoginName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbllastLoginDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLastLoginTime" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLastLoginOn" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
