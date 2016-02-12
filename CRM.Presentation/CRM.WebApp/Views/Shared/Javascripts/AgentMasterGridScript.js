var AGENT_ID = "";
var AgentTableView = null;
var AgentCommandName = "";

function radgridAgent_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    AgentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.AgentMaster.GetAgentName(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentGrid);
    AgentCommandName = args.get_commandName;

}
function radgridAgent_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        AGENT_ID = args.get_gridDataItem()._dataItem.AGENT_ID;

    }
    catch (e) { }

}
function updateAgentVirtualItemCount(result) {

    AgentTableView.set_virtualItemCount(result);

}
function updateAgentGrid(result) {
    
    AgentTableView.set_dataSource(result);
    AgentTableView.dataBind();
    if (result.length > 0) { AgentTableView.selectItem(0); }

    if (AgentCommandName == "Filter" || AgentCommandName == "Load") { CRM.WebApp.webservice.AgentMaster.AgentNameCount(updateAgentVirtualItemCount); }

}