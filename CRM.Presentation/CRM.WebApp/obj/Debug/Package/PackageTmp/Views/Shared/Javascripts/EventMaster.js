var EVENT_ID = "";
var EventTypeCommand = "";
var EventTableView = null;


function radgridEvent_Command(sender, args) {
    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    EventTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.EventMasterWebService.GetEvent(EventTableView.get_currentPageIndex() * EventTableView.get_pageSize(), EventTableView.get_pageSize(), EventTableView.get_sortExpressions().toString(), EventTableView.get_filterExpressions().toDynamicLinq(), updateEvent);
    EventTypeCommand = args.get_commandName;

}
function radgridEvent_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        EVENT_ID = args.get_gridDataItem()._dataItem.EVENT_ID;


    }
    catch (e) { }
}
function updateEventVirtualCount(result) { EventTableView.set_virtualItemCount(result); }

function updateEvent(result) {

    EventTableView.set_dataSource(result);
    EventTableView.dataBind();
    if (result.length > 0) { EventTableView.selectItem(0); }

    if (EventTypeCommand == "Filter" || EventTypeCommand == "Load") { CRM.WebApp.webservice.EventMasterWebService.EventCount(updateEventVirtualCount); }
}

// Double Click Save Data

function addEvent(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = EventTableView.get_dataItems()[currentRowIndex].findElement("EVENT_NAME").value;
    ary[2] = EventTableView.get_dataItems()[currentRowIndex].findElement("FORM_NAME").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EVENT_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.EventMasterWebService.InsertUpdateEvent(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.EventMasterWebService.GetEvent(0, EventTableView.get_pageSize(), EventTableView.get_sortExpressions().toString(), EventTableView.get_filterExpressions().toDynamicLinq(), updateEvent);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}