<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="HotelDashboardMaster.aspx.cs" Inherits="CRM.WebApp.Views.Settings.HotelDashboardMaster" %>
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
            <script type="text/javascript" src="../Shared/Javascripts/HotelDashboard.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                HotelTableView = $find("<%= radgridhotel.ClientID %>").get_masterTableView();
                HotelCommandName = "Load";

                
                if (HotelTableView.PageSize = 10) {
               
                    CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);
                }
                else if (HotelTableView.PageSize > 10) {
                    CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);
                }
                else if (HotelTableView.PageSize > 20) {
                    CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);
                }

            }
            function deleteCurrent() {

                CRM.WebApp.webservice.HotelDashboardMaster.delNews(HOTEL_DASHBOARD_ID);
                CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);
            }
            function addNews(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[2] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHAIN_NAME").value;
                ary[3] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_DASHBOARD").value;
                ary[4] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
      ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex- 1]._dataItem.HOTEL_DASHBOARD_ID;

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

                try {
                    CRM.WebApp.webservice.HotelDashboardMaster.InsertUpdatehotels(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function getCityName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Hotel Dashboard Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var yes_no = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var city_name = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FIVE";
            var HOTEL_NAME = "../../webservice/autocomplete.ashx?key=GET_HOTEL_NAME_CITY_WISE_AUTOSERARCH?" + globalvalue;
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridhotel_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city_name);
                $("#ctl00_cphPageContent_radgridhotel_ctl00_ctl" + i + "_CHAIN_NAME").autocomplete(HOTEL_NAME);
                $("#ctl00_cphPageContent_radgridhotel_ctl00_ctl" + i + "_IS_DASHBOARD").autocomplete(yes_no);
            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this News Detail?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridhotel" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="HOTEL_DASHBOARD_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="600px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="HOTEL_DASHBOARD_ID" DataField="HOTEL_DASHBOARD_ID" HeaderText="HOTEL_DASHBOARD_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="HOTEL_DASHBOARD_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" onblur="getCityName(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHAIN_NAME" DataField="CHAIN_NAME" HeaderText="Hotel">
                          <ItemTemplate>
                            <asp:TextBox ID="CHAIN_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="Description">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="IS_DASHBOARD" DataField="IS_DASHBOARD" HeaderText="Display On Dashboard">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_DASHBOARD" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addNews(this,event);">
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
                <ClientEvents OnCommand="radgridNews_Command" OnRowSelected="radgridNews_RowSelected" OnRowDblClick="addNewsMaster"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
