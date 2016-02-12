<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Hotels.aspx.cs" Inherits="CRM.WebApp.Views.FIT.Hotels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/Views/FIT/MulticheckDropdown.ascx" TagName="DropDownControl"
    TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: normal;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        
        .HellowWorldPopup
        {
            min-width: 200px;
            min-height: 150px;
            background: white;
        }
        .hyperlink
        {
            font-size: 12px;
            color: Blue;
            text-decoration: none;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
    </script>
    <div style="position: relative">
        <asp:Label ID="Label55" runat="server" Text="FIT Package Details" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <%--------------------------------------------------------------------------------------HOTELS---------------------------------------------------------------------------------------------%>
        <asp:Label ID="lblhotel" runat="server" Text="Hotels" class="pageTitle" Width="100px"
            Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
        <br />
        <asp:Label ID="Label28" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <asp:Label ID="Label49" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <asp:Label ID="Label50" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <%--<asp:LinkButton ID="lbtnHotelRate" runat="server" Text="Hotel Rate"></asp:LinkButton>--%>
        <%---1--------------------PATTAYA HOTELS------------------------------%>
        <div id="ptydiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelPty" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblpattaya" runat="server" Text="Pattaya" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>&nbsp;&nbsp;&nbsp;
                   <%-- <asp:HyperLink ID="hyperPriceRatePattaya" runat="server" Text="Hotel PriceList" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Pattaya"
                        Target="_blank"></asp:HyperLink>
                    <br />--%>
                    <table width="900px" id="pattyahotels" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label8" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label9" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label10" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label11" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label12" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td3">
                                <asp:Label ID="Label30" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td id="ptytd" style="width: 50px;">
                                <asp:Label ID="Labelpty" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusPTY" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="TdStarPattaya">
                                <asp:Label ID="Label39" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="TdLocPattaya">
                                <asp:Label ID="Label51" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                            <%--<td style="width: 100px">
                                <asp:Label ID="Label100" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrpty_HotelName" runat="server" OnSelectedIndexChanged="ddrpty_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrpty_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrpty_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtpty_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtpty_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender111" runat="server" targetcontrolid="txtpty_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderPattya1" runat="server" targetcontrolid="txtpty_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtpty_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtpty_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender22" runat="server" targetcontrolid="txtpty_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderPattya11" runat="server" targetcontrolid="txtpty_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblpty_Noof_Romms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td1" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </td>
                            <td id="ptyredtd" style="width: 50px;">
                                <asp:RadioButton ID="Rbtnconfirmpty" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmpty"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusPty" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="lblStarPattya1">
                                <telerik:RadRating ID="RadRating1" runat="server" ItemCount="5" AutoPostBack="true"
                                    SelectionMode="Continuous" Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPattaya1">
                                <asp:Label ID="lblLocPattaya" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                            <%--<td style="width: 50px;" runat="server" id="td39">
                                <asp:HyperLink ID="hyperlink" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Pattaya"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---2--------------------PHUKET HOTELS------------------------------%>
        <div id="phudiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelPhuket" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblphuket" runat="server" Text="Phuket" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>&nbsp;&nbsp;&nbsp;
                    <br />
                    <table width="900px" id="tblphukethotels" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label14" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label15" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label16" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label17" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label18" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td5">
                                <asp:Label ID="Label31" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="phutd">
                                <asp:Label ID="Labelphu" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusPHU" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td21">
                                <asp:Label ID="Label40" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPhuket">
                                <asp:Label ID="Label70" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 100px">
                                <asp:Label ID="Label99" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrphu_HotelName" runat="server" OnSelectedIndexChanged="ddrphu_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrphu_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrphu_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtphu_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtphu_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender23" runat="server" targetcontrolid="txtphu_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderPhuket1" runat="server" targetcontrolid="txtphu_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtphu_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtphu_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender24" runat="server" targetcontrolid="txtphu_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderPhuket11" runat="server" targetcontrolid="txtphu_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtphu_noofrooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td6" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="phuredtd">
                                <asp:RadioButton ID="rbtnconfirmphu" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmphu"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusPhu" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td22">
                                <telerik:RadRating ID="RadRating2" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPhuket1">
                                <asp:Label ID="lblLocPhuket" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 50px;" runat="server" id="td40">
                                <asp:HyperLink ID="hyperPriceRatePhuket" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Phuket"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---3--------------------KBV (KRABI) HOTELS------------------------------%>
        <div id="bkvdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelkBV" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblkbv" runat="server" Text="Krabi" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tblkbv" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label7" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label13" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label52" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label53" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label54" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td7">
                                <asp:Label ID="Label32" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="kbvtd">
                                <asp:Label ID="Labelkbv" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusKBV" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td23">
                                <asp:Label ID="Label41" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocKrabi">
                                <asp:Label ID="Label76" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                            <%--<td style="width: 100px">
                                <asp:Label ID="Label101" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrkbv_HotelName" runat="server" OnSelectedIndexChanged="ddrkbv_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrkbv_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrkbv_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtkbv_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtkbv_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                        id="TextBoxWatermarkExtender25" runat="server" targetcontrolid="txtkbv_CheckIn"
                                        watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                    </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderKrabi1" runat="server" targetcontrolid="txtkbv_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtkbv_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtkbv_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender26" runat="server" targetcontrolid="txtkbv_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderKrabi11" runat="server" targetcontrolid="txtkbv_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblkbv_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td8" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="kbvredtd">
                                <asp:RadioButton ID="rbtnconfirmkbv" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmpkbv"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusKbv" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td24">
                                <telerik:RadRating ID="RadRating3" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocKrabi1">
                                <asp:Label ID="lblLocKrabi" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 100px;" runat="server" id="td41">
                                <asp:HyperLink ID="hyperlinkKrabi" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Krabi"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---4--------------------USM (Samui) HOTELS------------------------------%>
        <div id="usmdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelUsm" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblusm" runat="server" Text="Samui" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tblusm" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label57" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label58" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label59" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label60" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label61" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td9">
                                <asp:Label ID="Label33" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="usmtd">
                                <asp:Label ID="Labelusm" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusUSM" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td25">
                                <asp:Label ID="Label42" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocUsm">
                                <asp:Label ID="Label77" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                            <%--<td style="width: 100px">
                                <asp:Label ID="Label102" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrusm_HotelName" runat="server" OnSelectedIndexChanged="ddrusm_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrusm_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrusm_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtusm_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtusm_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender27" runat="server" targetcontrolid="txtusm_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderSamui1" runat="server" targetcontrolid="txtusm_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtusm_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtusm_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender28" runat="server" targetcontrolid="txtusm_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderSamui11" runat="server" targetcontrolid="txtusm_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtusm_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td10" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox5" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="usmredtd">
                                <asp:RadioButton ID="rbtnconfirmusm" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmusm"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusKbvUsm" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td26">
                                <telerik:RadRating ID="RadRating4" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocUsm1">
                                <asp:Label ID="lblLocUsm" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 50px;" runat="server" id="td42">
                                <asp:HyperLink ID="hyperlink1" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Samui"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---5--------------------CNX ( Chiangmai) HOTELS------------------------------%>
        <div id="cnxdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="uphotelCnx" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblcnx" runat="server" Text="Chiangmai" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tblcnx" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label64" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label65" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label66" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label67" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label68" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td11">
                                <asp:Label ID="Label34" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="cnxtd">
                                <asp:Label ID="Labelcnx" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusCNX" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td27">
                                <asp:Label ID="Label43" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocCnx">
                                <asp:Label ID="Label83" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 100px">
                                <asp:Label ID="Label103" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrcnx_HotelName" runat="server" OnSelectedIndexChanged="ddrcnx_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrcnx_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrcnx_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtcnx_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtcnx_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender29" runat="server" targetcontrolid="txtcnx_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderChiangmai1" runat="server" targetcontrolid="txtcnx_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtcnx_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtcnx_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender30" runat="server" targetcontrolid="txtcnx_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderChiangmai11" runat="server" targetcontrolid="txtcnx_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtcnx_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td12" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox6" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="cnxredtd">
                                <asp:RadioButton ID="rbtnconfirmcnx" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmcnx"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusCnx" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td28">
                                <telerik:RadRating ID="RadRating5" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocCnx1">
                                <asp:Label ID="lblLocCnx" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 50px;" runat="server" id="td43">
                                <asp:HyperLink ID="hyperlink2" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Chiangmai"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---6--------------------ETC HOTELS (Hua Hin)------------------------------%>
        <div id="etcdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="uphotelEtc" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lbletc" runat="server" Text="Hua Hin" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tbletc" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label71" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label72" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label73" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label74" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label75" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td13">
                                <asp:Label ID="Label35" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="etctd">
                                <asp:Label ID="Labeletc" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusETC" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td29">
                                <asp:Label ID="Label44" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocEtc">
                                <asp:Label ID="Label84" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 100px">
                                <asp:Label ID="Label104" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddretc_HotelName" runat="server" OnSelectedIndexChanged="ddretc_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddretc_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddretc_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtetc_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtetc_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender31" runat="server" targetcontrolid="txtetc_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderHuahin1" runat="server" targetcontrolid="txtetc_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtetc_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtetc_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender32" runat="server" targetcontrolid="txtetc_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderHuahin11" runat="server" targetcontrolid="txtetc_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtetc_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td14" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox7" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="etcredtd">
                                <asp:RadioButton ID="rbtnconfirmetc" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmetc"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusEtc" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td30">
                                <telerik:RadRating ID="RadRating6" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocEtc1">
                                <asp:Label ID="lblLocEtc" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 50px;" runat="server" id="td44">
                                <asp:HyperLink ID="hyperlink3" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Hua Hin"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---7--------------------SS HOTELS (KANCHANBURI)------------------------------%>
        <div id="ssdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelSS" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSS" runat="server" Text="Kanchanburi" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tblss" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label78" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label79" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label80" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label81" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label82" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td15">
                                <asp:Label ID="Label36" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="sstd">
                                <asp:Label ID="Labelss" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusSS" runat="server" Text="Status" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td31">
                                <asp:Label ID="Label45" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSs">
                                <asp:Label ID="Label90" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 100px">
                                <asp:Label ID="Label105" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrss_HotelName" runat="server" OnSelectedIndexChanged="ddrss_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrss_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrss_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtss_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtss_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender33" runat="server" targetcontrolid="txtss_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderKanchanburi1" runat="server" targetcontrolid="txtss_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtss_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtss_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender34" runat="server" targetcontrolid="txtss_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderKanchanburi11" runat="server" targetcontrolid="txtss_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtss_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td16" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox8" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="ssredtd">
                                <asp:RadioButton ID="rbtnconfirmss" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmss"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusSs" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td32">
                                <telerik:RadRating ID="RadRating7" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSs1">
                                <asp:Label ID="lblLocSs" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 50px;" runat="server" id="td45">
                                <asp:HyperLink ID="hyperlink4" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Kanchanburi"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---8--------------------SPS HOTELS (Chingrai)------------------------------%>
        <div id="spsdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="uphotelsps" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblsps" runat="server" Text="Chingrai" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="tblsps" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label85" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label86" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label87" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label88" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label89" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td17">
                                <asp:Label ID="Label37" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="spstd">
                                <asp:Label ID="Labelsps" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusSPS" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td33">
                                <asp:Label ID="Label46" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSps">
                                <asp:Label ID="Label91" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                            <%--<td style="width: 100px">
                                <asp:Label ID="Label106" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrsps_HotelName" runat="server" OnSelectedIndexChanged="ddrsps_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrsps_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrsps_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtsps_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtsps_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender35" runat="server" targetcontrolid="txtsps_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderChingrai1" runat="server" targetcontrolid="txtsps_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtsps_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtsps_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender36" runat="server" targetcontrolid="txtsps_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderChingrai11" runat="server" targetcontrolid="txtsps_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtsps_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td18" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox9" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px" id="spsredtd">
                                <asp:RadioButton ID="rbtnconfirsps" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmsps"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusSps" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td34">
                                <telerik:RadRating ID="RadRating8" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSps1">
                                <asp:Label ID="lblLocSps" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 50px;" runat="server" id="td46">
                                <asp:HyperLink ID="hyperlink5" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Chingrai"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---9--------------------ZZ (Phi Phi Island) HOTELS------------------------------%>
        <div id="zzdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="uphotelZZ" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblzz" runat="server" Text="Phi Phi Island" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" id="txtzz" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label92" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label93" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label94" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label95" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label96" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td19">
                                <asp:Label ID="Label38" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px">
                                <asp:Label ID="Labelzz" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusZZ" runat="server" Text="Status" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td35">
                                <asp:Label ID="Label47" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocZz">
                                <asp:Label ID="Label97" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 100px">
                                <asp:Label ID="Label107" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrzz_HotelName" runat="server" OnSelectedIndexChanged="ddrzz_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrzz_RoomType" runat="server" OnSelectedIndexChanged="ddrzz_RoomType_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtzz_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtzz_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender37" runat="server" targetcontrolid="txtzz_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderzz1" runat="server" targetcontrolid="txtzz_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtzz_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtzz_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender38" runat="server" targetcontrolid="txtzz_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderzz11" runat="server" targetcontrolid="txtzz_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtzz_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td20" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox10" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 50px">
                                <asp:RadioButton ID="rbtnconfirmzz" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmzz"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <%--<asp:Label ID="" runat="server" ></asp:Label>--%>
                                <asp:Label ID="lblStatusZz" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td36">
                                <telerik:RadRating ID="RadRating9" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocZz1">
                                <asp:Label ID="lblLoczz" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                           <%-- <td style="width: 50px;" runat="server" id="td47">
                                <asp:HyperLink ID="hyperlink6" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Phi Phi Island"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---10--------------------BANGKOK HOTELS------------------------------%>
        <div id="bkkdiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="uphotelBkk" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblbangkok" runat="server" Text="Bangkok" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                    <table width="900px" runat="server" id="tbl_bnagkokhotels" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                        <tr style="background-color: #f3f3f3">
                            <td style="width: 200px">
                                <asp:Label ID="Label2" runat="server" Text="Hotel" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label3" runat="server" Text="Room Type" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label4" runat="server" Text="Check-in Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label5" runat="server" Text="Check-out Date" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label6" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td4">
                                <asp:Label ID="Label29" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="bkktd">
                                <asp:Label ID="Label1" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusBKK" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td37">
                                <asp:Label ID="Label48" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocBkk">
                                <asp:Label ID="Label69" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                         <%--   <td style="width: 100px">
                                <asp:Label ID="Label108" runat="server" Text="View Price" CssClass="headlabel"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddrbkk_HotelName" runat="server" OnSelectedIndexChanged="ddrbkk_HotelName_SelectedIndexChanged"
                                    AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 150px">
                                <asp:DropDownList ID="ddrbkk_RoomType" runat="server" Width="150px" OnSelectedIndexChanged="ddrbkk_RoomType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtbkk_CheckIn" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtbkk_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender39" runat="server" targetcontrolid="txtbkk_CheckIn"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderBangkok1" runat="server" targetcontrolid="txtbkk_CheckIn"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtbkk_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtbkk_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender40" runat="server" targetcontrolid="txtbkk_CheckOut"
                                    watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                </ajax:textboxwatermarkextender>
                                <ajax:calendarextender id="CalendarExtenderBangkok11" runat="server" targetcontrolid="txtbkk_CheckOut"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblbkkNoof_Romms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td2" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </td>
                            <td id="bkkredtd" style="width: 50px">
                                <asp:RadioButton ID="rbtnbkkconfirm" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChangedforconfirmbkk"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusBkk" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td38">
                                <telerik:RadRating ID="RadRating10" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocBkk1">
                                <asp:Label ID="lblLocBkk" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                          <%--  <td style="width: 50px;" runat="server" id="td48">
                                <asp:HyperLink ID="hyperlink7" runat="server" Text="View Price" NavigateUrl="~/Views/FIT/Adminhotelrate.aspx?city=Bangkok"
                                    Target="_blank" CssClass="hyperlink"></asp:HyperLink>
                            </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%--<div id="bkkeditconfirmdiv" runat="server" visible="false">--%>
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table runat="server">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Confirm" Visible="false" OnClick="Button1_onclick" />
                            <asp:Button ID="Button3" runat="server" Text="Wait List" Visible="false" />
                            <asp:Button ID="Button4" runat="server" Text="Change Of Hotel" Visible="false" />
                        </td>
                    </tr>
                </table>
                <ajax:modalpopupextender id="PopEx_lnkBtnChangePreference" runat="server" backgroundcssclass="modalPopupBackground"
                    popupcontrolid="pnlCompanyRoleSelection" targetcontrolid="Button1" drag="true"
                    popupdraghandlecontrolid="pnlCompanyRoleSelectionHeader" cancelcontrolid="ImageButton1">
                </ajax:modalpopupextender>
                <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px"
                    Style="display: none;">
                    <asp:Panel ID="Panel1" runat="server" Width="350px">
                        <fieldset style="background-color: White">
                            <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTitleAlert" runat="server" Text="Please Enter Reconfirmation Date"
                                                ForeColor="#FEFEFE" Font-Size="15px"></asp:Label>
                                        </td>
                                        <td style="width: 17px;" align="center" valign="middle">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                                Style="cursor: pointer;" ToolTip="Close" />
                                            <%--<asp:Button ID="Button5" runat="server" Text="Button" style="display:none" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label19" runat="server" Text="Reconfirmation Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="100px" OnTextChanged="TextBox1_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:Label ID="labelformet" runat="server" Text="dd/mm/yyyy"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelerror" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label56" runat="server" Text="Confirmation Number" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label62" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="TextBox3" runat="server" Width="100px" OnTextChanged="TextBox3_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="label63" runat="server" Text=""></asp:Label>
                                    </td>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reconfirmation date is required"
                                            ControlToValidate="TextBox1" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Confirmation Number is required"
                                            ControlToValidate="TextBox2" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Payment date is required"
                                            ControlToValidate="TextBox3" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button6" runat="server" Text="Confirm" ValidationGroup="popup" OnClick="Button6_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- </div>--%>
        <%---------------------------------------------------------------TRANSFER PACKAGES------------------------------------------------------%>
        <asp:Label ID="Label20" runat="server" Text="Transfer Package" Width="140px" Style="font-weight: normal;
            font-size: 16px; font-family: Verdana" class="pageTitle"></asp:Label>&nbsp;<span
                class="error">*</span>
        <br />
        <div class="pageTitle">
            <asp:UpdatePanel ID="uptransferPackage" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" OnRowDataBound="dlhoteldetails_ItemDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label21" runat="server" Text="Sr No" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTPsrno" runat="server" CssClass="lblstyle" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label22" runat="server" Text="Package Details" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTPpackageDetails" runat="server" Text='<%# Bind("NAME") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label23" runat="server" Text="SIC / PVT" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drptransfer" runat="server" OnSelectedIndexChanged="drptransfer_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="True">SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label24" runat="server" Text="Date" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTPdate" runat="server" CssClass="textboxstyle" OnTextChanged="txtTPdate_TextChanged"
                                                    AutoPostBack="true" Width="100px"></asp:TextBox><ajax:textboxwatermarkextender id="TextBoxWatermarkExtendertp"
                                                        runat="server" targetcontrolid="txtTPdate" watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label25" runat="server" Text=" Pickup Time" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="tpdrp_time" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label26" runat="server" Text="Add to Cart" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbtnTPselect" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_priceid" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_PRICE_ID") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_flag" runat="server" Text='<%# Bind("FLAG") %>' CssClass="lblstyle"
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_detialid" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_FROM_TO_DETAIL_ID") %>'
                                                    CssClass="lblstyle" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="btnRemoveTp" Text="Clear Transfer Package Selection" OnClick="btnRemoveTp_Click"
                        runat="server" />
                    <asp:Button ID="btnArrivalDate" Text="Change Arrival Date" OnClick="btnArrivalDate_Click"
                        runat="server" />
                    <asp:Button ID="btnDepartureDate" Text="Change Departure Date" OnClick="btnDepartureDate_Click"
                        runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <br />
        <%-------------------------------------------------------------------------------SIGHT SEEING------------------------------------%>
        <asp:Label ID="Label27" runat="server" Text="Sightseeing (Optional)" class="pageTitle"
            Width="200px" Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
        <br />
        <br />
        <%---1----------------------------PATTAYA SIGHT SEEING--------------------------------------%>
        <div id="Div2" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblPTY_bkk_header" runat="server" Text="Pattaya" CssClass="headlabel"
                Visible="false"></asp:Label>
            <br />
            <asp:UpdatePanel ID="upSSPty" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GV_Result" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_Ptysrno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_PtypackageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_Ptydate" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_Ptydate_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender1" runat="server" targetcontrolid="txtSS_Ptydate"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtSS_Ptydate"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_pty_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_PtynoofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_pty_select" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpty_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricepattaya" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricepattaya" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---2----------------------------PHUKET SIGHT SEEING--------------------------------------%>
        <div id="Div3" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_PHU_header" runat="server" Text="Phuket" CssClass="headlabel"
                Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_phuket" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_phuketsrno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_phuketpackageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_phuketdate" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_phuketdate_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender2" runat="server" targetcontrolid="txtSS_phuketdate"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtSS_phuketdate"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_phu_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_phuketnoofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_phu_select" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblphu_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstylePrice" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypersigthprice" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---3----------------------------KBV (Krabi) SIGHT SEEING--------------------------------------%>
        <div id="Div4" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_Kbv" runat="server" Text="Krabi" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_Kbv" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_kbv_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_kbv_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_kbv_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_kbv_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender12" runat="server" targetcontrolid="txtSS_kbv_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender3" runat="server" targetcontrolid="txtSS_kbv_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_kbv_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_kbv_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_kbv_select" runat="server" GroupName="sightseeingKBV" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkbv_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL4" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricekarabi" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricekarabi" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---4----------------------------USM(Samui) SIGHT SEEING--------------------------------------%>
        <div id="Div5" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_usm" runat="server" Text="Samui" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_usm" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_usm_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_usm_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList5" runat="server" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_usm_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_usm_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender4" runat="server" targetcontrolid="txtSS_usm_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender4" runat="server" targetcontrolid="txtSS_usm_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_usm_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_usm_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_usm_select" runat="server" GroupName="sightseeingUSM" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusm_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL5" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricesamui" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricesamui" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---5----------------------------CNX (Chiangmai) SIGHT SEEING--------------------------------------%>
        <div id="Div6" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSScnx" runat="server" Text="Chiangmai" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_cnx" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_cnx_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_cnx_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList6" runat="server" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_cnx_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_cnx_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender5" runat="server" targetcontrolid="txtSS_cnx_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender5" runat="server" targetcontrolid="txtSS_cnx_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_cnx_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_cnx_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_cnx_select" runat="server" GroupName="sightseeingCNX" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcnx_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL6" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricechaingmai" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricechaingmai" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---6----------------------------ETC (Hua Hin)SIGHT SEEING--------------------------------------%>
        <div id="Div7" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_etc" runat="server" Text="Hua Hin" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_Etc" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_etc_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_etc_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList7" runat="server" OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_etc_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_etc_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender6" runat="server" targetcontrolid="txtSS_etc_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender6" runat="server" targetcontrolid="txtSS_etc_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_etc_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_etc_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_etc_select" runat="server" GroupName="sightseeingETC" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbletc_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL7" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricehuahin" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricehuahin" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---7----------------------------SS (KANCHANBURI) SIGHT SEEING--------------------------------------%>
        <div id="Div8" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_s" runat="server" Text="Kanchanburi" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_s" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_s_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_s_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList8" runat="server" OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_s_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_s_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender7" runat="server" targetcontrolid="txtSS_s_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender7" runat="server" targetcontrolid="txtSS_s_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_ss_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_s_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_s_select" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblss_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL8" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricekanchanburi" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricekanchnburi" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---8----------------------------SPS (Chingrai)SIGHT SEEING--------------------------------------%>
        <div id="Div9" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_sps" runat="server" Text="Chiangrai" CssClass="headlabel" Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_sps" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_sps_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_sps_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList9" runat="server" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_sps_date" runat="server" CssClass="textboxstyle" Width="80px"
                                                    OnTextChanged="txtSS_sps_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:textboxwatermarkextender
                                                        id="TextBoxWatermarkExtender8" runat="server" targetcontrolid="txtSS_sps_date"
                                                        watermarktext="dd/MM/yyyy">
                                                    </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender8" runat="server" targetcontrolid="txtSS_sps_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_sps_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_sps_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_sps_select" runat="server" GroupName="sightseeingSPS" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsps_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL9" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricechingrai" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricechingrai" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---9----------------------------Phi Phi Island SIGHT SEEING--------------------------------------%>
        <div id="Div10" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_zz" runat="server" Text="Phi Phi Island" CssClass="headlabel"
                Visible="false"></asp:Label><br />
            <asp:UpdatePanel ID="upSS_zz" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_zz_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_zz_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList10" runat="server" OnSelectedIndexChanged="DropDownList10_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_zz_date" runat="server" CssClass="textboxstyle" Width="80px"></asp:TextBox><ajax:textboxwatermarkextender
                                                    id="TextBoxWatermarkExtender9" runat="server" targetcontrolid="txtSS_zz_date"
                                                    watermarktext="dd/MM/yyyy">
                                                </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender9" runat="server" targetcontrolid="txtSS_zz_date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_zz_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_zz_noofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Text="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_zz_select" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblzz_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL10" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricephiphi" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricephiphi" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---10----------------------------BANGKOK SIGHT SEEING--------------------------------------%>
        <div id="Div1" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upSSbkk_Header" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <asp:Label ID="lblSS_bkk" runat="server" Text="Bangkok" CssClass="headlabel"></asp:Label><br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upSSbkk" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_Bkk_srno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_Bkk_packageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                    CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>SIC</asp:ListItem>
                                                    <asp:ListItem>PVT</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_Bkkdate" runat="server" CssClass="textboxstyle" OnTextChanged="txtSS_Bkkdate_TextChanged"
                                                    AutoPostBack="true" Width="80px"></asp:TextBox>
                                                <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender10" runat="server" targetcontrolid="txtSS_Bkkdate"
                                                    watermarktext="dd/MM/yyyy">
                                                </ajax:textboxwatermarkextender>
                                                <ajax:calendarextender id="CalendarExtender10" runat="server" targetcontrolid="txtSS_Bkkdate"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_bkk_time_selectedindexchanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSS_Bkk_noofMeals" runat="server" CssClass="textboxstyle" Text="1"
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_bkk_select" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbkk_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPricebangkok" runat="server" Text="View Price" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperpricebangkok" runat="server" Text="Price" Target="_blank" NavigateUrl='<%# Eval("SIGHT_SEEING_PRICE_ID", @"Adminsightseenrate.aspx?id={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <br />
        <%---------------------------------ADDITIONAL SERVICES--------------------------------------%>
        <div id="divAddService" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upAddServices" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="Label98" runat="server" Text="Additional Services" Width="200px" Style="font-weight: normal;
                        font-size: 16px; font-family: Verdana"></asp:Label>
                    <br />
                    <asp:GridView ID="gridAddServices" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                        PageSize="15" Width="900px">
                        <%-- <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>--%>
                        <Columns>
                            <%--   <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />--%>
                            <asp:BoundField DataField="SERVICE_DESCRIPTION" HeaderText="Services" />
                            <asp:BoundField DataField="SIC_PVT_FLAG" HeaderText="SIC/PVT" />
                            <asp:BoundField DataField="DATE" HeaderText="Date" />
                            <asp:BoundField DataField="NO_OF_PAX" HeaderText="No. of PAX." />
                            <asp:BoundField DataField="FROM_DETAIL" HeaderText="From" />
                            <asp:BoundField DataField="TO_DETAIL" HeaderText="To" />
                        </Columns>
                        <HeaderStyle CssClass="rgHeader" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <%-- <div style="margin: 10px">
            <asp:RadioButton ID="rbtnUSD" runat="server" Text="USD Quote" GroupName="currency" />
            <asp:RadioButton ID="rbtnTHB" runat="server" Text="THB Quote" GroupName="currency" />
            <asp:RadioButtonList ID="rbtncurrency" runat="server">
                <asp:ListItem Selected="True" Text="USD Quote" Value="USD"></asp:ListItem>
                <asp:ListItem Text="THB Quote" Value="THB"></asp:ListItem>
            </asp:RadioButtonList>
        </div>--%>
        <%---------------------------------------------------------------------------------------BUTTONS--------------------------------------------------------------------------------------------%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnGetQuote" runat="server" Text="Generate Quote" Width="150px" OnClick="btnGetQuote_Click" />
                        </td>
                        <td>
                            <%--<asp:Button ID="Button5" runat="server" Text="Download Quote" Width="150px" style="display:none;" OnClick="btnDownloadQuote_Click" />--%>
                            <a id="lnkbtn" runat="server" style="font-family: Verdana; font-size: 12px; display: none;">
                                Download Quote</a>
                        </td>
                        <td>
                            <asp:Button ID="btnBook" runat="server" Text="Book" Width="150px" Style="display: none;"
                                OnClick="btnBook_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Booking" Width="150px" Style="display: none;"
                                OnClick="btnUpdateBook_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtncancelBook" runat="server" Text="Request For Cancellation Booking"
                                Width="250px" Style="display: none;" OnClick="BtncancelBook_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnAmmend" runat="server" Text="Request For Amendment" Width="150px"
                                Style="display: none;" OnClick="btnAmmend_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Reconfirm" Visible="false" OnClick="Button2_onclick"
                                OnClientClick="javascript:return confirm('Are you sure you want to Reconfirm this Hotel Booking?');" />
                        </td>
                        <td>
                            <asp:Button Text="Back" runat="server" ID="btnback" Width="50px" OnClick="btnBack_Click"
                                Visible="false" />
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
        <%--Loaders For HOTELS--%>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upHotelPty">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage2" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upHotelPhuket">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage3" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upHotelkBV">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage4" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upHotelUsm">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage5" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress6" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uphotelCnx">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage6" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress7" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uphotelEtc">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage7" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress8" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upHotelSS">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage8" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress9" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uphotelsps">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage9" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress10" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uphotelZZ">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage10" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress11" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uphotelBkk">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage11" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%--LOADERS FOR TRANSFER PACKAGES--%>
        <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="uptransferPackage">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage12" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%--LOADERS FOR SGHT SEEING--%>
        <asp:UpdateProgress ID="UpdateProgress13" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSSPty">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage13" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress14" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_phuket">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage14" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress15" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_Kbv">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage15" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress16" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_usm">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage16" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress17" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_cnx">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage17" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress18" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_Etc">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage18" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress19" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_s">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage19" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress20" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_sps">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage20" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress21" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSS_zz">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage21" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress22" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upSSbkk">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage22" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%--FORM :- 2 --%>
        <form id="form2" runat="server" visible="false">
        <div>
            <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                Height="8.5in" Width="14in">
            </rsweb:ReportViewer>
        </div>
        </form>
    </div>
</asp:Content>
