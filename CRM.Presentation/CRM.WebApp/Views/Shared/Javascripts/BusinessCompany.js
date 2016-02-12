var COMPANY_ID = "";
var BusinessCompanyTableView = null;
var BusinessCompanyCommand = "";

function radgridbusinesscompany_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    BusinessCompanyTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.BusinessCompanyWebService.GetBusinessCompany(BusinessCompanyTableView.get_currentPageIndex() * BusinessCompanyTableView.get_pageSize(), BusinessCompanyTableView.get_pageSize(), BusinessCompanyTableView.get_sortExpressions().toString(), BusinessCompanyTableView.get_filterExpressions().toDynamicLinq(), updateBusinessCompanyGrid);
    BusinessCompanyCommand = args.get_commandName;

}
function radgridbusinesscompany_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        COMPANY_ID = args.get_gridDataItem()._dataItem.COMPANY_ID;

    }
    catch (e) { }
}
function updateBusinessCompanyVirtualItemCount(result) {

    BusinessCompanyTableView.set_virtualItemCount(result);

}
function updateBusinessCompanyGrid(result) {

    BusinessCompanyTableView.set_dataSource(result);
    BusinessCompanyTableView.dataBind();
    if (result.length > 0) { BusinessCompanyTableView.selectItem(0); }

    if (BusinessCompanyCommand == "Filter" || BusinessCompanyCommand == "Load") { CRM.WebApp.webservice.BusinessCompanyWebService.GetBusinessCompanyCount(updateBusinessCompanyVirtualItemCount); }

}