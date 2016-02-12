var MYTASK_ID = "";
var taskCommandName = "";
var empCommandname = "";
var empTabelView = null;
var taskTabelView = null;

function radgritaskdmaster_Command(sender, args) {

    //args.set_Cancel(true);
    CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskDetails(taskTabelView.get_currentPageIndex() * taskTabelView.get_pageSize(), taskTabelView.get_pageSize(), taskTabelView.get_sortExpressions().toString(), taskTabelView.get_filterExpressions().toDynamicLinq(), updatetaskGrid);
    taskCommandName = args.get_commandName;
}

function radgritaskdmaster_RowSelected(sender, args) {
    
    try {
        MYTASK_ID = args.get_gridDataItem()._dataItem.MYTASK_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    }
    catch (e) { } //Array.remove($find())
}


function updateVirtualItemCount(result) { taskTabelView.set_virtualItemCount(result); }

function updatetaskGrid(result) {
    taskTabelView.set_dataSource(result);
    taskTabelView.dataBind();
    if (result.length > 0) { taskTabelView.selectItem(0); }
    if (taskCommandName == "Filter" || taskCommandName == "Load") { CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskCount(updateVirtualItemCount); }
}
function loadTask() {
    taskCommandName = "Load";
    CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskDetails(MYTASK_ID, updatetaskgrid);
    
}
