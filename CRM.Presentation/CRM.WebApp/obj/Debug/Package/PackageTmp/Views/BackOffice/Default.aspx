<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<script language="javascript" type="text/javascript">

    var sessionTimeout = "<%= Session.Timeout %>";

    var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
    setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>
</asp:Content>
