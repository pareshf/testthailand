var FIT_BOOKING_CLOSED_ID = "";
var FIT_BOOKING_DAY_ID = "";
var FitClosedCommandName = "";
var FitClosedTableView = null;
var FitDayTableView = null;
var FitDayCommand = "";

function radgridfitbookingclosed_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    FitClosedTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(FitClosedTableView.get_currentPageIndex() * FitClosedTableView.get_pageSize(), FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);
    FitClosedCommandName = args.get_commandName;

}
function radgridfitbookingclosed_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        FIT_BOOKING_CLOSED_ID = args.get_gridDataItem()._dataItem.FIT_BOOKING_CLOSED_ID;


    }
    catch (e) { }
}
function radgridfitbookingDay_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    FitDayTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FitBookingClosedWebService.GetFitDay(FitDayTableView.get_currentPageIndex() * FitDayTableView.get_pageSize(), FitDayTableView.get_pageSize(), FitDayTableView.get_sortExpressions().toString(), FitDayTableView.get_filterExpressions().toDynamicLinq(), updatefitday);
    FitDayCommand = args.get_commandName;
}
function radgridfitbookingDay_RowSelected(sender, args) {

    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    FIT_BOOKING_DAY_ID = args.get_gridDataItem()._dataItem.FIT_BOOKING_DAY_ID;

}
function updateVirtualCountDay(result) {
    FitDayTableView.set_virtualItemCount(result);
}
function updateFitClosedVirtualCount(result) { FitClosedTableView.set_virtualItemCount(result); }

function updatefitClosed(result) {

    FitClosedTableView.set_dataSource(result);
    FitClosedTableView.dataBind();
    if (result.length > 0) { FitClosedTableView.selectItem(0); }

    if (FitClosedCommandName == "Filter" || FitClosedCommandName == "Load") { CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosedCount(updateFitClosedVirtualCount); }
}
function updatefitday(result) {

    FitDayTableView.set_dataSource(result);
    FitDayTableView.dataBind();
    if (result.length > 0) { FitDayTableView.selectItem(0); }

    if (FitDayCommand == "Filter" || FitDayCommand == "Load") { CRM.WebApp.webservice.FitBookingClosedWebService.GetFitDayCount(updateVirtualCountDay); }
}

// Double Click Save

function addfitbookingClosed(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = FitClosedTableView.get_dataItems()[currentRowIndex].findElement("FIT_PACKAGE_NAME").value;
    ary[2] = FitClosedTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[3] = FitClosedTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FIT_BOOKING_CLOSED_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.FitBookingClosedWebService.InsertUpdateFitClosed(ary);
        CRM.WebApp.webservice.FitBookingClosedWebService.GetFitClosed(0, FitClosedTableView.get_pageSize(), FitClosedTableView.get_sortExpressions().toString(), FitClosedTableView.get_filterExpressions().toDynamicLinq(), updatefitClosed);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}

function addDay(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = FitDayTableView.get_dataItems()[currentRowIndex].findElement("DAY").value;
    ary[2] = FitDayTableView.get_dataItems()[currentRowIndex].findElement("UP_TO_DATE").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FIT_BOOKING_DAY_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.FitBookingClosedWebService.InsertUpdateFitDay(ary);
        CRM.WebApp.webservice.FitBookingClosedWebService.GetFitDay(0, FitDayTableView.get_pageSize(), FitDayTableView.get_sortExpressions().toString(), FitDayTableView.get_filterExpressions().toDynamicLinq(), updatefitday);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}