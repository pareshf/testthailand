<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ManualItinerySearch.aspx.cs" Inherits="CRM.WebApp.Views.FIT.ManualItinerySearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        <asp:Label runat="server" Text="Search ManualItinery" ID="headlbl" Width="400px" Font-Bold="true"
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
                                <asp:Label ID="lblcompanyname" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                            <asp:DropDownList ID="drpAgent" runat="server" Width="255px" AutoPostBack="true" OnSelectedIndexChanged="drpAgent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Invoice No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               <asp:DropDownList ID="drpInvoice" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="Date Of Arrival" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtarrival" runat="server" Width="250px"></asp:TextBox>
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtarrival"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="Date Of Departure" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDeparture" runat="server" Width="250px"></asp:TextBox>
                                 <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDeparture"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
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
                                    <asp:BoundField DataField="MANUAL_ITINERY_ID" HeaderText="MANUAL_ITINERY_ID"/>
                                    <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Agent Name" />
                                    <asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No" />
                                    <asp:BoundField DataField="PAX_NAME" HeaderText="Pax Name" />
                                    <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="Date Of Arrival" />
                                    <asp:BoundField DataField="DEPARTURE_DATE" HeaderText="Date Of Departure" />
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