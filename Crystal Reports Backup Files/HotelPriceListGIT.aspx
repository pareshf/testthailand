<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="HotelPriceListGIT.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.HotelPriceListGIT" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<style>
    
    
        .validation
        {
            font-family: Verdana;
            font-size: 12px;
        }
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: bold;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 12px;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight:normal;
        }
        .errorclass
        {
            font-family: Verdana;
            font-size: 12px;
            color: Red;
        }
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        div.RadGrid_Default .rgFilterRow td
        {
            background-color: #e5e5e5;
        }
        div.RadGrid_Default .rgHeader
        {
            background-color: #F3F3F3;
            background-position: 0 0;
            background-repeat: repeat-x !important;
            border-color: #E6E6E6 #E6E6E6 #CCCCCC;
            color: #636363;
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            height: 25px;
            line-height: 16px;
            text-align: left;
            text-decoration: none;
            text-indent: 0;
        }
        
        .RadMenu_Default
        {
            background-color: #fff;
            border: solid 0px #fff;
        }
        .RadMenu_Default UL.rmRootGroup
        {
            background-color: #fff;
            border: solid 0px #fff;
            padding: 2px;
        }
        .RadMenu rmLink
        {
            padding-left: 0px;
        }
        .RadMenu_Default .rmLink
        {
            color: #000;
            text-decoration: none;
            font-family: Verdana;
            font-size: 8pt;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText
        {
            border: solid 0px #fff;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem:hover
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink
        {
            color: #000;
            padding-top: 2px;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadPanelBar_Default .rpSlide
        {
            padding-left: 2px;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .radinput
        {
            width: 100%;
            border: 0px solid #c2c2c2;
        }
    </style>

 <div>
        <asp:Label runat="server" Text="GIT Hotel Price List " ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
       
        <asp:UpdatePanel ID="UpHotelPriceListGit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                    <table width="800px" border="1" style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="City" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_Hotelcity" runat="server" Width="255px" OnSelectedIndexChanged="drp_Hotelcity_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="City name is required." ControlToValidate="drp_Hotelcity" 
                                    Display="Dynamic" ValidationGroup="Save" Font-Size="6px"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Hotel Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_Hotel" runat="server" Width="255px" OnSelectedIndexChanged="drp_Hotel_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ErrorMessage="Hotel name is required." ControlToValidate="drp_Hotel" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="Room Type" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpRoomType" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ErrorMessage="Room type is required." ControlToValidate="drpRoomType" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAgent" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="From Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtfromdate" runat="server" Width="250px" 
                                     ontextchanged="txtfromdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertp1" runat="server" TargetControlID="txtfromdate" WatermarkText="dd/MM/yyyy"></ajax:TextBoxWatermarkExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ErrorMessage="From Date is required." ControlToValidate="txtfromdate" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="To Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txttodate" runat="server" Width="250px" 
                                    ontextchanged="txttodate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertp2" runat="server" TargetControlID="txttodate" WatermarkText="dd/MM/yyyy"></ajax:TextBoxWatermarkExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ErrorMessage="To Date is required." ControlToValidate="txttodate" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="Currency" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpCurrency" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label8" runat="server" Text="SurCharge" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurCharge" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label9" runat="server" Text="SurCharge Unit" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurChargeUnit" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label10" runat="server" Text="Single Room Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSingleRoomRate" runat="server" Width="250px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtSingleRoomRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label11" runat="server" Text="Double Room Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDoubleRoomRate" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtDoubleRoomRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label12" runat="server" Text="Tripple Room Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTrippleRoomRate" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtTrippleRoomRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label13" runat="server" Text="Extra Adult Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExtraAdultRate" runat="server" Width="250px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtExtraAdultRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label14" runat="server" Text="Extra CWB Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExtraCWBRate" runat="server" Width="250px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtExtraCWBRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                          <tr>
                            <td width="180px">
                                <asp:Label ID="Label15" runat="server" Text="Extra CNB Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExtraCNBRate" runat="server" Width="250px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtExtraCNBRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label16" runat="server" Text="Payment Terms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpPaymentTerms" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label17" runat="server" Text="Defult Quote?" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpIsDefaultQuote" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label18" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpStatus" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label19" runat="server" Text="A Margin In Amount" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtA_MarginAmount" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtA_MarginAmount" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label20" runat="server" Text="A Plus Margin In Amount" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtA_plusAmount" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtA_plusAmount" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label21" runat="server" Text="A Plus Plus Margin In Amount" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtA_plus_plusAmount" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtA_plus_plusAmount" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label22" runat="server" Text="A Margin Amount In[%]" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtAMarginAmountPer" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtAMarginAmountPer" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label24" runat="server" Text="A Plus Margin Amount In[%]" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtPlusMarginAmountPer" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtPlusMarginAmountPer" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label23" runat="server" Text="A Plus Plus Margin Amount In[%]" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtAPlusPlusMarginAmountPer" runat="server" Width="250px"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtAPlusPlusMarginAmountPer" Display="Dynamic"></asp:RegularExpressionValidator >

                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                 <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" ValidationGroup="Save"/>
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
