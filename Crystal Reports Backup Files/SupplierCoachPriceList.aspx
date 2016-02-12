﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierCoachPriceList.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierCoachPriceList" %>

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
        <script type="text/javascript" src="../Shared/Javascripts/CoachPriceList.js"></script>

         <script type="text/javascript">
             function pageLoad() {
                 CoachPriceTableView = $find("<%= radgridsupplierCoachPrice.ClientID %>").get_masterTableView();
                 //CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(CoachPriceTableView.get_currentPageIndex() * CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
                 CoachPriceCommand = "Load";

                 if (CoachPriceTableView.PageSize = 10) {
                     CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
                 }
                 else if (CoachPriceTableView.PageSize > 10) {
                     CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
                 }
                 else if (CoachPriceTableView.PageSize > 20) {
                     CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
                 }
             }
             function delCustomer() {

                 
                 CRM.WebApp.webservice.SupplierCoachPriceListWebService.delCoachPrice(COACH_PRICE_LIST_ID);
                 CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
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

                 CRM.WebApp.webservice.SupplierCoachPriceListWebService.CopyData(COACH_PRICE_LIST_ID);
                 var masterTable = $find("<%= radgridsupplierCoachPrice.ClientID %>").get_masterTableView();
                 masterTable.rebind();
             }
             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addewCoachPrice(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 //ary[1] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_FROM_DATE").value;
                 //ary[2] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_TO_DATE").value;
                 ary[3] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("COACH_NAME").value;
                 ary[4] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_NAME").value;
                 ary[5] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 // ary[6] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_RATE").value;
                 ary[7] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_RATE").value;
                 // ary[8] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_DISCOUNT").value;
                 //ary[9] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_DISCOUNT").value;
                 ary[10] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 ary[11] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[12] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[13] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                 ary[14] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_SEATS").value;
                 //ary[15] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT").value;
                 //ary[16] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[17] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                 ary[18] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                 ary[19] = CoachPriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.COACH_PRICE_LIST_ID;
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

                         CRM.WebApp.webservice.SupplierCoachPriceListWebService.InsertUpdateCoachPrice(ary);
                         CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);

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
//                     alert("Invalid Agent Name")
//                 }
//                 else if (results == "City") {
//                     alert("Invalid City Name")
//                 }
//                 else if (results == "Coach") {
//                     alert("Invalid Coach Name.")
//                 }
//                 else if (results == 0) {

//                     alert('Record Save Successfully');
//                 }
//                 else {

//                     alert('This Record All Ready Exist.');
//                 }

//             }
             function openuploadnewdoc() {

                 window.open('SupplierCoachPriceDocument.aspx?key=' + COACH_PRICE_LIST_ID);
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

                 CRM.WebApp.webservice.SupplierCoachPriceListWebService.GetCoachPriceList(0, CoachPriceTableView.get_pageSize(), CoachPriceTableView.get_sortExpressions().toString(), CoachPriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateCoachPrice);
             }
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Coach PriceList Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {


            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var paymentterms = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_TERMS_FOR_AUTOSEARCH";
            var agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            var chainname = "../../webservice/autocomplete.ashx?key=GET_COACH_COMPANY_NAME_AUTOSEARCH_BY_SUPPLIER_TYPE";
            var coach = "../../webservice/autocomplete.ashx?key=GET_COACH_NAME_FOR_AUTO_SEARCH";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            //var car = "../../webservice/autocomplete.ashx?key=GET_CAR_NAME_FOR_AUTOSEARCH";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_CUSTOMER_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_COACH_NAME").autocomplete(coach);
                $("#ctl00_cphPageContent_radgridsupplierCoachPrice_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);

            }
            $("#ctl00_cphPageContent_txtCity").autocomplete(city);

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Coach Price Detail?'))return false; delCustomer(); return false;"
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
                <telerik:radgrid id="radgridsupplierCoachPrice" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CAR_PRICE_LIST_MASTER_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2200px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>
                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression ="COACH_NAME" DataField="COACH_NAME" HeaderText="Coach Name">
                          <ItemTemplate>
                            <asp:TextBox ID="COACH_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_RESTAURANT_PRICE_LIST_ID" DataField="SUPPLIER_RESTAURANT_PRICE_LIST_ID" HeaderText="SUPPLIER_RESTAURANT_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_RESTAURANT_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
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
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Coach Company Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
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

                    <telerik:GridTemplateColumn SortExpression ="NO_OF_SEATS" DataField="NO_OF_SEATS" HeaderText="No Of Seats">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_SEATS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="FIT_RATE" DataField="FIT_RATE" HeaderText="Rate Per Person">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT" DataField="MARGIN_AMOUNT" HeaderText="Margin">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" Text="0"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="Margin In[%]">
                          <ItemTemplate>
                            <asp:TextBox ID="MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" Text="0"></asp:TextBox>
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

                    <telerik:GridTemplateColumn SortExpression ="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="GIT Margin Amount[%]" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
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
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addewCoachPrice(this,event);">
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
                <ClientEvents OnCommand="radgridsupplierCoachPrice_Command" OnRowSelected="radgridsupplierCoachPrice_RowSelected"  OnRowDblClick="CoachPriceList"/>
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
                                <asp:Label ID="lblfname" runat="server" Text="Coach Company Name:"></asp:Label>
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
                                <asp:Label ID="Label6" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="250px"></asp:TextBox>
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
