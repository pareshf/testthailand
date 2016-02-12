var AGENT_COMMISION_ID = "";
var AgentCommandName = "";
var AgentTableView = null;


function radgridAgentcommisionmaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    AgentTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.AgentCommisionMaster.GetAgentcommision(AgentTableView.get_currentPageIndex() * AgentTableView.get_pageSize(), AgentTableView.get_pageSize(), AgentTableView.get_sortExpressions().toString(), AgentTableView.get_filterExpressions().toDynamicLinq(), updateAgentTypeName);
    AgentCommandName = args.get_commandName;

}
function radgridAgentcommisionmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        AGENT_COMMISION_ID = args.get_gridDataItem()._dataItem.AGENT_COMMISION_ID;


    }
    catch (e) { }
}
function updateAgentVirtualCount(result) { AgentTableView.set_virtualItemCount(result); }

function updateAgentTypeName(result) {

    AgentTableView.set_dataSource(result);
    AgentTableView.dataBind();
    if (result.length > 0) { AgentTableView.selectItem(0); }

    if (AgentCommandName == "Filter" || AgentCommandName == "Load") { CRM.WebApp.webservice.AgentCommisionMaster.AgentcommisionCount(updateAgentVirtualCount); }
}
