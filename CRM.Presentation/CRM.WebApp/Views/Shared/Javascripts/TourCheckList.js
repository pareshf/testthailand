var SR_NO = "";
var TourCheckListTableView = null;
var TourCheckListCommand = "";
var BookingTableView = null;
var BookingCommand = "";
var booking_id = 0;
var rowIndex;
var globalvalue = "";
var TOUR_ID = 0;

function radgridtourchecklist_Command(sender, args) {

}
function radgridPassangerMaster_Command(sender, args) {

}
function radgridPassangerMaster_RowSelected(sender, args) {
    booking_id = args.get_gridDataItem()._dataItem.BOOKING_ID;
    CRM.WebApp.webservice.TourBookingCheckList.GetCheckListName(booking_id,updateTourCheckList);
}
function radgridtourchecklist_RowSelected(sender, args) {
   
}
function updateCheckListVirtualCount(result) { TourCheckListTableView.set_virtualItemCount(result); }

function updateBookingVirtualCount(result) { BookingTableView.set_virtualItemCount(result); }

function updateTourCheckList(result) {

    TourCheckListTableView.set_dataSource(result);
    TourCheckListTableView.dataBind();
    if (result.length > 0) { TourCheckListTableView.selectItem(0); }

    if (TourCheckListCommand == "Filter" || TourCheckListCommand == "Load") { CRM.WebApp.webservice.TourBookingCheckList.GetCheckListNameCount(updateCheckListVirtualCount); }
}
function updatebookingchecklist(result) {

    BookingTableView.set_dataSource(result);
    BookingTableView.dataBind();
    if (result.length > 0) { BookingTableView.selectItem(0); }

    if (BookingCommand == "Filter" || BookingCommand == "Load") { CRM.WebApp.webservice.TourBookingCheckList.GetBookingCheckListCount(updateBookingVirtualCount); }

}