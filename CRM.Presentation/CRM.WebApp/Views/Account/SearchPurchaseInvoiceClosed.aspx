<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SearchPurchaseInvoiceClosed.aspx.cs" Inherits="CRM.WebApp.Views.Account.SearchPurchaseInvoiceClosed" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
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

    <div>
        <asp:Label runat="server" Text="Search Purchase Invoices" ID="headlbl" Width="400px"
            Font-Bold="true" Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search_onclick" Style="float: right;
                                margin-right: 10px; display: block; color: black;" CssClass="button" />
                            <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                display: none; color: black;" CssClass="button" OnClick="searchnow_onclick" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Invoice No." CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_invoice_no" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Supplier Type" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_supplier_type" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Compnay Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_compnay_name" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="From Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="To Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging"
                                PageSize="10">
                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="PURCHASE_INVOICE_ID" HeaderText="Purchase Invoice ID" />
                                    <asp:BoundField DataField="PURCHASE_INVOICE_NO" HeaderText="Purchase Invoice No" />
                                    <asp:BoundField DataField="SUPPLIER_TYPE_NAME" HeaderText="Supplier Type" />
                                    <asp:BoundField DataField="SUPPLIER_COMPANY_NAME" HeaderText="Company Name" />
                                    <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Invoice Amount" />
                                    <asp:BoundField DataField="DATE" HeaderText="Date" />
                                    <asp:BoundField DataField="SALES_INVOICE_NO" HeaderText="Against Sales Invoice" />
                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
