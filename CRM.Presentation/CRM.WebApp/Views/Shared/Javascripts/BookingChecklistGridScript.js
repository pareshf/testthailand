var CHECKLIST_ID = "";
var SR_NO = "";
var BookingCheckTableView = null;
var BookingCheckListCommand = "";
var CheckListDetailsTableView = null;
var CheckListCommandName = "";

function radgridbookingChecklist_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    BookingCheckTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.BookingCheckListWebService.GetCheckList(BookingCheckTableView.get_currentPageIndex() * BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_sortExpressions().toString(), BookingCheckTableView.get_filterExpressions().toDynamicLinq(), updateCheckListGrid);
    BookingCheckListCommand = args.get_commandName;

}
function radgridChecklistdetails_Command(sender, args) {

}
function radgridbookingChecklist_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CHECKLIST_ID = args.get_gridDataItem()._dataItem.CHECKLIST_ID;
        loadCheckListDetail();

    }
    catch (e) { }

}
function radgridChecklistdetails_RowSelected(sender, args) {

    SR_NO = args.get_gridDataItem()._dataItem.SR_NO;
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    CheckListCommandName = "Load";

}
function updateCheckListVirtualItemCount(result) {

    BookingCheckTableView.set_virtualItemCount(result);

}
function updateCheckListGrid(result) {
   
    BookingCheckTableView.set_dataSource(result);
    BookingCheckTableView.dataBind();
    if (result.length > 0) { BookingCheckTableView.selectItem(0); }

    if (BookingCheckListCommand == "Filter" || BookingCheckListCommand == "Load") { CRM.WebApp.webservice.BookingCheckListWebService.GetCheckListCount(updateCheckListVirtualItemCount); }

}
function updateCheckListDetailVirtualItemCount(result) {

    CheckListDetailsTableView.set_virtualItemCount(result);

}
function updateCheckListDetail(result) {

    CheckListDetailsTableView.set_dataSource(result);
    CheckListDetailsTableView.dataBind();
    if (result.length > 0) { CheckListDetailsTableView.selectItem(0); }

    if (CheckListCommandName == "Filter" || CheckListCommandName == "Load") { CRM.WebApp.webservice.BookingCheckListWebService.GetCheckListDetailCount(updateCheckListDetailVirtualItemCount); }
}
function loadCheckListDetail() {

    CRM.WebApp.webservice.BookingCheckListWebService.GetCheckListDetails(CHECKLIST_ID, updateCheckListDetail);
}