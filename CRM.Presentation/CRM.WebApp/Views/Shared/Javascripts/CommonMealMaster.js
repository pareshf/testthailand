var MEAL_ID = "";
var CommonMealCommandName = "";
var CommonMealTableView = null;

function radgridcommonmealmaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CommonMealTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CommonMealMasterWebService.GetCommonMeal(CommonMealTableView.get_currentPageIndex() * CommonMealTableView.get_pageSize(), CommonMealTableView.get_pageSize(), CommonMealTableView.get_sortExpressions().toString(), CommonMealTableView.get_filterExpressions().toDynamicLinq(), updateCommonMealName);
    CommonMealCommandName = args.get_commandName;

}
function radgridcommonmealmaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        MEAL_ID = args.get_gridDataItem()._dataItem.MEAL_ID;


    }
    catch (e) { }
}
function updateCommonMealVirtualCount(result) { CommonMealTableView.set_virtualItemCount(result); }

function updateCommonMealName(result) {

    CommonMealTableView.set_dataSource(result);
    CommonMealTableView.dataBind();
    if (result.length > 0) { CommonMealTableView.selectItem(0); }

    if (CommonMealCommandName == "Filter" || CommonMealCommandName == "Load") { CRM.WebApp.webservice.CommonMealMasterWebService.GetCommonMealCount(updateCommonMealVirtualCount); }
}

// Save On Double Click

function addMyCommonMeal(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = CommonMealTableView.get_dataItems()[currentRowIndex].findElement("MEAL_DESC").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.MEAL_ID;
    for (i = 0; i < 2; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.CommonMealMasterWebservice.InsertUpdateCommonMeal(ary);
        CRM.WebApp.webservice.CommonMealMasterWebservice.GetCommonMeal(0, CommonMealTableView.get_pageSize(), CommonMealTableView.get_sortExpressions().toString(), CommonMealTableView.get_filterExpressions().toDynamicLinq(), updateCommonMealName);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}