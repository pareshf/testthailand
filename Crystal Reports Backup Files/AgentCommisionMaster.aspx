<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AgentCommisionMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.AgentCommisionMaster" %>

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
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/AgentCommisionMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                AgentTableView = $find("<%= radgridAgentcommisionmaster.ClientID %>").get_masterTableView();
                AgentCommandName = "Load";
                CRM.WebApp.webservice.AgentCommisionMaster.GetAgentcommision(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentTypeName);


            }
            function deleteCurrent() {
                var table = $find("<%= radgridAgentcommisionmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridAgentcommisionmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridAgentcommisionmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.AgentCommisionMaster.delAgent(AGENT_COMMISION_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewAgentCommision(sender, args) {
                debugger;
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[1].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.AGENT_COMMISION_ID;
            
                ary[1] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMISION_PERCENTAGE_LIMIT").value;
                ary[2] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
                ary[3] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("PRODUCT_DESC").value;
                ary[4] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("PRODUCT_TYPE_NAME").value;
                for (i = 0; i < 5; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                } 
                try {
                    CRM.WebApp.webservice.AgentCommisionMaster.InsertUpdateAgent(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.AgentCommisionMaster.GetAgentcommision(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentTypeName);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Agent Commision Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var Agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridAgentcommisionmaster_ctl00_ctl" + i + "_AGENT_NAME").autocomplete(Agent);
                $("#ctl00_cphPageContent_radgridAgentcommisionmaster_ctl00_ctl" + i + "_PRODUCT_TYPE_NAME").autocomplete(Producttype);
                $("#ctl00_cphPageContent_radgridAgentcommisionmaster_ctl00_ctl" + i + "_PRODUCT_DESC").autocomplete(productname);
            }
            {
                var Producttype = "../../webservice/autocomplete.ashx?key=GET_PRODUCT_TYPE_FOR_AGENT_MASTER_AUTOSEARCH";
                for (i = 0; i < 55; i++) {
                    if (i < 10)
                        i = '0' + i;
                    $("#ctl00_cphPageContent_radgridAgentcommisionmaster_ctl00_ctl" + i + "_PRODUCT_TYPE_NAME").autocomplete(Producttype);
                }
            }
            {
                var productname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_PRODUCT_AUTOSEARCH";
                for (i = 0; i < 55; i++) {
                    if (i < 10)
                        i = '0' + i;
                    $("#ctl00_cphPageContent_radgridAgentcommisionmaster_ctl00_ctl" + i + "_PRODUCT_DESC").autocomplete(productname);
                }
            }
        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Agent Commision ?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridAgentcommisionmaster" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="AGENT_COMMISION_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="AGENT_COMMISION_ID" DataField="AGENT_COMMISION_ID" HeaderText="AGENT_COMMISION_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_COMMISION_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    
                     <telerik:GridTemplateColumn SortExpression ="AGENT_NAME" DataField="AGENT_NAME" HeaderText="Agent Name">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                     <telerik:GridTemplateColumn SortExpression ="PRODUCT_ID" DataField="PRODUCT_ID" HeaderText="PRODUCT_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                     <telerik:GridTemplateColumn SortExpression ="PRODUCT_DESC" DataField="PRODUCT_DESC" HeaderText="Product Name">
                          <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                      <telerik:GridTemplateColumn SortExpression ="PRODUCT_TYPE_NAME" DataField="PRODUCT_TYPE_NAME" HeaderText="Product Type">
                          <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                                       
                     <telerik:GridTemplateColumn SortExpression ="COMMISION_PERCENTAGE_LIMIT" DataField="COMMISION_PERCENTAGE_LIMIT" HeaderText="Commision(%) Limit">
                          <ItemTemplate>
                            <asp:TextBox ID="COMMISION_PERCENTAGE_LIMIT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                                        
                    

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewAgentCommision(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridAgentcommisionmaster_Command" OnRowSelected="radgridAgentcommisionmaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
