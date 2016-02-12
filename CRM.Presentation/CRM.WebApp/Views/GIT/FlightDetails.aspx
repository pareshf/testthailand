<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="FlightDetails.aspx.cs" Inherits="CRM.WebApp.Views.GIT.FlightDetails" %>

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
    
    <style>
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
    </style>
    
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
    </script>

    <div class="pageTitle">
        <asp:Label ID="Label1" runat="server" Text="Flight Details" Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="upFlight" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="lblArrivalFlight" runat="server" Text="Arrival Flight Details" Width="200px"
                    Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="GridArrival" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid"
                    Width="500px">
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="300px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFlightName" runat="server" Text="Flight Name" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtFlightName" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblFlightDate" runat="server" Text="Flight Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFlightDate" runat="server" ></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFlightDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFlightTime" runat="server" Text="Flight Time" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:radtimepicker id="rdtpTime" runat="server" width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="200px">
                            <HeaderTemplate>
                                <asp:Label ID="lblNoOfPassenger" runat="server" Text="No Of Passenger" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNoOfPassenger" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnArrRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnAddArrivalFlight" runat="server" Text="Add Arrival Flight" OnClick="btnArrivalAdd_Click" />
                <br />
                <br />
                <asp:Label ID="lblDepartureFlight" runat="server" Text="Departure Flight Details"
                    Width="200px" Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="GridDeparture" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid"
                    Width="500px">
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="300px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFlightName" runat="server" Text="Flight Name" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtFlightName" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblFlightDate" runat="server" Text="Flight Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFlightDate" runat="server" ></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFlightDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFlightTime" runat="server" Text="Flight Time" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <telerik:radtimepicker id="rdtpTime" runat="server" width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="200px">
                            <HeaderTemplate>
                                <asp:Label ID="lblNoOfPassenger" runat="server" Text="No Of Passenger" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNoOfPassenger" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btndepRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnAddDepartureFlight" runat="server" Text="Add Departure Flight" OnClick="btnDepartureAdd_Click" />
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Save" ID="btnSave" Width="100px" OnClick="btnSave_Click"/>                            
                            <asp:Button runat="server" Text="Back" ID="btnBack" Width="100px" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
