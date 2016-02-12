var passengerMasterTableView = "";
var cruiseMasterTableView = "";
var cruiseRoomTableView = "";
var BOOKING_DETAIL_ID = 0;
var BOOKING_ID = 0;
var CRUISE_SRNO = 0;
var TOUR_ID = 0;
var a = 0;
var globalvalue = "";
var SHARE = "";


function radgridpassengermaster_Command(sender, args) {

    CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruisePassengerDetails(BOOKING_ID, updatepassengergrid);
    CRM.WebApp.webservice.CruiseRoomAllocationWebService._staticInstance.GetCruisePassDetails(BOOKING_ID, upadtepassgrid);


}

function radgridpassengermaster_RowSelected(sender, args) {
    BOOKING_DETAIL_ID = args.get_gridDataItem()._dataItem.BOOKING_DETAIL_ID;
    SHARE = args.get_gridDataItem()._dataItem.SHARE_ROOM_IN_CRUISE;
    CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseDetails(BOOKING_DETAIL_ID, updateCruiseGrid);
}

function radgridCruiseMaster_Command(sender, args) {

}

function radgridCruiseMaster_RowSelected(sender, args) {
    CRUISE_SRNO = args.get_gridDataItem()._dataItem.CRUISE_SR_NO;
    CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseRooms(CRUISE_SRNO, updateCruiseRoomGrid);
}

function radgridCruiseRoomDetails_Command(sender, args) {

}
function updatepassengergrid(result) {

    passengerMasterTableView.set_dataSource(result);
    passengerMasterTableView.dataBind();
    if (result.length > 0) { passengerMasterTableView.selectItem(0); }

}
function upadtepassgrid(result) {
    passengerMasterTableView.set_dataSource(result);
    passengerMasterTableView.dataBind();
    if (result.length > 0) {
        passengerMasterTableView.selectItem(0);
    }
}

function updateCruiseGrid(result) {

    cruiseMasterTableView.set_dataSource(result);
    cruiseMasterTableView.dataBind();
    if (result.length > 0) { cruiseMasterTableView.selectItem(0); }

}

function updateCruiseRoomGrid(result) {

    cruiseRoomTableView.set_dataSource(result);
    cruiseRoomTableView.dataBind();
    if (result.length > 0) { cruiseRoomTableView.selectItem(0); }

}
function CruiseRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];
    ary[1] = TOUR_ID;
    ary[2] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CRUISE_COMP_NAME").value;
    ary[3] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CABINE_CATEGORY").value;
    ary[4] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CABINE_TO_BE_BLOCKED").value;
    ary[5] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQ_TO").value;
    ary[6] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CHECK_COMMENTS").value;
    ary[7] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQ_TO").value;
    ary[8] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_ROOMS_BLOCKED").value;
    ary[9] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("TIME_LIMIT").value;
    ary[10] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS_NAME").value;
    ary[11] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_AMOUNT").value;
    ary[12] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[13] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("GST").value;
    ary[14] = cruiseMasterTableView.get_dataItems()[currentRowIndex].findElement("CRUISE_NAME").value;

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

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CRUISE_SR_NO;

    try {
        CRM.WebApp.webservice.CruiseRoomAllocationWebService.InsertUpdateCruiseDetails(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseDetails(BOOKING_DETAIL_ID, updateCruiseGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
function CruiseRoomRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    ary[1] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("DECK_NO").value;
    ary[2] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("CABINE_NO").value;
    ary[3] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("ADULT1").value;
    ary[4] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("ADULT2").value;
    ary[5] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("ADULT3").value;
    ary[6] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("CWB").value;
    ary[7] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("CNB1").value;
    ary[8] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("CNB2").value;
    ary[9] = cruiseRoomTableView.get_dataItems()[currentRowIndex].findElement("INFANT").value;
    ary[10] = SHARE;

    for (var i = 0; i < 11; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }

    try {
        CRM.WebApp.webservice.CruiseRoomAllocationWebService.InsertCruiseRoom(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CruiseRoomAllocationWebService.GetCruiseRooms(CRUISE_SRNO, updateCruiseRoomGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}