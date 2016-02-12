<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductFooter.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Templates.ProductFooter" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center" style="border-top:1px solid #D0D0D0; background-color:#ffffff; padding-top:5px; padding-bottom:3px; ">
          <table id="tblFooterCenter" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <%--<td valign="middle">
                        <asp:Image ID="imbFooterLogo" ImageAlign="Middle" runat="server" ImageUrl="~/Views/Shared/Images/Jaivver.jpg" />
                    </td>
                    <td class="clsQuickLinkSeperator">
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                    </td>
                    <td>
                        <span style="padding-top: 10px">
                            <asp:Label ID="lblCopyRights" class="clsrLblFooterInfo" runat="server" Text="Jaiveer Group Co Ltd -  All Rights Reserved"></asp:Label></span>
                    </td>--%>
                </tr>

                <tr></tr>
                <br />
                <br />
                <tr>
                    <%--<td valign="middle">
                        <asp:Image ID="Image1" ImageAlign="Middle" runat="server" ImageUrl="~/Views/Shared/Images/amba.jpg" />
                    </td>
                    <td class="clsQuickLinkSeperator">
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                    </td>--%>
                    <td valign="middle">
                        <span style="padding-top: 10px">
                          <asp:Label ID="Label1" class="clsrLblFooterInfo" runat="server" Text="Developed By: "></asp:Label>  <asp:HyperLink id="hyperlink1" Text="Amba Infotech"
                  Target="_blank"
                  NavigateUrl="http://www.ambait.com"
                  runat="server"
                 ForeColor="Black"
                 Font-Size="12px"
                 Font-Names="Arial"
                 
                  Font-Underline="False"
                 />       </span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
