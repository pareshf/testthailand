var CURRENCY_ID = "";
var CurrencyCommandName = "";
var CurrencyTableView = null;


function radgridbookingcurrencymaster_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    CurrencyTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.BookingCurrencyMaster.GetCurrency(CurrencyTableView.get_currentPageIndex() * CurrencyTableView.get_pageSize(), CurrencyTableView.get_pageSize(), CurrencyTableView.get_sortExpressions().toString(), CurrencyTableView.get_filterExpressions().toDynamicLinq(), updateCurrencyTypeName);
    CurrencyCommandName = args.get_commandName;

}
function radgridbookingcurrencymaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CURRENCY_ID = args.get_gridDataItem()._dataItem.CURRENCY_ID;


    }
    catch (e) { }
}
function updateCurrencyVirtualCount(result) { CurrencyTableView.set_virtualItemCount(result); }

function updateCurrencyTypeName(result) {

    CurrencyTableView.set_dataSource(result);
    CurrencyTableView.dataBind();
    if (result.length > 0) { CurrencyTableView.selectItem(0); }

    if (CurrencyCommandName == "Filter" || CurrencyCommandName == "Load") { CRM.WebApp.webservice.BookingCurrencyMaster.GetBookingCurrencyCount(updateCurrencyVirtualCount); }
}
