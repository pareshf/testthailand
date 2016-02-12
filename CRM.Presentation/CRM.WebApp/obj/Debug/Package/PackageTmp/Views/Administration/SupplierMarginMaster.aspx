<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SupplierMarginMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierMarginMaster" %>
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
            <script type="text/javascript" src="../Shared/Javascripts/SupplierMarginMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                SupplierMarginTableView = $find("<%= radgridsuppliermarginmaster.ClientID %>").get_masterTableView();
                SupplierMarginCommandName = "Load";
                CRM.WebApp.webservice.SupplierMarginMaster.GetSupplierMargin(SupplierMarginTableView.get_currentPageIndex() * SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_sortExpressions().toString(), SupplierMarginTableView.get_filterExpressions().toDynamicLinq(), updateSupplierMarginName);
            }
            function deleteCurrent() {
                var table = $find("<%= radgridsuppliermarginmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridsuppliermarginmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridsuppliermarginmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.SupplierMarginMaster.deleteSupplierMargin(SUPPLIER_MARGIN_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewSupplierMarginMaster(sender, args) {
                debugger;
                currentRowIndex = sender.parentNode.parentNode.rowIndex;

                var ary = [];

                ary[1] = SupplierMarginTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                ary[2] = SupplierMarginTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                ary[3] = SupplierMarginTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_COMPANY_NAME").value;
                ary[4] = SupplierMarginTableView.get_dataItems()[currentRowIndex - 1].findElement("PRODUCT_DESC").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[1].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_MARGIN_ID;
                for (i = 0; i < 5; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.SupplierMarginMaster.InsertUpdateSupplierMargin(ary);
                    CRM.WebApp.webservice.SupplierMarginMaster.GetSupplierMargin(SupplierMarginTableView.get_currentPageIndex() * SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_pageSize(), SupplierMarginTableView.get_sortExpressions().toString(), SupplierMarginTableView.get_filterExpressions().toDynamicLinq(), updateSupplierMarginName);

                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Margin Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {


            var Product = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_PRODUCT_AUTOSEARCH";
            var Supplier = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_COMPANY_NAME_FOR_AUTOSEARCH";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsuppliermarginmaster_ctl00_ctl" + i + "_PRODUCT_DESC").autocomplete(Product);
                $("#ctl00_cphPageContent_radgridsuppliermarginmaster_ctl00_ctl" + i + "_SUPPLIER_COMPANY_NAME").autocomplete(Supplier);
            }

        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Supplier Margin?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                     <telerik:radgrid id="radgridsuppliermarginmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_MARGIN_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="600px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_MARGIN_ID" DataField="SUPPLIER_MARGIN_ID" HeaderText="SUPPLIER_MARGIN_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_MARGIN_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="PRODUCT_DESC" DataField="PRODUCT_DESC" HeaderText="PRODUCT">
                          <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_COMPANY_NAME" DataField="SUPPLIER_COMPANY_NAME" HeaderText="SUPPLIER COMPANY">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     
                     <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT" DataField="MARGIN_AMOUNT" HeaderText="MARGIN AMOUNT" >
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="MARGIN AMOUNT IN PERCENTAGE" >
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewSupplierMarginMaster(this,event);">
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
                <ClientEvents OnCommand="radgridsuppliermarginmaster_Command" OnRowSelected="radgridsuppliermarginmaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
