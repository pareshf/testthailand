<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FareHotelRoomTypeMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.FareHotelRoomTypeMaster" %>
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
            <script type="text/javascript" src="../Shared/Javascripts/FareHotelRoom.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                HotelRoomTypeTableView = $find("<%= radgridhotelroomtype.ClientID %>").get_masterTableView();
                HotelRoomTypeComandName = "Load";
               // CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(HotelRoomTypeTableView.get_currentPageIndex() * HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);


                if (HotelRoomTypeTableView.PageSize = 10) {
                    CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);
                }
                else if (HotelRoomTypeTableView.PageSize > 10) {
                    CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);
                }
                else if (HotelRoomTypeTableView.PageSize > 20) {
                    CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);
                }

            }
            function deleteCurrent() {
                 CRM.WebApp.webservice.FareHotelRoomTypeWebService.deleteHotelType(ROOM_TYPE_ID);
                 CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);
            }
            function addnewHoteltype(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = HotelRoomTypeTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TYPE_NAME").value;
                ary[2] = HotelRoomTypeTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_SIZE").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.ROOM_TYPE_ID;
                for (i = 0; i < 3; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.FareHotelRoomTypeWebService.InsertUpdateHotelRoom(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Room Type Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            //            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";

            //            for (i = 0; i < 55; i++) {
            //                if (i < 10)
            //                    i = '0' + i;

            //                $("#ctl00_cphPageContent_radgridsitemaster_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
            //            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Customer?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridhotelroomtype" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CUST_TYPE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="300px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="ROOM_TYPE_ID" DataField="ROOM_TYPE_ID" HeaderText="ROOM_TYPE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="ROOM_TYPE_NAME" DataField="ROOM_TYPE_NAME" HeaderText="Room Type">
                          <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ROOM_SIZE" DataField="ROOM_SIZE" HeaderText="Room Size">
                          <ItemTemplate>
                            <asp:TextBox ID="ROOM_SIZE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewHoteltype(this,event);">
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
                <ClientEvents OnCommand="radgridhotelroomtype_Command" OnRowSelected="radgridhotelroomtype_RowSelected" OnRowDblClick="addNewHotelRoomType"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
