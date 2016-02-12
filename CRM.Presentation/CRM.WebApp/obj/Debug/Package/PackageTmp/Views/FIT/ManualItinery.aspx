<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="ManualItinery.aspx.cs" Inherits="CRM.WebApp.Views.FIT.ManualItinery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
       
    </script>
    <style type="text/css">
        .downloadlink
        {
            font-size: 13px !important;
            text-decoration: none;
        }
    </style>
    <asp:Label ID="lbltitle" runat="server" Text="Manual Itinery" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <%--------------------------------Form---------------------------------------------%>
    <div id="generate_invoice" runat="server" class="pageTitle">
        <asp:UpdatePanel ID="UpdatePanel_Manual_Itinery" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="6"
                    cellpadding="5">
                    <tr>
                        <td>
                            <table cellspacing="2">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpAgent" runat="server" Width="250px" OnSelectedIndexChanged="drpAgent_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Invoice" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpInvoice" runat="server" Width="250px" OnSelectedIndexChanged="drpInvoice_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Pax Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClientName" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Date Of Arrival" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblArrival" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Date Of Departure" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDeparture" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Arrival Flight With Time" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArrival_Flight" runat="server" CssClass="textboxstyle" Width="140px"></asp:TextBox>
                                        <telerik:RadTimePicker ID="rtparrival" runat="server" Width="110px">
                                            <TimeView TimeFormat="h:mm tt">
                                            </TimeView>
                                            <DateInput DisplayDateFormat="hh:mm tt">
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Departure Flight With Time" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeparture_Flight" runat="server" CssClass="textboxstyle" Width="140px"></asp:TextBox>
                                        <telerik:RadTimePicker ID="rtpDeparture" runat="server" Width="110px">
                                            <TimeView TimeFormat="h:mm tt">
                                            </TimeView>
                                            <DateInput DisplayDateFormat="hh:mm tt">
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="No Of Adult" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNoOfAdult" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="No Of Child" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNoOfChild" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellspacing="2">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="No Of CWB" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNOofCWB" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="No Of CNB" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNoOfCNB" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="NO Of Infant" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNoOfInfant" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="No. Of Rooms Single" CssClass="lblstyle"
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomSingle" runat="server" CssClass="textboxstyle" Width="250px"
                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbldouble" runat="server" Text="No. Of Rooms Double / Twin" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomDouble" runat="server" CssClass="textboxstyle" Width="250px"
                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltriple" runat="server" Text="No. Of Rooms Triple" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomTriple" runat="server" CssClass="textboxstyle" Width="250px"
                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Meeting Point" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtmeeting" runat="server" CssClass="textboxstyle" Width="250px"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxstyle" Width="250px"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- <asp:Button ID="btnSave" runat="server" Text="Save" Style="padding: 0px" ValidationGroup="Required"
                                            OnClick="btnSave_Click" />
                                        <asp:Button ID="btnclose1" runat="server" Text="Close-1" Style="padding: 0px" ValidationGroup="Required"
                                            Visible="false" OnClick="btnclose1_Click" OnClientClick="javascript:return confirm('After clicking on close-1 you cannot edit this invoice,are you sure want to proceed?');" />
                                        <asp:Button ID="btnclose2" runat="server" Text="Close-2" Style="padding: 0px" ValidationGroup="Required"
                                            Visible="false" OnClick="btnclose2_Click" OnClientClick="javascript:return confirm('Are you sure you want to close-2 this invoice?');" />--%>
                                    </td>
                                    <%--<td>
                                        <asp:Button ID="btnSendInvoice" runat="server" Text="Send Invoice To Agent" OnClick="btnSendInvoice_Click" />
                                        &nbsp;<a id="lnkbtn" runat="server" class="downloadlink" style="font-family: Verdana;
                                            display: none;" target="_blank"> Download Invoice</a>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCopy" runat="server" Text="Copy Booking" OnClick="btnCopy_Click"
                                            Visible="false" />
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <%--------------------------------HOTELS---------------------------------------------%>
    <div id="divhotels" runat="server" class="pageTitle">
        <table border="1" class="pageTitle">
            <tr>
                <td>
                    <div id="divHotel1" runat="server" class="pageTitle" style="padding-right: 10px;">
                        <asp:UpdatePanel ID="upHotel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Label ID="lblHotel1" runat="server" Text="Hotel" CssClass="headlabel" Font-Bold="true"></asp:Label>&nbsp;<span
                                    class="error"></span>
                                <br />
                                <br />
                                <asp:GridView ID="GridHotel" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="800px">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmain" runat="server" CssClass="lblstyle" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="200px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text="City" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpCity" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="280px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <%-- <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                <asp:TextBox ID="txtHotelName" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              <%--  <asp:DropDownList ID="drpRoomType" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                <asp:TextBox ID="txtRoomType" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCheckOutDate" runat="server"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="60px">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnHotelRemove_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Button ID="btnAddHotel1" runat="server" Text="Add Hotel" OnClick="btnAddHotel_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--------------------------------Schecule---------------------------------------------%>
    <div id="divSchedule" runat="server" class="pageTitle">
        <table border="1" class="pageTitle">
            <tr>
                <td>
                    <div id="div2" runat="server" class="pageTitle" style="padding-right: 10px;">
                        <asp:UpdatePanel ID="UpSchedule" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Label ID="lbls" runat="server" Text="Schedule" CssClass="headlabel" Font-Bold="true"></asp:Label>&nbsp;<span
                                    class="error"></span>
                                <br />
                                <br />
                                <asp:GridView ID="grdSchedule" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="800px">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchedule" runat="server" CssClass="lblstyle" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCitySchedule" runat="server" Text="City" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpCitySchedule" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="215px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTSM" runat="server" Text="Transfers / Sightseeings / Meals detail"
                                                    CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTSM" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="90px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPickTime" runat="server" Text="Pick up Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadTimePicker ID="rtpPick" runat="server">
                                                    <TimeView TimeFormat="h:mm tt">
                                                    </TimeView>
                                                    <DateInput DisplayDateFormat="hh:mm tt">
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="45px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSicPvt" runat="server" Text="SIC / PVT" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSICPVT" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="220px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSignature" runat="server" Text="Guest Signature / Restaurant & ETC"
                                                    CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsignature" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="60px">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnScheduleRemove_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Button ID="btnSchedule" runat="server" Text="Add Schedule" OnClick="btnSchedule_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--------------------------------Passenger ---------------------------------------------%>
    <div id="divPass" runat="server" class="pageTitle">
        <table border="1" class="pageTitle">
            <tr>
                <td>
                    <div id="divpassport" runat="server" class="pageTitle" style="padding-right: 10px;">
                        <asp:UpdatePanel ID="upPassenger" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Label ID="Label14" runat="server" Text="Travelling Passenger Detail" CssClass="headlabel"
                                    Font-Bold="true"></asp:Label>&nbsp;<span class="error"></span>
                                <br />
                                <br />
                                <asp:GridView ID="gvpass" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="800px">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPassenger" runat="server" CssClass="lblstyle" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblname" runat="server" Text="Name" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="360px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblpass" runat="server" Text="Passport No" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPassportno" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnationality" runat="server" Text="Nationality" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpNationality" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="60px">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnPassengerRemove_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Button ID="btnName" runat="server" Text="Add Passenger Detail" OnClick="btnName_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" Style="padding: 0px" ValidationGroup="Required"
                            OnClick="btnSave_Click" Width="100px" />
                    </td>
                    <td>
                        &nbsp;<a id="lnkbtn" runat="server" class="downloadlink" style="font-family: Verdana;
                            display: none;" target="_blank"> Download ManualItinery</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="TransparentGrayBackground">
            </div>
            <div class="Sample6PageUpdateProgress">
                <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                    AlternateText="" />
                &nbsp;Please Wait...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <form id="form2" runat="server" visible="false">
    <div>
        <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" BorderStyle="Solid"
            BorderWidth="1px" Height="8.5in" Width="14in">
        </rsweb:ReportViewer>
    </div>
    </form>
</asp:Content>
