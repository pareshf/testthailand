<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ChartsOfAccountSearch.aspx.cs" Inherits="CRM.WebApp.Views.AccountMaster.ChartsOfAccountSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
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
        <asp:Label runat="server" Text="Charts Of Account" ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server" Text="Account Id" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtaccountid" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="GL Code" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtglcode" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Account Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               <asp:TextBox ID="txtgldescription" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Under Group" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                
                    <telerik:radcombobox id="drpQuestion" runat="server" autopostback="True" markfirstmatch="true"
                                                        highlighttemplateditems="true" height="150px" width="350px" 
                                                        onitemdatabound="RadComboBox1_ItemDataBound">


                                                  <ItemTemplate>
                                                    <table>
                                                    <tr>
                                                    
                                                    <td style="width:200px"><%# DataBinder.Eval(Container.DataItem, "MAIN_GROUP")%> </td>
                                                    
                                                    <td style="width:150px"><%# DataBinder.Eval(Container.DataItem, "UNDER_GROUP")%></td>
                                                    </tr>
                                                    </table>
                                                     <%--<ul>
                                                            <li class="col1">
                                                                    <%# DataBinder.Eval(Container.DataItem, "MAIN_GROUP")%></li>
                                                    </ul>
                                                    <ul>
                                                             <li class="col2">
                                                                     <%# DataBinder.Eval(Container.DataItem, "UNDER_GROUP")%></li>
                                                                    
                                                     </ul>--%>
                                                     </ItemTemplate>
                                                 </telerik:radcombobox>

                    
            </td>
                            
                        </tr>
                        <tr id="tr1" runat="server" visible="false">
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="Side Code" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSidecode" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" 
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" Width="1000px" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10"
                                OnSelectedIndexChanging="GV_Result_SelectedIndexChanging">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="EDIT" />
                                    <asp:BoundField DataField="CHART_OF_ACCOUNTS_ID" HeaderText="Account ID." />
                                    <asp:BoundField DataField="GL_CODE" HeaderText="GL Code" />
                                    <asp:BoundField DataField="GL_DESCRIPTION" HeaderText="Account Name" />
                                    <asp:BoundField DataField="GROUP_NAME" HeaderText="Group Name" />
                                    <%--<asp:BoundField DataField="SIDE_CODE_NAME" HeaderText="Side Code" />--%>
                                    <asp:BoundField DataField="OP_BALANCE" HeaderText="Opening Balance" />
                                    <asp:BoundField DataField="OP_BAL_TYPE" HeaderText="Opening Balance Type" />
                                    <asp:BoundField DataField="OP_DATE" HeaderText="Opening Balance As on date" />
                                   <%-- <asp:BoundField DataField="CL_BALANCE_MONTH" HeaderText="Closing Balance Month" />--%>
                                   <%-- <asp:BoundField DataField="COMPANY_NAME" HeaderText="Company" />--%>
                                    <%--<asp:BoundField DataField="SUPPLIER_AGENT_ID" HeaderText="Supplier/Agent" />
                                    <asp:BoundField DataField="ACC_NAME" HeaderText="Company Account" />--%>
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
