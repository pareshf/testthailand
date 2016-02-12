<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TargetListMaster.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.TargetListMaster" %>
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
    
        <script type="text/javascript" src="../Shared/Javascripts/TargetListGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                TargetListTableView = $find("<%= radgridTargetListmaster.ClientID %>").get_masterTableView();
                TargetListCommand = "Load";
                CRM.WebApp.webservice.TargetListMasterWebService.GetTargetList(TargetListTableView.get_currentPageIndex() * TargetListTableView.get_pageSize(), TargetListTableView.get_pageSize(), TargetListTableView.get_sortExpressions().toString(), TargetListTableView.get_filterExpressions().toDynamicLinq(), updateTargetList);

                /*hELLO 123*/
            }
            function delTargetList() {
                var table = $find("<%= radgridTargetListmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridTargetListmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridTargetListmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.TargetListMasterWebService.delTargetList(TARGETLIST_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewTargetList(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                
                ary[0] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("TARGETLIST_NAME").value;
                ary[2] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("SOURCE").value;
                ary[3] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[4] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("COST").value;
                ary[5] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
                ary[6] = TargetListTableView.get_dataItems()[currentRowIndex - 1].findElement("EMPLOYEE_NAME").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TARGETLIST_ID;
                for (i = 0; i < 7; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.TargetListMasterWebService.InsertUpdateTargetList(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.TargetListMasterWebService.GetTargetList(TargetListTableView.get_currentPageIndex() * TargetListTableView.get_pageSize(), TargetListTableView.get_pageSize(), TargetListTableView.get_sortExpressions().toString(), TargetListTableView.get_filterExpressions().toDynamicLinq(), updateTargetList);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
//            function Redirect() {

//                window.location = "MarketingCustomerNew.aspx?TARGETLIST_ID=" + TARGETLIST_ID;
//            }
        </script>
  </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblpagename" runat="server" Text="Campaign Target List Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TOUR";
            var employee = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridTargetListmaster_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridTargetListmaster_ctl00_ctl" + i + "_EMPLOYEE_NAME").autocomplete(employee);
            }

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="btndelete" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Target List?'))return false; delTargetList(); return false;"
                    Text="Delete" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridTargetListmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="TARGETLIST_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="TARGETLIST_ID" DataField="TARGETLIST_ID" HeaderText="TARGETLIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TARGETLIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TARGETLIST_NAME" DataField="TARGETLIST_NAME" HeaderText="Target List Name">
                          <ItemTemplate>
                            <asp:TextBox ID="TARGETLIST_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SOURCE" DataField="SOURCE" HeaderText="Source">
                          <ItemTemplate>
                            <asp:TextBox ID="SOURCE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="▼Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COST" DataField="COST" HeaderText="Cost">
                          <ItemTemplate>
                            <asp:TextBox ID="COST" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="Description">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMPLOYEE_NAME" DataField="EMPLOYEE_NAME" HeaderText="▼Employee">
                          <ItemTemplate>
                            <asp:TextBox ID="EMPLOYEE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewTargetList(this,event);">
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
                <ClientEvents OnCommand="radgridTargetList_Command" OnRowSelected="radgridTargetList_RowSelected" OnRowDblClick="TargetRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
</table>
<table>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Go To Next" OnClientClick="Redirect();"
                    Style="color: black;" Visible="false"/>
            </td>
        </tr>
    </table>
</asp:Content>
