var ADDRESS_TYPE_ID = "";
var customersCommandName = "";
var customersTableView = null;
var searchparam = "";

function radgridmaster_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.AddressTypeLookup.GetAddressType(customersTableView.get_currentPageIndex() * customersTableView.get_pageSize(), customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), searchparam, updateGrid);
    customersCommandName = args.get_commandName();
}

function radgridmaster_RowSelected(sender, args) {
    try {

        ADDRESS_TYPE_ID = args.get_gridDataItem()._dataItem.ADDRESS_TYPE_ID; currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
       
           }
    catch (e) { }
}


function updateGrid(result) {
    customersTableView.set_dataSource(result); customersTableView.dataBind(); if (result.length > 0) { customersTableView.selectItem(0); ADDRESS_TYPE_ID = result[0]["ADDRESS_TYPE_ID"]; }
    else { ADDRESS_TYPE_ID = ""; }
    if (customersCommandName == "Filter" || customersCommandName == "Load") { CRM.WebApp.webservice.AddressTypeLookup.GetCustomersCount(updateVirtualItemCount); }
}

