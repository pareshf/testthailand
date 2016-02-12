var TOUR_ID = "";
var HotelTableView = null;
var HotelCommandName = "";
var globalvalue = null;
var AirlineTableView = null;
var CruiseTableView = null;
var tour_from_date = null;
var tour_to_date = null;
var cruise_id = null;


// Hotel Details Command Function

function radgridHotelDetails_Command(sender, args) {
    CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
    HotelCommandName = args.get_commandName();
}

// Hotel Details Row Selected Function 

function radgridHotelDetails_RowSelected(sender, args) {

}
function radgridAirlineDetails_Command(sender, args) {

}
function radgridCruiseDetail_Command(sender, args) {

}

// Hotel Update HotelGrid Function 

function updateHotelDetailsGrid(result) {
    HotelTableView.set_dataSource(result);
    HotelTableView.dataBind();
   
}

function updateFlightDetailGrid(result) {
    AirlineTableView.set_dataSource(result);
    AirlineTableView.dataBind();
}

function updateCruiseDetailGrid(result) {
    CruiseTableView.set_dataSource(result);
    CruiseTableView.dataBind();
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
// sachin row click
function RowClick(sender, eventArgs) {

    var text = "";
    text += "Row was clicked";
    text += ", Index: " + eventArgs.get_itemIndexHierarchical();
    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[0] = TOUR_ID;

    ary[1] = HotelTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[2] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[3] = HotelTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[4] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[5] = HotelTableView.get_dataItems()[currentRowIndex].findElement("HOTEL_NAME").value;
    ary[6] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[7] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.ROOM_TYPE_NAME.value;
    //ary[8] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_ROOMS").value;
    ary[9] = HotelTableView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    ary[10] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[11] = HotelTableView.get_dataItems()[currentRowIndex].findElement("GST").value;
    ary[12] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_AMOUNT").value;
    ary[13] = HotelTableView.get_dataItems()[currentRowIndex].findElement("REMARKS").value;
    ary[14] = HotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQUEST_TO").value;
    ary[15] = HotelTableView.get_dataItems()[currentRowIndex].findElement("ROOM_TO_BE_BLOCKED").value;
    ary[16] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQUEST_TO").value;
    ary[17] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQUEST_DATE").value;
    ary[18] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_COMMENTS").value;
    ary[19] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_ROOM_BLOCKED").value;
    ary[20] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TIME_LIMIT").value;
    ary[21] = HotelTableView.get_dataItems()[currentRowIndex].findElement("APPROVED_BY").value;
    ary[22] = HotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKED_BY").value;
    ary[23] = HotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS").value;
    ary[24] = HotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQUEST_DATE").value;
    ary[25] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_ROOM_ALLOTEED").value;
    ary[26] = HotelTableView.get_dataItems()[currentRowIndex].findElement("PARTIAL_ROOM_ALLOTED").value;
    ary[27] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_ADULT_ALLOTED").value;
    ary[28] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_CWB_ALLOTED").value;
    ary[29] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_CNB_ALLOTED").value;
    ary[30] = HotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_INFANT_ALLOTED").value;
    ary[31] = HotelTableView.get_dataItems()[currentRowIndex].findElement("AVALIBLE_ROOM").value;

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

    ary[32] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    try {
        CRM.WebApp.webservice.TourMasterWebService.InsertUpdateHotelDetails(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TourMasterWebService.GetHotelDetailsByTOUR_ID(TOUR_ID, updateHotelDetailsGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }


}
function RowClickforflight(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[0] = TOUR_ID;
    ary[1] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("AIRLINE_NAME").value;
    ary[2] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("FLIGHT_NO").value;
    ary[3] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("CLASS_NAME").value;
    ary[4] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("SEATS_TO_BE_BLOCKED").value;
    ary[5] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQ_TO").value;
    ary[6] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("CHECK_COMMENTS").value;
    ary[7] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQ_TO").value;
    ary[8] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("SEATS_TO_BLOCK").value;
    ary[9] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("TIME_LIMIT").value;
    ary[10] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS_NAME").value;
    ary[11] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_AMOUNT").value;
    ary[12] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[13] = AirlineTableView.get_dataItems()[currentRowIndex].findElement("GST").value;


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

    ary[14] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FLIGHT_SR_NO;
    try {
        CRM.WebApp.webservice.TourMasterWebService.InsertUpdateFlight(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TourMasterWebService.GetFlightDetailOnTourId(TOUR_ID, updateFlightDetailGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function RowClickforcruise(sender, eventArgs) {
    
    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[0] = TOUR_ID;
    ary[1] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CRUISE_COMP_NAME").value;
    ary[2] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CABINE_CATEGORY").value;
    ary[3] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CABINE_TO_BE_BLOCKED").value;
    ary[4] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQ_TO").value;
    ary[5] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CHECK_COMMENTS").value;
    ary[6] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQ_TO").value;
    ary[7] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_ROOMS_BLOCKED").value;
    ary[8] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("TIME_LIMIT").value;
    ary[9] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS_NAME").value;
    ary[10] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_AMOUNT").value;
    ary[11] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[12] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("GST").value;
    ary[14] = CruiseTableView.get_dataItems()[currentRowIndex].findElement("CRUISE_NAME").value;

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

    ary[13] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CRUISE_SR_NO;

    try {
        CRM.WebApp.webservice.TourMasterWebService.InsertUpdateCruise(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.TourMasterWebService.GetCruiseDetailOnTourId(TOUR_ID, updateCruiseDetailGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}