<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="TransferPackagePriceList.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TransferPackagePriceList" %>

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
        <script type="text/javascript" src="../Shared/Javascripts/TransferPackage.js"></script>

         <script type="text/javascript">

             function pageLoad() {
                 TransferpackageTableView = $find("<%= radgridTransferPackagePriceList.ClientID %>").get_masterTableView();
                 TransferdetailTableView = $find("<%= radgridtransferdetail.ClientID %>").get_masterTableView();
                 TransferpackageCommand = "Load";
                 TransferdetailCommand = "Load";
               

                 if (TransferpackageTableView.PageSize = 10) {
                     CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);
                 }
                 else if (TransferpackageTableView.PageSize > 10) {
                     CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);
                 }
                 else if (TransferpackageTableView.PageSize > 20) {
                     CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);
                 }

             }
             function delCustomer() {


                 CRM.WebApp.webservice.TransferPackageWebService.delTransferPackage(TRANSFER_PACKAGE_PRICE_ID);
                 CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);

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

                 CRM.WebApp.webservice.TransferPackageWebService.CopyData(TRANSFER_PACKAGE_PRICE_ID);
                 var masterTable = $find("<%= radgridTransferPackagePriceList.ClientID %>").get_masterTableView();
                 masterTable.rebind();

             }
             function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;

             }
             function addewTransferPackage(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[1] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_PLACE").value;

                 ary[2] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_FROM_DATE").value;
                 ary[3] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("EFFECTIVE_TO_DATE").value;
                 //ary[4] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT").value;
                 //ary[5] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[6] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                 ary[7] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                 //ary[8] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_NAME").value;
                 ary[9] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_PACKAGE_NAME").value;
                 ary[10] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_NAME").value;
                 ary[11] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS").value;
                // ary[12] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
                 ary[13] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_IN_AMOUNT").value;
                 ary[14] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[15] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_IN_AMOUNT").value;
                 ary[16] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[17] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;
                 ary[18] = TransferpackageTableView.get_dataItems()[currentRowIndex - 1].findElement("A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE").value;


                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TRANSFER_PACKAGE_PRICE_ID;

                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                 if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;


                 //                 if (ary[4] != 0 && ary[5] != 0) {

                 //                     alert("You Cant Enter Both Margin Or Margin in[%]");

                 //                 }
                 //                 else if (ary[4] == 0 && ary[5] == 0) {
                 //                     alert("Enter Either Margin Or Margin in[%]");
                 //                 }
                 //else {



                 try {
                     CRM.WebApp.webservice.TransferPackageWebService.InsertUpdateTransferPackage(ary);
                     CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(0, TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);

                     alert('Record Save Successfully');

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }


                 //}

             }
//             function OnCallBack(results, userContext, sender) {

