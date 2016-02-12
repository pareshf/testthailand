var VISA_TYPE_ID = "";
var VisaTableView = null;
var VisaCommandName = "";

function radgridvisatype_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    VisaTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.VisaTypeWebService.GetVisaType(VisaTableView.get_currentPageIndex() * VisaTableView.get_pageSize(), VisaTableView.get_pageSize(), VisaTableView.get_sortExpressions().toString(), VisaTableView.get_filterExpressions().toDynamicLinq(), updatevisa);
    VisaCommandName = args.get_commandName;
}
function radgridvisatype_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        VISA_TYPE_ID = args.get_gridDataItem()._dataItem.VISA_TYPE_ID;


    }
    catch (e) { }

}
function updateVisaVirtualItemCount(result) {

    VisaTableView.set_virtualItemCount(result);

}
function updatevisa(result) {

    VisaTableView.set_dataSource(result);
    VisaTableView.dataBind();
    if (result.length > 0) { VisaTableView.selectItem(0); }

    if (VisaCommandName == "Filter" || VisaCommandName == "Load") { CRM.WebApp.webservice.VisaTypeWebService.GetVisaTypeCount(updateVisaVirtualItemCount); }

}