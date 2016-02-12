<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="EmployeeInquiryReports.aspx.cs" Inherits="CRM.WebApp.Views.Reports.EmployeeInquiryReports" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

<div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal6" runat="server" Text="Customer Inquiry Report"></asp:Literal>
            <br />
         </div>
         <div>
            <table>
                <tr>
                    <td>
                        


                    </td>
                </tr> 
            </table>
         </div>

</div>
</asp:Content>
