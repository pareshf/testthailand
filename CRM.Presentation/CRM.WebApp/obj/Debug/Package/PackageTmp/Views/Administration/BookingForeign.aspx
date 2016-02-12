<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="BookingForeign.aspx.cs" Inherits="CRM.WebApp.Views.Administration.BookingForeign" %>

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
    <script type="text/javascript" src="../Shared/Javascripts/BookingForeignMoneyTransferAgent.js"></script>
    
        <script type="text/javascript">
            var receivednby = '<%=Session["usersname"]%>';
            var signature = '<%=Session["signature"]%>';
            function pageLoad() {
                paymentmasterTableView = $find("<%= radgridforeignagent.ClientID %>").get_masterTableView();
                paymentagenttableview = $find("<%= radgridforeignagent.ClientID %>").get_masterTableView();
             
                paymentmasterCommandName = "Load";
                CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
                var q = window.location.search.substring(1);

                if (q != "") {

                    CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
                  //  CRM.WebApp.webservice.BookingForeignWebService.GetForeignreport(TOUR_ID, updateForeignreportGrid);
                    recuvedbytodate();

                }



//                var receivednby = '<%=Session["usersname"]%>';

//                var grid = $find("<%=radgridforeignagent.ClientID %>")
//                var masterTable = grid.get_masterTableView();

//                for (var j = 0; j < masterTable.get_dataItems().length; j++) {

//                    var recuser;
//                    masterTable.get_dataItems()[j].findElement("USER_NAME").value = receivednby;
//                    recuser = receivednby;

//                    

//                }

            }
            function recuvedbytodate() {

                var receivednby = '<%=Session["usersname"]%>';
                var grid = $find("<%=radgridforeignagent.ClientID %>")
                var masterTable = grid.get_masterTableView();
                for (var j = 0; j < masterTable.get_dataItems().length; j++) {
                    masterTable.get_dataItems()[j].findElement("USER_NAME").value = receivednby;
                    
                }
            }

            function ForeignAdded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FA_SR_NO;
                //ary[2] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOKING_ID").value;
                ary[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BOOKING_ID;
                ary[3] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOREIGN_CURR_AGENT_ID").value;
                ary[4] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_CURR_CODE").value;
                ary[5] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_MODE_NAME").value;
                ary[6] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("REC_CHEQUE_DD_NO").value;
                ary[7] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
                ary[8] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
                ary[9] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_BRANCH_NAME").value;
                ary[10] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_NO").value;
                ary[11] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("RECEIPT_DATE").value;
                ary[12] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("USER_NAME").value;
                ary[13] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("SERVICE_CHARGE").value;
                ary[14] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
                //ary[15] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_ID").value;
                ary[15] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TOUR_ID;
                ary[1] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_ID").value;
                ary[16] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("Signature").value;
                ary[17] = paymentmasterTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;

                for (i = 0; i < 17; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                if (ary[16] == signature) {
                    try {
                        CRM.WebApp.webservice.BookingForeignWebService.InsertUpdateForeignAgent(ary);
                        CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
                        alert('Record Save Successfully');
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                else {

                    alert('Enter Correct Signature Password .');
                }
                var grid = $find("<%=radgridforeignagent.ClientID %>");
                var masterTable = grid.get_masterTableView();
                for (var i = 0; i < masterTable.get_dataItems().length; i++) {

                    var recuser = masterTable.get_dataItems()[i].findElement("Signature");
                    recuser.value = '';
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

            function openuploadnewphoto() {
                window.open('BookingForeignDoc.aspx?key=' + FA_SR_NO);
            }

            function getTourName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getTourId(sender) {
                var tourcode = sender.value;
                $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
            }

            function getTourData() {

                CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);
               // CRM.WebApp.webservice.BookingForeignWebService.GetForeignreport(TOUR_ID, updateForeignreportGrid);
            }

            function AddNewDetail() {
                CRM.WebApp.webservice.BookingForeignWebService.InsertNewDetail(TOUR_ID,receivednby);
                CRM.WebApp.webservice.BookingForeignWebService.GetForeign(TOUR_ID, updateForeignGrid);

            }

            </script>
      </telerik:radcodeblock>
    <script type="text/javascript" src="../Shared/Javascripts/BookingForeignMoneyTransferAgent.js"></script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageCust" runat="server" Text="Foreign Agent Payment"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {
            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var Mode = "../../webservice/autocomplete.ashx?key=FETCH_PAYMENT_MODE_NAME_AUTOSEARCH";
            var Bank = "../../webservice/autocomplete.ashx?key=FETCH_BANK_NAME_AUTOSEARCH";
            var Agent = "../../webservice/autocomplete.ashx?key=FETCH_AGENT_NAME_AUTOSEARCH";
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            var mta = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_AGENT_NAME";
            var status = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_STATUS_FOR_AUTOSEARCH";

            $("#ctl00_cphPageContent_txttourshortname").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txttourcodename").autocomplete(tourcode);


            for (var i = 1; i < 15; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_PAYMENT_CURR_CODE").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_PAYMENT_MODE_NAME").autocomplete(Mode);
                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_BANK_NAME").autocomplete(Bank);
                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_AGENT_ID").autocomplete(Agent);
                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_FOREIGN_CURR_AGENT_ID").autocomplete(mta);
                $("#ctl00_cphPageContent_radgridforeignagent_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(status);
            }
        }
        );
    </script>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="01/01/2100" runat="server">
<ClientEvents OnDateSelected="dateSelected"/>
</telerik:raddatepicker>
    <div id="divradmastergrid">
        <%-- <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Customer?'))return false; deleteCurrent(); return false;"
                        Text="Delete Customer" runat="server" />
                </td>
            </tr>
        </table>--%>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbltourshortname" runat="server" Text="Tour Short Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttourshortname" runat="server" Width="250px" onblur="getTourName(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltourcodename" runat="server" Text="Tour Code Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttourcodename" runat="server" Width="250px" onblur="getTourId(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnsave" runat="server" Text="Search" Style="color: black;"
                        OnClientClick="getTourData();" />
                </td>
            </tr>
        </table>
        <br />
        <%--<div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal1" runat="server" Text="Payment Received From Customer"></asp:Literal>
        </div>
        <br />
        <br />--%>
        <%--<table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridforeignagentreport" runat="server" allowpaging="false"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
            <MasterTableView ClientDataKeyNames="TOUR_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                  <Columns>
                   <telerik:GridTemplateColumn SortExpression="TOUR_ID" DataField="TOUR_ID" HeaderText="TOUR_ID" Visible="false">
                   <HeaderStyle Width = "90px" />
                  <ItemTemplate>
                            <asp:TextBox ID="TOUR_ID" runat="server" CssClass="radinput" text="0" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                   
                 <telerik:GridTemplateColumn SortExpression="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">          
                 <HeaderStyle Width = "90px" />             
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>   
                    <telerik:GridTemplateColumn SortExpression="PAYMENT_CURR_CODE" DataField="PAYMENT_CURR_CODE" HeaderText="Currency Code">
                    <HeaderStyle Width = "150px" />
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURR_CODE" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>     
                  <telerik:GridTemplateColumn SortExpression="AMT2" DataField="AMT2" HeaderText="Direct Forex">                       
                  <HeaderStyle Width = "90px" />
                        <ItemTemplate>
                            <asp:TextBox ID="AMT2" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>    
                    <telerik:GridTemplateColumn SortExpression="AMT1" DataField="AMT1" HeaderText="Converted Forex">
                    <HeaderStyle Width = "100px" />
                        <ItemTemplate>
                            <asp:TextBox ID="AMT1" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="TOT_AMT" DataField="TOT_AMT" HeaderText="Total Received">
                    <HeaderStyle Width = "110px" />
                        <ItemTemplate>
                            <asp:TextBox ID="TOT_AMT" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>   
                    
                      </Columns>
              </MasterTableView>
              <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridforeignagentreport_Command" OnRowSelected="radgridforeignagentreport_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
        </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />--%>
        
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridforeignagent" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
            <MasterTableView ClientDataKeyNames="FA_SR_NO" AllowMultiColumnSorting="true" EditMode="InPlace">
                  <Columns>
                   <telerik:GridTemplateColumn SortExpression="FA_SR_NO" DataField="FA_SR_NO" HeaderText="FA_SR_NO" Visible="false">
                  <ItemTemplate>
                            <asp:TextBox ID="FA_SR_NO" runat="server" CssClass="radinput" text="0" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                   
                 <telerik:GridTemplateColumn SortExpression="AGENT_ID" DataField="AGENT_ID" HeaderText="Agent Name">          
                 <HeaderStyle Width = "85px" />             
                        <ItemTemplate>
                            <asp:TextBox ID="AGENT_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>     
                  <telerik:GridTemplateColumn SortExpression="TOUR_ID" DataField="TOUR_ID" HeaderText="Tour Id" visible="false" >                       
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>    
                    <telerik:GridTemplateColumn SortExpression="BOOKING_ID" DataField="BOOKING_ID" HeaderText="Booking Id" visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOREIGN_CURR_AGENT_ID" DataField="FOREIGN_CURR_AGENT_ID" HeaderText="MTA">
                    <HeaderStyle Width = "30px" />
                        <ItemTemplate>
                            <asp:TextBox ID="FOREIGN_CURR_AGENT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="PAYMENT_CURR_CODE" DataField="PAYMENT_CURR_CODE" HeaderText="Currency Code">
                    <HeaderStyle Width = "90px" />
                        <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_CURR_CODE" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="PAYMENT_MODE_NAME" DataField=" PAYMENT_MODE_NAME" HeaderText="▼ Payment Mode">
                    <HeaderStyle Width = "90px" />
                         <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_MODE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="REC_CHEQUE_DD_NO" DataField="REC_CHEQUE_DD_NO" HeaderText="Cheque/DD No">
                    <HeaderStyle Width = "80px" />
                        <ItemTemplate>
                            <asp:TextBox ID="REC_CHEQUE_DD_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="AMOUNT" DataField="AMOUNT" HeaderText="Amount">
                    <HeaderStyle Width = "50px" />
                        <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="SERVICE_CHARGE" DataField="SERVICE_CHARGE" HeaderText="Service Charge">
                    <HeaderStyle Width = "90px" />
                        <ItemTemplate>
                            <asp:TextBox ID="SERVICE_CHARGE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="BANK_NAME" DataField="BANK_NAME" HeaderText="▼ Bank Name">
                    <HeaderStyle Width = "85px" />
                        <ItemTemplate>
                            <asp:TextBox ID="BANK_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="BANK_BRANCH_NAME" DataField="BANK_BRANCH_NAME" HeaderText="Bank Branch Name">
                    <HeaderStyle Width = "120px" />
                        <ItemTemplate>
                            <asp:TextBox ID="BANK_BRANCH_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="RECEIPT_NO" DataField="RECEIPT_NO" HeaderText="Receipt No">
                    <HeaderStyle Width = "80px" />
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="RECEIPT_DATE" DataField="RECEIPT_DATE" HeaderText="Receipt Date">
                    <HeaderStyle Width = "85px" />
                        <ItemTemplate>
                            <asp:TextBox ID="RECEIPT_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="USER_NAME" DataField="USER_NAME" HeaderText="User Name">
                     <HeaderStyle Width = "85px" />
                      <ItemTemplate>
                            <asp:TextBox ID="USER_NAME" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Branch Name">
                    <HeaderStyle Width = "95px" />
                        <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" readonly="true" Style="background-color: LightBlue" text="H.O" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATUS_NAME" DataField="STATUS_NAME" HeaderText="Payment Status">
                    <HeaderStyle Width = "95px" />
                        <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn  HeaderText="Signature Password" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                        <ItemTemplate>
                            <asp:TextBox ID="Signature" runat="server" CssClass="radinput" TextMode="password"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="Document">
                    <ItemStyle CssClass="ItemAlign" Width="100px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploadphoto" runat="server" Text="Document" onClientclick="openuploadnewphoto()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>                                       
                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="ForeignAdded(this,event);">
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
                <ClientEvents OnCommand="radgridforeignagent_Command" OnRowSelected="radgridforeignagent_RowSelected" OnRowDblClick="AgentRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                    <asp:LinkButton ID="lbAddContact" runat="server" Text="Add Another Agent Payment Detail"
                        OnClientClick="AddNewDetail();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
