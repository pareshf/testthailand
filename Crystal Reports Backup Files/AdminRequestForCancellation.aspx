<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AdminRequestForCancellation.aspx.cs" Inherits="CRM.WebApp.Views.FIT.AdminRequestForCancellation" %>
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
        <asp:Label runat="server" Text="FIT - Cancellation Bookings" ID="headlbl" Width="600px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server" Text="Quotation Refrence No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrefrence" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Tour Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
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
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table Width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false" >
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="QUOTE_ID" HeaderText="Quotation Refrence No" />
                                    <asp:BoundField DataField="TOUR_ID" HeaderText="Tour id" />
                                    <asp:BoundField DataField="TOUR_NAME" HeaderText="Tour Name" />
                                    <asp:BoundField DataField="FROM_DATE" HeaderText="From Date" />
                                    <asp:BoundField DataField="TO_DATE" HeaderText="To Date" />
                                    <asp:BoundField DataField="ORDER_STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="ADULT" HeaderText="No Adults" />
                                    <asp:BoundField DataField="CWB" HeaderText="No CWB" />
                                    <asp:BoundField DataField="CNB" HeaderText="No CNB" />
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
