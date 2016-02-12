<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AgentSubAccount.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.AgentSubAccount" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
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
         .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight:normal;
        }
    </style>

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Agent Sub Account"></asp:Literal>
    </div>
    <br />
    <br />
    <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        class="pageTitle">
        <ContentTemplate>
            <table width="700px" border="1" style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                <tr style="display: none">
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Agent id" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="CUST_UNQ_ID" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                            class="error">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpTitle" runat="server" AutoPostBack="true" Width="65px">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:TextBox ID="txtClientname" runat="server" Width="80px" Text="First Name"></asp:TextBox>
                        <asp:TextBox ID="txtClientlastname" runat="server" Width="80px" Text="Last Name"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="reqAgentName" runat="server" ControlToValidate="txtClientname"
                            ErrorMessage="Agent name is required" ValidationGroup="valid" CssClass="lblstyle"
                            Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Mobile" CssClass="lblstyle"></asp:Label>&nbsp;<span
                            class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="CUST_REL_MOBILE" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqMobile" runat="server" ControlToValidate="CUST_REL_MOBILE"
                            ErrorMessage="Mobile No is required." ValidationGroup="valid" CssClass="lblstyle"
                            Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Email (User Name)" CssClass="lblstyle"></asp:Label>&nbsp;<span
                            class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="CUST_REL_EMAIL" runat="server" Width="250px"></asp:TextBox>
                        <asp:Label ID="lblerror" runat="server" Text="Email already exist. Enter another E-mail."
                            ForeColor="Red" Visible="false" CssClass="lblstyle"></asp:Label>
                        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="CUST_REL_EMAIL"
                            ErrorMessage="Email is required." ValidationGroup="valid" CssClass="lblstyle"
                            Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Password" CssClass="lblstyle"></asp:Label>&nbsp;<span
                            class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="PASSWORD" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="PASSWORD"
                            ErrorMessage="Password is required." ValidationGroup="valid" CssClass="lblstyle"
                            Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Phone No" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtphone" runat="server" Width="250px"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Designation" CssClass="lblstyle"></asp:Label>
                        &nbsp;<span class="error">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpDesignation" runat="server" AutoPostBack="true" Width="255px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqDesignation" runat="server" ControlToValidate="drpDesignation"
                            ErrorMessage="Designation is required." ValidationGroup="valid" InitialValue="0"
                            CssClass="lblstyle" Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Alternate Email" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpuserstatus" runat="server" AutoPostBack="true" Width="255px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <div>
            <asp:Button ID="Register" runat="server" OnClick="register_onclick" Width="100px"
                            Text="Register" ValidationGroup="valid"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
