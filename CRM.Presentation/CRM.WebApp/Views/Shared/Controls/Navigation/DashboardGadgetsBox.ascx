<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardGadgetsBox.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.DashboardGadgetsBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
           <asp:DataList ID="dtlsgadget" runat="server" SelectedItemStyle-BackColor="#c2c2c2" RepeatDirection="Horizontal" RepeatColumns="4" CellPadding="5">
           <ItemTemplate><div align="left">
           <asp:CheckBox ID="chk1" runat="server" />&nbsp;&nbsp;
           <asp:Label ID="lblGadgeturl" runat="server" Text='<%#Eval("GADGET_URL") %>' Visible="false"></asp:Label>
           <asp:Label ID="lblgadgetname" runat="server" Text='<%#Eval("GADGET_NAME") %>'></asp:Label>
           </div>
           </ItemTemplate>
           </asp:DataList>
        </td>
    </tr>
   
</table>
