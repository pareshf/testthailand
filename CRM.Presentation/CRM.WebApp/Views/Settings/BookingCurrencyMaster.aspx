<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="BookingCurrencyMaster.aspx.cs" Inherits="CRM.WebApp.Views.Settings.BookingCurrencyMaster" %>

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

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/BookingCurrencyMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {
                
                CurrencyTableView = $find("<%= radgridbookingcurrencymaster.ClientID %>").get_masterTableView();
                CurrencyCommandName = "Load";
                //CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(0, CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);

                if (CurrencyTableView.PageSize = 10) {
                    CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(0, CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);
                }
                else if (CurrencyTableView.PageSize > 10) {
                    CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(0, CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);
                }
                else if (CurrencyTableView.PageSize > 20) {
                    CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(0, CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);
                }
            }
            function deleteCurrent() {

                CRM.WebApp.webservice.BookingCurrencyMaster.deleteBookingCurrency(CURRENCY_ID);
                CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(CurrencyTableView.get_currentPageIndex() * CurrencyTableView.get_pageSize(), CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);
                
            }
            function addnewBookingCurrency(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;

                var ary = [];
                ary[1] = CurrencyTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[2] = CurrencyTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_SYMBOL").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CURRENCY_ID;
                for (i = 0; i < 3; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.BookingCurrencyMaster.InsertUpdateBookingCurrency(ary);
                    CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(0, CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);

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
        <asp:Literal ID="lblPageSite" runat="server" Text="Currnecy Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {



            // $("#ctl00_cphPageContent_radgridaddresstypemaster_ctl00_ctl03_ctl01_PageSizeComboBox_DropDown").val();



        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Booking Currency?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridbookingcurrencymaster" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CURRENCY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_ID" DataField="CURRENCY_ID" HeaderText="CURRENCY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CURRENCY_SYMBOL" DataField="CURRENCY_SYMBOL" HeaderText="Currency Symbol">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_SYMBOL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewBookingCurrency(this,event);">
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
                <ClientEvents OnCommand="radgridbookingcurrencymaster_Command" OnRowSelected="radgridbookingcurrencymaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

