var VISA_QUOTE_ID = "";
var VisaQuoteTableView = null;
var VisaQuoteCommand = "";

function radgridfarevisaquote_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    VisaQuoteTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.VisaQuoteWebService.GetVisaQuote(VisaQuoteTableView.get_currentPageIndex() * VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_pageSize(), VisaQuoteTableView.get_sortExpressions().toString(), VisaQuoteTableView.get_filterExpressions().toDynamicLinq(), updatevisaQuote);
    VisaQuoteCommand = args.get_commandName;
}
function radgridfarevisaquote_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        VISA_QUOTE_ID = args.get_gridDataItem()._dataItem.VISA_QUOTE_ID;


    }
    catch (e) { }

}
function updateVisaQuoteVirtualItemCount(result) {

    VisaQuoteTableView.set_virtualItemCount(result);

}
function updatevisaQuote(result) {

    VisaQuoteTableView.set_dataSource(result);
    VisaQuoteTableView.dataBind();
    if (result.length > 0) { VisaQuoteTableView.selectItem(0); }

    if (VisaQuoteCommand == "Filter" || VisaQuoteCommand == "Load") { CRM.WebApp.webservice.VisaQuoteWebService.GetVisaQuoteCount(updateVisaQuoteVirtualItemCount); }

}