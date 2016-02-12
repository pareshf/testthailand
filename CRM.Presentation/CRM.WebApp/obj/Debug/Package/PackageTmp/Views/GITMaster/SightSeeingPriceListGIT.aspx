<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SightSeeingPriceListGIT.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.SightSeeingPriceListGIT" %>

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
        <asp:Label runat="server" Text="GIT SightSeeing Price List " ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
       
        <asp:UpdatePanel ID="UpSightSeeingPriceListGit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                    <table width="600px" border="1" style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">

                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label25" runat="server" Text="Supplier" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSupplier" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ErrorMessage="Supplier is required." ControlToValidate="drpSupplier" 
                                    Display="Dynamic" ValidationGroup="Save" Font-Size="6px"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="City" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_Hotelcity" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="City name is required." ControlToValidate="drp_Hotelcity" 
                                    Display="Dynamic" ValidationGroup="Save" Font-Size="6px"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label1" runat="server" Text="Sight Seeing Package" CssClass="lblstyle"></asp:Label>&nbsp;<span
                        class="error">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSightPackage" runat="server" Width="250px"></asp:TextBox>

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ErrorMessage="Sight Seeing Package is required." ControlToValidate="txtSightPackage" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="Sight Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSightPalce" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ErrorMessage="Sight Name is required." ControlToValidate="drpSightPalce" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label26" runat="server" Text="Meal Applicable?" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpMealApplicable" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ErrorMessage="Meal Applicable is required." ControlToValidate="drpMealApplicable" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label27" runat="server" Text="Resturant Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpRestaurant" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                    ErrorMessage="Resturant Name is required." ControlToValidate="drpRestaurant" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label28" runat="server" Text="Meal Type" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpMealtype" runat="server" Width="255px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ErrorMessage="Meal Type is required." ControlToValidate="drpMealtype" 
                                    Display="Dynamic" ValidationGroup="Save" CssClass="validation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label29" runat="server" Text="Time 1" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTime1" runat="server" Width="255px">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label30" runat="server" Text="Time 2" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTime2" runat="server" Width="255px">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label31" runat="server" Text="Time 3" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTime3" runat="server" Width="255px">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label32" runat="server" Text="Time 4" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTime4" runat="server" Width="255px">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label33" runat="server" Text="Time 5" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTime5" runat="server" Width="255px">
                                </asp:DropDownList>
                                
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
                                <asp:Label ID="Label34" runat="server" Text="Adut Sic Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdultSic" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtAdultSic" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label35" runat="server" Text="Child Sic Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChildSic" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtChildSic" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label36" runat="server" Text="Adult PVT Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdultPvtRate" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtAdultPvtRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label37" runat="server" Text="Child PVT Rate" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChildPvtRate" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtChildPvtRate" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label38" runat="server" Text="SIC Rate Per Person" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSicRateperPerson" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtSicRateperPerson" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label39" runat="server" Text="PVT Rate Per Person" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChildRatePerPerson" runat="server" Width="250px"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" 
                                    ErrorMessage="Only digits are allowed." CssClass="validation" 
                                    ValidationExpression="^[0-9]+(.[0-9]{1,2})?$" ValidationGroup="Save" 
                                    ControlToValidate="txtChildRatePerPerson" Display="Dynamic"></asp:RegularExpressionValidator >
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Validity From Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
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
                                <asp:Label ID="Label3" runat="server" Text="Validity To Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
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
                                <asp:Label ID="Label16" runat="server" Text="Payment Terms" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpPaymentTerms" runat="server" Width="255px">
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
