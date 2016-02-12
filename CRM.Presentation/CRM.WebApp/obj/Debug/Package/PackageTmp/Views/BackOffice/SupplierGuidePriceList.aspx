<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierGuidePriceList.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierGuidePriceList" %>

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
        <script type="text/javascript" src="../Shared/Javascripts/SupplierGuidePrice.js"></script>

         <script type="text/javascript">
             function pageLoad() {
                 GuidePriceTableView = $find("<%= radgridsupplierGuidePrice.ClientID %>").get_masterTableView();
                 GuidePriceCommand = "Load";

                 if (GuidePriceTableView.PageSize = 10) {
                     CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);
                 }
                 else if (GuidePriceTableView.PageSize > 10) {
                     CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);
                 }
                 else if (GuidePriceTableView.PageSize > 20) {
                     CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);
                 }
             }
             function delCustomer() {
                 
                 CRM.WebApp.webservice.SupplierGuidePriceListWebService.delGuidePrice(GUIDE_PRICE_LIST_ID);
                 CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);
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

                 CRM.WebApp.webservice.SupplierGuidePriceListWebService.CopyData(GUIDE_PRICE_LIST_ID);
                 var masterTable = $find("<%=radgridsupplierGuidePrice.ClientID %>").get_masterTableView();
                 masterTable.rebind();
             }
             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addewGuidePrice(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 //ary[1] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_FROM_DATE").value;
                 //ary[2] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_TO_DATE").value;
                 //ary[3] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SITE_NAME").value;
                 ary[4] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_NAME").value;
                 ary[5] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 //ary[6] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_RATE").value;
                 ary[7] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_RATE").value;
                 //ary[8] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_DISCOUNT").value;
                 // ary[9] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_DISCOUNT").value;
                 ary[10] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 ary[11] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[12] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[13] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                 ary[14] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                 //ary[15] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT").value;
                 //ary[16] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("GIT_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[18] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                 ary[19] = GuidePriceTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.GUIDE_PRICE_LIST_ID;
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
                         CRM.WebApp.webservice.SupplierGuidePriceListWebService.InsertUpdateGuidePrice(ary, s, OnCallBack);
                         CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(GuidePriceTableView.get_currentPageIndex() * GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);

                         //alert('Record Save Successfully');

                     }
                     catch (e) {
                         alert('Wrong Data Inserted');
                     }
                 }
             }

             function OnCallBack(results, userContext, sender) {

                
                 if (results == "currency") {

                     alert("Invalid Currency Name.");
                 }
                 else if (results == "Guide_Company") {

                     alert("Invalid GuideC ompany Name.");
                 }
                 else if (results == "Payment") {

                     alert("Invalid Payment Terms.");
                 }
                 else if (results == "agent") {
                     alert("Invalid Agent Name");
                 }
                 else if (results == "City") {
                     alert("Invalid City Name.")
                 }
                 else if (results == 0) {

                     alert('Record Save Successfully');
                 }
                 else {

                     alert('This Record All Ready Exist.');
                 }

             }

             function openuploadnewdoc() {

                 window.open('GuidePriceDocument.aspx?key=' + GUIDE_PRICE_LIST_ID);
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

                 scity = $("#ctl00_cphPageContent_txtCity").val();

                 sfname = $("#ctl00_cphPageContent_txtfname").val();

                 CRM.WebApp.webservice.SupplierGuidePriceListWebService.GetGuidePrice(0, GuidePriceTableView.get_pageSize(), GuidePriceTableView.get_sortExpressions().toString(), GuidePriceTableView.get_filterExpressions().toDynamicLinq(), sfname, scity, updateGuidePrice);
             }
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Guide PriceList Master"></asp:Literal>
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
            var chainname = "../../webservice/autocomplete.ashx?key=GET_GUIDE_COMPANY_NAME_AUTOSEARCH_BY_SUPPLIER_TYPE";
            var site = "../../webservice/autocomplete.ashx?key=GET_SITE_NAME_FOR_AUTO_SEARCH";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_CUSTOMER_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_SITE_NAME").autocomplete(site);
                $("#ctl00_cphPageContent_radgridsupplierGuidePrice_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
            }
            $("#ctl00_cphPageContent_txtCity").autocomplete(city);
        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Guide Price List?'))return false; delCustomer(); return false;"
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
                <telerik:radgrid id="radgridsupplierGuidePrice" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="GUIDE_PRICE_LIST_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2200px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Guide Company Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="GUIDE_PRICE_LIST_ID" DataField="GUIDE_PRICE_LIST_ID" HeaderText="GUIDE_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GUIDE_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
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
                            <asp:TextBox ID="MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MARGIN_AMOUNT_IN_PERCENTAGE" DataField="MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="Margin In[%]">
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
                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression ="SITE_NAME" DataField="SITE_NAME" HeaderText="Site Name" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SITE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GIT_RATE" DataField="GIT_RATE" HeaderText="GIT Rate" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GIT_MARGIN_AMOUNT" DataField="GIT_MARGIN_AMOUNT" HeaderText="GIT Margin Amount" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_MARGIN_AMOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" DataField="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" HeaderText="GIT Margin In[%]" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="GIT_MARGIN_AMOUNT_IN_PERCENTAGE" runat="server" CssClass="radinput" ></asp:TextBox>
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
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addewGuidePrice(this,event);">
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
                <ClientEvents OnCommand="radgridsupplierGuidePrice_Command" OnRowSelected="radgridsupplierGuidePrice_RowSelected" OnRowDblClick="GuidePriceList"/>
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
                                <asp:Label ID="lblfname" runat="server" Text="Guide Company Name:"></asp:Label>
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
