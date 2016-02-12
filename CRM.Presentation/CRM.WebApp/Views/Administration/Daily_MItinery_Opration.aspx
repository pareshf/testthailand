<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Daily_MItinery_Opration.aspx.cs" Inherits="CRM.WebApp.Views.Administration.Daily_MItinery_Opration" %>
    <%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Opration Sheet Manual"></asp:Literal>
    </div>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblfromdate" runat="server" Text="Date"></asp:Label>
            </td>
            <td>
                 <asp:TextBox ID="txtfromdate" runat="server" Width="200px"></asp:TextBox>
                <ajax:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtfromdate"
                    format="dd/MM/yyyy" popupbuttonid="Image1" />
            </td>
            <td>
                <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
