var CUST_UNQ_ID = "";
var customermasterTableView = null;
var customermasterCommandName = "";
var scomapany = "";
var scity = "";
var scode = "";
var stype = "";
var sbranch = "";
var semp = "";
var scommode = "";
var srelation = "";
var suniquetid = "";
var sfname = "";
var slname = "";
var semail = "";
var smob = "";
var stele = "";

function radgridcustomermaster_Command(sender, args) {
    args.set_cancel(true);
    CRM.WebApp.webservice.CustomerInquiriesReport.GetCustomer(customermasterTableView.get_currentPageIndex() * customermasterTableView.get_pageSize(), customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, srelation, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);
    customermasterCommandName = args.get_commandName();
}
function radgridcustomermaster_RowSelected(sender, args) {
    
    CUST_UNQ_ID = args.get_gridDataItem()._dataItem.CUST_UNQ_ID;
    
}
function updateCustGrid(result) {
    
    customermasterTableView.set_dataSource(result);
    customermasterTableView.dataBind();
    if (result.length > 0) { customermasterTableView.selectItem(0); }
    if (customermasterCommandName == "Filter" || customermasterCommandName == "Load") { CRM.WebApp.webservice.CustomerMasterWebService.GetCustCount(updateVirtualItemCount); }
}
function updateVirtualItemCount(result) {
    customermasterTableView.set_virtualItemCount(result);
}