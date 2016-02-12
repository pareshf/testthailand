var ROOM_TYPE_ID = "";
var HotelRoomTypeTableView = null;
var HotelRoomTypeCommand = "";


function radgridhotelroomtype_Command(sender, args) {
    
    pageSize = sender.get_masterTableView().get_pageSize();
    HotelRoomTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.RoomTypeMasterWebService.GetHotelRoomType(HotelRoomTypeTableView.get_currentPageIndex() * HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_pageSize(), HotelRoomTypeTableView.get_sortExpressions().toString(), HotelRoomTypeTableView.get_filterExpressions().toDynamicLinq(), updateRoomType);
    HotelRoomTypeCommand = args.get_commandName;

}
function radgridhotelroomtype_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        ROOM_TYPE_ID = args.get_gridDataItem()._dataItem.ROOM_TYPE_ID;


    }
    catch (e) { }
}
function updateHotelRoomTypeVirtualCount(result) { HotelRoomTypeTableView.set_virtualItemCount(result); }

function updateRoomType(result) {

    HotelRoomTypeTableView.set_dataSource(result);
    HotelRoomTypeTableView.dataBind();
    if (result.length > 0) { HotelRoomTypeTableView.selectItem(0); }

    if (HotelRoomTypeCommand == "Filter" || HotelRoomTypeCommand == "Load") { CRM.WebApp.webservice.RoomTypeMasterWebService.GetRoomTypeCount(updateHotelRoomTypeVirtualCount); }
}
