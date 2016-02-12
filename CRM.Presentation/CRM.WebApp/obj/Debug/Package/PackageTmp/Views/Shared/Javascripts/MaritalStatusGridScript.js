var MARITAL_STATUS_ID = "";
var MaritalStatusTableView = null;
var MaritalStatusCommandName = "";

function radgridMaritalStatusmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    MaritalStatusTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.MaritalStatusMasterWebService.GetMaritalName(MaritalStatusTableView.get_currentPageIndex() * MaritalStatusTableView.get_pageSize(), MaritalStatusTableView.get_pageSize(), MaritalStatusTableView.get_sortExpressions().toString(), MaritalStatusTableView.get_filterExpressions().toDynamicLinq(), updateMaritalStatus);
    MaritalStatusCommandName = args.get_commandName;
}
function radgridMaritalStatusmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        MARITAL_STATUS_ID = args.get_gridDataItem()._dataItem.MARITAL_STATUS_ID;


    }
    catch (e) { }

}
function updateMaritalStatusVirtualItemCount(result) {

    MaritalStatusTableView.set_virtualItemCount(result);

}
function updateMaritalStatus(result) {

    MaritalStatusTableView.set_dataSource(result);
    MaritalStatusTableView.dataBind();
    if (result.length > 0) { MaritalStatusTableView.selectItem(0); }

    if (MaritalStatusCommandName == "Filter" || MaritalStatusCommandName == "Load") { CRM.WebApp.webservice.MaritalStatusMasterWebService.MaritalStatusCount(updateMaritalStatusVirtualItemCount); }

}