var CHART_OF_ACCOUNTS_ID = "";
var ChartAccountTableView = null;
var ChartAccountCommandName = "";
var m = "";

function radgridchartofaccount_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    ChartAccountTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ChartsofAccountsWebService.GetChartsofAccounts(ChartAccountTableView.get_currentPageIndex() * ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_sortExpressions().toString(), ChartAccountTableView.get_filterExpressions().toDynamicLinq(), updatechartofAccounts);
    ChartAccountCommandName = args.get_commandName;

}
function radgridchartofaccount_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CHART_OF_ACCOUNTS_ID = args.get_gridDataItem()._dataItem.CHART_OF_ACCOUNTS_ID;

    }
    catch (e) { }
}
function updateChartsofAccountVirtualItemCount(result) {

    ChartAccountTableView.set_virtualItemCount(result);

}
function updatechartofAccounts(result) {

    ChartAccountTableView.set_dataSource(result);
    ChartAccountTableView.dataBind();
    if (result.length > 0) { ChartAccountTableView.selectItem(0); }

    if (ChartAccountCommandName == "Filter" || ChartAccountCommandName == "Load") { CRM.WebApp.webservice.ChartsofAccountsWebService.ChartsofAccountCount(updateChartsofAccountVirtualItemCount); }

}
function chartsofaccount(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];
    ary[1] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("GL_CODE").value;
    ary[2] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("GL_DESCRIPTION").value;
    ary[3] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("GROUP_NAME").value;
    ary[4] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("SIDE_CODE_NAME").value;
    ary[5] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("OP_BALANCE").value;
    ary[6] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("OP_BAL_TYPE").value;
    ary[7] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("OP_BALANCE_MONTH").value;
   // ary[8] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("CL_BALANCE").value;
    //ary[9] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("CL_BAL_TYPE").value;
    ary[10] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("CL_BALANCE_MONTH").value;
    ary[11] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    ary[12] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("SUPPLIER_AGENT_ID").value;
    ary[13] = ChartAccountTableView.get_dataItems()[currentRowIndex].findElement("ACC_NAME").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CHART_OF_ACCOUNTS_ID;
    //                for (i = 0; i < 8; i++) {
    //                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    //                }
    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    try {
        CRM.WebApp.webservice.ChartsofAccountsWebService.InsertUpdateChartAccount(ary,m,OnCallBack1);
        //alert('Record Save Successfully');
        CRM.WebApp.webservice.ChartsofAccountsWebService.GetChartsofAccounts(0, ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_sortExpressions().toString(), ChartAccountTableView.get_filterExpressions().toDynamicLinq(), updatechartofAccounts);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
    function OnCallBack1(results, userContext, sender) {

        
        if (results == "N") {

            alert('This GL Code Already Exist.');
        }
        if (results == "NA") {

            alert('This GL Description Already Exist.');
        }
        else if (results == "Y") {

            alert('Record Save Successfully');
        }
        else if (results == "") {
            alert('Record Save Successfully');
        }

    }
}