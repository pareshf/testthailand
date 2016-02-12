<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AllInvoice.aspx.cs" Inherits="CRM.WebApp.Views.Account.AllInvoice" %>

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
        <asp:Label runat="server" Text="Search Invoice" ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <contenttemplate>
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
                                <asp:Label ID="lblcompanyname" runat="server" Text="Company Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               <telerik:RadComboBox ID="drpAgent" runat="server" Width="250px"  ShowDropDownOnTextboxClick="true" AllowCustomText="false" AutoPostBack="true" OnSelectedIndexChanged="drpAgent_SelectedIndexChanged" >
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Quotation Refrence No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrefrence" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Invoice No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="DropDownList2_new" runat="server" Width="250px"  ShowDropDownOnTextboxClick="true" AllowCustomText="false" >
                                </telerik:RadComboBox>
                              <%--  <asp:DropDownList ID="DropDownList2" runat="server" Width="255px">
                                </asp:DropDownList>--%>
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
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" Width="938px" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="QUOTE_ID" HeaderText="Quotation Refrence No" />
                                    <asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No" />
                                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="Invoice Date" />
                                    <asp:BoundField DataField="PERIOD_STAY_FROM" HeaderText="Period Stay From" />
                                    <asp:BoundField DataField="PERIOD_STAY_TO" HeaderText="Period Stay To" />
                                    <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Amount" />
                                    <asp:BoundField DataField="SALES_INVOICE_ID" HeaderText="Invoice Id" />
                                    <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" />

                                 </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
