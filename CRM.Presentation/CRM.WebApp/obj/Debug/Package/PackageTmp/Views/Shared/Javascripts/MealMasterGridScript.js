var MEAL_ID = "";
var MealTableView = null;
var MealCommandName = "";

function radgridmeal_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    MealTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.MealMasterWebService.GetMealType(MealTableView.get_currentPageIndex() * MealTableView.get_pageSize(), MealTableView.get_pageSize(), MealTableView.get_sortExpressions().toString(), MealTableView.get_filterExpressions().toDynamicLinq(), updateMeal);
    MealCommandName = args.get_commandName;

}
function radgridmeal_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        MEAL_ID = args.get_gridDataItem()._dataItem.MEAL_ID;


    }
    catch (e) { }
}
function updatemealVirtualCount(result) { MealTableView.set_virtualItemCount(result); }

function updateMeal(result) {
    
    MealTableView.set_dataSource(result);
    MealTableView.dataBind();
    if (result.length > 0) { MealTableView.selectItem(0); }

    if (MealCommandName == "Filter" || MealCommandName == "Load") { CRM.WebApp.webservice.MealMasterWebService.GetMealTypeCount(updatemealVirtualCount); }
}

