<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="PartsPayments.aspx.cs" Inherits="CRM.WebApp.Views.Account.PartsPayments" %>

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

    <asp:Label runat="server" Text="Payments to be Made" ID="headlbl" Width="400px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <br />
    <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        class="pageTitle">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Due Date" Width="400px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"
                            Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="Tr1" runat="server" visible="false">
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Select Payment method" Width="140px"></asp:Label>
                        &nbsp;<span class="error">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>B2B</asp:ListItem>
                            <asp:ListItem>B2C2B</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="Tr2" runat="server" visible="false">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Select bank from payment transfer" Width="200px"></asp:Label>&nbsp;<span
                            class="error">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                            AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false">
                            <%--OnRowDataBound = "GV_Result_RowDataBound" --%>
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Pay Now" />
                                <asp:BoundField DataField="CUST_REL_NAME" HeaderText="Supplier Company Name" />
                                <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Total Amount" />
                                <asp:BoundField DataField="AMOUNT_PAID" HeaderText="Amount Paid" />
                                <asp:BoundField DataField="LEFT_AMOUNT" HeaderText="Amount to be Paid" />
                                <asp:BoundField DataField="SUPPLIER_TYPE_NAME" HeaderText="Supplier Type Name" />
                                <asp:BoundField DataField="SALES_INVOICE_NO" HeaderText="Against Sales Invoice No." />
                                <asp:BoundField DataField="PURCHASE_INVOICE_NO" HeaderText="Purchase Invoice No." />
                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" />
                                <asp:BoundField DataField="AGENT_NAME" HeaderText="Agent Name" />
                                <asp:BoundField DataField="DUE_DATE" HeaderText="Due Date" />
                            </Columns>
                            <HeaderStyle CssClass="rgHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
