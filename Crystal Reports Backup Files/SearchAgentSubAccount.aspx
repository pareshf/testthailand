<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SearchAgentSubAccount.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.SearchAgentSubAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
 <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<style>
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
            font-weight: normal;
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

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <div>
        <asp:Label runat="server" Text="Search SubAgent" ID="headlbl" Width="200px" Font-Bold="true"
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
                                <asp:Label ID="Label4" runat="server" Text="Name" CssClass="lblstyle"></asp:Label>
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
                         <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Email" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </asp:Panel>
            <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnRowCommand="GV_Result_RowCommand" AutoGenerateColumns="False"
                                SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10" >
                                <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Approved" Text="Edit" />
                                    <asp:BoundField DataField="CUST_REL_SRNO" HeaderText="Agent No"/>
                                    <asp:BoundField DataField="AGENT_NAME" HeaderText="Agent Name" />
                                    <asp:BoundField DataField="CUST_REL_MOBILE" HeaderText="Mobile" />
                                    <asp:BoundField DataField="CUST_REL_EMAIL" HeaderText="Email" />
                                    <asp:BoundField DataField="USER_STATUS_NAME" HeaderText="Status" />
                                    <asp:BoundField DataField="PARENT_AGENT" HeaderText="Parent Agent" />
                                    <asp:BoundField DataField="DESIGNATION_DESC" HeaderText="Designation" />
                                    <asp:BoundField DataField="CUST_REL_PHONE" HeaderText="Phone" />
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
