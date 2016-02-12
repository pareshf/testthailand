var CATEGORY_ID = "";
var CruiseCarbinCategoryCommand = "";
var CruiseCarbinCategoryTableView = null;

function radgridFareCruiseCarbinCategoryMaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    CruiseCarbinCategoryTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FareCruiseCarbinCategoryMaster.GetCategoryType(CruiseCarbinCategoryTableView.get_currentPageIndex() * CruiseCarbinCategoryTableView.get_pageSize(), CruiseCarbinCategoryTableView.get_pageSize(), CruiseCarbinCategoryTableView.get_sortExpressions().toString(), CruiseCarbinCategoryTableView.get_filterExpressions().toDynamicLinq(), updateCruiseCarbinCategory);
    CruiseCarbinCategoryCommand = args.get_commandName;

}

function radgridFareCruiseCarbinCategoryMaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CATEGORY_ID = args.get_gridDataItem()._dataItem.CATEGORY_ID;


    }
    catch (e) { }
}
function updateCruiseCarbinCategoryVirtualCount(result) { CruiseCarbinCategoryTableView.set_virtualItemCount(result); }

function updateCruiseCarbinCategory(result) {

    CruiseCarbinCategoryTableView.set_dataSource(result);
    CruiseCarbinCategoryTableView.dataBind();
    if (result.length > 0) { CruiseCarbinCategoryTableView.selectItem(0); }

    if (CruiseCarbinCategoryCommand == "Filter" || CruiseCarbinCategoryCommand == "Load") { CRM.WebApp.webservice.FareCruiseCarbinCategoryMaster.GetCategoryTypeCount(updateCruiseCarbinCategoryVirtualCount); }
}
// Double Click Save

function addCategory(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();
    var ary = [];
    ary[1] = CruiseCarbinCategoryTableView.get_dataItems()[currentRowIndex].findElement("CATEGORY_CODE").value;
    ary[2] = CruiseCarbinCategoryTableView.get_dataItems()[currentRowIndex].findElement("CATEGORY_DESC").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CATEGORY_ID;
    for (i = 0; i < 3; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.FareCruiseCarbinCategoryMaster.InsertUpdateACategoryType(ary);
        CRM.WebApp.webservice.FareCruiseCarbinCategoryMaster.GetCategoryType(0, CruiseCarbinCategoryTableView.get_pageSize(), CruiseCarbinCategoryTableView.get_sortExpressions().toString(), CruiseCarbinCategoryTableView.get_filterExpressions().toDynamicLinq(), updateCruiseCarbinCategory);

        alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}