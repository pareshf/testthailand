<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRM.WebApp.Views.Administration.Default" %>

<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <table>
        <tr>
            <td>
                <div class="pageTitle">
                    <asp:Literal ID="lblPageAdministration" runat="server" Text="Administration"></asp:Literal>
                    
                </div>
            </td>
        </tr>
       
        <%--<tr>
            <td>
               <asp:Image ID="Image1" runat="server" ImageUrl="~/Views/Shared/Images/dashboard_01.jpg" />
            </td>
        </tr>--%>
    </table>
</asp:Content>
