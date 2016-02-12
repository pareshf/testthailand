var HOTEL_DASHBOARD_ID = "";
var HotelCommandName = "";
var HotelTableView = null;
var globalvalue = "";

function radgridNews_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    HotelTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);
    departmentCommandName = args.get_commandName;

}
function radgridNews_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        HOTEL_DASHBOARD_ID = args.get_gridDataItem()._dataItem.HOTEL_DASHBOARD_ID;


    }
    catch (e) { }
}
function updateNewsVirtualCount(result) { HotelTableView.set_virtualItemCount(result); }

function updateNews(result) {

    HotelTableView.set_dataSource(result);
    HotelTableView.dataBind();
    if (result.length > 0) { HotelTableView.selectItem(0); }

    if (HotelCommandName == "Filter" || HotelCommandName == "Load") { CRM.WebApp.webservice.HotelDashboardMaster.GetNewsCount(updateNewsVirtualCount); }
}


// Double Click Save
function addNewsMaster(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[2] = HotelTableView.get_dataItems()[currentRowIndex].findElement("CHAIN_NAME").value;
    ary[3] = HotelTableView.get_dataItems()[currentRowIndex].findElement("IS_DASHBOARD").value;
    ary[4] = HotelTableView.get_dataItems()[currentRowIndex].findElement("DESCRIPTION").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.HOTEL_DASHBOARD_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.HotelDashboardMaster.InsertUpdatehotels(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.HotelDashboardMaster.GetHotel(0, HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateNews);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
