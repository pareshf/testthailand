var STATUS_ID = "";
var CompaignStatusCommand = "";
var CompaignStatusTableView = null;


function radgridCompaignStatus_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CompaignStatusTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CompaignStatusWebService.GetCompaignStatus(CompaignStatusTableView.get_currentPageIndex() * CompaignStatusTableView.get_pageSize(), CompaignStatusTableView.get_pageSize(), CompaignStatusTableView.get_sortExpressions().toString(), CompaignStatusTableView.get_filterExpressions().toDynamicLinq(), updatecompaignstatus);
    CompaignStatusCommand = args.get_commandName;

}
function radgridCompaignStatus_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        STATUS_ID = args.get_gridDataItem()._dataItem.STATUS_ID;


    }
    catch (e) { }
}
function updateCompaignStatusVirtualCount(result) { CompaignStatusTableView.set_virtualItemCount(result); }

function updatecompaignstatus(result) {

    CompaignStatusTableView.set_dataSource(result);
    CompaignStatusTableView.dataBind();
    if (result.length > 0) { CompaignStatusTableView.selectItem(0); }

    if (CompaignStatusCommand == "Filter" || CompaignStatusCommand == "Load") { CRM.WebApp.webservice.CompaignStatusWebService.GetStatusCount(updateCompaignStatusVirtualCount); }
}
/*double row Click save*/

function StatusRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = CompaignStatusTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.STATUS_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.CompaignStatusWebService.InsertUpdateCompaignStatus(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CompaignStatusWebService.GetCompaignStatus(CompaignStatusTableView.get_currentPageIndex() * CompaignStatusTableView.get_pageSize(), CompaignStatusTableView.get_pageSize(), CompaignStatusTableView.get_sortExpressions().toString(), CompaignStatusTableView.get_filterExpressions().toDynamicLinq(), updatecompaignstatus);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
