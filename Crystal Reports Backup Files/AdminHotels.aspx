<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AdminHotels.aspx.cs" Inherits="CRM.WebApp.Views.FIT.AdminHotels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Src="~/Views/FIT/MulticheckDropdown.ascx" TagName="DropDownControl"
    TagPrefix="uc1" %>
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
    </style>
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);


    </script>
    <div style="position: relative">
        <asp:Label ID="Label55" runat="server" Text="FIT Package Details" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <%--<table>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="FIT Package Type:" CssClass="headlabel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_select_package" runat="server" CssClass="headlabel"></asp:Label>
                </td>
            </tr>
        </table>--%>
        <br />
        <%--------------------------------------------------------------------------------------HOTELS---------------------------------------------------------------------------------------------%>
        <asp:Label ID="lblhotel" runat="server" Text="Hotels" class="pageTitle" Width="100px"
            Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
        <br />
        <asp:Label ID="Label28" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <asp:Label ID="Label107" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <asp:Label ID="Label108" runat="server" Text="" class="pageTitle" Visible="false"
            Style="font-weight: normal; font-size: 14px; font-family: Verdana; color: Red"></asp:Label>
        <br />
        <%---1--------------------PATTAYA HOTELS------------------------------%>
        <div id="ptydiv" runat="server" visible="false" class="pageTitle">
            <asp:UpdatePanel ID="upHotelPty" runat="server" Visible="false" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblpattaya" runat="server" Text="Pattaya" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
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
                                <asp:Label ID="Label54" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td id="ptytd" style="width: 50px;">
                                <asp:Label ID="Labelpty" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusPTY" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeespty" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="TdStarPattaya">
                                <asp:Label ID="Label97" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="TdLocPattaya">
                                <asp:Label ID="Label109" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" TargetControlID="txtpty_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderpty1" runat="server" TargetControlID="txtpty_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtpty_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtpty_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtpty_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderpty11" runat="server" TargetControlID="txtpty_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblpty_Noof_Romms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td1" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </td>
                            <td id="ptyredtd" style="width: 50px;">
                                <asp:RadioButton ID="Rbtnconfirmpty" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmpty"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusPty" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeespty" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="lblStarPattya1">
                                <telerik:RadRating ID="RadRating1" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPattaya1">
                                <asp:Label ID="lblLocPattaya" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                        class="error">*</span>
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
                                <asp:Label ID="Label29" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="phutd">
                                <asp:Label ID="Labelphu" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusPHU" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeesphu" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td21">
                                <asp:Label ID="Label98" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPhuket">
                                <asp:Label ID="Label110" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender23" runat="server" TargetControlID="txtphu_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderPhuket1" runat="server" TargetControlID="txtphu_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtphu_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtphu_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender24" runat="server" TargetControlID="txtphu_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderPhuket11" runat="server" TargetControlID="txtphu_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtphu_noofrooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td6" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox3" runat="server" />
                            </td>
                            <td style="width: 50px" id="phuredtd">
                                <asp:RadioButton ID="rbtnconfirmphu" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmphu"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusPhu" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeesphu" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td22">
                                <telerik:RadRating ID="RadRating2" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocPhuket1">
                                <asp:Label ID="lblLocPhuket" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label1" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td7">
                                <asp:Label ID="Label68" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="kbvtd">
                                <asp:Label ID="Labelkbv" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusKBV" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeeskbv" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td23">
                                <asp:Label ID="Label99" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocKrabi">
                                <asp:Label ID="Label111" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                    OnTextChanged="txtkbv_CheckIn_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender25" runat="server" TargetControlID="txtkbv_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderKrabi1" runat="server" TargetControlID="txtkbv_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtkbv_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtkbv_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender26" runat="server" TargetControlID="txtkbv_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderKrabi11" runat="server" TargetControlID="txtkbv_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblkbv_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td8" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox4" runat="server" />
                            </td>
                            <td style="width: 50px" id="kbvredtd">
                                <asp:RadioButton ID="rbtnconfirmkbv" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmpkbv"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusKbv" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeeskbv" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td24">
                                <telerik:RadRating ID="RadRating3" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocKrabi1">
                                <asp:Label ID="lblLocKrabi" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label30" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td9">
                                <asp:Label ID="Label75" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="usmtd">
                                <asp:Label ID="Labelusm" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusUSM" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeesusm" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td25">
                                <asp:Label ID="Label100" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocUsm">
                                <asp:Label ID="Label112" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender27" runat="server" TargetControlID="txtusm_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderSamui1" runat="server" TargetControlID="txtusm_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtusm_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtusm_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender28" runat="server" TargetControlID="txtusm_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderSamui11" runat="server" TargetControlID="txtusm_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtusm_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td10" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox5" runat="server" />
                            </td>
                            <td style="width: 50px" id="usmredtd">
                                <asp:RadioButton ID="rbtnconfirmusm" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmusm"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusKbvUsm" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeesusm" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td26">
                                <telerik:RadRating ID="RadRating4" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocUsm1">
                                <asp:Label ID="lblLocUsm" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label33" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td11">
                                <asp:Label ID="Label77" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="cnxtd">
                                <asp:Label ID="Labelcnx" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusCNX" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeescnx" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td27">
                                <asp:Label ID="Label101" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocCnx">
                                <asp:Label ID="Label113" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender29" runat="server" TargetControlID="txtcnx_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderChiangmai1" runat="server" TargetControlID="txtcnx_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtcnx_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtcnx_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender30" runat="server" TargetControlID="txtcnx_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderChiangmai11" runat="server" TargetControlID="txtcnx_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtcnx_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td12" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox6" runat="server" />
                            </td>
                            <td style="width: 50px" id="cnxredtd">
                                <asp:RadioButton ID="rbtnconfirmcnx" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmcnx"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusCnx" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeescnx" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td28">
                                <telerik:RadRating ID="RadRating5" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocCnx1">
                                <asp:Label ID="lblLocCnx" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label34" runat="server" Text="No of Rooms" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 150px; display: none" id="Td13">
                                <asp:Label ID="Label83" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="etctd">
                                <asp:Label ID="Labeletc" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusETC" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeesetc" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td29">
                                <asp:Label ID="Label102" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocEtc">
                                <asp:Label ID="Label114" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender31" runat="server" TargetControlID="txtetc_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderHuahin1" runat="server" TargetControlID="txtetc_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtetc_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtetc_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender32" runat="server" TargetControlID="txtetc_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderHuahin11" runat="server" TargetControlID="txtetc_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtetc_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td14" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox7" runat="server" />
                            </td>
                            <td style="width: 50px" id="etcredtd">
                                <asp:RadioButton ID="rbtnconfirmetc" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmetc"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusEtc" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeesetc" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td30">
                                <telerik:RadRating ID="RadRating6" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocEtc1">
                                <asp:Label ID="lblLocEtc" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label84" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="sstd">
                                <asp:Label ID="Labelss" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusSS" runat="server" Text="Status" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeesss" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td31">
                                <asp:Label ID="Label103" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSs">
                                <asp:Label ID="Label115" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender33" runat="server" TargetControlID="txtss_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderKanchanburi1" runat="server" TargetControlID="txtss_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtss_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtss_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender34" runat="server" TargetControlID="txtss_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderKanchanburi11" runat="server" TargetControlID="txtss_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtss_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td16" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox8" runat="server" />
                            </td>
                            <td style="width: 50px" id="ssredtd">
                                <asp:RadioButton ID="rbtnconfirmss" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmss"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusSs" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeesss" runat="server" CssClass="textboxstyle" Width="50px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td32">
                                <telerik:RadRating ID="RadRating7" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSs1">
                                <asp:Label ID="lblLocSs" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---8--------------------SPS (Chingrai)HOTELS------------------------------%>
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
                                <asp:Label ID="Label90" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="spstd">
                                <asp:Label ID="Labelsps" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusSPS" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeessps" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td33">
                                <asp:Label ID="Label104" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSps">
                                <asp:Label ID="Label116" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender35" runat="server" TargetControlID="txtsps_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderChiangrai1" runat="server" TargetControlID="txtsps_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtsps_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtsps_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender36" runat="server" TargetControlID="txtsps_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderChingrai11" runat="server" TargetControlID="txtsps_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtsps_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td18" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox9" runat="server" />
                            </td>
                            <td style="width: 50px" id="spsredtd">
                                <asp:RadioButton ID="rbtnconfirsps" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmsps"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusSps" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeessps" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td34">
                                <telerik:RadRating ID="RadRating8" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocSps1">
                                <asp:Label ID="lblLocSps" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label91" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="zztd">
                                <asp:Label ID="Labelzz" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusZZ" runat="server" Text="Status" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeeszz" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td35">
                                <asp:Label ID="Label105" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocZz">
                                <asp:Label ID="Label117" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender37" runat="server" TargetControlID="txtzz_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderzz1" runat="server" TargetControlID="txtzz_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtzz_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtzz_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender38" runat="server" TargetControlID="txtzz_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderzz11" runat="server" TargetControlID="txtzz_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="txtzz_NoofRooms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td20" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox10" runat="server" />
                            </td>
                            <td style="width: 50px" id="zzredtd">
                                <asp:RadioButton ID="rbtnconfirmzz" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmzz"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <%--<asp:Label ID="" runat="server" ></asp:Label>--%>
                                <asp:Label ID="lblStatusZz" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeeszz" runat="server" CssClass="textboxstyle" Width="50px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td36">
                                <telerik:RadRating ID="RadRating9" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocZz1">
                                <asp:Label ID="lblLoczz" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <asp:Label ID="Label61" runat="server" Text="Add to Cart" CssClass="headlabel" Width="70px"></asp:Label>
                            </td>
                            <td style="width: 50px" id="bkktd">
                                <asp:Label ID="lblselect" runat="server" Text="Select" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="LabelstatusBKK" runat="server" Text="Status" CssClass="headlabel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblfeesbkk" runat="server" Text="Fees" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="Td37">
                                <asp:Label ID="Label106" runat="server" Text="Star" CssClass="headlabel"></asp:Label>
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocBkk">
                                <asp:Label ID="Label69" runat="server" Text="Location" CssClass="headlabel"></asp:Label>
                            </td>
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
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender39" runat="server" TargetControlID="txtbkk_CheckIn"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderBangkok1" runat="server" TargetControlID="txtbkk_CheckIn"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtbkk_CheckOut" runat="server" CssClass="textboxstyle" Width="100px"
                                    OnTextChanged="txtbkk_CheckOut_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender40" runat="server" TargetControlID="txtbkk_CheckOut"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderBangkok11" runat="server" TargetControlID="txtbkk_CheckOut"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblbkkNoof_Romms" runat="server" Text="No of Rooms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td id="Td2" style="width: 50px; display: none">
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </td>
                            <td id="bkkredtd" style="width: 50px">
                                <asp:RadioButton ID="rbtnbkkconfirm" runat="server" GroupName="HotelRdoButton" OnCheckedChanged="CheckChangedforconfirmbkk"
                                    Visible="false" AutoPostBack="true" />
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblStatusBkk" runat="server" CssClass="headlabel" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:TextBox ID="txtfeesbkk" runat="server" CssClass="textboxstyle" Width="50px"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 100px; display: none" runat="server" id="Td38">
                                <telerik:RadRating ID="RadRating10" runat="server" ItemCount="5" SelectionMode="Continuous"
                                    Orientation="Horizontal" IsEnabled="False" />
                            </td>
                            <td style="width: 50px; display: none" runat="server" id="tdLocBkk1">
                                <asp:Label ID="lblLocBkk" runat="server" CssClass="headlabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table id="Table1" runat="server">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Confirm" Visible="false" OnClick="Button1_onclick" />
                            <asp:Button ID="Button3" runat="server" Text="Wait List" Visible="false" OnClick="Button3_onclick" />
                            <asp:Button ID="Button4" runat="server" Text="Change Of Hotel" Visible="false" />
                        </td>
                    </tr>
                </table>
                <ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
                    PopupControlID="pnlCompanyRoleSelection" TargetControlID="Button1" Drag="true"
                    PopupDragHandleControlID="pnlCompanyRoleSelectionHeader" CancelControlID="ImageButton1">
                </ajax:ModalPopupExtender>
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
                                        <ajax:CalendarExtender ID="CalendarExtenderReconfirmation" runat="server" TargetControlID="TextBox1"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelerror" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                        <asp:Label ID="label76" runat="server" Text="dd/mm/yyyy"></asp:Label>
                                        <ajax:CalendarExtender ID="CalendarExtenderPayment" runat="server" TargetControlID="TextBox3"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" />
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
                                                    AutoPostBack="true" Width="100px"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertp" runat="server" TargetControlID="txtTPdate"
                                                    WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderTransfer" runat="server" TargetControlID="txtTPdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label25" runat="server" Text="Pickup Time" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="tpdrp_time" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label69" runat="server" Text="Payment Date" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtduedate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertpdate" runat="server"
                                                    TargetControlID="txtduedate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderDueDate" runat="server" TargetControlID="txtduedate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbltransferpac" runat="server" Text="Fees" CssClass="headlabel"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttrans" runat="server" CssClass="textboxstyle" Visible="false"
                                                    Width="50px"></asp:TextBox>
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
                    <%-- <asp:Button ID="btnArrivalDate" Text="Change Arrival Date" OnClick="btnArrivalDate_Click"
                        runat="server" />
                        <asp:Button ID="btnDepartureDate" Text="Change Departure Date" OnClick="btnDepartureDate_Click"
                        runat="server" />--%>
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
                                                    OnTextChanged="txtSS_Ptydate_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSS_Ptydate"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSS_Ptydate"
                                                    Format="dd/MM/yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_pty_time_selectedindexchanged" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtptyssdate" runat="server" CssClass="textboxstyle" Visible="false"
                                                    Width="70px"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderptydate" runat="server"
                                                    TargetControlID="txtptyssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderPtyPaymentDate" runat="server" TargetControlID="txtptyssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="phu_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssptyfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpty_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    OnTextChanged="txtSS_phuketdate_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSS_phuketdate"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSS_phuketdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_phu_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtphussdate" runat="server" CssClass="textboxstyle" Width="80px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtphussdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderPhuPaymentDate" runat="server" TargetControlID="txtphussdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="phu_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssphufees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPHU_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    OnTextChanged="txtSS_kbv_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtSS_kbv_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSS_kbv_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_kbv_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_kbv" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtkbvssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtkbvssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderKbvPaymentDate" runat="server" TargetControlID="txtkbvssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="phu_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsskbvfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKBV_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL4" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    OnTextChanged="txtSS_usm_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSS_usm_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSS_usm_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_usm_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_kbv" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtusmssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtusmssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderUsmPaymentDate" runat="server" TargetControlID="txtusmssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="usm_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssusmfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUSM_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL5" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    OnTextChanged="txtSS_cnx_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSS_cnx_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtSS_cnx_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_cnx_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_cnx" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcnxssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtcnxssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderCnxPaymentDate" runat="server" TargetControlID="txtcnxssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="cnx_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsscnxfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCNX_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL6" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---6----------------------------ETC SIGHT SEEING--------------------------------------%>
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
                                                    OnTextChanged="txtSS_etc_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtSS_etc_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtSS_etc_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_etc_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_etc" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtetcssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtetcssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderEtcPaymentDate" runat="server" TargetControlID="txtetcssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="etc_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssetcfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblETC_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL7" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---7----------------------------SS SIGHT SEEING--------------------------------------%>
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
                                                    OnTextChanged="txtSS_s_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtSS_s_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtSS_s_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_ss_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_etc" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtetcssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderSsPaymentDate" runat="server" TargetControlID="txtssssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="ss_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssssfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSS_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL8" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <%---8----------------------------SPS SIGHT SEEING--------------------------------------%>
        <div id="Div9" runat="server" visible="false" class="pageTitle">
            <asp:Label ID="lblSS_sps" runat="server" Text="Chingrai" CssClass="headlabel" Visible="false"></asp:Label><br />
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
                                                    OnTextChanged="txtSS_sps_date_TextChanged" AutoPostBack="true"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                        ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtSS_sps_date"
                                                        WatermarkText="dd/MM/yyyy">
                                                    </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtSS_sps_date"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_sps_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_etc" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtspsssdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtspsssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderSpsPaymentDate" runat="server" TargetControlID="txtspsssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="sps_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssspsfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSPS_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL9" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <asp:TextBox ID="txtSS_zz_date" runat="server" CssClass="textboxstyle" Width="80px"></asp:TextBox><ajax:TextBoxWatermarkExtender
                                                    ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtSS_zz_date"
                                                    WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_zz_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date_etc" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtphuzzdate" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                                    TargetControlID="txtphuzzdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderZzPaymentDate" runat="server" TargetControlID="txtphuzzdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="sps_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsszzfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblZZ_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL10" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtSS_Bkkdate"
                                                    WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtSS_Bkkdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_ss_time" runat="server" OnSelectedIndexChanged="drp_bkk_time_selectedindexchanged"
                                                    AutoPostBack="true" />
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
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="Due_Date1" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtbkkssdate" runat="server" CssClass="textboxstyle" Visible="false"
                                                    Width="70px"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderssbkk" runat="server"
                                                    TargetControlID="txtbkkssdate" WatermarkText="dd/MM/yyyy">
                                                </ajax:TextBoxWatermarkExtender>
                                                <ajax:CalendarExtender ID="CalendarExtenderBkkPaymentDate" runat="server" TargetControlID="txtbkkssdate"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="phu_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtssbkkfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBKK_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                    CssClass="headlabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Remaining Meal Date">
                                            <ItemTemplate>
                                                <uc1:DropDownControl ID="DDL1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <asp:Label ID="lbladdiotional" runat="server" Text="Additional Services" class="pageTitle"
            Width="200px" Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
        <br />
        <br />
        <div>
            <asp:UpdatePanel ID="UpdateAdditional" runat="server" UpdateMode="Conditional" class="pageTitle">
                <ContentTemplate>
                    <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                        cellpadding="5" width="800px">
                        <tr id="gridheading" runat="server" style="background-color: #f3f3f3">
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Sr." CssClass="headlabel" Width="10px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Services" CssClass="headlabel" Width="60px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="Select Supplier" CssClass="headlabel"
                                    Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label50" runat="server" Text="SIC/PVT" CssClass="headlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label47" runat="server" Text="Date" CssClass="headlabel" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="No of pax" CssClass="headlabel" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Net Price(THB)" CssClass="headlabel"
                                    Width="50px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label48" runat="server" Text="Sell Price(THB)" CssClass="headlabel"
                                    Width="50px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label38" runat="server" Text="From" CssClass="headlabel" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label49" runat="server" Text="To" CssClass="headlabel" Width="40px"></asp:Label>
                            </td>
                            <td id="td_duedate" runat="server" style="display: none;">
                                <asp:Label ID="Label51" runat="server" Text="Due Date" CssClass="headlabel" Width="40px"></asp:Label>
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 1 -------------------------------------------------------------%>
                        <tr id="row1" runat="server">
                            <td>
                                <asp:Label ID="Label39" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList11" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox42" runat="server" AutoPostBack="true" OnTextChanged="TextBox42_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderphudate" runat="server"
                                    TargetControlID="TextBox42" WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer1" runat="server" TargetControlID="TextBox42"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox44" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox45" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox46" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox47" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate1" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender20" runat="server" TargetControlID="txt_duedate1"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate1" runat="server" TargetControlID="txt_duedate1"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 2 ------------------------------------------------------------%>
                        <tr id="row2" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label25" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList12" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList21" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" AutoPostBack="true" OnTextChanged="TextBox7_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="TextBox7"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer2" runat="server" TargetControlID="TextBox7"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox48" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox49" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox50" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate2" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender41" runat="server" TargetControlID="txt_duedate2"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate2" runat="server" TargetControlID="txt_duedate2"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 3 ---------------------------------------------------------%>
                        <tr id="row3" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label26" runat="server" Text="3" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList13" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList22" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" AutoPostBack="true" OnTextChanged="TextBox9_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="TextBox9"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer3" runat="server" TargetControlID="TextBox9"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox51" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox52" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox53" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate3" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender42" runat="server" TargetControlID="txt_duedate3"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate3" runat="server" TargetControlID="txt_duedate3"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 4 ---------------------------------------------------%>
                        <tr id="row4" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label40" runat="server" Text="4" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList14" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList23" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" AutoPostBack="true" OnTextChanged="TextBox11_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="TextBox11"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer4" runat="server" TargetControlID="TextBox11"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox54" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox55" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox56" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate4" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender43" runat="server" TargetControlID="txt_duedate4"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate4" runat="server" TargetControlID="txt_duedate4"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 5 ----------------------------------------------------------------%>
                        <tr id="row5" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="5" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList15" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList24" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" AutoPostBack="true" OnTextChanged="TextBox13_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="TextBox13"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer5" runat="server" TargetControlID="TextBox13"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox57" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox58" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox59" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate5" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender44" runat="server" TargetControlID="txt_duedate5"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate5" runat="server" TargetControlID="txt_duedate5"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 6 ----------------------------------------------------------%>
                        <tr id="row6" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label42" runat="server" Text="6" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList16" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList25" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server" AutoPostBack="true" OnTextChanged="TextBox15_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" TargetControlID="TextBox15"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer6" runat="server" TargetControlID="TextBox15"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox60" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox61" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox62" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate6" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender45" runat="server" TargetControlID="txt_duedate6"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate6" runat="server" TargetControlID="txt_duedate6"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 7------------------------------------------------------------%>
                        <tr id="row7" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label43" runat="server" Text="7" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList17" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList26" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server" AutoPostBack="true" OnTextChanged="TextBox17_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender16" runat="server" TargetControlID="TextBox17"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer7" runat="server" TargetControlID="TextBox17"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox63" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox64" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox65" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate7" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender46" runat="server" TargetControlID="txt_duedate7"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate7" runat="server" TargetControlID="txt_duedate7"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 8------------------------------------------------------------%>
                        <tr id="row8" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label44" runat="server" Text="8" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList18" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList27" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server" AutoPostBack="true" OnTextChanged="TextBox19_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender17" runat="server" TargetControlID="TextBox19"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer8" runat="server" TargetControlID="TextBox19"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox66" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox67" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox68" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate8" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender47" runat="server" TargetControlID="txt_duedate8"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate8" runat="server" TargetControlID="txt_duedate8"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 9------------------------------------------------------------%>
                        <tr id="row9" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label45" runat="server" Text="9" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList19" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList28" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox21" runat="server" AutoPostBack="true" OnTextChanged="TextBox21_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender18" runat="server" TargetControlID="TextBox21"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer9" runat="server" TargetControlID="TextBox21"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox39" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox69" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox70" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox71" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate9" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender48" runat="server" TargetControlID="txt_duedate9"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate9" runat="server" TargetControlID="txt_duedate9"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                        <%------------------------------------------- ROW 10------------------------------------------------------------%>
                        <tr id="row10" runat="server" style="display: none">
                            <td>
                                <asp:Label ID="Label46" runat="server" Text="10" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList20" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList29" runat="server">
                                    <asp:ListItem>SIC</asp:ListItem>
                                    <asp:ListItem>PVT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox23" runat="server" AutoPostBack="true" OnTextChanged="TextBox23_TextChanged"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" TargetControlID="TextBox23"
                                    WatermarkText="dd/MM/yyyy">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSer10" runat="server" TargetControlID="TextBox23"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox40" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox72" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox73" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox74" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_duedate10" runat="server" Visible="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender49" runat="server" TargetControlID="txt_duedate10"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                                <ajax:CalendarExtender ID="CalendarExtenderAdSerDueDate10" runat="server" TargetControlID="txt_duedate10"
                                    Format="dd/MM/yyyy" PopupButtonID="Image1" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnadd2" runat="server" Text="ADD" OnClick="btnadd2_Click" Width="100px"
                        CssClass="BtnStyle" />
                    <asp:Button ID="btnremove2" runat="server" Text="Remove" OnClick="btnremove2_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd3" runat="server" Text="ADD" OnClick="btnadd3_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove3" runat="server" Text="Remove" OnClick="btnremove3_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd4" runat="server" Text="ADD" OnClick="btnadd4_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove4" runat="server" Text="Remove" OnClick="btnremove4_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd5" runat="server" Text="ADD" OnClick="btnadd5_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove5" runat="server" Text="Remove" OnClick="btnremove5_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd6" runat="server" Text="ADD" OnClick="btnadd6_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove6" runat="server" Text="Remove" OnClick="btnremove6_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd7" runat="server" Text="ADD" OnClick="btnadd7_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove7" runat="server" Text="Remove" OnClick="btnremove7_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd8" runat="server" Text="ADD" OnClick="btnadd8_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove8" runat="server" Text="Remove" OnClick="btnremove8_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd9" runat="server" Text="ADD" OnClick="btnadd9_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove9" runat="server" Text="Remove" OnClick="btnremove9_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                    <asp:Button ID="btnadd10" runat="server" Text="ADD" OnClick="btnadd10_Click" Style="display: none"
                        Width="100px" CssClass="BtnStyle" />
                    <asp:Button ID="btnremove10" runat="server" Text="Remove" OnClick="btnremove10_Click"
                        Width="100px" Style="display: none" CssClass="BtnStyle" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" class="pageTitle">
                <ContentTemplate>
                    <table id="cc" runat="server" style="display: none">
                        <tr>
                            <td>
                                <asp:Label ID="Label70" runat="server" Text="Carbon Copy To" CssClass="headlabel"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="textboxstyle" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- <div style="margin: 10px;display:none">
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
                            <asp:Button ID="Buttonsend" runat="server" Text="Send Quote to Agent" Width="150px"
                                Style="display: none;" OnClick="Buttonsend_Click" />
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
                            <asp:Button ID="Button8" runat="server" Text="Confirm Sightseeing And TransferPackage"
                                Width="250px" Style="display: none;" OnClick="btnpaymentdate_click" />
                        </td>
                        <td>
                            <asp:Button ID="Button9" runat="server" Text="Confirm Additional Services" Width="250px"
                                Style="display: none;" OnClick="Button9_click" />
                        </td>
                        <td>
                            <asp:Button ID="Button5" runat="server" Text="Approve Cancellation" Width="150px"
                                Style="display: none;" OnClick="btnapprovedcancellation_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button7" runat="server" Text="Disapprove Cancellation" Width="150px"
                                Style="display: none;" OnClick="btndisapprovedcancellation_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnapproved" runat="server" Text="Approve" Width="150px" Style="display: none;"
                                OnClick="btnapproved_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btndisapproved" runat="server" Text="Disapprove" Width="150px" Style="display: none;"
                                OnClick="btndisapproved_Click" />
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
        <%--LOADERS FOR ENTERD TO BE RECONFIRMD DATE FOR HOTELS--%>
        <asp:UpdateProgress ID="UpdateProgress23" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Updateconfirm">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage44" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
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
