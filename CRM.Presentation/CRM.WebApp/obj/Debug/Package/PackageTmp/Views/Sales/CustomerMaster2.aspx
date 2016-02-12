<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CustomerMaster2.aspx.cs" Inherits="CRM.WebApp.Views.Sales.CustomerMaster2" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
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
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/NextTravelPlan.js"></script>
        <script type="text/javascript">
            var cust_id;
            var cust_rel_id;
            var cust_srno;
            // var inqid;
            function pageLoad() {


                cust_id = getValue("CUST_ID");
                cust_rel_id = getValue("CUST_REL_ID");
                cust_srno = getValue("CUST_REL_SRNO");
                //inqid = getValue("INQUIRY_ID");

                TravelPlanTableView = $find("<%= cutomernexttravelplan.ClientID %>").get_masterTableView();
                CustomerTravelTableView = $find("<%= cutomertravelhistory.ClientID %>").get_masterTableView();
                TravelWithOtherTableView = $find("<%= cutomertravelhistorywithother.ClientID %>").get_masterTableView();
                AirlineDetailTableView = $find("<%= adgridairlinedetail.ClientID %>").get_masterTableView();
                VisaDetailTableView = $find("<%= radgridvisadetail.ClientID %>").get_masterTableView();
                TravelPlanCommand = "Load";
                CustomerTravelCommand = "Load";
                TravelWithOtherCommand = "Load";
                AirlineCommand = "Load";
                VisaCommand = "Load";

                var q = window.location.search.substring(1);
                if (q != "") {

                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlanwithcustid(cust_id, updatetravelplan);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistorywithcustid(cust_id, updateCustomerTravelWithUs);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelWithOtherwithcustid(cust_id, updateCustomerTravelWithOther);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetailwithcustid(cust_id, updateAirLineDetail);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisawithcustid(cust_id, updateVisaDetail);
                }
                else {

                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlan(TravelPlanTableView.get_currentPageIndex() * TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_sortExpressions().toString(), TravelPlanTableView.get_filterExpressions().toDynamicLinq(), updatetravelplan);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistory(CustomerTravelTableView.get_currentPageIndex() * CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_sortExpressions().toString(), CustomerTravelTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithUs);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelWithOther(TravelWithOtherTableView.get_currentPageIndex() * TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_sortExpressions().toString(), TravelWithOtherTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithOther);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetail(AirlineDetailTableView.get_currentPageIndex() * AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_sortExpressions().toString(), AirlineDetailTableView.get_filterExpressions().toDynamicLinq(), updateAirLineDetail);
                    CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisa(VisaDetailTableView.get_currentPageIndex() * VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_sortExpressions().toString(), VisaDetailTableView.get_filterExpressions().toDynamicLinq(), updateVisaDetail);
                }
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

            function deleteCurrent() {
                var table = $find("<%= cutomernexttravelplan.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= cutomernexttravelplan.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= cutomernexttravelplan.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.deleteNextTravelPlan(SR_NO);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteCurrent1() {
                var table = $find("<%= cutomertravelhistory.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= cutomertravelhistory.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= cutomertravelhistory.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.deleteTravelHistory(SR_NO);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteCurrent2() {
                var table = $find("<%= cutomertravelhistorywithother.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= cutomertravelhistorywithother.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= cutomertravelhistorywithother.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.deleteTravelHistoryWithOther(SR_NO);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteCurrent3() {
                var table = $find("<%= adgridairlinedetail.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= adgridairlinedetail.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= adgridairlinedetail.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.deleteAirlinedetail(SR_NO);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteCurrent4() {
                var table = $find("<%= radgridvisadetail.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridvisadetail.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridvisadetail.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.deleteVisaDetail(SR_NO);
                gridItems[gridItems.length - 1].set_selected(true);
            }

            function addnewtravelplannewadd() {
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertNewTravelPlan(cust_id);
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlanwithcustid(cust_id, updatetravelplan);
            }
            function addnewtravelplannewadd2() {
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertNewTravelPlan2(cust_id);
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistorywithcustid(cust_id, updateCustomerTravelWithUs);

            }
            function addnewtravelplannewadd3() {
                
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertNewTravelPlan3(cust_id);
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelWithOtherwithcustid(cust_id, updateCustomerTravelWithOther);

            }
            function addnewtravelplannewadd4() {
                
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertNewTravelPlan4(cust_id);
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetailwithcustid(cust_id, updateAirLineDetail);

            }
            function addnewtravelplannewadd5() {
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertNewTravelPlan5(cust_id);
                CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisawithcustid(cust_id, updateVisaDetail);
            }

            function addnewtravelplan(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = cust_id;
                ary[2] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("NEXT_TRAVEL_PLAN_DATE").value;
                ary[3] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("REGION_SHORT_NAME").value;
                ary[4] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_PERSONS").value;
                ary[5] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
                ary[6] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[7] = TravelPlanTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                for (i = 0; i < 8; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    var q = window.location.search.substring(1);
                    if (q != "") {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelPlan(ary);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlanwithcustid(cust_id, updatetravelplan);
                        alert('Record Save Successfully');
                    }
                    else {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelPlan(ary);
                        alert('Record Save Successfully');
                        //CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlan(TravelPlanTableView.get_currentPageIndex() * TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_pageSize(), TravelPlanTableView.get_sortExpressions().toString(), TravelPlanTableView.get_filterExpressions().toDynamicLinq(), updatetravelplan);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetNextTravelPlanwithcustid(cust_id, updatetravelplan);
                    }
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }

            function addnewtravelhistory(sender, args) {


                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = cust_id;
                ary[2] = CustomerTravelTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_TRAVEL_DATE").value;
                ary[3] = CustomerTravelTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_PERSONS").value;
                ary[4] = CustomerTravelTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
                ary[5] = CustomerTravelTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_ID").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                for (i = 0; i < 6; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    var q = window.location.search.substring(1);
                    if (q != "") {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelHistory(ary);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistorywithcustid(cust_id, updateCustomerTravelWithUs);
                        alert('Record Save Successfully');
                    }
                    else {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelHistory(ary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistory(CustomerTravelTableView.get_currentPageIndex() * CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_pageSize(), CustomerTravelTableView.get_sortExpressions().toString(), CustomerTravelTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithUs);
                    }
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewtravelhistorywithother(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = cust_id;
                ary[2] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_TRAVEL_DATE").value;
                //ary[3] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_TYPE_NAME").value;
                ary[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TOUR_TYPE_NAME;
                ary[4] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_PERSON").value;
                ary[5] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPATION").value;
                ary[6] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[7] = TravelWithOtherTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;


                for (i = 0; i < 8; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    var q = window.location.search.substring(1);
                    if (q != "") {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelWithOther(ary);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelWithOtherwithcustid(cust_id, updateCustomerTravelWithOther);
                        alert('Record Save Successfully');
                    }
                    else {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateTravelWithOther(ary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetTravelHistory(TravelWithOtherTableView.get_currentPageIndex() * TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_pageSize(), TravelWithOtherTableView.get_sortExpressions().toString(), TravelWithOtherTableView.get_filterExpressions().toDynamicLinq(), updateCustomerTravelWithOther);

                    }
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function addnewairlinedetail(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = cust_id;
                ary[2] = cust_srno;
                ary[3] = AirlineDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("AIRLINE_NAME").value;
                ary[4] = AirlineDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CLASS_NAME").value;
                ary[5] = AirlineDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("FREQUENTLY_FLY_NO").value;
                ary[6] = AirlineDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CORPORATE_CLIENT_NO").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[7] = cust_rel_id;

                for (i = 0; i < 8; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
               try {
                    var q = window.location.search.substring(1);
                    if (q != "") {
                        
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateAirLineDetail(ary);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetailwithcustid(cust_id, updateAirLineDetail);
                        alert('Record Save Successfully');
                    }
                    else {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateAirLineDetail(ary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetAirlineDetail(AirlineDetailTableView.get_currentPageIndex() * AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_pageSize(), AirlineDetailTableView.get_sortExpressions().toString(), AirlineDetailTableView.get_filterExpressions().toDynamicLinq(), updateAirLineDetail);
                    }
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewvisadetail(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = cust_id;
                ary[2] = cust_srno;
                ary[3] = VisaDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[4] = VisaDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("VISA_EXPIRY_DATE").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[5] = cust_rel_id;

                for (i = 0; i < 6; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    var q = window.location.search.substring(1);
                    if (q != "") {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateVisaDetail(ary);
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisawithcustid(cust_id, updateVisaDetail);
                        alert('Record Save Successfully');
                    }
                    else {
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.InsertUpdateVisaDetail(ary);
                        alert('Record Save Successfully');
                        CRM.WebApp.webservice.CustomerNextTravelPlanWebService.GetCustomerVisa(VisaDetailTableView.get_currentPageIndex() * VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_pageSize(), VisaDetailTableView.get_sortExpressions().toString(), VisaDetailTableView.get_filterExpressions().toDynamicLinq(), updateVisaDetail);
                    }
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Customer Next Travel Plan"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var regionname = "../../webservice/autocomplete.ashx?key=GET_REGION_TYPE_FOR_AUTOSERCH";
            //  var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            //  var toursubtype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TOUR_SUB_TYPE_NAME_AUTOSEARCH_NEW";
            // var inquiry = "../../webservice/autocomplete.ashx?key=GET_INQUIRY_STATUS_NAME";
            //var status = "../../webservice/autocomplete.ashx?key=FETCH_STATUS_FOR_EMPLOYEE_MASTER_AUTOSEARCH";
            var agent = "../../webservice/autocomplete.ashx?key=GET_COMPITITOR_AGENT_NAME_AUTOSEARCH";
            var flightclass = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_FLIGHT_CLASS_AUTOSEARCH";
            var airlinename = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_AIRLINE_AUTOSEARCH";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                //   $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                //  $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                //  $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_TOUR_SHORT_NAME").autocomplete(tourshortname);
                //  $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_TOUR_TYPE_NAME").autocomplete(toursubtype);
                //                            $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_INQUIRY_STATUS_NAME").autocomplete(inquiry);
                //                            $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(status);
                $("#ctl00_cphPageContent_cutomernexttravelplan_ctl00_ctl" + i + "_REGION_SHORT_NAME").autocomplete(regionname);
                //                            $("#ctl00_cphPageContent_cutomertravelhistory_ctl00_ctl" + i + "_TOUR_SHORT_NAME").autocomplete(tourshortname);
                //                            $("#ctl00_cphPageContent_cutomertravelhistory_ctl00_ctl" + i + "_TOUR_TYPE_NAME").autocomplete(toursubtype);
                // $("#ctl00_cphPageContent_cutomertravelhistory_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                //                            $("#ctl00_cphPageContent_cutomertravelhistorywithother_ctl00_ctl" + i + "_TOUR_TYPE_NAME").autocomplete(toursubtype);
                // $("#ctl00_cphPageContent_cutomertravelhistorywithother_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_cutomertravelhistorywithother_ctl00_ctl" + i + "_AGENT_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_adgridairlinedetail_ctl00_ctl" + i + "_AIRLINE_NAME").autocomplete(airlinename);
                $("#ctl00_cphPageContent_adgridairlinedetail_ctl00_ctl" + i + "_CLASS_NAME").autocomplete(flightclass);
                $("#ctl00_cphPageContent_radgridvisadetail_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
            }

        });       
    </script>
    <div id="radmastergrid">
        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this TravelPlan?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="cutomernexttravelplan" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_NAME" DataField="CUST_NAME" HeaderText="CUSTOMER NAME" Visible="false" >
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput" ReadOnly="true" style="background-color:LightBlue" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NEXT_TRAVEL_PLAN_DATE" DataField="NEXT_TRAVEL_PLAN_DATE" HeaderText="TRAVEL PLAN DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="NEXT_TRAVEL_PLAN_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_TYPE_NAME" DataField="TOUR_TYPE_NAME" HeaderText="TOUR TYPE" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_SHORT_NAME" DataField="TOUR_SHORT_NAME" HeaderText="TOUR SHORT NAME" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="REGION_SHORT_NAME" DataField="REGION_SHORT_NAME" HeaderText="REGION SHORT NAME" >
                          <ItemTemplate>
                            <asp:TextBox ID="REGION_SHORT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NO_OF_PERSONS" DataField="NO_OF_PERSONS" HeaderText="NO OF PERSONS">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_PERSONS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="STATE">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="STATUS_NAME" DataField="STATUS_NAME" HeaderText="STATUS" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewtravelplan(this,event);">
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
                <ClientEvents OnCommand="cutomernexttravelplan_Command" OnRowSelected="cutomernexttravelplan_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                    <asp:LinkButton ID="lbAddtravelpaln" runat="server" Text="Add Another Travel Plan"
                        OnClientClick="addnewtravelplannewadd();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Customer Travel History With Us"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this TravelPlan?'))return false; deleteCurrent1(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="cutomertravelhistory" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_TRAVEL_DATE" DataField="CUSTOMER_TRAVEL_DATE" HeaderText="TRAVEL DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_TRAVEL_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_TYPE_NAME" DataField="TOUR_TYPE_NAME" HeaderText="TOUR TYPE" Visible=false>
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_SHORT_NAME" DataField="TOUR_SHORT_NAME" HeaderText="TOUR SHORT NAME" Visible=false>
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NO_OF_PERSONS" DataField="NO_OF_PERSONS" HeaderText="NO OF PERSONS">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_PERSONS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewtravelhistory(this,event);">
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
                <ClientEvents OnCommand="cutomertravelhistory_Command" OnRowSelected="cutomertravelhistory_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Add Another Travel History With Us"
                    OnClientClick="addnewtravelplannewadd2();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Customer Travel History With Other"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button2" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Travel History?'))return false; deleteCurrent2(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="cutomertravelhistorywithother" runat="server" allowpaging="true"
                    allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                    enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUSTOMER_TRAVEL_DATE" DataField="CUSTOMER_TRAVEL_DATE" HeaderText="TRAVEL DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_TRAVEL_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_TYPE_NAME" DataField="TOUR_TYPE_NAME" HeaderText="TOUR TYPE" visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="AGENT_NAME" DataField="AGENT_NAME" HeaderText="AGENT NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NO_OF_PERSON" DataField="NO_OF_PERSON" HeaderText="NO OF PERSONS">
                          <ItemTemplate>
                            <asp:TextBox ID="NO_OF_PERSON" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPATION" DataField="DESCRIPATION" HeaderText="DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPATION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewtravelhistorywithother(this,event);">
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
                <ClientEvents OnCommand="cutomertravelhistorywithother_Command" OnRowSelected="cutomertravelhistorywithother_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="LinkButton2" runat="server" Text="Add Another Travel History With Other"
                    OnClientClick="addnewtravelplannewadd3();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal3" runat="server" Text="Customer Airline Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button3" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this airline?'))return false; deleteCurrent3(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="adgridairlinedetail" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1000px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_REL_ID" DataField="CUST_REL_ID" HeaderText="CUST_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_SRNO" DataField="CUST_REL_SRNO" HeaderText="CUST_REL_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="AIRLINE_NAME" DataField="AIRLINE_NAME" HeaderText="AIRLINE NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="AIRLINE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CLASS_NAME" DataField="CLASS_NAME" HeaderText="CLASS NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CLASS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FREQUENTLY_FLY_NO" DataField="FREQUENTLY_FLY_NO" HeaderText="FREQUENTLY FLY NO">
                          <ItemTemplate>
                            <asp:TextBox ID="FREQUENTLY_FLY_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CORPORATE_CLIENT_NO" DataField="CORPORATE_CLIENT_NO" HeaderText="CORPORATE CLIENT NO">
                          <ItemTemplate>
                            <asp:TextBox ID="CORPORATE_CLIENT_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A4" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewairlinedetail(this,event);">
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
                <ClientEvents OnCommand="adgridairlinedetail_Command" OnRowSelected="adgridairlinedetail_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="LinkButton3" runat="server" Text="Add Another Air Line Detail" OnClientClick="addnewtravelplannewadd4();"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal4" runat="server" Text="Customer Visa Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="Button4" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Visa Detail?'))return false; deleteCurrent4(); return false;"
                    Text="Delete" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridvisadetail" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_ID" DataField="CUST_ID" HeaderText="CUST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CUST_REL_ID" DataField="CUST_REL_ID" HeaderText="CUST_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_ID" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_REL_SRNO" DataField="CUST_REL_SRNO" HeaderText="CUST_REL_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput" Text ="0"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="COUNTRY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VISA_EXPIRY_DATE" DataField="VISA_EXPIRY_DATE" HeaderText="VISA EXPIRY DATE">
                          <ItemTemplate>
                            <asp:TextBox ID="VISA_EXPIRY_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A5" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewvisadetail(this,event);">
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
                <ClientEvents OnCommand="radgridvisadetail_Command" OnRowSelected="radgridvisadetail_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="LinkButton4" runat="server" Text="Add Another Visa Deatil" OnClientClick="addnewtravelplannewadd5();"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
