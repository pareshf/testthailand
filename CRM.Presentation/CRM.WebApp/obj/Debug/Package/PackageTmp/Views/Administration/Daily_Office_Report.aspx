<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Daily_Office_Report.aspx.cs" Inherits="CRM.WebApp.Views.Administration.Daily_Office_Report" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Daily Office Reports"></asp:Literal>
    </div>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblfromdate" runat="server" Text="Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" Width="200px"></asp:TextBox>
                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
            </td>
            <td>
                <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
