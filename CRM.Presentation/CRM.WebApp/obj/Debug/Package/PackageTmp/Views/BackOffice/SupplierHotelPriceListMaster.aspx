<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierHotelPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierHotelPriceListMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="cphIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
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
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/SupplierHotelPrice.js"></script>
        <script type="text/javascript">
         (function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm) {
                    prm.add_endRequest(
                function (sender, args) {
                    // Any code you want here 

                    if (args.get_error() && args.get_error().name === 'Sys.WebForms.PageRequestManagerServerErrorException') {
                        args.set_errorHandled(args._error.httpStatusCode == 0);
                    }
                });
                }
            })(); 
            </script>
         <script type="text/javascript">
             function pageLoad() {
                 HotelPriceTableView = $find("<%= radgridsupplierHotelPrice.ClientID %>").get_masterTableView();

                 //CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(HotelPriceTableView.get_currentPageIndex() * HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                 HotelPriceCommand = "Load";
                 RoomInventoryCommand = "Load";

                 if (HotelPriceTableView.PageSize = 10) {
                     CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                 }
                 else if (HotelPriceTableView.PageSize > 10) {
                     CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                 }
                 else if (HotelPriceTableView.PageSize > 20) {
                     CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                 }
             }
             function delCustomer() {

                 CRM.WebApp.webservice.SupplierHotelPriceListWebService.delHotelPrice(SUPPLIER_HOTEL_PRICE_LIST_ID);
                 CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);

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
             function CopyData() {

                 CRM.WebApp.webservice.SupplierHotelPriceListWebService.CopyData(SUPPLIER_HOTEL_PRICE_LIST_ID);
                 var masterTable = $find("<%= radgridsupplierHotelPrice.ClientID %>").get_masterTableView();
                 masterTable.rebind();
             }
             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addnewHotelPrice(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                
                 ary[1] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TYPE_NAME").value;

                 ary[2] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SINGLE_ROOM_RATE").value;
                 ary[3] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("DOUBLE_ROOM_RATE").value;
                 ary[4] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_ADULT_RATE").value;
                 //ary[5] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 //ary[6] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[7] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[8] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                 ary[9] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 //ary[10] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_NAME").value;
                 ary[11] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_CWB_COST").value;
                 ary[12] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_CNB_COST").value;
                 ary[13] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                 ary[14] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
                 ary[15] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SURCHARGE").value;
                 ary[16] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_DEFAULT").value;
                 ary[17] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SURCHARGE_UNIT").value;
                 ary[18] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TRIPLE_ROOM_RATE").value;
                 ary[19] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS").value;
                 //ary[20] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_ISSUE_SERVICE_VOUCHER").value;

                 ary[21] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_IN_AMOUNT").value;
                 ary[22] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[23] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[24] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[25] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[26] = HotelPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_HOTEL_PRICE_LIST_ID;


                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                 if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                 if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                 if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                 if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
                 if (ary[18] == "" || ary[18] == 'null') ary[18] = 0;

                 //                 if (ary[5] != 0 && ary[6] != 0) {

                 //                     alert("You Cant Enter Both Margin Or Margin in[%]");

                 //                 }
                 //                 else if (ary[5] == 0 && ary[6] == 0) {
                 //                     alert("Enter Either Margin Or Margin in[%]");
                 //                 }
                 //                 else {

                 if (ary[2] != 0 && ary[3] != 0) {
                     if (ary[4] == 0 && ary[18] == 0 || ary[4] != 0 && ary[18] == 0 || ary[4] == 0 && ary[18] != 0) {

                         try {

                             CRM.WebApp.webservice.SupplierHotelPriceListWebService.InsertUpdateHotelPrice(ary);
                             CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
                             alert('Record Save Successfully');

                         }
                         catch (e) {
                             alert('Wrong Data Inserted');
                         }
                     }
                     else if (ary[4] != 0 && ary[18] != 0) {
                     
                         alert("Enter Either Extra Adult Rate Or Triple Room Rate");
                     }
                     //        else {

                     //           // alert("You Cant Enter Both Extra Adult Rate Or Triple Room Rate");
                     //        }
                 }
                 else {
                     alert('Enter Single Room Rate And Double Room Rate.');
                 }
             }

             //}
             //             function OnCallBack(results, userContext, sender) {

             //                 if (results == "currency") {

             //                     alert("Invalid Currency Name.");
             //                 }
             //                 else if (results == "Restaurant") {

             //                     alert("Invalid Restaurant Name.");
             //                 }
             //                 else if (results == "Payment") {

             //                     alert("Invalid Payment Terms.");
             //                 }
             //                 else if (results == "status") {

             //                     alert("Invalid Status.")
             //                 }
             //                 else if (results == "agent") {
             //                     alert("Invalid Agent Name")
             //                 }
             //                 else if (results == "Room")
             //                 {
             //                     alert("Invalid Room Type")
             //                 }
             //                 else if (results == "IsQuote")
             //                 {
             //                     alert("Invalid Is Quote Name")
             //                 }
             //                 else if (results == 0) {

             //                     alert('Record Save Successfully');
             //                 }
             //                 else {

             //                     alert('This Record All Ready Exist.');
             //                 }

             //             }
             function showpnl() {
                 document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                 document.getElementById('<%=Button4.ClientID %>').style.display = "";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "none";
             }
             function SearchResult() {

                 document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "";
                 document.getElementById('<%=Button4.ClientID %>').style.display = "none";
                 CHAIN_NAME = $("#ctl00_cphPageContent_txtfname").val();
                 scity = $("#ctl00_cphPageContent_txtCity").val();

                 CRM.WebApp.webservice.SupplierHotelPriceListWebService.GetHotelPrice(0, HotelPriceTableView.get_pageSize(), HotelPriceTableView.get_sortExpressions().toString(), HotelPriceTableView.get_filterExpressions().toDynamicLinq(), CHAIN_NAME, scity, UpdateSupplierHotelPrice);
             }
             function openuploadnewdoc() {

                 window.open('HotelPriceDocument.aspx?key=' + SUPPLIER_HOTEL_PRICE_LIST_ID);
             }
             
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Hotel Price List Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var TF = "../../webservice/autocomplete.ashx?key=GET_TRUE_FALSE_DROPDOWN";
            var RoomType = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var paymentterms = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_TERMS_FOR_AUTOSEARCH";
            var chainname = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_AUTOSEARCH_FOR_HOTEL_PRICE_LIST";
            var agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            var surchargeunit = "../../webservice/autocomplete.ashx?key=GET_SURCHARGE_UNIT_HOTEL_PRICELIST_AUTOSEARCH";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var status = "../../webservice/autocomplete.ashx?key=GET_PRICE_LIST_STATUS";

            $("#ctl00_cphPageContent_txtHotelName").autocomplete(chainname);

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(RoomType);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_AUTO_BOOK_ON_LOW_INVENTORY").autocomplete(a);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_CUSTOMER_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_IS_DEFAULT").autocomplete(TF);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_SURCHARGE_UNIT").autocomplete(surchargeunit);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_STATUS").autocomplete(status);
                $("#ctl00_cphPageContent_radgridsupplierHotelPrice_ctl00_ctl" + i + "_TO_ISSUE_SERVICE_VOUCHER").autocomplete(a);
            }
            $("#ctl00_cphPageContent_txtfname").autocomplete(chainname);
            $("#ctl00_cphPageContent_txtCity").autocomplete(city);
        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Hotel Detail?'))return false; delCustomer(); return false;"
                    Text="Delete" runat="server" />
            </td>
            <td>
                <asp:Button ID="btncopy" OnClientClick="CopyData();" CssClass="button" Style="float: right;
                    margin-right: 10px; color: black; font-weight: bold;" Text="Copy & Create New"
                    runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridsupplierHotelPrice" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_HOTEL_PRICE_LIST_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="3000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_HOTEL_PRICE_LIST_ID" DataField="SUPPLIER_HOTEL_PRICE_LIST_ID" HeaderText="SUPPLIER_HOTEL_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_HOTEL_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_SR_NO" DataField="SUPPLIER_SR_NO" HeaderText="SUPPLIER_SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Hotel Name" >
                     
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="CITY" >
                     
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" readOnly="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FROM_DATE" DataField="FROM_DATE" HeaderText="From Date" >
                     
                          <ItemTemplate>
                            <asp:TextBox ID="FROM_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TO_DATE" DataField="TO_DATE" HeaderText="To Date" >
                     
                          <ItemTemplate>
                            <asp:TextBox ID="TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ROOM_TYPE_NAME" DataField="ROOM_TYPE_NAME" HeaderText="Room Type">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="SURCHARGE" DataField="SURCHARGE" HeaderText="SurCharge">
                   
                          <ItemTemplate>
                            <asp:TextBox ID="SURCHARGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="SURCHARGE_UNIT" DataField="SURCHARGE_UNIT" HeaderText="Surcharge Unit">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="SURCHARGE_UNIT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="SINGLE_ROOM_RATE" DataField="SINGLE_ROOM_RATE" HeaderText="Single Room Rate">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="SINGLE_ROOM_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DOUBLE_ROOM_RATE" DataField="DOUBLE_ROOM_RATE" HeaderText="Double Room Rate">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="DOUBLE_ROOM_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="TRIPLE_ROOM_RATE" DataField="TRIPLE_ROOM_RATE" HeaderText="Triple Room Rate">
                    <HeaderStyle Width="65px" />
                          <ItemTemplate>
                            <asp:TextBox ID="TRIPLE_ROOM_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EXTRA_ADULT_RATE" DataField="EXTRA_ADULT_RATE" HeaderText="Extra Adult Rate" >
                   
                          <ItemTemplate>
                            <asp:TextBox ID="EXTRA_ADULT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EXTRA_CWB_COST" DataField="EXTRA_CWB_COST" HeaderText="Extra CWB Rate">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="EXTRA_CWB_COST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EXTRA_CNB_COST" DataField="EXTRA_CNB_COST" HeaderText="Extra CNB Rate">
                     
                          <ItemTemplate>
                            <asp:TextBox ID="EXTRA_CNB_COST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT" DataField="MARGIN_AMOUNT" HeaderText="Margin" visible="false">
                   
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="Margin(%)" visible="false">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUSTOMER_NAME" DataField="CUSTOMER_NAME" HeaderText="Agent Name" Visible="false">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_DEFAULT" DataField="IS_DEFAULT" HeaderText="Is Default Quote?">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="IS_DEFAULT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="STATUS" DataField="STATUS" HeaderText="Status">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="STATUS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TO_ISSUE_SERVICE_VOUCHER" DataField="TO_ISSUE_SERVICE_VOUCHER" HeaderText="To Issue Service Voucher?" visible="false">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="TO_ISSUE_SERVICE_VOUCHER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_MARGIN_IN_AMOUNT" DataField="A_MARGIN_IN_AMOUNT" HeaderText="A Margin Amount">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_MARGIN_IN_AMOUNT" DataField="A_PLUS_MARGIN_IN_AMOUNT" HeaderText="A+ Margin Amount">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_PLUS_MARGIN_IN_AMOUNT" DataField="A_PLUS_PLUS_MARGIN_IN_AMOUNT" HeaderText="A++ Margin Amount">
                    <HeaderStyle Width="65px" />
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_PLUS_MARGIN_IN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A Margin Amount[%]">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A+ Margin Amount[%]">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="A++ Margin Amount[%]">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="DOCUMENT">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploaddoc" runat="server" Text="DOC" onClientclick="openuploadnewdoc()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewHotelPrice(this,event);">
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
                <ClientEvents OnCommand="radgridsupplierHotelPrice_Command" OnRowSelected="radgridsupplierHotelPrice_RowSelected" OnRowDblClick="HotelPriceList"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Search" OnClientClick="showpnl();"
                    Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button" />
                <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                    display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Employee Detail:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkrel" runat="server" onClick="check();" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfname" runat="server" Text="Hotel Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
