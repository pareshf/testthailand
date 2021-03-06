﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierAgentMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.CustomerMasterNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var tour_name;
            var tour_code;
            var tour_type = 0;
            var cust_id;
            var inq_id;
            var empid;
            //var TOURs_NAME = "";
            var CUSTOMER_ID = "";

            function pageLoad() {

                inq_id = getValue("INQ_ID");
                cust_id = getValue("CUST_ID");
                tour_name = getValue("TOUR_SNM");
                tour_code = getValue("TOUR_CODE");

                customermasterTableView = $find("<%= radgridcustomermaster.ClientID %>").get_masterTableView();
                custcontactdetTableView = $find("<%= radgridcontdetmaster.ClientID %>").get_masterTableView();
                customerrelationTableView = $find("<%= radgridcustomerrelation.ClientID %>").get_masterTableView();
                custmsgTableView = $find("<%=radgridcusmsg.ClientID %>").get_masterTableView();
                customerDocumentTableView = $find("<%=radgriddocmaster.ClientID %>").get_masterTableView();
                customermasterCommandName = "Load";


                empid = '<%=Session["empid"]%>';
                /* When Admin Login All Agent Data Will Display and paging in Grid.*/
                if (empid == "121") {

                    if (customermasterTableView.PageSize = 10) {
                        CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
                    }
                    else if (customermasterTableView.PageSize > 10) {
                        CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
                    }
                    else if (customermasterTableView.PageSize > 20) {
                        CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
                    }

                }
                else {
                    /* When Agent Login Only Agent Data will Display */
                    CUSTOMER_ID = '<%=Session["CUST_ID"]%>';
                    CRM.WebApp.webservice.CustomerMasterWebService.GetCustwithID(CUSTOMER_ID, updateCustGrid);
                }
            }



            function RedirectBack() {

                var BOOKING_ID = 0;
                window.location = "BookingFormStep1.aspx?INQ_ID=" + inq_id + "&CUST_ID=" + CUST_ID + "&TOUR_SNM=" + tour_name + "&TOUR_CODE=" + tour_code + "&BOOKING_ID=" + BOOKING_ID;
            }

            function getValue(variable) {

                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    if (pair[0] == variable) {
                        return pair[1];
                    }
                }


            }
            function CustomerAdded(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[2] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
                ary[3] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_SURNAME").value;
                ary[4] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_NAME").value;
                ary[5] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_MOBILE").value;
                ary[6] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_EMAIL").value;
                ary[7] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_TYPE_NAME").value;

                ary[8] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMUNICATION_MODE_NAME").value;
                // ary[9] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMUNICATION_TIME").value;
                ary[10] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_PHONE").value;
                //ary[11] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BRANCH").value;
                //ary[12] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("EMPLOYEE").value;

                //ary[13] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_ID").value;
                //ary[13] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                //ary[14] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_SRNO").value;
                ary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;
                //ary[15] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("RELATION_DESC").value;
                ary[16] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("DESIGNATION_DESC").value;

                //ary[17] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_NAME").value;
                ary[18] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CREDIT_LIMIT").value;
                //ary[19] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ACCOUNTING_CODE").value;
                //ary[20] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_GENDER").value;
                //ary[21] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("MEAL_DESC").value;
                //ary[22] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NATIONALITY_NAME").value;
                ary[23] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_COMPANY_NAME").value;
                ary[24] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[25] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
                ary[26] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PASSWORD").value;
                ary[27] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENT_USABLE_CREDIT_LIMIT").value;
                ary[28] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_BOOKING_AMOUNT_IN_PECENTAGE").value;

                ary[29] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
                ary[30] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_BRNACH").value;
                ary[31] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ACC_NAME").value;
                ary[32] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;
                ary[33] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_CHARGES").value;
                ary[34] = customermasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_CHARGE_APPLICABLE").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_UNQ_ID;

                for (i = 0; i < 26; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                if (ary[6] != '' && ary[26] != '') {

                    if (ary[23] != '') {
                        if (ary[7] != '') {
                            try {

                                var q = window.location.search.substring(1);
                                if (q != "") {

                                    CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateCust(ary, s, OnCallBack);
                                    CRM.WebApp.webservice.CustomerMasterWebService.GetCustwithID(cust_id, updateCustGrid);
                                    //alert('Record Save Successfully');
                                }
                                else {

                                    CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateCust(ary, s, OnCallBack); // add new cust
                                    CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);

                                    //alert('Record Save Successfully');
                                }


                            }
                            catch (e) {
                                alert('Wrong Data Inserted');
                            }
                        }
                        else {

                            alert('Enter Agent Type');
                        }
                    }
                    else {
                        alert('Enter Agent Company Name.')
                    }
                }
                else {

                    alert('Enter User Name and Password.')
                }

            }
            function OnCallBack(results, userContext, sender) {


                if (results == "N") {

                    alert('This Email Already Exist.');
                }
                else if (results == "Y") {

                    alert('Record Save Successfully');
                }
                else if (results == "") {
                    alert('Record Save Successfully');
                }

            }
            function RelationAdded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_UNQ_ID;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                ary[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                ary[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;

                ary[4] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
                ary[5] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_SURNAME").value;
                ary[6] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_NAME").value;
                ary[7] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_MOBILE").value;
                ary[8] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_EMAIL").value;

                ary[9] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_PHONE").value;

                //ary[10] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("RELATION_DESC").value;
                ary[11] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("DESIGNATION_DESC").value;

                //ary[12] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_GENDER").value;
                // ary[13] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("MEAL_DESC").value;
                //ary[14] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("NATIONALITY_NAME").value;

                ary[15] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("PASSWORD").value;
                ary[16] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("ALT_EMAIL").value;
                //ary[17] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CREDIT_LIMIT").value;
                ary[18] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("USER_STATUS_NAME").value;
                //ary[19] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                //ary[21] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("PARENT_CUSTORMER").value;
                ary[21] = CUST_ID;
                ary[22] = SR_NO;
                // ary[24] = customerrelationTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_ACCOUNT").value;
                for (i = 0; i < 21; i++) {
                    if (ary[i] == "" || ary[i] == 'null' || ary[i] == ' ') ary[i] = 0;
                }
                if (ary[8] != '' && ary[15] != '') {
                    try {
                        CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateRelation(ary, s, OnCallBack); // add new cust
                        CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
                        //alert('Record Save Successfully');
                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                else {
                    alert('Enter Email and Password. ');
                }
            }
            function ContactAdded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];


                ary[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                //ary[0] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_ID").value;
                //ary[1] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("RELATION_DESC").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.RELATION_DESC;
                //ary[2] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("SR_NO").value;
                ary[3] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_TYPE_NAME").value;
                ary[4] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_ADDRESS_LINE1").value;
                ary[5] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_ADDRESS_LINE2").value;
                ary[6] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[7] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[8] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[9] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_PINCODE").value;
                ary[10] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_PHONE").value;
                //ary[11] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("EMERGENCY_NAME").value;
                ary[12] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_MOBILE").value;

                ary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                ary[15] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("WEBSITE").value;
                ary[16] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("CHAIN_NAME").value;
                //ary[17] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;
                //ary[18] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_ID;

                //                ary[17] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("VIDEO").value;
                //                ary[18] = custcontactdetTableView.get_dataItems()[currentRowIndex - 1].findElement("LOGO").value;
                for (i = 0; i < 39; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
                }
                //                if (!ary[10] == 0 || !ary[12] == 0) {

                try {
                    CRM.WebApp.webservice.CustomerMasterWebService.InsertUpdateContdet(ary); // add new cust
                    CRM.WebApp.webservice.CustomerMasterWebService.ContdetGrid(CUST_ID, updateCustcontactdet);
                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
                //                }
                //                else {
                //                    alert('Enter Mobile No Or Phone No');
                //                }
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
            function AddNewDetail() {
                CRM.WebApp.webservice.CustomerMasterWebService.InsertNewDetail(CUST_ID, CUST_REL_ID);
                CRM.WebApp.webservice.CustomerMasterWebService.ContdetGrid(CUST_ID, updateCustcontactdet);
            }

            function AddNewRelation() {
                CRM.WebApp.webservice.CustomerMasterWebService.InsertNewRel(CUST_ID, CUST_REL_ID, SR_NO);
                CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
            }
            function showpnl() {
                document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                document.getElementById('<%=Button2.ClientID %>').style.display = "";
                document.getElementById('<%=Button1.ClientID %>').style.display = "none";
            }

            function showpnlsms() {
                document.getElementById('<%=pnlSms.ClientID %>').style.display = "";
                document.getElementById('<%=Button6.ClientID %>').style.display = "";
                document.getElementById('<%=Button5.ClientID %>').style.display = "none";
            }


            function SearchResult() {
                document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                document.getElementById('<%=Button1.ClientID %>').style.display = "";
                document.getElementById('<%=Button2.ClientID %>').style.display = "none";
                scomapany = $("#ctl00_cphPageContent_txtcompany").val();
                scity = $("#ctl00_cphPageContent_txtCity").val();
                scode = ""; //$("#ctl00_cphPageContent_txtcode").val();
                stype = $("#ctl00_cphPageContent_txttype").val();
                sbranch = ""; // $("#ctl00_cphPageContent_txtbranch").val();
                semp = ""; //  $("#ctl00_cphPageContent_txtemp").val();
                scommode = ""; // $("#ctl00_cphPageContent_txtcommunicatinmode").val();
                suniquetid = $("#ctl00_cphPageContent_txtcust_id").val();
                sfname = $("#ctl00_cphPageContent_txtfname").val();
                slname = $("#ctl00_cphPageContent_txtlname").val();
                semail = $("#ctl00_cphPageContent_txtemail").val();
                smob = $("#ctl00_cphPageContent_txtMobile").val();
                stele = $("#ctl00_cphPageContent_txttelephon").val();
                //scheckdate = $("#ctl00_cphPageContent_chkdate").val();
                //scheckdate = $("#ctl00_cphPageContent_txtfrmdate").val();
                //scheckdate = $("#ctl00_cphPageContent_txttodate").val();

                var checking = document.getElementById('<%= chkrel.ClientID %>');

                if (checking.checked == true) {
                    CRM.WebApp.webservice.CustomerMasterWebService.GetCust(customermasterTableView.get_currentPageIndex() * customermasterTableView.get_pageSize(), customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
                    CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
                }
                else {

                    CRM.WebApp.webservice.CustomerMasterWebService.GetCust(0, customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
                }
            }
            function check() {

            }
            function SearchResultSms() {

                document.getElementById('<%= pnlSms.ClientID %>').style.display = "none";
                document.getElementById('<%=Button5.ClientID %>').style.display = "";
                document.getElementById('<%=Button6.ClientID %>').style.display = "none";


            }
            function showpnldoc() {
                document.getElementById('<%=Paneldocument.ClientID %>').style.display = "";
                document.getElementById('<%= Button4.ClientID %>').style.display = "none";
                document.getElementById('<%=Button7.ClientID %>').style.display = "";
            }
            function upload() {
                document.getElementById('<%=Paneldocument.ClientID %>').style.display = "none";
                document.getElementById('<%= Button7.ClientID %>').style.display = "none";
                document.getElementById('<%= Button4.ClientID %>').style.display = "";
            }

            function deleteCurrent() {

                CRM.WebApp.webservice.CustomerMasterWebService.DeleteCustomer(CUST_ID);
                CRM.WebApp.webservice.CustomerMasterWebService.GetCust(customermasterTableView.get_currentPageIndex() * customermasterTableView.get_pageSize(), customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);


            }

            function deleteCurrentSubProg() {

                CRM.WebApp.webservice.CustomerMasterWebService.DeleteContact(SR_NO);
                CRM.WebApp.webservice.CustomerMasterWebService.ContdetGrid(CUST_ID, updateCustcontactdet);
            }

            function deleteCurrentSubProg1() {

                CRM.WebApp.webservice.CustomerMasterWebService.DeleteRel(CUST_REL_SRNO);
                CRM.WebApp.webservice.CustomerMasterWebService.RelGrid(SR_NO, scity, suniquetid, sfname, slname, semail, smob, stele, updateRel);
            }


            function SelectMultiple(sender) {

                customerid = sender.value;
                if (customerid != "0") {
                    CRM.WebApp.webservice.CustomerMasterWebService.GetCustwithID(customerid, updateCustGrid);
                }
            }


            function validatelimit(obj, maxchar) {

                if (this.id) obj = this;

                var remaningChar = maxchar - obj.value.length;
                document.getElementById('<%= Label14.ClientID %>').innerHTML = remaningChar;

                if (remaningChar < 0) {
                    obj.value = obj.value.substring(maxchar, 0);
                    return false;
                }
                else
                { return true; }
            }


            function Redirect() {
                window.location = "CustomerMaster2.aspx?CUST_ID=" + CUST_ID + "&CUST_REL_SRNO=" + CUST_REL_SRNO + "&CUST_REL_ID=" + CUST_REL_ID;
            }
            function openuploadnewphoto() {
                window.open('Documents.aspx?key=' + SR_NO + '&key1=' + CUST_ID);
            }
            function newsitePhotoadded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var a = [];

                a[0] = customerDocumentTableView.get_dataItems()[currentRowIndex - 1].findElement("DOC_NAME").value;
                a[1] = customerDocumentTableView.get_dataItems()[currentRowIndex - 1].findElement("DOC_DESCRIPTION").value;
                a[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_ID;
                a[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                a[4] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;

                for (i = 0; i < 1; i++) {
                    if (a[i] == "" || a[i] == 'null') a[i] == "";
                }
                try {
                    CRM.WebApp.webservice.CustomerMasterWebService.insertupdatedoc(a);
                    CRM.WebApp.webservice.CustomerMasterWebService.doc(CUST_ID, updateCustdocument);
                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function GenerateCode() {

                CRM.WebApp.webservice.CustomerMasterWebService.InsertCode(CUST_COMPANY_NAME, CUST_ID, Agent);
                alert("Code Generated Sucessfully");
            }
            function AddAnotherDocument() {
                CRM.WebApp.webservice.CustomerMasterWebService.insertnewDocument(CUST_ID);
            }
            function getComapnyBankName(sender) {

                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript" src="../Shared/Javascripts/CustomerMasterGridScript.js"></script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageCust" runat="server" Text="Agent Main Account"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            //alert("in ready function")
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var profession = "../../webservice/autocomplete.ashx?key=FETCH_PROFESSION_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var codename = "../../webservice/autocomplete.ashx?key=FETCH_CODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var modename = "../../webservice/autocomplete.ashx?key=FETCH_COMMUNICATION_MODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var Designation = "../../webservice/autocomplete.ashx?key=FETCH_DESIGNATION_FOR_FAR_HOTEL_MASTER";
            var addresstype = "../../webservice/autocomplete.ashx?key=FETCH_ADDRESS_TYPE_FOR_EMPLOYEE_MASTER";
            var Customertype = "../../webservice/autocomplete.ashx?key=FETCH_CUSTOMER_TYPE_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var Branch = "../../webservice/autocomplete.ashx?key=FETCH_BRANCH_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var meal = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_DATA_FOR_BOOKINGMASTER_AUTOSEARCH";
            var nationality = "../../webservice/autocomplete.ashx?key=FETCH_NATIONALITY_FOR_BOOKING_MASTER";
            var relation = "../../webservice/autocomplete.ashx?key=FETCH_RELATION_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var comapnyname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            var gender = "../../webservice/autocomplete.ashx?key=FETCH_GENDER_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var marital = "../../webservice/autocomplete.ashx?key=FETCH_MATERIAL_FOR_EMPLOYEE_MASTER";
            var employee = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var religion = "../../webservice/autocomplete.ashx?key=FETCH_RELIGION_NAME_FOR_CUST_CUSTOMER_MASTER";
            var groupname = "../../webservice/autocomplete.ashx?key=FETCH_GROUPE_NAME_FOR_CUST_CUSTOMER_MASTER";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_NAME";
            var userstatus = "../../webservice/autocomplete.ashx?key=GET_USER_STATUS_FOR_CUSTOMER_RELATION_AUTOSEARCH";
            var paymentterms = "../../webservice/autocomplete.ashx?key=GET_PAYMENT_TERMS_FOR_AUTOSEARCH";
            var parentcustomer = "../../webservice/autocomplete.ashx?key=GET_PARENT_CUSTOMER_FOR_AUTOSEARCH";
            var chain = "../../webservice/autocomplete.ashx?key=GET_CHAIN_NAME_FOR_AUTOSEARCH";
            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var company_bank_branch = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_BRANCH?" + globalvalue;
            var account_name = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_BRANCH_ACCOUNT_NAME?" + globalvalue;
            for (var i = 1; i < 50; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_PAYMENT_TERMS").autocomplete(paymentterms);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_PROFESSION_DESC").autocomplete(profession);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CUST_CODE_NAME").autocomplete(codename);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_COMMUNICATION_MODE_NAME").autocomplete(modename);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_DESIGNATION_DESC").autocomplete(Designation);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CUST_TYPE_NAME").autocomplete(Customertype);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_BRANCH").autocomplete(Branch);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_GROUP_NAME").autocomplete(groupname);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_EMPLOYEE").autocomplete(employee);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_MARITAL_STATUS_NAME").autocomplete(marital);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CUST_REL_GENDER").autocomplete(gender);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(userstatus);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_MEAL_DESC").autocomplete(meal);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_NATIONALITY_NAME").autocomplete(nationality);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_RELATION_DESC").autocomplete(relation);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_RELIGION_NAME").autocomplete(religion);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CUST_REL_PASSPORT_ISSUE_PLACE").autocomplete(city);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_CUST_REL_PASSPORT_ISSUE_COUNTRY").autocomplete(country);
                $("#ctl00_cphPageContent_radgridcontdetmaster_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridcontdetmaster_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridcontdetmaster_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridcontdetmaster_ctl00_ctl" + i + "_ADDRESS_TYPE_NAME").autocomplete(addresstype);
                $("#ctl00_cphPageContent_radgridcontdetmaster_ctl00_ctl" + i + "_RELATION_DESC").autocomplete(relation);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_BANK_CHARGE_APPLICABLE").autocomplete(a);


                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_PROFESSION_DESC").autocomplete(profession);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_DESIGNATION_DESC").autocomplete(Designation);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_MARITAL_STATUS_NAME").autocomplete(marital);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CUST_REL_GENDER").autocomplete(gender);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_MEAL_DESC").autocomplete(meal);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_NATIONALITY_NAME").autocomplete(nationality);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_RELATION_DESC").autocomplete(relation);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_RELIGION_NAME").autocomplete(religion);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CUST_REL_PASSPORT_ISSUE_PLACE").autocomplete(city);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CUST_REL_PASSPORT_ISSUE_COUNTRY").autocomplete(country);

                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CHAIN").autocomplete(chain);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_USER_STATUS_NAME").autocomplete(userstatus);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_PARENT_CUSTORMER").autocomplete(parentcustomer);
                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);

                $("#ctl00_cphPageContent_radgridcustomerrelation_ctl00_ctl" + i + "_IS_ACCOUNT").autocomplete(a);

                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_BANK_BRNACH").autocomplete(company_bank_branch);
                $("#ctl00_cphPageContent_radgridcustomermaster_ctl00_ctl" + i + "_ACC_NAME").autocomplete(account_name);

                // $("#ctl00_cphPageContent_txtcompany").autocomplete(comapnyname);
                $("#ctl00_cphPageContent_txtCity").autocomplete(city);
                $("#ctl00_cphPageContent_txtcode").autocomplete(codename);
                $("#ctl00_cphPageContent_txttype").autocomplete(Customertype);
                $("#ctl00_cphPageContent_txtbranch").autocomplete(Branch);
                $("#ctl00_cphPageContent_txtemp").autocomplete(employee);
                $("#ctl00_cphPageContent_txtcommunicatinmode").autocomplete(modename);
                $("#ctl00_cphPageContent_txtrelation").autocomplete(relation);
            }
        });
    </script>
    <telerik:RadDatePicker ID="RadDatePicker1" Style="display: none;" MinDate="01/01/1900"
        MaxDate="01/01/2100" runat="server">
        <ClientEvents OnDateSelected="dateSelected" />
    </telerik:RadDatePicker>
    <div id="divradmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Agent?'))return false; deleteCurrent(); return false;"
                        Text="Delete Agent" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnglCode" runat="server" CssClass="button" Style="float: right;
                        margin-right: 10px; color: black; font-weight: bold;" Text="Generate GL Code"
                        OnClientClick="GenerateCode();" Visible="false" />
                </td>
                <td>
                    <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="None" ForeColor="White" Height="16px" Width="16px" onfocus="SelectMultiple(this);"
                        Text="0"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="radgridcustomermaster" runat="server" AllowPaging="true" AllowMultiRowSelection="false"
                        AllowSorting="True" PageSize="50" ItemStyle-Wrap="false" EnableEmbeddedSkins="false"
                        AllowAutomaticDeletes="True" AllowAutomaticInserts="True" Width="2500">
                        <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbxselect" runat="server" CssClass="radinput" OnClick="SelectMultiple();">
                                        </asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_UNQ_ID" DataField="CUST_UNQ_ID"
                                    HeaderText="Unique Id" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" Style="background-color: LightBlue"
                                            ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="GL_CODE" DataField="GL_CODE" HeaderText="GL Code"
                                    Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="GL_CODE" runat="server" CssClass="radinput" Style="background-color: LightBlue"
                                            ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_COMPANY_NAME" DataField="CUST_COMPANY_NAME"
                                    HeaderText="Agent Company Name">
                                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_COMPANY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_SURNAME" DataField="CUST_SURNAME"
                                    HeaderText="Agent Surname">
                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_SURNAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_NAME" DataField="CUST_NAME" HeaderText="Agent Name">
                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE"
                                    HeaderText="Mobile">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_EMAIL" DataField="CUST_REL_EMAIL"
                                    HeaderText="Email [User Name]">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_EMAIL" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PASSWORD" DataField="PASSWORD" HeaderText="Password">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="PASSWORD" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_TYPE_NAME" DataField="CUST_TYPE_NAME"
                                    HeaderText="Agent Type">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_TYPE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="REMARKS" DataField="REMARKS" HeaderText="Remarks"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="COMMUNICATION_MODE_NAME" DataField="COMMUNICATION_MODE_NAME"
                                    HeaderText="Communication Mode ">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="COMMUNICATION_MODE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="COMMUNICATION_TIME" DataField="COMMUNICATION_TIME"
                                    HeaderText="Communication Time" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="COMMUNICATION_TIME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PHONE" DataField="CUST_REL_PHONE"
                                    HeaderText="Telephone">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BRANCH" DataField="BRANCH" HeaderText="Branch *"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="BRANCH" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="EMPLOYEE" DataField="EMPLOYEE" HeaderText="Employee"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="EMPLOYEE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_ID" DataField="CUST_REL_ID"
                                    HeaderText="Relation Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="RELATION_DESC" DataField="RELATION_DESC"
                                    HeaderText="Relation With Agent" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="RELATION_DESC" runat="server" CssClass="radinput" Style="background-color: LightBlue"
                                            ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="DESIGNATION_DESC" DataField="DESIGNATION_DESC"
                                    HeaderText="Designation">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="DESIGNATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PROFESSION_DESC" DataField="PROFESSION_DESC"
                                    HeaderText="Profession" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PROFESSION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="GROUP_NAME" DataField="GROUP_NAME" HeaderText="Group Name"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GROUP_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CURRENCY_NAME" DataField="CURRENCY_NAME"
                                    HeaderText="Credit Limit Currency">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CREDIT_LIMIT" DataField="CREDIT_LIMIT"
                                    HeaderText="Credit Limit">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CREDIT_LIMIT" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CURRENT_USABLE_CREDIT_LIMIT" DataField="CURRENT_USABLE_CREDIT_LIMIT"
                                    HeaderText="Current Usable Credit Limit">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CURRENT_USABLE_CREDIT_LIMIT" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BANK_NAME" DataField="BANK_NAME" HeaderText="Bank">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="BANK_NAME" runat="server" CssClass="radinput" onblur="getComapnyBankName(this);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BANK_BRNACH" DataField="BANK_BRNACH"
                                    HeaderText="Branch">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="BANK_BRNACH" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="ACC_NAME" DataField="ACC_NAME" HeaderText="Account Name">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="ACC_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="FIT_BOOKING_AMOUNT_IN_PECENTAGE" DataField="FIT_BOOKING_AMOUNT_IN_PECENTAGE"
                                    HeaderText="FIT Booking Amount %">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="FIT_BOOKING_AMOUNT_IN_PECENTAGE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_ANNUAL_INCOME" DataField="CUST_REL_ANNUAL_INCOME"
                                    HeaderText="Anual Income" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_ANNUAL_INCOME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_NO" DataField="CUST_REL_PASSPORT_NO"
                                    HeaderText="Passport No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_PRINTED_NAME" DataField="CUST_REL_PASSPORT_PRINTED_NAME"
                                    HeaderText="Passport Printed Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_PRINTED_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_DATE" DataField="CUST_REL_PASSPORT_ISSUE_DATE"
                                    HeaderText="Passport Issue Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_DATE" runat="server" CssClass="radinput"
                                            onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_EXPIRY_DATE" DataField="CUST_REL_PASSPORT_EXPIRY_DATE"
                                    HeaderText="Passport Expiry Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_EXPIRY_DATE" runat="server" CssClass="radinput"
                                            onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_PLACE" DataField="CUST_REL_PASSPORT_ISSUE_PLACE"
                                    HeaderText="Passport Issue Place" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_PLACE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_COUNTRY" DataField="CUST_REL_PASSPORT_ISSUE_COUNTRY"
                                    HeaderText="Passport Issue Country" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_COUNTRY" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="ACCOUNTING_CODE" DataField="ACCOUNTING_CODE"
                                    HeaderText="Accounting Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ACCOUNTING_CODE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="MARITAL_STATUS_NAME" DataField="MARITAL_STATUS_NAME"
                                    HeaderText="Marital Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MARITAL_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_MARRIAGE_DATE" DataField="CUST_REL_MARRIAGE_DATE"
                                    HeaderText="Marriage Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_MARRIAGE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="RELIGION_NAME" DataField="RELIGION_NAME"
                                    HeaderText="Religion Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RELIGION_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_BIRTH_DATE" DataField="CUST_BIRTH_DATE"
                                    HeaderText="Birth Date" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_BIRTH_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_CODE_NAME" DataField="CUST_CODE_NAME"
                                    HeaderText="Customer Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_CODE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_GENDER" DataField="CUST_REL_GENDER"
                                    HeaderText="Gender" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_GENDER" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="MEAL_DESC" DataField="MEAL_DESC" HeaderText="Meal"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MEAL_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="NATIONALITY_NAME" DataField="NATIONALITY_NAME"
                                    HeaderText="Nationality" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="NATIONALITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="STATUS_NAME" DataField="STATUS_NAME"
                                    HeaderText="Status">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PAYMENT_TERMS" DataField="PAYMENT_TERMS"
                                    HeaderText="Payment Terms">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BANK_CHARGE_APPLICABLE" DataField="BANK_CHARGE_APPLICABLE"
                                    HeaderText="Bank Charge Applicable ?">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="BANK_CHARGE_APPLICABLE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="BANK_CHARGES" DataField="BANK_CHARGES"
                                    HeaderText="Bank Charge [USD]">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="BANK_CHARGES" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_ID" DataField="CUST_ID" HeaderText="Customer ID"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_SRNO" DataField="CUST_REL_SRNO"
                                    HeaderText="Relation Sr No." Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                                    <HeaderStyle Width="25px" />
                                    <ItemTemplate>
                                        <a id="A1" href="#" style="border: 0px solid #fff; text-decoration: none; height: 14px;
                                            width: 14px;" onkeydown="CustomerAdded(this,event);">&raquo; </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                            <KeyboardNavigationSettings AllowActiveRowCycle="true" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                            <ClientEvents OnCommand="radgridcustomermaster_Command" OnRowSelected="radgridcustomermaster_RowSelected"
                                OnRowDblClick="CustomerMasterRowClick" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <div id="div1">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClientClick="showpnl();"
                        Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button" />
                    <asp:Button ID="Button2" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                        display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="Send SMS" OnClientClick="showpnlsms();"
                        Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button"
                        OnClick="Button5_Click" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" Text="Upload Doc" OnClientClick="showpnldoc();"
                        Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button"
                        Visible="false" />
                    <asp:Button ID="Button7" runat="server" Text="Cancel" Style="float: right; margin-right: 10px;
                        display: none; color: black;" CssClass="button" OnClientClick="upload();" />
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
                                    <asp:Label ID="Label4" runat="server" Text="Agent Company Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcompany" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="cust_id" runat="server" Text="Unique Id:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcust_id" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Sub Account:"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkrel" runat="server" onClick="check();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfname" runat="server" Text="Agent SurName:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbllaname" runat="server" Text="Agent Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlname" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemail" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblmobile" runat="server" Text="Mobile No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobile" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Telephone No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttelephon" runat="server" Width="250px"></asp:TextBox>
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
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Type:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttype" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Code:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcode" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <%-- <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Branch:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbranch" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkdate" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Apply Date Filter" Visible="false"></asp:Label>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Employee:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemp" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text="From Date" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfrmdate" runat="server" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                                        onkeydown="parseDate(this, event);" Width="150px" Visible="false"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Communication Mode:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcommunicatinmode" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="To Date" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttodate" runat="server" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                                        onkeydown="parseDate(this, event);" Width="150px" Visible="false"></asp:TextBox>
                                </td>
                            </tr>--%>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlSms" runat="server" Width="500px" Style="display: none">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="radgridcusmsg" runat="server" AllowPaging="true" AllowMultiRowSelection="false"
                                        AllowSorting="True" PageSize="10" ItemStyle-Wrap="false" EnableEmbeddedSkins="false"
                                        AllowAutomaticDeletes="True" AllowAutomaticInserts="True">
                                        <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                                            <Columns>
                                                <telerik:GridTemplateColumn SortExpression="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="CUSTOMER_NAME" DataField="CUSTOMER_NAME"
                                                    HeaderText="Customer Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUSTOMER_NAME" runat="server" CssClass="radinput" Style="background-color: LightBlue"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE"
                                                    HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                                            <KeyboardNavigationSettings AllowActiveRowCycle="true" />
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                                            <ClientEvents OnCommand="radgridcusmsg_Command" OnRowSelected="radgridcusmsg_RowSelected" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" MaxLength="162"
                                        Width="400px" onkeyup="validatelimit(this,162)" Height="70px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Left:" CssClass="lblstyle"></asp:Label>
                                    <asp:Label ID="Label14" runat="server" Text="162" CssClass="lblstyle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button6" runat="server" Text="Send SMS Now" Style="margin-right: 10px;
                                        display: none; color: black;" CssClass="button" OnClientClick="SearchResultSms();"
                                        OnClick="Button6_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Panel ID="Paneldocument" runat="server" Style="display: none">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="radgriddocmaster" runat="server" AllowPaging="false" AllowMultiRowSelection="false"
                                        AllowSorting="True" PageSize="50" ItemStyle-Wrap="false" EnableEmbeddedSkins="false"
                                        AllowAutomaticDeletes="True" AllowAutomaticInserts="True">
                                        <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumSorting="true" EditMode="InPlace"
                                            Width="400px">
                                            <RowIndicatorColumn>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <telerik:GridTemplateColumn SortExpression="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="CUST_UNQ_ID" DataField="CUST_UNQ_ID"
                                                    HeaderText="CUST_UNQ_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="SR_NO"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="CUST_REL_SRNO" DataField="CUST_REL_SRNO"
                                                    HeaderText="CUST_REL_SRNO" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_NAME" DataField="DOC_NAME" HeaderText="Doc Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_DESCRIPTION" DataField="DOC_DESCRIPTION"
                                                    HeaderText="Doc Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_DESCRIPTION" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_FILE_NAME" DataField="DOC_FILE_NAME"
                                                    HeaderText="Document File Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_FILE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_DATE" DataField="DOC_DATE" HeaderText="Document Date"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_FILE_PATH" DataField="DOC_FILE_PATH"
                                                    HeaderText="Document File Path" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_FILE_PATH" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="DOC_FILE_SIZE" DataField="DOC_FILE_SIZE"
                                                    HeaderText="Document File size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DOC_FILE_SIZE" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false"
                                                    ShowSortIcon="false" HeaderText="Document">
                                                    <ItemStyle CssClass="ItemAlign" Width="100px" />
                                                    <HeaderStyle Width="65px" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="uploadphoto" runat="server" Text="Document" OnClientClick="openuploadnewphoto()" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                                                    <HeaderStyle Width="25px" />
                                                    <ItemTemplate>
                                                        <a id="A4" href="#" style="border: 0px solid #fff; text-decoration: none; height: 14px;
                                                            width: 14px;" onkeydown="newsitePhotoadded(this,event);">&raquo; </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                                            <KeyboardNavigationSettings AllowActiveRowCycle="true" />
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                                            <ClientEvents OnCommand="radgriddocmaster_Command" OnRowSelected="radgriddocmaster_RowSelected" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <asp:LinkButton ID="lbAddsitephoto" runat="server" Text="Add Another Document" OnClientClick="AddAnotherDocument();"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal3" runat="server" Text="Agent Contact Details"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="div2">
        <table>
            <tr>
                <td>
                    <asp:Button ID="DeleteSubProg" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete Contact Detail?'))return false;deleteCurrentSubProg(); return false;"
                        Text="Delete Contact Detail" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="radgridcontdetmaster" runat="server" AllowPaging="true" AllowMultiRowSelection="false"
                        AllowSorting="True" PageSize="10" ItemStyle-Wrap="false" EnableEmbeddedSkins="false"
                        AllowAutomaticDeletes="True" AllowAutomaticInserts="True" Width="1500">
                        <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                            <Columns>
                                <telerik:GridTemplateColumn SortExpression="CUST_ID" DataField="CUST_ID" HeaderText="Customer ID"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_ID" DataField="CUST_REL_ID"
                                    HeaderText="CustomerRelation Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="RELATION_DESC" DataField="RELATION_DESC"
                                    HeaderText="Relation Desc" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RELATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="SR_NO" DataField="SR_NO" HeaderText="Sr no"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CHAIN_NAME" DataField="CHAIN_NAME" HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CHAIN_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="ADDRESS_TYPE_NAME" DataField="ADDRESS_TYPE_NAME"
                                    HeaderText="Address Type">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ADDRESS_TYPE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_ADDRESS_LINE1" DataField="CUST_ADDRESS_LINE1"
                                    HeaderText="Address Line 1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_ADDRESS_LINE1" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_ADDRESS_LINE2" DataField="CUST_ADDRESS_LINE2"
                                    HeaderText="Address Line 2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_ADDRESS_LINE2" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                                    <ItemTemplate>
                                        <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="COUNTRY_NAME" DataField="COUNTRY_NAME"
                                    HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_PINCODE" DataField="CUST_PINCODE"
                                    HeaderText="Pincode">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_PINCODE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_PHONE" DataField="CUST_PHONE" HeaderText="Phone No">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_MOBILE" DataField="CUST_MOBILE"
                                    HeaderText="Mobile No">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="EMERGENCY_NAME" DataField="EMERGENCY_NAME"
                                    HeaderText="Emergency Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="EMERGENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="WEBSITE" DataField="WEBSITE" HeaderText="Website">
                                    <ItemTemplate>
                                        <asp:TextBox ID="WEBSITE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="VIDEO" DataField="VIDEO" HeaderText="Video"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="VIDEO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="LOGO" DataField="LOGO" HeaderText="Logo"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="LOGO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                                    <HeaderStyle Width="25px" />
                                    <ItemTemplate>
                                        <a id="A2" href="#" style="border: 0px solid #fff; text-decoration: none; height: 14px;
                                            width: 14px;" onkeydown="ContactAdded(this,event);">&raquo; </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                            <KeyboardNavigationSettings AllowActiveRowCycle="true" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                            <ClientEvents OnCommand="radgridcontdetmaster_Command" OnRowSelected="radgridcontdetmaster_RowSelected"
                                OnRowDblClick="ContactAddeddouble" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <asp:LinkButton ID="lbAddContact" runat="server" Text="Add Another Contact Detail"
                        OnClientClick="AddNewDetail();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Agent Sub Account"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="divRelation">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button3" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete Relation?'))return false;deleteCurrentSubProg1(); return false;"
                        Text="Delete Relation" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="radgridcustomerrelation" runat="server" AllowPaging="true" AllowMultiRowSelection="false"
                        AllowSorting="True" PageSize="50" ItemStyle-Wrap="false" EnableEmbeddedSkins="false"
                        AllowAutomaticDeletes="True" AllowAutomaticInserts="True" Width="2000">
                        <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                            <Columns>
                                <telerik:GridTemplateColumn SortExpression="CUST_ID" DataField="CUST_ID" HeaderText="Customer ID"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_UNQ_ID" DataField="CUST_UNQ_ID"
                                    HeaderText="Customer Unique Id" Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_ID" DataField="CUST_REL_ID"
                                    HeaderText="CustomerRelation Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_SRNO" DataField="CUST_REL_SRNO"
                                    HeaderText="Customer Relation SR No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_SURNAME" DataField="CUST_REL_SURNAME"
                                    HeaderText="Surname">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_SURNAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_NAME" DataField="CUST_REL_NAME"
                                    HeaderText="Name">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_GENDER" DataField="CUST_REL_GENDER"
                                    HeaderText="Gender" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_GENDER" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE"
                                    HeaderText="Mobile No">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CHAIN" DataField="CHAIN" HeaderText="Branch"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CHAIN" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_EMAIL" DataField="CUST_REL_EMAIL"
                                    HeaderText="Email [User Name]">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_EMAIL" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PASSWORD" DataField="PASSWORD" HeaderText="Password">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="PASSWORD" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="REMARKS" DataField="REMARKS" HeaderText="Remarks"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PHONE" DataField="CUST_REL_PHONE"
                                    HeaderText="Phone No">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="RELATION_DESC" DataField="RELATION_DESC"
                                    HeaderText="Relation With Customer*" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Red" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="RELATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="DESIGNATION_DESC" DataField="DESIGNATION_DESC"
                                    HeaderText="Designation">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="DESIGNATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PROFESSION_DESC" DataField="PROFESSION_DESC"
                                    HeaderText="Profession" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PROFESSION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_ANNUAL_INCOME" DataField="CUST_REL_ANNUAL_INCOME"
                                    HeaderText="Annual Income" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_ANNUAL_INCOME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_NO" DataField="CUST_REL_PASSPORT_NO"
                                    HeaderText="Passport No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_NO" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_PRINTED_NAME" DataField="CUST_REL_PASSPORT_PRINTED_NAME"
                                    HeaderText="Passport Printed Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_PRINTED_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_DATE" DataField="CUST_REL_PASSPORT_ISSUE_DATE"
                                    HeaderText="Passport Issue Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_DATE" runat="server" CssClass="radinput"
                                            onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_EXPIRY_DATE" DataField="CUST_REL_PASSPORT_EXPIRY_DATE"
                                    HeaderText="Passport Expiry Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_EXPIRY_DATE" runat="server" CssClass="radinput"
                                            onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_PLACE" DataField="CUST_REL_PASSPORT_ISSUE_PLACE"
                                    HeaderText="Passport Issue Place" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_PLACE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_PASSPORT_ISSUE_COUNTRY" DataField="CUST_REL_PASSPORT_ISSUE_COUNTRY"
                                    HeaderText="Passport Issue Country" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_PASSPORT_ISSUE_COUNTRY" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="MARITAL_STATUS_NAME" DataField="MARITAL_STATUS_NAME"
                                    HeaderText="Marital Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MARITAL_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_REL_MARRIAGE_DATE" DataField="CUST_REL_MARRIAGE_DATE"
                                    HeaderText="Marriage Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_REL_MARRIAGE_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="RELIGION_NAME" DataField="RELIGION_NAME"
                                    HeaderText="Religion" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RELIGION_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CUST_BIRTH_DATE" DataField="CUST_BIRTH_DATE"
                                    HeaderText="Birth Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_BIRTH_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);"
                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="MEAL_DESC" DataField="MEAL_DESC" HeaderText="Meal"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MEAL_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="NATIONALITY_NAME" DataField="NATIONALITY_NAME"
                                    HeaderText="Nationality" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="NATIONALITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="ALT_EMAIL" DataField="ALT_EMAIL" HeaderText="Alternate Email">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="ALT_EMAIL" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CURRENCY_NAME" DataField="CURRENCY_NAME"
                                    HeaderText="Credit Limit Currency" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="CREDIT_LIMIT" DataField="CREDIT_LIMIT"
                                    HeaderText="Credit Limit" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="CREDIT_LIMIT" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="USER_STATUS_NAME" DataField="USER_STATUS_NAME"
                                    HeaderText="User Status">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="USER_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="PARENT_CUSTORMER" DataField="PARENT_CUSTORMER"
                                    HeaderText="Parent Customer">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="PARENT_CUSTORMER" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn SortExpression="IS_ACCOUNT" DataField="IS_ACCOUNT" HeaderText="Is Account ID ?"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="IS_ACCOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                                    <HeaderStyle Width="25px" />
                                    <ItemTemplate>
                                        <a id="A3" href="#" style="border: 0px solid #fff; text-decoration: none; height: 14px;
                                            width: 14px;" onkeydown="RelationAdded(this,event);">&raquo; </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                            <KeyboardNavigationSettings AllowActiveRowCycle="true" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                            <ClientEvents OnCommand="radgridcustomerrelation_Command" OnRowSelected="radgridcustomerrelation_RowSelected"
                                OnRowDblClick="RelationAddeddouble" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Add Another Relation" OnClientClick="AddNewRelation();"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Btnback" runat="server" Text="Go To Booking Step-1" OnClientClick="RedirectBack();"
                        Style="color: black;" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnNext" runat="server" Text="Next To Step 2" OnClientClick="Redirect();"
                        Style="color: black;" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
