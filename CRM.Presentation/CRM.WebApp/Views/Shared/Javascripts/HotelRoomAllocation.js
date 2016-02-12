var globalvalue = "";
var masterTableView = null;
var hotelTableView = null;
var custTableView = null;
var roomTableView = null;
var BOOKING_ID = 0;
var BOOKING_DETAIL_ID = 0;
var HOTEL_SRNO = 0;
var TOUR_ID = 0;
var a = 0;
var SHARE = "";
var CITY_ID = "";
var COUNTRY_ID = "";

function radgridmaster_Command(sender, args) {
    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetPassengerDetail(BOOKING_ID, updateMasterGrid);
}

function radgridmaster_RowSelected(sender, args) {
    BOOKING_DETAIL_ID = args.get_gridDataItem()._dataItem.BOOKING_DETAIL_ID;
    SHARE = args.get_gridDataItem()._dataItem.SHARE_ROOM_IN_HOTEL;
    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetTourHotelDetail(TOUR_ID, updateHotelGrid);
    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetHotelDetail(BOOKING_DETAIL_ID, updateHotelGrid);
}

function radgridHotelDetails_Command(sender, args) {

}

function radgridHotelDetails_RowSelected(sender, args) {
    
    HOTEL_SRNO = args.get_gridDataItem()._dataItem.SR_NO;
    CITY_ID = args.get_gridDataItem()._dataItem.CITY_ID;
    COUNTRY_ID = args.get_gridDataItem()._dataItem.COUNTRY_ID;
    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetRoomDetail(HOTEL_SRNO, updateRoomGrid);
}

function radgridcustmaster_RowSelected(sender, args) {
    BOOKING_ID = args.get_gridDataItem()._dataItem.BOOKING_ID;
    CRM.WebApp.webservice.HotelRoomAllocationWebService.GetPassengerDetail(BOOKING_ID, updateMasterGrid);
}
function radgridcustmaster_Command(sender, args) {
}

function radgridRoomDetails_Command(sender, args) {

}


function updateMasterGrid(result) {
    masterTableView.set_dataSource(result);
    masterTableView.dataBind();
    if (result.length > 0) { masterTableView.selectItem(0); }
}

function updateHotelGrid(result) {
    hotelTableView.set_dataSource(result);
    hotelTableView.dataBind();
    if (result.length > 0) { hotelTableView.selectItem(0); }
}

function updateCustGrid(result) {
    custTableView.set_dataSource(result);
    custTableView.dataBind();
    if (result.length > 0) { hotelTableView.selectItem(0); }
}

function updateRoomGrid(result) {
    roomTableView.set_dataSource(result);
    roomTableView.dataBind();
}
function HotelRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[0] = TOUR_ID;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    ary[2] = hotelTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[3] = hotelTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[4] = hotelTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[5] = hotelTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[6] = hotelTableView.get_dataItems()[currentRowIndex].findElement("HOTEL_NAME").value;
    ary[7] = hotelTableView.get_dataItems()[currentRowIndex].findElement("AMOUNT").value;
    ary[8] = hotelTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[9] = hotelTableView.get_dataItems()[currentRowIndex].findElement("ROOM_TYPE_NAME").value;
    ary[10] = hotelTableView.get_dataItems()[currentRowIndex].findElement("TAX").value;
    ary[11] = hotelTableView.get_dataItems()[currentRowIndex].findElement("GST").value;
    ary[12] = hotelTableView.get_dataItems()[currentRowIndex].findElement("TOTAL_AMOUNT").value;
    ary[13] = hotelTableView.get_dataItems()[currentRowIndex].findElement("REMARKS").value;
    ary[14] = hotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_REQUEST_TO").value;
    ary[15] = hotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_REQUEST_TO").value;
    ary[16] = hotelTableView.get_dataItems()[currentRowIndex].findElement("TIME_LIMIT").value;
    ary[17] = hotelTableView.get_dataItems()[currentRowIndex].findElement("BOOKING_STATUS").value;
    ary[18] = hotelTableView.get_dataItems()[currentRowIndex].findElement("ROOM_TO_BE_BLOCKED").value;
    ary[19] = hotelTableView.get_dataItems()[currentRowIndex].findElement("CHECK_COMMENTS").value;

    for (var i = 0; i < 20; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }

    try {
        CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertUpdateHotel(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.HotelRoomAllocationWebService.GetPassengerDetail(BOOKING_ID, updateMasterGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function RoomAllocationRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SR_NO;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.BOOKING_HOTEL_SRNO;
    ary[2] = roomTableView.get_dataItems()[currentRowIndex].findElement("ROOM_NO").value;
    ary[3] = roomTableView.get_dataItems()[currentRowIndex].findElement("ADULT1").value;
    ary[4] = roomTableView.get_dataItems()[currentRowIndex].findElement("ADULT2").value;
    ary[5] = roomTableView.get_dataItems()[currentRowIndex].findElement("ADULT3").value;
    ary[6] = roomTableView.get_dataItems()[currentRowIndex].findElement("CWB").value;
    ary[7] = roomTableView.get_dataItems()[currentRowIndex].findElement("CNB1").value;
    ary[8] = roomTableView.get_dataItems()[currentRowIndex].findElement("CNB2").value;
    ary[9] = roomTableView.get_dataItems()[currentRowIndex].findElement("INFANT").value;
    ary[10] = SHARE;

    for (var i = 0; i < 11; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }

    try {
        CRM.WebApp.webservice.HotelRoomAllocationWebService.InsertUpdateRoom(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.HotelRoomAllocationWebService.GetRoomDetail(HOTEL_SRNO, updateRoomGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}