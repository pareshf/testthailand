<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="NewBookingInformation.aspx.cs" Inherits="CRM.WebApp.Views.Sales.NewBookingInformation" %>

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
        
         <script type ="text/javascript" src="../Shared/Javascripts/BookingInformationMasterGrid.js"></script>

         
         
         <script type ="text/javascript">

             function pageLoad() {
                 
                 PrerequisiteTabelView = $find("<%= radgridprerequisitengmaster.ClientID %>").get_masterTableView();
                 PrerequisiteCommandName = "Load";
                 
             }
            
             function PrerequisiteDetails(sender) {
                  var value;
                  value = sender.value;
                  PrerequisiteTabelView = $find("<%= radgridprerequisitengmaster.ClientID %>").get_masterTableView();
                  PrerequisiteCommandName = "Load";
                  CRM.WebApp.webservice.BookingInformationWebService.GetPrerequisiteDetails(0, PrerequisiteTabelView.get_pageSize(), PrerequisiteTabelView.get_sortExpressions().toString(), PrerequisiteTabelView.get_filterExpressions().toDynamicLinq(),value,updateprerequisiteGrid);
                  
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

                         currentTextBox.value = args.get_newDate().format('MM/dd/yyyy');
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



             function newrowadded(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 for (var i = 0; i < 41; i++) {
                     if (i < 10)
                         i = '0' + i;
                   //  debugger;

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_VALID_VISA') {

                         ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_VALID_PASSPORT') {

                         ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_TITLE') {

                         ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_REL_NAME') {

                         ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_REL_SURNAME') {

                         ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_REL_MOBILE') {

                         ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_REL_EMAIL') {

                         ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_ADDRESS_LINE1') {

                         ary[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_ADDRESS_LINE2') {

                         ary[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_COUNTRY') {

                         ary[10] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_STATE') {

                         ary[11] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_CITY') {

                         ary[12] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_PINCODE') {

                         ary[13] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_PHONE') {

                         ary[14] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_OFFICE_MOBILE') {

                         ary[15] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_ADDRESS_LINE1') {

                         ary[16] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_ADDRESS_LINE2') {

                         ary[17] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_COUNTRY') {

                         ary[18] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_STATE') {

                         ary[19] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_CITY') {

                         ary[20] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_PINCODE') {

                         ary[21] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_RES_PHONE') {

                         ary[22] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_EMERGENCY_NAME') {

                         ary[23] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMERGENCY_ADDRESS_LINE1') {

                         ary[24] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMERGENCY_ADDRESS_LINE2') {

                         ary[25] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMMERGENCY_COUNTRY') {

                         ary[26] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMMERGENCY_STATE') {

                         ary[27] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMERGENCY_CITY') {

                         ary[28] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMERGNECY_PINCODE') {

                         ary[29] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }
                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMMERGENCY_PHONE') {

                         ary[30] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_EMMERGENCY_MOBILE') {

                         ary[31] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_ACCOUNT_CODE') {

                         ary[32] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_NO') {

                         ary[33] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_PRINTED_NAME') {

                         ary[34] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_ISSUE_DATE') {

                         ary[35] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_EXPIRY_DATE') {

                         ary[36] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_ISSUE_PLACE') {

                         ary[37] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_PASSPORT_ISSUE_COUNTRY') {

                         ary[38] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_CUST_BIRTH_DATE') {

                         ary[39] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_MEAL_NAME') {

                         ary[40] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }

                     if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl04_NATIONALITY_NAME') {

                         ary[41] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value;
                     }
                     
                 }

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
                     if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
                     if (ary[15] == "" || ary[15] == 'null') ary[15] = 0;
                     if (ary[16] == "" || ary[16] == 'null') ary[16] = 0;
                     if (ary[17] == "" || ary[17] == 'null') ary[17] = 0;
                     if (ary[18] == "" || ary[18] == 'null') ary[18] = 0;
                     if (ary[19] == "" || ary[19] == 'null') ary[19] = 0;
                     if (ary[20] == "" || ary[20] == 'null') ary[20] = 0;
                     if (ary[21] == "" || ary[21] == 'null') ary[21] = 0;
                     if (ary[22] == "" || ary[22] == 'null') ary[22] = 0;
                     if (ary[23] == "" || ary[23] == 'null') ary[23] = 0;
                     if (ary[24] == "" || ary[24] == 'null') ary[24] = 0;
                     if (ary[25] == "" || ary[25] == 'null') ary[25] = 0;
                     if (ary[26] == "" || ary[26] == 'null') ary[26] = 0;
                     if (ary[27] == "" || ary[27] == 'null') ary[27] = 0;
                     if (ary[28] == "" || ary[28] == 'null') ary[28] = 0;
                     if (ary[29] == "" || ary[29] == 'null') ary[29] = 0;
                     if (ary[30] == "" || ary[30] == 'null') ary[30] = 0;
                     if (ary[31] == "" || ary[31] == 'null') ary[31] = 0;
                     if (ary[32] == "" || ary[32] == 'null') ary[32] = 0;
                     if (ary[33] == "" || ary[33] == 'null') ary[33] = 0;
                     if (ary[34] == "" || ary[34] == 'null') ary[34] = 0;
                     if (ary[35] == "" || ary[35] == 'null') ary[35] = 0;
                     if (ary[36] == "" || ary[36] == 'null') ary[36] = 0;
                     if (ary[37] == "" || ary[37] == 'null') ary[37] = 0;
                     if (ary[38] == "" || ary[38] == 'null') ary[38] = 0;
                     if (ary[39] == "" || ary[39] == 'null') ary[39] = 0;
                     if (ary[40] == "" || ary[40] == 'null') ary[40] = 0;
                     if (ary[41] == "" || ary[41] == 'null') ary[41] = 0;
                     if (ary[42] == "" || ary[42] == 'null') ary[42] = 0;
                     if (ary[43] == "" || ary[43] == 'null') ary[43] = 0;
                     if (ary[44] == "" || ary[44] == 'null') ary[44] = 0;
                     if (ary[45] == "" || ary[45] == 'null') ary[45] = 0;
                     if (ary[46] == "" || ary[46] == 'null') ary[46] = 0;
                     if (ary[47] == "" || ary[47] == 'null') ary[47] = 0;
                     if (ary[48] == "" || ary[48] == 'null') ary[48] = 0;
                     if (ary[49] == "" || ary[49] == 'null') ary[49] = 0;
                     if (ary[50] == "" || ary[50] == 'null') ary[50] = 0;
                     if (ary[51] == "" || ary[51] == 'null') ary[51] = 0;

                     ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.INQUIRY_ID;
                     ary[42] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PREREQUISITE_ID;
                     ary[43] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;
                     ary[44] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.STATE_ID;
                     ary[45] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CITY_ID;
                     ary[46] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.NATIONALITY_ID;
                     ary[47] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                     ary[48] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.MEAL_ID;
                     ary[49] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                     ary[50] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.COUNTRY_ID;
                     ary[51] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;

                     try {
                            CRM.WebApp.webservice.BookingInformationWebService.InsertUpdateBookingPrerequisite(ary);
                            CRM.WebApp.webservice.BookingInformationWebService.GetPrerequisiteDetails(0, PrerequisiteTabelView.get_pageSize(), PrerequisiteTabelView.get_sortExpressions().toString(), PrerequisiteTabelView.get_filterExpressions().toDynamicLinq(), value, updateprerequisiteGrid);
                            var masterTable = $find("<%= radgridprerequisitengmaster.ClientID %>").get_masterTableView();
                            //masterTable.rebind();
                            alert('Record Save Successfully');
                        // CRM.WebApp.webservice.BookingInformationWebService.InsertUpdateBookingPrerequisite()
                     }
                     catch (e) {
                         alert('Wrong Data Inserted');
                     }        
             }

    </script>
    </telerik:radcodeblock>


        <div class="pageTitle" style="float: left">
            <asp:Literal ID="lblPageTask" runat="server" Text="TOUR BOOKING MASTER"></asp:Literal>
        </div>
        <br />
        <br />
        <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
        <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
        <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

        <script type="text/javascript">

            $(document).ready(function () {
                debugger;
                var res_country = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COUNTRY_AUTOSEARCH_NEW";
                var res_state = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_STATE_AUTOSEARCH";
                var res_city = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CITY_AUTOSEARCH";
                var office_country = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COUNTRY_AUTOSEARCH_NEW";
                var office_state = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_STATE_AUTOSEARCH";
                var office_city = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CITY_AUTOSEARCH";
                var emergency_country = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COUNTRY_AUTOSEARCH_NEW";
                var emergency_state = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_STATE_AUTOSEARCH";
                var emergency_city = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CITY_AUTOSEARCH";
                var title = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TITLE";
                var meal = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_MEAL_AUTOSEARCH";
                var nationality = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_NATIONALITY_AUTOSEARCH";


                for (var i = 1; i < 55; i++) {
                    if (i < 10)
                        i = '0' + i;
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_RES_COUNTRY").autocomplete(res_country);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_RES_STATE").autocomplete(res_state);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_RES_CITY").autocomplete(res_city);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_OFFICE_COUNTRY").autocomplete(office_country);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_OFFICE_STATE").autocomplete(office_state);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_OFFICE_CITY").autocomplete(office_city);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_EMMERGENCY_COUNTRY").autocomplete(emergency_country);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_EMMERGENCY_STATE").autocomplete(emergency_state);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_EMERGENCY_CITY").autocomplete(emergency_city);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_CUST_TITLE").autocomplete(title);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_MEAL_NAME").autocomplete(meal);
                    $("#ctl00_cphPageContent_radgridprerequisitengmaster_ctl00_ctl" + i + "_NATIONALITY_NAME").autocomplete(nationality);
                }

            });
     </script>



    <!--  Inquiry NO Insertion -->
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal6" runat="server" Text="Step1 - PreRequisite"></asp:Literal>
            <br />
        </div>
        <br />
        <br />

        <br/>
        <br/>
        <table>
         <tr>
            <td>
                  <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                     color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Task?'))return false; deleteCurrent(); return false;"
                     Text="Delete" runat="server" />
            </td>
         </tr>
        </table>
        <br />
        <br />
        <table>
            <tr style=" text-align: center">
                <td>
                    <asp:Label ID="lblinquiry" runat="server" Text="Inquiry No." CssClass="RadMenu" BackColor="LightGray"></asp:Label>
                </td>
                <td>
                   <asp:TextBox ID="txtINQUIRY_ID" runat="server" TabIndex="1" Width="<%$appSettings:TextBoxWidth%>"  onblur="PrerequisiteDetails(this);" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>

        <br />
        <br />

    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900" maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>

    <%--"<% BOOKING PREREQUISITE GRID %>"--%>

    <div id = "radgridmaster">
        
    
        <telerik:radgrid id ="radgridprerequisitengmaster" runat = "server" allowpaging = "true" allowmultirowselection = "false"
            allowsorting = "True" pagesize = "10" itemstyle-wrap="false" enableembeddedskins="false"
            allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="" AllowMultiColumSorting="true" EditMode ="InPlace" Width="4500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               
               <Columns>
                    <%-- template column for raw data editing result --%>
                    
                    <telerik:GridTemplateColumn SortExpression ="SELECT" DataField="SELECT" HeaderText=" Select">
                        <ItemTemplate>
                            <asp:CheckBox id = "SELECT" runat ="server" CssClass="radinput" ></asp:CheckBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                    
                    <telerik:GridTemplateColumn SortExpression ="INQUIRY_ID" DataField="INQUIRY_ID" HeaderText="INQUIRY_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                    
                    <telerik:GridTemplateColumn SortExpression ="PREREQUISITE_ID" DataField="PREREQUISITE_ID" HeaderText="PREREQUISITE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="PREREQUISITE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TITLE_ID" DataField="TITLE_ID" HeaderText="TITLE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="TITLE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_SRNO" DataField="CUST_REL_SRNO" HeaderText="CUST_REL_SRNO" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="STATE_ID" DataField="STATE_ID" HeaderText="STATE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="STATE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_ID" DataField="CITY_ID" HeaderText="CITY_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CITY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NATIONALITY_ID" DataField="NATIONALITY_ID" HeaderText="NATIONALITY_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="NATIONALITY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MEAL_ID" DataField="MEAL_ID" HeaderText="MEAL_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="MEAL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="VALID_VISA" DataField="VALID_VISA" HeaderText="Valid Visa">
                        <ItemTemplate>
                            <asp:TextBox ID="VALID_VISA" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="VALID_PASSPORT" DataField="VALID_PASSPORT" HeaderText="Valid Passport">
                        <ItemTemplate>
                            <asp:TextBox ID="VALID_PASSPORT" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_TITLE" DataField="CUST_TITLE" HeaderText="Title">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_TITLE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="REL_NAME" DataField="REL_NAME" HeaderText="First Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="REL_SURNAME" DataField="REL_SURNAME" HeaderText="Last name ">
                        <ItemTemplate>
                            <asp:TextBox ID="REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE" HeaderText="Mobile No">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_EMAIL" DataField="CUST_EMAIL" HeaderText="Email">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_ADDRESS_LINE1" DataField="OFFICE_ADDRESS_LINE1" HeaderText="Office Address Line 1">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_ADDRESS_LINE2" DataField="OFFICE_ADDRESS_LINE2" HeaderText="Office Address Line 2">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_COUNTRY" DataField="OFFICE_COUNTRY" HeaderText="Country ">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="OFFICE_STATE" DataField="OFFICE_STATE" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_CITY" DataField="OFFICE_CITY" HeaderText="City">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_CITY" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_PINCODE" DataField="OFFICE_PINCODE" HeaderText="Pin Code">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_PHONE" DataField="OFFICE_PHONE" HeaderText="Office Phone No">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFICE_MOBILE" DataField="OFFICE_MOBILE" HeaderText="Office Mobile No">
                        <ItemTemplate>
                            <asp:TextBox ID="OFFICE_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_ADDRESS_LINE1" DataField="RES_ADDRESS_LINE1" HeaderText="Residence Address Line 1">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_ADDRESS_LINE1" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_ADDRESS_LINE2" DataField="RES_ADDRESS_LINE2" HeaderText="Residence Address Line 2">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_ADDRESS_LINE2" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_COUNTRY" DataField="RES_COUNTRY" HeaderText="Country ">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_COUNTRY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="RES_STATE" DataField="RES_STATE" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_CITY" DataField="RES_CITY" HeaderText="City">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_CITY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_PINCODE" DataField="RES_PINCODE" HeaderText="Pin Code">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_PINCODE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="RES_PHONE" DataField="RES_PHONE" HeaderText="Residence Phone No">
                        <ItemTemplate>
                            <asp:TextBox ID="RES_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_EMERGENCY_NAME" DataField="CUST_EMERGENCY_NAME" HeaderText="Emergency Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_EMERGENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMERGENCY_ADDRESS_LINE1" DataField="EMERGENCY_ADDRESS_LINE1" HeaderText="Emergency Address Line 1">
                        <ItemTemplate>
                            <asp:TextBox ID="EMERGENCY_ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMERGENCY_ADDRESS_LINE2" DataField="EMERGENCY_ADDRESS_LINE2" HeaderText="Emergency Address Line 2">
                        <ItemTemplate>
                            <asp:TextBox ID="EMERGENCY_ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_COUNTRY" DataField="EMMERGENCY_COUNTRY" HeaderText="Country">
                        <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_COUNTRY" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_STATE" DataField="EMMERGENCY_STATE" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_STATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMERGENCY_CITY" DataField="EMERGENCY_CITY" HeaderText="City">
                        <ItemTemplate>
                            <asp:TextBox ID="EMERGENCY_CITY" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMERGNECY_PINCODE" DataField="EMERGNECY_PINCODE" HeaderText="Pin Code">
                        <ItemTemplate>
                            <asp:TextBox ID="EMERGNECY_PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_PHONE" DataField="EMMERGENCY_PHONE" HeaderText="Emergency Phone No">
                        <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_PHONE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMMERGENCY_MOBILE" DataField="EMMERGENCY_MOBILE" HeaderText="Emergency Mobile No">
                        <ItemTemplate>
                            <asp:TextBox ID="EMMERGENCY_MOBILE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ACCOUNT_CODE" DataField="CUST_ACCOUNT_CODE" HeaderText="Accounting Code">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_ACCOUNT_CODE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_NO" DataField="CUST_PASSPORT_NO" HeaderText="Passport No">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_NO" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_PRINTED_NAME" DataField="CUST_PASSPORT_PRINTED_NAME" HeaderText="Passport Printed Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_PRINTED_NAME" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_ISSUE_DATE" DataField="CUST_PASSPORT_ISSUE_DATE" HeaderText="Passport Issue Date">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_ISSUE_DATE" runat="server" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);"  CssClass="radinput"> </asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_EXPIRY_DATE" DataField="CUST_PASSPORT_EXPIRY_DATE" HeaderText="Passport Expiry Date">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_EXPIRY_DATE" runat="server" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);"  CssClass="radinput"> </asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_ISSUE_PLACE" DataField="CUST_PASSPORT_ISSUE_PLACE" HeaderText="Passport Issue Place">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_ISSUE_PLACE" runat="server"  CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_PASSPORT_ISSUE_COUNTRY" DataField="CUST_PASSPORT_ISSUE_COUNTRY" HeaderText="Passport Issue Country ">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_PASSPORT_ISSUE_COUNTRY" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_BIRTH_DATE" DataField="CUST_BIRTH_DATE" HeaderText="Birth Date : (DD/MM/YYYY)">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_BIRTH_DATE" runat="server" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);"  CssClass="radinput" > </asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MEAL_NAME" DataField="MEAL_NAME" HeaderText="Meal">
                        <ItemTemplate>
                            <asp:TextBox ID="MEAL_NAME" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NATIONALITY_NAME" DataField="NATIONALITY_NAME" HeaderText="Nationality">
                        <ItemTemplate>
                            <asp:TextBox ID="NATIONALITY_NAME" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "More" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowadded(this,event);">
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
                <ClientEvents OnCommand="radgridprerequisitengmaster_Command" OnRowSelected="radgridprerequisitengmaster_RowSelected"/>
                <Selecting AllowRowSelect="True"/>
                </ClientSettings>

          </telerik:radgrid>
    </div>

</asp:Content>
