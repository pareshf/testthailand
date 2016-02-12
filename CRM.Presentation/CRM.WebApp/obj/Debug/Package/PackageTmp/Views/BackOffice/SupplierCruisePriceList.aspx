<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierCruisePriceList.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierCruisePriceList" %>

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

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/SupplierCruisePrice.js"></script>

         <script type="text/javascript">
             function pageLoad() {
                 CruisePriceTableView = $find("<%= radgridsupplierCruisePrice.ClientID %>").get_masterTableView();

                 //CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(CruisePriceTableView.get_currentPageIndex() * CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
                 CruisePriceCommand = "Load";

                 if (CruisePriceTableView.PageSize = 10) {
                     CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
                 }
                 else if (CruisePriceTableView.PageSize > 10) {
                     CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
                 }
                 else if (CruisePriceTableView.PageSize > 20) {
                     CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
                 }

             }
             function delCustomer() {
                 
                 CRM.WebApp.webservice.SupplierCruisePriceWebService.delCruisePrice(SUPPLIER_CRUISE_PRICE_LIST_ID);
                 CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
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

                 CRM.WebApp.webservice.SupplierCruisePriceWebService.CopyData(SUPPLIER_CRUISE_PRICE_LIST_ID);
                 var masterTable = $find("<%= radgridsupplierCruisePrice.ClientID %>").get_masterTableView();
                 masterTable.rebind();
             }

             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addewCruisePrice(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];
                 
                 //ary[1] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_FROM_DATE").value;
                 //ary[2] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_TO_DATE").value;
                 ary[3] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CATEGORY_CODE").value;
                 ary[4] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("DECK_NO").value;
                 ary[5] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_VIEW").value;
                 //ary[6] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_RATE").value;
                 //ary[7] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_RATE").value;
                 //ary[8] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_DISCOUNT").value;
                 //ary[9] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_DISCOUNT").value;
                 ary[10] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 ary[11] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[12] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[13] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                 //ary[14] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("AUTO_BOOK_ON_LOW_INVENTORY").value;
                 ary[15] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_NAME").value;
                 ary[16] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 //ary[17] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CRUISE_DESC").value;
                 //ary[18] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT").value;
                 //ary[19] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[17] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SINGLE_ROOM_RATE").value;
                 ary[18] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("DOUBLE_ROOM_RATE").value;
                 ary[19] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_ADULT_RATE").value;
                 ary[20] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_CWB_COST").value;
                 ary[21] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EXTRA_CNB_COST").value;
                 ary[22] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                 ary[23] = CruisePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_CRUISE_PRICE_LIST_ID;
                 //                 for (i = 0; i < 16; i++) {
                 //                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                 //                 }
                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
                 if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
                 if (ary[10] != 0 && ary[11] != 0) {

                     alert("You Cant Enter Both Margin Or Margin in[%]");

                 }
                 else if (ary[10] == 0 && ary[11] == 0) {
                     alert("Enter Either Margin Or Margin in[%]");
                 }
                 else {
                     try {
                         CRM.WebApp.webservice.SupplierCruisePriceWebService.InsertUpdateCruisePrice(ary);
                         CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(0, CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);

                         alert('Record Save Successfully');

                     }
                     catch (e) {
                         alert('Wrong Data Inserted');
                     }
                 }
             }
//             function OnCallBack(results, userContext, sender) {

//                 
//                 if (results == "currency") {

//                     alert("Invalid Currency Name.");
//                 }
//                 else if (results == "Coach_Company") {

//                     alert("Invalid Restaurant Name.");
//                 }
//                 else if (results == "Payment") {

//                     alert("Invalid Payment Terms.");
//                 }
//                 else if (results == "agent") {
//                 alert("Invalid Agent Name");
//                 }
//                 else if (results == "Cabin_category") {
//                     alert("Invalid Cabin Category.")
//                 }
//                 else if (results == 0) {

//                     alert('Record Save Successfully');
//                 }
//                 else {

//                     alert('This Record All Ready Exist.');
//                 }

