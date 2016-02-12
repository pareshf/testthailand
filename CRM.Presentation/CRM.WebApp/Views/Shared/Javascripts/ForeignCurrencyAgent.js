var FOREIGN_CURRENCY_AGENT_ID = "";
var ForeignCurrencyAgentTableView = null;
var ForeignCurrencyAgentCommand = "";


function radgridforeigncurrencyAgent_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    ForeignCurrencyAgentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ForeignCurrencyAgentWebService.GetCurrencyAgentName(ForeignCurrencyAgentTableView.get_currentPageIndex() * ForeignCurrencyAgentTableView.get_pageSize(), ForeignCurrencyAgentTableView.get_pageSize(), ForeignCurrencyAgentTableView.get_sortExpressions().toString(), ForeignCurrencyAgentTableView.get_filterExpressions().toDynamicLinq(), updateforeignCurrencyAgent);
    ForeignCurrencyAgentCommand = args.get_commandName;
}
function radgridforeigncurrencyAgent_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        FOREIGN_CURRENCY_AGENT_ID = args.get_gridDataItem()._dataItem.FOREIGN_CURRENCY_AGENT_ID;


    }
    catch (e) { }
}
function updateCurrencyAgentVirtualItemCount(result) {

    ForeignCurrencyAgentTableView.set_virtualItemCount(result);

}
function updateforeignCurrencyAgent(result) {

    ForeignCurrencyAgentTableView.set_dataSource(result);
    ForeignCurrencyAgentTableView.dataBind();
    if (result.length > 0) { ForeignCurrencyAgentTableView.selectItem(0); }

    if (ForeignCurrencyAgentCommand == "Filter" || ForeignCurrencyAgentCommand == "Load") { CRM.WebApp.webservice.ForeignCurrencyAgentWebService.GetCurrencyAgentCount(updateCurrencyAgentVirtualItemCount); }

}