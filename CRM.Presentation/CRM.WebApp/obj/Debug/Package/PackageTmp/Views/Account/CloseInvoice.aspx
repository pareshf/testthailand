<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CloseInvoice.aspx.cs" Inherits="CRM.WebApp.Views.Account.CloseInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
    <asp:Label runat="server" Text="Invoice Close" ID="headlbl" Width="400px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <br />
    <div>
        <asp:UpdatePanel ID="upCloseInvoice" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false"  >
            
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
                                <asp:Label ID="Label6" runat="server" Text="Invoice No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Invoice Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="Period Stay From" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="Period Stay To" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </asp:Panel>
                <br />

                <table width="100%">
                    <tr>
                        <td>

                         <asp:GridView ID="gridCloseinvoice" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" 
                         OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10">
                         <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>
                        <Columns>
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceHeader" runat="server" Text="Invoice No" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Bind("INVOICE_NO") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceDateHeader" runat="server" Text="Invoice Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Bind("INVOICE_DATE") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceStayfromHeader" runat="server" Text="From Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoicestayFrom" runat="server" Text='<%# Bind("PERIOD_STAY_FROM") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceStaytoHeader" runat="server" Text="To Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoicestayto" runat="server" Text='<%# Bind("PERIOD_STAY_TO") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceTotalAmountHeader" runat="server" Text="Invoice Amount" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceTotalAmount" runat="server" Text='<%# Bind("TOTAL_AMOUNT") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblInvoiceCompanyNameHeader" runat="server" Text="Company Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceCompanyName" runat="server" Text='<%# Bind("CUST_COMPANY_NAME") %>' CssClass="lblstyleGIT"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblCloseInvoiceHeader" runat="server" Text="Close Invoice" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnClose" Text = "Close" OnClick="btnClose_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
