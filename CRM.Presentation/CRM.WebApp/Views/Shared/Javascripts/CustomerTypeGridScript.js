var CUST_TYPE_ID = "";
var CustomerTypeTableView = null;
var CustomerCommandName = "";

function radgridcustomertype_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CustomerTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CustomerTypeWebService.GetCustomerType(CustomerTypeTableView.get_currentPageIndex() * CustomerTypeTableView.get_pageSize(), CustomerTypeTableView.get_pageSize(), CustomerTypeTableView.get_sortExpressions().toString(), CustomerTypeTableView.get_filterExpressions().toDynamicLinq(), updatecustomerType);
    CustomerCommandName = args.get_commandName;
}
function radgridcustomertype_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CUST_TYPE_ID = args.get_gridDataItem()._dataItem.CUST_TYPE_ID;


    }
    catch (e) { }

}
function updatecustomerTypeVirtualCount(result) { CustomerTypeTableView.set_virtualItemCount(result); }

function updatecustomerType(result) {

    CustomerTypeTableView.set_dataSource(result);
    CustomerTypeTableView.dataBind();
    if (result.length > 0) { CustomerTypeTableView.selectItem(0); }

    if (CustomerCommandName == "Filter" || CustomerCommandName == "Load") { CRM.WebApp.webservice.CustomerTypeWebService.GetCustomerTypeCount(updatecustomerTypeVirtualCount); }
}