//                 
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
//                 else if (results == "Package") {
//                     alert("Invalid Package Name")
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

                 scity = $("#ctl00_cphPageContent_txtCity").val();
                 sfname = $("#ctl00_cphPageContent_txtfname").val();
                 slname = "";
                 CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackage(TransferpackageTableView.get_currentPageIndex() * TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_pageSize(), TransferpackageTableView.get_sortExpressions().toString(), TransferpackageTableView.get_filterExpressions().toDynamicLinq(), scity, sfname, slname, UpdateTransferPackage);
             }
             function addnewPlace(sender, eventArgs) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[1] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_PLACE").value;
                 ary[2] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_PLACE").value;
                 ary[3] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("SIGHT_SEEING_PACKAGE_NAME").value;
                 ary[4] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("FLAG").value;
                 ary[5] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT_SIC_RATE").value;
                 ary[6] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_SIC_RATE").value;
                 ary[7] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("ADULT_PVT_RATE").value;
                 ary[8] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CHILD_PVT_RATE").value;
                 ary[9] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("SICRATE_PER_PERSON").value;
                 ary[10] = TransferdetailTableView.get_dataItems()[currentRowIndex - 1].findElement("PVTRATE_PER_PERSON").value;

                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TRANSFER_PACKAGE_FROM_TO_DETAIL_ID;
                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 try {
                     CRM.WebApp.webservice.TransferPackageWebService.InsertUpdateTransferPackagedetail(ary,d,OnCallBack);
                     CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackagedetail(TRANSFER_PACKAGE_PRICE_ID, UpdateTransferdetail);

                     //alert('Record Save Successfully');

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }
             }
             function OnCallBack(results, userContext, sender) {

                 
                 if (results == "From_Place") {

                     alert("Invalid From Palce.");
                 }
                 else if (results == "To_Place") {

                     alert("Invalid To Place.");
                 }
                 else if (results == "Sight_Package") {

                     alert("Invalid Sight Package.");
                 }
                 else if (results == "flag") {
                     alert("Invalid Flag.")
                 }
                 else if (results == 0) {

                     alert('Record Save Successfully');
                 }
                 else {

                     alert('This Record All Ready Exist.');
                 }

             }
             function AddNewTransferDetail() {

                 CRM.WebApp.webservice.TransferPackageWebService.InsertNewTransferDetail(TRANSFER_PACKAGE_PRICE_ID);
                 CRM.WebApp.webservice.TransferPackageWebService.GetTransferPackagedetail(TRANSFER_PACKAGE_PRICE_ID, UpdateTransferdetail);
             }
             function onrowclick(sender, args) {

                 currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
                 TRANSFER_PACKAGE_PRICE_ID = args.get_gridDataItem()._dataItem.TRANSFER_PACKAGE_PRICE_ID;
                 FIT_PACKAGE_NAME = args.get_gridDataItem()._dataItem.FIT_PACKAGE_NAME;


             }
             function onrowto(sender, args) {
                 currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
                 TRANSFER_PACKAGE_FROM_TO_DETAIL_ID = args.get_gridDataItem()._dataItem.TRANSFER_PACKAGE_FROM_TO_DETAIL_ID;
                 FROM_PLACE = args.get_gridDataItem()._dataItem.FROM_PLACE;
                 TO_PLACE = args.get_gridDataItem()._dataItem.TO_PLACE;
                 Show();              
             }
             function Show() {
                 document.getElementById('<%=Button6.ClientID %>').style.display = "";
             }
             function Rate() {
                 window.location = "TransferServicePrice.aspx?transid=" + TRANSFER_PACKAGE_FROM_TO_DETAIL_ID + "&name=" + FROM_PLACE + " - " + TO_PLACE + " Of " + FIT_PACKAGE_NAME;
             }
         </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Transfer Package Price List Master"></asp:Literal>
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
            var supplier = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_AUTOSEARCH";
            //var supplier = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_AUTOSEARCH_FOR_TRANSFER_PACKAGE";
            var Coach_Car = "../../webservice/autocomplete.ashx?key=GET_COACH_CAR_FOR_TRANSFER_PACKAGE_AUTOSEARCH";
            var Sic_pvt = "../../webservice/autocomplete.ashx?key=GET_SIC_PVT_FOR_TRANSFER_PACKAGE_AUTOSEARCH";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var agent = "../../webservice/autocomplete.ashx?key=GET_AGENT_NAME_SUPPLIER_CAR_PRICE_LISTFOR_AUTOSEARCH";
            var fitpackage = "../../webservice/autocomplete.ashx?key=GET_FIT_PACKAGE_FOR_TRANSFER_PACKAGE";
            var chainname = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_AUTOSEARCH_FOR_TRANSFER_PACKAGE";
            var packagename = "../../webservice/autocomplete.ashx?key=GET_PACKAGE_NAME_AUTOSEARCH";
            var arival_departure = "../../webservice/autocomplete.ashx?key=GET_ARRIVAL_DEPARTURE_FLAG_DROPDOWN";
            var sight_package = "../../webservice/autocomplete.ashx?key=GET_PACKAGE_NAME_AUTOSEARCH";
            var place = "../../webservice/autocomplete.ashx?key=GET_PALCE_FOR_TRANSFER_DETAIL";
            var status = "../../webservice/autocomplete.ashx?key=GET_PRICE_LIST_STATUS";
            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(supplier);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_COACH_CAR_FLAG").autocomplete(Coach_Car);
                
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_CUSTOMER_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_FIT_PACKAGE_NAME").autocomplete(fitpackage);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(chainname);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_STATUS").autocomplete(status);
                $("#ctl00_cphPageContent_radgridtransferdetail_ctl00_ctl" + i + "_SUPPLIER_NAME").autocomplete(packagename);
                $("#ctl00_cphPageContent_radgridtransferdetail_ctl00_ctl" + i + "_FLAG").autocomplete(arival_departure);
                $("#ctl00_cphPageContent_radgridtransferdetail_ctl00_ctl" + i + "_SIGHT_SEEING_PACKAGE_NAME").autocomplete(sight_package);
                $("#ctl00_cphPageContent_radgridtransferdetail_ctl00_ctl" + i + "_FROM_PLACE").autocomplete(place);
                $("#ctl00_cphPageContent_radgridtransferdetail_ctl00_ctl" + i + "_TO_PLACE").autocomplete(place);
                $("#ctl00_cphPageContent_radgridTransferPackagePriceList_ctl00_ctl" + i + "_TO_ISSUE_SERVICE_VOUCHER").autocomplete(a);
               

            }
            $("#ctl00_cphPageContent_txtCity").autocomplete(fitpackage);

        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Transfer Package Detail?'))return false; delCustomer(); return false;"
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
                <telerik:radgrid id="radgridTransferPackagePriceList" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="TRANSFER_PACKAGE_PRICE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="TRANSFER_PACKAGE_PRICE_ID" DataField="TRANSFER_PACKAGE_PRICE_ID" HeaderText="TRANSFER_PACKAGE_PRICE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TRANSFER_PACKAGE_PRICE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PRICE_LIST_ID" DataField="PRICE_LIST_ID" HeaderText="PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COACH_CAR_PRICE_LIST_ID" DataField="COACH_CAR_PRICE_LIST_ID" HeaderText="COACH_CAR_PRICE_LIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="COACH_CAR_PRICE_LIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City" visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_NAME" DataField="SUPPLIER_NAME" HeaderText="Supplier">
                    
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FROM_PLACE" DataField="FROM_PLACE" HeaderText="Trasfer Package From-To">
                  
                          <ItemTemplate>
                            <asp:TextBox ID="FROM_PLACE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_NAME" DataField="FIT_PACKAGE_NAME" HeaderText="FIT PACKAGE" >
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
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

                     <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_FROM_DATE" DataField="EFFECTIVE_FROM_DATE" HeaderText="Validity From Date">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_FROM_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EFFECTIVE_TO_DATE" DataField="EFFECTIVE_TO_DATE" HeaderText="Validity to Date">
                          <ItemTemplate>
                            <asp:TextBox ID="EFFECTIVE_TO_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addewTransferPackage(this,event);">
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
                <ClientEvents OnCommand="radgridTransferPackagePriceList_Command" OnRowSelected="radgridTransferPackagePriceList_RowSelected" OnRowDblClick="TransferPackagePriceList" OnRowClick="onrowclick"/>
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
                                <asp:Label ID="lblfname" runat="server" Text="Trasfer Package From-To:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbllaname" runat="server" Text="To Place:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlname" runat="server" Width="250px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="FIT Package:"></asp:Label>
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
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Transfer Package Details"></asp:Literal>
    </div>
    <br />
    <br />
        <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridtransferdetail" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="TRANSFER_PACKAGE_FROM_TO_DETAIL_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="TRANSFER_PACKAGE_FROM_TO_DETAIL_ID" DataField="TRANSFER_PACKAGE_FROM_TO_DETAIL_ID" HeaderText="BANK_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TRANSFER_PACKAGE_FROM_TO_DETAIL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="FROM_PLACE" DataField="FROM_PLACE" HeaderText="From Place">
                          <ItemTemplate>
                            <asp:TextBox ID="FROM_PLACE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="TO_PLACE" DataField="TO_PLACE" HeaderText="To Place">
                          <ItemTemplate>
                            <asp:TextBox ID="TO_PLACE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SIGHT_SEEING_PACKAGE_NAME" DataField="SIGHT_SEEING_PACKAGE_NAME" HeaderText="Sight Package">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHT_SEEING_PACKAGE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADULT_SIC_RATE" DataField="ADULT_SIC_RATE" HeaderText="Adult SIC Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="ADULT_SIC_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHILD_SIC_RATE" DataField="CHILD_SIC_RATE" HeaderText="Child SIC Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="CHILD_SIC_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADULT_PVT_RATE" DataField="ADULT_PVT_RATE" HeaderText="Adult PVT Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="ADULT_PVT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHILD_PVT_RATE" DataField="CHILD_PVT_RATE" HeaderText="Child PVT Rate">
                          <ItemTemplate>
                            <asp:TextBox ID="CHILD_PVT_RATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SICRATE_PER_PERSON" DataField="SICRATE_PER_PERSON" HeaderText="SIC Rate Per Person">
                          <ItemTemplate>
                            <asp:TextBox ID="SICRATE_PER_PERSON" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PVTRATE_PER_PERSON" DataField="PVTRATE_PER_PERSON" HeaderText="PVT Rate Per Person">
                          <ItemTemplate>
                            <asp:TextBox ID="PVTRATE_PER_PERSON" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FLAG" DataField="FLAG" HeaderText="FLAG">
                          <ItemTemplate>
                            <asp:TextBox ID="FLAG" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewPlace(this,event);">
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
                <ClientEvents OnCommand="radgridtransferdetail_Command" OnRowSelected="radgridtransferdetail_RowSelected" OnRowDblClick="TransferPackageDetail" OnRowClick="onrowto"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="lbAddContact" runat="server" Text="Add New Detail" OnClientClick="AddNewTransferDetail();"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button6" runat="server" Text="Transfer Rate" Width="150px" Style="float: left; margin-right: 10px;
                    display: none; color: black;" CssClass="button" OnClientClick="Rate();" />
            </td>
        </tr>
    </table>
</asp:Content>
