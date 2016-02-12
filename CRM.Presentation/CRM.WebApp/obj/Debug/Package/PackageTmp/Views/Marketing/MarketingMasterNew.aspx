<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="MarketingMasterNew.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.MarketingMasterNew" %>

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
    
        <script type="text/javascript" src="../Shared/Javascripts/MarketingMaterialGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                MarketingMaterialTableView = $find("<%= radgridMarketingmaster.ClientID %>").get_masterTableView();
                MarketingMaterialCommand = "Load";
                if (MarketingMaterialTableView.PageSize == 10) {
                    CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);
                }
                else if (MarketingMaterialTableView.PageSize > 10) {
                    CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);
                }
                else if (MarketingMaterialTableView.PageSize > 20) {
                    CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);
                }
            }
            var currentTextBox = null;
            var currentDatePicker = null;

            function showPopup(sender, e) {

                try {

                    currentTextBox = sender;
                    var datePicker = $find("<%= RadDatePicker1.ClientID %>");
                    currentDatePicker = datePicker;
                    datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));
                    var position = datePicker.getElementPosition(sender);
                    datePicker.showPopup(position.x, position.y + sender.offsetHeight);

                }
                catch (e) { }

            }

            function dateSelected(sender, args) {

                try {

                    if (currentTextBox != null) {

                        currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                        currentDatePicker.hidePopup();

                    }

                }
                catch (e) { }

            }

            function parseDate(sender, e) {

                currentDatePicker.hidePopup();
            }
            function delMaterial() {
                var table = $find("<%= radgridMarketingmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridMarketingmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridMarketingmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.MarketingMaterialWebService.delMarketingMaterial(MAR_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewMarketingMaterial(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[0] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_SHORT_NAME").value;
                ary[2] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_CODE").value;
                //ary[2] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("TYPE").value;
                //ary[3] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("EXPIRATION_DATE").value;
                //ary[4] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
                //ary[5] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("EMBEDCODE").value;
                //ary[6] = MarketingMaterialTableView.get_dataItems()[currentRowIndex - 1].findElement("WEBURL").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.MAR_ID;
                for (i = 0; i < 7; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.MarketingMaterialWebService.InsertUpdateMarketingMaterial(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.MarketingMaterialWebService.GetMarketingMaterail(MarketingMaterialTableView.get_currentPageIndex() * MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_pageSize(), MarketingMaterialTableView.get_sortExpressions().toString(), MarketingMaterialTableView.get_filterExpressions().toDynamicLinq(), updateMarketingMaterialGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function uploadnewDoc() {

                window.open('MarketingMaterialDoc.aspx?key=' + MAR_ID);
            }
            function getTourName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getTourId(sender) {
                var tourcode = sender.value;
                $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
            }
        </script>
   </telerik:radcodeblock>
   <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblpagename" runat="server" Text="Marketing Material Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridMarketingmaster_ctl00_ctl" + i + "_TOUR_SHORT_NAME").autocomplete(tourshortname);
                $("#ctl00_cphPageContent_radgridMarketingmaster_ctl00_ctl" + i + "_TOUR_CODE").autocomplete(tourcode);
            }

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Marketing Material?'))return false; delMaterial(); return false;"
                    Text="Delete" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridMarketingmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="MAR_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="600px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="MAR_ID" DataField="MAR_ID" HeaderText="MAR_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MAR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_SHORT_NAME" DataField="TOUR_SHORT_NAME" HeaderText="▼Tour Name">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" onblur="getTourName(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_CODE" DataField="TOUR_CODE" HeaderText="▼Tour Code">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_CODE" runat="server" CssClass="radinput" onblur="getTourId(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TITLE" DataField="TITLE" HeaderText="Title" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TYPE" DataField="TYPE" HeaderText="Type" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="EXPIRATION_DATE" DataField="EXPIRATION_DATE" HeaderText="Expiration Date" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EXPIRATION_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="Description" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMBEDCODE" DataField="EMBEDCODE" HeaderText="Embed Code" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EMBEDCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="WEBURL" DataField="WEBURL" HeaderText="Web Url" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="WEBURL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="DOC">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploaddoc" runat="server" Text="DOC" onClientclick="uploadnewDoc()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewMarketingMaterial(this,event);">
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
                <ClientEvents OnCommand="radgridMarketingmaster_Command" OnRowSelected="radgridMarketingmaster_RowSelected" OnRowDblClick="MarketingRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
</table>
</asp:Content>