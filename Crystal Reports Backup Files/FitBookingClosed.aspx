<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FitBookingClosed.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.FitBookingClosed" %>


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
            <script type="text/javascript" src="../Shared/Javascripts/FitBookingClosed.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                FitClosedTableView = $find("<%= radgridfitbookingclosed.ClientID %>").get_masterTableView();
                FitDayTableView = $find("<%= radgridfitbookingDay.ClientID %>").get_masterTableView();
                FitClosedCommandName = "Load";
                FitDayCommand = "Load";

                if (FitClosedTableView.PageSize = 10) {
                    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);
                }
                else if (FitClosedTableView.PageSize > 10) {
                    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);
                }
                else if (FitClosedTableView.PageSize > 20) {
                    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);
                }
                CRM.WebApp.webservice.FitBookingClosedWebService.GetFitDay(0, FitDayTableView.get_pageSize(), FitDayTableView.get_sortExpressions().toString(), FitDayTableView.get_filterExpressions().toDynamicLinq(), updatefitday);
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
            function PopUpShowing(sender, args) {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'block';
                divmore.style.position = 'Absolute';
                divmore.style.left = screen.width / 2 - 150;
                divmore.style.top = screen.height / 2 - 150;

            }
            function deleteCurrent() {

                CRM.WebApp.webservice.FitBookingClosedWebService.deleteFitClosed(FIT_BOOKING_CLOSED_ID);
                CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);
            }
            function addBookingClosed(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                
                var ary = [];
                ary[1] = FitClosedTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_PACKAGE_NAME").value;
                ary[2] = FitClosedTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                ary[3] = FitClosedTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FIT_BOOKING_CLOSED_ID;
                for (i = 0; i < 3; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.FitBookingClosedWebService.InsertUpdateFitClosed(ary);
                    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);

                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addBookingDay(sender,args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                
                var ary = [];
                ary[1] = FitDayTableView.get_dataItems()[currentRowIndex - 1].findElement("DAY").value;
                ary[2] = FitDayTableView.get_dataItems()[currentRowIndex - 1].findElement("UP_TO_DATE").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FIT_BOOKING_DAY_ID;
                
                if(ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                
                try {
                    CRM.WebApp.webservice.FitBookingClosedWebService.InsertUpdateFitDay(ary);
                    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitDay(0, FitDayTableView.get_pageSize(), FitDayTableView.get_sortExpressions().toString(), FitDayTableView.get_filterExpressions().toDynamicLinq(), updatefitday);

                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
     <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="FIT Booking Closed"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {


            var fitpackage = "../../webservice/autocomplete.ashx?key=GET_FIT_PACKAGE_FOR_TRANSFER_PACKAGE";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridfitbookingclosed_ctl00_ctl" + i + "_FIT_PACKAGE_NAME").autocomplete(fitpackage);

            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Fit Booking Closed?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server"/>
                </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridfitbookingclosed" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="FIT_BOOKING_CLOSED_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="FIT_BOOKING_CLOSED_ID" DataField="FIT_BOOKING_CLOSED_ID" HeaderText="FIT_BOOKING_CLOSED_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_BOOKING_CLOSED_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_NAME" DataField="FIT_PACKAGE_NAME" HeaderText="FIT Package">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="FROM_DATE" DataField="FROM_DATE" HeaderText="From Date">
                          <ItemTemplate>
                            <asp:TextBox ID="FROM_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TO_DATE" DataField="TO_DATE" HeaderText="To Date">
                          <ItemTemplate>
                            <asp:TextBox ID="TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addBookingClosed(this,event);">
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
                <ClientEvents OnCommand="radgridfitbookingclosed_Command" OnRowSelected="radgridfitbookingclosed_RowSelected" OnRowDblClick="addfitbookingClosed"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <table>
            <tr>
                <td>
                        <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Fit Booking Day?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" Visible="false"/>
                </td>
            </tr>
        </table>
           <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="No Of Days"></asp:Literal>
    </div>
    <br />
    <br /> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridfitbookingDay" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="FIT_BOOKING_DAY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="FIT_BOOKING_DAY_ID" DataField="FIT_BOOKING_DAY_ID" HeaderText="FIT_BOOKING_DAY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_BOOKING_DAY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DAY" DataField="DAY" HeaderText="No Of Days">
                          <ItemTemplate>
                            <asp:TextBox ID="DAY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="UP_TO_DATE" DataField="UP_TO_DATE" HeaderText="Date">
                          <ItemTemplate>
                            <asp:TextBox ID="UP_TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addBookingDay(this,event);">
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
                <ClientEvents OnCommand="radgridfitbookingDay_Command" OnRowSelected="radgridfitbookingDay_RowSelected" OnRowDblClick="addDay"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
</asp:Content>