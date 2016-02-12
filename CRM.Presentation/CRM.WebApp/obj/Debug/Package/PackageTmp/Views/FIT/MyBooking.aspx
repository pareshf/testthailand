<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="MyBooking.aspx.cs" Inherits="CRM.WebApp.Views.FIT.MyBooking" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
   
    <div>
    
        <asp:Label runat="server" Text="My Bookings" ID="headlbl" Width="200px" Font-Bold="true"
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
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false">
                                <Columns>
                                    <asp:BoundField DataField="QUOTE_ID" HeaderText="Quote id" />
                                    <asp:BoundField DataField="TOUR_ID" HeaderText="Tour id" />
                                    <asp:BoundField DataField="TOUR_NAME" HeaderText="Tour Name" />
                                    <asp:BoundField DataField="FROM_DATE" HeaderText="From Date" />
                                    <asp:BoundField DataField="TO_DATE" HeaderText="To Date" />
                                    <asp:BoundField DataField="ORDER_STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="ADULT" HeaderText="No Adults" />
                                    <asp:BoundField DataField="CWB" HeaderText="No CWB" />
                                    <asp:BoundField DataField="CNB" HeaderText="No CNB" />
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:ButtonField ButtonType="Button" Text="View" />
                                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnconfirm" runat="server" text="Confirm" OnClick="btnconfirm_onclick"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--
                            <asp:ButtonField ButtonType="Button" Text="Confirm" CommandName="confirm" />
                         <asp:ButtonField ButtonType="Button" Text="Reconfirm" />
                            <asp:ButtonField ButtonType="Button" Text="Wait List" />
                            <asp:ButtonField ButtonType="Button" Text="Change of Hotel" />
                        
                        <HeaderStyle CssClass="rgHeader" />
                        BackColor="Yellow" Font-Bold="False" Font-Names="Verdana" 
                Font-Size="11pt"--%>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- <ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
            PopupControlID="pnlCompanyRoleSelection" TargetControlID="Button4" Drag="true"
            PopupDragHandleControlID="pnlCompanyRoleSelectionHeader" CancelControlID="ImageButton1">
        </ajax:ModalPopupExtender>
        <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px">
            <asp:Panel ID="Panel1" runat="server" Width="350px">
                <fieldset style="background-color: White">
                    <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitleAlert" runat="server" Text="Change Preference" ForeColor="#FEFEFE"
                                        Font-Size="15px"></asp:Label>
                                </td>
                                <td style="width: 17px;" align="center" valign="middle">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                        Style="cursor: pointer;" ToolTip="Close" />
                                    <asp:Button ID="Button4" runat="server" Text="Button" style="display:none" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 45%">
                                <asp:Label ID="Label1" runat="server" Text="Reconfirmation Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                    class="error">*</span>
                            </td>
                            <td style="width: 55%">
                                <asp:TextBox ID="TextBox1" runat="server"  Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 45%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reconfirmation date is required"
                                    ControlToValidate="TextBox1" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Submit" ValidationGroup="popup" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </asp:Panel>--%>
    </div>
</asp:Content>
