var TOUR_ID = ""; var customersCommandName = "";
var ordersCommandName = "";
var ContectCommandName = "";
var customersTableView = null;
var MarketingTableView = null;
var CurrencyTableView = null;
var HotelTableView = null;
var TransportTableView = null;
var AirlineTableView = null;
var CruiseTableView = null;
var globalvalue = null;
var tour_from_date = null;
var tour_to_date = null;
var cruise_id = null;
var searchparam = "";

function radgridmaster_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.TourMasterWebService.GetTour(customersTableView.get_currentPageIndex() * customersTableView.get_pageSize(), customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), searchparam, updateGrid);
    customersCommandName = args.get_commandName();
}
function radgridCurrDetail_Command(sender, args) {

}
function radgridMarketingMaterial_Command(sender, args) {

}
function radgridHotelDetails_Command(sender, args) {

}
function radgridHotelDetails_Command(sender, args) {

}
function radgridTransportationDetails_Command(sender, args) {

}
function radgridAirlineDetails_Command(sender, args) {

}
function radgridCruiseDetail_Command(sender, args) {

}
function radgridCruiseDetailSub_Command(sender, args) {

}
function radgridmaster_RowSelected(sender, args) {
    try {

        TOUR_ID = args.get_gridDataItem()._dataItem.TOUR_ID; currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        tour_from_date = args.get_gridDataItem()._dataItem.TOUR_FROM_DATE;
        tour_to_date = args.get_gridDataItem()._dataItem.TOUR_TO_DATE;
        loadcurrency();
            ////loadMarketingMaterial();
        ///loadHotelDetails();
            ////loadTransportDetails();
        ///loadAirlineDetail();
        ///loadCruiseDetail();

        
    }
    catch (e) { }
}
function radgridCruiseDetail_RowSelected(sender, args) {
    try {
        cruise_id = args.get_gridDataItem()._dataItem.CRUISE_SR_NO;
        filltable();
        fillCountrySubGrid();
    }
    catch (e) { }
}
function filltable() {
    CRM.WebApp.webservice.TourMasterWebService.GetDeckDetail(cruise_id, output);
}
function fillCountrySubGrid() {
    CRM.WebApp.webservice.TourMasterWebService.GetCountryVisaDetail(cruise_id, countrydata);
}
function output(result) {
    for (var k = 0; k < 10; k++) {
        document.getElementById('ctl00_cphPageContent_txtCabine' + k).value = '0';
    }
    for (var i = 0; i < result[0].length; i++) {
        var j = i + 1;
        if (j == result[0][i].DECK_NO) {
            document.getElementById('ctl00_cphPageContent_txtCabine' + i).value = result[0][i].CABINES;
        }
        else {
            document.getElementById('ctl00_cphPageContent_txtCabine' + i).value = '0';
        }
    }
}
function countrydata(result) {
    for (var k = 0; k < 5; k++) {
        document.getElementById('ctl00_cphPageContent_TextBox' + k).value = '0';
    }
    for (var i = 0; i < result[0].length; i++) {
        document.getElementById('ctl00_cphPageContent_TextBox' + i).value = result[0][i].COUNTRY_NAME;
    }
}
function updateGrid(result) {
    customersTableView.set_dataSource(result); customersTableView.dataBind(); if (result.length > 0) { customersTableView.selectItem(0); TOUR_ID = result[0]["TOUR_ID"]; }
    else { TOUR_ID = ""; }
    if (customersCommandName == "Filter" || customersCommandName == "Load") { CRM.WebApp.webservice.TourMasterWebService.GetCustomersCount(updateVirtualItemCount); }
}
function loadcurrency() {
    ordersCommandName = "Load";
    CRM.WebApp.webservice.TourMasterWebService.GetCurrencyByTOUR_ID(TOUR_ID, updateCurrencyGrid);
}
function loadMarketingMaterial() {
    CRM.WebApp.webservice.TourMasterWebService.GetMarketingByTOUR_ID(TOUR_ID, updateMarketingGrid);
}
function loadHotelDetails() {
    CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
}

function loadTransportDetails() {
    CRM.WebApp.webservice.TourMasterWebService.GetTransportDetailsByTOUR_ID(TOUR_ID, updateTransportDetailsGrid);
}
function loadAirlineDetail() {
    CRM.WebApp.webservice.TourMasterWebService.GetFlightDetailOnTourId(TOUR_ID, updateFlightDetailGrid);
}
function loadCruiseDetail() {
    CRM.WebApp.webservice.TourMasterWebService.GetCruiseDetailOnTourId(TOUR_ID, updateCruiseDetailGrid);
}

