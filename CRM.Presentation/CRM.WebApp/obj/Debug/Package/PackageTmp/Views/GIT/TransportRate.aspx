<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="TransportRate.aspx.cs" Inherits="CRM.WebApp.Views.GIT.TransportRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div>
        <asp:Label ID="Label55" runat="server" Text="Transport Packge Rates" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:Label runat="server" ID="lblPriceHeader" Text="Price of Trasport Package" CssClass="pageTitle"></asp:Label>
        <br />
        <br />
        <div id="divCoachRate" class="pageTitle">
            <asp:Label ID="lblhotel" runat="server" Text="Coach Rate" class="pageTitle" Width="100px"
                Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
            <asp:UpdatePanel ID="upCoach" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gridCoachRate" AutoGenerateColumns="false" AllowPaging="false"
                        SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="250px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader1" runat="server" Text="Package Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPackName" runat="server" Text='<%# Bind("GIT_TRANSFER_PACKGE_NAME") %>'
                                        CssClass="lblstyle"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="250px" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackid" runat="server" CssClass="lblstyle" Text="0"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="80px" Visible="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader2" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader3" runat="server" Text="Select Supplier" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="drpSupplier">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadTimePicker ID="rdtpTime" runat="server" Width="90px">
                                        <TimeView TimeFormat="h:mm tt">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="hh:mm tt">
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnRemove" Text = "Remove" OnClick="btnCoachRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnCoach" Text="Rate" OnClick="btnCoach_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel1" runat="server" Text="ADD" OnClick="btnAddCoach_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <div id="divGuideRate" class="pageTitle">
            <asp:Label ID="Label1" runat="server" Text="Guide Rate" class="pageTitle" Width="100px"
                Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
            <asp:UpdatePanel ID="Upguide" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gridGuide" AutoGenerateColumns="false" AllowPaging="false"
                        SkinID="sknSubGrid" Width="800px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="265px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader1" runat="server" Text="Package Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPackName" runat="server" Text='<%# Bind("GIT_TRANSFER_PACKGE_NAME") %>'
                                        CssClass="lblstyle"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="80px" Visible="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader2" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="80px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader3" runat="server" Text="No of Guides" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGuides" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader4" runat="server" Text="Select Supplier" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="drpSupplier">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGuide" Text="Rate" OnClick="btnGuide_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddGuide" runat="server" Text="ADD" OnClick="btnAddGuide_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divBoatRate" class="pageTitle">
            <asp:Label ID="Label2" runat="server" Text="Boat Rate" class="pageTitle" Width="100px"
                Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
            <asp:UpdatePanel ID="upboat" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gridBoat" AutoGenerateColumns="false" AllowPaging="false"
                        SkinID="sknSubGrid" Width="800px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="265px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader1" runat="server" Text="Package Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPackName" runat="server" Text='<%# Bind("GIT_TRANSFER_PACKGE_NAME") %>'
                                        CssClass="lblstyle"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="250px" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackid" runat="server" CssClass="lblstyle" Text="0"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="80px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader2" runat="server" Text="No of Boats" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBoats" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeader3" runat="server" Text="Select Supplier" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="drpSupplier">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField ControlStyle-Width="80px">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader3" runat="server" Text="Boat Rate" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtGuides" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                            <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbltime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:RadTimePicker ID="rdtpTime" runat="server" Width="90px">
                                        <TimeView TimeFormat="h:mm tt">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="hh:mm tt">
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnRemove" Text = "Remove" OnClick="btnBoatRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnBoat" Text="Rate" OnClick="btnBoat_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddBoat" runat="server" Text="ADD" OnClick="btnAddBoat_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="pageTitle" id="divMisc">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblMisc" Text="Misc Expenses" Font-Bold="true" Width="100px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMisc" Width="100px"> </asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div class="pageTitle" id="divButtons">
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnUpdate" Text="Save" Width="100px" OnClick="btnUpdate_Click" />&nbsp;
                        &nbsp;
                        <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" Width="100px" />
                        <asp:Button runat="server" ID="btnTime" Text="Save TIme" Width="100px" OnClick="btnUpdateTime_Click" />&nbsp;
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