//             }
             function openuploadnewdoc() {

                 window.open('SupplierCruisePriceDocument.aspx?key=' + SUPPLIER_CRUISE_PRICE_LIST_ID);
             }
             function addewCruiseCabin(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];


                 ary[1] = CabinInventoryTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_CABINS_PURCHASED").value;
                 ary[2] = CabinInventoryTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_CABINS_AVAILABLE").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_CRUISE_CABIN_INVENTORY_ID;
                 //                 for (i = 0; i < 16; i++) {
                 //                     if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                 //                 }
                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 try {
                     CRM.WebApp.webservice.SupplierCruisePriceWebService.InsertUpdateCruiseCabinCategory(ary);
                     CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCruiseCabin(SUPPLIER_CRUISE_PRICE_LIST_ID, updateCruiseCabinInventory);

                     alert('Record Save Successfully');

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }

             }
             function showpnl() {
                 document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                 document.getElementById('<%=Button4.ClientID %>').style.display = "";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "none";
             }
             function SearchResult() {

                 document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                 document.getElementById('<%=Button3.ClientID %>').style.display = "";
                 document.getElementById('<%=Button4.ClientID %>').style.display = "none";
                 //scomapany = $("#ctl00_cphPageContent_txtcompany").val();
                 scity = $("#ctl00_cphPageContent_txtCity").val();

                 //scommode = $("#ctl00_cphPageContent_txtcommunicatinmode").val();
                 //suniquetid = $("#ctl00_cphPageContent_txtcust_id").val();
                 sfname = $("#ctl00_cphPageContent_txtfname").val();

                 CRM.WebApp.webservice.SupplierCruisePriceWebService.GetCrusiePriceList(CruisePriceTableView.get_currentPageIndex() * CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_pageSize(), CruisePriceTableView.get_sortExpressions().toString(), CruisePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierCruisePrice);
             }
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Cruise PriceList Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var paymentterms = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_TERMS_FOR_AUTOSEARCH";
            var agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            var chainname = "../../webservice/autocomplete.ashx?key=GET_CRUISE_COMPANY_NAME_AUTOSEARCH_BY_SUPPLIER_TYPE";
            var cruisecabin = "../../webservice/autocomplete.ashx?key=GET_CRUISE_CABIN_CATEGORY";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_AUTO_BOOK_ON_LOW_INVENTORY").autocomplete(a);
                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_CUSTOMER_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridsupplierCruisePrice_ctl00_ctl" + i + "_CATEGORY_CODE").autocomplete(cruisecabin);
            }

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Cruise Price Detail?'))return false; delCustomer(); return false;"
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
                <telerik:radgrid id="radgridsupplierCruisePrice" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_HOTEL_PRICE_LIST_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2200px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Cruise Company Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_CRUISE_PRICE_LIST_ID" DataField="SUPPLIER_CRUISE_PRICE_LIST_ID" HeaderText="SUPPLIER_CRUISE_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_CRUISE_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
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
                     
                    <telerik:GridTemplateColumn SortExpression ="CATEGORY_CODE" DataField="CATEGORY_CODE" HeaderText="Cabin Category">
                          <ItemTemplate>
                            <asp:TextBox ID="CATEGORY_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="DECK_NO" DataField="DECK_NO" HeaderText="Deck No">
                          <ItemTemplate>
                            <asp:TextBox ID="DECK_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CRUISE_VIEW" DataField="CRUISE_VIEW" HeaderText="Cruise View">
                          <ItemTemplate>
                            <asp:TextBox ID="CRUISE_VIEW" runat="server" CssClass="radinput" ></asp:TextBox>
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
                    <telerik:GridTemplateColumn SortExpression ="EXTRA_ADULT_RATE" DataField="EXTRA_ADULT_RATE" HeaderText="Extra Adult Rate">
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
                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT" DataField="MARGIN_AMOUNT" HeaderText="Margin">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="Margin in[%]">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_FROM_DATE" DataField="EFFECTIVE_FROM_DATE" HeaderText="Validity From Date" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_FROM_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_TO_DATE" DataField="EFFECTIVE_TO_DATE" HeaderText="Validity To Date" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_NAME" DataField="CUSTOMER_NAME" HeaderText="Agent Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GIT_RATE" DataField="GIT_RATE" HeaderText="GIT Rate" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FIT_RATE" DataField="FIT_RATE" HeaderText="FIT Rate" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FIT_DISCOUNT" DataField="FIT_DISCOUNT" HeaderText="FIT Discount" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_DISCOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GIT_DISCOUNT" DataField="GIT_DISCOUNT" HeaderText="GIT Discount" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_DISCOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GIT_MARGIN_AMOUNT" DataField="GIT_MARGIN_AMOUNT" HeaderText="GIT Margin Amount" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="GIT Margin Amount In[%]" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CRUISE_DESC" DataField="CRUISE_DESC" HeaderText="Cruise Desc" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CRUISE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="Doc">
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
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addewCruisePrice(this,event);">
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
                <ClientEvents OnCommand="radgridsupplierCruisePrice_Command" OnRowSelected="radgridsupplierCruisePrice_RowSelected" OnRowDblClick="CruisePriceList"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
    </table>
    <%--<div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Cruise Cabin Inventory"></asp:Literal>
    </div>
    <br />
    <br />--%>
    <%--<table>
        <tr>
            <td>
                <telerik:radgrid id="radgridsupplierCruiseCabinInventory" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="25" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_CRUISE_CABIN_INVENTORY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_CRUISE_CABIN_INVENTORY_ID" DataField="SUPPLIER_CRUISE_CABIN_INVENTORY_ID" HeaderText="SUPPLIER_CRUISE_CABIN_INVENTORY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_CRUISE_CABIN_INVENTORY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_CRUISE_PRICE_LIST_ID" DataField="SUPPLIER_CRUISE_PRICE_LIST_ID" HeaderText="SUPPLIER_CRUISE_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_CRUISE_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NO_OF_CABINS_PURCHASED" DataField="NO_OF_CABINS_PURCHASED" HeaderText="NO OF CABINS PURCHASED">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_CABINS_PURCHASED" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="NO_OF_CABINS_AVAILABLE" DataField="NO_OF_CABINS_AVAILABLE" HeaderText="NO OF CABINS AVAILABLE">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_CABINS_AVAILABLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                     </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addewCruiseCabin(this,event);">
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
                <ClientEvents OnCommand="radgridsupplierCruiseCabinInventory_Command" OnRowSelected="radgridsupplierCruiseCabinInventory_RowSelected" OnRowDblClick="CruisePriceList"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
    </table>--%>
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
                            <%--<td>
                                    <asp:Label ID="cust_id" runat="server" Text="Customer Id:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcust_id" runat="server" Width="250px"></asp:TextBox>
                                </td>--%>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Employee Detail:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkrel" runat="server" onClick="check();" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfname" runat="server" Text="Cruise Company Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                                    <td>
                                        <asp:Label ID="lbllaname" runat="server" Text="To Place:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlname" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                        <%--<tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtemail" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                        <%--<tr>
                                    <td>
                                        <asp:Label ID="lblmobile" runat="server" Text="Mobile No.:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                        <%--<tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Telephone No.:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttelephon" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                             </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="City:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="250px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Communication Mode:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcommunicatinmode" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