function loadRoles() { CRM.WebApp.webservice.TourMasterWebService.GetRolebyEmpID(TOUR_ID, updateRoleListBox); }
function loadAllCompany() { CRM.WebApp.webservice.TourMasterWebService.GetAllCompany(updateAllCompanyListBox); }
function loadAllRole() { CRM.WebApp.webservice.TourMasterWebService.GetAllRole(updateAllRoleListBox); }
function loadCompany() { CRM.WebApp.webservice.TourMasterWebService.GetCompanybyEmpID(TOUR_ID, updateCompanyListBox); }
function updateVirtualItemCount(result) { customersTableView.set_virtualItemCount(result); }
function updateCurrencyGrid(result) {
    CurrencyTableView.set_dataSource(result);
    CurrencyTableView.dataBind();
}
function updateMarketingGrid(result) {
    MarketingTableView.set_dataSource(result);
    MarketingTableView.dataBind();
}
//function updateHotelDetailsGrid(result) {
//    HotelTableView.set_dataSource(result);
//    HotelTableView.dataBind();
//}
//function updateTransportDetailsGrid(result) {
//    TransportTableView.set_dataSource(result);
//    TransportTableView.dataBind();
//}
//function updateOrdersVirtualItemCount(result) { ordersTableView.set_virtualItemCount(result); }

//function updateFlightDetailGrid(result) {
//    AirlineTableView.set_dataSource(result);
//    AirlineTableView.dataBind();
//}
//function updateCruiseDetailGrid(result) {
//    CruiseTableView.set_dataSource(result);
//    CruiseTableView.dataBind();
//}
function TourRowClick(sender, eventArgs) {
   
    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
   
    ary[0] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_TYPE_NAME").value;
    ary[1] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_CODE").value;
    ary[2] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_SHORT_NAME").value;
    ary[3] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_FROM_DATE").value;
    ary[4] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_TO_DATE").value;
    ary[5] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_SUB_TYPE_NAME").value;
    ary[6] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_ITENARY_TYPE_NAME").value;
    ary[7] = customersTableView.get_dataItems()[currentRowIndex].findElement("NO_OF_DAYS").value;
    ary[8] = customersTableView.get_dataItems()[currentRowIndex].findElement("NO_OF_NIGHTS").value;
    ary[9] = customersTableView.get_dataItems()[currentRowIndex].findElement("NO_OF_SEATS").value;
    //ary[10] = customersTableView.get_dataItems()[currentRowIndex].findElement("NO_OF_AVAILABLE_SEATS").value;
    ary[11] = customersTableView.get_dataItems()[currentRowIndex].findElement("GUIDE_TITLE").value;
    ary[12] = customersTableView.get_dataItems()[currentRowIndex].findElement("BASE_TOUR").value;
    ary[14] = customersTableView.get_dataItems()[currentRowIndex].findElement("TOUR_LONG_DESC").value;

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
    if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
    ary[13] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.TOUR_ID;
    try {
        CRM.WebApp.webservice.TourMasterWebService.InsertUpdateTour(ary); // add new tour 
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TourMasterWebService.GetTour(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), searchparam, updateGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function TourCurrRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[0] = TOUR_ID;
    ary[1] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("TOUR_CURRANCY_1").value;
    ary[2] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("ADULT_COST_C1").value;
    ary[3] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("ADULT_TAX_C1").value;
    ary[4] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("ADULT_GST_C1").value;
    ary[5] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CWB_COST_C1").value;
    ary[6] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CWB_TAX_C1").value;
    ary[7] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CWB_GST_C1").value;
    ary[8] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CNB_COST_C1").value;
    ary[9] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CNB_TAX_C1").value;
    ary[10] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CNB_GST_C1").value;
    ary[11] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("INFANT_COST_C1").value;
    ary[12] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("INFANT_TAX_C1").value;
    ary[13] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("INFANT_GST_C1").value;
    ary[14] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("TOUR_CURRANCY_2").value;
    ary[15] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("ADULT_COST_C2").value;
    ary[16] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CWB_COST_C2").value;
    ary[17] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("CNB_COST_C2").value;
    ary[18] = CurrencyTableView.get_dataItems()[currentRowIndex].findElement("INFANT_COST_C2").value;

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

    ary[19] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.QUOTE_ID;
    try {
        CRM.WebApp.webservice.TourMasterWebService.InsertUpdateTourQuote(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TourMasterWebService.GetCurrencyByTOUR_ID(TOUR_ID, updateCurrencyGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
            
            