<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="CRM.WebApp.Views.Administration.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    
     <div class="pageTitle">
        <asp:Literal ID="lblPageTitle" runat="server" Text="Access Denied"></asp:Literal>
        <%-- Aceess denied--%>
    </div>
    &nbsp;&nbsp;
    <asp:Label ID="lblAccessDenied" runat="server" 
        Text="You don't have permissions to view requested page."></asp:Label>
</asp:Content>
