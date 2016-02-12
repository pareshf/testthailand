var AGENT_ID = "";
var CompititorCommandName = "";
var CompititorTableView = null;


function radgridCompitiorAgent_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CompititorTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompititorAgentWebService.GetCompititor(CompititorTableView.get_currentPageIndex() * CompititorTableView.get_pageSize(), CompititorTableView.get_pageSize(), CompititorTableView.get_sortExpressions().toString(), CompititorTableView.get_filterExpressions().toDynamicLinq(), updateCompititorAgent);
    AddressTypeCommand = args.get_commandName;

}
function radgridCompitiorAgent_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        AGENT_ID = args.get_gridDataItem()._dataItem.AGENT_ID;


    }
    catch (e) { }
}
function updateCompititorVirtualCount(result) { CompititorTableView.set_virtualItemCount(result); }

function updateCompititorAgent(result) {

    CompititorTableView.set_dataSource(result);
    CompititorTableView.dataBind();
    if (result.length > 0) { CompititorTableView.selectItem(0); }

    if (CompititorCommandName == "Filter" || CompititorCommandName == "Load") { CRM.WebApp.webservice.CompititorAgentWebService.GetCompititorCount(updateCompititorVirtualCount); }
}
