<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SalaryVouchers.aspx.cs" Inherits="CRM.WebApp.Views.Account.SalaryVouchers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

 <script language="javascript" type="text/javascript">

     var sessionTimeout = "<%= Session.Timeout %>";

     var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
     setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


    <asp:Label runat="server" Text="Salary to be Paid" ID="headlbl" Width="400px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <br />
    <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        class="pageTitle">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                            AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Pay Salary" />
                                <asp:BoundField DataField="NAME" HeaderText="Employee Name" />
                                <asp:BoundField DataField="EMPLOYEE_SALARY" HeaderText="Salary" />
                            </Columns>
                            <HeaderStyle CssClass="rgHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
