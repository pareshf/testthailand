<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Agent.aspx.cs" Inherits="CRM.WebApp.Views.Administration.Agent" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cphIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
     <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cphPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
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
            <script type="text/javascript" src="../Shared/Javascripts/AgentMasterGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                AgentTableView = $find("<%= radgridAgent.ClientID %>").get_masterTableView();
                AgentCommandName = "Load";
                CRM.WebApp.webservice.AgentMaster.GetAgentName(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentGrid);
                

            }
            function deleteCurrent() {
                var table = $find("<%= radgridAgent.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridAgent.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridAgent.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.AgentMaster.delAgentName(AGENT_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewAgent(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                
                ary[0] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
                ary[2] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("EMAIL").value;
                ary[3] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("MOBILE").value;
                ary[4] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;
                ary[5] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("FAX").value;
                ary[6] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_TYPE").value;
                ary[7] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("REGION_SHORT_NAME").value;
                ary[8] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE1").value;
                ary[9] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE2").value;
                ary[10] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[11] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[12] = AgentTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.AGENT_ID;
                for (i = 0; i < 14; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.AgentMaster.InsertUpdateAgentName(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.AgentMaster.GetAgentName(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Agent Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var agenttype = "../../webservice/autocomplete.ashx?key=GET_AGENT_TYPE_NAME_AUTOSERCH";
            var regionname = "../../webservice/autocomplete.ashx?key=GET_REGION_TYPE_FOR_AUTOSERCH";

                        for (i = 0; i < 55; i++) {
                            if (i < 10)
                                i = '0' + i;

                            $("#ctl00_cphPageContent_radgridAgent_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                            $("#ctl00_cphPageContent_radgridAgent_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                            $("#ctl00_cphPageContent_radgridAgent_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                            $("#ctl00_cphPageContent_radgridAgent_ctl00_ctl" + i + "_AGENT_TYPE").autocomplete(agenttype);
                            $("#ctl00_cphPageContent_radgridAgent_ctl00_ctl" + i + "_REGION_SHORT_NAME").autocomplete(regionname);
                        }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Agent?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridAgent" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="AGENT_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1700px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="AGENT_ID" DataField="AGENT_ID" HeaderText="AGENT_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="AGENT_NAME" DataField="AGENT_NAME" HeaderText="AGENT NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMAIL" DataField="EMAIL" HeaderText="EMAIL">
                          <ItemTemplate>
                            <asp:TextBox ID="EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MOBILE" DataField="MOBILE" HeaderText="MOBILE">
                          <ItemTemplate>
                            <asp:TextBox ID="MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHONE" DataField="PHONE" HeaderText="PHONE">
                          <ItemTemplate>
                            <asp:TextBox ID="PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FAX" DataField="FAX" HeaderText="FAX">
                          <ItemTemplate>
                            <asp:TextBox ID="FAX" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="AGENT_TYPE" DataField="AGENT_TYPE" HeaderText="AGENT TYPE">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="REGION_SHORT_NAME" DataField="REGION_SHORT_NAME" HeaderText="REGION SHORT NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="REGION_SHORT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE1" DataField="ADDRESS_LINE1" HeaderText="ADDRESS LINE1">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE2" DataField="ADDRESS_LINE2" HeaderText="ADDRESS LINE2">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="CITY">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="STATE">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewAgent(this,event);">
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
                <ClientEvents OnCommand="radgridAgent_Command" OnRowSelected="radgridAgent_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
