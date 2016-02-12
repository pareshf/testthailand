var ROOM_TYPE_ID = "";
var HotelRoomTypeComandName = "";
var HotelRoomTypeTableView = null;

function radgridhotelroomtype_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    HotelRoomTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(HotelRoomTypeTableView.get_currentPageIndex() * HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);
    HotelRoomTypeComandName = args.get_commandName;

}
function radgridhotelroomtype_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        ROOM_TYPE_ID = args.get_gridDataItem()._dataItem.ROOM_TYPE_ID;


    }
    catch (e) { }
}
function updateHotelRoomTypeVirtualCount(result) { HotelRoomTypeTableView.set_virtualItemCount(result); }

function updateHotelRoomTypeName(result) {

    HotelRoomTypeTableView.set_dataSource(result);
    HotelRoomTypeTableView.dataBind();
    if (result.length > 0) { HotelRoomTypeTableView.selectItem(0); }

    if (HotelRoomTypeComandName == "Filter" || HotelRoomTypeComandName == "Load") { CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelTypeCount(updateHotelRoomTypeVirtualCount); }
}

// Save Double Click
function addNewHotelRoomType(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = HotelRoomTypeTableView.get_dataItems()[currentRowIndex].findElement("ROOM_TYPE_NAME").value;
    ary[2] = HotelRoomTypeTableView.get_dataItems()[currentRowIndex].findElement("ROOM_SIZE").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.ROOM_TYPE_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.FareHotelRoomTypeWebService.InsertUpdateHotelRoom(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.FareHotelRoomTypeWebService.GetHotelType(0, HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateHotelRoomTypeName);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}