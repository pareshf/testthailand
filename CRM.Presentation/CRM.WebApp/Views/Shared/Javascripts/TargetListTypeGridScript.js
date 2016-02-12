var CUST_TYPE_ID = "";
var TargetListTypeCommand = "";
var TargetListTypeTableView = null;


function radgridtargetlisttype_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    TargetListTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.TargetListTypeWebService.GetTargetListType(TargetListTypeTableView.get_currentPageIndex() * TargetListTypeTableView.get_pageSize(), TargetListTypeTableView.get_pageSize(), TargetListTypeTableView.get_sortExpressions().toString(), TargetListTypeTableView.get_filterExpressions().toDynamicLinq(), updateTargetlistType);
    TargetListTypeCommand = args.get_commandName;

}
function radgridtargetlisttype_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CUST_TYPE_ID = args.get_gridDataItem()._dataItem.CUST_TYPE_ID;


    }
    catch (e) { }
}
function updateTargetlistVirtualCount(result) { TargetListTypeTableView.set_virtualItemCount(result); }

function updateTargetlistType(result) {

    TargetListTypeTableView.set_dataSource(result);
    TargetListTypeTableView.dataBind();
    if (result.length > 0) { TargetListTypeTableView.selectItem(0); }

    if (TargetListTypeCommand == "Filter" || TargetListTypeCommand == "Load") { CRM.WebApp.webservice.AddressTypeMasterWebService.GetAddressTypeCount(updateTargetlistVirtualCount); }
}

