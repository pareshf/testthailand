<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductHeader.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Templates.ProductHeader" %>
<table border="0" style="height: 55px" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="15%" align="left">
            <asp:Image ID="imgLogo" runat="server" Height="50px" Width="148px" ImageAlign="Top"
                ImageUrl="~/Views/Shared/Images/flamingo.jpg" />
        </td>
        <td width="55%" align="center">
        
            <asp:Label ID="lblPreferedCompanyName" runat="server" SkinID="SknPreferedCompanyName"></asp:Label>
        
        </td>
        <td width="30%" style="padding-right: 10px; padding-top: 5px;" align="right" valign="middle">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblWelcome" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;|&nbsp;
                    </td>                   
                    
                    <td>
                        <asp:LinkButton ID="lnkBtnMyProfile" runat="server">My Profile</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;|&nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkBtnSignOut" runat="server" OnClick="lnkBtnSignOut_Click">Sign Out</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
