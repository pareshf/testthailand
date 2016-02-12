<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="OnAccounts.aspx.cs" Inherits="CRM.WebApp.Views.MIS.OnAccounts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style type="text/css">
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
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="On Account Transaction Reports"></asp:Literal>
    </div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Account Name"></asp:Label>
            </td>
            <td>
                <%--<asp:DropDownList ID="DropDownList1" runat="server" Width="200px">
                </asp:DropDownList>--%>
                <telerik:RadComboBox ID="DropDownList1" runat="server" Width="269px" ShowDropDownOnTextboxClick="true"
                    AllowCustomText="false" ValidationGroup="valid">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="GL code is required"
                    ValidationGroup="valid" InitialValue="" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_fromdate" runat="server"></asp:TextBox>
                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" TargetControlID="txt_fromdate"
                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                </ajax:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_todate" runat="server"></asp:TextBox>
                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txt_todate"
                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                </ajax:TextBoxWatermarkExtender>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div>
        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnshow_Click" ValidationGroup="valid" /></div>
</asp:Content>
