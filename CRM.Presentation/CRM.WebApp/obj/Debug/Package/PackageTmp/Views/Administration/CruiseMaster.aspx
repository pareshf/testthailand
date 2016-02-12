<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CruiseMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.CruiseMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
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
    <script type="text/javascript" src="../Shared/Javascripts/CruiseMasterGridScript.js"></script>
    
        <script type="text/javascript">
            function pageLoad() {
                customersTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                CruiseScheduleTableView = $find("<%= radgridschedule.ClientID %>").get_masterTableView();
                CruisePriceTableView = $find("<%= radgridprice.ClientID %>").get_masterTableView();
                CruiseVisaTableView = $find("<%= radgridvisa.ClientID %>").get_masterTableView();
                customersCommandName = "Load";
                CRM.WebApp.webservice.CruiseMasterWebService.GetCruise(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateCruiseGrid);
                // CRM.WebApp.webservice.CruiseMasterWebService.GetCruiseSchedule(0, CruiseScheduleTableView.get_pageSize(), CruiseScheduleTableView.get_sortExpressions().toString(), CruiseScheduleTableView.get_filterExpressions().toDynamicLinq(), updateCruiseScheduleGrid);
            }
            
            function newrowaddedforcruise(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                //  ary[0] = CRUISE_ID;
                ary[1] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_NAME").value;
                ary[2] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_CODE").value;
                ary[3] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("SCHEDULE_DATE").value;
                ary[4] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("SOURCE_CITY_NAME").value;
                ary[5] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DESTINATION_CITY_NAME").value;
                ary[6] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("SOURCE_PORT_NAME").value;
                ary[7] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DESTINATION_PORT_NAME").value;
                ary[8] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DEPT_TIME").value;
                ary[9] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("ARRIVAL_TIME").value;
                ary[10] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DURATION").value;

                ary[12] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DATE_OF_DEP").value;
                ary[13] = CruiseScheduleTableView.get_dataItems()[currentRowIndex - 1].findElement("DATE_OF_ARRIVAL").value;

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
                if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
                if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
                if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
                if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
                if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
                if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
                if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;


                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_ID;
                ary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_COMPANY_NAME;
                ary[15] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_COMPANY_ID;

                try {
                    CRM.WebApp.webservice.CruiseMasterWebService.InsertUpdateCruiseScheduleDetail(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CruiseMasterWebService.CruiseScheduleGrid(CRUISE_COMPANY_ID, updateCruiseScheduleGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }


            function NewCuiseAdded(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                //  ary[0] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_COMPANY_ID").value;
                ary[1] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_COMPANY_NAME").value;


                if (ary[1] == "" || ary[1] == 'null') ary[0] = 0;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_COMPANY_ID;
                try {
                    CRM.WebApp.webservice.CruiseMasterWebService.InsertUpdateCruise(ary); // add new tour

                    CRM.WebApp.webservice.CruiseMasterWebService.GetCruise(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateCruiseGrid);

                    //var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                    //customersTableView.rebind();
                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
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

            function PopUpShowing(sender, args) {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'block';
                divmore.style.position = 'Absolute';
                divmore.style.left = screen.width / 2 - 150;
                divmore.style.top = screen.height / 2 - 150;
                var IMG = document.getElementById("imgexistingimage");
                IMG.src = args.srcElement.all[1].value;
            }

            function disablepopup() {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'none';
                return false;
            }
            function newrowaddedforprice(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];


                ary[2] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY").value;
                ary[3] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("AUDULT_AMT").value;
                ary[4] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("AUDULT_TAX").value;
                ary[5] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("AUDULT_GST").value;
                ary[6] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_AMT").value;
                ary[7] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_TAX").value;
                ary[8] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_GST").value;
                ary[9] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("INFANT_AMT").value;
                ary[10] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("INFANT_TAX").value;
                ary[11] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("INFANT_GST").value;

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
                if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
                if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
                if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
                if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
                if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CURRENCY_PRICE_ID;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_ID;

                try {
                    CRM.WebApp.webservice.CruiseMasterWebService.InsertUpdateCruiseprice(ary);
                    CRM.WebApp.webservice.CruiseMasterWebService.CruisepriceGrid(CRUISE_ID, updateCruisePrice);

                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }


            function newrowaddedforvisa(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CRUISE_ID;
                ary[2] = CruiseVisaTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;


                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;

                try {
                    CRM.WebApp.webservice.CruiseMasterWebService.InsertUpdateCruisevisa(ary);
                    CRM.WebApp.webservice.CruiseMasterWebService.CruisevisaGrid(CRUISE_ID, updateCruiseVisa);

                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }

            function AddNewCruise() {
                CRM.WebApp.webservice.CruiseMasterWebService.InsertNewCruise(CRUISE_COMPANY_ID);
            }

            function AddNewprice() {
                CRM.WebApp.webservice.CruiseMasterWebService.InsertNewCruiseprice(CRUISE_ID);
            }
            function AddNewvisa() {
                CRM.WebApp.webservice.CruiseMasterWebService.InsertNewCruisevisa(CRUISE_ID);
            }

            function deleteCurrent() {
              
                var table = $find("<%=radgridmaster.ClientID%>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex];
                table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id)

                if (dataItem) {

                    dataItem.dispose();
                    Array.remove($find("<%= radgridmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }

                var gridItems = $find("<%= radgridmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CruiseMasterWebService.DeleteCruise(CRUISE_COMPANY_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }

            function deleteCurrentSubProg() {
                var table = $find("<%=radgridschedule.ClientID%>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex];
                table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id)

                if (dataItem) {

                    dataItem.dispose();
                    Array.remove($find("<%= radgridschedule.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }

                var gridItems = $find("<%= radgridschedule.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CruiseMasterWebService.DeleteCruiseSchedule(CRUISE_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }

          
            </script>
      </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageTour" runat="server" Text="Cruise Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            for (var i = 1; i < 15; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridschedule_ctl00_ctl" + i + "_SOURCE_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridschedule_ctl00_ctl" + i + "_DESTINATION_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridschedule_ctl00_ctl" + i + "_SOURCE_PORT_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridschedule_ctl00_ctl" + i + "_DESTINATION_PORT_NAME").autocomplete(city);
            }

            {
                var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
                for (var i = 1; i < 15; i++) {
                    if (i < 10)
                        i = '0' + i;

                    $("#ctl00_cphPageContent_radgridprice_ctl00_ctl" + i + "_CURRENCY").autocomplete(Currency);

                }

            }

            {
                var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
                for (var i = 1; i < 15; i++) {
                    if (i < 10)
                        i = '0' + i;

                    $("#ctl00_cphPageContent_radgridvisa_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);

                }

            }

        });
    </script>
    <div id="divradmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Cruise Company?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="400">
            <MasterTableView ClientDataKeyNames="CRUISE_COMPANY_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                   <Columns>
                        <%--template column for raw data editing result--%>
                    <telerik:GridTemplateColumn SortExpression="CRUISE_COMPANY_ID" DataField="CRUISE_COMPANY_ID" HeaderText="CRUISE COMPANY ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_COMPANY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                 <telerik:GridTemplateColumn SortExpression="CRUISE_COMPANY_NAME" DataField="CRUISE_COMPANY_NAME" UniqueName="FOOTERTOURCODE" HeaderText="Cruise Company Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_COMPANY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="NewCuiseAdded(this,event);">
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
                <ClientEvents OnCommand="radgridmaster_Command" OnRowSelected="radgridmaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Cruise Schedule"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="divradgriddetails" style="overflow: auto; height: 100%;">
        <table>
            <tr>
                <td>
                    <asp:Button ID="DeleteSubProg" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Schedule?'))return false; deleteCurrentSubProg(); return false;"
                        Text="Delete Schedule" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridschedule" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="15" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="2000">
            <MasterTableView ClientDataKeyNames="CRUISE_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                   <Columns>
                 <telerik:GridTemplateColumn SortExpression="CRUISE_ID" DataField="CRUISE_ID" HeaderText="CRUISE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_ID" runat="server" Width="80px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CRUISE_NAME" DataField="CRUISE_NAME" HeaderText="Cruise Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CRUISE_CODE" DataField="CRUISE_CODE" HeaderText="Cruise Code">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_CODE" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="SCHEDULE_DATE" DataField="SCHEDULE_DATE" HeaderText="Schedule Date">
                        <ItemTemplate>
                            <asp:TextBox ID="SCHEDULE_DATE" runat="server" Width="100%" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="SOURCE_CITY_NAME" DataField="SOURCE_CITY_NAME" HeaderText="Source City Name">
                        <ItemTemplate>
                            <asp:TextBox ID="SOURCE_CITY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="DESTINATION_CITY_NAME" DataField="DESTINATION_CITY_NAME" HeaderText="Destination City Name">
                        <ItemTemplate>
                          <asp:TextBox ID="DESTINATION_CITY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="SOURCE_PORT_NAME" DataField="SOURCE_PORT_NAME" HeaderText="Source Port Name">
                        <ItemTemplate>
                           <asp:TextBox ID="SOURCE_PORT_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="DESTINATION_PORT_NAME" DataField="DESTINATION_PORT_NAME" HeaderText="Destination Port Name">
                        <ItemTemplate>
                            <asp:TextBox ID="DESTINATION_PORT_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="DEPT_TIME" DataField="DEPT_TIME" HeaderText="Depart Time">
                        <ItemTemplate>
                             <asp:TextBox ID="DEPT_TIME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="ARRIVAL_TIME" DataField="ARRIVAL_TIME" HeaderText="Arrival Time">
                        <ItemTemplate>
                            <asp:TextBox ID="ARRIVAL_TIME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="DURATION" DataField="DURATION" HeaderText="Duration">
                        <ItemTemplate>
                            <asp:TextBox ID="DURATION" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="CRUISE_COMPANY_ID" DataField="CRUISE_COMPANY_ID" HeaderText="Duration" visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_COMPANY_ID" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CRUISE_COMPANY_NAME" DataField="CRUISE_COMPANY_NAME" HeaderText="Cruise Comp Name" visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_COMPANY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="DATE_OF_DEP" DataField="DATE_OF_DEP" HeaderText="Date Of Dept">
                        <ItemTemplate>
                             <asp:TextBox ID="DATE_OF_DEP" runat="server" Width="100%" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="DATE_OF_ARRIVAL" DataField="DATE_OF_ARRIVAL" HeaderText="Date Of Arrival">
                        <ItemTemplate>
                              <asp:TextBox ID="DATE_OF_ARRIVAL" runat="server" Width="100%" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   
                       <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" visible="false">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;">
                            Book
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforcruise(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
             <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridschedule_Command" OnRowSelected="radgridschedule_RowSelected" />
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddCruise" runat="server" Text="Add Another Cruise" OnClientClick="AddNewCruise();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal3" runat="server" Text="Cruise Price"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="divprice" style="overflow: auto; height: 100%;">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridprice" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="15" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="2000">
            <MasterTableView ClientDataKeyNames="CURRENCY_PRICE_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                   <Columns>
                 <telerik:GridTemplateColumn SortExpression="CURRENCY_PRICE_ID" DataField="CURRENCY_PRICE_ID" HeaderText="CRUISE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_PRICE_ID" runat="server" Width="80px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CRUISE_ID" DataField="CRUISE_ID" HeaderText="Cruise Name" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_ID" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CURRENCY" DataField="CURRENCY" HeaderText="Currency">
                        <ItemTemplate>
                            <asp:TextBox ID="CURRENCY" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="AUDULT_AMT" DataField="AUDULT_AMT" HeaderText="Adult Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="AUDULT_AMT" runat="server" Width="100%" CssClass="radinput" ></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="AUDULT_TAX" DataField="AUDULT_TAX" HeaderText="Adult Tax">
                        <ItemTemplate>
                            <asp:TextBox ID="AUDULT_TAX" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="AUDULT_GST" DataField="AUDULT_GST" HeaderText="Adult GST">
                        <ItemTemplate>
                          <asp:TextBox ID="AUDULT_GST" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CHILD_AMT" DataField="CHILD_AMT" HeaderText="Child Amount">
                        <ItemTemplate>
                           <asp:TextBox ID="CHILD_AMT" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CHILD_TAX" DataField="CHILD_TAX" HeaderText="Child Tax">
                        <ItemTemplate>
                            <asp:TextBox ID="CHILD_TAX" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CHILD_GST" DataField="CHILD_GST" HeaderText="Child GST">
                        <ItemTemplate>
                             <asp:TextBox ID="CHILD_GST" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="INFANT_AMT" DataField="INFANT_AMT" HeaderText="Infant Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="INFANT_AMT" runat="server" Width="100%" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="INFANT_TAX" DataField="INFANT_TAX" HeaderText="Infant Tax">
                        <ItemTemplate>
                            <asp:TextBox ID="INFANT_TAX" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="INFANT_GST" DataField="INFANT_GST" HeaderText="Infant GST">
                        <ItemTemplate>
                            <asp:TextBox ID="INFANT_GST" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     
                   
                       <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" visible="false">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A4" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;">
                            Book
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A5" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforprice(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
             <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridprice_Command"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Add Another price" OnClientClick="AddNewprice();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Country Visa"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="divvisa" style="overflow: auto; height: 100%;">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridvisa" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="15" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="400">
            <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumnSorting="true" EditMode="InPlace">
                   <Columns>
                 <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="Sr no" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" Width="80px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CRUISE_ID" DataField="CRUISE_ID" HeaderText="CRUISE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CRUISE_ID" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country Name">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                                         
                   
                       <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" visible="false">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A6" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;">
                            Book
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A7" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowaddedforvisa(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
             <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridvisa_Command"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Add Another country" OnClientClick="AddNewvisa();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
