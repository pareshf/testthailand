<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FareVisaQuote.aspx.cs" Inherits="CRM.WebApp.Views.Administration.FareVisaQuote" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
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
            <script type="text/javascript" src="../Shared/Javascripts/VisaQuoteGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                VisaQuoteTableView = $find("<%= radgridfarevisaquote.ClientID %>").get_masterTableView();
                VisaQuoteCommand = "Load";
                CRM.WebApp.webservice.VisaQuoteWebService.GetVisaQuote(VisaQuoteTableView.get_currentPageIndex() * VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_sortExpressions().toString(), VisaQuoteTableView.get_filterExpressions().toDynamicLinq(), updatevisaQuote);


            }
            function deleteCurrent() {
                var table = $find("<%= radgridfarevisaquote.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridfarevisaquote.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridfarevisaquote.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.VisaQuoteWebService.deleteVisaQuote(VISA_QUOTE_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewfarevisaquote(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("PROCESS_TYPE_NAME").value;
                ary[2] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("SINGLE_MULTIPLE_VISA").value;
                ary[3] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("VISA_FEE").value;
                ary[4] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("VFS").value;
                ary[5] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("SERVICE_CHARGE").value;
                ary[6] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("TRAVEL_PERIOD_ID").value;
                ary[7] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[8] = VisaQuoteTableView.get_dataItems()[currentRowIndex - 1].findElement("VISA_TYPE_NAME").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.VISA_QUOTE_ID;
                for (i = 0; i < 9; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.VisaQuoteWebService.InsertUpdateVisaQuote(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.VisaQuoteWebService.GetVisaQuote(VisaQuoteTableView.get_currentPageIndex() * VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_sortExpressions().toString(), VisaQuoteTableView.get_filterExpressions().toDynamicLinq(), updatevisaQuote);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function PopUpShowing(sender, args) { var divmore = document.getElementById('divmore'); divmore.style.display = 'block'; divmore.style.position = 'Absolute'; divmore.style.left = screen.width / 2 - 150; divmore.style.top = screen.height / 2 - 150; var IMG = document.getElementById("imgexistingimage"); IMG.src = args.srcElement.all[1].value; }
            function disablepopup() { var divmore = document.getElementById('divmore'); divmore.style.display = 'none'; return false; }
            function openuploadnewdocument() {

                window.open('VisaQuoteDocument.aspx?key=' + VISA_QUOTE_ID);
            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Visa Quote Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var single = "../../webservice/autocomplete.ashx?key=SINGLE_MULTIPLE_VISA_FOR_FARE_VISA_QUOTE_AUTOSEARCH";
            var visaname = "../../webservice/autocomplete.ashx?key=VISA_NAME_FOR_COMMON_VISA_TYPE_AUTOSEARCH";
            var visatype = "../../webservice/autocomplete.ashx?key=PROCESS_TYPE_NAME_FOR_FARE_VISA_QUOTE_AUTOSEARCH";

                        for (i = 0; i < 55; i++) {
                            if (i < 10)
                                i = '0' + i;

                            $("#ctl00_cphPageContent_radgridfarevisaquote_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                            $("#ctl00_cphPageContent_radgridfarevisaquote_ctl00_ctl" + i + "_VISA_TYPE_NAME").autocomplete(visaname);
                            $("#ctl00_cphPageContent_radgridfarevisaquote_ctl00_ctl" + i + "_PROCESS_TYPE_NAME").autocomplete(visatype);
                            $("#ctl00_cphPageContent_radgridfarevisaquote_ctl00_ctl" + i + "_SINGLE_MULTIPLE_VISA").autocomplete(single);
                        }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Visa Quote?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridfarevisaquote" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="VISA_QUOTE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1200px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="VISA_QUOTE_ID" DataField="VISA_QUOTE_ID" HeaderText="VISA_QUOTE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="VISA_QUOTE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="TRAVEL_PERIOD_ID" DataField="TRAVEL_PERIOD_ID" HeaderText="TRAVEL PERIOD">
                          <ItemTemplate>
                            <asp:TextBox ID="TRAVEL_PERIOD_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROCESS_TYPE_NAME" DataField="PROCESS_TYPE_NAME" HeaderText="PROCESS NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="PROCESS_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VISA_TYPE_NAME" DataField="VISA_TYPE_NAME" HeaderText="VISA TYPE">
                          <ItemTemplate>
                            <asp:TextBox ID="VISA_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VISA_FEE" DataField="VISA_FEE" HeaderText="VISA FEE">
                          <ItemTemplate>
                            <asp:TextBox ID="VISA_FEE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VFS" DataField="VFS" HeaderText="VFS">
                          <ItemTemplate>
                            <asp:TextBox ID="VFS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SERVICE_CHARGE" DataField="SERVICE_CHARGE" HeaderText="SERVICE CHARGE">
                          <ItemTemplate>
                            <asp:TextBox ID="SERVICE_CHARGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SINGLE_MULTIPLE_VISA" DataField="SINGLE_MULTIPLE_VISA" HeaderText="VISA NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="SINGLE_MULTIPLE_VISA" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="PHOTO">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploadphoto" runat="server" Text="DOC" onClientclick="openuploadnewdocument()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewfarevisaquote(this,event);">
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
                <ClientEvents OnCommand="radgridfarevisaquote_Command" OnRowSelected="radgridfarevisaquote_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>